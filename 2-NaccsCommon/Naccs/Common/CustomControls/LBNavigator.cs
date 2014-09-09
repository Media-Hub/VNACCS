namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(BindingNavigator)), DisplayName("Page Navigator")]
    public class LBNavigator : BindingNavigator, IRepetition
    {
        private int _repetitionMax = 1;
        private IContainer components;
        private string repetitionId = "";

        public LBNavigator()
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

        [Description("Max Number of Iteration"), Category("Service"), Localizable(true)]
        public int repetition_max
        {
            get
            {
                return this._repetitionMax;
            }
            set
            {
                this._repetitionMax = value;
            }
        }
    }
}

