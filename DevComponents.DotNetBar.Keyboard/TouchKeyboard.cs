using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;

namespace DevComponents.DotNetBar.Keyboard
{
    [ToolboxItem(true), ProvideProperty("ShowTouchKeyboard", typeof(Control))]
    public class TouchKeyboard : Component, IExtenderProvider
    {
        private List<Control> _Targets = new List<Control>();
        private Control _CurrentTarget;
        private PopupVirtualKeyboard _PopupKeyboard;
        private KeyboardControl _VirtualKeyboard;


        public TouchKeyboard()
        {
            _PopupKeyboard = new PopupVirtualKeyboard();
            _VirtualKeyboard = new KeyboardControl();

            _PopupKeyboard.Size = new Size(KeyboardControl.DefaultWidth, KeyboardControl.DefaultHeight);

            // Track floating keyboard location and size changed. The user can change this at runtime with the mouse.
            _PopupKeyboard.SizeChanged += new EventHandler(PopupKeyboard_SizeChanged);
            _PopupKeyboard.LocationChanged += new EventHandler(PopupKeyboard_LocationChanged);

            // Track changes when the keyboard is inline.
            _VirtualKeyboard.SizeChanged += new EventHandler(VirtualKeyboard_SizeChanged);
            _VirtualKeyboard.LocationChanged += new EventHandler(VirtualKeyboard_LocationChanged);
            _VirtualKeyboard.DockChanged += new EventHandler(VirtualKeyboard_DockChanged);
            _VirtualKeyboard.SendingKey += new KeyCancelEventHandler(VirtualKeyboardSendingKey);
            _VirtualKeyboard.KeySent += new KeyEventHandler(VirtualKeyboardKeySent);
        }

