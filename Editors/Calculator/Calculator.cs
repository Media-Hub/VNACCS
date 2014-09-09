using System;
using System.ComponentModel;
using System.Drawing;
using System.Media;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Globalization;

namespace DevComponents.Editors
{
    /// <summary>
    /// Standalone Calculator control.
    /// </summary>
    [ToolboxBitmap(typeof(DotNetBarManager), "Calculator.ico"), DefaultEvent("ValueChanged"), DefaultProperty("Value"), ToolboxItem(true), Designer(typeof(DevComponents.DotNetBar.Design.CalculatorDesigner))]
    public partial class Calculator : Control
    {
        #region Events

        #region ButtonClick

        /// <summary>
        /// Occurs when a calc button has been clicked
        /// </summary>
        [Description("Occurs when a calc button has been clicked.")]
        public event EventHandler<ButtonClickEventArgs> ButtonClick;

        #endregion

        #region ValueChanged

        /// <summary>
        /// Occurs when the calculator value has changed
        /// </summary>
        [Description("Occurs when the calculator value has changed.")]
        public event EventHandler<ValueChangedEventArgs> ValueChanged;

        #endregion

        public event EventHandler CalculatorDisplayChanged;
        /// <summary>
        /// Raises CalculatorDisplayChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnCalculatorDisplayChanged(EventArgs e)
        {
            EventHandler handler = CalculatorDisplayChanged;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Private variables

        private string _StringValue = null;

        private double _Value;
        private double _LValue;
        private double _MemValue;

        private Operators _Operator;
        private Operators _LastOp;

        private bool _IsIntValue;
        private bool _ShowMemKeys;

        private DecimalKeyVisibility _DecimalKeyVisibility;

        private Timer _KeyTimer;
        private string _NumberDecimalSeparator;
        #endregion

        public Calculator()
        {
            InitializeComponent();

            _ShowMemKeys = true;
            _DecimalKeyVisibility = DecimalKeyVisibility.Auto;
            this.SetStyle(DisplayHelp.DoubleBufferFlag | ControlStyles.AllPaintingInWmPaint | ControlStyles.Opaque, true);
            labelValue.BackColor = Color.White;
            pnlCalc.BackColor = Color.White;
            pnlPad.BackColor = Color.White;
            _NumberDecimalSeparator = CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator;
            BtnDecimal.Text = _NumberDecimalSeparator;
        }

        #region Public properties

        #region DisplayValue
        [Browsable(false)]
        public double DisplayValue
        {
            get
            {
                if (string.IsNullOrEmpty(_StringValue) == true)
                    return (_Value);

                return (double.Parse(_StringValue));
            }
        }

        #endregion

        #region UpdateDisplay
        private void UpdateDisplay()
        {
            labelValue.Text = DisplaySValue;
        }
        #endregion

        #region DisplaySValue
        [Browsable(false)]
        public string DisplaySValue
        {
            get
            {
                if (string.IsNullOrEmpty(_StringValue) == false)
                    return (_StringValue);

                return (_Value.ToString());
            }
        }

        #endregion

        #region IsIntValue
        /// <summary>
        /// Indicates whether calculator displays only Integer values.
        /// </summary>
        [DefaultValue(false), Category("Behavior"), Description("Indicates whether calculator displays only Integer values.")]
        public bool IsIntValue
        {
            get { return (_IsIntValue); }

            set
            {
                if (_IsIntValue != value)
                {
                    _IsIntValue = value;

                    UpdateLayout();
                }
            }
        }

        #endregion

        #region ShowMemKeys
        /// <summary>
        /// Gets or sets whether memory keys are visible. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether memory keys are visible.")]
        public bool ShowMemKeys
        {
            get { return (_ShowMemKeys); }

            set
            {
                if (_ShowMemKeys != value)
                {
                    _ShowMemKeys = value;

                    if (_ShowMemKeys == true)
                    {
                        pnlCalc.Height += (pnlMem.Height - 1);
                        pnlPad.Location = new Point(1, pnlMem.Bottom);
                    }
                    else
                    {
                        pnlCalc.Height -= (pnlMem.Height - 1);
                        if(_DisplayVisible)
                            pnlPad.Location = new Point(1, labelValue.Bottom);
                        else
                            pnlPad.Location = new Point(1, labelValue.Top);
                    }

                    InvalidateAutoSize();
                    //Size = pnlCalc.Size;
                }
            }
        }

        #endregion

        #region DecimalKeyVisibility
        /// <summary>
        /// Gets or sets visibility of the decimal calculator key.
        /// </summary>
        [DefaultValue(DecimalKeyVisibility.Auto), Category("Appearance"), Description("Indicates visibility of the decimal calculator key.")]
        public DecimalKeyVisibility DecimalKeyVisibility
        {
            get { return (_DecimalKeyVisibility); }

            set
            {
                if (_DecimalKeyVisibility != value)
                {
                    _DecimalKeyVisibility = value;

                    UpdateLayout();
                }
            }
        }

        #endregion

        #region SValue
        [DefaultValue(null), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string StringValue
        {
            get { return (_StringValue); }

            set
            {
                _StringValue = value;
                OnValueChanged();
            }
        }

        #endregion

        #region Value
        [DefaultValue(0d), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public double Value
        {
            get { return (_Value); }

            set
            {
                _Value = value;

                OnValueChanged();
            }
        }

        #endregion

        #endregion

        #region Keyboard Support

        internal void ProcessKeyPress(KeyPressEventArgs e)
        {
            ButtonX btn = null;
            switch (e.KeyChar)
            {
                case '0': btn = OnDigitSelect(BtnDigit0); break;
                case '1': btn = OnDigitSelect(BtnDigit1); break;
                case '2': btn = OnDigitSelect(BtnDigit2); break;
                case '3': btn = OnDigitSelect(BtnDigit3); break;
                case '4': btn = OnDigitSelect(BtnDigit4); break;
                case '5': btn = OnDigitSelect(BtnDigit5); break;
                case '6': btn = OnDigitSelect(BtnDigit6); break;
                case '7': btn = OnDigitSelect(BtnDigit7); break;
                case '8': btn = OnDigitSelect(BtnDigit8); break;
                case '9': btn = OnDigitSelect(BtnDigit9); break;
                case '+': btn = OnOperatorSelect(BtnAdd, Operators.Add); break;
                case '-': btn = OnOperatorSelect(BtnSubtract, Operators.Subtract); break;
                case '*': btn = OnOperatorSelect(BtnMultiply, Operators.Multiply); break;
                case '/': btn = OnOperatorSelect(BtnDivide, Operators.Divide); break;

                case '%': btn = OnPercentSelect(); break;
                case 'r': btn = OnReciprocalSelect(); break;
                case '@': btn = OnSqrtSelect(); break;
                case 'n': btn = OnNegateSelect(); break;

                case 'c': btn = OnClearSelect(); break;
                case 'e': btn = OnClearEntrySelect(); break;
                case '=': btn = OnEqualsSelect(); break;
                case '\b': btn = OnBackSelect(); break;

                case '\r':
                    btn = ((ModifierKeys & Keys.Control) == Keys.Control)
                        ? OnMemStoreSelect() : OnEqualsSelect();
                    break;

                case (char)18: btn = OnMemRestoreSelect(); break;
                case (char)16: btn = OnMemAddSelect(); break;
                case (char)17: btn = OnMemSubtractSelect(); break;
                case (char)12: btn = OnMemClearSelect(); break;
            }
            if (btn == null && e.KeyChar.ToString() == _NumberDecimalSeparator)
                btn = OnDecimalSelect();

            if (btn != null)
            {
                FlashKey(btn);
                e.Handled = true;
            }
        }
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            if (e.Handled == false)
            {
                ProcessKeyPress(e);
            }
        }

        #endregion

        #region Button Support

        #region BtnDigitClick

        private void BtnDigitClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnDigitSelect((ButtonX)sender);
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnDigitSelect(ButtonX btn)
        {
            AddDigit(btn);

            OnValueChanged();

            return (btn);
        }

        #region AddDigit

        private void AddDigit(object sender)
        {
            ButtonX btn = sender as ButtonX;

            if (btn != null)
            {
                char c = btn.Name[8];

                if (_StringValue == null || _LastOp == Operators.Set)
                {
                    _StringValue = "";

                    if (_LastOp == Operators.Calc || _LastOp == Operators.Set)
                        SetOperator(Operators.None);
                }

                _StringValue += c;
                UpdateDisplay();
                OnCalculatorDisplayChanged(EventArgs.Empty);
            }
        }

        #endregion

        #endregion

        #region BtnDecimalClick

        private void BtnDecimalClick(object sender, EventArgs e)
        {
            if (OnButtonClick(BtnDecimal) == false)
                OnDecimalSelect();

            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnDecimalSelect()
        {
            if (_IsIntValue == false)
            {
                if (_StringValue == null || _LastOp == Operators.Set)
                {
                    _StringValue = "0" + _NumberDecimalSeparator;

                    if (_LastOp == Operators.Calc || _LastOp == Operators.Set)
                        SetOperator(Operators.None);
                }
                else if (_StringValue.Contains(_NumberDecimalSeparator) == false)
                {
                    _StringValue += _NumberDecimalSeparator;
                }
            }

            OnValueChanged();

            return (BtnDecimal);
        }

        #endregion

        #region BtnBackClick

        private void BtnBackClick(object sender, EventArgs e)
        {
            if (OnButtonClick(BtnBack) == false)
                OnBackSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnBackSelect()
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
            {
                _StringValue = _StringValue.Substring(0, _StringValue.Length - 1);

                if (_StringValue.Length == 0 ||
                    (_StringValue.Length == 1 && _StringValue[0] == '-'))
                {
                    _StringValue = "0";
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }

            OnValueChanged();

            return (BtnBack);
        }

        #endregion

        #region BtnAddClick

        private void BtnAddClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnOperatorSelect(BtnAdd, Operators.Add);
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        #endregion

        #region BtnSubtractClick

        private void BtnSubtractClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnOperatorSelect(BtnSubtract, Operators.Subtract);
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        #endregion

        #region BtnMultiplyClick

        private void BtnMultiplyClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnOperatorSelect(BtnMultiply, Operators.Multiply);

            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        #endregion

        #region BtnDivideClick

        private void BtnDivideClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnOperatorSelect(BtnDivide, Operators.Divide);

            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        #endregion

        #region BtnNegateClick

        private void BtnNegateClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnNegateSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnNegateSelect()
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
            {
                _StringValue = '-' + _StringValue;
                _StringValue = _StringValue.Replace("--", "");
            }
            else
            {
                _Value = -_Value;
            }
            OnValueChanged();

            return (BtnNegate);
        }

        #endregion

        #region BtnSqrtClick

        private void BtnSqrtClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnSqrtSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnSqrtSelect()
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
            {
                double d = double.Parse(_StringValue);

                if (d > 0)
                {
                    d = Math.Sqrt(d);

                    if (IsIntValue == true)
                        d = Math.Round(d);

                    _StringValue = d.ToString();
                }
                else
                    SystemSounds.Beep.Play();
            }
            else
            {
                if (_Value > 0)
                    _Value = Math.Sqrt(_Value);
                else
                    SystemSounds.Beep.Play();
            }

            _LastOp = Operators.Set;

            OnValueChanged();

            return (BtnSqrt);
        }

        #endregion

        #region BtnReciprocalClick

        private void BtnReciprocalClick(object sender, EventArgs e)
        {
            if (OnButtonClick(BtnReciprocal) == false)
                OnReciprocalSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnReciprocalSelect()
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
            {
                double d = double.Parse(_StringValue);

                if (d != 0)
                {
                    d = 1 / d;

                    if (IsIntValue == true)
                        d = Math.Round(d);

                    _StringValue = d.ToString();
                }
                else
                {
                    SystemSounds.Beep.Play();
                }
            }
            else
            {
                if (_Value != 0)
                    _Value = 1 / _Value;
                else
                    SystemSounds.Beep.Play();
            }

            _LastOp = Operators.Set;

            OnValueChanged();

            return (BtnReciprocal);
        }

        #endregion

        #region BtnPercentClick

        private void BtnPercentClick(object sender, EventArgs e)
        {
            if (OnButtonClick(BtnPercent) == false)
                OnPercentSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnPercentSelect()
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
            {
                double d = double.Parse(_StringValue);

                d = (_Value * d) / 100;

                if (IsIntValue == true)
                    d = Math.Round(d);

                _StringValue = d.ToString();
            }
            else
            {
                _Value = 0;
            }

            OnValueChanged();

            return (BtnPercent);
        }

        #endregion

        #region BtnEqualsClick

        private void BtnEqualsClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnEqualsSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnEqualsSelect()
        {
            UpdateTotal();

            _LastOp = Operators.Calc;
            lbxOperator.Text = "";

            OnValueChanged();

            return (BtnEquals);
        }

        #endregion

        #region BtnClearEntryClick

        private void BtnClearEntryClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnClearEntrySelect();

            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnClearEntrySelect()
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
                _StringValue = "0";
            else
                _Value = 0;
            OnValueChanged();

            return (BtnClearEntry);
        }

        #endregion

        #region BtnClearClick

        private void BtnClearClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnClearSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnClearSelect()
        {
            _Value = 0;
            _LValue = 0;
            _StringValue = null;

            SetOperator(Operators.None);
            OnValueChanged();

            return (BtnClear);
        }

        #endregion

        #region BtnMemStoreClick

        private void BtnMemStoreClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnMemStoreSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnMemStoreSelect()
        {
            StoreToMem(DisplayValue);

            return (BtnMemStore);
        }

        #endregion

        #region BtnMemRestoreClick

        private void BtnMemRestoreClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnMemRestoreSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnMemRestoreSelect()
        {
            _StringValue = _MemValue.ToString();
            OnValueChanged();

            return (BtnMemRestore);
        }

        #endregion

        #region BtnMemClearClick

        private void BtnMemClearClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnMemClearSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnMemClearSelect()
        {
            StoreToMem(0);

            return (BtnMemClear);
        }

        #endregion

        #region BtnMemAddClick

        private void BtnMemAddClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnMemAddSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnMemAddSelect()
        {
            StoreToMem(_MemValue + DisplayValue);

            return (BtnMemAdd);
        }

        #endregion

        #region BtnMemSubtractClick

        private void BtnMemSubtractClick(object sender, EventArgs e)
        {
            if (OnButtonClick(sender) == false)
                OnMemSubtractSelect();
            if (_FocusButtonsOnMouseDown)
                Focus();
        }

        private ButtonX OnMemSubtractSelect()
        {
            StoreToMem(_MemValue - DisplayValue);

            return (BtnMemSubtract);
        }

        #endregion

        #region StoreToMem

        private void StoreToMem(double value)
        {
            _MemValue = value;

            lbxMemory.Text = (value == 0) ? "" : "M";
        }

        #endregion

        #endregion

        #region SetOperator

        private void SetOperator(Operators op)
        {
            _Operator = op;
            _LastOp = op;

            lbxOperator.Text = ((char)op).ToString();
        }

        #endregion

        #region OnOperatorSelect

        private ButtonX OnOperatorSelect(ButtonX btn, Operators op)
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
                UpdateTotal();

            SetOperator(op);
            OnValueChanged();

            return (btn);
        }

