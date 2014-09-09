namespace Naccs.Core.Classes
{
    using Naccs.Core.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    public class MessageDialogSimpleForm : Form
    {
        private Button btnCANCEL;
        private Button btnNO;
        private Button btnOK;
        private Button btnYES;
        private IContainer components;
        private FlowLayoutPanel flwPnlDown;
        private Icon icon;
        private PictureBox pictureBoxIcon;
        private RichTextBox rTxbMessage;
        private TableLayoutPanel tblPnlRoot;
        private TableLayoutPanel tblPnlTop;

        public MessageDialogSimpleForm()
        {
            this.InitializeComponent();
            this.rTxbMessage.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.pictureBoxIcon = new PictureBox();
            this.tblPnlRoot = new TableLayoutPanel();
            this.tblPnlTop = new TableLayoutPanel();
            this.rTxbMessage = new RichTextBox();
            this.flwPnlDown = new FlowLayoutPanel();
            this.btnOK = new Button();
            this.btnCANCEL = new Button();
            this.btnYES = new Button();
            this.btnNO = new Button();
//            ((ISupportInitialize) this.pictureBoxIcon).BeginInit();
            this.tblPnlRoot.SuspendLayout();
            this.tblPnlTop.SuspendLayout();
            base.SuspendLayout();
            this.pictureBoxIcon.Location = new Point(3, 3);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new Size(30, 30);
            this.pictureBoxIcon.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxIcon.TabIndex = 1;
            this.pictureBoxIcon.TabStop = false;
            this.tblPnlRoot.ColumnCount = 1;
            this.tblPnlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tblPnlRoot.Controls.Add(this.tblPnlTop, 0, 0);
            this.tblPnlRoot.Controls.Add(this.flwPnlDown, 0, 1);
            this.tblPnlRoot.Dock = DockStyle.Fill;
            this.tblPnlRoot.Location = new Point(0, 0);
            this.tblPnlRoot.Name = "tblPnlRoot";
            this.tblPnlRoot.RowCount = 2;
            this.tblPnlRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tblPnlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
            this.tblPnlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tblPnlRoot.Size = new Size(0x124, 0x88);
            this.tblPnlRoot.TabIndex = 0;
            this.tblPnlTop.ColumnCount = 2;
            this.tblPnlTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.98601f));
            this.tblPnlTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86.01398f));
            this.tblPnlTop.Controls.Add(this.rTxbMessage, 1, 0);
            this.tblPnlTop.Controls.Add(this.pictureBoxIcon, 0, 0);
            this.tblPnlTop.Dock = DockStyle.Fill;
            this.tblPnlTop.Location = new Point(3, 3);
            this.tblPnlTop.Name = "tblPnlTop";
            this.tblPnlTop.RowCount = 1;
            this.tblPnlTop.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tblPnlTop.Size = new Size(0x11e, 0x5e);
            this.tblPnlTop.TabIndex = 1;
            this.rTxbMessage.BorderStyle = BorderStyle.None;
            this.rTxbMessage.Dock = DockStyle.Fill;
            this.rTxbMessage.Location = new Point(0x2a, 3);
            this.rTxbMessage.Name = "rTxbMessage";
            this.rTxbMessage.ReadOnly = true;
            this.rTxbMessage.Size = new Size(0xf1, 0x58);
            this.rTxbMessage.TabIndex = 5;
            this.rTxbMessage.Text = "";
            this.flwPnlDown.Anchor = AnchorStyles.None;
            this.flwPnlDown.AutoSize = true;
            this.flwPnlDown.Location = new Point(0x92, 0x76);
            this.flwPnlDown.Name = "flwPnlDown";
            this.flwPnlDown.Size = new Size(0, 0);
            this.flwPnlDown.TabIndex = 0;
            this.btnOK.Anchor = AnchorStyles.None;
//            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0, 0);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x41, 0x17);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnCANCEL.Anchor = AnchorStyles.None;
//            this.btnCANCEL.DialogResult = DialogResult.Cancel;
            this.btnCANCEL.Location = new Point(0, 0);
            this.btnCANCEL.Name = "btnCANCEL";
            this.btnCANCEL.Size = new Size(60, 0x17);
            this.btnCANCEL.TabIndex = 0;
            this.btnCANCEL.Text = "Cancel";
            this.btnCANCEL.UseVisualStyleBackColor = true;
            this.btnYES.Anchor = AnchorStyles.None;
//            this.btnYES.DialogResult = DialogResult.Yes;
            this.btnYES.Location = new Point(0, 0);
            this.btnYES.Name = "btnYES";
            this.btnYES.Size = new Size(0x41, 0x17);
            this.btnYES.TabIndex = 0;
            this.btnYES.Text = "Yes(&Y)";
            this.btnYES.UseVisualStyleBackColor = true;
            this.btnNO.Anchor = AnchorStyles.None;
