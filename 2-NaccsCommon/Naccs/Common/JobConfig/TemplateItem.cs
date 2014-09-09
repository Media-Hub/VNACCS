namespace Naccs.Common.JobConfig
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TemplateItem
    {
        public int order;
        public string tabname;
        public string filename;
    }
}

