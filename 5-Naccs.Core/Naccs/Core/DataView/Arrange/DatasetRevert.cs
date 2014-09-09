namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.DataView;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.IO;

    public class DatasetRevert
    {
        private BackgroundWorker BackWorker;
        private ArrangeProgress RevertProgress;
        private RunWorkerCompletedEventArgs WorkerResult;

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = (BackgroundWorker) sender;
            RevertWorkerArgs argument = (RevertWorkerArgs) e.Argument;
            IDataView dataView = argument.DataView;
            int length = argument.FileNames.Length;
            int num2 = 0;
            int num3 = 0;
            bool flag = false;
            Exception exception = null;
            List<string> list = new List<string>();
            List<string> list2 = new List<string>();
            for (int i = 0; i < length; i++)
            {
                if (worker.CancellationPending)
                {
                    flag = true;
                    break;
                }
                try
                {
                    string path = Path.Combine(PathInfo.CreateInstance().DataViewPath, argument.FileNames[i]);
                    if (File.Exists(path))
                    {
                        list.Add(argument.FileNames[i]);
                    }
                    else
                    {
                        File.Copy(Path.Combine(argument.PathRoot, argument.FileNames[i]), path, false);
                        dataView.ViewRevert(path);
                        num3++;
                    }
                }
                catch (Exception exception2)
                {
                    list2.Add(argument.FileNames[i]);
                    if (exception == null)
                    {
                        exception = exception2;
                    }
                }
                if (worker.WorkerReportsProgress)
                {
                    int percentProgress = ArrangeProgress.DisplayPercentage(length, i + 1);
                    if (percentProgress > num2)
                    {
                        worker.ReportProgress(percentProgress);
                        num2 = percentProgress;
                    }
                }
            }
            RevertWorkerResult result = new RevertWorkerResult {
                RevertCount = num3,
                IsCancel = flag,
                FirstError = exception,
                SkipFiles = list.ToArray(),
                ErrorFiles = list2.ToArray()
            };
            e.Result = result;
        }

        public RevertWorkerResult Execute(RevertWorkerArgs args)
        {
            RevertWorkerResult result;
            try
            {
                this.RevertProgress = new ArrangeProgress(ArrangeProgress.ProgresType.Revert);
                this.RevertProgress.CancelEvent += new EventHandler(this.OnCancel);
                this.RevertProgress.CanCancel = true;
                this.BackWorker = new BackgroundWorker();
                this.BackWorker.DoWork += new DoWorkEventHandler(this.DoWork);
                this.BackWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(this.RunWorkerCompleted);
                this.BackWorker.ProgressChanged += new ProgressChangedEventHandler(this.ProgressChanged);
                this.BackWorker.WorkerReportsProgress = true;
                this.BackWorker.WorkerSupportsCancellation = true;
                this.BackWorker.RunWorkerAsync(args);
                this.RevertProgress.ShowDialog();
                if (this.WorkerResult.Error != null)
                {
                    throw this.WorkerResult.Error;
                }
                result = (RevertWorkerResult) this.WorkerResult.Result;
            }
            finally
            {
                this.BackWorker.Dispose();
                this.RevertProgress.Dispose();
                this.RevertProgress = null;
            }
            return result;
        }

        private void OnCancel(object sender, EventArgs e)
        {
            if (this.RevertProgress != null)
            {
                this.BackWorker.CancelAsync();
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            if ((this.RevertProgress != null) && (e.ProgressPercentage > 0))
            {
                this.RevertProgress.Percentage = e.ProgressPercentage;
            }
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.WorkerResult = e;
            this.RevertProgress.Close();
        }
    }
}

