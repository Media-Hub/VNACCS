using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid.SuperGrid;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// FilterExprEdit
    ///</summary>
    internal partial class FilterExprEdit : UserControl
    {
        #region InputTextChanged

        public event EventHandler<EventArgs> InputTextChanged;

        #endregion

        #region Private variables

        private GridPanel _GridPanel;
        private GridColumn _GridColumn;

        private string[] _Fonts;
        private int _FontSize;

        private string _WaterMarkText;

        #endregion

        ///<summary>
        /// FilterExprEdit
        ///</summary>
        public FilterExprEdit()
        {
            InitializeComponent();

            rtbInput.Font = SystemFonts.DialogFont;

            _FontSize = (int)rtbInput.Font.SizeInPoints;
            _Fonts = new string[] { rtbInput.Font.Name };
        }

        #region Public properties

        #region GridColumn

        internal GridColumn GridColumn
        {
            get { return (_GridColumn); }
            set { _GridColumn = value; }
        }

        #endregion

        #region GridPanel

        internal GridPanel GridPanel
        {
            get { return (_GridPanel); }
            set { _GridPanel = value; }
        }

        #endregion

        #region InputText

        internal string InputText
        {
            get { return (rtbInput.Text); }

            set
            {
                rtbInput.Text = value;

                UpdateOutput();
            }
        }

        #endregion

        #region WaterMarkText

        internal string WaterMarkText
        {
            get { return (_WaterMarkText); }

            set
            {
                _WaterMarkText = value;

                UpdateOutput();
            }
        }

        #endregion

        #endregion

        #region SetFocus

        public void SetFocus()
        {
            rtbInput.Focus();
        }

        #endregion

        #region RichTextBoxEx1TextChanged

        private void RichTextBoxEx1TextChanged(object sender, EventArgs e)
        {
            if (InputTextChanged != null)
                InputTextChanged(this, EventArgs.Empty);

            UpdateOutput();
        }

        #endregion

        #region UpdateOutput

        private void UpdateOutput()
        {
            if (string.IsNullOrEmpty(rtbInput.Text) == true)
            {
                OutPutWaterMarkText(rtbOutput);
            }
            else
            {
                try
                {
                    FilterMatchType mtype = (_GridColumn != null)
                        ? _GridColumn.GetFilterMatchType()
                        : _GridPanel.GetFilterMatchType();

                    FilterEval eval = new FilterEval(
                        _GridPanel, _GridColumn, mtype, rtbInput.Text);

                    OutputRtf(rtbOutput,
                        eval.GetInfix(_GridPanel.SuperGrid.FilterColorizeCustomExpr));
                }
                catch (Exception exp)
                {
                    OutputRtfError(rtbOutput, rtbInput.Text, exp.Message);
                }
            }
        }

        #region OutPutWaterMarkText

        private void OutPutWaterMarkText(RichTextBoxEx rtbOut)
        {
            if (_GridPanel != null &&
                string.IsNullOrEmpty(_WaterMarkText) == false)
            {
                Rtf myRtf = new Rtf(_Fonts,
                    _GridPanel.SuperGrid.FilterExprColors.Colors);

                myRtf.BeginGroup(true);

                myRtf.Font = 0;
                myRtf.FontSize = _FontSize;
                myRtf.CenterAlignText();

                myRtf.ForeColor = (int)ExprColorPart.Dim;

                myRtf.WriteLine();
                myRtf.WriteText(_WaterMarkText);

                myRtf.EndGroup();
                myRtf.Close();

                rtbOut.Rtf = myRtf.RtfText;
            }
            else
            {
                rtbOut.Clear();
            }
        }

        #endregion

        #region OutputRtf

        private void OutputRtf(RichTextBoxEx rtb, string s)
        {
            s = s.Trim();

            if (string.IsNullOrEmpty(s) == false)
            {
                Rtf myRtf = new Rtf(_Fonts,
                    _GridPanel.SuperGrid.FilterExprColors.Colors);

                myRtf.BeginGroup(true);

                myRtf.Font = 0;
                myRtf.FontSize = _FontSize;
                myRtf.ForeColor = (int)ExprColorPart.Default;

                Regex r = new Regex("(#@@#\\d)");
                MatchCollection mc = r.Matches(s);

                if (mc.Count > 0)
                {
                    int index = 0;

                    for (int i = 0; i < mc.Count; i++)
                    {
                        Match ma = mc[i];

                        if (ma.Index > index)
                            myRtf.WriteText(s.Substring(index, ma.Index - index));

                        index = ma.Index + ma.Length;

                        myRtf.ForeColor = s[index - 1] - '0';
                    }

                    if (s.Length > index)
                        myRtf.WriteText(s.Substring(index, s.Length - index));
                }
                else
                {
                    myRtf.WriteText(s);
                }

                myRtf.EndGroup();
                myRtf.Close();

                rtb.Rtf = myRtf.RtfText;
            }
            else
            {
                rtb.Text = "";
            }
        }

        #endregion

        #region OutputRtfError

        private void OutputRtfError(
            RichTextBoxEx rtbOut, string text, string error)
        {
            Rtf myRtf = new Rtf(_Fonts,
                _GridPanel.SuperGrid.FilterExprColors.Colors);

            myRtf.BeginGroup(true);

            myRtf.Font = 0;
            myRtf.FontSize = _FontSize;
            myRtf.ForeColor = (int)ExprColorPart.Default;

            myRtf.WriteText(text);
            myRtf.WriteText("  ");

            myRtf.ForeColor = (int)ExprColorPart.Error;

            myRtf.WriteLine();
            myRtf.WriteLine();
            myRtf.WriteText("(" + error + ")");

            myRtf.EndGroup();
            myRtf.Close();

            rtbOut.Rtf = myRtf.RtfText;
        }

        #endregion

        #endregion
    }
}
