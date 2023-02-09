using NKShare.NKShareMod.NetAccCatch.PPPoECatch;

namespace NKShare.NKShareMod.NetAccCatch {
    public class NetAccCatch {
        public Catch Catch { get; }

        public NetAccCatch(NetAccCatchType netAccType) {
            switch (netAccType) {
                    case NetAccCatchType.Pacp:
                        Catch = new PacketCatch();
                        break;
            }
        }
    }
}