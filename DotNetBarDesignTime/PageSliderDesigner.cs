using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.Design;
using DevComponents.DotNetBar.Controls;
using System.Collections;

namespace DevComponents.DotNetBar.Design
{
    public class PageSliderDesigner : System.Windows.Forms.Design.ControlDesigner
    {
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            if (!component.Site.DesignMode)
                return;

            IComponentChangeService cc = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (cc != null)
            {
                cc.ComponentRemoved += new ComponentEventHandler(this.OnComponentRemoved);
            }
        }
        protected override void Dispose(bool disposing)
        {
            IComponentChangeService cc = (IComponentChangeService)GetService(typeof(IComponentChangeService));
            if (cc != null)
            {
                cc.ComponentRemoved -= new ComponentEventHandler(this.OnComponentRemoved);
            }

            base.Dispose(disposing);
        }

        private void OnComponentRemoved(object sender, ComponentEventArgs e)
        {
            if (e.Component is PageSliderPage)
            {
                PageSliderPage page = e.Component as PageSliderPage;
                PageSlider slider = this.Control as PageSlider;

                if (page != null && slider.Controls.Contains(page))
                {
                    IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    if (cc != null)
                        cc.OnComponentChanging(slider, TypeDescriptor.GetProperties(slider)["Controls"]);

                    slider.Controls.Remove(page);

                    if (cc != null)
                        cc.OnComponentChanged(slider, TypeDescriptor.GetProperties(slider)["Controls"], null, null);
                }
            }
        }

        public override void DoDefaultAction()
        {
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerb[] verbs = null;
                verbs = new DesignerVerb[]
					{
						new DesignerVerb("Create Page", new EventHandler(CreatePage)),
                        new DesignerVerb("Delete Page", new EventHandler(DeletePage)),
                        new DesignerVerb("Next Page", new EventHandler(NextPage)),
                        new DesignerVerb("Previous Page", new EventHandler(PreviousPage))
                    };
                return new DesignerVerbCollection(verbs);
            }
        }

#if FRAMEWORK20
        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            SetDesignTimeDefaults();
        }
#else
		public override void OnSetComponentDefaults()
		{
			base.OnSetComponentDefaults();
            SetDesignTimeDefaults();
		}
#endif

        private void SetDesignTimeDefaults()
        {
        }

        private void CreatePage(object sender, EventArgs e)
        {
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            ISelectionService ss = this.GetService(typeof(ISelectionService)) as ISelectionService;

            CreatePage(this.Control as PageSlider, dh, cc, ss);
        }

        internal static PageSliderPage CreatePage(PageSlider parent, IDesignerHost dh, IComponentChangeService cc, ISelectionService ss)
        {
            DesignerTransaction dt = dh.CreateTransaction();
            PageSliderPage page = null;
            try
            {
                page = dh.CreateComponent(typeof(PageSliderPage)) as PageSliderPage;

                if (cc != null)
                    cc.OnComponentChanging(parent, TypeDescriptor.GetProperties(parent)["Controls"]);
                parent.Controls.Add(page);
                if (cc != null)
                    cc.OnComponentChanged(parent, TypeDescriptor.GetProperties(parent)["Controls"], null, null);

                if (ss != null)
                    ss.SetSelectedComponents(new PageSliderPage[] { page }, SelectionTypes.Replace);

                TypeDescriptor.GetProperties(parent)["SelectedPage"].SetValue(parent, page);
            }
            catch
            {
                dt.Cancel();
            }
            finally
            {
                if (!dt.Canceled)
                    dt.Commit();
            }

            return page;
        }

        private void DeletePage(object sender, EventArgs e)
        {
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
            Wizard w = this.Control as Wizard;
            if (w == null)
                return;
            DeletePage(w.SelectedPage, dh, cc);
        }

        internal static void DeletePage(WizardPage page, IDesignerHost dh, IComponentChangeService cc)
        {
            if (page == null || !(page.Parent is PageSlider))
                return;

            PageSlider slider = (PageSlider)page.Parent;

            DesignerTransaction dt = dh.CreateTransaction("Deleting page");

            try
            {
                if (cc != null)
                    cc.OnComponentChanging(slider, TypeDescriptor.GetProperties(slider)["Controls"]);

                slider.Controls.Remove(page);

                if (cc != null)
                    cc.OnComponentChanged(slider, TypeDescriptor.GetProperties(slider)["Controls"], null, null);

                dh.DestroyComponent(page);
            }
            catch
            {
                dt.Cancel();
            }
            finally
            {
                if (!dt.Canceled)
                    dt.Commit();
            }
        }

        private void NextPage(object sender, EventArgs e)
        {
            SelectNextPage(this.Control as PageSlider);
        }

        private void PreviousPage(object sender, EventArgs e)
        {
            SelectPreviousPage(this.Control as PageSlider);
        }

        internal static bool SelectNextPage(PageSlider slider)
        {
            if (slider == null)
                return false;
            if (slider.PageCount > 0)
            {
                if (slider.SelectedPageIndex < slider.PageCount - 1)
                    slider.SelectedPageIndex++;
                else
                    slider.SelectedPageIndex = 0;
                return true;
            }
            return false;
        }

        internal static bool SelectPreviousPage(PageSlider slider)
        {
            if (slider == null)
                return false;
            if (slider.PageCount > 0)
            {
                if (slider.SelectedPageIndex > 0)
                    slider.SelectedPageIndex--;
                else
                    slider.SelectedPageIndex = slider.PageCount - 1;
                return true;
            }
            return false;
        }
    }
}
