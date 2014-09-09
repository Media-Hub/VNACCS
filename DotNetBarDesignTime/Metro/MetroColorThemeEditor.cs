using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing.Design;
using DevComponents.DotNetBar.Metro.ColorTables;

namespace DevComponents.DotNetBar.Design.Metro
{
    /// <summary>
    /// 
    /// </summary>
    public class MetroColorThemeEditor : System.Drawing.Design.UITypeEditor
    {
        private IWindowsFormsEditorService edSvc = null;
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
                    MetroColorGeneratorParameters currentParams = (MetroColorGeneratorParameters)value;
                    MetroColorGeneratorParameters[] metroThemes = MetroColorGeneratorParameters.GetAllPredefinedThemes();
                    string selectedTheme = null;
                    foreach (MetroColorGeneratorParameters mt in metroThemes)
                    {
                        lb.Items.Add(mt.ThemeName);
                        if (currentParams.BaseColor == mt.BaseColor && currentParams.CanvasColor == mt.CanvasColor)
                        {
                            lb.SelectedItem = mt.ThemeName;
                            selectedTheme = mt.ThemeName;
                        }
                    }

                    edSvc.DropDownControl(lb);
                    if (lb.SelectedItem != null && selectedTheme != (string)lb.SelectedItem)
                    {
                        return metroThemes[lb.SelectedIndex];
                    }
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
