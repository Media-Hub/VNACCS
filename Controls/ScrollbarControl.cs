using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar.ScrollBar;
using System.Drawing;
using System.Runtime.InteropServices;

namespace DevComponents.DotNetBar.Controls
{
    /// <summary>
    /// Represents control which handles scroll-bars.
    /// </summary>
    [ToolboxItem(false)]
    public class ScrollbarControl : ControlWithBackgroundStyle
    {
        private const int ScrollPositionUpdateDelay = 400;
        private HScrollBarAdv _HScrollBar = null;
        private VScrollBarAdv _VScrollBar = null;
        private Control _Thumb = null;
        /// <summary>
        /// Initializes a new instance of the ScrollbarControl class.
        /// </summary>
        public ScrollbarControl()
        {
            this.SetStyle(ControlStyles.UserPaint | 
                ControlStyles.AllPaintingInWmPaint |
                ControlStyles.Opaque | 
                ControlStyles.ResizeRedraw |
                DisplayHelp.DoubleBufferFlag, true);
            
            _HScrollBar = new HScrollBarAdv();
            _HScrollBar.Visible = false;
            _HScrollBar.Appearance = _ScrollBarAppearance;
            _HScrollBar.Scroll += HScrollBarScroll;
            _VScrollBar = new VScrollBarAdv();
            _VScrollBar.Visible = false;
            _VScrollBar.Appearance = _ScrollBarAppearance;
            _VScrollBar.Scroll += VScrollBarScroll;
            this.Controls.Add(_HScrollBar);
            this.Controls.Add(_VScrollBar);

            _Thumb = new Control();
            _Thumb.Visible = false;
            _Thumb.BackColor = SystemColors.Window;
            this.Controls.Add(_Thumb);
        }

        private eScrollBarAppearance _ScrollBarAppearance = eScrollBarAppearance.Default;
        /// <summary>
        /// Gets or sets the scroll-bar visual style.
        /// </summary>
        [DefaultValue(eScrollBarAppearance.Default), Category("Appearance"), Description("Gets or sets the scroll-bar visual style.")]
        public eScrollBarAppearance ScrollBarAppearance
        {
            get { return _ScrollBarAppearance; }
            set
            {
                _ScrollBarAppearance = value;
                OnScrollBarAppearanceChanged();
            }
        }
        private void OnScrollBarAppearanceChanged()
        {
            if (_VScrollBar != null) _VScrollBar.Appearance = _ScrollBarAppearance;
            if (_HScrollBar != null) _HScrollBar.Appearance = _ScrollBarAppearance;
        }

        private void UpdateScrollOverrideControlBounds()
        {
            if (_ScrollOverrideControl != null)
                _ScrollOverrideControl.Bounds = GetScrollOverrideControlBounds();
        }
        protected override void OnResize(EventArgs e)
        {
            UpdateScrollOverrideControlBounds();
            UpdateScrollBars();
            base.OnResize(e);
        }

        protected override void OnVisualPropertyChanged()
        {
            base.OnVisualPropertyChanged();
            UpdateScrollOverrideControlBounds();
            UpdateScrollBars();
        }

