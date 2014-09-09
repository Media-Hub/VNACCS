namespace Naccs.Common.Function
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class Logging
    {
        private static Logging log = null;

        public static  event WriteDelegate OnWrite;

        public static Logging getInstace()
        {
            if (log == null)
            {
                log = new Logging();
            }
            return log;
        }

        public static void Write(object sender, string title, string description)
        {
            getInstace().writelog(sender, title, description);
        }

        private void writelog(object sender, string title, string description)
        {
            if (OnWrite != null)
            {
                LogEventArgs e = new LogEventArgs(sender, title, description);
                OnWrite(e);
            }
        }

        public delegate void WriteDelegate(LogEventArgs e);
    }
}

