namespace Naccs.Core.BatchDoc
{
    using Naccs.Common.Function;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    public class OutcodeTbl
    {
        public List<OutcodeRec> OutcodeList = new List<OutcodeRec>();

        public OutcodeTbl()
        {
            PathInfo info = PathInfo.CreateInstance();
            try
            {
                StreamReader reader = new StreamReader(info.OutCodeTable, Encoding.UTF8);
                try
                {
                    while (!reader.EndOfStream)
                    {
                        List<string> list = CommaText.CommaTextToItems(reader.ReadLine());
                        if (list.Count > 0)
                        {
                            OutcodeRec item = new OutcodeRec {
                                OutCode = list[0].ToUpper()
                            };
                            if (list.Count > 1)
                            {
                                item.DocName = list[1].ToUpper();
                            }
                            else
                            {
                                item.DocName = "";
                            }
                            this.OutcodeList.Add(item);
                        }
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            catch
            {
            }
        }

        public string Find(string outcode)
        {
            string str2 = outcode.ToUpper();
            if (str2.Length > 6)
            {
                str2 = str2.Remove(6);
            }
            for (int i = 0; i < this.OutcodeList.Count; i++)
            {
                string str3 = this.OutcodeList[i].OutCode.ToUpper();
                if (str3.Length > 6)
                {
                    str3 = str3.Remove(6);
                }
                if (str3.EndsWith("__"))
                {
                    str3 = str3.Remove(4);
                }
                if (str2.StartsWith(str3))
                {
                    return this.OutcodeList[i].DocName;
                }
            }
            return null;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct OutcodeRec
        {
            public string OutCode;
            public string DocName;
        }
    }
}