        #region Handle Changed events.
        /// <summary>
        /// Occurs before the key pressed on keyboard is sent to target control and allows cancellation of the message
        /// </summary>
        [Description("Occurs before the key pressed on keyboard is sent to target control and allows cancellation of the message.")]
        public event KeyCancelEventHandler SendingKey;
        /// <summary>
        /// Raises SendingKey event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnSendingKey(KeyboardKeyCancelEventArgs e)
        {
            KeyCancelEventHandler handler = SendingKey;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs after the key is sent to target control.
        /// </summary>
        [Description("Occurs after the key is sent to target control.")]
        public event KeyEventHandler KeySent;
        /// <summary>
        /// Raises KeySent event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnKeySent(KeyboardKeyEventArgs e)
        {
            KeyEventHandler handler = KeySent;
            if (handler != null)
                handler(this, e);
        }

        private void VirtualKeyboardKeySent(object sender, KeyboardKeyEventArgs e)
        {
            OnKeySent(e);
        }

        private void VirtualKeyboardSendingKey(object sender, KeyboardKeyCancelEventArgs e)
        {
            OnSendingKey(e);
        }

        void PopupKeyboard_SizeChanged(object sender, EventArgs e)
        {
            _FloatingSize = _PopupKeyboard.Size;
            OnFloatingSizeChanged();
        }

        void PopupKeyboard_LocationChanged(object sender, EventArgs e)
        {
            OnFloatingLocationChanged();
        }

        void VirtualKeyboard_LocationChanged(object sender, EventArgs e)
        {
            if (_VirtualKeyboard.FindForm() != _PopupKeyboard)
            {
                OnLocationChanged();
            }
        }

        void VirtualKeyboard_SizeChanged(object sender, EventArgs e)
        {
            if (_VirtualKeyboard.FindForm() != _PopupKeyboard)
            {
                _Size = _VirtualKeyboard.Size;
                OnSizeChanged();
            }

        }

        void VirtualKeyboard_DockChanged(object sender, EventArgs e)
        {
            if (_VirtualKeyboard.FindForm() != _PopupKeyboard)
            {
                OnDockChanged();
            }
        }

        #endregion

        /// <summary>
        /// Gets the reference to internal keyboard control that is used to provide Keyboard visual.
        /// </summary>
        [Browsable(false)]
        public KeyboardControl KeyboardControl 
        {
            get
            {
                return _VirtualKeyboard;
            }
        }

        /// <summary>
        /// Attaches the Keyboard to the specified control. The keyboard will automatically appear when the control receives input focus.
        /// </summary>
        /// <param name="control">The control to which the Keyboard will be attached.</param>
        private void AttachTo(Control control)
        {
            control.GotFocus += new EventHandler(control_GotFocus);
            control.LostFocus += new EventHandler(control_LostFocus);

            _Targets.Add(control);
        }


        /// <summary>
        /// Detaches the Keyboard from the specified control.
        /// </summary>
        /// <param name="control">The control from which the Keyboard will be detached.</param>
        private void DetachFrom(Control control)
        {
            if (_Targets.Contains(control))
            {
                control.GotFocus -= new EventHandler(control_GotFocus);
                control.LostFocus -= new EventHandler(control_LostFocus);

                _Targets.Remove(control);
            }
        }

        /// <summary>
        /// Occurs before keyboard is shown and allows canceling of opening.
        /// </summary>
        [Description("Occurs before keyboard is shown and allows canceling of opening.")]
        public event CancelKeyboardEventHandler KeyboardOpening;
        /// <summary>
        /// Raises KeyboardOpening event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnKeyboardOpening(CancelKeyboardEventArgs e)
        {
            CancelKeyboardEventHandler handler = KeyboardOpening;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs after keyboard is shown.
        /// </summary>
        [Description("Occurs after keyboard is shown.")]
        public event EventHandler KeyboardOpened;
        /// <summary>
        /// Raises KeyboardOpened event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnKeyboardOpened(EventArgs e)
        {
            EventHandler handler = KeyboardOpened;
            if (handler != null)
                handler(this, e);
        }

        private bool _ShowingFloatingKeyboard = false;
        public void ShowKeyboard(Control control, TouchKeyboardStyle style)
        {
            CancelKeyboardEventArgs ce = new CancelKeyboardEventArgs(control, style);
            OnKeyboardOpening(ce);
            if (ce.Cancel) return;

            _CurrentTarget = ce.TargetControl;
            style = ce.Style;

            _PopupKeyboard.CurrentControl = _CurrentTarget;

            if (_CurrentTarget != null)
            {
                if (style == TouchKeyboardStyle.Floating)
                {
                    try
                    {
                        _ShowingFloatingKeyboard = true;
                        _PopupKeyboard.Owner = _CurrentTarget.FindForm();
                        _PopupKeyboard.Controls.Add(_VirtualKeyboard);
                        _VirtualKeyboard.Dock = DockStyle.Fill;
                        _VirtualKeyboard.Visible = true;
                        _PopupKeyboard.Show();

                        // When floating, don't show the top bar. The information on the top bar are on the window's title bar.
                        _VirtualKeyboard.IsTopBarVisible = false;
                    }
                    finally
                    {
                        _ShowingFloatingKeyboard = false;
                    }
                }
                else if (style == TouchKeyboardStyle.Inline)
                {
                    Form owner = _CurrentTarget.FindForm();
                    _VirtualKeyboard.Dock = Dock;
                    owner.Controls.Add(_VirtualKeyboard);
                    _VirtualKeyboard.BringToFront();
                    _VirtualKeyboard.Visible = true;

                    // When inline, show the top bar.
                    _VirtualKeyboard.IsTopBarVisible = true;
                }
            }

            OnKeyboardOpened(EventArgs.Empty);
        }

        /// <summary>
        /// Gets keyboard target control.
        /// </summary>
        [Browsable(false)]
        public Control CurrentKeyboardTarget
        {
            get { return _CurrentTarget; }
        }

        /// <summary>
        /// Occurs before the keyboard is closed and allows canceling of keyboard closing.
        /// </summary>
        [Description("Occurs before the keyboard is closed and allows canceling of keyboard closing.")]
        public event CancelEventHandler KeyboardClosing;
        /// <summary>
        /// Raises KeyboardClosing event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnKeyboardClosing(CancelEventArgs e)
        {
            CancelEventHandler handler = KeyboardClosing;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Occurs after keyboard is closed.
        /// </summary>
        [Description("Occurs after keyboard is closed.")]
        public event EventHandler KeyboardClosed;
        /// <summary>
        /// Raises KeyboardClosed event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnKeyboardClosed(EventArgs e)
        {
            EventHandler handler = KeyboardClosed;
            if (handler != null)
                handler(this, e);
        }

        private void HideKeyboard()
        {
            CancelEventArgs ce = new CancelEventArgs();
            OnKeyboardClosing(ce);
            if (ce.Cancel) return;

            _PopupKeyboard.Hide();
            Form form = _VirtualKeyboard.FindForm();
            if (form != null && !(form is PopupVirtualKeyboard))
            {
                form.Controls.Remove(_VirtualKeyboard);
            }

            OnKeyboardClosed(EventArgs.Empty);
        }


        void control_GotFocus(object sender, EventArgs e)
        {
            Control control = sender as Control;

            if (control != null)
            {
                TouchKeyboardStyle style = TouchKeyboardStyle.No;
                TouchKeyboardStyle formStyle = GetShowTouchKeyboard(control.FindForm());

                // If the form specifies a mode to show the keyboard, then use that mode. If we want for the control to have the last
                // word about how to show the keyboard, the following two if instructions should be inverted.

                if (_ExtendedControls.ContainsKey(control))
                    style = _ExtendedControls[control];

                if (formStyle != TouchKeyboardStyle.No)
                    style = formStyle;

                if (style != TouchKeyboardStyle.No)
                {
                    ShowKeyboard(control, style);
                }
            }
        }

        private bool IsPopupKeyboardActivated 
        {
            get
            {
                if (_PopupKeyboard == null) return false;
                return Form.ActiveForm == _PopupKeyboard;
            }
        }

        void control_LostFocus(object sender, EventArgs e)
        {
            if (!_ShowingFloatingKeyboard && !IsPopupKeyboardActivated)
            {
                _CurrentTarget = null;
                HideKeyboard();
            }
        }


        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_VirtualKeyboard != null)
                    _VirtualKeyboard.Dispose();
                if (_PopupKeyboard != null)
                    _PopupKeyboard.Dispose();
            }

            base.Dispose(disposing);
        }