//            this.btnNO.DialogResult = DialogResult.No;
            this.btnNO.Location = new Point(0, 0);
            this.btnNO.Name = "btnNO";
            this.btnNO.Size = new Size(0x41, 0x17);
            this.btnNO.TabIndex = 0;
            this.btnNO.Text = "No(&N)";
            this.btnNO.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x124, 0x88);
            base.Controls.Add(this.tblPnlRoot);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MessageDialogSimpleForm";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.TopMost = true;
            ((ISupportInitialize) this.pictureBoxIcon).EndInit();
            this.tblPnlRoot.ResumeLayout(false);
            this.tblPnlRoot.PerformLayout();
            this.tblPnlTop.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message)
        {
            return this.ShowMessage(buttonPatern, messageKind, message, MessageBoxDefaultButton.Button1);
        }

        public DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message, MessageBoxDefaultButton defaultButton)
        {
            switch (messageKind)
            {
                case MessageKind.Information:
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Information;
                    this.pictureBoxIcon.Image = this.icon.ToBitmap();
                    break;

                case MessageKind.Error:
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Error;
                    this.pictureBoxIcon.Image = this.icon.ToBitmap();
                    break;

                case MessageKind.Warning:
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Warning;
                    this.pictureBoxIcon.Image = this.icon.ToBitmap();
                    break;

                case MessageKind.Confirmation:
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Question;
                    this.pictureBoxIcon.Image = this.icon.ToBitmap();
                    break;

                default:
                    return MessageBox.Show(string.Format(Resources.ResourceManager.GetString("CORE13"), messageKind));
            }
            switch (buttonPatern)
            {
                case ButtonPatern.OK_ONLY:
                    this.btnOK.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnOK);
                    this.btnCANCEL.Size = new Size(0, 0);
                    this.btnCANCEL.TabStop = false;
                    this.flwPnlDown.Controls.Add(this.btnCANCEL);
                    base.CancelButton = this.btnCANCEL;
                    break;

                case ButtonPatern.OK_CANCEL:
                    this.btnOK.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnOK);
                    this.btnCANCEL.TabIndex = 2;
                    this.flwPnlDown.Controls.Add(this.btnCANCEL);
                    base.CancelButton = this.btnCANCEL;
                    break;

                case ButtonPatern.YES_NO:
                    this.btnOK.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnYES);
                    this.btnNO.TabIndex = 2;
                    this.flwPnlDown.Controls.Add(this.btnNO);
                    base.CancelButton = this.btnNO;
                    break;

                case ButtonPatern.YES_NO_CANCEL:
                    this.btnOK.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnYES);
                    this.btnNO.TabIndex = 2;
                    this.flwPnlDown.Controls.Add(this.btnNO);
                    this.btnCANCEL.TabIndex = 3;
                    this.flwPnlDown.Controls.Add(this.btnCANCEL);
                    base.CancelButton = this.btnCANCEL;
                    break;

                default:
                    return MessageBox.Show(string.Format(Resources.ResourceManager.GetString("CORE14"), buttonPatern));
            }
            int num = 0;
            MessageBoxDefaultButton button = defaultButton;
            if (button == MessageBoxDefaultButton.Button2)
            {
                num = 1;
            }
            else if (button == MessageBoxDefaultButton.Button3)
            {
                num = 2;
            }
            else
            {
                num = 0;
            }
            if (num < this.flwPnlDown.Controls.Count)
            {
                this.flwPnlDown.Controls[num].Select();
            }
            this.rTxbMessage.Text = message;
            if (this.AutoSize)
            {
                Size size = new Size(base.Size.Width, base.Size.Height);
                if (this.rTxbMessage.Size.Width < (this.rTxbMessage.PreferredSize.Width + this.rTxbMessage.Margin.Horizontal))
                {
                    size.Width += (this.rTxbMessage.PreferredSize.Width + this.rTxbMessage.Margin.Horizontal) - this.rTxbMessage.Size.Width;
                }
                if (this.rTxbMessage.Lines.Length > 0)
                {
                    int num2 = this.rTxbMessage.GetPositionFromCharIndex(this.rTxbMessage.Lines[0].Length + 1).Y - this.rTxbMessage.GetPositionFromCharIndex(0).Y;
                    int num3 = (this.rTxbMessage.Lines.Length + 1) * num2;
                    if (this.rTxbMessage.Size.Height < (num3 + this.rTxbMessage.Margin.Vertical))
                    {
                        size.Height += (num3 + this.rTxbMessage.Margin.Vertical) - this.rTxbMessage.Size.Height;
                    }
                }
                this.MinimumSize = size;
            }
            return base.ShowDialog();
        }
    }
}