        private bool _UpdatingScrollbars = false;
        protected virtual void UpdateScrollBarsDelayed()
        {
            InvokeDelayed(new MethodInvoker(delegate { UpdateScrollBars(); }), ScrollPositionUpdateDelay);
        }
        protected virtual void UpdateScrollBars()
        {
            if (!IsHandleCreated || _UpdatingScrollbars) return;
            _UpdatingScrollbars = true;
            try
            {
                Control scrollOverrideControl = _ScrollOverrideControl;
                if (scrollOverrideControl == null)
                {
                    if (_HScrollBar.Visible) _HScrollBar.Visible = false;
                    if (_VScrollBar.Visible) _VScrollBar.Visible = false;
                    return;
                }
                WinApi.SCROLLBARINFO psbi = new WinApi.SCROLLBARINFO();
                psbi.cbSize = Marshal.SizeOf(psbi);
                WinApi.GetScrollBarInfo(scrollOverrideControl.Handle, (uint)WinApi.eObjectId.OBJID_VSCROLL, ref psbi);
                if (psbi.rgstate[0] != (int)WinApi.eStateFlags.STATE_SYSTEM_INVISIBLE)
                {
                    Rectangle vsBounds = psbi.rcScrollBar.ToRectangle();
                    vsBounds.Location = this.PointToClient(vsBounds.Location);
                    Rectangle scrollCountrolBounds = _ScrollOverrideControl.Bounds;
                    if (!scrollCountrolBounds.Contains(vsBounds))
                    {
                        // We need to guess bounds for best performance and appearance
                        if (vsBounds.Right > scrollCountrolBounds.Right)
                        {
                            vsBounds.X = scrollCountrolBounds.Right - vsBounds.Width;
                        }
                        else if (vsBounds.X < scrollCountrolBounds.X)
                        {
                            vsBounds.X = 0;
                        }
                    }
                    if (_VScrollBar.Bounds != vsBounds)
                    {
                        _VScrollBar.Bounds = vsBounds;
                        UpdateVerticalScrollBarValues();
                    }
                    if (!_VScrollBar.Visible)
                    {
                        _VScrollBar.Visible = true;
                        _VScrollBar.Refresh();
                        InvokeDelayed(new MethodInvoker(delegate { UpdateVerticalScrollBarValues(); }), ScrollPositionUpdateDelay);
                    }

                    if (psbi.rgstate[0] == (int)WinApi.eStateFlags.STATE_SYSTEM_UNAVAILABLE)
                        _VScrollBar.Enabled = false;
                    else if (!_VScrollBar.Enabled)
                        _VScrollBar.Enabled = true;
                    //Console.WriteLine("VscrollBar Bounds detection {0}", vsBounds);
                }
                else if (_VScrollBar.Visible)
                    _VScrollBar.Visible = false;

                psbi = new WinApi.SCROLLBARINFO();
                psbi.cbSize = Marshal.SizeOf(psbi);
                WinApi.GetScrollBarInfo(scrollOverrideControl.Handle, (uint)WinApi.eObjectId.OBJID_HSCROLL, ref psbi);
                if (psbi.rgstate[0] != (int)WinApi.eStateFlags.STATE_SYSTEM_INVISIBLE)
                {
                    Rectangle hsBounds = psbi.rcScrollBar.ToRectangle();
                    hsBounds.Location = this.PointToClient(hsBounds.Location);
                    Rectangle scrollCountrolBounds = _ScrollOverrideControl.Bounds;
                    if (!scrollCountrolBounds.Contains(hsBounds))
                    {
                        // We need to guess bounds for best performance and appearance
                        if (hsBounds.Bottom > scrollCountrolBounds.Bottom)
                        {
                            hsBounds.Y = scrollCountrolBounds.Bottom - hsBounds.Height;
                        }
                    }
                    if (_VScrollBar.Visible && hsBounds.Width == scrollCountrolBounds.Width)
                        hsBounds.Width -= _VScrollBar.Width;
                    if (_HScrollBar.Bounds != hsBounds)
                    {
                        _HScrollBar.Bounds = hsBounds;
                        UpdateHorizontalScrollBarValues();
                    }
                    if (!_HScrollBar.Visible)
                    {
                        _HScrollBar.Visible = true;
                        _HScrollBar.Refresh();
                        InvokeDelayed(new MethodInvoker(delegate { UpdateHorizontalScrollBarValues(); }), ScrollPositionUpdateDelay);
                    }

                    if (psbi.rgstate[0] == (int)WinApi.eStateFlags.STATE_SYSTEM_UNAVAILABLE)
                        _HScrollBar.Enabled = false;
                    else if (!_HScrollBar.Enabled)
                        _HScrollBar.Enabled = true;
                }
                else if (_HScrollBar.Visible)
                    _HScrollBar.Visible = false;

                if (_HScrollBar.Visible && _VScrollBar.Visible)
                {
                    _Thumb.Bounds = new Rectangle(_VScrollBar.Left, _VScrollBar.Bounds.Bottom, _VScrollBar.Width, _HScrollBar.Height);
                    _Thumb.Visible = true;
                }
                else
                {
                    _Thumb.Visible = false;
                }
            }
            finally
            {
                _UpdatingScrollbars = false;
            }
        }

