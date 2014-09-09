using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar.Metro.ColorTables;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroToolbarPainter : MetroRenderer
    {
        public override void Render(MetroRendererInfo renderingInfo)
        {
            MetroToolbar bar = (MetroToolbar)renderingInfo.Control;
            Graphics g = renderingInfo.PaintEventArgs.Graphics;
            MetroToolbarColorTable ct = renderingInfo.ColorTable.MetroToolbar;
            Rectangle bounds = bar.ClientRectangle;

            if (ct.BackgroundStyle != null)
            {
                ElementStyleDisplayInfo di = new ElementStyleDisplayInfo(ct.BackgroundStyle, g, bounds);
                ElementStyleDisplay.PaintBackground(di);
            }

#if TRIAL
            if (bar.Expanded)
            {
                Rectangle tr = bounds;
                tr.Inflate(-2, -2);
                using (Font font = new Font(bar.Font.FontFamily, 8f, FontStyle.Regular))
                    TextDrawing.DrawString(g, "DotNetBar Trial", font, Color.FromArgb(32, Color.Black), tr, eTextFormat.Bottom | eTextFormat.Right);
            }
#endif

        }
    }
}
