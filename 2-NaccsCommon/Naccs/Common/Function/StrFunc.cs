namespace Naccs.Common.Function
{
    using System;
    using System.Text;

    public class StrFunc
    {
        private static StrFunc singletonInstance;
        private const string UseCharset = "UTF-8";

        public string ConvertCommaToPeriod(string s)
        {
            return s.Replace(",", ".");
        }

        public string ConvertPeriodToComma(string s)
        {
            return s.Replace(".", ",");
        }

        public static StrFunc CreateInstance()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new StrFunc();
            }
            return singletonInstance;
        }

        public string CutByteLength(string st, int byteCnt)
        {
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            string s = st;
            if (byteCnt >= encoding.GetByteCount(s))
            {
                return s;
            }
            string str2 = "";
            for (int i = s.Length; i >= 0; i--)
            {
                str2 = s.Substring(0, i);
                if (encoding.GetByteCount(str2) <= byteCnt)
                {
                    return str2;
                }
            }
            return str2;
        }

        public string CutStringLength(string value, int maxLength)
        {
            if (!string.IsNullOrEmpty(value) && (value.Length > maxLength))
            {
                return value.Substring(0, maxLength);
            }
            return value;
        }

        public string FillBlankLeft(string S, int Leng)
        {
            int num = Leng - this.GetByteLength(S);
            if (num <= 0)
            {
                return S;
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < num; i++)
            {
                builder.Append(" ");
            }
            builder.Append(S);
            return builder.ToString();
        }

        public string FillBlankRight(string S, int Leng)
        {
            int num = Leng - this.GetByteLength(S);
            if (num <= 0)
            {
                return S;
            }
            StringBuilder builder = new StringBuilder(S);
            for (int i = 0; i < num; i++)
            {
                builder.Append(" ");
            }
            return builder.ToString();
        }

        public bool ForbiddenCharCheck(string S)
        {
            bool flag = true;
            for (int i = 0; i < S.Length; i++)
            {
                flag = this.ForbiddenCharCheck(S[i], false);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public bool ForbiddenCharCheck(char c, bool flgInput)
        {
            if ((' ' <= c) && (c <= ']'))
            {
                if ('$' == c)
                {
                    return false;
                }
                return true;
            }
            return ((flgInput && ('a' <= c)) && (c <= 'z'));
        }

        public bool ForbiddenConvCheck(char c)
        {
            if (((('\r' == c) || ('\n' == c)) || ((0xff60 <= c) && (c <= 0xff9f))) || ((c >= '\x0080') && (c <= '\x009f')))
            {
                return false;
            }
            byte[] bytes = Encoding.GetEncoding("UTF-8").GetBytes(char.ToString(c));
            if (((bytes.Length == 1) && (bytes[0] == 0x3f)) && (c != '?'))
            {
                return false;
            }
            return true;
        }

        public bool ForbiddenConvCheck(string S)
        {
            bool flag = true;
            for (int i = 0; i < S.Length; i++)
            {
                flag = this.ForbiddenConvCheck(S[i]);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public bool ForbiddenPasswordCharCheck(string S)
        {
            bool flag = true;
            for (int i = 0; i < S.Length; i++)
            {
                flag = this.ForbiddenPasswordCharCheck(S[i], false);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public bool ForbiddenPasswordCharCheck(char c, bool flgInput)
        {
            if (((' ' <= c) && (c <= 'Z')) || (('a' <= c) && (c <= 'z')))
            {
                if ('$' == c)
                {
                    return false;
                }
                return true;
            }
            return flgInput;
        }

        public int GetByteLength(string st)
        {
            return Encoding.GetEncoding("UTF-8").GetByteCount(st);
        }

        public int GetStringLength(string st)
        {
            if (st == null)
            {
                return 0;
            }
            return st.Length;
        }

        public bool InputInfoCharCheck(string S)
        {
            for (int i = 0; i < S.Length; i++)
            {
                char ch = S[i];
                if ((' ' > ch) || (ch > 'Z'))
                {
                    return false;
                }
                if ((('#' == ch) || ('$' == ch)) || ('@' == ch))
                {
                    return false;
                }
            }
            return true;
        }

        public bool IsIntegralNum(char c)
        {
            if (((c < '0') || (c > '9')) && (c != '-'))
            {
                return false;
            }
            return true;
        }

        public bool IsIntegralNum(string S)
        {
            bool flag = true;
            for (int i = 0; i < S.Length; i++)
            {
                if ((i > 0) && (S[i] == '-'))
                {
                    return false;
                }
                flag = this.IsIntegralNum(S[i]);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public bool IsNumeric(string S)
        {
            bool flag = false;
            bool flag2 = true;
            for (int i = 0; i < S.Length; i++)
            {
                if (i == 0)
                {
                    if ((('0' > S[i]) || (S[i] > '9')) && (S[i] != '-'))
                    {
                        return false;
                    }
                    flag2 = true;
                }
                else
                {
                    if ((('0' > S[i]) || (S[i] > '9')) && (S[i] != ','))
                    {
                        return false;
                    }
                    if (S[i] == ',')
                    {
                        if (flag)
                        {
                            return false;
                        }
                        flag = true;
                    }
                    flag2 = true;
                }
            }
            return flag2;
        }

        public bool IsRealNum(char c)
        {
            if (((c < '0') || (c > '9')) && ((c != '-') && (c != ',')))
            {
                return false;
            }
            return true;
        }

        public bool IsRealNum(string S)
        {
            bool flag = true;
            for (int i = 0; i < S.Length; i++)
            {
                flag = this.IsRealNum(S[i]);
                if (!flag)
                {
                    return flag;
                }
            }
            return flag;
        }

        public string MakeCommaStr(string S)
        {
            StringBuilder builder = new StringBuilder();
            string str = this.ReverseStr(S);
            for (int i = 0; i < str.Length; i++)
            {
                if ((i > 2) && ((i % 3) == 0))
                {
                    builder.Append(".");
                }
                builder.Append(str[i]);
            }
            return this.ReverseStr(builder.ToString());
        }

        public string MakeCycleStr(int cnt, string st)
        {
            if (string.IsNullOrEmpty(st) || (cnt < 1))
            {
                return "";
            }
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < cnt; i++)
            {
                builder.Append(st);
            }
            return builder.ToString();
        }

        public string MakeNumericStr(string S)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = 0; i < S.Length; i++)
            {
                if (char.IsNumber(S[i]))
                {
                    builder.Append(S[i]);
                }
            }
            return builder.ToString();
        }

        public string MakeYenCommaStr(string Data, string Yen, bool Comma)
        {
            string s = Data;
            string str2 = "";
            string str3 = "";
            if (s.IndexOf("-") >= 0)
            {
                str2 = "-";
                s = s.Remove(s.IndexOf("-"), 1);
            }
            int index = s.IndexOf(",");
            if (index >= 0)
            {
                string str4 = s.Substring(index, s.Length - index);
                str3 = "," + this.MakeNumericStr(str4);
                s = s.Substring(0, index);
            }
            if (Comma && (s.Length > 3))
            {
                s = this.MakeCommaStr(this.MakeNumericStr(s));
            }
            s = str2 + s + str3;
            if (((Yen != "") && (Yen != " ")) && ((Yen != "0") && (s.Length != 0)))
            {
                s = Yen + s;
            }
            return s;
        }

        public string ReverseStr(string S)
        {
            StringBuilder builder = new StringBuilder();
            for (int i = S.Length - 1; i >= 0; i--)
            {
                builder.Append(S[i]);
            }
            return builder.ToString();
        }

        public string SubByteString(string st, int iStart, int iLength)
        {
            Encoding encoding = Encoding.GetEncoding("UTF-8");
            byte[] bytes = encoding.GetBytes(st);
            if (iStart >= bytes.Length)
            {
                return "";
            }
            byte[] buffer2 = new byte[iLength];
            int index = 0;
            for (int i = iStart; i < bytes.Length; i++)
            {
                if (index > (buffer2.Length - 1))
                {
                    break;
                }
                buffer2[index] = bytes[i];
                index++;
            }
            char[] chars = new char[buffer2.Length];
            encoding.GetChars(buffer2, 0, iLength, chars, 0);
            return new string(chars);
        }
    }
}

