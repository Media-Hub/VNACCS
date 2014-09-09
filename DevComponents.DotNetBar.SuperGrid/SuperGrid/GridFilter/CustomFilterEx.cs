using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// CustomFilter
    ///</summary>
    internal partial class CustomFilterEx : Office2007Form
    {
        #region Static data

        private static bool _localizedStringsLoaded;

        private static string _filterClearString = "Clear";
        private static string _filterResetString = "Reset";
        private static string _filterCustomFilterString = "Custom Filter";
        private static string _filterEnterExprString = "Enter custom filter expression";
        private static string _filterHelpString = "Click the Title Bar help button for syntax help";
        private static string _newFilterExprString = "NewFilter";
        private static string _filterDescriptionString = "Description";
        private static string _filterNameString = "Filter Name:";
        private static string _showInFilterPopupString = "Show in FilterPopup";
        private static string _newFilterString = "New";
        private static string _deleteFilterString = "Delete";
        private static string _filterHelpTitle = "SampleExpr";

        private static Point _offset = Point.Empty;
        private static Size _size = Size.Empty;

        #endregion

        #region Private variables

        private GridPanel _GridPanel;
        private GridColumn _GridColumn;

        private string _FilterExpr;
        private object _FilterValue;
        private object _FilterDisplayValue;

        private List<UserFilterData> _FilterData;
        private UserFilterData _CurrentFilter;

        private bool _CancelFilterEdits;
        private bool _LockedResize = true;

        #endregion

        ///<summary>
        /// CustomFilter
        ///</summary>
        public CustomFilterEx(GridPanel gridPanel, GridColumn gridColumn)
            : this(gridPanel, gridColumn, gridColumn != null ? gridColumn.FilterExpr : gridPanel.FilterExpr)
        {
        }

        ///<summary>
        /// CustomFilter
        ///</summary>
        public CustomFilterEx(GridPanel gridPanel,
            GridColumn gridColumn, string filterText)
        {
            _GridPanel = gridPanel;
            _GridColumn = gridColumn;

            InitializeComponent();
            InitializeText();
            InitializeFilter(filterText);

            PositionWindow(_GridPanel.SuperGrid);

            FormClosing += CustomFilterExFormClosing;

            filterExprEdit1.InputTextChanged += FilterExprEdit1InputTextChanged;
        }

        #region Public properties

        #region FilterExpr

        /// <summary>
        /// FilterExpr
        /// </summary>
        public string FilterExpr
        {
            get { return (filterExprEdit1.InputText); }
        }

        #endregion

        #region ShowInPopupVisible

        /// <summary>
        /// ShowInPopupVisible
        /// </summary>
        public bool ShowInPopupVisible
        {
            get { return (cbxShowInPopup.Visible); }
            set { cbxShowInPopup.Visible = value; }
        }

        #endregion

        #endregion

        #region Private properties

        #region CurrentFilter

        private UserFilterData CurrentFilter
        {
            get { return (_CurrentFilter); }

            set
            {
                if (_CurrentFilter != value)
                {
                    _CurrentFilter = value;

                    UpdateDisplay();
                }
            }
        }

        #endregion

        #endregion

        #region Initialization

        #region InitializeText

        private void InitializeText()
        {
            SuperGridControl superGrid = _GridPanel.SuperGrid;

            LoadLocalizedStrings(superGrid);

            TitleText = _filterCustomFilterString;

            lblEnterText.Text = _filterEnterExprString;
            lblDesc.Text = _filterDescriptionString;
            lblFilterName.Text = _filterNameString;

            btnNewFilter.Text = _newFilterString;
            btnDeleteFilter.Text = _deleteFilterString;
            btnReset.Text = _filterResetString;
            btnClear.Text = _filterClearString;

            btnApply.Text = superGrid.FilterApplyString;
            btnOk.Text = superGrid.FilterOkString;
            btnCancel.Text = superGrid.FilterCancelString;

            cbxShowInPopup.Text = _showInFilterPopupString;

            if (superGrid.ShowCustomFilterHelp == true)
                filterExprEdit1.WaterMarkText = _filterHelpString;
            else
                HelpButton = false;
        }

        #endregion

        #region InitializeFilter

        private void InitializeFilter(string filterText)
        {
            if (_GridColumn != null)
            {
                _FilterExpr = _GridColumn.FilterExpr;
                _FilterValue = _GridColumn.FilterValue;
                _FilterDisplayValue = _GridColumn.FilterDisplayValue;
            }
            else
            {
                _FilterExpr = _GridPanel.FilterExpr;
                _FilterValue = null;
                _FilterDisplayValue = null;

                cbxShowInPopup.Visible = false;
            }

            filterExprEdit1.GridPanel = _GridPanel;
            filterExprEdit1.GridColumn = _GridColumn;
            filterExprEdit1.InputText = filterText;

            CurrentFilter = GetCurrentFilter();
        }

        #region GetCurrentFilter

        private UserFilterData GetCurrentFilter()
        {
            _FilterData = FilterUserData.LoadFilterData(_GridPanel);

            foreach (UserFilterData fd in _FilterData)
            {
                if (string.IsNullOrEmpty(fd.Expression) == false)
                {
                    if (fd.Expression.Equals(filterExprEdit1.InputText) == true)
                        return (fd);
                }
            }

            UserFilterData nfd = GetNewFilter();

            nfd.Expression = filterExprEdit1.InputText;

            return (nfd);
        }

        #endregion

        #endregion

        #region PositionWindow

        private void PositionWindow(SuperGridControl superGrid)
        {
            if (_size != Size.Empty)
                Size = _size;

            Form form = superGrid.FindForm();

            if (form != null)
            {
                Point pt = form.Location;

                if (_offset != Point.Empty)
                {
                    pt.Offset(_offset);

                    Location = pt;
                }
                else
                {
                    Screen screen = Screen.FromControl(form);

                    int boundWidth = screen.Bounds.Width;
                    int boundHeight = screen.Bounds.Height;

                    int x = boundWidth - Width;
                    int y = boundHeight - Height;

                    Location = new Point(screen.Bounds.X + x / 2, screen.Bounds.Y + y / 2);
                }
            }

            _LockedResize = false;
        }

        #endregion

        #endregion

        #region Event processong

        #region CustomFilterExFormClosing

        void CustomFilterExFormClosing(object sender, FormClosingEventArgs e)
        {
            if (_CancelFilterEdits == false)
                FilterUserData.StoreFilterData(_GridPanel, _FilterData);
        }

        #endregion

        #region CustomFilterShown

        private void CustomFilterShown(object sender, EventArgs e)
        {
            filterExprEdit1.SetFocus();
        }

        #endregion

        #region FilterExprEdit1InputTextChanged

        void FilterExprEdit1InputTextChanged(object sender, EventArgs e)
        {
            CurrentFilter.Expression = filterExprEdit1.InputText;
        }

        #endregion

        #region OnLocationChanged

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);

            if (_LockedResize == false)
            {
                Form form = _GridPanel.SuperGrid.FindForm();

                if (form != null)
                {
                    Point pt = Location;

                    pt.Offset(-form.Location.X, -form.Location.Y);

                    _offset = pt;
                }
            }
        }

        #endregion

        #region OnResize

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_LockedResize == false)
                _size = Size;
        }

        #endregion

        #endregion

        #region UpdateDisplay

        private void UpdateDisplay()
        {
            cbFilterName.Text = CurrentFilter.Name;
            tbxDesc.Text = CurrentFilter.Description;

            if (_GridColumn != null)
                cbxShowInPopup.Checked = CurrentFilter.ReferNames.Contains(_GridColumn.Name);

            filterExprEdit1.InputText = CurrentFilter.Expression;
        }

        #endregion

        #region Button processing

        #region BtnApplyClick

        private void BtnApplyClick(object sender, EventArgs e)
        {
            ApplyFilter(filterExprEdit1.InputText);
        }

        #endregion

        #region BtnResetClick

        private void BtnResetClick(object sender, EventArgs e)
        {
            ApplyFilter(null);

            Close();
        }

        #endregion

        #region BtnDeleteFilterClick

        private void BtnDeleteFilterClick(object sender, EventArgs e)
        {
            if (CurrentFilter != null)
            {
                _FilterData.Remove(CurrentFilter);

                UpdateFilterNameCombo();

                CurrentFilter = (_FilterData.Count > 0)
                    ? _FilterData[0] : GetNewFilter();
            }
        }

        #endregion

        #region BtnCancelClick

        private void BtnCancelClick(object sender, EventArgs e)
        {
            if (_GridColumn != null)
            {
                _GridColumn.FilterExpr = _FilterExpr;
                _GridColumn.FilterValue = _FilterValue;
                _GridColumn.FilterDisplayValue = _FilterDisplayValue;
            }
            else
            {
                _GridPanel.FilterExpr = _FilterExpr;
            }

            _CancelFilterEdits = true;
        }

        #endregion

        #region BtnClearClick

        private void BtnClearClick(object sender, EventArgs e)
        {
            filterExprEdit1.InputText = "";
        }

        #endregion

        #region BtnNewFilterClick

        private void BtnNewFilterClick(object sender, EventArgs e)
        {
            CurrentFilter = GetNewFilter();
        }

        #region GetNewFilter

        private UserFilterData GetNewFilter()
        {
            UserFilterData fd = new UserFilterData();

            fd.Name = GetNewFilterName();
            fd.Description = "";

            _FilterData.Add(fd);

            return (fd);
        }

        #region GetNewFilterName

        private string GetNewFilterName()
        {
            for (int i = 1; i < 100; i++)
            {
                string s = _newFilterExprString + i;

                if (FindFilterName(s) < 0)
                    return(s);
            }

            return (_newFilterExprString);
        }

        #region FindFilterName

        private int FindFilterName(string s)
        {
            for (int i = 0; i < _FilterData.Count; i++)
            {
                if (s.Equals(_FilterData[i].Name) == true)
                    return (i);
            }

            return (-1);
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region BtnOkClick

        private void BtnOkClick(object sender, EventArgs e)
        {
            ApplyFilter(filterExprEdit1.InputText);
        }

        #endregion

        #region CustomFilterHelpButtonClicked

        static protected SampleExpr Se;

        private void CustomFilterHelpButtonClicked(object sender, CancelEventArgs e)
        {
            if (Se == null)
            {
                Se = new SampleExpr(_GridPanel.SuperGrid);
                Se.TitleText = _filterHelpTitle;

                Se.FormClosed += SeFormClosed;
            }

            if (_GridPanel.SuperGrid.DoFilterHelpOpeningEvent(_GridPanel, _GridColumn, Se) == true)
            {
                Se.Close();
            }
            else
            {
                Se.Show();

                if (Se.WindowState == FormWindowState.Minimized)
                    Se.WindowState = FormWindowState.Normal;

                Se.BringToFront();
            }

            e.Cancel = true;
        }

        void SeFormClosed(object sender, FormClosedEventArgs e)
        {
            _GridPanel.SuperGrid.DoFilterHelpClosingEvent(_GridPanel, _GridColumn, Se);

            Se.FormClosed -= SeFormClosed;
            Se = null;
        }

        #endregion

        #region ApplyFilter

        private void ApplyFilter(string text)
        {
            object filterValue = null;
            object filterDisplayValue = text;
            string filterExpr = text;

            if (_GridColumn != null)
            {
                if (_GridColumn.SuperGrid.DoFilterEditValueChangedEvent(_GridPanel, _GridColumn, null,
                    _GridColumn.FilterValue, ref filterValue, ref filterDisplayValue, ref filterExpr) == false)
                {
                    _GridColumn.FilterExpr = text;
                    _GridColumn.FilterValue = null;
                    _GridColumn.FilterDisplayValue = text;
                }
            }
            else
            {
                if (_GridPanel.SuperGrid.DoFilterEditValueChangedEvent(_GridPanel, null, null,
                    _GridPanel.FilterExpr, ref filterValue, ref filterDisplayValue, ref filterExpr) == false)
                {
                    _GridPanel.FilterExpr = text;
                }
            }
        }

        #endregion

        #endregion

        #region Checkbox processing

        #region CbxShowInPopupCheckedChanged

        private void CbxShowInPopupCheckedChanged(object sender, EventArgs e)
        {
            if (_GridColumn != null)
            {
                if (cbxShowInPopup.Checked == true)
                {
                    if (CurrentFilter.ReferNames.Contains(_GridColumn.Name) == false)
                        CurrentFilter.ReferNames.Add(_GridColumn.Name);
                }
                else
                {
                    if (CurrentFilter.ReferNames.Contains(_GridColumn.Name) == true)
                        CurrentFilter.ReferNames.Remove(_GridColumn.Name);
                }
            }
        }

        #endregion

        #endregion

        #region Combobox processing

        #region CbFilterNameDropDown

        private void CbFilterNameDropDown(object sender, EventArgs e)
        {
            UpdateFilterNameCombo();
        }

        #region UpdateFilterNameCombo

        private void UpdateFilterNameCombo()
        {
            cbFilterName.Items.Clear();

            foreach (UserFilterData fd in _FilterData)
                cbFilterName.Items.Add(fd.Name);
        }

        #endregion

        #endregion

        #region CbFilterNameSelectedIndexChanged

        private void CbFilterNameSelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterName.SelectedIndex >= 0)
                CurrentFilter = _FilterData[cbFilterName.SelectedIndex];
        }

        #endregion

        #region CbFilterNameTextUpdate

        private void CbFilterNameTextUpdate(object sender, EventArgs e)
        {
            CurrentFilter.Name = cbFilterName.Text;
        }

        #endregion

        #endregion

        #region TextBox processing

        #region TbxDescTextChanged

        private void TbxDescTextChanged(object sender, EventArgs e)
        {
            CurrentFilter.Description = tbxDesc.Text;
        }

        #endregion

        #endregion

        #region LoadLocalizedStrings

        private void LoadLocalizedStrings(SuperGridControl sg)
        {
            if (_localizedStringsLoaded == false)
            {
                using (LocalizationManager lm = new LocalizationManager(sg))
                {
                    string s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterCustomHelp)) != "")
                        _filterHelpString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterEnterExpr)) != "")
                        _filterEnterExprString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterClear)) != "")
                        _filterClearString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterReset)) != "")
                        _filterResetString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterCustomFilter)) != "")
                        _filterCustomFilterString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridNewFilterExpr)) != "")
                        _newFilterExprString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridNewFilter)) != "")
                        _newFilterString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridDeleteFilter)) != "")
                        _deleteFilterString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterName)) != "")
                        _filterNameString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterDescription)) != "")
                        _filterDescriptionString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridShowInFilterPopup)) != "")
                        _showInFilterPopupString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterHelpTitle)) != "")
                        _filterHelpTitle = s;
                }

                _localizedStringsLoaded = true;
            }
        }

        #endregion
    }
}