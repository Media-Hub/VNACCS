using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using DevComponents.DotNetBar.Metro.Rendering;
using System.Runtime.InteropServices;
using System.Diagnostics;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar.Controls;
using System.Reflection;

namespace DevComponents.DotNetBar.Metro
{
    public class MetroAppForm : Form
    {
        #region System Menu Constants
        private const string SysMenuRestore = "restore";
        private const string SysMenuMove = "move";
        private const string SysMenuSize = "size";
        private const string SysMenuMinimize = "minimize";
        private const string SysMenuMaximize = "maximize";
        private const string SysMenuClose = "close";
        private const string SysMenu = "sysMenu";
        #endregion

        #region Events
        /// <summary>
        /// Occurs before modal panel is shown and allows change of modal panel bounds.
        /// </summary>
        [Description("Occurs before modal panel is shown and allows change of modal panel bounds.")]
        public event ModalPanelBoundsEventHandler PrepareModalPanelBounds;
        /// <summary>
        /// Raises PrepareModalPanelBounds event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnPrepareModalPanelBounds(ModalPanelBoundsEventArgs e)
        {
            ModalPanelBoundsEventHandler handler = PrepareModalPanelBounds;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        private System.Windows.Forms.Padding GlassDefaultPadding = new System.Windows.Forms.Padding(0, 1, 1, 1);
        private System.Windows.Forms.Padding PlainDefaultPadding = new System.Windows.Forms.Padding(1, 1, 1, 1);
        
        /// <summary>
        /// Initializes a new instance of the MetroForm class.
        /// </summary>
        public MetroAppForm()
        {
            if (StyleManager.Style != eStyle.Metro)
                StyleManager.Style = eStyle.Metro;
            StyleManager.Register(this);
            _IsGlassEnabled = WinApi.IsGlassEnabled;
            this.ControlBox = false;
            //this.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            if (_IsGlassEnabled)
                this.Padding = GlassDefaultPadding;
            else
                this.Padding = PlainDefaultPadding;
            //_BorderOverlay = new BorderOverlay();
            //this.Controls.Add(_BorderOverlay);
            //UpdateBorderOverlay();
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing) StyleManager.Unregister(this);
            base.Dispose(disposing);
        }

        //private BorderOverlay _BorderOverlay = null;
        //private void UpdateBorderOverlay()
        //{
        //    if (_BorderOverlay == null) return;
        //    Rectangle bounds = new Rectangle(0, 0, this.Width, this.Height);
        //    if (_BorderOverlay.Bounds != bounds)
        //    {
        //        // Exclude Border
        //        Rectangle borderExcludedBounds = bounds;
        //        Thickness thickness = this.BorderThickness;
        //        if (thickness.IsZero)
        //        {
        //            thickness = MetroRender.GetColorTable().MetroForm.BorderThickness;
        //        }
        //        if (thickness.IsZero)
        //        {
        //            _BorderOverlay.Visible = false;
        //            return;
        //        }

        //        borderExcludedBounds.Width -= (int)thickness.Horizontal;
        //        borderExcludedBounds.Height -= (int)thickness.Vertical;
        //        borderExcludedBounds.X += (int)thickness.Left;
        //        borderExcludedBounds.Y += (int)thickness.Top;
        //        Region region = new Region(bounds);
        //        region.Exclude(borderExcludedBounds);
        //        if (!_BorderOverlay.Visible) _BorderOverlay.Visible = true;
        //        _BorderOverlay.Bounds = bounds;
        //        _BorderOverlay.Region = region;
        //        if (this.Controls.IndexOf(_BorderOverlay) != 0)
        //            _BorderOverlay.SendToBack();

        //    }
        //}

        /// <summary>
        /// Called by StyleManager to notify control that style on manager has changed and that control should refresh its appearance if
        /// its style is controlled by StyleManager.
        /// </summary>
        /// <param name="newStyle">New active style.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void StyleManagerStyleChanged(eDotNetBarStyle newStyle)
        {
            UpdateColorScheme();
        }

