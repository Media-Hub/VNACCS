using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Metro;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Windows.Forms;
using System.Collections;
using System.Drawing;
using System.IO;

namespace DevComponents.DotNetBar.Design
{
    // <summary>
    /// Represents Windows Forms designer for MetroTab control.
    /// </summary>
    public class MetroShellDesigner : BarBaseControlDesigner
    {
        #region Private Variables
        private bool m_QuickAccessToolbarSelected = false;
        #endregion

        #region Internal Implementation

        public MetroShellDesigner()
        {
            this.EnableItemDragDrop = true;
            this.AcceptExternalControls = false;
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            if (!component.Site.DesignMode)
                return;
            MetroShell c = component as MetroShell;
            if (c != null)
            {
                c.SetDesignMode();
                //this.Expanded = c.Expanded;
            }
            this.EnableDragDrop(false);
        }

        public override bool CanParent(Control control)
        {
            if (control is MetroTabPanel && !this.Control.Controls.Contains(control))
                return true;
            return false;
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                Bar bar = this.Control as Bar;
                DesignerVerb[] verbs = null;
                verbs = new DesignerVerb[]
					{
						new DesignerVerb("Create Tab", new EventHandler(CreateMetroTab)),
						new DesignerVerb("Create Button", new EventHandler(CreateButton)),
						new DesignerVerb("Create Text Box", new EventHandler(CreateTextBox)),
						new DesignerVerb("Create Combo Box", new EventHandler(CreateComboBox)),
                        new DesignerVerb("Create Label", new EventHandler(CreateLabel)),
                        new DesignerVerb("Create Micro-Chart", new EventHandler(CreateMicroChart)),
                        new DesignerVerb("Create Switch Button", new EventHandler(CreateSwitch)),
                        new DesignerVerb("Create Rating Item", new EventHandler(CreateRatingItem)),};

                return new DesignerVerbCollection(verbs);
            }
        }

        private BaseItem GetNewItemContainer()
        {
            MetroShell r = this.Control as MetroShell;
            if (m_QuickAccessToolbarSelected)
                return r.MetroTabStrip.CaptionContainerItem;
            else
                return r.MetroTabStrip.StripContainerItem;
        }

