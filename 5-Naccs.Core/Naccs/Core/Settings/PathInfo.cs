namespace Naccs.Core.Settings
{
    using Naccs.Common.JobConfig;
    using System;
    using System.IO;
    using System.Windows.Forms;
    using System.Xml;
    using System.Xml.Serialization;

    [XmlRoot("path")]
    public class PathInfo
    {
        [XmlElement("attach-path")]
        public PathItem _attach = new PathItem(PathType.User, @"Attach\");
        [XmlElement("backup-path")]
        public PathItem _backup = new PathItem(PathType.User, @"Backup\");
        [XmlElement("code-list")]
        public PathItem _codeList = new PathItem(PathType.Environment, "code_list.csv");
        [XmlElement("customize-root")]
        public PathItem _customizeRoot = new PathItem(PathType.Install, @"Customize\");
        [XmlElement("custom-list-link")]
        public PathItem _customList = new PathItem(PathType.Install, @"Customize\Settings\custom_list.tbl");
        [XmlElement("custompath")]
        public PathItem _custompath = new PathItem(PathType.User, @"Custom\");
        [XmlElement("dataview-path")]
        public PathItem _dataView = new PathItem(PathType.User, @"DataView\");
        [XmlElement("dataview-setup")]
        public PathItem _dataviewSetup = new PathItem(PathType.User, "dataview_setup.xml");
        [XmlElement("default-gym-err-path")]
        public PathItem _default_gym_err_path = new PathItem(PathType.Install, @"help\gym_err\");
        [XmlElement("default-help-path")]
        public PathItem _default_help_path = new PathItem(PathType.Install, @"help\");
        [XmlElement("default-term-message-path")]
        public PathItem _default_term_msg_path = new PathItem(PathType.Install, @"help\term\");
        [XmlElement("demo-root")]
        public PathItem _demoRoot = new PathItem(PathType.Install, @"Help\Demo\");
        [XmlElement("file-save-root")]
        public PathItem _file_save_root = new PathItem(PathType.MyDocuments, "");
        [XmlElement("file-save-setup")]
        public PathItem _file_save_setup = new PathItem(PathType.User, "file_save_setup.xml");
        [XmlElement("general-app-link")]
        public PathItem _generalApp = new PathItem(PathType.Environment, "general_app.csv");
        [XmlElement("gstomp-info")]
        public PathItem _gstampInfo = new PathItem(PathType.User, "gstomp_info.xml");
        [XmlElement("guidance-root")]
        public PathItem _guidanceRoot = new PathItem(PathType.Install, @"Help\Demo\Guidance\");
        [XmlElement("guide-root")]
        public PathItem _guideRoot = new PathItem(PathType.Install, @"help\guide\");
        [XmlElement("gym_guidelines")]
        public PathItem _gym_guidelines = new PathItem(PathType.Install, @"Help\Demo\gym_guidelines.html");
        [XmlElement("help-setup")]
        public PathItem _help_setup = new PathItem(PathType.Protocol, "help_setup.xml");
        [XmlElement("input-history-root")]
        public PathItem _input_history_root = new PathItem(PathType.Protocol, @"InputHistory\");
        [XmlElement("jetras-exe")]
        public PathItem _jetras = new PathItem(PathType.Install, @"JETRAS\njetraslite.exe");
        [XmlElement("job-setup")]
        public PathItem _Job_setup = new PathItem(PathType.Protocol, "Job_setup.xml");
        [XmlElement("job-history")]
        public PathItem _jobHistoryName = new PathItem(PathType.Protocol, "job_history.csv");
        [XmlElement("jobtree-root")]
        public PathItem _jobtreeRoot = new PathItem(PathType.Install, @"JobTrees\");
        [XmlElement("kiosklink-root")]
        public PathItem _kiosklinkRoot = new PathItem(PathType.Install, @"Template\Kiosk\Link\");
        [XmlElement("kiosktables-root")]
        public PathItem _kiosktablesRoot = new PathItem(PathType.Install, @"Template\Kiosk\Tables\");
        [XmlElement("log-path")]
        public PathItem _logPath = new PathItem(PathType.Protocol, @"Log\");
        [XmlElement("message-classify-setup")]
        public PathItem _messageClassifySetup = new PathItem(PathType.User, "message_classify_setup.xml");
        [XmlElement("option-certification-setup")]
        public PathItem _optionCertificationSetup = new PathItem(PathType.User, "option_certification_setup.xml");
        [XmlElement("outcode-table")]
        public PathItem _outCodeTable = new PathItem(PathType.Environment, "Outcode.tbl");
        [XmlElement("printer-setup")]
        public PathItem _printerSetup = new PathItem(PathType.User, "printer_setup.xml");
        [XmlElement("printname-list")]
        public PathItem _printNameList = new PathItem(PathType.Environment, "PrintName_list.csv");
        [XmlElement("project-path")]
        public PathItem _projectpath = new PathItem(PathType.Debug, @"\VNACCS\");
        private ProtocolType _protocol = ProtocolType.netNACCS;
        [XmlElement("receive-notice-setup")]
        public PathItem _receive_notice_setup = new PathItem(PathType.User, "receive_notice_setup.xml");
        [XmlElement("scenario-root")]
        public PathItem _scenarioRoot = new PathItem(PathType.Install, @"Help\Demo\Scenario\");
        [XmlElement("signfile-path")]
        public PathItem _sFPath = new PathItem(PathType.User, Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\VNACCS\Common\SignChk\");
        [XmlElement("support-file")]
        public PathItem _supportFile = new PathItem(PathType.Install, @"help\support_info.html");
        [XmlElement("system-error-message")]
        public PathItem _system_error_message = new PathItem(PathType.Install, @"help\gym_err\sys_err.xml");
        [XmlElement("system-environment-setup")]
        public PathItem _systemEnvironmentSetup = new PathItem(PathType.Environment, "system_environment_setup.xml");
        [XmlElement("template-root")]
        public PathItem _templateRoot = new PathItem(PathType.Install, @"Template\");
        [XmlElement("user_key_setup")]
        public PathItem _user_key_setup = new PathItem(PathType.Protocol, "user_key_setup.xml");
        [XmlElement("user-setup")]
        public PathItem _user_setup = new PathItem(PathType.Protocol, "user_setup.xml");
        [XmlElement("user-environment-setup")]
        public PathItem _userEnvironmentSetup = new PathItem(PathType.Protocol, "user-environment_setup.xml");
        [XmlElement("web-sites")]
        public PathItem _webSites = new PathItem(PathType.Install, @"help\web_sites.html");
        private string allUserAppData;
        private string appdatapath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
        private const string COMMON_PATH = @"Common\";
        private string commonapppath = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);
        public const string CUSTOMIZE_PATH = @"Customize\";
        private string exePath = (Path.GetDirectoryName(Application.ExecutablePath) + @"\");
        private const string FILE_NAME = "PathNames.xml";
        private const string INTERACTIVE_PATH = @"Interactive\";
        private bool isSettingDivChanged;
        public const string JETRAS_PATH = @"JETRAS\";
        public const string JOBTREE_PATH = @"JobTrees\";
        private const string KIOSK_PATH = @"KIOSK\";
        private const string MAIL_PATH = @"Mail\";
        private string myDocuments = (Environment.GetFolderPath(Environment.SpecialFolder.Personal) + @"\");
        private const string NETNACCS_PATH = @"PT_Software\";
        private static PathInfo pathInfoInstance;
        private const string PROJECT_PATH = @"\VNACCS\";
        private const string PROJECT_PRIVATE_PATH = @"\NACCSPrivate\";
        private const string PROJECT_RECEIVER_PATH = @"\NACCSReceiver\";
        private const string PROJECT_TRAINING_PATH = @"\NACCSTraining\";
        private const string SETTING_PATH = @"Settings\";
        private SettingDivType settingDiv;
        public const string SUB_GUIDE = @"guide\";
        public const string SUB_GYM_ERR = @"gym_err\";
        public const string SUB_HELP = @"help\";
        public const string SUB_TERM = @"term\";
        public const string TEMPLATE_PATH = @"Template\";
        private string thisUserAppData;
        private string upperPath = (Path.GetDirectoryName(Path.GetDirectoryName(Application.ExecutablePath)) + @"\");

        public static PathInfo CreateInstance()
        {
            if (pathInfoInstance == null)
            {
                CreateInstance(-1);
            }
            return pathInfoInstance;
        }

        public static PathInfo CreateInstance(int Protocol)
        {
            if (pathInfoInstance == null)
            {
                string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PathNames.xml");
                if (File.Exists(path))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(PathInfo));
                    FileStream input = new FileStream(path, FileMode.Open);
                    XmlReader xmlReader = XmlReader.Create(input);
                    pathInfoInstance = (PathInfo) serializer.Deserialize(xmlReader);
                    input.Close();
                }
                else
                {
                    pathInfoInstance = new PathInfo();
                }
                SystemEnvironment environment = SystemEnvironment.CreateInstance(pathInfoInstance);
                if (environment.TerminalInfo.Demo)
                {
                    pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + @"\NACCSTraining\";
                    pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + @"\NACCSTraining\";
                }
                else if (environment.TerminalInfo.Receiver)
                {
                    pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + @"\NACCSReceiver\";
                    pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + @"\NACCSReceiver\";
                }
                else if (environment.TerminalInfo.UserKind == 4)
                {
                    pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + @"\NACCSPrivate\";
                    pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + @"\NACCSPrivate\";
                }
                else
                {
                    pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + pathInfoInstance.projectpath;
                    pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + pathInfoInstance.projectpath;
                }
                if (Protocol < 0)
                {
                    pathInfoInstance.Protocol = environment.TerminalInfo.Protocol;
                }
                else
                {
                    pathInfoInstance.Protocol = (ProtocolType) Protocol;
                }
                User user = User.CreateInstance();
                pathInfoInstance.SettingDiv = user.Application.SettingsDiv;
                pathInfoInstance.IsSettingDivChanged = false;
                user.OnSettingEnvChanged += new EventHandler(pathInfoInstance.user_OnSettingEnvChanged);
                JobConfigFactory.TemplatePath = pathInfoInstance.TemplateRoot;
                HelpSettings.CreateInstance();
            }
            return pathInfoInstance;
        }

        public string getInputHistory(string jobCode)
        {
            return (this.InputHistoryRoot + jobCode + "_history.xml");
        }

        public string getOptionCertification(int DivInt)
        {
            switch (DivInt)
            {
                case 0:
                {
                    UserPathNames names = UserPathNames.CreateInstance();
                    return Path.Combine(this.UserCommonPath(names.CommonPath.Specify, names.CommonPath.Folder), "option_certification_setup.xml");
                }
                case 1:
                    return (this.allUserAppData + @"Common\option_certification_setup.xml");
            }
            return "";
        }

        private string getValue(PathItem item)
        {
            switch (item.PathType)
            {
                case PathType.User:
                    return Path.Combine(this.CommonPath, item.Value);

                case PathType.Protocol:
                    switch (this._protocol)
                    {
                        case ProtocolType.Interactive:
                            return (this.thisUserAppData + @"Interactive\" + item.Value);

                        case ProtocolType.Mail:
                            return (this.thisUserAppData + @"Mail\" + item.Value);

                        case ProtocolType.netNACCS:
                            return (this.thisUserAppData + @"PT_Software\" + item.Value);

                        case ProtocolType.KIOSK:
                            return (this.thisUserAppData + @"KIOSK\" + item.Value);
                    }
                    return (this.thisUserAppData + item.Value);

                case PathType.Install:
                    return (this.upperPath + item.Value);

                case PathType.Environment:
                    return (this.exePath + @"Settings\" + item.Value);

                case PathType.MyDocuments:
                    return (this.myDocuments + item.Value);

                case PathType.Debug:
                    return item.Value;
            }
            return "";
        }

        public static PathInfo ResetInstance()
        {
            string path = Path.Combine(Path.GetDirectoryName(Application.ExecutablePath), "PathNames.xml");
            if (File.Exists(path))
            {
                XmlSerializer serializer = new XmlSerializer(typeof(PathInfo));
                FileStream input = new FileStream(path, FileMode.Open);
                XmlReader xmlReader = XmlReader.Create(input);
                pathInfoInstance = (PathInfo) serializer.Deserialize(xmlReader);
                input.Close();
            }
            else
            {
                pathInfoInstance = new PathInfo();
            }
            SystemEnvironment environment = SystemEnvironment.CreateInstance(pathInfoInstance);
            if (environment.TerminalInfo.Demo)
            {
                pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + @"\NACCSTraining\";
                pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + @"\NACCSTraining\";
            }
            else if (environment.TerminalInfo.Receiver)
            {
                pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + @"\NACCSReceiver\";
                pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + @"\NACCSReceiver\";
            }
            else if (environment.TerminalInfo.UserKind == 4)
            {
                pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + @"\NACCSPrivate\";
                pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + @"\NACCSPrivate\";
            }
            else
            {
                pathInfoInstance.thisUserAppData = pathInfoInstance.appdatapath + pathInfoInstance.projectpath;
                pathInfoInstance.allUserAppData = pathInfoInstance.commonapppath + pathInfoInstance.projectpath;
            }
            pathInfoInstance.Protocol = environment.TerminalInfo.Protocol;
            User user = User.CreateInstance();
            user.OnSettingEnvChanged += new EventHandler(pathInfoInstance.user_OnSettingEnvChanged);
            pathInfoInstance.SettingDiv = user.Application.SettingsDiv;
            pathInfoInstance.IsSettingDivChanged = false;
            JobConfigFactory.TemplatePath = pathInfoInstance.TemplateRoot;
            HelpSettings.CreateInstance();
            return pathInfoInstance;
        }

        public void user_OnSettingEnvChanged(object sender, EventArgs e)
        {
            this.SettingDiv = ((User) sender).Application.SettingsDiv;
        }

        public string UserCommonPath(bool specify, string folder)
        {
            if (!specify || string.IsNullOrEmpty(folder))
            {
                return (this.thisUserAppData + @"Common\");
            }
            string str = Path.Combine(folder, @"Common\");
            if (!str.EndsWith(@"\"))
            {
                str = str + @"\";
            }
            return str;
        }

        [XmlIgnore]
        public string AttachPath
        {
            get
            {
                return this.getValue(this._attach);
            }
        }

        [XmlIgnore]
        public string BackupPath
        {
            get
            {
                return this.getValue(this._backup);
            }
        }

        [XmlIgnore]
        public string CodeList
        {
            get
            {
                return this.getValue(this._codeList);
            }
        }

        public string CommonPath
        {
            get
            {
                if (this.SettingDiv == SettingDivType.User)
                {
                    UserPathNames names = UserPathNames.CreateInstance();
                    return this.UserCommonPath(names.CommonPath.Specify, names.CommonPath.Folder);
                }
                return (this.allUserAppData + @"Common\");
            }
        }

        [XmlIgnore]
        public string Custom_List
        {
            get
            {
                return this.getValue(this._customList);
            }
        }

        [XmlIgnore]
        public string CustomizeRoot
        {
            get
            {
                return this.getValue(this._customizeRoot);
            }
        }

        [XmlIgnore]
        public string CustomPath
        {
            get
            {
                return this.getValue(this._custompath);
            }
        }

        [XmlIgnore]
        public string DataViewPath
        {
            get
            {
                return this.getValue(this._dataView);
            }
        }

        [XmlIgnore]
        public string DataViewSetup
        {
            get
            {
                return this.getValue(this._dataviewSetup);
            }
        }

        public string DefaultGymErrPath
        {
            get
            {
                return this.getValue(this._default_gym_err_path);
            }
        }

        public string DefaultHelpPath
        {
            get
            {
                return this.getValue(this._default_help_path);
            }
        }

        public string DefaultTermMsgPath
        {
            get
            {
                return this.getValue(this._default_term_msg_path);
            }
        }

        public string DemoRoot
        {
            get
            {
                return this.getValue(this._demoRoot);
            }
        }

        [XmlIgnore]
        public string FileSaveRoot
        {
            get
            {
                return this.getValue(this._file_save_root);
            }
        }

        [XmlIgnore]
        public string FileSaveSetup
        {
            get
            {
                return this.getValue(this._file_save_setup);
            }
        }

        [XmlIgnore]
        public string GeneralApp
        {
            get
            {
                return this.getValue(this._generalApp);
            }
        }

        [XmlIgnore]
        public string GStampIngo
        {
            get
            {
                return this.getValue(this._gstampInfo);
            }
        }

        [XmlIgnore]
        public string GuidanceRoot
        {
            get
            {
                return this.getValue(this._guidanceRoot);
            }
        }

        [XmlIgnore]
        public string GuideRoot
        {
            get
            {
                return this.getValue(this._guideRoot);
            }
        }

        [XmlIgnore]
        public string GymGuidelines
        {
            get
            {
                return this.getValue(this._gym_guidelines);
            }
        }

        [XmlIgnore]
        public string HelpSetup
        {
            get
            {
                return this.getValue(this._help_setup);
            }
        }

        public string InputHistoryRoot
        {
            get
            {
                return this.getValue(this._input_history_root);
            }
        }

        public string InstallPath
        {
            get
            {
                return this.exePath;
            }
        }

        [XmlIgnore]
        public bool IsSettingDivChanged
        {
            get
            {
                return this.isSettingDivChanged;
            }
            set
            {
                this.isSettingDivChanged = value;
            }
        }

        [XmlIgnore]
        public string Jetras
        {
            get
            {
                return this.getValue(this._jetras);
            }
        }

        [XmlIgnore]
        public string JobHistory
        {
            get
            {
                return this.getValue(this._jobHistoryName);
            }
        }

        [XmlIgnore]
        public string JobSetup
        {
            get
            {
                return this.getValue(this._Job_setup);
            }
        }

        [XmlIgnore]
        public string JobTreeRoot
        {
            get
            {
                return this.getValue(this._jobtreeRoot);
            }
        }

        [XmlIgnore]
        public string KioskLinkRoot
        {
            get
            {
                return this.getValue(this._kiosklinkRoot);
            }
        }

        [XmlIgnore]
        public string KioskTablesRoot
        {
            get
            {
                return this.getValue(this._kiosktablesRoot);
            }
        }

        public string LogPath
        {
            get
            {
                return this.getValue(this._logPath);
            }
        }

        [XmlIgnore]
        public string MessageClassifySetup
        {
            get
            {
                return this.getValue(this._messageClassifySetup);
            }
        }

        [XmlIgnore]
        public string OptioncertificationSetup
        {
            get
            {
                return this.getValue(this._optionCertificationSetup);
            }
        }

        [XmlIgnore]
        public string OutCodeTable
        {
            get
            {
                return this.getValue(this._outCodeTable);
            }
        }

        [XmlIgnore]
        public string PrinterSetup
        {
            get
            {
                return this.getValue(this._printerSetup);
            }
        }

        [XmlIgnore]
        public string PrintNameList
        {
            get
            {
                return this.getValue(this._printNameList);
            }
        }

        [XmlIgnore]
        public string projectpath
        {
            get
            {
                return this.getValue(this._projectpath);
            }
        }

        [XmlIgnore]
        public ProtocolType Protocol
        {
            get
            {
                return this._protocol;
            }
            set
            {
                this._protocol = value;
            }
        }

        public string ProtocolEnvironmentPath
        {
            get
            {
                switch (this._protocol)
                {
                    case ProtocolType.Interactive:
                        return (this.thisUserAppData + @"Interactive\");

                    case ProtocolType.Mail:
                        return (this.thisUserAppData + @"Mail\");

                    case ProtocolType.netNACCS:
                        return (this.thisUserAppData + @"PT_Software\");

                    case ProtocolType.KIOSK:
                        return (this.thisUserAppData + @"KIOSK\");
                }
                return this.thisUserAppData;
            }
        }

        [XmlIgnore]
        public string ReceiveNoticeSetup
        {
            get
            {
                return this.getValue(this._receive_notice_setup);
            }
        }

        [XmlIgnore]
        public string ScenarioRoot
        {
            get
            {
                return this.getValue(this._scenarioRoot);
            }
        }

        [XmlIgnore]
        public SettingDivType SettingDiv
        {
            get
            {
                return this.settingDiv;
            }
            set
            {
                this.isSettingDivChanged = this.settingDiv != value;
                this.settingDiv = value;
            }
        }

        [XmlIgnore]
        public string SignFilePath
        {
            get
            {
                return this.getValue(this._sFPath);
            }
        }

        [XmlIgnore]
        public string SupportFile
        {
            get
            {
                return this.getValue(this._supportFile);
            }
        }

        public string SystemEnvironmentPath
        {
            get
            {
                return (this.exePath + @"Settings\");
            }
        }

        [XmlIgnore]
        public string SystemEnvironmentSetup
        {
            get
            {
                return this.getValue(this._systemEnvironmentSetup);
            }
        }

        public string SystemErrorMessage
        {
            get
            {
                return this.getValue(this._system_error_message);
            }
        }

        [XmlIgnore]
        public string TemplateRoot
        {
            get
            {
                return this.getValue(this._templateRoot);
            }
        }

        [XmlIgnore]
        public string UserEnvironmentSetup
        {
            get
            {
                return this.getValue(this._userEnvironmentSetup);
            }
        }

        [XmlIgnore]
        public string UserKeySetup
        {
            get
            {
                return this.getValue(this._user_key_setup);
            }
        }

        [XmlIgnore]
        public string UserPathNamesPath
        {
            get
            {
                string protocolEnvironmentPath = this.ProtocolEnvironmentPath;
                if (protocolEnvironmentPath.EndsWith(@"\"))
                {
                    protocolEnvironmentPath = protocolEnvironmentPath.TrimEnd(new char[] { '\\' });
                }
                return Path.Combine(Path.GetDirectoryName(protocolEnvironmentPath), "UserPathNames.xml");
            }
        }

        [XmlIgnore]
        public string UserSetup
        {
            get
            {
                return this.getValue(this._user_setup);
            }
        }

        [XmlIgnore]
        public string WebSites
        {
            get
            {
                return this.getValue(this._webSites);
            }
        }
    }
}

