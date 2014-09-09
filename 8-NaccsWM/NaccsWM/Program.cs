namespace NaccsWM
{
    using Naccs.Core.Classes;
    using System;
    using System.Globalization;
    using System.Threading;
    using System.Windows.Forms;

    internal static class Program
    {
        private static string appName = "PTSW";
        private const int MUTEX_WAIT = 300;
        private static string verName = "PTLauncher";

        [STAThread]
        private static void Main(string[] args)
        {
            Thread.CurrentThread.CurrentCulture = new CultureInfo("vi-VN", false);
            Thread.CurrentThread.CurrentUICulture = Thread.CurrentThread.CurrentCulture;
            Mutex mutex = null;
            Mutex mutex2 = null;
            try
            {
                bool proxyError = false;
                if (args.Length == 1)
                {
                    proxyError = bool.Parse(args[0]);
                }
                OperatingSystem oSVersion = Environment.OSVersion;
                if ((oSVersion.Platform == PlatformID.Win32NT) && (oSVersion.Version.Major >= 5))
                {
                    mutex = new Mutex(false, @"Global\" + appName);
                    mutex2 = new Mutex(false, @"Global\" + verName);
                }
                else
                {
                    mutex = new Mutex(false, appName);
                    mutex2 = new Mutex(false, verName);
                }
                ProcKeys nONE = ProcKeys.NONE;
                if (!mutex.WaitOne(300, false))
                {
                    nONE = ProcKeys.APP;
                }
                if (!mutex2.WaitOne(300, false))
                {
                    if (nONE == ProcKeys.LAUNCHER)
                    {
                        nONE = ProcKeys.WITH;
                    }
                    else
                    {
                        nONE = ProcKeys.LAUNCHER;
                    }
                }
                if ((nONE != ProcKeys.LAUNCHER) && (nONE != ProcKeys.WITH))
                {
                    mutex2.ReleaseMutex();
                    mutex2.Close();
                }
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                NaccsWMMain mainForm = null;
                if (proxyError)
                {
                    mainForm = new NaccsWMMain(proxyError);
                }
                else
                {
                    mainForm = new NaccsWMMain();
                }
                if (nONE == ProcKeys.NONE)
                {
                    Application.Run(mainForm);
                    mutex.ReleaseMutex();
                }
                else
                {
                    MessageDialog dialog = new MessageDialog();
                    dialog.ShowMessage("E001", "", "");
                    dialog.Close();
                    dialog.Dispose();
                }
            }
            finally
            {
                if (mutex != null)
                {
                    mutex.Close();
                }
                if (mutex2 != null)
                {
                    mutex2.Close();
                }
            }
        }

        private enum ProcKeys
        {
            APP,
            LAUNCHER,
            WITH,
            NONE
        }
    }
}

