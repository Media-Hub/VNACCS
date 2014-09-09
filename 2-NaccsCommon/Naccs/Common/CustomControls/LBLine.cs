namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [DisplayName("Line"), ToolboxBitmap(typeof(Panel))]
    public class LBLine : Control
    {
        private System.Drawing.Size lineSize = new System.Drawing.Size();
        private Naccs.Common.CustomControls.LineStyle lineStyle = new Naccs.Common.CustomControls.LineStyle();
        private Pen pen = new Pen(Color.Black);

        public LBLine()
        {
            base.Margin = new Padding(0);
            this.BackColor = Color.White;
            this.lineStyle.Color = this.pen.Color;
            this.lineStyle.Alignment = this.pen.Alignment;
            this.lineStyle.DashStyle = this.pen.DashStyle;
            this.lineStyle.Width = this.pen.Width;
            this.lineStyle.PropertyChanged += new EventHandler(this.lineStyle_PropertyChanged);
            this.lineSize.Height = base.Size.Height - 10;
            this.lineSize.Width = base.Size.Width - 10;
            base.SizeChanged += new EventHandler(this.LBLine_SizeChanged);
        }

        private void LBLine_SizeChanged(object sender, EventArgs e)
        {
            this.lineSize.Width = base.Size.Width - 10;
            this.lineSize.Height = base.Size.Height - 10;
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
            e.Graphics.DrawLine(this.pen, 0, 0, this.lineSize.Width, this.lineSize.Height);
        }

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

        public System.Drawing.Size Size
        {
            get
            {
                return this.lineSize;
            }
            set
            {
                this.lineSize = value;
                base.Size = new System.Drawing.Size(this.lineSize.Width + 10, this.lineSize.Height + 10);
            }
        }
    }
}

