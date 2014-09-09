using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using DevComponents.DotNetBar;

namespace DevComponents.Editors.DateTimeAdv
{
    /// <summary>
    /// Represents the Time selector item.
    /// </summary>
    public class TimeSelectorItem : BaseItem
    {
        #region Constructor
        private Command _HourCommand = null;
        private Command _MinuteCommand = null;
        private Command _MinuteChangeCommand = null;
        private Command _ClearCommand = null;
        private Command _AmPmCommand = null;
        private Command _OkCommand = null;
        private Font _DualButtonFont = null;
        private ItemContainer _InnerContainer = null;
        private LabelItem _AmPmLabel = null;
        private LabelItem _HourLabel = null, _MinuteLabel = null;
        /// <summary>
        /// Initializes a new instance of the TimeSelectionItem class.
        /// </summary>
        public TimeSelectorItem()
        {
            m_IsContainer = true;
            this.AutoCollapseOnClick = false;

            _InnerContainer = new ItemContainer();
            _InnerContainer.ItemSpacing = 3;
            _InnerContainer.LayoutOrientation = eOrientation.Vertical;
            _InnerContainer.AutoCollapseOnClick = false;
            this.SubItems.Add(_InnerContainer);

            _HourCommand = new Command();
            _HourCommand.Executed += new EventHandler(HourCommandExecuted);
            _MinuteCommand = new Command();
            _MinuteCommand.Executed += new EventHandler(MinuteCommandExecuted);
            _MinuteChangeCommand = new Command();
            _MinuteChangeCommand.Executed += new EventHandler(MinuteChangeCommandExecuted);
            _ClearCommand = new Command();
            _ClearCommand.Executed += new EventHandler(ClearCommandExecuted);
            _AmPmCommand = new Command();
            _AmPmCommand.Executed += new EventHandler(AmPmCommandExecuted);
            _OkCommand = new Command();
            _OkCommand.Executed += new EventHandler(OkCommandExecuted);

            RecreateItems();
        }

        protected override void Dispose(bool disposing)
        {
            if (_HourCommand != null)
            {
                _HourCommand.Dispose();
                _HourCommand = null;
            }
            if (_MinuteCommand != null)
            {
                _MinuteCommand.Dispose();
                _MinuteCommand = null;
            }
            if (_MinuteChangeCommand != null)
            {
                _MinuteChangeCommand.Dispose();
                _MinuteChangeCommand = null;
            }
            if (_ClearCommand != null)
            {
                _ClearCommand.Dispose();
                _ClearCommand = null;
            }
            if (_AmPmCommand != null)
            {
                _AmPmCommand.Dispose();
                _AmPmCommand = null;
            }
            if (_OkCommand != null)
            {
                _OkCommand.Dispose();
                _OkCommand = null;
            }
            if (_DualButtonFont != null)
            {
                _DualButtonFont.Dispose();
                _DualButtonFont = null;
            }
            base.Dispose(disposing);
        }
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

        #region Implementation
        public override void RecalcSize()
        {
            _InnerContainer.SetDisplayRectangle(new Rectangle(m_Rect.X, m_Rect.Y, 300, 300));

            if (_SelectorType == eTimeSelectorType.MonthCalendarStyle) // Adjust button size based on selected font
            {
            }

            _InnerContainer.Displayed = true;
            _InnerContainer.RecalcSize();
            m_Rect.Width = _InnerContainer.WidthInternal;
            m_Rect.Height = _InnerContainer.HeightInternal;

            base.RecalcSize();
        }

        public override void Paint(ItemPaintArgs p)
        {
            _InnerContainer.Paint(p);
        }

        public override BaseItem Copy()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        private void OkCommandExecuted(object sender, EventArgs e)
        {
            OnOkClick(EventArgs.Empty);
        }
        private void AmPmCommandExecuted(object sender, EventArgs e)
        {
            bool isAm = false;

            if (_SelectorType == eTimeSelectorType.TouchStyle)
            {
                if (sender == _ButtonAm)
                {
                    if (_ButtonPm != null)
                        _ButtonPm.Checked = false;
                    _ButtonAm.Checked = true;
                    isAm = true;
                }
                else
                {
                    if (_ButtonAm != null)
                        _ButtonAm.Checked = false;
                    _ButtonPm.Checked = true;
                }
            }
            else
            {
                if (_SelectedTime != TimeSpan.Zero && _SelectedTime.Hours >= 12)
                    isAm = true;
            }

            if (isAm)
            {
                if (_SelectedTime != TimeSpan.Zero && _SelectedTime.Hours >= 12)
                {
                    int hour = _SelectedTime.Hours - 12;
                    int min = _SelectedTime.Minutes;
                    int sec = _SelectedTime.Seconds;
                    if (hour == 0 && min == 0 && sec == 0) sec = 1;
                    SelectedTime = new TimeSpan(hour, min, sec);
                }
            }
            else
            {
                if (_SelectedTime != TimeSpan.Zero && _SelectedTime.Hours < 12)
                    SelectedTime = new TimeSpan(_SelectedTime.Hours + 12, _SelectedTime.Minutes, _SelectedTime.Seconds);
            }
        }
        private void ClearCommandExecuted(object sender, EventArgs e)
        {
            SelectedTime = TimeSpan.Zero;
        }
        private void MinuteChangeCommandExecuted(object sender, EventArgs e)
        {
            if (_SelectedTime == TimeSpan.Zero) return;
            int offset = (int)((ICommandSource)sender).CommandParameter;
            SelectedTime = new TimeSpan(_SelectedTime.Hours, _SelectedTime.Minutes + offset, _SelectedTime.Seconds);
        }
        private void HourCommandExecuted(object sender, EventArgs e)
        {
            int hour = (int)((ICommandSource)sender).CommandParameter;
            if (!IsZuluTime)
            {
                bool isAm = false;
                if (_SelectorType == eTimeSelectorType.TouchStyle)
                    isAm = _ButtonAm.Checked;
                else
                {
                    if (_SelectedTime != TimeSpan.Zero && _SelectedTime.Hours < 12)
                        isAm = true;
                }

                if (!isAm && hour < 12)
                    hour += 12;
                else if (isAm && hour == 12)
                    hour = 0;
            }
            int minute = _SelectedTime == TimeSpan.Zero ? DateTimeInput.DateTimeDefaults.Minute : _SelectedTime.Minutes;
            int second = _SelectedTime == TimeSpan.Zero ? DateTimeInput.DateTimeDefaults.Second : _SelectedTime.Seconds;
            if (hour == 0 && minute == 0 && second == 0) second = 1;
            SelectedTime = new TimeSpan(hour, minute, second);
        }
        void MinuteCommandExecuted(object sender, EventArgs e)
        {
            int minute = (int)((ICommandSource)sender).CommandParameter;
            int hour = _SelectedTime == TimeSpan.Zero ? DateTimeInput.DateTimeDefaults.Hour : _SelectedTime.Hours;
            int second = _SelectedTime == TimeSpan.Zero ? DateTimeInput.DateTimeDefaults.Second : _SelectedTime.Seconds;
            if (hour == 0 && minute == 0 && second == 0) second = 1;
            SelectedTime = new TimeSpan(hour, minute, second);
        }

