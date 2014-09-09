using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Data;

namespace DevComponents.DotNetBar.Touch
{
    internal class TouchHandler
    {
        #region Events
        public event EventHandler<GestureEventArgs> PanBegin;
        /// <summary>
        /// Raises PanBegin event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnPanBegin(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = PanBegin;
            if (handler != null)
                handler(this, e);
        }
        public event EventHandler<GestureEventArgs> PanEnd;
        /// <summary>
        /// Raises PanBegin event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnPanEnd(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = PanEnd;
            if (handler != null)
                handler(this, e);
        }
        public event EventHandler<GestureEventArgs> Pan;
        /// <summary>
        /// Raises PanBegin event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnPan(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = Pan;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> Begin;
        /// <summary>
        /// Raises Begin event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnBegin(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = Begin;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> End;
        /// <summary>
        /// Raises End event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnEnd(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = End;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> PressAndTap;
        /// <summary>
        /// Raises PressAndTap event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnPressAndTap(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = PressAndTap;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> RotateBegin;
        /// <summary>
        /// Raises RotateBegin event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnRotateBegin(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = RotateBegin;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> Rotate;
        /// <summary>
        /// Raises Rotate event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnRotate(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = Rotate;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> RotateEnd;
        /// <summary>
        /// Raises RotateEnd event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnRotateEnd(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = RotateEnd;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> TwoFingerTap;
        /// <summary>
        /// Raises TwoFingerTap event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnTwoFingerTap(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = TwoFingerTap;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> ZoomBegin;
        /// <summary>
        /// Raises ZoomBegin event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnZoomBegin(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = ZoomBegin;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> Zoom;
        /// <summary>
        /// Raises Zoom event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnZoom(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = Zoom;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<GestureEventArgs> ZoomEnd;
        /// <summary>
        /// Raises ZoomEnd event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnZoomEnd(GestureEventArgs e)
        {
            EventHandler<GestureEventArgs> handler = ZoomEnd;
            if (handler != null)
                handler(this, e);
        }

        // Touch events
        public event EventHandler<TouchEventArgs> TouchDown;
        /// <summary>
        /// Raises TouchDown event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnTouchDown(TouchEventArgs e)
        {
            EventHandler<TouchEventArgs> handler = TouchDown;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<TouchEventArgs> TouchUp;
        /// <summary>
        /// Raises TouchDown event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnTouchUp(TouchEventArgs e)
        {
            EventHandler<TouchEventArgs> handler = TouchUp;
            if (handler != null)
                handler(this, e);
        }

        public event EventHandler<TouchEventArgs> TouchMove;
        /// <summary>
        /// Raises TouchDown event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnTouchMove(TouchEventArgs e)
        {
            EventHandler<TouchEventArgs> handler = TouchMove;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        private Control _ParentControl = null;
        private eTouchHandlerType _HandlerType = eTouchHandlerType.Gesture;
        /// <summary>
        /// Initializes a new instance of the TouchHandler class.
        /// </summary>
        /// <param name="parentControl"></param>
        public TouchHandler(Control parentControl) : this(parentControl, eTouchHandlerType.Gesture) { }

        /// <summary>
        /// Initializes a new instance of the TouchHandler class.
        /// </summary>
        /// <param name="parentControl"></param>
        public TouchHandler(Control parentControl, eTouchHandlerType handlerType)
        {
            _ParentControl = parentControl;
            _HandlerType = handlerType;
            if (IsTouchEnabled)
            {
                if (_ParentControl.IsHandleCreated)
                    Initialize();
                else
                    _ParentControl.HandleCreated += new EventHandler(ParentControlHandleCreated);
            }
        }
        #endregion

        #region Implementation
        void ParentControlHandleCreated(object sender, EventArgs e)
        {
            Initialize();
        }

        private IntPtr _originalWindowProcId;
        private WinApi.WindowProcDelegate _windowProcDelegate;
        /// <summary>
        /// Initializes handler
        /// </summary>
        private void Initialize()
        {
            if (!RegisterTouchWindow())
            {
                throw new NotSupportedException("Cannot register window");
            }

            _windowProcDelegate = WindowProc;

            _originalWindowProcId = IntPtr.Size == 4 ?
                WinApi.SubclassWindow(_ParentControl.Handle, WinApi.GWLP_WNDPROC, _windowProcDelegate) :
                WinApi.SubclassWindow64(_ParentControl.Handle, WinApi.GWLP_WNDPROC, _windowProcDelegate);

            //take the desktop DPI
            using (Graphics graphics = Graphics.FromHwnd(_ParentControl.Handle))
            {
                DpiX = graphics.DpiX;
                DpiY = graphics.DpiY;
            }
        }


        public Control ParentControl
        {
            get { return _ParentControl; }
        }

        /// <summary>
        /// The Windows message handler.
        /// </summary>
        private uint WindowProc(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam)
        {
            if(msg == WinApi.WM_TOUCH && (_HandlerType & eTouchHandlerType.Touch)== eTouchHandlerType.Touch)
            {
                foreach (TouchEventArgs arg in DecodeMessage(hWnd, msg, wParam, lParam, DpiX, DpiY))
                {

                    if (arg.IsTouchDown)
                        OnTouchDown(arg);
                
                    if (arg.IsTouchMove)
                        OnTouchMove(arg);
                
                    if (arg.IsTouchUp)
                        OnTouchUp(arg);
                }
                return 1;
            }

            // Handle only gesture message
            if (msg != WinApi.WM_GESTURE)
            {
                return WinApi.CallWindowProc(_originalWindowProcId, hWnd, msg, wParam, lParam);
            }

            WinApi.GESTUREINFO gestureInfo = new WinApi.GESTUREINFO();
            gestureInfo.cbSize = (uint)Marshal.SizeOf(typeof(WinApi.GESTUREINFO));

            bool result = WinApi.GetGestureInfo(lParam, ref gestureInfo);

            if (!result)
                throw new Exception("Cannot retrieve gesture information");

            //Decode the gesture info and get the message event argument
            GestureEventArgs eventArgs = new GestureEventArgs(this, ref gestureInfo);
            try
            {
                //Fire the event using the event map
                uint gestureId = GetGestureEventId(gestureInfo.dwID, gestureInfo.dwFlags);
                if (gestureId == GestureEventId.Begin)
                    OnBegin(eventArgs);
                else if (gestureId == GestureEventId.End)
                    OnEnd(eventArgs);
                else if (gestureId == GestureEventId.Pan)
                    OnPan(eventArgs);
                else if (gestureId == GestureEventId.PanBegin)
                    OnPanBegin(eventArgs);
                else if (gestureId == GestureEventId.PanEnd)
                    OnPanEnd(eventArgs);
                else if (gestureId == GestureEventId.PressAndTap)
                    OnPressAndTap(eventArgs);
                else if (gestureId == GestureEventId.Rotate)
                    OnRotate(eventArgs);
                else if (gestureId == GestureEventId.RotateBegin)
                    OnRotateBegin(eventArgs);
                else if (gestureId == GestureEventId.RotateEnd)
                    OnRotateEnd(eventArgs);
                else if (gestureId == GestureEventId.TwoFingerTap)
                    OnTwoFingerTap(eventArgs);
                else if (gestureId == GestureEventId.Zoom)
                    OnZoom(eventArgs);
                else if (gestureId == GestureEventId.ZoomBegin)
                    OnZoomBegin(eventArgs);
                else if (gestureId == GestureEventId.ZoomEnd)
                    OnZoomEnd(eventArgs);
            }
            catch (ArgumentOutOfRangeException) //In case future releases will introduce new event values
            {
            }

            //Keep the last message for relative calculations
            LastEventArgs = eventArgs;

            //Keep the first message for relative calculations
            if (eventArgs.IsBegin)
                LastBeginEventArgs = eventArgs;

            WinApi.CloseGestureInfoHandle(lParam);

            return 1;
        }

        /// <summary>
        /// Decode the message and create a collection of event arguments
        /// </summary>
        private IEnumerable<TouchEventArgs> DecodeMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam, float dpiX, float dpiY)
        {
            // More than one touch input can be associated with a touch message
            int inputCount = WinApi.LoWord(wParam.ToInt32()); // Number of touch inputs, actual per-contact messages

            WinApi.TOUCHINPUT[] inputs; // Array of TOUCHINPUT structures
            inputs = new WinApi.TOUCHINPUT[inputCount]; // Allocate the storage for the parameters of the per-contact messages
            try
            {
                // Unpack message parameters into the array of TOUCHINPUT structures, each representing a message for one single contact.
                if (!WinApi.GetTouchInputInfo(lParam, inputCount, inputs, Marshal.SizeOf(inputs[0])))
                {
                    // Touch info failed.
                    throw new Exception("Error calling GetTouchInputInfo API");
                }

                // For each contact, dispatch the message to the appropriate message handler.
                // For WM_TOUCHDOWN you can get down & move notifications and for WM_TOUCHUP you can get up & move notifications
                // WM_TOUCHMOVE will only contain move notifications and up & down notifications will never come in the same message
                for (int i = 0; i < inputCount; i++)
                {
                    TouchEventArgs touchEventArgs = new TouchEventArgs(this, dpiX, dpiY, ref inputs[i]);
                    yield return touchEventArgs;
                }
            }
            finally
            {
                WinApi.CloseTouchInputHandle(lParam);
            }
        }

        private GestureEventArgs _LastBeginEventArgs;
        /// <summary>
        /// The event arguments that started the current gesture
        /// </summary>
        internal GestureEventArgs LastBeginEventArgs
        {
            get { return _LastBeginEventArgs; }
            set { _LastBeginEventArgs = value; }
        }
        /// <summary>
        /// The last event in the current gesture event sequence
        /// </summary>
        private GestureEventArgs _LastEventArgs;
        internal GestureEventArgs LastEventArgs
        {
            get { return _LastEventArgs; }
            set { _LastEventArgs = value; }
        }


        private static class GestureEventId
        {
            public static readonly uint Begin = GetGestureEventId(WinApi.GID_BEGIN, 0);
            public static readonly uint End = GetGestureEventId(WinApi.GID_END, 0);
            public static readonly uint PanBegin = GetGestureEventId(WinApi.GID_PAN, WinApi.GF_BEGIN);
            public static readonly uint Pan = GetGestureEventId(WinApi.GID_PAN, 0);
            public static readonly uint PanEnd = GetGestureEventId(WinApi.GID_PAN, WinApi.GF_END);
            public static readonly uint PressAndTap = GetGestureEventId(WinApi.GID_PRESSANDTAP, 0);
            public static readonly uint RotateBegin = GetGestureEventId(WinApi.GID_ROTATE, WinApi.GF_BEGIN);
            public static readonly uint Rotate = GetGestureEventId(WinApi.GID_ROTATE, 0);
            public static readonly uint RotateEnd = GetGestureEventId(WinApi.GID_ROTATE, WinApi.GF_END);
            public static readonly uint TwoFingerTap = GetGestureEventId(WinApi.GID_TWOFINGERTAP, 0);
            public static readonly uint ZoomBegin = GetGestureEventId(WinApi.GID_ZOOM, WinApi.GF_BEGIN);
            public static readonly uint Zoom = GetGestureEventId(WinApi.GID_ZOOM, 0);
            public static readonly uint ZoomEnd = GetGestureEventId(WinApi.GID_ZOOM, WinApi.GF_END);
        }
        private static uint GetGestureEventId(uint dwID, uint dwFlags)
        {
            return (dwID << 3) + (dwID == WinApi.GID_TWOFINGERTAP || dwID == WinApi.GID_PRESSANDTAP
                || dwID == WinApi.GID_BEGIN || dwID == WinApi.GID_END ?
                0 : dwFlags & 5);
        }

        /// <summary>
        /// Register for touch event
        /// </summary>
        /// <returns>true if succeeded</returns>
        private bool RegisterTouchWindow()
        {
            bool result = false;
            if ((_HandlerType & eTouchHandlerType.Gesture) == eTouchHandlerType.Gesture)
            {
                WinApi.GESTURECONFIG[] gestureConfig = new WinApi.GESTURECONFIG[] { new WinApi.GESTURECONFIG(0, WinApi.GC_ALLGESTURES, 0) };
                result = WinApi.SetGestureConfig(_ParentControl.Handle, 0, 1, gestureConfig, (uint)Marshal.SizeOf(typeof(WinApi.GESTURECONFIG)));
            }

            if ((_HandlerType & eTouchHandlerType.Touch) == eTouchHandlerType.Touch)
            {
                result |= WinApi.RegisterTouchWindow(_ParentControl.Handle, _DisablePalmRejection ? WinApi.TouchWindowFlag.WantPalm : 0);
            }
            return result;
        }

        private bool _DisablePalmRejection;
        /// <summary>
        /// Gets or sets whether palm rejection is enabled.
        /// </summary>
        public bool DisablePalmRejection
        {
            get
            {
                return _DisablePalmRejection;
            }
            set
            {
                if (_DisablePalmRejection == value)
                    return;

                _DisablePalmRejection = value;

                if (_ParentControl.IsHandleCreated)
                {
                    WinApi.UnregisterTouchWindow(_ParentControl.Handle);
                    RegisterTouchWindow();
                }
            }
        }

        private float _DpiX;
        public float DpiX
        {
            get { return _DpiX; }
            set { _DpiX = value; }
        }

        private float _DpiY;
        public float DpiY
        {
            get { return _DpiY; }
            set { _DpiY = value; }
        }

        /// <summary>
        /// Check if Multi-touch support device is ready
        /// </summary>
        public static bool IsTouchEnabled
        {
            get
            {
                return (WinApi.GetDigitizerStatus() & (WinApi.DigitizerStatus.StackReady | WinApi.DigitizerStatus.MultiInput)) != 0;
            }
        }
        #endregion
    }
    [Flags]
    internal enum eTouchHandlerType : short
    {
        Gesture = 1,
        Touch = 2
    }

    internal class GestureEventArgs : EventArgs
    {
        private readonly uint _Flags;

        /// <summary>
        /// Create new gesture event instance and decode the gesture info structure
        /// </summary>
        /// <param name="handler">The gesture handler</param>
        /// <param name="gestureInfo">The gesture information</param>
        internal GestureEventArgs(TouchHandler handler, ref WinApi.GESTUREINFO gestureInfo)
        {
            _Flags = gestureInfo.dwFlags;
            GestureId = gestureInfo.dwID;
            GestureArguments = gestureInfo.ullArguments;

            //Get the last event from the handler
            LastEvent = handler.LastEventArgs;

            //Get the last begin event from the handler 
            LastBeginEvent = handler.LastBeginEventArgs;

            ParseGesture(handler.ParentControl, ref gestureInfo);

            //new gesture, clear last and first event fields
            if (IsBegin)
            {
                LastBeginEvent = null;
                LastEvent = null;
            }
        }

        //Decode the gesture
        private void ParseGesture(Control parentControl, ref WinApi.GESTUREINFO gestureInfo)
        {
            Location = parentControl.PointToClient(new Point(gestureInfo.ptsLocation.x, gestureInfo.ptsLocation.y));

            Center = Location;

            switch (GestureId)
            {
                case WinApi.GID_ROTATE:
                    ushort lastArguments = (ushort)(IsBegin ? 0 : LastEvent.GestureArguments);

                    RotateAngle = WinApi.GID_ROTATE_ANGLE_FROM_ARGUMENT((ushort)(gestureInfo.ullArguments - lastArguments));
                    break;


                case WinApi.GID_ZOOM:
                    Point first = IsBegin ? Location : LastBeginEvent.Location;
                    Center = new Point((Location.X + first.X) / 2, (Location.Y + first.Y) / 2);
                    ZoomFactor = IsBegin ? 1 : (double)gestureInfo.ullArguments / LastEvent.GestureArguments;
                    //DistanceBetweenFingers = WinApi.LoDWord(gestureInfo.ullArguments);
                    break;

                case WinApi.GID_PAN:
                    PanTranslation = IsBegin ? new Size(0, 0) :
                        new Size(Location.X - LastEvent.Location.X, Location.Y - LastEvent.Location.Y);
                    int panVelocity = WinApi.HiDWord((long)(gestureInfo.ullArguments));
                    PanVelocity = new Size(WinApi.LoWord(panVelocity), WinApi.HiWord(panVelocity));
                    //DistanceBetweenFingers = WinApi.LoDWord(gestureInfo.ullArguments);
                    break;
            }

            
        }

        private uint _GestureId;
        public uint GestureId
        {
            get { return _GestureId; }
            private set { _GestureId = value; }
        }

        private ulong _GestureArguments;
        public ulong GestureArguments
        {
            get { return _GestureArguments; }
            private set { _GestureArguments = value; }
        }

        private Point _Location;
        /// <summary>
        /// The client location of gesture.
        /// </summary>
        public Point Location
        {
            get { return _Location; }
            private set { _Location = value; }
        }

        /// <summary>
        /// Is this the first event of a gesture.
        /// </summary>
        public bool IsBegin
        {
            get
            {
                return (_Flags & WinApi.GF_BEGIN) != 0;
            }
        }

        /// <summary>
        /// It this last event of a gesture.
        /// </summary>
        public bool IsEnd
        {
            get
            {
                return (_Flags & WinApi.GF_END) != 0;
            }
        }

        /// <summary>
        /// Has gesture triggered inertia.
        /// </summary>
        public bool IsInertia
        {
            get
            {
                return (_Flags & WinApi.GF_INERTIA) != 0;
            }
        }

        /// <summary>
        /// Gesture relative rotation angle for Rotate event.
        /// </summary>
        private double _RotateAngle;
        public double RotateAngle
        {
            get { return _RotateAngle; }
            private set { _RotateAngle = value; }
        }

        /// <summary>
        /// Indicates calculated gesture center.
        /// </summary>
        private Point _Center;
        public Point Center
        {
            get { return _Center; }
            private set { _Center = value; }
        }
        
        /// <summary>
        /// Gesture zoom factor for Zoom event.
        /// </summary>
        private double _ZoomFactor;
        public double ZoomFactor
        {
            get { return _ZoomFactor; }
            private set { _ZoomFactor = value; }
        }

        /// <summary>
        /// Gesture relative panning translation for Pan event.
        /// </summary>
        private Size _PanTranslation;
        public Size PanTranslation
        {
            get { return _PanTranslation; }
            set { _PanTranslation = value; }
        }

        /// <summary>
        /// Gesture velocity vector of the pan gesture for custom inertia implementations.
        /// </summary>
        private Size _PanVelocity;
        public Size PanVelocity
        {
            get { return _PanVelocity; }
            private set { _PanVelocity = value; }
        }

        private GestureEventArgs _LastBeginEvent;
        /// <summary>
        /// The first touch arguments in this gesture event sequence.
        /// </summary>
        public GestureEventArgs LastBeginEvent
        {
            get { return _LastBeginEvent; }
            internal set { _LastBeginEvent = value; }
        }
        
        /// <summary>
        /// The last touch arguments in this gesture event sequence.
        /// </summary>
        private GestureEventArgs _LastEvent;
        public GestureEventArgs LastEvent
        {
            get { return _LastEvent; }
            internal set { _LastEvent = value; }
        }
    }

    /// <summary>
    /// EventArgs passed to Touch handlers 
    /// </summary>
    internal class TouchEventArgs : EventArgs
    {
        private readonly TouchHandler _ParentHandler;
        private readonly float _dpiXFactor;
        private readonly float _dpiYFactor;

        /// <summary>
        /// Create new touch event argument instance
        /// </summary>
        /// <param name="hWndWrapper">The target control</param>
        /// <param name="touchInput">one of the inner touch input in the message</param>
        internal TouchEventArgs(TouchHandler parentHandler, float dpiX, float dpiY, ref WinApi.TOUCHINPUT touchInput)
        {
            _ParentHandler = parentHandler;
            _dpiXFactor = 96F / dpiX;
            _dpiYFactor = 96F / dpiY;
            DecodeTouch(ref touchInput);
        }

        private bool CheckFlag(int value)
        {
            return (Flags & value) != 0;
        }



        // Decodes and handles WM_TOUCH* messages.
        private void DecodeTouch(ref WinApi.TOUCHINPUT touchInput)
        {
            // TOUCHINFO point coordinates and contact size is in 1/100 of a pixel; convert it to pixels.
            // Also convert screen to client coordinates.
            if ((touchInput.dwMask & WinApi.TOUCHINPUTMASKF_CONTACTAREA) != 0)
                ContactSize = new Size(AdjustDpiX(touchInput.cyContact / 100), AdjustDpiY(touchInput.cyContact / 100));

            Id = touchInput.dwID;

            Point p = _ParentHandler.ParentControl.PointToClient(new Point(touchInput.x / 100, touchInput.y / 100));
            Location = p; // new Point(AdjustDpiX(p.X), AdjustDpiY(p.Y));

            Time = touchInput.dwTime;
            TimeSpan ellapse = TimeSpan.FromMilliseconds(Environment.TickCount - touchInput.dwTime);
            AbsoluteTime = DateTime.Now - ellapse;

            Mask = touchInput.dwMask;
            Flags = touchInput.dwFlags;
        }


        private int AdjustDpiX(int value)
        {
            return (int)(value * _dpiXFactor);
        }

        private int AdjustDpiY(int value)
        {
            return (int)(value * _dpiYFactor);
        }

        /// <summary>
        /// Touch client coordinate in pixels
        /// </summary>
        private Point _Location;
        public Point Location
        {
            get { return _Location; }
            private set
            {
                _Location = value;
            }
        }
        
        /// <summary>
        /// A touch point identifier that distinguishes a particular touch input
        /// </summary>
        private int _Id;
        public int Id
        {
            get { return _Id; }
            private set
            {
                _Id = value;
            }
        }
        
        /// <summary>
        /// A set of bit flags that specify various aspects of touch point
        /// press, release, and motion. 
        /// </summary>
        private int _Flags;
        public int Flags
        {
            get { return _Flags; }
            private set
            {
                _Flags = value;
            }
        }
        
        /// <summary>
        /// mask which fields in the structure are valid
        /// </summary>
        private int _Mask;
        public int Mask
        {
            get { return _Mask; }
            private set
            {
                _Mask = value;
            }
        }

        /// <summary>
        /// touch event time
        /// </summary>
        private DateTime _AbsoluteTime;
        public DateTime AbsoluteTime
        {
            get { return _AbsoluteTime; }
            private set { _AbsoluteTime = value; }
        }

        /// <summary>
        /// touch event time from system up
        /// </summary>
        private int _Time;
        public int Time
        {
            get { return _Time; }
            private set
            {
                _Time = value;
            }
        }
        /// <summary>
        /// the size of the contact area in pixels
        /// </summary>
        private Size? _ContactSize;
        public Size? ContactSize
        {
            get { return _ContactSize; }
            private set { _ContactSize = value; }
        }

        /// <summary>
        /// Is Primary Contact (The first touch sequence)
        /// </summary>
        public bool IsPrimaryContact
        {
            get { return (Flags & WinApi.TOUCHEVENTF_PRIMARY) != 0; }
        }

        /// <summary>
        /// Specifies that movement occurred
        /// </summary>
        public bool IsTouchMove
        {
            get { return CheckFlag(WinApi.TOUCHEVENTF_MOVE); }
        }

        /// <summary>
        /// Specifies that the corresponding touch point was established through a new contact
        /// </summary>
        public bool IsTouchDown
        {
            get { return CheckFlag(WinApi.TOUCHEVENTF_DOWN); }
        }

        /// <summary>
        /// Specifies that a touch point was removed
        /// </summary>
        public bool IsTouchUp
        {
            get { return CheckFlag(WinApi.TOUCHEVENTF_UP); }
        }

        /// <summary>
        /// Specifies that a touch point is in range
        /// </summary>
        public bool IsTouchInRange
        {
            get { return CheckFlag(WinApi.TOUCHEVENTF_INRANGE); }
        }

        /// <summary>
        /// specifies that this input was not coalesced.
        /// </summary>
        public bool IsTouchNoCoalesce
        {
            get { return CheckFlag(WinApi.TOUCHEVENTF_NOCOALESCE); }
        }

        /// <summary>
        /// Specifies that the touch point is associated with a pen contact
        /// </summary>
        public bool IsTouchPen
        {
            get { return CheckFlag(WinApi.TOUCHEVENTF_PEN); }
        }

        /// <summary>
        /// The touch event came from the user's palm
        /// </summary>
        /// <remarks>Set <see cref="DisablePalmRejection"/> to true</remarks>
        public bool IsTouchPalm
        {
            get { return CheckFlag(WinApi.TOUCHEVENTF_PALM); }
        }
    }
}
