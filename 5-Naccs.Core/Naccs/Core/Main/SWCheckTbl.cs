namespace Naccs.Core.Main
{
    using Naccs.Common.Function;
    using Naccs.Core.Settings;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;

    internal class SWCheckTbl
    {
        public List<SWRec> SWList = new List<SWRec>();

        public SWCheckTbl()
        {
            string path = Path.Combine(PathInfo.CreateInstance().SystemEnvironmentPath, "SW.tbl");
            try
            {
                StreamReader reader = new StreamReader(path, Encoding.UTF8);
                try
                {
                    while (!reader.EndOfStream)
                    {
                        List<string> list = CommaText.CommaTextToItems(reader.ReadLine());
                        if (list.Count > 0)
                        {
                            SWRec item = new SWRec {
                                JobCode = list[0].ToUpper()
                            };
                            if (list.Count > 1)
                            {
                                item.ROutCode = list[1].ToUpper();
                                if (list.Count > 2)
                                {
                                    item.OOutCode = list[2].ToUpper();
                                }
                                else
                                {
                                    item.OOutCode = "";
                                }
                            }
                            else
                            {
                                item.ROutCode = "";
                                item.OOutCode = "";
                            }
                            this.SWList.Add(item);
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

        [StructLayout(LayoutKind.Sequential)]
        public struct SWRec
        {
            public string JobCode;
            public string ROutCode;
            public string OOutCode;
        }
    }
}

