using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// FilterDateTimePicker
    ///</summary>
    [ToolboxItem(false)]
    public partial class FilterDateTimePicker : UserControl
    {
        #region Events

        #region ValueChanged

        /// <summary>
        /// Occurs when the DateTimePicker value has changed
        /// </summary>
        [Description("Occurs when the DateTimePicker value has changed.")]
        public event EventHandler<EventArgs> ValueChanged;

        #endregion

        #endregion

        #region Static data

        private static bool _localizedStringsLoaded;

        private static string _filterByRelativeDateString = "Filter by relative date";
        private static string _filterBySpecificDateString = "Filter by specific date";
        private static string _filterByDateRangeString = "Filter by date range";

        private static string _currentMonth = "Current month";
        private static string _currentYear = "Current year";

        private static string _lastMonthPeriod = "Last month period";
        private static string _last3MonthPeriod = "Last 3 month period";
        private static string _last6MonthPeriod = "Last 6 month period";
        private static string _last9MonthPeriod = "Last 9 month period";

        private static string _lastYear = "Last Year";
        private static string _last5YearPeriod = "Last 5 year period";
        private static string _last10YearPeriod = "Last 10 year period";

        #endregion

        #region Private variables

        private GridColumn _GridColumn;
        private string _CustomExpr;
        private bool _Cancel;

        #endregion

        /// <summary>
        /// FilterDateTimePicker
        /// </summary>
        /// <param name="gridColumn"></param>
        public FilterDateTimePicker(GridColumn gridColumn)
        {
            InitializeComponent();

            SuperGridControl sg = gridColumn.SuperGrid;

            LoadLocalizedStrings(sg);

            cbxShowAll.Text = sg.FilterShowAllString;
            cbxShowNull.Text = sg.FilterShowNullString;
            cbxShowNotNull.Text = sg.FilterShowNotNullString;

            if (string.IsNullOrEmpty(cbxShowNull.Text) == true)
                cbxShowNull.Visible = false;

            if (string.IsNullOrEmpty(cbxShowNotNull.Text) == true)
                cbxShowNotNull.Visible = false;

            if (cbxShowNull.Visible == false || cbxShowNotNull.Visible == false)
            {
                int y = (cbxDateRelative.Location.Y - cbxShowAll.Size.Height) / 2;

                cbxShowAll.Location = new Point(cbxShowAll.Location.X, y);

                if (cbxShowNull.Visible == true)
                    cbxShowNull.Location = new Point(cbxShowNull.Location.X, y);

                if (cbxShowNotNull.Visible == true)
                    cbxShowNotNull.Location = new Point(cbxShowNotNull.Location.X, y);
            }

            btnApply.Text = sg.FilterApplyString;

            if (sg.FilterCustomString != null)
            {
                cbxCustom.Text = sg.FilterCustomString;
                btnCustom.Text = sg.FilterCustomString + "...";
            }
            else
            {
                cbxCustom.Visible = false;
                btnCustom.Visible = false;
            }

            cbxDateRelative.Text = _filterByRelativeDateString;
            cbxDateSpecific.Text = _filterBySpecificDateString;
            cbxDateRange.Text = _filterByDateRangeString;

            cboDateRelative.Items.Add(CurrentMonth);
            cboDateRelative.Items.Add(CurrentYear);
            cboDateRelative.Items.Add(LastMonthPeriod);
            cboDateRelative.Items.Add(Last3MonthPeriod);
            cboDateRelative.Items.Add(Last6MonthPeriod);
            cboDateRelative.Items.Add(Last9MonthPeriod);
            cboDateRelative.Items.Add(LastYear);
            cboDateRelative.Items.Add(Last5YearPeriod);
            cboDateRelative.Items.Add(Last10YearPeriod);

            cboDateRelative.SelectedIndex = 0;

            _GridColumn = gridColumn;

            FilterColumnHeaderVisualStyle style = _GridColumn.GetFilterStyle(StyleType.Default);

            Font = style.Font;
            BackColor = style.Background.Color1;

            Cancel = false;
        }

        #region Public properties

        #region AcceptButton

        ///<summary>
        /// AcceptButton
        ///</summary>
        public IButtonControl AcceptButton
        {
            get { return (btnApply); }
        }

        #endregion

        #region Cancel

        ///<summary>
        /// Cancel
        ///</summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion

        #region CurrentMonth

        ///<summary>
        /// CurrentMonth
        ///</summary>
        [Localizable(true)]
        public string CurrentMonth
        {
            get { return (_currentMonth); }
            set { _currentMonth = value; }
        }

        #endregion

        #region CurrentYear

        ///<summary>
        /// CurrentYear
        ///</summary>
        [Localizable(true)]
        public string CurrentYear
        {
            get { return (_currentYear); }
            set { _currentYear = value; }
        }

        #endregion

        #region CustomFilter

        ///<summary>
        /// CustomFilter
        ///</summary>
        [Localizable(true)]
        public string CustomFilter
        {
            get { return ("Custom Filter"); }
        }

        #endregion

        #region LastMonthPeriod

        ///<summary>
        /// LastMonthPeriod
        ///</summary>
        [Localizable(true)]
        public string LastMonthPeriod
        {
            get { return (_lastMonthPeriod); }
            set { _lastMonthPeriod = value; }
        }

        #endregion

        #region Last3MonthPeriod

        ///<summary>
        /// Last3MonthPeriod
        ///</summary>
        [Localizable(true)]
        public string Last3MonthPeriod
        {
            get { return (_last3MonthPeriod); }
            set { _last3MonthPeriod = value; }
        }

        #endregion

        #region Last6MonthPeriod

        ///<summary>
        /// Last6MonthPeriod
        ///</summary>
        [Localizable(true)]
        public string Last6MonthPeriod
        {
            get { return (_last6MonthPeriod); }
            set { _last6MonthPeriod = value; }
        }

        #endregion

        #region Last9MonthPeriod

        ///<summary>
        /// Last9MonthPeriod
        ///</summary>
        [Localizable(true)]
        public string Last9MonthPeriod
        {
            get { return (_last9MonthPeriod); }
            set { _last9MonthPeriod = value; }
        }

        #endregion

        #region LastYear

        ///<summary>
        /// LastYear
        ///</summary>
        [Localizable(true)]
        public string LastYear
        {
            get { return (_lastYear); }
            set { _lastYear = value; }
        }

        #endregion

        #region Last5YearPeriod

        ///<summary>
        /// Last5YearPeriod
        ///</summary>
        [Localizable(true)]
        public string Last5YearPeriod
        {
            get { return (_last5YearPeriod); }
            set { _last5YearPeriod = value; }
        }

        #endregion

        #region Last10YearPeriod

        ///<summary>
        /// Last10YearPeriod
        ///</summary>
        [Localizable(true)]
        public string Last10YearPeriod
        {
            get { return (_last10YearPeriod); }
            set { _last10YearPeriod = value; }
        }

        #endregion

        #endregion

        #region GetFilterExpr

        /// <summary>
        /// GetFilterExpr
        /// </summary>
        /// <returns></returns>
        public string GetFilterExpr()
        {
            if (cbxCustom.Checked == true)
                return (_CustomExpr);

            if (cbxDateSpecific.Checked == true)
                return (DateSpecificExpr);

            if (cbxDateRange.Checked == true)
                return (DateRangeExpr);

            if (cbxDateRelative.Checked == true)
                return (DateRelativeExpr);

            if (cbxShowNull.Checked == true)
                return (ShowNullExpr);

            if (cbxShowNotNull.Checked == true)
                return (ShowNotNullExpr);

            return (null);
        }

        #region ShowNullExpr

        private string ShowNullExpr
        {
            get { return ("[" + _GridColumn.Name + "] = null"); }
        }

        #endregion

        #region ShowNotNullExpr

        private string ShowNotNullExpr
        {
            get { return ("[" + _GridColumn.Name + "] != null"); }
        }

        #endregion

        #region DateSpecificExpr

        private string DateSpecificExpr
        {
            get { return ("Date([" + _GridColumn.Name +"]) = Date(#" +
                dtiSpecificDate.Value.Date.ToShortDateString() + "#)"); }
            }

        #endregion

        #region DateRangeExpr

        private string DateRangeExpr
        {
            get
            {
                return ("Date([" +
                    _GridColumn.Name + "]) >= #" + dtiFromDate.Value.ToShortDateString() + "# And Date([" +
                    _GridColumn.Name + "]) <= #" + dtiToDate.Value.ToShortDateString() + "#");
            }
        }

        #endregion

        #region DateRelativeExpr

        private string DateRelativeExpr
        {
            get
            {
                string cname = "[" + _GridColumn.Name + "]";

                switch (cboDateRelative.SelectedIndex)
                {
                    case (int)RelativeFilter.CurrentMonth:
                        return (CurrentMonthExpr(cname));

                    case (int)RelativeFilter.CurrentYear:
                        return (CurrentYearExpr(cname));

                    case (int)RelativeFilter.LastMonthPeriod:
                        return (LastMonthExpr(cname, 1));

                    case (int)RelativeFilter.Last3MonthPeriod:
                        return (LastMonthExpr(cname, 3));

                    case (int)RelativeFilter.Last6MonthPeriod:
                        return (LastMonthExpr(cname, 6));

                    case (int)RelativeFilter.Last9MonthPeriod:
                        return (LastMonthExpr(cname, 9));

                    case (int) RelativeFilter.LastYear:
                        return (LastYearExpr(cname, 1));

                    case (int)RelativeFilter.Last5YearPeriod:
                        return (LastYearExpr(cname, 5));

                    case (int)RelativeFilter.Last10YearPeriod:
                        return (LastYearExpr(cname, 10));
                }

                return (null);
            }
        }

        #region CurrentMonthExpr

        private string CurrentMonthExpr(string cname)
        {
            string s = String.Format(
                "Year({0}) = Year() And Month({0}) = Month()", cname);

           return (s);
       }

        #endregion

        #region LastMonthExpr

        private string LastMonthExpr(string cname, int span)
        {
            string s = String.Format(
                "Date({0}) Is Between " +
                "FirstOfMonth(AddMonths(Date(), {1})) And " +
                "EndOfMonth(Date())", cname, -span);

            return (s);
        }

       #endregion

        #region CurrentYearExpr

        private string CurrentYearExpr(string cname)
        {
            string s = String.Format(
                "Year({0}) = Year()", cname);

            return (s);
        }

        #endregion

        #region LastYearExpr

        private string LastYearExpr(string cname, int span)
        {
            string s = String.Format(
                "Year({0}) Is Between " +
                "Year(AddYears(Date(), {1})) And " +
                "Year()", cname, -span);

            return (s);
        }

        #endregion

        #endregion

        #endregion

        #region ProcessTabKey

        protected override bool ProcessTabKey(bool forward)
        {
            return (SelectNextControl(ActiveControl, forward, false, false, true));
        }

        #endregion

        #region ProcessDialogKey

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData == Keys.Escape)
                Cancel = true;

            else if (keyData == Keys.Enter)
                OnValueChanged();

            return base.ProcessDialogKey(keyData);
        }

        #endregion

        #region BtnCustomClick

        private void BtnCustomClick(object sender, EventArgs e)
        {
            cbxCustom.Checked = true;

            LaunchCustomDialog(_CustomExpr);

            OnValueChanged();
        }

        #region LaunchCustomDialog

        private void LaunchCustomDialog(string filterExpr)
        {
            GridPanel panel = _GridColumn.GridPanel;

            if (panel.SuperGrid.FilterUseExtendedCustomDialog == true)
            {
                CustomFilterEx cf = new CustomFilterEx(panel, _GridColumn, filterExpr);

                cf.Text = "'" + _GridColumn.Name + "'" + CustomFilter;
                cf.ShowInPopupVisible = false;

                DialogResult dr = cf.ShowDialog();

                if (dr == DialogResult.OK)
                    _CustomExpr = cf.FilterExpr;
            }
            else
            {
                CustomFilter cf = new CustomFilter(panel, _GridColumn, filterExpr);

                cf.Text = "'" + _GridColumn.Name + "'" + CustomFilter;

                DialogResult dr = cf.ShowDialog();

                if (dr == DialogResult.OK)
                    _CustomExpr = cf.FilterExpr;
            }

            panel.SuperGrid.Focus();
        }

        #endregion

        #endregion

        #region BtnApplyClick

        private void BtnApplyClick(object sender, EventArgs e)
        {
            OnValueChanged();
        }

        #endregion

        #region BtnOkClick

        private void BtnOkClick(object sender, EventArgs e)
        {
            OnClose();
        }

        #endregion

        #region BtnCancelClick

        private void BtnCancelClick(object sender, EventArgs e)
        {
            OnCancel();
        }

        #endregion

        #region OnValueChanged

        private void OnValueChanged()
        {
            Parent.Invalidate();

            if (ValueChanged != null)
                ValueChanged(this, EventArgs.Empty);
        }

        #endregion

        #region OnClose

        private void OnClose()
        {
            OnValueChanged();

            PopupDropDown pdd = Parent as PopupDropDown;

            if (pdd != null)
                pdd.Hide();
        }

        #endregion

        #region OnCancel

        private void OnCancel()
        {
            _Cancel = true;

            PopupDropDown pdd = Parent as PopupDropDown;

            if (pdd != null)
                pdd.Hide();
        }

        #endregion

        #region DtiSpecificDateMouseDown

        private void DtiSpecificDateMouseDown(object sender, MouseEventArgs e)
        {
            cbxDateSpecific.Checked = true;
        }

        #endregion

        #region DtiDateRangeMouseDown

        private void DtiDateRangeMouseDown(object sender, MouseEventArgs e)
        {
            cbxDateRange.Checked = true;
        }

        #endregion

        #region CboDateRelativeMouseDown

        private void CboDateRelativeMouseDown(object sender, MouseEventArgs e)
        {
            cbxDateRelative.Checked = true;
        }

        #endregion

        #region LoadLocalizedStrings

        private void LoadLocalizedStrings(SuperGridControl sg)
        {
            if (_localizedStringsLoaded == false)
            {
                using (LocalizationManager lm = new LocalizationManager(sg))
                {
                    string s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterByRelativeDate)) != "")
                        _filterByRelativeDateString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterBySpecificDate)) != "")
                        _filterBySpecificDateString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterByDateRange)) != "")
                        _filterByDateRangeString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterCurrentMonth)) != "")
                        _currentMonth = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterCurrentYear)) != "")
                        _currentYear = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterLastMonthPeriod)) != "")
                        _lastMonthPeriod = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterLast3MonthPeriod)) != "")
                        _last3MonthPeriod = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterLast6MonthPeriod)) != "")
                        _last6MonthPeriod = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterLast9MonthPeriod)) != "")
                        _last9MonthPeriod = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterLastYear)) != "")
                        _lastYear = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterLast5YearPeriod)) != "")
                        _last5YearPeriod = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterLast10YearPeriod)) != "")
                        _last10YearPeriod = s;
                }

                _localizedStringsLoaded = true;
            }
        }

        #endregion
    }

    #region enums

    #region RelativeFilter

    /// <summary>
    /// RelativeFilter
    /// </summary>
    public enum RelativeFilter
    {
        /// <summary>
        /// CurrentMonth
        /// </summary>
        CurrentMonth,

        /// <summary>
        /// CurrentYear
        /// </summary>
        CurrentYear,

        /// <summary>
        /// LastMonthPeriod
        /// </summary>
        LastMonthPeriod,

        /// <summary>
        /// Last3MonthPeriod
        /// </summary>
        Last3MonthPeriod,

        /// <summary>
        /// Last6MonthPeriod
        /// </summary>
        Last6MonthPeriod,

        /// <summary>
        /// Last9MonthPeriod
        /// </summary>
        Last9MonthPeriod,

        /// <summary>
        /// LastYear,
        /// </summary>
        LastYear,

        /// <summary>
        /// Last5YearPeriod
        /// </summary>
        Last5YearPeriod,

        /// <summary>
        /// Last10YearPeriod
        /// </summary>
        Last10YearPeriod,
    }

    #endregion

    #endregion
}
