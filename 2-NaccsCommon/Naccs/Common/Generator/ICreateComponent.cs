namespace Naccs.Common.Generator
{
    using Naccs.Common.CustomControls;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Xml;

    public interface ICreateComponent
    {
        void ClonePanel(List<Control> controlList, Control ctl, int cntX, int cntY, int SpaceX, int SpaceY, int Max);
        void CloneTitlePanel(List<Control> controlList, Control ctl, int cntX, int cntY, int SpaceX, int SpaceY);
        Component CreateComponent(List<Control> controlList, System.Type type);
        void CreateNavigator(List<Control> controlList, XmlReader xml);
        BindingSourceEx GetBs(string rep_id);
        void SetBinding(Control control, string pro, string id, string rep_id);
    }
}

