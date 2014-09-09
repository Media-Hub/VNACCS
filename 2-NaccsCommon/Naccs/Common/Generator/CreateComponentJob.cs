namespace Naccs.Common.Generator
{
    using Naccs.Common.CustomControls;
    using Naccs.Common.Function;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Reflection;
    using System.Windows.Forms;
    using System.Xml;

    public class CreateComponentJob : ICreateComponent
    {
        private BindingSourceEx bsCom;
        private BindingSourceEx bsRepeat;
        private List<BindingSourceEx> bsRList;
        private ComponentProperty CP;
        private DataSet ds;
        private BindingNavigator navigator;
        private List<Component> pList;

        public CreateComponentJob(BindingNavigator navigator)
        {
            this.navigator = navigator;
            this.CP = new ComponentProperty(false);
        }

        public void ClonePanel(List<Control> controlList, Control ctl, int cntX, int cntY, int SpaceX, int SpaceY, int Max)
        {
            for (int i = 0; i < cntY; i++)
            {
                for (int j = 0; j < cntX; j++)
                {
                    if ((i != 0) || (j != 0))
                    {
                        if (((i * cntX) + (j + 1)) > Max)
                        {
                            break;
                        }
                        BindingSourceEx item = new BindingSourceEx(this.ds, this.bsRepeat.DataMember);
                        this.bsRList.Add(item);
                        Control control = this.copyControls(ctl, item);
                        control.SuspendLayout();
                        this.pList.Add(control);
                        Point point = new Point();
                        point.X = ctl.Location.X + ((ctl.Width + SpaceX) * j);
                            point.Y = ctl.Location.Y + ((ctl.Height + SpaceY) * i);
                        control.Location = point;
                        controlList.Add(control);
                    }
                }
            }
        }

        public void CloneTitlePanel(List<Control> controlList, Control ctl, int cntX, int cntY, int SpaceX, int SpaceY)
        {
            for (int i = 0; i < cntY; i++)
            {
                for (int j = 0; j < cntX; j++)
                {
                    if ((i != 0) || (j != 0))
                    {
                        Control item = this.copyControls(ctl);
                        item.SuspendLayout();
                        Point point = new Point();
                         point.X = ctl.Location.X + ((ctl.Width + SpaceX) * j);
                         point.Y = ctl.Location.Y + ((ctl.Height + SpaceY) * i);
                        item.Location = point;
                        controlList.Add(item);
                    }
                }
            }
        }

        private Control copyControls(Control sourceControl)
        {
            System.Type type = sourceControl.GetType();
            System.Type[] typeArray = new System.Type[] { type };
            ConstructorInfo[] constructors = type.GetConstructors();
            object[] parameters = new object[0];
            Control dest = (Control) constructors[0].Invoke(parameters);
            this.copyProperties(sourceControl, dest);
            if (sourceControl.HasChildren)
            {
                foreach (Control control2 in sourceControl.Controls)
                {
                    Control control3 = this.copyControls(control2);
                    dest.Controls.Add(control3);
                }
            }
            return dest;
        }

        private Control copyControls(Control sourceControl, BindingSourceEx BS)
        {
            string dataMember = "";
            System.Type type = sourceControl.GetType();
            System.Type[] typeArray = new System.Type[] { type };
            ConstructorInfo[] constructors = type.GetConstructors();
            object[] parameters = new object[0];
            Control dest = (Control) constructors[0].Invoke(parameters);
            this.copyProperties(sourceControl, dest);
            string propertyName = "";
            if ((sourceControl is LBMaskedTextBox) || (sourceControl is LBInputComboBox))
            {
                propertyName = "Text";
            }
            if (((sourceControl is LBTextBox) || (sourceControl is LBCommaTextBox)) || ((sourceControl is LBDivTextBox) || (sourceControl is LBDateTimePicker)))
            {
                propertyName = "BindText";
            }
            if (sourceControl is LBComboBox)
            {
                propertyName = "SelectedValue";
            }
            if ((sourceControl is LBCheckBox) || (sourceControl is LBRadioButton))
            {
                propertyName = "CheckedText";
            }
            if (propertyName != "")
            {
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(dest)["id"];
                if (descriptor != null)
                {
                    dataMember = descriptor.Converter.ConvertToInvariantString(descriptor.GetValue(dest));
                }
                Binding binding = new Binding(propertyName, BS, dataMember, false, DataSourceUpdateMode.OnPropertyChanged);
                dest.DataBindings.Clear();
                dest.DataBindings.Add(binding);
            }
            if (sourceControl is RanLabel)
            {
                BS.PositionItem = (RanLabel) dest;
            }
            if (!(sourceControl is LBDivTextBox) && sourceControl.HasChildren)
            {
                foreach (Control control2 in sourceControl.Controls)
                {
                    Control control3 = this.copyControls(control2, BS);
                    dest.Controls.Add(control3);
                }
            }
            return dest;
        }

        private void copyProperties(object src, object dest)
        {
            foreach (PropertyDescriptor descriptor in TypeDescriptor.GetProperties(src))
            {
                if (descriptor.ShouldSerializeValue(src) && !descriptor.Name.Equals("Controls"))
                {
                    switch (descriptor.SerializationVisibility)
                    {
                        case DesignerSerializationVisibility.Visible:
                            if (!descriptor.IsReadOnly)
                            {
                                descriptor.SetValue(dest, descriptor.GetValue(src));
                            }
                            break;

                        case DesignerSerializationVisibility.Content:
                        {
                            object obj2 = descriptor.GetValue(src);
                            object obj3 = descriptor.GetValue(dest);
                            this.copyProperties(obj2, obj3);
                            break;
                        }
                    }
                }
            }
        }

        public Component CreateComponent(List<Control> controlList, System.Type type)
        {
            System.Type[] typeArray = new System.Type[] { type };
            ConstructorInfo[] constructors = type.GetConstructors();
            object[] parameters = new object[0];
            Component component = (Component) constructors[0].Invoke(parameters);
            controlList.Add((Control) component);
            return component;
        }

        public void CreateNavigator(List<Control> controlList, XmlReader xml)
        {
            this.navigator.Visible = true;
            this.CP.setControlCommonProps(xml, this.navigator);
            this.navigator.BindingSource = this.GetBindingSource(xml.GetAttribute("repetition_id"));
            controlList.Add(this.navigator);
        }

        private BindingSourceEx GetBindingSource(string rep_id)
        {
            this.SetDataSource(rep_id);
            return this.bsRepeat;
        }

        public BindingSourceEx GetBs(string rep_id)
        {
            return this.GetBindingSource(rep_id);
        }

        public void SetBinding(Control control, string pro, string id, string rep_id)
        {
            Binding binding;
            string str;
            this.SetDataSource(rep_id);
            if (((str = rep_id) != null) && (str == ""))
            {
                binding = new Binding(pro, this.bsCom, id, false, DataSourceUpdateMode.OnPropertyChanged);
            }
            else
            {
                binding = new Binding(pro, this.bsRepeat, id, false, DataSourceUpdateMode.OnPropertyChanged);
            }
            control.DataBindings.Add(binding);
        }

        private void SetDataSource(string rep_id)
        {
            string str;
            if (((str = rep_id) != null) && (str == ""))
            {
                if (this.bsCom.DataSource == null)
                {
                    this.bsCom.DataSource = this.ds;
                    this.bsCom.DataMember = Naccs.Common.Generator.ItemInfo.tblComName;
                }
            }
            else
            {
                if (this.bsRepeat.DataSource == null)
                {
                    this.bsRepeat.DataSource = this.ds;
                }
                if (this.bsRepeat.DataMember != rep_id)
                {
                    this.bsRepeat.DataMember = rep_id;
                }
            }
        }

        public BindingSourceEx BsCom
        {
            get
            {
                return this.bsCom;
            }
            set
            {
                this.bsCom = value;
            }
        }

        public BindingSourceEx BsRepeat
        {
            get
            {
                return this.bsRepeat;
            }
            set
            {
                this.bsRepeat = value;
            }
        }

        public List<BindingSourceEx> BsRList
        {
            get
            {
                return this.bsRList;
            }
            set
            {
                this.bsRList = value;
            }
        }

        public DataSet DS
        {
            get
            {
                return this.ds;
            }
            set
            {
                this.ds = value;
            }
        }

        public List<Component> PList
        {
            get
            {
                return this.pList;
            }
            set
            {
                this.pList = value;
            }
        }
    }
}

