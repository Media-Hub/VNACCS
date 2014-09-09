using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using DevComponents.DotNetBar.Events;
using System.Collections;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar
{
    [ToolboxItem(true), DefaultEvent("ItemClick"), System.Runtime.InteropServices.ComVisible(false), Designer(typeof(DevComponents.DotNetBar.Design.RadialMenuDesigner)), ToolboxBitmap(typeof(ToolboxIconResFinder), "RadialMenu.ico")]
    public class RadialMenu : Control, IMessageHandlerClient, IOwner, IOwnerItemEvents
    {
        #region Constructor
        private RadialMenuContainer _MenuContainer = null;
        /// <summary>
        /// Initializes a new instance of the RadialMenu class.
        /// </summary>
        public RadialMenu()
        {
            _MenuContainer = new RadialMenuContainer();
            _MenuContainer.RadialMenu = this;
            _MenuContainer.Opened += new EventHandler(RadialMenuOpened);
            _MenuContainer.Closed += new EventHandler(RadialMenuClosed);

            this.SetStyle(
                ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.Opaque |
                ControlStyles.ResizeRedraw |
                DisplayHelp.DoubleBufferFlag |
                ControlStyles.SupportsTransparentBackColor, true);
            _MenuContainer.SetOwner(this);
        }

        protected override void Dispose(bool disposing)
        {
            _MenuContainer.Dispose();
            base.Dispose(disposing);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs before menu has been opened and allows you to cancel opening
        /// </summary>
        [Description("Occurs before menu has been opened and allows you to cancel opening.")]
        public event CancelableEventSourceHandler BeforeMenuOpen;
        /// <summary>
        /// Raises BeforeMenuOpen event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnBeforeMenuOpen(CancelableEventSourceArgs e)
        {
            CancelableEventSourceHandler handler = BeforeMenuOpen;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs after menu has been opened.
        /// </summary>
        [Description("Occurs after menu has been opened.")]
        public event EventHandler MenuOpened;
        /// <summary>
        /// Raises MenuOpened event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnMenuOpened(EventArgs e)
        {
            EventHandler handler = MenuOpened;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs after menu is closed.Occurs after menu is closed.
        /// </summary>
        [Description("Occurs after menu is closed.")]
        public event EventHandler MenuClosed;
        /// <summary>
        /// Raises MenuClosed event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnMenuClosed(EventArgs e)
        {
            EventHandler handler = MenuClosed;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Specifies the width of the sub-menu edge around the radial menu.
        /// </summary>
        [DefaultValue(18), Category("Appearance"), Description("Specifies the width of the sub-menu edge around the radial menu.")]
        public int SubMenuEdgeWidth
        {
            get { return _MenuContainer.SubMenuEdgeWidth; }
            set { _MenuContainer.SubMenuEdgeWidth = value; }
        }
        /// <summary>
        /// Indicates spacing between sub-menu marking edge and the item.
        /// </summary>
        [DefaultValue(3), Category("Appearance"), Description("Indicates spacing between sub-menu marking edge and the item.")]
        public int SubMenuEdgeItemSpacing
        {
            get { return _MenuContainer.SubMenuEdgeItemSpacing; }
            set { _MenuContainer.SubMenuEdgeItemSpacing = value; }
        }

        /// <summary>
        /// Gets or sets radial menu diameter. Minimum value is 64.
        /// </summary>
        [DefaultValue(180), Category("Appearance"), Description("Radial menu diameter.")]
        public int Diameter
        {
            get { return _MenuContainer.Diameter; }
            set { _MenuContainer.Diameter = value; }
        }

        /// <summary>
        /// Gets reference to colors used by the radial menu.
        /// </summary>
        [Category("Appearance"), Description("Gets reference to colors used by the radial menu."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RadialMenuColorTable Colors
        {
            get
            {
                return _MenuContainer.Colors;
            }
        }

        /// <summary>
        /// Indicates the size of the back symbol that is displayed on center of radial menu
        /// </summary>
        [DefaultValue(18), Browsable(false), Description("Indicates the size of the back symbol that is displayed on center of radial menu")]
        public int BackButtonSymbolSize
        {
            get { return _MenuContainer.BackButtonSymbolSize; }
            set { _MenuContainer.BackButtonSymbolSize = value; }
        }

        /// <summary>
        /// Specifies the back button symbol.
        /// </summary>
        [DefaultValue("\uf060"), Category("Appearance"), Description("Specifies the back button symbol.")]
        public string BackButtonSymbol
        {
            get { return _MenuContainer.BackButtonSymbol; }
            set { _MenuContainer.BackButtonSymbol = value; }
        }

        /// <summary>
        /// Indicates diameter of center button of radial menu.
        /// </summary>
        [DefaultValue(32), Category("Appearance"), Description("Indicates diameter of center button of radial menu.")]
        public int CenterButtonDiameter
        {
            get { return _MenuContainer.CenterButtonDiameter; }
            set { _MenuContainer.CenterButtonDiameter = value; }
        }

        /// <summary>
        /// Specifies the maximum pie part angle an item will occupy. Maximum is 180, minimum is 1 degree.
        /// </summary>
        [DefaultValue(90), Category("Appearance"), Description("Specifies the maximum pie part angle an item will occupy. Maximum is 180, minimum is 1 degree.")]
        public int MaxItemPieAngle
        {
            get { return _MenuContainer.MaxItemPieAngle; }
            set { _MenuContainer.MaxItemPieAngle = value; }
        }

        /// <summary>
        /// Indicates maximum radial angle single item in menu can consume. By default this property is set to zero which indicates that
        /// radial menu is equally divided between visible menu items.
        /// </summary>
        [DefaultValue(0), Category("Appearance"), Description("Indicates maximum radial angle single item in menu can consume. Max value is 180.")]
        public int MaxItemRadialAngle
        {
            get { return _MenuContainer.MaxItemRadialAngle; }
            set { _MenuContainer.MaxItemRadialAngle = value; }
        }

        /// <summary>
        /// Indicates the position of top-left corner of radial menu when shown in screen coordinates
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Category("Behavior"), Description("Indicates the position of top-left corner of radial menu when shown in screen coordinates")]
        public Point MenuLocation
        {
            get { return _MenuContainer.MenuLocation; }
            set { _MenuContainer.MenuLocation = value; }
        }

        /// <summary>
        /// Gets or sets whether radial menu is open.
        /// </summary>
        //[Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        //public bool IsMenuOpen
        //{
        //    get
        //    {
        //        return _MenuContainer.Expanded;
        //    }
        //    set
        //    {
        //        if (_MenuContainer.Expanded != value)
        //            _MenuContainer.Expanded = value;
        //    }
        //}

        private Image _Image = null;
        /// <summary>
        /// Indicates image displayed in center of the radial menu.
        /// </summary>
        [DefaultValue(null), Description("Indicates image displayed in center of the radial menu."), Category("Appearance")]
        public Image Image
        {
            get { return _Image; }
            set
            {
                if (value != _Image)
                {
                    Image oldValue = _Image;
                    _Image = value;
                    OnImageChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Image property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnImageChanged(Image oldValue, Image newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Image"));
            this.Invalidate();
        }

        /// <summary>
        /// Indicates symbol displayed in center of the radial menu. When set, it overrides any Image settings.
        /// </summary>
        [DefaultValue(""), Description("Indicates image displayed in center of the radial menu.  When set, it overrides any Image settings."), Category("Appearance")]
        [Editor("DevComponents.DotNetBar.Design.SymbolTypeEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public string Symbol
        {
            get { return _MenuContainer.Symbol; }
            set { _MenuContainer.Symbol = value; _SymbolTextSize = Size.Empty; this.Invalidate(); }
        }

        /// <summary>
        /// Indicates the size of the symbol in points.
        /// </summary>
        [DefaultValue(0f), Category("Appearance"), Description("Indicates the size of the symbol in points.")]
        public float SymbolSize
        {
            get { return _MenuContainer.SymbolSize; }
            set { _MenuContainer.SymbolSize = value; _SymbolTextSize = Size.Empty; this.Invalidate(); }
        }

        private bool _IsOpen;
        /// <summary>
        /// Gets or sets whether radial menu is open.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsOpen
        {
            get { return _IsOpen; }
            set
            {
                if (value != _IsOpen)
                {
                    SetIsOpen(value, eEventSource.Code);
                }
            }
        }
        /// <summary>
        /// Called when IsOpen property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
        }
        /// <summary>
        /// Sets whether radial menu is open and provides the source of the action.
        /// </summary>
        /// <param name="isOpen">true to open menu, false to close it.</param>
        /// <param name="source">Source of the action.</param>
        public void SetIsOpen(bool isOpen, eEventSource source)
        {
            if (isOpen == _IsOpen) return;

            if (isOpen)
            {
                CancelableEventSourceArgs cancel = new CancelableEventSourceArgs(source);
                OnBeforeMenuOpen(cancel);
                if (cancel.Cancel) return;
            }

            bool oldValue = _IsOpen;
            _IsOpen = isOpen;
            _MenuContainer.Expanded = isOpen;
            OnIsOpenChanged(oldValue, isOpen);
        }
        /// <summary>
        /// Returns collection of items on a bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection Items
        {
            get
            {
                return _MenuContainer.SubItems;
            }
        }

        protected override void OnClick(EventArgs e)
        {
            SetIsOpen(!this.IsOpen, eEventSource.Mouse);
            base.OnClick(e);
        }

        private Size _SymbolTextSize = Size.Empty;
        private bool _Painting = false;
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_Painting) return; // Stops painting when using transparent backgrounds since paint on parent paints this control too

            _Painting = true;
            try
            {
                RadialMenuColorTable renderTable = RadialMenuContainer.GetColorTable();
                RadialMenuColorTable localTable = _MenuContainer.Colors;

                Color symbolColor = ColorScheme.GetColor(0x2B579A);
                Color backColor = Color.White;

                if (!localTable.RadialMenuButtonBorder.IsEmpty)
                    symbolColor = localTable.RadialMenuButtonBorder;
                else if (renderTable != null && !renderTable.RadialMenuButtonBorder.IsEmpty)
                    symbolColor = renderTable.RadialMenuButtonBorder;

                if (!localTable.RadialMenuButtonBackground.IsEmpty)
                    backColor = localTable.RadialMenuButtonBackground;
                else if (renderTable != null && !renderTable.RadialMenuButtonBackground.IsEmpty)
                    backColor = renderTable.RadialMenuButtonBackground;

                SolidBrush backBrush = new SolidBrush(backColor);

                Graphics g = e.Graphics;

                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;

                Rectangle r = this.ClientRectangle;
                r.Inflate(1, 1);
                if (!this.BackColor.IsEmpty && this.BackColor != Color.Transparent)
                {
                    using (SolidBrush brush = new SolidBrush(this.BackColor))
                        g.FillRectangle(brush, r);
                }
                else if (this.BackColor == Color.Transparent)
                    PaintParentControlOnBackground(e.Graphics);

                r = this.ClientRectangle;
                r.Width--;
                r.Height--;

                Rectangle fill = r;
                fill.Inflate(-1, -1);
                g.FillEllipse(backBrush, fill);
                using (GraphicsPath path = new GraphicsPath())
                {
                    path.AddEllipse(r);
                    r.Inflate(-2, -2);
                    path.AddEllipse(r);
                    using (SolidBrush brush = new SolidBrush(symbolColor))
                        g.FillPath(brush, path);
                }

                if (!string.IsNullOrEmpty(_MenuContainer.Symbol))
                {
                    float symbolSize = _MenuContainer.SymbolSize;
                    Font symFont = Symbols.GetFontAwesome(symbolSize);
                    if (_SymbolTextSize.IsEmpty)
                    {
                        _SymbolTextSize = TextDrawing.MeasureStringLegacy(g, _MenuContainer.Symbol, Symbols.GetFontAwesome(symbolSize), Size.Empty, eTextFormat.Default);
                        int descent = (int)Math.Ceiling((symFont.FontFamily.GetCellDescent(symFont.Style) *
                        symFont.Size / symFont.FontFamily.GetEmHeight(symFont.Style)));
                        _SymbolTextSize.Height -= descent;
                    }

                    TextDrawing.DrawStringLegacy(g, _MenuContainer.Symbol, Symbols.GetFontAwesome(symbolSize),
                        symbolColor, new Rectangle(this.ClientRectangle.Width / 2, r.Y + (r.Height - _SymbolTextSize.Height) / 2 + 1, 0, 0),
                        eTextFormat.HorizontalCenter);
                }
                else if (_Image != null)
                {
                    r = this.ClientRectangle;
                    ImageHelper.DrawImageCenteredDpiAware(g, _Image, r);
                }

                backBrush.Dispose(); backBrush = null;
            }
            finally
            {
                _Painting = false;
            }
            base.OnPaint(e);
        }

        private void PaintParentControlOnBackground(Graphics g)
        {
            Control parent = this.Parent;
            if (parent != null)
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                using (Bitmap bmp = new Bitmap(parent.Width, parent.Height, g))
                {
                    parent.DrawToBitmap(bmp, parent.ClientRectangle);
                    g.TranslateTransform(-Left, -Top);
                    g.DrawImage(bmp, Point.Empty);
                    g.TranslateTransform(Left, Top);
                }
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(28, 28);
            }
        }
        public override System.Drawing.Size GetPreferredSize(System.Drawing.Size proposedSize)
        {
            if (proposedSize.Width <= 0) return new System.Drawing.Size(26, 26);

            return new System.Drawing.Size(proposedSize.Width, proposedSize.Height);
        }

        void RadialMenuClosed(object sender, EventArgs e)
        {
            _IsOpen = false;
            OnMenuClosed(EventArgs.Empty);
        }

        void RadialMenuOpened(object sender, EventArgs e)
        {
            OnMenuOpened(EventArgs.Empty);
        }

        private bool _ShortcutsEnabled = true;
        /// <summary>
        /// Gets or sets whether shortcut processing for the items hosted by this control is enabled. Default value is true.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool ShortcutsEnabled
        {
            get { return _ShortcutsEnabled; }
            set { _ShortcutsEnabled = value; }
        }

        private bool _CloseMenuOnAppDeactivate = true;
        /// <summary>
        /// Indicates whether radial menu is closed automatically when app is deactivated. Default value is true, if set to false
        /// you are responsible for closing the menu.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether radial menu is closed automatically when app is deactivated"), Browsable(false)]
        public bool CloseMenuOnAppDeactivate
        {
            get { return _CloseMenuOnAppDeactivate; }
            set
            {
                _CloseMenuOnAppDeactivate = value;
            }
        }

        private bool _UseHook = false;
        /// <summary>
        /// Gets or sets whether hooks are used for internal DotNetBar system functionality. Using hooks is recommended only if DotNetBar is used in hybrid environments like Visual Studio designers or IE.
        /// </summary>
        [Browsable(false), DefaultValue(false), Category("Behavior"), Description("Gets or sets whether hooks are used for internal DotNetBar system functionality. Using hooks is recommended only if DotNetBar is used in hybrid environments like Visual Studio designers or IE.")]
        public bool UseHook
        {
            get
            {
                return _UseHook;
            }
            set
            {
                _UseHook = value;
            }
        }

        private bool _MenuEventSupport = false;
        private Hook _Hook = null;
        private bool _FilterInstalled = false;
        protected override void OnHandleCreated(EventArgs e)
        {
            if (!_FilterInstalled && !this.DesignMode)
            {
                MessageHandler.RegisterMessageClient(this);
                _FilterInstalled = true;
            }

            if (!this.GetDesignMode() && !_UseHook)
            {
                if (!_MenuEventSupport)
                    MenuEventSupportHook();
            }
            else
            {
                if (_Hook == null)
                {
                    _Hook = new Hook(this);
                }
            }
            base.OnHandleCreated(e);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            MenuEventSupportUnhook();

            base.OnHandleDestroyed(e);

            if (_FilterInstalled)
            {
                MessageHandler.UnregisterMessageClient(this);
                _FilterInstalled = false;
            }

            if (_Hook != null)
            {
                _Hook.Dispose();
                _Hook = null;
            }
        }
        private void MenuEventSupportUnhook()
        {
            if (!_MenuEventSupport)
                return;
            _MenuEventSupport = false;

            Form parentForm = this.FindForm();
            if (parentForm == null)
                return;
            DotNetBarManager.UnRegisterParentMsgHandler(this, parentForm);
            parentForm.Resize -= new EventHandler(this.ParentResize);
            parentForm.Deactivate -= new EventHandler(this.ParentDeactivate);
        }

        /// <summary>
        /// Indicates the radial menu type. eRadialMenuType.Segment menu type allows for display of image, text and any sub-menu items.
        /// eRadialMenuType.Circular allows only for display of Symbol or Image and it does not display either text or sub-menu items. 
        /// eRadialMenuType.Circular is designed to be used for single level menus only that don't need text.
        /// </summary>
        [DefaultValue(eRadialMenuType.Segment), Category("Appearance"), Description("Indicates the radial menu type.")]
        public eRadialMenuType MenuType
        {
            get { return _MenuContainer.MenuType; }
            set { _MenuContainer.MenuType = value; }
        }
        #endregion

        #region IMessageHandlerClient Implementation
        bool IMessageHandlerClient.IsModal
        {
            get
            {
                Form form = this.FindForm();
                if (form != null)
                    return form.Modal;
                return false;
            }
        }

        bool IMessageHandlerClient.OnMouseWheel(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            return OnMouseWheel(hWnd, wParam, lParam);
        }

        protected virtual bool OnMouseWheel(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            return false;
        }

        bool IMessageHandlerClient.OnKeyDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            return OnKeyDown(hWnd, wParam, lParam);
        }

        /// <summary>
        /// Returns whether control has any popups registered.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
        public bool HasRegisteredPopups
        {
            get
            {
                return _MenuContainer.Expanded;
            }
        }

        #region OnKeyDown
        private bool _DesignModeInternal = false;
        protected bool GetDesignMode()
        {
            if (!_DesignModeInternal)
                return this.DesignMode;
            return _DesignModeInternal;
        }
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public void SetDesignMode(bool mode)
        {
            _DesignModeInternal = mode;
            _MenuContainer.SetDesignMode(mode);
        }
        protected virtual bool OnKeyDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            bool designMode = this.GetDesignMode();

            int wParamInt = WinApi.ToInt(wParam);

            if (_MenuContainer.Expanded && wParamInt == 27) // Escape
            {
                this.SetIsOpen(false, eEventSource.Keyboard);
                return true;
            }
            if (_MenuContainer.Expanded && _MenuContainer.Popup != null && _MenuContainer.Popup.Handle == hWnd)
            {
                Keys key = (Keys)NativeFunctions.MapVirtualKey((uint)wParam, 2);
                if (key == Keys.None)
                    key = (Keys)wParamInt;
                _MenuContainer.InternalKeyDown(new KeyEventArgs(key));
                return false;
            }

            if (!this.IsParentFormActive)
                return false;

            if (wParamInt >= 0x70 || ModifierKeys != Keys.None || (WinApi.ToInt(lParam) & 0x1000000000) != 0 || wParamInt == 0x2E || wParamInt == 0x2D) // 2E=VK_DELETE, 2D=VK_INSERT
            {
                int i = (int)ModifierKeys | wParamInt;
                return ProcessShortcut((eShortcut)i) && !designMode;
            }
            return false;
        }
        #endregion

        private bool _DispatchShortcuts = false;
        /// <summary>
        /// Indicates whether shortcuts handled by items are dispatched to the next handler or control.
        /// </summary>
        [Browsable(false), DefaultValue(false), Category("Behavior"), Description("Indicates whether shortucts handled by items are dispatched to the next handler or control.")]
        public bool DispatchShortcuts
        {
            get { return _DispatchShortcuts; }
            set { _DispatchShortcuts = value; }
        }
        private bool ProcessShortcut(eShortcut key)
        {
            if (!_ShortcutsEnabled || !this.Enabled) return false;

            Form form = this.FindForm();
            if (form == null || (form != Form.ActiveForm && form.MdiParent == null ||
                form.MdiParent != null && form.MdiParent.ActiveMdiChild != form) && !form.IsMdiContainer || Form.ActiveForm != null && Form.ActiveForm.Modal && Form.ActiveForm != form)
                return false;

            bool eat = BarFunctions.ProcessItemsShortcuts(key, _ShortcutTable);
            return !_DispatchShortcuts && eat;
        }
        protected bool IsParentFormActive
        {
            get
            {
                // Process only if parent form is active
                Form form = this.FindForm();
                if (form == null)
                    return false;
                if (form.IsMdiChild)
                {
                    if (form.MdiParent == null)
                        return false;
                    if (form.MdiParent.ActiveMdiChild != form)
                        return false;
                }
                else if (form != Form.ActiveForm)
                    return false;
                return true;
            }
        }

        private PopupDelayedClose m_DelayClose = null;
        private PopupDelayedClose GetDelayClose()
        {
            if (m_DelayClose == null)
                m_DelayClose = new PopupDelayedClose();
            return m_DelayClose;
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void DesignerNewItemAdded()
        {
            this.GetDelayClose().EraseDelayClose();
        }
        private ArrayList _RegisteredPopups = new ArrayList();
        protected virtual bool OnSysMouseDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            if (!_IsOpen || this.GetDesignMode() || _MenuContainer.Popup == null)
                return false;

            if (!_MenuContainer.Popup.Bounds.Contains(MousePosition))
            {
                SetIsOpen(false, eEventSource.Mouse);
            }

            return false;
        }
        bool IMessageHandlerClient.OnMouseDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            return OnSysMouseDown(hWnd, wParam, lParam);
        }
        bool IMessageHandlerClient.OnMouseMove(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            if (_IsOpen && _MenuContainer.Popup != null && _MenuContainer.Popup.Handle != hWnd) return true; // Eat mouse messages for other windows while popup is open
            return false;
        }
        bool IMessageHandlerClient.OnSysKeyDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            return OnSysKeyDown(hWnd, wParam, lParam);
        }

        protected virtual bool OnSysKeyDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            if (!this.GetDesignMode())
            {
                int wParamInt = WinApi.ToInt(wParam);
                if (wParamInt == 18 && System.Windows.Forms.Control.ModifierKeys == System.Windows.Forms.Keys.Alt)
                    this.SetIsOpen(false, eEventSource.Keyboard);

                // Check Shortcuts
                if (ModifierKeys != Keys.None || wParamInt >= (int)eShortcut.F1 && wParamInt <= (int)eShortcut.F12)
                {
                    int i = (int)ModifierKeys | wParamInt;
                    if (ProcessShortcut((eShortcut)i))
                        return true;
                }
            }
            return false;
        }

        bool IMessageHandlerClient.OnSysKeyUp(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            return OnSysKeyUp(hWnd, wParam, lParam);
        }

        protected virtual bool OnSysKeyUp(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            return false;
        }
        private void MenuEventSupportHook()
        {
            if (_MenuEventSupport)
                return;
            _MenuEventSupport = true;

            Form parentForm = this.FindForm();
            if (parentForm == null)
            {
                _MenuEventSupport = false;
                return;
            }

            parentForm.Resize += new EventHandler(this.ParentResize);
            parentForm.Deactivate += new EventHandler(this.ParentDeactivate);

            DotNetBarManager.RegisterParentMsgHandler(this, parentForm);
        }

        private void ParentResize(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null && parentForm.WindowState == FormWindowState.Minimized)
                ((IOwner)this).OnApplicationDeactivate();
        }
        private void ParentDeactivate(object sender, EventArgs e)
        {
            Form parentForm = this.FindForm();
            if (parentForm != null && parentForm.WindowState == FormWindowState.Minimized)
                ((IOwner)this).OnApplicationDeactivate();
        }
        #endregion

        #region IOwner Members

        Form IOwner.ParentForm
        {
            get
            {
                return base.FindForm();
            }
            set { }
        }
        /// <summary>
        /// Returns the collection of items with the specified name.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <returns></returns>
        System.Collections.ArrayList IOwner.GetItems(string ItemName)
        {
            ArrayList list = new ArrayList(15);
            BarFunctions.GetSubItemsByName(_MenuContainer, ItemName, list);
            return list;
        }
        /// <summary>
        /// Returns the collection of items with the specified name and type.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <param name="itemType">Item type to look for.</param>
        /// <returns></returns>
        System.Collections.ArrayList IOwner.GetItems(string ItemName, Type itemType)
        {
            ArrayList list = new ArrayList(15);
            BarFunctions.GetSubItemsByNameAndType(_MenuContainer, ItemName, list, itemType);
            return list;
        }
        /// <summary>
        /// Returns the collection of items with the specified name and type.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <param name="itemType">Item type to look for.</param>
        /// <param name="useGlobalName">Indicates whether GlobalName property is used for searching.</param>
        /// <returns></returns>
        System.Collections.ArrayList IOwner.GetItems(string ItemName, Type itemType, bool useGlobalName)
        {
            ArrayList list = new ArrayList(15);
            BarFunctions.GetSubItemsByNameAndType(_MenuContainer, ItemName, list, itemType, useGlobalName);
            return list;
        }
        /// <summary>
        /// Returns the first item that matches specified name.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <returns></returns>
        BaseItem IOwner.GetItem(string ItemName)
        {
            BaseItem item = BarFunctions.GetSubItemByName(_MenuContainer, ItemName);
            if (item != null)
                return item;
            return null;
        }

        private BaseItem _ExpandedItem = null;
        void IOwner.SetExpandedItem(BaseItem objItem)
        {
            if (objItem != null && (objItem.Parent is RadialMenuContainer || objItem.Parent is RadialMenu))
                return;
            if (_ExpandedItem != null)
            {
                if (_ExpandedItem.Expanded)
                    _ExpandedItem.Expanded = false;
                _ExpandedItem = null;
            }
            _ExpandedItem = objItem;
        }

        BaseItem IOwner.GetExpandedItem()
        {
            return _ExpandedItem;
        }

        private BaseItem _FocusItem = null;
        // Currently we are using this to communicate "focus" when control is in
        // design mode. This can be used later if we decide to add focus
        // handling to our BaseItem class.
        void IOwner.SetFocusItem(BaseItem objFocusItem)
        {
            if (_FocusItem != null && _FocusItem != objFocusItem)
            {
                _FocusItem.OnLostFocus();
            }
            OnSetFocusItem(objFocusItem);
            _FocusItem = objFocusItem;
            if (_FocusItem != null)
                _FocusItem.OnGotFocus();
        }
        protected virtual void OnSetFocusItem(BaseItem objFocusItem)
        {
        }
        BaseItem IOwner.GetFocusItem()
        {
            return _FocusItem;
        }

        void IOwner.DesignTimeContextMenu(BaseItem objItem) { }

        private Hashtable _ShortcutTable = new Hashtable();
        void IOwner.RemoveShortcutsFromItem(BaseItem objItem)
        {
            ShortcutTableEntry objEntry = null;
            if (objItem.ShortcutString != "")
            {
                foreach (eShortcut key in objItem.Shortcuts)
                {
                    if (_ShortcutTable.ContainsKey(key))
                    {
                        objEntry = (ShortcutTableEntry)_ShortcutTable[key];
                        try
                        {
                            objEntry.Items.Remove(objItem.Id);
                            if (objEntry.Items.Count == 0)
                                _ShortcutTable.Remove(objEntry.Shortcut);
                        }
                        catch (ArgumentException) { }
                    }
                }
            }
            IOwner owner = this as IOwner;
            foreach (BaseItem objTmp in objItem.SubItems)
                owner.RemoveShortcutsFromItem(objTmp);
        }

        void IOwner.AddShortcutsFromItem(BaseItem objItem)
        {
            ShortcutTableEntry objEntry = null;
            if (objItem.ShortcutString != "")
            {
                foreach (eShortcut key in objItem.Shortcuts)
                {
                    if (_ShortcutTable.ContainsKey(key))
                        objEntry = (ShortcutTableEntry)_ShortcutTable[objItem.Shortcuts[0]];
                    else
                    {
                        objEntry = new ShortcutTableEntry(key);
                        _ShortcutTable.Add(objEntry.Shortcut, objEntry);
                    }
                    try
                    {
                        objEntry.Items.Add(objItem.Id, objItem);
                    }
                    catch (ArgumentException) { }
                }
            }
            IOwner owner = this as IOwner;
            foreach (BaseItem objTmp in objItem.SubItems)
                owner.AddShortcutsFromItem(objTmp);
        }

        ImageList IOwner.Images
        {
            get { return null; }
            set { }
        }

        ImageList IOwner.ImagesMedium
        {
            get { return null; }
            set { }
        }

        ImageList IOwner.ImagesLarge
        {
            get { return null; }
            set { }
        }

        void IOwner.StartItemDrag(BaseItem objItem)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        BaseItem IOwner.DragItem
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        bool IOwner.DragInProgress
        {
            get { throw new Exception("The method or operation is not implemented."); }
        }

        private bool _ShowToolTips = true;
        /// <summary>
        /// Indicates whether items show tooltips.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether items show tooltips.")]
        public bool ShowToolTips
        {
            get
            {
                return _ShowToolTips;
            }
            set
            {
                _ShowToolTips = value;
            }
        }

        private bool _ShowShortcutKeysInToolTips = false;
        /// <summary>
        /// Indicates whether item shortcut is displayed in Tooltips.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Behavior"), Description("Indicates whether item shortcut is displayed in Tooltips.")]
        public virtual bool ShowShortcutKeysInToolTips
        {
            get
            {
                return _ShowShortcutKeysInToolTips;
            }
            set
            {
                _ShowShortcutKeysInToolTips = value;
            }
        }


        private bool _AlwaysDisplayKeyAccelerators = false;
        /// <summary>
        /// Gets or sets whether accelerator letters on buttons are underlined. Default value is false which indicates that system setting is used
        /// to determine whether accelerator letters are underlined. Setting this property to true
        /// will always display accelerator letter underlined.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Behavior"), Description("Indicates whether accelerator letters for buttons are underlined regardless of current Windows settings.")]
        public bool AlwaysDisplayKeyAccelerators
        {
            get { return _AlwaysDisplayKeyAccelerators; }
            set
            {
                if (_AlwaysDisplayKeyAccelerators != value)
                {
                    _AlwaysDisplayKeyAccelerators = value;
                    this.Invalidate();
                }
            }
        }

        Form IOwner.ActiveMdiChild
        {
            get
            {
                Form form = base.FindForm();
                if (form == null)
                    return null;
                if (form.IsMdiContainer)
                {
                    return form.ActiveMdiChild;
                }
                return null;
            }
        }

        MdiClient IOwner.GetMdiClient(Form MdiForm)
        {
            return BarFunctions.GetMdiClient(MdiForm);
        }

        void IOwner.Customize()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        void IOwner.InvokeResetDefinition(BaseItem item, EventArgs e)
        {

        }

        bool IOwner.ShowResetButton
        {
            get
            {
                return false;
            }
            set
            {
                throw new Exception("The method or operation is not implemented.");
            }
        }

        void IOwner.InvokeDefinitionLoaded(object sender, EventArgs e)
        {

        }

        void IOwner.InvokeUserCustomize(object sender, EventArgs e)
        {

        }

        void IOwner.InvokeEndUserCustomize(object sender, EndUserCustomizeEventArgs e)
        {

        }

        void IOwner.OnApplicationActivate()
        {

        }

        void IOwner.OnApplicationDeactivate()
        {
            if (this.IsOpen && _CloseMenuOnAppDeactivate) this.IsOpen = false;
        }

        void IOwner.OnParentPositionChanging()
        {
            if (this.IsOpen) this.IsOpen = false;
        }

        bool IOwner.DesignMode
        {
            get { return this.DesignMode; }
        }

        private bool _DisabledImagesGrayScale = true;
        /// <summary>
        /// Gets or sets whether gray-scale algorithm is used to create automatic gray-scale images. Default is true.
        /// </summary>
        [Browsable(true), DefaultValue(true), Category("Appearance"), Description("Gets or sets whether gray-scale algorithm is used to create automatic gray-scale images.")]
        public bool DisabledImagesGrayScale
        {
            get
            {
                return _DisabledImagesGrayScale;
            }
            set
            {
                _DisabledImagesGrayScale = value;
            }
        }
        #endregion

        #region IOwnerItemEvents Members
        /// <summary>
        /// Occurs after an item has been added to items collection.
        /// </summary>
        [Description("Occurs after an item has been added to items collection.")]
        public event EventHandler ItemAdded;
        /// <summary>
        /// Raises ItemAdded event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemAdded(BaseItem source, EventArgs e)
        {
            EventHandler handler = ItemAdded;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeItemAdded(BaseItem item, EventArgs e)
        {
            OnItemAdded(item, e);
        }

        /// <summary>
        /// Occurs after an item has been removed from items collection.
        /// </summary>
        [Description("Occurs after an item has been removed from items collection.")]
        public event DevComponents.DotNetBar.ItemControl.ItemRemovedEventHandler ItemRemoved;
        /// <summary>
        /// Raises ItemRemoved event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemRemoved(BaseItem source, ItemRemovedEventArgs e)
        {
            DevComponents.DotNetBar.ItemControl.ItemRemovedEventHandler handler = ItemRemoved;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeItemRemoved(BaseItem item, BaseItem parent)
        {
            OnItemRemoved(item, new ItemRemovedEventArgs(parent));
        }

        /// <summary>
        /// Occurs when mouse pointer enters boundaries of an item.
        /// </summary>
        [Description("Occurs when mouse pointer enters boundaries of an item.")]
        public event EventHandler ItemMouseEnter;
        /// <summary>
        /// Raises ItemMouseEnter event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemMouseEnter(BaseItem source, EventArgs e)
        {
            EventHandler handler = ItemMouseEnter;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeMouseEnter(BaseItem item, EventArgs e)
        {
            OnItemMouseEnter(item, e);
        }

        /// <summary>
        /// Occurs when mouse pointer hovers over an item.
        /// </summary>
        [Description("Occurs when mouse pointer hovers over an item.")]
        public event EventHandler ItemMouseHover;
        /// <summary>
        /// Raises ItemMouseHover event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemMouseHover(BaseItem source, EventArgs e)
        {
            EventHandler handler = ItemMouseHover;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeMouseHover(BaseItem item, EventArgs e)
        {
            OnItemMouseHover(item, e);
        }

        /// <summary>
        /// Occurs when mouse pointer leaves boundaries of an item.
        /// </summary>
        [Description("Occurs when mouse pointer leaves boundaries of an item.")]
        public event EventHandler ItemMouseLeave;
        /// <summary>
        /// Raises ItemMouseLeave event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemMouseLeave(BaseItem source, EventArgs e)
        {
            EventHandler handler = ItemMouseLeave;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeMouseLeave(BaseItem item, EventArgs e)
        {
            OnItemMouseLeave(item, e);
        }

        /// <summary>
        /// Occurs when mouse button is pressed on item.
        /// </summary>
        [Description("Occurs when mouse button is pressed on item.")]
        public event MouseEventHandler ItemMouseDown;
        /// <summary>
        /// Raises ItemMouseDown event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemMouseDown(BaseItem source, MouseEventArgs e)
        {
            MouseEventHandler handler = ItemMouseDown;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeMouseDown(BaseItem item, MouseEventArgs e)
        {
            OnItemMouseDown(item, e);
        }

        /// <summary>
        /// Occurs when mouse button is released on item.
        /// </summary>
        [Description("Occurs when mouse button is released on item.")]
        public event MouseEventHandler ItemMouseUp;
        /// <summary>
        /// Raises ItemMouseUp event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemMouseUp(BaseItem source, MouseEventArgs e)
        {
            MouseEventHandler handler = ItemMouseUp;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeMouseUp(BaseItem item, MouseEventArgs e)
        {
            OnItemMouseUp(item, e);
        }

        /// <summary>
        /// Occurs when mouse moves over an item.
        /// </summary>
        [Description("Occurs when mouse moves over an item.")]
        public event MouseEventHandler ItemMouseMove;
        /// <summary>
        /// Raises ItemMouseMove event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemMouseMove(BaseItem source, MouseEventArgs e)
        {
            MouseEventHandler handler = ItemMouseMove;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeMouseMove(BaseItem item, MouseEventArgs e)
        {
            OnItemMouseMove(item, e);
        }

        /// <summary>
        /// Occurs when an item is clicked.
        /// </summary>
        [Description("Occurs when an item is clicked.")]
        public event EventHandler ItemClick;
        /// <summary>
        /// Raises ItemClick event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemClick(BaseItem source, EventArgs e)
        {
            EventHandler handler = ItemClick;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeItemClick(BaseItem objItem)
        {
            OnItemClick(objItem, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when an item is double-clicked.
        /// </summary>
        [Description("Occurs when an item is double-clicked.")]
        public event MouseEventHandler ItemDoubleClick;
        /// <summary>
        /// Raises ItemDoubleClick event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemDoubleClick(BaseItem source, MouseEventArgs e)
        {
            MouseEventHandler handler = ItemDoubleClick;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeItemDoubleClick(BaseItem objItem, MouseEventArgs e)
        {
            OnItemDoubleClick(objItem, e);
        }

        void IOwnerItemEvents.InvokeGotFocus(BaseItem item, EventArgs e)
        {

        }

        void IOwnerItemEvents.InvokeLostFocus(BaseItem item, EventArgs e)
        {

        }

        /// <summary>
        /// Occurs when an item Expanded property value has changed.
        /// </summary>
        [Description("Occurs when an item Expanded property value has changed.")]
        public event EventHandler ItemExpandedChanged;
        /// <summary>
        /// Raises ItemExpandedChanged event.
        /// </summary>
        /// <param name="source">Reference to item.</param>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemExpandedChanged(BaseItem source, EventArgs e)
        {
            EventHandler handler = ItemExpandedChanged;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeExpandedChange(BaseItem item, EventArgs e)
        {
            OnItemExpandedChanged(item, e);
        }

        /// <summary>
        /// Occurs when an item Text property value has changed.
        /// </summary>
        [Description("Occurs when an item Text property value has changed.")]
        public event EventHandler ItemTextChanged;
        /// <summary>
        /// Raises ItemTextChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemTextChanged(BaseItem source, EventArgs e)
        {
            EventHandler handler = ItemTextChanged;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeItemTextChanged(BaseItem item, EventArgs e)
        {
            OnItemTextChanged(item, e);
        }

        void IOwnerItemEvents.InvokeContainerControlDeserialize(BaseItem item, ControlContainerSerializationEventArgs e)
        {

        }

        void IOwnerItemEvents.InvokeContainerControlSerialize(BaseItem item, ControlContainerSerializationEventArgs e)
        {

        }

        void IOwnerItemEvents.InvokeContainerLoadControl(BaseItem item, EventArgs e)
        {

        }

        void IOwnerItemEvents.InvokeOptionGroupChanging(BaseItem item, OptionGroupChangingEventArgs e)
        {

        }

        /// <summary>
        /// Occurs when tooltip for an item is about to be displayed.
        /// </summary>
        [Description("Occurs when tooltip for an item is about to be displayed.")]
        public event EventHandler ItemTooltipShowing;
        /// <summary>
        /// Raises ItemTooltipShowing event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnItemTooltipShowing(object source, EventArgs e)
        {
            EventHandler handler = ItemTooltipShowing;
            if (handler != null)
                handler(source, e);
        }
        void IOwnerItemEvents.InvokeToolTipShowing(object item, EventArgs e)
        {
            OnItemTooltipShowing(item, e);
        }

        void IOwnerItemEvents.InvokeCheckedChanged(ButtonItem item, EventArgs e)
        {

        }

        #endregion
    }
}
