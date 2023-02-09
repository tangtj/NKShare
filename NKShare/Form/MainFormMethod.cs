using NKShare.Control.Data;
using NKShare.NKShareMod.Config;
using System;

namespace NKShare.Form {
    partial class MainForm {
        private void LoadConfig(RuntimeData runtimeDatata) {
            var data = runtimeDatata.GetConfigData();
            RouterAccTextBox.Text = data.RouterAcc;
            RouterPwdTextBox.Text = data.RouterPwd;
            NetPwdTextBox.Text = data.NetPwd;
            MacChooseComboBox.SelectedIndex = data.ChooseUseMactype;
            MacTextBox.Text = data.UseMac;
            RouterIPTextBox.Text = data.RouterIp;
            NetAccLable.Text = runtimeDatata.NetAcc;
        }

        private void SaveConfig(RuntimeData data) {
            data.RouterAcc = RouterAccTextBox.Text;
            data.RouterPwd = RouterPwdTextBox.Text;
            data.NetPwd = NetPwdTextBox.Text;
            data.ChooseUseMactype = MacChooseComboBox.SelectedIndex;
            data.UseMac = MacTextBox.Text;
            data.RouterIp = RouterIPTextBox.Text;
            NksControl.SaveConfig();
        }

        private void TextBoxs_TextChanged(object sender, EventArgs e) {
            var result = CheckInput();
            StartButton.Enabled = result;
        }

        private bool CheckInput() {
            bool result = true;
            foreach (var textBox in _textBoxs) {
                if (textBox.Text == "") {
                    textBox.UseStyleColors = true;
                    result = false;
                }
                else {
                    textBox.UseStyleColors = false;
                }
            }
            return result;
        }
    }
}
