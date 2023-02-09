namespace NKShare.Form {
    partial class MainForm {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing) {
            if (disposing && (components != null)) {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent() {
            MetroFramework.Controls.MetroTabPage index;
            MetroFramework.Controls.MetroLabel metroLabel11;
            MetroFramework.Controls.MetroLabel metroLabel4;
            MetroFramework.Controls.MetroLabel metroLabel6;
            MetroFramework.Controls.MetroLabel metroLabel5;
            MetroFramework.Controls.MetroLabel metroLabel3;
            MetroFramework.Controls.MetroLabel metroLabel2;
            MetroFramework.Controls.MetroTabPage setting;
            MetroFramework.Controls.MetroLabel metroLabel1;
            MetroFramework.Controls.MetroTabPage about;
            this.TabControl = new MetroFramework.Controls.MetroTabControl();
            this.NetPwdTextBox = new MetroFramework.Controls.MetroTextBox();
            this.RouterPwdTextBox = new MetroFramework.Controls.MetroTextBox();
            this.RouterAccTextBox = new MetroFramework.Controls.MetroTextBox();
            this.StartButton = new MetroFramework.Controls.MetroButton();
            this.AutoRouterIPButton = new MetroFramework.Controls.MetroButton();
            this.NetAccLable = new MetroFramework.Controls.MetroLabel();
            this.RouterIPTextBox = new MetroFramework.Controls.MetroTextBox();
            this.SetMACButton = new System.Windows.Forms.Button();
            this.MacChooseComboBox = new MetroFramework.Controls.MetroComboBox();
            this.MacTextBox = new MetroFramework.Controls.MetroTextBox();
            this.MessageBoxTile = new MetroFramework.Controls.MetroTile();
            index = new MetroFramework.Controls.MetroTabPage();
            metroLabel11 = new MetroFramework.Controls.MetroLabel();
            metroLabel4 = new MetroFramework.Controls.MetroLabel();
            metroLabel6 = new MetroFramework.Controls.MetroLabel();
            metroLabel5 = new MetroFramework.Controls.MetroLabel();
            metroLabel3 = new MetroFramework.Controls.MetroLabel();
            metroLabel2 = new MetroFramework.Controls.MetroLabel();
            setting = new MetroFramework.Controls.MetroTabPage();
            metroLabel1 = new MetroFramework.Controls.MetroLabel();
            about = new MetroFramework.Controls.MetroTabPage();
            this.TabControl.SuspendLayout();
            index.SuspendLayout();
            setting.SuspendLayout();
            this.SuspendLayout();
            // 
            // TabControl
            // 
            this.TabControl.Controls.Add(index);
            this.TabControl.Controls.Add(setting);
            this.TabControl.Controls.Add(about);
            this.TabControl.ItemSize = new System.Drawing.Size(70, 25);
            this.TabControl.Location = new System.Drawing.Point(15, 62);
            this.TabControl.Name = "TabControl";
            this.TabControl.SelectedIndex = 1;
            this.TabControl.Size = new System.Drawing.Size(450, 208);
            this.TabControl.TabIndex = 7;
            // 
            // index
            // 
            index.Controls.Add(metroLabel11);
            index.Controls.Add(this.NetPwdTextBox);
            index.Controls.Add(metroLabel4);
            index.Controls.Add(this.RouterPwdTextBox);
            index.Controls.Add(this.RouterAccTextBox);
            index.Controls.Add(metroLabel6);
            index.Controls.Add(metroLabel5);
            index.Controls.Add(this.StartButton);
            index.Controls.Add(this.AutoRouterIPButton);
            index.Controls.Add(this.NetAccLable);
            index.Controls.Add(this.RouterIPTextBox);
            index.Controls.Add(metroLabel3);
            index.Controls.Add(metroLabel2);
            index.HorizontalScrollbarBarColor = true;
            index.Location = new System.Drawing.Point(4, 29);
            index.Name = "index";
            index.Size = new System.Drawing.Size(442, 175);
            index.TabIndex = 0;
            index.Text = "连接网络";
            index.VerticalScrollbarBarColor = true;
            // 
            // metroLabel11
            // 
            metroLabel11.AutoSize = true;
            metroLabel11.Location = new System.Drawing.Point(168, 98);
            metroLabel11.Name = "metroLabel11";
            metroLabel11.Size = new System.Drawing.Size(139, 19);
            metroLabel11.TabIndex = 15;
            metroLabel11.Text = "无则使用默认\"admin\"";
            // 
            // NetPwdTextBox
            // 
            this.NetPwdTextBox.Location = new System.Drawing.Point(242, 18);
            this.NetPwdTextBox.Name = "NetPwdTextBox";
            this.NetPwdTextBox.PasswordChar = '*';
            this.NetPwdTextBox.Size = new System.Drawing.Size(84, 23);
            this.NetPwdTextBox.Style = "Red";
            this.NetPwdTextBox.TabIndex = 14;
            this.NetPwdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroLabel4
            // 
            metroLabel4.AutoSize = true;
            metroLabel4.Location = new System.Drawing.Point(168, 22);
            metroLabel4.Name = "metroLabel4";
            metroLabel4.Size = new System.Drawing.Size(68, 19);
            metroLabel4.TabIndex = 13;
            metroLabel4.Text = "宽带密码:";
            metroLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // RouterPwdTextBox
            // 
            this.RouterPwdTextBox.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.RouterPwdTextBox.Location = new System.Drawing.Point(78, 138);
            this.RouterPwdTextBox.Name = "RouterPwdTextBox";
            this.RouterPwdTextBox.PasswordChar = '*';
            this.RouterPwdTextBox.Size = new System.Drawing.Size(84, 23);
            this.RouterPwdTextBox.Style = "Red";
            this.RouterPwdTextBox.TabIndex = 12;
            this.RouterPwdTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // RouterAccTextBox
            // 
            this.RouterAccTextBox.Location = new System.Drawing.Point(78, 98);
            this.RouterAccTextBox.Name = "RouterAccTextBox";
            this.RouterAccTextBox.Size = new System.Drawing.Size(84, 23);
            this.RouterAccTextBox.Style = "Red";
            this.RouterAccTextBox.TabIndex = 11;
            this.RouterAccTextBox.Text = "admin";
            this.RouterAccTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroLabel6
            // 
            metroLabel6.AutoSize = true;
            metroLabel6.Location = new System.Drawing.Point(4, 142);
            metroLabel6.Name = "metroLabel6";
            metroLabel6.Size = new System.Drawing.Size(68, 19);
            metroLabel6.TabIndex = 10;
            metroLabel6.Text = "路由密码:";
            metroLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel5
            // 
            metroLabel5.AutoSize = true;
            metroLabel5.Location = new System.Drawing.Point(4, 98);
            metroLabel5.Name = "metroLabel5";
            metroLabel5.Size = new System.Drawing.Size(68, 19);
            metroLabel5.TabIndex = 9;
            metroLabel5.Text = "路由帐号:";
            metroLabel5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // StartButton
            // 
            this.StartButton.Location = new System.Drawing.Point(335, 138);
            this.StartButton.Name = "StartButton";
            this.StartButton.Size = new System.Drawing.Size(103, 34);
            this.StartButton.TabIndex = 0;
            this.StartButton.Text = "开始";
            this.StartButton.EnabledChanged += new System.EventHandler(this.StartButton_EnabledChanged);
            this.StartButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // AutoRouterIPButton
            // 
            this.AutoRouterIPButton.Location = new System.Drawing.Point(168, 58);
            this.AutoRouterIPButton.Name = "AutoRouterIPButton";
            this.AutoRouterIPButton.Size = new System.Drawing.Size(75, 23);
            this.AutoRouterIPButton.TabIndex = 7;
            this.AutoRouterIPButton.Text = "自动检测";
            this.AutoRouterIPButton.Click += new System.EventHandler(this.AutoRouterIPButton_Click);
            // 
            // NetAccLable
            // 
            this.NetAccLable.AutoSize = true;
            this.NetAccLable.Location = new System.Drawing.Point(78, 22);
            this.NetAccLable.Name = "NetAccLable";
            this.NetAccLable.Size = new System.Drawing.Size(0, 0);
            this.NetAccLable.TabIndex = 6;
            // 
            // RouterIPTextBox
            // 
            this.RouterIPTextBox.Location = new System.Drawing.Point(78, 58);
            this.RouterIPTextBox.Name = "RouterIPTextBox";
            this.RouterIPTextBox.Size = new System.Drawing.Size(84, 23);
            this.RouterIPTextBox.Style = "Red";
            this.RouterIPTextBox.TabIndex = 5;
            this.RouterIPTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // metroLabel3
            // 
            metroLabel3.AutoSize = true;
            metroLabel3.Location = new System.Drawing.Point(4, 58);
            metroLabel3.Name = "metroLabel3";
            metroLabel3.Size = new System.Drawing.Size(68, 19);
            metroLabel3.TabIndex = 4;
            metroLabel3.Text = "路由网关:";
            metroLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // metroLabel2
            // 
            metroLabel2.AutoSize = true;
            metroLabel2.Location = new System.Drawing.Point(4, 22);
            metroLabel2.Name = "metroLabel2";
            metroLabel2.Size = new System.Drawing.Size(68, 19);
            metroLabel2.TabIndex = 3;
            metroLabel2.Text = "宽带帐号:";
            // 
            // setting
            // 
            setting.Controls.Add(this.SetMACButton);
            setting.Controls.Add(this.MacChooseComboBox);
            setting.Controls.Add(this.MacTextBox);
            setting.Controls.Add(metroLabel1);
            setting.HorizontalScrollbarBarColor = true;
            setting.Location = new System.Drawing.Point(4, 29);
            setting.Name = "setting";
            setting.Size = new System.Drawing.Size(442, 175);
            setting.Style = "red";
            setting.TabIndex = 1;
            setting.Text = "设置";
            setting.VerticalScrollbarBarColor = true;
            // 
            // SetMACButton
            // 
            this.SetMACButton.Location = new System.Drawing.Point(222, 54);
            this.SetMACButton.Name = "SetMACButton";
            this.SetMACButton.Size = new System.Drawing.Size(75, 23);
            this.SetMACButton.TabIndex = 8;
            this.SetMACButton.Text = "设置MAC";
            this.SetMACButton.UseVisualStyleBackColor = true;
            this.SetMACButton.Click += new System.EventHandler(this.SetMACButton_Click);
            // 
            // MacChooseComboBox
            // 
            this.MacChooseComboBox.ForeColor = System.Drawing.Color.Black;
            this.MacChooseComboBox.FormattingEnabled = true;
            this.MacChooseComboBox.ItemHeight = 23;
            this.MacChooseComboBox.Items.AddRange(new object[] {
            "恢复默认",
            "本机MAC",
            "自定义MAC"});
            this.MacChooseComboBox.Location = new System.Drawing.Point(111, 12);
            this.MacChooseComboBox.Name = "MacChooseComboBox";
            this.MacChooseComboBox.Size = new System.Drawing.Size(105, 29);
            this.MacChooseComboBox.TabIndex = 7;
            this.MacChooseComboBox.SelectedIndexChanged += new System.EventHandler(this.MacChooseComboBox_SelectedIndexChanged);
            // 
            // MacTextBox
            // 
            this.MacTextBox.BackColor = System.Drawing.Color.White;
            this.MacTextBox.FontSize = MetroFramework.Drawing.MetroFontSize.Large;
            this.MacTextBox.FontWeight = MetroFramework.Drawing.MetroFontWeight.Regular;
            this.MacTextBox.ForeColor = System.Drawing.Color.Black;
            this.MacTextBox.Location = new System.Drawing.Point(10, 47);
            this.MacTextBox.MaxLength = 17;
            this.MacTextBox.Name = "MacTextBox";
            this.MacTextBox.PromptText = "AA:AA:AA:AA:AA:AA";
            this.MacTextBox.Size = new System.Drawing.Size(206, 30);
            this.MacTextBox.TabIndex = 6;
            this.MacTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.MacTextBox.TextChanged += new System.EventHandler(this.MacTextBox_TextChanged);
            this.MacTextBox.MouseEnter += new System.EventHandler(this.MacTextBox_MouseEnter);
            this.MacTextBox.MouseLeave += new System.EventHandler(this.MacTextBox_MouseLeave);
            // 
            // metroLabel1
            // 
            metroLabel1.AutoSize = true;
            metroLabel1.FontSize = MetroFramework.Drawing.MetroFontSize.Medium;
            metroLabel1.Location = new System.Drawing.Point(10, 17);
            metroLabel1.Name = "metroLabel1";
            metroLabel1.Size = new System.Drawing.Size(95, 19);
            metroLabel1.TabIndex = 4;
            metroLabel1.Text = "路由MAC设置";
            metroLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // about
            // 
            about.HorizontalScrollbarBarColor = true;
            about.Location = new System.Drawing.Point(4, 29);
            about.Name = "about";
            about.Size = new System.Drawing.Size(442, 175);
            about.TabIndex = 2;
            about.Text = "关于";
            about.VerticalScrollbarBarColor = true;
            // 
            // MessageBoxTile
            // 
            this.MessageBoxTile.FontSize = MetroFramework.Drawing.MetroFontSize.Large;
            this.MessageBoxTile.ForeColor = System.Drawing.Color.White;
            this.MessageBoxTile.Location = new System.Drawing.Point(251, 30);
            this.MessageBoxTile.Name = "MessageBoxTile";
            this.MessageBoxTile.Size = new System.Drawing.Size(210, 55);
            this.MessageBoxTile.TabIndex = 6;
            this.MessageBoxTile.Click += new System.EventHandler(this.MessageBoxTile_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(480, 300);
            this.Controls.Add(this.MessageBoxTile);
            this.Controls.Add(this.TabControl);
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Resizable = false;
            this.Text = "NetKeeper Share";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.MainForm_FormClosed);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.TabControl.ResumeLayout(false);
            index.ResumeLayout(false);
            index.PerformLayout();
            setting.ResumeLayout(false);
            setting.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        public MetroFramework.Controls.MetroTile MessageBoxTile;
        private MetroFramework.Controls.MetroTextBox NetPwdTextBox;
        private MetroFramework.Controls.MetroTextBox RouterPwdTextBox;
        private MetroFramework.Controls.MetroTextBox RouterAccTextBox;
        private MetroFramework.Controls.MetroButton StartButton;
        private MetroFramework.Controls.MetroButton AutoRouterIPButton;
        private MetroFramework.Controls.MetroLabel NetAccLable;
        private MetroFramework.Controls.MetroTextBox RouterIPTextBox;
        private System.Windows.Forms.Button SetMACButton;
        private MetroFramework.Controls.MetroComboBox MacChooseComboBox;
        private MetroFramework.Controls.MetroTextBox MacTextBox;
        private MetroFramework.Controls.MetroTabControl TabControl;
    }
}

