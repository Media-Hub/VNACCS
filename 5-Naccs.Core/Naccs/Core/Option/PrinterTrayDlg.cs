namespace Naccs.Core.Option
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Windows.Forms;

    public class PrinterTrayDlg : Form
    {
        private Button btnCancel;
        private Button btnOK;
        private ComboBox cmbPrinterName;
        private ComboBox cmbTrayName;
        private IContainer components;
        private GroupBox grpPrinter;
        private Label lblPrinter;
        private Label lblTray;
        private System.Drawing.Printing.PrinterSettings ps;

        public PrinterTrayDlg()
        {
            this.InitializeComponent();
            this.ps = new System.Drawing.Printing.PrinterSettings();
            this.ReAllSet();
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            foreach (PaperSource source in this.ps.PaperSources)
            {
                if (source.SourceName == this.cmbTrayName.Text)
                {
                    this.ps.DefaultPageSettings.PaperSource = source;
                    break;
                }
            }
        }

        private void cmbPrinterName_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                this.ps.PrinterName = this.cmbPrinterName.SelectedItem.ToString();
                this.Refresh();
                this.ReSetTrayName();
                this.cmbTrayName.SelectedIndex = 0;
            }
            finally
            {
                this.Cursor = Cursors.Default;
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

        private void InitializeComponent()
        {
            this.grpPrinter = new GroupBox();
            this.cmbTrayName = new ComboBox();
            this.cmbPrinterName = new ComboBox();
            this.lblTray = new Label();
            this.lblPrinter = new Label();
            this.btnOK = new Button();
            this.btnCancel = new Button();
            this.grpPrinter.SuspendLayout();
            base.SuspendLayout();
            this.grpPrinter.Controls.Add(this.cmbTrayName);
            this.grpPrinter.Controls.Add(this.cmbPrinterName);
            this.grpPrinter.Controls.Add(this.lblTray);
            this.grpPrinter.Controls.Add(this.lblPrinter);
            this.grpPrinter.Location = new Point(14, 5);
            this.grpPrinter.Name = "grpPrinter";
            this.grpPrinter.Size = new Size(0x179, 0x7a);
            this.grpPrinter.TabIndex = 0;
            this.grpPrinter.TabStop = false;
            this.grpPrinter.Text = "Printer";
            this.cmbTrayName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbTrayName.FormattingEnabled = true;
            this.cmbTrayName.Items.AddRange(new object[] { "自動選択" });
            this.cmbTrayName.Location = new Point(100, 0x3e);
            this.cmbTrayName.Name = "cmbTrayName";
            this.cmbTrayName.Size = new Size(0x107, 20);
            this.cmbTrayName.TabIndex = 3;
            this.cmbPrinterName.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbPrinterName.FormattingEnabled = true;
            this.cmbPrinterName.Location = new Point(100, 0x17);
            this.cmbPrinterName.Name = "cmbPrinterName";
            this.cmbPrinterName.Size = new Size(0x107, 20);
            this.cmbPrinterName.TabIndex = 1;
            this.cmbPrinterName.SelectedIndexChanged += new EventHandler(this.cmbPrinterName_SelectedIndexChanged);
            this.lblTray.AutoSize = true;
            this.lblTray.Location = new Point(5, 0x41);
            this.lblTray.Name = "lblTray";
            this.lblTray.Size = new Size(0x59, 12);
            this.lblTray.TabIndex = 2;
            this.lblTray.Text = "Sheet feeder (&S)";
            this.lblPrinter.AutoSize = true;
            this.lblPrinter.Location = new Point(5, 0x1a);
            this.lblPrinter.Name = "lblPrinter";
            this.lblPrinter.Size = new Size(0x5c, 12);
            this.lblPrinter.TabIndex = 0;
            this.lblPrinter.Text = "Printer name (&N):";
            this.btnOK.DialogResult = DialogResult.OK;
            this.btnOK.Location = new Point(0xd7, 0x8b);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new Size(0x55, 0x15);
            this.btnOK.TabIndex = 1;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new EventHandler(this.btnOK_Click);
            this.btnCancel.DialogResult = DialogResult.Cancel;
            this.btnCancel.Location = new Point(0x132, 0x8b);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new Size(0x55, 0x15);
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            base.AcceptButton = this.btnOK;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btnCancel;
            base.ClientSize = new Size(0x197, 0xa6);
            base.Controls.Add(this.btnCancel);
            base.Controls.Add(this.btnOK);
            base.Controls.Add(this.grpPrinter);
            base.FormBorderStyle = FormBorderStyle.FixedSingle;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "PrinterTrayDlg";
            base.ShowIcon = false;
            base.ShowInTaskbar = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "Select printer";
            this.grpPrinter.ResumeLayout(false);
            this.grpPrinter.PerformLayout();
            base.ResumeLayout(false);
        }

        private void ReAllSet()
        {
            this.ReSetPrinterName();
            this.cmbPrinterName.SelectedItem = this.ps.PrinterName;
            this.ReSetTrayName();
            this.cmbTrayName.SelectedItem = this.ps.DefaultPageSettings.PaperSource.SourceName;
            if (this.cmbTrayName.Text.Length == 0)
            {
                this.cmbTrayName.SelectedIndex = 0;
            }
        }

        private void ReSetPrinterName()
        {
            this.cmbPrinterName.BeginUpdate();
            this.cmbPrinterName.Items.Clear();
            foreach (string str in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                this.cmbPrinterName.Items.Add(str);
            }
            this.cmbPrinterName.EndUpdate();
        }

        private void ReSetTrayName()
        {
            this.cmbTrayName.BeginUpdate();
            this.cmbTrayName.Items.Clear();
            foreach (PaperSource source in this.ps.PaperSources)
            {
                this.cmbTrayName.Items.Add(source.SourceName);
            }
            if (this.cmbTrayName.Items.Count == 0)
            {
                this.cmbTrayName.Items.Add("unknown");
            }
            this.cmbTrayName.EndUpdate();
        }

        public DialogResult ShowDialog()
        {
            DialogResult result;
            if (IntPtr.Size == 8)
            {
                return base.ShowDialog();
            }
            using (PrintDialog dialog = new PrintDialog())
            {
                dialog.PrinterSettings = this.ps;
                result = dialog.ShowDialog();
                if (result == DialogResult.OK)
                {
                    this.ps = dialog.PrinterSettings;
                }
            }
            return result;
        }

        public System.Drawing.Printing.PrinterSettings PrinterSettings
        {
            get
            {
                return this.ps;
            }
            set
            {
                if (value != null)
                {
                    this.ps.PrinterName = value.PrinterName;
                    this.ps.DefaultPageSettings.PaperSource = value.DefaultPageSettings.PaperSource;
                    this.ps.Copies = value.Copies;
                    this.ReAllSet();
                }
            }
        }
    }
}

