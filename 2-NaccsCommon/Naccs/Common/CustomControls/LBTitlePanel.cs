namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Windows.Forms;

    [DisplayName("TitlePanel"), ToolboxBitmap(typeof(Panel)), Designer(typeof(LBPanelDesigner), typeof(IDesigner))]
    public class LBTitlePanel : Panel, IContainerAttributes, IRepetition
    {
        private int _repetition_max = 1;
        private IContainer components;
        private bool isRan;
        private int max;
        private string repetitionId = "";
        private int repX = 1;
        private int repY = 1;
        private int spaceX;
        private int spaceY;

        public LBTitlePanel()
        {
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

        [Category("Service"), Description("Indicates whether this relates the iteration part."), Localizable(true)]
        public bool IsRan
        {
            get
            {
                return this.isRan;
            }
            set
            {
                this.isRan = value;
            }
        }

        [Description("The Number of Iteration in Container"), Localizable(true), Category("Service")]
        public int Max
        {
            get
            {
                return this.max;
            }
            set
            {
                this.max = value;
            }
        }

        [Localizable(true), Category("Service"), Description("Iteration ID")]
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
                return this._repetition_max;
            }
            set
            {
                this._repetition_max = value;
            }
        }

        [Description("The number of times to display (X direction)"), Category("Service"), Localizable(true)]
        public int RepX
        {
            get
            {
                return this.repX;
            }
            set
            {
                this.repX = value;
            }
        }

        [Localizable(true), Category("Service"), Description("The number of times to display (Y direction)")]
        public int RepY
        {
            get
            {
                return this.repY;
            }
            set
            {
                this.repY = value;
            }
        }

        [Description("Interval of display (X direction)"), Localizable(true), Category("Service")]
        public int SpaceX
        {
            get
            {
                return this.spaceX;
            }
            set
            {
                this.spaceX = value;
            }
        }

        [Category("Service"), Description("Interval of display (Y direction)"), Localizable(true)]
        public int SpaceY
        {
            get
            {
                return this.spaceY;
            }
            set
            {
                this.spaceY = value;
            }
        }
    }
}

