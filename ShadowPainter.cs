using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents class that provides shadows to elements.
    /// </summary>
    public class ShadowPainter
    {
        /// <summary>
        /// Creates new instance of shadow painter.
        /// </summary>
        public ShadowPainter()
        {
        }

        private static System.Drawing.Drawing2D.GraphicsPath GetPath(Rectangle r)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(r.Left + 1, r.Y, r.Right - 1, r.Y);
            path.AddLine(r.Right - 1, r.Y, r.Right - 1, r.Y + 1);
            path.AddLine(r.Right - 1, r.Y + 1, r.Right, r.Y + 1);
            path.AddLine(r.Right, r.Y + 1, r.Right, r.Bottom - 1);
            path.AddLine(r.Right, r.Bottom - 1, r.Right - 1, r.Bottom - 1);
            path.AddLine(r.Right - 1, r.Bottom - 1, r.Right - 1, r.Bottom);
            path.AddLine(r.Right - 1, r.Bottom, r.Left + 1, r.Bottom);
            path.AddLine(r.Left + 1, r.Bottom, r.Left + 1, r.Bottom - 1);
            path.AddLine(r.Left + 1, r.Bottom - 1, r.Left, r.Bottom - 1);
            path.AddLine(r.Left, r.Bottom - 1, r.Left, r.Top + 1);
            return path;
        }

        public static void Paint(ShadowPaintInfo info)
        {
            Paint(info, 0);
        }
        public static void Paint(ShadowPaintInfo info, int alphaOffset)
        {
            Graphics g = info.Graphics;
            Region oldClip = g.Clip;
            if (info.ClipRectangle.IsEmpty)
                g.SetClip(info.Rectangle, CombineMode.Exclude);
            else
                g.SetClip(info.ClipRectangle, CombineMode.Exclude);
            Color[] clr = new Color[]{
									   Color.FromArgb(Math.Max(0, 14-alphaOffset),Color.Black),
									   Color.FromArgb(Math.Max(0,43-alphaOffset),Color.Black),
									   Color.FromArgb(Math.Max(0,84-alphaOffset),Color.Black),
									   Color.FromArgb(Math.Max(0,113-alphaOffset),Color.Black),
									   Color.FromArgb(Math.Max(0,128-alphaOffset),Color.Black)};


            Rectangle r = info.Rectangle;
            if (info.IsSquare)
            {
                r.Inflate(info.Size, info.Size);
                r.Width += info.Size;
            }
            else
            {
                r.Width--;
                r.Height--;
                int offset = info.Size / 2;
                r.Offset(offset + 1, offset);
                r.Width += (info.Size - offset);
                r.Height += (info.Size - offset);
            }
            //r.Width--;
            //r.Height--;
            //int offset = info.Size / 2;
            //r.Offset(offset,offset);
            //r.Width+=(info.Size-offset);
            //r.Height+=(info.Size-offset);

            for (int i = 0; i < info.Size; i++)
            {
                using (Pen pen = new Pen(clr[i], 1))
                {
                    using (GraphicsPath path = GetPath(r))
                        g.DrawPath(pen, path);
                    r.Inflate(-1, -1);
                }
            }

            g.Clip = oldClip;
        }

        public static void Paint2(ShadowPaintInfo info)
        {
            if (info.Size <= 2) return;
            Graphics g = info.Graphics;
            Color c = Color.FromArgb(128, Color.Black);
            Rectangle r = info.Rectangle;
            r.Offset(info.Size - 1, info.Size - 1);

            using (Bitmap bmp = new Bitmap(info.Size, info.Size))
            {
                using (Graphics bg = Graphics.FromImage(bmp))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        path.AddEllipse(0, 0, info.Size * 2, info.Size * 2);
                        using (PathGradientBrush brush = new PathGradientBrush(path))
                        {
                            brush.CenterColor = c;
                            brush.SurroundColors = new Color[] { Color.Transparent };
                            bg.FillRectangle(brush, new Rectangle(0, 0, info.Size, info.Size));
                        }
                    }
                }
                g.DrawImage(bmp, r.X, r.Y);
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                g.DrawImage(bmp, r.Right - info.Size, r.Y);
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                g.DrawImage(bmp, r.Right - info.Size, r.Bottom - info.Size);
                bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
                g.DrawImage(bmp, r.X, r.Bottom - info.Size);
            }
            SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.None;
            Rectangle rb = new Rectangle(r.X + info.Size, r.Y + 1, r.Width - info.Size * 2, info.Size - 1);
            using (LinearGradientBrush brush = DisplayHelp.CreateLinearGradientBrush(rb, Color.Transparent, c, 90))
                g.FillRectangle(brush, rb);
            rb.Offset(0, r.Height - info.Size - 1);
            using (LinearGradientBrush brush = DisplayHelp.CreateLinearGradientBrush(rb, c, Color.Transparent, 90))
                g.FillRectangle(brush, rb);

            rb = new Rectangle(r.X, r.Y + info.Size, info.Size, r.Height - info.Size * 2);
            using (LinearGradientBrush brush = DisplayHelp.CreateLinearGradientBrush(rb, Color.Transparent, c, 0))
                g.FillRectangle(brush, rb);

            rb.Offset(r.Width - info.Size - 1, 0);
            using (LinearGradientBrush brush = DisplayHelp.CreateLinearGradientBrush(rb, c, Color.Transparent, 0))
                g.FillRectangle(brush, rb);

            g.SmoothingMode = sm;
        }

        private static Bitmap _ShadowTemplate = null;
        private static Bitmap[] _ShadowParts = null;
        private static int LastUsedShadowVersion = 1;
        public static void Paint3(ShadowPaintInfo p)
        {
            Paint3(p, 1);
        }
        public static void Paint3(ShadowPaintInfo p, int shadowVersion)
        {
            if (LastUsedShadowVersion != shadowVersion && _ShadowTemplate != null)
            {
                _ShadowTemplate.Dispose();
                _ShadowTemplate = null;
                for (int i = 0; i < _ShadowParts.Length; i++)
                {
                    _ShadowParts[i].Dispose();
                }
                _ShadowParts = null;
            }
            if (_ShadowTemplate == null)
            {
                if(shadowVersion == 1)
                    _ShadowTemplate = BarFunctions.LoadBitmap("SystemImages.Shadow3px.png");
                else
                    _ShadowTemplate = BarFunctions.LoadBitmap("SystemImages.Shadow3px-2.png");
                _ShadowParts = new Bitmap[4];
                // Left part
                Bitmap bmp = new Bitmap(_ShadowTemplate, 1, 1);
                using (Graphics gb = Graphics.FromImage(bmp))
                {
                    gb.DrawImage(_ShadowTemplate, new Rectangle(0, 0, 1, 1), new Rectangle(0, 3, 1, 1), GraphicsUnit.Pixel);
                    _ShadowParts[0] = bmp;
                }

                // Top part
                bmp = new Bitmap(_ShadowTemplate, 1, 1);
                using (Graphics gb = Graphics.FromImage(bmp))
                {
                    gb.DrawImage(_ShadowTemplate, new Rectangle(0, 0, 1, 1), new Rectangle(3, 0, 1, 1), GraphicsUnit.Pixel);
                    _ShadowParts[1] = bmp;
                }

                // Right part
                bmp = new Bitmap(_ShadowTemplate, 3, 1);
                using (Graphics gb = Graphics.FromImage(bmp))
                {
                    gb.DrawImage(_ShadowTemplate, new Rectangle(0, 0, 3, 1), new Rectangle(_ShadowTemplate.Width - 3, 6, 3, 1), GraphicsUnit.Pixel);
                    _ShadowParts[2] = bmp;
                    //bmp.Save(@"d:\rightpart.png", System.Drawing.Imaging.ImageFormat.Png);
                }

                // Bottom part
                bmp = new Bitmap(_ShadowTemplate, 1, 3);
                using (Graphics gb = Graphics.FromImage(bmp))
                {
                    gb.DrawImage(_ShadowTemplate, new Rectangle(0, 0, 1, 3), new Rectangle(5, _ShadowTemplate.Height - 3, 1, 3), GraphicsUnit.Pixel);
                    _ShadowParts[3] = bmp;
                }
            }
            
            Graphics g = p.Graphics;
            SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.None;
            Rectangle r = p.Rectangle;
            g.DrawImage(_ShadowTemplate, new Rectangle(r.X, r.Y, 1, 1), new Rectangle(0, 0, 1, 1), GraphicsUnit.Pixel);

            // Left side
            using (TextureBrush brush = new TextureBrush(_ShadowParts[0], System.Drawing.Drawing2D.WrapMode.Tile))
            {
                g.FillRectangle(brush, new Rectangle(r.X, r.Y + 1, _ShadowParts[0].Width, r.Height - 4));
            }
            // Top side
            using (TextureBrush brush = new TextureBrush(_ShadowParts[1], System.Drawing.Drawing2D.WrapMode.Tile))
            {
                g.FillRectangle(brush, new Rectangle(r.X + 1, r.Y, r.Width - 4, _ShadowParts[1].Height));
            }
            // Top-Right corner
            g.DrawImage(_ShadowTemplate, new Rectangle(r.Right - 3, r.Y, 3, 4), new Rectangle(_ShadowTemplate.Width - 3, 0, 3, 4), GraphicsUnit.Pixel);
            // Right side
            using (TextureBrush brush = new TextureBrush(_ShadowParts[2], System.Drawing.Drawing2D.WrapMode.Tile))
            {
                Rectangle rightSide = new Rectangle(r.Right - _ShadowParts[2].Width, r.Y + 4, _ShadowParts[2].Width, r.Height - 7);
                brush.TranslateTransform(rightSide.X, rightSide.Y);
                g.FillRectangle(brush, rightSide);
            }
            // Bottom-right corner
            g.DrawImage(_ShadowTemplate, new Rectangle(r.Right - 4, r.Bottom - 3, 4, 3), new Rectangle(_ShadowTemplate.Width - 4, _ShadowTemplate.Height - 3, 4, 3), GraphicsUnit.Pixel);
            // Bottom side
            using (TextureBrush brush = new TextureBrush(_ShadowParts[3], System.Drawing.Drawing2D.WrapMode.Tile))
            {
                Rectangle bottomSide = new Rectangle(r.X + 4, r.Bottom - 3, r.Width - 8, _ShadowParts[3].Height);
                brush.TranslateTransform(bottomSide.X, bottomSide.Y);
                g.FillRectangle(brush, bottomSide);
            }
            // Bottom-left corner
            g.DrawImage(_ShadowTemplate, new Rectangle(r.X, r.Bottom - 3, 4, 3), new Rectangle(0, _ShadowTemplate.Height - 3, 4, 3), GraphicsUnit.Pixel);
            g.SmoothingMode = sm;
        }
    }

    #region ShadowPaintInfo class
    /// <summary>
    /// Represents class that provides display context for shadow painter.
    /// </summary>
    public class ShadowPaintInfo
    {
        public System.Drawing.Graphics Graphics = null;
        public System.Drawing.Rectangle Rectangle = Rectangle.Empty;
        public int Size = 3;
        public System.Drawing.Rectangle ClipRectangle = Rectangle.Empty;
        public bool IsSquare = false;
    }
    #endregion
}
