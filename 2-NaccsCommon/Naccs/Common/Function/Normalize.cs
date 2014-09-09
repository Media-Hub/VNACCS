namespace Naccs.Common.Function
{
    using System;
    using System.Text;

    public class Normalize
    {
        public static bool NormalizeComp(string s1, string s2, bool ignorecase)
        {
            string str = NormalizeString(s1, ignorecase);
            string str2 = NormalizeString(s2, ignorecase);
            return (str == str2);
        }

        public static string NormalizeString(string value, bool ignorecase)
        {
            string str = value.Normalize(NormalizationForm.FormKC);
            if (ignorecase)
            {
                str = str.ToUpper();
            }
            return str;
        }
    }
}

