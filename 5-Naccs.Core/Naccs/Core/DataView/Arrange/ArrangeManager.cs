namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.Classes;
    using Naccs.Core.DataView;
    using Naccs.Core.Properties;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Diagnostics;
    using System.Globalization;
    using System.Text;
    using System.Windows.Forms;

    public class ArrangeManager
    {
        private List<ToolStripMenuItem> ArrangeMenuItems = new List<ToolStripMenuItem>();
        private const bool cnResetId = true;
        private const int cnSaveDate = -99;
        private const string cnTableName = "NACCS";
        private IDataView Dataview;
        private RevertDialog DlgRevert;
        private const string logRemoveCancel = "# Deletion of old messages has been stopped.[Number of restored messages:{0} Processing time:{1}]";
        private const string logRemoveEnded = "# Deletion of old messages has been completed.[Number of deleted messages:{0} Processing time:{1}]";
        private const string logRemoveError = "# Error occurred while deleting the old messages.\r\n{0}\r\n{1}";
        private const string logRemoveStart = "# Will start deleting the old messages.[Number of messages:{0} Date/time applied for deletion:{1}]";
        private const string logRevertCancel = "# Restoration of old messages has been stopped.[Number of restored messages:{0} Processing time:{1}]";
        private const string logRevertEnded = "# Restoration of old messages has been completed.  [Number of restored messages:{0} Processing time:{1}]";
        private const string logRevertError = "# Error occurred while restoring the old messages.\r\n{0}\r\n{1}";
        private const string logRevertSkip = "# The file exists, so no need to restore the file.[Number of skipped processes:{0}]";
        private const string logRevertStart = "# Will start restoring the old messages.[Number of messages:{0}]";
        private const string logStorageEnded = "# Saving of the old messages has been completed.[Processing time:{0}]";
        private const string logStorageStart = "# Will start saving the old messages. [Date/time applied for deletion:{0}]";
        private const string mnRemoveName = "mnDataRemove";
        private const string mnRemoveText = "X\x00f3a tin nhắn cũ";
        private const string mnRevertName = "mnDataRevert";
        private const string mnRevertText = "Xem danh s\x00e1ch th\x00f4ng điệp cũ (&Z)";
        private const string mnSeparator = "mnSepArrange";
        private const string mnStorageName = "mnDataStorage";
        private const string mnStorageText = "Lưu tin nhắn cũ";
        private Form OwnerForm;
        private string RemoveDate = string.Empty;
        private Naccs.Core.DataView.Arrange.DatasetStorage Storage;
        private Stopwatch StorageWatcher;

        public ArrangeManager(Form owner, IDataView dv)
        {
            this.OwnerForm = owner;
            this.Dataview = dv;
        }

        public void AppendArrangeMenu(ToolStripMenuItem menu)
        {
            ToolStripSeparator separator = new ToolStripSeparator {
                Name = "mnSepArrange"
            };
            menu.DropDownItems.Add(separator);
            ToolStripMenuItem item = new ToolStripMenuItem {
                Name = "mnDataRevert",
                Text = "Xem danh s\x00e1ch th\x00f4ng điệp cũ (&Z)"
            };
            item.Click += new EventHandler(this.mnDatasetRevert_Click);
            menu.DropDownItems.Add(item);
            this.ArrangeMenuItems.Add(item);
        }

        public void AutoRun()
        {
            if (this.DatasetRemove(true))
            {
                this.DatasetStorage();
            }
        }

        protected bool DatasetRemove(bool bResetid)
        {
            bool flag = false;
            try
            {
                flag = this.RemoveRequest(bResetid);
            }
            catch (Exception exception)
            {
                string internalCode = string.Format("# Error occurred while deleting the old messages.\r\n{0}\r\n{1}", exception.Message, exception.StackTrace);
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E002", "", internalCode);
                dialog.Dispose();
            }
            return flag;
        }

        protected void DatasetRevert()
        {
            if ((this.DlgRevert == null) || this.DlgRevert.IsDisposed)
            {
                this.DlgRevert = new RevertDialog();
                this.DlgRevert.FormIcon = this.OwnerForm.Icon;
                this.DlgRevert.RevertEvent += new Naccs.Core.DataView.Arrange.EventHandler<RevertEventArgs>(this.OnRevertRequest);
                this.DlgRevert.Show();
            }
            else
            {
                this.DlgRevert.BringToFront();
            }
        }

        protected void DatasetStorage()
        {
            if (this.Storage == null)
            {
                this.Storage = new Naccs.Core.DataView.Arrange.DatasetStorage();
                this.Storage.EndedEvent += new EventHandler(this.OnStorageEnded);
                this.StorageWatcher = new Stopwatch();
            }
            if (!this.Storage.IsBusy)
            {
                if (string.IsNullOrEmpty(this.RemoveDate))
                {
                    this.ResetRemoveDate();
                }
                ULogClass.LogWrite(string.Format("# Will start saving the old messages. [Date/time applied for deletion:{0}]", this.RemoveDate));
                this.StorageWatcher.Start();
                this.Storage.Execute(this.RemoveDate);
            }
        }

        public void Dispose()
        {
            if ((this.DlgRevert != null) && !this.DlgRevert.Disposing)
            {
                this.DlgRevert.Dispose();
            }
            if (this.Storage != null)
            {
                this.Storage.Dispose();
                this.Storage = null;
            }
        }

        private void mnDatasetRemove_Click(object sender, EventArgs e)
        {
            this.DatasetRemove(true);
        }

        private void mnDatasetRevert_Click(object sender, EventArgs e)
        {
            this.DatasetRevert();
        }

        private void mnDatasetStorage_Click(object sender, EventArgs e)
        {
            this.DatasetStorage();
        }

        protected virtual bool OnRevertRequest(object sender, RevertEventArgs e)
        {
            bool flag = false;
            try
            {
                flag = this.RevertRequest(e.PathRoot, e.FileNames);
            }
            catch (Exception exception)
            {
                string internalCode = string.Format("# Error occurred while restoring the old messages.\r\n{0}\r\n{1}", exception.Message, exception.StackTrace);
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E002", "", internalCode);
                dialog.Dispose();
            }
            return flag;
        }

        private void OnRowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            if ((sender as DataGridView).SelectedRows.Count == 0)
            {
                e.PaintParts = DataGridViewPaintParts.None;
            }
        }

        protected virtual void OnStorageEnded(object sender, EventArgs e)
        {
            this.StorageWatcher.Stop();
            ULogClass.LogWrite(string.Format("# Saving of the old messages has been completed.[Processing time:{0}]", this.StorageWatcher.ElapsedMilliseconds));
        }

        private bool RemoveRequest(bool bResetid)
        {
            this.ResetRemoveDate();
            DataGridView internalDataGrid = this.Dataview.InternalDataGrid;
            DataSet internalDataSet = this.Dataview.InternalDataSet;
            DataTable table = internalDataSet.Tables["NACCS"];
            ULogClass.LogWrite(string.Format("# Will start deleting the old messages.[Number of messages:{0} Date/time applied for deletion:{1}]", table.Rows.Count, this.RemoveDate));
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.Dataview.DataViewNoRepaint(true);
            internalDataGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(this.OnRowPrePaint);
            internalDataGrid.ClearSelection();
            internalDataGrid.Refresh();
            internalDataGrid.Cursor = Cursors.WaitCursor;
            bool flag = false;
            try
            {
                Naccs.Core.DataView.Arrange.DatasetRemove remove = new Naccs.Core.DataView.Arrange.DatasetRemove();
                RemoveWorkerArgs args = new RemoveWorkerArgs {
                    Table = table,
                    RemoveDate = this.RemoveDate,
                    ResetID = bResetid
                };
                RemoveWorkerResult result = remove.Execute(args);
                flag = !result.IsCancel;
                if (result.IsCancel)
                {
                    ULogClass.LogWrite(string.Format("# Deletion of old messages has been stopped.[Number of restored messages:{0} Processing time:{1}]", result.RemoveCount, stopwatch.ElapsedMilliseconds));
                    return flag;
                }
                ULogClass.LogWrite(string.Format("# Deletion of old messages has been completed.[Number of deleted messages:{0} Processing time:{1}]", result.RemoveCount, stopwatch.ElapsedMilliseconds));
            }
            finally
            {
                stopwatch.Stop();
                internalDataGrid.RowPrePaint -= new DataGridViewRowPrePaintEventHandler(this.OnRowPrePaint);
                this.Dataview.DataViewNoRepaint(false);
                internalDataSet.AcceptChanges();
                internalDataGrid.Refresh();
                this.Dataview.UpdateCount();
                internalDataGrid.Cursor = Cursors.Default;
            }
            return flag;
        }

        private void ResetRemoveDate()
        {
            this.RemoveDate = DateTime.Now.AddDays(-99.0).ToString("yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo);
        }

        private bool RevertRequest(string root, string[] files)
        {
            ULogClass.LogWrite(string.Format("# Will start restoring the old messages.[Number of messages:{0}]", files.Length));
            DataGridView internalDataGrid = this.Dataview.InternalDataGrid;
            DataSet internalDataSet = this.Dataview.InternalDataSet;
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            this.Dataview.DataViewNoRepaint(true);
            internalDataGrid.RowPrePaint += new DataGridViewRowPrePaintEventHandler(this.OnRowPrePaint);
            internalDataGrid.ClearSelection();
            internalDataGrid.Refresh();
            internalDataGrid.Cursor = Cursors.WaitCursor;
            bool flag = false;
            try
            {
                Naccs.Core.DataView.Arrange.DatasetRevert revert = new Naccs.Core.DataView.Arrange.DatasetRevert();
                RevertWorkerArgs args = new RevertWorkerArgs {
                    DataView = this.Dataview,
                    PathRoot = root,
                    FileNames = files
                };
                RevertWorkerResult result = revert.Execute(args);
                flag = !result.IsCancel;
                if (result.IsCancel)
                {
                    ULogClass.LogWrite(string.Format("# Restoration of old messages has been stopped.[Number of restored messages:{0} Processing time:{1}]", result.RevertCount, stopwatch.ElapsedMilliseconds));
                }
                else
                {
                    ULogClass.LogWrite(string.Format("# Restoration of old messages has been completed.  [Number of restored messages:{0} Processing time:{1}]", result.RevertCount, stopwatch.ElapsedMilliseconds));
                }
                if (result.SkipFiles.Length > 0)
                {
                    StringBuilder builder = new StringBuilder();
                    builder.AppendLine(string.Format("# The file exists, so no need to restore the file.[Number of skipped processes:{0}]", result.SkipFiles.Length));
                    ULogClass.LogWrite(builder.ToString());
                }
                if (result.FirstError == null)
                {
                    return flag;
                }
                StringBuilder builder2 = new StringBuilder();
                builder2.AppendLine(string.Format("# Error occurred while restoring the old messages.\r\n{0}\r\n{1}", result.FirstError.Message, result.FirstError.StackTrace));
                builder2.AppendLine(Resources.ResourceManager.GetString("CORE105") + result.ErrorFiles.Length);
                foreach (string str in result.ErrorFiles)
                {
                    builder2.AppendLine(str);
                }
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E402", "", builder2.ToString());
                dialog.Dispose();
            }
            finally
            {
                stopwatch.Stop();
                internalDataGrid.RowPrePaint -= new DataGridViewRowPrePaintEventHandler(this.OnRowPrePaint);
                this.Dataview.DataViewNoRepaint(false);
                internalDataSet.AcceptChanges();
                internalDataGrid.Refresh();
                this.Dataview.UpdateCount();
                internalDataGrid.Cursor = Cursors.Default;
            }
            return flag;
        }

        public bool MenuEnabled
        {
            set
            {
                foreach (ToolStripMenuItem item in this.ArrangeMenuItems)
                {
                    item.Enabled = value;
                }
            }
        }
    }
}

