using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// FloatWindow
    ///</summary>
    public partial class FloatWindow : Form
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FloatWindow()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.Selectable, false);
        }

        const int WS_EX_NOACTIVATE = 0x08000000;

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;

                cp.ExStyle |= WS_EX_NOACTIVATE;

                return (cp);
            }
        }

        /// <summary>
        /// ShowWithoutActivation
        /// </summary>
        protected override bool ShowWithoutActivation
        {
            get { return true; }
        }
    }
}