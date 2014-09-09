namespace Naccs.Core.Option
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class GatewaySetting : Form
    {
        private Button btnGwCncl;
        private Button btnGwOK;
        private CheckBox ckbFqdn;
        private IContainer components;
        private GatewaySettingClass gatewayInfo = new GatewaySettingClass();
        private GroupBox grpPOP;
        private GroupBox grpSMTP;
        private Label label1;
        private Label lblGwMailBox;
        private Label lblGwMailBox1;
        private Label lblPopMailBox;
        private Label lblPopName;
        private Label lblPopPort;
        private Label lblProxyPassword;
        private Label lblSmtpName;
        private Label lblSmtpPort;
        private TextBox txbPopDomain;
        private TextBox txbPopName;
        private TextBox txbPopPort;
        private TextBox txbSmtpDomain;
        private TextBox txbSmtpMailBox;
        private TextBox txbSmtpName;
        private TextBox txbSmtpPort;

        public GatewaySetting()
        {
            this.InitializeComponent();
        }

        private void btnGwOK_Click(object sender, EventArgs e)
        {
            if (this.GatewaySetting_Check())
            {
                this.gatewayInfo.GatewaySMTPServerName = this.txbSmtpName.Text;
                this.gatewayInfo.GatewaySMTPServerPort = int.Parse(this.txbSmtpPort.Text);
                this.gatewayInfo.GatewayPOPServerName = this.txbPopName.Text;
                this.gatewayInfo.GatewayPOPServerPort = int.Parse(this.txbPopPort.Text);
                this.gatewayInfo.GatewaySMTPDomainName = this.txbSmtpDomain.Text;
                this.gatewayInfo.GatewayPOPDomainName = this.txbPopDomain.Text;
                this.gatewayInfo.GatewayMailBox = this.txbSmtpMailBox.Text;
                this.gatewayInfo.GatewayAddDomainName = this.ckbFqdn.Checked;
                base.DialogResult = DialogResult.OK;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool GatewaySetting_Check()
        {
            if (!this.Text_Check(this.txbSmtpName.Text))
            {
                this.ShowEditErrMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("GatewayMsg01"), this.txbSmtpName);
                this.txbSmtpName.Select();
                return false;
            }
            if (!this.No_Check(this.txbSmtpPort.Text))
            {
                this.ShowErrMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("GatewayMsg02"));
                this.txbSmtpPort.Select();
                return false;
            }
            if (!this.Text_Check(this.txbSmtpMailBox.Text))
            {
                this.ShowEditErrMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("GatewayMsg01"), this.txbSmtpMailBox);
                this.txbSmtpMailBox.Select();
                return false;
            }
            if (!this.Text_Check(this.txbSmtpDomain.Text))
            {
                this.ShowEditErrMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("GatewayMsg01"), this.txbSmtpDomain);
                this.txbSmtpDomain.Select();
                return false;
            }
            if (!this.Text_Check(this.txbPopName.Text))
            {
                this.ShowEditErrMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("GatewayMsg01"), this.txbPopName);
                this.txbPopName.Select();
                return false;
            }
            if (!this.No_Check(this.txbPopPort.Text))
            {
                this.ShowErrMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("GatewayMsg02"));
                this.txbPopPort.Select();
                return false;
            }
            if (!this.Text_Check(this.txbPopDomain.Text))
            {
                this.ShowEditErrMessage(ButtonPatern.OK_ONLY, MessageKind.Error, Resources.ResourceManager.GetString("GatewayMsg01"), this.txbPopDomain);
                this.txbPopDomain.Select();
                return false;
            }
            return true;
        }

        private void GatewaySetting_Load(object sender, EventArgs e)
        {
            this.txbSmtpName.Text = this.gatewayInfo.GatewaySMTPServerName;
            this.txbSmtpPort.Text = this.gatewayInfo.GatewaySMTPServerPort.ToString();
            this.txbPopName.Text = this.gatewayInfo.GatewayPOPServerName;
            this.txbPopPort.Text = this.gatewayInfo.GatewayPOPServerPort.ToString();
            this.txbSmtpDomain.Text = this.gatewayInfo.GatewaySMTPDomainName;
            this.txbPopDomain.Text = this.gatewayInfo.GatewayPOPDomainName;
            this.txbSmtpMailBox.Text = this.gatewayInfo.GatewayMailBox;
            this.ckbFqdn.Checked = this.gatewayInfo.GatewayAddDomainName;
        }

        private void InitializeComponent()
        {
            this.btnGwCncl = new Button();
            this.btnGwOK = new Button();
            this.grpSMTP = new GroupBox();
            this.lblGwMailBox1 = new Label();
            this.txbSmtpDomain = new TextBox();
            this.lblProxyPassword = new Label();
            this.txbSmtpMailBox = new TextBox();
            this.lblGwMailBox = new Label();
            this.txbSmtpPort = new TextBox();
            this.lblSmtpPort = new Label();
            this.txbSmtpName = new TextBox();
            this.lblSmtpName = new Label();
            this.txbPopPort = new TextBox();
            this.lblPopPort = new Label();
            this.txbPopName = new TextBox();
            this.lblPopName = new Label();
            this.grpPOP = new GroupBox();
            this.ckbFqdn = new CheckBox();
            this.label1 = new Label();
            this.txbPopDomain = new TextBox();
            this.lblPopMailBox = new Label();
            this.grpSMTP.SuspendLayout();
            this.grpPOP.SuspendLayout();
            base.SuspendLayout();
            this.btnGwCncl.DialogResult = DialogResult.Cancel;
            this.btnGwCncl.Location = new Point(0x114, 0xe5);
            this.btnGwCncl.Name = "btnGwCncl";
            this.btnGwCncl.Size = new Size(0x4b, 0x17);
            this.btnGwCncl.TabIndex = 3;
            this.btnGwCncl.Text = "キャンセル";
            this.btnGwCncl.UseVisualStyleBackColor = true;
            this.btnGwOK.Location = new Point(0x9b, 0xe5);
            this.btnGwOK.Name = "btnGwOK";
            this.btnGwOK.Size = new Size(0x4b, 0x17);
            this.btnGwOK.TabIndex = 2;
            this.btnGwOK.Text = "OK";
            this.btnGwOK.UseVisualStyleBackColor = true;
            this.btnGwOK.Click += new EventHandler(this.btnGwOK_Click);
            this.grpSMTP.Controls.Add(this.lblGwMailBox1);
            this.grpSMTP.Controls.Add(this.txbSmtpDomain);
            this.grpSMTP.Controls.Add(this.lblProxyPassword);
            this.grpSMTP.Controls.Add(this.txbSmtpMailBox);
            this.grpSMTP.Controls.Add(this.lblGwMailBox);
            this.grpSMTP.Controls.Add(this.txbSmtpPort);
            this.grpSMTP.Controls.Add(this.lblSmtpPort);
            this.grpSMTP.Controls.Add(this.txbSmtpName);
            this.grpSMTP.Controls.Add(this.lblSmtpName);
            this.grpSMTP.Location = new Point(12, 12);
            this.grpSMTP.Name = "grpSMTP";
            this.grpSMTP.Size = new Size(0x1dc, 80);
            this.grpSMTP.TabIndex = 0;
            this.grpSMTP.TabStop = false;
            this.grpSMTP.Text = "SMTP";
            this.lblGwMailBox1.AutoSize = true;
            this.lblGwMailBox1.Location = new Point(0xed, 0x37);
            this.lblGwMailBox1.Name = "lblGwMailBox1";
            this.lblGwMailBox1.Size = new Size(13, 12);
            this.lblGwMailBox1.TabIndex = 6;
            this.lblGwMailBox1.Text = "@";
            this.txbSmtpDomain.ImeMode = ImeMode.Disable;
            this.txbSmtpDomain.Location = new Point(0xff, 0x34);
            this.txbSmtpDomain.MaxLength = 0x40;
            this.txbSmtpDomain.Name = "txbSmtpDomain";
            this.txbSmtpDomain.Size = new Size(0xc7, 0x13);
            this.txbSmtpDomain.TabIndex = 7;
            this.txbSmtpDomain.Tag = "宛先メールボックスドメイン";
            this.lblProxyPassword.AutoSize = true;
            this.lblProxyPassword.Location = new Point(14, 0x9e);
            this.lblProxyPassword.Name = "lblProxyPassword";
            this.lblProxyPassword.Size = new Size(0x69, 12);
            this.lblProxyPassword.TabIndex = 8;
            this.lblProxyPassword.Text = "ゲートウェイドメイン名";
            this.txbSmtpMailBox.ImeMode = ImeMode.Disable;
            this.txbSmtpMailBox.Location = new Point(0x6f, 0x34);
            this.txbSmtpMailBox.MaxLength = 0x40;
            this.txbSmtpMailBox.Name = "txbSmtpMailBox";
            this.txbSmtpMailBox.Size = new Size(120, 0x13);
            this.txbSmtpMailBox.TabIndex = 5;
            this.txbSmtpMailBox.Tag = "宛先メールボックス";
            this.lblGwMailBox.AutoSize = true;
            this.lblGwMailBox.Location = new Point(14, 0x37);
            this.lblGwMailBox.Name = "lblGwMailBox";
            this.lblGwMailBox.Size = new Size(0x5b, 12);
            this.lblGwMailBox.TabIndex = 4;
            this.lblGwMailBox.Text = "宛先メールボックス";
            this.txbSmtpPort.ImeMode = ImeMode.Disable;
            this.txbSmtpPort.Location = new Point(0x13c, 0x12);
            this.txbSmtpPort.MaxLength = 5;
            this.txbSmtpPort.Name = "txbSmtpPort";
            this.txbSmtpPort.Size = new Size(0x2a, 0x13);
            this.txbSmtpPort.TabIndex = 3;
            this.txbSmtpPort.Tag = "SMTPポート番号";
            this.lblSmtpPort.AutoSize = true;
            this.lblSmtpPort.Location = new Point(0xfd, 0x15);
            this.lblSmtpPort.Name = "lblSmtpPort";
            this.lblSmtpPort.Size = new Size(0x39, 12);
            this.lblSmtpPort.TabIndex = 2;
            this.lblSmtpPort.Text = "ポート番号";
            this.txbSmtpName.ImeMode = ImeMode.Disable;
            this.txbSmtpName.Location = new Point(0x43, 0x12);
            this.txbSmtpName.MaxLength = 0x40;
            this.txbSmtpName.Name = "txbSmtpName";
            this.txbSmtpName.Size = new Size(0xa4, 0x13);
            this.txbSmtpName.TabIndex = 1;
            this.txbSmtpName.Tag = "SMTPサーバ名";
            this.lblSmtpName.AutoSize = true;
            this.lblSmtpName.Location = new Point(14, 0x15);
            this.lblSmtpName.Name = "lblSmtpName";
            this.lblSmtpName.Size = new Size(0x2f, 12);
            this.lblSmtpName.TabIndex = 0;
            this.lblSmtpName.Text = "サーバ名";
            this.txbPopPort.ImeMode = ImeMode.Disable;
            this.txbPopPort.Location = new Point(0x13c, 0x12);
            this.txbPopPort.MaxLength = 5;
            this.txbPopPort.Name = "txbPopPort";
            this.txbPopPort.Size = new Size(0x2a, 0x13);
            this.txbPopPort.TabIndex = 3;
            this.txbPopPort.Tag = "POPポート番号";
            this.lblPopPort.AutoSize = true;
            this.lblPopPort.Location = new Point(0xfd, 0x15);
            this.lblPopPort.Name = "lblPopPort";
            this.lblPopPort.Size = new Size(0x39, 12);
            this.lblPopPort.TabIndex = 2;
            this.lblPopPort.Text = "ポート番号";
            this.txbPopName.ImeMode = ImeMode.Disable;
            this.txbPopName.Location = new Point(0x43, 0x12);
            this.txbPopName.MaxLength = 0x40;
            this.txbPopName.Name = "txbPopName";
            this.txbPopName.Size = new Size(0xa4, 0x13);
            this.txbPopName.TabIndex = 1;
            this.txbPopName.Tag = "POPサーバ名";
            this.lblPopName.AutoSize = true;
            this.lblPopName.Location = new Point(14, 0x15);
            this.lblPopName.Name = "lblPopName";
            this.lblPopName.Size = new Size(0x2f, 12);
            this.lblPopName.TabIndex = 0;
            this.lblPopName.Text = "サーバ名";
            this.grpPOP.Controls.Add(this.ckbFqdn);
            this.grpPOP.Controls.Add(this.label1);
            this.grpPOP.Controls.Add(this.txbPopDomain);
            this.grpPOP.Controls.Add(this.lblPopMailBox);
            this.grpPOP.Controls.Add(this.lblPopName);
            this.grpPOP.Controls.Add(this.txbPopPort);
            this.grpPOP.Controls.Add(this.txbPopName);
            this.grpPOP.Controls.Add(this.lblPopPort);
            this.grpPOP.Location = new Point(12, 0x62);
            this.grpPOP.Name = "grpPOP";
            this.grpPOP.Size = new Size(0x1dc, 0x73);
            this.grpPOP.TabIndex = 1;
            this.grpPOP.TabStop = false;
            this.grpPOP.Text = "POP";
            this.ckbFqdn.AutoSize = true;
            this.ckbFqdn.Location = new Point(0x10, 0x57);
            this.ckbFqdn.Name = "ckbFqdn";
            this.ckbFqdn.Size = new Size(0xbc, 0x10);
            this.ckbFqdn.TabIndex = 7;
            this.ckbFqdn.Text = "POP認証にドメイン付きで認証する";
            this.ckbFqdn.UseVisualStyleBackColor = true;
            this.label1.AutoSize = true;
            this.label1.Location = new Point(0xec, 0x3a);
            this.label1.Name = "label1";
            this.label1.Size = new Size(13, 12);
            this.label1.TabIndex = 5;
            this.label1.Text = "@";
            this.txbPopDomain.ImeMode = ImeMode.Disable;
            this.txbPopDomain.Location = new Point(0xff, 0x37);
            this.txbPopDomain.MaxLength = 0x40;
            this.txbPopDomain.Name = "txbPopDomain";
            this.txbPopDomain.Size = new Size(0xc7, 0x13);
            this.txbPopDomain.TabIndex = 6;
            this.txbPopDomain.Tag = "受信メールボックス";
            this.lblPopMailBox.AutoSize = true;
            this.lblPopMailBox.Location = new Point(14, 0x3a);
            this.lblPopMailBox.Name = "lblPopMailBox";
            this.lblPopMailBox.Size = new Size(0xcc, 12);
            this.lblPopMailBox.TabIndex = 4;
            this.lblPopMailBox.Text = "受信メールボックス　　[　メールボックスID　]";
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnGwCncl;
            base.ClientSize = new Size(0x1f5, 0x107);
            base.Controls.Add(this.grpPOP);
            base.Controls.Add(this.grpSMTP);
            base.Controls.Add(this.btnGwCncl);
            base.Controls.Add(this.btnGwOK);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "dlgGatewaySetting";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ゲートウェイ設定";
            base.Load += new EventHandler(this.GatewaySetting_Load);
            this.grpSMTP.ResumeLayout(false);
            this.grpSMTP.PerformLayout();
            this.grpPOP.ResumeLayout(false);
            this.grpPOP.PerformLayout();
            base.ResumeLayout(false);
        }

        private bool No_Check(string no)
        {
            if ((no.Length > 5) || (no.Length < 1))
            {
                return false;
            }
            foreach (char ch in no.ToUpper().ToCharArray())
            {
                if ((ch < '0') || (ch > '9'))
                {
                    return false;
                }
            }
            if (int.Parse(no) == 0)
            {
                return false;
            }
            return true;
        }

        private void ShowEditErrMessage(ButtonPatern bp, MessageKind mk, string err_str, TextBox txb)
        {
            MessageDialog dialog = new MessageDialog();
            dialog.ShowMessage(bp, mk, string.Concat(new object[] { txb.Tag.ToString(), "は", txb.MaxLength, err_str }));
            dialog.Dispose();
        }

        private void ShowErrMessage(ButtonPatern bp, MessageKind mk, string err_str)
        {
            MessageDialog dialog = new MessageDialog();
            dialog.ShowMessage(bp, mk, err_str);
            dialog.Dispose();
        }

        private bool Text_Check(string text)
        {
            if ((text.Length > 0x40) || (text.Length < 1))
            {
                return false;
            }
            foreach (char ch in text.ToUpper().ToCharArray())
            {
                if ((ch < ' ') || (ch > '\x007f'))
                {
                    return false;
                }
            }
            return true;
        }

        public GatewaySettingClass GatewayInfo
        {
            get
            {
                return this.gatewayInfo;
            }
            set
            {
                this.gatewayInfo = value;
            }
        }
    }
}

