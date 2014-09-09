namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(NumericUpDown)), DisplayName("Iteration Number")]
    public class RanLabel : Label
    {
        private int _figure;
        private IContainer components;
        private string format = "00";
        private bool padLeft = true;
        private string repetitionId = "";
        private int value;

        public RanLabel()
        {
            base.Text = "00";
            this.AutoSize = true;
            this.figure = 2;
            base.ParentChanged += new EventHandler(this.RanLabel_ParentChanged);
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

        private void RanLabel_ParentChanged(object sender, EventArgs e)
        {
            if (base.Parent == null)
            {
                this.repetition_id = "";
            }
            else
            {
                for (Control control = base.Parent; control != null; control = control.Parent)
                {
                    IContainerAttributes attributes = control as IContainerAttributes;
                    if ((attributes != null) && (attributes.repetition_id.Length > 0))
                    {
                        this.repetition_id = attributes.repetition_id;
                        break;
                    }
                }
                if (this.repetition_id.Length == 0)
                {
                    throw new Exception(Resources.ResourceManager.GetString("COM11"));
                }
            }
        }

        private void SetText(int iValue)
        {
            if (this.padLeft)
            {
                base.Text = iValue.ToString(this.format);
            }
            else
            {
                base.Text = iValue.ToString();
            }
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

        [Category("Service"), Localizable(true), Browsable(true), Description("Digit Number of Zero Padding")]
        public int figure
        {
            get
            {
                return this._figure;
            }
            set
            {
                if (value > 0)
                {
                    this._figure = value;
                    this.format = "";
                    for (int i = 0; i < this._figure; i++)
                    {
                        this.format = this.format + "0";
                    }
                    this.SetText(this.Value);
                }
            }
        }

        [Localizable(true), Description("Zero Padding"), Browsable(true), Category("Service")]
        public bool PadLeft
        {
            get
            {
                return this.padLeft;
            }
            set
            {
                this.padLeft = value;
                this.SetText(this.Value);
            }
        }

        [Localizable(true), Category("Service"), Browsable(true), Description("Iteration ID")]
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

        public string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
            }
        }

        [Browsable(true), Description("Iteration number"), Localizable(true), Category("Service")]
        public int Value
        {
            get
            {
                return this.value;
            }
            set
            {
                this.value = value;
                this.SetText(this.Value);
            }
        }
    }
}