        private LabelItem _CurrentTimeLabel = null;
        private ButtonItem _ButtonAm = null;
        private ButtonItem _ButtonPm = null;
        private void RecreateItems()
        {
            _AmPmLabel = null;
            _HourLabel = null;
            _MinuteLabel = null;
            if (_InnerContainer.SubItems.Count > 0)
            {
                BaseItem[] list = new BaseItem[_InnerContainer.SubItems.Count];
                _InnerContainer.SubItems.CopyTo(list, 0);
                _InnerContainer.SubItems.Clear();
                foreach (BaseItem item in list)
                {
                    item.Dispose();
                }
            }
            if (_SelectorType == eTimeSelectorType.MonthCalendarStyle)
                RecreateItemsMonthCalendarStyle();
            else if (_SelectorType == eTimeSelectorType.TouchStyle)
                RecreateItemsTouchStyle();
            else
                throw new NotImplementedException("Selector type '" + _SelectorType.ToString() + "' not implemented");

            UpdateSelectedTimeText();
        }
        private void RecreateItemsTouchStyle()
        {
            if (_InnerContainer.SubItems.Count > 0)
            {
                BaseItem[] list = new BaseItem[_InnerContainer.SubItems.Count];
                _InnerContainer.SubItems.CopyTo(list, 0);
                _InnerContainer.SubItems.Clear();
                foreach (BaseItem item in list)
                {
                    item.Dispose();
                }
            }
            _InnerContainer.ResizeItemsToFit = false;
            _CurrentTimeLabel = new DevComponents.DotNetBar.LabelItem();
            ItemContainer itemContainer1 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer itemContainer2 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer itemContainer6 = new DevComponents.DotNetBar.ItemContainer();
            ButtonItem buttonMinuteUp = new DevComponents.DotNetBar.ButtonItem();
            ButtonItem buttonMinuteDown = new DevComponents.DotNetBar.ButtonItem();
            LabelItem labelSpacerTop = new DevComponents.DotNetBar.LabelItem();
            ButtonItem buttonClearTime = new DevComponents.DotNetBar.ButtonItem();
            ItemContainer itemContainer3 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer itemContainer4 = new DevComponents.DotNetBar.ItemContainer();
            LabelItem labelHour = new DevComponents.DotNetBar.LabelItem();
            ItemContainer hourRow1 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer hourRow2 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer hourRow3 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer hourRow4 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer itemContainer5 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer itemContainer10 = new DevComponents.DotNetBar.ItemContainer();
            LabelItem labelMinute = new DevComponents.DotNetBar.LabelItem();
            ItemContainer minuteRow1 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer minuteRow2 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer minuteRow3 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer minuteRow4 = new DevComponents.DotNetBar.ItemContainer();
            ItemContainer itemContainer15 = new DevComponents.DotNetBar.ItemContainer();
            LabelItem labelSpacerAm = new DevComponents.DotNetBar.LabelItem();
            _ButtonAm = new DevComponents.DotNetBar.ButtonItem();
            _ButtonPm = new DevComponents.DotNetBar.ButtonItem();
            LabelItem labelSpacerBottom = new DevComponents.DotNetBar.LabelItem();
            ButtonItem buttonOk = new DevComponents.DotNetBar.ButtonItem();

            _InnerContainer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            itemContainer1,
            itemContainer3,
            itemContainer15});
            _InnerContainer.ItemSpacing = 3;
            _InnerContainer.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;

