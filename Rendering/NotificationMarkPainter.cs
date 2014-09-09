using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Collections;
using DevComponents.DotNetBar.Metro.Helpers;

namespace DevComponents.DotNetBar.Rendering
{
    internal class NotificationMarkPainter
    {
        #region Implementation
        private static readonly Size MarkSize = new Size(16, 16);
        private static readonly Point MarkOffset = new Point(3, 3);
        private static readonly Color DefaultBackColor1 = ColorScheme.GetColor(0xE80000);
        private static readonly Color DefaultBackColor2 = ColorScheme.GetColor(0xC50103);
        private static readonly Color DefaultTextColor = Color.White;

        public static Rectangle Paint(Graphics g, Rectangle targetBounds, eNotificationMarkPosition pos, string text)
        {
            return Paint(g, targetBounds, pos, text, MarkSize, MarkOffset);
        }
        public static Rectangle Paint(Graphics g, Rectangle targetBounds, eNotificationMarkPosition pos, string text, int markSize)
        {
            return Paint(g, targetBounds, pos, text, (markSize > 0 ? new Size(markSize, markSize) : MarkSize), MarkOffset);
        }
        public static Rectangle Paint(Graphics g, Rectangle targetBounds, eNotificationMarkPosition pos, string text, int markSize, Color markColor)
        {
            if (markColor.IsEmpty)
                return Paint(g, targetBounds, pos, text, (markSize > 0 ? new Size(markSize, markSize) : MarkSize), MarkOffset, DefaultBackColor1, DefaultBackColor2, DefaultTextColor);
            else
            {
                Color textColor, backColor1, backColor2;
                CreateColors(markColor, out backColor1, out backColor2, out textColor);
                return Paint(g, targetBounds, pos, text, (markSize > 0 ? new Size(markSize, markSize) : MarkSize), MarkOffset, backColor1, backColor2, textColor);
            }
        }
        private static void CreateColors(Color markColor, out Color backColor1, out Color backColor2, out Color textColor)
        {
            HSVColor baseHsv = ColorHelpers.ColorToHSV(markColor);
            ColorFunctions.HLSColor hslColor = ColorFunctions.RGBToHSL(markColor);
            textColor = hslColor.Lightness < .65 ? Color.White : Color.Black;
            backColor1 = ColorHelpers.HSVToColor(baseHsv.Hue, baseHsv.Saturation, baseHsv.Value + .1f);
            backColor2 = ColorHelpers.HSVToColor(baseHsv.Hue, baseHsv.Saturation, baseHsv.Value + .2f);
        }
        public static Rectangle Paint(Graphics g, Rectangle targetBounds, eNotificationMarkPosition pos, string text, Size markSize, Point offset)
        {
            return Paint(g, targetBounds, pos, text, markSize, offset, DefaultBackColor1, DefaultBackColor2, DefaultTextColor);
        }
        public static Rectangle Paint(Graphics g, Rectangle targetBounds, eNotificationMarkPosition pos, string text, Size markSize, Point offset, Color markColor)
        {
            if (markColor.IsEmpty)
                return Paint(g, targetBounds, pos, text, markSize, offset, DefaultBackColor1, DefaultBackColor2, DefaultTextColor);
            else
            {
                Color textColor, backColor1, backColor2;
                CreateColors(markColor, out backColor1, out backColor2, out textColor);
                return Paint(g, targetBounds, pos, text, markSize, offset, backColor1, backColor2, textColor);
            }
        }
        public static Rectangle Paint(Graphics g, Rectangle targetBounds, eNotificationMarkPosition pos, string text, Size markSize, Point offset, Color backColor1, Color backColor2, Color textColor)
        {
            if (markSize.IsEmpty) markSize = MarkSize; // Default it if empty
            Color borderColor = Color.White;
            int borderSize = 2;
            //Color backColor1 = DefaultBackColor1;
            //Color backColor2 = DefaultBackColor2;
            //Color textColor = DefaultTextColor;
            Font textFont = new Font("Microsoft Sans Serif", (text.Length > 1 ? 6.75f : 8.25f), FontStyle.Bold);

            Rectangle r;
            if (pos == eNotificationMarkPosition.TopRight)
                r = new Rectangle(targetBounds.Right - markSize.Width - 1 + offset.X, targetBounds.Y - offset.Y, markSize.Width, markSize.Height);
            else if (pos == eNotificationMarkPosition.TopLeft)
                r = new Rectangle(targetBounds.X - offset.X, targetBounds.Y - offset.Y, markSize.Width, markSize.Height);
            else if (pos == eNotificationMarkPosition.BottomLeft)
                r = new Rectangle(targetBounds.X - offset.X, targetBounds.Bottom - markSize.Height - 1 + offset.Y, markSize.Width, markSize.Height);
            else // eNotificationMarkPosition.BottomRight
                r = new Rectangle(targetBounds.Right - markSize.Width - 1 + offset.X, targetBounds.Bottom - markSize.Height + offset.Y, markSize.Width, markSize.Height);
            
            SmoothingMode sm = g.SmoothingMode;
            System.Drawing.Text.TextRenderingHint th = g.TextRenderingHint;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            Region oldClip = g.Clip;
            g.ResetClip();

            // Drop shadow
            Rectangle shadow = r;
            shadow.Inflate(1, 1);
            shadow.Offset(0, 3);
            using (GraphicsPath shadowPath = new GraphicsPath())
            {
                shadowPath.AddEllipse(shadow);
                using (PathGradientBrush brush = new PathGradientBrush(shadowPath))
                {
                    brush.CenterColor = Color.FromArgb(148, Color.Black);
                    brush.SurroundColors = new Color[] { Color.FromArgb(4,Color.Black)};
                    g.FillPath(brush, shadowPath);
                }
            }

            using (LinearGradientBrush brush = new LinearGradientBrush(r, backColor1, backColor2, 90))
                g.FillEllipse(brush, r);

            // Highlight
            Rectangle hr = new Rectangle(r.X - (int)(r.Width * .15), r.Y - (int)(r.Height * .15), (int)(r.Width * 1.3), (int)(r.Height * 1.32) / 2);
            using (LinearGradientBrush brush = new LinearGradientBrush(hr, Color.FromArgb(128, Color.White), Color.FromArgb(8, Color.White), 90))
            {
                GraphicsPath clipPath = new GraphicsPath();
                clipPath.AddEllipse(r);
                g.SetClip(clipPath);
                g.FillEllipse(brush, hr);
                g.ResetClip();
                clipPath.Dispose();
            }

            // Border
            using (Pen pen = new Pen(borderColor, borderSize))
                g.DrawEllipse(pen, r);

            if (!string.IsNullOrEmpty(text))
            {
                // Text
                Rectangle textBounds = r;
                if (text.Length == 1)
                    textBounds.Y++;
                using (StringFormat sf = (StringFormat)StringFormat.GenericDefault.Clone())
                {

                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    using (SolidBrush brush = new SolidBrush(textColor))
                        g.DrawString(text, textFont, brush, textBounds, sf);
                }
            }

            // Cleanup
            g.Clip = oldClip; oldClip.Dispose();
            textFont.Dispose();
            g.SmoothingMode = sm;
            g.TextRenderingHint = th;

            return r;
        }
        #endregion
    }
}
