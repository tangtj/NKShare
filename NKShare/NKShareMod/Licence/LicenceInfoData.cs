using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKShare.NKShareMod.Licence {
    public class LicenceInfoData {
        public LicenceInfoData(LicenceStatus status, string netAcc) {
            Status = status;
            NetAcc = netAcc;
        }

        public LicenceStatus Status { get; private set; }
        public string NetAcc { get; private set; }
    }
}
