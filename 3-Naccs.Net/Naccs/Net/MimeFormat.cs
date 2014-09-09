namespace Naccs.Net
{
    using System;
    using System.IO;
    using System.Net.Mime;
    using System.Text;

    public class MimeFormat
    {
        public const string BoundaryDist = "--";
        public const string ContentDispositionKey = "Content-Disposition";
        public const string ContentKey = "Content-";
        public const string ContentTransferKey = "Content-Transfer-Encoding";
        public const string ContentTypeKey = "Content-type";
        public const string CRLF = "\r\n";
        public const string Encoding8bit = "8bit";
        public const string EncodingBase64 = "base64";
        public const string EncodingUTF = "UTF-8";

        public static string AttachtPart(string path, string boundary)
        {
            string fileName = Path.GetFileName(path);
            string str2 = EncodeAttachFile(fileName);
            string str3 = string.Format("Content-Type: {0}; name=\"{1}\"", FileNameToContentType(fileName), str2);
            string str4 = string.Format("Content-Transfer-Encoding: {0}", "base64");
            string str5 = string.Format("Content-Disposition: attachment; filename=\"{0}\"", str2);
            return string.Format("{1}{2}{0}{3}{0}{4}{0}{5}{0}{0}", new object[] { "\r\n", "--", boundary, str3, str4, str5 });
        }

        public static string DecodeAttachFile(string filename)
        {
            string str = null;
            try
            {
                if (filename.Trim().StartsWith("=?"))
                {
                    str = "";
                    string[] strArray = filename.Trim().Split(new string[] { "?=" }, StringSplitOptions.None);
                    for (int i = 0; i < strArray.Length; i++)
                    {
                        if (!string.IsNullOrEmpty(strArray[i]))
                        {
                            string[] strArray2 = strArray[i].Trim().Split(new char[] { '?' });
                            if (strArray2[0] == "=")
                            {
                                if (strArray2[2].ToUpper() == "B")
                                {
                                    byte[] bytes = Convert.FromBase64String(strArray2[3]);
                                    str = str + GetEncoding(strArray2[1]).GetString(bytes);
                                }
                                else
                                {
                                    str = str + strArray2[3];
                                }
                            }
                            else
                            {
                                str = str + strArray[i].Trim();
                            }
                        }
                    }
                    return str;
                }
                return filename.Trim();
            }
            catch
            {
                return filename.Trim();
            }
        }

        public static string EncodeAttachFile(string filename)
        {
            byte[] bytes = GetEncoding("UTF-8").GetBytes(filename);
            return string.Format("=?utf-8?B?{0}?=", Convert.ToBase64String(bytes));
        }

        public static string FileNameToContentType(string filename)
        {
            switch (Path.GetExtension(filename).ToLower())
            {
                case ".txt":
                    return "text/plain";

                case ".xml":
                    return "text/xml";

                case ".xsl":
                    return "text/xml";

                case ".bmp":
                    return "image/bmp";

                case ".tif":
                    return "image/tiff";

                case ".jpg":
                    return "image/jpeg";

                case ".gif":
                    return "image/gif";

                case ".wav":
                    return "audio/wav";

                case ".csv":
                    return "application/vnd.ms-excel";

                case ".doc":
                    return "application/msword";

                case ".mdb":
                    return "application/msaccess";

                case ".pdf":
                    return "application/pdf";

                case ".ppt":
                    return "application/vnd.ms-excel";

                case ".xls":
                    return "application/vnd.ms-excel";
            }
            return "application/octet-stream";
        }

        public static Encoding GetEncoding(ContentType ct)
        {
            return GetEncoding(ct.CharSet);
        }

        public static Encoding GetEncoding(string charset)
        {
            if (string.IsNullOrEmpty(charset) || charset.Equals("utf-8", StringComparison.CurrentCultureIgnoreCase))
            {
                return Encoding.UTF8;
            }
            return Encoding.GetEncoding(charset);
        }

        public static bool IsApplication(ContentType ct)
        {
            if (ct == null)
            {
                return false;
            }
            return ct.MediaType.StartsWith("application", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsAttachment(ContentDisposition cd)
        {
            if (cd == null)
            {
                return false;
            }
            return cd.DispositionType.StartsWith("attachment", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsMulti(ContentType ct)
        {
            if (ct == null)
            {
                return false;
            }
            return ct.MediaType.StartsWith("multipart/mixed", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsSigned(ContentType ct)
        {
            if (ct == null)
            {
                return false;
            }
            return ct.MediaType.StartsWith("multipart/signed", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool IsText(ContentType ct)
        {
            if (ct == null)
            {
                return false;
            }
            return ct.MediaType.StartsWith("text", StringComparison.CurrentCultureIgnoreCase);
        }

        public static string LastPart(string boundary)
        {
            return string.Format("{1}{2}{1}{0}", "\r\n", "--", boundary);
        }

        public static string MultiContentType(string boundary)
        {
            return string.Format("multipart/mixed; boundary=\"{0}\"", boundary);
        }

        public static int StringLength(string str)
        {
            return Encoding.GetEncoding("UTF-8").GetByteCount(str);
        }

        public static string TextContentType(string charset)
        {
            return string.Format("text/plain; charset={0}", charset);
        }

        public static string TextPart(string boundary, string charset)
        {
            string str = string.Format("Content-Type: {0}", TextContentType(charset));
            string str2 = string.Format("Content-Transfer-Encoding: {0}", "8bit");
            return string.Format("{1}{2}{0}{3}{0}{4}{0}{0}", new object[] { "\r\n", "--", boundary, str, str2 });
        }

        public static string BoundaryString
        {
            get
            {
                int hashCode = string.Format("{0}", DateTime.Now).GetHashCode();
                return string.Format("{0}{1}", "--=_MultiPart_H", hashCode.ToString().Replace("-", ""));
            }
        }
    }
}

