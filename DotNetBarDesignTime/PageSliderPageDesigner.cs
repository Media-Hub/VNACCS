using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Design
{
    public class PageSliderPageDesigner : ScrollableControlDesigner
    {
        #region Internal Implementation
        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerb[] verbs = null;
                verbs = new DesignerVerb[]
					{
						new DesignerVerb("Move Forward", new EventHandler(MovePageForward)),
						new DesignerVerb("Move Backward", new EventHandler(MovePageBackward)),
						new DesignerVerb("Make First", new EventHandler(MakeFirstPage)),
                        new DesignerVerb("Make Last", new EventHandler(MakeLastPage))
                    };
                return new DesignerVerbCollection(verbs);
            }
        }

        private void MovePageTo(PageSliderPage page, PageSlider slider, int newIndex)
        {
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            ISelectionService ss = this.GetService(typeof(ISelectionService)) as ISelectionService;

            DesignerTransaction dt = dh.CreateTransaction("Moving page");
            if (cc != null)
                cc.OnComponentChanging(slider, TypeDescriptor.GetProperties(slider)["Controls"]);
            slider.Controls.SetChildIndex(page, newIndex);
            if (cc != null)
                cc.OnComponentChanged(slider, TypeDescriptor.GetProperties(slider)["Controls"], null, null);
            dt.Commit();
        }
        private void MovePageForward(object sender, EventArgs e)
        {
            PageSliderPage page = this.Control as PageSliderPage;
            if (page == null) return;
            PageSlider slider = page.Parent as PageSlider;
            if (slider == null) return;
            int newIndex = slider.Controls.IndexOf(page);
            if (newIndex < slider.PageCount - 1)
                newIndex++;
            else
                newIndex = 0;

            MovePageTo(page, slider, newIndex);
        }
        private void MovePageBackward(object sender, EventArgs e)
        {
            PageSliderPage page = this.Control as PageSliderPage;
            if (page == null) return;
            PageSlider slider = page.Parent as PageSlider;
            if (slider == null) return;
            int newIndex = slider.Controls.IndexOf(page);
            if (newIndex > 0)
                newIndex--;
            else
                newIndex = slider.PageCount - 1;

            MovePageTo(page, slider, newIndex);
        }
        private void MakeFirstPage(object sender, EventArgs e)
        {
            PageSliderPage page = this.Control as PageSliderPage;
            if (page == null) return;
            PageSlider slider = page.Parent as PageSlider;
            if (slider == null) return;
            MovePageTo(page, slider, 0);
        }
        private void MakeLastPage(object sender, EventArgs e)
        {
            PageSliderPage page = this.Control as PageSliderPage;
            if (page == null) return;
            PageSlider slider = page.Parent as PageSlider;
            if (slider == null) return;
            MovePageTo(page, slider, slider.PageCount - 1);
        }
        public override SelectionRules SelectionRules
        {
            get { return SelectionRules.Locked; }
        }

        protected override void OnPaintAdornments(PaintEventArgs pe)
        {
            PageSliderPage p;
            p = this.Component as PageSliderPage;
            if (p != null)
            {
                this.DrawBorder(pe.Graphics);
            }
            base.OnPaintAdornments(pe);
        }


        private void DrawBorder(Graphics g)
        {
            PageSliderPage panel = this.Control as PageSliderPage;
            Color border = SystemColors.ControlDarkDark;
            Rectangle rClient = this.Control.ClientRectangle;
            Color backColor = Color.Empty;

            Helpers.DrawDesignTimeSelection(g, rClient, backColor, border, 1);
        }
        #endregion
    }
}
