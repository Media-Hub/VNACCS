namespace Naccs.Core.Option
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Threading;
    using System.Windows.Forms;

    public class GridErrDialog : Form
    {
        private string _message;
        private Button btnOK;
        private IContainer components;
        private FlowLayoutPanel flpPnlDown;
        private PictureBox picIcon;
        private TableLayoutPanel tlpPnlRoot;
        private TableLayoutPanel tlpPnlTop;
        private RichTextBox txbMessage;

        public event OnCellErrorHandler OnCellError;

        public GridErrDialog()
        {
            this.InitializeComponent();
            this.txbMessage.LanguageOption = RichTextBoxLanguageOptions.UIFonts;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void GridErrDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.OnCellError();
        }

        private void GridErrDialog_Load(object sender, EventArgs e)
        {
            this.txbMessage.Text = this.Message;
            this.picIcon.Image = SystemIcons.Error.ToBitmap();
        }

        private void InitializeComponent()
        {
            this.tlpPnlRoot = new TableLayoutPanel();
            this.tlpPnlTop = new TableLayoutPanel();
            this.txbMessage = new RichTextBox();
            this.picIcon = new PictureBox();
            this.btnOK = new Button();
            this.flpPnlDown = new FlowLayoutPanel();
            this.tlpPnlRoot.SuspendLayout();
            this.tlpPnlTop.SuspendLayout();
            ((ISupportInitialize) this.picIcon).BeginInit();
            base.SuspendLayout();
            this.tlpPnlRoot.ColumnCount = 1;
            this.tlpPnlRoot.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 50f));
            this.tlpPnlRoot.Controls.Add(this.tlpPnlTop, 0, 0);
            this.tlpPnlRoot.Controls.Add(this.flpPnlDown, 0, 1);
            this.tlpPnlRoot.Dock = DockStyle.Fill;
            this.tlpPnlRoot.Location = new Point(0, 0);
            this.tlpPnlRoot.Name = "tlpPnlRoot";
            this.tlpPnlRoot.RowCount = 2;
            this.tlpPnlRoot.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tlpPnlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 36f));
            this.tlpPnlRoot.RowStyles.Add(new RowStyle(SizeType.Absolute, 20f));
            this.tlpPnlRoot.Size = new Size(0x124, 0x88);
            this.tlpPnlRoot.TabIndex = 4;
            this.tlpPnlTop.ColumnCount = 2;
            this.tlpPnlTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 13.98601f));
            this.tlpPnlTop.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 86.01398f));
            this.tlpPnlTop.Controls.Add(this.txbMessage, 1, 0);
            this.tlpPnlTop.Controls.Add(this.picIcon, 0, 0);
            this.tlpPnlTop.Dock = DockStyle.Fill;
            this.tlpPnlTop.Location = new Point(3, 3);
            this.tlpPnlTop.Name = "tlpPnlTop";
            this.tlpPnlTop.RowCount = 1;
            this.tlpPnlTop.RowStyles.Add(new RowStyle(SizeType.Percent, 50f));
            this.tlpPnlTop.Size = new Size(0x11e, 0x5e);
            this.tlpPnlTop.TabIndex = 1;
            this.txbMessage.BorderStyle = BorderStyle.None;
            this.txbMessage.Dock = DockStyle.Fill;
            this.txbMessage.Location = new Point(0x2a, 3);
            this.txbMessage.Name = "txbMessage";
            this.txbMessage.ReadOnly = true;
            this.txbMessage.Size = new Size(0xf1, 0x58);
            this.txbMessage.TabIndex = 5;
            this.txbMessage.Text = "";
            this.picIcon.Location = new Point(3, 3);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new Size(30, 30);
            this.picIcon.SizeMode = PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 1;
            this.picIcon.TabStop = false;
            this.btnOK.DialogResult = DialogResult.Cancel;
            this.btnOK.Location = new Point(0x6b, 0x67);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x41, 0x17);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.flpPnlDown.Anchor = AnchorStyles.None;
            this.flpPnlDown.AutoSize = true;
            this.flpPnlDown.Location = new Point(0x92, 0x76);
            this.flpPnlDown.Name = "flpPnlDown";
            this.flpPnlDown.Size = new Size(0, 0);
            this.flpPnlDown.TabIndex = 0;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnOK;
            base.ClientSize = new Size(0x124, 0x88);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.tlpPnlRoot);
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "GridErrDialog";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "エラー";
            base.FormClosing += new FormClosingEventHandler(this.GridErrDialog_FormClosing);
            base.Load += new EventHandler(this.GridErrDialog_Load);
            this.tlpPnlRoot.ResumeLayout(false);
            this.tlpPnlRoot.PerformLayout();
            this.tlpPnlTop.ResumeLayout(false);
            ((ISupportInitialize) this.picIcon).EndInit();
            base.ResumeLayout(false);
        }

        public string Message
        {
            get
            {
                return this._message;
            }
            set
            {
                this._message = value;
            }
        }
    }
}

