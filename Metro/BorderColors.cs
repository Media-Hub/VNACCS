using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Globalization;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Reflection;

namespace DevComponents.DotNetBar.Metro
{

    /// <summary>
    /// Defines BorderColors structure used to define border colors.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(BorderColorsConverter))]
    public struct BorderColors : IEquatable<BorderColors>
    {
        #region Constructor
        private Color _Left;
        private Color _Top;
        private Color _Right;
        private Color _Bottom;

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="uniformLength">Uniform BorderColors</param>
        public BorderColors(Color uniformBorderColors)
        {
            _Left = _Top = _Right = _Bottom = uniformBorderColors;
        }
        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="left">Left BorderColors</param>
        /// <param name="top">Top BorderColors</param>
        /// <param name="right">Right BorderColors</param>
        /// <param name="bottom">Bottom BorderColors</param>
        public BorderColors(Color left, Color top, Color right, Color bottom)
        {
            _Left = left;
            _Top = top;
            _Right = right;
            _Bottom = bottom;
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Gets whether object equals to this instance.
        /// </summary>
        /// <param name="obj">object to test.</param>
        /// <returns>returns whether objects are Equals</returns>
        public override bool Equals(object obj)
        {
            if (obj is BorderColors)
            {
                BorderColors BorderColors = (BorderColors)obj;
                return (this == BorderColors);
            }
            return false;
        }
        /// <summary>
        /// Gets whether object equals to this instance.
        /// </summary>
        /// <param name="BorderColors">object to test.</param>
        /// <returns>returns whether objects are Equals</returns>
        public bool Equals(BorderColors BorderColors)
        {
            return (this == BorderColors);
        }
        /// <summary>
        /// Returns hash code for object.
        /// </summary>
        /// <returns>Hash code</returns>
        public override int GetHashCode()
        {
            return (((_Left.GetHashCode() ^ _Top.GetHashCode()) ^ _Right.GetHashCode()) ^ _Bottom.GetHashCode());
        }

        const string StringValueSeparator = ",";
        /// <summary>
        /// Returns string representation of object.
        /// </summary>
        /// <returns>string representing BorderColors</returns>
        public override string ToString()
        {
            return Convert.ToString(_Left) + StringValueSeparator + Convert.ToString(_Top) + StringValueSeparator + Convert.ToString(_Right) + StringValueSeparator + Convert.ToString(_Bottom);
        }
        /// <summary>
        /// Gets string representation of object.
        /// </summary>
        /// <param name="cultureInfo">Culture info.</param>
        /// <returns>string representing BorderColors</returns>
        internal string ToString(CultureInfo cultureInfo)
        {
            return Convert.ToString(_Left, cultureInfo) + StringValueSeparator + Convert.ToString(_Top, cultureInfo) + StringValueSeparator + Convert.ToString(_Right, cultureInfo) + StringValueSeparator + Convert.ToString(_Bottom, cultureInfo);
        }
        /// <summary>
        /// Returns whether all values are empty.
        /// </summary>
        [Browsable(false)]
        public bool IsEmpty
        {
            get
            {
                return (this.Left.IsEmpty && this.Top.IsEmpty && this.Right.IsEmpty && this.Bottom.IsEmpty);
            }
        }
        /// <summary>
        /// Returns whether all values are the same.
        /// </summary>
        [Browsable(false)]
        public bool IsUniform
        {
            get
            {
                return (this.Left == this.Top && this.Top == this.Right && this.Right == this.Bottom);
            }
        }

        public static bool operator ==(BorderColors t1, BorderColors t2)
        {
            return (t1._Left == t2._Left && t1._Top == t2._Top && t1._Right == t2._Right && t1._Bottom == t2._Bottom);
        }

        public static bool operator !=(BorderColors t1, BorderColors t2)
        {
            return !(t1 == t2);
        }
        /// <summary>
        /// Gets or sets the left BorderColors.
        /// </summary>
        public Color Left
        {
            get
            {
                return _Left;
            }
            set
            {
                _Left = value;
            }
        }
        /// <summary>
        /// Gets or sets the top BorderColors.
        /// </summary>
        public Color Top
        {
            get
            {
                return _Top;
            }
            set
            {
                _Top = value;
            }
        }
        /// <summary>
        /// Gets or sets the Right BorderColors.
        /// </summary>
        public Color Right
        {
            get
            {
                return _Right;
            }
            set
            {
                _Right = value;
            }
        }
        /// <summary>
        /// Gets or sets the Bottom BorderColors.
        /// </summary>
        public Color Bottom
        {
            get
            {
                return _Bottom;
            }
            set
            {
                _Bottom = value;
            }
        }
        #endregion
    }


