using System.ComponentModel;

using System.Globalization;
using System.Drawing;
using System.Runtime.InteropServices;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using System;

namespace DevComponents.DotNetBar.Controls
{
    /// <summary>
    /// Represents the bound data navigator in current DotNetBar style.
    /// </summary>
    [DefaultEvent("RefreshItems"), Description("DotNetBar Binding Navigator Control"), ClassInterface(ClassInterfaceType.AutoDispatch), DefaultProperty("BindingSource")]
    [Designer(typeof(DevComponents.DotNetBar.Design.BindingNavigatorExDesigner))]
    [ToolboxBitmap(typeof(BindingNavigatorEx), "Controls.BindingNavigatorEx.ico"), ToolboxItem(true)]
    public class BindingNavigatorEx : Bar, ISupportInitialize
    {
        #region Private Variables
        private ButtonItem _AddNewButton;
        private bool _AddNewItemUserEnabled;
        private BindingSource _BindingSource;
        private LabelItem _CountLabel;
        private string _CountLabelFormat;
        private ButtonItem _DeleteButton;
        private bool _DeleteButtonUserEnabled;
        private bool _Initializing;
        private ButtonItem _MoveFirstButton;
        private ButtonItem _MoveLastButton;
        private ButtonItem _MoveNextButton;
        private ButtonItem _MovePreviousButton;
        private TextBoxItem _PositionTextBox;
        #endregion

        #region Constructor
        /// <summary>
        /// Creates new instance of BindingNavigatorEx
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public BindingNavigatorEx()
            : this(false)
        {
        }
        /// <summary>
        /// Creates new instance of BindingNavigatorEx
        /// </summary>
        public BindingNavigatorEx(bool addStandardItems)
        {
            _CountLabelFormat = "of {0}";
            _AddNewItemUserEnabled = true;
            _DeleteButtonUserEnabled = true;
            if (addStandardItems)
            {
                this.AddDefaultItems();
            }
        }
        /// <summary>
        /// Creates new instance of BindingNavigatorEx
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public BindingNavigatorEx(IContainer container)
            : this(false)
        {
            if (container == null)
            {
                throw new ArgumentNullException("container");
            }
            container.Add(this);

        }
        /// <summary>
        /// Creates new instance of BindingNavigatorEx
        /// </summary>
        public BindingNavigatorEx(BindingSource bindingSource)
            : this(true)
        {
            this.BindingSource = bindingSource;
        }
        #endregion

        #region Internal Implementation

