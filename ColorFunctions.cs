namespace DevComponents.DotNetBar
{
    using System;
    using System.Drawing;

    /// <summary>
    ///    Summary description for ColorFunctions.
    /// </summary>
    public class ColorFunctions
    {
        private static Color m_HoverBackColor;
        private static Color m_HoverBackColor2;
        private static Color m_HoverBackColor3;
        private static Color m_CheckBoxBackColor;
        private static Color m_MenuBackColor;
        private static Color m_MenuFocusBorderColor;
        private static Color m_PressedBackColor;
        private static Color m_ToolMenuFocusBackColor;
        private static Color m_SideRecentlyBackColor;
        private static Color m_RecentlyUsedOfficeBackColor;

        private static Bitmap m_PushedBrushBmp = null;

        internal static bool ColorsLoaded = false;

        public static void LoadColors()
        {
            RefreshColors();
            if (!ColorsLoaded)
                Microsoft.Win32.SystemEvents.UserPreferenceChanged += new Microsoft.Win32.UserPreferenceChangedEventHandler(ColorFunctions.PreferenceChanged);
            ColorsLoaded = true;
        }

        private static void PreferenceChanged(object sender, Microsoft.Win32.UserPreferenceChangedEventArgs e)
        {
            RefreshColors();
            NativeFunctions.RefreshSettings();
            BarFunctions.RefreshScreens();
        }

        private static void RefreshColors()
        {
            if (NativeFunctions.ColorDepth >= 16)
            {
                int red = (int)((System.Drawing.SystemColors.Highlight.R - System.Drawing.SystemColors.Window.R) * .30 + System.Drawing.SystemColors.Window.R);
                int green = (int)((System.Drawing.SystemColors.Highlight.G - System.Drawing.SystemColors.Window.G) * .30 + System.Drawing.SystemColors.Window.G);
                int blue = (int)((System.Drawing.SystemColors.Highlight.B - System.Drawing.SystemColors.Window.B) * .30 + System.Drawing.SystemColors.Window.B);
                m_HoverBackColor = Color.FromArgb(red, green, blue);

                red = (int)((System.Drawing.SystemColors.Highlight.R - System.Drawing.SystemColors.Window.R) * .45 + System.Drawing.SystemColors.Window.R);
                green = (int)((System.Drawing.SystemColors.Highlight.G - System.Drawing.SystemColors.Window.G) * .45 + System.Drawing.SystemColors.Window.G);
                blue = (int)((System.Drawing.SystemColors.Highlight.B - System.Drawing.SystemColors.Window.B) * .45 + System.Drawing.SystemColors.Window.B);
                m_HoverBackColor2 = Color.FromArgb(red, green, blue);

                red = (int)((System.Drawing.SystemColors.Highlight.R - System.Drawing.SystemColors.Window.R) * .10 + System.Drawing.SystemColors.Window.R);
                green = (int)((System.Drawing.SystemColors.Highlight.G - System.Drawing.SystemColors.Window.G) * .10 + System.Drawing.SystemColors.Window.G);
                blue = (int)((System.Drawing.SystemColors.Highlight.B - System.Drawing.SystemColors.Window.B) * .10 + System.Drawing.SystemColors.Window.B);
                m_HoverBackColor3 = Color.FromArgb(red, green, blue);

                red = (int)((System.Drawing.SystemColors.Highlight.R - System.Drawing.SystemColors.Window.R) * .25 + System.Drawing.SystemColors.Window.R);
                green = (int)((System.Drawing.SystemColors.Highlight.G - System.Drawing.SystemColors.Window.G) * .25 + System.Drawing.SystemColors.Window.G);
                blue = (int)((System.Drawing.SystemColors.Highlight.B - System.Drawing.SystemColors.Window.B) * .25 + System.Drawing.SystemColors.Window.B);
                m_CheckBoxBackColor = Color.FromArgb(red, green, blue);

                if (NativeFunctions.ColorDepth <= 16)
                {
                    m_MenuBackColor = System.Drawing.SystemColors.ControlLightLight;
                }
                else
                {
                    red = (int)((System.Drawing.SystemColors.Control.R - System.Drawing.Color.White.R) * .20 + System.Drawing.Color.White.R);
                    green = (int)((System.Drawing.SystemColors.Control.G - System.Drawing.Color.White.G) * .20 + System.Drawing.Color.White.G);
                    blue = (int)((System.Drawing.SystemColors.Control.B - System.Drawing.Color.White.B) * .20 + System.Drawing.Color.White.B);
                    m_MenuBackColor = Color.FromArgb(red, green, blue);
                }

                red = (int)(System.Drawing.SystemColors.ControlDark.R * .80);
                green = (int)(System.Drawing.SystemColors.ControlDark.G * .80);
                blue = (int)(System.Drawing.SystemColors.ControlDark.B * .80);
                m_MenuFocusBorderColor = Color.FromArgb(red, green, blue);

                // and Highlight color
                red = (int)((System.Drawing.SystemColors.Highlight.R - System.Drawing.SystemColors.Window.R) * .25 + System.Drawing.SystemColors.Window.R);
                green = (int)((System.Drawing.SystemColors.Highlight.G - System.Drawing.SystemColors.Window.G) * .25 + System.Drawing.SystemColors.Window.G);
                blue = (int)((System.Drawing.SystemColors.Highlight.B - System.Drawing.SystemColors.Window.B) * .25 + System.Drawing.SystemColors.Window.B);
                m_PressedBackColor = Color.FromArgb(red, green, blue);

                red = (int)((System.Drawing.SystemColors.Control.R - System.Drawing.Color.White.R) * .80 + System.Drawing.Color.White.R);
                green = (int)((System.Drawing.SystemColors.Control.G - System.Drawing.Color.White.G) * .80 + System.Drawing.Color.White.G);
                blue = (int)((System.Drawing.SystemColors.Control.B - System.Drawing.Color.White.B) * .80 + System.Drawing.Color.White.B);
                m_ToolMenuFocusBackColor = Color.FromArgb(red, green, blue);

                red = (int)((System.Drawing.SystemColors.Control.R - System.Drawing.Color.White.R) * .50 + System.Drawing.Color.White.R);
                green = (int)((System.Drawing.SystemColors.Control.G - System.Drawing.Color.White.G) * .50 + System.Drawing.Color.White.G);
                blue = (int)((System.Drawing.SystemColors.Control.B - System.Drawing.Color.White.B) * .50 + System.Drawing.Color.White.B);
                m_RecentlyUsedOfficeBackColor = Color.FromArgb(red, green, blue);

                m_SideRecentlyBackColor = System.Drawing.SystemColors.Control;
            }
            else
            {
                m_HoverBackColor = System.Drawing.SystemColors.ControlLightLight;
                m_HoverBackColor2 = m_HoverBackColor;
                m_HoverBackColor3 = m_HoverBackColor;
                m_CheckBoxBackColor = System.Drawing.SystemColors.ControlLight;
                m_MenuBackColor = System.Drawing.SystemColors.ControlLightLight;
                m_MenuFocusBorderColor = System.Drawing.SystemColors.ControlDark;
                m_PressedBackColor = System.Drawing.SystemColors.ControlLight;
                m_ToolMenuFocusBackColor = System.Drawing.SystemColors.ControlLight;
                m_RecentlyUsedOfficeBackColor = System.Drawing.SystemColors.ControlLight;
                m_SideRecentlyBackColor = System.Drawing.SystemColors.Control;
            }

            if (m_PushedBrushBmp != null)
            {
                m_PushedBrushBmp.Dispose();
                m_PushedBrushBmp = new System.Drawing.Bitmap(2, 2);
                m_PushedBrushBmp.SetPixel(0, 0, System.Drawing.SystemColors.Control);
                m_PushedBrushBmp.SetPixel(1, 0, System.Drawing.SystemColors.ControlLightLight);
                m_PushedBrushBmp.SetPixel(0, 1, System.Drawing.SystemColors.ControlLightLight);
                m_PushedBrushBmp.SetPixel(1, 1, System.Drawing.SystemColors.Control);
            }
        }

        public static Color GetShade(Color color, int shadePercent)
        {
            HLSColor hls = RGBToHSL(color);
            if (hls.Lightness == 0)
                return HLSToRGB(hls.Hue, shadePercent / 100, hls.Saturation);
            else
                return HLSToRGB(hls.Hue, Math.Max(0, (hls.Lightness - shadePercent / 100)), hls.Saturation);
        }

        public struct HLSColor
        {
            public double Hue;
            public double Lightness;
            public double Saturation;
        }
        //public struct RGBColor
        //{
        //    public int Red;
        //    public int Green;
        //    public int Blue;
        //}
        public static HLSColor RGBToHSL(int Red, int Green, int Blue)
        {
            double Max, Min, delta;
            double rR, rG, rB;
            HLSColor ret = new HLSColor();

            rR = (double)Red / 255;
            rG = (double)Green / 255;
            rB = (double)Blue / 255;

            // Given: rgb each in [0,1].
            // Desired: h in [0,360] and s in [0,1], except if s=0, then h=UNDEFINED.}
            Max = Maximum(rR, rG, rB);
            Min = Minimum(rR, rG, rB);
            delta = Max - Min;
            ret.Lightness = (Max + Min) / 2;    // {This is the lightness}
            // {Next calculate saturation}

            if (Max == Min)
            {
                // begin {Acrhomatic case}
                ret.Saturation = 0;
                ret.Hue = 0;
                // end {Acrhomatic case}	
            }
            else
            {
                // begin {Chromatic case}
                // {First calculate the saturation.}
                if (ret.Lightness <= 0.5)
                    ret.Saturation = (Max - Min) / (Max + Min);
                else
                    ret.Saturation = (Max - Min) / (2 - Max - Min);
                // {Next calculate the hue.}
                
                double delr = (((Max - rR) / 6) + (delta / 2)) / delta;
                double delg = (((Max - rG) / 6) + (delta / 2)) / delta;
                double delb = (((Max - rB) / 6) + (delta / 2)) / delta;

                if (rR == Max)
                    ret.Hue = delb - delg;		//{Resulting color is between yellow and magenta}
                else if (rG == Max)
                    ret.Hue = (1d / 3d) + delr - delb;	// {Resulting color is between cyan and yellow}
                else if (rB == Max)
                    ret.Hue = (2d / 3d) + delg - delr;;	// {Resulting color is between magenta and cyan}

                if (ret.Hue < 0) ret.Hue += 1;
                if (ret.Hue > 1) ret.Hue -= 1;
            }
            return ret;
        }

        public static HLSColor RGBToHSL(Color color)
        {
            return RGBToHSL(color.R, color.G, color.B);
        }

        private static double Maximum(double rR, double rG, double rB)
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

        private static double Minimum(double rR, double rG, double rB)
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

        public static Color HLSToRGB(double Hue, double Lightness, double Saturation)
        {
            if (Lightness < 0) Lightness = 0; else if (Lightness > 1) Lightness = 1;
            if (Saturation < 0) Saturation = 0; else if (Saturation > 1) Saturation = 1;

            int r, g, b;
            double var1, var2;
            if (Saturation == 0)
            {
                r = (int)(Lightness * 255);
                g = (int)(Lightness * 255);
                b = (int)(Lightness * 255);
            }
            else
            {
                if (Lightness < 0.5)
                {
                    var2 = Lightness * (1 + Saturation);
                }
                else
                {
                    var2 = (Lightness + Saturation) - (Saturation * Lightness);
                }

                var1 = 2 * Lightness - var2;
                r = (int)(255 * hue_2_rgb(var1, var2, Hue + (1d / 3d)));
                g = (int)(255 * hue_2_rgb(var1, var2, Hue));
                b = (int)(255 * hue_2_rgb(var1, var2, Hue - (1d / 3d)));
            }

            return Color.FromArgb(r, g, b);
        }

        private static double hue_2_rgb(double v1, double v2,double vh)
        {
            if (vh < 0)
            {
                vh += 1;
            }

            if (vh > 1)
            {
                vh -= 1;
            }

            if ((6 * vh) < 1)
            {
                return (v1 + (v2 - v1) * 6 * vh);
            };

            if ((2 * vh) < 1)
            {
                return (v2);
            };

            if ((3 * vh) < 2)
            {
                return (v1 + (v2 - v1) * ((2d / 3d - vh) * 6));
            };

            return (v1);
        }

        public static Color HLSToRGB(HLSColor clr)
        {
            return HLSToRGB(clr.Hue, clr.Lightness, clr.Saturation);
        }

        public static System.Drawing.Color HoverBackColor()
        {
            return m_HoverBackColor;
        }

        public static System.Drawing.Color HoverBackColor2()
        {
            return m_HoverBackColor2;
        }

        public static System.Drawing.Color HoverBackColor3()
        {
            return m_HoverBackColor3;
        }

        public static System.Drawing.Color HoverBackColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(HoverBackColor());
        }

        public static System.Drawing.Color PressedBackColor()
        {
            return m_PressedBackColor;
        }

        public static System.Drawing.Color PressedBackColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(PressedBackColor());
        }

        public static System.Drawing.Color CheckBoxBackColor()
        {
            return m_CheckBoxBackColor;
        }

        public static System.Drawing.Color CheckBoxBackColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(CheckBoxBackColor());
        }

        public static System.Drawing.Color ToolMenuFocusBackColor()
        {
            return m_ToolMenuFocusBackColor;
        }

        public static System.Drawing.Color RecentlyUsedOfficeBackColor()
        {
            return m_RecentlyUsedOfficeBackColor;
        }

        public static System.Drawing.Color RecentlyUsedOfficeBackColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(RecentlyUsedOfficeBackColor());
        }

        public static System.Drawing.Color SideRecentlyBackColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(SideRecentlyBackColor());
        }

        public static System.Drawing.Color SideRecentlyBackColor()
        {
            return m_SideRecentlyBackColor;
        }

        public static System.Drawing.Color ToolMenuFocusBackColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(ToolMenuFocusBackColor());
        }

        public static System.Drawing.Color MenuFocusBorderColor()
        {
            return m_MenuFocusBorderColor;
        }

        public static System.Drawing.Color MenuFocusBorderColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(MenuFocusBorderColor());
        }

        public static System.Drawing.Color MenuBackColor()
        {
            return m_MenuBackColor;
        }

        public static System.Drawing.Color MenuBackColor(System.Drawing.Graphics g)
        {
            return g.GetNearestColor(MenuBackColor());
        }
        public static System.Drawing.Brush GetPushedBrush(BaseItem item)
        {
            if (item.Parent is GenericItemContainer)
            {
                if (!((GenericItemContainer)item.Parent).m_BackgroundColor.IsEmpty)
                {
                    return new SolidBrush(((GenericItemContainer)item.Parent).m_BackgroundColor);
                }
            }
            else if (item.Parent is SideBarPanelItem)
            {
                if (((SideBarPanelItem)item.Parent).BackgroundStyle.ShouldSerializeBackColor1())
                {
                    return new SolidBrush(((SideBarPanelItem)item.Parent).BackgroundStyle.BackColor1.Color);
                }
                else if (((SideBarPanelItem)item.Parent).BackgroundStyle.ShouldSerializeBackColor1())
                {
                    return new SolidBrush(((SideBarPanelItem)item.Parent).BackgroundStyle.BackColor1.Color);
                }
            }

            return ColorFunctions.GetPushedBrush();
        }
        public static System.Drawing.TextureBrush GetPushedBrush()
        {
            if (m_PushedBrushBmp == null)
            {
                m_PushedBrushBmp = new System.Drawing.Bitmap(2, 2);
                m_PushedBrushBmp.SetPixel(0, 0, System.Drawing.SystemColors.Control);
                m_PushedBrushBmp.SetPixel(1, 0, System.Drawing.SystemColors.ControlLightLight);
                m_PushedBrushBmp.SetPixel(0, 1, System.Drawing.SystemColors.ControlLightLight);
                m_PushedBrushBmp.SetPixel(1, 1, System.Drawing.SystemColors.Control);
            }
            return (new System.Drawing.TextureBrush(m_PushedBrushBmp));
        }

        public static bool IsEqual(Color color1, Color color2)
        {
            return color1.A == color2.A && color1.R == color2.R && color1.G == color2.G && color1.B == color2.B;
        }
    }
}