        bool IExtenderProvider.CanExtend(object extendee)
        {
            if (extendee is Control && !(extendee is KeyboardControl))
                return true;
            else
                return false;
        }


        Dictionary<Control, TouchKeyboardStyle> _ExtendedControls = new Dictionary<Control, TouchKeyboardStyle>();


        /// <summary>
        /// Retursn the way the keyboard will be shown when controls receive focus.
        /// </summary>
        /// <param name="extendee">The control for which to retrieve the value.</param>
        /// <returns>A TouchKeyboardStyle value defining the way the keyboard appears.</returns>
        [Description("Shows an on screen touch keyboard when the control is focused.")]
        [Category("Touch Keyboard")]
        [DefaultValue(TouchKeyboardStyle.No)]
        public TouchKeyboardStyle GetShowTouchKeyboard(Control extendee)
        {
            if (extendee == null)
                return TouchKeyboardStyle.No;

            if (_ExtendedControls.ContainsKey(extendee))
                return _ExtendedControls[extendee];
            else
                return TouchKeyboardStyle.No;
        }

        /// <summary>
        /// Sets the way the keyboard will be shown when controls receive focus. If the control is a Form,
        /// all controls on the Form will support this way of displaying the touch keyboard.
        /// </summary>
        /// <param name="extendee">The control for which this value is specified.</param>
        /// <param name="value">A TouchKeyboardStyle value defining the way the keyboard appears.</param>
        public void SetShowTouchKeyboard(Control extendee, TouchKeyboardStyle value)
        {
            if (extendee is Form)
            {
                if (value != TouchKeyboardStyle.No)
                {
                    foreach (Control c in extendee.Controls)
                    {
                        AttachTo(c);
                    }

                    // If other controls are added to the form, attach to those controls also.
                    extendee.ControlAdded += new ControlEventHandler(form_ControlAdded);

                    // If controls are removed from the form, dettach from them, we don't want to keep a reference to them.
                    extendee.ControlRemoved += new ControlEventHandler(form_ControlRemoved);
                }
            }

            if (!_ExtendedControls.ContainsKey(extendee))
                _ExtendedControls.Add(extendee, value);
            else
                _ExtendedControls[extendee] = value;

            if (value == TouchKeyboardStyle.No)
                DetachFrom(extendee);
            else
                AttachTo(extendee);
        }