        protected override void CreateButton(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateButton(GetNewItemContainer());
            }
        }

        protected override void CreateTextBox(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateTextBox(GetNewItemContainer());
            }
        }

        protected override void CreateComboBox(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateComboBox(GetNewItemContainer());
            }
        }

        protected override void CreateLabel(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateLabel(GetNewItemContainer());
            }
        }

        protected override void CreateColorPicker(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateColorPicker(GetNewItemContainer());
            }
        }

        protected override void CreateProgressBar(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateProgressBar(GetNewItemContainer());
            }
        }

        protected override void CreateRatingItem(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateRatingItem(GetNewItemContainer());
            }
        }

        protected override void CreateSwitch(object sender, EventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                CreateSwitch(GetNewItemContainer());
            }
        }

        protected override void ComponentChangeComponentAdded(object sender, ComponentEventArgs e)
        {
            if (m_AddingItem)
            {
                m_AddingItem = false;
                IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                if (cc != null)
                    cc.OnComponentChanging(this.Control, null);
                this.GetNewItemContainer().SubItems.Add(e.Component as BaseItem);
                if (cc != null)
                    cc.OnComponentChanged(this.Control, null, null, null);
                m_InsertItemTransaction.Commit();
                m_InsertItemTransaction = null;
                this.RecalcLayout();
            }
        }

        protected override ArrayList GetAllAssociatedComponents()
        {
            ArrayList c = new ArrayList(base.AssociatedComponents);
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                AddSubItems(r.MetroTabStrip.StripContainerItem, c);
                AddSubItems(r.MetroTabStrip.CaptionContainerItem, c);
            }
            return c;
        }

        public override System.Collections.ICollection AssociatedComponents
        {
            get
            {
                ArrayList c = new ArrayList(this.BaseAssociatedComponents);
                MetroShell rc = this.Control as MetroShell;
                if (rc != null)
                {
                    foreach (BaseItem item in rc.QuickToolbarItems)
                    {
                        if (!item.SystemItem)
                            c.Add(item);
                    }
                    foreach (BaseItem item in rc.Items)
                    {
                        if (!item.SystemItem)
                            c.Add(item);
                    }
                }
                return c;
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
            SetDesignTimeDefaults();
            base.OnSetComponentDefaults();
        }
#endif

        private StyleManager CreateStyleManager()
        {
            StyleManager manager = null;

            IDesignerHost dh = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (dh != null)
            {
                DesignerTransaction trans = dh.CreateTransaction("Create Style Manager");
                try
                {
                    manager = dh.CreateComponent(typeof(StyleManager)) as StyleManager;
                }
                finally
                {
                    if (!trans.Canceled) trans.Commit();
                }
            }

            return manager;
        }
        private void SetDesignTimeDefaults()
        {
            CreateMetroTab("&HOME");
            CreateMetroTab("&VIEW");
            StyleManager manager = CreateStyleManager();
            MetroShell c = this.Control as MetroShell;
            if (c != null)
            {
                try
                {
                    c.KeyTipsFont = new Font("Tahoma", 7);
                    c.TabStripFont = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                }
                catch { }
            }
            //this.Style = eDotNetBarStyle.StyleManagerControlled;
            this.CanCustomize = true;
            c.Size = new Size(200, 154);
            c.Dock = DockStyle.Top;
            this.CaptionVisible = true;

            if (manager != null)
                manager.ManagerStyle = eStyle.Metro;
        }

        protected override BaseItem GetItemContainer()
        {
            MetroShell tab = this.Control as MetroShell;
            if (tab != null)
                return tab.MetroTabStrip.GetBaseItemContainer();
            return null;
        }

        protected override System.Windows.Forms.Control GetItemContainerControl()
        {
            MetroShell tab = this.Control as MetroShell;
            if (tab != null)
                return tab.MetroTabStrip;
            return null;
        }

        /// <summary>
        /// Support for popup menu closing.
        /// </summary>
        protected override void DesignTimeSelectionChanged(ISelectionService ss)
        {
            if (ss == null)
                return;
            if (this.Control == null || this.Control.IsDisposed)
                return;

            if (IsApplicationButtonBackstageControl(ss.PrimarySelection))
                return;

            base.DesignTimeSelectionChanged(ss);

            MetroShell r = this.Control as MetroShell;
            if (r == null) return;

            BaseItem container = r.MetroTabStrip.StripContainerItem;
            if (container == null) return;

            if (ss.PrimarySelection is MetroTabItem)
            {
                MetroTabItem item = ss.PrimarySelection as MetroTabItem;
                if (container.SubItems.Contains(item))
                {
                    TypeDescriptor.GetProperties(item)["Checked"].SetValue(item, true);
                }
            }
        }

        private bool IsApplicationButtonBackstageControl(object primarySelection)
        {
            MetroShell tab = this.Control as MetroShell;
            if (tab == null || !(tab.GetApplicationButton() is MetroAppButton)) return false;
            MetroAppButton appButton = tab.GetApplicationButton() as MetroAppButton;
            if (appButton.BackstageTab == null) return false;

            SuperTabControl backstageTab = appButton.BackstageTab;
            if (primarySelection is BaseItem)
            {
                BaseItem item = (BaseItem)primarySelection;
                if (backstageTab.Tabs.Contains(item)) return true;
                while (item.Parent != null) item = item.Parent;
                Control parentControl = item.ContainerControl as Control;
                if (parentControl == null) return false;
                return ContainsControl(backstageTab, parentControl);
            }
            else if (primarySelection is Control)
                return ContainsControl(backstageTab, (Control)primarySelection);

            return false;
        }

        private bool ContainsControl(Control container, Control childControl)
        {
            Control parent = childControl;
            while (parent != null)
            {
                if (parent == container) return true;
                parent = parent.Parent;
            }
            return false;
        }

        /// <summary>
        /// Triggered when some other component on the form is removed.
        /// </summary>
        protected override void OtherComponentRemoving(object sender, ComponentEventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (r != null)
            {
                BaseItem container = r.MetroTabStrip.StripContainerItem;

                if (e.Component is MetroTabItem && container != null && container.SubItems != null &&
                    container.SubItems.Contains(((MetroTabItem)e.Component)))
                {
                    IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    if (dh != null)
                        dh.DestroyComponent(((MetroTabItem)e.Component).Panel);
                }
            }
            base.OtherComponentRemoving(sender, e);
        }

        protected override void ComponentRemoved(object sender, ComponentEventArgs e)
        {
            MetroShell r = this.Control as MetroShell;
            if (e.Component is BaseItem && r != null)
            {
                BaseItem item = e.Component as BaseItem;
                if (r.Items.Contains(item))
                    r.Items.Remove(item);
                else if (r.QuickToolbarItems.Contains(item))
                    r.QuickToolbarItems.Remove(item);
                DestroySubItems(item);
            }
        }

        protected override bool OnMouseDown(ref Message m, MouseButtons button)
        {
            m_QuickAccessToolbarSelected = false;

            System.Windows.Forms.Control ctrl = this.GetItemContainerControl();
            MetroTabStrip strip = ctrl as MetroTabStrip;

            if (strip == null)
                return base.OnMouseDown(ref m, button);

            Point pos = strip.PointToClient(System.Windows.Forms.Control.MousePosition);
            if (!strip.ClientRectangle.Contains(pos))
                return base.OnMouseDown(ref m, button);

            if (button == MouseButtons.Right)
            {
                if (strip.QuickToolbarBounds.Contains(pos))
                    m_QuickAccessToolbarSelected = true;
                return base.OnMouseDown(ref m, button);
            }

            bool callBase = true;

            if (callBase)
                return base.OnMouseDown(ref m, button);
            else
                return true;
        }

        #endregion

        #region Design-Time Item Creation
        protected virtual void CreateMetroTab(object sender, EventArgs e)
        {
            CreateMetroTab(string.Empty);
        }

        private void CreateMetroTab(string text)
        {
            m_QuickAccessToolbarSelected = false;
            IDesignerHost dh = (IDesignerHost)GetService(typeof(IDesignerHost));
            if (dh != null)
            {
                DesignerTransaction trans = dh.CreateTransaction("Creating Metro Tab");
                IComponentChangeService cc = (IComponentChangeService)GetService(typeof(IComponentChangeService));
                MetroShell tab = this.Control as MetroShell;
                try
                {
                    m_CreatingItem = true;
                    OnSubItemsChanging();
                    MetroTabItem item = dh.CreateComponent(typeof(MetroTabItem)) as MetroTabItem;
                    if (string.IsNullOrEmpty(text))
                        TypeDescriptor.GetProperties(item)["Text"].SetValue(item, item.Name);
                    else
                        TypeDescriptor.GetProperties(item)["Text"].SetValue(item, text);

                    MetroTabPanel panel = dh.CreateComponent(typeof(MetroTabPanel)) as MetroTabPanel;
                    TypeDescriptor.GetProperties(panel)["Dock"].SetValue(panel, DockStyle.Fill);
                    //TypeDescriptor.GetProperties(panel)["ColorSchemeStyle"].SetValue(panel, tab.Style);
                    tab.SetTabPanelStyle(panel);
                    
                    //panel.Style.BorderBottom = eStyleBorderType.Solid;
                    //TypeDescriptor.GetProperties(panel.Style)["BorderBottom"].SetValue(panel.Style, eStyleBorderType.Solid);

                    cc.OnComponentChanging(this.Control, TypeDescriptor.GetProperties(typeof(Control))["Controls"]);
                    this.Control.Controls.Add(panel);
                    panel.SendToBack();
                    cc.OnComponentChanged(this.Control, TypeDescriptor.GetProperties(typeof(Control))["Controls"], null, null);

                    TypeDescriptor.GetProperties(item)["Panel"].SetValue(item, panel);

                    GenericItemContainer cont = tab.MetroTabStrip.StripContainerItem;
                    cc.OnComponentChanging(cont, TypeDescriptor.GetProperties(typeof(BaseItem))["SubItems"]);
                    cont.SubItems.Add(item);
                    cc.OnComponentChanged(cont, TypeDescriptor.GetProperties(typeof(BaseItem))["SubItems"], null, null);
                    if (cont.SubItems.Count == 1)
                        TypeDescriptor.GetProperties(item)["Checked"].SetValue(item, true);

                    this.RecalcLayout();
                    OnSubItemsChanged();
                }
                catch
                {
                    trans.Cancel();
                    throw;
                }
                finally
                {
                    if (!trans.Canceled)
                        trans.Commit();
                    m_CreatingItem = false;
                }
            }
        }

        #endregion

        #region Shadowing
        /// <summary>
        /// Gets or sets whether custom caption and quick access toolbar provided by the control is visible. Default value is false.
        /// This property should be set to true when control is used on MetroForm.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Appearance"), Description("Indicates whether custom caption and quick access toolbar provided by the control is visible.")]
        public bool CaptionVisible
        {
            get
            {
                MetroShell r = this.Control as MetroShell;
                return r.CaptionVisible;
            }
            set
            {
                MetroShell r = this.Control as MetroShell;
                if (r.CaptionVisible == value) return;
                r.CaptionVisible = value;
                
                if (r.CaptionVisible && (
                    r.QuickToolbarItems.Count == 6 && r.QuickToolbarItems[0] is SystemCaptionItem && r.QuickToolbarItems[5] is SystemCaptionItem))
                {
                    // Add custom items to the toolbar
                    IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
                    IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                    if (dh != null && !dh.Loading && cc != null && dh.TransactionDescription != "Paste components")
                    {
                        DesignerTransaction trans = dh.CreateTransaction();
                        try
                        {
                            m_CreatingItem = true;
                            MetroAppButton sb = dh.CreateComponent(typeof(MetroAppButton)) as MetroAppButton;
                            sb.Text = "&File";
                            sb.ShowSubItems = false;
                            sb.CanCustomize = false;
                            sb.AutoExpandOnClick = true;
                            cc.OnComponentChanging(r, TypeDescriptor.GetProperties(r)["QuickToolbarItems"]);
                            sb.ImageFixedSize = new Size(16, 16);
                            r.Items.Insert(0, sb);
                            cc.OnComponentChanged(r, TypeDescriptor.GetProperties(r)["QuickToolbarItems"], null, null);
                            ButtonItem buttonStart = sb;

                            ButtonItem b = dh.CreateComponent(typeof(ButtonItem)) as ButtonItem;
                            b.Text = b.Name;
                            cc.OnComponentChanging(r, TypeDescriptor.GetProperties(r)["QuickToolbarItems"]);
                            r.QuickToolbarItems.Add(b, 2);
                            cc.OnComponentChanged(r, TypeDescriptor.GetProperties(r)["QuickToolbarItems"], null, null);

                            
                        }
                        catch
                        {
                            trans.Cancel();
                        }
                        finally
                        {
                            if (!trans.Canceled) trans.Commit();
                            m_CreatingItem = false;
                        }
                    }
                }
            }
        }

        private ButtonItem CreateFileButton(IDesignerHost dh, string text, Image image)
        {
            ButtonItem button = dh.CreateComponent(typeof(ButtonItem)) as ButtonItem;
            button.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.ImageAndText;
            button.Image = image;
            button.SubItemsExpandWidth = 24;
            button.Text = text;

            return button;
        }

        private ButtonItem CreateMRUButton(IDesignerHost dh, string text)
        {
            ButtonItem button = dh.CreateComponent(typeof(ButtonItem)) as ButtonItem;
            button.Text = text;

            return button;
        }


        internal static Bitmap LoadImage(string imageName)
        {
            string imagesFolder = GetImagesFolder();
            if (imagesFolder != "")
            {
                if (File.Exists(imagesFolder + imageName))
                    return new Bitmap(imagesFolder + imageName);
            }

            return null;
        }

        private static string GetImagesFolder()
        {
            try
            {
                Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.LocalMachine;
                string path = "";
                try
                {
                    if (key != null)
                        key = key.OpenSubKey("Software\\DevComponents\\DotNetBar");
                    if (key != null)
                        path = key.GetValue("InstallationFolder", "").ToString();
                }
                finally { if (key != null) key.Close(); }

                if (path != "")
                {
                    if (path.Substring(path.Length - 1, 1) != "\\")
                        path += "\\";
                    path += "Images\\";
                    return path;
                }
            }
            catch (Exception)
            {
            }
            return "";
        }

        /// <summary>
        /// Gets or sets whether control can be customized and items added by end-user using context menu to the quick access toolbar.
        /// Caption of the control must be visible for customization to be enabled. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Quick Access Toolbar"), Description("Indicates whether control can be customized. Caption must be visible for customization to be fully enabled.")]
        public bool CanCustomize
        {
            get
            {
                MetroShell rc = this.Control as MetroShell;
                return rc.CanCustomize;
            }
            set
            {
                MetroShell rc = this.Control as MetroShell;
                rc.CanCustomize = value;

                IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
                if (dh != null && !dh.Loading)
                {
                    if (value)
                    {
                        // Make sure that QatCustomizeItem exists
                        QatCustomizeItem qatCustom = GetQatCustomizeItem(rc);
                        if (qatCustom == null)
                        {
                            DesignerTransaction dt = dh.CreateTransaction("Creating the QAT");
                            try
                            {
                                // Create QatCustomItem...
                                m_CreatingItem = true;
                                qatCustom = dh.CreateComponent(typeof(QatCustomizeItem)) as QatCustomizeItem;
                                qatCustom.BeginGroup = true;
                                IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (cc != null)
                                    cc.OnComponentChanging(rc, TypeDescriptor.GetProperties(rc)["QuickToolbarItems"]);
                                rc.QuickToolbarItems.Add(qatCustom);
                                if (cc != null)
                                    cc.OnComponentChanged(rc, TypeDescriptor.GetProperties(rc)["QuickToolbarItems"], null, null);
                                m_CreatingItem = false;
                                this.RecalcLayout();
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
                    }
                    else
                    {
                        QatCustomizeItem qatCustom = GetQatCustomizeItem(rc);
                        if (qatCustom != null)
                        {
                            DesignerTransaction dt = dh.CreateTransaction("Removing the QAT");
                            try
                            {
                                IComponentChangeService cc = this.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
                                if (cc != null)
                                    cc.OnComponentChanging(rc, TypeDescriptor.GetProperties(rc)["QuickToolbarItems"]);
                                rc.QuickToolbarItems.Remove(qatCustom);
                                if (cc != null)
                                    cc.OnComponentChanged(rc, TypeDescriptor.GetProperties(rc)["QuickToolbarItems"], null, null);
                                dh.DestroyComponent(qatCustom);
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
                    }
                }
            }
        }

        private QatCustomizeItem GetQatCustomizeItem(MetroShell rc)
        {
            QatCustomizeItem qatCustom = null;
            // Remove QatCustomizeItem if it exists
            foreach (BaseItem item in rc.QuickToolbarItems)
            {
                if (item is QatCustomizeItem)
                {
                    qatCustom = item as QatCustomizeItem;
                    break;
                }
            }
            return qatCustom;
        }

        protected override void PreFilterProperties(IDictionary properties)
        {
            base.PreFilterProperties(properties);

            properties["CaptionVisible"] = TypeDescriptor.CreateProperty(typeof(MetroShellDesigner), (PropertyDescriptor)properties["CaptionVisible"], new Attribute[]
				{
					new DefaultValueAttribute(false),
					new BrowsableAttribute(true),
					new CategoryAttribute("Appearance"),
                    new DescriptionAttribute("Indicates whether custom caption and quick access toolbar provided by the control is visible.")});

            properties["CanCustomize"] = TypeDescriptor.CreateProperty(typeof(MetroShellDesigner), (PropertyDescriptor)properties["CanCustomize"], new Attribute[]
				{
					new DefaultValueAttribute(true),
					new BrowsableAttribute(true),
					new CategoryAttribute("Quick Access Toolbar"),
                    new DescriptionAttribute("Indicates whether control can be customized. Caption must be visible for customization to be fully enabled")});

            //DesignTime.RemoveProperties(properties, new string[] { "AccessibleDescription" 
            //    ,"AccessibleName","AccessibleRole","AutoScroll", "AutoScrollMargin", "AutoScrollMinSize",
            //    "BackColor", "BackgroundImage", "BackgroundImageLayout", "DisplayRectangle", "ForeColor",
            //    "MaximumSize", "MinimumSize", "Site","Text","UseWaitCursor", "Cursor","ContextMenuStrip"});
        }

        #endregion
    }
}
