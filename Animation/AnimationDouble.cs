using System;
using System.Collections.Generic;
using System.Text;

namespace DevComponents.DotNetBar.Animation
{
    internal class AnimationDouble : Animation
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationDouble(AnimationEasing animationEasing, int animationDuration)
            :
            base(new AnimationRequest[0], animationEasing, animationDuration)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationDouble(AnimationRequest animationRequest, AnimationEasing animationEasing, int animationDuration)
            :
            base(new AnimationRequest[] { animationRequest }, animationEasing, animationDuration)
        {
        }
        /// <summary>
        /// Initializes a new instance of the Animation class.
        /// </summary>
        /// <param name="target">Target object for animation</param>
        /// <param name="targetPropertyName">Target property name for animation</param>
        public AnimationDouble(AnimationRequest[] animationRequests, AnimationEasing animationEasing, int animationDuration)
            :
            base(animationRequests, animationEasing, animationDuration) { }
        #endregion

        protected override object GetPropertyValueCorrectType(double value)
        {
            return value;
        }
    }
}
