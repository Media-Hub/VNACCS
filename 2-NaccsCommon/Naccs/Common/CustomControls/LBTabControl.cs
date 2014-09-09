namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Properties;
    using System;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(TabControl))]
    public class LBTabControl : TabControl
    {
        public LBTabControl()
        {
            base.TabStop = false;
            this.DoubleBuffered = true;
        }

        private bool canDelete(object obj)
        {
            if ((obj is IItemAttributes) || obj.GetType().Equals(DesignControls.GridView))
            {
                return false;
            }
            IRepetition repetition = obj as IRepetition;
            if ((repetition != null) && (repetition.repetition_id.Length > 0))
            {
                return false;
            }
            Control control = obj as Control;
            if ((control != null) && control.HasChildren)
            {
                foreach (Control control2 in control.Controls)
                {
                    if (!this.canDelete(control2))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public void verbAddTab(object sender, EventArgs e)
        {
            TabPage page = new TabPage("TabPage" + base.TabPages.Count.ToString());
            page.UseVisualStyleBackColor = true;
            base.TabPages.Add(page);
        }

        public void verbDeleteTab(object sender, EventArgs e)
        {
            if (base.SelectedTab != null)
            {
                foreach (Control control in base.SelectedTab.Controls)
                {
                    if (!this.canDelete(control))
                    {
                        MessageBox.Show(Resources.ResourceManager.GetString("COM05"));
                        return;
                    }
                }
                base.TabPages.Remove(base.SelectedTab);
            }
        }
    }
}

