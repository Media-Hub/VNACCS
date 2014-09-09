namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DisplayName("Page Navigator"), ToolboxBitmap(typeof(BindingNavigator))]
    public class LBNavigatorDesign : UserControl, IRepetition
    {
        private IContainer components;
        private PictureBox pictureBox1;
        private string repetitionId = "";
        private int repetitionMax = 1;

        public LBNavigatorDesign()
        {
            this.InitializeComponent();
            this.DoubleBuffered = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            ComponentResourceManager manager = new ComponentResourceManager(typeof(LBNavigatorDesign));
            this.pictureBox1 = new PictureBox();
            ((ISupportInitialize) this.pictureBox1).BeginInit();
            base.SuspendLayout();
//            this.pictureBox1.Image = (Image) manager.GetObject("pictureBox1.Image");
            this.pictureBox1.Location = new Point(0, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new Size(0xaf, 0x19);
            this.pictureBox1.SizeMode = PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
            this.AutoSize = true;
//            base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
            base.Controls.Add(this.pictureBox1);
            base.Name = "PageNavigator";
            base.Size = new Size(0xb2, 0x1c);
            ((ISupportInitialize) this.pictureBox1).EndInit();
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        [Category("Service"), Description("Iteration ID"), Localizable(true)]
        public string repetition_id
        {
            get
            {
                return this.repetitionId;
            }
            set
            {
                this.repetitionId = value;
            }
        }

        [Localizable(true), Category("Service"), Description("Max Number of Iteration")]
        public int repetition_max
        {
            get
            {
                return this.repetitionMax;
            }
            set
            {
                this.repetitionMax = value;
            }
        }
    }
}

