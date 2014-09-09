namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="ReceiveInform")]
    public class ReceiveInformClass : ListItem, ICloneable
    {
        public const bool default_inform = true;
        public const bool default_informsound = false;
        private bool inform;
        private bool informsound;
        private string outcode;

        public object Clone()
        {
            return base.MemberwiseClone();
        }

        [XmlElement(ElementName="Inform")]
        public bool Inform
        {
            get
            {
                return this.inform;
            }
            set
            {
                if (this.inform != value)
                {
                    this.inform = value;
                    base.Change();
                }
            }
        }

        [XmlElement(ElementName="InformSound")]
        public bool InformSound
        {
            get
            {
                return this.informsound;
            }
            set
            {
                if (this.informsound != value)
                {
                    this.informsound = value;
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

