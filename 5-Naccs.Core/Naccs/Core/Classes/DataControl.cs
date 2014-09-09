namespace Naccs.Core.Classes
{
    using Naccs.Core.Settings;
    using System;

    public class DataControl
    {
        private const string C_Interactive = "SS ";
        private const string C_Mail = "MS ";
        private const string C_Net = "SS ";
        private const string C_Zei = "GS ";

        public static string GetControl()
        {
            SystemEnvironment environment = SystemEnvironment.CreateInstance();
            UserEnvironment environment2 = UserEnvironment.CreateInstance();
            if (environment.TerminalInfo.Debug)
            {
                switch (environment2.DebugFunction.Control)
                {
                    case 1:
                        return "MS ";

                    case 2:
                        return "GS ";

                    case 3:
                        return "SS ";
                }
                return "SS ";
            }
            switch (environment.TerminalInfo.Protocol)
            {
                case ProtocolType.Mail:
                    return "MS ";

                case ProtocolType.netNACCS:
                case ProtocolType.KIOSK:
                    return "SS ";
            }
            return "SS ";
        }
    }
}