        private System.Drawing.Rectangle GetScrollOverrideControlBounds()
        {
            return GetContentRectangle();
        }
        
        private Control _ScrollOverrideControl = null;
        internal Control ScrollOverrideControl
        {
            get { return _ScrollOverrideControl; }
            set
            {
                if (value != _ScrollOverrideControl)
                {
                    if (!(value is IScrollBarOverrideSupport))
                        throw new ArgumentException("ScrollOverrideControl must implement IScrollBarOverrideSupport interface.");
                    Control oldValue = _ScrollOverrideControl;
                    _ScrollOverrideControl = value;
                    OnScrollOverrideControlChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when ScrollOverrideControl property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnScrollOverrideControlChanged(Control oldValue, Control newValue)
        {
            if (oldValue != null)
            {
                IScrollBarOverrideSupport sbo = (IScrollBarOverrideSupport)newValue;
                sbo.NonClientSizeChanged -= NonClientSizeChanged;
                sbo.ScrollBarValueChanged -= ControlScrollBarValueChanged;
                this.Controls.Remove(oldValue);
            }
            if (newValue != null)
            {
                IScrollBarOverrideSupport sbo = (IScrollBarOverrideSupport)newValue;
                sbo.NonClientSizeChanged += NonClientSizeChanged;
                sbo.ScrollBarValueChanged += ControlScrollBarValueChanged;
                this.Controls.Add(newValue);
            }
            UpdateScrollOverrideControlBounds();
            if (IsHandleCreated)
            {
                //InitializeWindowOverride();
                UpdateScrollBars();
            }
        }
        private bool _InternalVScrollPositionUpdated = false;
        private void VScrollBarScroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue == e.OldValue && e.Type != ScrollEventType.EndScroll) return;
            _InternalVScrollPositionUpdated = true;
            WinApi.SendMessage(_ScrollOverrideControl.Handle, (int)WinApi.WindowsMessages.WM_VSCROLL, WinApi.CreateLParam(MapVScrollType(e.Type), e.NewValue), IntPtr.Zero);
            _InternalVScrollPositionUpdated = false;

            if (e.Type == ScrollEventType.EndScroll)
                UpdateVerticalScrollBarValues();
        }
        private bool _InternalHScrollPositionUpdated = false;
        private void HScrollBarScroll(object sender, ScrollEventArgs e)
        {
            if (e.NewValue == e.OldValue && e.Type != ScrollEventType.EndScroll) return;
            _InternalHScrollPositionUpdated = true;
            //Console.WriteLine("{0} Setting Sys_HScrollBar value {1}, {2}", DateTime.Now, e.NewValue, e.Type);
            WinApi.SendMessage(_ScrollOverrideControl.Handle, (int)WinApi.WindowsMessages.WM_HSCROLL, WinApi.CreateLParam(MapHScrollType(e.Type), e.NewValue), IntPtr.Zero);
            _InternalHScrollPositionUpdated = false;

            if (e.Type == ScrollEventType.EndScroll)
                UpdateHorizontalScrollBarValues();
        }
        private static int MapVScrollType(ScrollEventType type)
        {
            if (type == ScrollEventType.EndScroll)
                return (int)WinApi.ScrollBarCommands.SB_ENDSCROLL;
            else if (type == ScrollEventType.First)
                return (int)WinApi.ScrollBarCommands.SB_TOP;
            else if (type == ScrollEventType.LargeDecrement)
                return (int)WinApi.ScrollBarCommands.SB_PAGEUP;
            else if (type == ScrollEventType.LargeIncrement)
                return (int)WinApi.ScrollBarCommands.SB_PAGEDOWN;
            else if (type == ScrollEventType.Last)
                return (int)WinApi.ScrollBarCommands.SB_BOTTOM;
            else if (type == ScrollEventType.SmallDecrement)
                return (int)WinApi.ScrollBarCommands.SB_LINEUP;
            else if (type == ScrollEventType.SmallIncrement)
                return (int)WinApi.ScrollBarCommands.SB_LINEDOWN;
            else if (type == ScrollEventType.ThumbPosition)
                return (int)WinApi.ScrollBarCommands.SB_THUMBPOSITION;
            else if (type == ScrollEventType.ThumbTrack)
                return (int)WinApi.ScrollBarCommands.SB_THUMBTRACK;

            return 0;
        }
        private static int MapHScrollType(ScrollEventType type)
        {
            if (type == ScrollEventType.EndScroll)
                return (int)WinApi.ScrollBarCommands.SB_ENDSCROLL;
            else if (type == ScrollEventType.First)
                return (int)WinApi.ScrollBarCommands.SB_LEFT;
            else if (type == ScrollEventType.LargeDecrement)
                return (int)WinApi.ScrollBarCommands.SB_PAGELEFT;
            else if (type == ScrollEventType.LargeIncrement)
                return (int)WinApi.ScrollBarCommands.SB_PAGERIGHT;
            else if (type == ScrollEventType.Last)
                return (int)WinApi.ScrollBarCommands.SB_RIGHT;
            else if (type == ScrollEventType.SmallDecrement)
                return (int)WinApi.ScrollBarCommands.SB_LINELEFT;
            else if (type == ScrollEventType.SmallIncrement)
                return (int)WinApi.ScrollBarCommands.SB_LINERIGHT;
            else if (type == ScrollEventType.ThumbPosition)
                return (int)WinApi.ScrollBarCommands.SB_THUMBPOSITION;
            else if (type == ScrollEventType.ThumbTrack)
                return (int)WinApi.ScrollBarCommands.SB_THUMBTRACK;

            return 0;
        }


        private void UpdateVerticalScrollBarValues()
        {
            if (_InternalVScrollPositionUpdated) return;
            WinApi.SCROLLINFO scrollInfo = new WinApi.SCROLLINFO();
            scrollInfo.cbSize = Marshal.SizeOf(scrollInfo);
            scrollInfo.fMask = WinApi.ScrollInfoMask.SIF_POS | WinApi.ScrollInfoMask.SIF_RANGE | WinApi.ScrollInfoMask.SIF_PAGE;
            if (WinApi.GetScrollInfo(_ScrollOverrideControl.Handle, WinApi.SBOrientation.SB_VERT, ref scrollInfo))
            {
                //Console.WriteLine("{0}   TRACKPOS={1}", DateTime.Now, scrollInfo.nTrackPos);
                if (_VScrollBar.Minimum != scrollInfo.nMin)
                    _VScrollBar.Minimum = scrollInfo.nMin;
                if (_VScrollBar.Maximum != scrollInfo.nMax)
                    _VScrollBar.Maximum = scrollInfo.nMax;
                if (_VScrollBar.Value != scrollInfo.nPos)
                    _VScrollBar.Value = scrollInfo.nPos;
                if (_VScrollBar.LargeChange != scrollInfo.nPage)
                    _VScrollBar.LargeChange = scrollInfo.nPage;
            }
        }
        private void UpdateHorizontalScrollBarValues()
        {
            if (_InternalHScrollPositionUpdated) return;
            WinApi.SCROLLINFO scrollInfo = new WinApi.SCROLLINFO();
            scrollInfo.cbSize = Marshal.SizeOf(scrollInfo);
            scrollInfo.fMask = WinApi.ScrollInfoMask.SIF_POS | WinApi.ScrollInfoMask.SIF_RANGE | WinApi.ScrollInfoMask.SIF_PAGE | WinApi.ScrollInfoMask.SIF_TRACKPOS;
            if (WinApi.GetScrollInfo(_ScrollOverrideControl.Handle, WinApi.SBOrientation.SB_HORZ, ref scrollInfo))
            {
                //Console.WriteLine("{0}   TRACKPOS={1}", DateTime.Now, scrollInfo);
                if (_HScrollBar.Minimum != scrollInfo.nMin)
                    _HScrollBar.Minimum = scrollInfo.nMin;
                if (_HScrollBar.Maximum != scrollInfo.nMax)
                    _HScrollBar.Maximum = scrollInfo.nMax;
                if (_HScrollBar.Value != scrollInfo.nPos)
                    _HScrollBar.Value = scrollInfo.nPos;
                if (_HScrollBar.LargeChange != scrollInfo.nPage)
                    _HScrollBar.LargeChange = scrollInfo.nPage;
            }
        }
        private void ControlScrollBarValueChanged(object sender, ScrollValueChangedEventArgs e)
        {
            if ((e.ScrollChange & eScrollBarScrollChange.Vertical) == eScrollBarScrollChange.Vertical)
            {
                UpdateVerticalScrollBarValues();
            }
            if ((e.ScrollChange & eScrollBarScrollChange.MouseWheel) == eScrollBarScrollChange.MouseWheel)
            {
                UpdateVerticalScrollBarValues();
                InvokeDelayed(new MethodInvoker(delegate { UpdateVerticalScrollBarValues(); }), ScrollPositionUpdateDelay);
            }
            if ((e.ScrollChange & eScrollBarScrollChange.Horizontal) == eScrollBarScrollChange.Horizontal)
            {
                UpdateHorizontalScrollBarValues();
            }
            UpdateScrollBars();
        }

        private void NonClientSizeChanged(object sender, EventArgs e)
        {
            UpdateScrollBars();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateScrollBars();
            base.OnHandleCreated(e);
        }

        #region Child Control WndProc Override
        internal static eScrollBarScrollChange MapMessageToScrollChange(int msg)
        {
            if (msg == (int)WinApi.WindowsMessages.WM_VSCROLL)
                return eScrollBarScrollChange.Vertical;
            else if (msg == (int)WinApi.WindowsMessages.WM_HSCROLL)
                return eScrollBarScrollChange.Horizontal;
            else if (msg == (int)WinApi.WindowsMessages.WM_MOUSEWHEEL)
                return eScrollBarScrollChange.MouseWheel;
            throw new ArgumentException("Message not recognized msg=" + msg.ToString());
        }
        #endregion

        #region Invoke Delayed
        protected void InvokeDelayed(MethodInvoker method)
        {
            InvokeDelayed(method, 10);
        }
        protected void InvokeDelayed(MethodInvoker method, int delayInterval)
        {
            if (delayInterval <= 0) { method.Invoke(); return; }

            Timer delayedInvokeTimer = new Timer();
            delayedInvokeTimer = new Timer();
            delayedInvokeTimer.Tag = method;
            delayedInvokeTimer.Interval = delayInterval;
            delayedInvokeTimer.Tick += new EventHandler(DelayedInvokeTimerTick);
            delayedInvokeTimer.Start();
        }
        void DelayedInvokeTimerTick(object sender, EventArgs e)
        {
            Timer timer = (Timer)sender;
            MethodInvoker method = (MethodInvoker)timer.Tag;
            timer.Stop();
            timer.Dispose();
            if (!this.IsDisposed)
                method.Invoke();
        }
        #endregion
    }

