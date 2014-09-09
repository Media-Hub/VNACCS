namespace Naccs.Core.Classes
{
    using Naccs.Core.Properties;
    using System;
    using System.Text;
    using System.Xml.Serialization;

    public class NaccsHeader : IHeader
    {
        public static Encoding CalculationEncoding = Encoding.GetEncoding("UTF-8");
        private string control;
        private string dataInfo;
        private string dataString;
        private string dataType;
        private string dispCode;
        private string div;
        private string endFlag;
        private string indexInfo;
        private string inputInfo;
        private string jobCode;
        private int length;
        public const int LENGTH_CONTROL = 3;
        public const int LENGTH_DATA_INFO = 0x1a;
        public const int LENGTH_DATA_LENGTH = 6;
        public const int LENGTH_DATA_TYPE = 1;
        public const int LENGTH_DISPLAY_CODE = 3;
        public const int LENGTH_DIVIDE_NUM = 3;
        public const int LENGTH_END_FLAG = 1;
        public const int LENGTH_HEADER = 400;
        public const int LENGTH_INDEX_INFO = 100;
        public const int LENGTH_INPUT_INFO = 10;
        public const int LENGTH_IO_PATH = 6;
        public const int LENGTH_JOBCODE = 5;
        public const int LENGTH_MAIL_ADDRESS = 0x40;
        public const int LENGTH_OUTCODE = 7;
        public const int LENGTH_PASSWORD = 8;
        public const int LENGTH_PATTERN = 1;
        public const int LENGTH_RESERVE_1 = 1;
        public const int LENGTH_RESERVE_2 = 1;
        public const int LENGTH_RESERVE_3 = 0x18;
        public const int LENGTH_RTP_INFO = 30;
        public const int LENGTH_SEND_GUARD = 1;
        public const int LENGTH_SERVER_INFO = 10;
        public const int LENGTH_SERVER_RECEIVE_TIME = 14;
        public const int LENGTH_SUBJECT = 0x40;
        public const int LENGTH_SYSTEM = 1;
        public const int LENGTH_USER_ID = 8;
        private string mailAddress;
        private const char MASK_CHAR = '*';
        private string outCode;
        private string password;
        private string path;
        private string pattern;
        public const int POSITION_CONTROL = 0;
        public const int POSITION_DATA_INFO = 0xdb;
        public const int POSITION_DATA_LENGTH = 0x188;
        public const int POSITION_DATA_TYPE = 0xf9;
        public const int POSITION_DISPLAY_CODE = 0x16d;
        public const int POSITION_DIVIDE_NUM = 0xf5;
        public const int POSITION_END_FLAG = 0xf8;
        public const int POSITION_INDEX_INFO = 0x107;
        public const int POSITION_INPUT_INFO = 0xfd;
        public const int POSITION_IO_PATH = 0x2d;
        public const int POSITION_JOBCODE = 3;
        public const int POSITION_MAIL_ADDRESS = 0x33;
        public const int POSITION_OUTCODE = 8;
        public const int POSITION_PASSWORD = 0x25;
        public const int POSITION_PATTERN = 0x16b;
        public const int POSITION_RESERVE_1 = 250;
        public const int POSITION_RESERVE_2 = 0xfc;
        public const int POSITION_RESERVE_3 = 0x170;
        public const int POSITION_RTP_INFO = 0xb3;
        public const int POSITION_SEND_GUARD = 0xfb;
        public const int POSITION_SERVER_INFO = 0xd1;
        public const int POSITION_SERVER_RECEIVE_TIME = 15;
        public const int POSITION_SUBJECT = 0x73;
        public const int POSITION_SYSTEM = 0x16c;
        public const int POSITION_USER_ID = 0x1d;
        private string rtpInfo;
        private string sendGuard;
        private string serverInfo;
        private string serverRecvTime;
        private const char SPACE = ' ';
        private string subject;
        private string system;
        private const string SYSTEM = "1";
        private string userId;

        public NaccsHeader()
        {
            this.Control = null;
            this.JobCode = null;
            this.OutCode = null;
            this.ServerRecvTime = null;
            this.UserId = null;
            this.Password = null;
            this.Path = null;
            this.MailAddress = null;
            this.Subject = null;
            this.RtpInfo = null;
            this.ServerInfo = null;
            this.DataInfo = null;
            this.Div = null;
            this.EndFlag = null;
            this.DataType = null;
            this.SendGuard = null;
            this.InputInfo = null;
            this.IndexInfo = null;
            this.Pattern = null;
            this.System = "1";
            this.DispCode = null;
        }

        private string buildDataString(bool bMask)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(this.control);
            builder.Append(this.jobCode);
            builder.Append(this.outCode);
            builder.Append(this.serverRecvTime);
            builder.Append(this.userId);
            if (bMask)
            {
                builder.Append(this.makeLoopString('*', 8));
            }
            else
            {
                builder.Append(this.password);
            }
            builder.Append(this.path);
            builder.Append(this.mailAddress);
            builder.Append(this.subject);
            builder.Append(this.rtpInfo);
            builder.Append(this.serverInfo);
            builder.Append(this.dataInfo);
            builder.Append(this.div);
            builder.Append(this.endFlag);
            builder.Append(this.dataType);
            builder.Append(this.makeLoopString(' ', 1));
            builder.Append(this.sendGuard);
            builder.Append(this.makeLoopString(' ', 1));
            builder.Append(this.inputInfo);
            builder.Append(this.indexInfo);
            builder.Append(this.pattern);
            builder.Append(this.system);
            builder.Append(this.dispCode);
            string str = this.length.ToString("D" + 6);
            int num = 6 - str.Length;
            builder.Append(this.makeLoopString(' ', 0x18 + num));
            builder.Append(this.length.ToString("D" + 6));
            return builder.ToString();
        }

        public static string ByteDataToString(byte[] byteArray, int startIndex, int length, Encoding encoding)
        {
            byte[] bytes = new byte[length];
            for (int i = 0; i < length; i++)
            {
                bytes[i] = byteArray[startIndex + i];
            }
            char[] chars = new char[encoding.GetCharCount(bytes, 0, bytes.Length)];
            encoding.GetChars(byteArray, startIndex, length, chars, 0);
            return new string(chars);
        }

        public void CopyTo(IHeader dest)
        {
            dest.Control = this.Control;
            dest.JobCode = this.JobCode;
            dest.OutCode = this.OutCode;
            dest.ServerRecvTime = this.ServerRecvTime;
            dest.UserId = this.UserId;
            dest.Password = this.Password;
            dest.Path = this.Path;
            dest.MailAddress = this.MailAddress;
            dest.Subject = this.Subject;
            dest.RtpInfo = this.RtpInfo;
            dest.ServerInfo = this.ServerInfo;
            dest.DataInfo = this.DataInfo;
            dest.Div = this.Div;
            dest.EndFlag = this.EndFlag;
            dest.DataType = this.DataType;
            dest.SendGuard = this.SendGuard;
            dest.InputInfo = this.InputInfo;
            dest.IndexInfo = this.IndexInfo;
            dest.Pattern = this.Pattern;
            dest.System = this.System;
            dest.DispCode = this.DispCode;
        }

        public static byte[] GetEncodedBytes(string value, Encoding encoding)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(value);
            return Encoding.Convert(Encoding.Unicode, encoding, bytes);
        }

        private string getString(string value, int length)
        {
            if (value == null)
            {
                value = " ";
            }
            while (value.Length < length)
            {
                value = value + ' ';
            }
            return ByteDataToString(GetEncodedBytes(value, CalculationEncoding), 0, length, CalculationEncoding);
        }

        private string makeLoopString(char c, int count)
        {
            string str = "";
            for (int i = 0; i < count; i++)
            {
                str = str + c;
            }
            return str;
        }

        public int SetBytesData(byte[] byteArray, Encoding encoding, bool ignoreLengthCheck)
        {
            if (byteArray.Length < 0x18e)
            {
                return -1;
            }
            if (!int.TryParse(ByteDataToString(byteArray, 0x188, 6, encoding), out this.length) && !ignoreLengthCheck)
            {
                return -2;
            }
            if ((this.length < 400) && !ignoreLengthCheck)
            {
                return -3;
            }
            try
            {
                this.control = ByteDataToString(byteArray, 0, 3, encoding);
                this.jobCode = ByteDataToString(byteArray, 3, 5, encoding);
                this.outCode = ByteDataToString(byteArray, 8, 7, encoding);
                this.serverRecvTime = ByteDataToString(byteArray, 15, 14, encoding);
                this.userId = ByteDataToString(byteArray, 0x1d, 8, encoding);
                this.password = ByteDataToString(byteArray, 0x25, 8, encoding);
                this.path = ByteDataToString(byteArray, 0x2d, 6, encoding);
                this.mailAddress = ByteDataToString(byteArray, 0x33, 0x40, encoding);
                this.subject = ByteDataToString(byteArray, 0x73, 0x40, encoding);
                this.rtpInfo = ByteDataToString(byteArray, 0xb3, 30, encoding);
                this.serverInfo = ByteDataToString(byteArray, 0xd1, 10, encoding);
                this.dataInfo = ByteDataToString(byteArray, 0xdb, 0x1a, encoding);
                this.div = ByteDataToString(byteArray, 0xf5, 3, encoding);
                this.endFlag = ByteDataToString(byteArray, 0xf8, 1, encoding);
                this.dataType = ByteDataToString(byteArray, 0xf9, 1, encoding);
                this.sendGuard = ByteDataToString(byteArray, 0xfb, 1, encoding);
                this.inputInfo = ByteDataToString(byteArray, 0xfd, 10, encoding);
                this.indexInfo = ByteDataToString(byteArray, 0x107, 100, encoding);
                this.pattern = ByteDataToString(byteArray, 0x16b, 1, encoding);
                this.system = ByteDataToString(byteArray, 0x16c, 1, encoding);
                this.dispCode = ByteDataToString(byteArray, 0x16d, 3, encoding);
                return 0;
            }
            catch
            {
                return -99;
            }
        }

        public string Control
        {
            get
            {
                return this.control;
            }
            set
            {
                this.control = this.getString(value, 3);
            }
        }

        public string DataInfo
        {
            get
            {
                return this.dataInfo;
            }
            set
            {
                this.dataInfo = this.getString(value, 0x1a);
            }
        }

        [XmlIgnore]
        public string DataString
        {
            get
            {
                if (this.dataString != null)
                {
                    return this.dataString;
                }
                return this.buildDataString(false);
            }
            set
            {
                this.dataString = value;
                byte[] encodedBytes = GetEncodedBytes(value, CalculationEncoding);
                switch (this.SetBytesData(encodedBytes, CalculationEncoding, false))
                {
                    case -2:
                        throw new Exception(Resources.ResourceManager.GetString("CORE73"));

                    case -1:
                    {
                        int num3 = 400;
                        throw new Exception(Resources.ResourceManager.GetString("CORE99") + "(" + num3.ToString() + ")");
                    }
                    case 0:
                        return;
                }
                throw new Exception(Resources.ResourceManager.GetString("CORE74"));
            }
        }

        public string DataType
        {
            get
            {
                return this.dataType;
            }
            set
            {
                this.dataType = this.getString(value, 1);
            }
        }

        public string DispCode
        {
            get
            {
                return this.dispCode;
            }
            set
            {
                this.dispCode = this.getString(value, 3);
            }
        }

        public string Div
        {
            get
            {
                return this.div;
            }
            set
            {
                this.div = this.getString(value, 3);
            }
        }

        public string EndFlag
        {
            get
            {
                return this.endFlag;
            }
            set
            {
                this.endFlag = this.getString(value, 1);
            }
        }

        public string IndexInfo
        {
            get
            {
                return this.indexInfo;
            }
            set
            {
                this.indexInfo = this.getString(value, 100);
            }
        }

        public string InputInfo
        {
            get
            {
                return this.inputInfo;
            }
            set
            {
                this.inputInfo = this.getString(value, 10);
            }
        }

        public string JobCode
        {
            get
            {
                return this.jobCode;
            }
            set
            {
                this.jobCode = this.getString(value, 5);
            }
        }

        public int Length
        {
            get
            {
                return this.length;
            }
            set
            {
                this.length = value;
            }
        }

        public string MailAddress
        {
            get
            {
                return this.mailAddress;
            }
            set
            {
                this.mailAddress = this.getString(value, 0x40);
            }
        }

        [XmlIgnore]
        public string MaskedString
        {
            get
            {
                return this.buildDataString(false);
            }
        }

        public string OutCode
        {
            get
            {
                return this.outCode;
            }
            set
            {
                this.outCode = this.getString(value, 7);
            }
        }

        [XmlIgnore]
        public string Password
        {
            get
            {
                return this.password;
            }
            set
            {
                this.password = this.getString(value, 8);
            }
        }

        public string Path
        {
            get
            {
                return this.path;
            }
            set
            {
                this.path = this.getString(value, 6);
            }
        }

        public string Pattern
        {
            get
            {
                return this.pattern;
            }
            set
            {
                this.pattern = this.getString(value, 1);
            }
        }

        public string RtpInfo
        {
            get
            {
                return this.rtpInfo;
            }
            set
            {
                this.rtpInfo = this.getString(value, 30);
            }
        }

        public string SendGuard
        {
            get
            {
                return this.sendGuard;
            }
            set
            {
                this.sendGuard = this.getString(value, 1);
            }
        }

        public string ServerInfo
        {
            get
            {
                return this.serverInfo;
            }
            set
            {
                this.serverInfo = this.getString(value, 10);
            }
        }

        public string ServerRecvTime
        {
            get
            {
                return this.serverRecvTime;
            }
            set
            {
                this.serverRecvTime = this.getString(value, 14);
            }
        }

        public string Subject
        {
            get
            {
                return this.subject;
            }
            set
            {
                this.subject = this.getString(value, 0x40);
            }
        }

        public string System
        {
            get
            {
                return this.system;
            }
            set
            {
                this.system = this.getString(value, 1);
            }
        }

        public string UserId
        {
            get
            {
                return this.userId;
            }
            set
            {
                this.userId = this.getString(value, 8);
            }
        }
    }
}

