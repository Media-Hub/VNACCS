namespace Naccs.Common.Common
{
    using System;
    using System.Drawing;
    using System.Drawing.Text;
    using System.IO;

    public class ImportFont
    {
        public static string DefOcrFontFile = "NACCS_OCR.TTF";
        public static string DefSpcFontFile = "ZSgothic.ttf";
        private PrivateFontCollection mFontCollection = new PrivateFontCollection();
        private static ImportFont singletonInstance = null;

        protected ImportFont()
        {
        }

        public void AddFontFile(string fontFile)
        {
            if (File.Exists(fontFile))
            {
                try
                {
                    this.mFontCollection.AddFontFile(fontFile);
                }
                catch
                {
                }
            }
        }

        public static ImportFont CreateInstance()
        {
            if (singletonInstance == null)
            {
                singletonInstance = new ImportFont();
            }
            return singletonInstance;
        }

        public Font GetFont(string familyName, float emSize)
        {
            foreach (FontFamily family in this.mFontCollection.Families)
            {
                if (family.Name == familyName)
                {
                    return new Font(family, emSize);
                }
            }
            return null;
        }

        public Font GetFont(string fontFile, string familyName, float emSize)
        {
            try
            {
                foreach (FontFamily family in this.mFontCollection.Families)
                {
                    if (family.Name == familyName)
                    {
                        return new Font(family, emSize);
                    }
                }
                if (!File.Exists(fontFile))
                {
                    return null;
                }
                this.mFontCollection.AddFontFile(fontFile);
                foreach (FontFamily family2 in this.mFontCollection.Families)
                {
                    if (family2.Name == familyName)
                    {
                        return new Font(family2, emSize);
                    }
                }
            }
            catch
            {
            }
            return null;
        }
    }
}

