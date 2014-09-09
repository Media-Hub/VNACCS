using System;
using System.ComponentModel;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Defines Thickness class.
    /// </summary>
    [TypeConverter(typeof(BlankExpandableObjectConverter))]
    public class BorderPattern : IEquatable<BorderPattern>, INotifyPropertyChanged
    {
        #region Static data

        /// <summary>
        /// Returns Empty instance of BorderPattern.
        /// </summary>
        public static BorderPattern Empty
        {
            get { return (new BorderPattern()); }
        }

        #endregion

        #region Private variables

        private LinePattern _Bottom = LinePattern.NotSet;
        private LinePattern _Left = LinePattern.NotSet;
        private LinePattern _Right = LinePattern.NotSet;
        private LinePattern _Top = LinePattern.NotSet;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="left">Left BorderPatternStyle.</param>
        /// <param name="top">Top BorderPatternStyle.</param>
        /// <param name="right">Right BorderPatternStyle.</param>
        /// <param name="bottom">Bottom BorderPatternStyle.</param>
        public BorderPattern(LinePattern left,
            LinePattern top, LinePattern right, LinePattern bottom)
        {
            _Left = left;
            _Top = top;
            _Right = right;
            _Bottom = bottom;

            PropertyChanged = null;
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="all">Specifies uniform Thickness.</param>
        public BorderPattern(LinePattern all)
            : this(all, all, all, all)
        {
        }

        ///<summary>
        /// Creates new instance of the object.
        ///</summary>
        public BorderPattern()
        {
        }

        #endregion

        #region Public properties

        #region All

        /// <summary>
        /// Gets or sets the thickness of all sides.
        /// </summary>
        //[Browsable(false)]
        //[EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public LinePattern All
        {
            set { _Top = _Left = _Bottom = _Right = value; }
        }

        #endregion

        #region Bottom

        /// <summary>
        /// Gets or sets the bottom Border Pattern
        /// </summary>
        [DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the bottom Border Pattern")]
        public LinePattern Bottom
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

        #region Left

        /// <summary>
        /// Gets or sets the left Border Pattern
        /// </summary>
        [DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the left Border Pattern")]
        public LinePattern Left
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
        /// Gets or sets the Right Border Pattern
        /// </summary>
        [DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the Right Border Pattern")]
        public LinePattern Right
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
        /// Gets or sets the Top Border Pattern
        /// </summary>
        [Browsable(true), DefaultValue(LinePattern.NotSet)]
        [Description("Indicates the Top Border Pattern")]
        public LinePattern Top
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

        #region IsEmpty

        /// <summary>
        /// Gets whether the item is empty
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmpty
        {
            get
            {
                return (_Left == LinePattern.NotSet &&
                        _Right == LinePattern.NotSet &&
                        _Top == LinePattern.NotSet &&
                        _Bottom == LinePattern.NotSet);
            }
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

        #endregion

        #region Equals

        /// <summary>
        /// Gets whether two instances are equal.
        /// </summary>
        /// <param name="obj">Instance to compare to.</param>
        /// <returns>true if equal otherwise false.</returns>
        public override bool Equals(object obj)
        {
            if (obj is BorderPattern)
                return (this == (BorderPattern)obj);

            return (false);
        }

        /// <summary>
        /// Gets whether two instances are equal.
        /// </summary>
        /// <param name="borderPattern">Instance to compare to</param>
        /// <returns>true if equal otherwise false</returns>
        public bool Equals(BorderPattern borderPattern)
        {
            return (this == borderPattern);
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
        public static bool operator ==(BorderPattern t1, BorderPattern t2)
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
        public static bool operator !=(BorderPattern t1, BorderPattern t2)
        {
            return ((t1 == t2) == false);
        }

        #endregion

        #endregion

        #region ApplyPattern

        /// <summary>
        /// Applies the pattern to instance of this pattern.
        /// </summary>
        /// <param name="pattern">Pattern to apply.</param>
        public void ApplyPattern(BorderPattern pattern)
        {
            if (pattern != null)
            {
                if (pattern.Top != LinePattern.NotSet)
                    _Top = pattern.Top;

                if (pattern.Left != LinePattern.NotSet)
                    _Left = pattern.Left;

                if (pattern.Bottom != LinePattern.NotSet)
                    _Bottom = pattern.Bottom;

                if (pattern.Right != LinePattern.NotSet)
                    _Right = pattern.Right;
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Creates an exact copy of the BorderPattern.
        /// </summary>
        /// <returns>Copy of the BorderPattern.</returns>
        public BorderPattern Copy()
        {
            BorderPattern copy = new BorderPattern(_Left, _Top, _Right, _Bottom);

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

    #region enums

    #region LinePattern

    ///<summary>
    /// LinePattern
    ///</summary>
    public enum LinePattern
    {
        ///<summary>
        /// None
        ///</summary>
        None = -2,

        ///<summary>
        /// NotSet
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// Solid
        ///</summary>
        Solid = DashStyle.Solid,

        ///<summary>
        /// Dash
        ///</summary>
        Dash = DashStyle.Dash,

        ///<summary>
        /// Dot
        ///</summary>
        Dot = DashStyle.Dot,

        ///<summary>
        /// DashDot
        ///</summary>
        DashDot = DashStyle.DashDot,

        ///<summary>
        /// DashDotDot
        ///</summary>
        DashDotDot = DashStyle.DashDotDot,
    }

    #endregion

    #endregion

}
