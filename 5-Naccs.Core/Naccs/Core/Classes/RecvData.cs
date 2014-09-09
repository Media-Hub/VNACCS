namespace Naccs.Core.Classes
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct RecvData
    {
        public IData ResultData;
        public IData OtherData;
        public IData MData;
    }
}

