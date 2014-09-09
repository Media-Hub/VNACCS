using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Imaging;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar
{
    [ToolboxItem(false)]
    public class RadialMenuPopup : Control
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RadialMenuPopup class.
        /// </summary>
        public RadialMenuPopup()
        {
            //this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Text = "RadialMenuPopup";
            //this.ShowInTaskbar = false;
            this.SetStyle(ControlStyles.Selectable, false);
            //this.StartPosition = FormStartPosition.Manual;
            //this.TopMost = true;
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
        }
        #endregion

        #region Implementation
        private bool _Painting = false;
        protected override void OnInvalidated(InvalidateEventArgs e)
        {
            if (!this.IsHandleCreated || this.IsDisposed || _DisplayItem == null || _Painting) return;
            _Painting = true;
            try
            {
                if (_DisplayItem.NeedRecalcSize)
                    _DisplayItem.RecalcSize();
                Bitmap oldContent = this.ContentImage;
                Bitmap b = null;
                bool clearBitmap = false;
                if (oldContent != null && oldContent.Width == _DisplayItem.WidthInternal && oldContent.Height == _DisplayItem.HeightInternal)
                {
                    b = oldContent;
                    clearBitmap = true;
                }
                else
                {
                    if (oldContent != null) oldContent.Dispose();
                    b = new Bitmap(_DisplayItem.WidthInternal, _DisplayItem.HeightInternal, PixelFormat.Format32bppArgb);
                }

                using (Graphics g = Graphics.FromImage(b))
                {
                    if (clearBitmap) g.Clear(Color.Transparent);
                    g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                    g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
                    ItemPaintArgs p = GetItemPaintArgs(g);
                    _DisplayItem.Paint(p);
                }
                this.ContentImage = b;
                //if (oldContent != null) oldContent.Dispose();
            }
            finally
            {
                _Painting = false;
            }
            base.OnInvalidated(e);
        }

        internal ItemPaintArgs GetItemPaintArgs(System.Drawing.Graphics g)
        {
            ItemPaintArgs pa = new ItemPaintArgs(this as IOwner, this, g, GetColorScheme());
            pa.Renderer = this.GetRenderer();
            if (_DisplayItem.DesignMode)
            {
                ISite site = this.GetSite();
                if (site != null && site.DesignMode)
                    pa.DesignerSelection = true;
            }
            pa.GlassEnabled = !this.DesignMode && WinApi.IsGlassEnabled;
            return pa;
        }
        private ISite GetSite()
        {
            ISite site = null;

            if (site == null && _DisplayItem != null)
            {
                BaseItem item = _DisplayItem;
                while (site == null && item != null)
                {
                    if (item.Site != null && item.Site.DesignMode)
                        site = item.Site;
                    else
                        item = item.Parent;
                }
            }

            return site;
        }

        private RadialMenuContainer _DisplayItem;
        /// <summary>
        /// Identifies the item displayed by this popup.
        /// </summary>
        public RadialMenuContainer DisplayItem
        {
            get { return _DisplayItem; }
            set
            {
                if (value != _DisplayItem)
                {
                    RadialMenuContainer oldValue = _DisplayItem;
                    _DisplayItem = value;
                    OnDisplayItemChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when DisplayItem property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnDisplayItemChanged(RadialMenuContainer oldValue, RadialMenuContainer newValue)
        {
            if (oldValue != null)
                oldValue.ContainerControl = null;
            if (newValue != null)
            {
                newValue.ContainerControl = this;
            }
            this.Invalidate();
            //OnPropertyChanged(new PropertyChangedEventArgs("DisplayItem"));
        }

        const uint WS_POPUP = 0x80000000;
        const uint WS_CLIPSIBLINGS = 0x04000000;
        const uint WS_CLIPCHILDREN = 0x02000000;
        const uint WS_EX_TOPMOST = 0x00000008;
        const uint WS_EX_TOOLWINDOW = 0x00000080;
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                //cp.ExStyle |= 0x00080000; // Sets WS_EX_LAYERED extended style

                cp.Style = unchecked((int)(WS_POPUP | WS_CLIPSIBLINGS | WS_CLIPCHILDREN));
                cp.ExStyle = (int)(WS_EX_TOPMOST | WS_EX_TOOLWINDOW | 0x00080000); // Sets WS_EX_LAYERED extended style
                cp.Caption = ""; // Setting caption would show window under tasks in Windows Task Manager

                return cp;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            PaintLayeredWindow(_ContentImage);
        }

        private Bitmap _ContentImage = null;
        /// <summary>
        /// Gets or sets the content image displayed on the window.
        /// </summary>
        public Bitmap ContentImage
        {
            get { return _ContentImage; }
            set
            {
                _ContentImage = value;
                if (_ContentImage != null && _ContentImage.Size != this.Size)
                    this.Size = _ContentImage.Size;
                PaintLayeredWindow(_ContentImage);
            }
        }

        private void PaintLayeredWindow(Bitmap windowContentImage)
        {
            if (!this.IsHandleCreated) return;

            IntPtr screenDc = NativeFunctions.GetDC(IntPtr.Zero);
            IntPtr memDc = WinApi.CreateCompatibleDC(screenDc);
            IntPtr hBitmap = IntPtr.Zero;
            IntPtr oldBitmap = IntPtr.Zero;

            try
            {
                //Display-image
                hBitmap = windowContentImage.GetHbitmap(Color.FromArgb(0));  //Set the fact that background is transparent
                oldBitmap = WinApi.SelectObject(memDc, hBitmap);

                //Display-rectangle
                Size size = windowContentImage.Size;
                Point pointSource = new Point(0, 0);
                Point topPos = new Point(this.Left, this.Top);

                //Set up blending options
                NativeFunctions.BLENDFUNCTION blend = new NativeFunctions.BLENDFUNCTION();
                blend.BlendOp = (byte)NativeFunctions.Win23AlphaFlags.AC_SRC_OVER;
                blend.BlendFlags = 0;
                blend.SourceConstantAlpha = 255;
                blend.AlphaFormat = (byte)NativeFunctions.Win23AlphaFlags.AC_SRC_ALPHA;

                NativeFunctions.UpdateLayeredWindow(this.Handle, screenDc, ref topPos, ref size,
                    memDc, ref pointSource, 0, ref blend, (int)NativeFunctions.Win32UpdateLayeredWindowsFlags.ULW_ALPHA);

                //Clean-up
                WinApi.ReleaseDC(IntPtr.Zero, screenDc);
                if (hBitmap != IntPtr.Zero)
                {
                    WinApi.SelectObject(memDc, oldBitmap);
                    WinApi.DeleteObject(hBitmap);
                }
                WinApi.DeleteDC(memDc);
            }
            catch (Exception)
            {
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WinApi.WindowsMessages.WM_MOUSEACTIVATE)
            {
                m.Result = new System.IntPtr(NativeFunctions.MA_NOACTIVATE);
                return;
            }
            base.WndProc(ref m);
        }

        #region BaseItem Forwarding
        protected override void OnClick(EventArgs e)
        {
            Point p = this.PointToClient(MousePosition);
            InternalOnClick(MouseButtons, p);
            base.OnClick(e);
        }

        protected virtual void InternalOnClick(MouseButtons mb, Point mousePos)
        {
            _DisplayItem.InternalClick(mb, mousePos);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            _DisplayItem.InternalDoubleClick(MouseButtons, MousePosition);
            base.OnDoubleClick(e);
        }

        protected override void OnKeyDown(KeyEventArgs e)
        {
            ExKeyDown(e);
            base.OnKeyDown(e);
        }

        internal virtual void ExKeyDown(KeyEventArgs e)
        {
            _DisplayItem.InternalKeyDown(e);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            _DisplayItem.InternalMouseDown(e);
            base.OnMouseDown(e);
        }

        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
            _DisplayItem.InternalMouseHover();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            _DisplayItem.InternalMouseLeave();
            base.OnMouseLeave(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _DisplayItem.InternalMouseUp(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            InternalMouseMove(e);
        }

        protected virtual void InternalMouseMove(MouseEventArgs e)
        {
            _DisplayItem.InternalMouseMove(e);
        }        
        #endregion

        #region Renderer Handling
        private Rendering.BaseRenderer m_DefaultRenderer = null;
        private Rendering.BaseRenderer m_Renderer = null;
        private eRenderMode m_RenderMode = eRenderMode.Global;
        /// <summary>
        /// Returns the renderer control will be rendered with.
        /// </summary>
        /// <returns>The current renderer.</returns>
        public virtual Rendering.BaseRenderer GetRenderer()
        {
            if (m_RenderMode == eRenderMode.Global && Rendering.GlobalManager.Renderer != null)
                return Rendering.GlobalManager.Renderer;
            else if (m_RenderMode == eRenderMode.Custom && m_Renderer != null)
                return m_Renderer;

            if (m_DefaultRenderer == null)
                m_DefaultRenderer = new Rendering.Office2007Renderer();

            return m_DefaultRenderer;
        }

        /// <summary>
        /// Gets or sets the rendering mode used by control. Default value is eRenderMode.Global which means that static GlobalManager.Renderer is used. If set to Custom then Renderer property must
        /// also be set to the custom renderer that will be used.
        /// </summary>
        [Browsable(false), DefaultValue(eRenderMode.Global)]
        public eRenderMode RenderMode
        {
            get { return m_RenderMode; }
            set
            {
                if (m_RenderMode != value)
                {
                    m_RenderMode = value;
                    this.Invalidate(true);
                }
            }
        }

        /// <summary>
        /// Gets or sets the custom renderer used by the items on this control. RenderMode property must also be set to eRenderMode.Custom in order renderer
        /// specified here to be used.
        /// </summary>
        [Browsable(false), DefaultValue(null)]
        public DevComponents.DotNetBar.Rendering.BaseRenderer Renderer
        {
            get
            {
                return m_Renderer;
            }
            set
            {
                m_Renderer = value;
            }
        }

        private ColorScheme m_ColorScheme = null;
        /// <summary>
        /// Returns the color scheme used by control. Color scheme for Office2007 style will be retrieved from the current renderer instead of
        /// local color scheme referenced by ColorScheme property.
        /// </summary>
        /// <returns>An instance of ColorScheme object.</returns>
        protected virtual ColorScheme GetColorScheme()
        {
            BaseRenderer r = GetRenderer();
            if (r is Office2007Renderer)
                return ((Office2007Renderer)r).ColorTable.LegacyColors;
            if (m_ColorScheme == null)
                m_ColorScheme = new ColorScheme(eDotNetBarStyle.Metro);
            return m_ColorScheme;
        }
        #endregion
        #endregion
    }
}
