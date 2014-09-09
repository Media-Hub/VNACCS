using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Security.Permissions;
using System.Runtime.InteropServices;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar.Controls
{
    internal class PopupControlHost : ToolStripDropDown
    {
        #region Constructor
        private ePopupResizeEdge _ResizeEdge = ePopupResizeEdge.None;
        private Rectangle _ResizeGripBounds = Rectangle.Empty;
        private bool _CanResizeHostControl = false;
        private bool _CanResizePopup = false;
        private bool _RefreshSize = false;
        private static readonly Size ResizeGripSize = new Size(16, 16);

        public PopupControlHost()
        {
            this.AutoSize = false;
            this.Padding = System.Windows.Forms.Padding.Empty;
            this.Margin = System.Windows.Forms.Padding.Empty;
        }

        #endregion

        #region Implementation

        protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
        {
            Control hostControl = GetHostedControl();
            if (hostControl != null)
                hostControl.SizeChanged -= HostControlSizeChanged;
            base.OnClosing(e);
        }
        
        protected override void OnPaint(PaintEventArgs e)
        {
            
            ResizeGripBounds = Rectangle.Empty;

            if (IsResizeEnabled(ePopupResizeEdge.BottomLeft))
            {
                ResizeGripColors colors = GetColors();
                
                using(SolidBrush brush=new SolidBrush(colors.BackColor))
                    e.Graphics.FillRectangle(brush, 1, Height - ResizeGripSize.Height, Width - 2, ResizeGripSize.Height - 1);
                ResizeGripBounds = new Rectangle(1, Height - ResizeGripSize.Height, ResizeGripSize.Width, ResizeGripSize.Height);
                ResizeHandlePainter.DrawResizeHandle(e.Graphics, ResizeGripBounds, colors.GripLightColor, colors.GripColor, true);
            }
            else if (IsResizeEnabled(ePopupResizeEdge.BottomRight))
            {
                ResizeGripColors colors = GetColors();
                using (SolidBrush brush = new SolidBrush(colors.BackColor))
                    e.Graphics.FillRectangle(brush, 1, Height - ResizeGripSize.Height, Width - 2, ResizeGripSize.Height - 1);
                ResizeGripBounds = new Rectangle(Width - ResizeGripSize.Width - 1, Height - ResizeGripSize.Height, ResizeGripSize.Width, ResizeGripSize.Height);
                ResizeHandlePainter.DrawResizeHandle(e.Graphics, ResizeGripBounds, colors.GripLightColor, colors.GripColor, false);
            }

            base.OnPaint(e);
        }
        private ResizeGripColors _ResizeGripColors;
        private ResizeGripColors GetColors()
        {
            return _ResizeGripColors;
        }

        #region ResizeGripColors
        private struct ResizeGripColors
        {
            public Color BackColor;
            public Color GripLightColor;
            public Color GripColor;
            /// <summary>
            /// Initializes a new instance of the ResizeGripColors structure.
            /// </summary>
            /// <param name="backColor"></param>
            /// <param name="gripLightColor"></param>
            /// <param name="gripColor"></param>
            public ResizeGripColors(Color backColor, Color gripLightColor, Color gripColor)
            {
                BackColor = backColor;
                GripLightColor = gripLightColor;
                GripColor = gripColor;
            }
        }
        #endregion

        private bool _PopupUserSize = false;
        /// <summary>
        /// Gets whether popup has been resized by end-user.
        /// </summary>
        public bool PopupUserSize
        {
            get { return _PopupUserSize; }
            set { _PopupUserSize = value; }
        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (!_CanResizePopup)
            {
                UpdateHostControlSize();
                _PopupUserSize = true;
            }
        }

        protected void HostControlSizeChanged(object sender, EventArgs e)
        {
            if (!_CanResizeHostControl)
                UpdateContentBasedSize(-1);
        }

        public new void Show(int x, int y)
        {
            Show(x, y, -1, -1);
        }

        public void Show(int x, int y, int width, int height)
        {
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                Office2007ColorTable table = ((Office2007Renderer)GlobalManager.Renderer).ColorTable;
                _ResizeGripColors = new ResizeGripColors(table.Form.BackColor, table.LegacyColors.SplitterBackground2, table.LegacyColors.BarStripeColor);
            }
            else
                _ResizeGripColors = new ResizeGripColors(SystemColors.ButtonFace, Color.White, SystemColors.ControlDark);

            Control hostControl = GetHostedControl();
            if (hostControl == null)
                return;

            _CanResizeHostControl = true;
            _CanResizePopup = true;

            this.Size = new Size(1, 1);
            base.Show(x, y);

            _CanResizeHostControl = false;
            _CanResizePopup = false;

            UpdateContentBasedSize(width);

            if (_CloseButtonVisible && IsResizeGripShown)
            {
                if (_CloseButtonController == null)
                {
                    ButtonItem button = new ButtonItem();
                    button.Symbol = "\uf00d";
                    button.SymbolSize = 8;
                    button.Style = eDotNetBarStyle.StyleManagerControlled;
                    button.ButtonStyle = eButtonStyle.ImageAndText;
                    button.LeftInternal = 1;
                    button.Click += new EventHandler(ClosePopupButtonClick);
                    _CloseButtonController = new BaseItemController(button, this);
                    button.RecalcSize();
                    button.TopInternal = this.Height - button.HeightInternal - 1;
                }
            }
            else if (_CloseButtonController != null)
            {
                _CloseButtonController.Dispose();
                _CloseButtonController = null;
            }

            if (_RefreshSize)
                UpdateHostControlSize();

            if (y > Top && y <= Bottom)
            {
                Top = y - Height - (height != -1 ? height : 0);

                ePopupResizeEdge previous = ResizeEdge;
                if (ResizeEdge == ePopupResizeEdge.BottomLeft)
                    ResizeEdge = ePopupResizeEdge.TopLeft;
                else if (ResizeEdge == ePopupResizeEdge.BottomRight)
                    ResizeEdge = ePopupResizeEdge.TopRight;

                if (ResizeEdge != previous)
                    UpdateHostControlSize();
            }

            hostControl.SizeChanged += HostControlSizeChanged;
        }

        private void ClosePopupButtonClick(object sender, EventArgs e)
        {
            this.Close(ToolStripDropDownCloseReason.CloseCalled);
        }

        BaseItemController _CloseButtonController = null;

        protected void UpdateContentBasedSize(int proposedWidth)
        {
            if (_CanResizePopup)
                return;

            _CanResizeHostControl = true;
            try
            {
                Rectangle bounds = Bounds;
                bounds.Size = SizeFromContent(proposedWidth);

                if (!IsResizeEnabled(ePopupResizeEdge.None))
                {
                    if (proposedWidth > 0 && bounds.Width - 2 > proposedWidth)
                        if (!IsResizeEnabled(ePopupResizeEdge.Right))
                            bounds.X -= bounds.Width - 2 - proposedWidth;
                }

                Bounds = bounds;
            }
            finally
            {
                _CanResizeHostControl = false;
            }
        }

        protected void UpdateHostControlSize()
        {
            if (_CanResizeHostControl)
                return;

            _CanResizePopup = true;

            try
            {
                Control hostedControl = GetHostedControl();
                if (hostedControl != null)
                {
                    Rectangle bounds = hostedControl.Bounds;
                    if (IsResizeEnabled(ePopupResizeEdge.TopLeft) || IsResizeEnabled(ePopupResizeEdge.TopRight))
                        bounds.Location = new Point(1, ResizeGripSize.Height);
                    else
                        bounds.Location = new Point(1, 1);

                    bounds.Width = ClientRectangle.Width - 2;
                    bounds.Height = ClientRectangle.Height - 2;
                    if (IsResizeGripShown)
                        bounds.Height -= ResizeGripSize.Height;

                    if (bounds.Size != hostedControl.Size)
                        hostedControl.Size = bounds.Size;
                    if (bounds.Location != hostedControl.Location)
                        hostedControl.Location = bounds.Location;

                    if (_CloseButtonController != null)
                    {
                        _CloseButtonController.Item.TopInternal = this.Height - _CloseButtonController.Item.HeightInternal - 1;
                    }
                }
            }
            finally
            {
                _CanResizePopup = false;
            }
        }

        public Control GetHostedControl()
        {
            if (Items.Count > 0)
            {
                ToolStripControlHost host = Items[0] as ToolStripControlHost;
                if (host != null)
                    return host.Control;
            }
            return null;
        }

        public bool IsResizeEnabled(ePopupResizeEdge edge)
        {
            return (ResizeEdge & edge) == edge;
        }

        protected Size SizeFromContent(int proposedWidth)
        {
            Size contentSize = Size.Empty;

            _RefreshSize = false;

            // Fetch hosted control.
            Control hostedControl = GetHostedControl();
            if (hostedControl != null)
            {
                if (IsResizeEnabled(ePopupResizeEdge.TopLeft) || IsResizeEnabled(ePopupResizeEdge.TopRight))
                    hostedControl.Location = new Point(1, 16);
                else
                    hostedControl.Location = new Point(1, 1);
                contentSize = SizeFromClientSize(hostedControl.Size);

                // Use minimum width (if specified).
                if (proposedWidth > 0 && contentSize.Width < proposedWidth)
                {
                    contentSize.Width = proposedWidth;
                    _RefreshSize = true;
                }
            }

            // If a grip box is shown then add it into the drop down height.
            if (IsResizeGripShown)
                contentSize.Height += 16;

            // Add some additional space to allow for borders.
            contentSize.Width += 2;
            contentSize.Height += 2;

            return contentSize;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            if (!ProcessResizeGripMessages(ref m, false))
                base.WndProc(ref m);
        }

        /// <summary>
        /// Processes the resizing messages.
        /// </summary>
        /// <param name="m">The message.</param>
        /// <returns>true, if the WndProc method from the base class shouldn't be invoked.</returns>
        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        public bool ProcessResizeGripMessages(ref Message m)
        {
            return ProcessResizeGripMessages(ref m, true);
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool ProcessResizeGripMessages(ref Message m, bool contentControl)
        {
            if (ResizeEdge != ePopupResizeEdge.None)
            {
                if (m.Msg == (int)WinApi.WindowsMessages.WM_NCHITTEST)
                    return ProcessNcHitTest(ref m, contentControl);
                else if (m.Msg == (int)WinApi.WindowsMessages.WM_GETMINMAXINFO)
                    return ProcessGetMinMaxInfo(ref m);
            }
            return false;
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private bool ProcessGetMinMaxInfo(ref Message m)
        {
            Control hostedControl = GetHostedControl();
            if (hostedControl != null)
            {
                WinApi.MINMAXINFO minmax = (WinApi.MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(WinApi.MINMAXINFO));

                // Maximum size.
                if (hostedControl.MaximumSize.Width != 0)
                    minmax.maxTrackSize.Width = hostedControl.MaximumSize.Width;
                if (hostedControl.MaximumSize.Height != 0)
                    minmax.maxTrackSize.Height = hostedControl.MaximumSize.Height;

                // Minimum size.
                minmax.minTrackSize = new Size(32, 32);
                if (hostedControl.MinimumSize.Width > minmax.minTrackSize.Width)
                    minmax.minTrackSize.Width = hostedControl.MinimumSize.Width;
                if (hostedControl.MinimumSize.Height > minmax.minTrackSize.Height)
                    minmax.minTrackSize.Height = hostedControl.MinimumSize.Height;

                Marshal.StructureToPtr(minmax, m.LParam, false);
            }
            return true;
        }

        private bool ProcessNcHitTest(ref Message m, bool contentControl)
        {
            Point location = PointToClient(new Point(WinApi.LOWORD(m.LParam), WinApi.HIWORD(m.LParam)));
            IntPtr transparent = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);

            if (ResizeGripBounds.Contains(location))
            {
                if (IsResizeEnabled(ePopupResizeEdge.BottomLeft))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.BottomLeftSizeableCorner;
                    return true;
                }
                else if (IsResizeEnabled(ePopupResizeEdge.BottomRight))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.BottomRightSizeableCorner;
                    return true;
                }
                else if (IsResizeEnabled(ePopupResizeEdge.TopLeft))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.TopLeftSizeableCorner;
                    return true;
                }
                else if (IsResizeEnabled(ePopupResizeEdge.TopRight))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.TopRightSizeableCorner;
                    return true;
                }
            }
            else
            {
                Rectangle rectClient = ClientRectangle;
                if (location.X > rectClient.Right - 3 && location.X <= rectClient.Right && IsResizeEnabled(ePopupResizeEdge.Right))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.RightSizeableBorder;
                    return true;
                }
                else if (location.Y > rectClient.Bottom - 3 && location.Y <= rectClient.Bottom && IsResizeEnabled(ePopupResizeEdge.Bottom))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.BottomSizeableBorder;
                    return true;
                }
                else if (location.X > -1 && location.X < 3 && IsResizeEnabled(ePopupResizeEdge.Left))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.LeftSizeableBorder;
                    return true;
                }
                else if (location.Y > -1 && location.Y < 3 && IsResizeEnabled(ePopupResizeEdge.Top))
                {
                    m.Result = contentControl ? transparent : (IntPtr)WinApi.WindowHitTestRegions.TopSizeableBorder;
                    return true;
                }
            }
            return false;
        }

        /// <summary>
        /// Type of resize mode, grips are automatically drawn at bottom-left and bottom-right corners.
        /// </summary>
        public ePopupResizeEdge ResizeEdge
        {
            get { return _ResizeEdge; }
            set
            {
                if (value != _ResizeEdge)
                {
                    _ResizeEdge = value;
                    Invalidate();
                }
            }
        }

        private bool _CloseButtonVisible = true;
        /// <summary>
        /// Gets or sets whether Close button is visible in fotter. Resize handle must also be visible in order for close button to render.
        /// </summary>
        public bool CloseButtonVisible
        {
            get { return _CloseButtonVisible; }
            set
            {
                _CloseButtonVisible = value;
            }
        }
        
        /// <summary>
        /// Gets resize grip bounds.
        /// </summary>
        public Rectangle ResizeGripBounds
        {
            get { return _ResizeGripBounds; }
            private set { _ResizeGripBounds = value; }
        }

        private bool IsResizeGripShown
        {
            get
            {
                return (ResizeEdge == ePopupResizeEdge.BottomLeft || ResizeEdge == ePopupResizeEdge.BottomRight);
            }
        }

        #endregion
    }

    #region ePopupResizeEdge
    /// <summary>
    /// Specifies the popup resize modes
    /// </summary>
    public enum ePopupResizeEdge
    {
        None = 0,
        Left = 1,
        Top = 2,
        Right = 4,
        Bottom = 8,
        All = (Top | Left | Bottom | Right),
        TopLeft = (Top | Left),
        TopRight = (Top | Right),
        BottomLeft = (Bottom | Left),
        BottomRight = (Bottom | Right),
    }
    #endregion

    #region PopupHostController
    internal class PopupHostController : IDisposable
    {
        #region Constructor
        private ToolStripControlHost _ControlHost;
        private PopupControlHost _PopupControlHost;
        private System.Windows.Forms.Padding _Padding = System.Windows.Forms.Padding.Empty;
        private System.Windows.Forms.Padding _Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);

        public PopupHostController()
        {
            InitializePopupControlHost();
        }

        #endregion

        #region Implementation
        /// <summary>
        /// Occurs after popup is closed.
        /// </summary>
        public event ToolStripDropDownClosedEventHandler Closed;
        /// <summary>
        /// Raises Closed event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnClosed(ToolStripDropDownClosedEventArgs e)
        {
            ToolStripDropDownClosedEventHandler handler = Closed;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs before popup is closed and allows canceling.
        /// </summary>
        public event ToolStripDropDownClosingEventHandler Closing;
        /// <summary>
        /// Raises Closing event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnClosing(ToolStripDropDownClosingEventArgs e)
        {
            ToolStripDropDownClosingEventHandler handler = Closing;
            if (handler != null)
                handler(this, e);
        }

        //TuyenHM
        //private bool _CloseButtonVisible = true;
        /// <summary>
        /// Gets or sets whether Close button is visible in fotter. Resize handle must also be visible in order for close button to render.
        /// </summary>
        public bool CloseButtonVisible
        {
            get { return _PopupControlHost.CloseButtonVisible; }
            set
            {
                _PopupControlHost.CloseButtonVisible = value;
            }
        }


        private void InitializeHost(Control control)
        {
            InitializePopupControlHost();

            if (control != this.Control)
                DisposeToolstripControlHost();

            if (_ControlHost == null)
            {
                _ControlHost = new ToolStripControlHost(control);
                _ControlHost.Padding = this.Padding;
                _ControlHost.Margin = this.Margin;
            }

            _PopupControlHost.Items.Clear();
            _PopupControlHost.Padding = _PopupControlHost.Margin = System.Windows.Forms.Padding.Empty;
            _PopupControlHost.Items.Add(_ControlHost);
        }

        private void InitializePopupControlHost()
        {
            if (_PopupControlHost == null)
            {
                _PopupControlHost = new PopupControlHost();
                _PopupControlHost.Closed += new ToolStripDropDownClosedEventHandler(DropDownClosed);
                _PopupControlHost.Closing += new ToolStripDropDownClosingEventHandler(DropDownClosing);
            }
        }

        private void DropDownClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            OnClosed(e);
        }
        void DropDownClosing(object sender, ToolStripDropDownClosingEventArgs e)
        {
            OnClosing(e);
        }

        /// <summary>
        /// Show control on popup at specified location.
        /// </summary>
        public void Show(Control control, int x, int y)
        {
            Show(control, x, y, ePopupResizeEdge.None);
        }

        /// <summary>
        /// Shows control on popup at specified location with specified popup resize edges.
        /// </summary>
        public void Show(Control control, int x, int y, ePopupResizeEdge resizeEdge)
        {
            Show(control, x, y, -1, -1, resizeEdge);
        }

        /// <summary>
        /// Shows control on popup at specified location and size with specified popup resize edges.
        /// </summary>
        public void Show(Control control, int x, int y, int width, int height, ePopupResizeEdge resizeEdge)
        {
            Size controlSize = control.Size;

            InitializeHost(control);

            _PopupControlHost.ResizeEdge = resizeEdge;
            _PopupControlHost.Show(x, y, width, height);

            control.Focus();
        }
        /// <summary>
        /// Hides popup if visible.
        /// </summary>
        public void Hide()
        {
            if (_PopupControlHost != null && _PopupControlHost.Visible)
            {
                _PopupControlHost.Hide();
            }
        }

        private void DisposeToolstripControlHost()
        {
            if (_ControlHost != null)
            {
                if (_PopupControlHost != null)
                    _PopupControlHost.Items.Clear();

                _ControlHost.Dispose();
                _ControlHost = null;
            }
        }

        /// <summary>
        /// Gets whether popup is visible.
        /// </summary>
        public bool Visible
        {
            get { return (_PopupControlHost != null && _PopupControlHost.Visible) ? true : false; }
        }
        /// <summary>
        /// Gets the control displayed on popup.
        /// </summary>
        public Control Control
        {
            get { return (_ControlHost != null) ? _ControlHost.Control : null; }
        }
        /// <summary>
        /// Gets or sets the popup padding.
        /// </summary>
        public System.Windows.Forms.Padding Padding
        {
            get { return _Padding; }
            set { _Padding = value; }
        }
        /// <summary>
        /// Gets or sets popup margin.
        /// </summary>
        public System.Windows.Forms.Padding Margin
        {
            get { return _Margin; }
            set { _Margin = value; }
        }

        public bool PopupUserSize
        {
            get
            {
                if (_PopupControlHost == null) return false;
                return _PopupControlHost.PopupUserSize;
            }
            set
            {
                if (_PopupControlHost != null)
                    _PopupControlHost.PopupUserSize = value;
            }
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            DisposeToolstripControlHost();
            if (_PopupControlHost != null)
            {
                _PopupControlHost.Closed -= new ToolStripDropDownClosedEventHandler(DropDownClosed);
                _PopupControlHost.Closing -= new ToolStripDropDownClosingEventHandler(DropDownClosing);
                _PopupControlHost.Dispose();
                _PopupControlHost = null;
            }

        }

        #endregion
    }
    #endregion
}
