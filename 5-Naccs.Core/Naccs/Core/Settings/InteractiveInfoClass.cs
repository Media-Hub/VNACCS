namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="InteractiveInfo")]
    public class InteractiveInfoClass : AbstractSettings
    {
        private bool autosendrecvmode;
        private int autosendrecvtm;
        public const bool default_autosendrecvmode = false;
        public const int default_autosendrecvtm = 10;
        public const bool default_jobconnect = false;
        public const string default_serverconnect = "MAIN";
        public const bool default_traceoutput = false;
        private bool jobconnect;
        private string serverconnect;
        private bool traceoutput;

        [DefaultValue(false), XmlElement(ElementName="AutoSendRecvMode")]
        public bool AutoSendRecvMode
        {
            get
            {
                return this.autosendrecvmode;
            }
            set
            {
                if (this.autosendrecvmode != value)
                {
                    base.updateFlg = this.autosendrecvmode != value;
                    this.autosendrecvmode = value;
                }
            }
        }

        [DefaultValue(10), XmlElement(ElementName="AutoSendRecvTm")]
        public int AutoSendRecvTm
        {
            get
            {
                return this.autosendrecvtm;
            }
            set
            {
                if (this.autosendrecvtm != value)
                {
                    base.updateFlg = this.autosendrecvtm != value;
                    this.autosendrecvtm = value;
                }
            }
        }

        [XmlElement(ElementName="JobConnect"), DefaultValue(false)]
        public bool JobConnect
        {
            get
            {
                return this.jobconnect;
            }
            set
            {
                if (this.jobconnect != value)
                {
                    base.updateFlg = this.jobconnect != value;
                    this.jobconnect = value;
                }
            }
        }

        [XmlElement(ElementName="ServerConnect"), DefaultValue("MAIN")]
        public string ServerConnect
        {
            get
            {
                return this.serverconnect;
            }
            set
            {
                if (this.serverconnect != value)
                {
                    base.updateFlg = this.serverconnect != value;
                    this.serverconnect = value;
                }
            }
        }

        [XmlElement(ElementName="TraceOutput"), DefaultValue(false)]
        public bool TraceOutput
        {
            get
            {
                return this.traceoutput;
            }
            set
            {
                if (this.traceoutput != value)
                {
                    base.updateFlg = this.traceoutput != value;
                    this.traceoutput = value;
                }
            }
        }
    }
}

