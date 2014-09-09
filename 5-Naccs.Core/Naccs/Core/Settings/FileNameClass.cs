namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="FileName")]
    public class FileNameClass : AbstractSettings
    {
        public const string default_filenamerule = "1234";
        private string filenamerule;

        [XmlElement(ElementName="FileNameRule"), DefaultValue("1234")]
        public string FileNameRule
        {
            get
            {
                return this.filenamerule;
            }
            set
            {
                base.updateFlg = this.filenamerule != value;
                this.filenamerule = value;
            }
        }
    }
}

