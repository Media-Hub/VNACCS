namespace Naccs.Common.Generator
{
    using Naccs.Common;
    using Naccs.Common.CustomControls;
    using Naccs.Common.Function;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Windows.Forms;
    using System.Xml;

    public class LayoutGenerator
    {
        private ComponentProperty CP;
        private ICreateComponent creator;
        private List<int> lstDispIdx;
        private string tblID = "";

        public LayoutGenerator(ICreateComponent creator)
        {
            this.creator = creator;
            this.CP = new ComponentProperty(false);
        }

        public int CreateLayout(string LayoutFileName, Control rootPanel)
        {
            int height = 0;
            List<Control> controlList = new List<Control>();
            XmlReaderSettings settings = new XmlReaderSettings {
                IgnoreComments = true,
                IgnoreWhitespace = true
            };
            XmlReader xReader = null;
            try
            {
                xReader = XmlReader.Create(LayoutFileName, settings);
                rootPanel.SuspendLayout();
                bool flag = false;
                while (xReader.Read())
                {
                    if ((xReader.NodeType == XmlNodeType.Element) & (xReader.LocalName == "layout"))
                    {
                        flag = true;
                        if (xReader.GetAttribute("UseDefaultSize") == "True")
                        {
                            Size size = new Size(xReader.GetAttribute("Size"));
                            height = size.Height;
                        }
                    }
                    else if (flag)
                    {
                        this.doCreateLayout(xReader, controlList);
                    }
                }
            }
            catch (XmlException exception)
            {
                NaccsException exception2 = new NaccsException(MessageKind.Error, 0x1f8, exception.Message);
                throw exception2;
            }
            catch (Exception exception3)
            {
                NaccsException exception4 = new NaccsException(MessageKind.Error, 510, exception3.Message);
                throw exception4;
            }
            finally
            {
                rootPanel.Controls.AddRange(controlList.ToArray());
                rootPanel.ResumeLayout(true);
                if (xReader != null)
                {
                    xReader.Close();
                }
            }
            return height;
        }

        private void doCreateLayout(XmlReader XReader, List<Control> controlList)
        {
            if (XReader.NodeType != XmlNodeType.Whitespace)
            {
                switch (XReader.LocalName)
                {
                    case "container":
                    {
                        System.Type panel = null;
                        Control control = null;
                        bool flag = true;
                        string str = "";
                        string str2 = "";
                        switch (XReader.GetAttribute("type"))
                        {
                            case "Panel":
                                panel = DesignControls.Panel;
                                break;

                            case "TabControl":
                                panel = DesignControls.TabControl;
                                break;

                            case "TabPage":
                                panel = DesignControls.TabPage;
                                break;

                            case "GroupBox":
                                panel = DesignControls.GroupBox;
                                break;

                            case "TitlePanel":
                                panel = DesignControls.TitlePanel;
                                str2 = "TitlePanel";
                                break;

                            case null:
                                panel = DesignControls.Panel;
                                break;
                        }
                        control = (Control) this.creator.CreateComponent(controlList, panel);
                        bool isEmptyElement = XReader.IsEmptyElement;
                        if (this.tblID != "")
                        {
                            flag = false;
                        }
                        else
                        {
                            flag = true;
                            str = XReader.GetAttribute("repetition_id");
                            if (str == null)
                            {
                                this.tblID = "";
                            }
                            else
                            {
                                this.tblID = str;
                            }
                        }
                        this.CP.setControlCommonProps(XReader, control);
                        int cntX = 1;
                        int cntY = 1;
                        int spaceX = 0;
                        int spaceY = 1;
                        int max = 1;
                        string attribute = XReader.GetAttribute("RepX");
                        if (attribute != null)
                        {
                            cntX = int.Parse(attribute);
                        }
                        attribute = XReader.GetAttribute("RepY");
                        if (attribute != null)
                        {
                            cntY = int.Parse(attribute);
                        }
                        attribute = XReader.GetAttribute("SpaceX");
                        if (attribute != null)
                        {
                            spaceX = int.Parse(attribute);
                        }
                        attribute = XReader.GetAttribute("SpaceY");
                        if (attribute != null)
                        {
                            spaceY = int.Parse(attribute);
                        }
                        attribute = XReader.GetAttribute("repetition_max");
                        if (attribute != null)
                        {
                            max = int.Parse(attribute);
                        }
                        if (!isEmptyElement)
                        {
                            List<Control> list = new List<Control>();
                            while (XReader.Read())
                            {
                                if (XReader.NodeType == XmlNodeType.EndElement)
                                {
                                    break;
                                }
                                this.doCreateLayout(XReader, list);
                            }
                            control.Controls.AddRange(list.ToArray());
                        }
                        if ((cntX > 1) || (cntY > 1))
                        {
                            if (str2 == "TitlePanel")
                            {
                                this.creator.CloneTitlePanel(controlList, control, cntX, cntY, spaceX, spaceY);
                            }
                            else
                            {
                                this.creator.ClonePanel(controlList, control, cntX, cntY, spaceX, spaceY, max);
                            }
                        }
                        if (flag)
                        {
                            this.tblID = "";
                        }
                        return;
                    }
                    case "label":
                    {
                        Label label = (Label) this.creator.CreateComponent(controlList, DesignControls.Label);
                        this.CP.setControlCommonProps(XReader, label);
                        return;
                    }
                    case "ranlabel":
                    {
                        RanLabel label2 = (RanLabel) this.creator.CreateComponent(controlList, DesignControls.RanLabel);
                        this.CP.setControlCommonProps(XReader, label2);
                        BindingSourceEx bs = this.creator.GetBs(XReader.GetAttribute("repetition_id"));
                        if (bs != null)
                        {
                            bs.PositionItem = label2;
                        }
                        return;
                    }
                    case "textbox":
                    {
                        LBTextBox box = (LBTextBox) this.creator.CreateComponent(controlList, DesignControls.TextBox);
                        this.CP.setControlCommonProps(XReader, box);
                        box.DisplayWidth = ComponentProperty.getTurnLength(box);
                        this.creator.SetBinding(box, "BindText", XReader.GetAttribute("id"), this.tblID);
                        box.Rep_ID = this.tblID;
                        return;
                    }
                    case "maskedtextbox":
                    {
                        LBMaskedTextBox box2 = (LBMaskedTextBox) this.creator.CreateComponent(controlList, DesignControls.MaskedTextBox);
                        this.CP.setControlCommonProps(XReader, box2);
                        this.creator.SetBinding(box2, "Text", XReader.GetAttribute("id"), this.tblID);
                        box2.Rep_ID = this.tblID;
                        return;
                    }
                    case "commatext":
                    {
                        LBCommaTextBox box3 = (LBCommaTextBox) this.creator.CreateComponent(controlList, DesignControls.CommaText);
                        this.CP.setControlCommonProps(XReader, box3);
                        this.creator.SetBinding(box3, "BindText", XReader.GetAttribute("id"), this.tblID);
                        box3.Rep_ID = this.tblID;
                        return;
                    }
                    case "div-textbox":
                    {
                        LBDivTextBox box4 = (LBDivTextBox) this.creator.CreateComponent(controlList, DesignControls.DivTextBox);
                        this.CP.setControlCommonProps(XReader, box4);
                        this.creator.SetBinding(box4, "BindText", XReader.GetAttribute("id"), this.tblID);
                        box4.Rep_ID = this.tblID;
                        return;
                    }
                    case "combobox":
                    {
                        LBComboBox box5 = (LBComboBox) this.creator.CreateComponent(controlList, DesignControls.ComboBox);
                        this.CP.setControlCommonProps(XReader, box5);
                        this.creator.SetBinding(box5, "SelectedValue", XReader.GetAttribute("id"), this.tblID);
                        box5.Rep_ID = this.tblID;
                        return;
                    }
                    case "inputcombobox":
                    {
                        LBInputComboBox box6 = (LBInputComboBox) this.creator.CreateComponent(controlList, DesignControls.InputComboBox);
                        this.CP.setControlCommonProps(XReader, box6);
                        this.creator.SetBinding(box6, "Text", XReader.GetAttribute("id"), this.tblID);
                        box6.Rep_ID = this.tblID;
                        return;
                    }
                    case "datetimepicker":
                    {
                        LBDateTimePicker picker = (LBDateTimePicker) this.creator.CreateComponent(controlList, DesignControls.DataTimePicker);
                        this.CP.setControlCommonProps(XReader, picker);
                        this.creator.SetBinding(picker, "BindText", XReader.GetAttribute("id"), this.tblID);
                        picker.Rep_ID = this.tblID;
                        return;
                    }
                    case "checkbox":
                    {
                        LBCheckBox box7 = (LBCheckBox) this.creator.CreateComponent(controlList, DesignControls.CheckBox);
                        this.CP.setControlCommonProps(XReader, box7);
                        this.creator.SetBinding(box7, "CheckedText", XReader.GetAttribute("id"), this.tblID);
                        box7.Rep_ID = this.tblID;
                        return;
                    }
                    case "radio":
                    {
                        LBRadioButton button = (LBRadioButton) this.creator.CreateComponent(controlList, DesignControls.RadioButton);
                        this.CP.setControlCommonProps(XReader, button);
                        this.creator.SetBinding(button, "CheckedText", XReader.GetAttribute("id"), this.tblID);
                        button.Rep_ID = this.tblID;
                        return;
                    }
                    case "navigator":
                        this.creator.CreateNavigator(controlList, XReader);
                        return;

                    case "datagrid":
                    {
                        DataGridView view = (DataGridView) this.creator.CreateComponent(controlList, DesignControls.GridView);
                        this.CP.setControlCommonProps(XReader, view);
                        view.AutoGenerateColumns = false;
                        this.tblID = XReader.GetAttribute("repetition_id");
                        BindingSource source = this.creator.GetBs(XReader.GetAttribute("repetition_id"));
                        if (source != null)
                        {
                            view.DataSource = source;
                        }
                        int num6 = int.Parse(XReader.GetAttribute("repetition_max"));
                        int num7 = 0;
                        while (XReader.Read())
                        {
                            if (XReader.NodeType != XmlNodeType.Whitespace)
                            {
                                if (XReader.NodeType == XmlNodeType.EndElement)
                                {
                                    if (!(XReader.LocalName == "datagrid"))
                                    {
                                        continue;
                                    }
                                    break;
                                }
                                if (XReader.LocalName == "column")
                                {
                                    LBTextBoxColumn dataGridViewColumn = new LBTextBoxColumn();
                                    view.Columns.Add(dataGridViewColumn);
                                    num7 = view.Columns.Count - 1;
                                    string str4 = XReader.GetAttribute("id");
                                    for (int i = 0; i <= (XReader.AttributeCount - 1); i++)
                                    {
                                        XReader.MoveToAttribute(i);
                                        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(dataGridViewColumn)[XReader.Name];
                                        if ((descriptor != null) && (XReader.Name != "DataPropertyName"))
                                        {
                                            descriptor.SetValue(dataGridViewColumn, descriptor.Converter.ConvertFromInvariantString(XReader.Value));
                                        }
                                    }
                                    if (source != null)
                                    {
                                        dataGridViewColumn.DataPropertyName = str4;
                                    }
                                    dataGridViewColumn.DisplayIndex = num7;
                                }
                                else
                                {
                                    DataGridViewColumn column2 = view.Columns[num7];
                                    LBTextBoxColumn column3 = (LBTextBoxColumn) column2;
                                    for (int j = 0; j <= (XReader.AttributeCount - 1); j++)
                                    {
                                        XReader.MoveToAttribute(j);
                                        if (!(XReader.Name == "BackColor"))
                                        {
                                            PropertyDescriptor descriptor2 = TypeDescriptor.GetProperties(column3.DefaultCellStyle)[XReader.Name];
                                            if (descriptor2 != null)
                                            {
                                                descriptor2.SetValue(column3.DefaultCellStyle, descriptor2.Converter.ConvertFromInvariantString(XReader.Value));
                                            }
                                        }
                                    }
                                    num7++;
                                }
                            }
                        }
                        if (!(this.creator is CreateComponentJob) && (view.RowCount < num6))
                        {
                            view.RowCount = num6;
                        }
                        if (this.ListDispIdx != null)
                        {
                            foreach (DataGridViewColumn column4 in view.Columns)
                            {
                                this.ListDispIdx.Add(column4.DisplayIndex);
                            }
                        }
                        view.AllowUserToAddRows = false;
                        view.AllowUserToDeleteRows = false;
                        view.AllowUserToOrderColumns = false;
                        this.tblID = "";
                        return;
                    }
                }
            }
        }

        public List<int> ListDispIdx
        {
            get
            {
                return this.lstDispIdx;
            }
            set
            {
                this.lstDispIdx = value;
            }
        }
    }
}

