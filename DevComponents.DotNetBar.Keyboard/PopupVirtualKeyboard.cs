using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Keyboard
{
    internal class PopupVirtualKeyboard : Form
    {
        /// <summary>
        /// Creates a new floating virtual keyboard.
        /// </summary>
        public PopupVirtualKeyboard()
        {
            StartPosition = FormStartPosition.Manual;
            
            // Make sure the popup window does not appear in the taskbar.
            ShowInTaskbar = false;
            
            MinimizeBox = false;
            MaximizeBox = false;
            //ControlBox = false;
        }


        #region Window activation handling

        // We need to prevent the keyboard window from activating, otherwise will still focus from our target control.
        // Also to achieve this effect, there must be no focusable control in the popup window.

        private const int WM_MOUSEACTIVATE = 0x0021;
        private const int MA_NOACTIVATE = 0x0003;
        private const int WS_EX_NOACTIVATE = 0x08000000;

        // Override CreateParams to specify that this Form should not be activated.
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams createParams = base.CreateParams;
                createParams.ExStyle = createParams.ExStyle & WS_EX_NOACTIVATE;
                return createParams;
            }
        }


        protected override void WndProc(ref Message m)
        {
            // This prevents the window from being activated with the mouse, 
            // but allows the window to be resized (with the mouse).
            if (m.Msg == WM_MOUSEACTIVATE)
            {
                m.Result = (IntPtr)MA_NOACTIVATE;
            }
            else
            {
                base.WndProc(ref m);
            }
        }


        protected override void OnActivated(EventArgs e)
        {
            Owner.Activate();
        }

        #endregion


        protected override void OnClosing(CancelEventArgs e)
        {
            // Cancel the Closing event as this will Dispose the form.
            e.Cancel = true;
            Hide();
        }


        protected override void OnVisibleChanged(EventArgs e)
        {
            base.OnVisibleChanged(e);

            if (Visible && CurrentControl != _LastControl && CurrentControl != null)
            {
                _LastControl = CurrentControl;
            }
        }


        internal Control CurrentControl { get; set; }
        private Control _LastControl;
        /*
        public void MoveNearControl(Control control)
        {
            // We want to show the keyboard near the target control, horizontally centered, 
            // under the control or above it and without overlapping it, if possible.

            // First try to position the Keyboard under the target control.
            Point location = control.PointToScreen(Point.Empty);
            location.Offset(control.Width / 2 - Width / 2, control.Height);
            Location = location;

            // And make sure it is in the screen containing the control.
            EnsureInScreen(control);

            // Now check if this position does not overlap the control.
            if (!OverlapsControl(control))
                return;

            // Now we try to position the Keyboard above the control.
            location = control.PointToScreen(Point.Empty);
            location.Offset(control.Width / 2 - Width / 2, -Height);
            Location = location;

            // And again make sure this location is in the screen with the control.
            EnsureInScreen(control);

            // And check if it does not overlap the control.
            if (!OverlapsControl(control))
                return;

            // If both below and above the control was not a good position, then this means the
            // target control is quite large, so show the Keyboard inside it.
            location = control.PointToScreen(Point.Empty);
            location.Offset(control.Width / 2 - Width / 2, control.Height / 2 - Height / 2);

            // And again, make sure the Keybboard is not clipped by the screen.
            EnsureInScreen(control);
        }

        /// <summary>
        /// Returns true if the Keyboard overlaps the specified control.
        /// </summary>
        /// <param name="control">The control for which to check if it is overlapped by the Keyboard.</param>
        /// <returns></returns>
        private bool OverlapsControl(Control control)
        {
            Rectangle rect = new Rectangle(control.PointToScreen(Point.Empty), control.Size);
            return rect.IntersectsWith(Bounds);
        }

        /// <summary>
        /// Checks if the Keyboard is in the screen containing the specified control, and if 
        /// the Keyboard is not fully visible, adjusts it's location accordingly.
        /// </summary>
        /// <param name="control">The control from which the screen is checked. If null, the primary screen is used.</param>
        private void EnsureInScreen(Control control)
        {
            Screen screen = Screen.PrimaryScreen;
            
            if(control != null)
                screen = Screen.FromControl(control);

            Rectangle rect = screen.WorkingArea;

            if (Location.X < rect.Left)
                Location = new Point(rect.Left, Location.Y);

            if (Location.Y < rect.Top)
                Location = new Point(Location.X, rect.Top);

            if (Bounds.Right > rect.Right)
                Location = new Point(rect.Right - Width, Location.Y);

            if (Bounds.Bottom > rect.Bottom)
                Location = new Point(Location.X, rect.Bottom - Height);
        }
        */
    }
}
