namespace Naccs.Core.Settings
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    public abstract class AbstractConfig : AbstractSettings
    {
        protected AbstractConfig()
        {
        }

        public static object Load(Type type, Type[] extraTypes, string fileName)
        {
            object obj2 = null;
            if (File.Exists(fileName))
            {
                try
                {
                    FileStream stream = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                    try
                    {
                        obj2 = new XmlSerializer(type).Deserialize(stream);
                        ((AbstractConfig) obj2).UpdateFlg = false;
                    }
                    finally
                    {
                        stream.Close();
                    }
                }
                catch
                {
                    obj2 = null;
                }
            }
            return obj2;
        }

        public bool Save()
        {
            try
            {
                string directoryName = Path.GetDirectoryName(this.FileName);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                FileStream stream = new FileStream(this.FileName, FileMode.Create);
                try
                {
                    new XmlSerializer(base.GetType()).Serialize((Stream) stream, this);
                }
                finally
                {
                    stream.Close();
                }
            }
            catch
            {
                return false;
            }
            this.UpdateFlg = false;
            return true;
        }

        protected abstract Type[] extraTypes { get; }

        protected abstract string FileName { get; }
    }
}

