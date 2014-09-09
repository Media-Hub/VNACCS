using System;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents complex gradient color table.
    /// </summary>
    public class GradientColorTable
    {
        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        public GradientColorTable() { }

        /// <summary>
        /// Creates new intance of the object and initializes it with default values.
        /// </summary>
        /// <param name="color1">Start color</param>
        /// <param name="color2">End color</param>
        public GradientColorTable(Color color)
        {
            BackgroundColorBlendCollection.InitializeCollection(Colors, color);
        }

        /// <summary>
        /// Creates new intance of the object and initializes it with default values.
        /// </summary>
        /// <param name="color1">Start color</param>
        /// <param name="color2">End color</param>
        public GradientColorTable(int color1, int color2)
            : this(color1, color2, 90)
        {
        }

        /// <summary>
        /// Creates new intance of the object and initializes it with default values.
        /// </summary>
        /// <param name="color1">Start color</param>
        /// <param name="color2">End color</param>
        public GradientColorTable(Color color1, Color color2)
            : this(color1, color2, 90)
        {
        }

        /// <summary>
        /// Creates new intance of the object and initializes it with default values.
        /// </summary>
        /// <param name="color1">Start color</param>
        /// <param name="color2">End color</param>
        /// <param name="linearGradientAngle">Linear gradient angle</param>
        public GradientColorTable(int color1, int color2, int linearGradientAngle)
        {
            BackgroundColorBlendCollection.InitializeCollection(Colors, color1, color2);
            this.LinearGradientAngle = linearGradientAngle;
        }

        /// <summary>
        /// Creates new intance of the object and initializes it with default values.
        /// </summary>
        /// <param name="color1">Start color</param>
        /// <param name="color2">End color</param>
        /// <param name="linearGradientAngle">Linear gradient angle</param>
        public GradientColorTable(Color color1, Color color2, int linearGradientAngle)
        {
            BackgroundColorBlendCollection.InitializeCollection(Colors, color1, color2);
            this.LinearGradientAngle = linearGradientAngle;
        }

        public bool IsEmpty
        {
            get
            {
                return Colors.Count == 0;
            }
        }

        /// <summary>
        /// Gets or sets the color collection blend that describes the gradient.
        /// </summary>
        public BackgroundColorBlendCollection Colors = new BackgroundColorBlendCollection();

        /// <summary>
        /// Gets or sets the gradient type.
        /// </summary>
        public eGradientType GradientType = eGradientType.Linear;

        /// <summary>
        /// Gets or sets the linear gradient angle.
        /// </summary>
        public int LinearGradientAngle = 90;
    }
}
