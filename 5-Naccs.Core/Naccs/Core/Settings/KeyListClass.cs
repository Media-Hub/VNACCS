namespace Naccs.Core.Settings
{
    using System;
    using Naccs.Core.Properties;

    public class KeyListClass
    {
        public JobKeyClass KeyInfoList = new JobKeyClass();
        private string[,] keyListG = new string[,] { 
            { "", "mnAppendJobData", Resources.ResourceManager.GetString("UK034"), "F6" }, { "", "mnUpdateJobData", Resources.ResourceManager.GetString("UK035"), "F7" }, { "", "mnFileOpen", Resources.ResourceManager.GetString("UK058"), "Ctrl+O" }, { "", "mnCsvCommon", Resources.ResourceManager.GetString("UK036"), "" }, { "", "mnCsvRan", Resources.ResourceManager.GetString("UK037"), "" }, { "", "mnAttachOpen", Resources.ResourceManager.GetString("UK038"), "" }, { "", "mnAttachDelete", Resources.ResourceManager.GetString("UK039"), "" }, { "", "mnAttachAppend", Resources.ResourceManager.GetString("UK040"), "" }, { "", "mnJobClose", Resources.ResourceManager.GetString("UK041"), "" }, { "", "mnJobUndo", Resources.ResourceManager.GetString("UK042"), "Ctrl+Z" }, { "", "mnCut", Resources.ResourceManager.GetString("UK043"), "Ctrl+X" }, { "", "mnCopy", Resources.ResourceManager.GetString("UK044"), "Ctrl+C" }, { "", "mnPaste", Resources.ResourceManager.GetString("UK045"), "Ctrl+V" }, { "", "mnRowCopy", Resources.ResourceManager.GetString("UK046"), "" }, { "", "mnRowPaste", Resources.ResourceManager.GetString("UK047"), "" }, { "", "mnGridPaste", Resources.ResourceManager.GetString("UK048"), "" }, 
            { "", "mnGridInsert", Resources.ResourceManager.GetString("UK049"), "" }, { "", "mnGridRemove", Resources.ResourceManager.GetString("UK050"), "" }, { "", "mnAllClear", Resources.ResourceManager.GetString("UK051"), "" }, { "", "mnCommonClear", Resources.ResourceManager.GetString("UK052"), "" }, { "", "mnClearCurrentToEnd", Resources.ResourceManager.GetString("UK053"), "Ctrl+D" }, { "", "mnSelectRanClear", Resources.ResourceManager.GetString("UK054"), "" }, { "", "mnChangeActivePage", Resources.ResourceManager.GetString("UK055"), "F5" }
         };
        private string[,] keyListR = new string[,] { 
            { "", "mnOpen", Resources.ResourceManager.GetString("UK066"), "F3" }, { "", "mnFileDataViewAppend", Resources.ResourceManager.GetString("UK003"), "" }, { "", "mnPrintDataView", Resources.ResourceManager.GetString("UK004"), "" }, { "", "mnPrintDataViewPreview", Resources.ResourceManager.GetString("UK005"), "" }, { "", "mnUndo", Resources.ResourceManager.GetString("UK006"), "Ctrl+Z" }, { "", "mnSelectAll", Resources.ResourceManager.GetString("UK007"), "Ctrl+A" }, { "", "mnCLearDelete", Resources.ResourceManager.GetString("UK008"), "" }, { "", "mnCreateFolder", Resources.ResourceManager.GetString("UK009"), "" }, { "", "mnRenameFolder", Resources.ResourceManager.GetString("UK010"), "" }, { "", "mnDistribute", Resources.ResourceManager.GetString("UK011"), "" }, { "", "mnSelectedDistribute", Resources.ResourceManager.GetString("UK012"), "" }, { "", "mnViewRefresh", Resources.ResourceManager.GetString("UK013"), "" }, { "", "mnLogOn", Resources.ResourceManager.GetString("UK014"), "F4" }, { "", "mnLogOff", Resources.ResourceManager.GetString("UK015"), "Shift+F4" }, { "I", "mnbatchSend", Resources.ResourceManager.GetString("UK016"), "" }, { "M", "mnMailSendRecv", Resources.ResourceManager.GetString("UK017"), "F11" }, 
            { "M", "mnMailRecv", Resources.ResourceManager.GetString("UK018"), "Shift+F12" }, { "I", "mnREP1", Resources.ResourceManager.GetString("UK019"), "" }, { "I", "mnTTM", Resources.ResourceManager.GetString("UK021"), "" }, { "", "mnDataViewRestore", Resources.ResourceManager.GetString("UK023"), "" }, { "", "mnDataViewRepair", Resources.ResourceManager.GetString("UK024"), "" }, { "", "mnDataViewImport", Resources.ResourceManager.GetString("UK025"), "" }, { "", "mnDataViewExport", Resources.ResourceManager.GetString("UK026"), "" }, { "", "mnJobErrorHelp", Resources.ResourceManager.GetString("UK027"), "F1" }, { "", "mnPackageMessage", Resources.ResourceManager.GetString("UK028"), "" }, { "", "mnSupport", Resources.ResourceManager.GetString("UK029"), "" }, { "", "mnJobOpen", Resources.ResourceManager.GetString("UK030"), "" }, { "", "mnSendedSearch", Resources.ResourceManager.GetString("UK031"), "" }, { "", "mnReceivedSearch", Resources.ResourceManager.GetString("UK032"), "" }, { "", "mnVersionUp", Resources.ResourceManager.GetString("UK033"), "" }
         };
        private string[,] keyListS = new string[,] { { "", "mnJobNew", Resources.ResourceManager.GetString("UK056"), "F2" }, { "", "mnRedo", Resources.ResourceManager.GetString("UK057"), "F9" }, { "", "mnFileSaveAs", Resources.ResourceManager.GetString("UK059"), "Ctrl+S" }, { "", "mnFileSend", Resources.ResourceManager.GetString("UK060"), "" }, { "", "mnPrintJobData", Resources.ResourceManager.GetString("UK061"), "Ctrl+P" }, { "", "mnPrintJobDataPreview", Resources.ResourceManager.GetString("UK062"), "" }, { "", "mnHardCopy", Resources.ResourceManager.GetString("UK063"), "Ctrl+H" }, { "", "mnHardCopyPreview", Resources.ResourceManager.GetString("UK064"), "" }, { "", "mnSend", Resources.ResourceManager.GetString("UK065"), "F12" }, { "", "mnSystem", Resources.ResourceManager.GetString("UK022"), "" } };

        private void CreateJobKeyList(string[,] keylist, bool isreset)
        {
            SystemEnvironment environment = SystemEnvironment.CreateInstance();
            for (int i = 0; i < keylist.GetLength(0); i++)
            {
                bool flag = false;
                string str = keylist[i, 0];
                if (str == null)
                {
                    goto Label_007E;
                }
                if (!(str == "B"))
                {
                    if (str == "I")
                    {
                        goto Label_005A;
                    }
                    if (str == "M")
                    {
                        goto Label_006C;
                    }
                    goto Label_007E;
                }
                if (environment.TerminalInfo.UserKind == 2)
                {
                    flag = true;
                }
                goto Label_0080;
            Label_005A:
                if (environment.TerminalInfo.Protocol != ProtocolType.Mail)
                {
                    flag = true;
                }
                goto Label_0080;
            Label_006C:
                if (environment.TerminalInfo.Protocol == ProtocolType.Mail)
                {
                    flag = true;
                }
                goto Label_0080;
            Label_007E:
                flag = true;
            Label_0080:
                if (flag)
                {
                    KeyClass item = new KeyClass {
                        Name = keylist[i, 1],
                        NameKJ = keylist[i, 2]
                    };
                    if (isreset)
                    {
                        item.Sckey = "";
                    }
                    else
                    {
                        item.Sckey = keylist[i, 3];
                    }
                    this.KeyInfoList.KeyList.Add(item);
                }
            }
        }

        public void GetJobKeyList(int type, bool isreset)
        {
            SystemEnvironment.CreateInstance();
            switch (type)
            {
                case 1:
                    this.CreateJobKeyList(this.keyListS, isreset);
                    return;

                case 2:
                    this.CreateJobKeyList(this.keyListR, isreset);
                    return;

                case 3:
                    this.CreateJobKeyList(this.keyListG, isreset);
                    return;
            }
        }

        public enum UserKeyType
        {
            Jkey,
            SKey,
            RKey,
            GKey
        }

        private enum UserKind
        {
            Bank = 2,
            Center = 3,
            Minkan = 1
        }
    }
}

