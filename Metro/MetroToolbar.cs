using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using DevComponents.DotNetBar.Metro.Rendering;
using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Metro
{
    [ToolboxBitmap(typeof(MetroShell), "MetroToolbar.ico"), ToolboxItem(true), Designer(typeof(DevComponents.DotNetBar.Design.MetroToolbarDesigner)), System.Runtime.InteropServices.ComVisible(false)]
    public class MetroToolbar : ItemControl
    {
        #region Events
        /// <summary>
        /// Occurs before Expanded property has changed, i.e. control expanded or collapsed and allows you to cancel action by setting Cancel=true on event arguments.
        /// </summary>
        [Description("Occurs before Expanded property has changed, i.e. control expanded or collapsed and allows you to cancel action by setting Cancel=true on event arguments.")]
        public event CancelEventHandler ExpandedChanging;
        /// <summary>
        /// Raises ExpandedChanging event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnExpandedChanging(CancelEventArgs e)
        {
            CancelEventHandler handler = ExpandedChanging;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Occurs after Expanded property value has changed, i.e. control was expanded or collapsed.
        /// </summary>
        [Description("Occurs after Expanded property value has changed, i.e. control was expanded or collapsed.")]
        public event EventHandler ExpandedChanged;
        /// <summary>
        /// Raises ExpandedChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnExpandedChanged(EventArgs e)
        {
            EventHandler handler = ExpandedChanged;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        private MetroToolbarContainer _ItemContainer = null;
        /// <summary>
        /// Initializes a new instance of the MetroStatusBar class.
        /// </summary>
        public MetroToolbar()
        {
            _ItemContainer = new MetroToolbarContainer(this);
            _ItemContainer.GlobalItem = false;
            _ItemContainer.ContainerControl = this;
            _ItemContainer.Stretch = true;
            _ItemContainer.Displayed = true;
            _ItemContainer.Style = eDotNetBarStyle.StyleManagerControlled;
            //base.AutoSize = true;
            this.ColorScheme.Style = eDotNetBarStyle.StyleManagerControlled;
            _ItemContainer.SetOwner(this);
            this.SetBaseItemContainer(_ItemContainer);
            this.DragDropSupport = true;
            this.ItemAdded += new EventHandler(ChildItemAdded);
            this.ItemRemoved += new ItemRemovedEventHandler(ChildItemRemoved);
        }

        protected override void Dispose(bool disposing)
        {
            this.ItemAdded -= new EventHandler(ChildItemAdded);
            this.ItemRemoved -= new ItemRemovedEventHandler(ChildItemRemoved);
            base.Dispose(disposing);
        }
        #endregion

        #region Internal Implementation
        protected override void PaintControlBackground(ItemPaintArgs pa)
        {
            MetroRender.Paint(this, pa);
            base.PaintControlBackground(pa);
        }
        private void ChildItemAdded(object sender, EventArgs e)
        {
            this.RecalcLayout();
        }
        private void ChildItemRemoved(object sender, ItemRemovedEventArgs e)
        {
            this.RecalcLayout();
        }
        protected override void OnForeColorChanged(EventArgs e)
        {
            _ItemContainer.UpdateSystemColors();
            base.OnForeColorChanged(e);
        }
        /// <summary>
        /// Returns collection of items on a bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection Items
        {
            get
            {
                return _ItemContainer.MainItemsContainer.SubItems;
            }
        }
        /// <summary>
        /// Gets or sets spacing between items, default value is 2.
        /// </summary>
        [DefaultValue(0), Category("Appearance"), Description("Gets or sets spacing between items.")]
        public int ItemSpacing
        {
            get { return _ItemContainer.MainItemsContainer.ItemSpacing; }
            set
            {
                _ItemContainer.MainItemsContainer.ItemSpacing = value;
                this.RecalcLayout();
            }
        }

        /// <summary>
        /// Returns collection of items on a bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection ExtraItems
        {
            get
            {
                return _ItemContainer.ExtraItemsContainer.SubItems;
            }
        }

        private bool _ToolbarRegistered = false;
        protected override void OnParentChanged(EventArgs e)
        {
            if (_AutoRegister)
                RegisterToolbar();
            base.OnParentChanged(e);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_AutoRegister)
                UnregisterToolbar();
            base.OnHandleDestroyed(e);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (_AutoRegister)
                RegisterToolbar();
            if (_SetExpandedDelayed)
                this.Expanded = _ExpandedDelayed;
        }

        private void RegisterToolbar()
        {
            RegisterToolbar(GetMetroTab());
        }
        /// <summary>
        /// Registers toolbar with MetroTab so it can participate in Quick Access Toolbar operations.
        /// </summary>
        /// <param name="tab">MetroTab</param>
        public void RegisterToolbar(MetroShell tab)
        {
            if (_ToolbarRegistered) return;
            if (tab == null) return;
            tab.RegisterToolbar(this);
            _ToolbarRegistered = true;
        }
        private void UnregisterToolbar()
        {
            UnregisterToolbar(GetMetroTab());
        }
        /// <summary>
        /// Unregisters previously registered toolbar from MetroTab and removes it from Quick Access Toolbar operations.
        /// </summary>
        public void UnregisterToolbar(MetroShell tab)
        {
            if (!_ToolbarRegistered) return;
            if (tab == null) return;
            tab.UnregisterToolbar(this);
            _ToolbarRegistered = false;
        }
        private MetroShell GetMetroTab()
        {
            MetroAppForm form = this.FindForm() as MetroAppForm;
            if (form == null)
            {
                // Walk up the chain perhaps its there
                Control parent = this.Parent;
                while (parent != null)
                {
                    if(parent is MetroShell)
                        return (MetroShell)parent;
                    parent = parent.Parent;
                }
                return null;
            }

            MetroShell tab = form.MetroShell;
            return tab;
        }

        private bool _AutoRegister = true;
        /// <summary>
        /// Gets or sets whether toolbar is attempted to be automatically registered with parent MetroShell control so it can participate in Quick Access Toolbar operations. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(false), Category("Behavior"), Description("Indicates whether toolbar is attempted to be automatically registered with parent MetroTab control so it can participate in Quick Access Toolbar operations")]
        public bool AutoRegister
        {
            get { return _AutoRegister; }
            set
            {
                _AutoRegister = value;
            }
        }
        

        private bool _SetExpandedDelayed = false;
        private bool _ExpandedDelayed = false;
        private bool _Expanded = false;
        /// <summary>
        /// Gets or sets whether control is expanded or not. When control is expanded both main and extra toolbar items are visible. When collapsed
        /// only main items are visible. Default value is false.
        /// </summary>
        [DefaultValue(false), Browsable(true), Category("Layout"), Description("Indicates whether control is expanded or not. When control is expanded both main and extra toolbar items are visible.")]
        public bool Expanded
        {
            get { return _Expanded; }
            set
            {
                if (_Expanded != value)
                {
                    CancelEventArgs cargs = new CancelEventArgs();
                    OnExpandedChanging(cargs);
                    if (cargs.Cancel) return;

                    if (IsUpdateSuspended || !BarFunctions.IsHandleValid(this))
                    {
                        _SetExpandedDelayed = true;
                        _ExpandedDelayed = value;
                        return;
                    }
                    _SetExpandedDelayed = false;
                    _Expanded = value;
                    OnExpandedChanged();
                }
            }
        }

        protected override void RecalcSize()
        {
            if (_ChangingExpanded) return;

            base.RecalcSize();
            int height = GetAutoSizeHeight();
            if (height != this.Height)
                this.Height = height;
        }

        /// <summary>
        /// Returns automatically calculated height of the control given current content.
        /// </summary>
        /// <returns>Height in pixels.</returns>
        public override int GetAutoSizeHeight()
        {
            ElementStyle style = GetBackgroundStyle();
            int styleWhiteSpace = 0;
            if (style != null)
                styleWhiteSpace = ElementStyleLayout.VerticalStyleWhiteSpace(style);

            if (_Expanded)
            {
                //if (_ItemContainer.MainItemsContainer.SubItems.Count == 0 && _ItemContainer.ExtraItemsContainer.SubItems.Count == 0)
                //    return 28;
                return _ItemContainer.HeightInternal;
            }
            else
            {
                //if (_ItemContainer.MainItemsContainer.SubItems.Count == 0)
                //    return 28;
                return _ItemContainer.MainItemsContainer.HeightInternal;
            }
        }

        private eExpandDirection _LastExpandDirection = eExpandDirection.Auto;
        private bool _ChangingExpanded = false;
        private void OnExpandedChanged()
        {
            _ChangingExpanded = true;

            try
            {
                Rectangle startRect = this.Bounds;
                Rectangle endRect;

                ElementStyle style = GetBackgroundStyle();
                int styleWhiteSpace = 0;
                if (style != null)
                    styleWhiteSpace = ElementStyleLayout.VerticalStyleWhiteSpace(style);

                if (_Expanded)
                {
                    eExpandDirection direction = GetExpandDirection();
                    if (direction == eExpandDirection.Bottom)
                        endRect = new Rectangle(this.Left, this.Top, this.Width, _ItemContainer.HeightInternal + styleWhiteSpace);
                    else
                        endRect = new Rectangle(this.Left, this.Top - (_ItemContainer.HeightInternal - _ItemContainer.MainItemsContainer.HeightInternal), this.Width, _ItemContainer.HeightInternal + styleWhiteSpace);
                    _LastExpandDirection = direction;
                }
                else
                {
                    if (_LastExpandDirection == eExpandDirection.Bottom)
                        endRect = new Rectangle(this.Left, this.Top, this.Width, _ItemContainer.MainItemsContainer.HeightInternal + styleWhiteSpace);
                    else
                        endRect = new Rectangle(this.Left, this.Top + (_ItemContainer.HeightInternal - _ItemContainer.MainItemsContainer.HeightInternal), this.Width, _ItemContainer.MainItemsContainer.HeightInternal + styleWhiteSpace);
                }

                if (_AnimationSpeed <= 0 || this.DesignMode)
                {
                    if (this.DesignMode)
                        TypeDescriptor.GetProperties(this)["Bounds"].SetValue(this, endRect);
                    else
                        this.Bounds = endRect;
                }
                else
                    BarFunctions.AnimateControl(this, true, _AnimationSpeed, startRect, endRect);

                OnExpandedChanged(EventArgs.Empty);

                if (_Expanded)
                {
                    if (_AutoCollapse && !this.DesignMode)
                        TrackAutoCollapse();
                }
                else
                {
                    if (_AutoCollapse)
                        StopAutoCollapseTracking();
                }
            }
            finally
            {
                _ChangingExpanded = false;
            }
        }

        private Timer _ActiveWindowTimer = null;
        private IntPtr _ForegroundWindow = IntPtr.Zero;
        private IntPtr _ActiveWindow = IntPtr.Zero;
        private void TrackAutoCollapse()
        {
            if (_ActiveWindowTimer != null)
                return;
            _ActiveWindowTimer = new Timer();
            _ActiveWindowTimer.Interval = 100;
            _ActiveWindowTimer.Tick += new EventHandler(ActiveWindowTimer_Tick);

            _ForegroundWindow = NativeFunctions.GetForegroundWindow();
            _ActiveWindow = NativeFunctions.GetActiveWindow();

            _ActiveWindowTimer.Start();

        }
        private void ActiveWindowTimer_Tick(object sender, EventArgs e)
        {
            if (_ActiveWindowTimer == null)
                return;

            IntPtr f = NativeFunctions.GetForegroundWindow();
            IntPtr a = NativeFunctions.GetActiveWindow();

            if (f != _ForegroundWindow || a != _ActiveWindow)
            {
                if (a != IntPtr.Zero)
                {
                    Control c = Control.FromHandle(a);
                    if (c is PopupContainer || c is PopupContainerControl || c is Balloon)
                        return;
                }
                _ActiveWindowTimer.Stop();
                OnActiveWindowTrackingChanged();
            }
        }
        /// <summary>
        /// Called after change of active window has been detected. SetupActiveWindowTimer must be called to enable detection.
        /// </summary>
        private void OnActiveWindowTrackingChanged()
        {
            if (this.Expanded)
                CollapseToolbar(eEventSource.Code);
            ReleaseActiveWindowTimer();
        }
        private void StopAutoCollapseTracking()
        {
            if (_ActiveWindowTimer != null)
            {
                Timer timer = _ActiveWindowTimer;
                _ActiveWindowTimer = null;
                timer.Stop();
                timer.Tick -= new EventHandler(ActiveWindowTimer_Tick);
                timer.Dispose();
            }
        }
        protected override void OnItemMouseUp(BaseItem item, MouseEventArgs e)
        {
            base.OnItemMouseUp(item, e);

            if (e.Button == MouseButtons.Right && item.CanCustomize)
            {
                MetroShell tab = GetAppMetroTab();
                if (tab != null)
                    tab.ShowCustomizeContextMenu(item, true);
            }
        }
        private MetroShell GetAppMetroTab()
        {
            MetroAppForm form = this.FindForm() as MetroAppForm;
            if (form != null)
            {
                return form.MetroShell;
            }
            return null;
        }

        /// <summary>
        /// Invokes the ItemClick event.
        /// </summary>
        /// <param name="item">Reference to the item that was clicked.</param>
        protected override void OnItemClick(BaseItem item)
        {
            base.OnItemClick(item);

            if (_Expanded && _AutoCollapse && !item.SystemItem)
            {
                if (item is PopupItem && !((PopupItem)item).AutoCollapseOnClick)
                    return;
                CollapseToolbar(eEventSource.Code);
            }
        }
        protected override bool OnSysMouseDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            if (!this.DesignMode && _AutoCollapse && this.Expanded && !this.ClientRectangle.Contains(this.PointToClient(Control.MousePosition)))
                CollapseToolbar(eEventSource.Mouse);
            return base.OnSysMouseDown(hWnd, wParam, lParam);
        }
        private void CollapseToolbar(eEventSource source)
        {
            if (this.Expanded)
                this.Expanded = false;
        }
        private eExpandDirection GetExpandDirection()
        {
            if (_ExpandDirection != eExpandDirection.Auto)
                return _ExpandDirection;
            if (this.Parent == null)
                return eExpandDirection.Bottom;
            Rectangle parentRect = this.Parent.ClientRectangle;
            if (this.Top + _ItemContainer.HeightInternal > parentRect.Bottom)
                return eExpandDirection.Top;
            return eExpandDirection.Bottom;
        }

        private bool _AutoCollapse = true;
        /// <summary>
        /// Gets or sets whether control is automatically collapsed, Expanded property set to False, if control was expanded and any button on the control was clicked or mouse is clicked elsewhere, parent form has lost input focus or some other control gains input focus.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether control is automatically collapsed, Expanded property set to False, if control was expanded and any button on the control was clicked or mouse is clicked elsewhere, parent form has lost input focus or some other control gains input focus.")]
        public bool AutoCollapse
        {
            get { return _AutoCollapse; }
            set
            {
                if (value != _AutoCollapse)
                {
                    bool oldValue = _AutoCollapse;
                    _AutoCollapse = value;
                    OnAutoCollapseChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when AutoCollapse property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnAutoCollapseChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("AutoCollapse"));
        }

        private int _AnimationSpeed = 150;
        /// <summary>
        /// Gets or sets the animation speed duration in milliseconds. Default value is 150 milliseconds. Set to zero, 0 to disable animation.
        /// </summary>
        [DefaultValue(150), Category("Behavior"), Description("Indicates animation speed duration in milliseconds. Default value is 150 milliseconds. Set to zero, 0 to disable animation.")]
        public int AnimationSpeed
        {
            get { return _AnimationSpeed; }
            set
            {
                if (value != _AnimationSpeed)
                {
                    int oldValue = _AnimationSpeed;
                    _AnimationSpeed = value;
                    OnAnimationSpeedChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when AnimationSpeed property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnAnimationSpeedChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("AnimationSpeed"));
        }

        private eExpandDirection _ExpandDirection = eExpandDirection.Auto;
        /// <summary>
        /// Gets or sets the expand direction for the toolbar. Default value is Auto.
        /// </summary>
        [DefaultValue(eExpandDirection.Auto), Category("Behavior"), Description("Indicates expand direction for the toolbar")]
        public eExpandDirection ExpandDirection
        {
            get { return _ExpandDirection; }
            set
            {
                if (value != _ExpandDirection)
                {
                    eExpandDirection oldValue = _ExpandDirection;
                    _ExpandDirection = value;
                    OnExpandDirectionChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when ExpandDirection property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnExpandDirectionChanged(eExpandDirection oldValue, eExpandDirection newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("ExpandDirection"));
        }

        /// <summary>
        /// Gets or sets whether control height is set automatically based on the content. Default value is false.
        /// </summary>
        [Browsable(false), Category("Layout"), Description("Indicates whether control height is set automatically."), DefaultValue(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
#if FRAMEWORK20
        public override bool AutoSize
#else
        public bool AutoSize
#endif
        {
            get { return base.AutoSize; }
            set
            {
                base.AutoSize = value;
            }
        }

        #endregion

        #region Licensing
#if !TRIAL
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        private string _LicenseKey = "";
        [Browsable(false), DefaultValue("")]
        public string LicenseKey
        {
            get { return _LicenseKey; }
            set
            {
                if (NativeFunctions.ValidateLicenseKey(value))
                    return;
                _LicenseKey = (!NativeFunctions.CheckLicenseKey(value) ? "9dsjkhds7" : value);
            }
        }
#endif
        #endregion
    }

    /// <summary>
    /// Defines expand direction behavior for MetroToolbar.
    /// </summary>
    public enum eExpandDirection
    {
        /// <summary>
        /// Expand direction is automatically determined by the position of the control on the form.
        /// </summary>
        Auto,
        /// <summary>
        /// Control is expanded up so bottom of the control is fixed.
        /// </summary>
        Top,
        /// <summary>
        /// Control sis expanded down so top of the control is fixed.
        /// </summary>
        Bottom
    }
}
