namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Key")]
    public class KeyClass : ICloneable
    {
        private string name;
        private string namekj;
        private string sckey;

        public object Clone()
        {
            return base.MemberwiseClone();
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

        [XmlIgnore]
        public string NameKJ
        {
            get
            {
                return this.namekj;
            }
            set
            {
                this.namekj = value;
            }
        }

        [XmlElement(ElementName="Sckey")]
        public string Sckey
        {
            get
            {
                return UserKey.NoneCheck(this.sckey);
            }
            set
            {
                this.sckey = value;
            }
        }
    }
}