            itemContainer1.ItemSpacing = 3;
            itemContainer1.Name = "itemContainer1";
            itemContainer1.AutoCollapseOnClick = false;
            itemContainer1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            itemContainer2});

            itemContainer2.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            itemContainer2.ItemSpacing = 4;
            itemContainer2.Name = "itemContainer2";
            itemContainer2.AutoCollapseOnClick = false;
            itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            _CurrentTimeLabel,
            itemContainer6,
            labelSpacerTop,
            buttonClearTime});

            _CurrentTimeLabel.Font = new System.Drawing.Font("Segoe UI", 18F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            _CurrentTimeLabel.Name = "labelCurrentTime";
            //_CurrentTimeLabel.Text = "1:00 AM";
            _CurrentTimeLabel.TextAlignment = System.Drawing.StringAlignment.Far;
            _CurrentTimeLabel.Width = 150;
            _CurrentTimeLabel.AutoCollapseOnClick = false;

            itemContainer6.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            itemContainer6.Name = "itemContainer6";
            itemContainer6.AutoCollapseOnClick = false;
            itemContainer6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            buttonMinuteUp,
            buttonMinuteDown});

            buttonMinuteUp.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            buttonMinuteUp.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            buttonMinuteUp.FixedSize = new System.Drawing.Size(24, 14);
            buttonMinuteUp.Name = "buttonMinuteUp";
            buttonMinuteUp.Text = "<div width=\"20\" align=\"center\"><expand direction=\"top\"/></div>";
            buttonMinuteUp.Command = _MinuteChangeCommand;
            buttonMinuteUp.CommandParameter = 1;
            buttonMinuteUp.ClickAutoRepeat = true;
            buttonMinuteUp.AutoCollapseOnClick = false;

            buttonMinuteDown.ButtonStyle = DevComponents.DotNetBar.eButtonStyle.TextOnlyAlways;
            buttonMinuteDown.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            buttonMinuteDown.FixedSize = new System.Drawing.Size(24, 14);
            buttonMinuteDown.Name = "buttonMinuteDown";
            buttonMinuteDown.Text = "<div width=\"20\" align=\"center\"><expand direction=\"bottom\"/></div>";
            buttonMinuteDown.Command = _MinuteChangeCommand;
            buttonMinuteDown.CommandParameter = -1;
            buttonMinuteDown.ClickAutoRepeat = true;
            buttonMinuteDown.AutoCollapseOnClick = false;

            labelSpacerTop.Name = "labelSpacerTop";
            labelSpacerTop.Width = 4;
            labelSpacerTop.AutoCollapseOnClick = false;

            buttonClearTime.Name = "buttonClearTime";
            buttonClearTime.Text = "<symbol/>";
            buttonClearTime.Command = _ClearCommand;
            buttonClearTime.Visible = _ClearButtonVisible;
            buttonClearTime.AutoCollapseOnClick = false;

            itemContainer3.ItemSpacing = 8;
            itemContainer3.Name = "itemContainer3";
            itemContainer3.AutoCollapseOnClick = false;
            itemContainer3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            itemContainer4,
            itemContainer5});

            itemContainer4.ItemSpacing = 3;
            itemContainer4.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            itemContainer4.MultiLine = true;
            itemContainer4.Name = "itemContainer4";
            itemContainer4.AutoCollapseOnClick = false;
            itemContainer4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            labelHour,
            hourRow1,
            hourRow2,
            hourRow3,
            hourRow4});

            labelHour.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelHour.Name = "labelHour";
            labelHour.Text = GetHourText();
            labelHour.TextAlignment = System.Drawing.StringAlignment.Center;
            labelHour.AutoCollapseOnClick = false;
            _HourLabel = labelHour;

            hourRow1.ItemSpacing = 3;
            hourRow1.Name = "hourRow1";
            hourRow1.AutoCollapseOnClick = false;
            hourRow1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                CreateHourItem(1, eTimeSelectorType.TouchStyle),
                CreateHourItem(2, eTimeSelectorType.TouchStyle),
                CreateHourItem(3, eTimeSelectorType.TouchStyle)});

            hourRow2.ItemSpacing = 3;
            hourRow2.Name = "hourRow2";
            hourRow2.AutoCollapseOnClick = false;
            hourRow2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            CreateHourItem(4, eTimeSelectorType.TouchStyle),
            CreateHourItem(5, eTimeSelectorType.TouchStyle),
            CreateHourItem(6, eTimeSelectorType.TouchStyle)});

            hourRow3.ItemSpacing = 3;
            hourRow3.Name = "hourRow3";
            hourRow3.AutoCollapseOnClick = false;
            hourRow3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            CreateHourItem(7, eTimeSelectorType.TouchStyle),
            CreateHourItem(8, eTimeSelectorType.TouchStyle),
            CreateHourItem(9, eTimeSelectorType.TouchStyle)});

            hourRow4.ItemSpacing = 3;
            hourRow4.Name = "hourRow4";
            hourRow4.AutoCollapseOnClick = false;
            hourRow4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            CreateHourItem(10, eTimeSelectorType.TouchStyle),
            CreateHourItem(11, eTimeSelectorType.TouchStyle),
            CreateHourItem(12, eTimeSelectorType.TouchStyle)});

            itemContainer5.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            itemContainer5.Name = "itemContainer5";
            itemContainer5.AutoCollapseOnClick = false;
            itemContainer5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            itemContainer10});

            itemContainer10.ItemSpacing = 3;
            itemContainer10.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            itemContainer10.MultiLine = true;
            itemContainer10.Name = "itemContainer10";
            itemContainer10.AutoCollapseOnClick = false;
            itemContainer10.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            labelMinute,
            minuteRow1,
            minuteRow2,
            minuteRow3,
            minuteRow4});

            labelMinute.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            labelMinute.Name = "labelMinute";
            labelMinute.Text = GetMinuteText();
            labelMinute.TextAlignment = System.Drawing.StringAlignment.Center;
            labelMinute.AutoCollapseOnClick = false;
            _MinuteLabel = labelMinute;

            minuteRow1.ItemSpacing = 3;
            minuteRow1.Name = "minuteRow1";
            minuteRow1.AutoCollapseOnClick = false;
            minuteRow1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            CreateMinuteItem(0, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(5, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(10, eTimeSelectorType.TouchStyle)});

            minuteRow2.ItemSpacing = 3;
            minuteRow2.Name = "minuteRow2";
            minuteRow2.AutoCollapseOnClick = false;
            minuteRow2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            CreateMinuteItem(15, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(20, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(25, eTimeSelectorType.TouchStyle)});

            minuteRow3.ItemSpacing = 3;
            minuteRow3.Name = "minuteRow3";
            minuteRow3.AutoCollapseOnClick = false;
            minuteRow3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            CreateMinuteItem(30, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(35, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(40, eTimeSelectorType.TouchStyle)});

            minuteRow4.ItemSpacing = 3;
            minuteRow4.Name = "minuteRow4";
            minuteRow4.AutoCollapseOnClick = false;
            minuteRow4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            CreateMinuteItem(45, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(50, eTimeSelectorType.TouchStyle),
            CreateMinuteItem(55, eTimeSelectorType.TouchStyle)});

            itemContainer15.Name = "itemContainer15";
            itemContainer15.AutoCollapseOnClick = false;
            itemContainer15.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            labelSpacerAm,
            _ButtonAm,
            _ButtonPm,
            labelSpacerBottom,
            buttonOk});

            labelSpacerAm.Name = "labelSpacerAm";
            labelSpacerAm.Width = IsZuluTime ? 84 : 36;
            labelSpacerAm.AutoCollapseOnClick = false;

            _ButtonAm.Checked = true;
            _ButtonAm.Name = "buttonAm";
            _ButtonAm.Text = "AM";
            _ButtonAm.Visible = !IsZuluTime;
            _ButtonAm.Command = _AmPmCommand;
            _ButtonAm.CommandParameter = "AM";
            _ButtonAm.AutoCollapseOnClick = false;
            _ButtonPm.Name = "buttonPm";
            _ButtonPm.Text = "PM";
            _ButtonPm.Visible = !IsZuluTime;
            _ButtonPm.Command = _AmPmCommand;
            _ButtonPm.CommandParameter = "PM";
            _ButtonPm.AutoCollapseOnClick = false;

            if (_SelectedTime != TimeSpan.Zero)
            {
                _ButtonAm.Checked = _SelectedTime.Hours <= 12;
                _ButtonPm.Checked = _SelectedTime.Hours > 12;
            }

            labelSpacerBottom.Name = "labelSpacerBottom";
            labelSpacerBottom.Width = 124;
            labelSpacerBottom.AutoCollapseOnClick = false;

            buttonOk.ColorTable = DevComponents.DotNetBar.eButtonColor.BlueWithBackground;
            buttonOk.Name = "buttonOk";
            buttonOk.Text = "<div align=\"center\" width=\"32\"><b>" + GetOkText() + "</b></div>";
            buttonOk.Visible = _OkButtonVisible;
            buttonOk.Command = _OkCommand;

        }

        private void RecreateItemsMonthCalendarStyle()
        {
            Size fixedButtonSize = new Size(13, 18);
            bool isZuluTime = IsZuluTime;

            ItemContainer itemContainer2 = new ItemContainer();
            ItemContainer itemContainer3 = new ItemContainer();

            // 
            // buttonMinuteDown
            // 
            ButtonItem buttonMinuteDown = new ButtonItem();
            buttonMinuteDown.FixedSize = fixedButtonSize;
            buttonMinuteDown.Name = "buttonMinuteDown";
            buttonMinuteDown.Text = "<expand direction=\"left\"/>";
            buttonMinuteDown.AutoCollapseOnClick = false;
            buttonMinuteDown._FixedSizeCenterText = true;
            buttonMinuteDown.ClickAutoRepeat = true;
            buttonMinuteDown.Command = _MinuteChangeCommand;
            buttonMinuteDown.CommandParameter = -1;
            // 
            // _CurrentTimeLabel
            // 
            _CurrentTimeLabel = new LabelItem();
            _CurrentTimeLabel.Name = "_CurrentTimeLabel";
            _CurrentTimeLabel.Text = "03:45";
            _CurrentTimeLabel.TextAlignment = System.Drawing.StringAlignment.Center;
            _CurrentTimeLabel.Width = 64;
            _CurrentTimeLabel.AutoCollapseOnClick = false;
            // 
            // buttonMinuteUp
            // 
            ButtonItem buttonMinuteUp = new ButtonItem();
            buttonMinuteUp.FixedSize = fixedButtonSize;
            buttonMinuteUp.Name = "buttonMinuteUp";
            buttonMinuteUp.Text = "<expand direction=\"right\"/>";
            buttonMinuteUp.AutoCollapseOnClick = false;
            buttonMinuteUp._FixedSizeCenterText = true;
            buttonMinuteUp.ClickAutoRepeat = true;
            buttonMinuteUp.Command = _MinuteChangeCommand;
            buttonMinuteUp.CommandParameter = 1;
            // 
            // labelSpacerLine1
            // 
            LabelItem labelSpacerLine1 = new LabelItem();
            labelSpacerLine1.Name = "labelSpacerLine1";
            labelSpacerLine1.Width = 10;
            labelSpacerLine1.AutoCollapseOnClick = false;
            // 
            // _ButtonAm
            // 
            ButtonItem _ButtonAm = new ButtonItem();
            _ButtonAm.FixedSize = fixedButtonSize;
            _ButtonAm.Name = "_ButtonAm";
            _ButtonAm.Text = "<expand direction=\"left\"/>";
            _ButtonAm.AutoCollapseOnClick = false;
            _ButtonAm._FixedSizeCenterText = true;
            _ButtonAm.Visible = !isZuluTime;
            _ButtonAm.Command = _AmPmCommand;
            _ButtonAm.CommandParameter = "AM";
            // 
            // labelItem2
            // 
            _AmPmLabel = new LabelItem();
            _AmPmLabel.Name = "labelItem2";
            _AmPmLabel.Text = "PM";
            _AmPmLabel.TextAlignment = System.Drawing.StringAlignment.Center;
            _AmPmLabel.Width = 28;
            _AmPmLabel.AutoCollapseOnClick = false;
            _AmPmLabel.Visible = !isZuluTime;
            // 
            // _ButtonPm
            // 
            _ButtonPm = new ButtonItem();
            _ButtonPm.FixedSize = fixedButtonSize;
            _ButtonPm.Name = "_ButtonPm";
            _ButtonPm.Text = "<expand direction=\"right\"/>";
            _ButtonPm.AutoCollapseOnClick = false;
            _ButtonPm._FixedSizeCenterText = true;
            _ButtonPm.Command = _AmPmCommand;
            _ButtonPm.CommandParameter = "PM";
            _ButtonPm.Visible = !isZuluTime;

            itemContainer2.AutoCollapseOnClick = false;
            //itemContainer2.BackgroundStyle.PaddingTop = 2;
            //itemContainer2.BackgroundStyle.PaddingBottom = 1;
            itemContainer2.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            itemContainer2.BackgroundStyle.BackColorGradientAngle = 90;
            itemContainer2.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            itemContainer2.BackgroundStyle.Class = "";
            itemContainer2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            itemContainer2.Name = "itemContainer2";
            itemContainer2.MinimumSize = new Size(0, fixedButtonSize.Height + 3);
            itemContainer2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            buttonMinuteDown,
            _CurrentTimeLabel,
            buttonMinuteUp,
            labelSpacerLine1,
            _ButtonAm,
            _AmPmLabel,
            _ButtonPm});

            // 
            // labelHour
            // 
            LabelItem labelHour = new LabelItem();
            labelHour.Name = "labelHour";
            labelHour.Text = GetHourText();
            labelHour.ForeColorColorSchemePart = eColorSchemePart.ItemText;
            labelHour.TextAlignment = System.Drawing.StringAlignment.Center;
            labelHour.Width = isZuluTime ? (_HourMinuteButtonSize.Width * 4 + 3) : (_HourMinuteButtonSize.Width * 3 + 2);
            labelHour.Height = _HourMinuteButtonSize.Height - 3;
            labelHour.AutoCollapseOnClick = false;
            labelHour.PaddingBottom = 1;
            _HourLabel = labelHour;
            // 
            // labelMinute
            // 
            LabelItem labelMinute = new LabelItem();
            labelMinute.Name = "labelMinute";
            labelMinute.Text = GetMinuteText();
            labelMinute.ForeColorColorSchemePart = eColorSchemePart.ItemText;
            labelMinute.TextAlignment = System.Drawing.StringAlignment.Center;
            labelMinute.Width = (_HourMinuteButtonSize.Width * 3 + 2);
            labelMinute.Height = _HourMinuteButtonSize.Height - 3;
            labelMinute.AutoCollapseOnClick = false;
            labelMinute.PaddingBottom = 1;
            _MinuteLabel = labelMinute;
            LabelItem labelHMSpacer = new LabelItem();
            labelHMSpacer.Height = _HourMinuteButtonSize.Height - 3;
            labelHMSpacer.Width = isZuluTime ? 2 : 18;
            // 
            // hourMinLabelRow
            // 
            // 
            // 
            // 
            ItemContainer hourMinLabelRow = new ItemContainer();
            hourMinLabelRow.AutoCollapseOnClick = false;
            hourMinLabelRow.BackgroundStyle.BorderBottom = DevComponents.DotNetBar.eStyleBorderType.Solid;
            hourMinLabelRow.BackgroundStyle.BorderBottomColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            hourMinLabelRow.BackgroundStyle.BorderBottomWidth = 1;
            hourMinLabelRow.BackgroundStyle.Class = "";
            hourMinLabelRow.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            hourMinLabelRow.Name = "hourMinLabelRow";
            hourMinLabelRow.BackgroundStyle.MarginBottom = 2;
            hourMinLabelRow.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            labelHour,
            labelHMSpacer,
            labelMinute});

            LabelItem labelSpacerLine3 = new LabelItem();
            labelSpacerLine3.Name = "labelSpacerLine3";
            labelSpacerLine3.Width = 18;
            labelSpacerLine3.AutoCollapseOnClick = false;
            // 
            // hourMinRow1
            // 
            // 
            // 
            // 
            ItemContainer hourMinRow1 = new ItemContainer();
            hourMinRow1.AutoCollapseOnClick = false;
            hourMinRow1.BackgroundStyle.Class = "";
            hourMinRow1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            hourMinRow1.Name = "hourMinRow1";
            if (isZuluTime)
            {
                labelSpacerLine3.Width -= 12;
                hourMinRow1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(0, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(1, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(2, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(3, eTimeSelectorType.MonthCalendarStyle),
                    labelSpacerLine3,
                    CreateMinuteItem(0, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(5, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(10, eTimeSelectorType.MonthCalendarStyle)});
            }
            else
            {
                hourMinRow1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(1, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(2, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(3, eTimeSelectorType.MonthCalendarStyle),
                    labelSpacerLine3,
                    CreateMinuteItem(0, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(5, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(10, eTimeSelectorType.MonthCalendarStyle)});
            }

            // 
            // labelItem5
            // 
            LabelItem labelItem5 = new LabelItem();
            labelItem5.Name = "labelItem5";
            labelItem5.Width = 18;
            labelItem5.AutoCollapseOnClick = false;
            // 
            // hourMinRow2
            // 
            // 
            // 
            // 
            ItemContainer hourMinRow2 = new ItemContainer();
            hourMinRow2.AutoCollapseOnClick = false;
            hourMinRow2.BackgroundStyle.Class = "";
            hourMinRow2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            hourMinRow2.Name = "hourMinRow2";
            if (isZuluTime)
            {
                labelItem5.Width -= 12;
                hourMinRow2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(4, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(5, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(6, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(7, eTimeSelectorType.MonthCalendarStyle),
                    labelItem5,
                    CreateMinuteItem(15, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(20, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(25, eTimeSelectorType.MonthCalendarStyle)});
            }
            else
            {
                hourMinRow2.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(4, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(5, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(6, eTimeSelectorType.MonthCalendarStyle),
                    labelItem5,
                    CreateMinuteItem(15, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(20, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(25, eTimeSelectorType.MonthCalendarStyle)});
            }
            // 
            // labelItem6
            // 
            LabelItem labelItem6 = new LabelItem();
            labelItem6.Name = "labelItem6";
            labelItem6.Width = 18;
            labelItem6.AutoCollapseOnClick = false;
            // 
            // hourMinRow3
            // 
            // 
            // 
            // 
            ItemContainer hourMinRow3 = new ItemContainer();
            hourMinRow3.AutoCollapseOnClick = false;
            hourMinRow3.BackgroundStyle.Class = "";
            hourMinRow3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            hourMinRow3.Name = "hourMinRow3";
            if (isZuluTime)
            {
                labelItem6.Width -= 12;
                hourMinRow3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(8, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(9, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(10, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(11, eTimeSelectorType.MonthCalendarStyle),
                    labelItem6,
                    CreateMinuteItem(30, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(35, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(40, eTimeSelectorType.MonthCalendarStyle)});
            }
            else
            {
                hourMinRow3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(7, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(8, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(9, eTimeSelectorType.MonthCalendarStyle),
                    labelItem6,
                    CreateMinuteItem(30, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(35, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(40, eTimeSelectorType.MonthCalendarStyle)});
            }
            // 
            // labelItem7
            // 
            LabelItem labelItem7 = new LabelItem();
            labelItem7.Name = "labelItem7";
            labelItem7.Width = 18;
            labelItem7.AutoCollapseOnClick = false;
            // 
            // hourMinRow4
            // 
            // 
            // 
            // 
            ItemContainer hourMinRow4 = new ItemContainer();
            hourMinRow4.AutoCollapseOnClick = false;
            hourMinRow4.BackgroundStyle.Class = "";
            hourMinRow4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            hourMinRow4.Name = "hourMinRow4";
            if (isZuluTime)
            {
                labelItem7.Width -= 12;
                hourMinRow4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(12, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(13, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(14, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(15, eTimeSelectorType.MonthCalendarStyle),
                    labelItem7,
                    CreateMinuteItem(45, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(50, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(55, eTimeSelectorType.MonthCalendarStyle)});
            }
            else
            {
                hourMinRow4.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                    CreateHourItem(10, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(11, eTimeSelectorType.MonthCalendarStyle),
                    CreateHourItem(12, eTimeSelectorType.MonthCalendarStyle),
                    labelItem7,
                    CreateMinuteItem(45, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(50, eTimeSelectorType.MonthCalendarStyle),
                    CreateMinuteItem(55, eTimeSelectorType.MonthCalendarStyle)});
            }
            // 
            // hourMinRow4
            // 
            // 
            // 
            // 
            ItemContainer hourMinRow5 = null, hourMinRow6 = null; ;
            if (isZuluTime)
            {
                hourMinRow5 = new ItemContainer();
                hourMinRow5.AutoCollapseOnClick = false;
                hourMinRow5.BackgroundStyle.Class = "";
                hourMinRow5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                hourMinRow5.Name = "hourMinRow5";
                hourMinRow5.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                CreateHourItem(16, eTimeSelectorType.MonthCalendarStyle),
                CreateHourItem(17, eTimeSelectorType.MonthCalendarStyle),
                CreateHourItem(18, eTimeSelectorType.MonthCalendarStyle),
                CreateHourItem(19, eTimeSelectorType.MonthCalendarStyle)});

                hourMinRow6 = new ItemContainer();
                hourMinRow6.AutoCollapseOnClick = false;
                hourMinRow6.BackgroundStyle.Class = "";
                hourMinRow6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
                hourMinRow6.Name = "hourMinRow6";
                hourMinRow6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
                CreateHourItem(20, eTimeSelectorType.MonthCalendarStyle),
                CreateHourItem(21, eTimeSelectorType.MonthCalendarStyle),
                CreateHourItem(22, eTimeSelectorType.MonthCalendarStyle),
                CreateHourItem(23, eTimeSelectorType.MonthCalendarStyle)});

            }
            //
            // itemContainer3
            // 
            // 
            // 
            // 
            itemContainer3.AutoCollapseOnClick = false;
            itemContainer3.BackgroundStyle.Class = "";
            itemContainer3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            itemContainer3.LayoutOrientation = DevComponents.DotNetBar.eOrientation.Vertical;
            itemContainer3.Name = "itemContainer3";
            itemContainer3.ItemSpacing = 0;
            itemContainer3.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            hourMinLabelRow,
            hourMinRow1,
            hourMinRow2,
            hourMinRow3,
            hourMinRow4});
            if (hourMinRow5 != null)
                itemContainer3.SubItems.Add(hourMinRow5);
            if (hourMinRow6 != null)
                itemContainer3.SubItems.Add(hourMinRow6);

            // 
            // buttonItem29
            // 
            ButtonItem buttonClearTime = new ButtonItem("buttonClearTime");
            buttonClearTime.Text = "<div align=\"center\" width=\"32\">" + GetClearText() + "</div>";
            buttonClearTime.Command = _ClearCommand;
            buttonClearTime.Visible = _ClearButtonVisible;
            buttonClearTime.AutoCollapseOnClick = false;
            // 
            // buttonItem30
            // 
            ButtonItem buttonOk = new ButtonItem("buttonOk");
            buttonOk.Text = "<div align=\"center\" width=\"32\">" + GetOkText() + "</div>";
            buttonOk.Visible = _OkButtonVisible;
            buttonOk.Command = _OkCommand;
            // 
            // commandRow
            // 
            // 
            // 
            // 
            ItemContainer commandRow = new ItemContainer();
            commandRow.AutoCollapseOnClick = false;
            commandRow.BackgroundStyle.MarginTop = isZuluTime ? 2 : 35 + 2 * (_HourMinuteButtonSize.Height - DefaultHourMinuteButtonSize.Height);
            commandRow.BackgroundStyle.Border = DevComponents.DotNetBar.eStyleBorderType.Solid;
            commandRow.BackgroundStyle.BorderColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBorder;
            commandRow.BackgroundStyle.BorderTopWidth = 1;
            commandRow.BackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            commandRow.BackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            commandRow.BackgroundStyle.BackColorGradientAngle = 90;
            commandRow.HorizontalItemAlignment = DevComponents.DotNetBar.eHorizontalItemsAlignment.Center;
            commandRow.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            commandRow.Name = "commandRow";
            commandRow.ItemSpacing = 5;
            commandRow.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            buttonClearTime,
            buttonOk});

            _InnerContainer.ResizeItemsToFit = true;
            _InnerContainer.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            itemContainer2,
            itemContainer3,
            commandRow});
        }

        private BaseItem CreateMinuteItem(int minute, eTimeSelectorType style)
        {
            BaseItem item = null;
            if (style == eTimeSelectorType.TouchStyle)
            {
                ButtonItem button = new ButtonItem();
                button.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
                button.Text = "<div align=\"center\" width=\"32\"><font face=\"Segoe UI\" size=\"10\"><b>" + minute.ToString("##00") + "</b></font></div>";
                button.Command = _MinuteCommand;
                button.CommandParameter = minute;
                item = button;
            }
            else
            {
                ButtonItem button = new ButtonItem();
                button.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
                button.Text = minute.ToString("##00");
                button.Command = _MinuteCommand;
                button.CommandParameter = minute;
                button.FixedSize = HourMinuteButtonSize;
                button._FixedSizeCenterText = true;
                button.ForeColorColorSchemePart = eColorSchemePart.ItemText;
                item = button;
            }

            item.AutoCollapseOnClick = false;
            return item;
        }
        private static readonly Size DefaultHourMinuteButtonSize = new Size(24, 15);
        private BaseItem CreateHourItem(int hour, eTimeSelectorType style)
        {
            BaseItem item = null;
            if (style == eTimeSelectorType.TouchStyle)
            {
                if (IsZuluTime)
                {
                    DualButton button = new DualButton();
                    button.Text = hour.ToString();
                    if (hour + 12 == 24)
                        button.Text2 = "0";
                    else
                        button.Text2 = (hour + 12).ToString();
                    button.Command = _HourCommand;
                    button.CommandParameter = hour;
                    button.Command2 = _HourCommand;
                    button.Command2Parameter = hour + 12;
                    item = button;
                    if (_DualButtonFont == null) CreateDualButtonFont();
                    button.Font = _DualButtonFont;
                }
                else
                {
                    ButtonItem button = new ButtonItem();
                    button.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
                    button.Text = "<div align=\"center\" width=\"32\"><font face=\"Segoe UI\" size=\"10\"><b>" + hour.ToString() + "</b></font></div>";
                    button.Command = _HourCommand;
                    button.CommandParameter = hour;
                    item = button;
                }
            }
            else
            {
                ButtonItem button = new ButtonItem();
                button.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor();
                button.Text = hour.ToString("##00");
                button.Command = _HourCommand;
                button.CommandParameter = hour;
                button.FixedSize = HourMinuteButtonSize;
                button._FixedSizeCenterText = true;
                button.ForeColorColorSchemePart = eColorSchemePart.ItemText;
                item = button;
            }

            item.AutoCollapseOnClick = false;
            return item;
        }
        private void CreateDualButtonFont()
        {
            if (_DualButtonFont != null) _DualButtonFont.Dispose();
            _DualButtonFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        }
        private bool IsZuluTime
        {
            get
            {
                if (GetEffectiveTimeFormat() == eTimeSelectorFormat.Time24H)
                    return true;
                return false;
            }
        }

        private eTimeSelectorFormat GetEffectiveTimeFormat()
        {
            eTimeSelectorFormat timeFormat = _TimeFormat;
            if (timeFormat == eTimeSelectorFormat.System)
            {
                if (System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.ShortTimePattern.Contains("H"))
                    return eTimeSelectorFormat.Time24H;
                return eTimeSelectorFormat.Time12H;
            }
            return timeFormat;
        }

        private TimeSpan _SelectedTime = TimeSpan.Zero;
        /// <summary>
        /// Gets or sets selected time. Returns TimeSpan.Zero if there is no time selected.
        /// </summary>
        [Category("Data"), Description("Indicates selected time.")]
        public TimeSpan SelectedTime
        {
            get { return _SelectedTime; }
            set
            {
                if (value != _SelectedTime)
                {
                    TimeSpan oldValue = _SelectedTime;
                    _SelectedTime = value;
                    OnSelectedTimeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Returns whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSelectedTime()
        {
            return !_SelectedTime.Equals(TimeSpan.Zero);
        }
        /// <summary>
        /// Resets property to default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetSelectedTime()
        {
            SelectedTime = TimeSpan.Zero;
        }
        /// <summary>
        /// Called when SelectedTime property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSelectedTimeChanged(TimeSpan oldValue, TimeSpan newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SelectedTime"));
            UpdateSelectedTimeText();
            OnSelectedTimeChanged(EventArgs.Empty);
        }

        internal const string DefaultTimeFormat24H = "HH:mm";
        private string _TimeFormat24H = DefaultTimeFormat24H;
        /// <summary>
        /// Gets or sets the format for the 24 Hour Time Display.
        /// </summary>
        [DefaultValue(DefaultTimeFormat24H), Category("Data"), Description("Indicates format for the 24 Hour Time Display."), Localizable(true)]
        public string TimeFormat24H
        {
            get { return _TimeFormat24H; }
            set
            {
                if (value != _TimeFormat24H)
                {
                    string oldValue = _TimeFormat24H;
                    _TimeFormat24H = value;
                    OnTimeFormat24HChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TimeFormat24H property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTimeFormat24HChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TimeFormat24H"));
            UpdateSelectedTimeText();
        }

        internal const string DefaultTimeFormat12H = "hh:mm tt";
        private string _TimeFormat12H = DefaultTimeFormat12H;
        /// <summary>
        /// Gets or sets the format for the 12 Hour Time Display.
        /// </summary>
        [DefaultValue(DefaultTimeFormat12H), Category("Data"), Description("Indicates format for the 12 Hour Time Display."), Localizable(true)]
        public string TimeFormat12H
        {
            get { return _TimeFormat12H; }
            set
            {
                if (value != _TimeFormat12H)
                {
                    string oldValue = _TimeFormat12H;
                    _TimeFormat12H = value;
                    OnTimeFormat12HChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TimeFormat12H property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTimeFormat12HChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TimeFormat12H"));
            UpdateSelectedTimeText();
        }
        private void UpdateSelectedTimeText()
        {
            if (_SelectedTime == TimeSpan.Zero)
            {
                _CurrentTimeLabel.Text = "--:--";
                if (_AmPmLabel != null)
                    _AmPmLabel.Text = "--";
            }
            else
            {
                if (IsZuluTime)
                {
                    _CurrentTimeLabel.Text = SelectedDateTime.ToString(_TimeFormat24H);
                }
                else
                {
                    _CurrentTimeLabel.Text = SelectedDateTime.ToString(_TimeFormat12H);
                }
                if (_SelectorType == eTimeSelectorType.TouchStyle)
                {
                    _ButtonAm.Checked = _SelectedTime.Hours < 12;
                    _ButtonPm.Checked = _SelectedTime.Hours >= 12;
                }
                else
                {
                    _ButtonAm.Checked = false;
                    _ButtonPm.Checked = false;
                }
                if (_AmPmLabel != null)
                    _AmPmLabel.Text = (_SelectedTime.Hours < 12) ? "AM" : "PM";
            }
        }

        private DateTime _SelectedDateTime = DateTime.MinValue;
        /// <summary>
        /// Gets or sets the selected date time.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DateTime SelectedDateTime
        {
            get
            {
                if (_SelectedTime == TimeSpan.Zero)
                    return DateTime.MinValue;

                return new DateTime(_SelectedDateTime.Year, _SelectedDateTime.Month, _SelectedDateTime.Day,
                    _SelectedTime.Hours, _SelectedTime.Minutes, _SelectedTime.Seconds);
            }
            set
            {
                if (value != _SelectedDateTime)
                {
                    DateTime oldValue = _SelectedDateTime;
                    _SelectedDateTime = value;
                    OnSelectedDateTimeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SelectedDateTime property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSelectedDateTimeChanged(DateTime oldValue, DateTime newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SelectedDateTime"));
            if (newValue == DateTime.MinValue)
                SelectedTime = TimeSpan.Zero;
            else
                SelectedTime = newValue.TimeOfDay;
        }

        private string _OkText = "OK";
        /// <summary>
        /// Gets or sets the text displayed on OK button.
        /// </summary>
        [DefaultValue("OK"), Category("Appearance"), Description("Indicates text displayed on OK button."), Localizable(true)]
        public string OkText
        {
            get { return _OkText; }
            set
            {
                if (value != _OkText)
                {
                    string oldValue = _OkText;
                    _OkText = value;
                    OnOkTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when OkText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnOkTextChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("OkText"));
            if (_OkCommand != null) _OkCommand.Text = GetOkText();
        }
        private string GetOkText()
        {
            using (LocalizationManager lm = new LocalizationManager(this.GetOwner() as IOwnerLocalize))
            {
                string s = lm.GetLocalizedString(LocalizationKeys.TimeSelectorOkButton);
                if (s != "") return s;
            }
            return _OkText;
        }

        private string _ClearText = "Clear";
        /// <summary>
        /// Gets or sets the text displayed on Clear button only when MonthCalendarStyle is used.
        /// </summary>
        [DefaultValue("Clear"), Category("Appearance"), Description("Indicates text displayed on Clear button only when MonthCalendarStyle is used."), Localizable(true)]
        public string ClearText
        {
            get { return _ClearText; }
            set
            {
                if (value != _ClearText)
                {
                    string oldValue = _ClearText;
                    _ClearText = value;
                    OnClearTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when ClearText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnClearTextChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("ClearText"));
            if (_ClearCommand != null) _ClearCommand.Text = GetClearText();
        }
        private string GetClearText()
        {
            using (LocalizationManager lm = new LocalizationManager(this.GetOwner() as IOwnerLocalize))
            {
                string s = lm.GetLocalizedString(LocalizationKeys.TimeSelectorClearButton);
                if (s != "") return s;
            }
            return _ClearText;
        }

        private eTimeSelectorFormat _TimeFormat = eTimeSelectorFormat.System;
        /// <summary>
        /// Gets or sets the time format used to present time by the selector.
        /// </summary>
        [DefaultValue(eTimeSelectorFormat.System), Category("Appearance"), Description("Indicates time format used to present time by the selector."), Localizable(true)]
        public eTimeSelectorFormat TimeFormat
        {
            get { return _TimeFormat; }
            set
            {
                if (value != _TimeFormat)
                {
                    eTimeSelectorFormat oldValue = _TimeFormat;
                    _TimeFormat = value;
                    OnTimeFormatChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TimeFormat property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTimeFormatChanged(eTimeSelectorFormat oldValue, eTimeSelectorFormat newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TimeFormat"));
            RecreateItems();
        }

        private bool _OkButtonVisible = true;
        /// <summary>
        /// Gets or sets whether Ok button is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether Ok button is visible.")]
        public bool OkButtonVisible
        {
            get { return _OkButtonVisible; }
            set
            {
                if (value != _OkButtonVisible)
                {
                    bool oldValue = _OkButtonVisible;
                    _OkButtonVisible = value;
                    OnOkButtonVisibleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when OkButtonVisible property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnOkButtonVisibleChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("OkButtonVisible"));
            if (_OkCommand != null) _OkCommand.Visible = newValue;
        }

        private bool _ClearButtonVisible = true;
        /// <summary>
        /// Gets or sets whether Ok button is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether Ok button is visible.")]
        public bool ClearButtonVisible
        {
            get { return _ClearButtonVisible; }
            set
            {
                if (value != _ClearButtonVisible)
                {
                    bool oldValue = _ClearButtonVisible;
                    _ClearButtonVisible = value;
                    OnClearButtonVisibleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when ClearButtonVisible property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnClearButtonVisibleChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("ClearButtonVisible"));
            if (_ClearCommand != null) _ClearCommand.Visible = newValue;
        }

        private eTimeSelectorType _SelectorType = eTimeSelectorType.TouchStyle;
        /// <summary>
        /// Indicates the type of the selector used to select time.
        /// </summary>
        [DefaultValue(eTimeSelectorType.TouchStyle), Category("Appearance"), Description("Indicates the type of the selector used to select time.")]
        public eTimeSelectorType SelectorType
        {
            get { return _SelectorType; }
            set
            {
                if (value != _SelectorType)
                {
                    eTimeSelectorType oldValue = _SelectorType;
                    _SelectorType = value;
                    OnSelectorTypeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SelectorType property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSelectorTypeChanged(eTimeSelectorType oldValue, eTimeSelectorType newValue)
        {
            RecreateItems();
        }

        private string _HourText = "Hour";
        /// <summary>
        /// Gets or sets the text displayed on Hour label.
        /// </summary>
        [DefaultValue("Hour"), Category("Appearance"), Description("Indicates text displayed on Hour label."), Localizable(true)]
        public string HourText
        {
            get { return _HourText; }
            set
            {
                if (value != _HourText)
                {
                    string oldValue = _HourText;
                    _HourText = value;
                    OnHourTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when HourText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnHourTextChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("HourText"));
            if (_HourLabel != null) _HourLabel.Text = GetHourText();
        }
        private string GetHourText()
        {
            using (LocalizationManager lm = new LocalizationManager(this.GetOwner() as IOwnerLocalize))
            {
                string s = lm.GetLocalizedString(LocalizationKeys.TimeSelectorHourLabel);
                if (s != "") return s;
            }
            return _HourText;
        }

        private string _MinuteText = "Minute";
        /// <summary>
        /// Gets or sets the text displayed on Minute label.
        /// </summary>
        [DefaultValue("Minute"), Category("Appearance"), Description("Indicates text displayed on Minute label."), Localizable(true)]
        public string MinuteText
        {
            get { return _MinuteText; }
            set
            {
                if (value != _MinuteText)
                {
                    string oldValue = _MinuteText;
                    _MinuteText = value;
                    OnMinuteTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when MinuteText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnMinuteTextChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("MinuteText"));
            if (_MinuteLabel != null) _MinuteLabel.Text = GetMinuteText();
        }
        private string GetMinuteText()
        {
            using (LocalizationManager lm = new LocalizationManager(this.GetOwner() as IOwnerLocalize))
            {
                string s = lm.GetLocalizedString(LocalizationKeys.TimeSelectorMinuteLabel);
                if (s != "") return s;
            }
            return _MinuteText;
        }


        private Size _HourMinuteButtonSize = DefaultHourMinuteButtonSize;
        internal Size HourMinuteButtonSize
        {
            get
            {
                return _HourMinuteButtonSize;
            }
            set
            {
                if (_HourMinuteButtonSize != value)
                {
                    _HourMinuteButtonSize = value;
                    RecreateItems();
                }
            }
        }
        #endregion
    }

    /// <summary>
    /// Defines time selector format.
    /// </summary>
    public enum eTimeSelectorFormat
    {
        /// <summary>
        /// Selector uses system format.
        /// </summary>
        System,
        /// <summary>
        /// Selector uses 24-hour time format.
        /// </summary>
        Time24H,
        /// <summary>
        /// Selector uses 12-hour time format.
        /// </summary>
        Time12H
    }

    /// <summary>
    /// Defines the TimeSelector styles.
    /// </summary>
    public enum eTimeSelectorType
    {
        /// <summary>
        /// Time selector uses style similar to MonthCalendarStyle.
        /// </summary>
        MonthCalendarStyle,
        /// <summary>
        /// Time selector uses the touch style.
        /// </summary>
        TouchStyle
    }
}