    public interface IScrollBarOverrideSupport
    {
        /// <summary>
        /// Should be fired when non-client size of the control changes, i.e. when WM_NCCALCSIZE message is received.
        /// </summary>
        event EventHandler NonClientSizeChanged;
        /// <summary>
        /// Should be fired when scroll-bar value on child control changes.
        /// </summary>
        event ScrollValueChangedHandler ScrollBarValueChanged;
    }
    /// <summary>
    /// Defines arguments for IScrollBarOverrideSupport.ScrollBarValueChanged event.
    /// </summary>
    public class ScrollValueChangedEventArgs : System.EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the ScrollValueChangedEventArgs class.
        /// </summary>
        /// <param name="scrollChange"></param>
        public ScrollValueChangedEventArgs(eScrollBarScrollChange scrollChange)
        {
            ScrollChange = scrollChange;
        }

        public readonly eScrollBarScrollChange ScrollChange;
    }
    /// <summary>
    /// Defines information for IScrollBarOverrideSupport.ScrollBarValueChanged event.
    /// </summary>
    [Flags()]
    public enum eScrollBarScrollChange
    {
        Vertical = 1,
        Horizontal = 2,
        MouseWheel = 4
    }
    /// <summary>
    /// Defines delegate for IScrollBarOverrideSupport.ScrollBarValueChanged event.
    /// </summary>
    /// <param name="sender">Sender.</param>
    /// <param name="e">Event arguments</param>
    public delegate void ScrollValueChangedHandler(object sender, ScrollValueChangedEventArgs e);
}
