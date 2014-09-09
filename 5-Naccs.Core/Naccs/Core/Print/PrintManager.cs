namespace Naccs.Core.Print
{
    using Naccs.Common;
    using Naccs.Common.Function;
    using Naccs.Common.JobConfig;
    using Naccs.Common.Print;
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using Naccs.Core.Settings;
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;

    public class PrintManager : IPrint
    {
        private const int KByte = 0x400;
        private int mBorderByte;
        private bool mCombi;
        private ArrayList mCustomList;
        private AbstractJobConfig mJobConfig;
        private bool mManualFlg;
        private object mSender;
        private Queue PrintQ;
        private bool ThreadPrintingFlg;

        public event OnPrintEndHandler OnPrintEnd;

        public event OnPrintErrorHandler OnPrintError;

        public PrintManager()
        {
            this.mBorderByte = 0x100000;
            this.PrintQ = new Queue();
            this.GetCustomTargetList();
        }

        public PrintManager(bool SettingRead)
        {
            this.mBorderByte = 0x100000;
            if (SettingRead)
            {
                this.SettingsRead();
            }
            this.PrintQ = new Queue();
            this.GetCustomTargetList();
        }

        private NaccsException ConfigException(string msg, int errcode)
        {
            int num;
            switch (errcode)
            {
                case -10:
                    msg = msg + Resources.ResourceManager.GetString("CORE97");
                    break;

                case -7:
                    msg = msg + Resources.ResourceManager.GetString("CORE96");
                    break;

                case -6:
                    msg = msg + Resources.ResourceManager.GetString("CORE95");
                    break;

                case -5:
                    msg = msg + Resources.ResourceManager.GetString("CORE94");
                    break;

                case -4:
                    msg = Resources.ResourceManager.GetString("CORE93") + msg;
                    break;

                case -3:
                    msg = string.Format(Resources.ResourceManager.GetString("CORE92"), msg);
                    break;

                case -2:
                    msg = msg + Resources.ResourceManager.GetString("CORE91");
                    break;

                case -1:
                    msg = msg + Resources.ResourceManager.GetString("CORE90");
                    break;

                default:
                    msg = msg + Resources.ResourceManager.GetString("CORE98");
                    break;
            }
            msg = msg + "(" + errcode.ToString() + ")";
            if (errcode > -4)
            {
                num = 0x25c;
            }
            else
            {
                num = 0x25b;
            }
            return new NaccsException(Naccs.Common.MessageKind.Error, num, msg);
        }

        public void DataviewPrint(object Sender, IData Data)
        {
            this.DataviewPrint(Sender, Data, false);
        }

        public void DataviewPrint(object Sender, IData Data, bool PreviewFlg)
        {
            if (!this.mManualFlg)
            {
                this.mManualFlg = true;
                this.mSender = Sender;
                this.SettingsRead();
                try
                {
                    PrintThread thread = new PrintThread();
                    thread.OnThPrintEnd += new OnThPrintEndHandler(this.PrintManager_OnPrintEnd);
                    thread.OnThPrintError += new OnThPrintErrorHandler(this.PrintManager_OnPrintError);
                    thread.TmThreadPrint(PrintStatus.DataviewPrint, Sender, "", 0, null, Data, PreviewFlg);
                }
                catch (NaccsException exception)
                {
                    this.mManualFlg = false;
                    if (this.OnPrintError != null)
                    {
                        this.OnPrintError(exception, "", 0);
                    }
                    else
                    {
                        this.PrintErrMsg(exception);
                    }
                }
                catch (Exception exception2)
                {
                    this.mManualFlg = false;
                    NaccsException sender = new NaccsException(Naccs.Common.MessageKind.Error, 0x261, MessageDialog.CreateExceptionMessage(exception2));
                    if (this.OnPrintError != null)
                    {
                        this.OnPrintError(sender, "", 0);
                    }
                    else
                    {
                        this.PrintErrMsg(sender);
                    }
                }
            }
        }

        private void GetCustomTargetList()
        {
            char[] anyOf = new char[] { '\'', '#', '/' };
            this.mCustomList = new ArrayList();
            PathInfo info = PathInfo.CreateInstance();
            if (File.Exists(info.Custom_List))
            {
                using (StreamReader reader = new StreamReader(info.Custom_List, Encoding.GetEncoding("UTF-8")))
                {
                    try
                    {
                        while (reader.Peek() > -1)
                        {
                            string str = reader.ReadLine().Trim();
                            if ((str.Length > 0) && (str.Substring(0, 1).IndexOfAny(anyOf) < 0))
                            {
                                this.mCustomList.Add(str);
                            }
                        }
                    }
                    finally
                    {
                        if (reader != null)
                        {
                            reader.Close();
                        }
                    }
                }
            }
        }

        private string GetCustomTempPath(IData data, AbstractJobConfig config)
        {
            string customPath = PathInfo.CreateInstance().CustomPath;
            if ((config == null) || !this.isHitCustom(config))
            {
                return "";
            }
            if (data.OutCode.Trim().Length > 0)
            {
                customPath = customPath.Trim(new char[] { '\\' }) + @"\Recv\";
                if (this.mCombi)
                {
                    customPath = customPath + data.SubCode.Substring(0, 6) + @"\" + data.SubCode.Substring(6, 1) + @"\";
                }
                else
                {
                    customPath = customPath + data.OutCode.Substring(0, 6) + @"\" + data.OutCode.Substring(6, 1) + @"\";
                }
                if (!Directory.Exists(customPath))
                {
                    return "";
                }
            }
            else if (data.JobCode.Trim().Length > 0)
            {
                customPath = customPath.Trim(new char[] { '\\' }) + @"\Send\";
                if (this.mCombi)
                {
                    customPath = customPath + data.SubCode.Replace(".", @"\") + @"\";
                }
                else
                {
                    customPath = customPath + data.JobCode.Trim() + @"\";
                    if (data.DispCode.Trim().Length > 0)
                    {
                        customPath = customPath + data.DispCode.Trim() + @"\";
                    }
                }
                if (!Directory.Exists(customPath))
                {
                    return "";
                }
                string s = "0";
                foreach (string str3 in Directory.GetDirectories(customPath))
                {
                    try
                    {
                        string str4 = str3.Substring(str3.LastIndexOf('\\')).Trim(new char[] { '\\' });
                        if (int.Parse(str4) > int.Parse(s))
                        {
                            s = str4;
                        }
                    }
                    catch
                    {
                    }
                }
                customPath = customPath + s + @"\";
                if (!Directory.Exists(customPath))
                {
                    return "";
                }
            }
            if (Directory.GetDirectories(customPath).Length == 0)
            {
                return "";
            }
            return customPath;
        }

        private string GetDefaultPattern(IData data, string customTempPath)
        {
            string str = data.Header.Subject.Substring(60);
            string str2 = "";
            if (Directory.Exists(customTempPath))
            {
                foreach (string str3 in Directory.GetDirectories(customTempPath))
                {
                    string str4 = str3.Substring(str3.LastIndexOf('\\')).Trim(new char[] { '\\' });
                    if (str4.StartsWith("0000", true, null))
                    {
                        str2 = str4;
                    }
                    if (str4.StartsWith(str, true, null))
                    {
                        return str4;
                    }
                }
            }
            return str2;
        }

        private void GetJobConfig(IData data, out AbstractJobConfig config)
        {
            int errcode = 0;
            string msg = "";
            char[] anyOf = new char[] { 'P', 'A', 'C', 'M' };
            this.mCombi = false;
            if ((data.Header.DataType.Trim().Length > 0) && (data.Header.DataType.IndexOfAny(anyOf) < 0))
            {
                config = null;
            }
            else
            {
                if (this.mJobConfig == null)
                {
                    if (data.OutCode.Trim().Length > 0)
                    {
                        msg = "Output Information Code[" + data.OutCode.Trim() + "]";
                        errcode = JobConfigFactory.createJobConfig(data.OutCode, out config);
                    }
                    else if (data.JobCode.Trim().Length > 0)
                    {
                        DateTime now = DateTime.Now;
                        if (data.DispCode.Trim().Length == 0)
                        {
                            msg = "Service Code[" + data.JobCode.Trim() + "]";
                            errcode = JobConfigFactory.createJobConfig(data.JobCode.Trim(), now, out config);
                        }
                        else
                        {
                            msg = "Service Code[" + data.JobCode.Trim() + "." + data.DispCode.Trim() + "]";
                            errcode = JobConfigFactory.createJobConfig(data.JobCode.Trim(), data.DispCode.Trim(), now, out config);
                        }
                    }
                    else
                    {
                        config = null;
                        throw new NaccsException(Naccs.Common.MessageKind.Error, 0x25c, Resources.ResourceManager.GetString("CORE77"));
                    }
                    if (errcode != 0)
                    {
                        throw this.ConfigException(msg, errcode);
                    }
                }
                else
                {
                    config = this.mJobConfig;
                    this.mJobConfig = null;
                }
                if (config.jobStyle == JobStyle.combi)
                {
                    data.Style = JobStyle.combi;
                    data.JobIndex = 1;
                    if (data.OutCode.Trim().Length > 0)
                    {
                        msg = "Output Information Code[" + data.SubCode.Trim() + "]";
                        errcode = JobConfigFactory.createJobConfig(data.SubCode, out config);
                    }
                    else
                    {
                        try
                        {
                            config = ((CombiConfig) config).configs[1].config;
                        }
                        catch
                        {
                            config = null;
                            throw new NaccsException(Naccs.Common.MessageKind.Error, 0x25b, Resources.ResourceManager.GetString("CORE78"));
                        }
                    }
                    if (errcode != 0)
                    {
                        throw this.ConfigException(msg, errcode);
                    }
                    this.mCombi = true;
                }
            }
        }

        public void HardCopyPrint(object Sender)
        {
            this.HardCopyPrinting(Sender, false);
        }

        public void HardCopyPrinting(object Sender, bool PreviewFlg)
        {
            try
            {
                this.SettingsRead();
                HrdCopy copy = new HrdCopy();
                Form form = (Form) Sender;
                form.TopMost = true;
                form.Refresh();
                Bitmap bitmapimg = copy.Capture(new Rectangle(form.Location, form.Size));
                form.TopMost = false;
                PrintSetups setups = PrintSetups.CreateInstance();
                PrinterSettings settings = this.PrinterSetting(setups.DefaultInfo.Printer, setups.DefaultInfo.BinNo);
                if (settings == null)
                {
                    throw new NaccsException(Naccs.Common.MessageKind.Error, 0x25f, Resources.ResourceManager.GetString("CORE76"));
                }
                copy.PrinterSettings = settings;
                copy.Print(bitmapimg, Sender, PreviewFlg);
            }
            catch (NaccsException exception)
            {
                this.PrintErrMsg(exception);
            }
            catch (Exception exception2)
            {
                NaccsException naccsException = new NaccsException(Naccs.Common.MessageKind.Error, 0x261, MessageDialog.CreateExceptionMessage(exception2));
                this.PrintErrMsg(naccsException);
            }
        }

        public void HardCopyPrintPreview(object Sender)
        {
            this.HardCopyPrinting(Sender, true);
        }

        private bool IsAutomaticPrint(IData data)
        {
            PrintSetups setups = PrintSetups.CreateInstance();
            if (!setups.DefaultInfo.Mode)
            {
                return false;
            }
            if (data.OutCode.Trim().Length == 0)
            {
                return false;
            }
            if (((data.Header.DataType != "P") && (data.Header.DataType != "A")) && ((data.Header.DataType != "C") && (data.Header.DataType != "M")))
            {
                return false;
            }
            foreach (OutCodeClass class2 in setups.Forms.OutCodeList)
            {
                if ((class2.Name.Trim() == data.OutCode.Substring(0, 4)) || (class2.Name.Trim() == data.OutCode.Substring(0, 6)))
                {
                    return class2.Mode;
                }
            }
            if (!(data.Header.DataType == "P") && !(data.Header.DataType == "A"))
            {
                return false;
            }
            return true;
        }

        private bool isHitCustom(AbstractJobConfig config)
        {
            bool flag = false;
            try
            {
                string directory = config.directory;
                string str3 = directory.ToUpper().Trim(new char[] { '\\' }) + @"\" + (directory.Substring(directory.Trim(new char[] { '\\' }).LastIndexOf('\\')).Trim(new char[] { '\\' }) + ".conf").ToUpper();
                for (int i = 0; i < this.mCustomList.Count; i++)
                {
                    string str4 = (string) this.mCustomList[i];
                    string str5 = CommaText.CommaTextToItems(str4)[1].ToUpper();
                    if (str3.IndexOf(str5) > -1)
                    {
                        return true;
                    }
                }
            }
            catch
            {
                return flag;
            }
            return flag;
        }

        public void Print(object Sender, string Key, IData Data, int PrintFlg)
        {
            this.Printing(Sender, Key, Data, PrintFlg, false);
        }

        private void PrintEnd(string Key, int PrintFlag, int Status)
        {
            if (((Control) this.mSender).InvokeRequired)
            {
                PrintEndDelegate method = new PrintEndDelegate(this.PrintEnd);
                ((Control) this.mSender).Invoke(method, new object[] { Key, PrintFlag, Status });
            }
            else
            {
                if (PrintFlag != 0)
                {
                    this.QueueThreadEnd();
                }
                else
                {
                    this.mManualFlg = false;
                }
                this.OnPrintEnd(Key, PrintFlag, Status);
            }
        }

        public DialogResult PrintErrMsg(object NaccsException)
        {
            NaccsException exception = (NaccsException) NaccsException;
            IMessageDialog dialog = new MessageDialog();
            string str = "E";
            switch (exception.Kind)
            {
                case Naccs.Common.MessageKind.Information:
                    str = "I";
                    break;

                case Naccs.Common.MessageKind.Error:
                    str = "E";
                    break;

                case Naccs.Common.MessageKind.Warning:
                    str = "W";
                    break;

                case Naccs.Common.MessageKind.Confirmation:
                    str = "C";
                    break;
            }
            string detail = exception.Detail;
            return dialog.ShowMessage(str + exception.Code, detail);
        }

        private void PrintError(object Sender, string Key, int PrintFlag)
        {
            if (((Control) this.mSender).InvokeRequired)
            {
                PrintErrorDelegate method = new PrintErrorDelegate(this.PrintError);
                ((Control) this.mSender).Invoke(method, new object[] { Sender, Key, PrintFlag });
            }
            else
            {
                if (PrintFlag != 0)
                {
                    this.QueueThreadEnd();
                }
                else
                {
                    this.mManualFlg = false;
                }
                if (this.OnPrintError != null)
                {
                    this.OnPrintError(Sender, Key, PrintFlag);
                }
                else
                {
                    this.PrintErrMsg(Sender);
                }
            }
        }

        private PrinterSettings PrinterSetting(string Printer, int BinNo)
        {
            PrinterSettings settings = new PrinterSettings();
            if (!settings.IsValid)
            {
                return null;
            }
            if (!string.IsNullOrEmpty(Printer) && PrintThread.PrinterChack(Printer))
            {
                settings.PrinterName = Printer;
                foreach (PaperSource source in settings.PaperSources)
                {
                    if (source.RawKind == BinNo)
                    {
                        settings.DefaultPageSettings.PaperSource = source;
                    }
                }
            }
            return settings;
        }

        public void Printing(object Sender, string Key, IData Data, int PrintFlg, bool PreviewFlg)
        {
            if (PrintFlg == 0)
            {
                if (this.mManualFlg)
                {
                    return;
                }
                this.mManualFlg = true;
            }
            this.mSender = Sender;
            this.SettingsRead();
            if ((PrintFlg != 2) || this.IsAutomaticPrint(Data))
            {
                int length = Data.Header.Length;
                if (length <= 400)
                {
                    length = Data.CalcLength();
                }
                if (PrintFlg == 0)
                {
                    if (ConfigFiles.printerSettings.DefaultInfo.SizeWarning && (length > this.mBorderByte))
                    {
                        string str;
                        if (!PreviewFlg)
                        {
                            str = "W601";
                        }
                        else
                        {
                            str = "W602";
                        }
                        IMessageDialog dialog = new MessageDialog();
                        if (dialog.ShowMessage(str, Math.Truncate((double) (((double) this.mBorderByte) / 1024.0)).ToString(), "") != DialogResult.Yes)
                        {
                            this.mManualFlg = false;
                            return;
                        }
                    }
                    try
                    {
                        AbstractJobConfig config;
                        this.GetJobConfig(Data, out config);
                        if (this.mCombi)
                        {
                            Data.Style = JobStyle.combi;
                            Data.JobIndex = 1;
                        }
                        PrintThread thread = new PrintThread();
                        thread.OnThPrintEnd += new OnThPrintEndHandler(this.PrintManager_OnPrintEnd);
                        thread.OnThPrintError += new OnThPrintErrorHandler(this.PrintManager_OnPrintError);
                        thread.TmThreadPrint(PrintStatus.Print, Sender, Key, PrintFlg, config, Data, PreviewFlg);
                    }
                    catch (NaccsException exception)
                    {
                        this.mManualFlg = false;
                        if (this.OnPrintError != null)
                        {
                            this.OnPrintError(exception, Key, PrintFlg);
                        }
                        else
                        {
                            this.PrintErrMsg(exception);
                        }
                    }
                    catch (Exception exception2)
                    {
                        this.mManualFlg = false;
                        NaccsException sender = new NaccsException(Naccs.Common.MessageKind.Error, 0x261, exception2.Message);
                        if (this.OnPrintError != null)
                        {
                            this.OnPrintError(sender, Key, PrintFlg);
                        }
                        else
                        {
                            this.PrintErrMsg(sender);
                        }
                    }
                }
                else if (!ConfigFiles.printerSettings.DefaultInfo.SizeWarning || (length <= this.mBorderByte))
                {
                    PQRecAttr rec = new PQRecAttr {
                        Key = Key,
                        Data = (NaccsData) Data,
                        PrintFlg = PrintFlg
                    };
                    this.QueueThreadStart(rec);
                }
            }
        }

        private void PrintManager_OnPrintEnd(string Key, int PrintFlag, int Status)
        {
            if (this.OnPrintEnd != null)
            {
                this.PrintEnd(Key, PrintFlag, Status);
            }
        }

        private void PrintManager_OnPrintError(object Sender, string Key, int PrintFlag)
        {
            this.PrintError(Sender, Key, PrintFlag);
        }

        public void PrintPreview(object Sender, string Key, IData Data)
        {
            this.Printing(Sender, Key, Data, 0, true);
        }

        private void QueueThreadEnd()
        {
            if (this.PrintQ.Count == 0)
            {
                this.ThreadPrintingFlg = false;
            }
            else
            {
                PQRecAttr attr = (PQRecAttr) this.PrintQ.Dequeue();
                this.ThreadPrintingFlg = true;
                try
                {
                    AbstractJobConfig config;
                    this.GetJobConfig(attr.Data, out config);
                    PrintThread thread = new PrintThread();
                    thread.OnThPrintEnd += new OnThPrintEndHandler(this.PrintManager_OnPrintEnd);
                    thread.OnThPrintError += new OnThPrintErrorHandler(this.PrintManager_OnPrintError);
                    string customTempPath = this.GetCustomTempPath(attr.Data, config);
                    if (customTempPath.Length > 0)
                    {
                        string defaultPattern = this.GetDefaultPattern(attr.Data, customTempPath);
                        if (defaultPattern.Length > 0)
                        {
                            thread.CustomTemplatePath = customTempPath.Trim(new char[] { '\\' }) + @"\" + defaultPattern;
                        }
                    }
                    thread.TmThreadPrint(PrintStatus.Print, this.mSender, attr.Key, attr.PrintFlg, config, attr.Data, false);
                }
                catch (NaccsException exception)
                {
                    if (this.OnPrintError != null)
                    {
                        this.PrintError(exception, attr.Key, attr.PrintFlg);
                    }
                    else
                    {
                        this.PrintErrMsg(exception);
                    }
                }
                catch (Exception exception2)
                {
                    NaccsException sender = new NaccsException(Naccs.Common.MessageKind.Error, 0x261, exception2.Message);
                    if (this.OnPrintError != null)
                    {
                        this.PrintError(sender, attr.Key, attr.PrintFlg);
                    }
                    else
                    {
                        this.PrintErrMsg(sender);
                    }
                }
            }
        }

        private void QueueThreadStart(PQRecAttr rec)
        {
            if (this.ThreadPrintingFlg)
            {
                this.PrintQ.Enqueue(rec);
            }
            else
            {
                this.ThreadPrintingFlg = true;
                try
                {
                    AbstractJobConfig config;
                    this.GetJobConfig(rec.Data, out config);
                    if (this.mCombi)
                    {
                        rec.Data.Style = JobStyle.combi;
                        rec.Data.JobIndex = 1;
                    }
                    PrintThread thread = new PrintThread();
                    thread.OnThPrintEnd += new OnThPrintEndHandler(this.PrintManager_OnPrintEnd);
                    thread.OnThPrintError += new OnThPrintErrorHandler(this.PrintManager_OnPrintError);
                    string customTempPath = this.GetCustomTempPath(rec.Data, config);
                    if (customTempPath.Length > 0)
                    {
                        string defaultPattern = this.GetDefaultPattern(rec.Data, customTempPath);
                        if (defaultPattern.Length > 0)
                        {
                            thread.CustomTemplatePath = customTempPath.Trim(new char[] { '\\' }) + @"\" + defaultPattern;
                        }
                    }
                    thread.TmThreadPrint(PrintStatus.Print, this.mSender, rec.Key, rec.PrintFlg, config, rec.Data, false);
                }
                catch (NaccsException exception)
                {
                    if (this.OnPrintError != null)
                    {
                        this.PrintError(exception, rec.Key, rec.PrintFlg);
                    }
                    else
                    {
                        this.PrintErrMsg(exception);
                    }
                }
                catch (Exception exception2)
                {
                    NaccsException sender = new NaccsException(Naccs.Common.MessageKind.Error, 0x261, exception2.Message);
                    if (this.OnPrintError != null)
                    {
                        this.PrintError(sender, rec.Key, rec.PrintFlg);
                    }
                    else
                    {
                        this.PrintErrMsg(sender);
                    }
                }
            }
        }

        private void SettingsRead()
        {
            if (ConfigFiles.printerSettings == null)
            {
                ConfigFiles.printerSettings = PrintSetups.CreateInstance();
            }
        }

        public int BorderByte
        {
            set
            {
                this.mBorderByte = value;
            }
        }

        public AbstractJobConfig JobConfig
        {
            set
            {
                this.mJobConfig = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PQRecAttr
        {
            public NaccsData Data;
            public string Key;
            public int PrintFlg;
        }

        private delegate void PrintEndDelegate(string Key, int PrintFlag, int Status);

        private delegate void PrintErrorDelegate(object Sender, string Key, int PrintFlag);
    }
}

