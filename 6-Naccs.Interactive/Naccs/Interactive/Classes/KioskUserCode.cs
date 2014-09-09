namespace Naccs.Interactive.Classes
{
    using Naccs.Core.Classes;
    using Naccs.Core.Main;
    using Naccs.Core.Settings;
    using Naccs.Interactive.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class KioskUserCode : Form, IInteractiveUserCode
    {
        private string _Password;
        private string _UserCode;
        private Button btnCancel;
        private Button btnOK;
        private IContainer components;
        private int DivFlag;
        private Label lblPassword;
        private Label lblUserCode;
        private Panel pnlButton;
        private Panel pnlUserCode;
        private TextBox txbPassword;
        private TextBox txbUserCode;

        public KioskUserCode()
        {
            this.InitializeComponent();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.txbUserCode.Text.Length != 8)
            {
                string message = Resources.ResourceManager.GetString("INTR06");
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, message);
                }
                this.txbUserCode.Focus();
                base.DialogResult = DialogResult.None;
            }
            else if (this.txbPassword.Text.Length == 0)
            {
                string str2 = Resources.ResourceManager.GetString("INTR07");
                using (MessageDialogSimpleForm form2 = new MessageDialogSimpleForm())
                {
                    form2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str2);
                }
                this.txbPassword.Focus();
                base.DialogResult = DialogResult.None;
            }
            this.UserCode = this.txbUserCode.Text;
            this.Password = this.txbPassword.Text;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public void HistoryUserListSet(StringList uc)
        {
            if (uc.Count > 0)
            {
                this.txbUserCode.Text = uc[uc.Count - 1];
            }
        }

        private void InitializeComponent()
        {
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.txbPassword = new TextBox();
            this.lblPassword = new Label();
            this.lblUserCode = new Label();
            this.pnlUserCode = new Panel();
            this.txbUserCode = new TextBox();
            this.pnlButton = new Panel();
            this.pnlUserCode.SuspendLayout();
            this.pnlButton.SuspendLayout();
            base.SuspendLayout();
//            this.btnCancel.DialogResult = DialogResult.Cancel;
//            this.btnCancel.Location = new Point(0xad, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "キャンセル";
            this.btnCancel.UseVisualStyleBackColor = true;
//            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x47, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.txbPassword.CharacterCasing = CharacterCasing.Upper;
            this.txbPassword.Location = new Point(0x6c, 0x2a);
            this.txbPassword.MaxLength = 8;
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new Size(0xbb, 0x13);
            this.txbPassword.TabIndex = 3;
            this.txbPassword.KeyPress += new KeyPressEventHandler(this.UserCode_KeyPress);
            this.lblPassword.AutoEllipsis = true;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(15, 0x2d);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new Size(0x34, 12);
            this.lblPassword.TabIndex = 2;
            this.lblPassword.Text = "パスワード";
            this.lblUserCode.AutoEllipsis = true;
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Location = new Point(15, 13);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new Size(0x34, 12);
            this.lblUserCode.TabIndex = 0;
            this.lblUserCode.Text = "利用者ID";
            this.pnlUserCode.Controls.Add(this.txbUserCode);
            this.pnlUserCode.Controls.Add(this.lblUserCode);
            this.pnlUserCode.Controls.Add(this.lblPassword);
            this.pnlUserCode.Controls.Add(this.txbPassword);
            this.pnlUserCode.Dock = DockStyle.Top;
            this.pnlUserCode.Location = new Point(0, 0);
            this.pnlUserCode.Name = "pnlUserCode";
            this.pnlUserCode.Size = new Size(0x13a, 0x45);
            this.pnlUserCode.TabIndex = 0;
            this.txbUserCode.CharacterCasing = CharacterCasing.Upper;
            this.txbUserCode.Location = new Point(0x6c, 10);
            this.txbUserCode.MaxLength = 8;
            this.txbUserCode.Name = "txbUserCode";
            this.txbUserCode.PasswordChar = '*';
            this.txbUserCode.Size = new Size(0xbb, 0x13);
            this.txbUserCode.TabIndex = 1;
            this.txbUserCode.KeyPress += new KeyPressEventHandler(this.UserCode_KeyPress);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Dock = DockStyle.Top;
            this.pnlButton.Location = new Point(0, 0x45);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new Size(0x13a, 0x26);
            this.pnlButton.TabIndex = 1;
            base.AcceptButton = this.btnOK;
            this.AutoSize = true;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x13a, 0x6a);
            base.Controls.Add(this.pnlButton);
            base.Controls.Add(this.pnlUserCode);
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "SimpleUserCodeDlg";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "利用者ID入力";
            this.pnlUserCode.ResumeLayout(false);
            this.pnlUserCode.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        void IInteractiveUserCode.Close()
        {
            base.Close();
        }

        DialogResult IInteractiveUserCode.ShowDialog()
        {
            return base.ShowDialog();
        }

        private void UserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] chArray = e.KeyChar.ToString().ToUpper().ToCharArray();
            e.KeyChar = chArray[0];
            if ((((e.KeyChar >= '0') && (e.KeyChar <= '9')) || ((e.KeyChar >= 'A') && (e.KeyChar <= 'Z'))) || (e.KeyChar == '\b'))
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public void VisibleCheck(int Flag)
        {
            this.DivFlag = Flag;
        }

        public string Password
        {
            get
            {
                return this._Password;
            }
            set
            {
                this._Password = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this._UserCode;
            }
            set
            {
                this._UserCode = value;
            }
        }
    }
}

