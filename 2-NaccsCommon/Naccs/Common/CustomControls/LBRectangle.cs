namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(Panel)), DisplayName("Rectangle")]
    public class LBRectangle : Control
    {
        private Naccs.Common.CustomControls.LineStyle lineStyle = new Naccs.Common.CustomControls.LineStyle();
        private Pen pen = new Pen(Color.Black);

        public LBRectangle()
        {
            base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            this.BackColor = Color.Transparent;
            this.lineStyle.Color = this.pen.Color;
            this.lineStyle.Alignment = this.pen.Alignment;
            this.lineStyle.DashStyle = this.pen.DashStyle;
            this.lineStyle.Width = this.pen.Width;
            this.lineStyle.PropertyChanged += new EventHandler(this.lineStyle_PropertyChanged);
            this.DoubleBuffered = true;
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
            int num = 1;
            e.Graphics.DrawRectangle(this.pen, 0, 0, base.Width - num, base.Height - num);
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

