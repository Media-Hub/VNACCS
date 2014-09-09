namespace Naccs.Common.Function
{
    using System;
    using System.Windows.Forms;

    public class ControlFunc
    {
        public static void SelectNextControlEx(Control ctl, bool foward)
        {
            for (Control control = ctl.Parent; control != null; control = control.Parent)
            {
                if (control is Form)
                {
                    Form form = (Form) control;
                    form.ActiveControl = null;
                    control.SelectNextControl(ctl, foward, true, true, true);
                    return;
                }
            }
        }
    }
}

