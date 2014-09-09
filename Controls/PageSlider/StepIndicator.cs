using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar.Controls
{
    /// <summary>
    /// Represents control which visually indicates current step in a sequence.
    /// </summary>
    [ToolboxItem(true)]
    [ToolboxBitmap(typeof(StepIndicator), "Controls.StepIndicator.ico")]
    public class StepIndicator : Control
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the StepIndicator class.
        /// </summary>
        public StepIndicator()
        {
            this.SetStyle(ControlStyles.UserPaint |
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.Opaque |
                ControlStyles.ResizeRedraw |
                ControlStyles.DoubleBuffer |
                ControlStyles.StandardDoubleClick | ControlStyles.StandardClick, true);
            this.SetStyle(ControlStyles.Selectable, false);
        }
        #endregion

        #region Implementation
        protected override void OnPaint(PaintEventArgs e)
        {
            StepIndicatorColorTable colors = GetColors();
            Color backColor = colors.BackgroundColor;
            Color indicatorColor = colors.IndicatorColor;
            Graphics g = e.Graphics;
            Rectangle r = this.ClientRectangle;

            using (SolidBrush brush = new SolidBrush(backColor))
                g.FillRectangle(brush, r);

            int currentStep = GetCurrentStep() - 1;
            if (currentStep >= 0)
            {
                int stepCount = _StepCount;
                Rectangle indicatorBounds;
                if (_Orientation == eOrientation.Horizontal)
                {
                    int indicatorWidth = (int)Math.Ceiling((double)r.Width / stepCount);
                    indicatorBounds = new Rectangle(r.X + indicatorWidth * currentStep, r.Y, indicatorWidth, r.Height);
                }
                else
                {
                    int indicatorHeight = (int)Math.Ceiling((double)r.Height / stepCount);
                    indicatorBounds = new Rectangle(r.X, r.Y + indicatorHeight * currentStep, r.Width, indicatorHeight);
                }

                if (r.Width > 0 && r.Height > 0)
                {
                    using (SolidBrush brush = new SolidBrush(indicatorColor))
                        g.FillRectangle(brush, indicatorBounds);
                }
            }

            base.OnPaint(e);
        }

        private StepIndicatorColorTable GetColors()
        {
            if (!_IndicatorColor.IsEmpty && !_BackgroundColor.IsEmpty)
                return new StepIndicatorColorTable(_BackgroundColor, _IndicatorColor);
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                return ((Office2007Renderer)GlobalManager.Renderer).ColorTable.StepIndicator;
            }
            else
                return new StepIndicatorColorTable(Color.White, Color.MediumSeaGreen);
        }

        private int GetCurrentStep()
        {
            return Math.Max(0, Math.Min(_StepCount, _CurrentStep));
        }

        private int _StepCount = 10;
        /// <summary>
        /// Gets or sets the total number of steps that control will track. Default value is 10.
        /// </summary>
        [DefaultValue(10), Category("Behavior"), Description("Indicates total number of steps that control will track.")]
        public int StepCount
        {
            get { return _StepCount; }
            set
            {
                value = Math.Max(1, value);
                if (value != _StepCount)
                {
                    int oldValue = _StepCount;
                    _StepCount = value;
                    OnStepCountChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when StepCount property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnStepCountChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("StepCount"));
            this.Invalidate();
        }

        private int _CurrentStep = 1;
        /// <summary>
        /// Gets or sets the current step in sequence. Current step should be less or equal than StepCount.
        /// </summary>
        [DefaultValue(1), Category("Behavior"), Description("Indicates current step in sequence..")]
        public int CurrentStep
        {
            get { return _CurrentStep; }
            set
            {
                if (value != _CurrentStep)
                {
                    int oldValue = _CurrentStep;
                    _CurrentStep = value;
                    OnCurrentStepChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when CurrentStep property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnCurrentStepChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("CurrentStep"));
            this.Invalidate();
        }

        private static readonly Color DefaultBackgroundColor = Color.Empty;
        private Color _BackgroundColor = DefaultBackgroundColor;
        /// <summary>
        /// Gets or sets the background color of the control.
        /// </summary>
        [Category("Appearance"), Description("Indicates background color of control.")]
        public  Color BackgroundColor
        {
        	get {	return _BackgroundColor; }
        	set { _BackgroundColor = value;	}
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBackgroundColor()
        {
            return _BackgroundColor != DefaultBackgroundColor;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBackgroundColor()
        {
            this.BackgroundColor =  DefaultBackgroundColor;
        }

        private static readonly Color DefaultIndicatorColor = Color.Empty;
        private Color _IndicatorColor = DefaultIndicatorColor;
        /// <summary>
        /// Gets or sets the color of the current step indicator.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of current step indicator.")]
        public  Color IndicatorColor
        {
        	get {	return _IndicatorColor; }
        	set { _IndicatorColor = value;	}
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeIndicatorColor()
        {
            return _IndicatorColor != DefaultIndicatorColor;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetIndicatorColor()
        {
            this.IndicatorColor =  DefaultIndicatorColor;
        }

        private eOrientation _Orientation = eOrientation.Horizontal;
        /// <summary>
        /// Indicates the control orientation.
        /// </summary>
        [Category("Appearance"), DefaultValue(eOrientation.Horizontal), Description("Indicates the control orientation.")]
        public eOrientation Orientation
        {
            get { return _Orientation; }
            set
            {
                if (value != _Orientation)
                {
                    eOrientation oldValue = _Orientation;
                    _Orientation = value;
                    OnOrientationChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Orientation property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnOrientationChanged(eOrientation oldValue, eOrientation newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Orientation"));
            this.Invalidate();
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(400,4);
            }
        }

        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color BackColor
        {
            get
            {
                return base.BackColor;
            }
            set
            {
                base.BackColor = value;
            }
        }
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override System.Drawing.Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
            }
        }
        #endregion
    }
}
