namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="SystemEnvironment")]
    public class SystemEnvironment : AbstractConfig
    {
        private BatchdocInfoSysClass batchdocInfo = new BatchdocInfoSysClass();
        public static Type[] ExtraTypes = new Type[] { typeof(TerminalInfoSysClass), typeof(InteractiveInfoSysClass), typeof(MailInfoSysClass), typeof(BatchdocInfoSysClass), typeof(LogFileLengthSysClass) };
        private InteractiveInfoSysClass interactiveInfo = new InteractiveInfoSysClass();
        private LogFileLengthSysClass logFileLength = new LogFileLengthSysClass();
        private MailInfoSysClass mailInfo = new MailInfoSysClass();
        private static SystemEnvironment singletonInstance = null;
        private TerminalInfoSysClass terminalInfo = new TerminalInfoSysClass();

        public static SystemEnvironment CreateInstance()
        {
            return CreateInstance(null);
        }

        public static SystemEnvironment CreateInstance(PathInfo pathInfo)
        {
            if (singletonInstance == null)
            {
                if (pathInfo == null)
                {
                    pathInfo = PathInfo.CreateInstance();
                }
                singletonInstance = AbstractConfig.Load(typeof(SystemEnvironment), ExtraTypes, pathInfo.SystemEnvironmentSetup) as SystemEnvironment;
                if (singletonInstance == null)
                {
                    singletonInstance = new SystemEnvironment();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public static SystemEnvironment ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(SystemEnvironment), ExtraTypes, info.SystemEnvironmentSetup) as SystemEnvironment;
            if (singletonInstance == null)
            {
                singletonInstance = new SystemEnvironment();
                singletonInstance.Initialize();
            }
            return singletonInstance;
        }

        public BatchdocInfoSysClass BatchdocInfo
        {
            get
            {
                return this.batchdocInfo;
            }
            set
            {
                this.batchdocInfo = value;
            }
        }

        protected override Type[] extraTypes
        {
            get
            {
                return ExtraTypes;
            }
        }

        protected override string FileName
        {
            get
            {
                return PathInfo.CreateInstance().SystemEnvironmentSetup;
            }
        }

        [XmlElement(ElementName="InteractiveInfo")]
        public InteractiveInfoSysClass InteractiveInfo
        {
            get
            {
                return this.interactiveInfo;
            }
            set
            {
                this.interactiveInfo = value;
            }
        }

        [XmlElement(ElementName="LogFileLength")]
        public LogFileLengthSysClass LogFileLength
        {
            get
            {
                return this.logFileLength;
            }
            set
            {
                this.logFileLength = value;
            }
        }

        [XmlElement(ElementName="MailInfo")]
        public MailInfoSysClass MailInfo
        {
            get
            {
                return this.mailInfo;
            }
            set
            {
                this.mailInfo = value;
            }
        }

        [XmlElement(ElementName="TerminalInfo")]
        public TerminalInfoSysClass TerminalInfo
        {
            get
            {
                return this.terminalInfo;
            }
            set
            {
                this.terminalInfo = value;
            }
        }
    }
}

