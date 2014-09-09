namespace Naccs.Core.Main
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using System;
    using System.Diagnostics;
    using System.IO;
    using System.Threading;
    using System.Windows.Forms;

    public class VersionuptoolUpdater
    {
        private const string cnDownloadFolder = "download";
        private const string cnUpdateWorkFolder = "updatework";
        private const int cnWaitMaxTimer = 0x2710;
        private const int cnWaitTimer = 100;

        public void Excute(string toolpath)
        {
            string directoryName = Path.GetDirectoryName(toolpath);
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(toolpath);
            try
            {
                this.Update(directoryName, fileNameWithoutExtension);
            }
            catch (Exception exception)
            {
                MessageDialog dialog = new MessageDialog();
                dialog.ShowMessage("E702", string.Format("{0}\r\n{1}", exception.Message, exception.StackTrace));
                dialog.Dispose();
            }
        }

        private bool ExistsProcess(string processName)
        {
            return (Process.GetProcessesByName(processName).Length > 0);
        }

        private void FolderCopy(string srcFolder, string destFolder)
        {
            this.Logout(string.Format(Resources.ResourceManager.GetString("msgFolderCopy"), srcFolder, destFolder));
            foreach (string str in Directory.GetFiles(srcFolder, "*", SearchOption.AllDirectories))
            {
                string str2 = str.Replace(srcFolder + @"\", "");
                string path = Path.Combine(destFolder, str2);
                if (!Directory.Exists(Path.GetDirectoryName(path)))
                {
                    this.Logout(string.Format(Resources.ResourceManager.GetString("msgFolderCreate"), Path.GetDirectoryName(path)));
                    Directory.CreateDirectory(Path.GetDirectoryName(path));
                }
                this.ReadOnlyCancel(str);
                this.ReadOnlyCancel(path);
                this.Logout(string.Format(Resources.ResourceManager.GetString("msgFileCopy"), str2, Path.GetDirectoryName(path)));
                File.Copy(str, path, true);
            }
        }

        private void FolderDelete(string folder)
        {
            if (Directory.Exists(folder))
            {
                Directory.Delete(folder, true);
            }
        }

        private void Logout(string message)
        {
            ULogClass.LogWrite(message);
        }

        private void ReadOnlyCancel(string path)
        {
            if (File.Exists(path))
            {
                FileAttributes fileAttributes = File.GetAttributes(path);
                if ((fileAttributes & FileAttributes.ReadOnly) == FileAttributes.ReadOnly)
                {
                    fileAttributes &= ~FileAttributes.ReadOnly;
                    File.SetAttributes(path, fileAttributes);
                }
            }
        }

        private void Update(string rootFolder, string waitProcess)
        {
            string path = Path.Combine(rootFolder, "updatework");
            if (Directory.Exists(path))
            {
                this.Logout(Resources.ResourceManager.GetString("msgStart"));
                string str2 = Path.Combine(path, "download");
                if (Directory.Exists(str2))
                {
                    if (!this.WaitProcess(waitProcess))
                    {
                        throw new Exception(Resources.ResourceManager.GetString("errProcess"));
                    }
                    this.FolderCopy(str2, rootFolder);
                    this.FolderDelete(str2);
                }
                this.FolderDelete(path);
                this.Logout(Resources.ResourceManager.GetString("msgEnded"));
            }
        }

        private bool WaitProcess(string processName)
        {
            bool flag = true;
            if (!string.IsNullOrEmpty(processName))
            {
                flag = false;
                int num = 0;
                while (num < 0x2710)
                {
                    if (this.ExistsProcess(processName))
                    {
                        Thread.Sleep(100);
                        num += 100;
                        Application.DoEvents();
                    }
                    else
                    {
                        return true;
                    }
                }
            }
            return flag;
        }
    }
}

