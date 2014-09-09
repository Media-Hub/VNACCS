using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Rendering;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents the controller which allows single BaseItem to be hosted on the control.
    /// </summary>
    internal class BaseItemController : IDisposable
    {
        #region Constructor
        private BaseItem _Item = null;
        private Control _ParentControl = null;

        /// <summary>
        /// Initializes a new instance of the BaseItemController class.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="parentControl"></param>
        public BaseItemController(BaseItem item, Control parentControl)
        {
            _Item = item;
            _ParentControl = parentControl;
            _ParentControl.MouseDown += new MouseEventHandler(ParentControlMouseDown);
            _ParentControl.MouseEnter += new EventHandler(ParentControlMouseEnter);
            _ParentControl.MouseMove += new MouseEventHandler(ParentControlMouseMove);
            _ParentControl.MouseUp += new MouseEventHandler(ParentControlMouseUp);
            _ParentControl.MouseLeave += new EventHandler(ParentControlMouseLeave);
            _ParentControl.Paint += new PaintEventHandler(ParentControlPaint);
            _Item.ContainerControl = _ParentControl;
        }

        #endregion

        #region Implementation
        public BaseItem Item
        {
            get
            {
                return _Item;
            }
        }
        void ParentControlPaint(object sender, PaintEventArgs e)
        {
            if (_Item.NeedRecalcSize)
            {
                _Item.RecalcSize();
            }
            _Item.Displayed = true;
            Graphics g = e.Graphics;
            SmoothingMode sm = g.SmoothingMode;
            TextRenderingHint th = g.TextRenderingHint;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            g.TextRenderingHint = DisplayHelp.AntiAliasTextRenderingHint; 
            _Item.Paint(GetItemPaintArgs(g));
            g.SmoothingMode = sm;
            g.TextRenderingHint = th;
        }

        void ParentControlMouseLeave(object sender, EventArgs e)
        {
            if (_MouseOver)
            {
                _Item.InternalMouseLeave();
                _MouseOver = false;
            }
        }

        void ParentControlMouseUp(object sender, MouseEventArgs e)
        {
            _Item.InternalMouseUp(e);
        }

        void ParentControlMouseMove(object sender, MouseEventArgs e)
        {
            if (_Item.Bounds.Contains(e.Location))
            {
                if (!_MouseOver)
                {
                    _Item.InternalMouseEnter();
                    _MouseOver = true;
                }
                _Item.InternalMouseMove(e);
            }
            else if (_MouseOver)
            {
                _Item.InternalMouseLeave();
                _MouseOver = false;
            }
        }
        private bool _MouseOver = false;
        void ParentControlMouseEnter(object sender, EventArgs e)
        {
            Point p = _ParentControl.PointToClient(Control.MousePosition);

            if (_Item.Bounds.Contains(p))
                _Item.InternalMouseEnter();
        }

        void ParentControlMouseDown(object sender, MouseEventArgs e)
        {
            if (_Item.Bounds.Contains(e.Location))
                _Item.InternalMouseDown(e);
        }

        private ItemPaintArgs GetItemPaintArgs(Graphics g)
        {
            ItemPaintArgs pa = new ItemPaintArgs(null, _ParentControl, g, GetColorScheme());
            pa.Renderer = this.GetRenderer();
            pa.ButtonStringFormat = pa.ButtonStringFormat & ~(pa.ButtonStringFormat & eTextFormat.SingleLine);
            pa.ButtonStringFormat |= (eTextFormat.WordBreak | eTextFormat.EndEllipsis);
            return pa;
        }

        private ColorScheme GetColorScheme()
        {
            BaseRenderer r = GetRenderer();
            if (r is Office2007Renderer)
                return ((Office2007Renderer)r).ColorTable.LegacyColors;
            return new ColorScheme(eDotNetBarStyle.StyleManagerControlled);
        }
        private Rendering.BaseRenderer m_DefaultRenderer = null;
        private Rendering.BaseRenderer m_Renderer = null;
        private eRenderMode m_RenderMode = eRenderMode.Global;
        /// <summary>
        /// Returns the renderer control will be rendered with.
        /// </summary>
        /// <returns>The current renderer.</returns>
        public virtual Rendering.BaseRenderer GetRenderer()
        {
            return Rendering.GlobalManager.Renderer;
        }
        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            if (_ParentControl != null)
            {
                _ParentControl.MouseDown -= new MouseEventHandler(ParentControlMouseDown);
                _ParentControl.MouseEnter -= new EventHandler(ParentControlMouseEnter);
                _ParentControl.MouseMove -= new MouseEventHandler(ParentControlMouseMove);
                _ParentControl.MouseUp -= new MouseEventHandler(ParentControlMouseUp);
                _ParentControl.MouseLeave -= new EventHandler(ParentControlMouseLeave);
                _ParentControl.Paint -= new PaintEventHandler(ParentControlPaint);
                _Item.ContainerControl = null;
                _Item = null;
                _ParentControl = null;
            }
        }

        #endregion
    }
}
