namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(Panel)), DisplayName("Panel without Title"), Designer(typeof(LBPanelDesigner), typeof(IDesigner))]
    public class LBPanel : Panel, IRepContainerAttributes, IContainerAttributes, IRepetition
    {
        private int _repetition_max = 1;
        private IContainer components;
        private int currentPage;
        private int max;
        private string repetitionId = "";
        private int repX = 1;
        private int repY = 1;
        private int spaceX;
        private int spaceY;

        public LBPanel()
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

        [Category("Service"), Description("Current Page"), Localizable(true)]
        public int CurrentPage
        {
            get
            {
                return this.currentPage;
            }
            set
            {
                this.currentPage = value;
            }
        }

        [Category("Service"), Description("The Number of Iteration in Container"), Localizable(true)]
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

        [Description("Iteration ID"), Category("Service"), Localizable(true)]
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

        [Description("Max Number of Iteration"), Localizable(true), Category("Service")]
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

        [Description("The number of times to display (X direction)"), Localizable(true), Category("Service")]
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

        [Localizable(true), Description("The number of times to display (Y direction)"), Category("Service")]
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

        [Localizable(true), Description("Interval of display (X direction)"), Category("Service")]
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

        [Description("Interval of display (Y direction)"), Localizable(true), Category("Service")]
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

