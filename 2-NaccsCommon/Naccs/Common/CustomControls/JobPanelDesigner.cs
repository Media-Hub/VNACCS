namespace Naccs.Common.CustomControls
{
    using System;
    using System.Collections;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;

    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    internal class JobPanelDesigner : DocumentDesigner
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
    }
}

