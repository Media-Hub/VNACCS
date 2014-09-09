namespace Naccs.Common.Function
{
    using Naccs.Common.Constant;
    using Naccs.Common.CustomControls;
    using System;

    public class CrtProperties : PropertiesCollection
    {
        public static string[] PanelAndGridColumnProperties = new string[] { "ReadOnly", "Visible", "AutoComplete" };
        public static string[] PanelAndGridProperties = new string[] { "repetition_id", "repetition_max", "Tag", "TabIndex", "TabPages", "TabStop", "Visible", "AutoSize", "Dock", "Location", "Size", "BackColor", "BorderStyle", "ForeColor" };
        public static string[] PanelAndGridStyleProperties = new string[] { "BackColor", "ForeColor", "Font" };

        public CrtProperties()
        {
            base.LoadProperties(DllResource.CrtPropertyList);
        }

        public static string getControlName(string tagName)
        {
            string name = null;
            if (tagName == "label")
            {
                name = DesignControls.Label.Name;
            }
            else if (tagName == "ranlabel")
            {
                name = DesignControls.RanLabel.Name;
            }
            else if (tagName == "textbox")
            {
                name = DesignControls.TextBox.Name;
            }
            else if (tagName == "maskedtextbox")
            {
                name = DesignControls.MaskedTextBox.Name;
            }
            else if (tagName == "combobox")
            {
                name = DesignControls.ComboBox.Name;
            }
            else if (tagName == "inputcombobox")
            {
                name = DesignControls.InputComboBox.Name;
            }
            else if (tagName == "datetimepicker")
            {
                name = DesignControls.DataTimePicker.Name;
            }
            else if (tagName == "checkbox")
            {
                name = DesignControls.CheckBox.Name;
            }
            else if (tagName == "radio")
            {
                name = DesignControls.RadioButton.Name;
            }
            else if (tagName == "button")
            {
                name = DesignControls.Button.Name;
            }
            else if (tagName == "navigator")
            {
                name = DesignControls.Navigator.Name;
            }
            else if (tagName == "datagrid")
            {
                name = DesignControls.GridView.Name;
            }
            else if (tagName == "column")
            {
                name = DesignControls.GridViewColumn.Name;
            }
            else if (tagName == "columnstyle")
            {
                name = DesignControls.GridViewCellStyle.Name;
            }
            else if (tagName == "commatext")
            {
                name = DesignControls.CommaText.Name;
            }
            if (tagName == "TabControl")
            {
                return DesignControls.TabControl.Name;
            }
            if (tagName == "TabPage")
            {
                return DesignControls.TabPage.Name;
            }
            if (tagName == "GroupBox")
            {
                return DesignControls.GroupBox.Name;
            }
            if (tagName == "Panel")
            {
                return DesignControls.Panel.Name;
            }
            if (tagName == "barcode")
            {
                return DesignControls.Barcode.Name;
            }
            if (tagName == "hand-ocr")
            {
                return DesignControls.HandOcr.Name;
            }
            if (tagName == "comp-ocr")
            {
                name = DesignControls.CompOcr.Name;
            }
            return name;
        }
    }
}

