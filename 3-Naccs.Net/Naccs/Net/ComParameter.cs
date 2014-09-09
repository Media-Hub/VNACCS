namespace Naccs.Net
{
    using System;
    using System.IO;

    public class ComParameter : SignParameter
    {
        private const string CN_XMLTAG = "<?xml version";
        private string mAttachFolder = "";
        private string mDataString = "";

        public bool CopyAttachFiles(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                foreach (string str in this.AttachFiles)
                {
                    File.Copy(Path.Combine(this.mAttachFolder, str), Path.Combine(path, str), true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool DeleteAttachFolder()
        {
            try
            {
                if (Directory.Exists(this.mAttachFolder))
                {
                    Directory.Delete(this.mAttachFolder, true);
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public bool IsXmlData()
        {
            return (this.DataType == DataTypes.XML);
        }

        public bool MoveAttachFiles(string path)
        {
            try
            {
                if (Directory.Exists(path))
                {
                    Directory.Delete(path, true);
                }
                Directory.CreateDirectory(path);
                foreach (string str in this.AttachFiles)
                {
                    File.Move(Path.Combine(this.mAttachFolder, str), Path.Combine(path, str));
                }
                this.mAttachFolder = path;
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int AttachCount
        {
            get
            {
                return this.AttachFiles.Length;
            }
        }

        public string[] AttachFiles
        {
            get
            {
                if (string.IsNullOrEmpty(this.mAttachFolder))
                {
                    return new string[0];
                }
                try
                {
                    string[] files = Directory.GetFiles(this.mAttachFolder);
                    string[] strArray2 = new string[files.Length];
                    int index = 0;
                    foreach (string str in files)
                    {
                        strArray2[index] = Path.GetFileName(str);
                        index++;
                    }
                    return strArray2;
                }
                catch
                {
                    return new string[0];
                }
            }
        }

        public string AttachFolder
        {
            get
            {
                return this.mAttachFolder;
            }
            set
            {
                this.mAttachFolder = value;
            }
        }

        public string DataString
        {
            get
            {
                return this.mDataString;
            }
            set
            {
                this.mDataString = value;
            }
        }

        public DataTypes DataType
        {
            get
            {
                if (string.IsNullOrEmpty(this.mDataString))
                {
                    return DataTypes.None;
                }
                if (this.mDataString.StartsWith("<?xml version"))
                {
                    return DataTypes.XML;
                }
                return DataTypes.Text;
            }
        }

        public enum DataTypes
        {
            None,
            Text,
            XML
        }
    }
}