        private void UpdateColorScheme()
        {
            StyleManager.UpdateMetroAmbientColors(this);
        }
        protected override void OnShown(EventArgs e)
        {
            if (this.DesignMode)
                UpdateColorScheme();
            else if (_PreRenderShell && _MetroTab != null)
            {
                using (Bitmap bmp = new Bitmap(16, 16))
                    _MetroTab.DrawToBitmap(bmp, new Rectangle(0, 0, this.Width, this.Height));
            }
            base.OnShown(e);
        }
        private bool _PreRenderShell = true;
        /// <summary>
        /// Gets or sets whether MetroShell is pre-rendered when form is shown to make first rendering smoother. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool PreRenderShell
        {
            get { return _PreRenderShell; }
            set { _PreRenderShell = value; }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateColorScheme();
            base.OnHandleCreated(e);
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Gets whether at least one modal panel is displayed.
        /// </summary>
        [Browsable(false)]
        public bool IsModalPanelDisplayed
        {
            get
            {
                return _ModalPanels.Count > 0;
            }
        }

        private bool _ModalPanelBoundsExcludeStatusBar = false;
        /// <summary>
        /// Indicates whether modal panel when displayed shows MetroStatusBar.
        /// </summary>
        [DefaultValue(false), Category("Appearance"), Description("Indicates whether modal panel when displayed shows MetroStatusBar.")]
        public bool ModalPanelBoundsExcludeStatusBar
        {
            get { return _ModalPanelBoundsExcludeStatusBar; }
            set
            {
                _ModalPanelBoundsExcludeStatusBar = value;
            }
        }
        

        /// <summary>
        /// Shows the panel control in the center of the form and covers all non system controls making the panel effectively modal.
        /// </summary>
        /// <param name="panel">Control to show.</param>
        public void ShowModalPanel(Control panel)
        {
            ShowModalPanel(panel, false, eSlideSide.Left, SlidePanel.DefaultAnimationTime);
        }
        /// <summary>
        /// Shows the panel control in the center of the form by sliding it in from specified side and covers all non system controls making the panel effectively modal.
        /// </summary>
        /// <param name="panel">Panel to show.</param>
        /// <param name="slideFromSide">Side to slide panel into the view from.</param>
        public void ShowModalPanel(Control panel, eSlideSide slideFromSide)
        {
            ShowModalPanel(panel, true, slideFromSide, SlidePanel.DefaultAnimationTime);
        }
        /// <summary>
        /// Shows the panel control in the center of the form by sliding it in from specified side and covers all non system controls making the panel effectively modal.
        /// </summary>
        /// <param name="panel">Panel to show.</param>
        /// <param name="slideFromSide">Side to slide panel into the view from.</param>
        /// <param name="animationTimeMilliseconds">Slide animation speed in milliseconds.</param>
        public void ShowModalPanel(Control panel, eSlideSide slideFromSide, int animationTimeMilliseconds)
        {
            ShowModalPanel(panel, true, slideFromSide, SlidePanel.DefaultAnimationTime);
        }
        private List<Control> _ModalPanels = new List<Control>();
        private List<long> _DisabledItemIds = new List<long>();
        private void ShowModalPanel(Control panel, bool slideIn, eSlideSide slideFromSide, int animationTimeMilliseconds)
        {
            StyleManager.UpdateAmbientColors(panel);

            SlidePanel slidePanel = new SlidePanel();
            slidePanel.CenterContent = true;
            slidePanel.AnimationTime = animationTimeMilliseconds;
            slidePanel.SlideSide = slideFromSide;
            slidePanel.SlideOutButtonVisible = false;

            if (slideIn)
            {
                slidePanel.Bounds = GetModalPanelBounds();
                slidePanel.SlideOutOfViewSilent(this);
            }
            else
            {
                slidePanel.Bounds = GetModalPanelBounds();
            }

            slidePanel.Controls.Add(panel);
            this.Controls.Add(slidePanel);
            this.Controls.SetChildIndex(slidePanel, 0);
            _ModalPanels.Add(slidePanel);

            if (_ModalPanels.Count == 1 && _MetroTab != null)
            {
                //_MetroTab.MetroTabStrip.Enabled = false;
                _MetroTab.MetroTabStrip.StripContainerItem.Enabled = false;
                for (int i = 0; i < _MetroTab.MetroTabStrip.CaptionContainerItem.SubItems.Count; i++)
                {
                    BaseItem item = _MetroTab.MetroTabStrip.CaptionContainerItem.SubItems[i];
                    if (item.SystemItem && !(item is QatCustomizeItem)) continue;
                    item.Enabled = false;
                    _DisabledItemIds.Add(item.Id);
                }
                _MetroTab.MetroTabStrip.Invalidate();
            }

            this.Update();
            if (slideIn)
                slidePanel.IsOpen = true;

        }
        /// <summary>
        /// Hides the panel control that was previously shown using ShowModalPanel method.
        /// </summary>
        /// <param name="panel">Control to hide.</param>
        public void CloseModalPanel(Control panel)
        {
            CloseModalPanel(panel, false, eSlideSide.Left);
        }
        /// <summary>
        /// Hides the panel control that was previously shown using ShowModalPanel method by sliding it out of the view to the specified side.
        /// </summary>
        /// <param name="panel">Control to hide.</param>
        /// <param name="slideOutToSide">Side to slide control into.</param>
        public void CloseModalPanel(Control panel, eSlideSide slideOutToSide)
        {
            CloseModalPanel(panel, true, slideOutToSide);
        }
        private void CloseModalPanel(Control panel, bool slideOut, eSlideSide slideOutToSide)
        {
            SlidePanel slidePanel = null;
            foreach (Control modalPanel in _ModalPanels)
            {
                if (modalPanel.Contains(panel))
                {
                    slidePanel = (SlidePanel)modalPanel;
                    break;
                }
            }
            if (slidePanel == null) throw new ArgumentException("panel was not shown previously using ShowModalPanel method");

            if (slideOut)
            {
                slidePanel.SlideSide = slideOutToSide;
                slidePanel.IsOpen = false;
                DateTime start = DateTime.Now;
                while (slidePanel.IsAnimating)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(start).TotalMilliseconds > 950)
                    {
                        slidePanel.AbortAnimation();
                        slidePanel.Visible = false;
                        break;
                    }
                }
            }
            else
                slidePanel.Visible = false;
            slidePanel.Controls.Remove(panel);
            if (slidePanel.Parent != null)
                slidePanel.Parent.Controls.Remove(slidePanel);
            if (_ModalPanels.Contains(slidePanel))
                _ModalPanels.Remove(slidePanel);
            if(!slidePanel.IsDisposed)
                slidePanel.Dispose();

            if (_ModalPanels.Count == 0 && _MetroTab != null)
            {
                //_MetroTab.MetroTabStrip.Enabled = true;
                _MetroTab.MetroTabStrip.StripContainerItem.Enabled = true;
                for (int i = 0; i < _MetroTab.MetroTabStrip.CaptionContainerItem.SubItems.Count; i++)
                {
                    BaseItem item = _MetroTab.MetroTabStrip.CaptionContainerItem.SubItems[i];
                    if (item.SystemItem) continue;
                    if(_DisabledItemIds.Contains(item.Id))
                        item.Enabled = true;
                }

                _MetroTab.MetroTabStrip.Refresh();
            }
        }

