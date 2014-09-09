namespace Naccs.Core.Settings
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Type")]
    public class TypeClass : ListItem, ICloneable
    {
        private string datatype;
        private string dir;
        private bool filesavemode;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlAttribute(AttributeName="DataType")]
        public string DataType
        {
            get
            {
                return this.datatype;
            }
            set
            {
                if (this.datatype != value)
                {
                    this.datatype = value;
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

        [XmlElement(ElementName="FileSaveMode")]
        public bool FileSaveMode
        {
            get
            {
                return this.filesavemode;
            }
            set
            {
                if (this.filesavemode != value)
                {
                    this.filesavemode = value;
                    base.Change();
                }
            }
        }
    }
}

