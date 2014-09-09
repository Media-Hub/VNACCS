using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DevComponents.SuperGrid.Design
{
    [ToolboxItem(false)]
    public class BoolValueTypeDropDown : ValueTypeDropDown
    {
        public BoolValueTypeDropDown()
        {
        }

        public BoolValueTypeDropDown(object value,
            IWindowsFormsEditorService editorService, ITypeDescriptorContext context)
            : base(value, editorService, context)
        {
        }

        protected override string[] GetListBoxItems()
        {
            return (new string[] { "True", "False" });
        }

        protected override int GetSelectedIndex()
        {
            if (Value is bool)
                return ((bool)Value == true ? 0 : 1);

            return (-1);
        }

        protected override void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
            ListBox listBox = sender as ListBox;

            if (listBox != null)
            {
                if (listBox.SelectedIndex >= 0)
                    Value = (listBox.SelectedIndex == 0);
            }
        }
    }
}
