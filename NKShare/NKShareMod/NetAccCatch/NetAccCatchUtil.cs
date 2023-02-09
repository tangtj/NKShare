using PacketDotNet;
using System.Collections.Generic;

namespace NKShare.NKShareMod.NetAccCatch {
    internal static class NetAccCatchUtil {
        public static byte[] GethostUniq(byte[] data) {
            List<byte> uniq = new List<byte>();
            int a = data[PPPoEFields.LengthPosition];
            int b = data[PPPoEFields.LengthPosition + 1];
            int length = (a << 8) + b;
            if (length > 6 && data[PPPoEFields.HeaderLength + 4] == 0x01 && data[PPPoEFields.HeaderLength + 5] == 0x03) {
                for (int i = 0, j = length - 4; i < j; i++) {
                    uniq.Add(data[PPPoEFields.HeaderLength + 4 + i]);
                }
            }
            return uniq.ToArray();
        }

        public static int ByteIndexOf(byte[] source, byte[] search) {
            byte c = 0;
            for (int d = 0; d < source.Length; d++) {
                if (search[c] == source[d]) {
                    c++;
                } else {
                    c = 0;
                    if (search[c] == source[d]) {
                        c++;
                    }
                }
                if (c == search.Length)
                    return d - c + 1;
            }
            return -1;
        }
    }
}