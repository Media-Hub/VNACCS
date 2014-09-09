namespace Naccs.Common.JobConfig
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RecordAttribute
    {
        public int generation;
        public string jobcode;
        public string dispcode;
        public string titile1;
        public string titile2;
        public bool attach;
        public DateTime validDate;
        public bool barcode;
        public ConfirmType confirm;
        public int signflg;
    }
}

