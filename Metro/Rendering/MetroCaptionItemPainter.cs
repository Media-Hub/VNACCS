using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar.Rendering;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroCaptionItemPainter : Office2007SystemCaptionItemPainter
    {
        #region Internal Implementation
        protected override Rectangle GetSignRect(Rectangle r, Size s)
        {
            if (r.Height < 10)
                return Rectangle.Empty;

            return new Rectangle(r.X + (r.Width - s.Width) / 2, r.Bottom - r.Height / 4 - s.Height - 3, s.Width, s.Height);
        }

        protected override void PaintMinimize(Graphics g, Rectangle r, Office2007SystemButtonStateColorTable ct, bool isEnabled)
        {
            Size s = new Size(9, 2);
            Rectangle rm = GetSignRect(r, s);
            if(isEnabled)
                DisplayHelp.FillRectangle(g, rm, ct.Foreground);
            else
                DisplayHelp.FillRectangle(g, rm, GetDisabledColor(ct.Foreground.Start), GetDisabledColor(ct.Foreground.End), ct.Foreground.GradientAngle);
        }

        internal static Color GetDisabledColor(Color color)
        {
            if(color.IsEmpty) return color;
            return Color.FromArgb(128, color);
        }


        protected override void PaintRestore(Graphics g, Rectangle r, Office2007SystemButtonStateColorTable ct, bool isEnabled)
        {
            SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.None;

            Size s = new Size(10, 10);
            Rectangle rm = GetSignRect(r, s);
            Region oldClip = g.Clip;

            LinearGradientColorTable buttonTable = isEnabled ? ct.Foreground : new LinearGradientColorTable(GetDisabledColor(ct.Foreground.Start), GetDisabledColor(ct.Foreground.End), ct.Foreground.GradientAngle);
            using (Brush fill = DisplayHelp.CreateBrush(rm, ct.Foreground))
            {
                Rectangle inner = new Rectangle(rm.X + 4, rm.Y + 2, 6, 4);
                g.SetClip(inner, CombineMode.Exclude);
                g.SetClip(new Rectangle(rm.X + 1, rm.Y + 5, 6, 4), CombineMode.Exclude);

                g.FillRectangle(fill, rm.X + 3, rm.Y, 8, 7);
                g.ResetClip();

                inner = new Rectangle(rm.X + 1, rm.Y + 5, 6, 4);
                g.SetClip(inner, CombineMode.Exclude);
                g.FillRectangle(fill, rm.X, rm.Y + 3, 8, 7);
                g.ResetClip();
            }
            if (oldClip != null)
            {
                g.Clip = oldClip;
                oldClip.Dispose();
            }
            g.SmoothingMode = sm;
        }

        protected override void PaintMaximize(Graphics g, Rectangle r, Office2007SystemButtonStateColorTable ct, bool isEnabled)
        {
            Size s = new Size(10, 10);
            Rectangle rm = GetSignRect(r, s);
            Region oldClip = g.Clip;

            Rectangle inner = new Rectangle(rm.X + 1, rm.Y + 3, ct.DarkShade.IsEmpty ? 8 : 7, ct.DarkShade.IsEmpty ? 6 : 5);
            g.SetClip(inner, CombineMode.Exclude);
            if(isEnabled)
                DisplayHelp.FillRectangle(g, rm, ct.Foreground);
            else
                DisplayHelp.FillRectangle(g, rm, GetDisabledColor(ct.Foreground.Start), GetDisabledColor(ct.Foreground.End), ct.Foreground.GradientAngle);

            if (oldClip != null)
            {
                g.Clip = oldClip;
                oldClip.Dispose();
            }
        }

        protected override void PaintClose(Graphics g, Rectangle r, Office2007SystemButtonStateColorTable ct, bool isEnabled)
        {
            SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.Default;

            Size s = new Size(8, 8);
            Rectangle rm = GetSignRect(r, s);

            Rectangle r1 = rm;
            r1.Offset(0, -1);

            if (isEnabled)
            {
                using (Pen pen = new Pen(ct.Foreground.Start, 2))
                {
                    g.DrawLine(pen, r1.X, r1.Y, r1.Right, r1.Bottom);
                    g.DrawLine(pen, r1.Right, r1.Y, r1.X, r1.Bottom);
                }
            }
            else
            {
                using (Pen pen = new Pen(GetDisabledColor(ct.Foreground.Start), 2))
                {
                    g.DrawLine(pen, r1.X, r1.Y, r1.Right, r1.Bottom);
                    g.DrawLine(pen, r1.Right, r1.Y, r1.X, r1.Bottom);
                }
            }

            g.SmoothingMode = sm;
        }

        protected override void PaintHelp(Graphics g, Rectangle r, Office2007SystemButtonStateColorTable ct, bool isEnabled)
        {
            SmoothingMode sm = g.SmoothingMode;
            TextRenderingHint th = g.TextRenderingHint;
            g.SmoothingMode = SmoothingMode.Default;
            g.TextRenderingHint = TextRenderingHint.ClearTypeGridFit;
#if FRAMEWORK20
            using (Font font = new Font(SystemFonts.DefaultFont, FontStyle.Bold))
#else
			using(Font font = new Font("Arial", 10, FontStyle.Bold))
#endif
            {
                Size s = TextDrawing.MeasureString(g, "?", font);
                s.Width += 4;
                s.Height -= 2;
                Rectangle rm = GetSignRect(r, s);

                rm.Offset(1, 1);
                Color color = isEnabled ? ct.DarkShade : GetDisabledColor(ct.DarkShade);
                using (SolidBrush brush = new SolidBrush(color))
                    g.DrawString("?", font, brush, rm);
                rm.Offset(-1, -1);
                color = isEnabled ? ct.Foreground.Start : GetDisabledColor(ct.Foreground.Start);
                using (SolidBrush brush = new SolidBrush(color))
                    g.DrawString("?", font, brush, rm);

            }
            g.SmoothingMode = sm;
            g.TextRenderingHint = th;
        }

        #endregion
    }
}
