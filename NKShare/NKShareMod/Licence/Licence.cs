using System;
using System.IO;
using NKShare.NKShareMod.Uitly.RSA;

namespace NKShare.NKShareMod.Licence {
    internal class Licence {

        private const string PublicKey = "MIIBIjANBgkqhkiG9w0BAQEFAAOCAQ8AMIIBCgKCAQEAxMma44H5wOws1QBESeL05bfb0RMeOhleGUypK4z1kmLWLy2JKHs5dYiOqu99LVYC3vpXEu+XQRHnXvdIQWU7OBLfsvEI39hGUhTVihZf+FuHxc0iASa0fFKIl58NGiEkUJtooapGeta2Fp0BHoPMMrzB94V5RVPP/95dJwBhJIeaWgSwGg0QiORiYD9OQ4xr7JYNdZhapFcESd1q6oBpnKzh+o29y0aneVpNqy1IDfYaRHsxzBlJwoBdLnrAjidMnFbfo9f2aIjhGSE9TBTpKdnJX2ABr2G2mQmHaQRR9zUK2VaImDnaryeXSY7XaBeY33KhJiZFcsGtJxgCgEN98QIDAQAB";
        private const string TimeLable = "endTime";
        private const string AccLable = "netAcc";

        private readonly string _path;
        
        public Licence(string path) {
            _path = path;
        }


        //TODO:需要进行测试
        public LicenceInfoData GetLicenceInfo() {
            try {
                var licenceStr = OpenLicence(_path);
                //Console.WriteLine(licenceStr);
                string decryptResult = new RSAStandard().RSADecryptByPub(licenceStr, PublicKey);
                string endTime = GetNodeContent(decryptResult, TimeLable);
                string netAcc = GetNodeContent(decryptResult, AccLable);
                //Console.WriteLine(decryptResult);
                long endTimeLong = long.Parse(endTime);
                if (netAcc != null) {
                    if (endTimeLong < GetJavaDateTime()) {
                        return new LicenceInfoData(LicenceStatus.TimeOver,netAcc);
                    }
                    //String catchAcc = decryptResult.Substring(markString.Length, decryptResult.Length - markString.Length);
                    //log.writeLog($"licence模块\n解析到授权账号:{netAcc}", log.msgType.info);
                    return new LicenceInfoData(LicenceStatus.Ok,netAcc);
                }
                //log.writeLog($"licence模块\n解析授权账号出错，在授权信息中未找到endTime或netAcc节点信息", log.msgType.error);
                return new LicenceInfoData(LicenceStatus.Broken,null);
            } catch(Exception e) {
                Console.WriteLine(e.ToString());
                //log.writeLog($"licence模块\n解析授权账号出错，遇到未知错误(多为许可证被损坏)", log.msgType.error);
                return new LicenceInfoData(LicenceStatus.Unknow,null);
            }
        }

        private static string OpenLicence(string path) {
            string licence;
            try {
                licence = File.ReadAllText(path);
            }
            catch (Exception) {
                return null;
            }
            return licence;
        }

        private static string GetNodeContent(string xmlStr, string nodeName) {
            if (xmlStr == null || nodeName == null)
                return null;
            string nodeStart = "<" + nodeName + ">";
            string nodeEnd = "</" + nodeName + ">";
            int startInt = xmlStr.IndexOf(nodeStart, StringComparison.Ordinal);
            int endInt = xmlStr.IndexOf(nodeEnd, StringComparison.Ordinal);
            if (startInt < 0 || endInt < 0)
                return null;
            return xmlStr.Substring(startInt + nodeStart.Length, endInt - startInt - nodeStart.Length);
        }

        private static long GetJavaDateTime() {
            DateTime dateTime = DateTime.Now;
            DateTime windowsEpoch = new DateTime(1601, 1, 1, 0, 0, 0, 0);
            DateTime javaEpoch = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            long epochDiff = (javaEpoch.ToFileTimeUtc() - windowsEpoch.ToFileTimeUtc()) / TimeSpan.TicksPerMillisecond;
            return (dateTime.ToFileTime() / TimeSpan.TicksPerMillisecond) - epochDiff;
        }
    }
}