        #endregion

        #region UpdateTotal

        private void UpdateTotal()
        {
            if (string.IsNullOrEmpty(_StringValue) == false)
                _LValue = double.Parse(_StringValue);

            switch (_Operator)
            {
                case Operators.None:
                    _Value = _LValue;
                    break;

                case Operators.Add:
                    _Value += _LValue;
                    break;

                case Operators.Subtract:
                    _Value -= _LValue;
                    break;

                case Operators.Multiply:
                    _Value *= _LValue;
                    break;

                case Operators.Divide:
                    if (_LValue > 0)
                        _Value /= _LValue;
                    else
                        SystemSounds.Beep.Play();
                    break;
            }

            _StringValue = null;
            OnValueChanged();
        }

        #endregion

        #region UpdateLayout

        private void UpdateLayout()
        {
            bool showDecimal;

            switch (_DecimalKeyVisibility)
            {
                case DecimalKeyVisibility.Always:
                    showDecimal = true;
                    break;

                case DecimalKeyVisibility.Auto:
                    showDecimal = (IsIntValue == false);
                    break;

                default:
                    showDecimal = false;
                    break;
            }

            if (showDecimal == true)
            {
                BtnDecimal.Visible = true;
                BtnDigit0.Width = BtnDigit2.Bounds.Right - BtnDigit0.Bounds.X;
            }
            else
            {
                BtnDecimal.Visible = false;
                BtnDigit0.Width = BtnDecimal.Bounds.Right - BtnDigit0.Bounds.X;
            }
            InvalidateAutoSize();
        }

