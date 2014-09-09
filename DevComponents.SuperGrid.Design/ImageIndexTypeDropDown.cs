using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;

namespace DevComponents.SuperGrid.Design
{
    [ToolboxItem(false)]
    public partial class ImageIndexTypeDropDown : UserControl
    {
        #region Private variables

        private int _Value;
        private bool _EscapePressed;
        private ImageList _ImageList;
        private ImageListBox _ListBox;

        private IWindowsFormsEditorService _EditorService;
        private ITypeDescriptorContext _Context;

        #endregion

        public ImageIndexTypeDropDown(int value, ImageList imageList,
            IWindowsFormsEditorService editorService, ITypeDescriptorContext context)
        {
            _EditorService = editorService;
            _Context = context;
            _Value = value;
            _ImageList = imageList;

            Initialize();
        }

        public ImageIndexTypeDropDown()
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

            if (_ImageList != null)
            {
                _ListBox = new ImageListBox();

                Controls.Add(_ListBox);

                _ListBox.MouseClick += ListBoxMouseClick;
                _ListBox.SelectedIndexChanged += ListBoxSelectedIndexChanged;
                _ListBox.ImageList = _ImageList;
                _ListBox.BorderStyle = BorderStyle.None;
                _ListBox.Dock = DockStyle.Fill;

                for (int i = 0; i < _ImageList.Images.Count; i++)
                {
                    ImageListBoxItem item =
                        new ImageListBoxItem(i.ToString(), i);

                    _ListBox.Items.Add(item);
                }

                _ListBox.Items.Add(new ImageListBoxItem("(none)", _ImageList.Images.Count));

                int index = Value;

                _ListBox.SelectedIndex =
                    (index >= 0 && index < _ImageList.Images.Count)
                    ? index : _ImageList.Images.Count;

                Size = _ListBox.PreferredSize;
            }
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

        public int Value
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
            int index = _ListBox.SelectedIndex;

            Value = (index >= _ImageList.Images.Count ? -1 : index);
        }

        #endregion
    }
}
