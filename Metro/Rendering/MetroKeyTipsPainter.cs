using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroKeyTipsPainter : Office2007KeyTipsPainter
    {
        public override void PaintKeyTips(KeyTipsRendererEventArgs e)
        {
            Rectangle r = e.Bounds;
            r.Inflate(1, 1);

            Color textColor = ColorTable.KeyTips.KeyTipText;
            Color backColor = ColorTable.KeyTips.KeyTipBackground;

            if (e.ReferenceObject is BaseItem && !((BaseItem)e.ReferenceObject).Enabled)
            {
                int alpha = 128;
                backColor = Color.FromArgb(alpha, backColor);
                textColor = Color.FromArgb(alpha, textColor);
            }

            Graphics g = e.Graphics;
            string keyTip = e.KeyTip;
            Font font = e.Font;

            using (SolidBrush brush = new SolidBrush(backColor))
                DisplayHelp.FillRoundedRectangle(g, brush, r, 1);
            using (Pen pen = new Pen(textColor, 1))
                DisplayHelp.DrawRoundedRectangle(g, pen, r, 1);

            //DisplayHelp.FillRectangle(g, r, backColor);
            //using (Pen pen = new Pen(textColor, 1))
            //    DisplayHelp.DrawRoundedRectangle(g, pen, r, 2);
            TextDrawing.DrawString(g, keyTip, font, textColor, r, eTextFormat.HorizontalCenter | eTextFormat.VerticalCenter);
        }
    }
}
