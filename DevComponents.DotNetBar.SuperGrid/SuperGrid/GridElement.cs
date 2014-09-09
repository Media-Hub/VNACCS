using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents the base grid item.
    /// </summary>
    public abstract class GridElement : INotifyPropertyChanged
    {
        #region Events

        /// <summary>
        /// Occurs when display/rendering of the item is invalidated.
        /// </summary>
        public event EventHandler RenderInvalid;

        /// <summary>
        /// Occurs when layout of the item is invalidated.
        /// </summary>
        public event EventHandler LayoutInvalid;

        /// <summary>
        /// Occurs when parent of the item has changed.
        /// </summary>
        public event EventHandler ParentChanged;

        #endregion

        #region Static variables

        static Point _mouseDownPoint;

        #endregion

        #region Private variables

        private Rectangle _BoundsRelative = Rectangle.Empty;

        private bool _IsLayoutValid;
        private bool _IsMouseOver;

        private bool _AllowSelection = true;
        private bool _Visible = true;
        private bool _NeedsMeasured = true;

        private GridElement _Parent;
        private SuperGridControl _SuperGrid;
        private GridPanel _GridPanel;

        private object _Tag;

        #endregion

        #region Abstract methods

        /// <summary>
        /// Performs the arrange pass layout of the item when
        /// the final position and size of the item has been set.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds"></param>
        protected abstract void ArrangeOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Rectangle layoutBounds);

        /// <summary>
        /// Performs the layout of the item
        /// and sets the Size property to size that item will take.
        /// </summary>
        /// <param name="layoutInfo">Layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="constraintSize"></param>
        protected abstract void MeasureOverride(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize);

        /// <summary>
        /// Performs drawing of the item and its children.
        /// </summary>
        /// <param name="renderInfo">Holds contextual rendering information.</param>
        protected abstract void RenderOverride(GridRenderInfo renderInfo);

        #endregion

        #region Internal properties

        #region Capture

        internal virtual bool Capture
        {
            get { return (CapturedItem != null); }

            set
            {
                if (SuperGrid != null)
                    CapturedItem = (value == true) ? this : null;
            }
        }

        #endregion

        #region CapturedItem

        internal GridElement CapturedItem
        {
            get
            {
                if (SuperGrid != null)
                    return (SuperGrid.CapturedItem);

                return (null);
            }

            set
            {
                if (SuperGrid != null)
                    SuperGrid.CapturedItem = value;
            }
        }

        #endregion

        #region CViewRect

        /// <summary>
        /// Client View Rectangle
        /// </summary>
        internal Rectangle CViewRect
        {
            get
            {
                if (SuperGrid != null)
                    return (SuperGrid.CViewRect);

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region HScrollBounds

        /// <summary>
        /// Horizontal Scroll Bounds
        /// </summary>
        internal Rectangle HScrollBounds
        {
            get
            {
                Rectangle r = BoundsRelative;
                r.X -= HScrollOffset;

                return (r);
            }
        }

        #endregion

        #region HScrollOffset

        internal int HScrollOffset
        {
            get
            {
                SuperGridControl sgc = SuperGrid;

                return (sgc != null)
                    ? sgc.HScrollOffset : 0;
            }
        }

        #endregion

        #region IsMouseDown

        /// <summary>
        /// Gets whether mouse is down
        /// </summary>
        internal virtual bool IsMouseDown
        {
            get { return (Control.MouseButtons != MouseButtons.None); }
        }

        #endregion

        #region IsVFrozen

        ///<summary>
        /// IsFrozen
        ///</summary>
        internal bool IsVFrozen
        {
            get
            {
                if (SuperGrid != null)
                {
                    GridPanel panel = SuperGrid.PrimaryGrid;

                    return (BoundsRelative.Y < panel.FixedRowHeight);
                }

                return (false);
            }
        }

        #endregion

        #region MouseDownPoint

        /// <summary>
        /// Gets the mouse down Point
        /// </summary>
        internal virtual Point MouseDownPoint
        {
            get { return (_mouseDownPoint); }
            set { _mouseDownPoint = value; }
        }

        #endregion

        #region NeedsMeasured

        /// <summary>
        /// Get or sets whether item needs measured
        /// </summary>
        internal virtual bool NeedsMeasured
        {
            get { return _NeedsMeasured; }
            set { _NeedsMeasured = value; }
        }

        #endregion

        #region ScrollBounds

        /// <summary>
        /// Horizontal and Vertical Scroll Bounds
        /// </summary>
        internal Rectangle ScrollBounds
        {
            get
            {
                Rectangle r = BoundsRelative;
                r.X -= HScrollOffset;
                r.Y -= VScrollOffset;

                return (r);
            }
        }

        #endregion

        #region SViewRect

        /// <summary>
        /// Scrollable View Rectangle
        /// </summary>
        internal Rectangle SViewRect
        {
            get
            {
                if (SuperGrid != null)
                    return (SuperGrid.SViewRect);

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region ViewRect

        /// <summary>
        /// View Rectangle
        /// </summary>
        internal Rectangle ViewRect
        {
            get
            {
                if (SuperGrid != null)
                    return (SuperGrid.ViewRect);

                return (Rectangle.Empty);
            }
        }

        #endregion

        #region VScrollBounds

        /// <summary>
        /// Vertical Scroll Bounds
        /// </summary>
        internal Rectangle VScrollBounds
        {
            get
            {
                Rectangle r = BoundsRelative;
                r.Y -= VScrollOffset;

                return (r);
            }
        }

        #endregion

        #region VScrollOffset

        internal int VScrollOffset
        {
            get
            {
                SuperGridControl cgc = SuperGrid;

                return (cgc != null)
                    ? cgc.VScrollOffset : 0;
            }
        }

        #endregion

        #endregion

        #region Public properties

        #region AllowSelection

        /// <summary>
        /// Gets or sets whether the element can be selected
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether the element can be selected")]
        public virtual bool AllowSelection
        {
            get { return (_AllowSelection); }

            set
            {
                if (_AllowSelection != value)
                {
                    _AllowSelection = value;

                    OnPropertyChangedEx("AllowSelection", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region Bounds

        ///<summary>
        /// Gets the scroll adjusted bounds
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Rectangle Bounds
        {
            get { return (BoundsRelative); }
        }

        #endregion

        #region BoundsRelative

        /// <summary>
        /// Gets or sets the relative bounds of the item.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual Rectangle BoundsRelative
        {
            get { return (_BoundsRelative); }
            internal set { _BoundsRelative = value; }
        }

        #endregion

        #region GridPanel

        /// <summary>
        /// Gets the parent GridPanel
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridPanel GridPanel
        {
            get { return (GetParentGridPanel()); }
            internal set { _GridPanel = value; }
        }

        #region GetParentGridPanel

        private GridPanel GetParentGridPanel()
        {
            if (_GridPanel != null)
                return (_GridPanel);

            GridElement parent = this;

            while (parent != null)
            {
                if (parent is GridPanel)
                    return ((GridPanel)parent);

                parent = parent.Parent;
            }

            return (null);
        }

        #endregion

        #endregion

        #region IsLayoutValid

        /// <summary>
        /// Gets whether layout for the item is valid.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsLayoutValid
        {
            get { return (_IsLayoutValid); }
            internal set { _IsLayoutValid = value; }
        }

        #endregion

        #region IsMouseOver

        /// <summary>
        /// Gets whether mouse is over the element.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual bool IsMouseOver
        {
            get { return (_IsMouseOver); }
            internal set { _IsMouseOver = value; }
        }

        #endregion

        #region LocationRelative

        /// <summary>
        /// Gets or sets the relative location of the item.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Point LocationRelative
        {
            get { return _BoundsRelative.Location; }
            internal set { _BoundsRelative.Location = value; }
        }

        #endregion

        #region Parent

        /// <summary>
        /// Gets or sets the parent of the item.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public virtual GridElement Parent
        {
            get { return (_Parent); }

            internal set
            {
                if (_Parent != value)
                {
                    GridElement oldParent = value;
                    _Parent = value;

                    OnParentChanged(oldParent, value);
                }
            }
        }

        #endregion

        #region Size

        /// <summary>
        /// Gets or sets the Size of the item.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Size Size
        {
            get { return (_BoundsRelative.Size); }
            internal set { _BoundsRelative.Size = value; }
        }

        #endregion

        #region SuperGrid

        /// <summary>
        /// Gets the parent super grid control.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public SuperGridControl SuperGrid
        {
            get { return GetParentSuperGrid(); }
            internal set { _SuperGrid = value; }
        }

        #region GetParentSuperGrid

        private SuperGridControl GetParentSuperGrid()
        {
            if (_SuperGrid != null)
                return (_SuperGrid);

            GridElement parent = _Parent;

            while (parent != null)
            {
                if (parent.SuperGrid != null)
                    return (_SuperGrid = parent.SuperGrid);

                parent = parent.Parent;
            }

            return (null);
        }

        #endregion

        #endregion

        #region Tag

        ///<summary>
        /// Gets or sets user-defined data associated with the object
        ///</summary>
        [DefaultValue(null)]
        [Description("User-defined data associated with the object")]
        [TypeConverter(typeof(StringConverter))]
        public object Tag
        {
            get { return (_Tag); }
            set { _Tag = value; }
        }

        #endregion

        #region Visible

        /// <summary>
        /// Get or sets whether the item is visible
        /// </summary>
        [DefaultValue(true), Category("Appearance")]
        [Description("Indicates whether item is visible")]
        public virtual bool Visible
        {
            get { return (_Visible); }

            set
            {
                if (_Visible != value)
                {
                    _Visible = value;

                    OnPropertyChangedEx("Visible", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #endregion

        #region OnEvent processing

        #region OnLayoutInvalid

        /// <summary>
        /// Raises LayoutInvalid event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnLayoutInvalid(EventArgs e)
        {
            EventHandler handler = LayoutInvalid;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region OnParentChanged

        /// <summary>
        /// Called after parent of the item has changed.
        /// </summary>
        /// <param name="oldParent">Reference to old parent.</param>
        /// <param name="newParent">Reference to new parent.</param>
        protected virtual void OnParentChanged(GridElement oldParent, GridElement newParent)
        {
            OnParentChanged(EventArgs.Empty);
        }

        /// <summary>
        /// Raises ParentChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnParentChanged(EventArgs e)
        {
            EventHandler handler = ParentChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region OnRenderInvalid

        /// <summary>
        /// Raises RenderInvalid event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnRenderInvalid(EventArgs e)
        {
            EventHandler handler = RenderInvalid;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #endregion

        #region Measure

        /// <summary>
        /// This method is used by the items internally to invoke the measure pass to
        /// get item size. Override MeasureOverride method to perform actual measuring.
        /// </summary>
        /// <param name="layoutInfo">Holds contextual layout information.</param>
        /// <param name="stateInfo"></param>
        /// <param name="constraintSize"></param>
        internal void Measure(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Size constraintSize)
        {
            MeasureOverride(layoutInfo, stateInfo, constraintSize);

            NeedsMeasured = false;
        }

        #endregion

        #region Arrange

        /// <summary>
        /// This method is used by the items internally to invoke the arrange pass after
        /// location and size of the item has been set. Override ArrangeOverride method
        /// to perform internal arranging.
        /// </summary>
        /// <param name="layoutInfo"></param>
        /// <param name="stateInfo"></param>
        /// <param name="layoutBounds"></param>
        internal void Arrange(
            GridLayoutInfo layoutInfo, GridLayoutStateInfo stateInfo, Rectangle layoutBounds)
        {
            IsLayoutValid = true;

            _BoundsRelative = layoutBounds;

            ArrangeOverride(layoutInfo, stateInfo, layoutBounds);
        }

        #endregion

        #region Render

        /// <summary>
        /// This method is used by the items internally to invoke the rendering
        /// for the item. Override RenderOverride method to perform actual rendering.
        /// </summary>
        /// <param name="renderInfo">Holds contextual rendering information.</param>
        internal void Render(GridRenderInfo renderInfo)
        {
            RenderOverride(renderInfo);
        }

        #endregion

        #region InvalidateLayout

        /// <summary>
        /// Invalidates the layout for the item.
        /// </summary>
        public virtual void InvalidateLayout()
        {
            _IsLayoutValid = false;

            if (_Parent != null)
            {
                _Parent.InvalidateLayout();
            }
            else
            {
                if (SuperGrid != null)
                    SuperGrid.Invalidate();
            }

            OnLayoutInvalid(EventArgs.Empty);
        }

        #endregion

        #region InvalidateRender

        /// <summary>
        /// Invalidates the display state of the item
        /// </summary>
        public virtual void InvalidateRender()
        {
            SuperGridControl grid = SuperGrid;

            if (grid != null)
            {
                if (grid.InvokeRequired)
                    grid.BeginInvoke(new MethodInvoker(delegate { grid.InvalidateRender(this); }));
                else
                    grid.InvalidateRender(this);
            }

            OnRenderInvalid(EventArgs.Empty);
        }

        /// <summary>
        /// Invalidates the display state of the item
        /// </summary>
        public virtual void InvalidateRender(Rectangle bounds)
        {
            SuperGridControl grid = SuperGrid;

            if (grid != null)
                grid.InvalidateRender(bounds);

            OnRenderInvalid(EventArgs.Empty);
        }

        #endregion

        #region Mouse Handling

        #region OnMouseEnter

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseEnter(EventArgs e)
        {
            IsMouseOver = true;

            OnMouseEnter(e);
        }

        /// <summary>
        /// Called when mouse enter the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseEnter(EventArgs e)
        {
        }

        #endregion

        #region OnMouseLeave

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseLeave(EventArgs e)
        {
            IsMouseOver = false;

            OnMouseLeave(e);
        }

        /// <summary>
        /// Called when mouse leaves the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseLeave(EventArgs e)
        {
        }

        #endregion

        #region OnMouseHover

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseHover(EventArgs e)
        {
            OnMouseHover(e);
        }

        /// <summary>
        /// Called when mouse hovers over the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseHover(EventArgs e)
        {
        }

        #endregion

        #region OnMouseDown

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseDown(MouseEventArgs e)
        {
            MouseDownPoint = e.Location;

            OnMouseDown(e);
        }

        /// <summary>
        /// Called when mouse button is pressed over the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseDown(MouseEventArgs e)
        {
        }

        #endregion

        #region OnMouseUp

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseUp(MouseEventArgs e)
        {
            Capture = false;

            OnMouseUp(e);
        }

        /// <summary>
        /// Called when mouse button is released over the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseUp(MouseEventArgs e)
        {
        }

        #endregion

        #region OnMouseMove

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseMove(MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        /// <summary>
        /// Called when mouse is moved over the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseMove(MouseEventArgs e)
        {
        }

        #endregion

        #region OnMouseClick

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseClick(MouseEventArgs e)
        {
            OnMouseClick(e);
        }
        /// <summary>
        /// Called when mouse is clicked on the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseClick(MouseEventArgs e)
        {
        }

        #endregion

        #region OnMouseDoubleClick

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="e"></param>
        internal virtual void InternalMouseDoubleClick(MouseEventArgs e)
        {
            OnMouseDoubleClick(e);
        }

        /// <summary>
        /// Called when mouse is double clicked on the element.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnMouseDoubleClick(MouseEventArgs e)
        {
        }

        #endregion

        #region PostInternalMouseMove

        internal void PostInternalMouseMove()
        {
            SuperGrid.PostInternalMouseMove();
        }

        #endregion

        #endregion

        #region Keyboard handling

        /// <summary>
        /// Called by top-level control to pass message into the grid
        /// element. To handle it override corresponding On - virtual method.
        /// </summary>
        /// <param name="keyData"></param>
        internal virtual void InternalKeyDown(Keys keyData)
        {
            OnKeyDown(keyData);
        }

        /// <summary>
        /// Called when mouse button is pressed over the element.
        /// </summary>
        /// <param name="keyData"></param>
        protected virtual void OnKeyDown(Keys keyData)
        {
        }

        #endregion

        #region EnsureVisible

        ///<summary>
        /// EnsureVisible
        ///</summary>
        public void EnsureVisible()
        {
            EnsureVisible(false);
        }

        ///<summary>
        /// EnsureVisible
        ///</summary>
        ///<param name="center"></param>
        public virtual void EnsureVisible(bool center)
        {
        }

        #endregion

        #region CancelCapture

        ///<summary>
        /// Cancels any inprogress operations that may
        /// have the mouse captured (resize, reorder).
        ///</summary>
        public virtual void CancelCapture()
        {
            if (CapturedItem == this)
                Capture = false;
        }

        #endregion

        #region IsDesignerHosted

        internal bool IsDesignerHosted
        {
            get
            {
                if (SuperGrid != null)
                    return (SuperGrid.IsDesignerHosted);

                return (false);
            }
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eh = PropertyChanged;

            if (eh != null)
                eh(this, e);
        }

        /// <summary>
        /// Default PropertyChanged processing
        /// </summary>
        /// <param name="s"></param>
        protected void OnPropertyChangedEx(string s)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(s));
        }

        /// <summary>
        /// Default PropertyChanged processing
        /// </summary>
        /// <param name="s"></param>
        /// <param name="changeType">invalidate</param>
        protected void OnPropertyChangedEx(string s, VisualChangeType changeType)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(s));

            if (changeType == VisualChangeType.Layout)
                InvalidateLayout();
            else
                InvalidateRender();
        }

        #endregion
    }
}
