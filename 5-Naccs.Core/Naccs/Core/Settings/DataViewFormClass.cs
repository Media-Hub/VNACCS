namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="DataViewForm")]
    public class DataViewFormClass : AbstractSettings
    {
        public const bool default_readvisible = true;
        private bool readvisible;

        [XmlElement(ElementName="ReadVisible"), DefaultValue(true)]
        public bool ReadVisible
        {
            get
            {
                return this.readvisible;
            }
            set
            {
                base.updateFlg = this.readvisible != value;
                this.readvisible = value;
            }
        }
    }
}

