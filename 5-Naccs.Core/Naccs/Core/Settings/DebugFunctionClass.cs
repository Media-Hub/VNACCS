namespace Naccs.Core.Settings
{
    using System;
    using System.ComponentModel;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="DebugFunction")]
    public class DebugFunctionClass : AbstractSettings
    {
        private bool check_off;
        private int control;
        public const bool default_check_off = true;
        public const int default_control = 0;
        public const bool default_userkindflag = false;
        private bool userkindflag;

        public override void Initialize()
        {
            base.Initialize();
            switch (SystemEnvironment.CreateInstance(PathInfo.CreateInstance()).TerminalInfo.Protocol)
            {
                case ProtocolType.Interactive:
                    this.control = 0;
                    return;

                case ProtocolType.Mail:
                    this.control = 1;
                    return;

                case ProtocolType.netNACCS:
                    this.control = 3;
                    return;
            }
        }

        [XmlElement(ElementName="Check_Off"), DefaultValue(true)]
        public bool Check_Off
        {
            get
            {
                return this.check_off;
            }
            set
            {
                this.check_off = value;
            }
        }

        [XmlElement(ElementName="Control"), DefaultValue(0)]
        public int Control
        {
            get
            {
                return this.control;
            }
            set
            {
                this.control = value;
            }
        }

        [XmlElement(ElementName="UserKindFlag"), DefaultValue(false)]
        public bool UserKindFlag
        {
            get
            {
                return this.userkindflag;
            }
            set
            {
                this.userkindflag = value;
            }
        }
    }
}

