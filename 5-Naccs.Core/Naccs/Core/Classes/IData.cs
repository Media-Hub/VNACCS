namespace Naccs.Core.Classes
{
    using Naccs.Common.JobConfig;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [XmlRoot("Data")]
    public interface IData : ICloneable
    {
        int CalcLength();
        string GetDataString();
        string GetMaskedDataString();
        void SetSubJobData(string sData, string sJobCode);

        int AttachCount { get; set; }

        string[] AttachFile { get; set; }

        string AttachFolder { get; set; }

        ContainedType Contained { get; set; }

        string DispCode { get; }

        IHeader Header { get; }

        [XmlElement("ID")]
        string ID { get; set; }

        bool IsDeleted { get; set; }

        bool IsPrinted { get; set; }

        bool IsRead { get; set; }

        bool IsSaved { get; set; }

        int ItemCount { get; }

        List<string> Items { get; }

        string JobCode { get; }

        int JobCount { get; }

        string JobData { get; set; }

        int JobIndex { get; set; }

        string OutCode { get; }

        string SignFileFolder { get; set; }

        int Signflg { get; set; }

        DataStatus Status { get; set; }

        JobStyle Style { get; set; }

        string SubCode { get; }

        List<string> SubItems { get; }

        DateTime TimeStamp { get; set; }

        string UserFolder { get; set; }
    }
}

