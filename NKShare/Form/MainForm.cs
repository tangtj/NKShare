using MetroFramework.Controls;
using MetroFramework.Forms;
using NKShare.Control.StatusTips;
using NKShare.NKShareMod.Licence;
using NKShare.NKShareMod.Util;
using System;
using System.Threading.Tasks;

namespace NKShare.Form {
    public partial class MainForm : MetroForm {

        public MainForm(Control.Control control) {
            InitializeComponent();
            NksControl = control;
            NksControl.Tips.SetFrom(MessageBoxTile);
        }

        private void StartButton_Click(object sender, EventArgs e) {
            SaveConfig(NksControl.Data);
            Task.Factory.StartNew(() => {
                var result = NksControl.CheckNetworkInf();
                if (NetworkSuppoet.IsOnline()) {
                    NksControl.Tips.SetStatus(StatusTips.StatusStyle.Error, "已连接网络", 0);
                    return;
                }
                if (result == false) {
                    NksControl.Tips.SetStatus(StatusTips.StatusStyle.Error, "网卡未连接", 101);
                    return;
                }
                result = NksControl.StartNk();
                if (result == false) {
                    return;
                }
                NksControl.Tips.SetStatus(StatusTips.StatusStyle.Normal, "等待拨号", 111);
                NksControl.NetCatch();

            });
        }


        private void MainForm_Load(object sender, EventArgs e) {
            LoadConfig(NksControl.Data);
            FormInit();
        }

        private void FormInit() {
            _textBoxs = new[] { RouterAccTextBox, RouterPwdTextBox, RouterIPTextBox, NetPwdTextBox };

            //NetPwdTextBox.TextChanged += TextBoxs_TextChanged;
            //RouterIPTextBox.TextChanged += TextBoxs_TextChanged;
            //RouterAccTextBox.TextChanged += TextBoxs_TextChanged;
            //RouterPwdTextBox.TextChanged += TextBoxs_TextChanged;

            foreach (var textBox in _textBoxs) {
                textBox.TextChanged += TextBoxs_TextChanged;
            }

            if (RouterAccTextBox.Text == string.Empty) {
                RouterAccTextBox.Text = "admin";
            }
            if (RouterIPTextBox.Text == string.Empty) {
                RouterIPTextBox.Text = "192.168.1.1";
            }
            if (MacChooseComboBox.SelectedItem == null) {
                MacChooseComboBox.SelectedIndex = 0;
            }

            switch (NksControl.CheckLicence()) {
                case LicenceStatus.Ok:
                    NksControl.Tips.SetStatus(StatusTips.StatusStyle.Normal, "已授权", 0);
                    StartButton.Enabled = true;
                    break;
                case LicenceStatus.TimeOver:
                    NksControl.Tips.SetStatus(StatusTips.StatusStyle.Waring, "授权已过期", 121);
                    StartButton.Enabled = false;
                    break;
                case LicenceStatus.Broken:
                    NksControl.Tips.SetStatus(StatusTips.StatusStyle.Error, "授权损坏", 123);
                    StartButton.Enabled = false;
                    break;
                default:
                case LicenceStatus.Unknow:
                    NksControl.Tips.SetStatus(StatusTips.StatusStyle.Error, "读取授权错误", 124);
                    StartButton.Enabled = false;
                    break;
            }
        }

        private void MainForm_FormClosed(object sender, System.Windows.Forms.FormClosedEventArgs e) {
            NksControl.ClosingAll();
        }

        private void MacChooseComboBox_SelectedIndexChanged(object sender, EventArgs e) {
            switch ((MacChooseType)MacChooseComboBox.SelectedIndex) {
                default:
                case MacChooseType.Router:
                    MacTextBox.Enabled = false;
                    MacTextBox.PromptText = "使用路由器默认MAC";
                    MacTextBox.Text = "";
                    MacTextBox.UseStyleColors = false;
                    break;
                case MacChooseType.Pc:
                    MacTextBox.Enabled = false;
                    MacTextBox.Text =
                        Util.ConvertToMacStr(NksControl.Data.NetworkInterface.GetPhysicalAddress().ToString());
                    MacTextBox.UseStyleColors = true;
                    break;
                case MacChooseType.Custom:
                    MacTextBox.Enabled = true;
                    MacTextBox.Text = NksControl.Data.UseMac;
                    MacTextBox.PromptText = "AA-AA-AA-AA-AA-AA";
                    MacTextBox.UseStyleColors = true;
                    break;
            }
        }

        //TODO:使用浏览器打开本地网页,现在还没办法跳转到指定表情待解决
        private void MessageBoxTile_Click(object sender, EventArgs e) {
            //System.Diagnostics.Process.Start($@"file://{Environment.CurrentDirectory}/test.html");
        }

        private void AutoRouterIPButton_Click(object sender, EventArgs e) {
            Task.Factory.StartNew(() => {
                if (!NksControl.CheckNetworkInf()) {
                    NksControl.Tips.SetStatus(StatusTips.StatusStyle.Error, "网络未连接", 201);
                    return;
                }
                var ip = NksControl.GetRouterIp();
                Func<MetroTextBox, string, int> changeText = (box, i) => {
                    box.Text = NetworkSuppoet.IsIp(i) ? i : null;
                    return 1;
                };
                NksControl.Tips.SetStatus(StatusTips.StatusStyle.Normal, "已检测到路由网关", 0);
                RouterIPTextBox.Invoke(changeText, RouterIPTextBox, ip);
            });
        }

        private void StartButton_EnabledChanged(object sender, EventArgs e) {
            if (NksControl.LicenceStatus != LicenceStatus.Ok) {
                StartButton.Enabled = false;
            }
        }

        //TODO:需要对数据进行验证
        private void SetMACButton_Click(object sender, EventArgs e) {
            SaveConfig(NksControl.Data);
            Task.Factory.StartNew(() => {
                NksControl.CloneRouterMac((MacChooseType) NksControl.Data.ChooseUseMactype, NksControl.Data.UseMac);
            });
        }

        private void MacTextBox_TextChanged(object sender, EventArgs e) {
            var isMac =  NetworkSuppoet.IsMac(MacTextBox.Text);
            if (!isMac && MacChooseComboBox.SelectedIndex != 0) {
                SetMACButton.Enabled = false;
                MacTextBox.Style = "red";
            }
            else {
                MacTextBox.Style = "bule";
                SetMACButton.Enabled = true;
            }
        }

        private void MacTextBox_MouseEnter(object sender, EventArgs e) {
            MacTextBox.PromptText = "";
        }

        private void MacTextBox_MouseLeave(object sender, EventArgs e) {
            MacTextBox.PromptText = "AA-AA-AA-AA-AA-AA";
        }
    }
}