        private void AcceptNewPosition()
        {
            if (_PositionTextBox != null && _BindingSource != null)
            {
                int position = this._BindingSource.Position;
                try
                {
                    position = Convert.ToInt32(this._PositionTextBox.Text, CultureInfo.CurrentCulture) - 1;
                }
                catch (FormatException)
                {
                }
                catch (OverflowException)
                {
                }
                if (position != this._BindingSource.Position)
                {
                    this._BindingSource.Position = position;
                }
                this.RefreshStateInternal();
            }
        }
        /// <summary>
        /// Adds default items that make up the binding navigation control.
        /// </summary>
        public virtual void AddDefaultItems()
        {
            this.MoveFirstButton = new ButtonItem();
            this.MovePreviousButton = new ButtonItem();
            this.MoveNextButton = new ButtonItem();
            this.MoveLastButton = new ButtonItem();
            this.PositionTextBox = new TextBoxItem();
            this.CountLabel = new LabelItem();
            this.AddNewRecordButton = new ButtonItem();
            this.DeleteButton = new ButtonItem();
            char ch = (string.IsNullOrEmpty(base.Name) || char.IsLower(base.Name[0])) ? 'b' : 'B';
            this.MoveFirstButton.Name = ch + "indingNavigatorMoveFirstItem";
            this.MovePreviousButton.Name = ch + "indingNavigatorMovePreviousItem";
            this.MoveNextButton.Name = ch + "indingNavigatorMoveNextItem";
            this.MoveLastButton.Name = ch + "indingNavigatorMoveLastItem";
            this.PositionTextBox.Name = ch + "indingNavigatorPositionItem";
            this.CountLabel.Name = ch + "indingNavigatorCountItem";
            this.AddNewRecordButton.Name = ch + "indingNavigatorAddNewItem";
            this.DeleteButton.Name = ch + "indingNavigatorDeleteItem";
            this.MoveFirstButton.Text = "Move first";
            this.MovePreviousButton.Text = "Move previous";
            this.MoveNextButton.Text = "Move next";
            this.MoveLastButton.Text = "Move last";
            this.AddNewRecordButton.Text = "Add new";
            this.DeleteButton.Text = "Delete";
            this.PositionTextBox.AccessibleName = "Position";

            _PositionTextBox.BeginGroup = true;
            _MoveNextButton.BeginGroup = true;
            _MoveFirstButton.Image = BarFunctions.LoadBitmap("SystemImages.FirstRecord.png");
            _MovePreviousButton.Image = BarFunctions.LoadBitmap("SystemImages.PreviousRecord.png");
            _MoveNextButton.Image = BarFunctions.LoadBitmap("SystemImages.NextRecord.png"); ;
            _MoveLastButton.Image = BarFunctions.LoadBitmap("SystemImages.LastRecord.png"); ;
            _AddNewButton.Image = BarFunctions.LoadBitmap("SystemImages.NewRecord.png"); ;
            _DeleteButton.Image = BarFunctions.LoadBitmap("SystemImages.Delete.png"); ;

            _PositionTextBox.TextBoxWidth = 54;
            this.Items.AddRange(new BaseItem[] { this.MoveFirstButton, this.MovePreviousButton, this.PositionTextBox, this.CountLabel, this.MoveNextButton, this.MoveLastButton, this.AddNewRecordButton, this.DeleteButton });
        }


        private void CancelNewPosition()
        {
            this.RefreshStateInternal();
        }

