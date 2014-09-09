namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Threading;

    [Description("Line Style"), TypeConverter(typeof(LineStyleConverter))]
    public class LineStyle
    {
        private PenAlignment alignment;
        private System.Drawing.Color color;
        private System.Drawing.Drawing2D.DashStyle dashStyle;
        private float width;

        public event EventHandler PropertyChanged;

        [Description("Center:Centered over the theoretical line\nInset :Positioned on the inside of the theoretical line\nLeft  :Positioned to the left of the theoretical line\nOutset: Positioned on the outside of the theoretical line\nRight :Positioned to the right of the theoretical line")]
        public PenAlignment Alignment
        {
            get
            {
                return this.alignment;
            }
            set
            {
                this.alignment = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, null);
                }
            }
        }

        [Description("Line Color")]
        public System.Drawing.Color Color
        {
            get
            {
                return this.color;
            }
            set
            {
                this.color = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, null);
                }
            }
        }

        [Description("Dash :Consisting of dashes\nDashDot :Consisting of a repeating pattern of dash-dot\nDashDotDot :Consisting of a repeating pattern of dash-dot-dot\nDot :Consisting of dots\nSolid :Solid line")]
        public System.Drawing.Drawing2D.DashStyle DashStyle
        {
            get
            {
                return this.dashStyle;
            }
            set
            {
                this.dashStyle = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, null);
                }
            }
        }

        [Description("Line Width")]
        public float Width
        {
            get
            {
                return this.width;
            }
            set
            {
                this.width = value;
                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, null);
                }
            }
        }
    }
}

