namespace Naccs.Core.Classes
{
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.Windows.Forms;

    public class CustomizeMenu
    {
        private SystemEnvironment mSysEnv = SystemEnvironment.CreateInstance();

        public void Change(MenuStrip parent, string[] menuitems, bool visible, bool enable)
        {
            foreach (string str in menuitems)
            {
                ToolStripItem[] itemArray = parent.Items.Find(str, true);
                if (itemArray.Length > 0)
                {
                    itemArray[0].Visible = visible;
                    itemArray[0].Enabled = enable;
                    if ((itemArray[0].GetType() == typeof(ToolStripMenuItem)) && !visible)
                    {
                        ((ToolStripMenuItem) itemArray[0]).ShortcutKeys = Keys.None;
                        if (((ToolStripMenuItem) itemArray[0]).DropDownItems.Count > 0)
                        {
                            List<string> list = new List<string>();
                            foreach (ToolStripItem item in ((ToolStripMenuItem) itemArray[0]).DropDownItems)
                            {
                                list.Add(item.Name);
                            }
                            this.Change(parent, list.ToArray(), visible, enable);
                            list.Clear();
                        }
                    }
                }
            }
        }

        public void EnableMain(MenuStrip menu, bool enable)
        {
            if (this.mSysEnv.TerminalInfo.UserKind == 4)
            {
                this.Change(menu, Enum.GetNames(typeof(EnableKioskMain)), true, enable);
            }
        }

        public void VisibleJob(MenuStrip menu)
        {
            if (this.mSysEnv.TerminalInfo.UserKind == 4)
            {
                this.Change(menu, Enum.GetNames(typeof(InvisibleKioskJob)), false, false);
            }
        }

        public void VisibleMain(MenuStrip menu)
        {
            if (this.mSysEnv.TerminalInfo.UserKind == 4)
            {
                this.Change(menu, Enum.GetNames(typeof(InvisibleKioskMain)), false, false);
            }
        }

        private enum EnableKioskMain
        {
            mnRedo,
            mnFileOpen,
            mnSeqSend,
            mnSeqLoadDt,
            mnHardCopyPrint,
            mnHardCopyPreview,
            mnQueryFieldClear,
            mnLogoffConf
        }

        private enum InvisibleKioskJob
        {
            mnJobNew,
            mnAppendJobData,
            mnUpdateJobData,
            mnFileSend,
            mnSep2
        }

        private enum InvisibleKioskMain
        {
            mnJobNew,
            mnOpen,
            mnJobOpen,
            mnFileSave,
            mnFileSend,
            mnAddRecord,
            mnFileDataViewAppend,
            mnPrintJobData,
            mnPrintJobDataPreview,
            mnDataViewPrint,
            mnEdit,
            mnVisible,
            mnSend,
            mnMailSendRecv,
            mnMailRecv,
            mnBatchSend,
            mnJSplitter2,
            mnJSplitter4,
            mnOther,
            mnDataViewRestore,
            mnDataViewRepair,
            mnOSplitter2,
            mnDataViewImport,
            mnDataViewExport,
            mnOSplitter3,
            mnACLCustomize,
            mnOSplitter4,
            mnGStamp,
            mnOSplitter5,
            mnCloseOnAppend,
            mnListRestriction,
            mnHSplitter3,
            mnNetworkTest,
            mnHSplitter4,
            mnGuidance
        }
    }
}

