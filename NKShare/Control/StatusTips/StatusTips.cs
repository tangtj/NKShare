using System.Drawing;
using MetroFramework.Controls;

namespace NKShare.Control.StatusTips {
    public partial class StatusTips {
        private static MetroTile _tips;
        private delegate void Status(StatusStyle style, string tipsText, int statusCode);//委托

        private static readonly string[] Stylecolor = { "Black", "White", "Silver", "Blue", "Green", "Lime", "Teal", "Orange", "Brown", "Pink", "Magenta", "Purple", "Red", "Yellow" };
        public enum StatusStyle {
            Error = 12,
            Waring = 13,
            Normal = 3
        }

        public void SetFrom(MetroTile tile) {
            if (_tips == null) {
                _tips = tile;
            }
        }

        private void SetFormStatus(StatusStyle style, string tipsText, int statusCode) {
            _tips.Style = TipStyle(style);
            _tips.Text = tipsText;
            _tips.TileCount = statusCode;
        }

        public void SetStatus(StatusStyle style, string tipsText, int statusCode) {
            if (_tips.InvokeRequired) {
                Status status = SetFormStatus;
                _tips.Invoke(status, style, tipsText, statusCode);
            }
            else {
                SetFormStatus(style, tipsText, statusCode);
            }
        }

        public void ClearStatus() {
            SetStatus(StatusStyle.Normal,"",0);
        }

        private static string TipStyle(StatusStyle style) {
            return Stylecolor[(int)style];
        }
    }
}
