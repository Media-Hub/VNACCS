using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using System.Drawing;

namespace DevComponents.DotNetBar.Animation
{
    internal abstract class Animation : Component
    {
        #region Events
        /// <summary>
        /// Occurs after animation has completed.
        /// </summary>
        public event EventHandler AnimationCompleted;
        /// <summary>
        /// Raises AnimationCompleted event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnAnimationCompleted(EventArgs e)
        {
            EventHandler handler = AnimationCompleted;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        private BackgroundWorker _Worker = null;
        private AnimationEasing _EasingFunction = AnimationEasing.EaseOutQuad;
        private double _Duration = 300;
        private Dictionary<AnimationEasing, EasingFunctionDelegate> _AnimationFunctions = new Dictionary<AnimationEasing, EasingFunctionDelegate>();
        private List<AnimationRequest> _AnimationList = new List<AnimationRequest>();

        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public Animation(AnimationEasing animationEasing, int animationDuration)
            :
            this(new AnimationRequest[0], animationEasing, animationDuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public Animation(AnimationRequest animationRequest, AnimationEasing animationEasing, int animationDuration)
            :
            this(new AnimationRequest[] { animationRequest }, animationEasing, animationDuration)
        {
        }
        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public Animation(AnimationRequest[] animationRequests, AnimationEasing animationEasing, int animationDuration)
        {
            if (animationRequests != null && animationRequests.Length > 0)
                _AnimationList.AddRange(animationRequests);
            _EasingFunction = animationEasing;
            _Duration = (int)animationDuration;
            InitializeAnimationFunctions();
        }

        private void InitializeAnimationFunctions()
        {
            _AnimationFunctions.Add(AnimationEasing.EaseInBounce, new EasingFunctionDelegate(EaseInBounce));
            _AnimationFunctions.Add(AnimationEasing.EaseInCirc, new EasingFunctionDelegate(EaseInCirc));
            _AnimationFunctions.Add(AnimationEasing.EaseInCubic, new EasingFunctionDelegate(EaseInCubic));
            _AnimationFunctions.Add(AnimationEasing.EaseInElastic, new EasingFunctionDelegate(EaseInElastic));
            _AnimationFunctions.Add(AnimationEasing.EaseInExpo, new EasingFunctionDelegate(EaseInExpo));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutBounce, new EasingFunctionDelegate(EaseInOutBounce));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutCirc, new EasingFunctionDelegate(EaseInOutCirc));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutCubic, new EasingFunctionDelegate(EaseInOutCubic));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutElastic, new EasingFunctionDelegate(EaseInOutElastic));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutExpo, new EasingFunctionDelegate(EaseInOutExpo));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutQuad, new EasingFunctionDelegate(EaseInOutQuad));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutQuart, new EasingFunctionDelegate(EaseInOutQuart));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutQuint, new EasingFunctionDelegate(EaseInOutQuint));
            _AnimationFunctions.Add(AnimationEasing.EaseInOutSine, new EasingFunctionDelegate(EaseInOutSine));
            _AnimationFunctions.Add(AnimationEasing.EaseInQuad, new EasingFunctionDelegate(EaseInQuad));
            _AnimationFunctions.Add(AnimationEasing.EaseInQuart, new EasingFunctionDelegate(EaseInQuart));
            _AnimationFunctions.Add(AnimationEasing.EaseInQuint, new EasingFunctionDelegate(EaseInQuint));
            _AnimationFunctions.Add(AnimationEasing.EaseInSine, new EasingFunctionDelegate(EaseInSine));
            _AnimationFunctions.Add(AnimationEasing.EaseOutBounce, new EasingFunctionDelegate(EaseOutBounce));
            _AnimationFunctions.Add(AnimationEasing.EaseOutCirc, new EasingFunctionDelegate(EaseOutCirc));
            _AnimationFunctions.Add(AnimationEasing.EaseOutCubic, new EasingFunctionDelegate(EaseOutCubic));
            _AnimationFunctions.Add(AnimationEasing.EaseOutElastic, new EasingFunctionDelegate(EaseOutElastic));
            _AnimationFunctions.Add(AnimationEasing.EaseOutExpo, new EasingFunctionDelegate(EaseOutExpo));
            _AnimationFunctions.Add(AnimationEasing.EaseOutQuad, new EasingFunctionDelegate(EaseOutQuad));
            _AnimationFunctions.Add(AnimationEasing.EaseOutQuart, new EasingFunctionDelegate(EaseOutQuart));
            _AnimationFunctions.Add(AnimationEasing.EaseOutQuint, new EasingFunctionDelegate(EaseOutQuint));
            _AnimationFunctions.Add(AnimationEasing.EaseOutSine, new EasingFunctionDelegate(EaseOutSine));

        }
        #endregion

        #region Implementation
        private bool _AutoDispose = false;
        /// <summary>
        /// Gets or sets whether animation is auto-disposed once its completed. Default value is false.
        /// </summary>
        public bool AutoDispose
        {
            get { return _AutoDispose; }
            set
            {
                _AutoDispose = value;
            }
        }

        public List<AnimationRequest> Animations
        {
            get
            {
                return _AnimationList;
            }
        }

        protected override void Dispose(bool disposing)
        {
            //Console.WriteLine("{0} Animation DISPOSED", DateTime.Now);
            Stop();
            _IsDisposed = true;
            base.Dispose(disposing);
        }

        private bool _IsDisposed = false;
        public bool IsDisposed
        {
            get { return _IsDisposed; }
            internal set
            {
                _IsDisposed = value;
            }
        }

        /// <summary>
        /// Stops animation if one is currently running.
        /// </summary>
        public void Stop()
        {
            BackgroundWorker worker = _Worker;
            if (worker != null)
            {
                worker.DoWork -= new DoWorkEventHandler(WorkerDoWork);
                worker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(RunWorkerCompleted);
                worker.CancelAsync();
                worker.Dispose();
                _Worker = null;
            }
        }

        public void Start()
        {
            Start(_AnimationList.ToArray());
        }

        protected void Start(AnimationRequest[] requests)
        {
            if (_Worker != null)
                throw new InvalidOperationException("Animation is already running animations");
            //Console.WriteLine("{0} Animation Started", DateTime.Now);
            _IsCompleted = false;
            _Worker = new BackgroundWorker();
            _Worker.WorkerSupportsCancellation = true;
            _Worker.DoWork += new DoWorkEventHandler(WorkerDoWork);
            _Worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(RunWorkerCompleted);
            _Worker.RunWorkerAsync(requests);
        }

        private void RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            //Console.WriteLine(string.Format("{0}  RunWorkerCompleted", DateTime.Now));
            BackgroundWorker worker = _Worker;
            _Worker = null;
            if (worker != null)
            {
                worker.DoWork -= new DoWorkEventHandler(WorkerDoWork);
                worker.RunWorkerCompleted -= new RunWorkerCompletedEventHandler(RunWorkerCompleted);
                worker.Dispose();
            }

            _IsCompleted = true;
            OnAnimationCompleted(EventArgs.Empty);

            if (_AutoDispose)
                this.Dispose();
        }

        protected virtual void SetTargetPropertyValue(object target, PropertyInfo property, object value)
        {
            Control c = target as Control;
            if (c != null)
            {
                c.Invoke(new MethodInvoker(delegate
                {
                    property.SetValue(target, value, null);
                    if (c.Parent != null)
                        c.Parent.Update();
                    else
                        c.Update();
                }));
            }
            else if (target is BaseItem)
            {
                if (_AnimationUpdateControl != null)
                    _AnimationUpdateControl.Invoke(new MethodInvoker(delegate { property.SetValue(target, value, null); if (value is Rectangle) _AnimationUpdateControl.Invalidate(); }));
                else
                    property.SetValue(target, value, null);
            }
            else
            {
                property.SetValue(target, value, null);
            }
        }
        protected abstract object GetPropertyValueCorrectType(double value);

        protected virtual void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            AnimationRequest[] requests = (AnimationRequest[])e.Argument;
            double elapsedTime = 0;
            DateTime startTime = DateTime.Now;
            double duration = _Duration;

            bool firstPass = true;

            if (_FixedStepCount <= 0)
            {
                while (elapsedTime <= duration)
                {
                    try
                    {
                        foreach (AnimationRequest request in requests)
                        {
                            double toValue = request.GetToValue();
                            double fromValue = request.GetFromValue();
                            double change = toValue - fromValue;

                            if (firstPass)
                                SetTargetPropertyValue(request.Target, request.Property, request.From);

                            double r = _AnimationFunctions[_EasingFunction](elapsedTime, fromValue, change, duration);
                            if (change < 0 && r < toValue || change > 0 && r > toValue) { r = toValue; e.Cancel = true; break; }
                            if (change < 0 && r > fromValue || change > 0 && r < fromValue) r = fromValue;

                            SetTargetPropertyValue(request.Target, request.Property, GetPropertyValueCorrectType(r));
                            elapsedTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;
                            if (e.Cancel) break;
                        }

                        if (e.Cancel) break;

                        ExecuteStepUpdateMethod();

                        if (_AnimationUpdateControl != null)
                        {
                            if (_AnimationUpdateControl.InvokeRequired)
                                _AnimationUpdateControl.BeginInvoke(new MethodInvoker(delegate { _AnimationUpdateControl.Update(); }));
                            else
                                _AnimationUpdateControl.Update();
                        }
                        if (e.Cancel) break;
                        firstPass = false;
                    }
                    catch (TargetInvocationException exc)
                    {
                        if (exc.InnerException is ObjectDisposedException) // Stop work if target has been disposed
                            return;
                        throw;
                    }
                }
            }
            else
            {
                try
                {
                    for (int i = 0; i < _FixedStepCount; i++)
                    {
                        foreach (AnimationRequest request in requests)
                        {
                            double toValue = request.GetToValue();
                            double fromValue = request.GetFromValue();
                            double change = toValue - fromValue;
                            double step = change / _FixedStepCount;

                            if (firstPass)
                                SetTargetPropertyValue(request.Target, request.Property, request.From);

                            double r = fromValue + step * (i + 1);
                            if (change < 0 && r < toValue || change > 0 && r > toValue) { r = toValue; e.Cancel = true; break; }
                            if (change < 0 && r > fromValue || change > 0 && r < fromValue) r = fromValue;

                            SetTargetPropertyValue(request.Target, request.Property, GetPropertyValueCorrectType(r));
                            if (e.Cancel) break;
                            if (_Duration > 0)
                                System.Threading.Thread.Sleep((int)_Duration);
                        }

                        if (e.Cancel) break;

                        ExecuteStepUpdateMethod();

                        if (_AnimationUpdateControl != null)
                            _AnimationUpdateControl.Update();
                        if (e.Cancel) break;
                        firstPass = false;
                    }
                }
                catch (TargetInvocationException exc)
                {
                    if (exc.InnerException is ObjectDisposedException) // Stop work if target has been disposed
                        return;
                    throw;
                }
            }
            // Make sure final to value is assigned
            foreach (AnimationRequest request in requests)
            {
                SetTargetPropertyValue(request.Target, request.Property, request.To);
            }
            //System.Diagnostics.Debug.WriteLine(string.Format("{0}  WorkerDoWork DONE", DateTime.Now));
        }
        private bool _IsCompleted;
        /// <summary>
        /// Gets whether animation run is complete.
        /// </summary>
        public bool IsCompleted
        {
            get { return _IsCompleted; }
        }
        /// <summary>
        /// Gets the animation duration in milliseconds.
        /// </summary>
        public double Duration
        {
            get { return _Duration; }
            internal set
            {
                _Duration = value;
            }
        }
        /// <summary>
        /// Gets the animation easing function.
        /// </summary>
        public AnimationEasing EasingFunction
        {
            get
            {
                return _EasingFunction;
            }
        }

        protected Dictionary<AnimationEasing, EasingFunctionDelegate> AnimationFunctions
        {
            get
            {
                return _AnimationFunctions;
            }
        }

        protected virtual void ExecuteStepUpdateMethod()
        {
            if (_StepUpdateMethod != null)
                _StepUpdateMethod.DynamicInvoke(null);
        }

        private Delegate _StepUpdateMethod = null;
        /// <summary>
        /// Sets the method which is called each time value on target object property is set. This method may execute the visual updates on animation client.
        /// </summary>
        /// <param name="method">Method to call</param>
        public void SetStepUpdateMethod(Delegate method)
        {
            _StepUpdateMethod = method;
        }

        protected delegate double EasingFunctionDelegate(double t, double b, double c, double d);

        private Control _AnimationUpdateControl;
        public Control AnimationUpdateControl
        {
            get { return _AnimationUpdateControl; }
            set { _AnimationUpdateControl = value; }
        }

        private int _FixedStepCount = 0;
        /// <summary>
        /// Gets or sets the number of fixed steps animation will perform from star to finish instead of using the easing function in time.
        /// Stepped animation executes specified number of steps always with Duration specifying delays between each step.
        /// </summary>
        public int FixedStepCount
        {
            get { return _FixedStepCount; }
            set
            {
                _FixedStepCount = value;
            }
        }
        #endregion

        #region Easing Functions
        private double EaseInOutQuad(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t + b;
            return -c / 2 * ((--t) * (t - 2) - 1) + b;
        }
        private double EaseInQuad(double t, double b, double c, double d)
        {
            return c * (t /= d) * t + b;
        }
        private double EaseOutQuad(double t, double b, double c, double d)
        {
            return -c * (t /= d) * (t - 2) + b;
        }
        private double EaseInCubic(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t + b;
        }
        private double EaseOutCubic(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t + 1) + b;
        }
        private double EaseInOutCubic(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t + 2) + b;
        }
        private double EaseInQuart(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t + b;
        }
        private double EaseOutQuart(double t, double b, double c, double d)
        {
            return -c * ((t = t / d - 1) * t * t * t - 1) + b;
        }
        private double EaseInOutQuart(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t + b;
            return -c / 2 * ((t -= 2) * t * t * t - 2) + b;
        }
        private double EaseInQuint(double t, double b, double c, double d)
        {
            return c * (t /= d) * t * t * t * t + b;
        }
        private double EaseOutQuint(double t, double b, double c, double d)
        {
            return c * ((t = t / d - 1) * t * t * t * t + 1) + b;
        }
        private double EaseInOutQuint(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return c / 2 * t * t * t * t * t + b;
            return c / 2 * ((t -= 2) * t * t * t * t + 2) + b;
        }
        private double EaseInSine(double t, double b, double c, double d)
        {
            return -c * Math.Cos(t / d * (Math.PI / 2)) + c + b;
        }
        private double EaseOutSine(double t, double b, double c, double d)
        {
            return c * Math.Sin(t / d * (Math.PI / 2)) + b;
        }
        private double EaseInOutSine(double t, double b, double c, double d)
        {
            return -c / 2 * (Math.Cos(Math.PI * t / d) - 1) + b;
        }
        private double EaseInExpo(double t, double b, double c, double d)
        {
            return (t == 0) ? b : c * Math.Pow(2, 10 * (t / d - 1)) + b;
        }
        private double EaseOutExpo(double t, double b, double c, double d)
        {
            return (t == d) ? b + c : c * (-Math.Pow(2, -10 * t / d) + 1) + b;
        }
        private double EaseInOutExpo(double t, double b, double c, double d)
        {
            if (t == 0) return b;
            if (t == d) return b + c;
            if ((t /= d / 2) < 1) return c / 2 * Math.Pow(2, 10 * (t - 1)) + b;
            return c / 2 * (-Math.Pow(2, -10 * --t) + 2) + b;
        }
        private double EaseInCirc(double t, double b, double c, double d)
        {
            return -c * (Math.Sqrt(1 - (t /= d) * t) - 1) + b;
        }
        private double EaseOutCirc(double t, double b, double c, double d)
        {
            return c * Math.Sqrt(1 - (t = t / d - 1) * t) + b;
        }
        private double EaseInOutCirc(double t, double b, double c, double d)
        {
            if ((t /= d / 2) < 1) return -c / 2 * (Math.Sqrt(1 - t * t) - 1) + b;
            return c / 2 * (Math.Sqrt(1 - (t -= 2) * t) + 1) + b;
        }
        private double EaseInElastic(double t, double b, double c, double d)
        {
            double s = 1.70158;
            double p = 0;
            double a = c;
            if (t == 0)
                return b;
            if ((t /= d) == 1)
                return b + c;
            if (p == 0)
                p = d * .3;
            if (a < Math.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
                s = p / (2 * Math.PI) * Math.Asin(c / a);
            return -(a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
        }
        private double EaseOutElastic(double t, double b, double c, double d)
        {
            double s = 1.70158;
            double p = 0;
            double a = c;
            if (t == 0)
                return b;
            if ((t /= d) == 1)
                return b + c;
            if (p == 0) p = d * .3;
            if (a < Math.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
                s = p / (2 * Math.PI) * Math.Asin(c / a);
            return a * Math.Pow(2, -10 * t) * Math.Sin((t * d - s) * (2 * Math.PI) / p) + c + b;
        }
        private double EaseInOutElastic(double t, double b, double c, double d)
        {
            double s = 1.70158;
            double p = 0;
            double a = c;
            if (t == 0)
                return b;
            if ((t /= d / 2) == 2)
                return b + c;
            if (p == 0)
                p = d * (.3 * 1.5);
            if (a < Math.Abs(c))
            {
                a = c;
                s = p / 4;
            }
            else
                s = p / (2 * Math.PI) * Math.Asin(c / a);
            if (t < 1)
                return -.5 * (a * Math.Pow(2, 10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p)) + b;
            return a * Math.Pow(2, -10 * (t -= 1)) * Math.Sin((t * d - s) * (2 * Math.PI) / p) * .5 + c + b;
        }

        private double EaseInBounce(double t, double b, double c, double d)
        {
            return c - EaseOutBounce(d - t, 0, c, d) + b;
        }
        private double EaseOutBounce(double t, double b, double c, double d)
        {
            if ((t /= d) < (1 / 2.75))
            {
                return c * (7.5625 * t * t) + b;
            }
            else if (t < (2 / 2.75))
            {
                return c * (7.5625 * (t -= (1.5 / 2.75)) * t + .75) + b;
            }
            else if (t < (2.5 / 2.75))
            {
                return c * (7.5625 * (t -= (2.25 / 2.75)) * t + .9375) + b;
            }
            else
            {
                return c * (7.5625 * (t -= (2.625 / 2.75)) * t + .984375) + b;
            }
        }
        private double EaseInOutBounce(double t, double b, double c, double d)
        {
            if (t < d / 2) return EaseInBounce(t * 2, 0, c, d) * .5 + b;
            return EaseOutBounce(t * 2 - d, 0, c, d) * .5 + c * .5 + b;
        }
        #endregion
    }

    internal class AnimationRequest
    {
        private PropertyInfo _TargetProperty = null;

        /// <summary>
        /// Initializes a new instance of the AnimationRequest class.
        /// </summary>
        /// <param name="target">Target object for animation.</param>
        /// <param name="targetPropertyName">Target property name for animation.</param>
        /// <param name="from">From value.</param>
        /// <param name="to">To value.</param>
        public AnimationRequest(object target, string targetPropertyName, object to)
        {
            _TargetProperty = target.GetType().GetProperty(targetPropertyName);
            _Target = target;
            From = _TargetProperty.GetValue(target, null);
            To = to;
        }

        /// <summary>
        /// Initializes a new instance of the AnimationRequest class.
        /// </summary>
        /// <param name="target">Target object for animation.</param>
        /// <param name="targetPropertyName">Target property name for animation.</param>
        /// <param name="from">From value.</param>
        /// <param name="to">To value.</param>
        public AnimationRequest(object target, string targetPropertyName, object from, object to)
        {
            _TargetProperty = target.GetType().GetProperty(targetPropertyName);
            _Target = target;
            From = from;
            To = to;
        }

        private object _Target;
        /// <summary>
        /// Target object for animation.
        /// </summary>
        public object Target
        {
            get { return _Target; }
            set { _Target = value; }
        }

        private object _From;
        /// <summary>
        /// Animation from value.
        /// </summary>
        public object From
        {
            get { return _From; }
            set
            {
                _From = value;
                _FromValue = GetDoubleValue(value);
            }
        }

        private object _To;
        /// <summary>
        /// Animation to value.
        /// </summary>
        public object To
        {
            get { return _To; }
            set
            {
                _To = value;
                _ToValue = GetDoubleValue(value);
            }
        }

        private static double GetDoubleValue(object value)
        {
            if (value is int)
                return (double)(int)value;
            else if (value is double)
                return (double)value;
            else if (value is long)
                return (double)(long)value;
            else if (value is float)
                return (double)(float)value;

            return double.NaN;
        }

        internal PropertyInfo Property
        {
            get
            {
                return _TargetProperty;
            }
        }

        private double _ToValue = double.NaN;
        internal double GetToValue()
        {
            return _ToValue;
        }
        private double _FromValue = double.NaN;
        internal double GetFromValue()
        {
            return _FromValue;
        }
        
    }

    public enum AnimationEasing
    {
        EaseInOutQuad,
        EaseInQuad,
        EaseOutQuad,
        EaseInCubic,
        EaseOutCubic,
        EaseInOutCubic,
        EaseInQuart,
        EaseOutQuart,
        EaseInOutQuart,
        EaseInQuint,
        EaseOutQuint,
        EaseInOutQuint,
        EaseInSine,
        EaseOutSine,
        EaseInOutSine,
        EaseInExpo,
        EaseOutExpo,
        EaseInOutExpo,
        EaseInCirc,
        EaseOutCirc,
        EaseInOutCirc,
        EaseInElastic,
        EaseOutElastic,
        EaseInOutElastic,
        EaseInBounce,
        EaseOutBounce,
        EaseInOutBounce
    }
}
