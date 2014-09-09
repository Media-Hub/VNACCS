namespace Naccs.Common.JobConfig
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct SubConfig
    {
        public int order;
        public string tabname;
        public string code;
        public NormalConfig config;
    }
}

