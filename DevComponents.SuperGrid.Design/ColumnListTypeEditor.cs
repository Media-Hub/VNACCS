using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;

namespace DevComponents.SuperGrid.Design
{
    public class ColumnListTypeEditor : UITypeEditor
    {
        #region Private variables

        IWindowsFormsEditorService _EditorService;

        #endregion

        #region GetEditStyle

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return (UITypeEditorEditStyle.DropDown);
        }

        #endregion

        #region GetPaintValueSupported

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return (false);
        }

        #endregion

        #region EditValue

        public override object EditValue(
            ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            _EditorService =
                provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (_EditorService != null)
            {
                GridPanel panel = context.Instance as GridPanel;

                if (panel != null)
                {
                    ListBox lb = new ListBox();
                    lb.Dock = DockStyle.Fill;
                    lb.BorderStyle = BorderStyle.None;

                    lb.MouseClick += ListBoxMouseClick;

                    for (int i = 0; i < panel.Columns.Count; i++)
                    {
                        GridColumn col = panel.Columns[i];

                        string s = GetColumnText(col, i);

                        lb.Items.Add(s);
                    }

                    _EditorService.DropDownControl(lb);

                    if (lb.SelectedIndex >= 0)
                        return (lb.SelectedIndex);

                    return (panel.PrimaryColumnIndex);
                }
            }

            return (base.EditValue(context, provider, value));
        }

        private string GetColumnText(GridColumn col, int index)
        {
            string s = index + " ";

            if (string.IsNullOrEmpty(col.Name) == false)
                return (s + "-" + col.Name);

            if (string.IsNullOrEmpty(col.HeaderText) == false)
                return (s + "-" + col.HeaderText);

            return (s);
        }

        private void ListBoxMouseClick(object sender, MouseEventArgs e)
        {
            _EditorService.CloseDropDown();
        }

        #endregion
    }
}
