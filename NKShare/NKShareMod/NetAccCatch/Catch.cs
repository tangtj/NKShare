namespace NKShare.NKShareMod.NetAccCatch {
    public abstract class Catch {
        public delegate void Call(NetAccPw pw);
        public abstract void StartCatch(Call call);
        public abstract void StopCatch();
    }
}