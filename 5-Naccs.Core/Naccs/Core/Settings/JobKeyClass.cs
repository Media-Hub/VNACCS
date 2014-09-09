namespace Naccs.Core.Settings
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="JobKey")]
    public class JobKeyClass : ListItem, ICloneable
    {
        private List<KeyClass> keyList = new List<KeyClass>();
        private int kind;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlArrayItem(typeof(KeyClass), ElementName="Key")]
        public List<KeyClass> KeyList
        {
            get
            {
                return this.keyList;
            }
            set
            {
                this.keyList = value;
            }
        }

        [XmlAttribute(AttributeName="Kind")]
        public int Kind
        {
            get
            {
                return this.kind;
            }
            set
            {
                if (this.kind != value)
                {
                    this.kind = value;
                    base.Change();
                }
            }
        }
    }
}