        #endregion

        #region OnButtonClick

        private bool OnButtonClick(object button)
        {
            if (ButtonClick != null)
            {
                ButtonClickEventArgs e =
                    new ButtonClickEventArgs(button);

                ButtonClick(this, e);

                return (e.Cancel);
            }

            return (false);
        }

        #endregion

        #region OnValueChanged

        public void OnValueChanged()
        {
            if (ValueChanged != null)
            {
                ValueChanged(this,
                    new ValueChangedEventArgs(DisplaySValue, DisplayValue));
            }

            UpdateDisplay();
            OnCalculatorDisplayChanged(EventArgs.Empty);
        }

        #endregion

        #region OnVisibleChanged

        protected override void OnVisibleChanged(EventArgs e)
        {
            if (Visible == true)
            {
                _StringValue = null;
                _LValue = 0;

                StoreToMem(0);
                SetOperator(Operators.Set);
                OnValueChanged();
            }

            base.OnVisibleChanged(e);
        }

        #endregion

        #region FlashKey

        public void FlashKey(ButtonX keyButton)
        {
            if (_KeyTimer == null)
            {
                _KeyTimer = new Timer();

                _KeyTimer.Interval = 125;
                _KeyTimer.Tick += KeyTimerTick;
            }

            ButtonX btn = _KeyTimer.Tag as ButtonX;

            if (btn != null)
            {
                _KeyTimer.Stop();

                DoKeyTimerUp(btn);
            }

            DoKeyTimerDown(keyButton);

            _KeyTimer.Tag = keyButton;

            _KeyTimer.Start();
        }

