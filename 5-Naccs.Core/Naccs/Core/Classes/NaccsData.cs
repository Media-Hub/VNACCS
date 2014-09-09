namespace Naccs.Core.Classes
{
    using Naccs.Common.Function;
    using Naccs.Common.JobConfig;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web;
    using System.Xml.Serialization;

    [XmlRoot("ExData")]
    public class NaccsData : IData, ICloneable
    {
        private string[] ArySubJobCode = new string[1];
        private string[] ArySubJobData = new string[1];
        private string[] attach;
        private string attachFolder;
        private ContainedType contained;
        private Naccs.Core.Classes.NaccsHeader header = new Naccs.Core.Classes.NaccsHeader();
        private string id;
        private bool isDeleted;
        private bool isPrinted;
        [XmlElement(Order=10)]
        private bool isRead;
        private bool isSaved;
        private List<string> items = new List<string>();
        private int jobIndex;
        private const int LENGTH_CODE = 7;
        private const int LENGTH_CRLF = 2;
        private const int LENGTH_INFO_LINE = 13;
        private const int LENGTH_SIZE = 6;
        public const int POSITION_START_DATA = 0x4b;
        private string signFileFolder;
        private int signflg;
        private DataStatus status;
        private JobStyle style;
        private List<string> subitems = new List<string>();
        private DateTime timeStamp;
        private string userFolder;

        public int CalcLength()
        {
            this.header.Length = 400 + Naccs.Core.Classes.NaccsHeader.CalculationEncoding.GetByteCount(this.JobData);
            return this.header.Length;
        }

        public object Clone()
        {
            NaccsData data = (NaccsData) base.MemberwiseClone();
            data.header = new Naccs.Core.Classes.NaccsHeader();
            this.header.CopyTo(data.header);
            if (this.AttachFile != null)
            {
                data.AttachFile = (string[]) this.AttachFile.Clone();
            }
            data.JobData = this.JobData;
            data.CalcLength();
            return data;
        }

        private void DivideJobData()
        {
            for (int i = 0; i < this.ArySubJobData.Length; i++)
            {
                this.ArySubJobData[i] = "";
                this.ArySubJobCode[i] = "";
            }
            if (this.Style == JobStyle.combi)
            {
                string jobData = this.JobData;
                if (!string.IsNullOrEmpty(jobData))
                {
                    byte[] encodedBytes = Naccs.Core.Classes.NaccsHeader.GetEncodedBytes(jobData, Naccs.Core.Classes.NaccsHeader.CalculationEncoding);
                    int startIndex = 0;
                    int num3 = 0x4b;
                    int length = 0;
                    for (int j = 0; j < 5; j++)
                    {
                        length = int.Parse(Naccs.Core.Classes.NaccsHeader.ByteDataToString(encodedBytes, startIndex + 7, 6, Naccs.Core.Classes.NaccsHeader.CalculationEncoding));
                        this.ArySubJobCode[j] = Naccs.Core.Classes.NaccsHeader.ByteDataToString(encodedBytes, startIndex, 7, Naccs.Core.Classes.NaccsHeader.CalculationEncoding);
                        this.ArySubJobCode[j] = this.ArySubJobCode[j].Trim();
                        this.ArySubJobData[j] = Naccs.Core.Classes.NaccsHeader.ByteDataToString(encodedBytes, num3, length, Naccs.Core.Classes.NaccsHeader.CalculationEncoding);
                        char[] trimChars = new char[] { '\r', '\n' };
                        if (this.ArySubJobData[j].EndsWith("\r\n"))
                        {
                            this.ArySubJobData[j] = this.ArySubJobData[j].TrimEnd(trimChars);
                        }
                        num3 += length;
                        startIndex = (startIndex + 13) + 2;
                    }
                }
            }
        }

        public string GetDataString()
        {
            this.CalcLength();
            return (this.header.DataString + "\r\n" + this.JobData);
        }

        public string GetMaskedDataString()
        {
            this.CalcLength();
            return (this.header.MaskedString + "\r\n" + this.JobData);
        }

        private void MergeJobData()
        {
            string[] collection = new string[5];
            List<string> list = new List<string>();
            StrFunc func = StrFunc.CreateInstance();
            this.Items.Clear();
            for (int i = 0; i < this.ArySubJobData.Length; i++)
            {
                string str = this.ArySubJobData[i];
                if (!this.ArySubJobData[i].EndsWith("\r\n") && (str != ""))
                {
                    str = str + "\r\n";
                }
                int byteCount = Naccs.Core.Classes.NaccsHeader.CalculationEncoding.GetByteCount(str.ToString());
                collection[i] = func.FillBlankRight(this.ArySubJobCode[i], 7) + string.Format("{0:D6}", byteCount);
                if (!string.IsNullOrEmpty(this.ArySubJobData[i]))
                {
                    string[] separator = new string[] { "\r\n" };
                    string[] strArray3 = this.ArySubJobData[i].Split(separator, StringSplitOptions.None);
                    list.AddRange(strArray3);
                }
            }
            this.Items.AddRange(collection);
            this.Items.AddRange(list);
        }

        private void SetSubItems(int idx)
        {
            this.subitems.Clear();
            this.subitems = new List<string>();
            if (!string.IsNullOrEmpty(this.ArySubJobData[idx]))
            {
                string[] separator = new string[] { "\r\n" };
                string[] collection = this.ArySubJobData[idx].Split(separator, StringSplitOptions.None);
                this.subitems.AddRange(collection);
            }
        }

        public void SetSubJobData(string sData, string sJobCode)
        {
            if (this.style == JobStyle.combi)
            {
                this.ArySubJobData[this.JobIndex] = sData;
                this.ArySubJobCode[this.JobIndex] = sJobCode;
                this.SetSubItems(this.JobIndex);
                this.MergeJobData();
            }
        }

        [XmlIgnore]
        public int AttachCount
        {
            get
            {
                if (this.attach != null)
                {
                    return this.attach.Length;
                }
                return 0;
            }
            set
            {
                if (value == 0)
                {
                    this.attach = null;
                }
                else if ((this.attach == null) || (this.attach.Length != value))
                {
                    string[] strArray = new string[value];
                    if (this.attach != null)
                    {
                        for (int i = 0; (i < this.attach.Length) && (i < value); i++)
                        {
                            strArray[i] = this.attach[i];
                        }
                    }
                    this.attach = strArray;
                }
            }
        }

        [XmlArrayItem("FileName"), XmlArray("Attach", Order=8)]
        public string[] AttachFile
        {
            get
            {
                return this.attach;
            }
            set
            {
                this.attach = value;
            }
        }

        [XmlElement(Order=7)]
        public string AttachFolder
        {
            get
            {
                return this.attachFolder;
            }
            set
            {
                this.attachFolder = value;
            }
        }

        [XmlElement(Order=6)]
        public ContainedType Contained
        {
            get
            {
                return this.contained;
            }
            set
            {
                this.contained = value;
            }
        }

        public string DispCode
        {
            get
            {
                return this.header.DispCode;
            }
        }

        public IHeader Header
        {
            get
            {
                return this.header;
            }
        }

        [XmlElement(Order=0)]
        public string ID
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
            }
        }

        [XmlElement(Order=9)]
        public bool IsDeleted
        {
            get
            {
                return this.isDeleted;
            }
            set
            {
                this.isDeleted = value;
            }
        }

        [XmlElement(Order=13)]
        public bool IsPrinted
        {
            get
            {
                return this.isPrinted;
            }
            set
            {
                this.isPrinted = value;
            }
        }

        [XmlElement(Order=11)]
        public bool IsRead
        {
            get
            {
                return this.isRead;
            }
            set
            {
                this.isRead = value;
            }
        }

        [XmlElement(Order=12)]
        public bool IsSaved
        {
            get
            {
                return this.isSaved;
            }
            set
            {
                this.isSaved = value;
            }
        }

        [XmlIgnore]
        public int ItemCount
        {
            get
            {
                return this.items.Count;
            }
            set
            {
                while (this.items.Count > value)
                {
                    this.items.RemoveAt(this.items.Count - 1);
                }
                while (this.items.Count < value)
                {
                    this.items.Add("");
                }
            }
        }

        [XmlArrayItem("item"), XmlArray("JobData", Order=15)]
        public List<string> Items
        {
            get
            {
                return this.items;
            }
        }

        public string JobCode
        {
            get
            {
                return this.header.JobCode;
            }
        }

        [XmlElement(Order=5)]
        public int JobCount
        {
            get
            {
                if (this.style == JobStyle.combi)
                {
                    return 5;
                }
                return 1;
            }
        }

        [XmlIgnore]
        public string JobData
        {
            get
            {
                StringBuilder builder = new StringBuilder();
                foreach (string str in this.items)
                {
                    builder.Append(str);
                    builder.Append("\r\n");
                }
                return builder.ToString();
            }
            set
            {
                this.items = new List<string>();
                if (value != null)
                {
                    if (value.EndsWith("\r\n"))
                    {
                        value = value.Remove(value.Length - 2, 2);
                    }
                    string[] separator = new string[] { "\r\n" };
                    string[] collection = value.Split(separator, StringSplitOptions.None);
                    this.items.AddRange(collection);
                }
                this.DivideJobData();
                this.JobIndex = 0;
            }
        }

        [XmlIgnore]
        public string JobDataXml
        {
            get
            {
                return HttpUtility.UrlEncode(this.JobData);
            }
            set
            {
                this.JobData = HttpUtility.UrlDecode(value);
            }
        }

        [XmlIgnore]
        public int JobIndex
        {
            get
            {
                return this.jobIndex;
            }
            set
            {
                if (value < 0)
                {
                    this.jobIndex = 0;
                }
                else if (value >= this.ArySubJobData.Length)
                {
                    this.jobIndex = this.ArySubJobData.Length - 1;
                }
                else
                {
                    this.jobIndex = value;
                }
                this.SetSubItems(this.jobIndex);
            }
        }

        [XmlElement("Header", Order=14)]
        public Naccs.Core.Classes.NaccsHeader NaccsHeader
        {
            get
            {
                return this.header;
            }
            set
            {
                this.header = value;
            }
        }

        public string OutCode
        {
            get
            {
                return this.header.OutCode;
            }
        }

        [XmlElement("SignFileFolder", Order=0x11)]
        public string SignFileFolder
        {
            get
            {
                return this.signFileFolder;
            }
            set
            {
                this.signFileFolder = value;
            }
        }

        [XmlElement("Signflg", Order=0x10)]
        public int Signflg
        {
            get
            {
                return this.signflg;
            }
            set
            {
                this.signflg = value;
            }
        }

        [XmlElement(Order=2)]
        public DataStatus Status
        {
            get
            {
                return this.status;
            }
            set
            {
                this.status = value;
            }
        }

        [XmlElement(Order=1)]
        public JobStyle Style
        {
            get
            {
                return this.style;
            }
            set
            {
                this.style = value;
                if (value == JobStyle.combi)
                {
                    this.ArySubJobData = new string[5];
                    this.ArySubJobCode = new string[5];
                    this.DivideJobData();
                }
                else
                {
                    this.ArySubJobData = new string[1];
                    this.ArySubJobCode = new string[1];
                }
                this.JobIndex = 0;
            }
        }

        public string SubCode
        {
            get
            {
                return this.ArySubJobCode[this.jobIndex];
            }
        }

        [XmlIgnore]
        public List<string> SubItems
        {
            get
            {
                return this.subitems;
            }
        }

        [XmlElement("TimeStamp", Order=3)]
        public DateTime TimeStamp
        {
            get
            {
                return this.timeStamp;
            }
            set
            {
                this.timeStamp = value;
            }
        }

        [XmlElement(Order=4)]
        public string UserFolder
        {
            get
            {
                return this.userFolder;
            }
            set
            {
                this.userFolder = value;
            }
        }
    }
}

