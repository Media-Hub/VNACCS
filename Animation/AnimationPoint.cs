using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Animation
{
    internal class AnimationPoint : Animation
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationPoint(AnimationEasing animationEasing, int animationDuration)
            :
            base(new AnimationRequest[0], animationEasing, animationDuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationPoint(AnimationRequest animationRequest, AnimationEasing animationEasing, int animationDuration)
            :
            base(new AnimationRequest[] { animationRequest }, animationEasing, animationDuration)
        {
        }
        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationPoint(AnimationRequest[] animationRequests, AnimationEasing animationEasing, int animationDuration)
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
                    Point toValue = (Point)request.To;
                    Point fromValue = (Point)request.From;
                    Point change = new Point(toValue.X - fromValue.X, toValue.Y - fromValue.Y);
                    Point newValue = toValue;

                    if (firstPass)
                        SetTargetPropertyValue(request.Target, request.Property, request.From);

                    if (change.X != 0)
                        newValue.X = (int)AnimationFunctions[EasingFunction](elapsedTime, fromValue.X, change.X, duration);
                    if (change.Y != 0)
                        newValue.Y = (int)AnimationFunctions[EasingFunction](elapsedTime, fromValue.Y, change.Y, duration);

                    SetTargetPropertyValue(request.Target, request.Property, newValue);

                    elapsedTime = DateTime.Now.Subtract(startTime).TotalMilliseconds;

                    if (e.Cancel) return;
                }

                ExecuteStepUpdateMethod();

                if (AnimationUpdateControl != null)
                    AnimationUpdateControl.Update();

                firstPass = false;
            }

            // Make sure final to value is assigned
            foreach (AnimationRequest request in requests)
            {
                SetTargetPropertyValue(request.Target, request.Property, request.To);
            }
        }

        protected override object GetPropertyValueCorrectType(double value)
        {
            return (int)value;
        }
        #endregion

    }
}
