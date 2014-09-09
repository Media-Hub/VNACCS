using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.Helpers
{
    internal static class ColorHelpers
    {
        public static HSVColor ColorToHSV(Color color)
        {
            double var_R = ((double)color.R / 255);                     //RGB from 0 to 255
            double var_G = ((double)color.G / 255);
            double var_B = ((double)color.B / 255);

            double var_Min = Min(var_R, var_G, var_B);    //Min. value of RGB
            double var_Max = Max(var_R, var_G, var_B);    //Max. value of RGB
            double del_Max = var_Max - var_Min;             //Delta RGB value

            HSVColor hsv = new HSVColor();

            hsv.Value = var_Max;

            if (del_Max == 0)                     //This is a gray, no chroma...
            {
                hsv.Hue = 0;                                //HSV results from 0 to 1
                hsv.Saturation = 0;
            }
            else                                    //Chromatic data...
            {
                hsv.Saturation = del_Max / var_Max;

                double del_R = (((var_Max - var_R) / 6) + (del_Max / 2)) / del_Max;
                double del_G = (((var_Max - var_G) / 6) + (del_Max / 2)) / del_Max;
                double del_B = (((var_Max - var_B) / 6) + (del_Max / 2)) / del_Max;

                if (var_R == var_Max)
                    hsv.Hue = del_B - del_G;
                else if (var_G == var_Max)
                    hsv.Hue = (1d / 3d) + del_R - del_B;
                else if (var_B == var_Max)
                    hsv.Hue = (2d / 3d) + del_G - del_R;

                if (hsv.Hue < 0) hsv.Hue += 1;
                if (hsv.Hue > 1) hsv.Hue -= 1;
            }

            return hsv;
        }

        public static Color HSVToColor(double h, double s, double v)
        {
            return HSVToColor(new HSVColor(h, s, v));
        }

        public static Color HSVToColor(HSVColor hsv)
        {
            Color color;
            if (hsv.Value < 0)
                hsv.Value = 0;
            else if (hsv.Value > 1)
                hsv.Value = 1;
            if (hsv.Saturation > 1)
                hsv.Saturation = 1;
            else if (hsv.Saturation < 0)
                hsv.Saturation = 0;
            if (hsv.Saturation == 0)                       //HSV from 0 to 1
            {
                color = Color.FromArgb((int)(hsv.Value * 255), (int)(hsv.Value * 255), (int)(hsv.Value * 255));
            }
            else
            {
                double var_h = hsv.Hue * 6;
                if (var_h == 6) var_h = 0;      //H must be < 1
                int var_i = (int)var_h;             //Or ... var_i = floor( var_h )
                double var_1 = hsv.Value * (1 - hsv.Saturation);
                double var_2 = hsv.Value * (1 - hsv.Saturation * (var_h - var_i));
                double var_3 = hsv.Value * (1 - hsv.Saturation * (1 - (var_h - var_i)));

                double var_r, var_g, var_b;
                if (var_i == 0)
                {
                    var_r = hsv.Value;
                    var_g = var_3;
                    var_b = var_1;
                }
                else if (var_i == 1)
                {
                    var_r = var_2;
                    var_g = hsv.Value;
                    var_b = var_1;
                }
                else if (var_i == 2)
                {
                    var_r = var_1;
                    var_g = hsv.Value;
                    var_b = var_3;
                }
                else if (var_i == 3)
                {
                    var_r = var_1;
                    var_g = var_2;
                    var_b = hsv.Value;
                }
                else if (var_i == 4)
                {
                    var_r = var_3;
                    var_g = var_1;
                    var_b = hsv.Value;
                }
                else
                {
                    var_r = hsv.Value;
                    var_g = var_1;
                    var_b = var_2;
                }

                color = Color.FromArgb((int)(var_r * 255), (int)(var_g * 255), (int)(var_b * 255));
            }

            return color;
        }

        private static double Max(double rR, double rG, double rB)
        {
            double ret = 0;
            if (rR > rG)
            {
                if (rR > rB)
                    ret = rR;
                else
                    ret = rB;
            }
            else
            {
                if (rB > rG)
                    ret = rB;
                else
                    ret = rG;
            }
            return ret;
        }

        private static double Min(double rR, double rG, double rB)
        {
            double ret = 0;
            if (rR < rG)
            {
                if (rR < rB)
                    ret = rR;
                else
                    ret = rB;
            }
            else
            {
                if (rB < rG)
                    ret = rB;
                else
                    ret = rG;
            }
            return ret;
        }
    }

    internal struct HSVColor
    {
        /// <summary>
        /// Gets or sets color hue. Hue is value from 0-1 which determines the degree on color wheel color is on, i.e. 0.5 = 180 degrees
        /// </summary>
        public double Hue;
        /// <summary>
        /// Gets or sets the color saturation from 0-1, i.e. 0-100%.
        /// </summary>
        public double Saturation;
        /// <summary>
        /// Gets or sets the amount of white and black in color.
        /// </summary>
        public double Value;

        /// <summary>
        /// Initializes a new instance of the HSVColor structure.
        /// </summary>
        /// <param name="hue"></param>
        /// <param name="saturation"></param>
        /// <param name="value"></param>
        public HSVColor(double hue, double saturation, double value)
        {
            Hue = hue;
            Saturation = saturation;
            Value = value;
        }

        public override string ToString()
        {
            return string.Format("Hue={0}, Saturation={1}, Value={2}", Hue, Saturation, Value);
        }
    }
}
