using System;
using System.Drawing;
using System.ComponentModel;
using System.Globalization;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Represents style border color.
    /// </summary>
    [TypeConverter(typeof(BorderColorConverter))]
    public class BorderColor : INotifyPropertyChanged
    {
        #region Static data

        ///<summary>
        /// Empty
        ///</summary>
        /// <summary>
        /// Returns Empty instance of BorderColor.
        /// </summary>
        public static BorderColor Empty
        {
            get { return (new BorderColor()); }
        }

        #endregion

        #region Private variables

        private Color _Top = Color.Empty;
        private Color _Left = Color.Empty;
        private Color _Bottom = Color.Empty;
        private Color _Right = Color.Empty;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the BorderColor object.
        /// </summary>
        /// <param name="left"></param>
        /// <param name="top"></param>
        /// <param name="right"></param>
        /// <param name="bottom"></param>
        public BorderColor(Color left, Color top, Color right, Color bottom)
        {
            _Top = top;
            _Bottom = bottom;
            _Left = left;
            _Right = right;
        }

        /// <summary>
        /// Initializes a new instance of the BorderColor object.
        /// </summary>
        public BorderColor(Color all)
            : this(all, all, all, all)
        {
        }

        /// <summary>
        /// Initializes a new instance of the BorderColor object.
        /// </summary>
        public BorderColor()
        {
        }

        #endregion

        #region Public properties

        #region All

        /// <summary>
        /// Gets or sets the color of all borders.
        /// </summary>
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Color All
        {
            set { _Top = _Left = _Bottom = _Right = value; }
        }

        #endregion

        #region Bottom

        /// <summary>
        /// Gets or sets the color of the bottom border
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the color of the bottom border")]
        public Color Bottom
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

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBottom()
        {
            return (_Bottom.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBottom()
        {
            _Bottom = Color.Empty;
        }

        #endregion

        #region Left

        /// <summary>
        /// Gets or sets the color of the left border
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the color of the left border")]
        public Color Left
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

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeLeft()
        {
            return (_Left.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetLeft()
        {
            _Left = Color.Empty;
        }

        #endregion

        #region Right

        /// <summary>
        /// Gets or sets the color of the right border
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the color of the right border")]
        public Color Right
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

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRight()
        {
            return (_Right.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRight()
        {
            _Right = Color.Empty;
        }

        #endregion

        #region Top

        /// <summary>
        /// Gets or sets the color of the top border
        /// </summary>
        [Category("Appearance")]
        [Description("Indicates the color of the top border")]
        public Color Top
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

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeTop()
        {
            return (_Top.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetTop()
        {
            _Top = Color.Empty;
        }

        #endregion

        #endregion

        #region Internal properties

        #region IsEmpty

        internal bool IsEmpty
        {
            get
            {
                return (_Left.IsEmpty && _Top.IsEmpty &&
                        _Right.IsEmpty && _Bottom.IsEmpty);
            }
        }

        #endregion

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

        #endregion

        #region Operators

        #region '==' operator

        /// <summary>
        /// Compares two instances.
        /// </summary>
        /// <param name="t1">Instance 1</param>
        /// <param name="t2">Instance 2</param>
        /// <returns>true if same</returns>
        public static bool operator ==(BorderColor t1, BorderColor t2)
        {
            // If both are null, or both are same instance, return true.

            if (ReferenceEquals(t1, t2))
                return (true);

            // If one is null, but not both, return false.

            if (((object)t1 == null) || ((object)t2 == null))
                return (false);

            return (t1._Left == t2._Left && t1._Right == t2._Right &&
                    t1._Top == t2._Top && t1._Bottom == t2._Bottom);
        }

        #endregion

        #region "!=" operator

        /// <summary>
        /// Compares two instances.
        /// </summary>
        /// <param name="t1">Instance 1</param>
        /// <param name="t2">Instance 2</param>
        /// <returns>true if different.</returns>
        public static bool operator !=(BorderColor t1, BorderColor t2)
        {
            return ((t1 == t2) == false);
        }

        #endregion

        #endregion

        #region GetHashCode

        /// <summary>
        /// Returns hash-code for object.
        /// </summary>
        /// <returns>Hash-code value.</returns>
        public override int GetHashCode()
        {
            return (((_Left.GetHashCode() ^ _Top.GetHashCode()) ^
                     _Right.GetHashCode()) ^ _Bottom.GetHashCode());
        }

        #endregion

        #region Equals

        /// <summary>
        /// Compares object to this instance.
        /// </summary>
        /// <param name="obj">Object to compare.</param>
        /// <returns>true if same.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BorderColor)
                return ((BorderColor) obj == this);

            return (false);
        }

        /// <summary>
        /// Compares object to this instance.
        /// </summary>
        /// <param name="borderColor">Border color</param>
        /// <returns>true if same</returns>
        public bool Equals(BorderColor borderColor)
        {
            return (this == borderColor);
        }

        #endregion

        #region Copy

        /// <summary>
        /// Creates an exact copy of the BorderColor.
        /// </summary>
        /// <returns>Copy of the BorderColor.</returns>
        public BorderColor Copy()
        {
            BorderColor copy = new BorderColor(_Left, _Top, _Right, _Bottom);

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
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eh = PropertyChanged;

            if (eh != null)
                eh(this, e);
        }

        #endregion
    }

    #region BorderColorConverter

    /// <summary>
    /// BorderColorConverter
    /// </summary>
    public class BorderColorConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return (String.Empty);

            return (base.ConvertTo(context, culture, value, destinationType));
        }
    }

    #endregion

}
