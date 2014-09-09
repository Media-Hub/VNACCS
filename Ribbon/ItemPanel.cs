using System;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Collections;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents generic item panel container control.
    /// </summary>
    [ToolboxBitmap(typeof(ItemPanel), "Ribbon.ItemPanel.ico"), ToolboxItem(true), Designer(typeof(DevComponents.DotNetBar.Design.ItemPanelDesigner)), System.Runtime.InteropServices.ComVisible(false)]
    public class ItemPanel : ItemControl, IScrollableItemControl, IBindingSupport
    {
        #region Private Variables
        private ItemContainer m_ItemContainer = null;
        private bool m_EnableDragDrop = false;
        private Point m_MouseDownPoint = Point.Empty;
        private Touch.TouchHandler _TouchHandler = null;
        #endregion

        #region Constructor
        public ItemPanel()
        {
            m_ItemContainer = new ItemContainer();
            m_ItemContainer.GlobalItem = false;
            m_ItemContainer.ContainerControl = this;
            m_ItemContainer.Stretch = false;
            m_ItemContainer.Displayed = true;
            m_ItemContainer.Style = eDotNetBarStyle.Office2007;
            this.ColorScheme.Style = eDotNetBarStyle.Office2007;
            m_ItemContainer.SetOwner(this);
            m_ItemContainer.SetSystemContainer(true);

            this.SetBaseItemContainer(m_ItemContainer);
            m_ItemContainer.Style = eDotNetBarStyle.Office2007;

            this.DragDropSupport = true;

            if (BarFunctions.IsWindows7 && Touch.TouchHandler.IsTouchEnabled)
            {
                _TouchHandler = new DevComponents.DotNetBar.Touch.TouchHandler(this, Touch.eTouchHandlerType.Gesture);
                _TouchHandler.PanBegin += new EventHandler<DevComponents.DotNetBar.Touch.GestureEventArgs>(TouchHandlerPanBegin);
                _TouchHandler.Pan += new EventHandler<DevComponents.DotNetBar.Touch.GestureEventArgs>(TouchHandlerPan);
                _TouchHandler.PanEnd += new EventHandler<DevComponents.DotNetBar.Touch.GestureEventArgs>(TouchHandlerPanEnd);
            }
        }
        #endregion

        #region Internal Implementation
        private eScrollBarAppearance _ScrollBarAppearance = eScrollBarAppearance.Default;
        /// <summary>
        /// Gets or sets the scroll-bar visual style.
        /// </summary>
        [DefaultValue(eScrollBarAppearance.Default), Category("Appearance"), Description("Gets or sets the scroll-bar visual style.")]
        public eScrollBarAppearance ScrollBarAppearance
        {
            get { return _ScrollBarAppearance; }
            set
            {
                _ScrollBarAppearance = value;
                OnScrollBarAppearanceChanged();
            }
        }
        private void OnScrollBarAppearanceChanged()
        {
            if (_VScrollBar != null) _VScrollBar.Appearance = _ScrollBarAppearance;
            if (_HScrollBar != null) _HScrollBar.Appearance = _ScrollBarAppearance;
        }

        /// <summary>
        /// Invalidates non-client area of the control. This method should be used
        /// when you need to invalidate non-client area of the control.
        /// </summary>
        public void InvalidateNonClient()
        {
            if (BarFunctions.IsHandleValid(this))
                WinApi.RedrawWindow(this.Handle, IntPtr.Zero, IntPtr.Zero, WinApi.RedrawWindowFlags.RDW_FRAME | WinApi.RedrawWindowFlags.RDW_INVALIDATE);
        }
        /// <summary>
        /// Returns first checked top-level button item.
        /// </summary>
        /// <returns>An ButtonItem object or null if no button could be found.</returns>
        public ButtonItem GetChecked()
        {
            foreach (BaseItem item in this.Items)
            {
                if (item.Visible && item is ButtonItem && ((ButtonItem)item).Checked)
                    return item as ButtonItem;
            }

            return null;
        }

        /// <summary>
        /// Gets/Sets the visual style for items and color scheme.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), Category("Appearance"), Description("Specifies the visual style of the control."), DefaultValue(eDotNetBarStyle.Office2007)]
        public eDotNetBarStyle Style
        {
            get
            {
                return m_ItemContainer.Style;
            }
            set
            {
                //if(value == eDotNetBarStyle.Office2007)
                //    m_NCPainter.SkinScrollbars = eScrollBarSkin.Optimized;
                //else
                //    m_NCPainter.SkinScrollbars = eScrollBarSkin.None;
                this.ColorScheme.SwitchStyle(value);
                m_ItemContainer.Style = value;
                this.Invalidate();
                this.RecalcLayout();
            }
        }

        /// <summary>
        /// Gets or sets spacing in pixels between items. Default value is 1.
        /// </summary>
        [Browsable(true), DefaultValue(1), Category("Layout"), Description("Indicates spacing in pixels between items.")]
        public virtual int ItemSpacing
        {
            get { return m_ItemContainer.ItemSpacing; }
            set
            {
                m_ItemContainer.ItemSpacing = value;
            }
        }

        /// <summary>
        /// Gets or sets default layout orientation inside the control. You can have multiple layouts inside of the control by adding
        /// one or more instances of the ItemContainer object and chaning it's LayoutOrientation property.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), Category("Layout"), DefaultValue(eOrientation.Horizontal), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public virtual eOrientation LayoutOrientation
        {
            get { return m_ItemContainer.LayoutOrientation; }
            set
            {
                m_ItemContainer.LayoutOrientation = value;
                if (this.DesignMode)
                    this.RecalcLayout();
            }
        }

        /// <summary>
        /// Gets or sets whether items contained by container are resized to fit the container bounds. When container is in horizontal
        /// layout mode then all items will have the same height. When container is in vertical layout mode then all items
        /// will have the same width. Default value is true.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), DefaultValue(true), Category("Layout")]
        public virtual bool ResizeItemsToFit
        {
            get { return m_ItemContainer.ResizeItemsToFit; }
            set
            {
                m_ItemContainer.ResizeItemsToFit = value;
            }
        }

        /// <summary>
        /// Gets or sets whether ButtonItem buttons when in vertical layout are fit into the available width so any text inside of them
        /// is wrapped if needed. Default value is false.
        /// </summary>
        [DefaultValue(false), Category("Layout"), Description("Indicates whether ButtonItem buttons when in vertical layout are fit into the available width so any text inside of them is wrapped if needed.")]
        public bool FitButtonsToContainerWidth
        {
            get { return m_ItemContainer.FitOversizeItemIntoAvailableWidth; }
            set
            {
                m_ItemContainer.FitOversizeItemIntoAvailableWidth = value;
                if (this.DesignMode)
                    RecalcLayout();
            }
        }


        /// <summary>
        /// Gets or sets the item alignment when container is in horizontal layout. Default value is Left.
        /// </summary>
        [Browsable(true), DefaultValue(eHorizontalItemsAlignment.Left), Category("Layout"), Description("Indicates item alignment when container is in horizontal layout."), DevCoBrowsable(true)]
        public eHorizontalItemsAlignment HorizontalItemAlignment
        {
            get { return m_ItemContainer.HorizontalItemAlignment; }
            set
            {
                m_ItemContainer.HorizontalItemAlignment = value;
            }
        }

        /// <summary>
        /// Gets or sets whether items in horizontal layout are wrapped into the new line when they cannot fit allotted container size. Default value is false.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Layout"), Description("Indicates whether items in horizontal layout are wrapped into the new line when they cannot fit allotted container size.")]
        public virtual bool MultiLine
        {
            get { return m_ItemContainer.MultiLine; }
            set
            {
                m_ItemContainer.MultiLine = value;
            }
        }

        /// <summary>
        /// Returns collection of items on a bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection Items
        {
            get
            {
                return m_ItemContainer.SubItems;
            }
        }

        /// <summary>
        /// Returns collection of items on a bar for binding support.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        IList IBindingSupport.ItemsCollection
        {
            get
            {
                return this.Items;
            }
        }

        /// <summary>
        /// Scrolls the control so that item is displayed within the visible bounds of the control.
        /// </summary>
        /// <param name="item">Item to ensure visibility for. Item must belong to this control.</param>
        public void EnsureVisible(BaseItem item)
        {
            if (item.ContainerControl != this)
                return;

            Rectangle r = item.DisplayRectangle;
            Rectangle bounds = this.ClientRectangle;
            if (bounds.Width < m_ItemContainer.WidthInternal)
                bounds.Width = m_ItemContainer.WidthInternal;
            if (r.Y < bounds.Y || r.Bottom > bounds.Height)
            {
                if (this.AutoScroll)
                {
                    Point p = Point.Empty;
                    if (r.Y < 0)
                        p = new Point(this.AutoScrollPosition.X, Math.Abs(this.AutoScrollPosition.Y - r.Y) - 2);
                    else
                        p = new Point(this.AutoScrollPosition.X,
                            (r.Bottom + SystemInformation.HorizontalScrollBarHeight + 2) - (this.AutoScrollPosition.Y + this.ClientRectangle.Height));
                    this.InvalidateLayout();
                    this.AutoScrollPosition = p;
                    this.RecalcLayout();
                }
            }
            //m_NCPainter.PaintNonClientAreaBuffered();
        }

        protected override Rectangle GetPaintClipRectangle()
        {
            Rectangle r = this.ClientRectangle;

            if (this.BackgroundStyle == null)
                return r;

            r.X += ElementStyleLayout.LeftWhiteSpace(this.BackgroundStyle);
            r.Width -= ElementStyleLayout.HorizontalStyleWhiteSpace(this.BackgroundStyle);
            r.Y += ElementStyleLayout.TopWhiteSpace(this.BackgroundStyle);
            r.Height -= ElementStyleLayout.VerticalStyleWhiteSpace(this.BackgroundStyle);
            if (_VScrollBar != null) r.Width -= _VScrollBar.Width;
            if (_HScrollBar != null) r.Height -= _HScrollBar.Height;

            return r;
        }

        protected override Rectangle GetItemContainerRectangle()
        {
            Rectangle r = base.GetItemContainerRectangle();
            if (this.AutoScroll && this.AutoScrollPosition.Y != 0)
                r.Y += this.AutoScrollPosition.Y;
            if (this.AutoScroll && this.AutoScrollPosition.X != 0)
                r.X += this.AutoScrollPosition.X;

            if (_VScrollBar != null)
                r.Width -= _VScrollBar.Width;
            if (_HScrollBar != null)
                r.Height -= _HScrollBar.Height;

            return r;
        }

        private void ApplyScrollChange()
        {
            if (!this.AutoScroll) return;

            if (m_ItemContainer.NeedRecalcSize)
            {
                this.RecalcSize();
                return;
            }

            Rectangle r = base.GetItemContainerRectangle();
            if (this.AutoScrollPosition.Y != 0)
                r.Y += this.AutoScrollPosition.Y;
            if (this.AutoScrollPosition.X != 0)
                r.X += this.AutoScrollPosition.X;
            r.Height = m_ItemContainer.HeightInternal;
            r.Width = m_ItemContainer.WidthInternal;
            m_ItemContainer.Bounds = r;
        }
        private bool _MouseClientScrollEnabled = true;
        /// <summary>
        /// Indicates whether control can be scrolled when client area is dragged using mouse. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether control can be scrolled when client area is dragged using mouse.")]
        public bool MouseClientScrollEnabled
        {
            get { return _MouseClientScrollEnabled; }
            set
            {
                _MouseClientScrollEnabled = value;
            }
        }
        private bool _MouseScrollDrag = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (!this.Focused && this.IsSelectable)
                this.Select();

            m_MouseDownPoint.X = e.X;
            m_MouseDownPoint.Y = e.Y;

            if (e.Button == MouseButtons.Left && _TouchHandler == null && _MouseClientScrollEnabled)
            {
                Rectangle inner = ElementStyleLayout.GetInnerRect(this.BackgroundStyle, this.ClientRectangle);
                if (_AutoScrollMinSize.Width > inner.Width || _AutoScrollMinSize.Height > inner.Height)
                {
                    BaseItem item = HitTest(e.X, e.Y);
                    if (item == null || item.IsContainer)
                    {
                        _MouseScrollDrag = true;
                        _TouchInnerBounds = ElementStyleLayout.GetInnerRect(this.BackgroundStyle, this.ClientRectangle);
                        _TouchStartLocation = e.Location;
                        _TouchStartScrollPosition = _AutoScrollPosition;
                    }
                }
            }

            base.OnMouseDown(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_MouseScrollDrag)
            {
                _MouseScrollDrag = false;
                if (_AutoScrollMinSize.Width > _TouchInnerBounds.Width)
                {
                    if (_AutoScrollMinSize.Width - _TouchInnerBounds.Width < -_AutoScrollPosition.X)
                        this.AutoScrollPosition = new Point(_AutoScrollMinSize.Width - _TouchInnerBounds.Width, 0);
                    else if (-_AutoScrollPosition.X < 0)
                        this.AutoScrollPosition = new Point(0, 0);
                }
                else if (_AutoScrollMinSize.Height > _TouchInnerBounds.Height)
                {
                    if (_AutoScrollMinSize.Height - _TouchInnerBounds.Height < -_AutoScrollPosition.Y)
                        this.AutoScrollPosition = new Point(0, _AutoScrollMinSize.Height - _TouchInnerBounds.Height);
                    else if (-_AutoScrollPosition.Y < 0)
                        this.AutoScrollPosition = new Point(0, 0);
                }
                ApplyScrollChange();
                return;
            }
            base.OnMouseUp(e);
        }
        private bool IsSelectable
        {
            get
            {
                return this.GetStyle(ControlStyles.Selectable);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_MouseScrollDrag)
            {
                if (_AutoScrollMinSize.Width > _TouchInnerBounds.Width)
                {
                    int offset = (e.Location.X - _TouchStartLocation.X);
                    int offsetChange = offset + _TouchStartScrollPosition.X;

                    if (Math.Abs(offsetChange) + MaximumReversePageOffset > _AutoScrollMinSize.Width - _TouchInnerBounds.Width)
                    {
                        _AutoScrollPosition.X = -(_AutoScrollMinSize.Width + MaximumReversePageOffset - _TouchInnerBounds.Width);
                    }
                    else if (offsetChange > MaximumReversePageOffset)
                    {
                        _AutoScrollPosition.X = MaximumReversePageOffset;
                    }
                    else
                        _AutoScrollPosition.X = offsetChange;
                    this.Invalidate();
                    ApplyScrollChange();
                    Update();
                }
                else if (_AutoScrollMinSize.Height > _TouchInnerBounds.Height)
                {
                    int offset = (e.Location.Y - _TouchStartLocation.Y);
                    int offsetChange = offset + _TouchStartScrollPosition.Y;

                    if (Math.Abs(offsetChange) + MaximumReversePageOffset > _AutoScrollMinSize.Height - _TouchInnerBounds.Height)
                    {
                        _AutoScrollPosition.Y = -(_AutoScrollMinSize.Height + MaximumReversePageOffset - _TouchInnerBounds.Height);
                    }
                    else if (offsetChange > MaximumReversePageOffset)
                    {
                        _AutoScrollPosition.Y = MaximumReversePageOffset;
                    }
                    else
                        _AutoScrollPosition.Y = offsetChange;
                    this.Invalidate();
                    ApplyScrollChange();
                    Update();
                }

                if (_VScrollBar != null && _VScrollBar.Value != -_AutoScrollPosition.Y)
                    _VScrollBar.Value = Math.Min(_VScrollBar.Maximum, Math.Max(0, -_AutoScrollPosition.Y));
                if (_HScrollBar != null && _HScrollBar.Value != -_AutoScrollPosition.X)
                    _HScrollBar.Value = Math.Min(_HScrollBar.Maximum, Math.Max(0, -_AutoScrollPosition.X));
                return;
            }

            base.OnMouseMove(e);

            if (m_EnableDragDrop && !this.DragInProgress && (e.Button == MouseButtons.Left || e.Button == System.Windows.Forms.MouseButtons.Right && _TouchHandler!=null))
            {
                if (Math.Abs(e.X - m_MouseDownPoint.X) > SystemInformation.DragSize.Width ||
                    Math.Abs(e.Y - m_MouseDownPoint.Y) > SystemInformation.DragSize.Height)
                {
                    BaseItem item = HitTest(e.X, e.Y);
                    if (item != null && item.CanCustomize)
                        ((IOwner)this).StartItemDrag(item);
                }
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (this.Focused)
            {
                KeyEventArgs e = new KeyEventArgs(keyData);
                m_ItemContainer.InternalKeyDown(e);
                if (e.Handled)
                    return true;
            }
            return base.ProcessDialogKey(keyData);
        }

        protected override void OnMouseWheel(MouseEventArgs e)
        {
            if (_VScrollBar != null && _VScrollBar.Visible)
            {
                int newValue = _VScrollBar.Value + _VScrollBar.SmallChange * (e.Delta < 0 ? 1 : -1);
                if (newValue < _VScrollBar.Minimum) newValue = _VScrollBar.Minimum;
                if (newValue > _VScrollBar.Maximum - _VScrollBar.LargeChange + 1)
                    newValue = _VScrollBar.Maximum - _VScrollBar.LargeChange + 1;
                _VScrollBar.DoScroll(newValue, e.Delta < 0 ? ScrollEventType.SmallIncrement : ScrollEventType.SmallDecrement);

            }
            base.OnMouseWheel(e);
        }

        private int _EntryCount = 0;
        protected override void RecalcSize()
        {
            _EntryCount++;
            try
            {
                m_ItemContainer.MinimumSize = new Size(this.GetItemContainerRectangle().Width, 0);
                base.RecalcSize();

                if (!this.AutoSize && this.AutoScroll)
                {
                    int containerHeight = m_ItemContainer.HeightInternal + ElementStyleLayout.TopWhiteSpace(this.BackgroundStyle, eSpacePart.Padding | eSpacePart.Margin)+
                        ElementStyleLayout.BottomWhiteSpace(this.BackgroundStyle, eSpacePart.Padding | eSpacePart.Margin);
                    int containerWidth = m_ItemContainer.WidthInternal + ElementStyleLayout.LeftWhiteSpace(this.BackgroundStyle, eSpacePart.Margin | eSpacePart.Padding) +
                        ElementStyleLayout.RightWhiteSpace(this.BackgroundStyle, eSpacePart.Padding | eSpacePart.Margin);
                    if (containerHeight > this.ClientRectangle.Height || containerWidth > this.ClientRectangle.Width)
                    {
                        Size areaSize = Size.Empty;
                        int size = this.ClientRectangle.Height;
                        if (containerWidth > this.ClientRectangle.Width)
                            size -= SystemInformation.HorizontalScrollBarHeight;
                        if (containerHeight > size)
                            areaSize.Height = containerHeight;

                        size = this.ClientRectangle.Width;
                        if (containerHeight > this.ClientRectangle.Height)
                            size -= SystemInformation.VerticalScrollBarWidth;
                        if (containerWidth > size)
                            areaSize.Width = containerWidth + SystemInformation.VerticalScrollBarWidth;

                        if (this.BackgroundStyle != null)
                        {
                            areaSize.Width += ElementStyleLayout.HorizontalStyleWhiteSpace(this.BackgroundStyle);
                            areaSize.Height += ElementStyleLayout.VerticalStyleWhiteSpace(this.BackgroundStyle);
                            if (areaSize.Width > this.ClientRectangle.Width)
                                areaSize.Height += SystemInformation.HorizontalScrollBarHeight + 1;
                        }

                        bool verticalScrollBarChange = _VScrollBar == null;
                        bool horizontalScrollBarChange = _HScrollBar == null;
                        if (this.AutoScrollMinSize != areaSize)
                        {
                            this.Invalidate();
                            this.AutoScrollMinSize = areaSize;
                        }
                        verticalScrollBarChange ^= (_VScrollBar == null);
                        horizontalScrollBarChange ^= (_HScrollBar == null);
                        if (_EntryCount < 4 && (verticalScrollBarChange || horizontalScrollBarChange))
                        {
                            RecalcSize();
                        }
                    }
                    else if (!this.AutoScrollMinSize.IsEmpty)
                    {
                        this.AutoScrollMinSize = Size.Empty;
                    }
                }
            }
            finally
            {
                _EntryCount--;
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            UpdateScrollBars();
            RepositionScrollBars();
            //m_NCPainter.PaintNonClientAreaBuffered();
        }

        protected override void OnLayout(LayoutEventArgs e)
        {
            base.OnLayout(e);
            RepositionScrollBars();
        }
        #endregion

        #region Scrolling Support
        private DevComponents.DotNetBar.VScrollBarAdv _VScrollBar = null;
        private DevComponents.DotNetBar.ScrollBar.HScrollBarAdv _HScrollBar = null;
        private Control _Thumb = null;
        /// <summary>
        /// Gets the reference to internal vertical scroll-bar control if one is created or null if no scrollbar is visible.
        /// </summary>
        [Browsable(false)]
        public DevComponents.DotNetBar.VScrollBarAdv VScrollBar
        {
            get
            {
                return _VScrollBar;
            }
        }

        /// <summary>
        /// Gets the reference to internal horizontal scroll-bar control if one is created or null if no scrollbar is visible.
        /// </summary>
        [Browsable(false)]
        public DevComponents.DotNetBar.ScrollBar.HScrollBarAdv HScrollBar
        {
            get
            {
                return _HScrollBar;
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMargin
        {
            get { return base.AutoScrollMargin; }
            set { base.AutoScrollMargin = value; }
        }

        private bool _AutoScroll = false;
        /// <summary>
        /// Gets or sets a value indicating whether the control enables the user to scroll to items placed outside of its visible boundaries.
        /// </summary>
        [Browsable(true), DefaultValue(false)]
        public new virtual bool AutoScroll
        {
            get { return _AutoScroll; }
            set
            {
                if (_AutoScroll != value)
                {
                    _AutoScroll = value;
                    RecalcLayout();
                    UpdateScrollBars();
                }
            }
        }

        private Size _AutoScrollMinSize = Size.Empty;
        /// <summary>
        /// Gets or sets the minimum size of the auto-scroll. Returns a Size that represents the minimum height and width of the scrolling area in pixels.
        /// This property is managed internally by control and should not be modified.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize
        {
            get { return _AutoScrollMinSize; }
            set
            {
                _AutoScrollMinSize = value;
                UpdateScrollBars();
            }
        }

        private Point _AutoScrollPosition = Point.Empty;
        /// <summary>
        /// Gets or sets the location of the auto-scroll position.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), Description("Indicates location of the auto-scroll position.")]
        public new Point AutoScrollPosition
        {
            get
            {
                return _AutoScrollPosition;
            }
            set
            {
                if (value.X > 0) value.X = -value.X;
                if (value.Y > 0) value.Y = -value.Y;
                if (_AutoScrollPosition != value)
                {
                    _AutoScrollPosition = value;
                    if (_AutoScroll)
                    {
                        if (_VScrollBar != null && _VScrollBar.Value != -_AutoScrollPosition.Y)
                            _VScrollBar.Value = Math.Min(_VScrollBar.Maximum, -_AutoScrollPosition.Y);
                        if (_HScrollBar != null && _HScrollBar.Value != -_AutoScrollPosition.X)
                            _HScrollBar.Value = Math.Min(_HScrollBar.Maximum, -_AutoScrollPosition.X);
                        //RepositionHostedControls(false);
                        Invalidate();
                    }
                }
            }
        }

        private void UpdateScrollBars()
        {
            if (!_AutoScroll)
            {
                RemoveHScrollBar();
                RemoveVScrollBar();
                if (_Thumb != null)
                {
                    this.Controls.Remove(_Thumb);
                    _Thumb.Dispose();
                    _Thumb = null;
                }
                return;
            }

            Rectangle innerBounds = ElementStyleLayout.GetInnerRect(this.BackgroundStyle, this.ClientRectangle);
            // Check do we need vertical scrollbar
            Size scrollSize = _AutoScrollMinSize;
            if (scrollSize.Height > innerBounds.Height)
            {
                if (_VScrollBar == null)
                {
                    _VScrollBar = new DevComponents.DotNetBar.VScrollBarAdv();
                    _VScrollBar.Appearance = _ScrollBarAppearance;
                    _VScrollBar.Width = SystemInformation.VerticalScrollBarWidth;
                    this.Controls.Add(_VScrollBar);
                    _VScrollBar.BringToFront();
                    _VScrollBar.Scroll += new ScrollEventHandler(VScrollBarScroll);
                }
                if (_VScrollBar.Minimum != 0)
                    _VScrollBar.Minimum = 0;
                if (_VScrollBar.LargeChange != innerBounds.Height && innerBounds.Height > 0)
                    _VScrollBar.LargeChange = innerBounds.Height;
                _VScrollBar.SmallChange = 22;
                if (_VScrollBar.Maximum != _AutoScrollMinSize.Height)
                    _VScrollBar.Maximum = _AutoScrollMinSize.Height;
                if (_VScrollBar.Value != -_AutoScrollPosition.Y)
                    _VScrollBar.Value = (Math.Min(_VScrollBar.Maximum, Math.Abs(_AutoScrollPosition.Y)));
            }
            else
                RemoveVScrollBar();

            // Check horizontal scrollbar
            if (scrollSize.Width > innerBounds.Width)
            {
                if (_HScrollBar == null)
                {
                    _HScrollBar = new DevComponents.DotNetBar.ScrollBar.HScrollBarAdv();
                    _HScrollBar.Appearance = _ScrollBarAppearance;
                    _HScrollBar.Height = SystemInformation.HorizontalScrollBarHeight;
                    this.Controls.Add(_HScrollBar);
                    _HScrollBar.BringToFront();
                    _HScrollBar.Scroll += new ScrollEventHandler(HScrollBarScroll);
                }
                if (_HScrollBar.Minimum != 0)
                    _HScrollBar.Minimum = 0;
                if (_HScrollBar.LargeChange != innerBounds.Width && innerBounds.Width > 0)
                    _HScrollBar.LargeChange = innerBounds.Width;
                if (_HScrollBar.Maximum != _AutoScrollMinSize.Width)
                    _HScrollBar.Maximum = _AutoScrollMinSize.Width;
                if (_HScrollBar.Value != -_AutoScrollPosition.X)
                    _HScrollBar.Value = (Math.Min(_HScrollBar.Maximum, Math.Abs(_AutoScrollPosition.X)));
                _HScrollBar.SmallChange = 22;
            }
            else
                RemoveHScrollBar();
            RepositionScrollBars();
        }

        private void VScrollBarScroll(object sender, ScrollEventArgs e)
        {
            _AutoScrollPosition.Y = -e.NewValue;
            ApplyScrollChange();
            this.Invalidate();
#if FRAMEWORK20
            OnScroll(e);
#endif
        }
        private void HScrollBarScroll(object sender, ScrollEventArgs e)
        {
            _AutoScrollPosition.X = -e.NewValue;
            ApplyScrollChange();
            this.Invalidate();
        }

        private bool _RepositioningScrollbars = false;
        private void RepositionScrollBars()
        {
            if (_RepositioningScrollbars) return;

            _RepositioningScrollbars = true;
            try
            {
                Rectangle innerBounds = ElementStyleLayout.GetInnerRect(this.BackgroundStyle, this.ClientRectangle);
                if (_HScrollBar != null)
                {
                    int width = innerBounds.Width;
                    if (_VScrollBar != null)
                        width -= _VScrollBar.Width;
                    _HScrollBar.Bounds = new Rectangle(innerBounds.X, innerBounds.Bottom - _HScrollBar.Height + 1, width, _HScrollBar.Height);
                }

                if (_VScrollBar != null)
                {
                    int height = innerBounds.Height;
                    if (_HScrollBar != null)
                        height -= _HScrollBar.Height;
                    _VScrollBar.Bounds = new Rectangle(innerBounds.Right - _VScrollBar.Width + 1, innerBounds.Y, _VScrollBar.Width, height);
                }

                if (_VScrollBar != null && _HScrollBar != null)
                {
                    if (_Thumb == null)
                    {
                        _Thumb = new Control();
                        if (this.BackColor.A == 255)
                            _Thumb.BackColor = this.BackColor;
                        else
                            _Thumb.BackColor = Color.White;
                        if (!this.BackgroundStyle.BackColor.IsEmpty && this.BackgroundStyle.BackColor.A == 255)
                            _Thumb.BackColor = this.BackgroundStyle.BackColor;
                        this.Controls.Add(_Thumb);
                    }
                    _Thumb.Bounds = new Rectangle(_HScrollBar.Bounds.Right, _VScrollBar.Bounds.Bottom, _VScrollBar.Width, _HScrollBar.Height);
                    _Thumb.BringToFront();
                }
                else if (_Thumb != null)
                {
                    Control thumb = _Thumb;
                    _Thumb = null;
                    this.Controls.Remove(thumb);
                    thumb.Dispose();
                }
            }
            finally
            {
                _RepositioningScrollbars = false;
            }
        }

        private void RemoveHScrollBar()
        {
            if (_HScrollBar != null)
            {
                Rectangle r = _HScrollBar.Bounds;
                this.Controls.Remove(_HScrollBar);
                _HScrollBar.Dispose();
                _HScrollBar = null;
                this.Invalidate(r);
                _AutoScrollPosition.X = 0;
                if (m_ItemContainer != null)
                    m_ItemContainer.NeedRecalcSize = true;
            }
        }

        private void RemoveVScrollBar()
        {
            if (_VScrollBar != null)
            {
                Rectangle r = _VScrollBar.Bounds;
                this.Controls.Remove(_VScrollBar);
                _VScrollBar.Dispose();
                _VScrollBar = null;
                this.Invalidate(r);
                _AutoScrollPosition.Y = 0;
                if (m_ItemContainer != null)
                    m_ItemContainer.NeedRecalcSize = true;
            }
        }

        private bool _SuspendPaint = false;
        /// <summary>
        /// Gets or sets whether all painting in control is suspended.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool SuspendPaint
        {
            get { return _SuspendPaint; }
            set
            {
                _SuspendPaint = value;
            }
        }

        #endregion

        #region Licensing
#if !TRIAL
        protected override void OnPaint(PaintEventArgs e)
        {
            if (_SuspendPaint) return;
            base.OnPaint(e);
           
        }

        private string m_LicenseKey = "";
        [Browsable(false), DefaultValue("")]
        public string LicenseKey
        {
            get { return m_LicenseKey; }
            set
            {
                if (NativeFunctions.ValidateLicenseKey(value))
                    return;
                m_LicenseKey = (!NativeFunctions.CheckLicenseKey(value) ? "9dsjkhds7" : value);
            }
        }
#else
        protected override void OnPaint(PaintEventArgs e)
        {
            if (NativeFunctions.ColorExpAlt() || !NativeFunctions.CheckedThrough)
		    {
			    e.Graphics.Clear(SystemColors.Control);
                return;
            }
            if (_SuspendPaint) return;
            base.OnPaint(e);
        }
#endif
        #endregion

        #region Binding and Templating Support
        private BaseItem _ItemTemplate = null;
        /// <summary>
        /// Gets or sets the item template that is repeated for each data-row when using data binding.
        /// </summary>
        [Browsable(true), DefaultValue(null), Editor("DevComponents.DotNetBar.Design.ItemSelectorTypeEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public BaseItem ItemTemplate
        {
            get { return _ItemTemplate; }
            set
            {
                _ItemTemplate = value; OnItemTemplateChanged();
            }
        }

        private void OnItemTemplateChanged()
        {
            if (_ItemTemplate != null) _ItemTemplate.GlobalItem = false;
            if (_BindingGenerator != null)
                _BindingGenerator.VisualTemplate = GetItemTemplate();
        }
        private BaseItem GetItemTemplate()
        {
            if (_ItemTemplate == null)
                return GetDefaultItemTemplate();
            return _ItemTemplate;
        }

        /// <summary>
        /// Gets the collection of the binding applied to ItemTemplate visual when using data-binding.
        /// </summary>
        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public List<BindingDef> ItemTemplateBindings
        {
            get
            {
                EnsureBindingGenerator();
                return _BindingGenerator.Bindings;
            }
        }

        private BaseItem GetDefaultItemTemplate()
        {
            ButtonItem button = new ButtonItem();
            button.Shape = new RoundRectangleShapeDescriptor();
            button.OptionGroup = "items";
            button.GlobalItem = false;
            button.ColorTable = eButtonColor.Flat;
            return button;
        }

        /// <summary>
        /// Adds new item to the ItemPanel based on specified ItemTemplate and sets its Text property.
        /// </summary>
        /// <param name="text">Text to assign to the item.</param>
        /// <returns>reference to newly created item</returns>
        public BaseItem AddItem(string text)
        {
            BaseItem template = GetItemTemplate();
            if (template == null)
                throw new NullReferenceException("ItemTemplate property not set.");
            BaseItem item = template.Copy();
            item.Text = text;
            this.Items.Add(item);

            return item;
        }

#if (FRAMEWORK20)
        /// <summary>
        /// Gets the list of ButtonItem or CheckBoxItem controls that have their Checked property set to true.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public System.Collections.Generic.List<BaseItem> SelectedItems
        {
            get
            {
                System.Collections.Generic.List<BaseItem> items = new System.Collections.Generic.List<BaseItem>();
                SubItemsCollection itemsCollection = this.Items;
                GetSelectedItems(items, itemsCollection);
                return items;
            }
        }

        private static void GetSelectedItems(System.Collections.Generic.List<BaseItem> items, SubItemsCollection itemsCollection)
        {
            foreach (BaseItem item in itemsCollection)
            {
                if (item is ItemContainer)
                {
                    GetSelectedItems(items, item.SubItems);
                    continue;
                }
                ButtonItem button = item as ButtonItem;
                if (button != null && button.Checked)
                    items.Add(button);
                else
                {
                    CheckBoxItem cb = item as CheckBoxItem;
                    if (cb != null && cb.Checked)
                        items.Add(cb);
                    else if (item is DevComponents.DotNetBar.Metro.MetroTileItem && ((DevComponents.DotNetBar.Metro.MetroTileItem)item).Checked)
                        items.Add(item);

                }
            }
        }
        /// <summary>
        /// Gets or sets ButtonItem or CheckBoxItem item that have their Checked property set to true.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public BaseItem SelectedItem
        {
            get
            {
                int i = this.SelectedIndex;
                if (i == -1) return null;
                return this.Items[i];
            }
            set
            {
                SetItemSelection(this.SelectedItem, false);
                SetItemSelection(value, true);
                if (value != null)
                    _SelectedIndex = this.Items.IndexOf(value);
                else
                    _SelectedIndex = -1;
            }
        }
        private bool SetItemSelection(BaseItem item, bool isSelected)
        {
            if (item == null) return false;
            bool isSet = true;
            if (item is CheckBoxItem)
                ((CheckBoxItem)item).Checked = isSelected;
            else if (item is ButtonItem)
                ((ButtonItem)item).Checked = isSelected;
            else if (item is DevComponents.DotNetBar.Metro.MetroTileItem)
                ((DevComponents.DotNetBar.Metro.MetroTileItem)item).Checked = isSelected;
            else
                isSet = false;

            return isSet;
        }
#endif
        #endregion

        #region IScrollableItemControl Members
        /// <summary>
        /// Indicates that item has been selected via keyboard.
        /// </summary>
        /// <param name="item">Reference to item being selected</param>
        void IScrollableItemControl.KeyboardItemSelected(BaseItem item)
        {
            if (item != null)
                EnsureVisible(item);
        }

        #endregion

        #region Misc Properties
        /// <summary>
        /// Gets or sets whether external ButtonItem object is accepted in drag and drop operation. UseNativeDragDrop must be set to true in order for this property to be effective.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Behavior"), Description("Gets or sets whether external ButtonItem object is accepted in drag and drop operation.")]
        public override bool AllowExternalDrop
        {
            get { return base.AllowExternalDrop; }
            set { base.AllowExternalDrop = value; }
        }

        /// <summary>
        /// Gets or sets whether native .NET Drag and Drop is used by control to perform drag and drop operations. AllowDrop must be set to true to allow drop of the items on control.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Behavior"), Description("Specifies whether native .NET Drag and Drop is used by control to perform drag and drop operations.")]
        public override bool UseNativeDragDrop
        {
            get { return base.UseNativeDragDrop; }
            set { base.UseNativeDragDrop = value; }
        }

        /// <summary>
        /// Gets or sets whether automatic drag &amp; drop support is enabled. Default value is false.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Behavior"), Description("Specifies whether automatic drag & drop support is enabled.")]
        public virtual bool EnableDragDrop
        {
            get { return m_EnableDragDrop; }
            set { m_EnableDragDrop = value; }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(200, 200);
            }
        }
        #endregion

        #region Data-binding
        private ItemVisualGenerator _BindingGenerator = null;
        /// <summary>
        /// Gets or sets the data source for the ComboTree. Expected is an object that implements the IList or IListSource interfaces, 
        /// such as a DataSet or an Array. The default is null.
        /// </summary>
        [AttributeProvider(typeof(IListSource)), Description("Indicates data source for the ComboTree."), Category("Data"), DefaultValue(null), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource
        {
            get
            {
                if (_BindingGenerator == null) return null;
                return _BindingGenerator.DataSource;
            }
            set
            {
                if (value == null && _BindingGenerator == null) return;
                EnsureBindingGenerator();
                if (_BindingGenerator.DataSource != value)
                {
                    _BindingGenerator.DataSource = value;
                    OnDataSourceChanged(EventArgs.Empty);
                }
            }
        }

        private void EnsureBindingGenerator()
        {
            if (_BindingGenerator == null)
            {
                _BindingGenerator = new ItemVisualGenerator(this);
                _BindingGenerator.VisualTemplate = GetItemTemplate();
            }
        }
        /// <summary>
        /// Occurs when the DataSource changes.
        /// </summary>
        [Description("Occurs when the DataSource changes.")]
        public event EventHandler DataSourceChanged;
        /// <summary>
        /// Raises the DataSourceChanged event.
        /// </summary>
        /// <param name="e">An EventArgs that contains the event data. </param>
        protected virtual void OnDataSourceChanged(EventArgs e)
        {
            EventHandler handler = DataSourceChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }


        /// <summary>
        /// When overridden in a derived class, resynchronizes the item data with the contents of the data source.
        /// </summary>
        public virtual void RefreshItems()
        {
            if (_BindingGenerator != null)
            {
                _BindingGenerator.RefreshItems();
            }
        }

        void IBindingSupport.InvokeDataNodeCreated(DataItemVisualEventArgs e)
        {
            OnDataNodeCreated(e);
        }

        /// <summary>
        /// Occurs when a item for an data-bound object item has been created and provides you with opportunity to modify the created item.
        /// </summary>
        [Description("Occurs when a item for an data-bound object item has been created and provides you with opportunity to modify the created item")]
        public event DataItemVisualEventHandler DataItemVisualCreated;
        /// <summary>
        /// Raises the DataNodeCreated event.
        /// </summary>
        /// <param name="dataNodeEventArgs">Provides event arguments.</param>
        protected virtual void OnDataNodeCreated(DataItemVisualEventArgs dataNodeEventArgs)
        {
            if (DataItemVisualCreated != null) DataItemVisualCreated(this, dataNodeEventArgs);
        }

        private bool _AllowSelection = true;
        bool IBindingSupport.AllowSelection
        {
            get { return _AllowSelection; }
            set { _AllowSelection = value; }
        }


        private CurrencyManager _DataManager = null;
        protected CurrencyManager DataManager
        {
            get
            {
                return _DataManager;
            }
        }

        private int _SelectedIndex = -1;
        /// <summary>
        /// Gets or sets the index specifying the currently selected item.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Description("Gets or sets the index specifying the currently selected item.")]
        public int SelectedIndex
        {
            get
            {
                return _SelectedIndex;
            }
            set
            {
                if (value != _SelectedIndex)
                {
                    if (value == -1)
                    {
                        _SelectedIndex = -1;
                        SetItemSelection(this.SelectedItem, false);
                    }
                    else
                    {
                        BaseItem item = null;
                        if (_SelectedIndex >= this.Items.Count)
                        {
                            _SelectedIndex = -1;
                            return;
                        }
                        if (_SelectedIndex > -1)
                            item = this.Items[_SelectedIndex];
                        SetItemSelection(this.SelectedItem, false);
                        SetItemSelection(item, true);
                        _SelectedIndex = value;
                    }
                    OnSelectedIndexChanged(EventArgs.Empty);
                }
            }
        }

        protected override void OnButtonCheckedChanged(ButtonItem item, EventArgs e)
        {
            if (item.Checked)
            {
                _SelectedIndex = this.Items.IndexOf(item);
                OnSelectedIndexChanged(e);
            }
            base.OnButtonCheckedChanged(item, e);
        }

        /// <summary>
        /// Occurs when value of SelectedIndex property has changed.
        /// </summary>
        [Description("Occurs when value of SelectedIndex property has changed.")]
        public event EventHandler SelectedIndexChanged;
        /// <summary>
        /// Raises the SelectedIndexChanged event.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnSelectedIndexChanged(EventArgs e)
        {
            EventHandler handler = SelectedIndexChanged;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        #endregion

        #region Touch Handling
        private bool _TouchEnabled = true;
        /// <summary>
        /// Indicates whether touch support for scrolling is enabled.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether touch support for scrolling is enabled.")]
        public bool TouchEnabled
        {
            get { return _TouchEnabled; }
            set
            {
                if (value != _TouchEnabled)
                {
                    bool oldValue = _TouchEnabled;
                    _TouchEnabled = value;
                    OnTouchEnabledChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TouchEnabled property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTouchEnabledChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TouchEnabled"));
        }
        private int TriggerPageChangeOffset
        {
            get
            {
                return 32;
            }
        }
        private int MaximumReversePageOffset
        {
            get
            {
                return Math.Min(32, this.Width / 6);
            }
        }
        private bool _TouchDrag = false;
        private Point _TouchStartLocation = Point.Empty;
        private Point _TouchStartScrollPosition = Point.Empty;
        private Rectangle _TouchInnerBounds = Rectangle.Empty;
        private void TouchHandlerPanBegin(object sender, DevComponents.DotNetBar.Touch.GestureEventArgs e)
        {
            if (_TouchEnabled)
            {
                _TouchInnerBounds = ElementStyleLayout.GetInnerRect(this.BackgroundStyle, this.ClientRectangle);
                _TouchStartLocation = e.Location;
                _TouchStartScrollPosition = _AutoScrollPosition;
                _TouchDrag = true;
                e.Handled = true;
            }
        }

        private void TouchHandlerPanEnd(object sender, DevComponents.DotNetBar.Touch.GestureEventArgs e)
        {
            if (_TouchDrag)
            {
                EndTouchPan();
                e.Handled = true;
            }
        }

        private void EndTouchPan()
        {
            _TouchDrag = false;
            if (m_ItemContainer.LayoutOrientation == eOrientation.Horizontal)
            {
                if (_AutoScrollMinSize.Width - _TouchInnerBounds.Width < -_AutoScrollPosition.X)
                    this.AutoScrollPosition = new Point(_AutoScrollMinSize.Width - _TouchInnerBounds.Width, 0);
                else if (-_AutoScrollPosition.X < 0)
                    this.AutoScrollPosition = new Point(0, 0);
            }
            else
            {
                if (_AutoScrollMinSize.Height - _TouchInnerBounds.Height < -_AutoScrollPosition.Y)
                    this.AutoScrollPosition = new Point(0, _AutoScrollMinSize.Height - _TouchInnerBounds.Height);
                else if (-_AutoScrollPosition.Y < 0)
                    this.AutoScrollPosition = new Point(0, 0);
            }
            ApplyScrollChange();
            
        }

        private void TouchHandlerPan(object sender, DevComponents.DotNetBar.Touch.GestureEventArgs e)
        {
            if (_TouchDrag)
            {
                if (m_ItemContainer.LayoutOrientation == eOrientation.Horizontal)
                {
                    int offset = (e.Location.X - _TouchStartLocation.X);
                    int offsetChange = offset + _TouchStartScrollPosition.X;

                    bool overflow = false;
                    if (Math.Abs(offsetChange) + MaximumReversePageOffset > _AutoScrollMinSize.Width - _TouchInnerBounds.Width)
                    {
                        _AutoScrollPosition.X = -(_AutoScrollMinSize.Width + MaximumReversePageOffset - _TouchInnerBounds.Width);
                        overflow = true;
                    }
                    else if (offsetChange > MaximumReversePageOffset)
                    {
                        _AutoScrollPosition.X = MaximumReversePageOffset;
                        overflow = true;
                    }
                    else
                        _AutoScrollPosition.X = offsetChange;
                    this.Invalidate();
                    ApplyScrollChange();
                    Update();
                    if (overflow && e.IsInertia) EndTouchPan();

                }
                else
                {
                    int offset = (e.Location.Y - _TouchStartLocation.Y);
                    int offsetChange = offset + _TouchStartScrollPosition.Y;

                    bool overflow = false;
                    if (Math.Abs(offsetChange) + MaximumReversePageOffset > _AutoScrollMinSize.Height - _TouchInnerBounds.Height)
                    {
                        _AutoScrollPosition.Y = -(_AutoScrollMinSize.Height + MaximumReversePageOffset - _TouchInnerBounds.Height);
                        overflow = true;
                    }
                    else if (offsetChange > MaximumReversePageOffset)
                    {
                        _AutoScrollPosition.Y = MaximumReversePageOffset;
                        overflow = true;
                    }
                    else
                        _AutoScrollPosition.Y = offsetChange;
                    this.Invalidate();
                    ApplyScrollChange();
                    Update();
                    if (overflow && e.IsInertia) EndTouchPan();
                }

                if (_VScrollBar != null && _VScrollBar.Value != -_AutoScrollPosition.Y)
                    _VScrollBar.Value = Math.Min(_VScrollBar.Maximum, Math.Max(0,-_AutoScrollPosition.Y));
                if (_HScrollBar != null && _HScrollBar.Value != -_AutoScrollPosition.X)
                    _HScrollBar.Value = Math.Min(_HScrollBar.Maximum, Math.Max(0, -_AutoScrollPosition.X));
                e.Handled = true;
            }
        }
        #endregion
    }

    /// <summary>
    /// Provides information about the binding for the item.
    /// </summary>
    public class ItemBindingData
    {
        /// <summary>
        /// Initializes a new instance of the ItemBindingInfo class.
        /// </summary>
        /// <param name="dataItem"></param>
        /// <param name="bindingIndex"></param>
        public ItemBindingData(object dataItem, int bindingIndex)
        {
            DataItem = dataItem;
            _BindingIndex = bindingIndex;
        }

        private WeakReference _DataItem;
        /// <summary>
        /// Gets or sets the data row item is bound to.
        /// </summary>
        public object DataItem
        {
            get
            {
                if (_DataItem == null) return null;
                if (_DataItem.IsAlive)
                    return _DataItem.Target;
                return null;
            }
            set
            {
                if (value != null)
                    _DataItem = new WeakReference(value);
                else
                    _DataItem = null;
            }
        }

        private int _BindingIndex;
        /// <summary>
        /// Gets or sets the data item index.
        /// </summary>
        public int BindingIndex
        {
            get { return _BindingIndex; }
            set { _BindingIndex = value; }
        }

        private object _Tag;
        /// <summary>
        /// Gets or sets the additional data connected to this binding information.
        /// </summary>
        public object Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
    }
}
