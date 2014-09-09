using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// CustomFilter
    ///</summary>
    public partial class CustomFilter : Office2007Form
    {
        #region Static data

        private static bool _localizedStringsLoaded;

        private static string _filterClearString = "Clear";
        private static string _filterCustomFilterString = "Custom Filter";
        private static string _filterEnterExprString = "Enter custom filter expression";
        private static string _filterHelpString = "Click the Title Bar help button for syntax help";
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

        private bool _LockedResize = true;

        #endregion

        ///<summary>
        /// CustomFilter
        ///</summary>
        public CustomFilter(GridPanel gridPanel, GridColumn gridColumn)
            : this(gridPanel, gridColumn, gridColumn != null ? gridColumn.FilterExpr : gridPanel.FilterExpr)
        {
        }

        ///<summary>
        /// CustomFilter
        ///</summary>
        public CustomFilter(GridPanel gridPanel, GridColumn gridColumn, string filterText)
        {
            _GridPanel = gridPanel;
            _GridColumn = gridColumn;

            InitializeComponent();
            InitializeText();
            InitializeFilter(filterText);

            PositionWindow(_GridPanel.SuperGrid);
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

        #endregion

        #region Initialization

        #region InitializeText

        private void InitializeText()
        {
            SuperGridControl superGrid = _GridPanel.SuperGrid;

            LoadLocalizedStrings(superGrid);

            TitleText = _filterCustomFilterString;

            lblEnterText.Text = _filterEnterExprString;

            btnClear.Text = _filterClearString;
            btnApply.Text = superGrid.FilterApplyString;
            btnOk.Text = superGrid.FilterOkString;
            btnCancel.Text = superGrid.FilterCancelString;

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
            }

            filterExprEdit1.GridPanel = _GridPanel;
            filterExprEdit1.GridColumn = _GridColumn;
            filterExprEdit1.InputText = filterText;
        }

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

        #region CustomFilterShown

        private void CustomFilterShown(object sender, EventArgs e)
        {
            filterExprEdit1.SetFocus();
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

        #endregion

        #region Button processing

        #region BtnApplyClick

        private void BtnApplyClick(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        #region ApplyFilter

        private void ApplyFilter()
        {
            string text = filterExprEdit1.InputText;

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

        #region BtnOkClick

        private void BtnOkClick(object sender, EventArgs e)
        {
            ApplyFilter();
        }

        #endregion

        #region BtnCancelClick

        private void BtnCancelClick(object sender, EventArgs e)
        {
            CancelFilter();
        }

        private void CancelFilter()
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
        }

        #endregion

        #region BtnClearClick

        private void BtnClearClick(object sender, EventArgs e)
        {
            filterExprEdit1.InputText = "";
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

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterCustomFilter)) != "")
                        _filterCustomFilterString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterHelpTitle)) != "")
                        _filterHelpTitle = s;
                }

                _localizedStringsLoaded = true;
            }
        }

        #endregion
    }
}