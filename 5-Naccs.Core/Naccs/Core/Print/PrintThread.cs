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
    using System.Collections.Generic;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Timers;
    using System.Windows.Forms;

    internal class PrintThread
    {
        private string mCustomTemplatePath = "";
        private IData mData;
        private AbstractJobConfig mJobConfig;
        private string mKey = "";
        private bool mPreviewFlg = true;
        private int mPrintFlg = 0;
        private PrintStatus mPrintMode;
        private object mSender = null;

        public event OnThPrintEndHandler OnThPrintEnd;

        public event OnThPrintErrorHandler OnThPrintError;

        public static bool PrinterChack(string PrinterName)
        {
            try
            {
                return PrinterValid(PrinterName);
            }
            catch
            {
                return false;
            }
        }

        private void PrinterSetting(Naccs.Common.Print.Print prt)
        {
            string name = "";
            int binNo = 0;
            int count = 1;
            string str2 = "";
            int t = 0;
            int l = 0;
            string size = "";
            string orientation = "";
            PrintSetups setups = PrintSetups.CreateInstance();
            if ((this.mData.OutCode.Trim().Length > 0) && (this.mJobConfig != null))
            {
                foreach (OutCodeClass class2 in setups.Forms.OutCodeList)
                {
                    if ((class2.Name.Trim() == this.mData.OutCode.Substring(0, 6)) || (class2.Name.Trim() == this.mData.OutCode.Substring(0, 4)))
                    {
                        name = class2.Printer;
                        binNo = class2.BinNo;
                        count = class2.Count;
                        break;
                    }
                }
            }
            if (name.Length == 0)
            {
                if (string.IsNullOrEmpty(setups.DefaultInfo.Printer))
                {
                    PrinterSettings settings = new PrinterSettings();
                    if (!settings.IsValid)
                    {
                        throw new NaccsException(Naccs.Common.MessageKind.Error, 0x25f, Resources.ResourceManager.GetString("CORE76"));
                    }
                    name = settings.PrinterName;
                    binNo = settings.DefaultPageSettings.PaperSource.RawKind;
                }
                else
                {
                    name = setups.DefaultInfo.Printer;
                    binNo = setups.DefaultInfo.BinNo;
                }
            }
            if (!PrinterValid(name))
            {
                if (this.mPrintFlg != 0)
                {
                    throw new NaccsException(Naccs.Common.MessageKind.Error, 0x25a, Resources.ResourceManager.GetString("CORE79"));
                }
                if (PrinterValid(setups.DefaultInfo.Printer))
                {
                    name = setups.DefaultInfo.Printer;
                    binNo = setups.DefaultInfo.BinNo;
                }
                else
                {
                    PrinterSettings settings2 = new PrinterSettings();
                    name = settings2.PrinterName;
                    binNo = settings2.DefaultPageSettings.PaperSource.RawKind;
                }
            }
            foreach (PrinterInfoClass class3 in setups.PrinterInfoList)
            {
                if (class3.Name.Trim() == name.Trim())
                {
                    if (class3.DoublePrint)
                    {
                        str2 = "Vertical";
                    }
                    else
                    {
                        str2 = "Simplex";
                    }
                    foreach (BinClass class4 in class3.BinList)
                    {
                        if (class4.No == binNo)
                        {
                            t = class4.T;
                            l = class4.L;
                            size = class4.Size;
                            orientation = class4.Orientation;
                            break;
                        }
                    }
                    break;
                }
            }
            prt.PrinterName = name;
            prt.BinNo = binNo;
            prt.Copies = count;
            prt.DoublePrint = str2;
            PathInfo info = PathInfo.CreateInstance();
            prt.PrinterSettingsPath = info.PrinterSetup;
            prt.Top = t;
            prt.Left = l;
            prt.PaperSize = size;
            prt.Orientation = orientation;
        }

        private static bool PrinterValid(string name)
        {
            PrinterSettings settings = new PrinterSettings();
            if (!settings.IsValid)
            {
                throw new NaccsException(Naccs.Common.MessageKind.Error, 0x25f, Resources.ResourceManager.GetString("CORE76"));
            }
            try
            {
                foreach (string str in PrinterSettings.InstalledPrinters)
                {
                    if (str == name)
                    {
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                return false;
            }
        }

        private void PRT_OnPrintEnd(string Key, int PrintFlag, int Status)
        {
            if (this.OnThPrintEnd != null)
            {
                this.OnThPrintEnd(Key, PrintFlag, Status);
            }
        }

        private void ResultReceived()
        {
            if (((this.mPrintMode != PrintStatus.Print) || ((this.mPrintMode == PrintStatus.Print) && (this.mPrintFlg == 0))) && ((Control) this.mSender).InvokeRequired)
            {
                ReceivedDelegate method = new ReceivedDelegate(this.ResultReceived);
                ((Control) this.mSender).Invoke(method, new object[0]);
            }
            else
            {
                try
                {
                    if (this.mPrintMode == PrintStatus.DataviewPrint)
                    {
                        Naccs.Common.Print.Print prt = new Naccs.Common.Print.Print {
                            PrintKey = this.mKey
                        };
                        this.PrinterSetting(prt);
                        prt.OnPrintEnd += new OnPrintEndEventHandler(this.PRT_OnPrintEnd);
                        prt.OwnerSender = this.mSender;
                        prt.printDataView(this.mData.Items, this.mPreviewFlg);
                    }
                    else
                    {
                        Naccs.Common.Print.Print print2 = new Naccs.Common.Print.Print {
                            PrintDlgMode = this.mPrintFlg,
                            PrintKey = this.mKey,
                            TimeStamp = this.mData.TimeStamp
                        };
                        if (this.mData.Header.InputInfo.Trim().Length > 0)
                        {
                            print2.InputInfo = this.mData.Header.InputInfo;
                        }
                        if (this.mData.Header.ServerRecvTime.Trim().Length > 0)
                        {
                            print2.ServerRecvTime = this.mData.Header.ServerRecvTime.Replace(' ', '0');
                        }
                        if (this.mData.Contained == ContainedType.ReceiveOnly)
                        {
                            print2.Contained = 2;
                        }
                        else if (this.mData.Contained == ContainedType.SendOnly)
                        {
                            print2.Contained = 1;
                        }
                        else
                        {
                            print2.Contained = 0;
                        }
                        PathInfo info = PathInfo.CreateInstance();
                        print2.FontFilePath = info.SystemEnvironmentPath;
                        print2.ImportFontFiles = info.SystemEnvironmentPath + @"\NACCS_OCR.TTF";
                        print2.OcrFontName = "NACCS_OCR";
                        print2.OcrFontSize = 12f;
                        this.PrinterSetting(print2);
                        GStampSettings settings = GStampSettings.CreateInstance();
                        print2.IsWriteGStamp = settings.GStampInfo.GStampOn;
                        List<string> list = new List<string> {
                            settings.GStampInfo.TopItem,
                            settings.GStampInfo.DateItem,
                            settings.GStampInfo.UnderItem1,
                            settings.GStampInfo.UnderItem2,
                            settings.GStampInfo.UnderItem3
                        };
                        print2.GStampInfo = CommaText.ItemsToCommaText(list);
                        print2.GStampDateAlign = settings.GStampInfo.DateAlignment;
                        print2.OnPrintEnd += new OnPrintEndEventHandler(this.PRT_OnPrintEnd);
                        print2.CustomTemplatePath = this.mCustomTemplatePath;
                        SystemEnvironment environment = SystemEnvironment.CreateInstance();
                        print2.StampMarkText = environment.TerminalInfo.PrintStampMark;
                        print2.OwnerSender = this.mSender;
                        if (this.mJobConfig == null)
                        {
                            print2.execTextPrint(this.mData.Items, this.mPreviewFlg);
                        }
                        else if (this.mData.Style == JobStyle.combi)
                        {
                            this.mData.JobIndex = 1;
                            print2.printTemplate(this.mJobConfig, this.mData.SubItems, this.mPreviewFlg);
                        }
                        else
                        {
                            print2.printTemplate(this.mJobConfig, this.mData.Items, this.mPreviewFlg);
                        }
                    }
                }
                catch (NaccsException exception)
                {
                    this.OnThPrintError(exception, this.mKey, this.mPrintFlg);
                }
                catch (Exception exception2)
                {
                    NaccsException sender = new NaccsException(Naccs.Common.MessageKind.Error, 0x261, exception2.Message);
                    this.OnThPrintError(sender, this.mKey, this.mPrintFlg);
                }
            }
        }

        private void ThreadTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer {
                AutoReset = false
            };
            timer.Elapsed += new ElapsedEventHandler(this.TmThreadRun);
            timer.Interval = 1.0;
            timer.Enabled = true;
        }

        public void TmThreadPrint(PrintStatus printMode, object sender, string key, int printFlg, AbstractJobConfig jobConfig, IData data, bool previewFlg)
        {
            this.mPrintMode = printMode;
            this.mSender = sender;
            this.mKey = key;
            this.mPrintFlg = printFlg;
            this.mJobConfig = jobConfig;
            this.mData = data;
            this.mPreviewFlg = previewFlg;
            this.ThreadTimer();
        }

        private void TmThreadRun(object sender, ElapsedEventArgs e)
        {
            System.Timers.Timer timer = (System.Timers.Timer) sender;
            timer.Enabled = false;
            timer.Dispose();
            this.ResultReceived();
        }

        public string CustomTemplatePath
        {
            set
            {
                this.mCustomTemplatePath = value;
            }
        }

        private delegate void ReceivedDelegate();
    }
}

