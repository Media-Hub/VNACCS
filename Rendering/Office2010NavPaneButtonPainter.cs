using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar.Rendering
{
    internal class Office2010NavPaneButtonPainter : Office2007ButtonItemPainter
    {
        protected override void PaintStateBackground(ButtonItem button, Graphics g, Office2007ButtonItemStateColorTable stateColors, Rectangle r, IShapeDescriptor shape, bool isDefault, bool paintBorder)
        {
            if (stateColors == null || stateColors.Background == null || stateColors.Background.IsEmpty)
                return;

            Rectangle shadowRect = r;
            ShadowPaintInfo spi = new ShadowPaintInfo();
            spi.Graphics = g;
            spi.Rectangle = shadowRect;
            ShadowPainter.Paint3(spi);

            r.Width -= 4;
            r.Height -= 4;
            r.Offset(1, 1);

            DisplayHelp.FillRectangle(g, r, stateColors.Background);
            if (stateColors.BottomBackgroundHighlight != null && !stateColors.BottomBackgroundHighlight.IsEmpty)
            {
                Rectangle ellipse = new Rectangle(r.X, r.Y + r.Height / 2 - 2, r.Width, r.Height + 4);
                GraphicsPath path = new GraphicsPath();
                path.AddEllipse(ellipse);
                PathGradientBrush brush = new PathGradientBrush(path);
                brush.CenterColor = stateColors.BottomBackgroundHighlight.Start;
                brush.SurroundColors = new Color[] { stateColors.BottomBackgroundHighlight.End };
                brush.CenterPoint = new PointF(ellipse.X + ellipse.Width / 2, r.Bottom);
                Blend blend = new Blend();
                blend.Factors = new float[] { 0f, .5f, .6f };
                blend.Positions = new float[] { .0f, .4f, 1f };
                brush.Blend = blend;

                g.FillRectangle(brush, r);
                brush.Dispose();
                path.Dispose();
            }

            DisplayHelp.DrawGradientRectangle(g, r, stateColors.OuterBorder, 1);
            r.Inflate(-1, -1);
            DisplayHelp.DrawGradientRectangle(g, r, stateColors.InnerBorder, 1);
        }
    }
}
