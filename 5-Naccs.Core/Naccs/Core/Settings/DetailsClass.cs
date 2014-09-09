namespace Naccs.Core.Settings
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Details")]
    public class DetailsClass : ListItem, ICloneable
    {
        private bool autosave;
        public const bool default_autosave = false;
        public const string default_dir = @"Windowsのマイドキュメントからの相対パス\RecvUser";
        private string dir;
        private string outcode;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="AutoSave")]
        public bool AutoSave
        {
            get
            {
                return this.autosave;
            }
            set
            {
                if (this.autosave != value)
                {
                    this.autosave = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Dir")]
        public string Dir
        {
            get
            {
                return this.dir;
            }
            set
            {
                if (this.dir != value)
                {
                    if (value.EndsWith(@"\"))
                    {
                        if (string.IsNullOrEmpty(Path.GetDirectoryName(value)))
                        {
                            this.dir = value;
                        }
                        else
                        {
                            this.dir = Path.GetDirectoryName(value) + @"\";
                        }
                    }
                    else
                    {
                        this.dir = value;
                    }
                    base.Change();
                }
            }
        }

        [XmlAttribute(AttributeName="OutCode")]
        public string OutCode
        {
            get
            {
                return this.outcode;
            }
            set
            {
                if (this.outcode != value)
                {
                    this.outcode = value;
                    base.Change();
                }
            }
        }
    }
}

