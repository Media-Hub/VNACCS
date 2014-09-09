namespace Naccs.Common.Function
{
    using Naccs.Common;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;

    public class CharsWidth
    {
        private Font font;
        private string fontFamily;
        private int fontSize;
        private Graphics grfx;
        private PrinterSettings printer;
        private SizeF sizeF;

        public CharsWidth(string fontStr)
        {
            string[] strArray = fontStr.Split(new char[] { ',' });
            this.fontFamily = strArray[0];
            string s = strArray[1];
            s = s.Trim(new char[] { ' ', 'p', 't' });
            try
            {
                this.fontSize = int.Parse(s);
            }
            catch (Exception)
            {
                NaccsException exception = new NaccsException(MessageKind.Error, 0x3e7, fontStr);
                throw exception;
            }
            this.font = new Font(this.fontFamily, (float) this.fontSize);
            this.printer = new PrinterSettings();
            this.grfx = this.printer.CreateMeasurementGraphics();
        }

        public void disposeGraphics()
        {
            this.grfx.Dispose();
        }

        public Font getFont()
        {
            return this.font;
        }

        public int getWidth(string str)
        {
            int num = 0;
            string text = str;
            if (!text.Equals(""))
            {
                this.sizeF = this.grfx.MeasureString(text, this.font);
                num = (int) Math.Ceiling((double) this.sizeF.Width);
            }
            return num;
        }

        public int getWidth(int figure, string attribute)
        {
            int num = 0;
            string text = "";
            if ((figure >= 1) && "W".Equals(attribute))
            {
                figure /= 3;
            }
            if (figure == 1)
            {
                text = text + "W";
            }
            for (int i = 0; i < figure; i++)
            {
                text = text + "W";
            }
            if (!text.Equals(""))
            {
                this.sizeF = this.grfx.MeasureString(text, this.font);
                num = (int) Math.Ceiling((double) this.sizeF.Width);
            }
            return num;
        }
    }
}

