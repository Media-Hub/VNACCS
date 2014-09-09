namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="ReceiveNotice")]
    public class ReceiveNotice : AbstractConfig
    {
        public static Type[] ExtraTypes = new Type[] { typeof(ReceiveInformListClass), typeof(SoundClass), typeof(WarningInformClass), typeof(InformClass) };
        private InformClass inform = new InformClass();
        private ReceiveInformListClass receiveInformList = new ReceiveInformListClass();
        private static ReceiveNotice singletonInstance = null;
        private SoundClass sound = new SoundClass();
        private WarningInformClass warningInform = new WarningInformClass();

        public static ReceiveNotice CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(ReceiveNotice), ExtraTypes, info.ReceiveNoticeSetup) as ReceiveNotice;
                if (singletonInstance == null)
                {
                    singletonInstance = new ReceiveNotice();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.receiveInformList.Clear();
            this.sound.Initialize();
            this.warningInform.Initialize();
            this.inform.Initialize();
        }

        public static ReceiveNotice ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(ReceiveNotice), ExtraTypes, info.ReceiveNoticeSetup) as ReceiveNotice;
            if (singletonInstance == null)
            {
                singletonInstance = new ReceiveNotice();
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
                return PathInfo.CreateInstance().ReceiveNoticeSetup;
            }
        }

        public InformClass Inform
        {
            get
            {
                return this.inform;
            }
            set
            {
                this.inform = value;
            }
        }

        [XmlArrayItem(typeof(ReceiveInformClass), ElementName="ReceiveInform")]
        public ReceiveInformListClass ReceiveInformList
        {
            get
            {
                return this.receiveInformList;
            }
            set
            {
                this.receiveInformList = value;
            }
        }

        public SoundClass Sound
        {
            get
            {
                return this.sound;
            }
            set
            {
                this.sound = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if ((!base.UpdateFlg && !this.receiveInformList.UpdateFlg) && (!this.sound.UpdateFlg && !this.warningInform.UpdateFlg))
                {
                    return this.inform.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.receiveInformList.UpdateFlg = value;
                    this.sound.UpdateFlg = value;
                    this.warningInform.UpdateFlg = value;
                    this.inform.UpdateFlg = value;
                }
            }
        }

        public WarningInformClass WarningInform
        {
            get
            {
                return this.warningInform;
            }
            set
            {
                this.warningInform = value;
            }
        }
    }
}

