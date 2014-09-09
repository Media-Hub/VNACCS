using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using System.ComponentModel.Design;
using System.Drawing.Design;

namespace DevComponents.DotNetBar.Design
{
    public class ItemSelectorTypeEditor : System.Drawing.Design.UITypeEditor
    {
        private IWindowsFormsEditorService edSvc = null;
        private Type[] _AvailableTypes = new Type[] {
            typeof(ButtonItem),
            typeof(CheckBoxItem),
            typeof(MetroTileItem),
            typeof(LabelItem)
        };

        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {
                edSvc = provider.GetService(typeof(IWindowsFormsEditorService)) as IWindowsFormsEditorService;

                if (edSvc != null)
                {
                    ListBox lb = new ListBox();
                    lb.SelectedIndexChanged += new EventHandler(this.SelectedChanged);

                    for (int i = 0; i < _AvailableTypes.Length; i++)
                    {
                        Type itemType = _AvailableTypes[i];
                        lb.Items.Add(itemType.Name);
                        if (value != null && value.GetType() == itemType)
                            lb.SelectedIndex = i;
                    }

                    edSvc.DropDownControl(lb);
                    if (lb.SelectedItem != null)
                    {
                        int index = lb.SelectedIndex;
                        if (value == null || _AvailableTypes[index] != value.GetType())
                        {
                            IDesignerHost dh = provider.GetService(typeof(IDesignerHost)) as IDesignerHost;
                            if (dh != null)
                            {
                                DesignerTransaction trans = dh.CreateTransaction(BarBaseControlDesigner.CreatingItemTransactionDescription);
                                if (value is IComponent)
                                    dh.DestroyComponent((IComponent)value);
                                value = dh.CreateComponent(_AvailableTypes[index]);
                                trans.Commit();
                            }
                        }
                    }

                    return value;
                }
            }

            return value;
        }

        private void SelectedChanged(object sender, EventArgs e)
        {
            if (edSvc != null)
                edSvc.CloseDropDown();
        }

        /// <summary>
        /// Gets the editor style used by the EditValue method.
        /// </summary>
        /// <param name="context">An ITypeDescriptorContext that can be used to gain additional context information.</param>
        /// <returns>A UITypeEditorEditStyle value that indicates the style of editor used by EditValue. If the UITypeEditor does not support this method, then GetEditStyle will return None</returns>
        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null && context.Instance != null)
            {
                return UITypeEditorEditStyle.DropDown;
            }
            return base.GetEditStyle(context);
        }

    }
}
