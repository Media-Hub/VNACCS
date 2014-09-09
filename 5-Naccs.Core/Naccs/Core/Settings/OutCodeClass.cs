namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="OutCode")]
    public class OutCodeClass : ListItem, ICloneable
    {
        private string binname;
        private int binno;
        private int count;
        public const int default_count = 1;
        public const bool default_mode = true;
        private bool mode;
        private string name;
        private string printer;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="BinName")]
        public string BinName
        {
            get
            {
                return this.binname;
            }
            set
            {
                if (this.binname != value)
                {
                    this.binname = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="BinNo")]
        public int BinNo
        {
            get
            {
                return this.binno;
            }
            set
            {
                if (this.binno != value)
                {
                    this.binno = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Count")]
        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                if (this.count != value)
                {
                    this.count = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Mode")]
        public bool Mode
        {
            get
            {
                return this.mode;
            }
            set
            {
                if (this.mode != value)
                {
                    this.mode = value;
                    base.Change();
                }
            }
        }

        [XmlAttribute(AttributeName="Name")]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                if (this.name != value)
                {
                    this.name = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="Printer")]
        public string Printer
        {
            get
            {
                return this.printer;
            }
            set
            {
                if (this.printer != value)
                {
                    this.printer = value;
                    base.Change();
                }
            }
        }
    }
}

