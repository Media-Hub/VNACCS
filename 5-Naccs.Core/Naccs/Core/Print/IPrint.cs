namespace Naccs.Core.Print
{
    using Naccs.Common.JobConfig;
    using Naccs.Core.Classes;
    using System;
    using System.Windows.Forms;

    public interface IPrint
    {
        event OnPrintEndHandler OnPrintEnd;

        event OnPrintErrorHandler OnPrintError;

        void DataviewPrint(object Sender, IData Data);
        void DataviewPrint(object Sender, IData Data, bool PreviewFlg);
        void HardCopyPrint(object Sender);
        void HardCopyPrinting(object Sender, bool PreviewFlg);
        void HardCopyPrintPreview(object Sender);
        void Print(object Sender, string Key, IData Data, int PrintFlg);
        DialogResult PrintErrMsg(object NaccsException);
        void Printing(object Sender, string Key, IData Data, int PrintFlg, bool PreviewFlg);
        void PrintPreview(object Sender, string Key, IData Data);

        AbstractJobConfig JobConfig { set; }
    }
}

