using System;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Text;

namespace DevComponents.DotNetBar.SuperGrid
{
    internal class TextDrawing
    {
        #region Public / internal variables

        public static bool UseTextRenderer = true;
        public static bool TextDrawingEnabled = true;

        internal static bool UseGenericDefault;

        #endregion

        #region DrawString

        public static void DrawString(Graphics g,
            string text, Font font, Color color, int x, int y, eTextFormat format)
        {
            DrawString(g, text, font, color, new Rectangle(x, y, 0, 0), format);
        }

        public static void DrawString(Graphics g,
            string text, Font font, Color color, Rectangle bounds, eTextFormat format)
        {
            if (UseTextRenderer && (format & eTextFormat.Vertical) == 0)
                TextRenderer.DrawText(g, text, font, bounds, color, GetTextFormatFlags(format));
            else
                DrawStringLegacy(g, text, font, color, bounds, format);
        }

        #endregion

        #region DrawStringLegacy

        public static void DrawStringLegacy(Graphics g,
            string text, Font font, Color color, Rectangle bounds, eTextFormat format)
        {
            if (color.IsEmpty == false && TextDrawingEnabled == true)
            {
                using (SolidBrush brush = new SolidBrush(color))
                {
                    using (StringFormat sf = GetStringFormat(format))
                        g.DrawString(text, font, brush, bounds, sf);
                }
            }
        }

        #endregion

        #region MeasureString

        public static Size MeasureString(Graphics g,
            string text, Font font)
        {
            return MeasureString(g, text, font, Size.Empty, eTextFormat.Default);
        }

        public static Size MeasureString(Graphics g,
            string text, Font font, int proposedWidth, eTextFormat format)
        {
            return MeasureString(g, text, font, new Size(proposedWidth, 0), format);
        }

        public static Size MeasureString(Graphics g,
            string text, Font font, int proposedWidth)
        {
            return MeasureString(g, text, font, new Size(proposedWidth, 0), eTextFormat.Default);
        }

        public static Size MeasureString(Graphics g,
            string text, Font font, Size proposedSize, eTextFormat format)
        {
            if (UseTextRenderer && (format & eTextFormat.Vertical) == 0)
            {
                format = format & ~(format & eTextFormat.VerticalCenter); // Bug in .NET Framework 2.0
                format = format & ~(format & eTextFormat.Bottom); // Bug in .NET Framework 2.0
                format = format & ~(format & eTextFormat.HorizontalCenter); // Bug in .NET Framework 2.0
                format = format & ~(format & eTextFormat.Right); // Bug in .NET Framework 2.0
                format = format & ~(format & eTextFormat.EndEllipsis); // Bug in .NET Framework 2.0

                return (Size.Ceiling(TextRenderer.MeasureText(g, text, font, proposedSize, GetTextFormatFlags(format))));
            }

            using (StringFormat sf = GetStringFormat(format))
                return Size.Ceiling(g.MeasureString(text, font, proposedSize, sf));
        }

        #endregion

        #region MeasureStringLegacy

        public static Size MeasureStringLegacy(Graphics g,
            string text, Font font, Size proposedSize, eTextFormat format)
        {
            using (StringFormat sf = GetStringFormat(format))
                return (g.MeasureString(text, font, proposedSize, sf).ToSize());
        }

        #endregion

        #region TranslateHorizontal

        public static eTextFormat TranslateHorizontal(StringAlignment align)
        {
            if (align == StringAlignment.Center)
                return (eTextFormat.HorizontalCenter);

            if (align == StringAlignment.Far)
                return (eTextFormat.Right);

            return (eTextFormat.Default);
        }

        #endregion

        #region TranslateVertical

        public static eTextFormat TranslateVertical(StringAlignment align)
        {
            if (align == StringAlignment.Center)
                return (eTextFormat.VerticalCenter);

            if (align == StringAlignment.Far)
                return (eTextFormat.Bottom);

            return (eTextFormat.Default);
        }

        #endregion

        #region GetTextFormatFlags

