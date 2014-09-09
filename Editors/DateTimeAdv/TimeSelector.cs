using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System.Drawing;
using System.Globalization;

namespace DevComponents.Editors.DateTimeAdv
{
    /// <summary>
    /// Represents a control that enables the user to select time using visual time display. 
    /// </summary>
    [ToolboxBitmap(typeof(DotNetBarManager), "TimeSelector.ico"), ToolboxItem(true), Designer(typeof(DevComponents.DotNetBar.Design.TimeSelectorDesigner))]
    [DefaultEvent("SelectedTimeChanged"), DefaultProperty("SelectedTime")]
    public class TimeSelector : ItemControl
    {
         #region Private Variables
        private TimeSelectorItem _TimeSelector = null;
        #endregion

        #region Events
        /// <summary>
        /// Occurs after SelectedTime changes.
        /// </summary>
        [Description("Occurs after SelectedTime changes.")]
        public event EventHandler SelectedTimeChanged;
        /// <summary>
        /// Raises SelectedTimeChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnSelectedTimeChanged(EventArgs e)
        {
            EventHandler handler = SelectedTimeChanged;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs when OK button is clicked.
        /// </summary>
        [Description("Occurs when OK button is clicked.")]
        public event EventHandler OkClick;
        /// <summary>
        /// Raises OkClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnOkClick(EventArgs e)
        {
            EventHandler handler = OkClick;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MonthCalendarAdv class.
        /// </summary>
        public TimeSelector()
        {
            this.SetStyle(ControlStyles.Selectable, false);

            _TimeSelector = new TimeSelectorItem();
            _TimeSelector.GlobalItem = false;
            _TimeSelector.ContainerControl = this;
            _TimeSelector.Stretch = false;
            _TimeSelector.Displayed = true;
            _TimeSelector.Style = eDotNetBarStyle.StyleManagerControlled;
            this.ColorScheme.Style = eDotNetBarStyle.StyleManagerControlled;
            _TimeSelector.SetOwner(this);

            _TimeSelector.SelectedTimeChanged += new EventHandler(TimeSelector_SelectedTimeChanged);
            _TimeSelector.OkClick += new EventHandler(TimeSelector_OkClick);
            this.SetBaseItemContainer(_TimeSelector);
        }
        #endregion

        #region Internal Implementation
        void TimeSelector_OkClick(object sender, EventArgs e)
        {
            OnOkClick(e);
        }

        void TimeSelector_SelectedTimeChanged(object sender, EventArgs e)
        {
            OnSelectedTimeChanged(e);
        }

        internal bool GetDesignModeInternal()
        {
            return DesignMode;
        }

        /// <summary>
        /// Gets or sets the text displayed on OK button.
        /// </summary>
        [DefaultValue("OK"), Category("Appearance"), Description("Indicates text displayed on OK button."), Localizable(true)]
        public string OkText
        {
            get { return _TimeSelector.OkText; }
            set { _TimeSelector.OkText = value; }
        }
        /// <summary>
        /// Gets or sets whether Ok button is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether Ok button is visible.")]
        public bool OkButtonVisible
        {
            get { return _TimeSelector.OkButtonVisible; }
            set
            {
                _TimeSelector.OkButtonVisible = value;
            }
        }
        /// <summary>
        /// Gets or sets the selected date time.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime SelectedDateTime
        {
            get
            {
                return _TimeSelector.SelectedDateTime;
            }
            set
            {
                _TimeSelector.SelectedDateTime = value;
            }
        }
        /// <summary>
        /// Gets or sets selected time. Returns TimeSpan.Zero if there is no time selected.
        /// </summary>
        [Category("Data"), Description("Indicates selected time.")]
        public TimeSpan SelectedTime
        {
            get { return _TimeSelector.SelectedTime; }
            set
            {
                _TimeSelector.SelectedTime = value;
            }
        }
        /// <summary>
        /// Returns whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSelectedTime()
        {
            return _TimeSelector.ShouldSerializeSelectedTime();
        }
        /// <summary>
        /// Resets property to default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetSelectedTime()
        {
            _TimeSelector.ResetSelectedTime();
        }
        /// <summary>
        /// Gets or sets the time format used to present time by the selector.
        /// </summary>
        [DefaultValue(eTimeSelectorFormat.System), Category("Appearance"), Description("Indicates time format used to present time by the selector."), Localizable(true)]
        public eTimeSelectorFormat TimeFormat
        {
            get { return _TimeSelector.TimeFormat; }
            set
            {
                _TimeSelector.TimeFormat = value;
                _PreferredSize = Size.Empty;
                AdjustSize();
            }
        }
        /// <summary>
        /// Gets or sets the format for the 12 Hour Time Display.
        /// </summary>
        [DefaultValue(TimeSelectorItem.DefaultTimeFormat12H), Category("Data"), Description("Indicates format for the 12 Hour Time Display."), Localizable(true)]
        public string TimeFormat12H
        {
            get { return _TimeSelector.TimeFormat12H; }
            set
            {
                _TimeSelector.TimeFormat12H = value;
            }
        }
        /// <summary>
        /// Gets or sets the format for the 24 Hour Time Display.
        /// </summary>
        [DefaultValue(TimeSelectorItem.DefaultTimeFormat24H), Category("Data"), Description("Indicates format for the 24 Hour Time Display."), Localizable(true)]
        public string TimeFormat24H
        {
            get { return _TimeSelector.TimeFormat24H; }
            set
            {
                _TimeSelector.TimeFormat24H = value;
            }
        }

        private Size _PreferredSize = Size.Empty;
        /// <summary>
        /// Invalidates control auto-size and resizes the control if AutoSize is set to true.
        /// </summary>
        public void InvalidateAutoSize()
        {
            _PreferredSize = Size.Empty;
            AdjustSize();
        }

        [Browsable(false)]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }
#if FRAMEWORK20
        /// <summary>
        /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents. You can set MaximumSize.Width property to set the maximum width used by the control.
        /// </summary>
        [Browsable(true), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                if (this.AutoSize != value)
                {
                    base.AutoSize = value;
                    AdjustSize();
                }
            }
        }

