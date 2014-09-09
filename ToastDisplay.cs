using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents toast display control. This class is not for public use.
    /// </summary>
    [ToolboxItem(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class ToastDisplay : Control
    {
        private const int GlowSize = 4;
        private readonly Size TextPadding = new Size(4, 4);

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the ToastDisplay class.
        /// </summary>
        public ToastDisplay()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.OptimizedDoubleBuffer
                        | ControlStyles.UserPaint
                        | ControlStyles.SupportsTransparentBackColor, true);
            this.ForeColor = Color.White;
        }
        protected override void Dispose(bool disposing)
        {
            if (_BackgroundImage != null)
            {
                _BackgroundImage.Dispose();
                _BackgroundImage = null;
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Implementation

        protected override void OnPaint(PaintEventArgs e)
        {
            BufferedGraphicsContext context = new BufferedGraphicsContext();

            using (Bitmap canvas = new Bitmap(this.Width, this.Height, e.Graphics))
            {
                Graphics g = Graphics.FromImage(canvas);
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

                Color glowColor = GetGlowColor(_GlowColor);
                Rectangle r = this.ClientRectangle;
                Color back = _ToastBackColor;

                r.Inflate(-3, -3);

                if (!glowColor.IsEmpty)
                {
                    for (int i = 0; i < GlowSize; i++)
                    {
                        using (Pen pen = new Pen(glowColor, 5))
                        {
                            pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                            g.DrawRectangle(pen, r);
                        }
                        r.Inflate(-1, -1);
                    }
                }

                r = this.ClientRectangle;
                r.Inflate(-GlowSize, -GlowSize);

                using (Pen pen = new Pen(_ToastBackColor, 5))
                {
                    pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                    r.Inflate(-3, -3);
                    g.DrawRectangle(pen, r);
                }
                DisplayHelp.FillRectangle(g, r, _ToastBackColor);

                r = this.ClientRectangle;
                r.Inflate(-(TextPadding.Width + GlowSize), -(TextPadding.Height + GlowSize));

                if (_Image != null)
                {
                    g.DrawImage(_Image, r.Location);
                    r.X += _Image.Width + TextPadding.Width;
                    r.Width -= _Image.Width + TextPadding.Width;
                }

                if (r.Width > 1)
                {
                    if (_TextMarkup == null)
                    {
                        eTextFormat format = TextFormat;
                        if (this.RightToLeft == RightToLeft.Yes) format |= eTextFormat.RightToLeft;
                        //TextDrawing.DrawString(g, Text, this.Font, this.ForeColor, r, format);
                        TextRenderer.DrawText(g, Text, Font, r, this.ForeColor, _ToastBackColor);
                    }
                    else
                    {
                        TextMarkup.MarkupDrawContext d = new TextMarkup.MarkupDrawContext(g, this.Font, this.ForeColor,
                            (this.RightToLeft == RightToLeft.Yes), r, true);
                        _TextMarkup.Arrange(r, d);
                        _TextMarkup.Render(d);
                    }
                }

                g.Dispose();

                if (_Alpha == 255)
                {
                    e.Graphics.DrawImage(canvas, 0, 0);
                }
                else
                {
                    ColorMatrix matrix = new ColorMatrix();
                    matrix.Matrix33 = _Alpha / 255f;
                    using (ImageAttributes imageAtt = new ImageAttributes())
                    {
                        imageAtt.SetColorMatrix(matrix);
                        e.Graphics.DrawImage(canvas, new Rectangle(0, 0, canvas.Width, canvas.Height), 0, 0, canvas.Width, canvas.Height, GraphicsUnit.Pixel, imageAtt);
                    }

                }
            }

            base.OnPaint(e);
        }
        internal Size DesiredSize(Padding margin, Rectangle parentBounds)
        {
            parentBounds.Width -= margin.Horizontal;
            parentBounds.Height -= margin.Vertical;
            parentBounds.Width -= GlowSize + TextPadding.Width * 2;
            parentBounds.Height -= GlowSize + TextPadding.Height * 2;

            Size desiredSize = new Size(GlowSize * 2 + TextPadding.Width * 2, GlowSize * 2 + TextPadding.Height * 2);

            using (Graphics g = this.CreateGraphics())
            {
                if (_TextMarkup != null)
                {
                    TextMarkup.MarkupDrawContext d = new TextMarkup.MarkupDrawContext(g, this.Font, Color.Black, (this.RightToLeft == RightToLeft.Yes));
                    _TextMarkup.Measure(new Size(parentBounds.Width, 1), d);
                    Size textSize = _TextMarkup.Bounds.Size;
                    _TextMarkup.Arrange(new Rectangle(Point.Empty, new Size(parentBounds.Width, 1)), d);
                    textSize = _TextMarkup.Bounds.Size;
                    if (_Image != null)
                    {
                        textSize.Width += _Image.Width + TextPadding.Width;
                        textSize.Height = Math.Max(_Image.Height, textSize.Height);
                    }
                    desiredSize.Width += textSize.Width;
                    desiredSize.Height += textSize.Height;
                }
                else
                {
                    //Size textSize = TextDrawing.MeasureString(g, Text, this.Font, parentBounds.Size, TextFormat);
                    Size textSize = TextRenderer.MeasureText(Text, this.Font, parentBounds.Size, TextFormatFlags.WordBreak);
                    if (_Image != null)
                    {
                        textSize.Width += _Image.Width + TextPadding.Width;
                        textSize.Height = Math.Max(_Image.Height, textSize.Height);
                    }
                    desiredSize.Width += textSize.Width;
                    desiredSize.Height += textSize.Height;
                }
            }

            return desiredSize;
        }

        private eTextFormat TextFormat
        {
            get
            {
                return eTextFormat.Default | eTextFormat.WordBreak | eTextFormat.VerticalCenter;
            }
        }
        private int _Alpha = 255;
        /// <summary>
        /// Specifies the alpha-blending, opacity for the content rendered by the control. Setting this property allows fading in and out of the control content.
        /// </summary>
        [System.Reflection.Obfuscation(Exclude = true, ApplyToMembers = false)]
        public int Alpha
        {
            get { return _Alpha; }
            set
            {
                if (value == _Alpha) return;
                if (value < 0) value = 0; else if (value > 255) value = 255;
                _Alpha = value;

                if (_Alpha == 0 && _BackgroundImage != null)
                {
                    _BackgroundImage.Dispose();
                    _BackgroundImage = null;
                }
                if (this.IsHandleCreated)
                {
                    this.Invalidate();
                }
            }
        }


        private Color GetGlowColor(eToastGlowColor glowColor)
        {
            if (glowColor == eToastGlowColor.Blue)
                return ColorScheme.GetColor(32, 0x0070C0); // Color.FromArgb(48, Color.Blue);
            else if (glowColor == eToastGlowColor.Green)
                return Color.FromArgb(32, Color.Green);
            else if (glowColor == eToastGlowColor.Orange)
                return Color.FromArgb(32, Color.Orange);
            else if (glowColor == eToastGlowColor.Red)
                return Color.FromArgb(32, Color.Red);
            else if (glowColor == eToastGlowColor.None)
                return Color.Empty;
            else if (glowColor == eToastGlowColor.Custom)
                return ToastNotification.CustomGlowColor;

            return Color.FromArgb(50, Color.Red);
        }

        private Image _BackgroundImage = null;
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            if (_BackgroundImage != null) // Background image is cached during animation to improve performance its disposed once animation is done
            {
                pevent.Graphics.DrawImage(_BackgroundImage, Point.Empty);
                if (_Alpha == 255)
                {
                    _BackgroundImage.Dispose();
                    _BackgroundImage = null;
                }
                return;
            }

            if (_Alpha != 255 && _BackgroundImage == null && this.Width > 0 && this.Height > 0) // Create cached background image to improve performance on subsequent rendering
            {
                _BackgroundImage = new Bitmap(this.Width, this.Height, pevent.Graphics);
                using (Graphics g = Graphics.FromImage(_BackgroundImage))
                {
                    PaintEventArgs pe = new PaintEventArgs(g, pevent.ClipRectangle);
                    base.OnPaintBackground(pe);
                    PaintParentControlOnBackground(g);
                }
                pevent.Graphics.DrawImage(_BackgroundImage, Point.Empty);
                return;
            }

            base.OnPaintBackground(pevent);

            PaintParentControlOnBackground(pevent.Graphics);
        }

        private void PaintParentControlOnBackground(Graphics g)
        {
            if (this.Parent != null)
            {
                for (int i = this.Parent.Controls.Count - 1; i >= 0; i--)
                {
                    Control ctrl = this.Parent.Controls[i];
                    if (!(ctrl is ToastDisplay) && ctrl.Visible && ctrl.Bounds.IntersectsWith(this.Bounds) && ctrl.Bounds.Width > 0 && ctrl.Bounds.Height > 0)
                    {
                        using (Bitmap bmp = new Bitmap(ctrl.Width, ctrl.Height, g))
                        {
                            ctrl.DrawToBitmap(bmp, ctrl.ClientRectangle);
                            g.TranslateTransform(ctrl.Left - Left, ctrl.Top - Top);
                            g.DrawImage(bmp, Point.Empty);
                            g.TranslateTransform(Left - ctrl.Left, Top - ctrl.Top);
                        }
                    }
                }
            }
        }
        private TextMarkup.BodyElement _TextMarkup = null;
        protected override void OnTextChanged(EventArgs e)
        {
            string text = this.Text;
            if (TextMarkup.MarkupParser.IsMarkup(ref text))
            {
                _TextMarkup = TextMarkup.MarkupParser.Parse(text);
            }
            else
                _TextMarkup = null;
            this.Invalidate();
            base.OnTextChanged(e);
        }

        private Color _ToastBackColor = Color.FromArgb(7, 7, 7);
        /// <summary>
        /// Gets or sets the toast background color.
        /// </summary>
        public Color ToastBackColor
        {
            get { return _ToastBackColor; }
            set { _ToastBackColor = value; }
        }

        private eToastGlowColor _GlowColor = eToastGlowColor.Blue;
        /// <summary>
        /// Gets or sets the toast glow color.
        /// </summary>
        public eToastGlowColor GlowColor
        {
            get { return _GlowColor; }
            set { _GlowColor = value; }
        }

        private Image _Image = null;
        /// <summary>
        /// Gets or sets the image displayed on the toast.
        /// </summary>
        public Image Image
        {
            get { return _Image; }
            set { _Image = value; this.Invalidate(); }
        }

        #endregion
    }
}
