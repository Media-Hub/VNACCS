using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.Drawing;
using DevComponents.DotNetBar.Metro;
using System.Windows.Forms.Design;

namespace DevComponents.DotNetBar.Design
{
    public class MetroTabPanelDesigner : PanelControlDesigner
    {
        #region Internal Implementation
        public override SelectionRules SelectionRules
        {
            get { return (SelectionRules.Locked | SelectionRules.Visible); }
        }

        protected override void SetDesignTimeDefaults()
        {
//            RibbonPanel p = this.Control as RibbonPanel;
//#if FRAMEWORK20
//            p.Padding = new System.Windows.Forms.Padding(3, 0, 3, 3);
//#else
//            p.DockPadding.Left = 3;
//            p.DockPadding.Right = 3;
//            p.DockPadding.Bottom = 3;
//#endif
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                return new DesignerVerbCollection();
            }
        }

        /// <summary>
        /// Draws design-time border around the panel when panel does not have one.
        /// </summary>
        /// <param name="g"></param>
        protected override void DrawBorder(Graphics g)
        {
            MetroTabPanel panel = this.Control as MetroTabPanel;
            if (panel == null) return;
            Rectangle r = panel.ClientRectangle;
            using (Pen pen = new Pen(Color.WhiteSmoke, 1))
            {
                pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
                r.Width--;
                r.Height--;
                g.DrawRectangle(pen, r);
            }
        }
        #endregion
    }
}
