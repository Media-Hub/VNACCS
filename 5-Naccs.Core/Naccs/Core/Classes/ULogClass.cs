namespace Naccs.Core.Classes
{
    using Naccs.Core.Settings;
    using System;
    using System.Diagnostics;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ULogClass
    {
        private const int CnPasswordLen = 8;
        private const int CnPasswordPos = 0x26;

        [DllImport("user32.dll")]
        private static extern int GetGuiResources(IntPtr hProcess, int uiFlags);
        public static void LogWrite(string s)
        {
            LogWrite(s, null, false);
        }

        public static void LogWrite(string title, string data, bool mask)
        {
            SystemEnvironment environment = SystemEnvironment.CreateInstance();
            PathInfo info = PathInfo.CreateInstance();
            UserEnvironment environment2 = UserEnvironment.CreateInstance();
            try
            {
                if (!Directory.Exists(info.LogPath))
                {
                    Directory.CreateDirectory(info.LogPath);
                }
                FileStream stream = null;
                try
                {
                    if (File.Exists(info.LogPath + environment.LogFileLength.NACCS_ComLog1))
                    {
                        FileInfo info2 = new FileInfo(info.LogPath + environment.LogFileLength.NACCS_ComLog1);
                        long length = info2.Length;
                        DateTime lastWriteTime = info2.LastWriteTime;
                        if (length > (environment2.LogFileLength.ComLog * 0x400))
                        {
                            if (File.Exists(info.LogPath + environment.LogFileLength.NACCS_ComLog2))
                            {
                                info2 = new FileInfo(info.LogPath + environment.LogFileLength.NACCS_ComLog2);
                                if (info2.Length > (environment2.LogFileLength.ComLog * 0x400))
                                {
                                    if (lastWriteTime > info2.LastWriteTime)
                                    {
                                        stream = new FileStream(info.LogPath + environment.LogFileLength.NACCS_ComLog2, FileMode.Create);
                                    }
                                    else
                                    {
                                        stream = new FileStream(info.LogPath + environment.LogFileLength.NACCS_ComLog1, FileMode.Create);
                                    }
                                }
                                else
                                {
                                    stream = new FileStream(info.LogPath + environment.LogFileLength.NACCS_ComLog2, FileMode.Append);
                                }
                            }
                            else
                            {
                                stream = new FileStream(info.LogPath + environment.LogFileLength.NACCS_ComLog2, FileMode.Create);
                            }
                        }
                        else
                        {
                            stream = new FileStream(info.LogPath + environment.LogFileLength.NACCS_ComLog1, FileMode.Append);
                        }
                    }
                    else
                    {
                        stream = new FileStream(info.LogPath + environment.LogFileLength.NACCS_ComLog1, FileMode.Create);
                    }
                    StreamWriter writer = new StreamWriter(stream);
                    try
                    {
                        writer.WriteLine(DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo) + " " + title);
                        if (data != null)
                        {
                            string str = data;
                            if (mask && (str.Length > 0x2e))
                            {
                                char[] destination = str.ToCharArray();
                                string str2 = "";
                                str2.PadRight(8, '*').CopyTo(0, destination, 0x25, 8);
                                str = new string(destination);
                            }
                            writer.WriteLine(str);
                        }
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
                finally
                {
                    stream.Close();
                }
            }
            catch
            {
            }
        }

        public static void WriteProcessInfo(string message)
        {
            StringBuilder builder = new StringBuilder();
            Process currentProcess = Process.GetCurrentProcess();
            if (!string.IsNullOrEmpty(message))
            {
                builder.Append(message);
                builder.Append(" ");
            }
            builder.Append("Manage memory:");
            builder.Append(string.Format("{0, 19:d}", GC.GetTotalMemory(false)));
            builder.Append(" byte, ");
            builder.Append("Physical memory of the process:");
            builder.Append(string.Format("{0, 19:d}", currentProcess.WorkingSet64));
            builder.Append(" byte, ");
            builder.Append("Virtual memory of the process:");
            builder.Append(string.Format("{0, 19:d}", currentProcess.VirtualMemorySize64));
            builder.Append(" byte, ");
            builder.Append("Number of GDI objects:");
            builder.Append(string.Format("{0, 5:d}", GetGuiResources(currentProcess.Handle, 0)));
            builder.Append(" , ");
            builder.Append("Number of USER objects:");
            builder.Append(string.Format("{0, 5:d}", GetGuiResources(currentProcess.Handle, 1)));
            builder.Append(" ");
            LogWrite("Info", builder.ToString(), false);
        }
    }
}

