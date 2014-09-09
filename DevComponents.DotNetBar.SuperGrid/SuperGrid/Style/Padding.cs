using System;
using System.Collections;
using System.ComponentModel;
using System.Globalization;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// Padding
    ///</summary>
    [TypeConverter(typeof(PaddingTypeConverter))]
    public class Padding : Thickness
    {
        #region Static data

        /// <summary>
        /// Returns Empty instance of Thickness.
        /// </summary>
        public new static Padding Empty
        {
            get { return (new Padding()); }
        }

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of the class and initializes it.
        /// </summary>
        /// <param name="left">Left padding</param>
        /// <param name="right">Right padding</param>
        /// <param name="top">Top padding</param>
        /// <param name="bottom">Bottom padding</param>
        public Padding(int left, int top, int right, int bottom)
            : base(left, top, right, bottom)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Padding class.
        /// </summary>
        /// <param name="all">Uniform padding.</param>
        public Padding(int all)
            : base(all)
        {
        }

        /// <summary>
        /// Initializes a new instance of the Padding class.
        /// </summary>
        public Padding()
        {
        }

        #endregion

        #region Copy

        /// <summary>
        /// Creates an exact copy of the Padding.
        /// </summary>
        /// <returns>Copy of the Padding.</returns>
        public new Padding Copy()
        {
            Padding copy = new Padding(Left, Top, Right, Bottom);

            return (copy);
        }

        #endregion
    }

    #region PaddingTypeConverter

    ///<summary>
    /// PaddingTypeConverter
    ///</summary>
    public class PaddingTypeConverter : ExpandableObjectConverter
    {
        #region CanConvertTo

        public override bool CanConvertTo(
            ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(string))
                return (true);

            return (base.CanConvertTo(context, destinationType));
        }

        #endregion

        #region ConvertTo

        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Padding p = value as Padding;

                if (p != null)
                {
                    if (p.IsUniform == true)
                        return (p.Left.ToString());

                    return (String.Format("{0:d}, {1:d}, {2:d}, {3:d}",
                        p.Bottom, p.Left, p.Right, p.Top));
                }
            }

            return (base.ConvertTo(context, culture, value, destinationType));
        }

        #endregion

        #region CanConvertFrom

        public override bool CanConvertFrom(
            ITypeDescriptorContext context, Type sourceType)
        {
            if (sourceType == typeof(string))
                return (true);

            return (base.CanConvertFrom(context, sourceType));
        }

        #endregion

        #region ConvertFrom

        public override object ConvertFrom(
            ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] values = ((string)value).Split(',');

                if (values.Length != 1 && values.Length != 4)
                    throw new ArgumentException("Invalid value to convert.");

                try
                {
                    int[] v = new int[values.Length];

                    for (int i = 0; i < values.Length; i++)
                        v[i] = int.Parse(values[i]);

                    Padding p = (values.Length == 1)
                        ? new Padding(v[0])
                        : new Padding(v[1], v[3], v[2], v[0]);

                    return (p);
                }
                catch (Exception)
                {
                    throw new ArgumentException("Invalid value to convert.");
                }
            }

            return base.ConvertFrom(context, culture, value);
        }

        #endregion

        #region GetCreateInstanceSupported

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return (true);
        }

        #endregion

        #region CreateInstance

        public override object CreateInstance(
            ITypeDescriptorContext context, IDictionary propertyValues)
        {
            return (new Padding((int)propertyValues["Left"], (int)propertyValues["Top"],
                (int)propertyValues["Right"], (int)propertyValues["Bottom"]));
        }

        #endregion
    }

    #endregion
}
