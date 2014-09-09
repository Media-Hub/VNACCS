namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [DisplayName("GStamp"), ToolboxBitmap(typeof(Panel))]
    public class LBGStamp : Control
    {
        private Size _boxSize = new Size(0x71, 0x57);
        public static PointF _dateText = new PointF(-1f, 20f);
        public static string _defaultFontStr = "Tahoma, 9pt";
        public const int _line1 = 0x13;
        public static Size _textSize = new Size(0x73, 20);
        public static PointF _topText = new PointF(-1f, 3f);
        public static PointF _underText1 = new PointF(-1f, 42f);
        public static PointF _underText2 = new PointF(-1f, 57f);
        public static PointF _underText3 = new PointF(-1f, 72f);
        private Naccs.Common.CustomControls.LineStyle lineStyle = new Naccs.Common.CustomControls.LineStyle();
        private Pen pen = new Pen(Color.Black);

        public LBGStamp()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.lineStyle.Color = this.pen.Color;
            this.lineStyle.Alignment = this.pen.Alignment;
            this.lineStyle.DashStyle = this.pen.DashStyle;
            this.lineStyle.Width = 0.5f;
            this.lineStyle.PropertyChanged += new EventHandler(this.lineStyle_PropertyChanged);
            base.Width = this._boxSize.Width;
            base.Height = this._boxSize.Height;
            this.Font = (Font) TypeDescriptor.GetConverter(this.Font).ConvertFromString(_defaultFontStr);
            this.DoubleBuffered = true;
            base.SetStyle(ControlStyles.UserPaint, true);
            base.SetStyle(ControlStyles.AllPaintingInWmPaint, true);
        }

        private void lineStyle_PropertyChanged(object sender, EventArgs e)
        {
            this.Refresh();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            this.pen.Color = this.lineStyle.Color;
            this.pen.Alignment = this.lineStyle.Alignment;
            if (this.lineStyle.DashStyle == DashStyle.Custom)
            {
                this.pen.DashStyle = DashStyle.Solid;
            }
            else
            {
                this.pen.DashStyle = this.lineStyle.DashStyle;
            }
            this.pen.Width = this.lineStyle.Width;
            base.Width = this._boxSize.Width;
            base.Height = this._boxSize.Height;
            int num = 1;
            e.Graphics.DrawRectangle(this.pen, 0, 0, base.Width - num, base.Height - num);
            e.Graphics.DrawLine(this.pen, 0, 0x13, base.Width, 0x13);
            e.Graphics.DrawLine(this.pen, 0, 0x26, base.Width, 0x26);
            Font font = new Font(this.Font.Name, this.Font.Size);
            SolidBrush brush = new SolidBrush(Color.Black);
            StringFormat format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.NoWrap);
           
            format.LineAlignment = StringAlignment.Center;
          
            try
            {
                e.Graphics.DrawString("領収印", font, brush, _topText);
                e.Graphics.DrawString("XX.XX.XX", font, brush, new RectangleF(new PointF(_dateText.X, _dateText.Y), (SizeF) _textSize), format);
                e.Graphics.DrawString("○\x00d7銀行", font, brush, _underText1);
                e.Graphics.DrawString("△△△△△△△支店", font, brush, _underText2);
                e.Graphics.DrawString("□□□□□□出張所", font, brush, _underText3);
            }
            finally
            {
                format.Dispose();
                brush.Dispose();
                font.Dispose();
            }
        }

        [Description("Style of line"), Category("Service")]
        public Naccs.Common.CustomControls.LineStyle LineStyle
        {
            get
            {
                if (this.lineStyle.DashStyle == DashStyle.Custom)
                {
                    this.lineStyle.DashStyle = DashStyle.Solid;
                }
                return this.lineStyle;
            }
            set
            {
                if (value.DashStyle == DashStyle.Custom)
                {
                    value.DashStyle = DashStyle.Solid;
                }
                this.lineStyle = value;
                this.lineStyle.PropertyChanged += new EventHandler(this.lineStyle_PropertyChanged);
                this.Refresh();
            }
        }
    }
}

