namespace Naccs.Core.Option
{
    using Naccs.Core.Classes;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class dlgPrinterSetting : Form
    {
        private Button btnPrtCncl;
        private Button btnPrtDef;
        private Button btnPrtOK;
        private const string C_SETTEI_NASI = "設定なし";
        private ComboBox cmbOrientation;
        private ComboBox cmbSize;
        private IContainer components;
        private GroupBox gpbList;
        private Label lblOrientation;
        private Label lblSize;
        private string mListOrientation;
        private string mListSize;
        private string mPrinterName;
        private const string PrinterMsg01 = "両方指定するか、両方設定なしにしてください。";

        public dlgPrinterSetting()
        {
            this.InitializeComponent();
        }

        private void btnPrtDef_Click(object sender, EventArgs e)
        {
            this.cmbSize.SelectedIndex = 0;
            this.cmbOrientation.SelectedIndex = 0;
            this.ListSize = null;
            this.ListOrientation = null;
        }

        private void btnPrtOK_Click(object sender, EventArgs e)
        {
            if (((this.cmbSize.SelectedIndex == 0) && (this.cmbOrientation.SelectedIndex != 0)) || ((this.cmbSize.SelectedIndex != 0) && (this.cmbOrientation.SelectedIndex == 0)))
            {
                new MessageDialog().ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, "両方指定するか、両方設定なしにしてください。");
            }
            else
            {
                if (this.cmbSize.Text == "設定なし")
                {
                    this.cmbSize.Text = null;
                }
                this.ListSize = this.cmbSize.Text;
                if (this.cmbOrientation.Text == "設定なし")
                {
                    this.cmbOrientation.Text = null;
                }
                this.ListOrientation = this.cmbOrientation.Text;
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

        private void dlgPrinterSetting_Load(object sender, EventArgs e)
        {
            PrintDocument document = new PrintDocument {
                PrinterSettings = { PrinterName = this.PrinterName }
            };
            this.cmbSize.Items.Add("設定なし");
            foreach (PaperSize size in document.PrinterSettings.PaperSizes)
            {
                this.cmbSize.Items.Add(size.PaperName);
            }
            if (string.IsNullOrEmpty(this.ListSize))
            {
                this.ListSize = "設定なし";
            }
            this.cmbSize.SelectedItem = this.ListSize;
            if (string.IsNullOrEmpty(this.ListOrientation))
            {
                this.ListOrientation = "設定なし";
            }
            this.cmbOrientation.SelectedItem = this.ListOrientation;
        }

        private void InitializeComponent()
        {
            this.gpbList = new GroupBox();
            this.cmbOrientation = new ComboBox();
            this.cmbSize = new ComboBox();
            this.lblOrientation = new Label();
            this.lblSize = new Label();
            this.btnPrtOK = new Button();
            this.btnPrtCncl = new Button();
            this.btnPrtDef = new Button();
            this.gpbList.SuspendLayout();
            base.SuspendLayout();
            this.gpbList.Controls.Add(this.cmbOrientation);
            this.gpbList.Controls.Add(this.cmbSize);
            this.gpbList.Controls.Add(this.lblOrientation);
            this.gpbList.Controls.Add(this.lblSize);
            this.gpbList.Location = new Point(12, 12);
            this.gpbList.Name = "gpbList";
            this.gpbList.Size = new Size(300, 0x60);
            this.gpbList.TabIndex = 0;
            this.gpbList.TabStop = false;
            this.gpbList.Text = "用紙";
            this.cmbOrientation.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbOrientation.FormattingEnabled = true;
            this.cmbOrientation.Items.AddRange(new object[] { "設定なし", "縦", "横" });
            this.cmbOrientation.Location = new Point(0x55, 0x39);
            this.cmbOrientation.Name = "cmbOrientation";
            this.cmbOrientation.Size = new Size(0xc7, 20);
            this.cmbOrientation.TabIndex = 3;
            this.cmbSize.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbSize.FormattingEnabled = true;
            this.cmbSize.Location = new Point(0x55, 0x17);
            this.cmbSize.Name = "cmbSize";
            this.cmbSize.Size = new Size(0xc7, 20);
            this.cmbSize.TabIndex = 2;
            this.lblOrientation.AutoSize = true;
            this.lblOrientation.Location = new Point(12, 60);
            this.lblOrientation.Name = "lblOrientation";
            this.lblOrientation.Size = new Size(0x35, 12);
            this.lblOrientation.TabIndex = 1;
            this.lblOrientation.Text = "印刷方向";
            this.lblSize.AutoSize = true;
            this.lblSize.Location = new Point(12, 0x1f);
            this.lblSize.Name = "lblSize";
            this.lblSize.Size = new Size(0x3a, 12);
            this.lblSize.TabIndex = 0;
            this.lblSize.Text = "用紙サイズ";
            this.btnPrtOK.DialogResult = DialogResult.OK;
            this.btnPrtOK.Location = new Point(0x4b, 0x72);
            this.btnPrtOK.Name = "btnPrtOK";
            this.btnPrtOK.Size = new Size(0x4b, 0x17);
            this.btnPrtOK.TabIndex = 4;
            this.btnPrtOK.Text = "OK";
            this.btnPrtOK.UseVisualStyleBackColor = true;
            this.btnPrtOK.Click += new EventHandler(this.btnPrtOK_Click);
            this.btnPrtCncl.DialogResult = DialogResult.Cancel;
            this.btnPrtCncl.Location = new Point(0x9c, 0x72);
            this.btnPrtCncl.Name = "btnPrtCncl";
            this.btnPrtCncl.Size = new Size(0x4b, 0x17);
            this.btnPrtCncl.TabIndex = 5;
            this.btnPrtCncl.Text = "キャンセル";
            this.btnPrtCncl.UseVisualStyleBackColor = true;
            this.btnPrtDef.DialogResult = DialogResult.OK;
            this.btnPrtDef.Location = new Point(0xed, 0x72);
            this.btnPrtDef.Name = "btnPrtDef";
            this.btnPrtDef.Size = new Size(0x4b, 0x17);
            this.btnPrtDef.TabIndex = 6;
            this.btnPrtDef.Text = "設定なし";
            this.btnPrtDef.UseVisualStyleBackColor = true;
            this.btnPrtDef.Click += new EventHandler(this.btnPrtDef_Click);
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnPrtCncl;
            base.ClientSize = new Size(0x144, 0x8f);
            base.Controls.Add(this.btnPrtDef);
            base.Controls.Add(this.btnPrtCncl);
            base.Controls.Add(this.btnPrtOK);
            base.Controls.Add(this.gpbList);
            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "dlgPrinterSetting";
            base.StartPosition = FormStartPosition.CenterScreen;
            this.Text = "ドットインパクトプリンタ設定";
            base.Load += new EventHandler(this.dlgPrinterSetting_Load);
            this.gpbList.ResumeLayout(false);
            this.gpbList.PerformLayout();
            base.ResumeLayout(false);
        }

        public string ListOrientation
        {
            get
            {
                return this.mListOrientation;
            }
            set
            {
                this.mListOrientation = value;
            }
        }

        public string ListSize
        {
            get
            {
                return this.mListSize;
            }
            set
            {
                this.mListSize = value;
            }
        }

        public string PrinterName
        {
            get
            {
                return this.mPrinterName;
            }
            set
            {
                this.mPrinterName = value;
            }
        }
    }
}

