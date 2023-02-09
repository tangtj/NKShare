using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using NKShare.NKShareMod.Config;
using NKShare.NKShareMod.Util;

namespace NKShare.Control.Data {
    public class RuntimeData {

        public RuntimeData(ConfigData data) {
            UseMac = data.UseMac;
            ChooseUseMactype = data.ChooseUseMactype;
            NetAcc = data.NetAcc;
            NetPwd = data.NetPwd;
            RouterAcc = data.RouterAcc;
            RouterPwd = data.RouterPwd;
            NetworkInterface = NetworkSuppoet.GetEthernetInterface();
        }

        private string _licenceAcc;

        public NetworkInterface NetworkInterface { get; private set; }

        private string LicenceAcc {
            get { return Encoding.UTF8.GetString(Convert.FromBase64String(_licenceAcc)); }

            set { _licenceAcc = Convert.ToBase64String(Encoding.UTF8.GetBytes(value)); }
        }

        public bool IsConectNet { get; set; }

        public bool IsOnline { get; set; }


        #region 需要保存的一些信息
        public string RouterAcc { get; set; }

        public string RouterPwd { get; set; }

        public string NetPwd { get; set; }

        public string NetAcc { get; set; }

        public string UseMac { get; set; }

        public string RouterIp { get; set; }

        public int ChooseUseMactype { get; set; }
        #endregion

        public ConfigData GetConfigData() {
            ConfigData data = new ConfigData {
                UseMac = UseMac,
                ChooseUseMactype = ChooseUseMactype,
                NetAcc = NetAcc,
                NetPwd = NetPwd,
                RouterAcc = RouterAcc,
                RouterPwd = RouterPwd
            };
            return data;
        }
    }
}
