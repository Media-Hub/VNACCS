using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar.Metro.ColorTables;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroStatusBarPainter : MetroRenderer
    {
        public override void Render(MetroRendererInfo renderingInfo)
        {
            MetroStatusBar bar = (MetroStatusBar)renderingInfo.Control;
            Graphics g = renderingInfo.PaintEventArgs.Graphics;
            MetroStatusBarColorTable ct = renderingInfo.ColorTable.MetroStatusBar;
            Rectangle bounds = bar.ClientRectangle;

            if (ct.BackgroundStyle != null)
            {
                ElementStyleDisplayInfo di = new ElementStyleDisplayInfo(ct.BackgroundStyle, g, bounds);
                ElementStyleDisplay.PaintBackground(di);
            }

            if (ct.TopBorders != null && ct.TopBorders.Length > 0)
            {
                for (int i = 0; i < ct.TopBorders.Length; i++)
                {
                    using (Pen pen = new Pen(ct.TopBorders[i]))
                        g.DrawLine(pen, bounds.X, bounds.Y + i, bounds.Right, bounds.Y + i);
                }
            }

            if (ct.BottomBorders != null && ct.BottomBorders.Length > 0)
            {
                for (int i = 0; i < ct.BottomBorders.Length; i++)
                {
                    using (Pen pen = new Pen(ct.BottomBorders[i]))
                        g.DrawLine(pen, bounds.X, bounds.Bottom - i - 1, bounds.Right, bounds.Bottom - i - 1);
                }
            }

            if (bar.ResizeHandleVisible)
            {
                Form form = bar.FindForm();
                if (form != null && form.WindowState == FormWindowState.Normal)
                    DevComponents.DotNetBar.Rendering.ResizeHandlePainter.DrawResizeHandle(
                        g, bounds, ct.ResizeMarkerLightColor, ct.ResizeMarkerColor, (bar.RightToLeft == RightToLeft.Yes));
            }

#if TRIAL
            Rectangle tr = bounds;
            tr.Inflate(-2, -2);
            using(Font font=new Font(bar.Font.FontFamily, 8f, FontStyle.Regular))
                TextDrawing.DrawString(g, "DotNetBar Trial", font, Color.FromArgb(80, Color.Black), tr, eTextFormat.Bottom | eTextFormat.HorizontalCenter);
#endif
        }
    }
}
