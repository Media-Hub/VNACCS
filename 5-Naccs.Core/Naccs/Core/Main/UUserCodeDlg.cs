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

    public class UUserCodeDlg : Form, IInteractiveUserCodeEx, IInteractiveUserCode
    {
        private string _Password;
        private string _UserCode;
        private Button btnCancel;
        private Button btnOK;
        private CheckBox chkMailBoxID;
        private CheckBox chkUpper;
        private ComboManager Cm = new ComboManager();
        private ComboBox cmbMailBoxID;
        private ComboBox cmbUserCode;
        private IContainer components;
        private int DivFlag;
        private bool FUpperCheck;
        private Label lblMailBoxID;
        private Label lblMailPassword;
        private Label lblPassword;
        private Label lblUserCode;
        public string MailBoxID;
        public bool MailBoxIDCheck;
        public string MailBoxPassword;
        private Panel pnlButton;
        private Panel pnlMailBox;
        private Panel pnlMailBoxCheck;
        private Panel pnlUserCode;
        private StrFunc SF = StrFunc.CreateInstance();
        private TextBox txbPassword;
        private TextBox txtMailPassword;

        public event OnDeleteEventHandler OnDeleteList;

        public UUserCodeDlg()
        {
            this.InitializeComponent();
            this.cmbUserCode.ContextMenuStrip = new ContextMenuStrip();
            this.txbPassword.ContextMenuStrip = new ContextMenuStrip();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cmbUserCode.Text.Length != 8)
            {
                string message = Resources.ResourceManager.GetString("CORE42");
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, message);
                }
                this.cmbUserCode.Focus();
                base.DialogResult = DialogResult.None;
            }
            else if (this.txbPassword.Text.Length == 0)
            {
                string str2 = Resources.ResourceManager.GetString("CORE43");
                using (MessageDialogSimpleForm form2 = new MessageDialogSimpleForm())
                {
                    form2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str2);
                }
                this.txbPassword.Focus();
                base.DialogResult = DialogResult.None;
            }
            else if (!this.SF.ForbiddenPasswordCharCheck(this.txbPassword.Text))
            {
                string str3 = Resources.ResourceManager.GetString("CORE112");
                using (MessageDialogSimpleForm form3 = new MessageDialogSimpleForm())
                {
                    form3.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str3);
                }
                this.txbPassword.Focus();
                base.DialogResult = DialogResult.None;
            }
            else if (this.DivFlag != 0)
            {
                char ch = this.cmbUserCode.Text[this.cmbUserCode.Text.Length - 1];
                if (!this.SF.IsNumeric(ch.ToString()))
                {
                    string str4 = Resources.ResourceManager.GetString("CORE44");
                    using (MessageDialogSimpleForm form4 = new MessageDialogSimpleForm())
                    {
                        form4.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str4);
                    }
                    this.cmbUserCode.Focus();
                    base.DialogResult = DialogResult.None;
                    return;
                }
                if (!this.chkMailBoxID.Checked)
                {
                    if (this.cmbMailBoxID.Text.Length == 0)
                    {
                        string str5 = Resources.ResourceManager.GetString("CORE45");
                        using (MessageDialogSimpleForm form5 = new MessageDialogSimpleForm())
                        {
                            form5.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str5);
                        }
                        this.cmbMailBoxID.Focus();
                        base.DialogResult = DialogResult.None;
                    }
                    else if (this.cmbMailBoxID.Text.Trim().Length == 0)
                    {
                        string str6 = Resources.ResourceManager.GetString("CORE46");
                        using (MessageDialogSimpleForm form6 = new MessageDialogSimpleForm())
                        {
                            form6.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str6);
                        }
                        this.cmbMailBoxID.Focus();
                        base.DialogResult = DialogResult.None;
                    }
                    else if (this.txtMailPassword.Text.Length == 0)
                    {
                        string str7 = Resources.ResourceManager.GetString("CORE47");
                        using (MessageDialogSimpleForm form7 = new MessageDialogSimpleForm())
                        {
                            form7.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str7);
                        }
                        this.txtMailPassword.Focus();
                        base.DialogResult = DialogResult.None;
                    }
                    else if (this.txtMailPassword.Text.Trim().Length == 0)
                    {
                        string str8 = Resources.ResourceManager.GetString("CORE48");
                        using (MessageDialogSimpleForm form8 = new MessageDialogSimpleForm())
                        {
                            form8.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str8);
                        }
                        this.txtMailPassword.Focus();
                        base.DialogResult = DialogResult.None;
                    }
                }
            }
            this.UserCode = this.cmbUserCode.Text;
            this.Password = this.txbPassword.Text;
            if (this.chkMailBoxID.Checked)
            {
                this.MailBoxID = this.cmbUserCode.Text;
                this.MailBoxPassword = this.txbPassword.Text;
            }
            else
            {
                this.MailBoxID = this.cmbMailBoxID.Text;
                this.MailBoxPassword = this.txtMailPassword.Text;
            }
            this.MailBoxIDCheck = this.chkMailBoxID.Checked;
            this.UpperCheck = this.chkUpper.Checked;
        }

        private void chkMailBoxID_CheckedChanged(object sender, EventArgs e)
        {
            this.pnlMailBox.Visible = !this.chkMailBoxID.Checked;
            base.Height = 0;
        }

        private void chkUpper_CheckedChanged(object sender, EventArgs e)
        {
            if (this.chkUpper.Checked)
            {
                this.cmbMailBoxID.Text = this.cmbMailBoxID.Text.ToUpper();
                this.txtMailPassword.Text = this.txtMailPassword.Text.ToUpper();
            }
        }

        private void cmbMailBoxID_DropDownClosed(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                Interval = 3
            };
            timer.Tick += new EventHandler(this.tmSC_Tick);
            timer.Enabled = true;
        }

        private void cmbMailBoxID_SelectedIndexChanged(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer {
                Interval = 3
            };
            timer.Tick += new EventHandler(this.tmSC_Tick);
            timer.Enabled = true;
        }

        private void cmsDleDlg_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            string str2;
            if (((str2 = e.ClickedItem.Name) != null) && (str2 == "TsmHistoryDlg"))
            {
                string str;
                if (base.ActiveControl.Name == this.cmbUserCode.Name)
                {
                    if (this.cmbUserCode.Text.Length == 8)
                    {
                        str = this.Cm.DeleteID(this.cmbUserCode);
                        if (str != "")
                        {
                            this.DeleteList(ListType.UserList, str);
                            this.cmbUserCode.Text = "";
                        }
                    }
                }
                else if (base.ActiveControl.Name == this.cmbMailBoxID.Name)
                {
                    str = this.Cm.DeleteID(this.cmbMailBoxID);
                    if (str != "")
                    {
                        this.DeleteList(ListType.MailList, str);
                        this.cmbMailBoxID.Text = "";
                    }
                }
            }
        }

        private void DeleteList(ListType Type, string Value)
        {
            if (this.OnDeleteList != null)
            {
                this.OnDeleteList(true, Type, Value);
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
            this.cmbMailBoxID.Items.Clear();
            for (int i = mc.Count - 1; i > -1; i--)
            {
                this.cmbMailBoxID.Items.Add(mc[i]);
            }
            this.setDropDownWidth();
            if (this.cmbMailBoxID.Items.Count > 0)
            {
                this.cmbMailBoxID.Text = this.cmbMailBoxID.Items[0].ToString();
            }
            this.HistoryUserListSet(uc);
        }

        private void InitializeComponent()
        {
            this.btnCancel = new Button();
            this.btnOK = new Button();
            this.txbPassword = new TextBox();
            this.lblPassword = new Label();
            this.lblUserCode = new Label();
            this.cmbUserCode = new ComboBox();
            this.pnlUserCode = new Panel();
            this.pnlMailBoxCheck = new Panel();
            this.chkMailBoxID = new CheckBox();
            this.pnlMailBox = new Panel();
            this.chkUpper = new CheckBox();
            this.lblMailBoxID = new Label();
            this.cmbMailBoxID = new ComboBox();
            this.lblMailPassword = new Label();
            this.txtMailPassword = new TextBox();
            this.pnlButton = new Panel();
            this.pnlUserCode.SuspendLayout();
            this.pnlMailBoxCheck.SuspendLayout();
            this.pnlMailBox.SuspendLayout();
            this.pnlButton.SuspendLayout();
            base.SuspendLayout();
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0xad, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0x47, 6);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(0x4b, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.txbPassword.Location = new Point(0x6c, 0x2a);
            this.txbPassword.MaxLength = 8;
            this.txbPassword.Name = "txbPassword";
            this.txbPassword.PasswordChar = '*';
            this.txbPassword.Size = new System.Drawing.Size(0xbb, 0x13);
            this.txbPassword.TabIndex = 1;
            this.txbPassword.KeyPress += new KeyPressEventHandler(this.UserCode_KeyPress);
            this.lblPassword.AutoEllipsis = true;
            this.lblPassword.AutoSize = true;
            this.lblPassword.Location = new Point(15, 0x2d);
            this.lblPassword.Name = "lblPassword";
            this.lblPassword.Size = new System.Drawing.Size(50, 12);
            this.lblPassword.TabIndex = 8;
            this.lblPassword.Text = "Mật khẩu";
            this.lblUserCode.AutoEllipsis = true;
            this.lblUserCode.AutoSize = true;
            this.lblUserCode.Location = new Point(15, 13);
            this.lblUserCode.Name = "lblUserCode";
            this.lblUserCode.Size = new System.Drawing.Size(0x5b, 12);
            this.lblUserCode.TabIndex = 7;
            this.lblUserCode.Text = "M\x00e3 người sử dụng";
            this.cmbUserCode.FormattingEnabled = true;
            this.cmbUserCode.Location = new Point(0x6c, 10);
            this.cmbUserCode.MaxLength = 8;
            this.cmbUserCode.Name = "cmbUserCode";
            this.cmbUserCode.Size = new System.Drawing.Size(0xbb, 20);
            this.cmbUserCode.TabIndex = 0;
            this.cmbUserCode.KeyDown += new KeyEventHandler(this.UserCode_KeyDown);
            this.cmbUserCode.KeyPress += new KeyPressEventHandler(this.UserCode_KeyPress);
            this.pnlUserCode.Controls.Add(this.lblUserCode);
            this.pnlUserCode.Controls.Add(this.cmbUserCode);
            this.pnlUserCode.Controls.Add(this.lblPassword);
            this.pnlUserCode.Controls.Add(this.txbPassword);
            this.pnlUserCode.Dock = DockStyle.Top;
            this.pnlUserCode.Location = new Point(0, 0);
            this.pnlUserCode.Name = "pnlUserCode";
            this.pnlUserCode.Size = new System.Drawing.Size(0x13a, 0x45);
            this.pnlUserCode.TabIndex = 9;
            this.pnlMailBoxCheck.Controls.Add(this.chkMailBoxID);
            this.pnlMailBoxCheck.Dock = DockStyle.Top;
            this.pnlMailBoxCheck.Location = new Point(0, 0x45);
            this.pnlMailBoxCheck.Name = "pnlMailBoxCheck";
            this.pnlMailBoxCheck.Size = new System.Drawing.Size(0x13a, 0x19);
            this.pnlMailBoxCheck.TabIndex = 10;
            this.chkMailBoxID.AutoSize = true;
            this.chkMailBoxID.Location = new Point(0x11, 5);
            this.chkMailBoxID.Name = "chkMailBoxID";
            this.chkMailBoxID.Size = new System.Drawing.Size(0x9a, 0x10);
            this.chkMailBoxID.TabIndex = 0;
            this.chkMailBoxID.Text = "メールボックスID入力を省略";
            this.chkMailBoxID.UseVisualStyleBackColor = true;
            this.chkMailBoxID.CheckedChanged += new EventHandler(this.chkMailBoxID_CheckedChanged);
            this.pnlMailBox.Controls.Add(this.chkUpper);
            this.pnlMailBox.Controls.Add(this.lblMailBoxID);
            this.pnlMailBox.Controls.Add(this.cmbMailBoxID);
            this.pnlMailBox.Controls.Add(this.lblMailPassword);
            this.pnlMailBox.Controls.Add(this.txtMailPassword);
            this.pnlMailBox.Dock = DockStyle.Top;
            this.pnlMailBox.Location = new Point(0, 0x5e);
            this.pnlMailBox.Name = "pnlMailBox";
            this.pnlMailBox.Size = new System.Drawing.Size(0x13a, 0x59);
            this.pnlMailBox.TabIndex = 11;
            this.chkUpper.AutoSize = true;
            this.chkUpper.Location = new Point(0x11, 0x43);
            this.chkUpper.Name = "chkUpper";
            this.chkUpper.Size = new System.Drawing.Size(0x70, 0x10);
            this.chkUpper.TabIndex = 9;
            this.chkUpper.Text = "大文字に変換する";
            this.chkUpper.UseVisualStyleBackColor = true;
            this.chkUpper.CheckedChanged += new EventHandler(this.chkUpper_CheckedChanged);
            this.lblMailBoxID.AutoEllipsis = true;
            this.lblMailBoxID.AutoSize = true;
            this.lblMailBoxID.Location = new Point(15, 13);
            this.lblMailBoxID.Name = "lblMailBoxID";
            this.lblMailBoxID.Size = new System.Drawing.Size(0x4e, 12);
            this.lblMailBoxID.TabIndex = 7;
            this.lblMailBoxID.Text = "メールボックスID";
            this.cmbMailBoxID.FormattingEnabled = true;
            this.cmbMailBoxID.ImeMode = ImeMode.Disable;
            this.cmbMailBoxID.Location = new Point(0x6c, 10);
            this.cmbMailBoxID.MaxLength = 0x40;
            this.cmbMailBoxID.Name = "cmbMailBoxID";
            this.cmbMailBoxID.Size = new System.Drawing.Size(0xbb, 20);
            this.cmbMailBoxID.TabIndex = 0;
            this.cmbMailBoxID.SelectedIndexChanged += new EventHandler(this.cmbMailBoxID_SelectedIndexChanged);
            this.cmbMailBoxID.DropDownClosed += new EventHandler(this.cmbMailBoxID_DropDownClosed);
            this.cmbMailBoxID.KeyDown += new KeyEventHandler(this.MailBoxID_KeyDown);
            this.cmbMailBoxID.KeyPress += new KeyPressEventHandler(this.MailBoxID_KeyPress);
            this.lblMailPassword.AutoEllipsis = true;
            this.lblMailPassword.AutoSize = true;
            this.lblMailPassword.Location = new Point(15, 0x2d);
            this.lblMailPassword.Name = "lblMailPassword";
            this.lblMailPassword.Size = new System.Drawing.Size(0x34, 12);
            this.lblMailPassword.TabIndex = 8;
            this.lblMailPassword.Text = "パスワード";
            this.txtMailPassword.ImeMode = ImeMode.Disable;
            this.txtMailPassword.Location = new Point(0x6c, 0x2a);
            this.txtMailPassword.MaxLength = 0x10;
            this.txtMailPassword.Name = "txtMailPassword";
            this.txtMailPassword.PasswordChar = '*';
            this.txtMailPassword.Size = new System.Drawing.Size(0xbb, 0x13);
            this.txtMailPassword.TabIndex = 1;
            this.txtMailPassword.KeyPress += new KeyPressEventHandler(this.MailBoxID_KeyPress);
            this.pnlButton.Controls.Add(this.btnCancel);
            this.pnlButton.Controls.Add(this.btnOK);
            this.pnlButton.Dock = DockStyle.Top;
            this.pnlButton.Location = new Point(0, 0xb7);
            this.pnlButton.Name = "pnlButton";
            this.pnlButton.Size = new System.Drawing.Size(0x13a, 0x26);
            this.pnlButton.TabIndex = 12;
            base.AcceptButton = this.btnOK;
            this.AutoSize = true;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new System.Drawing.Size(0x13a, 0x123);
            base.Controls.Add(this.pnlButton);
            base.Controls.Add(this.pnlMailBox);
            base.Controls.Add(this.pnlMailBoxCheck);
            base.Controls.Add(this.pnlUserCode);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UUserCodeDlg";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "X\x00e1c thực người sử dụng";
            this.pnlUserCode.ResumeLayout(false);
            this.pnlUserCode.PerformLayout();
            this.pnlMailBoxCheck.ResumeLayout(false);
            this.pnlMailBoxCheck.PerformLayout();
            this.pnlMailBox.ResumeLayout(false);
            this.pnlMailBox.PerformLayout();
            this.pnlButton.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        private void MailBoxID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                string str = this.Cm.DeleteID(this.cmbMailBoxID);
                if (str != "")
                {
                    this.DeleteList(ListType.MailList, str);
                    this.cmbMailBoxID.Text = "";
                }
            }
        }

        private void MailBoxID_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] chArray;
            if (this.chkUpper.Checked)
            {
                chArray = e.KeyChar.ToString().ToUpper().ToCharArray();
            }
            else
            {
                chArray = e.KeyChar.ToString().ToCharArray();
            }
            e.KeyChar = chArray[0];
            if ((!(sender is ComboBox) || !((ComboBox) sender).DroppedDown) && ((e.KeyChar != '\b') && ((e.KeyChar < ' ') || (e.KeyChar > '\x007f'))))
            {
                e.Handled = true;
            }
        }

        void IInteractiveUserCode.Close()
        {
            base.Close();
        }

        DialogResult IInteractiveUserCode.ShowDialog()
        {
            return base.ShowDialog();
        }

        private void setDropDownWidth()
        {
            Graphics graphics = base.CreateGraphics();
            float num = 0f;
            for (int i = 0; i < this.cmbMailBoxID.Items.Count; i++)
            {
                num = Math.Max(num, graphics.MeasureString(this.cmbMailBoxID.Items[i].ToString(), this.cmbMailBoxID.Font).Width);
            }
            int num3 = (int) decimal.Round((decimal) num, 0);
            num3 += 30;
            this.cmbMailBoxID.DropDownWidth = Math.Max(base.Width, num3);
            graphics.Dispose();
        }

        private void tmSC_Tick(object sender, EventArgs e)
        {
            System.Windows.Forms.Timer timer = (System.Windows.Forms.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            if (this.chkUpper.Checked)
            {
                this.cmbMailBoxID.Text = this.cmbMailBoxID.Text.ToUpper();
                this.txtMailPassword.Text = this.txtMailPassword.Text.ToUpper();
            }
        }

        private void UserCode_KeyDown(object sender, KeyEventArgs e)
        {
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

        public void VisibleCheck(int Flag)
        {
            if (Flag == 0)
            {
                this.pnlMailBoxCheck.Visible = false;
                this.pnlMailBox.Visible = false;
            }
            else
            {
                this.chkMailBoxID.Checked = this.MailBoxIDCheck;
            }
            base.Height = 0;
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

        public bool UpperCheck
        {
            get
            {
                return this.chkUpper.Checked;
            }
            set
            {
                this.FUpperCheck = value;
                this.chkUpper.Checked = value;
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

