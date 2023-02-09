using System.Collections.Generic;

namespace NKShare.NKShareMod.NetAccCatch.PPPoECatch {
    class PppoEPacket {
        private readonly List<byte> _packet;
        private readonly List<byte> _tags;
        private byte[] _dstMac;
        private byte[] _srcMac;
        private byte[] _pppoeType;
        private byte _versionType;
        private byte _pppoeCode;
        private byte[] _session;
        private byte _paylodLength;
        public PppoEPacket() {
            _packet = new List<byte>();
            _tags = new List<byte>();
        }

        public PppoEPacket SetDstMac(byte[] mac) {
            _dstMac = mac;
            return this;
        }

        public PppoEPacket SetSrcMac(byte[] mac) {
            _srcMac = mac;
            return this;
        }

        public PppoEPacket SetPppoEType(byte[] type) {
            _pppoeType = type;
            return this;
        }

        public PppoEPacket SetVersionType(byte versionType) {
            _versionType = versionType;
            return this;
        }

        public PppoEPacket SetPppoECode(byte code) {
            _pppoeCode = code;
            return this;
        }

        public PppoEPacket SetSession(byte[] session) {
            _session = session;
            return this;
        }

        public PppoEPacket SetPaylodLength(byte length) {
            _paylodLength = length;
            return this;
        }

        public PppoEPacket SetTags(byte[] tags) {
            _tags.AddRange(tags);
            return this;
        }

        public byte[] GetResult() {
            _packet.AddRange(_dstMac);
            _packet.AddRange(_srcMac);
            _packet.AddRange(_pppoeType);
            _packet.Add(_versionType);
            _packet.Add(_pppoeCode);
            _packet.AddRange(_session);
            _packet.Add(0x00);
            _packet.Add(_paylodLength);
            _packet.AddRange(_tags);
            return _packet.ToArray();
        }

    }
}

