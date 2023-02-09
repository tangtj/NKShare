using System;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Win32;

namespace NKShare.NKShareMod.NKtool {
    public class Nktool {
        public string NkPath { get; private set; }

        private const string NkName = "NetKeeper";
        private const string NkRegName = "NetKeeper.exe";
        private const string RegPath = @"Software\Microsoft\Windows\CurrentVersion\App Paths";
        private const string RegKeyName = "INSTDIR";

        /// <summary>
        /// 确认是否安装NK
        /// </summary>
        /// <returns></returns>
        public bool FindNk() {
            bool result = false;
            string nkpath = null;
            RegistryKey currentKey = null;
            RegistryKey pregkey = null;
            try {
                pregkey = Registry.LocalMachine.OpenSubKey(RegPath);//获取指定路径下的键
                if (pregkey != null)
                    foreach (string item in pregkey.GetSubKeyNames()) {
                        if (item == NkRegName) {
                            currentKey = pregkey.OpenSubKey(item);
                            string paths = (string)currentKey?.GetValue(RegKeyName);
                            Console.WriteLine(paths);
                            if (paths != null) {
                                nkpath = paths;
                            }
                        }
                    }
            } finally {
                currentKey?.Close();
                pregkey?.Close();
            }
            if (nkpath != null) {
                NkPath = nkpath;
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 启动NK
        /// </summary>
        /// <returns></returns>
        public bool StartNk() {
            if (NkPath == null)
                return false;
            var taskRunAble = true;
            ProcessStartInfo nkinfo = new ProcessStartInfo {
                WorkingDirectory = NkPath,
                FileName = $@"{NkPath}\{NkName}.exe"
            };
            Process.Start(nkinfo);
            Task findNkTask = new Task(() => {
                Process process = null;
                while (process == null && taskRunAble) {
                    process = FindNKProcess();
                    Thread.Sleep(100);
                }
            });
            findNkTask.Start();
            var result = findNkTask.Wait(8000);
            if (!result) {
                taskRunAble = false;
            }
            return result;
        }

        private Process FindNKProcess() {
            Process nkProcess = null;
            Process[] vProcesses = Process.GetProcesses();
            foreach (Process vProcess in vProcesses) {
                if (NkName == vProcess.MainWindowTitle) {
                    nkProcess =  vProcess;
                    break;
                }
            }
            return nkProcess;
        }

        /// <summary>
        /// 验证nk是否存活
        /// </summary>
        /// <returns></returns>
        public bool IsAlive() {
            var result = false;
            var process = FindNKProcess();
            if (process != null) {
                result = true;
            }
            return result;
        }

        /// <summary>
        /// 关闭NK
        /// </summary>
        public void KillNk() {
            if (!IsAlive()) return;
            var process = FindNKProcess();
            process.Kill();
        }
    }
}