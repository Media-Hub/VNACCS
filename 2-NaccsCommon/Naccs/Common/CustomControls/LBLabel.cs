namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [DisplayName("Label"), ToolboxBitmap(typeof(Label))]
    public class LBLabel : Label
    {
        public LBLabel()
        {
            this.AutoSize = true;
            base.UseCompatibleTextRendering = true;
            this.TextAlign = ContentAlignment.MiddleLeft;
            this.DoubleBuffered = true;
        }

        [DefaultValue(true)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                base.AutoSize = value;
            }
        }

        [DefaultValue(0x10)]
        public override ContentAlignment TextAlign
        {
            get
            {
                return base.TextAlign;
            }
            set
            {
                base.TextAlign = value;
            }
        }
    }
}

