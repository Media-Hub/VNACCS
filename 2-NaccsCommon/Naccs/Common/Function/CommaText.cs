namespace Naccs.Common.Function
{
    using System;
    using System.Collections.Generic;

    public class CommaText
    {
        public static List<string> CommaTextToItems(string value)
        {
            return CommaTextToItems(value, false);
        }

        public static List<string> CommaTextToItems(string value, bool excelMode)
        {
            string str = value.Replace("\r\n", ",");
            List<string> list = new List<string>();
            int num = 0;
            string item = "";
            while (num < str.Length)
            {
                if (str[num] != '"')
                {
                    goto Label_00FA;
                }
                num++;
                while (num < str.Length)
                {
                    if (str[num] == '"')
                    {
                        num++;
                        if (num < str.Length)
                        {
                            if (str[num] != '"')
                            {
                                num++;
                                break;
                            }
                            item = item + str[num];
                        }
                    }
                    else
                    {
                        item = item + str[num];
                    }
                    num++;
                }
                goto Label_0103;
            Label_00A6:
                if (excelMode)
                {
                    if ((str[num] != ',') && (str[num] != '\n'))
                    {
                        goto Label_00E1;
                    }
                    num++;
                    goto Label_0103;
                }
                if ((str[num] == ',') || (str[num] <= ' '))
                {
                    num++;
                    goto Label_0103;
                }
            Label_00E1:
                item = item + str[num];
                num++;
            Label_00FA:
                if (num < str.Length)
                {
                    goto Label_00A6;
                }
            Label_0103:
                list.Add(item);
                item = "";
                while (num < str.Length)
                {
                    if (excelMode)
                    {
                        if (str[num] == ',')
                        {
                            goto Label_013A;
                        }
                        continue;
                    }
                    if ((str[num] > ' ') && (str[num] != ','))
                    {
                        continue;
                    }
                Label_013A:
                    if (str[num] == ',')
                    {
                        list.Add("");
                    }
                    num++;
                }
            }
            return list;
        }

        public static string ItemsToCommaText(string value)
        {
            string str = "";
            foreach (string str3 in value.Replace("\r\n", "\n").Split(new char[] { '\n' }))
            {
                if (str != "")
                {
                    str = str + ',';
                }
                object obj2 = str;
                str = string.Concat(new object[] { obj2, '"', str3.Replace("\"", "\"\""), '"' });
            }
            return str;
        }

        public static string ItemsToCommaText(List<string> list)
        {
            string str = "";
            foreach (string str2 in list)
            {
                if (str != "")
                {
                    str = str + ',';
                }
                object obj2 = str;
                str = string.Concat(new object[] { obj2, '"', str2.Replace("\"", "\"\""), '"' });
            }
            return str;
        }
    }
}

