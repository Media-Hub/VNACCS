using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Metro;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Design
{
    class MetroToolbarDesigner : BarBaseControlDesigner
    {
        public MetroToolbarDesigner()
		{
			this.EnableItemDragDrop=true;
            this.ShadowProperties["Expanded"] = false;
		}

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            if (component == null || component.Site == null || !component.Site.DesignMode)
                return;

#if !TRIAL
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (dh != null)
                dh.LoadComplete += new EventHandler(dh_LoadComplete);
#endif
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
            SetDesignTimeDefaults();
			base.OnSetComponentDefaults();
		}
#endif

        private void SetDesignTimeDefaults()
        {
            MetroToolbar bar = this.Control as MetroToolbar;
            try
            {
                bar.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            catch
            {
                
            }
#if !TRIAL
            string key = GetLicenseKey();
            bar.LicenseKey = key;
#endif
        }

        //protected override BaseItem GetItemContainer()
        //{
        //    MetroToolbar bar = this.Control as MetroToolbar;
        //    return ((MetroToolbarContainer)bar.GetBaseItemContainer()).MainItemsContainer;
        //}

        public override DesignerVerbCollection Verbs
        {
            get
            {
                Bar bar = this.Control as Bar;
                DesignerVerb[] verbs = null;
                verbs = new DesignerVerb[]
						{
							new DesignerVerb("Add Button", new EventHandler(CreateButton)),
							new DesignerVerb("Add Horizontal Container", new EventHandler(CreateHorizontalContainer)),
							new DesignerVerb("Add Vertical Container", new EventHandler(CreateVerticalContainer)),
							new DesignerVerb("Add Text Box", new EventHandler(CreateTextBox)),
							new DesignerVerb("Add Combo Box", new EventHandler(CreateComboBox)),
							new DesignerVerb("Add Label", new EventHandler(CreateLabel)),
							new DesignerVerb("Add Color Picker", new EventHandler(CreateColorPicker)),
                            new DesignerVerb("Add Micro-Chart", new EventHandler(CreateMicroChart)),
                            new DesignerVerb("Add Switch Button", new EventHandler(CreateSwitch)),
                            new DesignerVerb("Add Progress bar", new EventHandler(CreateProgressBar)),
                            new DesignerVerb("Add Check box", new EventHandler(CreateCheckBox)),
                            new DesignerVerb("Add WinForms Control Container", new EventHandler(CreateControlContainer))
                        };
                return new DesignerVerbCollection(verbs);
            }
        }

        private void CreateVerticalContainer(object sender, EventArgs e)
        {
            CreateContainer(this.GetItemContainer(), eOrientation.Vertical);
        }

        private void CreateHorizontalContainer(object sender, EventArgs e)
        {
            CreateContainer(this.GetItemContainer(), eOrientation.Horizontal);
        }

        private void CreateContainer(BaseItem parent, eOrientation orientation)
        {
            m_CreatingItem = true;
            try
            {
                DesignerSupport.CreateItemContainer(this, parent, orientation);
            }
            finally
            {
                m_CreatingItem = false;
            }
            this.RecalcLayout();
        }

        protected override void OnIsSelectedChanged(bool oldValue, bool newValue)
        {
            if (newValue)
            {
                if (!this.Expanded)
                {
                    MetroToolbar bar = (MetroToolbar)this.Control;
                    bar.Expanded = true;
                }
            }
            else
            {
                if (!this.Expanded)
                {
                    MetroToolbar bar = (MetroToolbar)this.Control;
                    bar.Expanded = false;
                    IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    if (cc != null)
                        cc.OnComponentChanged(bar, TypeDescriptor.GetProperties(cc)["Location"], null, null);
                }
            }
            base.OnIsSelectedChanged(oldValue, newValue);
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);
            properties["Expanded"] = TypeDescriptor.CreateProperty(typeof(MetroToolbarDesigner), (PropertyDescriptor)properties["Expanded"], new Attribute[]
				{
					new DefaultValueAttribute(false),
					new BrowsableAttribute(true),
					new CategoryAttribute("Layout"),
                    new DescriptionAttribute("Indicates whether control is expanded or not. When control is expanded both main and extra toolbar items are visible.")});
        }

        /// <summary>
        /// Gets or sets whether control is expanded or not. When control is expanded both main and extra toolbar items are visible. When collapsed
        /// only main items are visible. Default value is false.
        /// </summary>
        [DefaultValue(false), Browsable(true), Category("Layout"), Description("Indicates whether control is expanded or not. When control is expanded both main and extra toolbar items are visible.")]
        public bool Expanded
        {
            get { return (bool)ShadowProperties["Expanded"]; }
            set
            {
                // this value is not passed to the actual control at design-time
                this.ShadowProperties["Expanded"] = value;
            }
        }

        public override System.Collections.ICollection AssociatedComponents
        {
            get
            {
                ArrayList c = new ArrayList(base.AssociatedComponents);
                MetroToolbar bar = (MetroToolbar)this.Control;
                foreach (BaseItem item in bar.Items)
                {
                    if (item.DesignMode)
                        c.Add(item);
                }
                foreach (BaseItem item in bar.ExtraItems)
                {
                    if (item.DesignMode)
                        c.Add(item);
                }
                
                return c;
            }
        }

        protected override ArrayList GetAllAssociatedComponents()
        {
            ArrayList c = new ArrayList(base.AssociatedComponents);
            MetroToolbar bar = (MetroToolbar)this.Control;
            BaseItem container = ((MetroToolbarContainer)bar.GetBaseItemContainer()).MainItemsContainer;
            if (container != null)
            {
                AddSubItems(container, c);
            }
            container = ((MetroToolbarContainer)bar.GetBaseItemContainer()).ExtraItemsContainer;
            if (container != null)
            {
                AddSubItems(container, c);
            }
            return c;
        }

        protected override BaseItem GetControlItem(System.Windows.Forms.Control control)
        {
            MetroToolbar bar = (MetroToolbar)this.Control;
            BaseItem parent = ((MetroToolbarContainer)bar.GetBaseItemContainer()).MainItemsContainer;
            if (parent == null)
                return null;
            BaseItem item = GetControlItem(control, parent);
            if (item != null) return item;

            parent = ((MetroToolbarContainer)bar.GetBaseItemContainer()).ExtraItemsContainer;
            if (parent == null)
                return null;
            item = GetControlItem(control, parent);
            return item;
        }

        protected override BaseItem GetDefaultNewItemContainer()
        {
            MetroToolbar bar = (MetroToolbar)this.Control;
            return ((MetroToolbarContainer)bar.GetBaseItemContainer()).MainItemsContainer;
        }

        #region Licensing Stuff
#if !TRIAL
        private string GetLicenseKey()
        {
            string key = "";
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.LocalMachine;
            regkey = regkey.OpenSubKey("Software\\DevComponents\\Licenses", false);
            if (regkey != null)
            {
                object keyValue = regkey.GetValue("DevComponents.DotNetBar.DotNetBarManager2");
                if (keyValue != null)
                    key = keyValue.ToString();
            }
            return key;
        }
        private void dh_LoadComplete(object sender, EventArgs e)
        {
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (dh != null)
                dh.LoadComplete -= new EventHandler(dh_LoadComplete);

            string key = GetLicenseKey();
            MetroToolbar bar = this.Control as MetroToolbar;
            if (key != "" && bar != null && bar.LicenseKey == "" && bar.LicenseKey != key)
                TypeDescriptor.GetProperties(bar)["LicenseKey"].SetValue(bar, key);
        }
#endif
        #endregion
    }
}
