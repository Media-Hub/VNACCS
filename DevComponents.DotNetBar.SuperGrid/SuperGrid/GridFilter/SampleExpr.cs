using System;
using System.IO;
using DevComponents.DotNetBar.Controls;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// SampleExpr
    /// </summary>
    public partial class SampleExpr : Office2007Form
    {
        ///<summary>
        /// SampleExpr
        ///</summary>
        public SampleExpr(SuperGridControl superGrid)
        {
            InitializeComponent();

            btnClose.Text = superGrid.FilterCloseString;

            string s = GetRtbText(superGrid);

            try
            {
                richTextBoxEx1.Rtf = s;
            }
            catch
            {
                richTextBoxEx1.Text = s;
            }
        }

        #region Public properties

        ///<summary>
        ///The associated help RichTextBoxEx
        ///</summary>
        public RichTextBoxEx RichTextBoxEx
        {
            get { return (richTextBoxEx1); }
        }

        #endregion

        #region GetRtbText

        internal string GetRtbText(SuperGridControl superGrid)
        {
            using (LocalizationManager lm = new LocalizationManager(superGrid))
            {
                string s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterSampleExpr);

                if (s == "")
                {
                    using (Stream stream = typeof(SampleExpr).Assembly.GetManifestResourceStream(
                        "DevComponents.DotNetBar.SuperGrid.SampleExpr.rtf"))
                    {
                        if (stream != null)
                        {
                            using (StreamReader reader = new StreamReader(stream))
                                s = reader.ReadToEnd();
                        }
                    }
                }

                return (s);
            }
        }

        #endregion

        #region BtnCloseClick

        private void BtnCloseClick(object sender, System.EventArgs e)
        {
            Close();
        }

        #endregion
    }
}