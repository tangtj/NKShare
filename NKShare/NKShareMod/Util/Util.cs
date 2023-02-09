using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;

namespace NKShare.NKShareMod.Util {
    public static class Util {
        public static string ConvertToMacStr(string mac) {
            if (mac == null || mac.Length != 12) {
                return null;
            }
            var macstr = "";
            for (int i = 0; i <= 5; i++) {
                macstr += mac.Substring(i * 2, 2);
                if (i < 5) {
                    macstr += "-";
                }
            }
            Console.WriteLine(macstr);
            return macstr;
        }
    }
}
