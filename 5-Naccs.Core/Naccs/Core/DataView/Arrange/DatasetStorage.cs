namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.Classes;
    using Naccs.Core.Settings;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Globalization;
    using System.IO;
    using System.Threading;

    public class DatasetStorage
    {
        private BackgroundWorker BackWorker;
        private const string cnLogFileName = "DatasetStorage.log";
        private bool isBusy;
        private WriterLog log;
        private const string logEnded = "=====< Log End >=====";
        private const string logStart = "=====< Log Start >=====";
        private const string logStorageCancel = "# Saving of the old messages has been cancelled.";
        private const string logStorageEnded = "# Saving of the old messages has been completed.";
        private const string logStorageError = "# Failure in saving the old messages.「{0}」";
        private const string logStorageStart = "# Will start saving the old messages. Date/time applied for deletion:{0}";

        public event EventHandler EndedEvent;

        public DatasetStorage()
        {
            PathInfo info = PathInfo.CreateInstance();
            UserEnvironment environment = UserEnvironment.CreateInstance();
            this.log = new WriterLog();
            this.log.LogFile = Path.Combine(info.LogPath, "DatasetStorage.log");
            this.log.LogSize = environment.LogFileLength.ComLog;
            this.log.LogWrite("=====< Log Start >=====");
        }

        public void Dispose()
        {
            this.OnCancel();
            this.log.LogWrite("=====< Log End >=====");
        }

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker bw = sender as BackgroundWorker;
            object[] argument = (object[]) e.Argument;
            string strB = argument[0] as string;
            PathInfo info = PathInfo.CreateInstance();
            string[] array = new string[0];
            if (Directory.Exists(info.DataViewPath))
            {
                array = Directory.GetFiles(info.DataViewPath, "*.txt", SearchOption.TopDirectoryOnly);
            }
            Array.Sort<string>(array, (Comparison<string>) ((x, y) => ArrangeDataTable.ExtractFileDate(y).CompareTo(ArrangeDataTable.ExtractFileDate(x))));
            DataTable table = null;
            IData data = null;
            string subFolder = "";
            for (int i = 0; i < array.Length; i++)
            {
                if (bw.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                string s = ArrangeDataTable.ExtractFileDate(array[i]);
                if (!this.IsDateTime(s))
                {
                    this.log.LogWrite("Incorrect file name format.name=[" + Path.GetFileName(array[i]) + "]");
                    continue;
                }
                if (s.CompareTo(strB) >= 0)
                {
                    continue;
                }
                try
                {
                    data = DataFactory.LoadFromDataView("0", array[i]);
                }
                catch
                {
                    continue;
                }
                if (!data.IsDeleted && (data.Status == DataStatus.Send))
                {
                    continue;
                }
                string str4 = s.Substring(0, 6);
                string path = ArrangeDataTable.IndexFolderPath(str4);
                if (!(subFolder != str4))
                {
                    goto Label_0236;
                }
                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }
                this.TableSave(table, subFolder);
                if (!ArrangeDataTable.IsLock(str4))
                {
                    ArrangeDataTable.BeginLock(str4);
                    table = ArrangeDataTable.CreateDataTable();
                    if (!File.Exists(ArrangeDataTable.IndexFilePath(str4)))
                    {
                        goto Label_0232;
                    }
                    try
                    {
                        ArrangeDataTable.Load(table, str4);
                        goto Label_0232;
                    }
                    catch (Exception exception)
                    {
                        this.log.LogWrite("Failure in loading the tables.：name=[" + ArrangeDataTable.IndexFilePath(str4) + "] " + exception.Message);
                        if (table != null)
                        {
                            table.Dispose();
                            table = null;
                        }
                        table = this.TableRepair(bw, str4, false);
                        if (bw.CancellationPending && (table == null))
                        {
                            e.Cancel = true;
                            this.isBusy = false;
                            return;
                        }
                        goto Label_0232;
                    }
                }
                this.log.LogWrite("Lock files exist.：path=[" + ArrangeDataTable.IndexFolderPath(str4) + "]");
                table = this.TableRepair(bw, str4, false);
                if (bw.CancellationPending && (table == null))
                {
                    e.Cancel = true;
                    this.isBusy = false;
                    return;
                }
            Label_0232:
                subFolder = str4;
            Label_0236:
                if (!data.IsDeleted)
                {
                    if (!File.Exists(Path.Combine(path, Path.GetFileName(array[i]))))
                    {
                        File.Copy(array[i], Path.Combine(path, Path.GetFileName(array[i])), true);
                        ArrangeDataTable.DataTableDataAdd(table, data, Path.GetFileName(array[i]));
                    }
                    else
                    {
                        this.log.LogWrite("Files already exist in the save folder.：name=[" + array[i] + "]");
                    }
                }
                File.Delete(array[i]);
            }
            this.TableSave(table, subFolder);
            if (!bw.CancellationPending)
            {
                string[] strArray2 = new string[0];
                if (Directory.Exists(ArrangeDataTable.PastDataViewPath))
                {
                    strArray2 = Directory.GetFiles(ArrangeDataTable.PastDataViewPath, ArrangeDataTable.LockFileName, SearchOption.AllDirectories);
                }
                for (int j = 0; j < strArray2.Length; j++)
                {
                    string fileName = Path.GetFileName(Path.GetDirectoryName(strArray2[j]));
                    this.log.LogWrite("Lock files exist.：path=[" + ArrangeDataTable.IndexFolderPath(fileName) + "]");
                    this.TableRepair(bw, fileName, true);
                    if (bw.CancellationPending)
                    {
                        e.Cancel = true;
                        break;
                    }
                }
            }
            this.isBusy = false;
        }

        public void Execute(string RemoveDate)
        {
            if (!this.isBusy)
            {
                this.isBusy = true;
                this.BackWorker = new BackgroundWorker();
                this.BackWorker.DoWork += new DoWorkEventHandler(this.DoWork);
                this.BackWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
                this.BackWorker.WorkerSupportsCancellation = true;
                object[] argument = new object[] { RemoveDate };
                this.log.LogWrite(string.Format("# Will start saving the old messages. Date/time applied for deletion:{0}", RemoveDate));
                this.BackWorker.RunWorkerAsync(argument);
            }
        }

        private bool IsDateTime(string s)
        {
            DateTime time;
            return DateTime.TryParseExact(s, "yyyyMMddHHmmss", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out time);
        }

        public void OnCancel()
        {
            if (!this.isBusy)
            {
                return;
            }
            this.BackWorker.CancelAsync();
        Label_0014:
            if (!this.IsBusy)
            {
                this.log.LogWrite("# Saving of the old messages has been cancelled.");
            }
            else
            {
                Thread.Sleep(100);
                goto Label_0014;
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.isBusy = false;
            if (e.Error != null)
            {
                this.log.LogWrite(string.Format("# Failure in saving the old messages.「{0}」", e.Error.Message));
            }
            else if (!e.Cancelled)
            {
                this.log.LogWrite("# Saving of the old messages has been completed.");
            }
            if (this.EndedEvent != null)
            {
                this.EndedEvent(sender, new EventArgs());
            }
        }

        private DataTable TableRepair(BackgroundWorker bw, string subFolder, bool saveFlg)
        {
            try
            {
                DataTable table = ArrangeDataTable.CreateDataTable();
                string[] strArray = new string[0];
                string path = ArrangeDataTable.IndexFolderPath(subFolder);
                this.log.LogWrite("Table restoration started.：path=[" + path + "]");
                if (Directory.Exists(path))
                {
                    strArray = Directory.GetFiles(path, "*.txt", SearchOption.TopDirectoryOnly);
                }
                IData data = null;
                for (int i = 0; i < strArray.Length; i++)
                {
                    if (bw.CancellationPending)
                    {
                        this.log.LogWrite("Table restoration cancelled.");
                        return null;
                    }
                    try
                    {
                        data = DataFactory.LoadFromDataView(i.ToString(), strArray[i]);
                    }
                    catch
                    {
                        this.log.LogWrite("Incorrect message files.：name=[" + strArray[i] + "]");
                        continue;
                    }
                    string fileName = Path.GetFileName(strArray[i].ToString());
                    ArrangeDataTable.DataTableDataAdd(table, data, fileName);
                }
                if (saveFlg)
                {
                    this.log.LogWrite("Table restoration saved.");
                    this.TableSave(table, subFolder);
                }
                this.log.LogWrite("Table restoration completed.：[ " + strArray.Length.ToString() + " 件]");
                return table;
            }
            catch (Exception exception)
            {
                this.log.LogWrite("Failure in table restoration.：" + exception.Message);
                return null;
            }
        }

        private void TableSave(DataTable table, string subFolder)
        {
            if (table == null)
            {
                ArrangeDataTable.EndLock(subFolder);
            }
            else
            {
                table.AcceptChanges();
                if (table.Rows.Count > 0)
                {
                    ArrangeDataTable.Save(table, subFolder);
                }
                ArrangeDataTable.EndLock(subFolder);
                table.Dispose();
                table = null;
            }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
        }
    }
}

