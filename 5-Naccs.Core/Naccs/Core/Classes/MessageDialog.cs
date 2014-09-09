namespace Naccs.Core.Classes
{
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;
    using System.Xml;

    public class MessageDialog : Form, IMessageDialog
    {
        private Button btnCANCEL;
        private Button btnCOPY;
        private Button btnNO;
        private Button btnOK;
        private Button btnYES;
        private IContainer components;
        private FlowLayoutPanel flwPnlDown;
        private HelpSettings helpSet;
        private Icon icon;
        private PictureBox pictureBoxIcon;
        private RichTextBox rTxbDescription;
        private RichTextBox rTxbDisposition;
        private RichTextBox rTxbInternalCode;
        private RichTextBox rTxbMessage;
        private TabControl tabCtl;
        private TabPage tabPDescription;
        private TabPage tabPDisposition;
        private TabPage tabPInternalCode;
        private TabPage tabPMessage;
        private TableLayoutPanel tblPnlMessageInner;
        private TableLayoutPanel tblPnlMessageRoot;
        private TableLayoutPanel tblPnlRoot;
        private TermMessage trmMsg;
        private TermMessageSetClass trmMsgSet;
        private TextBox txbMessageCode;

        public MessageDialog()
        {
            this.InitializeComponent();
            this.rTxbMessage.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            this.rTxbInternalCode.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            this.rTxbDescription.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
            this.rTxbDisposition.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
        }

        private void btn_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void btnCOPY_Click(object sender, EventArgs e)
        {
            Clipboard.SetDataObject(("#Code\n\t" + this.txbMessageCode.Text + "\n\n#Message\n\t" + this.rTxbMessage.Text + "\n\n#Contents\n\t" + this.rTxbDescription.Text + "\n\n#Solution\n\t" + this.rTxbDisposition.Text + "\n\n#Internal Code\n\t" + this.rTxbInternalCode.Text + "\n").Replace("\n", "\r\n"), true);
            string message = Resources.ResourceManager.GetString("CORE12");
            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
            {
                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Information, message);
            }
        }

        private void buttonSet(ButtonPatern buttonPatern, MessageKind messageKind)
        {
            switch (messageKind)
            {
                case MessageKind.Information:
                {
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Information;
                    using (MemoryStream stream = new MemoryStream())
                    {
                        this.icon.ToBitmap().Save(stream, ImageFormat.Png);
                        this.pictureBoxIcon.Image = Image.FromStream(stream);
                        break;
                    }
                }
                case MessageKind.Error:
                {
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Error;
                    using (MemoryStream stream4 = new MemoryStream())
                    {
                        this.icon.ToBitmap().Save(stream4, ImageFormat.Png);
                        this.pictureBoxIcon.Image = Image.FromStream(stream4);
                        break;
                    }
                }
                case MessageKind.Warning:
                {
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Warning;
                    using (MemoryStream stream3 = new MemoryStream())
                    {
                        this.icon.ToBitmap().Save(stream3, ImageFormat.Png);
                        this.pictureBoxIcon.Image = Image.FromStream(stream3);
                        break;
                    }
                }
                case MessageKind.Confirmation:
                {
                    this.Text = "Private Terminal Software";
                    this.icon = SystemIcons.Question;
                    using (MemoryStream stream2 = new MemoryStream())
                    {
                        this.icon.ToBitmap().Save(stream2, ImageFormat.Png);
                        this.pictureBoxIcon.Image = Image.FromStream(stream2);
                        break;
                    }
                }
                default:
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, string.Format(Resources.ResourceManager.GetString("CORE09"), messageKind));
                    }
                    break;
            }
            switch (buttonPatern)
            {
                case ButtonPatern.OK_ONLY:
                    this.btnOK.Click += new EventHandler(this.btn_Click);
                    this.btnOK.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnOK);
                    this.btnCOPY.TabIndex = 2;
                    this.flwPnlDown.Controls.Add(this.btnCOPY);
                    this.btnCANCEL.Click += new EventHandler(this.btn_Click);
                    this.btnCANCEL.Size = new Size(0, 0);
                    this.btnCANCEL.TabStop = false;
                    this.flwPnlDown.Controls.Add(this.btnCANCEL);
                    base.CancelButton = this.btnCANCEL;
                    break;

                case ButtonPatern.OK_CANCEL:
                    this.btnOK.Click += new EventHandler(this.btn_Click);
                    this.btnOK.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnOK);
                    this.btnCANCEL.Click += new EventHandler(this.btn_Click);
                    this.btnCANCEL.TabIndex = 2;
                    this.flwPnlDown.Controls.Add(this.btnCANCEL);
                    this.btnCOPY.TabIndex = 3;
                    this.flwPnlDown.Controls.Add(this.btnCOPY);
                    this.btnOK.TabIndex = 1;
                    base.CancelButton = this.btnCANCEL;
                    break;

                case ButtonPatern.YES_NO:
                    this.btnYES.Click += new EventHandler(this.btn_Click);
                    this.btnYES.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnYES);
                    this.btnNO.Click += new EventHandler(this.btn_Click);
                    this.btnNO.TabIndex = 2;
                    this.flwPnlDown.Controls.Add(this.btnNO);
                    this.btnCOPY.TabIndex = 3;
                    this.flwPnlDown.Controls.Add(this.btnCOPY);
                    base.CancelButton = this.btnNO;
                    break;

                case ButtonPatern.YES_NO_CANCEL:
                    this.btnYES.Click += new EventHandler(this.btn_Click);
                    this.btnYES.TabIndex = 1;
                    this.flwPnlDown.Controls.Add(this.btnYES);
                    this.btnNO.Click += new EventHandler(this.btn_Click);
                    this.btnNO.TabIndex = 2;
                    this.flwPnlDown.Controls.Add(this.btnNO);
                    this.btnCANCEL.Click += new EventHandler(this.btn_Click);
                    this.btnCANCEL.TabIndex = 3;
                    this.flwPnlDown.Controls.Add(this.btnCANCEL);
                    this.btnCOPY.TabIndex = 4;
                    this.flwPnlDown.Controls.Add(this.btnCOPY);
                    base.CancelButton = this.btnCANCEL;
                    break;

                default:
                    using (MessageDialogSimpleForm form2 = new MessageDialogSimpleForm())
                    {
                        form2.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, string.Format(Resources.ResourceManager.GetString("CORE09"), buttonPatern));
                    }
                    break;
            }
            this.btnCOPY.Click += new EventHandler(this.btnCOPY_Click);
        }

        public static string CreateExceptionMessage(Exception e)
        {
            return (e.Message + Environment.NewLine + e.GetType().Name + Environment.NewLine + e.StackTrace);
        }

        public void CreateMessage(string messageCode, string applyStr, string internalCode)
        {
            ULogClass.LogWrite("Message Code=[" + messageCode + "] Apply Character String=[" + applyStr + "]\r\n" + internalCode);
            this.trmMsg = TermMessage.CreateInstance();
            this.trmMsgSet = this.trmMsg.TermMessageSetList.GetTermMessageSet(messageCode);
            if (this.trmMsgSet == null)
            {
                this.txbMessageCode.Text = "E---";
                this.rTxbMessage.Text = string.Format(Resources.ResourceManager.GetString("CORE102"), messageCode);
                this.rTxbDescription.Text = Resources.ResourceManager.GetString("CORE103");
                this.rTxbDisposition.Text = Resources.ResourceManager.GetString("CORE104");
                this.buttonSet(ButtonPatern.OK_ONLY, MessageKind.Error);
            }
            else
            {
                this.txbMessageCode.Text = messageCode;
                string str = this.trmMsgSet.Message.Replace(@"\n", "\n");
                if (applyStr == null)
                {
                    this.rTxbMessage.Text = str;
                }
                else
                {
                    this.rTxbMessage.Text = str.Replace("#s", applyStr);
                }
                this.rTxbDescription.Text = this.trmMsgSet.Description.Replace(@"\n", "\n");
                this.rTxbDisposition.Text = this.trmMsgSet.Disposition.Replace(@"\n", "\n");
                this.buttonSet(this.getButtonPatern(this.trmMsgSet.Button), this.getMessageKind(messageCode));
            }
            if ((internalCode != null) && !internalCode.Equals(""))
            {
                this.rTxbInternalCode.Text = internalCode;
            }
            else
            {
                this.rTxbInternalCode.Text = Resources.ResourceManager.GetString("INTERNAL_CODE_NULL");
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

        private ButtonPatern getButtonPatern(string btnPtnStr)
        {
            ButtonPatern patern = ButtonPatern.OK_ONLY;
            switch (btnPtnStr)
            {
                case "OK_ONLY":
                    return ButtonPatern.OK_ONLY;

                case "OK_CANCEL":
                    return ButtonPatern.OK_CANCEL;

                case "YES_NO":
                    return ButtonPatern.YES_NO;

                case "YES_NO_CANCEL":
                    return ButtonPatern.YES_NO_CANCEL;
            }
            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
            {
                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, string.Format(Resources.ResourceManager.GetString("CORE09"), btnPtnStr));
            }
            return patern;
        }

        private MessageKind getJobMessageKind(string messageCode)
        {
            string str2;
            if (((str2 = messageCode.Substring(0, 1)) != null) && (str2 == "W"))
            {
                return MessageKind.Warning;
            }
            return MessageKind.Error;
        }

        private MessageKind getMessageKind(string messageCode)
        {
            MessageKind information = MessageKind.Information;
            switch (messageCode.Substring(0, 1))
            {
                case "C":
                    return MessageKind.Confirmation;

                case "E":
                    return MessageKind.Error;

                case "I":
                    return MessageKind.Information;

                case "W":
                    return MessageKind.Warning;
            }
            using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
            {
                form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, string.Format(Resources.ResourceManager.GetString("CORE09"), messageCode));
            }
            return information;
        }

        private void InitializeComponent()
        {
            this.txbMessageCode = new TextBox();
            this.tblPnlRoot = new TableLayoutPanel();
            this.flwPnlDown = new FlowLayoutPanel();
            this.tabCtl = new TabControl();
            this.tabPMessage = new TabPage();
            this.tblPnlMessageRoot = new TableLayoutPanel();
            this.pictureBoxIcon = new PictureBox();
            this.tblPnlMessageInner = new TableLayoutPanel();
            this.rTxbMessage = new RichTextBox();
            this.tabPDescription = new TabPage();
            this.rTxbDescription = new RichTextBox();
            this.tabPDisposition = new TabPage();
            this.rTxbDisposition = new RichTextBox();
            this.tabPInternalCode = new TabPage();
            this.rTxbInternalCode = new RichTextBox();
            this.btnOK = new Button();
            this.btnCANCEL = new Button();
            this.btnYES = new Button();
            this.btnNO = new Button();
            this.btnCOPY = new Button();
            this.tblPnlRoot.SuspendLayout();
            this.tabCtl.SuspendLayout();
            this.tabPMessage.SuspendLayout();
            this.tblPnlMessageRoot.SuspendLayout();
            ((ISupportInitialize) this.pictureBoxIcon).BeginInit();
            this.tblPnlMessageInner.SuspendLayout();
            this.tabPDescription.SuspendLayout();
            this.tabPDisposition.SuspendLayout();
            this.tabPInternalCode.SuspendLayout();
            base.SuspendLayout();
//            this.txbMessageCode.Anchor = AnchorStyles.Right | AnchorStyles.Left;
            this.txbMessageCode.Location = new Point(3, 7);
            this.txbMessageCode.Name = "txbMessageCode";
            this.txbMessageCode.ReadOnly = true;
            this.txbMessageCode.Size = new Size(0x179, 0x13);
            this.txbMessageCode.TabIndex = 2;
            this.txbMessageCode.TabStop = false;
            this.tblPnlRoot.ColumnCount = 1;
            this.tblPnlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tblPnlRoot.Controls.Add(this.flwPnlDown, 0, 1);
            this.tblPnlRoot.Controls.Add(this.tabCtl, 0, 0);
            this.tblPnlRoot.Dock = DockStyle.Fill;
            this.tblPnlRoot.Location = new Point(0, 0);
            this.tblPnlRoot.Name = "tblPnlRoot";
            this.tblPnlRoot.RowCount = 2;
            this.tblPnlRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tblPnlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 35f));
            this.tblPnlRoot.Size = new Size(0x1ba, 0x13c);
            this.tblPnlRoot.TabIndex = 0;
            this.flwPnlDown.Anchor = AnchorStyles.None;
            this.flwPnlDown.AutoSize = true;
            this.flwPnlDown.Location = new Point(0xdd, 0x12a);
            this.flwPnlDown.Name = "flwPnlDown";
            this.flwPnlDown.Size = new Size(0, 0);
            this.flwPnlDown.TabIndex = 0;
            this.tabCtl.Controls.Add(this.tabPMessage);
            this.tabCtl.Controls.Add(this.tabPDescription);
            this.tabCtl.Controls.Add(this.tabPDisposition);
            this.tabCtl.Controls.Add(this.tabPInternalCode);
            this.tabCtl.Dock = DockStyle.Fill;
            this.tabCtl.Location = new Point(3, 3);
            this.tabCtl.Name = "tabCtl";
            this.tabCtl.SelectedIndex = 0;
            this.tabCtl.Size = new Size(0x1b4, 0x113);
            this.tabCtl.TabIndex = 1;
            this.tabPMessage.BackColor = Color.Transparent;
            this.tabPMessage.Controls.Add(this.tblPnlMessageRoot);
            this.tabPMessage.Location = new Point(4, 0x16);
            this.tabPMessage.Name = "tabPMessage";
            this.tabPMessage.Size = new Size(0x1ac, 0xf9);
            this.tabPMessage.TabIndex = 0;
            this.tabPMessage.Text = "Th\x00f4ng điệp";
            this.tabPMessage.UseVisualStyleBackColor = true;
            this.tblPnlMessageRoot.BackColor = SystemColors.Control;
            this.tblPnlMessageRoot.ColumnCount = 2;
            this.tblPnlMessageRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 39f));
            this.tblPnlMessageRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tblPnlMessageRoot.Controls.Add(this.pictureBoxIcon, 0, 0);
            this.tblPnlMessageRoot.Controls.Add(this.tblPnlMessageInner, 1, 0);
            this.tblPnlMessageRoot.Dock = DockStyle.Fill;
            this.tblPnlMessageRoot.Location = new Point(0, 0);
            this.tblPnlMessageRoot.Name = "tblPnlMessageRoot";
            this.tblPnlMessageRoot.RowCount = 1;
            this.tblPnlMessageRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tblPnlMessageRoot.Size = new Size(0x1ac, 0xf9);
            this.tblPnlMessageRoot.TabIndex = 0;
            this.pictureBoxIcon.BackColor = SystemColors.Control;
            this.pictureBoxIcon.Location = new Point(3, 3);
            this.pictureBoxIcon.Name = "pictureBoxIcon";
            this.pictureBoxIcon.Size = new Size(0x20, 0x20);
            this.pictureBoxIcon.SizeMode = PictureBoxSizeMode.Zoom;
            this.pictureBoxIcon.TabIndex = 2;
            this.pictureBoxIcon.TabStop = false;
            this.tblPnlMessageInner.BackColor = SystemColors.Control;
            this.tblPnlMessageInner.ColumnCount = 1;
            this.tblPnlMessageInner.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100f));
            this.tblPnlMessageInner.Controls.Add(this.rTxbMessage, 0, 1);
            this.tblPnlMessageInner.Controls.Add(this.txbMessageCode, 0, 0);
            this.tblPnlMessageInner.Dock = DockStyle.Fill;
            this.tblPnlMessageInner.Location = new Point(0x2a, 3);
            this.tblPnlMessageInner.Name = "tblPnlMessageInner";
            this.tblPnlMessageInner.RowCount = 2;
            this.tblPnlMessageInner.RowStyles.Add(new RowStyle(SizeType.Absolute, 33f));
            this.tblPnlMessageInner.RowStyles.Add(new RowStyle(SizeType.Percent, 100f));
            this.tblPnlMessageInner.Size = new Size(0x17f, 0xf3);
            this.tblPnlMessageInner.TabIndex = 3;
            this.rTxbMessage.Dock = DockStyle.Fill;
            this.rTxbMessage.Location = new Point(3, 0x24);
            this.rTxbMessage.Name = "rTxbMessage";
            this.rTxbMessage.ReadOnly = true;
            this.rTxbMessage.Size = new Size(0x179, 0xcc);
            this.rTxbMessage.TabIndex = 5;
            this.rTxbMessage.Text = "";
            this.tabPDescription.BackColor = Color.Transparent;
            this.tabPDescription.Controls.Add(this.rTxbDescription);
            this.tabPDescription.Location = new Point(4, 0x16);
            this.tabPDescription.Name = "tabPDescription";
            this.tabPDescription.Size = new Size(0x1ac, 0xf9);
            this.tabPDescription.TabIndex = 1;
            this.tabPDescription.Text = "Nội dung";
            this.tabPDescription.UseVisualStyleBackColor = true;
            this.rTxbDescription.BorderStyle = BorderStyle.None;
            this.rTxbDescription.Dock = DockStyle.Fill;
            this.rTxbDescription.Location = new Point(0, 0);
            this.rTxbDescription.Name = "rTxbDescription";
            this.rTxbDescription.ReadOnly = true;
            this.rTxbDescription.Size = new Size(0x1ac, 0xf9);
            this.rTxbDescription.TabIndex = 5;
            this.rTxbDescription.Text = "";
            this.tabPDisposition.BackColor = Color.Transparent;
            this.tabPDisposition.Controls.Add(this.rTxbDisposition);
            this.tabPDisposition.Location = new Point(4, 0x16);
            this.tabPDisposition.Name = "tabPDisposition";
            this.tabPDisposition.Size = new Size(0x1ac, 0xf9);
            this.tabPDisposition.TabIndex = 2;
            this.tabPDisposition.Text = "Giải ph\x00e1p";
            this.tabPDisposition.UseVisualStyleBackColor = true;
            this.rTxbDisposition.BorderStyle = BorderStyle.None;
            this.rTxbDisposition.Dock = DockStyle.Fill;
            this.rTxbDisposition.Location = new Point(0, 0);
            this.rTxbDisposition.Name = "rTxbDisposition";
            this.rTxbDisposition.ReadOnly = true;
            this.rTxbDisposition.Size = new Size(0x1ac, 0xf9);
            this.rTxbDisposition.TabIndex = 5;
            this.rTxbDisposition.Text = "";
            this.tabPInternalCode.BackColor = Color.Transparent;
            this.tabPInternalCode.Controls.Add(this.rTxbInternalCode);
            this.tabPInternalCode.Location = new Point(4, 0x16);
            this.tabPInternalCode.Name = "tabPInternalCode";
            this.tabPInternalCode.Size = new Size(0x1ac, 0xf9);
            this.tabPInternalCode.TabIndex = 3;
            this.tabPInternalCode.Text = "Cho người quản trị";
            this.tabPInternalCode.UseVisualStyleBackColor = true;
            this.rTxbInternalCode.BorderStyle = BorderStyle.None;
            this.rTxbInternalCode.Dock = DockStyle.Fill;
            this.rTxbInternalCode.Location = new Point(0, 0);
            this.rTxbInternalCode.Name = "rTxbInternalCode";
            this.rTxbInternalCode.ReadOnly = true;
            this.rTxbInternalCode.Size = new Size(0x1ac, 0xf9);
            this.rTxbInternalCode.TabIndex = 5;
            this.rTxbInternalCode.Text = "";
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
            this.btnCOPY.Anchor = AnchorStyles.None;
            this.btnCOPY.Location = new Point(0, 0);
            this.btnCOPY.Name = "btnCOPY";
            this.btnCOPY.Size = new Size(0x66, 0x17);
            this.btnCOPY.TabIndex = 0;
            this.btnCOPY.Text = "Copy message";
            this.btnCOPY.UseVisualStyleBackColor = true;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.ClientSize = new Size(0x1ba, 0x13c);
            base.Controls.Add(this.tblPnlRoot);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "MessageDialog";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            base.TopMost = true;
            this.tblPnlRoot.ResumeLayout(false);
            this.tblPnlRoot.PerformLayout();
            this.tabCtl.ResumeLayout(false);
            this.tabPMessage.ResumeLayout(false);
            this.tblPnlMessageRoot.ResumeLayout(false);
            ((ISupportInitialize) this.pictureBoxIcon).EndInit();
            this.tblPnlMessageInner.ResumeLayout(false);
            this.tblPnlMessageInner.PerformLayout();
            this.tabPDescription.ResumeLayout(false);
            this.tabPDisposition.ResumeLayout(false);
            this.tabPInternalCode.ResumeLayout(false);
            base.ResumeLayout(false);
        }

        public DialogResult ShowJobMessage(string jobCode, string resultCode, string internalCode)
        {
            string messageCode = resultCode.Substring(0, 5);
            if (messageCode.IndexOf('A') == 0)
            {
                jobCode = "SYS";
            }
            ULogClass.LogWrite("Result Code=[" + resultCode + "] Job Code=[" + jobCode + "]\r\n" + internalCode);
            this.helpSet = HelpSettings.CreateInstance();
            string filename = this.helpSet.HelpPath.GymErrPath + jobCode + "_err.xml";
            try
            {
                XmlDocument document = new XmlDocument();
                document.Load(filename);
                XmlNode node = document.SelectSingleNode("//response[@code='" + messageCode + "']");
                this.txbMessageCode.Text = messageCode;
                this.rTxbMessage.Text = node.SelectSingleNode("description").InnerText.Replace(@"\n", "\n");
                this.rTxbDescription.Text = "Process results code；" + resultCode + "\nItem name；" + node.Attributes["name"].Value + "\n\n" + this.rTxbMessage.Text + "\n\n";
                this.rTxbDisposition.Text = node.SelectSingleNode("disposition").InnerText.Replace(@"\n", "\n");
            }
            catch (Exception)
            {
                MessageDialog dialog = new MessageDialog();
                return dialog.ShowMessage("E302", filename, "Service code=" + jobCode + "\nProcess results code=" + resultCode);
            }
            if ((internalCode != null) && !internalCode.Equals(""))
            {
                this.rTxbInternalCode.Text = internalCode;
            }
            else
            {
                this.rTxbInternalCode.Text = Resources.ResourceManager.GetString("INTERNAL_CODE_NULL");
            }
            this.buttonSet(ButtonPatern.OK_ONLY, this.getJobMessageKind(messageCode));
            return base.ShowDialog();
        }

        public DialogResult ShowMessage(string messageCode, string internalCode)
        {
            string applyStr = null;
            return this.ShowMessage(messageCode, applyStr, internalCode);
        }

        public DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message)
        {
            MessageDialogSimpleForm form = new MessageDialogSimpleForm();
            return form.ShowMessage(buttonPatern, messageKind, message);
        }

        public DialogResult ShowMessage(string messageCode, string applyStr, string internalCode)
        {
            this.CreateMessage(messageCode, applyStr, internalCode);
            return base.ShowDialog();
        }

        public DialogResult ShowMessage(string messageCode, string applyStr, string internalCode, Exception ex)
        {
            StringBuilder builder = new StringBuilder();
            if (!string.IsNullOrEmpty(internalCode))
            {
                builder.AppendLine(internalCode);
            }
            builder.Append(CreateExceptionMessage(ex));
            this.CreateMessage(messageCode, applyStr, internalCode);
            return base.ShowDialog();
        }

        public DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message, bool logout, bool autosize)
        {
            return this.ShowMessage(buttonPatern, messageKind, message, logout, autosize, MessageBoxDefaultButton.Button1);
        }

        public DialogResult ShowMessage(ButtonPatern buttonPatern, MessageKind messageKind, string message, bool logout, bool autosize, MessageBoxDefaultButton defaultButton)
        {
            if (logout)
            {
                ULogClass.LogWrite(string.Format("{0} {1}", messageKind.GetType().ToString(), message));
            }
            MessageDialogSimpleForm form = new MessageDialogSimpleForm {
                AutoSize = autosize
            };
            return form.ShowMessage(buttonPatern, messageKind, message, defaultButton);
        }

        public void ShowModelessMessageDialog(string messageCode, string applyStr, string internalCode)
        {
            this.CreateMessage(messageCode, applyStr, internalCode);
            if (this.trmMsgSet != null)
            {
                this.buttonSet(this.getButtonPatern(this.trmMsgSet.Button), this.getJobMessageKind(messageCode));
            }
            else
            {
                this.buttonSet(ButtonPatern.OK_ONLY, this.getJobMessageKind(messageCode));
            }
            base.Show();
        }
    }
}

