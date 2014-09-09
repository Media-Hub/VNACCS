using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// PopupControl
    ///</summary>
    public class PopupControl
    {
        #region Events

        #region Closed

        /// <summary>
        /// Closed event handler
        /// </summary>
        public event ToolStripDropDownClosedEventHandler Closed
        {
            add { _DropDown.Closed += value; }
            remove { _DropDown.Closed -= value; }
        }

        #endregion

        #region Closing

        /// <summary>
        /// Closing event handler
        /// </summary>
        public event ToolStripDropDownClosingEventHandler Closing
        {
            add { _DropDown.Closing += value; }
            remove { _DropDown.Closing -= value; }
        }

        #endregion

        #region Opened

        /// <summary>
        /// Opened event handler
        /// </summary>
        public event EventHandler Opened
        {
            add { _DropDown.Opened += value; }
            remove { _DropDown.Opened -= value; }
        }

        #endregion

        #region Opening

        /// <summary>
        /// Opening event handler
        /// </summary>
        public event CancelEventHandler Opening
        {
            add { _DropDown.Opening += value; }
            remove { _DropDown.Opening -= value; }
        }

        #endregion

        #region PostRenderGripBar

        /// <summary>
        /// Opening event handler
        /// </summary>
        public event EventHandler<PostRenderGripBarEventArgs> PostRenderGripBar
        {
            add { _DropDown.PostRenderGripBar += value; }
            remove { _DropDown.PostRenderGripBar -= value; }
        }

        #endregion

        #region PreRenderGripBar

        /// <summary>
        /// Opening event handler
        /// </summary>
        public event EventHandler<PreRenderGripBarEventArgs>PreRenderGripBar
        {
            add { _DropDown.PreRenderGripBar += value; }
            remove { _DropDown.PreRenderGripBar -= value; }
        }

        #endregion

        #region UserResize

        /// <summary>
        /// UserResize event handler
        /// </summary>
        public event EventHandler<EventArgs> UserResize
        {
            add { _DropDown.UserResize += value; }
            remove { _DropDown.UserResize -= value; }
        }

        #endregion

        #endregion

        #region Private variables

        private ToolStripControlHost _Host;
        private PopupDropDown _DropDown;

        private PopupResizeMode _ResizeMode = PopupResizeMode.All;

        private Size _GripSize = new Size(14, 14);
        private System.Windows.Forms.Padding _Padding = System.Windows.Forms.Padding.Empty;
        private System.Windows.Forms.Padding _Margin = new System.Windows.Forms.Padding(1);

        private bool _AutoReset;

        private Background _Background;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public PopupControl()
        {
            InitializeDropDown();
        }

        #region InitializeDropDown

        protected void InitializeDropDown()
        {
            if (_DropDown == null)
            {
                _DropDown = new PopupDropDown(false);

                _DropDown.Closed += DropDownClosed;
            }
        }

        #endregion

        #region Public properties

        #region AutoResetWhenClosed

        /// <summary>
        /// AutoResetWhenClosed
        /// </summary>
        public bool AutoResetWhenClosed
        {
            get { return (_AutoReset); }
            set { _AutoReset = value; }
        }

        #endregion

        #region Background

        ///<summary>
        ///Background
        ///</summary>
        public Background Background
        {
            get { return (_Background); }
            set { _Background = value; }
        }

        #endregion

        #region Control

        /// <summary>
        /// Associated drop down Control
        /// </summary>
        public Control Control
        {
            get { return (_Host != null) ? _Host.Control : null; }
        }

        #endregion

        #region GripSize

        /// <summary>
        /// GripSize
        /// </summary>
        public Size GripSize
        {
            get { return (_GripSize); }
            set { _GripSize = value; }
        }

        #endregion

        #region Margin

        /// <summary>
        /// Margin
        /// </summary>
        public System.Windows.Forms.Padding Margin
        {
            get { return (_Margin); }
            set { _Margin = value; }
        }

        #endregion

        #region Padding

        /// <summary>
        /// Padding
        /// </summary>
        public System.Windows.Forms.Padding Padding
        {
            get { return (_Padding); }
            set { _Padding = value; }
        }

        #endregion

        #region ResizeMode

        /// <summary>
        /// Type of resize mode
        /// </summary>
        public PopupResizeMode ResizeMode
        {
            get { return (_ResizeMode); }
            set { _ResizeMode = value; }
        }

        #endregion

        #region Visible

        /// <summary>
        /// Visible
        /// </summary>
        public bool Visible
        {
            get { return (_DropDown != null && _DropDown.Visible) ? true : false; }
        }

        #endregion

        #endregion

        #region Show

        /// <summary>
        /// Shows the PopupControl
        /// </summary>
        /// <param name="control"></param>
        /// <param name="pt"></param>
        /// <param name="anchor"></param>
        /// <param name="form"></param>
        public void Show(Control control, Point pt, PopupAnchor anchor, Form form)
        {
            Office2007RibbonForm parentForm = form as Office2007RibbonForm;

            if (parentForm != null) parentForm.NonClientPaintEnabled = false;

            InitializeHost(control);

            _DropDown.ResizeMode = _ResizeMode;
            _DropDown.PopupAnchor = anchor;
            _DropDown.Background = _Background;

            _DropDown.Show(pt);

            control.Focus();

            if (parentForm != null) { parentForm.NonClientPaintEnabled = true; parentForm.RedrawNonClient(); }
        }

        #region InitializeHost

        protected void InitializeHost(Control control)
        {
            InitializeDropDown();

            if (control != Control)
                DisposeHost();

            if (_Host == null)
            {
                _Host = new ToolStripControlHost(control);

                _Host.AutoSize = false;
                _Host.Padding = Padding;
                _Host.Margin = Margin;
            }

            _DropDown.Items.Clear();

            _DropDown.AutoSize = false;

            _DropDown.GripSize = _GripSize;

            _DropDown.Margin = System.Windows.Forms.Padding.Empty;
            _DropDown.Padding = System.Windows.Forms.Padding.Empty;

            _DropDown.Items.Add(_Host);
        }

        #endregion

        #endregion

        #region Hide

        /// <summary>
        /// Hides / closes the PopupControl
        /// </summary>
        public void Hide()
        {
            if (_DropDown != null && _DropDown.Visible == true)
                _DropDown.Hide();
        }

        #endregion

        #region Reset

        /// <summary>
        /// Resets / disposes of the Host control
        /// </summary>
        public void Reset()
        {
            DisposeHost();
        }

        #endregion

        #region Event processing

        #region DropDownClosed

        private void DropDownClosed(object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (AutoResetWhenClosed == true)
                DisposeHost();
        }

        #endregion

        #endregion

        #region DisposeHost

        protected void DisposeHost()
        {
            if (_Host != null)
            {
                //Type t = _Host.GetType();
                //FieldInfo fi = t.GetField("control", BindingFlags.NonPublic | BindingFlags.Instance);
                //if (fi != null)
                //{
                //    fi.SetValue(_Host, null);
                //    _Host.Dispose();
                //}

                _Host.Dispose();

                _Host = null;
            }
        }

        #endregion
    }

    #region PopupDropDown

    /// <summary>
    /// PopupDropDown
    /// </summary>
    [ToolboxItem(false)]
    public class PopupDropDown : ToolStripDropDown
    {
        #region Events

        #region PostRenderGripBar

        /// <summary>
        /// Occurs when the popup grip bar has been rendered
        /// </summary>
        [Description("Occurs when the popup grip bar has been rendered.")]
        public event EventHandler<PostRenderGripBarEventArgs> PostRenderGripBar;

        #endregion

        #region PreRenderGripBar

        /// <summary>
        /// Occurs when the popup grip bar is about to be rendered
        /// </summary>
        [Description("Occurs when the popup grip bar is about to be rendered.")]
        public event EventHandler<PreRenderGripBarEventArgs> PreRenderGripBar;

        #endregion

        #region UserResize

        /// <summary>
        /// Occurs when the user has resized the control
        /// </summary>
        [Description("Occurs when the user has resized the control.")]
        public event EventHandler<EventArgs> UserResize;

        #endregion

        #endregion

        #region Private variables

        private GripAlignMode _GripAlignMode;
        private PopupAnchor _PopupAnchor = PopupAnchor.Left;
        private PopupResizeMode _ResizeMode = PopupResizeMode.All;

        private Size _GripSize;
        private Rectangle _GripBounds;

        private bool _LockedHostedControlSize;
        private bool _LockedThisSize;
        private bool _ForceClose;

        private Background _Background;

        #endregion

        /// <summary>
        /// PopupDropDown
        /// </summary>
        /// <param name="autoSize"></param>
        public PopupDropDown(bool autoSize)
        {
            AutoSize = autoSize;
            Padding = System.Windows.Forms.Padding.Empty;
            Margin = System.Windows.Forms.Padding.Empty;
        }

        #region Public properties

        #region Background

        ///<summary>
        ///Background
        ///</summary>
        public Background Background
        {
            get { return (_Background); }
            set { _Background = value; }
        }

        #endregion

        #region GripAlignMode

        /// <summary>
        /// Grip align mode
        /// </summary>
        public GripAlignMode GripAlignMode
        {
            get { return (_GripAlignMode); }
            set { _GripAlignMode = value; }
        }

        #endregion

        #region GripSize

        /// <summary>
        /// Size of the grip box
        /// </summary>
        public Size GripSize
        {
            get { return (_GripSize); }
            set { _GripSize = value; }
        }

        #endregion

        #region PopupAnchor

        /// <summary>
        /// PopupAnchor
        /// </summary>
        public PopupAnchor PopupAnchor
        {
            get { return (_PopupAnchor); }
            set { _PopupAnchor = value; }
        }

        #endregion

        #region ResizeMode

        /// <summary>
        /// Type of resize mode
        /// </summary>
        public PopupResizeMode ResizeMode
        {
            get { return (_ResizeMode); }
            set { _ResizeMode = value; }
        }

        #endregion

        #endregion

        #region Protected properties

        #region GripBounds

        /// <summary>
        /// Bounds of active grip box position
        /// </summary>
        protected Rectangle GripBounds
        {
            get { return (_GripBounds); }
            set { _GripBounds = value; }
        }

        #endregion

        #region IsGripShown

        /// <summary>
        /// Indicates when a grip box is shown.
        /// </summary>
        protected bool IsGripShown
        {
            get { return (GripAlignMode != GripAlignMode.None); }
        }

        #endregion

        #endregion

        #region Show

        /// <summary>
        /// Shows the popup drop down
        /// </summary>
        /// <param name="pt"></param>
        public new void Show(Point pt)
        {
            Show(pt.X, pt.Y);
        }

        /// <summary>
        /// Shows the popup drop down
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        public new void Show(int x, int y)
        {
            Control hostedControl = GetHostedControl();

            if (hostedControl != null)
            {
                _ForceClose = false;

                _LockedHostedControlSize = true;
                _LockedThisSize = true;

                // Display actual popup and occupy just 1x1 pixel to avoid automatic reposition

                Size = new Size(1, 1);

                base.Show(x, y);

                _LockedHostedControlSize = false;
                _LockedThisSize = false;

                ResizeFromContent();

                // If popup is overlapping the initial position then move above

                if (y > Top && y <= Bottom)
                {
                    Top = y - Height;

                    GripAlignMode previous = GripAlignMode;

                    switch (GripAlignMode)
                    {
                        case GripAlignMode.BottomLeft:
                            GripAlignMode = GripAlignMode.TopLeft;

                            if (CompareResizeMode(PopupResizeMode.BottomLeft))
                                ResizeMode = PopupResizeMode.TopLeft;
                            break;

                        case GripAlignMode.BottomRight:
                            GripAlignMode = GripAlignMode.TopRight;

                            if (CompareResizeMode(PopupResizeMode.BottomRight))
                                ResizeMode = PopupResizeMode.TopRight;
                            break;
                    }

                    if (GripAlignMode != previous)
                        RecalculateHostedControlLayout();
                }

                hostedControl.SizeChanged += HostedControlSizeChanged;
            }
        }

        #endregion

        #region Hide

        /// <summary>
        /// Hide
        /// </summary>
        public new void Hide()
        {
            _ForceClose = true;

            base.Hide();
        }

        #endregion

        #region ResizeFromContent

        protected void ResizeFromContent()
        {
            if (_LockedThisSize == false)
            {
                _LockedHostedControlSize = true;

                Rectangle bounds = Bounds;
                bounds.Size = SizeFromContent();

                if (PopupAnchor == PopupAnchor.Right)
                    bounds.X -= bounds.Width;

                Bounds = bounds;

                _LockedHostedControlSize = false;
            }
        }

        #region SizeFromContent

        protected Size SizeFromContent()
        {
            Size contentSize = Size.Empty;

            Control hostedControl = GetHostedControl();

            if (hostedControl != null)
            {
                hostedControl.Location = new Point(1, 1);

                contentSize = SizeFromClientSize(hostedControl.Size);
            }

            SetGripAlignMode();

            if (IsGripShown == true)
                contentSize.Height += GripSize.Height + 2;

            contentSize.Width += 2;
            contentSize.Height += 2;

            return (contentSize);
        }

        #region SetGripAlignMode

        private void SetGripAlignMode()
        {
            if (CompareResizeMode(PopupResizeMode.BottomRight))
                GripAlignMode = GripAlignMode.BottomRight;

            else if (CompareResizeMode(PopupResizeMode.TopLeft))
                GripAlignMode = GripAlignMode.TopLeft;

            else if (CompareResizeMode(PopupResizeMode.TopRight))
                GripAlignMode = GripAlignMode.TopRight;

            else if (CompareResizeMode(PopupResizeMode.BottomLeft))
                GripAlignMode = GripAlignMode.BottomLeft;

            else
                GripAlignMode = GripAlignMode.None;
        }

        #endregion

        #endregion

        #endregion

        #region RecalculateHostedControlLayout

        protected void RecalculateHostedControlLayout()
        {
            if (_LockedHostedControlSize == false)
            {
                _LockedThisSize = true;

                Control hostedControl = GetHostedControl();

                if (hostedControl != null)
                {
                    Rectangle bounds = new
                        Rectangle(0, 0, ClientRectangle.Width, ClientRectangle.Height);

                    if (GripAlignMode == GripAlignMode.TopLeft || GripAlignMode == GripAlignMode.TopRight)
                        bounds.Y = GripSize.Height + 2;

                    bounds.Inflate(-1, -1);

                    if (IsGripShown == true)
                        bounds.Height -= (GripSize.Height + 2);

                    if (bounds.Size != hostedControl.Size)
                        hostedControl.Size = bounds.Size;

                    if (bounds.Location != hostedControl.Location)
                        hostedControl.Location = bounds.Location;
                }

                _LockedThisSize = false;
            }
        }

        #endregion

        #region GetHostedControl

        /// <summary>
        /// Gets the hosted control
        /// </summary>
        /// <returns></returns>
        public Control GetHostedControl()
        {
            if (Items.Count > 0)
            {
                ToolStripControlHost host = Items[0] as ToolStripControlHost;

                if (host != null)
                    return (host.Control);
            }

            return (null);
        }

        #endregion

        #region CompareResizeMode

        /// <summary>
        /// CompareResizeMode
        /// </summary>
        /// <param name="resizeMode"></param>
        /// <returns></returns>
        public bool CompareResizeMode(PopupResizeMode resizeMode)
        {
            return (ResizeMode & resizeMode) == resizeMode;
        }

        #endregion

        #region ToolStripDropDown overrides

        #region OnClosing

        protected override void OnClosing(ToolStripDropDownClosingEventArgs e)
        {
            if (_ForceClose == false && ContainsFocus == true)
            {
                e.Cancel = true;
            }
            else
            {
                Control hostedControl = GetHostedControl();

                if (hostedControl != null)
                    hostedControl.SizeChanged -= HostedControlSizeChanged;

                base.OnClosing(e);
            }
        }

        #endregion

        #region OnPaint

        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            base.OnPaint(e);

            GripBounds = Rectangle.Empty;

            if (IsGripShown == true)
            {
                int x = Width - (GripSize.Width + 2);
                int y = Height - (_GripSize.Height + 1);

                int width = GripSize.Width;
                int height = GripSize.Height;

                Rectangle r = new Rectangle(1, 1, Width - 2, height + 1);

                switch (_GripAlignMode)
                {
                    case GripAlignMode.TopLeft:
                        GripBounds = new Rectangle(2, 1, width, height);
                        r.Y = 1;
                        break;

                    case GripAlignMode.TopRight:
                        r.Y = 1;
                        GripBounds = new Rectangle(x, 1, width, height);
                        break;

                    case GripAlignMode.BottomLeft:
                        GripBounds = new Rectangle(2, y, width, height);
                        r.Y = GripBounds.Y - 1;
                        break;

                    case GripAlignMode.BottomRight:
                        GripBounds = new Rectangle(x, y, width, height);
                        r.Y = GripBounds.Y - 1;
                        break;
                }

                if (_Background.IsEmpty == false)
                {
                    using (Brush br = _Background.GetBrush(r))
                        g.FillRectangle(br, r);
                }

                if (DoPreRenderGripBar(g, r) == false)
                {
                    GripRenderer.Render(g,
                        GripBounds.Location, GripSize, _GripAlignMode);

                    DoPostRenderGripBar(g, r);
                }
            }
        }

        #region DoPreRenderGripBar

        private bool DoPreRenderGripBar(Graphics g, Rectangle r)
        {
            if (PreRenderGripBar != null)
            {
                PreRenderGripBarEventArgs args = new PreRenderGripBarEventArgs(g, r);

                PreRenderGripBar(this, args);

                return (args.Cancel);
            }

            return (false);
        }

        #endregion

        #region DoPostRenderGripBar

        private void DoPostRenderGripBar(Graphics g, Rectangle r)
        {
            if (PostRenderGripBar != null)
            {
                PostRenderGripBar(this,
                    new PostRenderGripBarEventArgs(g, r));
            }
        }

        #endregion

        #endregion

        #region OnSizeChanged

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);

            if (_LockedThisSize == false)
                RecalculateHostedControlLayout();
        }

        #endregion

        #region ProcessDialogKey

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape || (keyData & Keys.Alt) == Keys.Alt ||
                (keyData & Keys.LWin) == Keys.LWin || (keyData & Keys.RWin) == Keys.RWin)
            {
                _ForceClose = true;
            }

            return (base.ProcessDialogKey(keyData));
        }

        #endregion

        #region HostedControlSizeChanged

        protected void HostedControlSizeChanged(object sender, EventArgs e)
        {
            if (_LockedHostedControlSize == false)
                ResizeFromContent();
        }

        #endregion

        #endregion

        #region Win32 processing

        #region Win32 defines

        // ReSharper disable InconsistentNaming

        protected const int WM_GETMINMAXINFO = 0x0024;
        protected const int WM_NCHITTEST = 0x0084;
        protected const int WM_EXITSIZEMOVE = 0x0232;

        protected const int HTTRANSPARENT = -1;
        protected const int HTLEFT = 10;
        protected const int HTRIGHT = 11;
        protected const int HTTOP = 12;
        protected const int HTTOPLEFT = 13;
        protected const int HTTOPRIGHT = 14;
        protected const int HTBOTTOM = 15;
        protected const int HTBOTTOMLEFT = 16;
        protected const int HTBOTTOMRIGHT = 17;

        [StructLayout(LayoutKind.Sequential)]
        internal struct MINMAXINFO
        {
            public Point reserved;
            public Size maxSize;
            public Point maxPosition;
            public Size minTrackSize;
            public Size maxTrackSize;
        }

        protected static int HIWORD(int n)
        {
            return ((n >> 16) & 0xffff);
        }

        protected static int HIWORD(IntPtr n)
        {
            return (HIWORD(unchecked((int)(long)n)));
        }

        protected static int LOWORD(int n)
        {
            return (n & 0xffff);
        }

        protected static int LOWORD(IntPtr n)
        {
            return (LOWORD(unchecked((int)(long)n)));
        }

        // ReSharper restore InconsistentNaming

        #endregion

        #region WndProc

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WM_NCHITTEST:
                    if (OnNcHitTest(ref m) == true)
                        return;
                    break;

                case WM_GETMINMAXINFO:
                    OnGetMinMaxInfo(ref m);
                    return;

                case WM_EXITSIZEMOVE:
                    if (UserResize != null)
                        UserResize(this, EventArgs.Empty);
                    break;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region OnNcHitTest

        private bool OnNcHitTest(ref Message m)
        {
            Point pt = PointToClient(new Point(LOWORD(m.LParam), HIWORD(m.LParam)));

            if (IsGripShown == true)
            {
                if (GripBounds.Contains(pt))
                {
                    switch (_GripAlignMode)
                    {
                        case GripAlignMode.TopLeft:
                            m.Result = (IntPtr)HTTOPLEFT;
                            break;

                        case GripAlignMode.TopRight:
                            m.Result = (IntPtr)HTTOPRIGHT;
                            break;

                        case GripAlignMode.BottomLeft:
                            m.Result = (IntPtr) HTBOTTOMLEFT;
                            break;

                        case GripAlignMode.BottomRight:
                            m.Result = (IntPtr) HTBOTTOMRIGHT;
                            break;
                    }

                    return (true);
                }
            }

            if (ResizeMode != PopupResizeMode.None)
            {
                Rectangle rectClient = ClientRectangle;

                if (pt.X > rectClient.Right - 3 && pt.X <= rectClient.Right)
                {
                    if (CompareResizeMode(PopupResizeMode.Right) == true)
                    {
                        m.Result = (IntPtr) HTRIGHT;
                        return (true);
                    }
                }
                else if (pt.Y > rectClient.Bottom - 3 && pt.Y <= rectClient.Bottom)
                {
                    if (CompareResizeMode(PopupResizeMode.Bottom) == true)
                    {
                        m.Result = (IntPtr) HTBOTTOM;
                        return (true);
                    }
                }
                else if (pt.X > -1 && pt.X < 3)
                {
                    if (CompareResizeMode(PopupResizeMode.Left) == true)
                    {
                        m.Result = (IntPtr) HTLEFT;
                        return (true);
                    }
                }
                else if (pt.Y > -1 && pt.Y < 3)
                {
                    if (CompareResizeMode(PopupResizeMode.Top) == true)
                    {
                        m.Result = (IntPtr) HTTOP;
                        return (true);
                    }
                }
            }

            return (false);
        }

        #endregion

        #region OnGetMinMaxInfo

        [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
        private void OnGetMinMaxInfo(ref Message m)
        {
            Control hostedControl = GetHostedControl();

            if (hostedControl != null)
            {
                MINMAXINFO minmax = (MINMAXINFO)Marshal.PtrToStructure(m.LParam, typeof(MINMAXINFO));

                if (hostedControl.MaximumSize.Width != 0)
                    minmax.maxTrackSize.Width = hostedControl.MaximumSize.Width;

                if (hostedControl.MaximumSize.Height != 0)
                    minmax.maxTrackSize.Height = hostedControl.MaximumSize.Height;

                minmax.minTrackSize = new Size(32, 32);

                if (hostedControl.MinimumSize.Width > minmax.minTrackSize.Width)
                    minmax.minTrackSize.Width = hostedControl.MinimumSize.Width;

                if (hostedControl.MinimumSize.Height > minmax.minTrackSize.Height)
                    minmax.minTrackSize.Height = hostedControl.MinimumSize.Height;

                if (IsGripShown == true)
                    minmax.minTrackSize.Height += GripSize.Height + 4;

                minmax.minTrackSize.Width += 4;

                Marshal.StructureToPtr(minmax, m.LParam, false);
            }
        }

        #endregion

        #endregion
    }

    #endregion

    #region GripRenderer

    /// <summary>
    /// Grip Renderer
    /// </summary>
    public static class GripRenderer
    {
        #region Private variables

        private static Bitmap _gripBitmap;

        #endregion

        #region RefreshGrip

        /// <summary>
        /// Refreshes the Grip
        /// </summary>
        /// <param name="g"></param>
        /// <param name="size"></param>
        public static void RefreshGrip(Graphics g, Size size)
        {
            InitializeGripBitmap(g, size, true);
        }

        #endregion

        #region InitializeGripBitmap

        private static void InitializeGripBitmap(Graphics g, Size size, bool refresh)
        {
            if (_gripBitmap == null || refresh == true || _gripBitmap.Size != size)
            {
                _gripBitmap = new Bitmap(size.Width, size.Height, g);

                using (Graphics gripG = Graphics.FromImage(_gripBitmap))
                    ControlPaint.DrawSizeGrip(gripG, SystemColors.ButtonFace, 0, 0, size.Width, size.Height);
            }
        }

        #endregion

        #region Render

        /// <summary>
        /// Grip renderer
        /// </summary>
        /// <param name="g"></param>
        /// <param name="location"></param>
        /// <param name="mode"></param>
        public static void Render(
            Graphics g, Point location, GripAlignMode mode)
        {
            Render(g, location, new Size(14, 14), mode);
        }

        /// <summary>
        /// Grip renderer
        /// </summary>
        /// <param name="g"></param>
        /// <param name="location"></param>
        /// <param name="size"></param>
        /// <param name="mode"></param>
        public static void Render(Graphics g,
            Point location, Size size, GripAlignMode mode)
        {
            InitializeGripBitmap(g, size, false);

            switch (mode)
            {
                case GripAlignMode.TopLeft:
                    size.Height = -size.Height;
                    size.Width = -size.Width;
                    break;

                case GripAlignMode.TopRight:
                    size.Height = -size.Height;
                    break;

                case GripAlignMode.BottomLeft:
                    size.Width = -size.Width;
                    break;
            }

            // Reverse size grip for left-aligned

            if (size.Width < 0)
                location.X -= size.Width;

            if (size.Height < 0)
                location.Y -= size.Height;

            g.DrawImage(_gripBitmap, location.X, location.Y, size.Width, size.Height);
        }

        #endregion
    }

    #endregion

    #region Enums

    #region GripAlignMode

    /// <summary>
    /// GripAlignMode
    /// </summary>
    public enum GripAlignMode
    {
        /// <summary>
        /// None
        /// </summary>
        None,

        /// <summary>
        /// TopLeft
        /// </summary>
        TopLeft,

        /// <summary>
        /// TopRight
        /// </summary>
        TopRight,

        /// <summary>
        /// BottomLeft
        /// </summary>
        BottomLeft,

        /// <summary>
        /// BottomRight
        /// </summary>
        BottomRight,
    }

    #endregion

    #region PopupAnchor

    ///<summary>
    /// PopupAnchor
    ///</summary>
    public enum PopupAnchor
    {
        ///<summary>
        /// Left
        ///</summary>
        Left,

        ///<summary>
        /// Right
        ///</summary>
        Right
    }

    #endregion

    #region PopupResizeMode

    ///<summary>
    /// PopupResizeMode
    ///</summary>
    [Flags]
    public enum PopupResizeMode
    {
        ///<summary>
        /// None
        ///</summary>
        None = 0,

        /// <summary>
        /// Left
        /// </summary>
        Left = 1,

        /// <summary>
        /// Top
        /// </summary>
        Top = 2,

        /// <summary>
        /// Right
        /// </summary>
        Right = 4,

        /// <summary>
        /// Bottom
        /// </summary>
        Bottom = 8,

        /// <summary>
        /// All
        /// </summary>
        All = (Top | Left | Bottom | Right),

        /// <summary>
        /// TopLeft
        /// </summary>
        TopLeft = (Top | Left),

        /// <summary>
        /// TopRight
        /// </summary>
        TopRight = (Top | Right),

        /// <summary>
        /// BottomLeft
        /// </summary>
        BottomLeft = (Bottom | Left),

        /// <summary>
        /// BottomRight
        /// </summary>
        BottomRight = (Bottom | Right),
    }

    #endregion

    #endregion

    #region EventArgs

    #region PostRenderGripBarEventArgs

    /// <summary>
    /// PostRenderGripBarEventArgs
    /// </summary>
    public class PostRenderGripBarEventArgs : EventArgs
    {
        #region Private variables

        private Graphics _Graphics;
        private Rectangle _Bounds;

        #endregion

        ///<summary>
        /// PostRenderGripBarEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="bounds"></param>
        public PostRenderGripBarEventArgs(Graphics graphics, Rectangle bounds)
        {
            _Graphics = graphics;
            _Bounds = bounds;
        }

        #region Public properties

        /// <summary>
        /// Gets the bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        #endregion
    }

    #endregion

    #region PreRenderGripBarEventArgs

    /// <summary>
    /// PreRenderGripBarEventArgs
    /// </summary>
    public class PreRenderGripBarEventArgs : PostRenderGripBarEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// PreRenderGripBarEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="bounds"></param>
        public PreRenderGripBarEventArgs(Graphics graphics, Rectangle bounds)
            : base(graphics, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether the operation is canceled
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #endregion

}
