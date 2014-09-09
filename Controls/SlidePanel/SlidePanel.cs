using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.Controls
{
    /// <summary>
    /// Represents the panel which can slide out and into the view.
    /// </summary>
    [ToolboxBitmap(typeof(CircularProgress), "Controls.SlidePanel.ico"), ToolboxItem(true), 
    Designer(typeof(DevComponents.DotNetBar.Design.SlidePanelDesigner))]
    public class SlidePanel : UserControl
    {
        internal const int DefaultAnimationTime = 500;
        #region Events
        /// <summary>
        /// Occurs when IsOpen property value has changed, i.e. slide-panel is shown or hidden.
        /// </summary>
        [Description("Occurs when IsOpen property value has changed, i.e. slide-panel is shown or hidden.")]
        public event EventHandler IsOpenChanged;
        /// <summary>
        /// Raises IsOpenChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnIsOpenChanged(EventArgs e)
        {
            EventHandler handler = IsOpenChanged;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        protected override void Dispose(bool disposing)
        {
            DisposeSlideOutButton();
            base.Dispose(disposing);
        }
        #endregion

        #region Implementation

        private eSlideSide _SlideSide = eSlideSide.Left;
        /// <summary>
        /// Gets or sets side panel slides into.
        /// </summary>
        [DefaultValue(eSlideSide.Left), Category("Behavior"), Description("Indicates side panel slides into.")]
        public eSlideSide SlideSide
        {
            get { return _SlideSide; }
            set
            {
                if (value != _SlideSide)
                {
                    eSlideSide oldValue = _SlideSide;
                    _SlideSide = value;
                    OnSlideSideChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SlideSide property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSlideSideChanged(eSlideSide oldValue, eSlideSide newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SlideSide"));
            if (!this.IsOpen)
            {
                WaitForCurrentAnimationFinish();
                this.Bounds = GetSlideOutBounds(this.Parent, _OpenBounds);

                if (_SlideOutButtonVisible)
                    CreateSlideOutButton();
            }
        }

        private bool _IsOpen = true;
        /// <summary>
        /// Gets or sets whether panel is open. When this property is changed panel will slide in or out of the view.
        /// </summary>
        [Browsable(false), DefaultValue(true), Description("Indicates whether panel is open. When this property is changed panel will slide in or out of the view.")]
        public bool IsOpen
        {
            get { return _IsOpen; }
            set
            {
                if (_UsesBlockingAnimation && _CurrentAnimation != null || _IsWaiting) return;
                if (value != _IsOpen)
                {
                    bool oldValue = _IsOpen;
                    _IsOpen = value;
                    OnIsOpenChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when IsOpen property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnIsOpenChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("IsOpen"));
            if (!newValue)
                SlideOutOfView();
            else
                SlideIntoView();
            OnIsOpenChanged(EventArgs.Empty);
        }

        private bool _UsesBlockingAnimation = false;
        /// <summary>
        /// Gets or sets whether panel uses modal animation, meaning when IsOpen property is set the call is not returned until animation is complete.
        /// </summary>
        [Browsable(false)]
        public bool UsesBlockingAnimation
        {
            get { return _UsesBlockingAnimation; }
            set
            {
                _UsesBlockingAnimation = value;
            }
        }

        private Rectangle _OpenBounds = Rectangle.Empty;
        /// <summary>
        /// Gets or sets the open panel bounds. When control IsOpen=false and panel is collapsed its original bounds are stored in OpenBounds property and restored once panel is open.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle OpenBounds
        {
            get { return _OpenBounds; }
            set
            {
                _OpenBounds = value;
                if (!this.Visible && this.Bounds.Size != _OpenBounds.Size)
                    this.Bounds = GetSlideOutBounds(this.Parent, _OpenBounds);
            }
        }
        /// <summary>
        /// Slides panel into the view.
        /// </summary>
        private void SlideIntoView()
        {
            Animation.Animation current = _CurrentAnimation;
            if (current != null)
            {
                current.Stop();
                WaitForCurrentAnimationFinish();
            }

            Rectangle bounds = this.Bounds;
            Rectangle targetBounds = _OpenBounds;
            if (_SlideOutButton != null) _SlideOutButton.Visible = false;

            if (_AnimationTime > 0)
            {
                _IsAnimating = true;
                //BarFunctions.AnimateControl(this, true, _AnimationTime, bounds, targetBounds);
                this.Visible = true;
                Animation.AnimationRectangle anim = new DevComponents.DotNetBar.Animation.AnimationRectangle(
                   new Animation.AnimationRequest(this, "Bounds", bounds, targetBounds),
                   Animation.AnimationEasing.EaseOutExpo, _AnimationTime);
                anim.AnimationCompleted += new EventHandler(AnimationCompleted);
                anim.Start();
                _CurrentAnimation = anim;
            }
            else
                this.Bounds = targetBounds;
            if (_SlideOutButtonVisible && _SlideOutButton != null)
            {
                DisposeSlideOutButton();
            }
        }

        internal void AbortAnimation()
        {
            Animation.Animation anim = _CurrentAnimation;
            _CurrentAnimation = null;
            if (anim != null)
            {
                anim.Stop();
                anim.Dispose();
                //Console.WriteLine("{0} Animation Forcefully Stopped", DateTime.Now);
            }
        }

        private bool _IsWaiting = false;
        /// <summary>
        /// Waits for current slide animation to finish and then returns control.
        /// </summary>
        public void WaitForCurrentAnimationFinish()
        {
            if (_IsWaiting) return;

            _IsWaiting = true;
            try
            {
                if (_CurrentAnimation == null) return;
                DateTime start = DateTime.Now;
                while (_CurrentAnimation != null)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(start).TotalMilliseconds > _AnimationTime * 1.5f)
                    {
                        AbortAnimation();
                        break;
                    }
                }
            }
            finally
            {
                _IsWaiting = false;
            }
        }
        private void DisposeSlideOutButton()
        {
            if (_SlideOutButton == null) return;

            Control parent = _SlideOutButton.Parent;
            if (parent != null)
            {
                parent.Resize -= ParentResize;
                parent.Controls.Remove(_SlideOutButton);
            }
            _SlideOutButton.Dispose();
            _SlideOutButton = null;
        }

        private Animation.Animation _CurrentAnimation = null;
        /// <summary>
        /// Slides panel out of the view.
        /// </summary>
        private void SlideOutOfView()
        {
            Animation.Animation current = _CurrentAnimation;
            if (current != null)
            {
                current.Stop();
                WaitForCurrentAnimationFinish();
            }
            
            Rectangle bounds = this.Bounds;
            Rectangle targetBounds = GetSlideOutBounds();

            if (_AnimationTime > 0 && this.Visible)
            {
                _IsAnimating = true;
                //BarFunctions.AnimateControl(this, true, _AnimationTime, bounds, targetBounds);
                Animation.AnimationRectangle anim = new DevComponents.DotNetBar.Animation.AnimationRectangle(
                    new Animation.AnimationRequest(this, "Bounds", bounds, targetBounds),
                    Animation.AnimationEasing.EaseOutExpo, _AnimationTime);
                anim.AnimationCompleted += new EventHandler(AnimationCompleted);
                anim.Start();
                _CurrentAnimation = anim;
                //this.Visible = false;
            }
            else
                this.Bounds = targetBounds;
            _OpenBounds = bounds;

            if (_SlideOutButtonVisible)
                CreateSlideOutButton();
        }

        private bool _IsAnimating = false;
        internal bool IsAnimating
        {
            get
            {
                return _IsAnimating;
            }
        }

        private void AnimationCompleted(object sender, EventArgs e)
        {
            //Console.WriteLine("{0} Animation Completed", DateTime.Now);
            Animation.Animation anim = _CurrentAnimation;
            _CurrentAnimation = null;
            if (anim != null)
                anim.Dispose();
            if (!_IsOpen)
            {
                this.Visible = false;
            }
            _IsAnimating = false;
        }
        internal void SlideOutOfViewSilent(Control parent)
        {
            _OpenBounds = this.Bounds;
            this.Bounds = GetSlideOutBounds(parent);
            _IsOpen = false;
        }

        private SliderButton _SlideOutButton = null;
        private void CreateSlideOutButton()
        {
            DisposeSlideOutButton();
            Control parent = this.Parent;
            if (parent == null) return;

            _SlideOutButton = new SliderButton(this);
            if (_SlideOutButtonStyle != null)
                _SlideOutButton.Style = _SlideOutButtonStyle;
            else
                _SlideOutButton.Style = new ElementStyle(DevComponents.DotNetBar.Rendering.ElementStyleClassKeys.SlideOutButtonKey);

            Size activeSlideOutSize = _SlideOutActiveButtonSize;
            Size slideOutSize = _SlideOutButtonSize;
            if (_SlideSide == eSlideSide.Top || _SlideSide == eSlideSide.Bottom) // Flip width/height
            {
                activeSlideOutSize = new Size(activeSlideOutSize.Height, activeSlideOutSize.Width);
                slideOutSize = new Size(slideOutSize.Height, slideOutSize.Width);
            }
            _SlideOutButton.ActiveSliderSize = activeSlideOutSize;
            _SlideOutButton.SliderSize = slideOutSize;
            parent.Controls.Add(_SlideOutButton);
            parent.Controls.SetChildIndex(_SlideOutButton, 0);
            parent.Resize += ParentResize;
        }

        private void ParentResize(object sender, EventArgs e)
        {
            if (_SlideOutButton != null) _SlideOutButton.UpdatePosition();
        }

        private Rectangle GetSlideOutBounds()
        {
            return GetSlideOutBounds(this.Parent);
        }
        private Rectangle GetSlideOutBounds(Control parent)
        {
            return GetSlideOutBounds(parent, this.Bounds);
        }
        private Rectangle GetSlideOutBounds(Control parent, Rectangle panelBounds)
        {
            if (parent == null) return panelBounds;

            Rectangle r = panelBounds;
            if (_SlideSide == eSlideSide.Left)
            {
                r.X = -r.Width;
            }
            else if (_SlideSide == eSlideSide.Right)
            {
                r.X = parent.Width;
            }
            else if (_SlideSide == eSlideSide.Top)
            {
                r.Y = -r.Height;
            }
            else if (_SlideSide == eSlideSide.Bottom)
            {
                r.Y = parent.Height;
            }
            return r;
        }

        private int _AnimationTime = DefaultAnimationTime;
        /// <summary>
        /// Gets or sets the animation duration time in milliseconds. Setting this property to 0 will disable slide animation.
        /// </summary>
        [DefaultValue(DefaultAnimationTime), Category("Behavior"), Description("Indicates animation duration time in milliseconds. Setting this property to 0 will disable slide animation.")]
        public int AnimationTime
        {
            get { return _AnimationTime; }
            set
            {
                if (value != _AnimationTime)
                {
                    int oldValue = _AnimationTime;
                    _AnimationTime = value;
                    OnAnimationTimeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when AnimationTime property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnAnimationTimeChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("AnimationTime"));

        }

        private bool _SlideOutButtonVisible = true;
        /// <summary>
        /// Gets or sets whether slide out button is shown when panel is out of the view and which allows panel to be shown.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether slide out button is shown when panel is out of the view and which allows panel to be shown.")]
        public bool SlideOutButtonVisible
        {
            get { return _SlideOutButtonVisible; }
            set
            {
                if (value != _SlideOutButtonVisible)
                {
                    bool oldValue = _SlideOutButtonVisible;
                    _SlideOutButtonVisible = value;
                    OnSlideOutButtonVisibleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SlideOutButtonVisible property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSlideOutButtonVisibleChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SlideOutButtonVisible"));
            if (_IsOpen) return;
            if (newValue)
                CreateSlideOutButton();
            else
                DisposeSlideOutButton();
        }

        private Size _SlideOutButtonSize = SliderButton.DefaultSliderSize;
        /// <summary>
        /// Gets or sets the slide-out buttton size in default state.
        /// </summary>
        [Category("Appearance"), Description("Indicates slide-out buttton size in default state.")]
        public Size SlideOutButtonSize
        {
            get { return _SlideOutButtonSize; }
            set
            {
                if (value != _SlideOutButtonSize)
                {
                    Size oldValue = _SlideOutButtonSize;
                    _SlideOutButtonSize = value;
                    OnSlideOutButtonSizeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SlideOutButtonSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSlideOutButtonSizeChanged(Size oldValue, Size newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SlideOutButtonSize"));
            if (_SlideOutButton != null)
            {
                _SlideOutButton.SliderSize = newValue;
                _SlideOutButton.UpdatePosition();
            }
        }
        /// <summary>
        /// Returns whether property should be serialized.
        /// </summary>
        public bool ShouldSerializeSlideOutButtonSize()
        {
            return _SlideOutButtonSize != SliderButton.DefaultSliderSize;
        }
        /// <summary>
        /// Resets property to default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetSlideOutButtonSize()
        {
            SlideOutButtonSize = SliderButton.DefaultSliderSize;
        }

        private Size _SlideOutActiveButtonSize = SliderButton.DefaultActiveSliderSize;
        /// <summary>
        /// Gets or sets active (mouse over) slide out button size.
        /// </summary>
        [Category("Appearance"), Description("Indicates active (mouse over) slide out button size.")]
        public Size SlideOutActiveButtonSize
        {
            get { return _SlideOutActiveButtonSize; }
            set
            {
                if (value != _SlideOutActiveButtonSize)
                {
                    Size oldValue = _SlideOutActiveButtonSize;
                    _SlideOutActiveButtonSize = value;
                    OnSlideOutActiveButtonSizeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SlideOutActiveButtonSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSlideOutActiveButtonSizeChanged(Size oldValue, Size newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SlideOutActiveButtonSize"));
            if (_SlideOutButton != null)
            {
                _SlideOutButton.ActiveSliderSize = newValue;
                _SlideOutButton.UpdatePosition();
            }
        }
        /// <summary>
        /// Returns whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSlideOutActiveButtonSize()
        {
            return _SlideOutActiveButtonSize != SliderButton.DefaultActiveSliderSize;
        }
        /// <summary>
        /// Resets property to default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetSlideOutActiveButtonSize()
        {
            SlideOutActiveButtonSize = SliderButton.DefaultActiveSliderSize;
        }

        private ElementStyle _SlideOutButtonStyle = null;
        /// <summary>
        /// Gets or sets slide-out button style.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Indicates slide-out button style.")]
        public ElementStyle SlideOutButtonStyle
        {
            get { return _SlideOutButtonStyle; }
            set
            {
                if (value != _SlideOutButtonStyle)
                {
                    ElementStyle oldValue = _SlideOutButtonStyle;
                    _SlideOutButtonStyle = value;
                    OnSlideOutButtonStyleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SlideOutButtonStyle property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSlideOutButtonStyleChanged(ElementStyle oldValue, ElementStyle newValue)
        {
            if (newValue != null)
            {
                if (_SlideOutButton != null)
                    _SlideOutButton.Style = newValue;
            }
            //OnPropertyChanged(new PropertyChangedEventArgs("SlideOutButtonStyle"));
        }

        private bool _CenterContent = false;
        /// <summary>
        /// Gets or sets whether panel centers the Controls inside of it. Default value is false.
        /// </summary>
        internal bool CenterContent
        {
            get { return _CenterContent; }
            set
            {
                if (value != _CenterContent)
                {
                    bool oldValue = _CenterContent;
                    _CenterContent = value;
                    OnCenterContentChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when CenterContent property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnCenterContentChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("CenterContent"));
            if (newValue)
                CenterControls();
        }

        protected override void OnResize(EventArgs e)
        {
            if (_CenterContent)
                CenterControls();
            base.OnResize(e);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            if (_CenterContent)
                CenterControl(e.Control);
            base.OnControlAdded(e);
        }

        private void CenterControl(Control item)
        {
            Point loc = new Point(Math.Max(0, (this.Width - item.Width) / 2), Math.Max(0, (this.Height - item.Height) / 2));
            if (item.Location != loc)
                item.Location = loc;
        }
        private void CenterControls()
        {
            foreach (Control item in this.Controls)
            {
                CenterControl(item);
            }
        }

        [Browsable(true), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible), EditorBrowsable(EditorBrowsableState.Always), Bindable(true)]
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
        #endregion
    }

    /// <summary>
    /// Defines the side SlidePanel slides into.
    /// </summary>
    public enum eSlideSide
    {
        Left,
        Right,
        Top,
        Bottom
    }
}
