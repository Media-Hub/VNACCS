namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Windows.Forms;

    [Designer(typeof(JobPanelDesigner), typeof(IRootDesigner)), ToolboxBitmap(typeof(Form)), DisplayName("Business Service Screen")]
    public class JobPanel : UserControl
    {
        private bool useDefaultSize;

        public JobPanel()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
        }

        private void InitializeComponent()
        {
            base.SuspendLayout();
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            this.Font = new Font("Tahoma", 9f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
            base.Name = "JobPanel";
            base.Size = new Size(0x2ce, 0x417);
            base.Paint += new PaintEventHandler(this.JobPanel_Paint);
            base.ResumeLayout(false);
        }

        private void JobPanel_Paint(object sender, PaintEventArgs e)
        {
            if (base.DesignMode)
            {
                int num = 0x402;
                if (base.Height > num)
                {
                    Pen pen = new Pen(Color.Blue);
                    pen.DashStyle = DashStyle.Dash;
                    for (int i = num; i < base.Height; i += num)
                    {
                        e.Graphics.DrawLine(pen, 0, i, base.Width, i);
                    }
                }
            }
        }

        [Category("Appearance"), Description("Set \"True\" if you use the size you set up as the prescribed value in the fixed part of the divide screen"), DefaultValue(false)]
        public bool UseDefaultSize
        {
            get
            {
                return this.useDefaultSize;
            }
            set
            {
                this.useDefaultSize = value;
            }
        }
    }
}

