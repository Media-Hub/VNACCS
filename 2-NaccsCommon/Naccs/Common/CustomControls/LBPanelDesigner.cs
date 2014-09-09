namespace Naccs.Common.CustomControls
{
    using System;
    using System.Collections;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    public class LBPanelDesigner : ParentControlDesigner
    {
        private bool checkControl(Control c)
        {
            if (!c.GetType().Equals(DesignControls.Label))
            {
                if (!(c is IContainerAttributes))
                {
                    return false;
                }
                foreach (Control control in c.Controls)
                {
                    if (!this.checkControl(control))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        protected override void OnDragDrop(DragEventArgs de)
        {
            if (de.Effect == DragDropEffects.Copy)
            {
                System.Type type = de.Data.GetType();
                if (type.Name == "BehaviorDataObject")
                {
                    ArrayList list = type.GetProperty("DragComponents").GetValue(de.Data, null) as ArrayList;
                    if ((list != null) && (list.Count > 0))
                    {
                        foreach (Control control in list)
                        {
                            if (!this.checkControl(control))
                            {
                                return;
                            }
                        }
                    }
                }
                base.OnDragDrop(de);
            }
            else
            {
                base.OnDragDrop(de);
            }
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            if ((this.Control is Panel) && ((this.Control as Panel).BorderStyle == BorderStyle.None))
            {
                Pen pen = new Pen(Color.Gray);
                pen.DashStyle = DashStyle.Dash;
                pe.Graphics.DrawRectangle(pen, 0, 0, pe.ClipRectangle.Width - 1, pe.ClipRectangle.Height - 1);
            }
            base.OnPaintAdornments(pe);
        }
    }
}

