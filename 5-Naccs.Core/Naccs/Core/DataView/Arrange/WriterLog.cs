namespace Naccs.Core.DataView.Arrange
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public class WriterLog
    {
        private string mLogFile = "";
        private int mLogSize = 0x3e8;

        private string DateTimeString()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
        }

        public void LogWrite(string s)
        {
            string str = this.DateTimeString() + " " + s + Environment.NewLine;
            this.Write(str);
        }

        public void Write(string s)
        {
            try
            {
                if (!Directory.Exists(Path.GetDirectoryName(this.LogFile)))
                {
                    Directory.CreateDirectory(Path.GetDirectoryName(this.LogFile));
                }
                if (File.Exists(this.LogFile))
                {
                    FileInfo info = new FileInfo(this.LogFile);
                    long length = info.Length;
                    if (info.Length > (this.LogSize * 0x400))
                    {
                        File.Copy(this.LogFile, Path.Combine(Path.GetDirectoryName(this.LogFile), Path.GetFileNameWithoutExtension(this.LogFile)) + ".bak", true);
                        File.Delete(this.LogFile);
                    }
                }
                File.AppendAllText(this.LogFile, s, Encoding.GetEncoding("UTF-8"));
            }
            catch
            {
            }
        }

        public string LogFile
        {
            get
            {
                return this.mLogFile;
            }
            set
            {
                this.mLogFile = value;
            }
        }

        public int LogSize
        {
            get
            {
                return this.mLogSize;
            }
            set
            {
                this.mLogSize = value;
            }
        }
    }
}

