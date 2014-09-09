namespace Naccs.Net
{
    using System;
    using System.Globalization;
    using System.IO;
    using System.Text;

    public class NetTrace
    {
        private bool cnFullDump = true;
        private const int CnPasswordLen = 8;
        private const int CnPasswordPos = 0x26;
        private bool isDebug;
        private bool mPasswordMask = true;
        private string mTraceFile = "";
        private int mTraceSize = 0x3e8;
        private bool mUseTrace;
        public const string TraceFmtCancel = "# {0,-16} : Cancel status[{1}]";
        public const string TraceFmtError = "- {0,-16} : error[{1}]";
        public const string TraceFmtException = "! {0,-16} : {1} \r\n{2}";
        public const string TraceFmtParam = "+ {0,-16} : {1}";
        public const string TraceFmtRequest = "# {0,-16} : status[{1}]";
        public const string TraceFmtStart = "=====< Trace Start >=====";
        private const string TraceFmtTitle = "{0} [{1,-5}] : {2}";
        public const string TraceFmtWebException = "! {0,-16} : {1}({2})\r\n{3}";

        private string DateTimeString()
        {
            return DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss", DateTimeFormatInfo.InvariantInfo);
        }

        private string DumpData(string data)
        {
            string str = null;
            string str2 = null;
            if (this.cnFullDump)
            {
                if (data.Length >= 400)
                {
                    str = data.Substring(0, 400);
                    str2 = data.Substring(400, data.Length - 400);
                }
                else
                {
                    str2 = data;
                }
            }
            else if (data.Length >= 400)
            {
                str = data.Substring(0, 400);
            }
            else
            {
                str2 = data;
            }
            if (this.mPasswordMask && !string.IsNullOrEmpty(str))
            {
                char[] destination = str.ToCharArray();
                string str3 = "";
                str3.PadRight(8, '*').CopyTo(0, destination, 0x25, 8);
                str2 = new string(destination) + str2;
            }
            return str2;
        }

        private StreamWriter OpenTrace()
        {
            bool append = false;
            try
            {
                string directoryName = Path.GetDirectoryName(this.TraceFile);
                if (!Directory.Exists(directoryName))
                {
                    Directory.CreateDirectory(directoryName);
                }
                FileInfo info = new FileInfo(this.TraceFile);
                if (info.Exists)
                {
                    if (info.Length >= (this.TraceSize * 0x3e8))
                    {
                        string path = Path.ChangeExtension(this.TraceFile, ".bak");
                        if (File.Exists(path))
                        {
                            File.Delete(path);
                        }
                        File.Move(this.TraceFile, path);
                    }
                    append = true;
                }
                else
                {
                    append = false;
                }
                return new StreamWriter(this.TraceFile, append, Encoding.GetEncoding("UTF-8"));
            }
            catch
            {
                return null;
            }
        }

        public bool WriteDump(Tracekind kind, string data, bool isSend)
        {
            if (this.UseTrace || this.isDebug)
            {
                try
                {
                    StreamWriter writer = this.OpenTrace();
                    if (writer == null)
                    {
                        return false;
                    }
                    Encoding encoding = writer.Encoding;
                    byte[] bytes = encoding.GetBytes(this.DumpData(data));
                    try
                    {
                        string str;
                        string str2;
                        if (isSend)
                        {
                            str = string.Format("<SEND DATA HEX DUMP({0})>", encoding.EncodingName);
                        }
                        else
                        {
                            str = string.Format("<RECEIVE DATA HEX DUMP({0})>", encoding.EncodingName);
                        }
                        writer.WriteLine("{0} [{1,-5}] : {2}", this.DateTimeString(), kind, str);
                        writer.WriteLine("< DUMP > 0------- 4------- 8------- C-------  0---4---8---C---");
                        int num = 0;
                        int num2 = 0;
                        byte[] buffer2 = new byte[0x10];
                        int count = 0;
                        long num4 = 0L;
                        long num5 = 0L;
                    Label_009E:
                        str2 = string.Format("{0:X08} ", num5);
                        buffer2 = new byte[0x10];
                        count = 0;
                        int index = 0;
                        for (num = 0; num <= 3; num++)
                        {
                            for (num2 = 0; num2 <= 3; num2++)
                            {
                                if (num4 < bytes.Length)
                                {
                                    str2 = str2 + string.Format("{0:X02}", bytes[(int) ((IntPtr) num4)]);
                                    char c = (char) bytes[(int) ((IntPtr) num4)];
                                    index = ((int) num4) % 0x10;
                                    if (char.IsControl(c) && (c < '\x0014'))
                                    {
                                        buffer2[index] = 0x2e;
                                    }
                                    else
                                    {
                                        buffer2[index] = bytes[(int) ((IntPtr) num4)];
                                    }
                                    count++;
                                }
                                else
                                {
                                    str2 = str2 + "  ";
                                }
                                num4 += 1L;
                            }
                            if (num4 >= bytes.Length)
                            {
                                break;
                            }
                            str2 = str2 + " ";
                        }
                        if (num4 < bytes.Length)
                        {
                            writer.WriteLine("{0} {1}", str2, encoding.GetString(buffer2, 0, count));
                            num5 += 1L;
                            goto Label_009E;
                        }
                        int num7 = 0x2d - str2.Length;
                        for (num4 = 0L; num4 < num7; num4 += 1L)
                        {
                            str2 = str2 + " ";
                        }
                        writer.WriteLine("{0} {1}", str2, encoding.GetString(buffer2, 0, count));
                    }
                    finally
                    {
                        writer.WriteLine("");
                        writer.Close();
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public bool WriteString(Tracekind kind, string data)
        {
            if (this.UseTrace || this.isDebug)
            {
                try
                {
                    StreamWriter writer = this.OpenTrace();
                    if (writer == null)
                    {
                        return false;
                    }
                    try
                    {
                        if (kind == Tracekind.START)
                        {
                            writer.WriteLine("{0} [{1,-5}] : {2}", this.DateTimeString(), kind, "=====< Trace Start >=====");
                        }
                        else
                        {
                            writer.WriteLine("{0} [{1,-5}] : {2}", this.DateTimeString(), kind, data);
                        }
                    }
                    finally
                    {
                        writer.Close();
                    }
                }
                catch
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsDebug
        {
            get
            {
                return this.isDebug;
            }
            set
            {
                this.isDebug = value;
            }
        }

        public bool PasswordMask
        {
            get
            {
                return this.mPasswordMask;
            }
            set
            {
                this.mPasswordMask = value;
            }
        }

        public string TraceFile
        {
            get
            {
                return this.mTraceFile;
            }
            set
            {
                this.mTraceFile = value;
            }
        }

        public int TraceSize
        {
            get
            {
                return this.mTraceSize;
            }
            set
            {
                this.mTraceSize = value;
            }
        }

        public bool UseTrace
        {
            get
            {
                return this.mUseTrace;
            }
            set
            {
                this.mUseTrace = value;
            }
        }

        public enum Tracekind
        {
            START,
            HTTP,
            HTTPS,
            MAIL,
            SMTP,
            POP,
            MIME
        }
    }
}

