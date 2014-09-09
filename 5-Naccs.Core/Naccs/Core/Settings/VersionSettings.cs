namespace Naccs.Core.Settings
{
    using System;
    using System.IO;
    using System.Windows.Forms;

    public class VersionSettings
    {
        private const string C_ApplicationVersionFileName = @"Version\{0}.ver";
        private const string C_ReceiverFileName = "NACCS_Receiver";
        private const string C_TemplateVersionFileName = @"Version\Template.ver";
        private int mApplication;
        private string mApplicationPath;
        private int mTemplate;
        private string mTemplatePath;

        public VersionSettings()
        {
            this.mApplication = 1;
            this.mTemplate = 1;
            this.Initialize(false);
        }

        public VersionSettings(bool isLauncher)
        {
            this.mApplication = 1;
            this.mTemplate = 1;
            this.Initialize(isLauncher);
        }

        private void Initialize(bool isLauncher)
        {
            SystemEnvironment environment = SystemEnvironment.CreateInstance();
            string str = environment.TerminalInfo.Protocol.ToString();
            if (environment.TerminalInfo.Receiver)
            {
                str = "NACCS_Receiver";
            }
            string directoryName = Path.GetDirectoryName(System.Windows.Forms.Application.StartupPath);
            if (isLauncher)
            {
                directoryName = Path.GetDirectoryName(directoryName);
            }
            this.mApplicationPath = Path.Combine(directoryName, string.Format(@"Version\{0}.ver", str));
            this.mTemplatePath = Path.Combine(directoryName, @"Version\Template.ver");
            this.Application = this.ReadVersion(this.mApplicationPath);
            this.Template = this.ReadVersion(this.mTemplatePath);
        }

        private int ReadVersion(string path)
        {
            int num = 1;
            try
            {
                if (!File.Exists(path))
                {
                    return num;
                }
                StreamReader reader = new StreamReader(path);
                try
                {
                    num = int.Parse(reader.ReadToEnd().Trim());
                }
                finally
                {
                    reader.Close();
                }
            }
            catch
            {
            }
            return num;
        }

        public void Save()
        {
            this.WriteVersion(this.mApplicationPath, this.Application);
            this.WriteVersion(this.mTemplatePath, this.Template);
        }

        private void WriteVersion(string path, int version)
        {
            try
            {
                string directoryName = Path.GetDirectoryName(path);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                StreamWriter writer = new StreamWriter(path);
                try
                {
                    string str2 = version.ToString().PadLeft(4, '0');
                    writer.WriteLine(str2);
                }
                finally
                {
                    writer.Close();
                }
                File.SetCreationTime(path, File.GetLastAccessTime(path));
            }
            catch
            {
            }
        }

        public int Application
        {
            get
            {
                return this.mApplication;
            }
            set
            {
                this.mApplication = value;
            }
        }

        public int Template
        {
            get
            {
                return this.mTemplate;
            }
            set
            {
                this.mTemplate = value;
            }
        }
    }
}

