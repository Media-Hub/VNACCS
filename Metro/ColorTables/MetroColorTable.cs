using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Globalization;
using System.Reflection;
using System.Collections;

namespace DevComponents.DotNetBar.Metro.ColorTables
{
    public class MetroColorTable
    {
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public Color CanvasColor = Color.White;
        /// <summary>
        /// Gets or sets the canvas light shade color. This property is not directly used but it is provided for reference.
        /// </summary>
        public Color CanvasColorShadeLight = Color.White;
        /// <summary>
        /// Gets or sets the canvas lighter shade color. This property is not directly used but it is provided for reference.
        /// </summary>
        public Color CanvasColorShadeLighter = Color.White;

        /// <summary>
        /// Gets or sets the foreground color.
        /// </summary>
        public Color ForeColor = Color.Black;
        /// <summary>
        /// Gets or sets the base metro color.
        /// </summary>
        public Color BaseColor = Color.Black;

        /// <summary>
        /// Gets or sets the color table for MetroAppForm.
        /// </summary>
        public MetroAppFormColorTable MetroAppForm = new MetroAppFormColorTable();

        /// <summary>
        /// Gets or sets the color table for MetroForm.
        /// </summary>
        public MetroFormColorTable MetroForm = new MetroFormColorTable();

        /// <summary>
        /// Gets or sets the color table for MetroTab.
        /// </summary>
        public MetroTabColorTable MetroTab = new MetroTabColorTable();

        /// <summary>
        /// Gets or sets color table for MetroStatusBar.
        /// </summary>
        public MetroStatusBarColorTable MetroStatusBar = new MetroStatusBarColorTable();

        /// <summary>
        /// Gets or sets color table for MetroToolbar.
        /// </summary>
        public MetroToolbarColorTable MetroToolbar = new MetroToolbarColorTable();

        /// <summary>
        /// Gets or sets the color table used by MetroTile items.
        /// </summary>
        public MetroTileColorTable MetroTile = new MetroTileColorTable();

        /// <summary>
        /// Gets or sets the metro-part colors that define the metro UI color scheme. These colors are provided for you reference and reuse in your app.
        /// </summary>
        public MetroPartColors MetroPartColors = new MetroPartColors();
    }

    /// <summary>
    /// Represents class that defines parameters for Metro style color scheme.
    /// </summary>
    [TypeConverterAttribute(typeof(MetroColorGeneratorParametersConverter))]
    public struct MetroColorGeneratorParameters
    {
        #region Implementation
        /// <summary>
        /// Initializes a new instance of the MetroStyleParameters class.
        /// </summary>
        /// <param name="canvasColor">Canvas color.</param>
        /// <param name="baseColor">Base color.</param>
        public MetroColorGeneratorParameters(Color canvasColor, Color baseColor)
        {
            if (canvasColor.IsEmpty) canvasColor = DefaultCanvasColor;
            if (baseColor.IsEmpty) baseColor = DefaultBaseColor;
            _CanvasColor = canvasColor;
            _BaseColor = baseColor;
            _ThemeName = "";
        }

        /// <summary>
        /// Initializes a new instance of the MetroColorGeneratorParameters structure.
        /// </summary>
        /// <param name="canvasColor">Canvas color.</param>
        /// <param name="baseColor">Base color.</param>
        /// <param name="themeName">User friendly theme name.</param>
        public MetroColorGeneratorParameters(Color canvasColor, Color baseColor, string themeName)
        {
            _CanvasColor = canvasColor;
            _BaseColor = baseColor;
            _ThemeName = themeName;
        }

        /// <summary>
        /// Returns array of all predefined Metro color themes.
        /// </summary>
        /// <returns></returns>
        public static MetroColorGeneratorParameters[] GetAllPredefinedThemes()
        {
            Type t = typeof(MetroColorGeneratorParameters);
            PropertyInfo[] members = t.GetProperties(BindingFlags.Public | BindingFlags.Static);
            List<MetroColorGeneratorParameters> themes = new List<MetroColorGeneratorParameters>();
            foreach (PropertyInfo mem in members)
            {
                if (mem.PropertyType == t)
                {
                    themes.Add((MetroColorGeneratorParameters)mem.GetValue(null, null));
                }
            }
            return themes.ToArray();
        }

