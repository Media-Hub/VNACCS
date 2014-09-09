namespace Naccs.Core.Main
{
    using Naccs.Common.Function;
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class UUserCode : UserControl, IUserCode
    {
        private Button btnLogon;
        private ComboManager Cm = new ComboManager();
        private ComboBox cmbUserCode;
        private IContainer components;
        private bool FMailBoxCheck;
        private string FMailBoxID;
        private string FMailBoxPassword;
        private string FPassword;
        private bool FUpperCheck;
        private string FUserCode;
        private Label lblPassword;
        private Label lblUserCode;
        private Label lblUserTitle;
        private Panel pnlLogonWindow;
        private Panel pnlUserTitle;
        private StrFunc SF = StrFunc.CreateInstance();
        private TextBox txbPassword;

        public event OnButtonHandler OnButton;

        public event OnDeleteEventHandler OnDeleteList;

        public UUserCode()
        {
            this.InitializeComponent();
            this.cmbUserCode.ContextMenuStrip = new ContextMenuStrip();
            this.txbPassword.ContextMenuStrip = new ContextMenuStrip();
        }

        private void btnLogon_Click(object sender, EventArgs e)
        {
            if (!this.cmbUserCode.Enabled)
            {
                this.OnButton(false);
            }
            else if (this.cmbUserCode.Text.Length != 8)
            {
                string message = Resources.ResourceManager.GetString("CORE42");
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, message);
                }
                this.cmbUserCode.Focus();
            }
            else if (this.txbPassword.Text.Length == 0)
            {
                string str2 = Resources.ResourceManager.GetString("CORE43");
                using (MessageDialogSimpleForm form2 = new MessageDialogSimpleForm())
                {
                    form2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str2);
                }
                this.txbPassword.Focus();
            }
            else if (!this.SF.ForbiddenPasswordCharCheck(this.txbPassword.Text))
            {
                string str3 = Resources.ResourceManager.GetString("CORE112");
                using (MessageDialogSimpleForm form3 = new MessageDialogSimpleForm())
                {
                    form3.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str3);
                }
                this.txbPassword.Focus();
            }
            else
            {
                this.OnButton(true);
            }
        }

        public void ChangeEnabled(bool EnbFlag)
        {
            this.cmbUserCode.Enabled = EnbFlag;
            this.txbPassword.Enabled = EnbFlag;
            if (EnbFlag)
            {
                this.btnLogon.Text = Resources.ResourceManager.GetString("CORE110");
                this.cmbUserCode.Font = new Font(this.cmbUserCode.Font.FontFamily, this.cmbUserCode.Font.Size, FontStyle.Regular);
            }
            else
            {
                this.btnLogon.Text = Resources.ResourceManager.GetString("CORE111");
                this.cmbUserCode.Font = new Font(this.cmbUserCode.Font.FontFamily, this.cmbUserCode.Font.Size, FontStyle.Bold);
            }
        }

        private void cmbCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.IsInputKey = true;
            }
        }

        private void cmcDle_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string str2;
            if ((((str2 = e.ClickedItem.Name) != null) && (str2 == "TsmHistory")) && ((base.ActiveControl.Name == this.cmbUserCode.Name) && (this.cmbUserCode.Text.Length == 8)))
            {
                string str = this.Cm.DeleteID(this.cmbUserCode);
                if (str != "")
                {
                    this.DeleteList(ListType.UserList, str);
                    this.cmbUserCode.Text = "";
                }
            }
        }

        private void DeleteList(ListType Type, string Value)
        {
            if (this.OnDeleteList != null)
            {
                this.OnDeleteList(false, Type, Value);
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

        public void HistoryUserListSet(StringList uc)
        {
            this.cmbUserCode.Items.Clear();
            for (int i = uc.Count - 1; i > -1; i--)
            {
                this.cmbUserCode.Items.Add(uc[i]);
            }
            if (this.cmbUserCode.Items.Count > 0)
            {
                this.cmbUserCode.Text = this.cmbUserCode.Items[0].ToString();
            }
        }

        public void HistoryUserListSet(StringList uc, StringList mc)
        {
            this.HistoryUserListSet(uc);
        }

        private void InitializeComponent()
        {
            this.pnlLogonWindow = new Panel();
            this.btnLogon = new Button();
            this.txbPassword = new TextBox();
            this.lblPassword = new Label();
            this.lblUserCode = new Label();
            this.cmbUserCode = new ComboBox();
            this.pnlUserTitle = new Panel();
            this.lblUserTitle = new Label();
            this.pnlLogonWindow.SuspendLayout();
            this.pnlUserTitle.SuspendLayout();
            base.SuspendLayout();
            this.pnlLogonWindow.BackColor = SystemColors.Control;
            //this.pnlLogonWindow.BorderStyle = BorderStyle.FixedSingle;
            this.pnlLogonWindow.Controls.Add(this.btnLogon);
            this.pnlLogonWindow.Controls.Add(this.txbPassword);
            this.pnlLogonWindow.Controls.Add(this.lblPassword);
            this.pnlLogonWindow.Controls.Add(this.lblUserCode);
            this.pnlLogonWindow.Controls.Add(this.cmbUserCode);
            this.pnlLogonWindow.Dock = DockStyle.Top;
            this.pnlLogonWindow.Location = new Point(0, 20);
            this.pnlLogonWindow.Name = "pnlLogonWindow";
            this.pnlLogonWindow.Size = new System.Drawing.Size(0xca, 0x53);
            this.pnlLogonWindow.TabIndex = 0x1b;
            this.btnLogon.Anchor = AnchorStyles.None;
            this.btnLogon.Location = new Point(0x3b, 0x33);
            this.btnLogon.Name = "btnLogon";
            this.btnLogon.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnLogon.TabIndex = 2;
            this.btnLogon.Text = "Đăng nhập";
            this.btnLogon.UseVisualStyleBackColor = true;
            this.btnLogon.Click += new EventHandler(this.btnLogon_Click);
            this.txbPassword.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
//            this.txbPassword.ImeMode = ImeMode.Close;
            this.txbPassword.Location = new Point(0x5d, 0x1a);
            this.txbPassword.MaxLength = 8;
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(0x66, 0x13);
            this.txbPassword.TabIndex = 1;
            this.txbPassword.KeyDown += new KeyEventHandler(this.UserCode_KeyDown);
            this.txbPassword.KeyPress += new KeyPressEventHandler(this.UserCode_KeyPress);
            this.txbPassword.PreviewKeyDown += new PreviewKeyDownEventHandler(this.cmbCode_PreviewKeyDown);
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(1, 0x1d);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(50, 12);
            this.lblPassword.TabIndex = 1;
            this.lblPassword.Text = "Mật khẩu";
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Location = new Point(1, 8);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(0x5b, 12);
            this.lblUserCode.TabIndex = 1;
            this.lblUserCode.Text = "M\x00e3 người sử dụng";
            this.cmbUserCode.Anchor = AnchorStyles.Right | AnchorStyles.Left | AnchorStyles.Top;
            this.cmbUserCode.FormattingEnabled = true;
//            this.cmbUserCode.ImeMode = ImeMode.Close;
            this.cmbUserCode.Location = new Point(0x5d, 3);
            this.cmbUserCode.MaxLength = 8;
            this.cmbUserCode.Name = "cmbUserCode";
            this.cmbUserCode.Size = new System.Drawing.Size(0x66, 20);
            this.cmbUserCode.TabIndex = 0;
            this.cmbUserCode.KeyDown += new KeyEventHandler(this.UserCode_KeyDown);
            this.cmbUserCode.KeyPress += new KeyPressEventHandler(this.UserCode_KeyPress);
            this.cmbUserCode.PreviewKeyDown += new PreviewKeyDownEventHandler(this.cmbCode_PreviewKeyDown);
            this.pnlUserTitle.BackColor = SystemColors.GradientInactiveCaption;
            this.pnlUserTitle.Controls.Add(this.lblUserTitle);
            this.pnlUserTitle.Dock = DockStyle.Top;
            this.pnlUserTitle.Location = new Point(0, 0);
            this.pnlUserTitle.Name = "pnlUserTitle";
            this.pnlUserTitle.Size = new System.Drawing.Size(0xca, 20);
            this.pnlUserTitle.TabIndex = 0x1a;
            this.pnlUserTitle.Click += new EventHandler(this.UserTitle_Click);
            this.lblUserTitle.AutoSize = true;
            this.lblUserTitle.Location = new Point(0x26, 5);
            this.lblUserTitle.Name = "lblUserTitle";
            this.lblUserTitle.Size = new System.Drawing.Size(120, 12);
            this.lblUserTitle.TabIndex = 1;
            this.lblUserTitle.Text = "X\x00e1c thực người sử dụng";
            this.lblUserTitle.Click += new EventHandler(this.UserTitle_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            this.AutoSize = true;
            base.Controls.Add(this.pnlLogonWindow);
            base.Controls.Add(this.pnlUserTitle);
            base.Name = "UUserCode";
            base.Size = new System.Drawing.Size(0xca, 0xa1);
            this.pnlLogonWindow.ResumeLayout(false);
            this.pnlLogonWindow.PerformLayout();
            this.pnlUserTitle.ResumeLayout(false);
            this.pnlUserTitle.PerformLayout();
            base.ResumeLayout(false);
        }

        private void UserCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if ((sender is ComboBox) && ((ComboBox) sender).DroppedDown)
                {
                    return;
                }
                this.btnLogon.Focus();
                this.btnLogon_Click(sender, null);
            }
            if ((e.KeyCode == Keys.Delete) && (this.cmbUserCode.Text.Length == 8))
            {
                string str = this.Cm.DeleteID(this.cmbUserCode);
                if (str != "")
                {
                    this.DeleteList(ListType.UserList, str);
                    this.cmbUserCode.Text = "";
                }
            }
        }

        private void UserCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!(sender is ComboBox) || !((ComboBox) sender).DroppedDown)
            {
                if (sender == this.txbPassword)
                {
                    if (this.SF.ForbiddenPasswordCharCheck(e.KeyChar, false) || (e.KeyChar == '\b'))
                    {
                        e.Handled = false;
                    }
                    else
                    {
                        e.Handled = true;
                    }
                }
                else
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
            }
        }

        private void UserTitle_Click(object sender, EventArgs e)
        {
            this.pnlLogonWindow.Visible = !this.pnlLogonWindow.Visible;
        }

        public bool MailBoxCheck
        {
            get
            {
                return this.FMailBoxCheck;
            }
            set
            {
                this.FMailBoxCheck = value;
            }
        }

        public string MailBoxID
        {
            get
            {
                return this.FMailBoxID;
            }
            set
            {
                this.FMailBoxID = value;
            }
        }

        public string MailBoxPassword
        {
            get
            {
                return this.FMailBoxPassword;
            }
            set
            {
                this.FMailBoxPassword = value;
            }
        }

        public string Password
        {
            get
            {
                return this.txbPassword.Text;
            }
            set
            {
                this.FPassword = value;
                this.txbPassword.Text = value;
            }
        }

        public bool UpperCheck
        {
            get
            {
                return this.FUpperCheck;
            }
            set
            {
                this.FUpperCheck = value;
            }
        }

        public string UserCode
        {
            get
            {
                return this.cmbUserCode.Text;
            }
            set
            {
                this.FUserCode = value;
                this.cmbUserCode.Text = value;
            }
        }
    }
}

