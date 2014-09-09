using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using System.ComponentModel.Design;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Drawing;
using System.Collections;
namespace DevComponents.DotNetBar.Design
{
    public class RadialMenuDesigner : ControlDesigner
    {
        #region Implementation
        public override System.Collections.ICollection AssociatedComponents
        {
            get
            {
                ArrayList c = new ArrayList(base.AssociatedComponents);
                RadialMenu menu = this.Control as RadialMenu;
                if (menu != null)
                {
                    foreach (BaseItem item in menu.Items)
                    {
                        if (item.DesignMode)
                            c.Add(item);
                    }
                }
                return c;
            }
        }

        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            RadialMenu menu = this.Control as RadialMenu;
            menu.Symbol = "\uf043";
            
            if (defaultValues != null)
            {
                if (defaultValues.Contains("Size"))
                {
                    defaultValues["Size"] = new Size(28, 28);
                }
            }
            base.InitializeNewComponent(defaultValues);
        }

        private DesignerActionListCollection _ActionLists = null;
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (this._ActionLists == null)
                {
                    this._ActionLists = new DesignerActionListCollection();
                    this._ActionLists.Add(new RadialMenuActionList(this));
                }
                return this._ActionLists;
            }
        }

        public void EditItems()
        {
            RadialMenu radialMenu = this.Component as RadialMenu;

            Form form = new Form();
            form.Text = "Radial Menu Editor";
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.MinimizeBox = false;
            //form.MaximizeBox = false;
            form.StartPosition = FormStartPosition.CenterScreen;
            RadialMenuItemEditor editor = new RadialMenuItemEditor();
            editor.Dock = DockStyle.Fill;
            form.ClientSize = new System.Drawing.Size(722, 660);
            form.Controls.Add(editor);
            form.BackColor = Color.White;
            editor.RadialMenu = radialMenu;
            editor.Designer = this;
            editor.UpdateDisplay();
            form.ShowDialog();
            form.Dispose();
        }

        public object GetDesignService(Type serviceType)
        {
            return GetService(serviceType);
        }

        public override SelectionRules SelectionRules
        {
            get
            {
                return SelectionRules.Moveable | SelectionRules.Visible;
            }
        }
        #endregion
    }
}
