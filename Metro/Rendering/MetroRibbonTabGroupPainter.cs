using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroRibbonTabGroupPainter : Office2007RibbonTabGroupPainter
    {
        protected override void PaintTabGroupBackground(System.Drawing.Graphics g, DevComponents.DotNetBar.Rendering.Office2007RibbonTabGroupColorTable colorTable, System.Drawing.Rectangle bounds, System.Drawing.Rectangle groupBounds, bool glassEnabled)
        {
            DisplayHelp.FillRectangle(g, groupBounds, colorTable.Background);
            Rectangle top = new Rectangle(groupBounds.X, groupBounds.Y, groupBounds.Width, 3);
            DisplayHelp.FillRectangle(g, top, colorTable.Border);
        }
    }
}
