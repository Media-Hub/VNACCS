namespace Naccs.Core.Classes
{
    using Naccs.Core.DataView;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.IO;
    using System.Text;
    using System.Windows.Forms;

    public class ResourceManager
    {
        private const string C_DELLIST = "DeleteList.cfg";
        private const string C_XCOPY = "copy.cfg";
        private const string UseCharset = "UTF-8";

        public void CopyDirectory(string stSourcePath, string stDestPath)
        {
            if (!Directory.Exists(stDestPath))
            {
                Directory.CreateDirectory(stDestPath);
                File.SetAttributes(stDestPath, File.GetAttributes(stSourcePath));
            }
            foreach (string str in Directory.GetFiles(stSourcePath))
            {
                Application.DoEvents();
                string path = Path.Combine(stDestPath, Path.GetFileName(str));
                if (File.GetLastWriteTime(str) != File.GetLastWriteTime(path))
                {
                    File.Copy(str, path, true);
                }
            }
            foreach (string str3 in Directory.GetDirectories(stSourcePath))
            {
                string str4 = Path.Combine(stDestPath, Path.GetFileName(str3));
                this.CopyDirectory(str3, str4);
            }
        }

        public void ResourceCopy()
        {
            PathInfo info = PathInfo.CreateInstance();
            string path = Path.Combine(info.ProtocolEnvironmentPath, "copy.cfg");
            if (File.Exists(path))
            {
                using (DeleteRestore restore = new DeleteRestore(5))
                {
                    restore.Show();
                    restore.Refresh();
                    try
                    {
                        string serverPath = "";
                        using (StreamReader reader = new StreamReader(path, Encoding.GetEncoding("UTF-8")))
                        {
                            serverPath = reader.ReadLine();
                        }
                        if (serverPath.Length != 0)
                        {
                            string directoryName = Path.GetDirectoryName(info.TemplateRoot.TrimEnd(new char[] { '\\' }));
                            this.RestoreDelete(directoryName, serverPath);
                            this.CopyDirectory(serverPath, directoryName);
                        }
                    }
                    catch (Exception exception)
                    {
                        string message = Resources.ResourceManager.GetString("CORE17") + Environment.NewLine + Resources.ResourceManager.GetString("CORE18") + Environment.NewLine + Resources.ResourceManager.GetString("CORE19") + Environment.NewLine + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                        using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                        {
                            form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, message);
                        }
                    }
                }
            }
        }

        public void RestoreDelete(string localPath, string serverPath)
        {
            string str = "";
            PathInfo info = PathInfo.CreateInstance();
            string path = Path.Combine(serverPath, "DeleteList.cfg");
            if (File.Exists(path))
            {
                string str4 = Path.Combine(localPath, "DeleteList.cfg");
                if (File.Exists(str4))
                {
                    using (StreamReader reader = new StreamReader(str4, Encoding.GetEncoding("UTF-8")))
                    {
                        str = reader.ReadLine();
                    }
                }
                StreamReader reader2 = new StreamReader(path, Encoding.GetEncoding("UTF-8"));
                try
                {
                    if (reader2.ReadLine() != str)
                    {
                        if (Directory.Exists(info.TemplateRoot))
                        {
                            Directory.Delete(info.TemplateRoot, true);
                        }
                        if (Directory.Exists(info.DefaultHelpPath))
                        {
                            Directory.Delete(info.DefaultHelpPath, true);
                        }
                    }
                }
                catch (Exception exception)
                {
                    string message = Resources.ResourceManager.GetString("CORE20") + Environment.NewLine + MessageDialog.CreateExceptionMessage(exception);
                    using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                    {
                        form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Error, message);
                    }
                }
                finally
                {
                    if (reader2 != null)
                    {
                        reader2.Dispose();
                    }
                }
            }
        }
    }
}

