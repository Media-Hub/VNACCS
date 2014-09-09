namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(GroupBox)), DisplayName("Panel for Iteration"), Designer(typeof(LBRanDesigner), typeof(IDesigner))]
    public class LBRan : LBPanel
    {
        private IContainer components;
        private int count = 1;
        private int offset;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Pen pen = new Pen(Color.LightBlue);
            pen.DashStyle = DashStyle.Dash;
            pen.Alignment = PenAlignment.Inset;
            e.Graphics.DrawRectangle(pen, 0, 0, base.Width - 1, base.Height - 1);
        }

        [Description("Iteration Count"), Category("Service")]
        public int Count
        {
            get
            {
                return this.count;
            }
            set
            {
                this.count = value;
            }
        }

        [Category("Service"), Description("Start Number of Iteration(Offset)"), DefaultValue(0)]
        public int Offset
        {
            get
            {
                return this.offset;
            }
            set
            {
                this.offset = value;
            }
        }
    }
}