        internal NonClientInfo GetNonClientInfo()
        {
            WinApi.RECT rect = new WinApi.RECT(0, 0, 200, 200);
            CreateParams params1 = this.CreateParams;
            WinApi.AdjustWindowRectEx(ref rect, params1.Style, false, params1.ExStyle);

            NonClientInfo n = new NonClientInfo();
            n.CaptionTotalHeight = Math.Abs(rect.Top);
            n.BottomBorder = rect.Height - 200 - n.CaptionTotalHeight;
            n.LeftBorder = Math.Abs(rect.Left);
            n.RightBorder = rect.Width - 200 - n.LeftBorder;

            return n;
        }

        private void UpdateModalPanelsSize()
        {
            foreach (Control modalPanel in _ModalPanels)
            {
                modalPanel.Bounds = GetModalPanelBounds();
                if (this.WindowState == FormWindowState.Maximized)
                {
                    NonClientInfo nci = GetNonClientInfo();
                    modalPanel.Padding = new System.Windows.Forms.Padding(nci.LeftBorder + 1);
                }
                else
                    modalPanel.Padding = new System.Windows.Forms.Padding();
            }
        }
        private Rectangle GetModalPanelBounds()
        {
            Thickness borderThickness = GetBorderThickness();

            int statusBarHeight = 0;
            if (_ModalPanelBoundsExcludeStatusBar)
            {
                foreach (Control item in this.Controls)
                {
                    if (item is MetroStatusBar && item.Dock == DockStyle.Bottom)
                    {
                        statusBarHeight = item.Height;
                        break;
                    }
                }
            }

            Rectangle bounds = Rectangle.Empty;
            if (_MetroTab != null && _MetroTab.CaptionVisible)
            {
                int captionHeight = _MetroTab.MetroTabStrip.GetCaptionHeight() + 2;
                bounds = new Rectangle((int)borderThickness.Left, captionHeight, this.Width - (int)borderThickness.Horizontal, this.Height - captionHeight - (int)borderThickness.Bottom - statusBarHeight);
            }
            else
            {
                bounds = new Rectangle((int)borderThickness.Left, (int)borderThickness.Top, this.Width - (int)borderThickness.Horizontal, this.Height - (int)borderThickness.Vertical - statusBarHeight);
            }

            ModalPanelBoundsEventArgs args = new ModalPanelBoundsEventArgs(bounds);
            OnPrepareModalPanelBounds(args);
            bounds = args.ModalPanelBounds;

            return bounds;
        }

        private bool _IsGlassEnabled = false;
        /// <summary>
        /// Returns whether Windows Glass effects are enabled.
        /// </summary>
        [Browsable(false)]
        public bool IsGlassEnabled
        {
            get
            {
                return _IsGlassEnabled;
            }
        }
        private void UpdateIsGlassEnabled()
        {
            bool glassEnabled = WinApi.IsGlassEnabled;
            if (glassEnabled != _IsGlassEnabled)
            {
                _IsGlassEnabled = glassEnabled;
                OnGlassEnabledChanged();
            }
        }
        private void OnGlassEnabledChanged()
        {
            if (_IsGlassEnabled)
                this.Padding = GlassDefaultPadding;
            else
                this.Padding = PlainDefaultPadding;
            if (_IsGlassEnabled && this.Region != null)
                this.Region = null;
            else
                this.Region = GetPlainFormRegion();
            this.Refresh();
        }
        private Region GetPlainFormRegion()
        {
            Region reg = new Region(new Rectangle(0, 0, this.Width, this.Height));
            return reg;
        }

        private MetroShell _MetroTab = null;
        /// <summary>
        /// Gets or sets the MetroTab that is hosted by this form. This property is for internal use only.
        /// </summary>
        [Browsable(false)]
        public virtual MetroShell MetroShell
        {
            get { return _MetroTab; }
            internal set
            {
                _MetroTab = value;
            }
        }

        protected override void OnStyleChanged(EventArgs e)
        {
            UpdateMetroSystemCaptionItem();
            base.OnStyleChanged(e);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (e.Control is MetroShell)
            {
                _MetroTab = e.Control as MetroShell;
                UpdateMetroSystemCaptionItem();
            }
            base.OnControlAdded(e);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            if (e.Control == _MetroTab)
            {
                _MetroTab = null;
            }
            base.OnControlRemoved(e);
        }

        private bool _Sizable = true;
        /// <summary>
        /// Gets or sets whether form can be resized.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether form can be resized.")]
        public bool Sizable
        {
            get { return _Sizable; }
            set { _Sizable = value; }
        }

        /// <summary>
        /// Gets effective Border Thickness for the form.
        /// </summary>
        /// <returns>Thickness</returns>
        public Thickness GetBorderThickness()
        {
            if (!_BorderThickness.IsZero) return _BorderThickness;
            MetroAppFormColorTable ct = GetFormColorTable();
            return this.IsGlassEnabled ? ct.BorderThickness : ct.BorderPlainThickness;
        }

        private MetroAppFormColorTable GetFormColorTable()
        {
            MetroColorTable ct = MetroRender.GetColorTable();
            return ct.MetroAppForm;
        }

