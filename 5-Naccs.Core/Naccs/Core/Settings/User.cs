namespace Naccs.Core.Settings
{
    using System;
    using System.Threading;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="User")]
    public class User : AbstractConfig
    {
        private ApplicationClass application = new ApplicationClass();
        private BatchSendClass batchSend = new BatchSendClass();
        private ColumnWidthClass columnWidth = new ColumnWidthClass();
        private DataViewFormClass dataViewForm = new DataViewFormClass();
        public static Type[] ExtraTypes = new Type[] { typeof(ApplicationClass), typeof(ToolBarClass), typeof(WindowClass), typeof(LastLogonClass), typeof(StringList), typeof(ColumnWidthClass), typeof(DataViewFormClass), typeof(ResultCodeColorClass), typeof(JobOptionClass), typeof(BatchSendClass) };
        private StringList historyAddressList = new StringList();
        private StringList historyJobsList = new StringList();
        private StringList historySearchList = new StringList();
        private StringList historyUserList = new StringList();
        private JobOptionClass jobOption = new JobOptionClass();
        private LastLogonClass lastLogon = new LastLogonClass();
        private ResultCodeColorClass resultCodeColor = new ResultCodeColorClass();
        private static User singletonInstance = null;
        private ToolBarClass toolBar = new ToolBarClass();
        private WindowClass window = new WindowClass();

        public event EventHandler OnSettingEnvChanged;

        public static User CreateInstance()
        {
            return CreateInstance(null);
        }

        public static User CreateInstance(PathInfo pathInfo)
        {
            if (singletonInstance == null)
            {
                if (pathInfo == null)
                {
                    pathInfo = PathInfo.CreateInstance();
                }
                singletonInstance = AbstractConfig.Load(typeof(User), ExtraTypes, pathInfo.UserSetup) as User;
                if (singletonInstance == null)
                {
                    singletonInstance = new User();
                    singletonInstance.Initialize();
                }
                singletonInstance.Application.OnSettingDivChanged += new EventHandler(singletonInstance.SettingDivChanged);
            }
            return singletonInstance;
        }

        public override void Initialize()
        {
            base.Initialize();
            this.historyJobsList.Clear();
            this.historyUserList.Clear();
            this.historyAddressList.Clear();
            this.historySearchList.Clear();
        }

        public static User ResetInstance()
        {
            PathInfo info = PathInfo.CreateInstance();
            singletonInstance = AbstractConfig.Load(typeof(User), ExtraTypes, info.UserSetup) as User;
            if (singletonInstance == null)
            {
                singletonInstance = new User();
                singletonInstance.Initialize();
            }
            singletonInstance.Application.OnSettingDivChanged += new EventHandler(singletonInstance.SettingDivChanged);
            singletonInstance.Application.SettingsDiv = info.SettingDiv;
            return singletonInstance;
        }

        public void SettingDivChanged(object sender, EventArgs e)
        {
            if (this.OnSettingEnvChanged != null)
            {
                this.OnSettingEnvChanged(this, new EventArgs());
            }
        }

        public ApplicationClass Application
        {
            get
            {
                return this.application;
            }
            set
            {
                this.application = value;
            }
        }

        public BatchSendClass BatchSend
        {
            get
            {
                return this.batchSend;
            }
            set
            {
                this.batchSend = value;
            }
        }

        public ColumnWidthClass ColumnWidth
        {
            get
            {
                return this.columnWidth;
            }
            set
            {
                this.columnWidth = value;
            }
        }

        public DataViewFormClass DataViewForm
        {
            get
            {
                return this.dataViewForm;
            }
            set
            {
                this.dataViewForm = value;
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
                return PathInfo.CreateInstance().UserSetup;
            }
        }

        [XmlArrayItem(typeof(string), ElementName="Address"), XmlArray("HistoryAddress")]
        public StringList HistoryAddressList
        {
            get
            {
                return this.historyAddressList;
            }
            set
            {
                this.historyAddressList = value;
            }
        }

        [XmlArrayItem(typeof(string), ElementName="Job"), XmlArray("HistoryJobs")]
        public StringList HistoryJobsList
        {
            get
            {
                return this.historyJobsList;
            }
            set
            {
                this.historyJobsList = value;
            }
        }

        [XmlArray("HistorySearch"), XmlArrayItem(typeof(string), ElementName="SearchInfo")]
        public StringList HistorySearchList
        {
            get
            {
                return this.historySearchList;
            }
            set
            {
                this.historySearchList = value;
            }
        }

        [XmlArray("HistroyUser"), XmlArrayItem(typeof(string), ElementName="UserCode")]
        public StringList HistoryUserList
        {
            get
            {
                return this.historyUserList;
            }
            set
            {
                this.historyUserList = value;
            }
        }

        public JobOptionClass JobOption
        {
            get
            {
                return this.jobOption;
            }
            set
            {
                this.jobOption = value;
            }
        }

        public LastLogonClass LastLogon
        {
            get
            {
                return this.lastLogon;
            }
            set
            {
                this.lastLogon = value;
            }
        }

        public ResultCodeColorClass ResultCodeColor
        {
            get
            {
                return this.resultCodeColor;
            }
            set
            {
                this.resultCodeColor = value;
            }
        }

        public ToolBarClass ToolBar
        {
            get
            {
                return this.toolBar;
            }
            set
            {
                this.toolBar = value;
            }
        }

        [XmlIgnore]
        public override bool UpdateFlg
        {
            get
            {
                if ((((!base.updateFlg && !this.application.UpdateFlg) && (!this.toolBar.UpdateFlg && !this.window.UpdateFlg)) && ((!this.lastLogon.UpdateFlg && !this.historyJobsList.UpdateFlg) && (!this.columnWidth.UpdateFlg && !this.dataViewForm.UpdateFlg))) && (((!this.resultCodeColor.UpdateFlg && !this.jobOption.UpdateFlg) && (!this.batchSend.UpdateFlg && !this.historyUserList.UpdateFlg)) && !this.historyAddressList.UpdateFlg))
                {
                    return this.historySearchList.UpdateFlg;
                }
                return true;
            }
            set
            {
                base.UpdateFlg = value;
                if (!value)
                {
                    this.application.UpdateFlg = false;
                    this.toolBar.UpdateFlg = false;
                    this.window.UpdateFlg = false;
                    this.lastLogon.UpdateFlg = false;
                    this.historyJobsList.UpdateFlg = false;
                    this.columnWidth.UpdateFlg = false;
                    this.dataViewForm.UpdateFlg = false;
                    this.resultCodeColor.UpdateFlg = false;
                    this.jobOption.UpdateFlg = false;
                    this.batchSend.UpdateFlg = false;
                    this.historyUserList.UpdateFlg = false;
                    this.historyAddressList.UpdateFlg = false;
                    this.historySearchList.UpdateFlg = false;
                }
            }
        }

        public WindowClass Window
        {
            get
            {
                return this.window;
            }
            set
            {
                this.window = value;
            }
        }
    }
}

