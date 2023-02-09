using MetroFramework.Controls;

namespace NKShare.Form {
    partial class MainForm {
        private Control.Control NksControl { get; set; }
        private MetroTextBox[] _textBoxs;

        public enum MacChooseType {
            Router = 0,
            Pc = 1,
            Custom = 2
        }
    }
}
