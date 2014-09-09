#if FRAMEWORK20
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar;

namespace DevComponents.Editors
{
    public class VisualLabel : VisualItem
    {
        #region Private Variables
        #endregion

        #region Events
        #endregion

        #region Constructor
        #endregion

        #region Internal Implementation
        private Padding _TextPadding = new Padding(0);
        /// <summary>
        /// Gets or sets the amount of padding added to text.
        /// </summary>
        public Padding TextPadding
        {
            get { return _TextPadding; }
            set { _TextPadding = value; InvalidateArrange(); }
        }

        private string _Text = "";
        /// <summary>
        /// Gets or sets the text displayed by the label.
        /// </summary>
        public string Text
        {
            get { return _Text; }
            set
            {
                if (value == null) value = "";
                if (_Text != value)
                {
                    _Text = value;
                    InvalidateArrange();
                }
            }
        }

        public override void PerformLayout(PaintInfo p)
        {
            Size size = Size.Empty;
            Graphics g = p.Graphics;
            Font font = p.DefaultFont;
            eTextFormat textFormat = eTextFormat.Default | eTextFormat.GlyphOverhangPadding | eTextFormat.LeftAndRightPadding;
            if (_Text.Length > 0)
            {
                if (Text.Trim().Length == 0)
                {
                    size = TextDrawing.MeasureString(g, Text.Replace(' ', '-'), font, 0, textFormat);
                    size.Width = (int)(size.Width);
                }
                else
                    size = TextDrawing.MeasureString(g, Text, font, 0, textFormat);
            }
            size.Height += _TextPadding.Vertical;
            size.Width += _TextPadding.Horizontal;
            this.Size = size;
            base.PerformLayout(p);
        }

        protected override void OnPaint(PaintInfo p)
        {

            Graphics g = p.Graphics;
            Font font = p.DefaultFont;
            Rectangle r = this.RenderBounds;
            eTextFormat textFormat = eTextFormat.Default | eTextFormat.NoPadding;
            Color color = p.ForeColor;
            r.X += _TextPadding.Left;
            r.Y += _TextPadding.Top;
            if (!GetIsEnabled(p))
                color = p.DisabledForeColor;
            if (_Text.Length > 0)
                TextDrawing.DrawString(g, _Text, font, color, r, textFormat);

            base.OnPaint(p);
        }
        #endregion
    }
}
#endif

