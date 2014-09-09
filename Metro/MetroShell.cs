using System.Drawing;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System;
using System.Collections;
using System.Text;
using DevComponents.DotNetBar.Rendering;
using System.Collections.Generic;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
    /// Represents Metro Tab control, usually used as application tab but can be used as standard tab control as well.
    /// </summary>
    [ToolboxBitmap(typeof(MetroShell), "MetroShell.ico"), ToolboxItem(true)
   // , Designer(typeof(DevComponents.DotNetBar.Design.MetroShellDesigner, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf")
   , Designer(typeof(DevComponents.DotNetBar.Design.MetroShellDesigner))
    , System.Runtime.InteropServices.ComVisible(false)]
    public class MetroShell : System.Windows.Forms.ContainerControl
    {
        #region Events
        /// <summary>
        /// Occurs just before the customize popup menu is displayed and provides the ability to cancel the menu display as well
        /// as to add/remove the menu items from the customize popup menu.
        /// </summary>
        public event CustomizeMenuPopupEventHandler BeforeCustomizeMenuPopup;

        /// <summary>
        /// Occurs before an item is added to the quick access toolbar as result of user action. This event provides ability to
        /// cancel the addition of the item by setting the Cancel=true of event arguments.
        /// </summary>
        public event CustomizeMenuPopupEventHandler BeforeAddItemToQuickAccessToolbar;

        /// <summary>
        /// Occurs before an item is removed from the quick access toolbar as result of user action. This event provides ability to
        /// cancel the addition of the item by setting the Cancel=true of event arguments.
        /// </summary>
        public event CustomizeMenuPopupEventHandler BeforeRemoveItemFromQuickAccessToolbar;

        /// <summary>
        /// Occurs when DotNetBar is looking for translated text for one of the internal text that are
        /// displayed on menus, toolbars and customize forms. You need to set Handled=true if you want
        /// your custom text to be used instead of the built-in system value.
        /// </summary>
        public event DotNetBarManager.LocalizeStringEventHandler LocalizeString;

        /// <summary>
        /// Occurs when Item on metro tab strip or quick access toolbar is clicked.
        /// </summary>
        [Description("Occurs when Item on metro tab strip or quick access toolbar is clicked.")]
        public event EventHandler ItemClick;

        /// <summary>
        /// Occurs before Quick Access Toolbar dialog is displayed. This event provides the opportunity to cancel the showing of
        /// built-in dialog and display custom customization dialog. You can also set the Dialog property of the event arguments to
        /// the custom dialog you want used instead of the DotNetBar system customization dialog.
        /// </summary>
        public event QatCustomizeDialogEventHandler BeforeQatCustomizeDialog;
        /// <summary>
        /// Occurs after the Quick Access Toolbar dialog is closed.
        /// </summary>
        public event QatCustomizeDialogEventHandler AfterQatCustomizeDialog;

        /// <summary>
        /// Occurs after any changes done on the Quick Access Toolbar dialog are applied to the actual Quick Access Toolbar.
        /// </summary>
        public event EventHandler AfterQatDialogChangesApplied;

        /// <summary>
        ///     Occurs after selected Metro tab has changed. You can use
        ///     <see cref="SelectedTab">MetroShell.SelectedTab</see>
        ///     property to get reference to newly selected tab.
        /// </summary>
        public event EventHandler SelectedTabChanged;

        /// <summary>
        /// Occurs when text markup link from TitleText markup is clicked. Markup links can be created using "a" tag, for example:
        /// <a name="MyLink">Markup link</a>
        /// </summary>
        [Description("Occurs when text markup link from TitleText markup is clicked.")]
        public event MarkupLinkClickEventHandler TitleTextMarkupLinkClick;

        /// <summary>
        /// Occurs when SETTINGS button, if displayed, is clicked.
        /// </summary>
        [Description("Occurs when SETTINGS button, if displayed, is clicked.")]
        public event EventHandler SettingsButtonClick;
        /// <summary>
        /// Raises SettingsButtonClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnSettingsButtonClick(EventArgs e)
        {
            EventHandler handler = SettingsButtonClick;
            if (handler != null)
                handler(this, e);
        }
        internal void InvokeSettingsButtonClick(EventArgs e)
        {
            OnSettingsButtonClick(e);
        }

        /// <summary>
        /// Occurs when HELP button, if displayed, is clicked.
        /// </summary>
        [Description("Occurs when HELP button, if displayed, is clicked.")]
        public event EventHandler HelpButtonClick;
        /// <summary>
        /// Raises HelpButtonClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnHelpButtonClick(EventArgs e)
        {
            EventHandler handler = HelpButtonClick;
            if (handler != null)
                handler(this, e);
        }
        internal void InvokeHelpButtonClick(EventArgs e)
        {
            OnHelpButtonClick(e);
        }
        #endregion

        #region Private Variables and Constructor
        private MetroTabStrip _TabStrip = null;
        private bool m_AutoSize = false;
        private ShadowPaintInfo m_ShadowPaintInfo = null;
        private bool _UseCustomizeDialog = true;
        //private bool m_EnableQatPlacement = true;

        private eMetroCategorizeMode m_CategorizeMode = eMetroCategorizeMode.Toolbar;
        private DevComponents.DotNetBar.Ribbon.QatToolbar m_QatToolbar = null;
        private int DefaultBottomDockPadding = 0;
        private DevComponents.DotNetBar.Ribbon.SubItemsQatCollection m_QatSubItemsCollection = null;
        private bool m_QatLayoutChanged = false;
        private RibbonLocalization m_SystemText = new RibbonLocalization();
        private bool _UseExternalCustomization = false;
        private const string SYS_CUSTOMIZE_POPUP_MENU = "syscustomizepopupmenu";
        private bool m_MenuTabsEnabled = true;
        private ContextMenuBar m_GlobalContextMenuBar = null;
        private SubItemsCollection _QatFrequentCommands = new SubItemsCollection(null);

        /// <summary>
        /// Gets the name of the QAT Customize Item which is used to display the QAT Customize Dialog box.
        /// </summary>
        public static readonly string SysQatCustomizeItemName = "sysCustomizeQuickAccessToolbar";
        /// <summary>
        /// Gets the name of the Add to Quick Access Toolbar context menu item.
        /// </summary>
        public static readonly string SysQatAddToItemName = "sysAddToQuickAccessToolbar";
        /// <summary>
        /// Gets the name of the Remove from Quick Access Toolbar context menu item.
        /// </summary>
        public static readonly string SysQatRemoveFromItemName = "sysRemoveFromQuickAccessToolbar";
        /// <summary>
        /// Gets the name of the QAT placement change context menu item.
        /// </summary>
        public static readonly string SysQatPlaceItemName = "sysPlaceQuickAccessToolbar";
        ///// <summary>
        ///// Gets the name of the Minimize Ribbon Item which is used to minimize the ribbon.
        ///// </summary>
        //public static readonly string SysMinimizeRibbon = "sysMinimizeRibbon";
        ///// <summary>
        ///// Gets the name of the Maximize Ribbon Item which is used to maximize the ribbon.
        ///// </summary>
        //public static readonly string SysMaximizeRibbon = "sysMaximizeRibbon";
        /// <summary>
        /// Gets the name of the label displayed on Quick Access Toolbar customize popup menu.
        /// </summary>
        public static readonly string SysQatCustomizeLabelName = "sysCustomizeQuickAccessToolbarLabel";
        /// <summary>
        /// Gets the string that is used as starting name for the frequently used QAT menu items created when QAT Customize menu is displayed.
        /// </summary>
        public static readonly string SysFrequentlyQatNamePart = "sysQatFrequent_";

        public MetroShell()
        {
            // This forces the initialization out of paint loop which speeds up how fast components show up
            BaseRenderer renderer = DevComponents.DotNetBar.Rendering.GlobalManager.Renderer;

            _QatFrequentCommands.IgnoreEvents = true;
            _QatFrequentCommands.AllowParentRemove = false;

            this.SetStyle(ControlStyles.AllPaintingInWmPaint
                  | ControlStyles.ResizeRedraw
                  | DisplayHelp.DoubleBufferFlag
                  | ControlStyles.UserPaint
                  | ControlStyles.Opaque
                  , true);
            _TabStrip = new MetroTabStrip();
            _TabStrip.Dock = DockStyle.Top;
            _TabStrip.Height = 32;
            _TabStrip.ItemAdded += new System.EventHandler(TabStripItemAdded);
            _TabStrip.LocalizeString += new DotNetBarManager.LocalizeStringEventHandler(TabStripLocalizeString);
            _TabStrip.ItemClick += new System.EventHandler(TabStripItemClick);
            _TabStrip.ButtonCheckedChanged += new EventHandler(TabStripButtonCheckedChanged);
            _TabStrip.TitleTextMarkupLinkClick += new MarkupLinkClickEventHandler(TabStripTitleTextMarkupLinkClick);
            this.Controls.Add(_TabStrip);
            this.TabStop = false;
            this.DockPadding.Bottom = DefaultBottomDockPadding;

            StyleManager.Register(this);
        }
        protected override void Dispose(bool disposing)
        {
            StyleManager.Unregister(this);
            base.Dispose(disposing);
        }
        #endregion

        #region Internal Implementation
        /// <summary>
        /// Called by StyleManager to notify control that style on manager has changed and that control should refresh its appearance if
        /// its style is controlled by StyleManager.
        /// </summary>
        /// <param name="newStyle">New active style.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void StyleManagerStyleChanged(eDotNetBarStyle newStyle)
        {

        }

        /// <summary>
        /// Gets the collection of the Quick Access Toolbar Frequently used commands. You should add existing buttons to this collection that
        /// you already have on the MetroToolbar controls or on the application menu. The list will be used to construct the frequently used
        /// menu that is displayed when Customize Quick Access Toolbar menu is displayed and it allows end-user to remove and add these
        /// frequently used commands to the QAT directly from this menu.
        /// Note that items you add here should not be items that are already on Quick Access Toolbar, i.e. in MetroShell.QuickToolbarItems collection.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SubItemsCollection QatFrequentCommands
        {
            get
            {
                return _QatFrequentCommands;
            }
        }

        /// <summary>
        /// Gets or sets whether KeyTips functionality is enabled. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Behavior"), Description("Indicates whether KeyTips functionality is enabled.")]
        public bool KeyTipsEnabled
        {
            get { return _TabStrip.KeyTipsEnabled; }
            set { _TabStrip.KeyTipsEnabled = value; }
        }

        internal bool IsDesignMode
        {
            get { return this.DesignMode; }
        }

        protected virtual void OnSelectedTabChanged(EventArgs e)
        {
            if (SelectedTabChanged != null)
                SelectedTabChanged(this, e);
        }

        void TabStripButtonCheckedChanged(object sender, EventArgs e)
        {
            if (sender is MetroTabItem && ((MetroTabItem)sender).Checked)
            {
                OnSelectedTabChanged(new EventArgs());
            }
        }

        private void TabStripItemClick(object sender, System.EventArgs e)
        {
            if (ItemClick != null)
                ItemClick(sender, e);
        }

        private void TabStripLocalizeString(object sender, LocalizeEventArgs e)
        {
            if (LocalizeString != null)
                LocalizeString(this, e);
        }

        /// <summary>
        /// Gets or sets whether anti-alias smoothing is used while painting. Default value is false.
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Appearance"), Description("Gets or sets whether anti-aliasing is used while painting.")]
        public bool AntiAlias
        {
            get { return _TabStrip.AntiAlias; }
            set
            {
                _TabStrip.AntiAlias = value;
                this.Invalidate();
            }
        }

        internal DevComponents.DotNetBar.Rendering.Office2007ColorTable GetOffice2007ColorTable()
        {
            DevComponents.DotNetBar.Rendering.Office2007Renderer r = DevComponents.DotNetBar.Rendering.GlobalManager.Renderer as DevComponents.DotNetBar.Rendering.Office2007Renderer;
            if (r != null)
                return r.ColorTable;
            return new DevComponents.DotNetBar.Rendering.Office2007ColorTable();
        }

        /// <summary>
        /// Performs the setup of the MetroTabPanel with the current style of the MetroShell Control.
        /// </summary>
        /// <param name="panel">Panel to apply style changes to.</param>
        public void SetTabPanelStyle(MetroTabPanel panel)
        {
            if (this.DesignMode)
            {
                TypeDescriptor.GetProperties(panel.DockPadding)["Left"].SetValue(panel.DockPadding, 3);
                TypeDescriptor.GetProperties(panel.DockPadding)["Right"].SetValue(panel.DockPadding, 3);
                TypeDescriptor.GetProperties(panel.DockPadding)["Bottom"].SetValue(panel.DockPadding, 3);
            }
            else
            {
                panel.DockPadding.Left = 3;
                panel.DockPadding.Right = 3;
                panel.DockPadding.Bottom = 3;
            }
        }

        /// <summary>
        /// Creates new Rendering Tab at specified position, creates new associated panel and adds them to the control.
        /// </summary>
        /// <param name="text">Specifies the text displayed on the tab.</param>
        /// <param name="name">Specifies the name of the tab</param>
        /// <param name="insertPosition">Specifies the position of the new tab inside of Items collection.</param>
        /// <returns>New instance of the MetroTabItem that was created.</returns>
        public MetroTabItem CreateTab(string text, string name, int insertPosition)
        {
            MetroTabItem item = new MetroTabItem();
            item.Text = text;
            item.Name = name;

            MetroTabPanel panel = new MetroTabPanel();
            panel.Dock = DockStyle.Fill;
            SetTabPanelStyle(panel);
            this.Controls.Add(panel);
            panel.SendToBack();

            item.Panel = panel;
            if (insertPosition < 0)
            {
                insertPosition = this.Items.Count;
                for (int i = 0; i < this.Items.Count; i++)
                {
                    if (this.Items[i].ItemAlignment == eItemAlignment.Far)
                    {
                        insertPosition = i;
                        break;
                    }
                }
                if (insertPosition >= this.Items.Count)
                    this.Items.Add(item);
                else
                    this.Items.Insert(insertPosition, item);
            }
            else if (insertPosition > this.Items.Count - 1)
                this.Items.Add(item);
            else
                this.Items.Insert(insertPosition, item);

            return item;
        }

        /// <summary>
        /// Creates new Rendering Tab and associated panel and adds them to the control.
        /// </summary>
        /// <param name="text">Specifies the text displayed on the tab.</param>
        /// <param name="name">Specifies the name of the tab</param>
        /// <returns>New instance of the MetroTabItem that was created.</returns>
        public MetroTabItem CreateTab(string text, string name)
        {
            return CreateTab(text, name, -1);
        }

        /// <summary>
        /// Recalculates layout of the control and applies any changes made to the size or position of the items contained.
        /// </summary>
        public void RecalcLayout()
        {
            _TabStrip.RecalcLayout();
            if (m_QatToolbar != null && m_QatToolbar.Visible)
                m_QatToolbar.RecalcLayout();
        }

        protected override void OnHandleCreated(System.EventArgs e)
        {
            base.OnHandleCreated(e);
            foreach (Control c in this.Controls)
            {
                IntPtr h = c.Handle;
                if (c is MetroTabPanel)
                {
                    foreach (Control r in c.Controls)
                        h = r.Handle;
                }
            }
            this.RecalcLayout();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (this.DesignMode)
                return;
            MetroTabPanel panel = e.Control as MetroTabPanel;
            if (panel == null)
                return;
            if (panel.MetroTabItem != null)
            {
                if (this.Items.Contains(panel.MetroTabItem) && this.SelectedTab == panel.MetroTabItem)
                {
                    panel.Visible = true;
                    panel.BringToFront();
                }
                else
                    panel.Visible = false;
            }
            else
                panel.Visible = false;
        }

        private void TabStripItemAdded(object sender, System.EventArgs e)
        {
            if (this.DesignMode)
                return;

            if (sender is MetroTabItem)
            {
                MetroTabItem tab = sender as MetroTabItem;
                if (tab.Panel != null)
                {
                    if (this.Controls.Contains(tab.Panel) && tab.Checked)
                    {
                        tab.Panel.Visible = true;
                        tab.Panel.BringToFront();
                    }
                    else
                        tab.Panel.Visible = false;
                }
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);
            MetroTabPanel panel = e.Control as MetroTabPanel;
            if (panel == null)
                return;
            if (panel.MetroTabItem != null)
            {
                if (this.Items.Contains(panel.MetroTabItem))
                    this.Items.Remove(panel.MetroTabItem);
            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDesignMode()
        {
            _TabStrip.SetDesignMode(true);
        }

        private ElementStyle GetBackgroundStyle()
        {
            return _TabStrip.InternalGetBackgroundStyle();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            ElementStyle style = GetBackgroundStyle();
            if (style.BackColor.A < 255 && !style.BackColor.IsEmpty ||
                this.BackColor == Color.Transparent || this.BackgroundImage != null)
            {
                base.OnPaintBackground(e);
            }
            else
            {
                using (SolidBrush brush = new SolidBrush(this.BackColor))
                    e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            ElementStyleDisplayInfo info = new ElementStyleDisplayInfo(style, e.Graphics, this.ClientRectangle);
            ElementStyleDisplay.PaintBackground(info);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WinApi.WindowsMessages.WM_NCHITTEST)
            {
                // Get position being tested...
                int x = WinApi.LOWORD(m.LParam);
                int y = WinApi.HIWORD(m.LParam);
                Point p = PointToClient(new Point(x, y));

                if (this.CaptionVisible && _TabStrip != null && !_TabStrip.IsMaximized)
                {
                    MetroAppForm form = this.FindForm() as MetroAppForm;
                    if (form == null || form.Sizable)
                    {
                        int formBorderSize = 4;
                        Rectangle r = new Rectangle(0, 0, this.Width, formBorderSize);
                        if (r.Contains(p)) // Top side form resizing
                        {
                            m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                            return;
                        }
                        r = new Rectangle(0, 0, formBorderSize, this.Height); // Left side form resizing
                        if (r.Contains(p))
                        {
                            m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                            return;
                        }
                        r = new Rectangle(this.Width - formBorderSize, 0, formBorderSize, this.Height); // Right side form resizing
                        if (r.Contains(p))
                        {
                            m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                            return;
                        }
                    }
                }
                if (BarFunctions.IsWindows7 && _TabStrip != null && _TabStrip.IsMaximized)
                {
                    Rectangle r = _TabStrip.CaptionBounds;
                    if (r.Contains(p))
                    {
                        m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                        return;
                    }
                }
                // System Icon
                if ((p.X < 28 && this.RightToLeft == RightToLeft.No || p.X > this.Width - 28 && this.RightToLeft == RightToLeft.Yes) && p.Y < 28)
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                    return;
                }
            }
            base.WndProc(ref m);
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the rich text displayed on title bar instead of the Form.Text property. This property supports text-markup.
        /// You can use <font color="SysCaptionTextExtra"> markup to instruct the markup renderer to use Office 2007 system caption extra text color which
        /// changes depending on the currently selected color table. Note that when using this property you should manage also the Form.Text property since
        /// that is the text that will be displayed in Windows task-bar and elsewhere where system Form.Text property is used.
        /// You can also use the hyperlinks as part of the text markup and handle the TitleTextMarkupLinkClick event to be notified when they are clicked.
        /// </summary>
        [Browsable(true), DefaultValue(""), Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor)), EditorBrowsable(EditorBrowsableState.Always), Category("Appearance"), Description("Indicates text displayed on Title bar instead of the Form.Text property.")]
        public string TitleText
        {
            get { return _TabStrip.TitleText; }
            set { _TabStrip.TitleText = value; }
        }

        /// <summary>
        /// Occurs when text markup link is clicked.
        /// </summary>
        private void TabStripTitleTextMarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
        {
            if (TitleTextMarkupLinkClick != null)
                TitleTextMarkupLinkClick(this, new MarkupLinkClickEventArgs(e.Name, e.HRef));
        }

        /// <summary>
        /// Gets or sets the Context menu bar associated with the this control which is used as part of Global Items feature. The context menu 
        /// bar assigned here will be used to search for the items with the same Name or GlobalName property so global properties can be propagated when changed.
        /// You should assign this property to enable the Global Items feature to reach your ContextMenuBar.
        /// </summary>
        [DefaultValue(null), Description("Indicates Context menu bar associated with the MetroShell control which is used as part of Global Items feature."), Category("Data")]
        public ContextMenuBar GlobalContextMenuBar
        {
            get { return m_GlobalContextMenuBar; }
            set
            {
                if (m_GlobalContextMenuBar != null)
                    m_GlobalContextMenuBar.GlobalParentComponent = null;
                m_GlobalContextMenuBar = value;
                if (m_GlobalContextMenuBar != null)
                    m_GlobalContextMenuBar.GlobalParentComponent = this;
            }
        }

        /// <summary>
        /// Gets or sets whether custom caption and quick access toolbar provided by the control is visible. Default value is false.
        /// This property should be set to true when control is used on MetroAppForm.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Caption"), Description("Indicates whether custom caption and quick access toolbar provided by the control is visible.")]
        public bool CaptionVisible
        {
            get { return _TabStrip.CaptionVisible; }
            set
            {
                _TabStrip.CaptionVisible = value;
            }
        }

        /// <summary>
        /// Gets or sets the font for the form caption text when CaptionVisible=true. Default value is NULL which means that system font is used.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Caption"), Description("Indicates font for the form caption text when CaptionVisible=true.")]
        public Font CaptionFont
        {
            get { return _TabStrip.CaptionFont; }
            set
            {
                _TabStrip.CaptionFont = value;
            }
        }

        /// <summary>
        /// Gets or sets the explicit height of the caption provided by control. Caption height when set is composed of the TabGroupHeight and
        /// the value specified here. Default value is 0 which means that system default caption size is used.
        /// </summary>
        [Browsable(true), DefaultValue(0), Category("Caption"), Description("Indicates explicit height of the caption provided by control")]
        public int CaptionHeight
        {
            get { return _TabStrip.CaptionHeight; }
            set
            {
                _TabStrip.CaptionHeight = value;
                this.RecalcLayout();
            }
        }

        /// <summary>
        /// Gets or sets the font that is used to display Key Tips (accelerator keys) when they are displayed. Default value is null which means
        /// that control Font is used for Key Tips display.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Appearance"), Description("Indicates font that is used to display Key Tips (accelerator keys) when they are displayed.")]
        public virtual Font KeyTipsFont
        {
            get { return _TabStrip.KeyTipsFont; }
            set { _TabStrip.KeyTipsFont = value; }
        }

        /// <summary>
        /// Specifies the background style of the control.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), Category("Style"), Description("Gets or sets bar background style."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ElementStyle BackgroundStyle
        {
            get { return _TabStrip.BackgroundStyle; }
        }

        /// <summary>
        /// Gets or sets the currently selected MetroTabItem. MetroTabItems are selected using the Checked property. Only a single
        /// MetroTabItem can be selected (Checked) at any given time.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroTabItem SelectedTab
        {
            get { return _TabStrip.SelectedTab; }
            set
            {
                if (value != null) value.Checked = true;
            }
        }

        /// <summary>
        /// Returns reference to internal metro tab-strip control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public MetroTabStrip MetroTabStrip
        {
            get { return _TabStrip; }
        }

        /// <summary>
        /// Returns collection of items on a bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection Items
        {
            get { return _TabStrip.Items; }
        }

        /// <summary>
        /// Returns collection of quick toolbar access and caption items.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection QuickToolbarItems
        {
            get { return GetQatSubItemsCollection(); }
        }

        private SubItemsCollection GetQatSubItemsCollection()
        {
            return _TabStrip.QuickToolbarItems;
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public eDotNetBarStyle EffectiveStyle
        {
            get
            {
                return _TabStrip.EffectiveStyle;
            }
        }

        private static BaseItem GetAppButton(MetroShell tab)
        {
            BaseItem appButton = null;
            for (int i = 0; i < tab.QuickToolbarItems.Count; i++)
            {
                if (tab.QuickToolbarItems[i] is MetroAppButton)
                {
                    appButton = tab.QuickToolbarItems[i];
                    break;
                }

            }
            return appButton;
        }
        protected override void OnBackColorChanged(EventArgs e)
        {
            base.OnBackColorChanged(e);
        }

        internal void OnChildItemClick(BaseItem item)
        {
            _TabStrip.OnChildItemClick(item);
            if (item == null) return;
            if (item is MetroTabItem || item.IsContainer || item.Name.StartsWith("sysgallery") || item is CheckBoxItem || !item.AutoCollapseOnClick) return;
            if (item is PopupItem && item.Expanded) return;

        }

#if TRIAL
        private bool _ShownOnce = false;
#endif
        protected override void OnVisibleChanged(EventArgs e)
        {
#if TRIAL
                if(!this.DesignMode && !_ShownOnce)
                {
				    RemindForm frm=new RemindForm();
				    frm.ShowDialog();
				    frm.Dispose();
                    _ShownOnce = true;
                }
#endif

            base.OnVisibleChanged(e);
        }

        private bool _MouseWheelTabScrollEnabled = true;
        /// <summary>
        /// Gets or sets whether mouse wheel scrolls through the Metro tabs. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether mouse wheel scrolls through the Metro tabs.")]
        public bool MouseWheelTabScrollEnabled
        {
            get { return _MouseWheelTabScrollEnabled; }
            set
            {
                _MouseWheelTabScrollEnabled = value;
            }
        }

        /// <summary>
        /// ImageList for images used on Items. Images specified here will always be used on menu-items and are by default used on all Bars.
        /// </summary>
        [Browsable(true), Category("Data"), DefaultValue(null), Description("ImageList for images used on Items. Images specified here will always be used on menu-items and are by default used on all Bars.")]
        public ImageList Images
        {
            get { return _TabStrip.Images; }
            set { _TabStrip.Images = value; }
        }

        /// <summary>
        /// ImageList for medium-sized images used on Items.
        /// </summary>
        [Browsable(true), Category("Data"), DefaultValue(null), Description("ImageList for medium-sized images used on Items.")]
        public ImageList ImagesMedium
        {
            get { return _TabStrip.ImagesMedium; }
            set { _TabStrip.ImagesMedium = value; }
        }

        /// <summary>
        /// ImageList for large-sized images used on Items.
        /// </summary>
        [Browsable(true), Category("Data"), DefaultValue(null), Description("ImageList for large-sized images used on Items.")]
        public ImageList ImagesLarge
        {
            get { return _TabStrip.ImagesLarge; }
            set { _TabStrip.ImagesLarge = value; }
        }

        protected override void OnTabStopChanged(System.EventArgs e)
        {
            base.OnTabStopChanged(e);
            _TabStrip.TabStop = this.TabStop;
        }

        /// <summary>
        /// Gets or sets a value indicating whether the user can give the focus to this control using the TAB key. Default value is false.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Behavior"), Description("Indicates whether the user can give the focus to this control using the TAB key.")]
        public new bool TabStop
        {
            get { return base.TabStop; }
            set { base.TabStop = value; }
        }
        #endregion

        #region Metro Customization

        private bool DisplayCustomizeContextMenu
        {
            get { return this.CanCustomize && (this.CaptionVisible || _UseExternalCustomization); }
        }

        /// <summary>
        /// Called when right-mouse button is pressed over MetroTabStrip
        /// </summary>
        /// <param name="metroStrip">Reference to MetroTabStrip object.</param>
        internal void OnTabStripRightClick(MetroTabStrip metroStrip, int x, int y)
        {
            if (!DisplayCustomizeContextMenu)
                return;
            BaseItem item = metroStrip.HitTest(x, y);
            if (item != null && item.CanCustomize && !item.SystemItem)
            {
                ShowCustomizeContextMenu(item, true);
            }
        }

        /// <summary>
        /// Displays popup customize context menu for given customization object.
        /// </summary>
        /// <param name="o">Object that should be customized, usually an instance of BaseItem.</param>
        /// <param name="isTabStrip">Indicates whether customize menu is displayed over metro tab strip</param>
        internal virtual void ShowCustomizeContextMenu(object o, bool isTabStrip)
        {
            if (o == null || !_UseCustomizeDialog)
                return;

            _TabStrip.ClosePopup(SYS_CUSTOMIZE_POPUP_MENU);

            ButtonItem cont = new ButtonItem(SYS_CUSTOMIZE_POPUP_MENU);
            cont.Style = eDotNetBarStyle.StyleManagerControlled;
            cont.SetOwner(_TabStrip);

            if ((CanCustomizeItem(o as BaseItem)) && !_UseExternalCustomization)
            {
                if (o is BaseItem && this.QuickToolbarItems.Contains((BaseItem)o))
                {

                    ButtonItem b = new ButtonItem(SysQatRemoveFromItemName);
                    b.Text = this.SystemText.QatRemoveItemText;
                    b.Click += new System.EventHandler(CustomizeRemoveFromQuickAccessToolbar);
                    b.Tag = o;
                    cont.SubItems.Add(b);
                }
                else
                {
                    BaseItem itemToCustomize = o as BaseItem;

                    ButtonItem b = new ButtonItem(SysQatAddToItemName);
                    b.Text = this.SystemText.QatAddItemText;
                    b.Click += new System.EventHandler(CustomizeAddToQuickAccessToolbar);
                    b.Tag = o;
                    cont.SubItems.Add(b);

                    if (itemToCustomize != null && this.QuickToolbarItems.Contains(itemToCustomize.Name))
                        b.Enabled = false;

                    if (itemToCustomize != null && BaseItem.IsOnPopup(itemToCustomize) && itemToCustomize.Parent != null)
                    {
                        Control c = itemToCustomize.ContainerControl as Control;
                        if (c != null) c.VisibleChanged += new EventHandler(CustomizePopupItemParentVisibleChange);
                    }
                }
            }

            if (_UseCustomizeDialog)
            {
                ButtonItem b = new ButtonItem(SysQatCustomizeItemName);
                b.Text = this.SystemText.QatCustomizeText;
                b.BeginGroup = true;
                b.Click += new EventHandler(CustomizeQuickAccessToolbarDialog);
                cont.SubItems.Add(b);
            }

            RibbonCustomizeEventArgs e = new RibbonCustomizeEventArgs(o, cont);
            OnBeforeCustomizeMenuPopup(e);
            if (e.Cancel)
            {
                cont.Dispose();
                return;
            }

            ((IOwnerMenuSupport)_TabStrip).RegisterPopup(cont);
            cont.Popup(Control.MousePosition);
        }

        private void CustomizePopupItemParentVisibleChange(object sender, EventArgs e)
        {
            Control c = sender as Control;
            if (c == null) return;
            c.VisibleChanged -= new EventHandler(CustomizePopupItemParentVisibleChange);
            // Close the Customize Context menu we displayed
            _TabStrip.ClosePopup(SYS_CUSTOMIZE_POPUP_MENU);
        }

        private ArrayList GetQatItems(bool includeCustomizeItem)
        {
            ArrayList list = new ArrayList();
            foreach (BaseItem item in this.QuickToolbarItems)
            {
                if (IsSystemItem(item) || item is ButtonItem && ((ButtonItem)item).HotTrackingStyle == eHotTrackingStyle.Image && this.QuickToolbarItems.IndexOf(item) == 0)
                {
                    if (includeCustomizeItem && item is CustomizeItem)
                        list.Add(item);
                    continue;
                }
                list.Add(item);
            }

            return list;
        }

        internal Ribbon.QatToolbar QatToolbar
        {
            get { return m_QatToolbar; }
        }

        private void CustomizeQuickAccessToolbarDialog(object sender, EventArgs e)
        {
            ShowQatCustomizeDialog();
        }

        private bool CanCustomizeItem(BaseItem item)
        {
            if (item == null)
                return false;

            if (!item.CanCustomize || item.SystemItem)
                return false;

            if (item is ItemContainer) // && ((ItemContainer)item).SystemContainer)
                return false;

            if (item is ButtonItem && this.QuickToolbarItems.IndexOf(item) == 0)
                return false;

            return true;
        }

        private string GetQATRibbonBarName(RibbonBar bar)
        {
            return "qt_" + bar.Name;
        }

        private void CustomizeRemoveFromQuickAccessToolbar(object sender, System.EventArgs e)
        {
            ButtonItem b = sender as ButtonItem;
            if (b.Tag is BaseItem)
            {
                BaseItem item = b.Tag as BaseItem;
                RemoveItemFromQuickAccessToolbar(item);
            }

            b.Tag = null;
        }

        /// <summary>
        /// Removes an item from the Quick Access Toolbar.
        /// </summary>
        /// <param name="item">Reference to the item that is already part of Quick Access Toolbar.</param>
        public void RemoveItemFromQuickAccessToolbar(BaseItem item)
        {
            RibbonCustomizeEventArgs rc = new RibbonCustomizeEventArgs(item, null);
            OnBeforeRemoveItemFromQuickAccessToolbar(rc);

            if (!rc.Cancel)
            {
                this.QuickToolbarItems.Remove(item);
                //item.Parent.SubItems.Remove(item);
                this.RecalcLayout();
                m_QatLayoutChanged = true;
            }
        }

        private void CustomizeAddToQuickAccessToolbar(object sender, System.EventArgs e)
        {
            ButtonItem b = sender as ButtonItem;

            AddItemToQuickAccessToolbar(b.Tag);

            b.Tag = null;
        }

        /// <summary>
        /// Adds an instance of base type BaseItem to the Quick Access Toolbar. Note that this method creates
        /// new instance of the item or an representation of the item being added and adds that to the Quick Access Toolbar.
        /// </summary>
        /// <param name="originalItem">Reference to the item to add, must be an BaseItem type.</param>
        public void AddItemToQuickAccessToolbar(object originalItem)
        {
            BaseItem copy = GetQatItemCopy(originalItem);
            if (copy != null)
            {
                RibbonCustomizeEventArgs re = new RibbonCustomizeEventArgs(copy, null);
                OnBeforeAddItemToQuickAccessToolbar(re);
                if (!re.Cancel)
                {
                    this.QuickToolbarItems.Add(copy);
                    this.RecalcLayout();
                    m_QatLayoutChanged = true;
                }
            }
        }

        private BaseItem GetQatItemCopy(object o)
        {
            BaseItem copy = null;
            if (o is ButtonItem)
            {
                ButtonItem button = ((ButtonItem)o).Copy() as ButtonItem;
                button.KeyTips = "";
                button.ImageFixedSize = Size.Empty;
                button.FixedSize = Size.Empty;
                button.ButtonStyle = eButtonStyle.Default;
                button.ImagePosition = eImagePosition.Left;
                button.BeginGroup = false;
                button.UseSmallImage = true;
                button.ImagePaddingHorizontal = 8;
                button.ImagePaddingVertical = 6;

                if (button.TextMarkupBody != null && button.TextMarkupBody.HasExpandElement)
                    button.Text = TextMarkup.MarkupParser.RemoveExpand(button.Text);

                if (button.Image == null && button.ImageIndex == -1 && button.Icon == null)
                    button.Image = BarFunctions.LoadBitmap("SystemImages.GreenLight.png");
                else
                    button.ImageFixedSize = new Size(16, 16);
                copy = button;
            }
            else if (o is BaseItem)
            {
                copy = ((BaseItem)o).Copy();
                copy.KeyTips = "";
            }
            //else if (o is RibbonBar)
            //{
            //    RibbonBar bar = o as RibbonBar;
            //    ButtonItem item = new ButtonItem(GetQATRibbonBarName(bar));
            //    item.Image = bar.GetOverflowButtonImage();
            //    item.ImageFixedSize = new Size(16, 16);
            //    item.AutoExpandOnClick = true;
            //    item.Tooltip = bar.Text;
            //    item.Text = bar.Text;
            //    RibbonBar qatBar = null;
            //    bool recalcLayout = false;
            //    if (bar.IsItemsAutoSizeActive)
            //    {
            //        bar.RestoreAutoSizedItems();
            //        qatBar = bar.CreateOverflowRibbon(true);
            //        recalcLayout = true;
            //    }
            //    else
            //        qatBar = bar.CreateOverflowRibbon(true);
            //    qatBar.IsOnQat = true;
            //    qatBar.QatButtonParent = item;
            //    foreach (BaseItem child in bar.Items)
            //    {
            //        BaseItem childCopy = child.Copy();
            //        qatBar.Items.Add(childCopy);
            //    }
            //    if (recalcLayout) bar.RecalcLayout();

            //    ItemContainer ic = new ItemContainer();
            //    ic.Stretch = true;
            //    ControlContainerItem cont = new ControlContainerItem();
            //    cont.AllowItemResize = false;
            //    ic.SubItems.Add(cont);
            //    cont.Control = qatBar;
            //    item.SubItems.Add(ic);

            //    copy = item;
            //}

            return copy;
        }

        /// <summary>
        /// Raises the BeforeCustomizeMenuPopup event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnBeforeCustomizeMenuPopup(RibbonCustomizeEventArgs e)
        {
            if (BeforeCustomizeMenuPopup != null)
                BeforeCustomizeMenuPopup(this, e);
        }

        /// <summary>
        /// Raises the BeforeAddItemToQuickAccessToolbar event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnBeforeAddItemToQuickAccessToolbar(RibbonCustomizeEventArgs e)
        {
            if (BeforeAddItemToQuickAccessToolbar != null)
                BeforeAddItemToQuickAccessToolbar(this, e);
        }

        protected virtual void OnBeforeRemoveItemFromQuickAccessToolbar(RibbonCustomizeEventArgs e)
        {
            if (BeforeRemoveItemFromQuickAccessToolbar != null)
                BeforeRemoveItemFromQuickAccessToolbar(this, e);
        }

        /// <summary>
        /// Gets or sets whether control can be customized and items added by end-user using context menu to the quick access toolbar.
        /// Caption of the control must be visible for customization to be enabled. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Quick Access Toolbar"), Description("Indicates whether control can be customized. Caption must be visible for customization to be fully enabled.")]
        public bool CanCustomize
        {
            get { return _TabStrip.CanCustomize; }
            set { _TabStrip.CanCustomize = value; }
        }

        /// <summary>
        /// Gets or sets whether external implementation for metro toolbar and menu item customization will be used for customizing the control. When set to true
        /// it enables the displaying of MetroToolbar and menu item context menus which allow customization. You are responsible for
        /// adding the menu items to context menu to handle all aspects of item customization. See "MetroShell Control Quick Access Toolbar Customization" topic in help file under How To.
        /// Default value is false.
        /// </summary>
        [DefaultValue(false), Browsable(true), Category("Quick Access Toolbar"), Description("Indicates whether external implementation for MetroToolbar and menu item customization will be used for customizing the MetroShell control.")]
        public bool UseExternalCustomization
        {
            get { return _UseExternalCustomization; }
            set { _UseExternalCustomization = value; }
        }

        /// <summary>
        /// Gets or sets whether end-user customization of the placement of the Quick Access Toolbar is enabled. User
        /// can change the position of the Quick Access Toolbar using the customize menu. Default value is true.
        /// </summary>
        //[Browsable(true), DefaultValue(true), Category("Quick Access Toolbar"), Description("Indicates whether end-user customization of the placement of the Quick Access Toolbar is enabled.")]
        //public bool EnableQatPlacement
        //{
        //    get { return m_EnableQatPlacement; }
        //    set { m_EnableQatPlacement = value; }
        //}

        /// <summary>
        /// Gets or sets whether customize dialog is used to customize the quick access toolbar. You can handle the EnterCustomize event 
        /// to display your custom dialog instead of built-in dialog for customization. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Quick Access Toolbar"), Description("Indicates whether customize dialog is used to customize the quick access toolbar.")]
        public bool UseCustomizeDialog
        {
            get { return _UseCustomizeDialog; }
            set { _UseCustomizeDialog = value; }
        }

        /// <summary>
        /// Gets or sets the categorization mode for the items on Quick Access Toolbar customize dialog box. Default value categorizes
        /// items by the toolbar they appear on.
        /// </summary>
        [DefaultValue(eMetroCategorizeMode.Toolbar), Browsable(true), Category("Quick Access Toolbar"), Description("Indicates categorization mode for the items on Quick Access Toolbar customize dialog box.")]
        public eMetroCategorizeMode CategorizeMode
        {
            get { return m_CategorizeMode; }
            set { m_CategorizeMode = value; }
        }

        /// <summary>
        /// Shows the quick access toolbar customize dialog.
        /// </summary>
        public virtual void ShowQatCustomizeDialog()
        {
            Form qatDialog = CreateQatCustomizeDialog();

            if (BeforeQatCustomizeDialog != null)
            {
                QatCustomizeDialogEventArgs ce = new QatCustomizeDialogEventArgs(qatDialog);
                BeforeQatCustomizeDialog(this, ce);
                if (ce.Cancel || ce.Dialog == null)
                {
                    qatDialog.Dispose();
                    return;
                }

                qatDialog = ce.Dialog;
            }

            if (qatDialog is MetroQatCustomizeDialog)
                ((MetroQatCustomizeDialog)qatDialog).LoadItems(this);

            Form form = this.FindForm();
            qatDialog.StartPosition = FormStartPosition.CenterParent;
            DialogResult result = qatDialog.ShowDialog(form);

            if (AfterQatCustomizeDialog != null)
            {
                QatCustomizeDialogEventArgs ce = new QatCustomizeDialogEventArgs(qatDialog);
                AfterQatCustomizeDialog(this, ce);
                if (ce.Cancel)
                {
                    qatDialog.Dispose();
                    return;
                }
            }

            if (result == DialogResult.Cancel)
            {
                qatDialog.Dispose();
                return;
            }

            // Apply changes to the Quick Access Toolbar
            MetroQatCustomizeDialog qd = qatDialog as MetroQatCustomizeDialog;
            if (qd == null || !qd.QatCustomizePanel.DataChanged) return;
            ApplyQatCustomizePanelChanges(qd.QatCustomizePanel);
            qatDialog.Dispose();
        }

        /// <summary>
        /// Applies the Quick Access Toolbar customization changes made on QatCustomizePanel to the MetroShell Control Quick Access Toolbar. Note that QatCustomizePanel.DataChanged property indicates whether user made any changes to the data on the panel.
        /// </summary>
        /// <param name="customizePanel">Reference to the QatCustomizePanel</param>
        public void ApplyQatCustomizePanelChanges(Ribbon.QatCustomizePanel customizePanel)
        {
            if (customizePanel == null || !customizePanel.DataChanged) return;
            _TabStrip.BeginUpdate();
            try
            {
                ItemPanel itemPanelQat = customizePanel.itemPanelQat;

                int start = 0;
                BaseItem startButton = GetApplicationButton();
                if (startButton != null)
                    start = this.QuickToolbarItems.IndexOf(startButton) + 1;

                ArrayList removeList = new ArrayList();
                SubItemsCollection qatList = new SubItemsCollection(null);

                for (int i = start; i < this.QuickToolbarItems.Count; i++)
                {
                    BaseItem item = this.QuickToolbarItems[i];
                    if (IsSystemItem(item))
                        continue;

                    if (!itemPanelQat.Items.Contains(item.Name))
                        removeList.Add(item);
                    else
                        qatList._Add(item);
                }

                foreach (BaseItem item in removeList)
                    this.QuickToolbarItems.Remove(item);
                foreach (BaseItem item in qatList)
                    this.QuickToolbarItems.Remove(item);

                foreach (BaseItem item in itemPanelQat.Items)
                {
                    // Already exists on Quick Access Toolbar
                    if (item.Tag != null)
                    {
                        BaseItem copy = GetQatItemCopy(item.Tag as BaseItem);
                        this.QuickToolbarItems.Add(copy);
                    }
                    else
                    {
                        BaseItem qatItem = qatList[item.Name];
                        if (qatItem != null)
                            this.QuickToolbarItems.Add(qatItem);
                    }
                }

                m_QatLayoutChanged = true;
            }
            finally
            {
                _TabStrip.EndUpdate();
            }

            OnAfterQatDialogChangesApplied();
        }

        /// <summary>
        /// Raises the AfterQatDialogChangesApplied event.
        /// </summary>
        protected virtual void OnAfterQatDialogChangesApplied()
        {
            if (AfterQatDialogChangesApplied != null)
                AfterQatDialogChangesApplied(this, new EventArgs());
        }

        private MetroQatCustomizeDialog CreateQatCustomizeDialog()
        {
            MetroQatCustomizeDialog qat = new MetroQatCustomizeDialog();
            qat.Text = SystemText.QatDialogCaption;
            qat.buttonCancel.Text = SystemText.QatDialogCancelButton;
            qat.buttonOK.Text = SystemText.QatDialogOkButton;
            qat.QatCustomizePanel.buttonAddToQat.Text = SystemText.QatDialogAddButton;
            qat.QatCustomizePanel.buttonRemoveFromQat.Text = SystemText.QatDialogRemoveButton;
            qat.QatCustomizePanel.labelCategories.Text = SystemText.QatDialogCategoriesLabel;
            qat.QatCustomizePanel.checkQatBelowRibbon.Text = SystemText.QatDialogPlacementCheckbox;

            return qat;
        }

        private bool IsSystemItem(BaseItem item)
        {
            if (item.SystemItem || item is ItemContainer || item is CustomizeItem || item is SystemCaptionItem)
                return true;
            return false;
        }
        /// <summary>
        /// Returns the Metro Application Button.
        /// </summary>
        /// <returns>reference to Application Button or null if button is not found.</returns>
        public BaseItem GetApplicationButton()
        {
            return _TabStrip.GetApplicationButton();
        }

        /// <summary>
        /// Gets or sets the Quick Access Toolbar layout description. You can use the value obtained from this property to save
        /// the customized Quick Access Toolbar into registry or into any other storage object. You can also set the saved layout description back
        /// to restore user customize layout.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string QatLayout
        {
            get
            {
                return GetQatLayoutDescription();
            }
            set
            {
                SetQatLayoutDescription(value);
            }
        }

        private string GetQatLayoutDescription()
        {
            ArrayList qatList = GetQatItems(false);
            StringBuilder layoutDesc = new StringBuilder();
            foreach (BaseItem item in qatList)
            {
                if (item.Name != "")
                {
                    layoutDesc.Append("," + item.Name);
                }
            }

            return layoutDesc.ToString();
        }

        private void SetQatLayoutDescription(string layoutDesc)
        {
            if (layoutDesc == "" || layoutDesc == null)
                return;

            string[] values = layoutDesc.Split(',');
            if (values.Length == 0)
                return;
            ArrayList qatItems = new ArrayList();
            for (int i = 0; i < values.Length; i++)
            {
                if (this.QuickToolbarItems.Contains(values[i]))
                    qatItems.Add(this.QuickToolbarItems[values[i]]);
                else
                {
                    BaseItem item = FindItem(values[i]);
                    if (item != null && CanCustomizeItem(item))
                        qatItems.Add(GetQatItemCopy(item));
                }
            }

            ArrayList removeItems = GetQatItems(false);
            foreach (BaseItem item in removeItems)
                this.QuickToolbarItems.Remove(item);

            foreach (BaseItem item in qatItems)
                this.QuickToolbarItems.Add(item);

            m_QatLayoutChanged = false;
        }

        private BaseItem FindItem(string name)
        {
            foreach (WeakReference toolbarRef in _Toolbars)
            {
                if (!toolbarRef.IsAlive)
                    continue;
                IOwner toolbar = (IOwner)toolbarRef.Target;
                if (toolbar != null)
                {
                    BaseItem item = toolbar.GetItem(name);
                    if(item!=null)
                        return item;
                }
            }
            return null;
        }

        internal List<IOwner> RegisteredToolbars
        {
            get
            {
                List<IOwner> list = new List<IOwner>();
                foreach (WeakReference toolbarRef in _Toolbars)
                {
                    if (toolbarRef.IsAlive)
                    {
                        list.Add((IOwner)toolbarRef.Target);
                    }
                }
                return list;
            }
        }

        private List<WeakReference> _Toolbars = new List<WeakReference>();
        /// <summary>
        /// Registers the MetroToolbar or any other DotNetBar container that implements IOwner interface so it can participate in Quick Access Toolbar serialization and customization.
        /// </summary>
        /// <param name="toolbar"></param>
        public void RegisterToolbar(IOwner toolbar)
        {
            if (toolbar == null)
                throw new ArgumentNullException("toolbar parameter cannot be null");
            if (IsToolbarRegistered(toolbar))
                throw new ArgumentException("toolbar is already registered");
            WeakReference toolbarRef = new WeakReference(toolbar);
            _Toolbars.Add(toolbarRef);
        }
        /// <summary>
        /// Registers the MetroToolbar or any other DotNetBar container that implements IOwner interface so it can participate in Quick Access Toolbar serialization and customization.
        /// </summary>
        /// <param name="toolbar"></param>
        public void UnregisterToolbar(IOwner toolbar)
        {
            if (toolbar == null)
                throw new ArgumentNullException("toolbar parameter cannot be null");

            foreach (WeakReference toolbarRef in _Toolbars)
            {
                if (toolbarRef.IsAlive && toolbarRef.Target == toolbar)
                {
                    _Toolbars.Remove(toolbarRef);
                    return;
                }
            }
        }
        private bool IsToolbarRegistered(IOwner toolbar)
        {
            foreach (WeakReference toolbarRef in _Toolbars)
            {
                if(toolbarRef.IsAlive && toolbarRef.Target == toolbar)
                    return true;
            }
            return false;
        }

        /// <summary>
        /// Gets or sets whether Quick Access Toolbar has been customized by end-user. You can use value of this property to determine
        /// whether Quick Access Toolbar layout that can be accessed using QatLayout property should be saved.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool QatLayoutChanged
        {
            get { return m_QatLayoutChanged; }
            set { m_QatLayoutChanged = value; }
        }

        /// <summary>
        /// Gets the reference to the Metro localization object which holds all system text used by the component.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), NotifyParentPropertyAttribute(true), Category("Localization"), Description("Gets system text used by the component.."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RibbonLocalization SystemText
        {
            get { return m_SystemText; }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 100);
            }
        }

        private eMetroTabStripDock _TabStripDock = eMetroTabStripDock.Top;
        /// <summary>
        /// Gets or sets the side tab-strip is docked to.
        /// </summary>
        [DefaultValue(eMetroTabStripDock.Top), Category("Appearance"), Description("Indicates side tab-strip is docked to.")]
        public eMetroTabStripDock TabStripDock
        {
            get { return _TabStripDock; }
            set
            {
                _TabStripDock = value;
                if (_TabStripDock == eMetroTabStripDock.Top)
                    _TabStrip.Dock = DockStyle.Top;
                else if (_TabStripDock == eMetroTabStripDock.Bottom)
                    _TabStrip.Dock = DockStyle.Bottom;
            }
        }
        /// <summary>
        /// Gets or sets whether SETTINGS button is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether SETTINGS button is visible.")]
        public bool SettingsButtonVisible
        {
            get { return _TabStrip.MetroStripContainer.SettingsButtonVisible; }
            set
            {
                if (value != _TabStrip.MetroStripContainer.SettingsButtonVisible)
                {
                    _TabStrip.MetroStripContainer.SettingsButtonVisible = value;
                }
            }
        }
        /// Gets or sets whether HELP button is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether HELP button is visible.")]
        public bool HelpButtonVisible
        {
            get { return _TabStrip.MetroStripContainer.HelpButtonVisible; }
            set
            {
                if (value != _TabStrip.MetroStripContainer.HelpButtonVisible)
                {
                    _TabStrip.MetroStripContainer.HelpButtonVisible = value;
                }
            }
        }
        /// <summary>
        /// Gets or sets the SETTINGS button text.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates SETTINGS button text")]
        public string SettingsButtonText
        {
            get { return _TabStrip.MetroStripContainer.SettingsButtonText; }
            set { _TabStrip.MetroStripContainer.SettingsButtonText = value; }
        }
        /// <summary>
        /// Gets or sets the HELP button text.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates HELP button text")]
        public string HelpButtonText
        {
            get { return _TabStrip.MetroStripContainer.HelpButtonText; }
            set { _TabStrip.MetroStripContainer.HelpButtonText = value; }
        }

        /// <summary>
        /// Gets or sets the font tab items are displayed with.
        /// </summary>
        [AmbientValue((string) null), Localizable(true) , Category("Appearance"), Description("Indicates the font tab items are displayed with.")]
        public Font TabStripFont
        {
            get { return _TabStrip.Font; }
            set
            {
                _TabStrip.Font = value;
            }
        }
        #endregion
    }

    public enum eMetroTabStripDock
    {
        Top,
        Bottom
    }

    /// <summary>
    /// Describes the categorization mode used to categorize items on the Customize Metro dialog.
    /// </summary>
    public enum eMetroCategorizeMode
    {
        /// <summary>
        /// Items are automatically categorized by the toolbar they appear on.
        /// </summary>
        Toolbar,
        /// <summary>
        /// Items are categorized by the Category property on each item. Category property should be set on each item.
        /// </summary>
        Categories
    }
}