    #region BorderColorsConverter
    /// <summary>
    /// Provides BorderColors TypeConverter.
    /// </summary>
    public class BorderColorsConverter : TypeConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(InstanceDescriptor)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            string str = value as string;
            if (string.IsNullOrEmpty(str))
            {
                return new BorderColors();
            }
            string str2 = str.Trim();
            if (str2.Length == 0)
            {
                return null;
            }
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            char ch = culture.TextInfo.ListSeparator[0];
            string[] strArray = str2.Split(new char[] { ch });
            Color[] numArray = new Color[strArray.Length];
            //TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = ColorScheme.GetColor(strArray[i]);// (Color)converter.ConvertFromString(context, culture, strArray[i]);
            }

            if (numArray.Length == 1)
            {
                return new BorderColors(numArray[0]);
            }

            if (numArray.Length != 4)
            {
                throw new ArgumentException("Text Parsing Failed");
            }
            return new BorderColors(numArray[0], numArray[1], numArray[2], numArray[3]);
        }

        private string ToArgbString(Color color)
        {
            if (color.IsEmpty) return "";
            if (color.A == 255)
                return color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
            return color.A.ToString("X2") + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is BorderColors)
            {
                if (destinationType == typeof(string))
                {
                    BorderColors colors = (BorderColors)value;
                    if (colors.IsEmpty) return "";
                    if (colors.IsUniform) return ToArgbString(colors.Left);
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string separator = culture.TextInfo.ListSeparator + " ";
                    //TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));
                    string[] strArray = new string[4];
                    int num = 0;
                    //strArray[num++] = converter.ConvertToString(context, culture, BorderColors.Left);
                    //strArray[num++] = converter.ConvertToString(context, culture, BorderColors.Top);
                    //strArray[num++] = converter.ConvertToString(context, culture, BorderColors.Right);
                    //strArray[num++] = converter.ConvertToString(context, culture, BorderColors.Bottom);
                    strArray[num++] = ToArgbString(colors.Left);
                    strArray[num++] = ToArgbString(colors.Top);
                    strArray[num++] = ToArgbString(colors.Right);
                    strArray[num++] = ToArgbString(colors.Bottom);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    BorderColors BorderColors2 = (BorderColors)value;
                    ConstructorInfo constructor = typeof(BorderColors).GetConstructor(new Type[] { typeof(Color), typeof(Color), typeof(Color), typeof(Color) });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[] { BorderColors2.Left, BorderColors2.Top, BorderColors2.Right, BorderColors2.Bottom });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            object left = propertyValues["Left"];
            object top = propertyValues["Top"];
            object right = propertyValues["Right"];
            object bottom = propertyValues["Bottom"];
            if ((((left == null) || (top == null)) || ((right == null) || (bottom == null))) || ((!(left is Color) || !(top is Color)) || (!(right is Color) || !(bottom is Color))))
            {
                throw new ArgumentException(string.Format("Property Value Invalid: left={0}, top={1}, right={2}, bottom={3}", left, top, right, bottom));
            }
            return new BorderColors((Color)left, (Color)top, (Color)right, (Color)bottom);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(BorderColors), attributes).Sort(new string[] { "Left", "Top", "Right", "Bottom" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    #endregion
}
