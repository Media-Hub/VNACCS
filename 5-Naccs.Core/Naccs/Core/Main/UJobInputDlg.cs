namespace Naccs.Core.Main
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class UJobInputDlg : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private ComboBox cmbDispCode;
        private ComboBox cmbJobCode;
        private IContainer components;
        private DispCodeList dcl;
        public string DispCode;
        public string JobCode;
        private Label lblJobCode;
        private Label lblKind;
        private PathInfo pi;

        public UJobInputDlg()
        {
            this.InitializeComponent();
            this.pi = PathInfo.CreateInstance();
            this.dcl = new DispCodeList();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            if (this.cmbJobCode.Text == "")
            {
                using (MessageDialog dialog = new MessageDialog())
                {
                    dialog.ShowMessage("E582", null, null);
                }
                this.cmbJobCode.Focus();
                base.DialogResult = DialogResult.None;
            }
            else if ((this.cmbDispCode.Items.Count < 1) && (this.DispSet().Count > 0))
            {
                this.cmbDispCode.Focus();
                base.DialogResult = DialogResult.None;
            }
            else
            {
                this.JobCode = this.cmbJobCode.Text;
                if (this.JobCode.IndexOf(" ") > -1)
                {
                    this.JobCode = this.JobCode.Remove(this.JobCode.IndexOf(" "));
                }
                if (this.JobCode.IndexOf(".") > -1)
                {
                    string jobCode = this.JobCode;
                    int index = jobCode.IndexOf(".");
                    this.JobCode = jobCode.Substring(0, index);
                    this.DispCode = jobCode.Remove(0, index + 1);
                    if (this.JobCode == "")
                    {
                        using (MessageDialog dialog2 = new MessageDialog())
                        {
                            dialog2.ShowMessage("E582", null, null);
                        }
                        this.cmbJobCode.Focus();
                        base.DialogResult = DialogResult.None;
                    }
                    else if (this.DispCode == "")
                    {
                        string message = Resources.ResourceManager.GetString("CORE40");
                        using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                        {
                            form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, message);
                        }
                        this.cmbJobCode.Focus();
                        base.DialogResult = DialogResult.None;
                    }
                }
                else
                {
                    if (this.cmbDispCode.Enabled)
                    {
                        this.DispCode = this.cmbDispCode.Text.Substring(0, 3);
                    }
                    else
                    {
                        this.DispCode = "";
                    }
                    int length = -1;
                    length = this.JobCode.IndexOf(" ");
                    if (length != -1)
                    {
                        this.JobCode = this.JobCode.Substring(0, length);
                    }
                }
            }
        }

        private void cmbCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && !((ComboBox) sender).DroppedDown)
            {
                this.btnOK.Focus();
            }
        }

        private void cmbCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
                if (this.DispSet().Count > 0)
                {
                    this.cmbDispCode.Focus();
                }
                else
                {
                    this.btnOK.Focus();
                }
            }
        }

        private void cmbJobCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            char[] chArray = e.KeyChar.ToString().ToUpper().ToCharArray();
            e.KeyChar = chArray[0];
            if (!((ComboBox) sender).DroppedDown)
            {
                if ((((e.KeyChar >= '0') && (e.KeyChar <= '9')) || ((e.KeyChar >= 'A') && (e.KeyChar <= 'Z'))) || ((e.KeyChar == '\b') || (e.KeyChar == '.')))
                {
                    e.Handled = false;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void cmbJobCode_TextChanged(object sender, EventArgs e)
        {
            if (this.cmbDispCode.Items.Count > 0)
            {
                this.cmbDispCode.Items.Clear();
                this.cmbDispCode.Enabled = false;
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

        private List<string> DispSet()
        {
            this.cmbDispCode.Items.Clear();
            List<string> list = this.dcl.DispCodeGet(this.GetJobCode());
            try
            {
                if (list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        this.cmbDispCode.Items.Add(list[i]);
                    }
                    this.setDropDownWidth(this.cmbDispCode);
                    this.cmbDispCode.Enabled = true;
                    this.cmbDispCode.SelectedIndex = 0;
                    this.cmbDispCode.Focus();
                    return list;
                }
                this.cmbDispCode.Enabled = false;
            }
            catch (Exception exception)
            {
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, MessageDialog.CreateExceptionMessage(exception));
                }
                return list;
            }
            return list;
        }

        private string GetJobCode()
        {
            string text = this.cmbJobCode.Text;
            int length = -1;
            length = text.IndexOf(" ");
            if (length != -1)
            {
                text = text.Substring(0, length);
            }
            return text;
        }

        public void HistoryJobListSet(StringList jl)
        {
            this.cmbJobCode.Items.Clear();
            for (int i = jl.Count - 1; i > -1; i--)
            {
                this.cmbJobCode.Items.Add(jl[i]);
            }
            this.setDropDownWidth(this.cmbJobCode);
            if (this.cmbJobCode.Items.Count > 0)
            {
                this.cmbJobCode.Text = this.cmbJobCode.Items[0].ToString();
            }
        }

        private void InitializeComponent()
        {
            this.cmbJobCode = new ComboBox();
            this.cmbDispCode = new ComboBox();
            this.lblJobCode = new Label();
            this.lblKind = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            base.SuspendLayout();
            this.cmbJobCode.FormattingEnabled = true;
            this.cmbJobCode.Location = new Point(0x59, 12);
            this.cmbJobCode.MaxLength = 9;
            this.cmbJobCode.Name = "cmbJobCode";
            this.cmbJobCode.Size = new Size(400, 20);
            this.cmbJobCode.TabIndex = 0;
            this.cmbJobCode.TextChanged += new EventHandler(this.cmbJobCode_TextChanged);
            this.cmbJobCode.KeyDown += new KeyEventHandler(this.cmbCode_KeyDown);
            this.cmbJobCode.KeyPress += new KeyPressEventHandler(this.cmbJobCode_KeyPress);
            this.cmbJobCode.PreviewKeyDown += new PreviewKeyDownEventHandler(this.cmbCode_PreviewKeyDown);
            this.cmbDispCode.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbDispCode.Enabled = false;
            this.cmbDispCode.FormattingEnabled = true;
            this.cmbDispCode.Location = new Point(0x59, 0x30);
            this.cmbDispCode.Name = "cmbDispCode";
            this.cmbDispCode.Size = new Size(0x145, 20);
            this.cmbDispCode.TabIndex = 1;
            this.cmbDispCode.Visible = false;
            this.cmbDispCode.KeyDown += new KeyEventHandler(this.cmbCode_KeyDown);
            this.lblJobCode.AutoSize = true;
            this.lblJobCode.Location = new Point(12, 15);
            this.lblJobCode.Name = "lblJobCode";
            this.lblJobCode.Size = new Size(0x47, 12);
            this.lblJobCode.TabIndex = 2;
            this.lblJobCode.Text = "M\x00e3 nghiệp vụ";
            this.lblKind.AutoSize = true;
            this.lblKind.Location = new Point(12, 0x33);
            this.lblKind.Name = "lblKind";
            this.lblKind.Size = new Size(0x19, 12);
            this.lblKind.TabIndex = 3;
            this.lblKind.Text = "Loại";
            this.lblKind.Visible = false;
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xaf, 0x26);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x4b, 0x17);
            this.btnOK.TabIndex = 2;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(280, 0x26);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x4b, 0x17);
            this.btnCancel.TabIndex = 3;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x1f5, 0x4c);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.lblKind);
            base.Controls.Add(this.lblJobCode);
            base.Controls.Add(this.cmbDispCode);
            base.Controls.Add(this.cmbJobCode);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "UJobInputDlg";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "M\x00e3 nghiệp vụ VNACCS";
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        public void SetCodeSelect(string JobCode)
        {
            this.cmbJobCode.Text = JobCode;
            this.DispSet();
            this.cmbJobCode.Enabled = false;
        }

        private void setDropDownWidth(ComboBox cmb)
        {
            Graphics graphics = base.CreateGraphics();
            float num = 0f;
            for (int i = 0; i < cmb.Items.Count; i++)
            {
                num = Math.Max(num, graphics.MeasureString(cmb.Items[i].ToString(), cmb.Font).Width);
            }
            int num3 = (int) decimal.Round((decimal) num, 0);
            num3 += 30;
            cmb.DropDownWidth = Math.Max(cmb.Width, num3);
            graphics.Dispose();
        }
    }
}

