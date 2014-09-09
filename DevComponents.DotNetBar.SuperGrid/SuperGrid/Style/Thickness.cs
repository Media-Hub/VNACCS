using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Defines Thickness class.
    /// </summary>
    [TypeConverter(typeof(ThicknessTypeConverter))]
    public class Thickness : IEquatable<Thickness>, INotifyPropertyChanged
    {
        #region Static data

        /// <summary>
        /// Returns Empty instance of Thickness.
        /// </summary>
        public static Thickness Empty
        {
            get { return (new Thickness(0)); }
        }

        #endregion

        #region Private variables

        private int _Bottom;
        private int _Left;
        private int _Right;
        private int _Top;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="left">Left thickness in pixels.</param>
        /// <param name="top">Top thickness in pixels.</param>
        /// <param name="right">Right thickness in pixels.</param>
        /// <param name="bottom">Bottom thickness in pixels.</param>
        public Thickness(int left, int top, int right, int bottom)
        {
            _Left = left;
            _Top = top;
            _Right = right;
            _Bottom = bottom;
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="all">Specifies uniform Thickness.</param>
        public Thickness(int all)
            : this(all, all, all, all)
        {
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        public Thickness()
        {
        }

        #endregion

        #region Public properties

        #region All

        /// <summary>
        /// Gets or sets the thickness of all sides
        /// </summary>
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int All
        {
            set { _Top = _Left = _Bottom = _Right = value; }
        }

        #endregion

        #region Bottom

        /// <summary>
        /// Gets or sets the Bottom thickness in pixels.
        /// </summary>
        [DefaultValue(0)]
        [Description("Indicates the Bottom thickness in pixels.")]
        public int Bottom
        {
            get { return (_Bottom); }

            set
            {
                if (_Bottom != value)
                {
                    _Bottom = value;

                    OnPropertyChanged(new VisualPropertyChangedEventArgs("Bottom"));
                }
            }
        }

        #endregion

        #region Horizontal

        /// <summary>
        /// Gets horizontal thickness (Left + Right)
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Horizontal
        {
            get { return (_Left + _Right); }
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Gets whether the item is empty.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmpty
        {
            get
            {
                return (_Left == 0 && _Right == 0 &&
                        _Top == 0 && _Bottom == 0);
            }
        }

        #endregion

        #region Left

        /// <summary>
        /// Gets or sets the left thickness in pixels
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Description("Indicates the left thickness in pixels")]
        public int Left
        {
            get { return (_Left); }

            set
            {
                if (_Left != value)
                {
                    _Left = value;

                    OnPropertyChanged(new VisualPropertyChangedEventArgs("Left"));
                }
            }
        }

        #endregion

        #region Right

        /// <summary>
        /// Gets or sets the Right thickness in pixels
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Description("Indicates the Right thickness in pixels")]
        public int Right
        {
            get { return (_Right); }

            set
            {
                if (_Right != value)
                {
                    _Right = value;

                    OnPropertyChanged(new VisualPropertyChangedEventArgs("Right"));
                }
            }
        }

        #endregion

        #region Top

        /// <summary>
        /// Gets or sets the Top thickness in pixels
        /// </summary>
        [Browsable(true), DefaultValue(0)]
        [Description("Indicates the Top thickness in pixels")]
        public int Top
        {
            get { return (_Top); }

            set
            {
                if (_Top != value)
                {
                    _Top = value;

                    OnPropertyChanged(new VisualPropertyChangedEventArgs("Top"));
                }
            }
        }

        #endregion

        #region Vertical

        /// <summary>
        /// Gets vertical thickness (Top + Bottom)
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Vertical
        {
            get { return (_Top + _Bottom); }
        }

        #endregion

        #endregion

        #region Internal properties

        #region IsUniform

        internal bool IsUniform
        {
            get
            {
                return (_Left == _Top &&
                        _Left == _Right && _Left == _Bottom);
            }
        }

        #endregion

        #region IsZero

        internal bool IsZero
        {
            get
            {
                return (_Left == 0 && _Top == 0 &&
                        _Right == 0 && _Bottom == 0);
            }
        }

        #endregion

        #region Size

        internal Size Size
        {
            get { return (new Size(_Left + _Right, _Top + _Bottom)); }
        }

        #endregion

        #endregion

        #region Equals

        /// <summary>
        /// Gets whether two instances are equal.
        /// </summary>
        /// <param name="obj">Instance to compare to.</param>
        /// <returns>true if equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is Thickness)
                return (this == (Thickness)obj);

            return (false);
        }

        /// <summary>
        /// Gets whether two instances are equal.
        /// </summary>
        /// <param name="thickness">Instance to compare to</param>
        /// <returns>true if equal otherwise false</returns>
        public bool Equals(Thickness thickness)
        {
            return (this == thickness);
        }

        #endregion

        #region GetHashCode

        /// <summary>
        /// Returns hash-code.
        /// </summary>
        /// <returns>hash-code</returns>
        public override int GetHashCode()
        {
            return (((_Left.GetHashCode() ^ _Top.GetHashCode()) ^
                     _Right.GetHashCode()) ^ _Bottom.GetHashCode());
        }

        #endregion

        #region Operators

        #region "==" operator

        /// <summary>
        /// Implements == operator.
        /// </summary>
        /// <param name="t1">Object 1</param>
        /// <param name="t2">Object 2</param>
        /// <returns>true if equals</returns>
        public static bool operator ==(Thickness t1, Thickness t2)
        {
            if (ReferenceEquals(t1, t2))
                return (true);

            if (((object)t1 == null) || ((object)t2 == null))
                return (false);

            return (t1._Left == t2._Left && t1._Right == t2._Right &&
                    t1._Top == t2._Top && t1._Bottom == t2._Bottom);
        }

        #endregion

        #region "!=" operator

        /// <summary>
        /// Implements != operator
        /// </summary>
        /// <param name="t1">Object 1</param>
        /// <param name="t2">Object 2</param>
        /// <returns>true if different</returns>
        public static bool operator !=(Thickness t1, Thickness t2)
        {
            return ((t1 == t2) == false);
        }

        #endregion

        #endregion

        #region Copy

        /// <summary>
        /// Creates an exact copy of the Thickness.
        /// </summary>
        /// <returns>Copy of the Thickness.</returns>
        public Thickness Copy()
        {
            Thickness copy = new Thickness(_Left, _Top, _Right, _Bottom);

            return (copy);
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        void OnPropertyChanged(VisualPropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eh = PropertyChanged;

            if (eh != null)
                eh(this, e);
        }

        #endregion
    }

    #region ThicknessTypeConverter

    ///<summary>
    /// ThicknessTypeConverter
    ///</summary>
    public class ThicknessTypeConverter : ExpandableObjectConverter
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
                Thickness t = value as Thickness;

                if (t != null)
                {
                    if (t.IsUniform == true)
                        return (t.Left.ToString());

                    return (String.Format("{0:d}, {1:d}, {2:d}, {3:d}",
                        t.Bottom, t.Left, t.Right, t.Top));
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

                    Thickness t = (values.Length == 1)
                        ? new Thickness(v[0])
                        : new Thickness(v[1], v[3], v[2], v[0]);

                    return (t);
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
            return (new Thickness((int)propertyValues["Left"], (int)propertyValues["Top"],
                (int)propertyValues["Right"], (int)propertyValues["Bottom"]));
        }

        #endregion
    }

    #endregion
}
