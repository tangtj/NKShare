using NKShare.Properties;

namespace NKShare.NKShareMod.Config {
    static class Config {
        public static ConfigData GetConfigData() {
            ConfigData configData = new ConfigData {
                UseMac = Settings.Default.UseMac,
                NetPwd = Settings.Default.NetPwd,
                NetAcc = Settings.Default.NetAcc,
                RouterAcc = Settings.Default.RouterAcc,
                RouterPwd = Settings.Default.RouterPwd
            };
            return configData;
        }

        public static void SaveConfigData(ConfigData data) {
            Settings saveConfig = new Settings {
                NetAcc = data.NetAcc,
                NetPwd = data.NetPwd,
                RouterAcc = data.RouterAcc,
                RouterPwd = data.RouterPwd,
                UseMac = data.UseMac,
                ChooseUseMacType = data.ChooseUseMactype
            };
            saveConfig.Save();
        }
    }
}
