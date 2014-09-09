namespace Naccs.Common.Common
{
    using System;
    using System.ComponentModel;
    using System.Drawing;

    public class ChangeFont
    {
        public static string DefNewFontName = "Tahoma";
        private static ChangeFont singletonInstance = null;

        protected ChangeFont()
        {
        }

        public static Font ConvertFromString(string value)
        {
            Font font;
            float num;
            string[] strArray = value.Split(new char[] { ',' });
            string familyName = strArray[0].Trim();
            try
            {
                num = float.Parse(strArray[1].Trim(new char[] { ' ', 'p', 't' }));
            }
            catch
            {
                num = 10f;
            }
            try
            {
                font = new Font(familyName, num);
            }
            catch
            {
                font = new Font(DefNewFontName, num);
            }
            try
            {
                using (FontFamily family = new FontFamily(familyName))
                {
                    font = (Font) TypeDescriptor.GetConverter(font).ConvertFromInvariantString(value);
                    if (font.Name != family.Name)
                    {
                        font = (Font) TypeDescriptor.GetConverter(font).ConvertFromInvariantString(value.Replace(familyName, family.Name));
                    }
                }
            }
            catch
            {
            }
            return font;
        }

        public static ChangeFont CreateInstance()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new ChangeFont();
            }
            return singletonInstance;
        }
    }
}