        private Thickness _BorderThickness = new Thickness();
        /// <summary>
        /// Gets or sets the form border thickness. Default value is empty thickness which indicates that thickness is taken from MetroFormColorTable.
        /// </summary>
        [Category("Appearance"), Description("Indicates form border thickness.")]
        public Thickness BorderThickness
        {
            get { return _BorderThickness; }
            set
            {
                if (value != _BorderThickness)
                {
                    Thickness oldValue = _BorderThickness;
                    _BorderThickness = value;
                    OnBorderThicknessChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when BorderThickness property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnBorderThicknessChanged(Thickness oldValue, Thickness newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("BorderThickness"));
            this.Invalidate();
        }

        private BorderColors _BorderColor;
        /// <summary>
        /// Gets or sets the form border colors.
        /// </summary>
        public BorderColors BorderColor
        {
            get { return _BorderColor; }
            set
            {
                if (value != _BorderColor)
                {
                    BorderColors oldValue = _BorderColor;
                    _BorderColor = value;
                    OnBorderColorChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when BorderColor property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnBorderColorChanged(BorderColors oldValue, BorderColors newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("BorderColor"));
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (!_IsGlassEnabled && this.Region == null)
                this.Region = GetPlainFormRegion();
            MetroRender.Paint(this, e);
            base.OnPaint(e);
        }

        protected override void OnActivated(EventArgs e)
        {
            ExtendGlass();
            this.Invalidate();
            base.OnActivated(e);
        }

        protected override void OnDeactivate(EventArgs e)
        {
            this.Invalidate();
            base.OnDeactivate(e);
        }

        private void ExtendGlass()
        {
            if (!WinApi.IsGlassEnabled) return;

            WinApi.MARGINS m = new WinApi.MARGINS();
            m.cyTopHeight = 0;
            m.cxLeftWidth = 0;
            m.cxRightWidth = 0;
            m.cyBottomHeight = 1;
            WinApi.DwmExtendFrameIntoClientArea(this.Handle, ref m);
        }

        protected virtual bool WindowsMessageSetText(ref Message m)
        {
            if (WinApi.IsGlassEnabled) return true;

            bool modified = WinApi.ModifyHwndStyle(m.HWnd, (int)WinApi.WindowStyles.WS_VISIBLE, 0);

            base.DefWndProc(ref m);

            if (_MetroTab != null) _MetroTab.Refresh();
            if (modified)
            {
                WinApi.ModifyHwndStyle(m.HWnd, 0, (int)WinApi.WindowStyles.WS_VISIBLE);
            }
            return false;
        }
        protected virtual bool WindowsMessageWindowsPosChanged(ref Message m)
        {
            if (this.WindowState == FormWindowState.Minimized || this.WindowState == FormWindowState.Maximized)
            {
                _TrackSetBoundsCore = true;
                base.WndProc(ref m);
                _TrackSetBoundsCore = true;
                return false;
            }
            return true;
        }
        protected override void WndProc(ref Message m)
        {
            bool callBase = true;
            if (!PreProcessWndProc(ref m))
                return;

            if (m.Msg == (int)WinApi.WindowsMessages.WM_NCCALCSIZE)
            {
                callBase = WindowsMessageNCCalcSize(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_NCACTIVATE)
            {
                callBase = WindowsMessageNCActivate(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_DWMCOMPOSITIONCHANGED)
            {
                callBase = WindowsMessageDwmCompositionChanged(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_NCHITTEST)
            {
                callBase = WindowsMessageNCHitTest(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_SETTEXT)
            {
                callBase = WindowsMessageSetText(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_NCPAINT)
            {
                callBase = WindowsMessageNCPaint(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_ERASEBKGND)
            {
                if (!WinApi.IsGlassEnabled)
                {
                    callBase = false;
                    m.Result = new IntPtr(1);
                }
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_SETICON)
            {
                callBase = WindowsMessageSetIcon(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_SETTEXT)
            {
                callBase = WindowsMessageSetText(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_SETCURSOR)
            {
                callBase = WindowsMessageSetCursor(ref m);
            }
            else if (m.Msg == (int)WinApi.WindowsMessages.WM_WINDOWPOSCHANGED)
            {
                callBase = WindowsMessageWindowsPosChanged(ref m);
            }

            if (callBase)
                base.WndProc(ref m);
        }
        protected virtual bool WindowsMessageSetCursor(ref Message m)
        {
            // Button down on client area while there is a modal form displayed
            if (!_IsActive && (WinApi.HIWORD(m.LParam) == (int)WinApi.WindowsMessages.WM_LBUTTONUP || WinApi.HIWORD(m.LParam) == (int)WinApi.WindowsMessages.WM_LBUTTONDOWN) && WinApi.LOWORD(m.LParam) == (int)WinApi.WindowHitTestRegions.Error ||
                !this.Enabled || BarUtilities.IsModalFormOpen)
                return true;

            bool modified = WinApi.ModifyHwndStyle(m.HWnd, (int)WinApi.WindowStyles.WS_VISIBLE, 0);

            base.DefWndProc(ref m);

            if (_MetroTab != null && this.DesignMode) _MetroTab.Refresh();
            if (modified)
            {
                WinApi.ModifyHwndStyle(m.HWnd, 0, (int)WinApi.WindowStyles.WS_VISIBLE);
            }

            return false;
        }
        protected virtual bool WindowsMessageSetIcon(ref Message m)
        {
            bool modified = WinApi.ModifyHwndStyle(m.HWnd, (int)WinApi.WindowStyles.WS_VISIBLE, 0);

            base.DefWndProc(ref m);

            if (_MetroTab != null) _MetroTab.Refresh();
            if (modified)
            {
                WinApi.ModifyHwndStyle(m.HWnd, 0, (int)WinApi.WindowStyles.WS_VISIBLE);
            }

            return false;
        }

        protected virtual bool WindowsMessageNCPaint(ref Message m)
        {
            if (WinApi.IsGlassEnabled)
            {
                return true;
            }
            else
            {
                m.Result = IntPtr.Zero;
                return false;
            }
        }

        /// <summary>
        /// Called when WM_NCHITTEST message is received.
        /// </summary>
        /// <param name="m">Reference to message data.</param>
        /// <returns>Return true to call base form implementation otherwise return false.</returns>
        protected virtual bool WindowsMessageNCHitTest(ref Message m)
        {
            // Get position being tested...
            int x = WinApi.LOWORD(m.LParam);
            int y = WinApi.HIWORD(m.LParam);
            Point p = PointToClient(new Point(x, y));

            Thickness borderResize = FormResizeBorder;
            if (this.Sizable && !borderResize.IsZero && this.WindowState == FormWindowState.Normal)
            {
                Rectangle hitTest = new Rectangle(0, 0, (int)borderResize.Left, (int)borderResize.Top); hitTest.Inflate(1, 1);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TopLeftSizeableCorner);
                    return false;
                }
                hitTest = new Rectangle(this.Width - (int)borderResize.Right, 0, (int)borderResize.Right, (int)borderResize.Top); hitTest.Inflate(1, 1);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TopRightSizeableCorner);
                    return false;
                }
                hitTest = new Rectangle(this.Width - (int)borderResize.Right, this.Height - (int)borderResize.Bottom, (int)borderResize.Right, (int)borderResize.Bottom); hitTest.Inflate(2, 2);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.BottomRightSizeableCorner);
                    return false;
                }
                hitTest = new Rectangle(0, this.Height - (int)borderResize.Bottom, (int)borderResize.Left, (int)borderResize.Bottom); hitTest.Inflate(1, 1);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.BottomLeftSizeableCorner);
                    return false;
                }

                hitTest = new Rectangle(0, 0, (int)borderResize.Left, this.Height);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.LeftSizeableBorder);
                    return false;
                }
                hitTest = new Rectangle(0, 0, this.Width, (int)borderResize.Top);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TopSizeableBorder);
                    return false;
                }
                hitTest = new Rectangle(this.Width - (int)borderResize.Right, 0, (int)borderResize.Right, this.Height);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.RightSizeableBorder);
                    return false;
                }

