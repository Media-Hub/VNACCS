using System;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar.Controls
{
    [ToolboxItem(false)]
    public class ControlWithBackgroundStyle : Control
    {
        #region Private Variables
        private ElementStyle _BackgroundStyle = null;
        private bool _AntiAlias = true;
        #endregion

        #region Events
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ControlWithBackgroundStyle class.
        /// </summary>
        public ControlWithBackgroundStyle()
        {
            this.SetStyle(ControlStyles.UserPaint, true);
            this.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            this.SetStyle(ControlStyles.Opaque, true);
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            this.SetStyle(ControlStyles.StandardDoubleClick, true);
            this.SetStyle(DisplayHelp.DoubleBufferFlag, true);
            this.SetStyle(ControlStyles.SupportsTransparentBackColor, true);

            _BackgroundStyle = new ElementStyle();
            _BackgroundStyle.StyleChanged += new EventHandler(this.VisualPropertyChanged);
        }
        #endregion

        #region Internal Implementation
        /// <summary>
        /// Gets or sets whether anti-alias smoothing is used while painting. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Gets or sets whether anti-aliasing is used while painting.")]
        public virtual bool AntiAlias
        {
            get { return _AntiAlias; }
            set
            {
                if (_AntiAlias != value)
                {
                    _AntiAlias = value;
                    this.Invalidate();
                }
            }
        }

        /// <summary>
        /// Specifies the background style of the control.
        /// </summary>
        [Category("Style"), Description("Indicates control background style."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ElementStyle BackgroundStyle
        {
            get { return _BackgroundStyle; }
        }

        /// <summary>
        /// Resets style to default value. Used by windows forms designer.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBackgroundStyle()
        {
            _BackgroundStyle.StyleChanged -= new EventHandler(this.VisualPropertyChanged);
            _BackgroundStyle = new ElementStyle();
            _BackgroundStyle.StyleChanged += new EventHandler(this.VisualPropertyChanged);
            this.Invalidate();
        }

        private void VisualPropertyChanged(object sender, EventArgs e)
        {
            OnVisualPropertyChanged();
        }

        protected virtual void OnVisualPropertyChanged()
        {
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.BackColor.IsEmpty || this.BackColor == Color.Transparent)
            {
                base.OnPaintBackground(e);
            }

            PaintBackground(e);
            PaintContent(e);

            base.OnPaint(e);
        }

        protected virtual void PaintContent(PaintEventArgs e)
        {
            
        }

        protected virtual Rectangle GetContentRectangle()
        {
            bool disposeStyle = false;
            ElementStyle style = this.GetBackgroundStyle(out disposeStyle);
            Rectangle r = ElementStyleLayout.GetInnerRect(style, this.ClientRectangle);
            if (disposeStyle) style.Dispose();
            //Rectangle r=this.ClientRectangle;
            //r.X += ElementStyleLayout.LeftWhiteSpace(style);
            //r.Y += ElementStyleLayout.TopWhiteSpace(style);
            //r.Width -= ElementStyleLayout.HorizontalStyleWhiteSpace(style);
            //r.Height -= ElementStyleLayout.VerticalStyleWhiteSpace(style);

            return r;
        }

        protected virtual ElementStyle GetBackgroundStyle(out bool disposeStyle)
        {
            disposeStyle = false;
            return ElementStyleDisplay.GetElementStyle(_BackgroundStyle, out disposeStyle);
        }

        protected virtual void PaintBackground(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            Rectangle r = this.ClientRectangle;
            bool disposeStyle = false;
            ElementStyle style = this.GetBackgroundStyle(out disposeStyle);

            if (!this.BackColor.IsEmpty)
            {
                DisplayHelp.FillRectangle(g, r, this.BackColor);
            }

            if (style.Custom)
            {
                SmoothingMode sm = g.SmoothingMode;
                if (this.AntiAlias)
                    g.SmoothingMode = SmoothingMode.HighQuality;
                ElementStyleDisplayInfo displayInfo = new ElementStyleDisplayInfo(style, e.Graphics, r);
                ElementStyleDisplay.Paint(displayInfo);
                if (this.AntiAlias)
                    g.SmoothingMode = sm;
            }

            if (disposeStyle) style.Dispose();
        }
        #endregion

    }
}
