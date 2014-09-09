namespace Naccs.Net
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Mime;
    using System.Runtime.InteropServices;
    using System.Text;

    public class ComParamBuilder
    {
        private const string CharsetUTF = "UTF-8";
        private const string CRLF = "\r\n";
        private string mBaseCharsetStirng = "UTF-8";
        private const int StreamBufferSize = 0x800;
        private const string TempAttachFolder = @"tempAttach\";

        private void CreateAttachFolder(string path)
        {
            string directoryName = Path.GetDirectoryName(path);
            if (Directory.Exists(directoryName))
            {
                Directory.Delete(directoryName, true);
            }
            Directory.CreateDirectory(directoryName);
        }

        private bool GetPart(string boundary, byte[] buffer, long offset, out MimePart part)
        {
            int num;
            string str;
            part = new MimePart();
            part.Header = "";
            part.HdList = new List<string>();
            part.BodyOffset = offset;
            part.BodyLength = 0;
            part.LastPart = false;
            part.NextOffset = offset;
            bool flag = false;
            bool flag2 = false;
            while ((num = this.MemReadToLine(buffer, part.NextOffset)) > 0)
            {
                str = Encoding.ASCII.GetString(buffer, (int) part.NextOffset, num);
                part.NextOffset += num;
                if (str.StartsWith("--") && (str.IndexOf(boundary) >= 0))
                {
                    break;
                }
            }
            while ((num = this.MemReadToLine(buffer, part.NextOffset)) > 0)
            {
                str = Encoding.ASCII.GetString(buffer, (int) part.NextOffset, num);
                if (str.StartsWith("--") && (str.IndexOf(boundary) >= 0))
                {
                    flag2 = true;
                    part.LastPart = str.EndsWith("--");
                    return flag2;
                }
                if (!flag)
                {
                    if (str == "\r\n")
                    {
                        flag = true;
                        part.BodyOffset = part.NextOffset + num;
                    }
                    else
                    {
                        part.Header = part.Header + str;
                        if (str.StartsWith(" ") || str.StartsWith("\t"))
                        {
                            List<string> list;
                            int num2;
                            part.HdList[part.HdList.Count - 1] = part.HdList[part.HdList.Count - 1].TrimEnd(new char[0]);
                            (list = part.HdList)[num2 = part.HdList.Count - 1] = list[num2] + str.TrimStart(new char[0]);
                        }
                        else
                        {
                            part.HdList.Add(str);
                        }
                    }
                }
                else
                {
                    part.BodyLength += num;
                }
                part.NextOffset += num;
            }
            return flag2;
        }

        private string GetValue(List<string> list, string key)
        {
            string str = null;
            foreach (string str2 in list)
            {
                string[] strArray = str2.Split(new char[] { ':' });
                if (((strArray.Length > 0) && strArray[0].StartsWith(key, StringComparison.CurrentCultureIgnoreCase)) && (strArray.Length > 1))
                {
                    str = strArray[1].Trim();
                }
            }
            return str;
        }

        private int MemReadToLine(byte[] buffer, long offset)
        {
            int num = 0;
            for (long i = offset; i < buffer.LongLength; i += 1L)
            {
                num++;
                if (buffer[(int) ((IntPtr) i)] == 10)
                {
                    return num;
                }
            }
            return num;
        }

        private string ReadAttachFile(string path)
        {
            string str;
            Stream readStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            try
            {
                MemoryStream stream2 = this.ReadToMemory(readStream);
                str = Convert.ToBase64String(stream2.ToArray(), Base64FormattingOptions.InsertLineBreaks);
                stream2.Close();
            }
            finally
            {
                readStream.Close();
            }
            return str;
        }

        public MemoryStream ReadToMemory(Stream readStream)
        {
            MemoryStream stream = new MemoryStream();
            byte[] buffer = new byte[0x800];
            int num = 0;
            while (true)
            {
                int count = readStream.Read(buffer, 0, 0x800);
                if (count == 0)
                {
                    return stream;
                }
                num += count;
                stream.SetLength((long) num);
                stream.Write(buffer, 0, count);
            }
        }

        private string TextDecoding(byte[] bytedata, string encode)
        {
            Encoding baseEncoding;
            if (string.IsNullOrEmpty(encode))
            {
                baseEncoding = this.BaseEncoding;
            }
            else
            {
                baseEncoding = MimeFormat.GetEncoding(encode);
            }
            return baseEncoding.GetString(bytedata);
        }

        private string TextDecoding(byte[] bytedata, int index, int count, string encode)
        {
            return MimeFormat.GetEncoding(encode).GetString(bytedata, index, count);
        }

        private MemoryStream TextEncoding(string data)
        {
            MemoryStream stream = new MemoryStream();
            if (!data.EndsWith("\r\n"))
            {
                data = data + "\r\n";
            }
            byte[] bytes = this.BaseEncoding.GetBytes(data);
            stream.Write(bytes, 0, bytes.Length);
            return stream;
        }

        public ComParameter ToAttachComParameter(MemoryStream mem, string name, string trans)
        {
            ComParameter parameter = new ComParameter {
                AttachFolder = this.AttachFolder
            };
            this.CreateAttachFolder(parameter.AttachFolder);
            if ((trans != null) && trans.StartsWith("base64", StringComparison.CurrentCultureIgnoreCase))
            {
                string str = Encoding.ASCII.GetString(mem.ToArray());
                this.WriteAttachFile(Path.Combine(parameter.AttachFolder, name), str);
                return parameter;
            }
            FileStream stream = new FileStream(Path.Combine(parameter.AttachFolder, name), FileMode.OpenOrCreate, FileAccess.Write);
            stream.Write(mem.ToArray(), 0, mem.ToArray().Length);
            stream.Close();
            return parameter;
        }

        public ComParameter ToComParameter(MemoryStream mem, string boundary, NetTrace trace)
        {
            MimePart part;
            ComParameter parameter = new ComParameter();
            byte[] buffer = mem.ToArray();
            for (long i = 0L; this.GetPart(boundary, buffer, i, out part); i = part.NextOffset)
            {
                if (Encoding.ASCII.GetString(buffer, (((int) part.BodyOffset) + part.BodyLength) - 4, 4).EndsWith("\r\n\r\n"))
                {
                    part.BodyLength -= 2;
                }
                ContentType ct = null;
                string str2 = null;
                ContentDisposition cd = null;
                string str3 = this.GetValue(part.HdList, "Content-type");
                if (!string.IsNullOrEmpty(str3))
                {
                    ct = new ContentType(str3);
                }
                str2 = this.GetValue(part.HdList, "Content-Transfer-Encoding");
                str3 = this.GetValue(part.HdList, "Content-Disposition");
                if (!string.IsNullOrEmpty(str3))
                {
                    cd = new ContentDisposition(str3);
                }
                if (MimeFormat.IsAttachment(cd))
                {
                    if (string.IsNullOrEmpty(parameter.AttachFolder))
                    {
                        parameter.AttachFolder = this.AttachFolder;
                        this.CreateAttachFolder(parameter.AttachFolder);
                    }
                    string str4 = MimeFormat.DecodeAttachFile(cd.FileName);
                    if (trace != null)
                    {
                        trace.WriteString(NetTrace.Tracekind.MIME, string.Format("+ {0,-16} : {1}", "ToComParameter", string.Format("AttachFilename={0} DataLength={1}{2}{3}", new object[] { str4, part.BodyLength, "\r\n", part.Header })));
                    }
                    if ((str2 != null) && str2.StartsWith("base64", StringComparison.CurrentCultureIgnoreCase))
                    {
                        string str5 = Encoding.ASCII.GetString(buffer, (int) part.BodyOffset, part.BodyLength);
                        this.WriteAttachFile(Path.Combine(parameter.AttachFolder, str4), str5);
                    }
                    else
                    {
                        FileStream stream = new FileStream(Path.Combine(parameter.AttachFolder, str4), FileMode.OpenOrCreate, FileAccess.Write);
                        stream.Write(buffer, (int) part.BodyOffset, part.BodyLength);
                        stream.Close();
                    }
                }
                else if (MimeFormat.IsText(ct))
                {
                    if (trace != null)
                    {
                        trace.WriteString(NetTrace.Tracekind.MIME, string.Format("+ {0,-16} : {1}", "ToComParameter", string.Format("TextDataLength={0}{1}{2}", part.BodyLength, "\r\n", part.Header)));
                    }
                    if (string.IsNullOrEmpty(parameter.DataString))
                    {
                        parameter.DataString = this.TextDecoding(buffer, (int) part.BodyOffset, part.BodyLength, ct.CharSet);
                    }
                }
                if (part.LastPart)
                {
                    return parameter;
                }
            }
            return parameter;
        }

        public MemoryStream ToMemoryStream(ComParameter data, string boundary, NetTrace trace)
        {
            byte[] bytes = Encoding.ASCII.GetBytes("\r\n");
            MemoryStream stream = new MemoryStream();
            bool flag = data.AttachCount > 0;
            if (!string.IsNullOrEmpty(data.DataString))
            {
                string str = MimeFormat.TextPart(boundary, this.BaseCharset);
                MemoryStream stream2 = this.TextEncoding(data.DataString);
                try
                {
                    if (flag)
                    {
                        if (trace != null)
                        {
                            trace.WriteString(NetTrace.Tracekind.MIME, string.Format("+ {0,-16} : {1}", "ToMemoryStream", string.Format("TextDataLength={0}{1}{2}", stream2.ToArray().Length, "\r\n", str)));
                        }
                        byte[] buffer = Encoding.ASCII.GetBytes(str);
                        stream.Write(buffer, 0, buffer.Length);
                    }
                    stream.Write(stream2.ToArray(), 0, stream2.ToArray().Length);
                }
                finally
                {
                    stream2.Close();
                }
            }
            foreach (string str2 in data.AttachFiles)
            {
                if (stream.ToArray().Length > 0)
                {
                    stream.Write(bytes, 0, bytes.Length);
                }
                string path = Path.Combine(data.AttachFolder, str2);
                string s = MimeFormat.AttachtPart(path, boundary);
                string str5 = this.ReadAttachFile(path) + "\r\n";
                byte[] buffer3 = this.BaseEncoding.GetBytes(str5);
                if (trace != null)
                {
                    trace.WriteString(NetTrace.Tracekind.MIME, string.Format("+ {0,-16} : {1}", "ToMemoryStream", string.Format("AttachFilename={0} DataLength={1}{2}{3}", new object[] { str2, buffer3.Length, "\r\n", s })));
                }
                byte[] buffer4 = Encoding.ASCII.GetBytes(s);
                stream.Write(buffer4, 0, buffer4.Length);
                stream.Write(buffer3, 0, buffer3.Length);
            }
            if (flag)
            {
                stream.Write(bytes, 0, bytes.Length);
                byte[] buffer5 = Encoding.ASCII.GetBytes(MimeFormat.LastPart(boundary));
                stream.Write(buffer5, 0, buffer5.Length);
            }
            return stream;
        }

        public ComParameter ToTextComParameter(MemoryStream mem, string charSet)
        {
            return new ComParameter { DataString = this.TextDecoding(mem.ToArray(), charSet) };
        }

        private void WriteAttachFile(string path, string b64String)
        {
            Stream stream = new FileStream(path, FileMode.OpenOrCreate, FileAccess.Write);
            try
            {
                MemoryStream stream2 = new MemoryStream(Convert.FromBase64String(b64String));
                stream2.WriteTo(stream);
                stream2.Close();
            }
            finally
            {
                stream.Close();
            }
        }

        private string AttachFolder
        {
            get
            {
                return Path.Combine(Path.GetTempPath(), @"tempAttach\");
            }
        }

        public string BaseCharset
        {
            get
            {
                return this.mBaseCharsetStirng;
            }
            set
            {
                this.mBaseCharsetStirng = value;
            }
        }

        private Encoding BaseEncoding
        {
            get
            {
                return MimeFormat.GetEncoding(this.mBaseCharsetStirng);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct MimePart
        {
            public string Header;
            public List<string> HdList;
            public long BodyOffset;
            public int BodyLength;
            public bool LastPart;
            public long NextOffset;
        }
    }
}

