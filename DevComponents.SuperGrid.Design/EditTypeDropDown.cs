using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.SuperGrid;

namespace DevComponents.SuperGrid.Design
{
    [ToolboxItem(false)]
    public partial class EditTypeDropDown : UserControl
    {
        #region Static data

        static readonly Type[] Types = new Type[]
        {
            typeof(GridButtonXEditControl), typeof(GridCheckBoxXEditControl),
            typeof(GridComboBoxExEditControl),
            typeof(GridComboTreeEditControl), typeof(GridDateTimeInputEditControl),
            typeof(GridDateTimePickerEditControl), typeof(GridDoubleInputEditControl),
            typeof(GridIntegerInputEditControl), typeof(GridIpAddressInputEditControl),
            typeof(GridLabelXEditControl), typeof(GridMaskedTextBoxEditControl),
            typeof(GridMicroChartEditControl), typeof(GridNumericUpDownEditControl),
            typeof(GridImageEditControl), typeof(GridProgressBarXEditControl),
            typeof(GridSliderEditControl), typeof(GridSwitchButtonEditControl),
            typeof(GridTextBoxDropDownEditControl), typeof(GridTextBoxXEditControl),
        };

        #endregion

        #region Private variables

        private Type _EditType;

        private bool _EscapePressed;

        private IWindowsFormsEditorService _EditorService;
        private ITypeDescriptorContext _Context;

        #endregion

        public EditTypeDropDown(Type value, 
            IWindowsFormsEditorService editorService, ITypeDescriptorContext context)
        {
            Initialize();

            _EditorService = editorService;
            _Context = context;

            EditType = value;
        }

        public EditTypeDropDown()
        {
            Initialize();
        }

        #region Initialize

        private void Initialize()
        {
            InitializeComponent();

            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);

            listBox1.Items.Add("(None)");

            foreach (Type type in Types)
                listBox1.Items.Add(type.Name);
        }

        #endregion

        #region Public properties

        #region EditorService

        public IWindowsFormsEditorService EditorService
        {
            get { return (_EditorService); }
            set { _EditorService = value; }
        }

        #endregion

        #region EscapePressed

        public bool EscapePressed
        {
            get { return (_EscapePressed); }
            set { _EscapePressed = value; }
        }

        #endregion

        #region EditType

        public Type EditType
        {
            get { return (_EditType); }

            set
            {
                _EditType = value;

                _Context.OnComponentChanging();
                _Context.PropertyDescriptor.SetValue(_Context.Instance, value);
                _Context.OnComponentChanged();
            }
        }

        #endregion

        #endregion

        #region DropDownPreviewKeyDown

        private void DropDownPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
                _EscapePressed = true;
        }

        #endregion

        #region ListBox1MouseClick

        private void ListBox1MouseClick(object sender, MouseEventArgs e)
        {
            _EditorService.CloseDropDown();
        }

        #endregion

        #region ListBox1SelectedIndexChanged

        private void ListBox1SelectedIndexChanged(object sender, EventArgs e)
        {
            EditType = (listBox1.SelectedIndex > 0)
                ? Types[listBox1.SelectedIndex - 1] : null;
        }

        #endregion
    }
}