        private void AdjustSize()
        {
            if (this.AutoSize)
            {
                this.Size = base.PreferredSize;
            }
        }

        protected override void OnFontChanged(EventArgs e)
        {
            InvalidateAutoSize();
            base.OnFontChanged(e);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (!_PreferredSize.IsEmpty) return _PreferredSize;

            if (!BarFunctions.IsHandleValid(this))
                return base.GetPreferredSize(proposedSize);

            ElementStyle style = this.GetBackgroundStyle();

            _TimeSelector.RecalcSize();
            _PreferredSize = _TimeSelector.Size;
            if (style != null)
            {
                _PreferredSize.Width += ElementStyleLayout.HorizontalStyleWhiteSpace(style);
                _PreferredSize.Height += ElementStyleLayout.VerticalStyleWhiteSpace(style);
            }

            return _PreferredSize;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (this.AutoSize)
            {
                Size preferredSize = base.PreferredSize;
                width = preferredSize.Width;
                height = preferredSize.Height;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (this.AutoSize)
                this.AdjustSize();
            base.OnHandleCreated(e);
        }

        [Browsable(false)]
        public override bool ThemeAware
        {
            get
            {
                return base.ThemeAware;
            }
            set
            {
                base.ThemeAware = value;
            }
        }
#endif

        /// <summary>
        /// Indicates the type of the selector used to select time.
        /// </summary>
        [DefaultValue(eTimeSelectorType.TouchStyle), Category("Appearance"), Description("Indicates the type of the selector used to select time.")]
        public eTimeSelectorType SelectorType
        {
            get { return _TimeSelector.SelectorType; }
            set
            {
                _TimeSelector.SelectorType = value;
                _PreferredSize = Size.Empty;
                AdjustSize();
            }
        }
        /// <summary>
        /// Gets or sets the text displayed on Clear button only when MonthCalendarStyle is used.
        /// </summary>
        [DefaultValue("Clear"), Category("Appearance"), Description("Indicates text displayed on Clear button only when MonthCalendarStyle is used."), Localizable(true)]
        public string ClearText
        {
            get { return _TimeSelector.ClearText; }
            set { _TimeSelector.ClearText = value; }
        }

        /// <summary>
        /// Gets or sets the text displayed on Hour label.
        /// </summary>
        [DefaultValue("Hour"), Category("Appearance"), Description("Indicates text displayed on Hour label."), Localizable(true)]
        public string HourText
        {
            get { return _TimeSelector.HourText; }
            set { _TimeSelector.HourText = value; }
        }

        /// <summary>
        /// Gets or sets the text displayed on Minute label.
        /// </summary>
        [DefaultValue("Minute"), Category("Appearance"), Description("Indicates text displayed on Minute label."), Localizable(true)]
        public string MinuteText
        {
            get { return _TimeSelector.MinuteText; }
            set { _TimeSelector.MinuteText = value; }
        }
        #endregion

    }
}
