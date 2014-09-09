namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="TermMessage")]
    public class TermMessage : AbstractConfig
    {
        public static Type[] ExtraTypes = new Type[] { typeof(TermMessageSetClass) };
        private static TermMessage singletonInstance = null;
        private TermMessageSetListClass termMessageSetList = new TermMessageSetListClass();

        public void AddMessage(string code, string message, string button, string description, string disposition)
        {
            TermMessageSetClass termmessageset = new TermMessageSetClass {
                Code = code,
                Message = message,
                Button = button,
                Description = description,
                Disposition = disposition
            };
            this.termMessageSetList.Add(termmessageset);
        }

        public static TermMessage CreateInstance()
        {
            if (singletonInstance == null)
            {
                HelpSettings settings = HelpSettings.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(TermMessage), ExtraTypes, settings.HelpPath.TermMsg) as TermMessage;
                if (singletonInstance == null)
                {
                    singletonInstance = new TermMessage();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
        }

        public static TermMessage ResetInstance()
        {
            HelpSettings settings = HelpSettings.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(TermMessage), ExtraTypes, settings.HelpPath.TermMsg) as TermMessage;
            if (singletonInstance == null)
            {
                singletonInstance = new TermMessage();
                singletonInstance.Initialize();
            }
            return singletonInstance;
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
                return HelpSettings.CreateInstance().HelpPath.TermMsg;
            }
        }

        [XmlArrayItem(typeof(TermMessageSetClass), ElementName="TermMessageSet")]
        public TermMessageSetListClass TermMessageSetList
        {
            get
            {
                return this.termMessageSetList;
            }
            set
            {
                this.termMessageSetList = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.UpdateFlg)
                {
                    return this.termMessageSetList.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.termMessageSetList.UpdateFlg = value;
                }
            }
        }
    }
}