        #region DoKeyTimerDown

        private void DoKeyTimerDown(ButtonX btn)
        {
            MouseEventArgs args = new MouseEventArgs(
                MouseButtons.Left, 1, btn.Location.X, btn.Location.Y, 0);

            btn.InternalItem.InternalMouseDown(args);
        }

        #endregion

        #region DoKeyTimerUp

        private void DoKeyTimerUp(ButtonX btn)
        {
            MouseEventArgs args = new MouseEventArgs(
                MouseButtons.Left, 1, btn.Location.X, btn.Location.Y, 0);

            btn.InternalItem.InternalMouseUp(args);
        }

        #endregion

        #region KeyTimerTick

        void KeyTimerTick(object sender, EventArgs e)
        {
            ButtonX btn = _KeyTimer.Tag as ButtonX;

            if (btn != null)
                DoKeyTimerUp(btn);

            _KeyTimer.Stop();
        }

        #endregion

        #endregion

        #region Auto-Sizing
        protected override void OnPaint(PaintEventArgs e)
        {
            using(SolidBrush brush=new SolidBrush(this.BackColor))
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            base.OnPaint(e);
        }
        private Size _PreferredSize = Size.Empty;
        /// <summary>
        /// Invalidates control auto-size and resizes the control if AutoSize is set to true.
        /// </summary>
        public void InvalidateAutoSize()
        {
            _PreferredSize = Size.Empty;
            AdjustSize();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents. You can set MaximumSize.Width property to set the maximum width used by the control.
        /// </summary>
        [Browsable(true), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                if (this.AutoSize != value)
                {
                    base.AutoSize = value;
                    AdjustSize();
                }
            }
        }

