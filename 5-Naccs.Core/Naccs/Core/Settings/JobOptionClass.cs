namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="JobOption")]
    public class JobOptionClass : AbstractSettings
    {
        public const bool default_queryfieldclear = true;
        private bool queryfieldclear;

        [DefaultValue(true), XmlElement(ElementName="QueryFieldClear")]
        public bool QueryFieldClear
        {
            get
            {
                return this.queryfieldclear;
            }
            set
            {
                base.updateFlg = this.queryfieldclear != value;
                this.queryfieldclear = value;
            }
        }
    }
}

