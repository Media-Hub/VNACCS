using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using DevComponents.DotNetBar.Rendering;
using DevComponents.DotNetBar.Metro.Helpers;

namespace DevComponents.DotNetBar.Metro.ColorTables
{
    /// <summary>
    /// Initialized the Metro UI color table based on colors specified.
    /// </summary>
    internal class MetroColorTableInitializer
    {
        public static MetroColorTable CreateColorTable(MetroColorGeneratorParameters colorParams)
        {
            return CreateColorTable(colorParams.CanvasColor, colorParams.BaseColor);
        }
        public static MetroColorTable CreateColorTable(Color canvasColor, Color baseColor)
        {
            MetroColorTable metroTable = new MetroColorTable();
            Office2007ColorTable officeMetroColorTable = new Office2007ColorTable();
            CreateColors(metroTable, officeMetroColorTable, canvasColor, baseColor);
            if(StyleManager.Style == eStyle.Metro)
                ((Office2007Renderer)GlobalManager.Renderer).ColorTable = officeMetroColorTable;
            return metroTable;
        }

        private const double DEGREE = 1d / 360;
        private static double GetHue(double hue, int addDegrees)
        {
            hue += addDegrees * DEGREE;
            if (hue > 1)
                hue -= 1;
            else if (hue < 0)
                hue += 1;
            return hue;
        }
        private static double GetComplementSaturation(double saturation)
        {
            if (saturation == 1) return 1;

            if (saturation < .81d)
                return saturation + .1d;
            if (saturation < .91d)
                return saturation + .05d;
            return saturation + (.99d - saturation);
        }
        public static MetroPartColors CreateMetroPartColors(Color canvasColor, Color baseColor)
        {
            ColorFunctions.HLSColor canvasHsl = ColorFunctions.RGBToHSL(canvasColor);
            ColorFunctions.HLSColor baseHsl = ColorFunctions.RGBToHSL(baseColor);
            HSVColor baseHsv = ColorHelpers.ColorToHSV(baseColor);
            HSVColor canvasHsv = ColorHelpers.ColorToHSV(canvasColor);

            // Create metro colors
            MetroPartColors partColors = new MetroPartColors();
            partColors.CanvasColor = canvasColor;
            partColors.BaseColor = baseColor;
            partColors.TextColor = (canvasHsl.Lightness < .4) ? Color.White : Color.Black;
            //HSVColor textHsv = ColorHelpers.ColorToHSV(partColors.TextColor);
            partColors.TextInactiveColor = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (partColors.TextColor == Color.White ? canvasHsv.Value + .53 : canvasHsv.Value - .47));
            partColors.TextDisabledColor = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (partColors.TextColor == Color.Black ? canvasHsv.Value - .33 : canvasHsv.Value + .67));
            partColors.TextLightColor = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (partColors.TextColor == Color.Black ? canvasHsv.Value - .6 : canvasHsv.Value + .4));
            partColors.CanvasColorDarkShade = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (partColors.TextColor == Color.Black ? canvasHsv.Value - .33 : canvasHsv.Value + .67));
            partColors.CanvasColorLightShade = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (partColors.TextColor == Color.White ? canvasHsv.Value + .17 : canvasHsv.Value - .17));
            partColors.CanvasColorLighterShade = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (partColors.TextColor == Color.White ? canvasHsv.Value + .05 : canvasHsv.Value - .05));
            partColors.BaseTextColor = GetTextColor(baseColor);

            partColors.BaseColorLight = ColorHelpers.HSVToColor(baseHsv.Hue, GetColorMin(0.08, baseHsv.Saturation - .41), baseHsv.Value + .3);
            partColors.BaseColorLight1 = ColorHelpers.HSVToColor(baseHsv.Hue, GetColorMin(0.05, baseHsv.Saturation - .06), baseHsv.Value + .11);
            partColors.BaseColorLightText = GetTextColor(partColors.BaseColorLight);

            partColors.BaseColorLightest = ColorHelpers.HSVToColor(baseHsv.Hue, GetColorMin(0.05, baseHsv.Saturation - .6), baseHsv.Value + .5);
            partColors.BaseColorLighter = ColorHelpers.HSVToColor(baseHsv.Hue, GetColorMin(0.08, baseHsv.Saturation - .46), baseHsv.Value + .39);
            partColors.BaseColorDark = ColorHelpers.HSVToColor(baseHsv.Hue, baseHsv.Saturation + .1, baseHsv.Value - .06);
            partColors.BaseColorDarker = ColorFunctions.HLSToRGB(baseHsl.Hue, baseHsl.Lightness - .2, baseHsl.Saturation);

            partColors.ComplementColor = ColorHelpers.HSVToColor(GetComplementHue(baseHsv.Hue), baseHsv.Saturation, baseHsv.Value + (baseHsv.Value > .5d ? -.35 : 0d));
            ColorFunctions.HLSColor compHsl = ColorFunctions.RGBToHSL(partColors.ComplementColor);
            HSVColor compHsv = ColorHelpers.ColorToHSV(partColors.ComplementColor);
            partColors.ComplementColorLight = ColorHelpers.HSVToColor(compHsv.Hue, compHsv.Saturation, compHsv.Value + .2d);
            partColors.ComplementColorDark = ColorFunctions.HLSToRGB(compHsl.Hue, compHsl.Lightness - .10, compHsl.Saturation);
            partColors.ComplementColorDarker = ColorFunctions.HLSToRGB(compHsl.Hue, compHsl.Lightness - .2, compHsl.Saturation);
            partColors.ComplementColorText = GetTextColor(partColors.ComplementColor);
            partColors.ComplementColorLightText = GetTextColor(partColors.ComplementColorLight);

            partColors.BaseButtonGradientStart = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (canvasHsv.Value > 0 ? canvasHsv.Value - .01 : canvasHsv.Value + .08));
            partColors.BaseButtonGradientEnd = ColorHelpers.HSVToColor(canvasHsv.Hue, canvasHsv.Saturation, (canvasHsv.Value > 0 ? canvasHsv.Value - .08 : canvasHsv.Value + .20));

            return partColors;
        }
        public static void CreateColors(MetroColorTable metroTable, Office2007ColorTable officeMetroColorTable, Color canvasColor, Color baseColor)
        {
            MetroPartColors partColors = CreateMetroPartColors(canvasColor, baseColor);

            metroTable.CanvasColor = partColors.CanvasColor;
            metroTable.CanvasColorShadeLight = partColors.CanvasColorLightShade;
            metroTable.CanvasColorShadeLighter = partColors.CanvasColorLighterShade;
            metroTable.ForeColor = partColors.TextColor;
            metroTable.BaseColor = baseColor;
            metroTable.MetroPartColors = partColors;

            metroTable.MetroAppForm.BorderThickness = new Thickness(0, 0, 1, 1);
            metroTable.MetroAppForm.BorderColors = new BorderColors(partColors.BaseColor);
            metroTable.MetroAppForm.BorderColorsInactive = new BorderColors(partColors.BaseColorLight);

            metroTable.MetroForm.BorderThickness = new Thickness(3, 3, 3, 3);
            metroTable.MetroForm.BorderColors = new BorderColors[3] { new BorderColors(partColors.BaseColorDark), new BorderColors(partColors.BaseColor), new BorderColors(partColors.BaseColor) };
            metroTable.MetroForm.BorderColorsInactive = new BorderColors[4] { new BorderColors(partColors.BaseColorLight), new BorderColors(partColors.BaseColorLight), new BorderColors(partColors.BaseColorLight), new BorderColors(partColors.BaseColorLight) };

            metroTable.MetroTab.ActiveCaptionText = partColors.TextInactiveColor;
            metroTable.MetroTab.InactiveCaptionText = partColors.TextDisabledColor;

            metroTable.MetroTab.MetroTabItem.Default = GetMetroTabItemStateTable(partColors.TextInactiveColor);
            metroTable.MetroTab.MetroTabItem.Selected = GetMetroTabItemStateTable(partColors.BaseColor);
            metroTable.MetroTab.MetroTabItem.Disabled = GetMetroTabItemStateTable(partColors.TextDisabledColor);
            metroTable.MetroTab.MetroTabItem.MouseOver = GetMetroTabItemStateTable(partColors.TextInactiveColor, partColors.BaseColor);
            metroTable.MetroTab.MetroTabItem.Pressed = null;

            metroTable.MetroTab.TabPanelBackgroundStyle = new ElementStyle(partColors.TextColor, partColors.CanvasColor);

            // Toolbar
            metroTable.MetroToolbar.BackgroundStyle = new ElementStyle(partColors.TextColor, partColors.CanvasColor);

            // Status Bar
            metroTable.MetroStatusBar.BackgroundStyle = new ElementStyle(partColors.TextColor, partColors.BaseColor);
            metroTable.MetroStatusBar.TopBorders = new Color[0];
            metroTable.MetroStatusBar.BottomBorders = new Color[0];
            metroTable.MetroStatusBar.ResizeMarkerLightColor = Color.FromArgb(92, Color.White);
            metroTable.MetroStatusBar.ResizeMarkerColor = partColors.BaseColorDark;

            MetroOfficeColorTableInitializer.InitializeColorTable(officeMetroColorTable, ColorFactory.Empty, partColors);
        }
        private static double GetColorMin(double minValue, double value)
        {
            if (value < 0)
                return minValue;
            return value;
        }

        private static Color GetTextColor(Color color)
        {
            ColorFunctions.HLSColor hslColor = ColorFunctions.RGBToHSL(color);
            return hslColor.Lightness < .65 ? Color.White : Color.Black;
            //HSVColor hsvColor = ColorHelpers.ColorToHSV(backColor);
            //return hsvColor.Value < .65 ? Color.White : Color.Black;
        }

        private static double GetComplementHue(double hue)
        {
            if (hue <= .37777d)
            {
                return Math.Min(1, hue + (137 * DEGREE + (86 * DEGREE * (hue / .37777d))));
            }
            else
            {
                return Math.Max(0, hue - (137 * DEGREE + (86 * DEGREE * (hue / .6222222d))));//     ((137 + 86 * DEGREE * (hue / .6222222) * DEGREE)));
            }
        }

        private static MetroTabItemStateColorTable GetMetroTabItemStateTable(Color textColor)
        {
            return GetMetroTabItemStateTable(textColor, Color.Empty);
        }
        private static MetroTabItemStateColorTable GetMetroTabItemStateTable(Color textColor, Color bottomBorderColor)
        {
            ElementStyle style = new ElementStyle(textColor);
            style.TextAlignment = eStyleTextAlignment.Center;
            style.TextLineAlignment = eStyleTextAlignment.Center;
            style.HideMnemonic = true;
            if (!bottomBorderColor.IsEmpty)
            {
                style.BorderBottom = eStyleBorderType.Solid;
                style.BorderBottomWidth = 2;
                style.BorderBottomColor = bottomBorderColor;
            }
            return new MetroTabItemStateColorTable(style);
        }
    }
    /// <summary>
    /// Defines base set of Metro UI color scheme.
    /// </summary>
    public class MetroPartColors
    {
        /// <summary>
        /// Gets or sets the base canvas color, like form background.
        /// </summary>
        public Color CanvasColor;
        /// <summary>
        /// Gets or sets the chrome base color, used for window border, selection marking etc.
        /// </summary>
        public Color BaseColor;
        /// <summary>
        /// Gets or sets the text color for text displayed over the BaseColor.
        /// </summary>
        public Color BaseTextColor;
        /// <summary>
        /// Gets or sets the text color displayed over the canvas color.
        /// </summary>
        public Color TextColor;
        /// <summary>
        /// Gets or sets the lighter text color used for example for inactive non selected tab text etc.
        /// </summary>
        public Color TextInactiveColor;
        /// <summary>
        /// Gets or sets the text color used for disabled text.
        /// </summary>
        public Color TextDisabledColor;
        /// <summary>
        /// Gets or sets the text light color.
        /// </summary>
        public Color TextLightColor;
        /// <summary>
        /// Gets or sets the color that is in darker shade off of the canvas color.
        /// </summary>
        public Color CanvasColorDarkShade;
        /// <summary>
        /// Gets or sets the color that is in light shade off of the canvas color.
        /// </summary>
        public Color CanvasColorLightShade;
        /// <summary>
        /// Gets or sets the color that is in lighter shade off of the canvas color.
        /// </summary>
        public Color CanvasColorLighterShade;
        /// <summary>
        /// Gets or sets the light base color shade.
        /// </summary>
        public Color BaseColorLight;
        /// <summary>
        /// Gets or sets the just a tad lighter base color.
        /// </summary>
        public Color BaseColorLight1;
        /// <summary>
        /// Gets or sets the text color for light base color.
        /// </summary>
        public Color BaseColorLightText;
        /// <summary>
        /// Gets or sets the lighter base color shade.
        /// </summary>
        public Color BaseColorLighter;
        /// <summary>
        /// Gets or sets the lightest base color shade.
        /// </summary>
        public Color BaseColorLightest;
        /// <summary>
        /// Gets or sets the dark base color shade.
        /// </summary>
        public Color BaseColorDark;
        /// <summary>
        /// Gets or sets the darker base color shade.
        /// </summary>
        public Color BaseColorDarker;
        /// <summary>
        /// Gets or sets the base color analogous color 1
        /// </summary>
        public Color ComplementColor;
        /// <summary>
        /// Gets or sets the Analogous color light variant.
        /// </summary>
        public Color ComplementColorLight;
        /// <summary>
        /// Gets or sets the text color for Analogous color light variant.
        /// </summary>
        public Color ComplementColorLightText;
        /// <summary>
        /// Gets or sets the Analogous color dark variant.
        /// </summary>
        public Color ComplementColorDark;
        /// <summary>
        /// Gets or sets the Analogous color darker variant.
        /// </summary>
        public Color ComplementColorDarker;
        /// <summary>
        /// Gets or sets the Analogous color text color.
        /// </summary>
        public Color ComplementColorText;
        /// <summary>
        /// Gets or sets the off base color button gradient start.
        /// </summary>
        public Color BaseButtonGradientStart;
        /// <summary>
        /// Gets or sets the off base color button gradient start.
        /// </summary>
        public Color BaseButtonGradientEnd;

    }
}
