using System;
using System.Drawing;
using System.ComponentModel;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Globalization;
using DevComponents.Instrumentation.Primitives;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Represents background of visual style.
    /// </summary>
    [TypeConverter(typeof(BackgroundConvertor))]
    [Editor(typeof(BackgroundEditor), typeof(UITypeEditor))]
    public class Background : INotifyPropertyChanged
    {
        #region Static data

        ///<summary>
        /// Empty
        ///</summary>
        /// <summary>
        /// Returns Empty instance of BorderPattern.
        /// </summary>
        public static Background Empty
        {
            get { return (new Background()); }
        }

        #endregion

        #region Private variables

        private Color _Color1 = Color.Empty;
        private Color _Color2 = Color.Empty;

        private int _GradientAngle = 90;

        private BackFillType _BackFillType = BackFillType.Angle;
        private BackColorBlend _BackColorBlend;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        public Background() { }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="color1">Start color.</param>
        public Background(Color color1)
        {
            _Color1 = color1;
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="color1">Start color.</param>
        /// <param name="color2">End color.</param>
        public Background(Color color1, Color color2)
        {
            _Color1 = color1;
            _Color2 = color2;
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="color1">Start color in hexadecimal representation like FFFFFF.</param>
        /// <param name="color2">End color in hexadecimal representation like FFFFFF.</param>
        public Background(string color1, string color2)
        {
            _Color1 = ColorFactory.GetColor(color1);
            _Color2 = ColorFactory.GetColor(color2);
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="color1">Start color in 32-bit RGB representation.</param>
        /// <param name="color2">End color in 32-bit RGB representation.</param>
        public Background(int color1, int color2)
        {
            _Color1 = ColorFactory.GetColor(color1);
            _Color2 = ColorFactory.GetColor(color2);
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="color1">Start color in 32-bit RGB representation.</param>
        /// <param name="color2">End color in 32-bit RGB representation.</param>
        /// <param name="gradientAngle">Gradient angle.</param>
        public Background(int color1, int color2, int gradientAngle)
        {
            _Color1 = ColorFactory.GetColor(color1);
            _Color2 = ColorFactory.GetColor(color2);

            GradientAngle = gradientAngle;
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="color1">Start color.</param>
        /// <param name="color2">End color.</param>
        /// <param name="gradientAngle">Gradient angle.</param>
        public Background(Color color1, Color color2, int gradientAngle)
        {
            _Color1 = color1;
            _Color2 = color2;

            GradientAngle = gradientAngle;
        }

        /// <summary>
        /// Creates new instance of the object.
        /// </summary>
        /// <param name="color1">Start color.</param>
        /// <param name="color2">End color.</param>
        /// <param name="fillType">Gradient angle.</param>
        public Background(Color color1, Color color2, BackFillType fillType)
        {
            _Color1 = color1;
            _Color2 = color2;

            _BackFillType = fillType;
        }

        #endregion

        #region Public properties

        #region Color1

        /// <summary>
        /// Gets or sets the start color.
        /// </summary>
        [Description("Indicates the Starting Gradient Color.")]
        public Color Color1
        {
            get { return (_Color1); }

            set
            {
                if (_Color1 != value)
                {
                    _Color1 = value;

                    OnPropertyChangedEx("Color1");
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeColor1()
        {
            return (_Color1.IsEmpty == false);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual void ResetColor1()
        {
            _Color1 = Color.Empty;
        }

        #endregion

        #region Color2

        /// <summary>
        /// Gets or sets the Ending Gradient Color
        /// </summary>
        [Description("Indicates the Ending Gradient Color")]
        public Color Color2
        {
            get { return (_Color2); }

            set
            {
                if (_Color2 != value)
                {
                    _Color2 = value;

                    OnPropertyChangedEx("Color2");
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeColor2()
        {
            return (_Color2.IsEmpty == false);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual void ResetColor2()
        {
            _Color2 = Color.Empty;
        }

        #endregion

        #region BackColorBlend

        /// <summary>
        /// Gets or sets the BackColorBlend.
        /// </summary>
        [Description("Indicates the BackColorBlend.")]
        public BackColorBlend BackColorBlend
        {
            get
            {
                if (_BackColorBlend == null)
                {
                    _BackColorBlend = new BackColorBlend();

                    UpgateChangeHandler(null, _BackColorBlend);
                }

                return (_BackColorBlend);
            }

            set
            {
                if (_BackColorBlend != value)
                {
                    UpgateChangeHandler(_BackColorBlend, value);

                    _BackColorBlend = value;

                    OnPropertyChangedEx("BackColorBlend");
                }
            }
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual bool ShouldSerializeBackColorBlend()
        {
            return (_BackColorBlend != null && _BackColorBlend.IsEmpty == false);
        }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        internal virtual void ResetBackColorBlend()
        {
            BackColorBlend = null;
        }

        #endregion

        #region GradientAngle

        /// <summary>
        /// Gets or sets the gradient angle (default is 90)
        /// </summary>
        [DefaultValue(90)]
        [Description("Indicates the Gradient Angle default is 90)")]
        public int GradientAngle
        {
            get { return (_GradientAngle); }

            set
            {
                if (_GradientAngle != value)
                {
                    _GradientAngle = value;

                    OnPropertyChangedEx("GradientAngle");
                }
            }
        }

        #endregion

        #region BackFillType

        /// <summary>
        /// Gets or sets the Gradient BackFillType
        /// </summary>
        [DefaultValue(BackFillType.Angle)]
        [Description("Indicates the Gradient BackFillType.")]
        public BackFillType BackFillType
        {
            get { return (_BackFillType); }

            set
            {
                if (_BackFillType != value)
                {
                    _BackFillType = value;

                    OnPropertyChangedEx("BackFillType");
                }
            }
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Gets whether both colors assigned are empty.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmpty
        {
            get
            {
                return (_Color1.IsEmpty && _Color2.IsEmpty && 
                    (_BackColorBlend == null || _BackColorBlend.IsEmpty));
            }
        }

        #endregion

        #region IsEqualTo

        /// <summary>
        /// Determines if the Background is equal to the given one
        /// </summary>
        /// <param name="background">Background to compare</param>
        /// <returns>true if equal</returns>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEqualTo(Background background)
        {
            return (_Color1 == background.Color1 &&
                    _Color2 == background.Color2 &&
                    _BackColorBlend.Equals(background._BackColorBlend) &&
                    _GradientAngle == background.GradientAngle &&
                    _BackFillType == background.BackFillType);
        }

        #endregion

        #endregion

        #region GetBrush

        ///<summary>
        /// GetBrush
        ///</summary>
        ///<param name="r"></param>
        ///<returns></returns>
        public Brush GetBrush(Rectangle r)
        {
            if (_BackColorBlend != null && _BackColorBlend.IsEmpty == false)
            {
                if (_BackColorBlend.Colors.Length == 1)
                    return (new SolidBrush(_BackColorBlend.Colors[0]));

                Brush br = GetLinearBrush(r);

                if (br is LinearGradientBrush)
                {
                    ColorBlend cb = GetColorBlend();

                    if (cb != null)
                        ((LinearGradientBrush) br).InterpolationColors = cb;
                }

                return (br);
            }

            if (_Color2.IsEmpty == true)
                return (new SolidBrush(_Color1));

            return (GetLinearBrush(r));
        }

        #region GetLinearBrush

        private Brush GetLinearBrush(Rectangle r)
        {
            LinearGradientBrush lbr = null;

            if (r.Width > 0 && r.Height > 0)
            {
                switch (_BackFillType)
                {
                    case BackFillType.Angle:
                        lbr = new LinearGradientBrush(r, _Color1, _Color2, _GradientAngle);
                        break;

                    case BackFillType.HorizontalCenter:
                        r.Width /= 2;
                        r.Width = Math.Max(1, r.Width);

                        lbr = new LinearGradientBrush(r, _Color1, _Color2, 0f);
                        break;

                    case BackFillType.VerticalCenter:
                        r.Height /= 2;
                        r.Height = Math.Max(1, r.Height);

                        lbr = new LinearGradientBrush(r, _Color1, _Color2, 90f);
                        break;

                    case BackFillType.ForwardDiagonal:
                        lbr = new LinearGradientBrush(r, _Color1, _Color2, LinearGradientMode.ForwardDiagonal);
                        break;

                    case BackFillType.BackwardDiagonal:
                        lbr = new LinearGradientBrush(r, _Color1, _Color2, LinearGradientMode.BackwardDiagonal);
                        break;

                    case BackFillType.ForwardDiagonalCenter:
                        r.Width /= 2;
                        r.Height /= 2;
                        lbr = new LinearGradientBrush(r, _Color1, _Color2, LinearGradientMode.ForwardDiagonal);
                        break;

                    case BackFillType.BackwardDiagonalCenter:
                        r.Width /= 2;
                        r.Height /= 2;
                        r.X += r.Width;

                        lbr = new LinearGradientBrush(r, _Color1, _Color2, LinearGradientMode.BackwardDiagonal);
                        break;

                    case BackFillType.Center:
                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddRectangle(r);

                            PathGradientBrush pbr = new PathGradientBrush(path);

                            pbr.CenterColor = _Color1;
                            pbr.SurroundColors = new Color[] {_Color2};

                            ColorBlend cb = GetColorBlend();

                            if (cb != null)
                                pbr.InterpolationColors = cb;

                            return (pbr);
                        }

                    case BackFillType.Radial:
                        int n = (int) Math.Sqrt(r.Width*r.Width + r.Height*r.Height) + 4;

                        using (GraphicsPath path = new GraphicsPath())
                        {
                            path.AddEllipse(r.X - (n - r.Width)/2, r.Y - (n - r.Height)/2, n, n);

                            PathGradientBrush pbr = new PathGradientBrush(path);

                            pbr.CenterColor = _Color1;
                            pbr.SurroundColors = new Color[] {_Color2};

                            ColorBlend cb = GetColorBlend();

                            if (cb != null)
                                pbr.InterpolationColors = cb;

                            return (pbr);
                        }
                }

                if (lbr != null)
                    lbr.WrapMode = WrapMode.TileFlipXY;
            }

            return (lbr);
        }

        #endregion

        #region GetColorBlend

        private ColorBlend GetColorBlend()
        {
            if (_BackColorBlend != null && 
                _BackColorBlend.Colors != null && _BackColorBlend.Colors.Length > 0)
            {
                ColorBlend cb = new ColorBlend(_BackColorBlend.Colors.Length);

                cb.Colors = _BackColorBlend.Colors;
                cb.Positions = GetPositions();

                return (cb);
            }

            return (null);
        }

        #endregion

        #region GetPositions

        private float[] GetPositions()
        {
            float[] cp = _BackColorBlend.Positions;

            if (cp == null || cp.Length != _BackColorBlend.Colors.Length)
            {
                cp = new float[_BackColorBlend.Colors.Length];

                float f = 1f / _BackColorBlend.Colors.Length;

                for (int i = 0; i < cp.Length; i++)
                    cp[i] = i * f;

                cp[_BackColorBlend.Colors.Length - 1] = 1;
            }

            return (cp);
        }

        #endregion

        #endregion

        #region Copy

        /// <summary>
        /// Creates an exact copy of the background.
        /// </summary>
        /// <returns>Copy of the background.</returns>
        public Background Copy()
        {
            Background copy = new Background();

            copy.Color1 = _Color1;
            copy.Color2 = _Color2;

            copy.GradientAngle = _GradientAngle;
            copy.BackFillType = _BackFillType;

            if (_BackColorBlend != null)
                copy.BackColorBlend = _BackColorBlend.Copy();

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
        protected virtual void OnPropertyChanged(VisualPropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eh = PropertyChanged;

            if (eh != null)
                eh(this, e);
        }

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="s">Event arguments</param>
        protected virtual void OnPropertyChangedEx(string s)
        {
            PropertyChangedEventHandler eh = PropertyChanged;

            if (eh != null)
            {
                VisualPropertyChangedEventArgs e =
                    new VisualPropertyChangedEventArgs(s);

                eh(this, e);
            }
        }

        #endregion

        #region UpgateChangeHandler

        private void UpgateChangeHandler(
            INotifyPropertyChanged oldValue, INotifyPropertyChanged newValue)
        {
            if (oldValue != null)
                oldValue.PropertyChanged -= StyleChanged;

            if (newValue != null)
                newValue.PropertyChanged += StyleChanged;
        }

        #endregion

        #region StyleChanged

        protected virtual void StyleChanged(
            object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged((VisualPropertyChangedEventArgs)e);
        }

        #endregion
    }

    #region enums

    ///<summary>
    /// BackFillType
    ///</summary>
    public enum BackFillType
    {
        ///<summary>
        /// Angle
        ///</summary>
        Angle,

        ///<summary>
        /// Center
        ///</summary>
        Center,

        ///<summary>
        /// HorizontalCenter
        ///</summary>
        HorizontalCenter,

        ///<summary>
        /// VerticalCenter
        ///</summary>
        VerticalCenter,

        ///<summary>
        /// ForwardDiagonal
        ///</summary>
        ForwardDiagonal,

        ///<summary>
        /// BackwardDiagonal
        ///</summary>
        BackwardDiagonal,

        ///<summary>
        /// ForwardDiagonalCenter
        ///</summary>
        ForwardDiagonalCenter,

        ///<summary>
        /// BackwardDiagonalCenter
        ///</summary>
        BackwardDiagonalCenter,

        ///<summary>
        /// Radial
        ///</summary>
        Radial,
    }

    #endregion

    #region BackgroundConvertor

    ///<summary>
    /// BackgroundConvertor
    ///</summary>
    public class BackgroundConvertor : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
            {
                Background gc = value as Background;

                if (gc != null)
                {
                    if (gc.BackColorBlend != null && gc.BackColorBlend.IsEmpty == false)
                        return ("Blended");

                    ColorConverter cvt = new ColorConverter();

                    string s = gc.Color1.IsEmpty ? "(Empty)" : cvt.ConvertToString(gc.Color1);

                    if (gc.Color2 != Color.Empty)
                        s += ", " + cvt.ConvertToString(gc.Color2);

                    return (s);
                }
            }

            return (base.ConvertTo(context, culture, value, destinationType));
        }
    }

    #endregion

    #region BackgroundEditor

    ///<summary>
    /// BackgroundEditor
    ///</summary>
    public class BackgroundEditor : UITypeEditor
    {
        #region GetPaintValueSupported

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return (true);
        }

        #endregion

        #region PaintValue

        public override void PaintValue(PaintValueEventArgs e)
        {
            Background b = e.Value as Background;

            if (b != null)
            {
                using (Brush br = b.GetBrush(e.Bounds))
                    e.Graphics.FillRectangle(br, e.Bounds);
            }
        }

        #endregion
    }

    #endregion
}