        void form_ControlAdded(object sender, ControlEventArgs e)
        {
            if (sender is KeyboardControl)
                return;

            AttachTo(e.Control);
        }


        void form_ControlRemoved(object sender, ControlEventArgs e)
        {
            if (sender is KeyboardControl)
                return;

            DetachFrom(e.Control);
        }


        /// <summary>
        /// Gets or set the ColorTable used to draw the keyboard.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VirtualKeyboardColorTable ColorTable
        {
            get { return _VirtualKeyboard.ColorTable; }
            set { _VirtualKeyboard.ColorTable = value; }
        }


        /// <summary>
        /// Gets or set the Renderer used to draw the keyboard.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Renderer Renderer
        {
            get { return _VirtualKeyboard.Renderer; }
            set { _VirtualKeyboard.Renderer = value; }
        }


        private Size _Size = new Size(KeyboardControl.DefaultWidth, KeyboardControl.DefaultHeight);
        /// <summary>
        /// Gets or sets the size of the keyboard, when shown inline.
        /// </summary>
        [Category("Inline Layout")]
        [Description("Gets or sets the size of the keyboard, when the keyboard is shown inline.")]
        [Browsable(true)]
        public Size Size
        {
            get { return _Size; }
            set
            {
                if (_Size != value)
                {
                    _Size = value; 
                    OnSizeChanged();
                }
            }
        }

        private void OnSizeChanged()
        {
            Size newSize = Size;
            if (newSize.IsEmpty)
                newSize = new Size(KeyboardControl.DefaultWidth, KeyboardControl.DefaultHeight);

            _VirtualKeyboard.Size = newSize;

            if (SizeChanged != null)
                SizeChanged(this, EventArgs.Empty);
        }


        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the keyboard when shown inline, relative to the form.
        /// </summary>
        [Category("Inline Layout")]
        [Description("The coordinates of the upper-left corner of the keyboard, when the keyboard is shown inline, relative to the form.")]
        [Browsable(true)]
        public Point Location
        {
            get { return _VirtualKeyboard.Location; }
            set 
            {
                if (_VirtualKeyboard.Location != value)
                {
                    _VirtualKeyboard.Location = value;
                    OnLocationChanged();
                }
            }
        }

        private void OnLocationChanged()
        {
            if (LocationChanged != null)
                LocationChanged(this, EventArgs.Empty);
        }


        private DockStyle _Dock = DockStyle.Bottom;
        /// <summary>
        /// Defines which borders of the keyboard are bound to the container.
        /// </summary>
        [Category("Inline Layout")]
        [Description("Defines which border of the keyboard are bound to the form, when the keyboard is shown inline. Default value is DockStyle.Bottom.")]
        [DefaultValue(DockStyle.Bottom)]
        [Browsable(true)]
        public DockStyle Dock
        {
            get { return _Dock; }
            set
            {
                if (_Dock != value)
                {
                    _Dock = value; 
                    OnDockChanged();
                }
            }
        }

        private void OnDockChanged()
        {
            // When keybaord style is Floating, dock is always fill (to fill to popup window).
            if (_PopupKeyboard.Controls.Contains(_VirtualKeyboard))
                _VirtualKeyboard.Dock = DockStyle.Fill;
            else
                _VirtualKeyboard.Dock = _Dock;

            if (DockChanged != null)
                DockChanged(this, EventArgs.Empty);
        }


        /// <summary>
        /// Gets or sets the coordinates of the upper-left corner of the keyboard when shown floating, relative to the screen.
        /// </summary>
        [Category("Floating Layout")]
        [Description("The coordinates of the upper-left corner of the keyboard, when the keyboard is shown floating, relative to the screen.")]
        [Browsable(true)]
        public Point FloatingLocation
        {
            get { return _PopupKeyboard.Location; }
            set
            {
                if (_PopupKeyboard.Location != value)
                {
                    _PopupKeyboard.Location = value;
                    OnFloatingLocationChanged();
                }
            }
        }

        private void OnFloatingLocationChanged()
        {
            if (FloatingLocationChanged != null)
                FloatingLocationChanged(this, EventArgs.Empty);
        }


