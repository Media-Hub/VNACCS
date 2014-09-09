using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Globalization;
using System.Drawing;
using System.Collections;
using System.Reflection;
using System.ComponentModel.Design.Serialization;
using DevComponents.DotNetBar.Metro.Helpers;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
    /// Defines Thickness structure used by borders and margins.
    /// </summary>
    [StructLayout(LayoutKind.Sequential), TypeConverter(typeof(ThicknessConverter))]
    public struct Thickness : IEquatable<Thickness>
    {
        #region Constructor
        private double _Left;
        private double _Top;
        private double _Right;
        private double _Bottom;

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="uniformLength">Uniform Thickness</param>
        public Thickness(double uniformThickness)
        {
            _Left = _Top = _Right = _Bottom = uniformThickness;
        }
        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="left">Left Thickness</param>
        /// <param name="top">Top Thickness</param>
        /// <param name="right">Right Thickness</param>
        /// <param name="bottom">Bottom Thickness</param>
        public Thickness(double left, double top, double right, double bottom)
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
            if (obj is Thickness)
            {
                Thickness thickness = (Thickness)obj;
                return (this == thickness);
            }
            return false;
        }
        /// <summary>
        /// Gets whether object equals to this instance.
        /// </summary>
        /// <param name="thickness">object to test.</param>
        /// <returns>returns whether objects are Equals</returns>
        public bool Equals(Thickness thickness)
        {
            return (this == thickness);
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
        /// <returns>string representing Thickness</returns>
        public override string ToString()
        {
            return Convert.ToString(_Left) + StringValueSeparator + Convert.ToString(_Top) + StringValueSeparator + Convert.ToString(_Right) + StringValueSeparator + Convert.ToString(_Bottom);
        }
        /// <summary>
        /// Gets string representation of object.
        /// </summary>
        /// <param name="cultureInfo">Culture info.</param>
        /// <returns>string representing Thickness</returns>
        internal string ToString(CultureInfo cultureInfo)
        {
            return Convert.ToString(_Left, cultureInfo) + StringValueSeparator + Convert.ToString(_Top, cultureInfo) + StringValueSeparator + Convert.ToString(_Right, cultureInfo) + StringValueSeparator + Convert.ToString(_Bottom, cultureInfo);
        }
        /// <summary>
        /// Returns whether all values are zero.
        /// </summary>
        [Browsable(false)]
        public bool IsZero
        {
            get
            {
                return (((DoubleHelpers.IsZero(this.Left) && DoubleHelpers.IsZero(this.Top)) && DoubleHelpers.IsZero(this.Right)) && DoubleHelpers.IsZero(this.Bottom));
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
                return ((DoubleHelpers.AreClose(this.Left, this.Top) && DoubleHelpers.AreClose(this.Left, this.Right)) && DoubleHelpers.AreClose(this.Left, this.Bottom));
            }
        }
        /// <summary>
        /// Returns whether object holds valid value.
        /// </summary>
        /// <param name="allowNegative">Specifies whether negative values are allowed.</param>
        /// <param name="allowNaN">Specifies whether NaN values are allowed.</param>
        /// <param name="allowPositiveInfinity">Specifies whether positive infinity values are allowed</param>
        /// <param name="allowNegativeInfinity">Specifies whether negative infinity values are allowed</param>
        /// <returns>true if object holds valid value</returns>
        internal bool IsValid(bool allowNegative, bool allowNaN, bool allowPositiveInfinity, bool allowNegativeInfinity)
        {
            if (!allowNegative && (((this.Left < 0.0) || (this.Right < 0.0)) || ((this.Top < 0.0) || (this.Bottom < 0.0))))
            {
                return false;
            }
            if (!allowNaN && ((DoubleHelpers.IsNaN(this.Left) || DoubleHelpers.IsNaN(this.Right)) || (DoubleHelpers.IsNaN(this.Top) || DoubleHelpers.IsNaN(this.Bottom))))
            {
                return false;
            }
            if (!allowPositiveInfinity && ((double.IsPositiveInfinity(this.Left) || double.IsPositiveInfinity(this.Right)) || (double.IsPositiveInfinity(this.Top) || double.IsPositiveInfinity(this.Bottom))))
            {
                return false;
            }
            return (allowNegativeInfinity || ((!double.IsNegativeInfinity(this.Left) && !double.IsNegativeInfinity(this.Right)) && (!double.IsNegativeInfinity(this.Top) && !double.IsNegativeInfinity(this.Bottom))));
        }
        /// <summary>
        /// Returns true if two objects are close.
        /// </summary>
        /// <param name="thickness">Thickness to test.</param>
        /// <returns>true if values are close.</returns>
        internal bool IsClose(Thickness thickness)
        {
            return (((DoubleHelpers.AreClose(this.Left, thickness.Left) && DoubleHelpers.AreClose(this.Top, thickness.Top)) && DoubleHelpers.AreClose(this.Right, thickness.Right)) && DoubleHelpers.AreClose(this.Bottom, thickness.Bottom));
        }
        /// <summary>
        /// Returns true if two objects are close.
        /// </summary>
        /// <param name="thickness1">Thickness 1</param>
        /// <param name="thickness2">Thickness 2</param>
        /// <returns>true if values are close.</returns>
        internal static bool AreClose(Thickness thickness1, Thickness thickness2)
        {
            return thickness1.IsClose(thickness2);
        }

        public static bool operator ==(Thickness t1, Thickness t2)
        {
            return (((((t1._Left == t2._Left) || (DoubleHelpers.IsNaN(t1._Left) && DoubleHelpers.IsNaN(t2._Left))) && ((t1._Top == t2._Top) || (DoubleHelpers.IsNaN(t1._Top) && DoubleHelpers.IsNaN(t2._Top)))) && ((t1._Right == t2._Right) || (DoubleHelpers.IsNaN(t1._Right) && DoubleHelpers.IsNaN(t2._Right)))) && ((t1._Bottom == t2._Bottom) || (DoubleHelpers.IsNaN(t1._Bottom) && DoubleHelpers.IsNaN(t2._Bottom))));
        }

        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return !(t1 == t2);
        }
        /// <summary>
        /// Gets or sets the left Thickness.
        /// </summary>
        public double Left
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
        /// Gets or sets the top Thickness.
        /// </summary>
        public double Top
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
        /// Gets or sets the Right Thickness.
        /// </summary>
        public double Right
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
        /// Gets or sets the Bottom Thickness.
        /// </summary>
        public double Bottom
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

        /// <summary>
        /// Gets the total horizontal thickness i.e. Left+Right.
        /// </summary>
        [Browsable(false)]
        public double Horizontal
        {
            get
            {
                return _Left + _Right;
            }
        }
        /// <summary>
        /// Gets the total vertical thickness i.e. Top+Bottom.
        /// </summary>
        [Browsable(false)]
        public double Vertical
        {
            get
            {
                return _Top + _Bottom;
            }
        }
        #endregion
    }

    #region ThicknessConverter
    /// <summary>
    /// Provides Thickness TypeConverter.
    /// </summary>
    public class ThicknessConverter : TypeConverter
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
            if (str == null)
            {
                return base.ConvertFrom(context, culture, value);
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
            double[] numArray = new double[strArray.Length];
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(double));
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = (double)converter.ConvertFromString(context, culture, strArray[i]);
            }
            if (numArray.Length != 4)
            {
                throw new ArgumentException("Text Parsing Failed");
            }
            return new Thickness(numArray[0], numArray[1], numArray[2], numArray[3]);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is Thickness)
            {
                if (destinationType == typeof(string))
                {
                    Thickness Thickness = (Thickness)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string separator = culture.TextInfo.ListSeparator + " ";
                    TypeConverter converter = TypeDescriptor.GetConverter(typeof(double));
                    string[] strArray = new string[4];
                    int num = 0;
                    strArray[num++] = converter.ConvertToString(context, culture, Thickness.Left);
                    strArray[num++] = converter.ConvertToString(context, culture, Thickness.Top);
                    strArray[num++] = converter.ConvertToString(context, culture, Thickness.Right);
                    strArray[num++] = converter.ConvertToString(context, culture, Thickness.Bottom);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    Thickness Thickness2 = (Thickness)value;
                    ConstructorInfo constructor = typeof(Thickness).GetConstructor(new Type[] { typeof(double), typeof(double), typeof(double), typeof(double) });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[] { Thickness2.Left, Thickness2.Top, Thickness2.Right, Thickness2.Bottom });
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
            if ((((left == null) || (top == null)) || ((right == null) || (bottom == null))) || ((!(left is double) || !(top is double)) || (!(right is double) || !(bottom is double))))
            {
                throw new ArgumentException(string.Format("Property Value Invalid: left={0}, top={1}, right={2}, bottom={3}", left, top, right, bottom));
            }
            return new Thickness((double)left, (double)top, (double)right, (double)bottom);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(Thickness), attributes).Sort(new string[] { "Left", "Top", "Right", "Bottom" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    #endregion
}
