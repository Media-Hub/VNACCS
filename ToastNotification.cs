using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents class used to display toast notifications. A toast notification is a message that appears on the surface of the screen for a moment, 
    /// but it does not take focus (or pause the current activity), so it cannot accept any user input.
    /// Notification pops up on the surface of the specified Form or Control.
    /// It only fills the amount of space required for the message and the user's current activity remains visible and interactive.
    /// The notification automatically fades in and out after specified time interval.
    /// Notification text supports text-markup.
    /// </summary>
    public static class ToastNotification
    {
        /// <summary>
        /// Closes all toast notifications open on specified parent control.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        public static void Close(Control parent)
        {
            if (parent == null) throw new NullReferenceException("parent parameter for toast notification must be set.");
            Close(parent, IntPtr.Zero);
        }
        /// <summary>
        /// Closes specified toast notification on parent control.
        /// </summary>
        /// <param name="parent">Parent control.</param>
        /// <param name="toastId">Toast ID as returned by the Show method.</param>
        public static void Close(Control parent, IntPtr toastId)
        {
            if (parent == null) throw new NullReferenceException("parent parameter for toast notification must be set.");

            if (toastId == IntPtr.Zero)
            {
                foreach (Control item in parent.Controls)
                {
                    if (item is ToastDisplay)
                        item.Visible = false;
                }
            }
            else
            {
                foreach (Control item in parent.Controls)
                {
                    if (item.Handle == toastId)
                    {
                        item.Visible = false;
                        break;
                    }
                }
            }
        }

        private static IntPtr ShowInternal(Control parent, string message, Image image, int timeoutInterval, eToastGlowColor toastGlowColor, eToastPosition? toastPosition, int x, int y)
        {
            if (parent == null) throw new NullReferenceException("parent parameter for toast notification must be set.");

            if (timeoutInterval < 1) timeoutInterval = 0;

            ToastDisplay toast = new ToastDisplay();
            toast.BackColor = Color.Transparent;
            toast.ToastBackColor = _ToastBackColor;
            toast.ForeColor = _ToastForeColor;
            if (_ToastFont != null) toast.Font = _ToastFont;
            bool isTerminalSession = NativeFunctions.IsTerminalSession();
            toast.Alpha = isTerminalSession ? 255 : 0;
            toast.Text = message;
            toast.Image = image;
            toast.GlowColor = toastGlowColor;
            parent.Controls.Add(toast);
            Size toastSize = toast.DesiredSize(_ToastMargin, parent.ClientRectangle);
            toast.BringToFront();
            if (toastPosition != null)
                toast.Bounds = CalculateToastBounds(toastPosition.Value, parent.ClientRectangle, toastSize);
            else
                toast.Bounds = new Rectangle(x, y, toastSize.Width, toastSize.Height);

            toast.Visible = true;
            IntPtr toastId = toast.Handle;
            if (!isTerminalSession)
            {
                Animation.AnimationInt anim = new DevComponents.DotNetBar.Animation.AnimationInt(new Animation.AnimationRequest(toast, "Alpha", 0, 255),
                        Animation.AnimationEasing.EaseOutQuad, 250);
                //anim.FixedStepCount = 10;
                anim.AutoDispose = true;
                anim.Start();
            }

            if (timeoutInterval > 0)
                BarUtilities.InvokeDelayed(new MethodInvoker(delegate { CloseToast(toast); }), timeoutInterval);

            return toastId;
        }

        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form.
        /// </summary>
        /// <param name="parent">Parent form to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="image">Image to display next to toast text.</param>
        /// <param name="timeoutInterval">Interval in milliseconds after which the notification is hidden.</param>
        /// <param name="toastGlowColor">Specifies toast-glow color used.</param>
        /// <param name="toastPosition">Specifies the position of the toast notification.</param>
        public static IntPtr Show(Control parent, string message, Image image, int timeoutInterval, eToastGlowColor toastGlowColor, eToastPosition toastPosition)
        {
            return ShowInternal(parent, message, image, timeoutInterval, toastGlowColor, toastPosition, 0, 0);
        }

        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form.
        /// </summary>
        /// <param name="parent">Parent form to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="image">Image to display next to toast text.</param>
        /// <param name="timeoutInterval">Interval in milliseconds after which the notification is hidden.</param>
        /// <param name="toastGlowColor">Specifies toast-glow color used.</param>
        /// <param name="x">Specifies the X position of the toast notification with its parent window.</param>
        /// <param name="y">Specifies the Y position of the toast notification with its parent window.</param>
        public static IntPtr Show(Control parent, string message, Image image, int timeoutInterval, eToastGlowColor toastGlowColor, int x, int y)
        {
            return ShowInternal(parent, message, image, timeoutInterval, toastGlowColor, null, x, y);
        }

        private static Rectangle CalculateToastBounds(eToastPosition toastPosition, Rectangle parentClientRectangle, Size toastSize)
        {
            Rectangle displayBounds = Rectangle.Empty;
            if (toastPosition == eToastPosition.BottomCenter)
                displayBounds = new Rectangle(parentClientRectangle.X + (parentClientRectangle.Width - toastSize.Width) / 2,
                    parentClientRectangle.Bottom - toastSize.Height - _ToastMargin.Bottom,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.BottomLeft)
                displayBounds = new Rectangle(parentClientRectangle.X + _ToastMargin.Left,
                    parentClientRectangle.Bottom - toastSize.Height - _ToastMargin.Bottom,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.BottomRight)
                displayBounds = new Rectangle(parentClientRectangle.Right - _ToastMargin.Right - toastSize.Width,
                    parentClientRectangle.Bottom - toastSize.Height - _ToastMargin.Bottom,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.MiddleCenter)
                displayBounds = new Rectangle(parentClientRectangle.X + (parentClientRectangle.Width - toastSize.Width) / 2,
                    parentClientRectangle.Y + (parentClientRectangle.Height - toastSize.Height) / 2,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.MiddleLeft)
                displayBounds = new Rectangle(parentClientRectangle.X + _ToastMargin.Left,
                    parentClientRectangle.Y + (parentClientRectangle.Height - toastSize.Height) / 2,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.MiddleRight)
                displayBounds = new Rectangle(parentClientRectangle.Right - _ToastMargin.Right - toastSize.Width,
                    parentClientRectangle.Y + (parentClientRectangle.Height - toastSize.Height) / 2,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.TopCenter)
                displayBounds = new Rectangle(parentClientRectangle.X + (parentClientRectangle.Width - toastSize.Width) / 2,
                    parentClientRectangle.Y + _ToastMargin.Top,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.TopLeft)
                displayBounds = new Rectangle(parentClientRectangle.X + _ToastMargin.Left,
                    parentClientRectangle.Y + _ToastMargin.Top,
                    toastSize.Width, toastSize.Height);
            else if (toastPosition == eToastPosition.TopRight)
                displayBounds = new Rectangle(parentClientRectangle.Right - _ToastMargin.Right - toastSize.Width,
                    parentClientRectangle.Y + _ToastMargin.Top,
                    toastSize.Width, toastSize.Height);

            return displayBounds;
        }
        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form.
        /// </summary>
        /// <param name="parent">Parent form to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="image">Image to display next to toast text.</param>
        /// <param name="timeoutInterval">Interval in milliseconds after which the notification is hidden.</param>
        /// /// <param name="toastGlowColor">Specifies toast-glow color used.</param>
        public static IntPtr Show(Control parent, string message, Image image, int timeoutInterval, eToastGlowColor toastGlowColor)
        {
            return Show(parent, message, image, timeoutInterval, toastGlowColor, eToastPosition.BottomCenter);
        }

        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form, with default timeout interval.
        /// </summary>
        /// <param name="parent">Parent control to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        public static IntPtr Show(Control parent, string message)
        {
            return Show(parent, message, null, _DefaultTimeoutInterval, _DefaultToastGlowColor);
        }

        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form, with default timeout interval.
        /// </summary>
        /// <param name="parent">Parent control to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="toastPosition">Specifies the position of the toast notification.</param>
        public static IntPtr Show(Control parent, string message, eToastPosition toastPosition)
        {
            return Show(parent, message, null, _DefaultTimeoutInterval, _DefaultToastGlowColor, toastPosition);
        }

        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form, with default timeout interval.
        /// </summary>
        /// <param name="parent">Parent control to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="image">Image to display next to toast text.</param>
        public static IntPtr Show(Control parent, string message, Image image)
        {
            return Show(parent, message, image, _DefaultTimeoutInterval, _DefaultToastGlowColor);
        }

        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form.
        /// </summary>
        /// <param name="parent">Parent form to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="image">Image to display next to toast text.</param>
        /// <param name="timeoutInterval">Interval in milliseconds after which the notification is hidden.</param>
        public static IntPtr Show(Control parent, string message, Image image, int timeoutInterval)
        {
            return Show(parent, message, image, timeoutInterval, _DefaultToastGlowColor);
        }
        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form.
        /// </summary>
        /// <param name="parent">Parent form to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="timeoutInterval">Interval in milliseconds after which the notification is hidden.</param>
        public static IntPtr Show(Control parent, string message, int timeoutInterval)
        {
            return Show(parent, message, null, timeoutInterval, _DefaultToastGlowColor);
        }
        /// <summary>
        /// Displays the toast notification on top of the specified parent control, we recommend always using a parent form.
        /// </summary>
        /// <param name="parent">Parent form to display toast notification on top of</param>
        /// <param name="message">Message to display.</param>
        /// <param name="timeoutInterval">Interval in milliseconds after which the notification is hidden.</param>
        /// <param name="toastPosition">Specifies the position of the toast notification.</param>
        public static IntPtr Show(Control parent, string message, int timeoutInterval, eToastPosition toastPosition)
        {
            return Show(parent, message, null, timeoutInterval, _DefaultToastGlowColor, toastPosition);
        }

        private static void CloseToast(ToastDisplay toast)
        {
            bool isTerminalSession = NativeFunctions.IsTerminalSession();

            if (toast.IsDisposed)
            {
                if (toast.Parent != null)
                    toast.Parent.Controls.Remove(toast);
                return;
            }

            if (isTerminalSession)
            {
                toast.Visible = false;
                toast.Parent.Controls.Remove(toast);
                toast.Dispose();
            }
            else
            {
                Animation.AnimationInt anim = new DevComponents.DotNetBar.Animation.AnimationInt(new Animation.AnimationRequest(toast, "Alpha", 255, 0),
                        Animation.AnimationEasing.EaseInQuad, 250);
                anim.AutoDispose = true;
                anim.Start();
                anim.AnimationCompleted += new EventHandler(delegate { if (toast.Parent != null) toast.Parent.Controls.Remove(toast); toast.Dispose(); });
            }
        }

        private static Padding _ToastMargin = new Padding(16);
        /// <summary>
        /// Specifies the toast margin from the edges of the parent control. Default value is 16 pixels on all sides.
        /// </summary>
        public static Padding ToastMargin
        {
            get
            {
                return _ToastMargin;
            }
            set
            {
                _ToastMargin = value;
            }
        }

        private static eToastPosition _DefaultToastPosition = eToastPosition.BottomCenter;
        /// <summary>
        /// Specifies the default toast position within the parent control. Default value is BottomCenter.
        /// </summary>
        public static eToastPosition DefaultToastPosition
        {
            get { return _DefaultToastPosition; }
            set { _DefaultToastPosition = value; }
        }

        private static eToastGlowColor _DefaultToastGlowColor = eToastGlowColor.Blue;
        /// <summary>
        /// Specifies default glow color around toast notification. Default value is Blue.
        /// </summary>
        public static eToastGlowColor DefaultToastGlowColor
        {
            get { return _DefaultToastGlowColor; }
            set { _DefaultToastGlowColor = value; }
        }

        private static int _DefaultTimeoutInterval = 2500;
        /// <summary>
        /// Specifies the default timeout interval for the toast notification.
        /// </summary>
        public static int DefaultTimeoutInterval
        {
            get { return _DefaultTimeoutInterval; }
            set
            {
                _DefaultTimeoutInterval = value;
            }
        }

        private static Color _ToastBackColor = Color.FromArgb (7, 7, 7);
        /// <summary>
        /// Specifies the toast background color.
        /// </summary>
        public static Color ToastBackColor
        {
            get { return _ToastBackColor; }
            set { _ToastBackColor = value; }
        }

        private static Color _ToastForeColor = Color.White;
        /// <summary>
        /// Specifies the toast text color.
        /// </summary>
        public static Color ToastForeColor
        {
            get { return _ToastForeColor; }
            set { _ToastForeColor = value; }
        }

        private static Font _ToastFont = null;
        /// <summary>
        /// Specifies the font used for the toast.
        /// </summary>
        public static Font ToastFont
        {
            get { return _ToastFont; }
            set { _ToastFont = value; }
        }

        private static Color _CustomGlowColor = Color.Brown;
        /// <summary>
        /// Specifies the custom glow color used when eToastGlowColor.Custom is used.
        /// </summary>
        public static Color CustomGlowColor
        {
            get { return _CustomGlowColor; }
            set { _CustomGlowColor = value; }
        }
    }

    /// <summary>
    /// Specifies toast position within parent control.
    /// </summary>
    public enum eToastPosition
    {
        TopLeft,
        TopCenter,
        TopRight,
        MiddleLeft,
        MiddleCenter,
        MiddleRight,
        BottomLeft,
        BottomCenter,
        BottomRight
    }

    /// <summary>
    /// Specifies the glow color around toast notification.
    /// </summary>
    public enum eToastGlowColor
    {
        None,
        Red,
        Blue,
        Green,
        Orange,
        Custom
    }
}
