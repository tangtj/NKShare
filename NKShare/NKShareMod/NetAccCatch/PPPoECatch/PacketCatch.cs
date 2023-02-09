using System;
using System.Threading;
using System.Threading.Tasks;

namespace NKShare.NKShareMod.NetAccCatch.PPPoECatch {
    public class PacketCatch : Catch {
        private readonly PppoECatch _pppoECatch;
        //private static bool _isCatch; 

        public PacketCatch() {
            _pppoECatch = new PppoECatch();
        }

        public override void StartCatch(Call call) {
            _pppoECatch.StartCatch();
            NetAccPw netAccPw = null;
            Console.WriteLine("111");
            while (_pppoECatch.NetAccPw == null) {
                Thread.Sleep(100);
            }
            Console.WriteLine("222");
            _pppoECatch.StopCatch();
            netAccPw = _pppoECatch.NetAccPw;
            call(netAccPw);
        }

        public override void StopCatch() {
            _pppoECatch.StopCatch();
        }

        public NetAccPw GetNetAcc() {
            return _pppoECatch?.NetAccPw;
        }
    }
}