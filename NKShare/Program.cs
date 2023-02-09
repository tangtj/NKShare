using NKShare.Control;
using NKShare.Form;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace NKShare {
    static class Program {
        private const string Cmdinput = "debug";

        [DllImport("kernel32.dll")]
        private static extern bool FreeConsole();
        [DllImport("kernel32.dll")]
        private static extern bool AllocConsole();

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args) {
            if (args.Length != 0 && args[0] == Cmdinput) {
                AllocConsole();
            }


            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Control.Control control = new Control.Control();
            var mainForm = new MainForm(control);

            Application.Run(mainForm);

            if (args.Length != 0 && args[0] == Cmdinput) {
                FreeConsole();
            }
        }
    }
}