        private static TextFormatFlags GetTextFormatFlags(eTextFormat format)
        {
            format |= eTextFormat.PreserveGraphicsTranslateTransform |
                eTextFormat.PreserveGraphicsClipping;

            if ((format & eTextFormat.SingleLine) == eTextFormat.SingleLine &&
                (format & eTextFormat.WordBreak) == eTextFormat.WordBreak)
            {
                format = format & ~(format & eTextFormat.SingleLine);
            }

            return (TextFormatFlags)format;
        }

        #endregion

        #region GetStringFormat

        public static StringFormat GetStringFormat(eTextFormat format)
        {
            StringFormat sf = new StringFormat(UseGenericDefault
                ? StringFormat.GenericDefault : StringFormat.GenericTypographic);

            if (format == eTextFormat.Default)
                return sf;

            if ((format & eTextFormat.HorizontalCenter) == eTextFormat.HorizontalCenter)
                sf.Alignment = StringAlignment.Center;

            else if ((format & eTextFormat.Right) == eTextFormat.Right)
                sf.Alignment = StringAlignment.Far;

            if ((format & eTextFormat.VerticalCenter) == eTextFormat.VerticalCenter)
                sf.LineAlignment = StringAlignment.Center;

            else if ((format & eTextFormat.Bottom) == eTextFormat.Bottom)
                sf.LineAlignment = StringAlignment.Far;

            if ((format & eTextFormat.EndEllipsis) == eTextFormat.EndEllipsis)
                sf.Trimming = StringTrimming.EllipsisCharacter;
            else
                sf.Trimming = StringTrimming.Character;

            if ((format & eTextFormat.HidePrefix) == eTextFormat.HidePrefix)
                sf.HotkeyPrefix = HotkeyPrefix.Hide;
            else if ((format & eTextFormat.NoPrefix) == eTextFormat.NoPrefix)
                sf.HotkeyPrefix = HotkeyPrefix.None;
            else
                sf.HotkeyPrefix = HotkeyPrefix.Show;

            if ((format & eTextFormat.WordBreak) == eTextFormat.WordBreak)
                sf.FormatFlags = sf.FormatFlags & ~(sf.FormatFlags & StringFormatFlags.NoWrap);
            else
                sf.FormatFlags |= StringFormatFlags.NoWrap;

            if ((format & eTextFormat.LeftAndRightPadding) == eTextFormat.LeftAndRightPadding)
                sf.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;

            if ((format & eTextFormat.RightToLeft) == eTextFormat.RightToLeft)
                sf.FormatFlags |= StringFormatFlags.DirectionRightToLeft;

            if ((format & eTextFormat.Vertical) == eTextFormat.Vertical)
                sf.FormatFlags |= StringFormatFlags.DirectionVertical;

            if ((format & eTextFormat.NoClipping) == eTextFormat.NoClipping)
                sf.FormatFlags |= StringFormatFlags.NoClip;

            return (sf);
        }

        #endregion
    }

    #region enums

    #region eTextFormat
    [Flags]
    internal enum eTextFormat
    {
        Bottom = 8,
        Default = 0,
        EndEllipsis = 0x8000,
        ExpandTabs = 0x40,
        ExternalLeading = 0x200,
        GlyphOverhangPadding = 0,
        HidePrefix = 0x100000,
        HorizontalCenter = 1,
        Internal = 0x1000,
        Left = 0,
        LeftAndRightPadding = 0x20000000,
        ModifyString = 0x10000,
        NoClipping = 0x100,
        NoFullWidthCharacterBreak = 0x80000,
        NoPadding = 0x10000000,
        NoPrefix = 0x800,
        PathEllipsis = 0x4000,
        PrefixOnly = 0x200000,
        PreserveGraphicsClipping = 0x1000000,
        PreserveGraphicsTranslateTransform = 0x2000000,
        Right = 2,
        RightToLeft = 0x20000,
        SingleLine = 0x20,
        TextBoxControl = 0x2000,
        Top = 0,
        VerticalCenter = 4,
        WordBreak = 0x10,
        WordEllipsis = 0x40000,
        Vertical = 0x40000000
    }
    #endregion

    #endregion
}
