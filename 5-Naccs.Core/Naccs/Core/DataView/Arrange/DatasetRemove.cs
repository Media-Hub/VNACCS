namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.Classes;
    using System;
    using System.ComponentModel;
    using System.Data;
    using System.Windows.Forms;

    public class DatasetRemove
    {
        private BackgroundWorker BackWorker;
        private ArrangeProgress RemoveProgress;
        private RunWorkerCompletedEventArgs WorkerResult;

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker) sender;
            RemoveWorkerArgs argument = (RemoveWorkerArgs) e.Argument;
            DataTable table = argument.Table;
            int count = table.Rows.Count;
            int num2 = 0;
            int num3 = 0;
            bool flag = false;
            for (int i = count - 1; i > -1; i--)
            {
                if (worker.CancellationPending)
                {
                    flag = true;
                    break;
                }
                DataRow row = table.Rows[i];
                string str = ArrangeDataTable.ExtractFileDate(row["clFileName"].ToString());
                string str2 = row["clStatus"].ToString();
                bool flag2 = (bool) row["clDelete"];
                if ((str.CompareTo(argument.RemoveDate) < 0) && (!str2.Equals("s") || flag2))
                {
                    table.Rows[i].Delete();
                    num3++;
                }
                if (worker.WorkerReportsProgress)
                {
                    int percentProgress = ArrangeProgress.DisplayPercentage(count, count - i);
                    if (percentProgress > num2)
                    {
                        worker.ReportProgress(percentProgress);
                        num2 = percentProgress;
                    }
                }
            }
            if (argument.ResetID)
            {
                table.AcceptChanges();
                for (int j = 0; j < table.Rows.Count; j++)
                {
                    table.Rows[j]["clID"] = j;
                }
            }
            RemoveWorkerResult result = new RemoveWorkerResult {
                RemoveCount = num3,
                IsCancel = flag
            };
            e.Result = result;
        }

        public RemoveWorkerResult Execute(RemoveWorkerArgs args)
        {
            RemoveWorkerResult result;
            try
            {
                this.RemoveProgress = new ArrangeProgress(ArrangeProgress.ProgresType.Remove);
                this.RemoveProgress.CancelEvent += new EventHandler(this.OnCancel);
                this.RemoveProgress.CanCancel = true;
                this.BackWorker = new BackgroundWorker();
                this.BackWorker.DoWork += new DoWorkEventHandler(this.DoWork);
                this.BackWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
                this.BackWorker.ProgressChanged += new ProgressChangedEventHandler(this.ProgressChanged);
                this.BackWorker.WorkerReportsProgress = true;
                this.BackWorker.WorkerSupportsCancellation = true;
                this.BackWorker.RunWorkerAsync(args);
                this.RemoveProgress.ShowDialog();
                if (this.WorkerResult.Error != null)
                {
                    throw this.WorkerResult.Error;
                }
                result = (RemoveWorkerResult) this.WorkerResult.Result;
            }
            finally
            {
                this.BackWorker.Dispose();
                this.RemoveProgress.Dispose();
                this.RemoveProgress = null;
            }
            return result;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            MessageDialog dialog = new MessageDialog {
                TopMost = true
            };
            DialogResult result = dialog.ShowMessage("C404", "");
            dialog.Close();
            dialog.Dispose();
            if ((result == DialogResult.Yes) && (this.RemoveProgress != null))
            {
                this.BackWorker.CancelAsync();
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((this.RemoveProgress != null) && (e.ProgressPercentage > 0))
            {
                this.RemoveProgress.Percentage = e.ProgressPercentage;
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.WorkerResult = e;
            this.RemoveProgress.Close();
        }
    }
}

