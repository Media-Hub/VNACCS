using System;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// BackColorBlend
    ///</summary>
    [TypeConverter(typeof(BackColorBlendConvertor))]
    public class BackColorBlend : INotifyPropertyChanged
    {
        #region Private variables

        private Color[] _Colors;        // Color values
        private float[] _Positions;     // Gradient color positions

        #endregion

        #region Public properties

        #region Colors

        /// <summary>
        /// Gets or sets the ColorBlend Color array
        /// </summary>
        [Browsable(true), DefaultValue(null)]
        [Description("Indicates the ColorBlend Color array")]
        [TypeConverter(typeof(ArrayConverter))]
        public Color[] Colors
        {
            get { return (_Colors); }

            set
            {
                if (value != null && value.Length == 0)
                    value = null;

                _Colors = value;

                OnPropertyChangedEx("Colors");
            }
        }

        #endregion

        #region Positions

        /// <summary>
        /// Gets or sets the ColorBlend Color Positions
        /// </summary>
        [Browsable(true), DefaultValue(null)]
        [Description("Indicates the ColorBlend Color Positions")]
        [TypeConverter(typeof(ArrayConverter))]
        public float[] Positions
        {
            get { return (_Positions); }

            set
            {
                if (value != null && value.Length == 0)
                    value = null;
                
                _Positions = value;

                OnPropertyChangedEx("Positions");
            }
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// IsEmpty
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsEmpty
        {
            get { return (_Colors == null || _Colors.Length == 1 && _Colors[0].IsEmpty); }
        }

        #endregion

        #endregion

        #region Copy

        /// <summary>
        /// Creates an exact copy of the BackColorBlend.
        /// </summary>
        /// <returns>Copy of the BackColorBlend.</returns>
        public BackColorBlend Copy()
        {
            BackColorBlend copy = new BackColorBlend();

            if (_Colors != null)
            {
                copy.Colors = new Color[_Colors.Length];

                _Colors.CopyTo(copy.Colors, 0);
            }

            if (_Positions != null)
            {
                copy.Positions = new float[_Positions.Length];

                _Positions.CopyTo(copy.Positions, 0);
            }

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
    }

    #region BackColorBlendConvertor

        /// <summary>
        /// BackColorBlendConvertor
        /// </summary>
        public class BackColorBlendConvertor : ExpandableObjectConverter
        {
            public override object ConvertTo(
                ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
            {
                if (destinationType == typeof(string))
                {
                    BackColorBlend cb = value as BackColorBlend;

                    if (cb != null)
                    {
                        ColorConverter cvt = new ColorConverter();

                        if (cb.Colors != null)
                        {
                            if (cb.Colors[0] != Color.Empty)
                                return (cvt.ConvertToString(cb.Colors[0]));

                            if (cb.Colors.Length > 1 && cb.Colors[1] != Color.Empty)
                                return (cvt.ConvertToString(cb.Colors[1]));
                        }
                    }

                    return (String.Empty);
                }

                return (base.ConvertTo(context, culture, value, destinationType));
            }
        }

        #endregion
}
