using System;
using System.Collections.Generic;
using System.Text;
using PacketDotNet;

namespace NKShare.NKShareMod.NetAccCatch.PPPoECatch {
    internal class PppoECatch {
        public SharpPcap.WinPcap.WinPcapDevice Device { get; }

        private bool _lcpCatchopen = true;
        private bool _isFirstLcpPacket = true;
        private bool _finshRrquestAck;

        public NetAccPw NetAccPw;

        private readonly byte[] _lcpRequestByte = new byte[] { 0x11, 0x00, 0x00, 0x01, 0x00, 0x14, 0xc0, 0x21, 0x01, 0x00, 0x00, 0x12, 0x01, 0x04, 0x05, 0xc8, 0x03, 0x04, 0xc0, 0x23, 0x05, 0x06, 0x5e, 0x63, 0x0a, 0xb8 };

        public PppoECatch() {
            var networkInterface = Util.NetworkSuppoet.GetEthernetInterface();
            var devices = SharpPcap.WinPcap.WinPcapDeviceList.Instance;
            foreach (var item in devices) {
                item.Open();
                if (item.MacAddress.Equals(networkInterface.GetPhysicalAddress())) {
                    Device = item;
                    item.Close();
                    break;
                }
                item.Close();
            }
        }

        public void StopCatch() {
            if (Device != null && Device.Opened) {
                Device.StopCapture();
            }
        }

        public bool StartCatch() {
            if (Device == null)
                return false;
            Device.OnPacketArrival += Device_OnPacketArrival;
            Device.Open(SharpPcap.DeviceMode.Promiscuous, 200);
            Device.Filter = "pppoed||pppoes";
            Device.StartCapture();
            return true;
        }

        private void SendPacket(byte[] data) {
            if (data == null || Device == null) return;
            Device.SendPacket(data);
        }


        private void Device_OnPacketArrival(object sender, SharpPcap.CaptureEventArgs e) {
            var packet = e.Packet.Data;
            DoReply(packet);
        }

        private void DoReply(byte[] packet) {
            byte[] data = new byte[packet.Length - EthernetFields.HeaderLength];
            Array.Copy(packet, EthernetFields.HeaderLength, data, 0, data.Length);
            int type = (packet[EthernetFields.TypePosition] << 8) + packet[EthernetFields.TypePosition + 1];
            EthernetPacketType packetType = (EthernetPacketType)type;
            switch (packetType) {
                case EthernetPacketType.PointToPointProtocolOverEthernetDiscoveryStage:
                    DoReply8863(data);
                    break;
                case EthernetPacketType.PointToPointProtocolOverEthernetSessionStage:
                    var lcptype = (data[6] << 8) + data[7];
                    if (lcptype == 0xc021) {
                        DoReply8864(data);
                    } else if (lcptype == 0xc023) {
                        NetAccPw = GetAcc(data);
                        DoReply8863(data);
                        StopCatch();
                    }
                    break;
            }
        }

        private byte[] BuildRequest8863Bytes(byte[] data) {
            List<byte> tags = new List<byte>();
            byte code;
            byte[] sessionid = { 0x00, 0x00 };
            byte[] pppoetype = { 0x88, 0x63 };
            byte[] tagstitle = { 0x01, 0x01, 0x00, 0x00 };
            byte[] tagspppoesrv = { 0x01, 0x02, 0x00, 0x08, 0x50, 0x50, 0x50, 0x4f, 0x45, 0x53, 0x52, 0x56 };
            switch (data[PPPoEFields.CodePosition]) {
                case 0x09:
                    code = (byte)PPPoECode.ActiveDiscoveryOffer;
                    tags.AddRange(tagstitle);
                    tags.AddRange(NetAccCatchUtil.GethostUniq(data));
                    tags.AddRange(tagspppoesrv);
                    break;
                case 0x19:
                    code = 0x65;
                    sessionid[1] = 0x01;
                    tags.AddRange(tagstitle);
                    tags.AddRange(NetAccCatchUtil.GethostUniq(data));
                    tags.AddRange(tagspppoesrv);
                    break;
                case 0x00:
                    code = 0xa7;
                    sessionid[1] = 0x01;
                    break;
                default:
                    return null;
            }
            var paylodLength = (byte)tags.ToArray().Length;
            var requestdata = new PppoEPacket()
                .SetDstMac(Device.MacAddress.GetAddressBytes()).SetSrcMac(Device.MacAddress.GetAddressBytes())
                .SetVersionType(0x11).SetPppoEType(pppoetype)
                .SetPppoECode(code).SetSession(sessionid)
                .SetPaylodLength(paylodLength)
                .SetTags(tags.ToArray())
                .GetResult();
            return requestdata;
        }

        private byte[] BuildRequest8864Bytes(byte[] data) {
            byte[] reqByte;
            if (_isFirstLcpPacket) {
                reqByte = new byte[data.Length];
                Array.Copy(data, reqByte, reqByte.Length);
                reqByte[8] = 0x4;
                _isFirstLcpPacket = false;
            } else {
                reqByte = new byte[data.Length];
                Array.Copy(data, reqByte, reqByte.Length);
                reqByte[8] = 0x2;
                _finshRrquestAck = true;
            }
            EthernetPacket packet = new EthernetPacket(Device.MacAddress, Device.MacAddress, EthernetPacketType.PointToPointProtocolOverEthernetSessionStage) {
                PayloadData = reqByte
            };
            return packet.Bytes;
        }

        private void DoReply8864(byte[] data) {
            if (data[8] != 0x01 || !_lcpCatchopen)
                return;
            var reqBytes = BuildRequest8864Bytes(data);
            SendPacket(reqBytes);
            if (_finshRrquestAck) {
                EthernetPacket tpacket = new EthernetPacket(Device.MacAddress, Device.MacAddress, EthernetPacketType.PointToPointProtocolOverEthernetSessionStage) {
                    PayloadData = _lcpRequestByte
                };
                SendPacket(tpacket.Bytes);
                _lcpCatchopen = false;
            }
        }

        private void DoReply8863(byte[] data) {
            var reqBytes = BuildRequest8863Bytes(data);
            SendPacket(reqBytes);
        }

        private static NetAccPw GetAcc(byte[] data) {
            var length = (data[10] << 8) + data[11];
            byte[] accPw = new byte[length];
            Array.Copy(data, 8, accPw, 0, length);
            var acclength = accPw[4];
            var pwlength = accPw[4 + acclength + 1];
            var acc = new byte[acclength];
            var pw = new byte[pwlength];
            Array.Copy(accPw, 4 + 1, acc, 0, acclength);
            Array.Copy(accPw, 4 + 1 + acclength + 1, pw, 0, pwlength);
            var stracc = Encoding.ASCII.GetString(acc);
            var strpw = Encoding.ASCII.GetString(pw);
            var tmp = new NetAccPw(stracc, strpw);
            return tmp;
        }
    }
}