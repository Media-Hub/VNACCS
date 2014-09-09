namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="LogFileLength")]
    public class LogFileLengthClass : AbstractSettings
    {
        private int comlog;
        public const int default_comlog = 0x3e8;
        public const int default_msglog = 100;
        private int msglog;

        [XmlElement(ElementName="ComLog"), DefaultValue(0x3e8)]
        public int ComLog
        {
            get
            {
                return this.comlog;
            }
            set
            {
                if (this.comlog != value)
                {
                    base.updateFlg = this.comlog != value;
                    this.comlog = value;
                }
            }
        }

        [XmlElement(ElementName="MsgLog"), DefaultValue(100)]
        public int MsgLog
        {
            get
            {
                return this.msglog;
            }
            set
            {
                if (this.msglog != value)
                {
                    base.updateFlg = this.msglog != value;
                    this.msglog = value;
                }
            }
        }
    }
}