                hitTest = new Rectangle(0, this.Height - (int)borderResize.Bottom, this.Width, (int)borderResize.Bottom);
                if (hitTest.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.BottomSizeableBorder);
                    return false;
                }
            }

            MetroShell tab = _MetroTab;
            if (tab != null)
            {
                Rectangle r = new Rectangle((RightToLeft == RightToLeft.No ? (int)borderResize.Left : this.Width - (int)borderResize.Right - 24), (int)borderResize.Left, 24, 24);
                if (r.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.Menu);
                    return false;
                }
            }

            if (BarFunctions.IsWindows7 && this.WindowState == FormWindowState.Maximized && tab != null && tab.CaptionVisible)
            {
                p = tab.MetroTabStrip.PointToClient(new Point(x, y));
                if (tab.MetroTabStrip.CaptionBounds.Contains(p))
                {
                    m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TitleBar);
                    return false;
                }
            }


            return true;
        }

        /// <summary>
        /// Called when WM_DWMCOMPOSITIONCHANGED message is received.
        /// </summary>
        /// <param name="m">Reference to message data.</param>
        /// <returns>Return true to call base form implementation otherwise return false.</returns>
        protected virtual bool WindowsMessageDwmCompositionChanged(ref Message m)
        {
            UpdateIsGlassEnabled();
            ExtendGlass();
            return true;
        }

        private bool _IsActive = false;
        /// <summary>
        /// Gets whether form is active.
        /// </summary>
        [Browsable(false)]
        public bool IsActive
        {
            get { return _IsActive; }
            internal set
            {
                if (_IsActive != value)
                {
                    _IsActive = value;
                    OnIsActiveChanged();
                }
            }
        }
        private void OnIsActiveChanged()
        {
            if (_MetroTab != null)
                _MetroTab.MetroTabStrip.Invalidate(_MetroTab.MetroTabStrip.CaptionBounds);
        }

        /// <summary>
        /// Called when WM_NCACTIVATE message is received.
        /// </summary>
        /// <param name="m">Reference to message data.</param>
        /// <returns>Return true to call base form implementation otherwise return false.</returns>
        protected virtual bool WindowsMessageNCActivate(ref Message m)
        {
            if (m.WParam != IntPtr.Zero)
                IsActive = true;
            else
                IsActive = false;

            m.Result = WinApi.DefWindowProc(m.HWnd, WinApi.WindowsMessages.WM_NCACTIVATE, m.WParam, new IntPtr(-1));
            if (this.DesignMode)
                RefreshAll();

            return false;
        }
        private void RefreshAll()
        {
            if (this.IsHandleCreated)
            {
                this.Invalidate(true);
                this.Refresh();
            }
        }

        protected override void OnTextChanged(EventArgs e)
        {
            if (this.DesignMode)
            {
                if (_MetroTab != null) _MetroTab.Refresh();
            }
            base.OnTextChanged(e);
        }

        private bool _Minimized = false;
        private bool _Maximized = false;
        private bool _ChangingSize = false;
        protected override void OnResize(EventArgs e)
        {
            if (!_IsGlassEnabled)
            {
                this.Region = GetPlainFormRegion();
                this.Invalidate();
            }

            UpdateModalPanelsSize();
            
            base.OnResize(e);
        }

        private bool _TrackSetBoundsCore = false;
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (_TrackSetBoundsCore ||
                this.DesignMode && ((specified & BoundsSpecified.Height) == BoundsSpecified.Height || (specified & BoundsSpecified.Width) == BoundsSpecified.Width))
            {
                if (width != this.Width || height != this.Height)
                {
                    AdjustBounds(ref width, ref height, specified);
                }
                _TrackSetBoundsCore = false;
            }
            //else if ((specified & BoundsSpecified.Width) == BoundsSpecified.None)
            //{
            //    specified |= BoundsSpecified.Width;
            //    AdjustBounds(ref width, ref height, specified);
            //}

            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected virtual void AdjustBounds(ref int width, ref int height)
        {
            AdjustBounds(ref width, ref height, BoundsSpecified.Width | BoundsSpecified.Height);
        }
        protected virtual void AdjustBounds(ref int width, ref int height, BoundsSpecified specified)
        {
            WinApi.RECT rect = new WinApi.RECT(0, 0, width, height);
            CreateParams params1 = this.CreateParams;
            WinApi.AdjustWindowRectEx(ref rect, params1.Style, false, params1.ExStyle);

            if ((specified & BoundsSpecified.Height) == BoundsSpecified.Height)
            {
                height -= (rect.Height - height);
                //if (IsGlassEnabled)
                //{
                //    if (this.FormBorderStyle == FormBorderStyle.FixedDialog)
                //        height += SystemInformation.FixedFrameBorderSize.Height;
                //    else if (this.FormBorderStyle == FormBorderStyle.Fixed3D)
                //        height += SystemInformation.FixedFrameBorderSize.Height;
                //    else if (this.FormBorderStyle == FormBorderStyle.FixedSingle)
                //        height += 1;
                //    else if (this.FormBorderStyle == FormBorderStyle.FixedToolWindow)
                //        height += SystemInformation.FixedFrameBorderSize.Height;
                //    else
                //        height += SystemInformation.FrameBorderSize.Height /* * NativeFunctions.BorderMultiplierFactor*/;
                //}
            }

            if ((specified & BoundsSpecified.Width) == BoundsSpecified.Width)
            {
                width -= (rect.Width - width);
            }
        }
        protected override void SetClientSizeCore(int x, int y)
        {
            if (!this.DesignMode)
            {
                this.Size = new Size(x, y);

                Type type = typeof(Control);
                try // try with ignore catch is bad idea but in this case its for future proofing, just in case M$ removes these members
                {
                    type.InvokeMember("clientWidth", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, this, new object[] { x });
                    type.InvokeMember("clientHeight", BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.SetField, null, this, new object[] { y });
                }
                catch { }
                OnClientSizeChanged(EventArgs.Empty);
                return;
            }
            base.SetClientSizeCore(x, y);
        }
        private FormWindowState GetWindowState()
        {
            WinApi.WINDOWPLACEMENT wp = new WinApi.WINDOWPLACEMENT();
            if (WinApi.GetWindowPlacement(this.Handle, out wp))
            {
                if (wp.showCmd == (int)WinApi.ShowWindowCommands.Maximize)
                    return FormWindowState.Maximized;
                else if (wp.showCmd == (int)WinApi.ShowWindowCommands.Minimize)
                    return FormWindowState.Minimized;
                else if (wp.showCmd == (int)WinApi.ShowWindowCommands.Normal)
                    return FormWindowState.Normal;
            }

            return this.WindowState;
        }
        /// <summary>
        /// Called when WM_NCCALCSIZE message is received.
        /// </summary>
        /// <param name="m">Message structure.</param>
        /// <returns>true to call base WndProc otherwise false.</returns>
        protected virtual bool WindowsMessageNCCalcSize(ref Message m)
        {
            // Default implementation of ribbon form does not have non-client area at all...
            if (m.WParam != IntPtr.Zero && GetWindowState() == FormWindowState.Maximized)
            {
                WinApi.NCCALCSIZE_PARAMS csp;
                csp = (WinApi.NCCALCSIZE_PARAMS)Marshal.PtrToStructure(m.LParam, typeof(WinApi.NCCALCSIZE_PARAMS));
                NonClientInfo nci = GetNonClientInfo();
                Size reduction = new Size(nci.LeftBorder, nci.BottomBorder); //SystemInformation.FrameBorderSize;
                Size frameBorderSize = reduction;
                if (reduction.Width > 2)
                    reduction.Width -= reduction.Width - 2;
                if (reduction.Height > 2)
                    reduction.Height -= reduction.Height - 2;

                WinApi.WINDOWPOS pos = (WinApi.WINDOWPOS)Marshal.PtrToStructure(csp.lppos, typeof(WinApi.WINDOWPOS));
                //Console.WriteLine(pos);
                WinApi.RECT newClientRect = WinApi.RECT.FromRectangle(new Rectangle(pos.x + frameBorderSize.Width - 1, pos.y + reduction.Height,
                    pos.cx - frameBorderSize.Width * 2 + 3, pos.cy - frameBorderSize.Height - 1));

                csp.rgrc0 = newClientRect;
                csp.rgrc1 = newClientRect;
                Marshal.StructureToPtr(csp, m.LParam, false);
                m.Result = new IntPtr((int)WinApi.WindowsMessages.WVR_VALIDRECTS);
                this.Invalidate();
                return false;
            }

            m.Result = IntPtr.Zero;
            this.Invalidate();
            return false;
        }
        private bool PreProcessWndProc(ref Message m)
        {
            return true;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
            {
                MetroShell metroTab = this.MetroShell;
                if (metroTab != null)
                {
                    MetroAppButton appButton = metroTab.GetApplicationButton() as MetroAppButton;
                    if (appButton != null)
                    {
                        if (appButton.ProcessEscapeKey(keyData))
                            return true;
                    }
                }
            }
            return base.ProcessDialogKey(keyData);
        }

        /// <summary>
        /// This property is not to be used with MetroForm.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public new FormBorderStyle FormBorderStyle
        {
            get { return base.FormBorderStyle; }
            set { base.FormBorderStyle = value; }
        }

        private Thickness _FormResizeBorder = new Thickness(5);
        /// <summary>
        /// Gets or sets the size of the border on the edges of the form that when mouse is over allow for form resizing.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Thickness FormResizeBorder
        {
            get { return _FormResizeBorder; }
            set { _FormResizeBorder = value; }
        }

        private void UpdateMetroSystemCaptionItem()
        {
            SystemCaptionItem captionItem = GetSystemCaptionItem();
            if (captionItem != null)
            {
                captionItem.MinimizeVisible = this.MinimizeBox;
                captionItem.RestoreMaximizeVisible = this.MaximizeBox;

                captionItem.HelpVisible = this.HelpButton;
                if (!this.MaximizeBox && !this.MinimizeBox && !this.CloseBoxVisible)
                    captionItem.Visible = false;
                else
                    captionItem.Visible = true;
                if (_MetroTab != null)
                    _MetroTab.RecalcLayout();
            }
        }
        private SystemCaptionItem GetSystemCaptionItem()
        {
            if (_MetroTab != null)
            {
                return _MetroTab.MetroTabStrip.SystemCaptionItem;
            }
            return null;
        }

        private bool _CloseBoxVisible = true;
        /// <summary>
        /// Indicates whether Close button in top-right corner of the form is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether Close button in top-right corner of the form is visible.")]
        public bool CloseBoxVisible
        {
            get { return _CloseBoxVisible; }
            set { _CloseBoxVisible = value; UpdateMetroSystemCaptionItem(); }
        }

        /// <summary>
        /// This property cannot be used on MetroAppForm
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override bool AutoScroll
        {
            get
            {
                return base.AutoScroll;
            }
            set
            {
                if (value) value = false;
                base.AutoScroll = value;
            }
        }
        /// <summary>
        /// This property cannot be used on MetroAppForm
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public override Point AutoScrollOffset
        {
            get
            {
                return base.AutoScrollOffset;
            }
            set
            {
                base.AutoScrollOffset = value;
            }
        }
        /// <summary>
        /// This property cannot be used on MetroAppForm
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMargin
        {
            get
            {
                return base.AutoScrollMargin;
            }
            set
            {
                base.AutoScrollMargin = value;
            }
        }
        /// <summary>
        /// This property cannot be used on MetroAppForm
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new Size AutoScrollMinSize
        {
            get
            {
                return base.AutoScrollMinSize;
            }
            set
            {
                base.AutoScrollMinSize = value;
            }
        }
 


 

        #endregion

        #region System Menu Handling
        private ContextMenuBar _ContextMenuBar = null;
        internal void ShowSystemWindowMenu(Point p)
        {
            if (this.DesignMode) return;
            CreateContextMenuBar();

            // Update items enabled state
            ButtonItem sysMenu = ((ButtonItem)_ContextMenuBar.Items[SysMenu]);
            sysMenu.SubItems[SysMenuRestore].Enabled = this.WindowState != FormWindowState.Normal;
            sysMenu.SubItems[SysMenuMove].Enabled = this.WindowState == FormWindowState.Normal;
            sysMenu.SubItems[SysMenuSize].Enabled = this.WindowState == FormWindowState.Normal && this.Sizable;
            sysMenu.SubItems[SysMenuMinimize].Enabled = this.WindowState != FormWindowState.Minimized && this.MinimizeBox; ;
            sysMenu.SubItems[SysMenuMaximize].Enabled = this.WindowState != FormWindowState.Maximized && this.Sizable && this.MaximizeBox; ;
            sysMenu.SubItems[SysMenuClose].Enabled = true;

            sysMenu.Popup(p);
        }
        internal void ShowSystemWindowMenu()
        {
            ShowSystemWindowMenu(Control.MousePosition);
        }

        private string GetLocalizedString(string key)
        {
            string text = "";
            if (key == LocalizationKeys.FormSystemMenuRestore)
                text = _SystemMenuRestore;
            else if (key == LocalizationKeys.FormSystemMenuMove)
                text = _SystemMenuMove;
            else if (key == LocalizationKeys.FormSystemMenuSize)
                text = _SystemMenuSize;
            else if (key == LocalizationKeys.FormSystemMenuMinimize)
                text = _SystemMenuMinimize;
            else if (key == LocalizationKeys.FormSystemMenuMaximize)
                text = _SystemMenuMaximize;
            else if (key == LocalizationKeys.FormSystemMenuClose)
                text = _SystemMenuClose;

            return LocalizationManager.GetLocalizedString(key, text);
        }
        private void CreateContextMenuBar()
        {
            if (_ContextMenuBar != null || this.DesignMode) return;
            _ContextMenuBar = new ContextMenuBar();
            _ContextMenuBar.Style = eDotNetBarStyle.StyleManagerControlled;
            this.Controls.Add(_ContextMenuBar);

            ButtonItem sysMenu = new ButtonItem(SysMenu);
            _ContextMenuBar.Items.Add(sysMenu);
            ButtonItem sysItem = new ButtonItem(SysMenuRestore, GetLocalizedString(LocalizationKeys.FormSystemMenuRestore));
            sysItem.Image = BarFunctions.LoadBitmap("SystemImages.WinRestore.png");
            sysItem.Click += new EventHandler(SysItemClick);
            sysMenu.SubItems.Add(sysItem);
            sysItem = new ButtonItem(SysMenuMove, GetLocalizedString(LocalizationKeys.FormSystemMenuMove));
            sysItem.Click += new EventHandler(SysItemClick);
            sysMenu.SubItems.Add(sysItem);
            sysItem = new ButtonItem(SysMenuSize, GetLocalizedString(LocalizationKeys.FormSystemMenuSize));
            sysItem.Click += new EventHandler(SysItemClick);
            sysMenu.SubItems.Add(sysItem);
            sysItem = new ButtonItem(SysMenuMinimize, GetLocalizedString(LocalizationKeys.FormSystemMenuMinimize));
            sysItem.Click += new EventHandler(SysItemClick);
            sysItem.Image = BarFunctions.LoadBitmap("SystemImages.WinMin.png");
            sysMenu.SubItems.Add(sysItem);
            sysItem = new ButtonItem(SysMenuMaximize, GetLocalizedString(LocalizationKeys.FormSystemMenuMaximize));
            sysItem.Click += new EventHandler(SysItemClick);
            sysItem.Image = BarFunctions.LoadBitmap("SystemImages.WinMax.png");
            sysMenu.SubItems.Add(sysItem);
            sysItem = new ButtonItem(SysMenuClose, GetLocalizedString(LocalizationKeys.FormSystemMenuClose));
            sysItem.Click += new EventHandler(SysItemClick);
            sysItem.Image = BarFunctions.LoadBitmap("SystemImages.WinClose.png");
            sysItem.BeginGroup = true;
            sysItem.FontBold = true;
            sysItem.HotFontBold = true;
            sysItem.Shortcuts.Add(eShortcut.CtrlF4);
            sysMenu.SubItems.Add(sysItem);
        }

        void SysItemClick(object sender, EventArgs e)
        {
            BaseItem item = sender as BaseItem;
            if (item == null) return;
            if (item.Name == SysMenuRestore)
                NativeFunctions.PostMessage(this.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_RESTORE, 0);
            else if (item.Name == SysMenuMove)
                NativeFunctions.PostMessage(this.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MOVE, 0);
            else if (item.Name == SysMenuSize)
                NativeFunctions.PostMessage(this.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_SIZE, 0);
            else if (item.Name == SysMenuMinimize)
                NativeFunctions.PostMessage(this.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MINIMIZE, 0);
            else if (item.Name == SysMenuMaximize)
                NativeFunctions.PostMessage(this.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MAXIMIZE, 0);
            else if (item.Name == SysMenuClose)
                NativeFunctions.PostMessage(this.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_CLOSE, 0);

        }

        private string _SystemMenuRestore = "Restore";
        /// <summary>
        /// Gets or sets text for form system menu Restore item.
        /// </summary>
        [DefaultValue("Restore"), Description("Indicates text for form system menu Restore item."), Localizable(true), Category("System Menu")]
        public string SystemMenuRestore
        {
            get { return _SystemMenuRestore; }
            set { _SystemMenuRestore = value; }
        }
        private string _SystemMenuMove = "Move";
        /// <summary>
        /// Gets or sets text for form system menu Move item.
        /// </summary>
        [DefaultValue("Move"), Description("Indicates text for form system menu Move item."), Localizable(true), Category("System Menu")]
        public string SystemMenuMove
        {
            get { return _SystemMenuMove; }
            set { _SystemMenuMove = value; }
        }
        private string _SystemMenuSize = "Size";
        /// <summary>
        /// Gets or sets text for form system menu Size item.
        /// </summary>
        [DefaultValue("Size"), Description("Indicates text for form system menu Size item."), Localizable(true), Category("System Menu")]
        public string SystemMenuSize
        {
            get { return _SystemMenuSize; }
            set { _SystemMenuSize = value; }
        }
        private string _SystemMenuMinimize = "Minimize";
        /// <summary>
        /// Gets or sets text for form system menu Minimize item.
        /// </summary>
        [DefaultValue("Minimize"), Description("Indicates text for form system menu Minimize item."), Localizable(true), Category("System Menu")]
        public string SystemMenuMinimize
        {
            get { return _SystemMenuMinimize; }
            set { _SystemMenuMinimize = value; }
        }
        private string _SystemMenuMaximize = "Maximize";
        /// <summary>
        /// Gets or sets text for form system menu Maximize item.
        /// </summary>
        [DefaultValue("Maximize"), Description("Indicates text for form system menu Maximize item."), Localizable(true), Category("System Menu")]
        public string SystemMenuMaximize
        {
            get { return _SystemMenuMaximize; }
            set { _SystemMenuMaximize = value; }
        }
        private string _SystemMenuClose = "Close";
        /// <summary>
        /// Gets or sets text for form system menu Close item.
        /// </summary>
        [DefaultValue("Close"), Description("Indicates text for form system menu Close item."), Localizable(true), Category("System Menu")]
        public string SystemMenuClose
        {
            get { return _SystemMenuClose; }
            set { _SystemMenuClose = value; }
        }
        #endregion
    }

    #region BorderOverlay
    internal class BorderOverlay : Control
    {
        /// <summary>
        /// Initializes a new instance of the BorderOverlay class.
        /// </summary>
        public BorderOverlay()
        {
            this.SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque |
                ControlStyles.ResizeRedraw | ControlStyles.DoubleBuffer | ControlStyles.Selectable, true);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            MetroRender.Paint(this, e);
            base.OnPaint(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WinApi.WindowsMessages.WM_NCHITTEST)
            {
                m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                return;
            }
            base.WndProc(ref m);
        }
    }
    #endregion

    #region PrepareModalPanelBounds Event
    /// <summary>
    /// Delegate for PrepareModalPanelBounds event.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void ModalPanelBoundsEventHandler(object sender, ModalPanelBoundsEventArgs e);
    /// <summary>
    /// Provides data for PrepareModalPanelBounds event.
    /// </summary>
    public class ModalPanelBoundsEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the bounds modal panel will occupy when shown.
        /// </summary>
        public Rectangle ModalPanelBounds = Rectangle.Empty;
        /// <summary>
        /// Initializes a new instance of the ModalPanelBoundsEventArgs class.
        /// </summary>
        /// <param name="modalPanelBounds"></param>
        public ModalPanelBoundsEventArgs(Rectangle modalPanelBounds)
        {
            ModalPanelBounds = modalPanelBounds;
        }
    }
    #endregion
}