        private static readonly Color DefaultCanvasColor = Color.White;
        private Color _CanvasColor;
        /// <summary>
        /// Gets or sets the color of the Metro canvas.
        /// </summary>
        [Category("Columns"), Description("Indicates color of the metro Canvas.")]
        public Color CanvasColor
        {
            get { return _CanvasColor; }
            set { _CanvasColor = value; }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCanvasColor()
        {
            return _CanvasColor != DefaultCanvasColor;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetCanvasColor()
        {
            this.CanvasColor = DefaultCanvasColor;
        }

        private static readonly Color DefaultBaseColor = ColorScheme.GetColor("FFA31A");
        private Color _BaseColor;
        /// <summary>
        /// Gets or sets the base color for the Metro style.
        /// </summary>
        [Category("Columns"), Description("Indicates base color of the Metro style.")]
        public Color BaseColor
        {
            get { return _BaseColor; }
            set { _BaseColor = value; }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeBaseColor()
        {
            return _BaseColor != DefaultBaseColor;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetBaseColor()
        {
            this.BaseColor = DefaultBaseColor;
        }

        private string _ThemeName;
        /// <summary>
        /// Gets or sets the user friendly theme name.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string ThemeName
        {
            get { return _ThemeName; }
            set { _ThemeName = value; }
        }
        #endregion

        #region Static Color Schemes
        public static MetroColorGeneratorParameters Default { get { return new MetroColorGeneratorParameters(Color.White, ColorScheme.GetColor("2B579A"), "Default"); } }
        public static MetroColorGeneratorParameters Purple { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("6A3889"), "Purple"); } }
        public static MetroColorGeneratorParameters Red { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("CF4926"), "Red"); } }
        public static MetroColorGeneratorParameters Green { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("217346"), "Green"); } }
        public static MetroColorGeneratorParameters Blue { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("0072C6"), "Blue"); } }
        public static MetroColorGeneratorParameters DarkPurple { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("80397B"), "DarkPurple"); } }
        public static MetroColorGeneratorParameters RedAmplified { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("D24726"), "RedAmplified"); } }
        public static MetroColorGeneratorParameters ForestGreen { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("077568"), "ForestGreen"); } }
        
        public static MetroColorGeneratorParameters Brown { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor(0xE7DEC0), ColorScheme.GetColor("6D4320"), "Brown");}}
        public static MetroColorGeneratorParameters DarkRed { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("F2F2F2"), ColorScheme.GetColor("A4373A"), "Dark Red"); } }
        public static MetroColorGeneratorParameters Orange { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("ED8E00"), "Orange"); } }
        public static MetroColorGeneratorParameters BlackMaroon { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("1C190F"), ColorScheme.GetColor("8A2F25"), "Black Maroon");}}
        public static MetroColorGeneratorParameters BlackSky { get { return new MetroColorGeneratorParameters(Color.Black, ColorScheme.GetColor("00A7FF"), "Black Sky");}}
        public static MetroColorGeneratorParameters BlackClouds { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("000000"), ColorScheme.GetColor("9B96A3"), "Black Clouds");}}
        public static MetroColorGeneratorParameters BlackLilac { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("191919"), ColorScheme.GetColor("D8CAFF"), "Black Lilac");}}
        public static MetroColorGeneratorParameters Bordeaux { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("F2EBDC"), ColorScheme.GetColor("960E00"), "Bordeaux");}}
        public static MetroColorGeneratorParameters Rust { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("B4B48D"), ColorScheme.GetColor("A94D2D"), "Rust");}}
        public static MetroColorGeneratorParameters Espresso { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("AB9667"), ColorScheme.GetColor("383025"), "Espresso");}}
        public static MetroColorGeneratorParameters PowderRed { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("D0D9D6"), ColorScheme.GetColor("BF0404"), "Powder Red");}}
        public static MetroColorGeneratorParameters BlueishBrown { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("D5D5E0"), ColorScheme.GetColor("3D1429"), "Blueish Brown");}}
        public static MetroColorGeneratorParameters SilverGreen { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("F2F2F2"), ColorScheme.GetColor("588C3F"), "Silver Green");}}
        public static MetroColorGeneratorParameters MaroonSilver { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("260B01"), ColorScheme.GetColor("F2F2F2"), "Maroon Silver");}}
        public static MetroColorGeneratorParameters SkyGreen { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("BDD5D0"), ColorScheme.GetColor("04ADBF"), "Sky Green");}}
        public static MetroColorGeneratorParameters DarkBrown { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("D9B573"), ColorScheme.GetColor("261105"), "Dark Brown");}}
        public static MetroColorGeneratorParameters Latte { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("B18D5A"), ColorScheme.GetColor("851A1D"), "Latte");}}
        public static MetroColorGeneratorParameters Cherry { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("DECDAE"), ColorScheme.GetColor("5A0D12"), "Cherry");}}
        public static MetroColorGeneratorParameters GrayOrange { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("E8E8DC"), ColorScheme.GetColor("FF8600"), "Gray Orange");}}
        public static MetroColorGeneratorParameters EarlyRed { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FCFCF0"), ColorScheme.GetColor("C93C00"), "Early Red");}}
        public static MetroColorGeneratorParameters EarlyMaroon { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FCFCF0"), ColorScheme.GetColor("730046"), "Early Maroon");}}
        public static MetroColorGeneratorParameters EarlyOrange { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FCFCF0"), ColorScheme.GetColor("E88801"), "Early Orange");}}
        public static MetroColorGeneratorParameters SilverBlues { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("dcdcdc"), ColorScheme.GetColor("002395"), "Silver Blues");}}
        public static MetroColorGeneratorParameters RetroBlue { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FEFEFE"), ColorScheme.GetColor("3E788F"), "Retro Blue");}}
        public static MetroColorGeneratorParameters LatteRed { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("A77A51"), ColorScheme.GetColor("8C170D"), "Latte Red");}}
        public static MetroColorGeneratorParameters LatteDarkSteel { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("A77A51"), ColorScheme.GetColor("2C2D40"), "Latte Dark Steel");}}
        public static MetroColorGeneratorParameters SimplyBlue { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("E2E2E2"), ColorScheme.GetColor("215BA6"), "Simply Blue");}}
        public static MetroColorGeneratorParameters WashedBlue { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("B8CDD1"), ColorScheme.GetColor("B4090A"), "Washed Blue");}}
        public static MetroColorGeneratorParameters WashedWhite { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FEFFF8"), ColorScheme.GetColor("92AAB1"), "Washed White");}}
        public static MetroColorGeneratorParameters NapaRed { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("F2F2F2"), ColorScheme.GetColor("A64B29"), "Napa Red");}}
        public static MetroColorGeneratorParameters Magenta { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("7F3B78"), "Magenta"); } }
        public static MetroColorGeneratorParameters DarkBlue { get { return new MetroColorGeneratorParameters(ColorScheme.GetColor("FFFFFF"), ColorScheme.GetColor("2B569A"), "Dark Blue"); } }
        
        

        #endregion
    }
    #region MetroColorGeneratorParameters
    public class MetroColorGeneratorParametersConverter : TypeConverter
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
            if (strArray.Length != 2)
            {
                throw new ArgumentException("String parsing failed due to incorrect format.");
            }

            MetroColorGeneratorParameters colorParams = new MetroColorGeneratorParameters();
            TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));

            colorParams.CanvasColor = (Color)converter.ConvertFromString(context, culture, strArray[0]);
            colorParams.BaseColor = (Color)converter.ConvertFromString(context, culture, strArray[1]);
            
            return colorParams;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
            {
                throw new ArgumentNullException("destinationType");
            }
            if (value is MetroColorGeneratorParameters)
            {
                if (destinationType == typeof(string))
                {
                    MetroColorGeneratorParameters colorParams = (MetroColorGeneratorParameters)value;
                    if (culture == null)
                    {
                        culture = CultureInfo.CurrentCulture;
                    }
                    string separator = culture.TextInfo.ListSeparator + " ";
                    //TypeConverter converter = TypeDescriptor.GetConverter(typeof(Color));
                    string[] strArray = new string[2];
                    int num = 0;
                    strArray[num++] = ColorToString(culture, colorParams.CanvasColor); // converter.ConvertToString(context, culture, colorParams.CanvasColor);
                    strArray[num++] = ColorToString(culture, colorParams.BaseColor); // converter.ConvertToString(context, culture, colorParams.BaseColor);
                    return string.Join(separator, strArray);
                }
                if (destinationType == typeof(InstanceDescriptor))
                {
                    MetroColorGeneratorParameters colorParams = (MetroColorGeneratorParameters)value;
                    ConstructorInfo constructor = typeof(MetroColorGeneratorParameters).GetConstructor(new Type[] { typeof(Color), typeof(Color) });
                    if (constructor != null)
                    {
                        return new InstanceDescriptor(constructor, new object[] { colorParams.CanvasColor, colorParams.BaseColor });
                    }
                }
            }
            return base.ConvertTo(context, culture, value, destinationType);
        }

        private static string ColorToString(CultureInfo culture, Color color)
        {
            string[] strArray;
            if (color == Color.Empty)
            {
                return string.Empty;
            }
            if (color.IsKnownColor)
            {
                return color.Name;
            }
            if (color.IsNamedColor)
            {
                return ("'" + color.Name + "'");
            }
            if (culture == null)
            {
                culture = CultureInfo.CurrentCulture;
            }
            string separator = "";
            int num = 0;
            if (color.A < 0xff)
            {
                strArray = new string[4];
                strArray[num++] = color.A.ToString("X");
            }
            else
            {
                strArray = new string[3];
            }
            strArray[num++] = color.R.ToString("X");
            strArray[num++] = color.G.ToString("X");
            strArray[num++] = color.B.ToString("X");
            return "#" + string.Join(separator, strArray);

        }

        public override object CreateInstance(ITypeDescriptorContext context, IDictionary propertyValues)
        {
            if (propertyValues == null)
            {
                throw new ArgumentNullException("propertyValues");
            }
            object canvasColor = propertyValues["CanvasColor"];
            object baseColor = propertyValues["BaseColor"];
            if (canvasColor == null || !(canvasColor is Color) || baseColor==null || !(baseColor is Color))
            {
                throw new ArgumentException("Invalid property value entry");
            }
            return new MetroColorGeneratorParameters((Color)canvasColor, (Color)baseColor);
        }

        public override bool GetCreateInstanceSupported(ITypeDescriptorContext context)
        {
            return true;
        }

        public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
        {
            return TypeDescriptor.GetProperties(typeof(MetroColorGeneratorParameters), attributes).Sort(new string[] { "CanvasColor", "BaseColor" });
        }

        public override bool GetPropertiesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
    #endregion
 

}
