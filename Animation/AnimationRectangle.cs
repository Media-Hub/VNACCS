using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Animation
{
    internal class AnimationRectangle : Animation
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationRectangle(AnimationEasing animationEasing, int animationDuration)
            :
            base(new AnimationRequest[0], animationEasing, animationDuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationRectangle(AnimationRequest animationRequest, AnimationEasing animationEasing, int animationDuration)
            :
            base(new AnimationRequest[] { animationRequest }, animationEasing, animationDuration)
        {
        }
        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationRectangle(AnimationRequest[] animationRequests, AnimationEasing animationEasing, int animationDuration)
            :
            base(animationRequests, animationEasing, animationDuration) { }
        #endregion

        #region Implementation
        protected override void WorkerDoWork(object sender, DoWorkEventArgs e)
        {
            AnimationRequest[] requests = (AnimationRequest[])e.Argument;
            double elapsedTime = 0;
            DateTime startTime = DateTime.Now;
            double duration = Duration;

            bool firstPass = true;
            while (elapsedTime <= duration)
            {
                foreach (AnimationRequest request in requests)
                {
                    Rectangle toValue = (Rectangle)request.To;
                    Rectangle fromValue = (Rectangle)request.From;
                    Rectangle change = new Rectangle(toValue.X - fromValue.X, toValue.Y - fromValue.Y, 
                        toValue.Width - fromValue.Width, toValue.Height - fromValue.Height);
                    Rectangle newValue = toValue;
                    if (IsDisposed(request.Target)) return;
                    if (firstPass)
                        SetTargetPropertyValue(request.Target, request.Property, request.From);
                    
                    if (change.X != 0)
                        newValue.X = (int)AnimationFunctions[EasingFunction](elapsedTime, fromValue.X, change.X, duration);
                    if (change.Y != 0)
                        newValue.Y = (int)AnimationFunctions[EasingFunction](elapsedTime, fromValue.Y, change.Y, duration);
                    if (change.Width != 0)
                        newValue.Width = (int)AnimationFunctions[EasingFunction](elapsedTime, fromValue.Width, change.Width, duration);
                    if (change.Height != 0)
                        newValue.Height = (int)AnimationFunctions[EasingFunction](elapsedTime, fromValue.Height, change.Height, duration);

                    SetTargetPropertyValue(request.Target, request.Property, newValue);

                    elapsedTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;

                    if (e.Cancel) return;
                }
                
                ExecuteStepUpdateMethod();

                if (AnimationUpdateControl != null)
                    AnimationUpdateControl.Invoke(new MethodInvoker(delegate { AnimationUpdateControl.Update(); }));

                firstPass = false;
            }

            // Make sure final to value is assigned
            foreach (AnimationRequest request in requests)
            {
                SetTargetPropertyValue(request.Target, request.Property, request.To);
            }

            //Console.WriteLine("{0}  WorkerDoWork Complete", DateTime.Now);
        }

        private bool IsDisposed(object p)
        {
            if (p == null) return true;
            if (p is Control && ((Control)p).IsDisposed) return true;
            if (p is BaseItem && ((BaseItem)p).IsDisposed) return true;
            return false;
        }

        protected override object GetPropertyValueCorrectType(double value)
        {
            return (int)value;
        }
        #endregion

    }
}
