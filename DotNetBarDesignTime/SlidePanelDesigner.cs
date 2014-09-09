using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevComponents.DotNetBar.Controls;

namespace DevComponents.DotNetBar.Design
{
    [ToolboxItemFilter("System.Windows.Forms.MainMenu", ToolboxItemFilterType.Prevent), ToolboxItemFilter("System.Windows.Forms.UserControl", ToolboxItemFilterType.Custom)]
    public class SlidePanelDesigner : ScrollableControlDesigner
    {
        public SlidePanelDesigner()
        {
            base.AutoResizeHandles = true;
        }

        protected virtual void DrawBorder(Graphics graphics)
        {
            SlidePanel component = (SlidePanel)base.Component;
            if ((component != null) && component.Visible)
            {
                Pen borderPen = this.BorderPen;
                Rectangle clientRectangle = this.Control.ClientRectangle;
                clientRectangle.Width--;
                clientRectangle.Height--;
                graphics.DrawRectangle(borderPen, clientRectangle);
                borderPen.Dispose();
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            SlidePanel component = (SlidePanel)base.Component;
            if (component.BorderStyle == BorderStyle.None)
            {
                this.DrawBorder(pe.Graphics);
            }
            base.OnPaintAdornments(pe);
        }

        // Properties
        protected Pen BorderPen
        {
            get
            {
                Color color = (this.Control.BackColor.GetBrightness() < 0.5) ? ControlPaint.Light(this.Control.BackColor) : ControlPaint.Dark(this.Control.BackColor);
                Pen pen = new Pen(color);
                pen.DashStyle = DashStyle.Dash;
                return pen;
            }
        }
    }
}