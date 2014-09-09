namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="Window")]
    public class WindowClass : AbstractSettings
    {
        private bool dataview;
        public const bool default_dataview = true;
        public const bool default_jobinput = true;
        public const bool default_jobmenu = true;
        public const bool default_userinput = true;
        private bool jobinput;
        private bool jobmenu;
        private bool userinput;

        [XmlElement(ElementName="DataView"), DefaultValue(true)]
        public bool DataView
        {
            get
            {
                return this.dataview;
            }
            set
            {
                base.updateFlg = this.dataview != value;
                this.dataview = value;
            }
        }

        [DefaultValue(true), XmlElement(ElementName="JobInput")]
        public bool JobInput
        {
            get
            {
                return this.jobinput;
            }
            set
            {
                base.updateFlg = this.jobinput != value;
                this.jobinput = value;
            }
        }

        [DefaultValue(true), XmlElement(ElementName="JobMenu")]
        public bool JobMenu
        {
            get
            {
                return this.jobmenu;
            }
            set
            {
                base.updateFlg = this.jobmenu != value;
                this.jobmenu = value;
            }
        }

        [XmlElement(ElementName="UserInput"), DefaultValue(true)]
        public bool UserInput
        {
            get
            {
                return this.userinput;
            }
            set
            {
                base.updateFlg = this.userinput != value;
                this.userinput = value;
            }
        }
    }
}

