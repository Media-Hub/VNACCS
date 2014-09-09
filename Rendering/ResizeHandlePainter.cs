using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Rendering
{
    internal static class ResizeHandlePainter
    {
        internal static void DrawResizeHandle(Graphics g, Rectangle statusBarBounds, Color lightColor, Color color, bool rightToLeft)
        {
            int direction = 1;
            Point startLoc = new Point(statusBarBounds.Right, statusBarBounds.Bottom);
            if (rightToLeft)
            {
                direction = -1;
                startLoc = new Point(0, statusBarBounds.Bottom - 2);
            }

            using (Pen pen = new Pen(lightColor, 1))
            {
                g.DrawLine(pen, startLoc.X - 2 * direction, startLoc.Y - 2,
                    startLoc.X - 3 * direction, startLoc.Y - 2);
                g.DrawLine(pen, startLoc.X - 6 * direction, startLoc.Y - 2,
                    startLoc.X - 7 * direction, startLoc.Y - 2);
                g.DrawLine(pen, startLoc.X - 10 * direction, startLoc.Y - 2,
                    startLoc.X - 11 * direction, startLoc.Y - 2);
                g.DrawLine(pen, startLoc.X - 2 * direction, startLoc.Y - 2,
                    startLoc.X - 2 * direction, startLoc.Y - 3);
                g.DrawLine(pen, startLoc.X - 6 * direction, startLoc.Y - 2,
                    startLoc.X - 6 * direction, startLoc.Y - 3);
                g.DrawLine(pen, startLoc.X - 10 * direction, startLoc.Y - 2,
                    startLoc.X - 10 * direction, startLoc.Y - 3);
                g.DrawLine(pen, startLoc.X - 2 * direction, startLoc.Y - 6,
                    startLoc.X - 3 * direction, startLoc.Y - 6);
                g.DrawLine(pen, startLoc.X - 6 * direction, startLoc.Y - 6,
                    startLoc.X - 7 * direction, startLoc.Y - 6);
                g.DrawLine(pen, startLoc.X - 2 * direction, startLoc.Y - 6,
                    startLoc.X - 2 * direction, startLoc.Y - 7);
                g.DrawLine(pen, startLoc.X - 6 * direction, startLoc.Y - 6,
                    startLoc.X - 6 * direction, startLoc.Y - 7);
                g.DrawLine(pen, startLoc.X - 2 * direction, startLoc.Y - 10,
                    startLoc.X - 3 * direction, startLoc.Y - 10);
                g.DrawLine(pen, startLoc.X - 2 * direction, startLoc.Y - 10,
                    startLoc.X - 2 * direction, startLoc.Y - 11);
            }

            // draw dark squares
            using (Pen pen = new Pen(color, 1))
            {
                g.DrawRectangle(pen, startLoc.X - 4 * direction, startLoc.Y - 4, 1, 1);
                g.DrawRectangle(pen, startLoc.X - 8 * direction, startLoc.Y - 4, 1, 1);
                g.DrawRectangle(pen, startLoc.X - 12 * direction, startLoc.Y - 4, 1, 1);
                g.DrawRectangle(pen, startLoc.X - 4 * direction, startLoc.Y - 8, 1, 1);
                g.DrawRectangle(pen, startLoc.X - 8 * direction, startLoc.Y - 8, 1, 1);
                g.DrawRectangle(pen, startLoc.X - 4 * direction, startLoc.Y - 12, 1, 1);
            }
        }
    }
}
