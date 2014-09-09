using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal static class DrawingHelpers
    {
        /// <summary>
        /// Draws the border.
        /// </summary>
        /// <param name="g">Graphics canvas.</param>
        /// <param name="bounds">Bounds for border.</param>
        /// <param name="borderThickness">Border thickness.</param>
        /// <param name="borderColor">Border color.</param>
        public static void DrawBorder(Graphics g, RectangleF bounds, Thickness borderThickness, BorderColors borderColor)
        {
            if (borderColor.IsEmpty || borderThickness.IsZero) return;

            bounds.Width -= (float)borderThickness.Right / 2;
            bounds.Height -= (float)borderThickness.Bottom / 2;
            if (borderThickness.Left > 0d && !borderColor.Left.IsEmpty)
            {
                using (Pen pen = new Pen(borderColor.Left, (float)borderThickness.Left))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    g.DrawLine(pen, bounds.X, bounds.Y, bounds.X, bounds.Bottom);
                }
            }

            if (borderThickness.Top > 0d && !borderColor.Top.IsEmpty)
            {
                using (Pen pen = new Pen(borderColor.Top, (float)borderThickness.Top))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    g.DrawLine(pen, bounds.X, bounds.Y, bounds.Right, bounds.Y);
                }
            }

            if (borderThickness.Right > 0d && !borderColor.Right.IsEmpty)
            {
                using (Pen pen = new Pen(borderColor.Right, (float)borderThickness.Right))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    g.DrawLine(pen, bounds.Right, bounds.Y, bounds.Right, bounds.Bottom + (float)borderThickness.Bottom / 2);
                }
            }

            if (borderThickness.Bottom > 0d && !borderColor.Bottom.IsEmpty)
            {
                using (Pen pen = new Pen(borderColor.Bottom, (float)borderThickness.Bottom))
                {
                    pen.Alignment = System.Drawing.Drawing2D.PenAlignment.Inset;
                    g.DrawLine(pen, bounds.X, bounds.Bottom, bounds.Right, bounds.Bottom);
                }
            }
        }
        /// <summary>
        /// Draws background.
        /// </summary>
        /// <param name="g">Graphics canvas.</param>
        /// <param name="bounds">Background bounds.</param>
        /// <param name="color">Background color</param>
        public static void DrawBackground(Graphics g, RectangleF bounds, string color)
        {
            if (string.IsNullOrEmpty(color)) return;

            using (SolidBrush brush = new SolidBrush(ColorScheme.GetColor(color)))
                g.FillRectangle(brush, bounds);

        }

        /// <summary>
        /// Deflates the rectangle by the border thickness.
        /// </summary>
        /// <param name="bounds">Rectangle.</param>
        /// <param name="borderThickness">Border thickness</param>
        /// <returns>Rectangle deflated by the border thickness</returns>
        public static RectangleF Deflate(RectangleF bounds, Thickness borderThickness)
        {
            if (borderThickness.IsZero) return bounds;

            bounds.X += (float)borderThickness.Left;
            bounds.Width -= (float)(borderThickness.Left + borderThickness.Right);
            bounds.Y += (float)borderThickness.Top;
            bounds.Height -= (float)(borderThickness.Top + borderThickness.Bottom);

            return bounds;
        }


    }
}
