namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="UserEnvironment")]
    public class UserEnvironment : AbstractConfig
    {
        private DebugFunctionClass debugFunction = new DebugFunctionClass();
        public static Type[] ExtraTypes = new Type[] { typeof(TerminalInfoClass), typeof(DebugFunctionClass), typeof(InteractiveInfoClass), typeof(MailInfoClass), typeof(HttpOptionClass), typeof(LogFileLengthClass) };
        private HttpOptionClass httpOption = new HttpOptionClass();
        private InteractiveInfoClass interactiveInfo = new InteractiveInfoClass();
        private LogFileLengthClass logFileLength = new LogFileLengthClass();
        private MailInfoClass mailInfo = new MailInfoClass();
        private static UserEnvironment singletonInstance = null;
        private TerminalInfoClass terminalInfo = new TerminalInfoClass();

        public static UserEnvironment CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(UserEnvironment), ExtraTypes, info.UserEnvironmentSetup) as UserEnvironment;
                if (singletonInstance == null)
                {
                    singletonInstance = new UserEnvironment();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.terminalInfo.Initialize();
            this.debugFunction.Initialize();
            this.interactiveInfo.Initialize();
            this.mailInfo.Initialize();
            this.httpOption.Initialize();
            this.logFileLength.Initialize();
        }

        public static UserEnvironment ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(UserEnvironment), ExtraTypes, info.UserEnvironmentSetup) as UserEnvironment;
            if (singletonInstance == null)
            {
                singletonInstance = new UserEnvironment();
                singletonInstance.Initialize();
            }
            return singletonInstance;
        }

        public DebugFunctionClass DebugFunction
        {
            get
            {
                return this.debugFunction;
            }
            set
            {
                this.debugFunction = value;
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
                return PathInfo.CreateInstance().UserEnvironmentSetup;
            }
        }

        public HttpOptionClass HttpOption
        {
            get
            {
                return this.httpOption;
            }
            set
            {
                this.httpOption = value;
            }
        }

        public InteractiveInfoClass InteractiveInfo
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

        public LogFileLengthClass LogFileLength
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

        public MailInfoClass MailInfo
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

        public TerminalInfoClass TerminalInfo
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

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (((!base.UpdateFlg && !this.terminalInfo.UpdateFlg) && (!this.debugFunction.UpdateFlg && !this.interactiveInfo.UpdateFlg)) && (!this.mailInfo.UpdateFlg && !this.httpOption.UpdateFlg))
                {
                    return this.logFileLength.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.terminalInfo.UpdateFlg = value;
                    this.debugFunction.UpdateFlg = value;
                    this.interactiveInfo.UpdateFlg = value;
                    this.mailInfo.UpdateFlg = value;
                    this.httpOption.UpdateFlg = value;
                    this.logFileLength.UpdateFlg = value;
                }
            }
        }
    }
}

