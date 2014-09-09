using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Drawing;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Design
{
	/// <summary>
	/// Summary description for NavigationPaneDesigner.
	/// </summary>
	public class NavigationPaneDesigner:NavigationBarDesigner
	{
		public override void Initialize(IComponent component) 
		{
			base.Initialize(component);
			if(!component.Site.DesignMode)
				return;
			NavigationPane pane=this.Control as NavigationPane;
			if(pane!=null)
				pane.SetDesignMode();
		}
		public override DesignerVerbCollection Verbs 
		{
			get 
			{
				DesignerVerb[] verbs;
				verbs = new DesignerVerb[]
				{
					new DesignerVerb("Create New Pane", new EventHandler(CreateNewPane)),
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
            CreateNewPane();
            NavigationPane pane = this.Control as NavigationPane;
            pane.Style = eDotNetBarStyle.StyleManagerControlled;
        }

		private void CreateNewPane(object sender, EventArgs e)
		{
			CreateNewPane();
		}

		private void CreateNewPane()
		{
			IDesignerHost dh=(IDesignerHost)GetService(typeof(IDesignerHost));
			NavigationPane navPane=this.Control as NavigationPane;
			if(navPane==null || dh==null)
				return;

            DesignerTransaction dt = dh.CreateTransaction();
            try
            {
                IComponentChangeService change = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (change != null)
                    change.OnComponentChanging(this.Component, null);

                ButtonItem item = null;
                try
                {
                    m_CreatingItem = true;
                    item = dh.CreateComponent(typeof(ButtonItem)) as ButtonItem;
                    item.Text = item.Name;
                    item.OptionGroup = "navBar";
                    item.Image = Helpers.LoadBitmap("SystemImages.DefaultNavBarImage.png");
                }
                finally
                {
                    m_CreatingItem = false;
                }

                NavigationPanePanel panel = dh.CreateComponent(typeof(NavigationPanePanel)) as NavigationPanePanel;
                panel.ParentItem = item;
                navPane.Items.Add(item);
                navPane.Controls.Add(panel);
                panel.Dock = DockStyle.Fill;
                panel.SendToBack();
                panel.ApplyPanelStyle();
                panel.ColorSchemeStyle = navPane.Style;
                if (Helpers.IsOffice2007Style(navPane.Style))
                    panel.ColorScheme = navPane.NavigationBar.GetColorScheme();
                panel.Style.BackColor2.ColorSchemePart = eColorSchemePart.None;
                panel.Style.Border = eBorderType.None;
                panel.Style.BorderColor.ColorSchemePart = eColorSchemePart.PanelBorder;

                if (navPane.Items.Count == 1)
                    item.Checked = true;
                navPane.RecalcLayout();
                if (change != null)
                    change.OnComponentChanged(this.Component, null, null, null);

                if (change != null)
                {
                    change.OnComponentChanging(panel, null);
                    change.OnComponentChanged(panel, null, null, null);
                }
            }
            catch
            {
                dt.Cancel();
            }
            finally
            {
                if (!dt.Canceled) dt.Commit();
            }
		}

		public override bool CanParent(Control c)
		{
			if(c is NavigationPanePanel)
				return true;
			return false;
		}

		protected override BaseItem GetItemContainer()
		{
			NavigationPane bar=this.Control as NavigationPane;
			if(bar!=null)
				return bar.NavigationBar.GetBaseItemContainer();
			return null;
		}

		protected override System.Windows.Forms.Control GetItemContainerControl()
		{
			NavigationPane bar=this.Control as NavigationPane;
			if(bar!=null)
				return bar.NavigationBar;
			return null;
		}
		/// <summary>
		/// Triggered when some other component on the form is removed.
		/// </summary>
		protected override void OtherComponentRemoving(object sender, ComponentEventArgs e)
		{
			base.OtherComponentRemoving(sender,e);

			NavigationPane bar=this.Component as NavigationPane;
			if(bar==null || bar.IsDisposed)
				return;

			IDesignerHost dh=(IDesignerHost)GetService(typeof(IDesignerHost));
			if(dh==null)
				return;
            IComponentChangeService cc = (IComponentChangeService)GetService(typeof(IComponentChangeService));

			// Check is any of the buttons or panel that we host
			if(!m_InternalRemoving && e.Component is BaseItem && dh.TransactionDescription.StartsWith("Delete "))
			{
                ButtonItem item = e.Component as ButtonItem;
                if (item != null)
                {
                    NavigationPanePanel panel = bar.GetPanel(item);
                    if (panel != null && dh != null)
                    {
                        DesignerTransaction dt = dh.CreateTransaction("Removing associated NavigationPanePanel");
                        panel.ParentItem = null;
                        if (cc != null) cc.OnComponentChanging(bar, TypeDescriptor.GetProperties(bar)["Controls"]);
                        bar.Controls.Remove(panel);
                        if (cc != null) cc.OnComponentChanged(bar, TypeDescriptor.GetProperties(bar)["Controls"], null, null);
                        dh.DestroyComponent(panel);
                        dt.Commit();
                        bar.RecalcLayout();
                    }
                }
			}
			else if(!m_InternalRemoving && e.Component is NavigationPanePanel)
			{
				if(bar.Controls.Contains(e.Component as NavigationPanePanel))
				{
					NavigationPanePanel navpane=e.Component as NavigationPanePanel;
                    if (navpane.ParentItem != null)
                    {
                        BaseItem item = navpane.ParentItem;
                        navpane.ParentItem = null;
                        if (cc != null) cc.OnComponentChanging(bar, TypeDescriptor.GetProperties(bar)["Items"]);
                        bar.Items.Remove(item);
                        if (cc != null) cc.OnComponentChanged(bar, TypeDescriptor.GetProperties(bar)["Items"], null, null);
                        dh.DestroyComponent(item);
                    }
					bar.RecalcLayout();
				}
			}

            if (bar.CheckedButton == null && bar.Items.Count > 0 && bar.Items[0] is ButtonItem)
            {
                ButtonItem buttonItem = (ButtonItem)bar.Items[0];
                buttonItem.Checked = true;
            }
		}

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);
            properties["Expanded"] = TypeDescriptor.CreateProperty(typeof(NavigationPaneDesigner), (PropertyDescriptor)properties["Expanded"], new Attribute[]
				{
					new DefaultValueAttribute(true),
					new BrowsableAttribute(true),
					new CategoryAttribute("Title"),
                    new DescriptionAttribute("Indicates whether navigation pane can be collapsed.")});
        }

        /// <summary>
        /// Gets or sets whether navigation pane is expanded. Default value is true. 
        /// When control is collapsed it is reduced in size so it consumes less space.
        /// </summary>
        [Browsable(true), Category("Title"), DefaultValue(true), Description("Indicates whether navigation pane can be collapsed.")]
        public bool Expanded
        {
            get { return (bool)ShadowProperties["Expanded"]; }
            set
            {
                // this value is not passed to the actual control at design-time
                this.ShadowProperties["Expanded"] = value;
            }
        }
	}
}
