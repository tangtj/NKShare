using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.RegularExpressions;

namespace NKShare.NKShareMod.Util {
    public static class NetworkSuppoet {

        /// <summary>
        /// 对网址进行ping测试,检测是否联网
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static bool IsOnline(string url) {
            bool result = false;
            Ping ping = new Ping();
            try {
                var pingReply = ping.Send(url);
                if (pingReply?.Status == IPStatus.Success) {
                    result = true;
                }
            } catch (Exception e) {
                Console.WriteLine(e + "11");
            }
            Console.WriteLine(result);
            return result;
        }

        /// <summary>
        /// 测试网络是否联通,默认"baidu.com"
        /// </summary>
        /// <returns></returns>
        public static bool IsOnline() {
            return IsOnline("114.114.114.114");
        }

        public static bool IsIp(string ip) {
            const string isip = @"^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
            if (ip == null) {
                return false;
            }
            Regex r = new Regex(isip);
            Match m = r.Match(ip);
            return m.Success;
        }

        public static bool IsMac(string mac) {
            const string ismac = @"^([A-Fa-f0-9]{2}[-]){5}[A-Fa-f0-9]{2}$";
            if (mac == null) {
                return false;
            }
            Regex r = new Regex(ismac, RegexOptions.IgnoreCase);
            Match m = r.Match(mac);
            return m.Success;
        }


        public static NetworkInterface GetEthernetInterface() {
            NetworkInterface[] devices = NetworkInterface.GetAllNetworkInterfaces();
            NetworkInterface device = null;
            foreach (var item in devices) {
                if (item.NetworkInterfaceType == NetworkInterfaceType.Ethernet) {
                    device = item;
                    break;
                }
            }
            return device;
        }

        public static string GetRouterIp() {
            string routerIp = null;
            var nic = GetEthernetInterface();
            if (nic != null) {
                var b = nic.GetIPProperties().GatewayAddresses;
                routerIp = b.First().Address.ToString();
            }
            return routerIp;
        }
    }
}
