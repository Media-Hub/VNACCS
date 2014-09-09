namespace Naccs.Core.Settings
{
    using System;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="MessageClassify")]
    public class MessageClassify : AbstractConfig
    {
        private DataViewClass dataview = new DataViewClass();
        public static Type[] ExtraTypes = new Type[] { typeof(TermListClass) };
        private StringList folders = new StringList();
        private static MessageClassify singletonInstance = null;
        [XmlIgnore]
        private TermListClass termList = new TermListClass();

        public static MessageClassify CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(MessageClassify), ExtraTypes, info.MessageClassifySetup) as MessageClassify;
                if (singletonInstance == null)
                {
                    singletonInstance = new MessageClassify();
                    singletonInstance.Initialize();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.termList.Clear();
            this.dataview.Initialize();
        }

        public static MessageClassify ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(MessageClassify), ExtraTypes, info.MessageClassifySetup) as MessageClassify;
            if (singletonInstance == null)
            {
                singletonInstance = new MessageClassify();
                singletonInstance.Initialize();
            }
            return singletonInstance;
        }

        public DataViewClass DataView
        {
            get
            {
                return this.dataview;
            }
            set
            {
                this.dataview = value;
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
                return PathInfo.CreateInstance().MessageClassifySetup;
            }
        }

        [XmlArray("Folders"), XmlArrayItem(typeof(string), ElementName="UserFolder")]
        public StringList Folders
        {
            get
            {
                return this.folders;
            }
            set
            {
                this.folders = value;
            }
        }

        [XmlArrayItem(typeof(TermClass), ElementName="Term")]
        public TermListClass TermList
        {
            get
            {
                return this.termList;
            }
            set
            {
                this.termList = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (!base.UpdateFlg && !this.termList.UpdateFlg)
                {
                    return this.dataview.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.termList.UpdateFlg = value;
                    this.dataview.UpdateFlg = value;
                }
            }
        }
    }
}

