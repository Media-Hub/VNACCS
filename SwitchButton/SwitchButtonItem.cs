using System;
using System.ComponentModel;
using DevComponents.DotNetBar.Events;
using DevComponents.DotNetBar.Rendering;
using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents the switch button UI element.
    /// </summary>
    [DefaultEvent("ValueChanged"), DefaultProperty("Value")]
    public class SwitchButtonItem : BaseItem
    {
        #region Events
        /// <summary>
        /// Occurs before Value property has changed and it allows you to cancel the change.
        /// </summary>
        public event EventHandler ValueChanging;
        /// <summary>
        /// Raises ValueChanging event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnValueChanging(CancelableEventSourceArgs e)
        {
            EventHandler handler = ValueChanging;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs after Value property has changed.
        /// </summary>
        public event EventHandler ValueChanged;
        /// <summary>
        /// Raises ValueChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnValueChanged(EventSourceArgs e)
        {
            EventHandler handler = ValueChanged;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        internal static readonly int TextButtonSpacing = 3;
        /// <summary>
		/// Creates new instance of SwitchButtonItem.
		/// </summary>
		public SwitchButtonItem():this("","") {}
		/// <summary>
		/// Creates new instance of SwitchButtonItem and assigns the name to it.
		/// </summary>
		/// <param name="sItemName">Item name.</param>
		public SwitchButtonItem(string sItemName):this(sItemName,"") {}
		/// <summary>
        /// Creates new instance of SwitchButtonItem and assigns the name and text to it.
		/// </summary>
		/// <param name="sItemName">Item name.</param>
		/// <param name="itemText">item text.</param>
        public SwitchButtonItem(string sItemName, string itemText)
            : base(sItemName, itemText)
        {
            _Margin.PropertyChanged += MarginPropertyChanged;
        }

        /// <summary>
        /// Returns copy of the item.
        /// </summary>
        public override BaseItem Copy()
        {
            SwitchButtonItem objCopy = new SwitchButtonItem(m_Name);
            this.CopyToItem(objCopy);
            return objCopy;
        }

        /// <summary>
        /// Copies the SwitchButtonItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New SwitchButtonItem instance.</param>
        internal void InternalCopyToItem(SwitchButtonItem copy)
        {
            CopyToItem(copy);
        }

        /// <summary>
        /// Copies the SwitchButtonItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New SwitchButtonItem instance.</param>
        protected override void CopyToItem(BaseItem copy)
        {
            SwitchButtonItem objCopy = copy as SwitchButtonItem;

            if (objCopy != null)
            {
                objCopy.Value = this.Value;
                objCopy.BorderColor = this.BorderColor;
                objCopy.OffBackColor = this.OffBackColor;
                objCopy.OffText = this.OffText;
                objCopy.OffTextColor = this.OffTextColor;
                objCopy.OnBackColor = this.OnBackColor;
                objCopy.OnText = this.OnText;
                objCopy.OnTextColor = this.OnTextColor;
                objCopy.SwitchBackColor = this.SwitchBackColor;
                objCopy.SwitchBorderColor = this.SwitchBorderColor;
                objCopy.SwitchWidth = this.SwitchWidth;
                objCopy.ButtonWidth = this.ButtonWidth;
                objCopy.ButtonHeight = this.ButtonHeight;

                base.CopyToItem(objCopy);
            }
        }
        #endregion

        #region Implementation
        protected override void Dispose(bool disposing)
        {
            CancelAnimation();
            base.Dispose(disposing);
        }
        
        public override void Paint(ItemPaintArgs p)
        {
            BaseRenderer renderer = p.Renderer;
            if (renderer != null)
            {
                SwitchButtonRenderEventArgs e = new SwitchButtonRenderEventArgs(p.Graphics, this, p.Colors, p.Font, p.RightToLeft);
                e.ItemPaintArgs = p;
                renderer.DrawSwitchButton(e);
            }
            else
            {
                SwitchButtonPainter painter = PainterFactory.CreateSwitchButtonPainter(this);
                if (painter != null)
                {
                    SwitchButtonRenderEventArgs e = new SwitchButtonRenderEventArgs(p.Graphics, this, p.Colors, p.Font, p.RightToLeft);
                    e.ItemPaintArgs = p;
                    painter.Paint(e);
                }
            }

            this.DrawInsertMarker(p.Graphics);
        }

        public override void RecalcSize()
        {
            Size size = new Size(_ButtonWidth + _Margin.Horizontal, _ButtonHeight + _Margin.Vertical);

            if (_TextVisible && !string.IsNullOrEmpty(this.Text))
            {
                Control parent = this.ContainerControl as Control;
                if (parent != null)
                {
                    Font font = parent.Font;
                    using (Graphics g = parent.CreateGraphics())
                    {
                        Size textSize = ButtonItemLayout.MeasureItemText(this, g, 0, font, eTextFormat.WordBreak, parent.RightToLeft == RightToLeft.Yes);
                        textSize.Width += _TextPadding.Horizontal + TextButtonSpacing;
                        textSize.Height += _TextPadding.Vertical;
                        size.Width += textSize.Width;
                        size.Height = Math.Max(size.Height, textSize.Height);
                    }
                }
            }

            m_Rect.Width = size.Width;
            m_Rect.Height = size.Height;

            base.RecalcSize();
        }

        internal Size GetPreferredSize()
        {
            Size size = new Size(_SwitchWidth + _Margin.Horizontal, 8);

            Control parent = this.ContainerControl as Control;
            if (parent != null)
            {
                Font font = parent.Font;
                using (Graphics g = parent.CreateGraphics())
                {
                    Size textOnSize = TextDrawing.MeasureString(g, _OnText, font);
                    Size textOffSize = TextDrawing.MeasureString(g, _OffText, font);
                    size.Width += Math.Max(textOnSize.Width, textOffSize.Width) + 8;
                    size.Height = Math.Max(size.Height, textOffSize.Height + _Margin.Vertical + 4);

                    if (_TextVisible && !string.IsNullOrEmpty(this.Text))
                    {
                        Size textSize = ButtonItemLayout.MeasureItemText(this, g, 0, font, eTextFormat.WordBreak, parent.RightToLeft == RightToLeft.Yes);
                        textSize.Width += _TextPadding.Horizontal + TextButtonSpacing;
                        textSize.Height += _TextPadding.Vertical;
                        size.Width += textSize.Width;
                        size.Height = Math.Max(size.Height, textSize.Height);
                    }
                }
            }

            return size;
        }

        /// <summary>
        /// Gets whether item supports text markup. Default is false.
        /// </summary>
        protected override bool IsMarkupSupported
        {
            get { return _EnableMarkup; }
        }

        private bool _EnableMarkup = true;
        /// <summary>
        /// Gets or sets whether text-markup support is enabled for items Text property. Default value is true.
        /// Set this property to false to display HTML or other markup in the item instead of it being parsed as text-markup.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether text-markup support is enabled for items Text property.")]
        public bool EnableMarkup
        {
            get { return _EnableMarkup; }
            set
            {
                if (_EnableMarkup != value)
                {
                    _EnableMarkup = value;
                    NeedRecalcSize = true;
                    OnTextChanged();
                }
            }
        }

        /// <summary>
        /// Gets or sets the text associated with this item.
        /// </summary>
        [Browsable(true), Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor)), Category("Appearance"), Description("The text contained in the item."), Localizable(true), DefaultValue("")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        private bool _Value;
        /// <summary>
        /// Gets or sets the switch value.
        /// </summary>
        [DefaultValue(false), Category("Appearance"), Description("Indicates switch value."), Bindable(true)]
        public bool Value
        {
            get { return _Value; }
            set
            {
                if (_Value != value)
                    SetValue(value, eEventSource.Code);
            }
        }

        /// <summary>
        /// Sets the value of the control and specifies the source of the action.
        /// </summary>
        /// <param name="newValue">New value for Value property.</param>
        /// <param name="source">Source of the action.</param>
        public void SetValue(bool newValue, eEventSource source)
        {
            CancelableEventSourceArgs cancelEventArgs = new CancelableEventSourceArgs(source);
            OnValueChanging(cancelEventArgs);
            if (cancelEventArgs.Cancel)
            {
                SwitchOffset = 0;
                this.Refresh();
                return;
            }

            _Value = newValue;
            _AsyncValue = newValue;

            if (ShouldSyncProperties)
                BarFunctions.SyncProperty(this, "Value");

            SwitchOffset = 0;

            if (this.Displayed)
                this.Refresh();

            ExecuteCommand();
            EventSourceArgs sourceEventArgs = new EventSourceArgs(source);
            OnValueChanged(sourceEventArgs);
        }

        private Point _MouseDownPoint = Point.Empty;
         public override void InternalMouseDown(MouseEventArgs objArg)
        {
            if (this.Enabled && !_IsReadOnly && !this.DesignMode && objArg.Button == MouseButtons.Left)
            {
                if (!Value && _OffPartBounds.Contains(objArg.X, objArg.Y))
                    this.SetValueAndAnimate(true, eEventSource.Mouse);
                else if (Value && _OnPartBounds.Contains(objArg.X, objArg.Y))
                    this.SetValueAndAnimate(false, eEventSource.Mouse);
                else if (_SwitchBounds.Contains(objArg.X, objArg.Y))
                {
                    _MouseDownPoint = new Point(objArg.X, objArg.Y);
                    SwitchPressed = true;
                }
            }
            base.InternalMouseDown(objArg);
        }

        public override void InternalMouseMove(MouseEventArgs objArg)
        {
            if (_SwitchPressed)
            {
                if (objArg.Button == MouseButtons.Left)
                {
                    Point mouseDownPoint = _MouseDownPoint;
                    int offsetX = objArg.X - mouseDownPoint.X;
                    if (offsetX != 0)
                    {
                        if (!Value && offsetX > 0)
                        {
                            if (_SwitchBounds.Right + offsetX >= _ButtonBounds.Right)
                            {
                                SetValue(true, eEventSource.Mouse);
                                _MouseDownPoint = new Point(objArg.X, objArg.Y);
                            }
                            else
                                SwitchOffset = offsetX;
                        }
                        else if (Value && offsetX < 0)
                        {
                            if (_SwitchBounds.X + offsetX <= _ButtonBounds.X)
                            {
                                SetValue(false, eEventSource.Mouse);
                                _MouseDownPoint = new Point(objArg.X, objArg.Y);
                            }
                            else
                                SwitchOffset = Math.Abs(offsetX);
                        }
                    }
                }
                else
                {
                    SwitchPressed = false;
                }
            }

            base.InternalMouseMove(objArg);
        }

        public override void InternalMouseUp(MouseEventArgs objArg)
        {
            if (_SwitchPressed)
            {
                SwitchPressed = false;
            }
            base.InternalMouseUp(objArg);
        }
        
        private Rectangle _SwitchBounds;
        /// <summary>
        /// Gets the switch bounds.
        /// </summary>
        [Browsable(false)]
        public Rectangle SwitchBounds
        {
            get { return _SwitchBounds; }
            internal set { _SwitchBounds = value; }
        }

        private Rectangle _ButtonBounds;
        /// <summary>
        /// Gets the button bounds.
        /// </summary>
        [Browsable(false)]
        public Rectangle ButtonBounds
        {
            get { return _ButtonBounds; }
            internal set { _ButtonBounds = value; }
        }

        private Rectangle _OnPartBounds;
        /// <summary>
        /// Gets the On part of the switch button bounds excluding the SwitchBounds.
        /// </summary>
        [Browsable(false)]
        public Rectangle OnPartBounds
        {
            get { return _OnPartBounds; }
            internal set { _OnPartBounds = value; }
        }

        private Rectangle _OffPartBounds;
        /// <summary>
        /// Gets the Off part of the switch button bounds excluding the SwitchBounds.
        /// </summary>
        [Browsable(false)]
        public Rectangle OffPartBounds
        {
            get { return _OffPartBounds; }
            internal set { _OffPartBounds = value; }
        }

        private int _SwitchOffset;
        /// <summary>
        /// Gets or sets the switch offset from its initial position. Used for animation and dragging of the switch.
        /// </summary>
        internal int SwitchOffset
        {
            get { return _SwitchOffset; }
            set
            {
                _SwitchOffset = value;
                this.Refresh();
            }
        }

        private bool _SwitchPressed;
        /// <summary>
        /// Gets whether switch part of the button is pressed using mouse left button.
        /// </summary>
        [Browsable(false)]
        public bool SwitchPressed
        {
            get { return _SwitchPressed; }
            internal set 
            {
                _SwitchPressed = value;
                SwitchOffset = 0;
                this.Refresh();
            }
        }

        private int _SwitchWidth = 28;
        /// <summary>
        /// Gets or sets the width in pixels of the switch part of the button. Minimum value is 6.
        /// </summary>
        [DefaultValue(28), Category("Appearance"), Description("Indicates width in pixels of the switch part of the button.")]
        public int SwitchWidth
        {
            get { return _SwitchWidth; }
            set
            {
                if (value < 6) value = 6;
                if (_SwitchWidth != value)
                {
                    _SwitchWidth = value;
                    this.Refresh();
                }
            }
        }

        private string _OnText = "ON";
        /// <summary>
        /// Gets or sets the text that is displayed on switch when Value property is set to true.
        /// </summary>
        [DefaultValue("ON"), Localizable(true), Category("Appearance"), Description("Indicates text that is displayed on switch when Value property is set to true.")]
        public string OnText
        {
            get { return _OnText; }
            set
            {
                if (value == null) value = "";
                if (_OnText != value)
                {
                    _OnText = value;
                    this.Refresh();
                }
            }
        }

        private string _OffText = "OFF";
        /// <summary>
        /// Gets or sets the text that is displayed on switch when Value property is set to false.
        /// </summary>
        [DefaultValue("OFF"), Localizable(true), Category("Appearance"), Description("Indicates text that is displayed on switch when Value property is set to true.")]
        public string OffText
        {
            get { return _OffText; }
            set
            {
                if (value == null) value = "";
                if (_OffText != value)
                {
                    _OffText = value;
                    this.Refresh();
                }
            }
        }

        private bool _AsyncValue;
        private eEventSource _AsyncSource;
        private int _AsyncTotalSwitchOffset;
        private BackgroundWorker _AnimationWorker;

        /// <summary>
        /// Sets the value of the control with state transition animation (if enabled) and specifies the source of the action.
        /// </summary>
        /// <param name="value">New value for Value property.</param>
        /// <param name="source">Source of the action.</param>
        public void SetValueAndAnimate(bool value, eEventSource source)
        {
            if (!IsAnimationEnabled)
            {
                SetValue(value, source);
                return;
            }

            BackgroundWorker worker = _AnimationWorker;
            if (worker != null)
            {
                worker.CancelAsync();
                SetValue(value, source);
                return;
            }

            if (Value != value)
            {
                _AsyncValue = value;
                _AsyncSource = source;
                _AsyncTotalSwitchOffset = _ButtonBounds.Width - SwitchWidth;

                _AnimationWorker = new BackgroundWorker();

                _AnimationWorker.WorkerSupportsCancellation = true;
                _AnimationWorker.DoWork += AnimationWorkerDoWork;
                _AnimationWorker.RunWorkerCompleted += AnimationWorkerRunWorkerCompleted;

                _AnimationWorker.RunWorkerAsync();
            }
        }

        private void AnimationWorkerRunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            BackgroundWorker worker = _AnimationWorker;
            _AnimationWorker = null;

            if (worker != null)
                worker.Dispose();

            if (e.Cancelled == false)
                SetValue(_AsyncValue, _AsyncSource);
        }

        private void AnimationWorkerDoWork(object sender, DoWorkEventArgs e)
        {
            DateTime start = DateTime.Now;

            int steps = Math.Max(9, _AsyncTotalSwitchOffset / 5);

            for (int i = 0; i < _AsyncTotalSwitchOffset; i += steps)
            {
                if (_AnimationWorker.CancellationPending == true)
                {
                    e.Cancel = true;
                    break;
                }

                SwitchOffset = i;
                System.Threading.Thread.Sleep(40);

                if (DateTime.Now.Subtract(start).TotalMilliseconds > 400)
                    break;
            }
        }

        /// <summary>
        /// Cancels any current inprogress animation.
        /// </summary>
        internal void CancelAnimation()
        {
            BackgroundWorker worker = _AnimationWorker;

            if (worker != null)
            {
                if (worker.IsBusy)
                {
                    worker.CancelAsync();

                    if (Value != _AsyncValue)
                        SetValue(_AsyncValue, _AsyncSource);
                }
            }
        }

        /// <summary>
        /// Gets whether fade effect is enabled.
        /// </summary>
        protected virtual bool IsAnimationEnabled
        {
            get { return _AnimationEnabled; }
        }

        private bool _AnimationEnabled = true;
        /// <summary>
        /// Gets or sets whether state transition animation is enabled.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether state transition animation is enabled.")]
        public bool AnimationEnabled
        {
            get { return _AnimationEnabled; }
            set { _AnimationEnabled = value; }
        }

        private int _ButtonWidth = 66;
        /// <summary>
        /// Gets or sets the width of the switch button. Must be greater than SwitchWidth.
        /// </summary>
        [DefaultValue(66), Category("Appearance"), Description("Indicates width of the switch button. Must be greater than SwitchWidth.")]
        public int ButtonWidth
        {
            get { return _ButtonWidth; }
            set
            {
                if (value < _SwitchWidth)
                    value = _SwitchWidth + 1;
                _ButtonWidth = value;
                NeedRecalcSize = true;
                this.Refresh();
            }
        }

        private int _ButtonHeight = 22;
        /// <summary>
        /// Gets or sets the height of the switch button. Must be greater than 5.
        /// </summary>
        [DefaultValue(22), Category("Appearance"), Description("Indicates height of the switch button. Must be greater than 5.")]
        public int ButtonHeight
        {
            get { return _ButtonHeight; }
            set
            {
                if (value < 6) value = 6;
                _ButtonHeight = value;
                NeedRecalcSize = true;
                this.Refresh();
            }
        }

        private Padding _TextPadding = new Padding(0, 0, 0, 0);
        /// <summary>
        /// Gets or sets text padding.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Gets or sets text padding."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Padding TextPadding
        {
            get { return _TextPadding; }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeTextPadding()
        {
            return _TextPadding.Bottom != 0 || _TextPadding.Top != 0 || _TextPadding.Left != 0 || _TextPadding.Right != 0;
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetTextPadding()
        {
            _TextPadding = new Padding(0, 0, 0, 0);
        }
        //private void TextPaddingPropertyChanged(object sender, PropertyChangedEventArgs e)
        //{
        //    NeedRecalcSize = true;
        //    this.Refresh();
        //}

        private Padding _Margin = new Padding(0, 0, 0, 0);
        /// <summary>
        /// Gets or sets switch margin.
        /// </summary>
        [Browsable(true), Category("Appearance"), Description("Gets or sets switch margin."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public Padding Margin
        {
            get { return _Margin; }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeMargin()
        {
            return _Margin.Bottom != 0 || _Margin.Top != 0 || _Margin.Left != 0 || _Margin.Right != 0;
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetMargin()
        {
            _Margin = new Padding(0, 0, 0, 0);
        }
        private void MarginPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            NeedRecalcSize = true;
            this.Refresh();
        }

        private Color _OffBackColor = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the OFF state background.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of OFF state background.")]
        public Color OffBackColor
        {
            get { return _OffBackColor; }
            set { _OffBackColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOffBackColor()
        {
            return !_OffBackColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetOffBackColor()
        {
            this.OffBackColor = Color.Empty;
        }

        private Color _OnBackColor = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the ON state background.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of ON state background.")]
        public Color OnBackColor
        {
            get { return _OnBackColor; }
            set { _OnBackColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnBackColor()
        {
            return !_OnBackColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetOnBackColor()
        {
            this.OnBackColor = Color.Empty;
        }

        private Color _OnTextColor = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the ON state text.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of ON state text.")]
        public Color OnTextColor
        {
            get { return _OnTextColor; }
            set { _OnTextColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOnTextColor()
        {
            return !_OnTextColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetOnTextColor()
        {
            this.OnTextColor = Color.Empty;
        }

        private Color _OffTextColor = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the OFF state text.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of OFF state text.")]
        public Color OffTextColor
        {
            get { return _OffTextColor; }
            set { _OffTextColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeOffTextColor()
        {
            return !_OffTextColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetOffTextColor()
        {
            this.OffTextColor = Color.Empty;
        }

        private Color _BorderColor = Color.Empty;
        /// <summary>
        /// Gets or sets the item border color.
        /// </summary>
        [Category("Appearance"), Description("Indicates item border color.")]
        public Color BorderColor
        {
            get { return _BorderColor; }
            set { _BorderColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBorderColor()
        {
            return !_BorderColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBorderColor()
        {
            this.BorderColor = Color.Empty;
        }

        private Color _SwitchBorderColor = Color.Empty;
        /// <summary>
        /// Gets or sets the border color of the button switch.
        /// </summary>
        [Category("Appearance"), Description("Indicates border color of the button switch.")]
        public Color SwitchBorderColor
        {
            get { return _SwitchBorderColor; }
            set { _SwitchBorderColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSwitchBorderColor()
        {
            return !_SwitchBorderColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetSwitchBorderColor()
        {
            this.SwitchBorderColor = Color.Empty;
        }

        private Color _SwitchBackColor = Color.Empty;
        /// <summary>
        /// Gets or sets the background color of the switch button.
        /// </summary>
        [Category("Appearance"), Description("Indicates background color of the switch button.")]
        public Color SwitchBackColor
        {
            get { return _SwitchBackColor; }
            set { _SwitchBackColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSwitchBackColor()
        {
            return !_SwitchBackColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetSwitchBackColor()
        {
            this.SwitchBackColor = Color.Empty;
        }

        private bool _TextVisible = true;
        /// <summary>
        /// Gets or sets whether caption/label set using Text property is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether caption/label set using Text property is visible.")]
        public bool TextVisible
        {
            get { return _TextVisible; }
            set
            {
                _TextVisible = value;
                NeedRecalcSize = true;
                this.Refresh();
            }
        }

        private Color _TextColor = Color.Empty;
        /// <summary>
        /// Gets or sets the text color.
        /// </summary>
        [Category("Appearance"), Description("Indicates text color.")]
        public Color TextColor
        {
            get { return _TextColor; }
            set { _TextColor = value; }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeTextColor()
        {
            return !_TextColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetTextColor()
        {
            this.TextColor = Color.Empty;
        }

        private Font _SwitchFont;
        /// <summary>
        /// Gets or sets the font that is used to draw ON/OFF text on the switch button.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Indicates font that is used to draw ON/OFF text on the switch button.")]
        public Font SwitchFont
        {
            get { return _SwitchFont; }
            set { _SwitchFont = value; this.Refresh(); }
        }

        private bool _IsReadOnly;
        /// <summary>
        /// Gets or sets whether button is in read-only state meaning that it appears as enabled but user cannot change its state.
        /// </summary>
        [DefaultValue(false), Category("Behavior"), Description("Indicates whether button is in read-only state meaning that it appears as enabled but user cannot change its state.")]
        public bool IsReadOnly
        {
            get { return _IsReadOnly; }
            set { _IsReadOnly = value; this.Refresh(); }
        }

        private bool _ShowReadOnlyMarker = true;
        /// <summary>
        /// Gets or sets whether lock marker is visible on face of the control when IsReadOnly is set to true.
        /// Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether lock marker is visible on face of the control when IsReadOnly is set to true.")]
        public bool ShowReadOnlyMarker
        {
            get { return _ShowReadOnlyMarker; }
            set { _ShowReadOnlyMarker = value; this.Refresh(); }
        }

        private static readonly Color DefaultReadOnlyMarkerColor = ColorScheme.GetColor(0xC0504D);
        private Color _ReadOnlyMarkerColor = DefaultReadOnlyMarkerColor;
        /// <summary>
        /// Gets or sets the color of the read-only marker.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of read-only marker.")]
        public Color ReadOnlyMarkerColor
        {
            get { return _ReadOnlyMarkerColor; }
            set { _ReadOnlyMarkerColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeReadOnlyMarkerColor()
        {
            return !_ReadOnlyMarkerColor.Equals(DefaultReadOnlyMarkerColor);
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetReadOnlyMarkerColor()
        {
            this.ReadOnlyMarkerColor = DefaultReadOnlyMarkerColor;
        }
        #endregion
    }
}