        private bool _IsDisposing = false;
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _IsDisposing = true;
                try
                {
                    this.BindingSource = null;
                }
                finally
                {
                    _IsDisposing = false;
                }
            }
            base.Dispose(disposing);
        }

        private void OnAddNew(object sender, EventArgs e)
        {
            if (this.Validate() && _BindingSource != null)
            {
                _BindingSource.EndEdit();
                _BindingSource.AddNew();
                RefreshStateInternal();
            }
        }

        private void OnAddNewItemEnabledChanged(object sender, EventArgs e)
        {
            if (AddNewRecordButton != null)
            {
                _AddNewItemUserEnabled = _AddNewButton.Enabled;
            }
        }

        private void OnBindingSourceListChanged(object sender, ListChangedEventArgs e)
        {
            RefreshStateInternal();
        }

        private void OnBindingSourceStateChanged(object sender, EventArgs e)
        {
            RefreshStateInternal();
        }

        private void OnDelete(object sender, EventArgs e)
        {
            if (this.Validate() && _BindingSource != null)
            {
                _BindingSource.RemoveCurrent();
                RefreshStateInternal();
            }
        }

        private void OnDeleteItemEnabledChanged(object sender, EventArgs e)
        {
            if (DeleteButton != null)
            {
                _DeleteButtonUserEnabled = _DeleteButton.Enabled;
            }
        }

        private void OnMoveFirst(object sender, EventArgs e)
        {
            if (Validate() && _BindingSource != null)
            {
                _BindingSource.MoveFirst();
                RefreshStateInternal();
            }
        }

        private void OnMoveLast(object sender, EventArgs e)
        {
            if (Validate() && _BindingSource != null)
            {
                _BindingSource.MoveLast();
                RefreshStateInternal();
            }
        }

        private void OnMoveNext(object sender, EventArgs e)
        {
            if (Validate() && this._BindingSource != null)
            {
                _BindingSource.MoveNext();
                RefreshStateInternal();
            }
        }

        private void OnMovePrevious(object sender, EventArgs e)
        {
            if (Validate() && _BindingSource != null)
            {
                _BindingSource.MovePrevious();
                RefreshStateInternal();
            }
        }

        private void OnPositionKey(object sender, KeyEventArgs e)
        {
            Keys keyCode = e.KeyCode;
            if (keyCode != Keys.Return)
            {
                if (keyCode != Keys.Escape)
                {
                    return;
                }
            }
            else
            {
                AcceptNewPosition();
                return;
            }
            CancelNewPosition();
        }

        private void OnPositionLostFocus(object sender, EventArgs e)
        {
            AcceptNewPosition();
        }

        protected virtual void OnRefreshState()
        {
            RefreshState();
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected virtual void RefreshState()
        {
            int count;
            int currentPosition;
            bool allowNew;
            bool allowRemove;
            if (_BindingSource == null)
            {
                count = 0;
                currentPosition = 0;
                allowNew = false;
                allowRemove = false;
            }
            else if (_BindingSource.Position == -1)
            {
                count = -1;
                currentPosition = -1;
                allowNew = true;
                allowRemove = false;
            }
            else
            {
                count = _BindingSource.Count;
                currentPosition = _BindingSource.Position + 1;
                allowNew = _BindingSource.AllowNew;
                allowRemove = _BindingSource.AllowRemove;
            }
            if (!base.DesignMode)
            {
                if (MoveFirstButton != null)
                {
                    _MoveFirstButton.Enabled = currentPosition > 1;
                }
                if (MovePreviousButton != null)
                {
                    _MovePreviousButton.Enabled = currentPosition > 1;
                }
                if (MoveNextButton != null)
                {
                    _MoveNextButton.Enabled = currentPosition < count;
                }
                if (MoveLastButton != null)
                {
                    _MoveLastButton.Enabled = currentPosition < count;
                }
                if (AddNewRecordButton != null)
                {
                    EventHandler handler = this.OnAddNewItemEnabledChanged;
                    _AddNewButton.EnabledChanged -= handler;
                    _AddNewButton.Enabled = _AddNewItemUserEnabled && allowNew;
                    _AddNewButton.EnabledChanged += handler;
                }
                if (DeleteButton != null)
                {
                    EventHandler handler2 = OnDeleteItemEnabledChanged;
                    _DeleteButton.EnabledChanged -= handler2;
                    _DeleteButton.Enabled = (this._DeleteButtonUserEnabled && allowRemove) && (count > 0);
                    _DeleteButton.EnabledChanged += handler2;
                }
                if (PositionTextBox != null)
                {
                    _PositionTextBox.Enabled = (currentPosition > 0) && (count > 0);
                }
                if (CountLabel != null)
                {
                    _CountLabel.Enabled = count > 0;
                }
            }
            if (_PositionTextBox != null)
            {
                _PositionTextBox.Text = currentPosition.ToString(CultureInfo.CurrentCulture);
            }
            if (_CountLabel != null)
            {
                _CountLabel.Text = base.DesignMode ? CountLabelFormat : string.Format(CultureInfo.CurrentCulture, CountLabelFormat, new object[] { count });
            }
        }

        private void RefreshStateInternal()
        {
            if (!_Initializing && !_IsDisposing)
                OnRefreshState();
        }

        public bool Validate()
        {
            bool flag = false;
            System.Reflection.MethodInfo mi = ((Control)this).GetType().GetMethod("ValidateActiveControl", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
            if (mi != null)
            {
                return (bool)mi.Invoke(this, new object[] { flag });
            }
            return true;
        }

        private void WireUpBindingSource(ref BindingSource oldBindingSource, BindingSource newBindingSource)
        {
            if (oldBindingSource != newBindingSource)
            {
                if (oldBindingSource != null)
                {
                    oldBindingSource.PositionChanged -= OnBindingSourceStateChanged;
                    oldBindingSource.CurrentChanged -= OnBindingSourceStateChanged;
                    oldBindingSource.CurrentItemChanged -= OnBindingSourceStateChanged;
                    oldBindingSource.DataSourceChanged -= OnBindingSourceStateChanged;
                    oldBindingSource.DataMemberChanged -= OnBindingSourceStateChanged;
                    oldBindingSource.ListChanged -= OnBindingSourceListChanged;
                }
                if (newBindingSource != null)
                {
                    newBindingSource.PositionChanged += OnBindingSourceStateChanged;
                    newBindingSource.CurrentChanged += OnBindingSourceStateChanged;
                    newBindingSource.CurrentItemChanged += OnBindingSourceStateChanged;
                    newBindingSource.DataSourceChanged += OnBindingSourceStateChanged;
                    newBindingSource.DataMemberChanged += OnBindingSourceStateChanged;
                    newBindingSource.ListChanged += OnBindingSourceListChanged;
                }
                oldBindingSource = newBindingSource;
                this.RefreshStateInternal();
            }
        }

        private void WireUpButton(ref ButtonItem oldButton, ButtonItem newButton, EventHandler clickHandler)
        {
            if (oldButton != newButton)
            {
                if (oldButton != null)
                {
                    oldButton.Click -= clickHandler;
                }
                if (newButton != null)
                {
                    newButton.Click += clickHandler;
                }
                oldButton = newButton;
                this.RefreshStateInternal();
            }
        }

        private void WireUpLabel(ref LabelItem oldLabel, LabelItem newLabel)
        {
            if (oldLabel != newLabel)
            {
                oldLabel = newLabel;
                this.RefreshStateInternal();
            }
        }

        private void WireUpTextBox(ref TextBoxItem oldTextBox, TextBoxItem newTextBox, KeyEventHandler keyUpHandler, EventHandler lostFocusHandler)
        {
            if (oldTextBox != newTextBox)
            {
                if (oldTextBox != null)
                {
                    oldTextBox.KeyUp -= keyUpHandler;
                    oldTextBox.LostFocus -= lostFocusHandler;
                }
                if (newTextBox != null)
                {
                    newTextBox.KeyUp += keyUpHandler;
                    newTextBox.LostFocus += lostFocusHandler;
                }
                oldTextBox = newTextBox;
                this.RefreshStateInternal();
            }
        }

        /// <summary>
        /// Gets or sets the reference to Add New record button.
        /// </summary>
        [DefaultValue(null), Category("Items"), TypeConverter(typeof(ReferenceConverter)), Description("BindingNavigatorAddNewItemPropDescr")]
        public ButtonItem AddNewRecordButton
        {
            get
            {
                if (_AddNewButton != null && _AddNewButton.IsDisposed)
                {
                    _AddNewButton = null;
                }
                return _AddNewButton;
            }
            set
            {
                if ((_AddNewButton != value) && (value != null))
                {
                    value.EnabledChanged += OnAddNewItemEnabledChanged;
                    _AddNewItemUserEnabled = value.Enabled;
                }
                WireUpButton(ref _AddNewButton, value, OnAddNew);
            }
        }

        /// <summary>
        /// Gets or sets the binding source for the navigator.
        /// </summary>
        [Category("Data"), Description("Gets or sets the binding source for the navigator."), DefaultValue((string)null), TypeConverter(typeof(ReferenceConverter))]
        public BindingSource BindingSource
        {
            get
            {
                return _BindingSource;
            }
            set
            {
                WireUpBindingSource(ref _BindingSource, value);
            }
        }

        /// <summary>
        /// Gets or sets the label which represents the items count.
        /// </summary>
        [TypeConverter(typeof(ReferenceConverter)), Category("Items"), Description("Indicates label which represents the items count.")]
        public LabelItem CountLabel
        {
            get
            {
                if (_CountLabel != null && _CountLabel.IsDisposed)
                {
                    _CountLabel = null;
                }
                return _CountLabel;
            }
            set
            {
                this.WireUpLabel(ref _CountLabel, value);
            }
        }

        /// <summary>
        /// Indicates the format string for the label which displays the number of items bound.
        /// </summary>
        [Description("Indicates the format string for the label which displays the number of items bound."), Category("Appearance")]
        public string CountLabelFormat
        {
            get
            {
                return _CountLabelFormat;
            }
            set
            {
                if (_CountLabelFormat != value)
                {
                    _CountLabelFormat = value;
                    RefreshStateInternal();
                }
            }
        }

        /// <summary>
        /// Gets or sets the item which deletes current record.
        /// </summary>
        [DefaultValue(null), Description("Indicates item which deletes current record."), TypeConverter(typeof(ReferenceConverter)), Category("Items")]
        public ButtonItem DeleteButton
        {
            get
            {
                if ((this._DeleteButton != null) && this._DeleteButton.IsDisposed)
                {
                    _DeleteButton = null;
                }
                return _DeleteButton;
            }
            set
            {
                if ((this._DeleteButton != value) && (value != null))
                {
                    value.EnabledChanged += new EventHandler(this.OnDeleteItemEnabledChanged);
                    _DeleteButtonUserEnabled = value.Enabled;
                }
                this.WireUpButton(ref _DeleteButton, value, new EventHandler(this.OnDelete));
            }
        }

        /// <summary>
        /// Gets or sets the item which moves to first record.
        /// </summary>
        [DefaultValue(null), TypeConverter(typeof(ReferenceConverter)), Category("Items"), Description("Indicates item which moves to first record.")]
        public ButtonItem MoveFirstButton
        {
            get
            {
                if (_MoveFirstButton != null && _MoveFirstButton.IsDisposed)
                {
                    _MoveFirstButton = null;
                }
                return _MoveFirstButton;
            }
            set
            {
                WireUpButton(ref _MoveFirstButton, value, this.OnMoveFirst);
            }
        }
        /// <summary>
        /// Gets or sets the item which moves to last record.
        /// </summary>
        [DefaultValue(null), Category("Items"), Description("Indicates item which moves to last record."), TypeConverter(typeof(ReferenceConverter))]
        public ButtonItem MoveLastButton
        {
            get
            {
                if ((_MoveLastButton != null) && _MoveLastButton.IsDisposed)
                {
                    _MoveLastButton = null;
                }
                return _MoveLastButton;
            }
            set
            {
                this.WireUpButton(ref _MoveLastButton, value, OnMoveLast);
            }
        }
        /// <summary>
        /// Gets or sets the item which moves to next record.
        /// </summary>
        [DefaultValue(null), TypeConverter(typeof(ReferenceConverter)), Category("Items"), Description("Indicates item which moves to next record")]
        public ButtonItem MoveNextButton
        {
            get
            {
                if (_MoveNextButton != null && _MoveNextButton.IsDisposed)
                {
                    _MoveNextButton = null;
                }
                return _MoveNextButton;
            }
            set
            {
                this.WireUpButton(ref _MoveNextButton, value, new EventHandler(this.OnMoveNext));
            }
        }
        /// <summary>
        /// Gets or sets the item which moves to previous record.
        /// </summary>
        [DefaultValue(null), Description("Indicates item which moves to previous record"), TypeConverter(typeof(ReferenceConverter)), Category("Items")]
        public ButtonItem MovePreviousButton
        {
            get
            {
                if (_MovePreviousButton != null && this._MovePreviousButton.IsDisposed)
                {
                    _MovePreviousButton = null;
                }
                return _MovePreviousButton;
            }
            set
            {
                this.WireUpButton(ref _MovePreviousButton, value, OnMovePrevious);
            }
        }
        /// <summary>
        /// Gets or sets the text-box which shows current position.
        /// </summary>
        [DefaultValue(null), Category("Items"), Description("Indicates text-box which shows current position."), TypeConverter(typeof(ReferenceConverter))]
        public TextBoxItem PositionTextBox
        {
            get
            {
                if (_PositionTextBox != null && _PositionTextBox.IsDisposed)
                {
                    _PositionTextBox = null;
                }
                return _PositionTextBox;
            }
            set
            {
                this.WireUpTextBox(ref _PositionTextBox, value, OnPositionKey, OnPositionLostFocus);
            }
        }
        #endregion

        #region ISupportInitialize Members

        void ISupportInitialize.BeginInit()
        {
            _Initializing = true;
        }

        void ISupportInitialize.EndInit()
        {

            _Initializing = false;
            this.RefreshStateInternal();
        }


        #endregion

    }
}