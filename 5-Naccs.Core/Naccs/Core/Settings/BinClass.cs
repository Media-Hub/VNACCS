namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Bin")]
    public class BinClass : ICloneable
    {
        public const int default_l = 0;
        public const string default_orientation = "";
        public const string default_size = "";
        public const int default_t = 0;
        private int l;
        private string name;
        private int no;
        private string orientation;
        private string size;
        private int t;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="L"), DefaultValue(0)]
        public int L
        {
            get
            {
                return this.l;
            }
            set
            {
                this.l = value;
            }
        }

        [XmlElement(ElementName="Name")]
        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
            }
        }

        [XmlAttribute(AttributeName="No")]
        public int No
        {
            get
            {
                return this.no;
            }
            set
            {
                this.no = value;
            }
        }

        [XmlElement(ElementName="Orientation"), DefaultValue("")]
        public string Orientation
        {
            get
            {
                return this.orientation;
            }
            set
            {
                this.orientation = value;
            }
        }

        [XmlElement(ElementName="Size"), DefaultValue("")]
        public string Size
        {
            get
            {
                return this.size;
            }
            set
            {
                this.size = value;
            }
        }

        [DefaultValue(0), XmlElement(ElementName="T")]
        public int T
        {
            get
            {
                return this.t;
            }
            set
            {
                this.t = value;
            }
        }
    }
}

