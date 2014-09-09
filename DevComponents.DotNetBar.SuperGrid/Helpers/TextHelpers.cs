using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Provides helpers when working with text.
    /// </summary>
    internal static class TextHelper
    {
        private static int _textMarkupCultureSpecific = 3;

        /// <summary>
        /// Get or sets the text-markup padding for text
        /// measurement when running on Japanese version of Windows.
        /// </summary>
        public static int TextMarkupCultureSpecificPadding
        {
            get { return _textMarkupCultureSpecific; }
            set { _textMarkupCultureSpecific = value; }
        }

        /// <summary>
        /// MeasureText always adds about 1/2 em width of white space on the right,
        /// even when NoPadding is specified. It returns zero for an empty string.
        /// To get the precise string width, measure the width of a string containing a
        /// single period and subtract that from the width of our original string plus a period.
        /// </summary>
        public static Size MeasureText(Graphics g,
            string s, Font font, Size csize, eTextFormat tf)
        {
            return (TextDrawing.MeasureString(g, s, font, csize, tf));

            if (font.Italic == true)
                return (TextDrawing.MeasureString(g, s, font, csize, tf));

            Size sz1 = TextDrawing.MeasureString(g, ".", font, csize, tf);
            Size sz2 = TextDrawing.MeasureString(g, s + ".", font, csize, tf);

            return (new Size(sz2.Width - sz1.Width, sz2.Height));
        }

        public static Size MeasureText(Graphics g, string s, Font font)
        {
            return (TextDrawing.MeasureString(g, s, font));

            if (font.Italic == true)
                return (TextDrawing.MeasureString(g, s, font));

            Size sz1 = TextDrawing.MeasureString(g, ".", font);
            Size sz2 = TextDrawing.MeasureString(g, s + ".", font);

            return (new Size(sz2.Width - sz1.Width, sz2.Height));
        }
    }
}
