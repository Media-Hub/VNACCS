using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar.Metro.ColorTables;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroFormRenderer : MetroRenderer
    {
        public override void Render(MetroRendererInfo renderingInfo)
        {
            MetroAppForm form = renderingInfo.Control as MetroAppForm;
            BorderOverlay overlay = renderingInfo.Control as BorderOverlay;
            if (form == null && overlay != null) form = overlay.Parent as MetroAppForm;
            Graphics g = renderingInfo.PaintEventArgs.Graphics;
            MetroAppFormColorTable fct = renderingInfo.ColorTable.MetroAppForm;

            Thickness borderThickness = form.BorderThickness;
            BorderColors colors = form.BorderColor;
            if (borderThickness.IsZero && colors.IsEmpty)
            {
                // Get it from table
                borderThickness = form.IsGlassEnabled ? fct.BorderThickness : fct.BorderPlainThickness;
                colors = form.IsActive ? fct.BorderColors : fct.BorderColorsInactive;
            }

            if (overlay == null) // If overlay is being rendered it does not need fill
            {
                using (SolidBrush brush = new SolidBrush(form.BackColor))
                    g.FillRectangle(brush, new Rectangle(0, 0, form.Width, form.Height));
            }


            if (!borderThickness.IsZero && !colors.IsEmpty)
            {
                RectangleF br = new RectangleF(0, 0, form.Width, form.Height);
                DrawingHelpers.DrawBorder(g, br, borderThickness, colors);
            }
        }
    }
}
