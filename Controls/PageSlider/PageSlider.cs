using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using DevComponents.DotNetBar.ScrollBar;

namespace DevComponents.DotNetBar.Controls
{
    [ToolboxItem(true), Description("Page Slider Control with Touch Support")]
    [Designer(typeof(DevComponents.DotNetBar.Design.PageSliderDesigner))]
    [DefaultEvent("SelectedPageChanged"), DefaultProperty("SelectedPageIndex")]
    [ToolboxBitmap(typeof(StepIndicator), "Controls.PageSlider.ico")]
    public class PageSlider : Control
    {
        #region Constructor
        private StepIndicator _StepIndicator;
        private Touch.TouchHandler _TouchHandler = null;
        private HScrollBarAdv _HorizontalScrollBar = null;
        private VScrollBarAdv _VerticalScrollBar = null;
        /// <summary>
        /// Initializes a new instance of the PageSlider class.
        /// </summary>
        public PageSlider()
        {
            _StepIndicator = new StepIndicator();
            _StepIndicator.Dock = DockStyle.Top;
            this.Controls.Add(_StepIndicator);
            if (BarFunctions.IsWindows7 && Touch.TouchHandler.IsTouchEnabled)
            {
                _TouchHandler = new DevComponents.DotNetBar.Touch.TouchHandler(this);
                _TouchHandler.PanBegin += new EventHandler<DevComponents.DotNetBar.Touch.GestureEventArgs>(TouchHandlerPanBegin);
                _TouchHandler.Pan += new EventHandler<DevComponents.DotNetBar.Touch.GestureEventArgs>(TouchHandlerPan);
                _TouchHandler.PanEnd += new EventHandler<DevComponents.DotNetBar.Touch.GestureEventArgs>(TouchHandlerPanEnd);
            }
            _HorizontalScrollBar = new HScrollBarAdv();
            _HorizontalScrollBar.Dock = DockStyle.Bottom;
            _HorizontalScrollBar.Height = 12;
            _HorizontalScrollBar.Scroll += new ScrollEventHandler(ScrollBarScroll);
            _HorizontalScrollBar.Visible = false;
            this.Controls.Add(_HorizontalScrollBar);

            _VerticalScrollBar = new VScrollBarAdv();
            _VerticalScrollBar.Dock = DockStyle.Right;
            _VerticalScrollBar.Width = 12;
            _VerticalScrollBar.Scroll += new ScrollEventHandler(ScrollBarScroll);
            _VerticalScrollBar.Visible = false;
            this.Controls.Add(_VerticalScrollBar);
        }

