using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DevComponents.SuperGrid.Design
{
    [ToolboxItem(false)]
    public partial class ValueTypeDropDown : UserControl
    {
        #region Private variables

        private object _Value;
        private bool _EscapePressed;

        private IWindowsFormsEditorService _EditorService;
        private ITypeDescriptorContext _Context;

        #endregion

        public ValueTypeDropDown(object value, 
            IWindowsFormsEditorService editorService, ITypeDescriptorContext context)
        {
            _EditorService = editorService;
            _Context = context;
            _Value = value;

            Initialize();
        }

        public ValueTypeDropDown()
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

            string[] items = GetListBoxItems();

            listBox1.Items.Clear();
            listBox1.Items.AddRange(items);

            listBox1.SelectedIndex = GetSelectedIndex();

            int height = 0;
            int width = 0;

            foreach (string s in listBox1.Items)
            {
                Size size = TextRenderer.MeasureText(s, listBox1.Font);

                width = Math.Max(width, size.Width);
                height += listBox1.ItemHeight;
            }

            height += 5;

            Size = new Size(width, height);
        }

        protected virtual string[] GetListBoxItems()
        {
            return (null);
        }

        protected virtual int GetSelectedIndex()
        {
            return (0);
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

        #region Value

        public object Value
        {
            get { return (_Value); }

            set
            {
                _Value = value;

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

        #region ListBoxMouseClick

        private void ListBoxMouseClick(object sender, MouseEventArgs e)
        {
            _EditorService.CloseDropDown();
        }

        #endregion

        #region ListBoxSelectedIndexChanged

        protected virtual void ListBoxSelectedIndexChanged(object sender, EventArgs e)
        {
        }

        #endregion
    }
}
