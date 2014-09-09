namespace Naccs.Core.Settings
{
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="FileSave")]
    public class FileSave : AbstractConfig
    {
        private AllFileSaveClass allFileSave = new AllFileSaveClass();
        private DetailsListClass detailsList = new DetailsListClass();
        public static Type[] ExtraTypes = new Type[] { typeof(AllFileSaveClass), typeof(TypeListClass), typeof(FileNameClass), typeof(SendFileClass), typeof(DetailsListClass) };
        private FileNameClass fileNaming = new FileNameClass();
        private SendFileClass sendFile = new SendFileClass();
        private static FileSave singletonInstance = null;
        private TypeListClass typeList = new TypeListClass();

        private void addTypeList(string dataType, string dir, bool mode)
        {
            TypeClass type = new TypeClass {
                DataType = dataType,
                Dir = dir,
                FileSaveMode = mode
            };
            this.typeList.Add(type);
        }

        public static FileSave CreateInstance()
        {
            if (singletonInstance == null)
            {
                PathInfo info = PathInfo.CreateInstance();
                singletonInstance = AbstractConfig.Load(typeof(FileSave), ExtraTypes, info.FileSaveSetup) as FileSave;
                if (singletonInstance == null)
                {
                    singletonInstance = new FileSave();
                    singletonInstance.Initialize();
                    singletonInstance.SetDefaultDir();
                }
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.allFileSave.Initialize();
            this.detailsList.Clear();
            this.fileNaming.Initialize();
        }

        public static FileSave ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(FileSave), ExtraTypes, info.FileSaveSetup) as FileSave;
            if (singletonInstance == null)
            {
                singletonInstance = new FileSave();
                singletonInstance.Initialize();
                singletonInstance.SetDefaultDir();
            }
            return singletonInstance;
        }

        public void SetDefaultDir()
        {
            string fileSaveRoot = PathInfo.CreateInstance().FileSaveRoot;
            this.sendFile.SendUser = Path.Combine(fileSaveRoot, @"SendUser\");
            this.typeList.Clear();
            this.addTypeList("F", Path.Combine(fileSaveRoot, @"Csv\"), true);
            this.addTypeList("T", Path.Combine(fileSaveRoot, @"Text\"), true);
            this.addTypeList("C", Path.Combine(fileSaveRoot, @"RecvUser\"), false);
            this.addTypeList("R", Path.Combine(fileSaveRoot, @"RecvUser\"), false);
            this.addTypeList("M", Path.Combine(fileSaveRoot, @"RecvUser\"), false);
            this.addTypeList("P", Path.Combine(fileSaveRoot, @"RecvUser\"), false);
        }

        public AllFileSaveClass AllFileSave
        {
            get
            {
                return this.allFileSave;
            }
            set
            {
                this.allFileSave = value;
            }
        }

        [XmlArrayItem(typeof(DetailsClass), ElementName="Details")]
        public DetailsListClass DetailsList
        {
            get
            {
                return this.detailsList;
            }
            set
            {
                this.detailsList = value;
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
                return PathInfo.CreateInstance().FileSaveSetup;
            }
        }

        public FileNameClass FileNaming
        {
            get
            {
                return this.fileNaming;
            }
            set
            {
                this.fileNaming = value;
            }
        }

        public SendFileClass SendFile
        {
            get
            {
                return this.sendFile;
            }
            set
            {
                this.sendFile = value;
            }
        }

        [XmlArrayItem(typeof(TypeClass), ElementName="Type")]
        public TypeListClass TypeList
        {
            get
            {
                return this.typeList;
            }
            set
            {
                this.typeList = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if (((!base.UpdateFlg && !this.allFileSave.UpdateFlg) && (!this.detailsList.UpdateFlg && !this.fileNaming.UpdateFlg)) && !this.sendFile.UpdateFlg)
                {
                    return this.typeList.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.allFileSave.UpdateFlg = value;
                    this.detailsList.UpdateFlg = value;
                    this.fileNaming.UpdateFlg = value;
                    this.sendFile.UpdateFlg = value;
                    this.typeList.UpdateFlg = value;
                }
            }
        }
    }
}