        protected override void Dispose(bool disposing)
        {
            StopScrollBarTimer();
            base.Dispose(disposing);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when selected page has changed.
        /// </summary>
        [Description("Occurs when selected page has changed.")]
        public event EventHandler SelectedPageChanged;
        /// <summary>
        /// Raises SelectedPageChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnSelectedPageChanged(EventArgs e)
        {
            EventHandler handler = SelectedPageChanged;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Implementation
        protected override Size DefaultSize
        {
            get
            {
                return new Size(300, 200);
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateBoundsForAllPages(false);
            base.OnHandleCreated(e);
        }
        protected override void OnHandleDestroyed(EventArgs e)
        {
            StopScrollBarTimer();
            base.OnHandleDestroyed(e);
        }

        private bool _IndicatorVisible = true;
        /// <summary>
        /// Gets or sets whether current page indicator is visible. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether current page indicator is visible.")]
        public bool IndicatorVisible
        {
            get { return _IndicatorVisible; }
            set
            {
                if (value != _IndicatorVisible)
                {
                    bool oldValue = _IndicatorVisible;
                    _IndicatorVisible = value;
                    OnIndicatorVisibleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when IndicatorVisible property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnIndicatorVisibleChanged(bool oldValue, bool newValue)
        {
            _StepIndicator.Visible = newValue;
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is PageSliderPage)
            {
                PageSliderPage page = (PageSliderPage)e.Control;
                UpdatePageTable();
                page.Bounds = GetPageBounds(page);
                UpdateScrollBar();
            }
            base.OnControlAdded(e);
        }
        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (e.Control is PageSliderPage)
            {
                PageSliderPage page = (PageSliderPage)e.Control;
                int pageIndex = _PageTable.IndexOf(page);
                _PageTable.Remove(page);
                UpdatePageTable();
                if (_SelectedPage == page)
                {
                    if (_PageTable.Count > 0)
                        this.SelectedPage = _PageTable[pageIndex > 0 ? pageIndex-- : 0];
                    else
                        this.SelectedPage = null;
                }
                UpdateScrollBar();
                UpdateBoundsForAllPages(false, true);
            }
            base.OnControlRemoved(e);
        }
        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
            if (levent.AffectedProperty == "ChildIndex")
            {
                UpdatePageTable();
                UpdateBoundsForAllPages(true, true);
            }
        }
        private void UpdateBoundsForAllPages(bool updateControl)
        {
            UpdateBoundsForAllPages(updateControl, false);
        }
        private void UpdateBoundsForAllPages(bool updateControl, bool syncPageIndicator)
        {
            Rectangle selectedPageBounds = GetSelectedPageBounds();
            int currentPage = -1;
            for (int i = 0; i < _PageTable.Count; i++)
            {

                PageSliderPage page = _PageTable[i];
                page.Bounds = GetPageBounds(page);
                if (updateControl && page.Bounds.IntersectsWith(this.ClientRectangle))
                    this.Update();
                if (syncPageIndicator && !_PageBoundsOffset.IsEmpty && page.Bounds.IntersectsWith(selectedPageBounds) && currentPage == -1)
                    currentPage = i + 1;
            }
            if (syncPageIndicator && _PageBoundsOffset.IsEmpty)
                currentPage = this.SelectedPageIndex + 1;
            if (syncPageIndicator && currentPage > 0)
                _StepIndicator.CurrentStep = currentPage;
            UpdateScrollBar();
        }
        private void UpdateScrollBar()
        {
            int selectedPageIndex = this.SelectedPageIndex;
            Rectangle selectedPageBounds = GetSelectedPageBounds();
            Rectangle firstPageBounds = Rectangle.Empty, lastPageBounds = Rectangle.Empty;
            if (_PageTable.Count > 0)
            {
                firstPageBounds = _PageTable[0].Bounds;
                lastPageBounds = _PageTable[_PageTable.Count - 1].Bounds;
            }

            if (_Orientation == eOrientation.Horizontal)
            {
                _HorizontalScrollBar.Minimum = 0;
                _HorizontalScrollBar.Maximum = Math.Max(1, lastPageBounds.Right - firstPageBounds.X + (selectedPageIndex == -1 ? firstPageBounds.X : 0));
                _HorizontalScrollBar.LargeChange = Math.Max(1, selectedPageBounds.Width);
                _HorizontalScrollBar.SmallChange = 32;
                if (!_IsScrolling && selectedPageIndex != -1)
                    _HorizontalScrollBar.Value = Math.Min(Math.Max(_HorizontalScrollBar.Minimum, Math.Abs(firstPageBounds.X)), _HorizontalScrollBar.Maximum);
            }
            else
            {
                _VerticalScrollBar.Minimum = 0;
                _VerticalScrollBar.Maximum = Math.Max(1, lastPageBounds.Bottom - firstPageBounds.Y + (selectedPageIndex == -1 ? firstPageBounds.Y : 0));
                _VerticalScrollBar.LargeChange = Math.Max(1, selectedPageBounds.Height);
                _VerticalScrollBar.SmallChange = 32;
                if (!_IsScrolling && selectedPageIndex != -1)
                    _VerticalScrollBar.Value = Math.Min(Math.Max(_VerticalScrollBar.Minimum, Math.Abs(firstPageBounds.Y)), _VerticalScrollBar.Maximum);
            }
        }

        private bool _IsScrolling = false;
        private int _InitialScrollValue = 0;
        private void ScrollBarScroll(object sender, ScrollEventArgs e)
        {
            if (e.Type == ScrollEventType.EndScroll)
            {
                _IsScrolling = false;
                Rectangle selectedPageBounds = GetSelectedPageBounds();
                PageSliderPage newSelection = this.SelectedPage;
                // Find right page to select since control can be scrolled a lot using scroll-bars
                if (e.NewValue > _InitialScrollValue)
                {
                    for (int i = _PageTable.Count - 1; i >= 0; i--)
                    {
                        PageSliderPage page = _PageTable[i];
                        if (page.Bounds.IntersectsWith(selectedPageBounds) && selectedPageBounds.Right - page.Left >= TriggerPageChangeOffset)
                        {
                            newSelection = page;
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < _PageTable.Count; i++)
                    {
                        PageSliderPage page = _PageTable[i];
                        if (page.Bounds.IntersectsWith(selectedPageBounds) && page.Right - selectedPageBounds.X >= TriggerPageChangeOffset)
                        {
                            newSelection = page;
                            break;
                        }
                    }
                }

                if (this.SelectedPage != newSelection)
                {
                    this.SelectedPage = newSelection;
                    if (_Orientation == eOrientation.Horizontal)
                        _HorizontalScrollBar.Value = Math.Abs(_PageTable[0].Left);
                    else
                        _VerticalScrollBar.Value = Math.Abs(_PageTable[0].Top);
                }
                else
                    PageBoundsOffset = Point.Empty;
            }
            else
            {
                if (!_IsScrolling)
                {
                    if (_Orientation == eOrientation.Horizontal)
                        _InitialScrollValue = _HorizontalScrollBar.Value;
                    else
                        _InitialScrollValue = _VerticalScrollBar.Value;
                    _IsScrolling = true;
                }
                if (e.ScrollOrientation == ScrollOrientation.HorizontalScroll)
                    PageBoundsOffset = new Point(_InitialScrollValue - e.NewValue, 0);
                else
                    PageBoundsOffset = new Point(0, _InitialScrollValue - e.NewValue);
            }
        }
        protected override void OnResize(EventArgs e)
        {
            UpdateBoundsForAllPages(false);
            base.OnResize(e);
        }

        private Rectangle GetSelectedPageBounds()
        {
            Rectangle selectedPageBounds = DeflateRectangle(this.ClientRectangle, _PagePadding);
            if (_Orientation == eOrientation.Horizontal)
                selectedPageBounds.Width -= _PageSpacing + _NextPageVisibleMargin;
            else
                selectedPageBounds.Height -= _PageSpacing + _NextPageVisibleMargin;

            if (_IndicatorVisible)
            {
                if (_StepIndicator.Dock == DockStyle.Top)
                {
                    selectedPageBounds.Y += _StepIndicator.Height;
                    selectedPageBounds.Height -= _StepIndicator.Height;
                }
                else if (_StepIndicator.Dock == DockStyle.Bottom)
                {
                    selectedPageBounds.Height -= _StepIndicator.Height;
                }
                else if (_StepIndicator.Dock == DockStyle.Left)
                {
                    selectedPageBounds.X += _StepIndicator.Width;
                    selectedPageBounds.Width -= _StepIndicator.Width;
                }
                else if (_StepIndicator.Dock == DockStyle.Right)
                {
                    selectedPageBounds.Width -= _StepIndicator.Width;
                }
            }
            if (_ScrollBarVisibility == eScrollBarVisibility.AlwaysVisible)
            {
                if (_Orientation == eOrientation.Horizontal)
                    selectedPageBounds.Height -= _HorizontalScrollBar.Height;
                else
                    selectedPageBounds.Width -= _HorizontalScrollBar.Width;

            }
            return selectedPageBounds;
        }

        private Point PageBoundsOffset
        {
            get { return _PageBoundsOffset; }
            set
            {
                if (_PageBoundsOffset != value)
                {
                    _PageBoundsOffset = value;
                    UpdateBoundsForAllPages(true, true);
                }
            }
        }
        private Point _PageBoundsOffset = Point.Empty;
        private System.Drawing.Rectangle GetPageBounds(PageSliderPage page)
        {
            int pageIndex = _PageTable.IndexOf(page);
            int selectedPageIndex = this.SelectedPageIndex;
            Rectangle bounds = Rectangle.Empty;

            Rectangle selectedPageBounds = GetSelectedPageBounds();

            if (pageIndex == selectedPageIndex)
            {
                bounds = selectedPageBounds;
            }
            else if (pageIndex > selectedPageIndex)
            {
                bounds = selectedPageBounds;
                if (_Orientation == eOrientation.Horizontal)
                    bounds.X += (_PageSpacing + bounds.Width) * (pageIndex - selectedPageIndex);
                else
                    bounds.Y += (_PageSpacing + bounds.Height) * (pageIndex - selectedPageIndex);
            }
            else
            {
                bounds = selectedPageBounds;
                if (_Orientation == eOrientation.Horizontal)
                    bounds.X -= (_PageSpacing + bounds.Width) * (selectedPageIndex - pageIndex);
                else
                    bounds.Y -= (_PageSpacing + bounds.Height) * (selectedPageIndex - pageIndex);
            }

            bounds.Offset(_PageBoundsOffset);

            return bounds;
        }

        private Rectangle DeflateRectangle(Rectangle rect, Padding padding)
        {
            rect.X += padding.Left;
            rect.Y += padding.Top;
            rect.Width -= padding.Horizontal;
            rect.Height -= padding.Vertical;
            return rect;
        }

        private List<PageSliderPage> _PageTable = new List<PageSliderPage>();
        private void UpdatePageTable()
        {
            _PageTable.Clear();
            foreach (Control control in this.Controls)
            {
                PageSliderPage page = control as PageSliderPage;
                if (page == null) continue;
                bool appendPage = true;
                for (int i = 0; i < _PageTable.Count; i++)
                {
                    if (_PageTable[i].PageNumber > page.PageNumber)
                    {
                        _PageTable.Insert(i, page);
                        appendPage = false;
                        break;
                    }
                }
                if (appendPage) _PageTable.Add(page);
            }

            _StepIndicator.StepCount = _PageTable.Count;
            _StepIndicator.CurrentStep = this.SelectedPageIndex + 1;
        }

        /// <summary>
        /// Returns array of all pages in control.
        /// </summary>
        /// <returns>Array of pages.</returns>
        public PageSliderPage[] GetAllPages()
        {
            return _PageTable.ToArray();
        }

        /// <summary>
        /// Removes and disposes all pages within the control.
        /// </summary>
        public void RemoveAllPages()
        {
            while (_PageTable.Count > 0)
            {
                PageSliderPage page = _PageTable[0];
                this.Controls.Remove(page);
                page.Dispose();
            }
        }

        private PageSliderPage _SelectedPage;
        /// <summary>
        /// Indicates selected page.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Indicates selected page.")]
        public PageSliderPage SelectedPage
        {
            get { return _SelectedPage; }
            set
            {
                if (value != _SelectedPage)
                {
                    PageSliderPage oldValue = _SelectedPage;
                    _SelectedPage = value;
                    OnSelectedPageChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SelectedPage property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSelectedPageChanged(PageSliderPage oldValue, PageSliderPage newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SelectedPage"));
            if (_AnimationTime > 0)
                AnimatePageTransition(oldValue, newValue);
            else
                UpdateBoundsForAllPages(false);
            //if (newValue != null)
            //{
            //    newValue.Bounds = GetPageBounds(newValue);
            //    newValue.BringToFront();
            //}
            UpdateScrollBar();
            _StepIndicator.CurrentStep = this.SelectedPageIndex + 1;
            OnSelectedPageChanged(EventArgs.Empty);
        }

        private void AnimatePageTransition(PageSliderPage oldValue, PageSliderPage newValue)
        {
            int oldIndex = -1;
            if (oldValue != null)
                oldIndex = _PageTable.IndexOf(oldValue);
            int newIndex = -1;
            if (newValue != null)
                newIndex = _PageTable.IndexOf(newValue);
            if (oldIndex == newIndex)
            {
                UpdateBoundsForAllPages(false);
                _PageBoundsOffset = Point.Empty;
                return;
            }
            int totalOffset = 0;

            Rectangle selectedPageBounds = GetSelectedPageBounds();

            if (_Orientation == eOrientation.Horizontal)
            {
                totalOffset = ((oldIndex - newIndex) * (selectedPageBounds.Width + _PageSpacing)) - _PageBoundsOffset.X;
            }
            else
            {
                totalOffset = ((oldIndex - newIndex) * (selectedPageBounds.Height + _PageSpacing)) - _PageBoundsOffset.Y;
            }

            _PageBoundsOffset = Point.Empty;

            DateTime startingTime = DateTime.Now;
            int speedFactor = 1;
            int remainOffset = Math.Abs(totalOffset);
            int offsetSign = Math.Sign(totalOffset);
            int animationTimeDuration = _AnimationTime;

            if (_Orientation == eOrientation.Horizontal && remainOffset < selectedPageBounds.Width * .8 ||
                _Orientation == eOrientation.Vertical && remainOffset < selectedPageBounds.Height * .8)
            {
                if (_Orientation == eOrientation.Horizontal)
                    animationTimeDuration *= (remainOffset / selectedPageBounds.Width);
                else
                    animationTimeDuration *= (remainOffset / selectedPageBounds.Height);
                animationTimeDuration = Math.Max(animationTimeDuration, 50);
            }

            TimeSpan animationTime = new TimeSpan(0, 0, 0, 0, animationTimeDuration);
            while (remainOffset > 0)
            {
                DateTime startPerMove = DateTime.Now;

                foreach (PageSliderPage page in _PageTable)
                {
                    Rectangle bounds = page.Bounds;
                    if (_Orientation == eOrientation.Horizontal)
                        bounds.Offset(speedFactor * offsetSign, 0);
                    else
                        bounds.Offset(0, speedFactor * offsetSign);
                    page.Bounds = bounds;
                    if (bounds.IntersectsWith(this.ClientRectangle))
                        this.Update();
                }
                remainOffset -= speedFactor;
                TimeSpan elapsedPerMove = DateTime.Now - startPerMove;
                TimeSpan elapsedTime = DateTime.Now - startingTime;
                if ((animationTime - elapsedTime).TotalMilliseconds <= 0)
                    speedFactor = remainOffset;
                else
                {
                    if ((int)(animationTime - elapsedTime).TotalMilliseconds == 0)
                        speedFactor = 1;
                    else
                        speedFactor = remainOffset * (int)elapsedPerMove.TotalMilliseconds / Math.Max(1, (int)((animationTime - elapsedTime).TotalMilliseconds));
                }
                if (speedFactor > remainOffset) speedFactor = remainOffset;
            }
        }

        private int _AnimationTime = 250;
        public int AnimationTime
        {
            get { return _AnimationTime; }
            set
            {
                if (value != _AnimationTime)
                {
                    int oldValue = _AnimationTime;
                    _AnimationTime = value;
                    OnAnimationTimeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when AnimationTime property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnAnimationTimeChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("AnimationTime"));

        }

        /// <summary>
        /// Gets or sets zero based selected page index. If there is no page selected returns -1.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int SelectedPageIndex
        {
            get { return _SelectedPage != null ? _PageTable.IndexOf(_SelectedPage) : -1; }
            set
            {
                if (value < 0 || value >= _PageTable.Count)
                    throw new ArgumentOutOfRangeException("SelectedPageIndex must be greater or equal than 0 and less or equal than number of pages added to control.");
                this.SelectedPage = _PageTable[value];
            }
        }

        private int _PageCount;
        /// <summary>
        /// Gets the total number of pages displayed by the control.
        /// </summary>
        [Browsable(false)]
        public int PageCount
        {
            get
            {
                return _PageTable.Count;
            }
        }

        private Padding _PagePadding = new Padding(4);
        /// <summary>
        /// Gets or sets the single page padding.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Padding PagePadding
        {
            get { return _PagePadding; }
            set
            {
                if (value != _PagePadding)
                {
                    Padding oldValue = _PagePadding;
                    _PagePadding = value;
                    OnPagePaddingChanged(oldValue, value);
                }
            }
        }

        /// <summary>
        /// Called when SelectedPagePadding property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnPagePaddingChanged(Padding oldValue, Padding newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SelectedPagePadding"));
            UpdateBoundsForAllPages(false);
        }

        private const int DefaultPageSpacing = 48;
        private int _PageSpacing = DefaultPageSpacing;
        /// <summary>
        /// Gets or sets the spacing in pixels between pages.
        /// </summary>
        [DefaultValue(DefaultPageSpacing), Category("Appearance"), Description("Indicates spacing in pixels between pages.")]
        public int PageSpacing
        {
            get { return _PageSpacing; }
            set
            {
                if (value != _PageSpacing)
                {
                    int oldValue = _PageSpacing;
                    _PageSpacing = value;
                    OnPageSpacingChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when PageSpacing property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnPageSpacingChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("PageSpacing"));
            UpdateBoundsForAllPages(false);
        }

        private const int DefaultNextPageVisibleMargin = 50;
        private int _NextPageVisibleMargin = DefaultNextPageVisibleMargin;
        [DefaultValue(DefaultNextPageVisibleMargin)]
        public int NextPageVisibleMargin
        {
            get { return _NextPageVisibleMargin; }
            set
            {
                if (value != _NextPageVisibleMargin)
                {
                    int oldValue = _NextPageVisibleMargin;
                    _NextPageVisibleMargin = value;
                    OnNextPageVisibleMarginChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when NextPageVisibleMargin property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnNextPageVisibleMarginChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("NextPageVisibleMargin"));
            UpdateBoundsForAllPages(false);
        }

        private eOrientation _Orientation = eOrientation.Horizontal;
        /// <summary>
        /// Gets or sets the page layout orientation. Default is horizontal.
        /// </summary>
        [DefaultValue(eOrientation.Horizontal), Category("Appearance"), Description("Indicates page layout orientation.")]
        public eOrientation Orientation
        {
            get { return _Orientation; }
            set
            {
                if (value != _Orientation)
                {
                    eOrientation oldValue = _Orientation;
                    _Orientation = value;
                    OnOrientationChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Orientation property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnOrientationChanged(eOrientation oldValue, eOrientation newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Orientation"));
            UpdateBoundsForAllPages(false);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WinApi.WindowsMessages.WM_APPCOMMAND)
            {
                int cmd = WinApi.GetAppCommandLParam(m.LParam);
                if (cmd == (int)WinApi.AppCommands.APPCOMMAND_BROWSER_BACKWARD)
                {
                    if (this.SelectedPageIndex > 0)
                        this.SelectedPageIndex--;
                    m.Result = new IntPtr(1);
                }
                else if (cmd == (int)WinApi.AppCommands.APPCOMMAND_BROWSER_FORWARD)
                {
                    if (this.SelectedPageIndex < _PageTable.Count - 1)
                        this.SelectedPageIndex++;
                    m.Result = new IntPtr(1);
                }
            }
            base.WndProc(ref m);
        }
        private bool _MouseDrag = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && _TouchHandler == null && _PageTable.Count > 0 && _MouseDragEnabled)
            {
                _MouseDrag = true;
                _TouchStartLocation = e.Location;
            }
            base.OnMouseDown(e);
        }

        private bool _PageMouseDragEnabled = true;
        /// <summary>
        /// Indicates whether page can be dragged using mouse to change currently selected page
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether page can be dragged using mouse to change currently selected page")]
        public bool PageMouseDragEnabled
        {
            get { return _PageMouseDragEnabled; }
            set
            {
                _PageMouseDragEnabled = value;
            }
        }
        private bool _MouseDragEnabled = true;
        /// <summary>
        /// Indicates whether selected page can be changed by using mouse drag on PageSlider client area which is not covered by pages.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether selected page can be changed by using mouse drag on PageSlider client area which is not covered by pages.")]
        public bool MouseDragEnabled
        {
            get { return _MouseDragEnabled; }
            set
            {
                _MouseDragEnabled = value;
            }
        }
        
        
        internal void StartPageDrag()
        {
            if (_PageMouseDragEnabled && !_MouseDrag && Control.MouseButtons == MouseButtons.Left && _TouchHandler == null && _PageTable.Count > 0)
            {
                this.Capture = true;
                _MouseDrag = true;
                _TouchStartLocation = this.PointToClient(Control.MousePosition);
            }
        }
        private int MaximumReversePageOffset
        {
            get
            {
                return Math.Min(32, this.Width / 6);
            }
        }
        private int TriggerPageChangeOffset
        {
            get
            {
                return Math.Max(32, this.Width / 5);
            }
        }
        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_ScrollBarVisibility == eScrollBarVisibility.AutoVisible && !_HorizontalScrollBar.Visible && !_VerticalScrollBar.Visible)
                StartScrollBarTimer();

            if (_MouseDrag)
            {
                Rectangle selectedPageBounds = GetSelectedPageBounds();
                if (_Orientation == eOrientation.Horizontal)
                {
                    int offset = e.Location.X - _TouchStartLocation.X;
                    int offsetChange = offset - _PageBoundsOffset.X;
                    Rectangle firstPageBounds = GetPageBounds(_PageTable[0]);
                    if (_PageTable[0].Left + offsetChange > selectedPageBounds.X + MaximumReversePageOffset && SelectedPageIndex >= 0)
                    {
                        offset -= (_PageTable[0].Left + offsetChange) - (selectedPageBounds.X + MaximumReversePageOffset);
                    }
                    else if (_PageTable[_PageTable.Count - 1].Left + offsetChange < selectedPageBounds.X - MaximumReversePageOffset)
                    {
                        offset += (selectedPageBounds.X - MaximumReversePageOffset) - (_PageTable[_PageTable.Count - 1].Left + offsetChange);
                    }
                    PageBoundsOffset = new Point(offset, 0);
                }
                else
                {
                    int offset = e.Location.Y - _TouchStartLocation.Y;
                    int offsetChange = offset - _PageBoundsOffset.Y;
                    Rectangle firstPageBounds = GetPageBounds(_PageTable[0]);
                    if (_PageTable[0].Top + offsetChange > selectedPageBounds.Y + MaximumReversePageOffset && SelectedPageIndex >= 0)
                    {
                        offset -= (_PageTable[0].Top + offsetChange) - (selectedPageBounds.Y + MaximumReversePageOffset);
                    }
                    else if (_PageTable[_PageTable.Count - 1].Top + offsetChange < selectedPageBounds.Y - MaximumReversePageOffset)
                    {
                        offset += (selectedPageBounds.Y - MaximumReversePageOffset) - (_PageTable[_PageTable.Count - 1].Top + offsetChange);
                    }
                    PageBoundsOffset = new Point(0, offset);
                }
            }
            base.OnMouseMove(e);
        }
        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_MouseDrag)
            {
                _MouseDrag = false;
                int totalMoveOffset = _Orientation == eOrientation.Horizontal ? _PageBoundsOffset.X : _PageBoundsOffset.Y;
                if (Math.Abs(totalMoveOffset) >= TriggerPageChangeOffset)
                {
                    Rectangle selectedPageBounds = GetSelectedPageBounds();
                    if (totalMoveOffset < 0)
                    {
                        if (this.SelectedPageIndex < _PageTable.Count - 1)
                        {
                            PageSliderPage newSelection = _PageTable[this.SelectedPageIndex + 1];
                            // Find right page to select since control can be scrolled a lot through inertia
                            for (int i = _PageTable.Count - 1; i >= 0; i--)
                            {
                                PageSliderPage page = _PageTable[i];
                                if (page.Bounds.IntersectsWith(selectedPageBounds) && selectedPageBounds.Right - page.Left >= TriggerPageChangeOffset)
                                {
                                    newSelection = page;
                                    break;
                                }
                            }
                            if (this.SelectedPage != newSelection)
                                this.SelectedPage = newSelection;
                            else
                                PageBoundsOffset = Point.Empty;
                        }
                        else
                            PageBoundsOffset = Point.Empty;
                    }
                    else
                    {
                        if (this.SelectedPageIndex > 0)
                        {
                            PageSliderPage newSelection = _PageTable[this.SelectedPageIndex - 1];
                            // Find right page to select since control can be scrolled a lot through inertia
                            for (int i = 0; i < _PageTable.Count; i++)
                            {
                                PageSliderPage page = _PageTable[i];
                                if (page.Bounds.IntersectsWith(selectedPageBounds) && page.Right - selectedPageBounds.X >= TriggerPageChangeOffset)
                                {
                                    newSelection = page;
                                    break;
                                }
                            }
                            if (this.SelectedPage != newSelection)
                                this.SelectedPage = newSelection;
                            else
                                PageBoundsOffset = Point.Empty;
                        }
                        else
                            PageBoundsOffset = Point.Empty;
                    }
                }
                else
                    PageBoundsOffset = Point.Empty;
            }
            base.OnMouseUp(e);
        }

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (_ScrollBarVisibility == eScrollBarVisibility.AutoVisible && this.Visible)
            {
                StartScrollBarTimer();
            }
            base.OnVisibleChanged(e);
        }

        private eScrollBarVisibility _ScrollBarVisibility = eScrollBarVisibility.AutoVisible;
        /// <summary>
        /// Indicates scrollbar visibility.
        /// </summary>
        [DefaultValue(eScrollBarVisibility.AutoVisible), Category("Behavior"), Description("Indicates scrollbar visibility.")]
        public eScrollBarVisibility ScrollBarVisibility
        {
            get { return _ScrollBarVisibility; }
            set
            {
                if (value != _ScrollBarVisibility)
                {
                    eScrollBarVisibility oldValue = _ScrollBarVisibility;
                    _ScrollBarVisibility = value;
                    OnScrollBarVisibilityChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when ScrollBarVisibility property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnScrollBarVisibilityChanged(eScrollBarVisibility oldValue, eScrollBarVisibility newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("ScrollBarVisibility"));
            UpdateScrollBarVisibility();
        }
        private void UpdateScrollBarVisibility()
        {
            if (_ScrollBarVisibility == eScrollBarVisibility.Hidden)
            {
                StopScrollBarTimer();
                _HorizontalScrollBar.Visible = false;
                _VerticalScrollBar.Visible = false;
                UpdateBoundsForAllPages(false);
            }
            else if (_ScrollBarVisibility == eScrollBarVisibility.AlwaysVisible)
            {
                StopScrollBarTimer();
                if (_Orientation == eOrientation.Horizontal)
                {
                    if (!_HorizontalScrollBar.Visible)
                    {
                        _HorizontalScrollBar.Visible = true;
                        UpdateBoundsForAllPages(false);
                    }
                    _VerticalScrollBar.Visible = false;
                }
                else
                {
                    _HorizontalScrollBar.Visible = false;
                    if (!_VerticalScrollBar.Visible)
                    {
                        _VerticalScrollBar.Visible = true;
                        UpdateBoundsForAllPages(false);
                    }
                }
            }
            else
            {
                Point p = this.PointToClient(Control.MousePosition);
                if (_ScrollBarTimer == null && this.ClientRectangle.Contains(p))
                {
                    StartScrollBarTimer();
                }
            }
        }
        private int _AutoScrollBarVisibleTime = 3000;
        /// <summary>
        /// Gets or sets the time in milliseconds that scrollbar is kept visible when there is no activity in control and mouse is over the control.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int AutoScrollBarVisibleTime
        {
            get { return _AutoScrollBarVisibleTime; }
            set
            {
                _AutoScrollBarVisibleTime = value;
            }
        }
        internal void MouseEnterInternal()
        {
            if (_ScrollBarVisibility == eScrollBarVisibility.AutoVisible)
                StartScrollBarTimer();
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            MouseEnterInternal();
            base.OnMouseEnter(e);
        }
        protected override void OnMouseLeave(EventArgs e)
        {
            MouseLeaveInternal();
            base.OnMouseLeave(e);
        }

        internal void MouseLeaveInternal()
        {
            if (_ScrollBarVisibility == eScrollBarVisibility.AutoVisible)
                StartScrollBarTimer();
        }

        private Timer _ScrollBarTimer = null;
        private void StartScrollBarTimer()
        {
            if (_Orientation == eOrientation.Horizontal)
            {
                _HorizontalScrollBar.Visible = true;
                _VerticalScrollBar.Visible = false;
            }
            else
            {
                _HorizontalScrollBar.Visible = false;
                _VerticalScrollBar.Visible = true;
            }

            if (_ScrollBarTimer == null)
            {
                _ScrollBarTimer = new Timer();
                _ScrollBarTimer.Tick += new EventHandler(ScrollBarTimerTick);
                _ScrollBarTimer.Interval = _AutoScrollBarVisibleTime;
            }
            _ScrollBarTimer.Stop();
            _ScrollBarTimer.Start();
        }
        private Point _LastMousePosition = Point.Empty;
        void ScrollBarTimerTick(object sender, EventArgs e)
        {
            bool hideScrollBars = false;
            Point p = this.PointToClient(Control.MousePosition);
            if (this.ClientRectangle.Contains(p))
            {
                if (!_LastMousePosition.IsEmpty && _LastMousePosition == p && (!_HorizontalScrollBar.Bounds.Contains(p) && _HorizontalScrollBar.Visible || !_VerticalScrollBar.Bounds.Contains(p) && _VerticalScrollBar.Visible))
                    hideScrollBars = true;
            }
            else
            {
                if (!_LastMousePosition.IsEmpty)
                    hideScrollBars = true;
            }
            _LastMousePosition = p;

            if (hideScrollBars)
            {
                StopScrollBarTimer();
                _HorizontalScrollBar.Visible = false;
                _VerticalScrollBar.Visible = false;
                _LastMousePosition = Point.Empty;
            }
        }
        private void StopScrollBarTimer()
        {
            Timer t = _ScrollBarTimer;
            _ScrollBarTimer = null;
            if (t != null)
            {
                t.Tick -= new EventHandler(ScrollBarTimerTick);
                t.Stop();
                t.Dispose();
            }
        }
        #endregion

        #region Touch Handling
        private bool ProcessTouch(DevComponents.DotNetBar.Touch.GestureEventArgs e)
        {
            if (_TouchEnabled == eTouchHandling.No) return false;
            if (_TouchEnabled == eTouchHandling.ClientContentOnly)
            {
                NativeFunctions.POINT p = new NativeFunctions.POINT(e.Location);
                IntPtr handle = NativeFunctions.ChildWindowFromPoint(this.Handle, p);
                if (handle == IntPtr.Zero)
                    return false;
                Control c = Control.FromChildHandle(handle);
                if (c == null) return false;
                if (c is PageSliderPage)
                {
                    handle = NativeFunctions.ChildWindowFromPoint(c.Handle, p);
                    if (handle == IntPtr.Zero)
                        return false;
                    c = Control.FromChildHandle(handle);
                    if (c == null || c is PageSliderPage || c is StepIndicator
                        || c is Label || c is LabelX) return true;
                }
                if(c == this || c is StepIndicator)
                    return true;
                return false;
            }
            return true;
        }

        private bool _TouchDrag = false;
        private Point _TouchStartLocation = Point.Empty;
        private void TouchHandlerPanBegin(object sender, DevComponents.DotNetBar.Touch.GestureEventArgs e)
        {
            if (!ProcessTouch(e)) return;

            _TouchDrag = true;
            _TouchStartLocation = e.Location;
            e.Handled = true;
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
            int totalMoveOffset = _Orientation == eOrientation.Horizontal ? _PageBoundsOffset.X : _PageBoundsOffset.Y;
            if (Math.Abs(totalMoveOffset) >= TriggerPageChangeOffset)
            {
                Rectangle selectedPageBounds = GetSelectedPageBounds();
                if (totalMoveOffset < 0)
                {
                    if (this.SelectedPageIndex < _PageTable.Count - 1)
                    {
                        PageSliderPage newSelection = _PageTable[this.SelectedPageIndex + 1];
                        // Find right page to select since control can be scrolled a lot through inertia
                        for (int i = _PageTable.Count - 1; i >= 0; i--)
                        {
                            PageSliderPage page = _PageTable[i];
                            if (page.Bounds.IntersectsWith(selectedPageBounds) && selectedPageBounds.Right - page.Left >= TriggerPageChangeOffset)
                            {
                                newSelection = page;
                                break;
                            }
                        }
                        if (this.SelectedPage != newSelection)
                            this.SelectedPage = newSelection;
                        else
                            PageBoundsOffset = Point.Empty;
                    }
                    else
                        PageBoundsOffset = Point.Empty;
                }
                else
                {
                    if (this.SelectedPageIndex > 0)
                    {
                        PageSliderPage newSelection = _PageTable[this.SelectedPageIndex - 1];
                        // Find right page to select since control can be scrolled a lot through inertia
                        for (int i = 0; i < _PageTable.Count; i++)
                        {
                            PageSliderPage page = _PageTable[i];
                            if (page.Bounds.IntersectsWith(selectedPageBounds) && page.Right - selectedPageBounds.X >= TriggerPageChangeOffset)
                            {
                                newSelection = page;
                                break;
                            }
                        }
                        if (this.SelectedPage != newSelection)
                            this.SelectedPage = newSelection;
                        else
                            PageBoundsOffset = Point.Empty;
                    }
                    else
                        PageBoundsOffset = Point.Empty;
                }
            }
            else
                PageBoundsOffset = Point.Empty;
        }

        private void TouchHandlerPan(object sender, DevComponents.DotNetBar.Touch.GestureEventArgs e)
        {
            if (_TouchDrag)
            {
                Rectangle selectedPageBounds = GetSelectedPageBounds();
                if (_Orientation == eOrientation.Horizontal)
                {
                    int offset = e.Location.X - _TouchStartLocation.X;
                    int offsetChange = offset - _PageBoundsOffset.X;
                    Rectangle firstPageBounds = GetPageBounds(_PageTable[0]);
                    bool overflow = false;
                    if (_PageTable[0].Left + offsetChange > selectedPageBounds.X + MaximumReversePageOffset && SelectedPageIndex >= 0)
                    {
                        offset -= (_PageTable[0].Left + offsetChange) - (selectedPageBounds.X + MaximumReversePageOffset);
                        overflow = true;
                    }
                    else if (_PageTable[_PageTable.Count - 1].Left + offsetChange < selectedPageBounds.X - MaximumReversePageOffset)
                    {
                        offset += (selectedPageBounds.X - MaximumReversePageOffset) - (_PageTable[_PageTable.Count - 1].Left + offsetChange);
                        overflow = true;
                    }
                    PageBoundsOffset = new Point(offset, 0);
                    if (overflow && e.IsInertia) EndTouchPan();
                }
                else
                {
                    int offset = e.Location.Y - _TouchStartLocation.Y;
                    int offsetChange = offset - _PageBoundsOffset.Y;
                    Rectangle firstPageBounds = GetPageBounds(_PageTable[0]);
                    bool overflow = false;
                    if (_PageTable[0].Top + offsetChange > selectedPageBounds.Y + MaximumReversePageOffset && SelectedPageIndex >= 0)
                    {
                        offset -= (_PageTable[0].Top + offsetChange) - (selectedPageBounds.Y + MaximumReversePageOffset);
                        overflow = true;
                    }
                    else if (_PageTable[_PageTable.Count - 1].Top + offsetChange < selectedPageBounds.Y - MaximumReversePageOffset)
                    {
                        offset += (selectedPageBounds.Y - MaximumReversePageOffset) - (_PageTable[_PageTable.Count - 1].Top + offsetChange);
                        overflow = true;
                    }
                    PageBoundsOffset = new Point(0, offset);
                    if (overflow && e.IsInertia) EndTouchPan();
                }
                e.Handled = true;
            }
        }

        private eTouchHandling _TouchEnabled = eTouchHandling.Yes;
        /// <summary>
        /// Indicates whether native touch support in control is enabled if available on target system.
        /// </summary>
        [DefaultValue(eTouchHandling.Yes), Category("Behavior"), Description("Indicates whether native touch support in control is enabled if available on target system.")]
        public eTouchHandling TouchEnabled
        {
            get { return _TouchEnabled; }
            set
            {
                if (value != _TouchEnabled)
                {
                    eTouchHandling oldValue = _TouchEnabled;
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
        protected virtual void OnTouchEnabledChanged(eTouchHandling oldValue, eTouchHandling newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TouchEnabled"));
        }
        #endregion
    }

    /// <summary>
    /// Specifies scrollbar visibility.
    /// </summary>
    public enum eScrollBarVisibility
    {
        /// <summary>
        /// Scrollbars are not visible.
        /// </summary>
        Hidden,
        /// <summary>
        /// Scrollbars are visible only if mouse is inside of the control.
        /// </summary>
        AutoVisible,
        /// <summary>
        /// Scrollbars are always visible.
        /// </summary>
        AlwaysVisible
    }

    /// <summary>
    /// Specifies level of touch enabled on the control.
    /// </summary>
    public enum eTouchHandling
    {
        /// <summary>
        /// Touch is enabled control wide.
        /// </summary>
        Yes,
        /// <summary>
        /// Touch is disabled control wide.
        /// </summary>
        No,
        /// <summary>
        /// Touch is enabled but only for the direct control client content. If touch input occurs on any child control it is not processed.
        /// </summary>
        ClientContentOnly
    }
}
