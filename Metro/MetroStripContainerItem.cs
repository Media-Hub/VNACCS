using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
    /// Defines the internal container item for the ribbon strip control.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false), System.ComponentModel.DesignTimeVisible(false)]
    internal class MetroStripContainerItem : ImageItem, IDesignTimeProvider
    {
        #region Private Variables
        private const string DefaultSettingsButtonText = "<font size=\"7\">SETTINGS</font>";
        private const string DefaultHelpButtonText = "<font size=\"7\">HELP</font>";

        private MetroTabItemContainer _ItemContainer = null;
        private CaptionItemContainer _CaptionContainer = null;
        private SystemCaptionItem _SystemCaptionItem = null;
        private MetroTabStrip _TabStrip = null;
        private SystemCaptionItem _WindowIcon = null;
        private Separator _IconSeparator = null;
        private ButtonItem _Settings = null;
        private ButtonItem _Help = null;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates new instance of the class and initializes it with the parent RibbonStrip control.
        /// </summary>
        /// <param name="parent">Reference to parent RibbonStrip control</param>
        public MetroStripContainerItem(MetroTabStrip parent)
        {
            _TabStrip = parent;

            // We contain other controls
            m_IsContainer = true;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;

            _ItemContainer = new MetroTabItemContainer();
            _ItemContainer.ContainerControl = parent;
            _ItemContainer.GlobalItem = false;
            _ItemContainer.WrapItems = false;
            _ItemContainer.EventHeight = false;
            _ItemContainer.UseMoreItemsButton = false;
            _ItemContainer.Stretch = true;
            _ItemContainer.Displayed = true;
            _ItemContainer.SystemContainer = true;
            _ItemContainer.PaddingTop = 0;
            _ItemContainer.PaddingBottom = 0;
            _ItemContainer.PaddingLeft = 0;
            _ItemContainer.ItemSpacing = 1;

            _CaptionContainer = new CaptionItemContainer();
            _CaptionContainer.ContainerControl = parent;
            _CaptionContainer.GlobalItem = false;
            _CaptionContainer.WrapItems = false;
            _CaptionContainer.EventHeight = false;
            _CaptionContainer.EqualButtonSize = false;
            _CaptionContainer.ToolbarItemsAlign = eContainerVerticalAlignment.Top;
            _CaptionContainer.UseMoreItemsButton = false;
            _CaptionContainer.Stretch = true;
            _CaptionContainer.Displayed = true;
            _CaptionContainer.SystemContainer = true;
            _CaptionContainer.PaddingBottom = 0;
            _CaptionContainer.PaddingTop = 0;
            _CaptionContainer.PaddingLeft = 6;
            _CaptionContainer.ItemSpacing = 1;
            _CaptionContainer.TrackSubItemsImageSize = false;
            _CaptionContainer.ItemAdded += new EventHandler(this.CaptionContainerNewItemAdded);
            this.SubItems.Add(_CaptionContainer);
            this.SubItems.Add(_ItemContainer);

            _Settings = new ButtonItem("sysSettingsButton");
            _Settings.Text=DefaultSettingsButtonText;
            //_Settings.HotTrackingStyle = eHotTrackingStyle.None;
            _Settings.ItemAlignment = eItemAlignment.Far;
            _Settings.Click += new EventHandler(SettingsButtonClick);
            _Settings.SetSystemItem(true);
            _Settings.CanCustomize = false;
            _CaptionContainer.SubItems.Add(_Settings);

            _Help = new ButtonItem("sysHelpButton");
            _Help.Text = DefaultHelpButtonText;
            _Help.SetSystemItem(true);
            _Help.CanCustomize = false;
            //_Help.HotTrackingStyle = eHotTrackingStyle.None;
            _Help.ItemAlignment = eItemAlignment.Far;
            _Help.Click += new EventHandler(HelpButtonClick);
            _CaptionContainer.SubItems.Add(_Help);

            SystemCaptionItem sc = new SystemCaptionItem();
            sc.RestoreEnabled = false;
            sc.IsSystemIcon = false;
            sc.ItemAlignment = eItemAlignment.Far;
            _CaptionContainer.SubItems.Add(sc);
            _SystemCaptionItem = sc;
        }
        #endregion

        #region Internal Implementation
        private void CaptionContainerNewItemAdded(object sender, EventArgs e)
        {
            if (sender is BaseItem)
            {
                BaseItem item = sender as BaseItem;
                if (!(item is SystemCaptionItem))
                {
                    if (_CaptionContainer.SubItems.Contains(_Settings))
                    {
                        _CaptionContainer.SubItems._Remove(_Settings);
                        _CaptionContainer.SubItems._Add(_Settings);
                    }
                    if (_CaptionContainer.SubItems.Contains(_Help))
                    {
                        _CaptionContainer.SubItems._Remove(_Help);
                        _CaptionContainer.SubItems._Add(_Help);
                    }
                    if (_CaptionContainer.SubItems.Contains(_SystemCaptionItem))
                    {
                        _CaptionContainer.SubItems._Remove(_SystemCaptionItem);
                        _CaptionContainer.SubItems._Add(_SystemCaptionItem);
                    }
                }
            }
        }

        private void SettingsButtonClick(object sender, EventArgs e)
        {
            MetroTabStrip mts = this.ContainerControl as MetroTabStrip;
            if (mts == null) return;

            MetroShell tab = mts.Parent as MetroShell;
            if (tab == null) return;

            tab.InvokeSettingsButtonClick(e);
        }
        private void HelpButtonClick(object sender, EventArgs e)
        {
            MetroTabStrip mts = this.ContainerControl as MetroTabStrip;
            if (mts == null) return;

            MetroShell tab = mts.Parent as MetroShell;
            if (tab == null) return;

            tab.InvokeHelpButtonClick(e);
        }

        internal void ReleaseSystemFocus()
        {
            _ItemContainer.ReleaseSystemFocus();
            if (_TabStrip.CaptionVisible)
                _CaptionContainer.ReleaseSystemFocus();
        }

        public override void ContainerLostFocus(bool appLostFocus)
        {
            base.ContainerLostFocus(appLostFocus);
            _ItemContainer.ContainerLostFocus(appLostFocus);
            if (_TabStrip.CaptionVisible)
                _CaptionContainer.ContainerLostFocus(appLostFocus);
        }

        internal void SetSystemFocus()
        {
            if (_TabStrip.CaptionVisible && _ItemContainer.ExpandedItem()==null)
                _CaptionContainer.SetSystemFocus();
            else
                _ItemContainer.SetSystemFocus();
        }

        /// <summary>
		/// Paints this base container
		/// </summary>
        public override void Paint(ItemPaintArgs pa)
        {
            if (this.SuspendLayout)
                return;

            _ItemContainer.Paint(pa);
            if (_TabStrip.CaptionVisible)
                _CaptionContainer.Paint(pa);
        }

        public override void RecalcSize()
        {
            if (this.SuspendLayout)
                return;
            _ItemContainer.Bounds = GetItemContainerBounds();
            _ItemContainer.RecalcSize();
            if (_ItemContainer.HeightInternal < 0) _ItemContainer.HeightInternal = 0;
            bool isMaximized = false;
            if (_TabStrip.CaptionVisible)
            {
                _CaptionContainer.Bounds = GetCaptionContainerBounds();
                _CaptionContainer.RecalcSize();
                Size frameBorderSize = SystemInformation.FrameBorderSize;
                Control container = this.ContainerControl as Control;
                if (container != null)
                {
                    MetroAppForm appForm = container.FindForm() as MetroAppForm;
                    if (appForm != null)
                    {
                        NonClientInfo nci = appForm.GetNonClientInfo();
                        frameBorderSize.Width = nci.LeftBorder;
                        frameBorderSize.Height = nci.BottomBorder;
                    }
                }
                if (_TabStrip.CaptionHeight == 0 && _SystemCaptionItem.TopInternal < (frameBorderSize.Height - 1))
                {
                    Control c = this.ContainerControl as Control;
                    Form form = null;
                    if (c != null) form = c.FindForm();
                    if (form != null && form.WindowState == FormWindowState.Maximized)
                        isMaximized = true;

                    if (isMaximized)
                    {
                        _SystemCaptionItem.TopInternal = 1;
                        if (_WindowIcon != null) _WindowIcon.TopInternal = 1;
                    }
                    else
                    {
                        _SystemCaptionItem.TopInternal = frameBorderSize.Height - 4;
                        if (_WindowIcon != null) _WindowIcon.TopInternal = frameBorderSize.Height - 5;
                    }
                }
                
                // Adjust the Y position of the items inside of the caption container since they are top aligned and
                // quick access toolbar items should be aligned with the bottom of the system caption item.
                if (System.Environment.OSVersion.Version.Major >= 6)
                {
                    int topOffset = 3;
                    if (isMaximized)
                        topOffset += 1;
                    int maxBottom = 0;
                    foreach (BaseItem item in _CaptionContainer.SubItems)
                    {
                        if (!(item is ApplicationButton || item is DevComponents.DotNetBar.Metro.MetroAppButton) && item != _SystemCaptionItem && item != _IconSeparator)
                            item.TopInternal += topOffset;
                        else if (item == _IconSeparator)
                            item.TopInternal += (isMaximized ? 2 : 1);
                        maxBottom = Math.Max(item.Bounds.Bottom, maxBottom);
                    }
                    if (_CaptionContainer.MoreItems != null)
                        _CaptionContainer.MoreItems.TopInternal += topOffset;
                    if (maxBottom > _CaptionContainer.HeightInternal) _CaptionContainer.SetDisplayRectangle(new Rectangle(_CaptionContainer.Bounds.X, _CaptionContainer.Bounds.Y, _CaptionContainer.Bounds.Width, maxBottom));
                }
                else
                {
                    int maxBottom = 0;
                    foreach (BaseItem item in _CaptionContainer.SubItems)
                    {
                        if (item.HeightInternal < _SystemCaptionItem.HeightInternal && (item != _IconSeparator && item!= _WindowIcon))
                        {
                            //item.TopInternal += (m_SystemCaptionItem.HeightInternal - item.HeightInternal);
                            item.TopInternal = (_SystemCaptionItem.Bounds.Bottom - (item.HeightInternal + ((item is LabelItem) ? 2 : 0)));
                            maxBottom = Math.Max(item.Bounds.Bottom, maxBottom);
                        }
                    }
                    if (_CaptionContainer.MoreItems != null)
                        _CaptionContainer.MoreItems.TopInternal += (_SystemCaptionItem.HeightInternal - _CaptionContainer.MoreItems.HeightInternal);
                    if (maxBottom > _CaptionContainer.HeightInternal) _CaptionContainer.SetDisplayRectangle(new Rectangle(_CaptionContainer.Bounds.X, _CaptionContainer.Bounds.Y, _CaptionContainer.Bounds.Width, maxBottom));
                }

                if (_ItemContainer.HeightInternal == 0)
                    this.HeightInternal = _CaptionContainer.HeightInternal;
                else
                    this.HeightInternal = _ItemContainer.Bounds.Bottom;// -m_CaptionContainer.Bounds.Top;
            }
            else
            {
                int h = _ItemContainer.HeightInternal;
                this.HeightInternal = h;
            }

            base.RecalcSize();
        }

        private Rectangle GetItemContainerBounds()
        {
            return _TabStrip.GetItemContainerBounds();
        }

        private Rectangle GetCaptionContainerBounds()
        {
            return _TabStrip.GetCaptionContainerBounds();
        }

        /// <summary>
        /// Gets reference to internal ribbon strip container that contains tabs and/or other items.
        /// </summary>
        public GenericItemContainer RibbonStripContainer
        {
            get { return _ItemContainer; }
        }

        /// <summary>
        /// Gets reference to internal caption container item that contains the quick toolbar, start button and system caption item.
        /// </summary>
        public GenericItemContainer CaptionContainer
        {
            get { return _CaptionContainer; }
        }

        public SystemCaptionItem SystemCaptionItem
        {
            get { return _SystemCaptionItem; }
        }

        /// <summary>
        /// Returns copy of GenericItemContainer item
        /// </summary>
        public override BaseItem Copy()
        {
            MetroStripContainerItem objCopy = new MetroStripContainerItem(_TabStrip);
            this.CopyToItem(objCopy);

            return objCopy;
        }
        protected override void CopyToItem(BaseItem copy)
        {
            MetroStripContainerItem objCopy = copy as MetroStripContainerItem;
            base.CopyToItem(objCopy);
        }


        public override void InternalClick(MouseButtons mb, Point mpos)
        {
            _ItemContainer.InternalClick(mb, mpos);
            if (_TabStrip.CaptionVisible) _CaptionContainer.InternalClick(mb, mpos);
        }

        public override void InternalDoubleClick(MouseButtons mb, Point mpos)
        {
            _ItemContainer.InternalDoubleClick(mb, mpos);
            if (_TabStrip.CaptionVisible) _CaptionContainer.InternalDoubleClick(mb, mpos);
        }

        public override void InternalMouseDown(MouseEventArgs objArg)
        {
            _ItemContainer.InternalMouseDown(objArg);
            if (_TabStrip.CaptionVisible)
            {
                if(this.DesignMode && _CaptionContainer.ItemAtLocation(objArg.X, objArg.Y)!=null || !this.DesignMode)
                    _CaptionContainer.InternalMouseDown(objArg);
            }
        }

        public override void InternalMouseHover()
        {
            _ItemContainer.InternalMouseHover();
            if (_TabStrip.CaptionVisible) _CaptionContainer.InternalMouseHover();
        }

        public override void InternalMouseLeave()
        {
            _ItemContainer.InternalMouseLeave();
            if (_TabStrip.CaptionVisible) _CaptionContainer.InternalMouseLeave();
        }

        public override void InternalMouseMove(MouseEventArgs objArg)
        {
            _ItemContainer.InternalMouseMove(objArg);
            if (_TabStrip.CaptionVisible) _CaptionContainer.InternalMouseMove(objArg);
        }

        public override void InternalMouseUp(MouseEventArgs objArg)
        {
            _ItemContainer.InternalMouseUp(objArg);
            if (_TabStrip.CaptionVisible) _CaptionContainer.InternalMouseUp(objArg);
        }

        public override void InternalKeyDown(KeyEventArgs objArg)
        {
            BaseItem expanded = this.ExpandedItem();
            if (expanded == null)
                expanded = _CaptionContainer.ExpandedItem();
            if (expanded == null)
                expanded = _ItemContainer.ExpandedItem();

            if (expanded == null || !_TabStrip.CaptionVisible)
            {
                _ItemContainer.InternalKeyDown(objArg);
                if (!objArg.Handled && _TabStrip.CaptionVisible)
                    _CaptionContainer.InternalKeyDown(objArg);
            }
            else
            {
                if (expanded.Parent == _ItemContainer)
                {
                    _ItemContainer.InternalKeyDown(objArg);
                }
                else
                {
                    _CaptionContainer.InternalKeyDown(objArg);
                }
            }
        }

        /// <summary>
        /// Return Sub Item at specified location
        /// </summary>
        public override BaseItem ItemAtLocation(int x, int y)
        {
            if (_ItemContainer.DisplayRectangle.Contains(x, y))
                return _ItemContainer.ItemAtLocation(x, y);

            if (_CaptionContainer.DisplayRectangle.Contains(x, y))
                return _CaptionContainer.ItemAtLocation(x, y);

            return null;
        }

        protected override void OnStyleChanged()
        {
            eDotNetBarStyle effectiveStyle = this.EffectiveStyle;
            if (effectiveStyle == eDotNetBarStyle.Office2010)
            {
                if (_WindowIcon == null)
                {
                    _IconSeparator = new Separator("sys_caption_separator");
                    _IconSeparator.SetSystemItem(true);
                    _IconSeparator.DesignTimeVisible = false;
                    _IconSeparator.CanCustomize = false;
                    _CaptionContainer.SubItems._Add(_IconSeparator, 0);
                    _WindowIcon = new SystemCaptionItem();
                    _WindowIcon.Name = "sys_caption_icon";
                    _WindowIcon.Enabled = false;
                    _WindowIcon.Style = this.Style;
                    _WindowIcon.IsSystemIcon = true;
                    _WindowIcon.DesignTimeVisible = false;
                    _WindowIcon.CanCustomize = false;
                    _WindowIcon.QueryIconOnPaint = true;
                    _WindowIcon.MouseDown += WindowIconMouseDown;
                    _WindowIcon.DoubleClick += WindowIconDoubleClick;
                    _CaptionContainer.SubItems._Add(_WindowIcon, 0);
                }
            }
            else if (effectiveStyle == eDotNetBarStyle.Windows7)
            {
                if (_WindowIcon == null)
                {
                    _IconSeparator = new Separator("sys_caption_separator");
                    _IconSeparator.FixedSize = new Size(3, 12);
                    _IconSeparator.SetSystemItem(true);
                    _IconSeparator.DesignTimeVisible = false;
                    _IconSeparator.CanCustomize = false;
                    _CaptionContainer.SubItems._Add(_IconSeparator, 0);
                    _WindowIcon = new SystemCaptionItem();
                    _WindowIcon.Name = "sys_caption_icon";
                    _WindowIcon.Enabled = false;
                    _WindowIcon.Style = this.Style;
                    _WindowIcon.IsSystemIcon = true;
                    _WindowIcon.QueryIconOnPaint = true;
                    _WindowIcon.DesignTimeVisible = false;
                    _WindowIcon.CanCustomize = false;
                    _WindowIcon.MouseDown += WindowIconMouseDown;
                    _CaptionContainer.SubItems._Add(_WindowIcon, 0);
                }
            }
            else if (effectiveStyle == eDotNetBarStyle.Metro)
            {
                if (_WindowIcon == null)
                {
                    _IconSeparator = new Separator("sys_caption_separator");
                    _IconSeparator.FixedSize = new Size(3, 14);
                    _IconSeparator.SetSystemItem(true);
                    _IconSeparator.DesignTimeVisible = false;
                    _IconSeparator.CanCustomize = false;
                    _CaptionContainer.SubItems._Add(_IconSeparator, 0);
                    _WindowIcon = new SystemCaptionItem();
                    _WindowIcon.Name = "sys_caption_icon";
                    _WindowIcon.Enabled = false;
                    _WindowIcon.Style = this.Style;
                    _WindowIcon.IsSystemIcon = true;
                    _WindowIcon.DesignTimeVisible = false;
                    _WindowIcon.CanCustomize = false;
                    _WindowIcon.QueryIconOnPaint = true;
                    _WindowIcon.MouseDown += WindowIconMouseDown;
                    _WindowIcon.DoubleClick += WindowIconDoubleClick;
                    _CaptionContainer.SubItems._Add(_WindowIcon, 0);
                }
            }
            else if (_WindowIcon != null)
            {
                if (_CaptionContainer.SubItems.Contains(_WindowIcon))
                    _CaptionContainer.SubItems._Remove(_WindowIcon);
                _WindowIcon.MouseDown -= WindowIconMouseDown;
                _WindowIcon.DoubleClick -= WindowIconDoubleClick;
                _WindowIcon.Dispose();
                _WindowIcon = null;
                if (_CaptionContainer.SubItems.Contains(_IconSeparator))
                    _CaptionContainer.SubItems._Remove(_IconSeparator);
                _IconSeparator.Dispose();
                _IconSeparator = null;
            }
            base.OnStyleChanged();
        }

        void WindowIconDoubleClick(object sender, EventArgs e)
        {
            if (_TabStrip != null) 
            {
                _TabStrip.CloseParentForm();
            }
        }

        void WindowIconMouseDown(object sender, MouseEventArgs e)
        {
            MetroTabStrip mts = this.ContainerControl as MetroTabStrip;
            if (mts != null)
            {
                Point p = new Point(_WindowIcon.LeftInternal, _WindowIcon.Bounds.Bottom + 1);
                p = mts.PointToScreen(p);
                mts.ShowSystemMenu(p);
            }
        }
        #endregion

        #region IDesignTimeProvider Members

        public InsertPosition GetInsertPosition(Point pScreen, BaseItem DragItem)
        {
            InsertPosition pos = _ItemContainer.GetInsertPosition(pScreen, DragItem);
            if(pos==null && _TabStrip.CaptionVisible)
                pos = _CaptionContainer.GetInsertPosition(pScreen, DragItem);
            return pos;
        }

        public void DrawReversibleMarker(int iPos, bool Before)
        {
            //DesignTimeProviderContainer.DrawReversibleMarker(this, iPos, Before);
        }

        public void InsertItemAt(BaseItem objItem, int iPos, bool Before)
        {
            //DesignTimeProviderContainer.InsertItemAt(this, objItem, iPos, Before);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is expanded or not. For Popup items this would indicate whether the item is popped up or not.
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.DefaultValue(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public override bool Expanded
        {
            get
            {
                return base.Expanded;
            }
            set
            {
                base.Expanded = value;
                if (!value)
                {
                    foreach (BaseItem item in this.SubItems)
                        item.Expanded = false;
                }
            }
        }

        /// <summary>
        /// When parent items does recalc size for its sub-items it should query
        /// image size and store biggest image size into this property.
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override System.Drawing.Size SubItemsImageSize
        {
            get
            {
                return base.SubItemsImageSize;
            }
            set
            {
                //m_SubItemsImageSize = value;
            }
        }

        /// <summary>
        /// Gets or sets whether Settings button is visible.
        /// </summary>
        public bool SettingsButtonVisible
        {
            get { return _Settings.Visible; }
            set
            {
                if (value != _Settings.Visible)
                {
                    _Settings.Visible = value;
                    this.RecalcSize();
                }
            }
        }

        /// <summary>
        /// Gets or sets whether Help button is visible.
        /// </summary>
        public bool HelpButtonVisible
        {
            get { return _Help.Visible; }
            set
            {
                if (value != _Help.Visible)
                {
                    _Help.Visible = value;
                    this.RecalcSize();
                }
            }
        }

        private string _SettingsButtonText = "";
        /// <summary>
        /// Gets or sets the Settings button text.
        /// </summary>
        public string SettingsButtonText
        {
            get { return _SettingsButtonText; }
            set
            {
                if (value != _SettingsButtonText)
                {
                    string oldValue = _SettingsButtonText;
                    _SettingsButtonText = value;
                    OnSettingsButtonTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SettingsButtonText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSettingsButtonTextChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
                _Settings.Text = DefaultSettingsButtonText;
            else
                _Settings.Text = "<font size=\"7\">" + newValue + "</font>";
        }

        private string _HelpButtonText;
        public string HelpButtonText
        {
            get { return _HelpButtonText; }
            set
            {
                if (value != _HelpButtonText)
                {
                    string oldValue = _HelpButtonText;
                    _HelpButtonText = value;
                    OnHelpButtonTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when HelpButtonText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnHelpButtonTextChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
                _Help.Text = DefaultHelpButtonText;
            else
                _Help.Text = "<font size=\"7\">" + newValue + "</font>";
        }
        #endregion
    }
}
