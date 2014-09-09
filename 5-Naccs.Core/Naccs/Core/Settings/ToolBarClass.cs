namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="ToolBar")]
    public class ToolBarClass : AbstractSettings
    {
        public const bool default_functionbar = true;
        public const bool default_job = true;
        public const bool default_standard = true;
        private bool functionbar;
        private bool job;
        private bool standard;

        [XmlElement(ElementName="FunctionBar"), DefaultValue(true)]
        public bool FunctionBar
        {
            get
            {
                return this.functionbar;
            }
            set
            {
                this.functionbar = value;
            }
        }

        [DefaultValue(true), XmlElement(ElementName="Job")]
        public bool Job
        {
            get
            {
                return this.job;
            }
            set
            {
                base.updateFlg = this.job != value;
                this.job = value;
            }
        }

        [XmlElement(ElementName="Standard"), DefaultValue(true)]
        public bool Standard
        {
            get
            {
                return this.standard;
            }
            set
            {
                base.updateFlg = this.standard != value;
                this.standard = value;
            }
        }
    }
}

