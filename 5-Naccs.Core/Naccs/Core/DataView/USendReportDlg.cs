namespace Naccs.Core.DataView
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class USendReportDlg : Form
    {
        private ColumnHeader clmDataKey;
        private ColumnHeader clmDetail;
        private ColumnHeader clmFileName;
        private ColumnHeader clmFolderName;
        private ColumnHeader clmStatus;
        private string CnWriteFormat = "\"{0}\",\"{1}\",\"{2}\",\"{3}\"";
        private IContainer components;
        private SaveFileDialog dlgSave;
        private ImageList imgReport;
        private ListView lstReport;
        private ToolStripMenuItem mnClose;
        private ToolStripMenuItem mnDelete;
        private ToolStripMenuItem mnFile;
        private ToolStripSeparator mnFileSeparator1;
        private MenuStrip mnReport;
        private ToolStripMenuItem mnSave;

        public USendReportDlg()
        {
            this.InitializeComponent();
            this.Clear();
        }

        public void Add(string path)
        {
            this.lstReport.Items.Add(Path.GetFileName(path));
            int num = this.Count - 1;
            this.lstReport.Items[num].SubItems.Add(Path.GetDirectoryName(path));
            this.lstReport.Items[num].SubItems.Add("");
            this.lstReport.Items[num].SubItems.Add("");
            this.lstReport.Items[num].SubItems.Add("");
        }

        public void Add(string path, ReportStatus status, string datakey, string detail)
        {
            this.Add(path);
            int index = this.Count - 1;
            this.SetStatus(index, status, datakey, detail);
        }

        public void Clear()
        {
            this.lstReport.Items.Clear();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string FilePath(int index)
        {
            string str = null;
            if ((index >= 0) && ((this.Count > 0) && (index < this.Count)))
            {
                str = Path.Combine(this.lstReport.Items[index].SubItems[this.clmFolderName.Index].Text, this.lstReport.Items[index].SubItems[this.clmFileName.Index].Text);
            }
            return str;
        }

        private int Find(string datakey)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.lstReport.Items[i].SubItems[this.clmDataKey.Index].Text == datakey)
                {
                    return i;
                }
            }
            return -1;
        }

        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem("");
            this.lstReport = new System.Windows.Forms.ListView();
            this.clmFileName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmFolderName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmStatus = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDetail = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmDataKey = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.imgReport = new System.Windows.Forms.ImageList(this.components);
            this.mnReport = new System.Windows.Forms.MenuStrip();
            this.mnFile = new System.Windows.Forms.ToolStripMenuItem();
            this.mnSave = new System.Windows.Forms.ToolStripMenuItem();
            this.mnDelete = new System.Windows.Forms.ToolStripMenuItem();
            this.mnFileSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.mnClose = new System.Windows.Forms.ToolStripMenuItem();
            this.dlgSave = new System.Windows.Forms.SaveFileDialog();
            this.mnReport.SuspendLayout();
            this.SuspendLayout();
            // 
            // lstReport
            // 
            this.lstReport.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmFileName,
            this.clmFolderName,
            this.clmStatus,
            this.clmDetail,
            this.clmDataKey});
            this.lstReport.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lstReport.FullRowSelect = true;
            this.lstReport.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1});
            this.lstReport.Location = new System.Drawing.Point(0, 24);
            this.lstReport.Name = "lstReport";
            this.lstReport.Size = new System.Drawing.Size(684, 234);
            this.lstReport.TabIndex = 0;
            this.lstReport.UseCompatibleStateImageBehavior = false;
            this.lstReport.View = System.Windows.Forms.View.Details;
            // 
            // clmFileName
            // 
            this.clmFileName.Text = "Tên tệp";
            this.clmFileName.Width = 120;
            // 
            // clmFolderName
            // 
            this.clmFolderName.Text = "Tên thư mục";
            this.clmFolderName.Width = 190;
            // 
            // clmStatus
            // 
            this.clmStatus.Text = "Tình trạng";
            this.clmStatus.Width = 70;
            // 
            // clmDetail
            // 
            this.clmDetail.Text = "Thông tin chi tiết";
            this.clmDetail.Width = 300;
            // 
            // clmDataKey
            // 
            this.clmDataKey.Text = "登録キー";
            this.clmDataKey.Width = 0;
            // 
            // imgReport
            // 
            this.imgReport.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imgReport.ImageSize = new System.Drawing.Size(16, 16);
            this.imgReport.TransparentColor = System.Drawing.Color.Magenta;
            // 
            // mnReport
            // 
            this.mnReport.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnFile});
            this.mnReport.Location = new System.Drawing.Point(0, 0);
            this.mnReport.Name = "mnReport";
            this.mnReport.Size = new System.Drawing.Size(684, 24);
            this.mnReport.TabIndex = 1;
            this.mnReport.Text = "menuStrip1";
            // 
            // mnFile
            // 
            this.mnFile.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.mnSave,
            this.mnDelete,
            this.mnFileSeparator1,
            this.mnClose});
            this.mnFile.Name = "mnFile";
            this.mnFile.Size = new System.Drawing.Size(73, 20);
            this.mnFile.Text = "Tệp tin (&F)";
            // 
            // mnSave
            // 
            this.mnSave.Name = "mnSave";
            this.mnSave.Size = new System.Drawing.Size(186, 22);
            this.mnSave.Text = "Lưu kết quả (&S)";
            this.mnSave.Click += new System.EventHandler(this.mnSave_Click);
            // 
            // mnDelete
            // 
            this.mnDelete.Name = "mnDelete";
            this.mnDelete.Size = new System.Drawing.Size(186, 22);
            this.mnDelete.Text = "Xóa tệp tin đã gửi (&D)";
            this.mnDelete.Click += new System.EventHandler(this.mnDelete_Click);
            // 
            // mnFileSeparator1
            // 
            this.mnFileSeparator1.Name = "mnFileSeparator1";
            this.mnFileSeparator1.Size = new System.Drawing.Size(183, 6);
            // 
            // mnClose
            // 
            this.mnClose.Name = "mnClose";
            this.mnClose.Size = new System.Drawing.Size(186, 22);
            this.mnClose.Text = "kết thúc (&X)";
            this.mnClose.Click += new System.EventHandler(this.mnClose_Click);
            // 
            // dlgSave
            // 
            this.dlgSave.DefaultExt = "csv";
            this.dlgSave.Filter = "csv|*.csv|すべてのファイル|*.*";
            this.dlgSave.RestoreDirectory = true;
            // 
            // USendReportDlg
            // 
            this.ClientSize = new System.Drawing.Size(684, 258);
            this.Controls.Add(this.lstReport);
            this.Controls.Add(this.mnReport);
            this.MainMenuStrip = this.mnReport;
            this.Name = "USendReportDlg";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Báo cáo kết quả xử lý";
            this.mnReport.ResumeLayout(false);
            this.mnReport.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        private void mnClose_Click(object sender, EventArgs e)
        {
            base.Close();
        }

        private void mnDelete_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.Count; i++)
            {
                if (this.lstReport.Items[i].ImageKey == "Sent")
                {
                    string path = this.FilePath(i);
                    try
                    {
                        File.Delete(path);
                        this.UpdateStatus(i, ReportStatus.Delete);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void mnSave_Click(object sender, EventArgs e)
        {
            if (this.dlgSave.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    StreamWriter writer = new StreamWriter(this.dlgSave.FileName, false, Encoding.GetEncoding("UTF-8"));
                    try
                    {
                        writer.Flush();
                        string str = string.Format(this.CnWriteFormat, new object[] { this.clmFileName.Text, this.clmFolderName.Text, this.clmStatus.Text, this.clmDetail.Text });
                        writer.WriteLine(str);
                        for (int i = 0; i < this.Count; i++)
                        {
                            string str2 = this.lstReport.Items[i].SubItems[this.clmFileName.Index].Text.Replace("\"", "\"\"");
                            string str3 = this.lstReport.Items[i].SubItems[this.clmFolderName.Index].Text.Replace("\"", "\"\"");
                            string str4 = this.lstReport.Items[i].SubItems[this.clmStatus.Index].Text.Replace("\"", "\"\"");
                            string str5 = this.lstReport.Items[i].SubItems[this.clmDetail.Index].Text.Replace("\"", "\"\"");
                            str = string.Format(this.CnWriteFormat, new object[] { str2, str3, str4, str5 });
                            writer.WriteLine(str);
                        }
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
                catch (Exception exception)
                {
                    new MessageDialog().ShowMessage("E303", string.Format("File : {0}", this.dlgSave.FileName), string.Format("Errocode : E303\r\nFileName : {0}\r\n{1}", this.dlgSave.FileName, MessageDialog.CreateExceptionMessage(exception)));
                }
            }
        }

        public void SetStatus(string datakey, ReportStatus status, string detail)
        {
            int index = this.Find(datakey);
            this.SetStatus(index, status, datakey, detail);
        }

        public void SetStatus(int index, ReportStatus status, string datakey, string detail)
        {
            if ((index >= 0) && ((this.Count > 0) && (index < this.Count)))
            {
                if (detail != null)
                {
                    this.lstReport.Items[index].SubItems[this.clmDetail.Index].Text = detail;
                }
                if (datakey != null)
                {
                    this.lstReport.Items[index].SubItems[this.clmDataKey.Index].Text = datakey;
                }
                this.UpdateStatus(index, status);
            }
        }

        public int StatusCount(ReportStatus status)
        {
            int num = 0;
            for (int i = 0; i < this.Count; i++)
            {
                if (status == ReportStatus.None)
                {
                    if (string.IsNullOrEmpty(this.lstReport.Items[i].ImageKey))
                    {
                        num++;
                    }
                }
                else if (this.lstReport.Items[i].ImageKey == this.StatusImageKey(status))
                {
                    num++;
                }
            }
            return num;
        }

        private string StatusImageKey(ReportStatus status)
        {
            string str = null;
            if (status == ReportStatus.Wait)
            {
                return "Wait";
            }
            if (status == ReportStatus.Sent)
            {
                return "Sent";
            }
            if (status == ReportStatus.Error)
            {
                return "Error";
            }
            if (status == ReportStatus.Delete)
            {
                str = "Delete";
            }
            return str;
        }

        private string StatusString(ReportStatus status)
        {
            string str = null;
            if (status == ReportStatus.Wait)
            {
                return Resources.ResourceManager.GetString("CORE113");
            }
            if (status == ReportStatus.Sent)
            {
                return Resources.ResourceManager.GetString("CORE114");
            }
            if (status == ReportStatus.Error)
            {
                return Resources.ResourceManager.GetString("CORE115");
            }
            if (status == ReportStatus.Delete)
            {
                str = Resources.ResourceManager.GetString("CORE116");
            }
            return str;
        }

        private void UpdateStatus(int index, ReportStatus status)
        {
            this.lstReport.Items[index].ImageKey = this.StatusImageKey(status);
            this.lstReport.Items[index].SubItems[this.clmStatus.Index].Text = this.StatusString(status);
        }

        public int Count
        {
            get
            {
                return this.lstReport.Items.Count;
            }
        }

        public enum ReportStatus
        {
            None,
            Wait,
            Sent,
            Error,
            Delete
        }
    }
}

