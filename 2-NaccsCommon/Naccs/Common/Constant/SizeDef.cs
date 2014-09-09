namespace Naccs.Common.Constant
{
    using System;
    using System.Windows.Forms;

    public class SizeDef
    {
        public const int AN_HEIGHT = 0x10;
        public const int AN_HEIGHT_R = 0x10;
        private static float displayDPI = 96f;
        public static float DisplayPixelPerMiliMeter = 3.78f;
        public const int FRIDAY_X = 1;
        public const int FRIDAY_X_R = 1;
        public const int FRIDAY_Y = 0x13;
        public const int FRIDAY_Y_R = 0x16;
        public const int ITEM_SPACE = 6;
        public const int ITEM_SPACE_R = 2;
        public const int LABEL_TEXTBOX = 3;
        public static int LAYOUT_A3_HEIGHT = 0x5e8;
        public static int LAYOUT_A3_WIDTH = 0x417;
        public static int LAYOUT_A4_HEIGHT = 0x417;
        public static int LAYOUT_A4_WIDTH = 0x2ce;
        public static int LAYOUT_A5_HEIGHT = 0x2ce;
        public static int LAYOUT_A5_WIDTH = 0x1e4;
        public const int LAYOUT_HEIGHT = 0x417;
        public const int LAYOUT_LEFT = 20;
        public const int LAYOUT_LEFT_R = 2;
        public const int LAYOUT_PAGE_HEIGHT = 0x402;
        public const int LAYOUT_UP = 20;
        public const int LAYOUT_UP_R = 20;
        public const int LAYOUT_WIDTH = 0x2ce;
        public static int MARGINS_BOTTOM = 10;
        public static int MARGINS_LEFT = 10;
        public static int MARGINS_RIGHT = 10;
        public static int MARGINS_TOP = 10;
        public static float MiliMeterPerDisplayPixel = 0.2645f;
        public const int NAVI_HEIGHT = 0x1c;
        public const int NAVI_WIDTH = 0xb2;
        public const int PAPER_A3_HEIGHT = 420;
        public const int PAPER_A3_WIDTH = 0x129;
        public const int PAPER_A4_HEIGHT = 0x129;
        public const int PAPER_A4_WIDTH = 210;
        public const int PAPER_A5_HEIGHT = 210;
        public const int PAPER_A5_WIDTH = 0x94;
        public const int PREVIEW_HEIGHT = 600;
        public const int PREVIEW_WIDTH = 800;
        public const double PREVIEW_ZOOM = 0.75;
        private static float printerDPI = 100f;
        public static float PrintPixelPerDisplayPixel = 1.042f;
        public const int PrtTextCtrl3DMargin = 3;
        public const int PrtTextSingleMargin = 2;
        public const string REPORT = "r";
        public const int ROW_CONTAINER_SPACE = 6;
        public const int ROW_CONTAINER_SPACE_R = 6;
        public const int ROW_ITEM_SPACE = 3;
        public const int ROW_ITEM_SPACE_R = 6;
        public const string SCREAN = "s";

        public static void calcSizeSettings()
        {
            PrintPixelPerDisplayPixel = printerDPI / displayDPI;
            DisplayPixelPerMiliMeter = displayDPI / 25.4f;
            MiliMeterPerDisplayPixel = 25.4f / displayDPI;
            LAYOUT_A3_HEIGHT = mmToDisplayPixel((float) (420 - (MARGINS_TOP + MARGINS_BOTTOM)));
            LAYOUT_A3_WIDTH = mmToDisplayPixel((float) (0x129 - (MARGINS_LEFT + MARGINS_RIGHT)));
            LAYOUT_A4_HEIGHT = mmToDisplayPixel((float) (0x129 - (MARGINS_TOP + MARGINS_BOTTOM)));
            LAYOUT_A4_WIDTH = mmToDisplayPixel((float) (210 - (MARGINS_LEFT + MARGINS_RIGHT)));
            LAYOUT_A5_HEIGHT = mmToDisplayPixel((float) (210 - (MARGINS_TOP + MARGINS_BOTTOM)));
            LAYOUT_A5_WIDTH = mmToDisplayPixel((float) (0x94 - (MARGINS_LEFT + MARGINS_RIGHT)));
        }

        public static int DisplayPixelToPrinterPixel(int displayPixel)
        {
            float num = displayPixel * PrintPixelPerDisplayPixel;
            decimal d = (decimal) num;
            return decimal.ToInt32(decimal.Round(d));
        }

        public static int mmToDisplayPixel(float mm)
        {
            float num = mm * DisplayPixelPerMiliMeter;
            decimal d = (decimal) num;
            return decimal.ToInt32(decimal.Round(d));
        }

        public static int prtTextMargin(string borderStyle)
        {
            int num = 0;
            if (borderStyle == BorderStyle.FixedSingle.ToString())
            {
                return 2;
            }
            if (borderStyle == BorderStyle.Fixed3D.ToString())
            {
                num = 3;
            }
            return num;
        }

        public static float DisplayDPI
        {
            get
            {
                return displayDPI;
            }
            set
            {
                displayDPI = value;
                calcSizeSettings();
            }
        }

        public static float PrinterDPI
        {
            get
            {
                return printerDPI;
            }
            set
            {
                printerDPI = value;
                PrintPixelPerDisplayPixel = printerDPI / displayDPI;
            }
        }
    }
}