        private Size _FloatingSize = new Size(KeyboardControl.DefaultWidth, KeyboardControl.DefaultHeight);
        /// <summary>
        /// Gets or sets the size of the keyboard, when shown floating.
        /// </summary>
        [Category("Floating Layout")]
        [Description("Gets or sets the size of the keyboard, when the keyboard is shown floating.")]
        [Browsable(true)]
        public Size FloatingSize
        {
            get { return _FloatingSize; }
            set 
            {
                if (_FloatingSize != value)
                {
                    _FloatingSize = value;
                    OnFloatingSizeChanged();
                }
            }
        }

        private void OnFloatingSizeChanged()
        {
            Size newSize = FloatingSize;
            if (newSize.IsEmpty)
                newSize = new Size(KeyboardControl.DefaultWidth, KeyboardControl.DefaultHeight);

            _PopupKeyboard.Size = newSize;

            if (FloatingSizeChanged != null)
                FloatingSizeChanged(this, EventArgs.Empty);
        }


        /// <summary>
        /// Gets or sets a text associated with this control.
        /// </summary>
        [Category("Appearance")]
        [Description("The text associated with this control.")]
        [Browsable(true)]
        public string Text
        {
            get { return _VirtualKeyboard.Text; }
            set 
            {
                _VirtualKeyboard.Text = value; 
                _PopupKeyboard.Text = value; 
                OnTextChanged(); 
            }
        }

        /// <summary>
        /// Raises the TextChanged event.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Advanced)]
        protected void OnTextChanged()
        {
            if (TextChanged != null)
                TextChanged(this, EventArgs.Empty);
        }

        /// <summary>
        /// Occurs when the Text property has changed.
        /// </summary>
        [Category("PropertyChanged")]
        [Description("Event raised when the Text property has changed.")]
        public event EventHandler TextChanged;

        /// <summary>
        /// Occurs when the Size property has changed.
        /// </summary>
        [Category("Inline Layout")]
        [Description("Event raised when the Size property has changed.")]
        public event EventHandler SizeChanged;

        /// <summary>
        /// Occurs when the Location property has changed.
        /// </summary>
        [Category("Inline Layout")]
        [Description("Event raised when the Location property has changed.")]
        public event EventHandler LocationChanged;

        /// <summary>
        /// Occurs when the Dock property has changed.
        /// </summary>
        [Category("Inline Layout")]
        [Description("Event raised when the Dock property has changed.")]
        public event EventHandler DockChanged;

        /// <summary>
        /// Occurs when the FloatingSize property has changed.
        /// </summary>
        [Category("Floating Layout")]
        [Description("Event raised when the FloatingSize property has changed.")]
        public event EventHandler FloatingSizeChanged;

        /// <summary>
        /// Occurs when the FloatingLocation property has changed.
        /// </summary>
        [Category("Floating Layout")]
        [Description("Event raised when the FloatingLocation property has changed.")]
        public event EventHandler FloatingLocationChanged;
    }


    /// <summary>
    /// Defines the way the touch keyboard will appear when attached to controls.
    /// </summary>
    public enum TouchKeyboardStyle
    {
        /// <summary>
        /// Touch keyboard will not be visible.
        /// </summary>
        No,

        /// <summary>
        /// Touch keyboard will appear inline in the form.
        /// </summary>
        Inline,

        /// <summary>
        /// Touch keyboard will appear floating on the screen.
        /// </summary>
        Floating
    }

    /// <summary>
    /// Defines delegate for keyboard events.
    /// </summary>
    public delegate void CancelKeyboardEventHandler(object sender, CancelKeyboardEventArgs e);
    /// <summary>
    /// Defines event arguments for keyboard based events.
    /// </summary>
    public class CancelKeyboardEventArgs : CancelEventArgs
    {
        /// <summary>
        /// Initializes a new instance of the CancelKeyboardEventArgs class.
        /// </summary>
        /// <param name="targetControl"></param>
        /// <param name="style"></param>
        public CancelKeyboardEventArgs(Control targetControl, TouchKeyboardStyle style)
        {
            TargetControl = targetControl;
            Style = style;
        }
        /// <summary>
        /// Gets or sets the keyboard target control.
        /// </summary>
        public Control TargetControl;

        /// <summary>
        /// Gets or sets the keyboard style.
        /// </summary>
        public TouchKeyboardStyle Style;
    }

}