        private void AdjustSize()
        {
            if (this.AutoSize)
            {
                this.Size = base.PreferredSize;
            }
        }

        private bool _FocusButtonsOnMouseDown = true;
        internal bool FocusButtonsOnMouseDown
        {
            get { return _FocusButtonsOnMouseDown; }
            set
            {
                if (value != _FocusButtonsOnMouseDown)
                {
                    bool oldValue = _FocusButtonsOnMouseDown;
                    _FocusButtonsOnMouseDown = value;
                    OnFocusButtonsOnMouseDownChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when FocusButtonsOnMouseDown property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnFocusButtonsOnMouseDownChanged(bool oldValue, bool newValue)
        {
            this.BtnBack.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit8.FocusOnLeftMouseButtonDown = newValue;
            this.BtnReciprocal.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit5.FocusOnLeftMouseButtonDown = newValue;
            this.BtnSqrt.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDivide.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit9.FocusOnLeftMouseButtonDown = newValue;
            this.BtnClearEntry.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDecimal.FocusOnLeftMouseButtonDown = newValue;
            this.BtnAdd.FocusOnLeftMouseButtonDown = newValue;
            this.BtnEquals.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit4.FocusOnLeftMouseButtonDown = newValue;
            this.BtnSubtract.FocusOnLeftMouseButtonDown = newValue;
            this.BtnMultiply.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit6.FocusOnLeftMouseButtonDown = newValue;
            this.BtnClear.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit1.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit2.FocusOnLeftMouseButtonDown = newValue;
            this.BtnNegate.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit0.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit3.FocusOnLeftMouseButtonDown = newValue;
            this.BtnPercent.FocusOnLeftMouseButtonDown = newValue;
            this.BtnDigit7.FocusOnLeftMouseButtonDown = newValue;
            this.BtnMemStore.FocusOnLeftMouseButtonDown = newValue;
            this.BtnMemRestore.FocusOnLeftMouseButtonDown = newValue;
            this.BtnMemAdd.FocusOnLeftMouseButtonDown = newValue;
            this.BtnMemClear.FocusOnLeftMouseButtonDown = newValue;
            this.BtnMemSubtract.FocusOnLeftMouseButtonDown = newValue;
        }

        protected override void OnFontChanged(EventArgs e)
        {
            InvalidateAutoSize();
            BtnMemAdd.Font = new Font(this.Font.FontFamily, BtnMemAdd.Font.SizeInPoints);
            BtnMemClear.Font = new Font(this.Font.FontFamily, BtnMemClear.Font.SizeInPoints);
            BtnMemRestore.Font = new Font(this.Font.FontFamily, BtnMemRestore.Font.SizeInPoints);
            BtnMemStore.Font = new Font(this.Font.FontFamily, BtnMemStore.Font.SizeInPoints);
            BtnMemSubtract.Font = new Font(this.Font.FontFamily, BtnMemSubtract.Font.SizeInPoints);

            BtnDigit0.Font = new Font(this.Font.FontFamily, BtnDigit0.Font.SizeInPoints);
            BtnDigit1.Font = new Font(this.Font.FontFamily, BtnDigit1.Font.SizeInPoints);
            BtnDigit2.Font = new Font(this.Font.FontFamily, BtnDigit2.Font.SizeInPoints);
            BtnDigit3.Font = new Font(this.Font.FontFamily, BtnDigit3.Font.SizeInPoints);
            BtnDigit4.Font = new Font(this.Font.FontFamily, BtnDigit4.Font.SizeInPoints);
            BtnDigit5.Font = new Font(this.Font.FontFamily, BtnDigit5.Font.SizeInPoints);
            BtnDigit6.Font = new Font(this.Font.FontFamily, BtnDigit6.Font.SizeInPoints);
            BtnDigit7.Font = new Font(this.Font.FontFamily, BtnDigit7.Font.SizeInPoints);
            BtnDigit8.Font = new Font(this.Font.FontFamily, BtnDigit8.Font.SizeInPoints);
            BtnDigit9.Font = new Font(this.Font.FontFamily, BtnDigit9.Font.SizeInPoints);

            labelValue.Font = new Font(this.Font.FontFamily, labelValue.Font.SizeInPoints);

            base.OnFontChanged(e);
        }

        public override Size GetPreferredSize(Size proposedSize)
        {
            if (!_PreferredSize.IsEmpty) return _PreferredSize;

            if (!BarFunctions.IsHandleValid(this))
                return base.GetPreferredSize(proposedSize);

            _PreferredSize = pnlCalc.Size;
            return _PreferredSize;
        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            if (this.AutoSize)
            {
                Size preferredSize = base.PreferredSize;
                width = preferredSize.Width;
                height = preferredSize.Height;
            }
            base.SetBoundsCore(x, y, width, height, specified);
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            if (this.AutoSize)
                this.AdjustSize();
            base.OnHandleCreated(e);
        }
        #endregion

        #region DisplayVisible
        private bool _DisplayVisible = true;
        /// <summary>
        /// Gets or sets whether calculator display is visible. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether calculator display is visible.")]
        public bool DisplayVisible
        {
            get { return _DisplayVisible; }
            set
            {
                if (value != _DisplayVisible)
                {
                    bool oldValue = _DisplayVisible;
                    _DisplayVisible = value;
                    OnDisplayVisibleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when DisplayVisible property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnDisplayVisibleChanged(bool oldValue, bool newValue)
        {
            labelValue.Visible = newValue;
            if (newValue)
            {
                pnlPad.Top += labelValue.Height;
                pnlMem.Top += labelValue.Height;
                pnlCalc.Height += labelValue.Height;
            }
            else
            {
                pnlPad.Top -= labelValue.Height;
                pnlMem.Top -= labelValue.Height;
                pnlCalc.Height -= labelValue.Height;
            }
            _PreferredSize = Size.Empty;
            AdjustSize();
        }        
        #endregion
    }

    #region Enums

    public enum Operators
    {
        None = ' ',
        Calc = '=',
        Set = '\t',

        Add = '+',
        Subtract = '-',
        Multiply = '*',
        Divide = '/',
    }

    public enum DecimalKeyVisibility
    {
        Auto,
        Always,
        Never
    }

    #endregion

    #region ButtonClickEventArgs

    /// <summary>
    /// ButtonClickEventArgs
    /// </summary>
    public class ButtonClickEventArgs : CancelEventArgs
    {
        #region Private variables

        private object _Button;

        #endregion

        ///<summary>
        /// ButtonClickEventArgs
        ///</summary>
        public ButtonClickEventArgs(object button)
        {
            _Button = button;
        }

        #region Public properties

        /// <summary>
        /// Gets the calc button that was clicked
        /// </summary>
        public object Button
        {
            get { return (_Button); }
        }

        #endregion
    }

    #endregion

    #region ValueChangedEventArgs

    /// <summary>
    /// ValueChangedEventArgs
    /// </summary>
    public class ValueChangedEventArgs : EventArgs
    {
        #region Private variables

        private double _Value;
        private string _SValue;

        #endregion

        ///<summary>
        /// ValueChangedEventArgs
        ///</summary>
        ///<param name="svalue"></param>
        ///<param name="value"></param>
        public ValueChangedEventArgs(string svalue, double value)
        {
            _Value = value;
            _SValue = svalue;
        }

        #region Public properties

        /// <summary>
        /// Gets the input string value
        /// </summary>
        public string SValue
        {
            get { return (_SValue); }
        }

        /// <summary>
        /// Gets or sets calculator value
        /// </summary>
        public double Value
        {
            get { return (_Value); }
            set { _Value = value; }
        }

        #endregion
    }

    #endregion
}
