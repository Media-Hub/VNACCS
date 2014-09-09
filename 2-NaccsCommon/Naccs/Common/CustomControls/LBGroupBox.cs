namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Windows.Forms;

    [Designer(typeof(LBPanelDesigner), typeof(IDesigner)), ToolboxBitmap(typeof(GroupBox)), DisplayName("GroupBox with Title")]
    public class LBGroupBox : GroupBox, IRepContainerAttributes, IContainerAttributes, IRepetition
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

        public LBGroupBox()
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

        [Localizable(true), Category("Service"), Description("Current Page")]
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

        [Localizable(true), Description("The Number of Iteration in Container"), Category("Service")]
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

        [Category("Service"), Localizable(true), Description("Iteration ID")]
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

        [Category("Service"), Localizable(true), Description("Max Number of Iteration")]
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

