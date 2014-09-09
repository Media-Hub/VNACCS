#if FRAMEWORK20
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Imaging;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar.Schedule
{
    /// <summary>
    /// Represents date-navigation control that is used with CalendarView to provide calendar date navigation.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(DateNavigator), "Schedule.DateNavigator.ico")]
    [Designer("DevComponents.DotNetBar.Design.DateNavigatorDesigner, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf")]
    public partial class DateNavigator : PanelEx
    {
        #region Events

        /// <summary>
        /// Occurs when a date Navigation is occurring
        /// </summary>
        [Description("Occurs when a date Navigation is occurring.")]
        public event EventHandler<DateChangingEventArgs> DateChanging;

        /// <summary>
        /// Occurs when a date Navigation has occurred
        /// </summary>
        [Description("Occurs when a date Navigation has occurred.")]
        public event EventHandler<DateChangedEventArgs> DateChanged;

        /// <summary>
        /// Occurs when the DateNavigator needs a formatted date label
        /// </summary>
        [Description("Occurs when the DateNavigator needs a formatted date label.")]
        public event EventHandler<GetDateLabelEventArgs> GetDateLabel;
        
        #endregion

        #region Constants

        private const int MaxYearMonthSpan = 120;

        #endregion

        #region Private variables

        private CalendarView _CalendarView;

        private string _CurrentDayLabel = "MMMM dd, yyyy";

        private string _CurrentWeekStartLabel = "MMMM dd, yyyy";
        private string _CurrentWeekEndLabel = "MMMM dd, yyyy";
        private string _CurrentWeekSameYearStartLabel = "MMMM dd";
        private string _CurrentWeekSameYearEndLabel = "MMMM dd, yyyy";
        private string _CurrentWeekSameMonthYearStartLabel = "MMMM dd";
        private string _CurrentWeekSameMonthYearEndLabel = "dd, yyyy";

        private string _CurrentMonthStartLabel = "MMMM yyyy";
        private string _CurrentMonthEndLabel = "MMMM yyyy";

        private string _CurrentYearStartLabel = "MMMM yyyy";
        private string _CurrentYearEndLabel = "MMMM yyyy";

        private string _CurrentTimeLineHourLabel = "MMMM dd, yyyy";
        private string _CurrentTimeLineDayStartLabel = "MMMM dd, yyyy";
        private string _CurrentTimeLineDayEndLabel = "MMMM dd, yyyy";
        private string _CurrentTimeLineDaySameYearStartLabel = "MMMM dd";
        private string _CurrentTimeLineDaySameYearEndLabel = "MMMM dd, yyyy";
        private string _CurrentTimeLineDaySameMonthYearStartLabel = "MMMM dd";
        private string _CurrentTimeLineDaySameMonthYearEndLabel = "dd, yyyy";
        private string _CurrentTimeLineYearStartLabel = "yyyy";
        private string _CurrentTimeLineYearEndLabel = "yyyy";

        #endregion

        public DateNavigator()
        {
            InitializeComponent();
            UpdateButtonImages();

            Style.BackColor1.Color =
                ((Office2007Renderer)GlobalManager.Renderer).ColorTable.CalendarView.TimeRulerColors[0].Colors[0];

            StyleManager.Register(this);
        }

        #region Public properties

        #region CalendarView

        /// <summary>
        /// Gets or sets the CalendarView date navigation will be applied to.
        /// </summary>
        [DefaultValue(null), Category("Behavior")]
        [Description("Indicates CalendarView date navigation will be applied to.")]
        public CalendarView CalendarView
        {
            get { return _CalendarView; }

            set
            {
                if (_CalendarView != null)
                {
                    _CalendarView.SelectedViewChanged -= CalendarSelectedViewChanged;
                    _CalendarView.ViewDateChanged -= CalendarViewViewDateChanged;
                    _CalendarView.TimeLineViewScrollDateChanged -= CalendarViewTimeLineViewScrollDateChanged;
                }

                _CalendarView = value;

                if (_CalendarView != null)
                {
                    _CalendarView.SelectedViewChanged += CalendarSelectedViewChanged;
                    _CalendarView.ViewDateChanged += CalendarViewViewDateChanged;
                    _CalendarView.TimeLineViewScrollDateChanged += CalendarViewTimeLineViewScrollDateChanged;
                }

                UpdateDisplay();
            }
        }

        /// <summary>
        /// Handles CalendarView ViewDateChanged events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalendarViewViewDateChanged(object sender, ViewDateChangedEventArgs e)
        {
            UpdateDisplay();
        }

        /// <summary>
        /// Handle CalendarView SelectedViewChanged events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CalendarSelectedViewChanged(object sender, SelectedViewEventArgs e)
        {
            UpdateDisplay();
        }

        /// <summary>
        /// Handles CalendarView_TimeLineViewScrollDateChanged events
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void CalendarViewTimeLineViewScrollDateChanged(object sender, EventArgs e)
        {
            UpdateDisplay();
        }

        #endregion

        #region CurrentDayLabel

        /// <summary>
        /// Gets or sets the current Day format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current Day format label.")]
        public string CurrentDayLabel
        {
            get { return (_CurrentDayLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentDayLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentWeekStartLabel

        /// <summary>
        /// Gets or sets the current Week, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current Week, start format label.")]
        public string CurrentWeekStartLabel
        {
            get { return (_CurrentWeekStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentWeekStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentWeekEndLabel

        /// <summary>
        /// Gets or sets the current Week, end format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current Week, end format label.")]
        public string CurrentWeekEndLabel
        {
            get { return (_CurrentWeekEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentWeekEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentWeekSameYearStartLabel

        /// <summary>
        /// Gets or sets the current Week, same Year, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd"), Category("Appearance")]
        [Description("Indicates the current Week, same Year, start format label.")]
        public string CurrentWeekSameYearStartLabel
        {
            get { return (_CurrentWeekSameYearStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentWeekSameYearStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentWeekSameYearEndLabel

        /// <summary>
        /// Gets or sets the current Week, same Year, end format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current Week, same Year, end format label.")]
        public string CurrentWeekSameYearEndLabel
        {
            get { return (_CurrentWeekSameYearEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentWeekSameYearEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentWeekSameMonthYearStartLabel

        /// <summary>
        /// Gets or sets the current Week, same Month & Year, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd"), Category("Appearance")]
        [Description("Indicates the current Week, same Month & Year, start format label.")]
        public string CurrentWeekSameMonthYearStartLabel
        {
            get { return (_CurrentWeekSameMonthYearStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentWeekSameMonthYearStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentWeekSameMonthYearEndLabel

        /// <summary>
        /// Gets or sets the current Week, same Month & Year, end format label
        /// </summary>
        [Localizable(true), DefaultValue("dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current Week, same Month & Year, end format label.")]
        public string CurrentWeekSameMonthYearEndLabel
        {
            get { return (_CurrentWeekSameMonthYearEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentWeekSameMonthYearEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentMonthStartLabel

        /// <summary>
        /// Gets or sets the current Month, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM yyyy"), Category("Appearance")]
        [Description("Indicates the current Month, start format label.")]
        public string CurrentMonthStartLabel
        {
            get { return (_CurrentMonthStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentMonthStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentMonthEndLabel

        /// <summary>
        /// Gets or sets the current Month, end format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM yyyy"), Category("Appearance")]
        [Description("Indicates the current Month, end format label.")]
        public string CurrentMonthEndLabel
        {
            get { return (_CurrentMonthEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentMonthEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentYearStartLabel

        /// <summary>
        /// Gets or sets the current Year, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM yyyy"), Category("Appearance")]
        [Description("Indicates the current Year, start format label.")]
        public string CurrentYearStartLabel
        {
            get { return (_CurrentYearStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentYearStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentYearEndLabel

        /// <summary>
        /// Gets or sets the current Year, end format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM yyyy"), Category("Appearance")]
        [Description("Indicates the current Year, end format label.")]
        public string CurrentYearEndLabel
        {
            get { return (_CurrentYearEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentYearEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineHourLabel

        /// <summary>
        /// Gets or sets the current TimeLine Hour format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current TimeLine Hour format label.")]
        public string CurrentTimeLineHourLabel
        {
            get { return (_CurrentTimeLineHourLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineHourLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineDayStartLabel

        /// <summary>
        /// Gets or sets the current TimeLine Day, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current TimeLine Day, start format label.")]
        public string CurrentTimeLineDayStartLabel
        {
            get { return (_CurrentTimeLineDayStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineDayStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineDayEndLabel

        /// <summary>
        /// Gets or sets the current TimeLine Day, end format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current TimeLine Day, end format label.")]
        public string CurrentTimeLineDayEndLabel
        {
            get { return (_CurrentTimeLineDayEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineDayEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineDaySameYearStartLabel

        /// <summary>
        /// Gets or sets the current TimeLine Day, same Year, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd"), Category("Appearance")]
        [Description("Indicates the current TimeLine Day, same Year, start format label.")]
        public string CurrentTimeLineDaySameYearStartLabel
        {
            get { return (_CurrentTimeLineDaySameYearStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineDaySameYearStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineDaySameYearEndLabel

        /// <summary>
        /// Gets or sets the current TimeLine Day, same Year, end format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current TimeLine Day, same Year, end format label.")]
        public string CurrentTimeLineDaySameYearEndLabel
        {
            get { return (_CurrentTimeLineDaySameYearEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineDaySameYearEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineDaySameMonthYearStartLabel

        /// <summary>
        /// Gets or sets the current TimeLine Day, same Month & Year, start format label
        /// </summary>
        [Localizable(true), DefaultValue("MMMM dd"), Category("Appearance")]
        [Description("Indicates the current TimeLine Day, same Month & Year, start format label.")]
        public string CurrentTimeLineDaySameMonthYearStartLabel
        {
            get { return (_CurrentTimeLineDaySameMonthYearStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineDaySameMonthYearStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineDaySameMonthYearEndLabel

        /// <summary>
        /// Gets or sets the current TimeLine Day, same Month & Year, end format label
        /// </summary>
        [Localizable(true), DefaultValue("dd, yyyy"), Category("Appearance")]
        [Description("Indicates the current TimeLine Day, same Month & Year, end format label.")]
        public string CurrentTimeLineDaySameMonthYearEndLabel
        {
            get { return (_CurrentTimeLineDaySameMonthYearEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineDaySameMonthYearEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineYearStartLabel

        /// <summary>
        /// Gets or sets the current TimeLine Year, start format label
        /// </summary>
        [Localizable(true), DefaultValue("yyyy"), Category("Appearance")]
        [Description("Indicates the current TimeLine Year, start format label.")]
        public string CurrentTimeLineYearStartLabel
        {
            get { return (_CurrentTimeLineYearStartLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineYearStartLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region CurrentTimeLineYearEndLabel

        /// <summary>
        /// Gets or sets the current TimeLine Year, end format label
        /// </summary>
        [Localizable(true), DefaultValue("yyyy"), Category("Appearance")]
        [Description("Indicates the current TimeLine Year, end format label.")]
        public string CurrentTimeLineYearEndLabel
        {
            get { return (_CurrentTimeLineYearEndLabel); }

            set
            {
                if (value == null)
                    value = "";

                _CurrentTimeLineYearEndLabel = value;

                UpdateDisplay();
            }
        }

        #endregion

        #region WeekDaysNavigationEnabled

        private bool _WeekDaysNavigationEnabled = true;

        /// <summary>
        /// Gets or sets whether in Week View week-days navigation is enabled. Default value is true. When week-days navigation is enabled
        /// and start date points to Monday and end date points to Friday, navigating to next day will navigate to next Monday-Friday view.
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether in Week View week-days navigation is enabled. When week-days navigation is enabled and start date points to Monday and end date points to Friday, navigating to next day will navigate to next Monday-Friday view.")]
        public bool WeekDaysNavigationEnabled
        {
            get { return _WeekDaysNavigationEnabled; }
            set
            {
                _WeekDaysNavigationEnabled = value;
            }
        }

        #endregion

        #endregion

        #region StyleManagerStyleChanged

        /// <summary>
        /// Called by StyleManager to notify control that style on manager has changed and that control should refresh its appearance if
        /// its style is controlled by StyleManager.
        /// </summary>
        /// <param name="newStyle">New active style.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void StyleManagerStyleChanged(eDotNetBarStyle newStyle)
        {
            base.StyleManagerStyleChanged(newStyle);
            UpdateButtonImages();
            this.Style.BackColor1.Color = ((Office2007Renderer)GlobalManager.Renderer).ColorTable.CalendarView.TimeRulerColors[0].Colors[0];
        }

        #endregion

        #region Navigate_click routines

        /// <summary>
        /// Handles NavigateForward button clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateForwardClick(object sender, EventArgs e)
        {
            NavigateDatesForward();
        }

        /// <summary>
        /// Handles NavigateBack button clicks
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NavigateBackClick(object sender, EventArgs e)
        {
            NavigateDatesBack();
        }

        #endregion

        #region NavigateDatesForward

        /// <summary>
        /// Navigates forward
        /// </summary>
        private void NavigateDatesForward()
        {
            if (_CalendarView != null)
            {
                switch (_CalendarView.SelectedView)
                {
                    case eCalendarView.Day:
                        NavigateDayForward();
                        break;

                    case eCalendarView.Week:
                        NavigateWeekForward();
                        break;

                    case eCalendarView.Month:
                        NavigateMonthForward();
                        break;

                    case eCalendarView.Year:
                        NavigateYearForward();
                        break;

                    case eCalendarView.TimeLine:
                        NavigateTimeLineForward();
                        break;
                }

                UpdateDisplay();
            }
        }

        #region NavigateDayForward

        /// <summary>
        /// NavigateDayForward
        /// </summary>
        private void NavigateDayForward()
        {
            DateTime oldDate = _CalendarView.DayViewDate;
            DateTime newDate = _CalendarView.DayViewDate.AddDays(1);

            if (DoDateChanging(NavigationDirection.Forward,
                oldDate, oldDate, ref newDate, ref newDate) == false)
            {
                _CalendarView.DayViewDate = newDate;

                DoDateChanged(NavigationDirection.Forward, oldDate, oldDate, newDate, newDate);
            }
        }

        #endregion

        #region NavigateWeekForward

        /// <summary>
        /// NavigateWeekForward
        /// </summary>
        private void NavigateWeekForward()
        {
            DateTime calendarStart = _CalendarView.WeekViewStartDate;
            DateTime calendarEnd = _CalendarView.WeekViewEndDate;

            TimeSpan span = calendarEnd.Subtract(calendarStart);
            DateTime start = calendarStart.AddDays(1).Add(span);
            DateTime end = calendarEnd.AddDays(1).Add(span);

            if (WeekDaysNavigationEnabled &&
                calendarStart.DayOfWeek == DayOfWeek.Monday && calendarEnd.DayOfWeek == DayOfWeek.Friday)
            {
                // Browse week-days only

                start = calendarStart.AddDays(7);
                end = start.AddDays(4);
            }

            if (DoDateChanging(NavigationDirection.Forward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.WeekViewStartDate = start;
                _CalendarView.WeekViewEndDate = end;

                DoDateChanged(NavigationDirection.Forward, calendarStart, calendarEnd, start, end);
            }
        }

        #endregion

        #region NavigateMonthForward

        /// <summary>
        /// NavigateMonthForward
        /// </summary>
        private void NavigateMonthForward()
        {
            DateTime calendarStart = _CalendarView.MonthViewStartDate;
            DateTime calendarEnd = _CalendarView.MonthViewEndDate;

            TimeSpan span = calendarEnd.Subtract(calendarStart);
            DateTime start = calendarStart.AddMonths(1);
            DateTime end = start.Add(span);

            if (calendarStart.Day == 1 &&
                calendarEnd.Day == ScheduleSettings.GetActiveCulture().Calendar.GetDaysInMonth(calendarEnd.Year, calendarEnd.Month))
            {
                end = start.AddMonths(1).AddMinutes(-1);
            }

            if (DoDateChanging(NavigationDirection.Forward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.MonthViewStartDate = start;
                _CalendarView.MonthViewEndDate = end;

                DoDateChanged(NavigationDirection.Forward, calendarStart, calendarEnd, start, end);
            }
        }

        #endregion

        #region NavigateYearForward

        /// <summary>
        /// NavigateYearForward
        /// </summary>
        private void NavigateYearForward()
        {
            DateTime calendarStart = _CalendarView.YearViewStartDate;
            DateTime calendarEnd = _CalendarView.YearViewEndDate;

            int months = CountMonthSpan(calendarStart, calendarEnd);

            DateTime start = calendarEnd.AddMonths(1);
            start = start.AddDays(-start.Day + 1);

            DateTime end = start.AddMonths(months + 1);
            end = end.AddDays(-1);

            if (DoDateChanging(NavigationDirection.Forward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.YearViewStartDate = start;
                _CalendarView.YearViewEndDate = end;

                DoDateChanged(NavigationDirection.Forward, calendarStart, calendarEnd, start, end);
            }
        }

        #endregion

        #region NavigateTimeLineForward

        /// <summary>
        /// NavigateTimeLineForward
        /// </summary>
        private void NavigateTimeLineForward()
        {
            DateTime calendarStart = _CalendarView.TimeLineViewScrollStartDate;
            DateTime calendarEnd = _CalendarView.TimeLineViewScrollEndDate;

            DateTime start = GetForwardPeriodDate(_CalendarView.TimeLineViewScrollStartDate);
            DateTime end = start;

            if (DoDateChanging(NavigationDirection.Forward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.TimeLineViewScrollStartDate = start;

                DoDateChanged(NavigationDirection.Forward, calendarStart, calendarEnd,
                    _CalendarView.TimeLineViewScrollStartDate, _CalendarView.TimeLineViewScrollEndDate);
            }
        }

        #endregion

        #region GetForwardPeriodDate

        /// <summary>
        /// Gets the next forward TimeLine Period Date
        /// </summary>
        /// <param name="calendarStart">Current date</param>
        /// <returns>Next Period Date</returns>
        private DateTime GetForwardPeriodDate(DateTime calendarStart)
        {
            // Hours or Minutes

            if (_CalendarView.TimeLinePeriod == eTimeLinePeriod.Minutes ||
                _CalendarView.TimeLinePeriod == eTimeLinePeriod.Hours)
            {
                // Move forward by a day

                calendarStart = calendarStart.AddDays(1);

                return (new DateTime(calendarStart.Year,
                    calendarStart.Month, calendarStart.Day, 0, 0, 0));
            }

            // Days

            if (_CalendarView.TimeLinePeriod == eTimeLinePeriod.Days)
            {
                // If our interval is a single day, then
                // move forward by a month

                if (_CalendarView.TimeLineInterval == 1)
                {
                    calendarStart = calendarStart.AddMonths(1);

                    return (new DateTime(calendarStart.Year, calendarStart.Month, 1));
                }

                // The interval is multiple days, so
                // move forward by a year

                calendarStart = calendarStart.AddYears(1);

                if (calendarStart.AddMinutes(_CalendarView.BaseInterval).Year > calendarStart.Year)
                    calendarStart = calendarStart.AddYears(1);

                return (new DateTime(calendarStart.Year, 1, 1));
            }

            // Years

            if (_CalendarView.TimeLinePeriod == eTimeLinePeriod.Years)
            {
                TimeSpan ts = calendarStart - _CalendarView.TimeLineViewStartDate;

                // Move forward by 10 columns at a time

                int interval = (int)(ts.TotalMinutes / _CalendarView.BaseInterval);

                interval = ((interval + 10) / 10) * 10;

                return (_CalendarView.TimeLineViewStartDate.AddMinutes(
                    interval * _CalendarView.BaseInterval));
            }

            return (calendarStart);
        }

        #endregion

        #endregion

        #region NavigateDatesBack

        /// <summary>
        /// Navigates back
        /// </summary>
        private void NavigateDatesBack()
        {
            if (_CalendarView != null)
            {
                switch (_CalendarView.SelectedView)
                {
                    case eCalendarView.Day:
                        NavigateDayBack();
                        break;

                    case eCalendarView.Week:
                        NavigateWeekBack();
                        break;

                    case eCalendarView.Month:
                        NavigateMonthBack();
                        break;

                    case eCalendarView.Year:
                        NavigateYearBack();
                        break;

                    case eCalendarView.TimeLine:
                        NavigateTimeLineBack();
                        break;
                }

                UpdateDisplay();
            }
        }

        #region NavigateDayBack

        /// <summary>
        /// NavigateDayBack
        /// </summary>
        private void NavigateDayBack()
        {
            DateTime oldDate = _CalendarView.DayViewDate;
            DateTime newDate = _CalendarView.DayViewDate.AddDays(-1);

            if (DoDateChanging(NavigationDirection.Backward,
                oldDate, oldDate, ref newDate, ref newDate) == false)
            {
                _CalendarView.DayViewDate = newDate;

                DoDateChanged(NavigationDirection.Backward, oldDate, oldDate, newDate, newDate);
            }
        }

        #endregion

        #region NavigateWeekBack

        /// <summary>
        /// NavigateWeekBack
        /// </summary>
        private void NavigateWeekBack()
        {
            DateTime calendarStart = _CalendarView.WeekViewStartDate;
            DateTime calendarEnd = _CalendarView.WeekViewEndDate;

            TimeSpan span = calendarEnd.Subtract(calendarStart).Negate();
            DateTime start = calendarStart.AddDays(-1).Add(span);
            DateTime end = calendarEnd.AddDays(-1).Add(span);

            if (WeekDaysNavigationEnabled &&
                calendarStart.DayOfWeek == DayOfWeek.Monday && calendarEnd.DayOfWeek == DayOfWeek.Friday)
            {
                // Browse week-days only

                start = calendarStart.AddDays(-7);
                end = start.AddDays(4);
            }

            if (DoDateChanging(NavigationDirection.Backward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.WeekViewStartDate = start;
                _CalendarView.WeekViewEndDate = end;

                DoDateChanged(NavigationDirection.Backward, calendarStart, calendarEnd, start, end);
            }
        }

        #endregion

        #region NavigateMonthBack

        /// <summary>
        /// NavigateMonthBack
        /// </summary>
        private void NavigateMonthBack()
        {
            DateTime calendarStart = _CalendarView.MonthViewStartDate;
            DateTime calendarEnd = _CalendarView.MonthViewEndDate;

            TimeSpan span = calendarEnd.Subtract(calendarStart);
            DateTime start = calendarStart.AddMonths(-1);
            DateTime end = start.Add(span);

            if (calendarStart.Day == 1 &&
                calendarEnd.Day == ScheduleSettings.GetActiveCulture().Calendar.GetDaysInMonth(calendarEnd.Year, calendarEnd.Month))
            {
                end = start.AddMonths(1).AddMinutes(-1);
            }

            if (DoDateChanging(NavigationDirection.Backward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.MonthViewStartDate = start;
                _CalendarView.MonthViewEndDate = end;

                DoDateChanged(NavigationDirection.Backward, calendarStart, calendarEnd, start, end);
            }
        }

        #endregion

        #region NavigateYearBack

        /// <summary>
        /// NavigateYearBack
        /// </summary>
        private void NavigateYearBack()
        {
            DateTime calendarStart = _CalendarView.YearViewStartDate;
            DateTime calendarEnd = _CalendarView.YearViewEndDate;

            int months = CountMonthSpan(calendarStart, calendarEnd);

            DateTime end = calendarStart.AddDays(-1);
            DateTime start = end.AddMonths(-months);
            start = start.AddDays(-start.Day + 1);

            if (DoDateChanging(NavigationDirection.Backward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.YearViewStartDate = start;
                _CalendarView.YearViewEndDate = end;

                DoDateChanged(NavigationDirection.Backward, calendarStart, calendarEnd, start, end);
            }
        }

        #endregion

        #region NavigateTimeLineBack

        /// <summary>
        /// NavigateTimeLineBack
        /// </summary>
        private void NavigateTimeLineBack()
        {
            DateTime calendarStart = _CalendarView.TimeLineViewScrollStartDate;
            DateTime calendarEnd = _CalendarView.TimeLineViewScrollEndDate;

            DateTime start = GetBackPeriodDate(_CalendarView.TimeLineViewScrollStartDate);
            DateTime end = start;

            if (DoDateChanging(NavigationDirection.Backward,
                calendarStart, calendarEnd, ref start, ref end) == false)
            {
                _CalendarView.TimeLineViewScrollStartDate = start;

                DoDateChanged(NavigationDirection.Backward, calendarStart, calendarEnd,
                    _CalendarView.TimeLineViewScrollStartDate, _CalendarView.TimeLineViewScrollEndDate);
            }
        }

        #endregion

        #region GetBackPeriodDate

        /// <summary>
        /// Gets the next back TimeLine Period Date
        /// </summary>
        /// <param name="calendarStart">Current date</param>
        /// <returns>Next back Period Date</returns>
        private DateTime GetBackPeriodDate(DateTime calendarStart)
        {
            // Hours or minutes

            if (_CalendarView.TimeLinePeriod == eTimeLinePeriod.Minutes ||
                _CalendarView.TimeLinePeriod == eTimeLinePeriod.Hours)
            {
                // Go back by a day

                if (calendarStart.Hour == 0)
                    calendarStart = calendarStart.AddDays(-1);

                return (new DateTime(calendarStart.Year,
                    calendarStart.Month, calendarStart.Day, 0, 0, 0));
            }

            // Days

            if (_CalendarView.TimeLinePeriod == eTimeLinePeriod.Days)
            {
                // If the interval is a single day
                // then go back by a month

                if (_CalendarView.TimeLineInterval == 1)
                {
                    if (calendarStart.Day == 1)
                        calendarStart = calendarStart.AddMonths(-1);

                    return (new DateTime(calendarStart.Year, calendarStart.Month, 1));
                }

                // Multi-day interval, go back
                // by a year

                if (calendarStart.AddMinutes(-_CalendarView.BaseInterval).Year < calendarStart.Year)
                    calendarStart = calendarStart.AddYears(-1);

                return (new DateTime(calendarStart.Year, 1, 1));
            }

            // Years

            if (_CalendarView.TimeLinePeriod == eTimeLinePeriod.Years)
            {
                TimeSpan ts = calendarStart - _CalendarView.TimeLineViewStartDate;

                // Go back 10 columns

                int interval = (int)(ts.TotalMinutes / _CalendarView.BaseInterval);

                interval = ((interval % 10 == 0)
                    ? interval - 10 : (interval / 10) * 10);

                return (_CalendarView.TimeLineViewStartDate.AddMinutes(
                    interval * _CalendarView.BaseInterval));
            }

            return (calendarStart);
        }

        #endregion

        #endregion

        #region CountMonthSpan

        /// <summary>
        /// CountMonthSpan
        /// </summary>
        /// <param name="calendarStart"></param>
        /// <param name="calendarEnd"></param>
        /// <returns></returns>
        private int CountMonthSpan(DateTime calendarStart, DateTime calendarEnd)
        {
            int monthSpan = (calendarEnd.Year * 12 + calendarEnd.Month) -
                (calendarStart.Year * 12 + calendarStart.Month);

            return (monthSpan > MaxYearMonthSpan ? MaxYearMonthSpan : monthSpan);
        }

        #endregion

        #region UpdateDisplay

        #region UpdateDisplay

        /// <summary>
        /// Updates the Date Label text
        /// </summary>
        public void UpdateDisplay()
        {
            if (IsDisposed == false)
            {
                if (_CalendarView == null)
                {
                    CurrentDateLabel.Text = "";
                }
                else
                {
                    if (GetDateLabel != null)
                    {
                        GetDateLabelEventArgs args = new GetDateLabelEventArgs();

                        GetDateLabel(this, args);

                        if (string.IsNullOrEmpty(args.DateLabel) == false)
                        {
                            CurrentDateLabel.Text = args.DateLabel;
                            return;
                        }
                    }

                    IFormatProvider formatProvider =
                        ScheduleSettings.GetActiveCulture(); //.GetFormat(typeof (DateTime));

                    switch (_CalendarView.SelectedView)
                    {
                        case eCalendarView.Day:
                            UpdateDayDisplay(formatProvider, _CalendarView.DayViewDate);
                            break;

                        case eCalendarView.Week:
                            UpdateWeekDisplay(formatProvider,
                                _CalendarView.WeekViewStartDate, _CalendarView.WeekViewEndDate);
                            break;

                        case eCalendarView.Month:
                            UpdateMonthDisplay(formatProvider,
                                _CalendarView.MonthViewStartDate, _CalendarView.MonthViewEndDate);
                            break;

                        case eCalendarView.Year:
                            UpdateYearDisplay(formatProvider,
                                _CalendarView.YearViewStartDate, _CalendarView.YearViewEndDate);
                            break;

                        case eCalendarView.TimeLine:
                            UpdateTimeLineDisplay(formatProvider,
                                _CalendarView.TimeLineViewScrollStartDate,
                                _CalendarView.TimeLineViewScrollEndDate);
                            break;
                    }
                }
            }
        }

        #endregion

        #region UpdateDayDisplay

        /// <summary>
        /// Updates the DayView display
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="start"></param>
        private void UpdateDayDisplay(IFormatProvider formatProvider, DateTime start)
        {
            CurrentDateLabel.Text = (start == DateTime.MinValue)
                ? "" : start.ToString(CurrentDayLabel, formatProvider);
        }

        #endregion

        #region UpdateWeekDisplay

        /// <summary>
        /// Updates the WeekView display
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void UpdateWeekDisplay(IFormatProvider formatProvider, DateTime start, DateTime end)
        {
            if (start == DateTime.MinValue || end == DateTime.MinValue)
            {
                CurrentDateLabel.Text = "";
            }
            else
            {
                if (start.Year == end.Year && start.Month == end.Month)
                {
                    CurrentDateLabel.Text =
                        start.ToString(CurrentWeekSameYearStartLabel, formatProvider) + @" - " +
                        end.ToString(CurrentWeekSameYearEndLabel, formatProvider);
                }
                else if (start.Year == end.Year)
                {
                    CurrentDateLabel.Text =
                        start.ToString(CurrentWeekSameMonthYearStartLabel, formatProvider) + @" - " +
                        end.ToString(CurrentWeekSameMonthYearEndLabel, formatProvider);
                }
                else
                {
                    CurrentDateLabel.Text =
                        start.ToString(CurrentWeekStartLabel, formatProvider) + @" - " +
                        end.ToString(CurrentWeekEndLabel, formatProvider);
                }
            }
        }

        #endregion

        #region UpdateMonthDisplay

        /// <summary>
        /// Updates the MonthView display
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void UpdateMonthDisplay(IFormatProvider formatProvider, DateTime start, DateTime end)
        {
            if (start == DateTime.MinValue || end == DateTime.MinValue)
            {
                CurrentDateLabel.Text = "";
            }
            else
            {
                string s = start.ToString(CurrentMonthStartLabel, formatProvider);

                if (end.Year != start.Year)
                    s += " - " + end.ToString(CurrentMonthEndLabel, formatProvider);

                CurrentDateLabel.Text = s;
            }
        }

        #endregion

        #region UpdateYearDisplay

        /// <summary>
        /// Updates the YearView display
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void UpdateYearDisplay(IFormatProvider formatProvider, DateTime start, DateTime end)
        {
            if (start == DateTime.MinValue)
            {
                CurrentDateLabel.Text = "";
            }
            else
            {
                string s = start.ToString(CurrentYearStartLabel, formatProvider);

                if (start.Month != end.Month || start.Year != end.Year)
                    s += " - " + end.ToString(CurrentYearEndLabel, formatProvider);

                CurrentDateLabel.Text = s;
            }
        }

        #endregion

        #region UpdateTimeLineDisplay

        /// <summary>
        /// Updates the TimeLineView display
        /// </summary>
        /// <param name="formatProvider"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        private void UpdateTimeLineDisplay(IFormatProvider formatProvider, DateTime start, DateTime end)
        {
            if (start == DateTime.MinValue || end == DateTime.MinValue)
            {
                CurrentDateLabel.Text = "";
            }
            else
            {
                switch (_CalendarView.TimeLinePeriod)
                {
                    case eTimeLinePeriod.Hours:
                    case eTimeLinePeriod.Minutes:
                        CurrentDateLabel.Text = start.ToString(CurrentTimeLineHourLabel, formatProvider);
                        break;

                    case eTimeLinePeriod.Days:
                        if (start.Year == end.Year && start.Month == end.Month)
                        {
                            CurrentDateLabel.Text =
                                start.ToString(CurrentTimeLineDaySameMonthYearStartLabel, formatProvider) + @" - " +
                                end.ToString(CurrentTimeLineDaySameMonthYearEndLabel, formatProvider);
                        }
                        else if (start.Year == end.Year)
                        {
                            CurrentDateLabel.Text =
                                start.ToString(CurrentTimeLineDaySameYearStartLabel, formatProvider) + @" - " +
                                end.ToString(CurrentTimeLineDaySameYearEndLabel, formatProvider);
                        }
                        else
                        {
                            CurrentDateLabel.Text =
                                start.ToString(CurrentTimeLineDayStartLabel, formatProvider) + @" - " +
                                end.ToString(CurrentTimeLineDayEndLabel, formatProvider);
                        }
                        break;

                    case eTimeLinePeriod.Years:
                        CurrentDateLabel.Text =
                            start.ToString(CurrentTimeLineYearStartLabel, formatProvider) + @" - " +
                            end.ToString(CurrentTimeLineYearEndLabel, formatProvider);
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region UpdateButtonImages

        /// <summary>
        /// Updates the button images
        /// </summary>
        private void UpdateButtonImages()
        {
            Bitmap image = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(GetImageForeColor(), 2))
                {
                    g.DrawLines(pen, new Point[] {
                        new Point(5,0),
                        new Point(1,4),
                        new Point(5,8)
                    });
                    g.DrawLines(pen, new Point[] {
                        new Point(1,4),
                        new Point(9,4)
                    });

                }
            }
            if (NavigateBack.Image != null)
            {
                Image img = NavigateBack.Image;
                NavigateBack.Image = null;
                img.Dispose();
            }
            NavigateBack.Image = image;


            image = new Bitmap(10, 10, PixelFormat.Format32bppArgb);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                using (Pen pen = new Pen(GetImageForeColor(), 2))
                {
                    g.DrawLines(pen, new Point[] {
                        new Point(0,4),
                        new Point(8,4),
                        new Point(4,0),
                        new Point(8,4),
                        new Point(4,8)
                    });
                }
            }
            if (NavigateForward.Image != null)
            {
                Image img = NavigateForward.Image;
                NavigateForward.Image = null;
                img.Dispose();
            }
            NavigateForward.Image = image;
        }

        /// <summary>
        /// Gets image fore color
        /// </summary>
        /// <returns></returns>
        private Color GetImageForeColor()
        {
            if (GlobalManager.Renderer is Office2007Renderer)
            {
                return ((Office2007Renderer)GlobalManager.Renderer).ColorTable.CheckBoxItem.Default.Text;
            }

            return this.ColorScheme.ItemText;
        }

        #endregion

        #region DoDateChanging

        private bool DoDateChanging(NavigationDirection direction,
            DateTime oldStartDate, DateTime oldEndDate, ref DateTime newStartDate, ref DateTime newEndDate)
        {
            if (DateChanging != null)
            {
                DateChangingEventArgs ev = new DateChangingEventArgs(
                    direction, oldStartDate, oldEndDate, newStartDate, newEndDate);

                DateChanging(this, ev);

                if (ev.Cancel == true)
                    return (true);

                newStartDate = ev.NewStartDate;
                newEndDate = ev.NewEndDate;
            }

            return (false);
        }

        #endregion

        #region DoDateChanged

        private void DoDateChanged(NavigationDirection direction,
            DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate)
        {
            if (DateChanged != null)
            {
                DateChangedEventArgs ev = new DateChangedEventArgs(
                    direction, oldStartDate, oldEndDate, newStartDate, newEndDate);

                DateChanged(this, ev);
            }
        }

        #endregion

        #region DateChangingEventArgs

        /// <summary>
        /// DateChangingEventArgs
        /// </summary>
        public class DateChangingEventArgs : DateChangedEventArgs
        {
            #region Private variables

            private bool _Cancel;

            #endregion

            public DateChangingEventArgs(NavigationDirection direction,
                DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate)
                : base(direction, oldStartDate, oldEndDate, newStartDate, newEndDate)
            {
            }

            #region Public properties

            /// <summary>
            /// Gets or sets whether to cancel the operation
            /// </summary>
            public bool Cancel
            {
                get { return (_Cancel); }
                set { _Cancel = value; }
            }

            #endregion
        }

        #endregion

        #region DateChangedEventArgs

        /// <summary>
        /// DateChangingEventArgs
        /// </summary>
        public class DateChangedEventArgs : EventArgs
        {
            #region Private variables

            NavigationDirection _Direction;

            private DateTime _OldStartDate;
            private DateTime _OldEndDate;
            private DateTime _NewStartDate;
            private DateTime _NewEndDate;

            #endregion

            public DateChangedEventArgs(NavigationDirection direction,
                DateTime oldStartDate, DateTime oldEndDate, DateTime newStartDate, DateTime newEndDate)
            {
                _Direction = direction;

                _OldStartDate = oldStartDate;
                _OldEndDate = oldEndDate;
                _NewStartDate = newStartDate;
                _NewEndDate = newEndDate;
            }

            #region Public properties

            /// <summary>
            /// Gets the navigation direction
            /// </summary>
            public NavigationDirection Direction
            {
                get { return (_Direction); }
                set { _Direction = value; }
            }

            /// <summary>
            /// Gets the old navigation start date
            /// </summary>
            public DateTime OldStartDate
            {
                get { return (_OldStartDate); }
            }

            /// <summary>
            /// Gets the old navigation end date
            /// </summary>
            public DateTime OldEndDate
            {
                get { return (_OldEndDate); }
            }

            /// <summary>
            /// Gets or sets the new navigation start date
            /// </summary>
            public DateTime NewStartDate
            {
                get { return (_NewStartDate); }
                set { _NewStartDate = value; }
            }

            /// <summary>
            /// Gets or sets the new navigation end date
            /// </summary>
            public DateTime NewEndDate
            {
                get { return (_NewEndDate); }
                set { _NewEndDate = value; }
            }

            #endregion
        }

        #endregion

        #region GetDateLabelEventArgs

        /// <summary>
        /// GetDateLabelEventArgs
        /// </summary>
        public class GetDateLabelEventArgs : EventArgs
        {
            #region Private variables

            private string _DateLabel;

            #endregion

            #region Public properties

            /// <summary>
            /// Gets the formatted DateLabel
            /// </summary>
            public string DateLabel
            {
                get { return (_DateLabel); }
                set { _DateLabel = value; }
            }

            #endregion
        }

        #endregion
    }

    #region enums

    public enum NavigationDirection
    {
        Forward,
        Backward
    }

    #endregion
}
#endif

