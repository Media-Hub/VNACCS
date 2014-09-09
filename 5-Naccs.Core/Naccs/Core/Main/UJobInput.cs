namespace Naccs.Core.Main
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;
    using System.Deployment.Application;

    public class UJobInput : UserControl
    {
        private Button btnJobOK;
        private ComboBox cmbDispCode;
        private ComboBox cmbJobCode;
        private IContainer components;
        private DispCodeList dcl;
        private bool isDesigning = ((AppDomain.CurrentDomain.FriendlyName == "DefaultDomain") && !ApplicationDeployment.IsNetworkDeployed);
        private Label lblJobCode;
        private Label lblJobInputTitle;
        private Label lblKind;
        private PathInfo pi;
        private Panel pnlJobInputTitle;
        private Panel pnlJobInputWindow;

        public event JobOpenHandler OnJobOpen;

        public UJobInput()
        {
            this.InitializeComponent();
        }

        private void btnJobOK_Click(object sender, EventArgs e)
        {
            if (this.cmbJobCode.Text == "")
            {
                using (MessageDialog dialog = new MessageDialog())
                {
                    dialog.ShowMessage("E582", null, null);
                }
                this.cmbJobCode.Focus();
            }
            else if ((this.cmbDispCode.Items.Count < 1) && (this.DispSet().Count > 0))
            {
                this.cmbDispCode.Focus();
            }
            else
            {
                string str2;
                string text = this.cmbJobCode.Text;
                if (text.IndexOf(" ") > -1)
                {
                    text = text.Remove(text.IndexOf(" "));
                }
                if (text.IndexOf(".") > -1)
                {
                    string str3 = text;
                    int index = str3.IndexOf(".");
                    text = str3.Substring(0, index);
                    str2 = str3.Remove(0, index + 1);
                    if (text == "")
                    {
                        using (MessageDialog dialog2 = new MessageDialog())
                        {
                            dialog2.ShowMessage("E582", null, null);
                        }
                        this.cmbJobCode.Focus();
                        return;
                    }
                    if (str2 == "")
                    {
                        string message = Resources.ResourceManager.GetString("CORE40");
                        using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                        {
                            form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, message);
                        }
                        this.cmbJobCode.Focus();
                        return;
                    }
                }
                else if (this.cmbDispCode.Enabled)
                {
                    str2 = this.cmbDispCode.Text.Substring(0, 3);
                }
                else
                {
                    str2 = "";
                }
                this.OnJobOpen(this, text, str2);
            }
        }

        private void cmbCode_KeyDown(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter) && !((ComboBox) sender).DroppedDown)
            {
                this.btnJobOK_Click(sender, null);
            }
        }

        private void cmbCode_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                e.IsInputKey = true;
            }
            else if (e.KeyCode == Keys.Tab)
            {
                e.IsInputKey = true;
                if (this.DispSet().Count > 0)
                {
                    if (e.Shift)
                    {
                        e.IsInputKey = false;
                    }
                    else
                    {
                        this.cmbDispCode.Focus();
                    }
                }
                else if (e.Shift)
                {
                    e.IsInputKey = false;
                }
                else
                {
                    this.btnJobOK.Focus();
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
            this.pnlJobInputWindow = new System.Windows.Forms.Panel();
            this.btnJobOK = new System.Windows.Forms.Button();
            this.lblKind = new System.Windows.Forms.Label();
            this.lblJobCode = new System.Windows.Forms.Label();
            this.cmbDispCode = new System.Windows.Forms.ComboBox();
            this.cmbJobCode = new System.Windows.Forms.ComboBox();
            this.pnlJobInputTitle = new System.Windows.Forms.Panel();
            this.lblJobInputTitle = new System.Windows.Forms.Label();
            this.pnlJobInputWindow.SuspendLayout();
            this.pnlJobInputTitle.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlJobInputWindow
            // 
            this.pnlJobInputWindow.BackColor = System.Drawing.SystemColors.Control;
            this.pnlJobInputWindow.Controls.Add(this.btnJobOK);
            this.pnlJobInputWindow.Controls.Add(this.lblKind);
            this.pnlJobInputWindow.Controls.Add(this.lblJobCode);
            this.pnlJobInputWindow.Controls.Add(this.cmbDispCode);
            this.pnlJobInputWindow.Controls.Add(this.cmbJobCode);
            this.pnlJobInputWindow.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlJobInputWindow.Location = new System.Drawing.Point(0, 19);
            this.pnlJobInputWindow.Name = "pnlJobInputWindow";
            this.pnlJobInputWindow.Size = new System.Drawing.Size(200, 100);
            this.pnlJobInputWindow.TabIndex = 25;
            // 
            // btnJobOK
            // 
            this.btnJobOK.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnJobOK.Location = new System.Drawing.Point(59, 65);
            this.btnJobOK.Name = "btnJobOK";
            this.btnJobOK.Size = new System.Drawing.Size(75, 23);
            this.btnJobOK.TabIndex = 2;
            this.btnJobOK.Text = "OK";
            this.btnJobOK.UseVisualStyleBackColor = true;
            this.btnJobOK.Click += new System.EventHandler(this.btnJobOK_Click);
            // 
            // lblKind
            // 
            this.lblKind.AutoSize = true;
            this.lblKind.Location = new System.Drawing.Point(39, 41);
            this.lblKind.Name = "lblKind";
            this.lblKind.Size = new System.Drawing.Size(27, 13);
            this.lblKind.TabIndex = 1;
            this.lblKind.Text = "Loại";
            this.lblKind.Visible = false;
            // 
            // lblJobCode
            // 
            this.lblJobCode.AutoSize = true;
            this.lblJobCode.Location = new System.Drawing.Point(11, 16);
            this.lblJobCode.Name = "lblJobCode";
            this.lblJobCode.Size = new System.Drawing.Size(72, 13);
            this.lblJobCode.TabIndex = 1;
            this.lblJobCode.Text = "Mã nghiệp vụ";
            // 
            // cmbDispCode
            // 
            this.cmbDispCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbDispCode.Enabled = false;
            this.cmbDispCode.FormattingEnabled = true;
            this.cmbDispCode.Location = new System.Drawing.Point(83, 38);
            this.cmbDispCode.Name = "cmbDispCode";
            this.cmbDispCode.Size = new System.Drawing.Size(108, 21);
            this.cmbDispCode.TabIndex = 1;
            this.cmbDispCode.Visible = false;
            this.cmbDispCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCode_KeyDown);
            // 
            // cmbJobCode
            // 
            this.cmbJobCode.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbJobCode.FormattingEnabled = true;
            this.cmbJobCode.Location = new System.Drawing.Point(83, 13);
            this.cmbJobCode.MaxLength = 9;
            this.cmbJobCode.Name = "cmbJobCode";
            this.cmbJobCode.Size = new System.Drawing.Size(108, 21);
            this.cmbJobCode.TabIndex = 0;
            this.cmbJobCode.TextChanged += new System.EventHandler(this.cmbJobCode_TextChanged);
            this.cmbJobCode.KeyDown += new System.Windows.Forms.KeyEventHandler(this.cmbCode_KeyDown);
            this.cmbJobCode.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.cmbJobCode_KeyPress);
            this.cmbJobCode.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.cmbCode_PreviewKeyDown);
            // 
            // pnlJobInputTitle
            // 
            this.pnlJobInputTitle.BackColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.pnlJobInputTitle.Controls.Add(this.lblJobInputTitle);
            this.pnlJobInputTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlJobInputTitle.Location = new System.Drawing.Point(0, 0);
            this.pnlJobInputTitle.Name = "pnlJobInputTitle";
            this.pnlJobInputTitle.Size = new System.Drawing.Size(200, 19);
            this.pnlJobInputTitle.TabIndex = 24;
            this.pnlJobInputTitle.Click += new System.EventHandler(this.pnlJobInputTitle_Click);
            // 
            // lblJobInputTitle
            // 
            this.lblJobInputTitle.AutoSize = true;
            this.lblJobInputTitle.Location = new System.Drawing.Point(39, 2);
            this.lblJobInputTitle.Name = "lblJobInputTitle";
            this.lblJobInputTitle.Size = new System.Drawing.Size(118, 13);
            this.lblJobInputTitle.TabIndex = 3;
            this.lblJobInputTitle.Text = "Mã nghiệp vụ VNACCS";
            this.lblJobInputTitle.Click += new System.EventHandler(this.pnlJobInputTitle_Click);
            // 
            // UJobInput
            // 
            this.AutoSize = true;
            this.Controls.Add(this.pnlJobInputWindow);
            this.Controls.Add(this.pnlJobInputTitle);
            this.Name = "UJobInput";
            this.Size = new System.Drawing.Size(200, 119);
            this.Load += new System.EventHandler(this.UJobInput_Load);
            this.pnlJobInputWindow.ResumeLayout(false);
            this.pnlJobInputWindow.PerformLayout();
            this.pnlJobInputTitle.ResumeLayout(false);
            this.pnlJobInputTitle.PerformLayout();
            this.ResumeLayout(false);

        }

        private void pnlJobInputTitle_Click(object sender, EventArgs e)
        {
            this.pnlJobInputWindow.Visible = !this.pnlJobInputWindow.Visible;
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

        private void UJobInput_Load(object sender, EventArgs e)
        {
            if (!this.isDesigning)
            {
                this.pi = PathInfo.CreateInstance();
                this.dcl = new DispCodeList();
            }
        }
    }
}

