using System;
using NKShare.Control.Data;
using NKShare.Form;
using NKShare.NKShareMod.Config;
using NKShare.NKShareMod.Licence;
using NKShare.NKShareMod.NetAccCatch;
using NKShare.NKShareMod.NKtool;
using NKShare.NKShareMod.Router;
using NKShare.NKShareMod.Util;
using System.Net.NetworkInformation;
using static NKShare.Control.StatusTips.StatusTips;

namespace NKShare.Control {
    public class Control {
        private readonly Nktool _nktool;
        private readonly NetAccCatch _netAccCatch;
        private readonly Licence _licence;

        private const string Path = @"C:\ProgramData\licence";


        public readonly StatusTips.StatusTips Tips;

        public RuntimeData Data { get; }

        public LicenceStatus LicenceStatus { get; private set; }

        public Control() {
            Data = new RuntimeData(Config.GetConfigData());
            Tips = new StatusTips.StatusTips();
            _nktool = new Nktool();
            _netAccCatch = new NetAccCatch(NetAccCatchType.Pacp);
            _licence = new Licence(Path);
        }

        //TODO:未完成,明天继续
        public bool StartNk() {
            if (_nktool.IsAlive()) {
                return true;
            }
            bool result = _nktool.FindNk();
            if (!result) {
                Tips.SetStatus(StatusStyle.Waring, "nk未安装", 20);
                return false;
            }
            Tips.SetStatus(StatusStyle.Normal, "正在启动nk", 0);
            result = _nktool.StartNk();
            if (!result) {
                Tips.SetStatus(StatusStyle.Error, "NK启动失败", 102);
                return false;
            }
            return true;
        }

        public bool NetCatch() {
            bool result = false;
            Console.WriteLine("3");
            _netAccCatch.Catch.StartCatch(pw => {
                Tips.SetStatus(StatusStyle.Normal, "路由器拨号中", 0);
                //这里写拨号的东西
                routerP router = new routerP(Data.RouterIp);
                Console.WriteLine(router.ToString());
                if (router.IsSupportRouterType()) {
                    result = true;
                    Console.WriteLine($@"{pw.NetAcc}-{pw.NetPw}");
                    router.setInternetMode_PPPOE(Data.RouterAcc, Data.RouterPwd, pw.NetAcc, pw.NetPw, 0, 1);
                } else {
                    Tips.SetStatus(StatusStyle.Waring, "路由器不支持", 99);
                }

            });
            return result;
        }

        /// <summary>
        /// 关闭所有服务,于关闭窗口时执行
        /// </summary>
        public void ClosingAll() {
            _netAccCatch.Catch.StopCatch();
            if (_nktool.IsAlive()) {
                _nktool.KillNk();
            }
        }

        /// <summary>
        /// 得到网关ip
        /// </summary>
        /// <returns>网关ip</returns>
        public string GetRouterIp() {
            return NetworkSuppoet.GetRouterIp();
        }

        /// <summary>
        /// 保存数据配置
        /// </summary>
        public void SaveConfig() {
            Config.SaveConfigData(Data.GetConfigData());
        }

        /// <summary>
        /// 检查网卡是否连接网络
        /// </summary>
        /// <returns>检查网卡是否连接网络</returns>
        public bool CheckNetworkInf() {
            bool result = false;
            var device = NetworkSuppoet.GetEthernetInterface();
            if (device?.OperationalStatus == OperationalStatus.Up) {
                result = true;
            }
            return result;
        }

        //TODO:有非常严重的问题
        public void CloneRouterMac(MainForm.MacChooseType mactype,string mac) {
            routerP router = new routerP(Data.RouterIp);
            string routermac;
            switch (mactype) {
                default:
                case MainForm.MacChooseType.Router:
                    routermac = "router"; break;
                case MainForm.MacChooseType.Pc:
                    routermac = "pc"; break;
                case MainForm.MacChooseType.Custom:
                    routermac = mac;break;
            }
            router.setCloneMAC(Data.RouterAcc, Data.RouterPwd, routermac);
        }

        
        public LicenceStatus CheckLicence() {
            var licenceInfoData = _licence.GetLicenceInfo();
            var result = licenceInfoData.Status;
            LicenceStatus = result;
            if (licenceInfoData.Status == LicenceStatus.Ok) {
                Data.NetAcc = licenceInfoData.NetAcc;
            }
            return result;
        }
    }
}
