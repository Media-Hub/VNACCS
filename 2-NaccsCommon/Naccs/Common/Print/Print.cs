namespace Naccs.Common.Print
{
    using Naccs.Common;
    using Naccs.Common.Common;
    using Naccs.Common.Constant;
    using Naccs.Common.CustomControls;
    using Naccs.Common.Function;
    using Naccs.Common.JobConfig;
    using Naccs.Common.Properties;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading;
    using System.Windows.Forms;
    using System.Xml;

    public class Print
    {
        private LBPrtTextBox BaseTxtBox = new LBPrtTextBox();
        private const int CHEKBOX_SIZE = 12;
        private ImportFont impFont = ImportFont.CreateInstance();
        private int mBinNo;
        private int mContained;
        private int mConvMode;
        private int mCopies = 1;
        private string mCustomTemplatePath = "";
        private List<string> mDataList;
        private string mDoublePrint = "";
        private int mDrawPage;
        private string mFontFilePath = "";
        private string mGStampDateAlign = "";
        private string mGStampInfo = "";
        private const float MILIMETER_TO_INCH = 0.254f;
        private string mImportFontFiles = "";
        private string mInputInfo = "";
        private bool mIsNullData;
        private bool mIsWriteGStamp;
        private AbstractJobConfig mJobConfig;
        private int mLeft;
        private Font mOcrFont;
        private string mOcrFontName = "";
        private float mOcrFontSize;
        private string mOrientation = "";
        private XmlNodeList mPageList;
        private string mPaperSize = "";
        private PrinterSettings.PaperSizeCollection mPaperSizes;
        private int mPrintDlgMode;
        private string mPrinterName = "";
        private string mPrinterSettingsPath = "";
        private string mPrintKey = "";
        private RecordAttribute mRecordAtb;
        private object mSender;
        private string mServerRecvTime = "";
        private string mSpcFontName = "";
        private string mStampMarkText;
        private DateTime mTimeStamp;
        private int mTop;
        private const int MULTI_LINECOUNT_A4V = 0x44;
        private StrFunc SF = StrFunc.CreateInstance();

        public event OnPrintEndEventHandler OnPrintEnd;

        private string defaultProerty(string typeName, string propName)
        {
            string str = null;
            string str2 = PrtProperties.getControlName(typeName);
            foreach (System.Type type in DesignControls.AllControls)
            {
                if (str2 == type.Name)
                {
                    str = ComponentProperty.getPropertyDefault(type, propName);
                    if (str != null)
                    {
                        return str;
                    }
                    Control component = (Control) TypeDescriptor.CreateInstance(null, type, null, null);
                    if (component == null)
                    {
                        return str;
                    }
                    using (component)
                    {
                        PropertyDescriptor descriptor = TypeDescriptor.GetProperties(component)[propName];
                        if (descriptor != null)
                        {
                            str = descriptor.Converter.ConvertToInvariantString(descriptor.GetValue(component));
                        }
                        return str;
                    }
                }
            }
            return str;
        }

        private void drawNode(Graphics g, XmlNode source, PointF margin, Font defaultFont)
        {
            if (source.Name == "label")
            {
                this.drawNodeString(g, source, margin, defaultFont);
            }
            else if (source.Name == "ranlabel")
            {
                this.drawNodeString(g, source, margin, defaultFont);
            }
            else if (source.Name == "pagelabel")
            {
                this.drawNodeString(g, source, margin, defaultFont);
            }
            else if (source.Name == "prttextbox")
            {
                this.drawNodeString(g, source, margin, defaultFont);
            }
            else if (source.Name == "checkbox")
            {
                this.drawNodeCheckOrRadio(g, source, margin, defaultFont);
            }
            else if (source.Name == "radio")
            {
                this.drawNodeCheckOrRadio(g, source, margin, defaultFont);
            }
            else if (source.Name == "line")
            {
                this.drawNodeLine(g, source, margin);
            }
            else if (source.Name == "rectangle")
            {
                this.drawNodeRect(g, source, margin);
            }
            else if (source.Name == "gstomp")
            {
                this.drawNodeGStamp(g, source, margin, defaultFont);
            }
            else if (source.Name == "barcode")
            {
                this.drawNodeBarcode(g, source, margin, defaultFont);
            }
            else if (source.Name == "hand-ocr")
            {
                this.drawNodeOcr(g, source, margin, defaultFont);
            }
            else if (source.Name == "comp-ocr")
            {
                this.drawNodeOcr(g, source, margin, defaultFont);
            }
            else if (source.Name == "imagebox")
            {
                this.drawNodeImage(g, source, margin);
            }
        }

        private void drawNodeBarcode(Graphics g, XmlNode source, PointF margin, Font defaultFont)
        {
            if (XmlFunc.getBoolAttribute(source, "Visible", true))
            {
                string code = source.InnerText.Trim();
                if (code.Length != 0)
                {
                    PointF tf = this.getLocationAttribute(source);
                    tf.X += margin.X;
                    tf.Y += margin.Y;
                    Color color = this.getFontColorAttribute(source);
                    Color white = new Color();
                    white = Color.White;
                    float num = float.Parse(XmlFunc.getStringAttribute(source, "ByteWidth", "12.00"));
                    SizeF ef = this.getSizeAttribute(source);
                    ef.Width = (num * SizeDef.PrintPixelPerDisplayPixel) * (code.Length + 2);
                    DotNetBarcode barcode = new DotNetBarcode(DotNetBarcode.Types.Code39);
                    using (Font font = this.getFontAttribute(source, defaultFont))
                    {
                        barcode.FontName = font.Name;
                        barcode.FontSize = font.Size;
                        barcode.FontBold = font.Bold;
                        barcode.FontItalic = font.Italic;
                        barcode.FontColor = color;
                        barcode.FontBackGroundColor = white;
                        barcode.BarColor = color;
                        barcode.BackGroundColor = white;
                        barcode.WriteBar(code, tf.X, tf.Y, ef.Width, ef.Height, g);
                    }
                }
            }
        }

        private void drawNodeCheckOrRadio(Graphics g, XmlNode source, PointF margin, Font defaultFont)
        {
            if (XmlFunc.getBoolAttribute(source, "Visible", true))
            {
                PointF loc = this.getLocationAttribute(source);
                loc.X += margin.X;
                loc.Y += margin.Y;
                SizeF size = this.getSizeAttribute(source);
                float num = (size.Height - 12f) / 2f;
                RectangleF ef2 = new RectangleF(loc.X, loc.Y + num, 12f, 12f);
                Color color = this.getFontColorAttribute(source);
                bool flag = false;
                string strA = source.InnerText.Trim();
                flag = (string.Compare(strA, "TRUE", true) == 0) || (strA == "1");
                if (source.Name == "checkbox")
                {
                    if (flag)
                    {
                        ControlPaint.DrawCheckBox(g, (int) ef2.X, (int) ef2.Y, (int) ef2.Width, (int) ef2.Height, ButtonState.Checked);
                    }
                    else
                    {
                        ControlPaint.DrawCheckBox(g, (int) ef2.X, (int) ef2.Y, (int) ef2.Width, (int) ef2.Height, ButtonState.Normal);
                    }
                }
                else if (flag)
                {
                    ControlPaint.DrawRadioButton(g, (int) ef2.X, (int) ef2.Y, (int) ef2.Width, (int) ef2.Height, ButtonState.Checked);
                }
                else
                {
                    ControlPaint.DrawRadioButton(g, (int) ef2.X, (int) ef2.Y, (int) ef2.Width, (int) ef2.Height, ButtonState.Normal);
                }
                string text = XmlFunc.getStringAttribute(source, "Text");
                loc.X += ef2.Width + 2f;
                size.Width -= ef2.Width + 2f;
                using (Font font = this.getFontAttribute(source, defaultFont))
                {
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        using (StringFormat format = this.getStringFormatAttribute(source))
                        {
                            this.drawText(g, text, font, brush, loc, size, format, false);
                        }
                    }
                }
            }
        }

        private void drawNodeGStamp(Graphics g, XmlNode source, PointF margin, Font defaultFont)
        {
            if (this.mIsWriteGStamp || this.mIsNullData)
            {
                string mGStampInfo;
                if (this.mIsNullData)
                {
                    mGStampInfo = Resources.ResourceManager.GetString("MSG0208");
                }
                else
                {
                    mGStampInfo = this.mGStampInfo;
                }
                List<string> list = CommaText.CommaTextToItems(mGStampInfo);
                PointF tf = this.getLocationAttribute(source);
                tf.X += margin.X;
                tf.Y += margin.Y;
                SizeF ef = this.getSizeAttribute(source);
                float num = 19f * SizeDef.PrintPixelPerDisplayPixel;
                PointF tf2 = new PointF(LBGStamp._topText.X * SizeDef.PrintPixelPerDisplayPixel, LBGStamp._topText.Y * SizeDef.PrintPixelPerDisplayPixel);
                PointF tf3 = new PointF(LBGStamp._dateText.X * SizeDef.PrintPixelPerDisplayPixel, LBGStamp._dateText.Y * SizeDef.PrintPixelPerDisplayPixel);
                PointF tf4 = new PointF(LBGStamp._underText1.X * SizeDef.PrintPixelPerDisplayPixel, LBGStamp._underText1.Y * SizeDef.PrintPixelPerDisplayPixel);
                PointF tf5 = new PointF(LBGStamp._underText2.X * SizeDef.PrintPixelPerDisplayPixel, LBGStamp._underText2.Y * SizeDef.PrintPixelPerDisplayPixel);
                PointF tf6 = new PointF(LBGStamp._underText3.X * SizeDef.PrintPixelPerDisplayPixel, LBGStamp._underText3.Y * SizeDef.PrintPixelPerDisplayPixel);
                SizeF size = new SizeF(LBGStamp._textSize.Width * SizeDef.PrintPixelPerDisplayPixel, LBGStamp._textSize.Height * SizeDef.PrintPixelPerDisplayPixel);
                using (Pen pen = this.getPenAttribute(source))
                {
                    g.DrawRectangle(pen, tf.X, tf.Y, ef.Width, ef.Height);
                    g.DrawLine(pen, tf.X, tf.Y + num, tf.X + ef.Width, tf.Y + num);
                    g.DrawLine(pen, tf.X, tf.Y + (num * 2f), tf.X + ef.Width, tf.Y + (num * 2f));
                }
                Font font = new Font("Tahoma", 9f);
                SolidBrush brush = new SolidBrush(Color.Black);
                StringFormat format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.NoWrap);
                try
                {
                    if (list.Count > 0)
                    {
                        g.DrawString(list[0], font, brush, new PointF(tf.X + tf2.X, tf.Y + tf2.Y));
                    }
                    if (list.Count > 1)
                    {
                        format.LineAlignment = StringAlignment.Center;
                        if (this.mGStampDateAlign.IndexOf("Center") >= 0)
                        {
                            format.Alignment = StringAlignment.Center;
                        }
                        else if (this.mGStampDateAlign.IndexOf("Right") >= 0)
                        {
                            format.Alignment = StringAlignment.Far;
                        }
                        else
                        {
                            format.Alignment = StringAlignment.Near;
                        }
                        string s = "";
                        Calendar calendar = new JapaneseCalendar();
                        CultureInfo provider = new CultureInfo("ja-JP") {
                            DateTimeFormat = { Calendar = calendar }
                        };
                        try
                        {
                            s = DateTime.ParseExact(list[1].Trim(), "yyyy/MM/dd", null).ToString("yy.MM.dd", provider);
                        }
                        catch
                        {
                            s = "XX.XX.XX";
                        }
                        g.DrawString(s, font, brush, new RectangleF(new PointF(tf.X + tf3.X, tf.Y + tf3.Y), size), format);
                    }
                    if (list.Count > 2)
                    {
                        g.DrawString(list[2], font, brush, new PointF(tf.X + tf4.X, tf.Y + tf4.Y));
                    }
                    if (list.Count > 3)
                    {
                        g.DrawString(list[3], font, brush, new PointF(tf.X + tf5.X, tf.Y + tf5.Y));
                    }
                    if (list.Count > 4)
                    {
                        g.DrawString(list[4], font, brush, new PointF(tf.X + tf6.X, tf.Y + tf6.Y));
                    }
                }
                finally
                {
                    format.Dispose();
                    brush.Dispose();
                    font.Dispose();
                }
            }
        }

        private void drawNodeImage(Graphics g, XmlNode source, PointF margin)
        {
            if (XmlFunc.getBoolAttribute(source, "Visible", true))
            {
                PointF tf = this.getLocationAttribute(source);
                tf.X += margin.X;
                tf.Y += margin.Y;
                SizeF ef = this.getSizeAttribute(source);
                string str = XmlFunc.getStringAttribute(source, "Image");
                try
                {
                    string str2;
                    NormalConfig mJobConfig = (NormalConfig) this.mJobConfig;
                    if (mJobConfig.Display.Trim().Length > 0)
                    {
                        str2 = Path.GetDirectoryName(mJobConfig.Display.Trim()) + @"\";
                    }
                    else
                    {
                        str2 = Path.GetDirectoryName(mJobConfig.Print.Trim()) + @"\";
                    }
                    using (FileStream stream = File.OpenRead(str2 + str))
                    {
                        using (Image image = Image.FromStream(stream, false, false))
                        {
                            g.DrawImage(image, tf.X, tf.Y, ef.Width, ef.Height);
                        }
                    }
                }
                catch
                {
                }
            }
        }

        private void drawNodeLine(Graphics g, XmlNode source, PointF margin)
        {
            PointF tf = this.getLocationAttribute(source);
            tf.X += margin.X;
            tf.Y += margin.Y;
            SizeF ef = this.getSizeAttribute(source);
            PointF tf2 = new PointF {
                X = tf.X + ((int) ef.Width),
                Y = tf.Y + ((int) ef.Height)
            };
            using (Pen pen = this.getPenAttribute(source))
            {
                g.DrawLine(pen, tf, tf2);
            }
        }

        private void drawNodeOcr(Graphics g, XmlNode source, PointF margin, Font defaultFont)
        {
            if (XmlFunc.getBoolAttribute(source, "Visible", true))
            {
                PointF tf = this.getLocationAttribute(source);
                tf.X += margin.X;
                tf.Y += margin.Y;
                Color color = this.getFontColorAttribute(source);
                string innerText = source.InnerText;
                float num = float.Parse(XmlFunc.getStringAttribute(source, "BytePitch", "0")) * SizeDef.PrintPixelPerDisplayPixel;
                Point component = new Point(0, 0);
                PointF tf2 = new PointF(0f, 0f);
                string text = XmlFunc.getStringAttribute(source, "FontLocate");
                if (text.Length > 0)
                {
                    component = (Point) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(text);
                }
                tf2.X = component.X * SizeDef.PrintPixelPerDisplayPixel;
                tf2.Y = component.Y * SizeDef.PrintPixelPerDisplayPixel;
                SizeF ef = new SizeF(0f, 0f);
                text = XmlFunc.getStringAttribute(source, "BoxSize");
                if (text.Length > 0)
                {
                    ef = (SizeF) TypeDescriptor.GetConverter(ef).ConvertFromInvariantString(text);
                }
                ef.Width *= SizeDef.PrintPixelPerDisplayPixel;
                ef.Height *= SizeDef.PrintPixelPerDisplayPixel;
                int num2 = XmlFunc.getNumericAttribute(source, "figure", 0);
                if (XmlFunc.getStringAttribute(source, "CurrencyMark").Length > 0)
                {
                    num2++;
                }
                if (XmlFunc.getBoolAttribute(source, "BoxLine", false))
                {
                    using (Pen pen = new Pen(color))
                    {
                        for (int i = 0; i < num2; i++)
                        {
                            g.DrawRectangle(pen, tf.X + (i * num), tf.Y, ef.Width, ef.Height);
                        }
                    }
                }
                if (this.mOcrFont != null)
                {
                    tf.X += tf2.X;
                    tf.Y += tf2.Y;
                    using (SolidBrush brush = new SolidBrush(color))
                    {
                        using (StringFormat format = this.getStringFormatAttribute(source))
                        {
                            for (int j = 0; j < innerText.Length; j++)
                            {
                                this.drawText(g, innerText[j].ToString(), this.mOcrFont, brush, new PointF(tf.X + (j * num), tf.Y), ef, format, false);
                            }
                        }
                    }
                }
            }
        }

        private void drawNodeRect(Graphics g, XmlNode source, PointF margin)
        {
            Pen pen;
            PointF location = this.getLocationAttribute(source);
            location.X += margin.X;
            location.Y += margin.Y;
            SizeF size = this.getSizeAttribute(source);
            bool flag = true;
            if (source.Name != "rectangle")
            {
                string str = XmlFunc.getStringAttribute(source, "BorderStyle").Trim();
                if (str.Length == 0)
                {
                    str = this.defaultProerty(source.Name, "BorderStyle");
                    if (str == null)
                    {
                        str = BorderStyle.None.ToString();
                    }
                }
                flag = !(str == BorderStyle.None.ToString());
            }
            if (source.Name == "rectangle")
            {
                pen = this.getPenAttribute(source);
            }
            else
            {
                pen = new Pen(Color.Black);
            }
            using (pen)
            {
                using (SolidBrush brush = this.getBrushAttribute(source))
                {
                    if (brush != null)
                    {
                        RectangleF ef2 = new RectangleF(location, size);
                        RectangleF[] rects = new RectangleF[] { ef2 };
                        g.FillRectangles(brush, rects);
                    }
                }
                if (flag)
                {
                    g.DrawRectangle(pen, location.X, location.Y, size.Width, size.Height);
                }
            }
        }

        private void drawNodeString(Graphics g, XmlNode source, PointF margin, Font defaultFont)
        {
            if (XmlFunc.getBoolAttribute(source, "Visible", true))
            {
                this.drawNodeRect(g, source, margin);
                PointF loc = this.getLocationAttribute(source);
                loc.X += margin.X;
                loc.Y += margin.Y;
                SizeF size = this.getSizeAttribute(source);
                string text = "";
                if (source.Name == "label")
                {
                    text = XmlFunc.getStringAttribute(source, "Text");
                }
                else
                {
                    text = source.InnerText;
                    if ((text.IndexOf("[%mp%]") >= 0) && (this.mPageList != null))
                    {
                        text = text.Replace("[%mp%]", this.mPageList.Count.ToString());
                    }
                }
                bool auto = false;
                if (source.Name != "prttextbox")
                {
                    auto = this.getBoolAttribute(source, "AutoSize");
                }
                string borderStyle = XmlFunc.getStringAttribute(source, "BorderStyle").Trim();
                if (borderStyle.Length == 0)
                {
                    borderStyle = BorderStyle.None.ToString();
                }
                int num = SizeDef.prtTextMargin(borderStyle);
                if (this.mConvMode == 0)
                {
                    loc.Y += num;
                    size.Height -= num;
                }
                else
                {
                    loc.Y += num;
                    loc.X += num;
                    size.Height -= num * 2;
                    size.Width -= num * 2;
                }
                using (Font font = this.getFontAttribute(source, defaultFont))
                {
                    using (SolidBrush brush = new SolidBrush(this.getFontColorAttribute(source)))
                    {
                        using (StringFormat format = this.getStringFormatAttribute(source))
                        {
                            if (XmlFunc.getBoolAttribute(source, "Multiline", false))
                            {
                                string str3 = XmlFunc.getStringAttribute(source, "Format");
                                string str4 = XmlFunc.getStringAttribute(source, "TextAlign", "Left");
                                if ((str3.Trim().Length == 0) || (str4 == "Left"))
                                {
                                    text = this.GetDisplayPrintLineEditStr(source, defaultFont);
                                    if (this.mConvMode == 0)
                                    {
                                        size.Width += 30f;
                                        switch (str4)
                                        {
                                            case "Right":
                                                loc.X -= 30f;
                                                break;

                                            case "Center":
                                                loc.X -= 15f;
                                                break;
                                        }
                                    }
                                }
                            }
                            this.drawText(g, text, font, brush, loc, size, format, auto);
                        }
                    }
                }
            }
        }

        private void drawPrintableArea(PrintPageEventArgs ev, bool is_preview)
        {
            using (Pen pen = new Pen(Color.Aqua))
            {
                pen.Alignment = PenAlignment.Inset;
                pen.Width = float.Parse("1");
                pen.DashStyle = DashStyle.Dash;
                pen.DashStyle = DashStyle.Dash;
                PointF tf = this.getDrawMargin(ev, is_preview);
                ev.Graphics.DrawRectangle(pen, tf.X, tf.Y, (float) ev.MarginBounds.Size.Width, (float) ev.MarginBounds.Size.Height);
            }
        }

        private void drawStampMark(Graphics g)
        {
            if (!string.IsNullOrEmpty(this.mStampMarkText) && (this.mStampMarkText.Trim().Length != 0))
            {
                int byteLength = this.SF.GetByteLength(this.mStampMarkText);
                byteLength += byteLength % 2;
                this.mStampMarkText = this.SF.FillBlankRight(this.mStampMarkText, byteLength);
                StringBuilder builder = new StringBuilder();
                builder.Append(this.mStampMarkText);
                builder.Append("　　　");
                g.RotateTransform(45f);
                using (Font font = new Font("Tahoma", 72f, FontStyle.Bold))
                {
                    using (SolidBrush brush = new SolidBrush(Color.LightGray))
                    {
                        for (float i = -1040f; i < 1160f; i += 200f)
                        {
                            g.DrawString(this.SF.MakeCycleStr(10, builder.ToString()), font, brush, 0f, i);
                            string str = this.SF.CutByteLength(builder.ToString(), byteLength - 1);
                            str = builder.ToString().Substring(str.Length + 1);
                            builder.Remove(builder.Length - str.Length, str.Length);
                            builder.Insert(0, str);
                        }
                    }
                }
                g.RotateTransform(-45f);
            }
        }

        private void drawText(Graphics g, string text, Font font, SolidBrush brush, PointF loc, SizeF size, StringFormat format, bool auto)
        {
            Font font2 = font;
            SizeF ef = g.MeasureString(text, font2, loc, format);
            if (auto)
            {
                if (ef.Width < size.Width)
                {
                    ef.Width = size.Width;
                }
                if (ef.Height < size.Height)
                {
                    ef.Height = size.Height;
                }
            }
            else
            {
                ef.Width = size.Width;
                ef.Height = size.Height;
            }
            RectangleF layoutRectangle = new RectangleF(loc, ef);
            g.DrawString(text, font2, brush, layoutRectangle, format);
        }

        private void execPrint(XmlDocument printXml, bool previewFlg)
        {
            if ((this.mOcrFont == null) && (printXml.InnerXml.Contains("<comp-ocr ") || printXml.InnerXml.Contains("<hand-ocr ")))
            {
                throw new NaccsException(MessageKind.Error, 610, "");
            }
            printXml.InnerXml = printXml.InnerXml.Replace("‐", "－");
            this.mPageList = this.getPageList(printXml);
            if (this.mPageList.Count > 0)
            {
                try
                {
                    using (PrintDocument document = new PrintDocument())
                    {
                        document.PrintController = new StandardPrintController();
                        document.BeginPrint += new PrintEventHandler(this.pdBeginPrint);
                        document.PrintPage += new PrintPageEventHandler(this.pdPrintPage);
                        document.QueryPageSettings += new QueryPageSettingsEventHandler(this.pdQueryPageSettingsEvent);
                        document.EndPrint += new PrintEventHandler(this.pd_EndPrint);
                        document.PrinterSettings.MinimumPage = 1;
                        document.PrinterSettings.MaximumPage = this.mPageList.Count;
                        if (previewFlg)
                        {
                            using (PrintPreviewDialog dialog = new PrintPreviewDialog())
                            {
                                dialog.AutoScroll = true;
                                dialog.Shown += new EventHandler(this.pvActivateEvent);
                                dialog.Paint += new PaintEventHandler(this.printPreview_Paint);
                                dialog.MouseWheel += new MouseEventHandler(this.printPreview_MouseWheel);
                                dialog.Document = document;
                                dialog.Width = 800;
                                dialog.Height = 600;
                                dialog.PrintPreviewControl.Zoom = 0.75;
                                dialog.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                                dialog.ShowInTaskbar = true;
                                this.mDrawPage = 0;
                                dialog.ShowDialog((Form) this.mSender);
                                return;
                            }
                        }
                        this.mDrawPage = 0;
                        document.Print();
                    }
                }
                catch (Exception exception)
                {
                    throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
                }
            }
            throw new NaccsException(MessageKind.Error, 0x25d, Resources.ResourceManager.GetString("COM17"));
        }

        public void execTextPrint(List<string> data, bool previewFlg)
        {
            this.mConvMode = 0;
            this.mDataList = data;
            if (data.Count > 0)
            {
                try
                {
                    using (PrintDocument document = new PrintDocument())
                    {
                        document.PrintController = new StandardPrintController();
                        document.BeginPrint += new PrintEventHandler(this.pdBeginPrint);
                        document.PrintPage += new PrintPageEventHandler(this.pdTextPrintPage);
                        document.QueryPageSettings += new QueryPageSettingsEventHandler(this.pdQueryTextPageSettingsEvent);
                        document.EndPrint += new PrintEventHandler(this.pd_EndPrint);
                        document.PrinterSettings.MinimumPage = 1;
                        document.PrinterSettings.MaximumPage = (int) Math.Ceiling((double) (((double) this.mDataList.Count) / 68.0));
                        if (previewFlg)
                        {
                            using (PrintPreviewDialog dialog = new PrintPreviewDialog())
                            {
                                dialog.Shown += new EventHandler(this.pvActivateEvent);
                                dialog.Paint += new PaintEventHandler(this.printPreview_Paint);
                                dialog.MouseWheel += new MouseEventHandler(this.printPreview_MouseWheel);
                                dialog.Document = document;
                                dialog.Width = 800;
                                dialog.Height = 600;
                                dialog.PrintPreviewControl.Zoom = 0.75;
                                dialog.Icon = Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                                dialog.ShowInTaskbar = true;
                                this.mDrawPage = 0;
                                dialog.ShowDialog((Form) this.mSender);
                                return;
                            }
                        }
                        this.mDrawPage = 0;
                        document.Print();
                    }
                }
                catch (Exception exception)
                {
                    throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
                }
            }
            throw new NaccsException(MessageKind.Error, 0x25d, Resources.ResourceManager.GetString("COM17"));
        }

        private bool getBoolAttribute(XmlNode source, string tag)
        {
            string strA = XmlFunc.getStringAttribute(source, tag);
            if (strA.Length > 0)
            {
                return (string.Compare(strA, "TRUE", true) == 0);
            }
            string str2 = this.defaultProerty(source.Name, tag);
            return ((str2 != null) && (string.Compare(str2.Trim(), "TRUE", true) == 0));
        }

        private SolidBrush getBrushAttribute(XmlNode source)
        {
            if (source.Name != "label")
            {
                return null;
            }
            SolidBrush brush = null;
            string text = XmlFunc.getStringAttribute(source, "BackColor");
            if (text == "Window")
            {
                Color color = (Color) TypeDescriptor.GetConverter(new Color()).ConvertFromInvariantString(text);
                brush = new SolidBrush(color);
            }
            return brush;
        }

        private string GetDisplayPrintLineEditStr(XmlNode source, Font defaultFont)
        {
            SizeF component = new SizeF(0f, 0f);
            string text = XmlFunc.getStringAttribute(source, "Size");
            if (text.Length > 0)
            {
                component = (SizeF) TypeDescriptor.GetConverter(component).ConvertFromString(text);
            }
            string str2 = XmlFunc.getStringAttribute(source, "BorderStyle").Trim();
            if (str2.Length == 0)
            {
                str2 = this.defaultProerty(source.Name, "BorderStyle");
                if (str2 == null)
                {
                    str2 = BorderStyle.None.ToString();
                }
            }
            if (str2 == BorderStyle.Fixed3D.ToString())
            {
                this.BaseTxtBox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (str2 == BorderStyle.FixedSingle.ToString())
            {
                this.BaseTxtBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.BaseTxtBox.BorderStyle = BorderStyle.None;
            }
            Font font = this.getFontAttribute(source, defaultFont);
            this.BaseTxtBox.Font = font;
            string str3 = XmlFunc.getStringAttribute(source, "ScrollBars");
            if (!string.IsNullOrEmpty(str3))
            {
                try
                {
                    this.BaseTxtBox.ScrollBars = (ScrollBars) Enum.Parse(typeof(ScrollBars), str3, true);
                }
                catch
                {
                }
            }
            this.BaseTxtBox.Width = (int) component.Width;
            this.BaseTxtBox.Height = (int) component.Height;
            System.Drawing.Size clientSize = this.BaseTxtBox.ClientSize;
            if ((this.mConvMode == 1) || (str2 == BorderStyle.Fixed3D.ToString()))
            {
                clientSize.Width = this.BaseTxtBox.Width - 4;
                clientSize.Height = this.BaseTxtBox.Height - 4;
            }
            else
            {
                clientSize.Width = this.BaseTxtBox.Width;
                clientSize.Height = this.BaseTxtBox.Height;
            }
            switch (this.BaseTxtBox.ScrollBars)
            {
                case ScrollBars.Vertical:
                case ScrollBars.Both:
                    clientSize.Width -= 0x11;
                    break;
            }
            this.BaseTxtBox.ClientSize = clientSize;
            string innerText = source.InnerText;
            StringBuilder builder = new StringBuilder();
            int num = 0;
            int startIndex = 0;
            bool flag = false;
            foreach (char ch in innerText)
            {
                if (flag)
                {
                    startIndex += num;
                    num = 0;
                    flag = false;
                }
                else if (ComponentProperty.IsNewLineNeeded(innerText.Substring(startIndex, num + 1), this.BaseTxtBox))
                {
                    builder.Append(Environment.NewLine);
                    startIndex += num;
                    num = 0;
                }
                num++;
                builder.Append(ch);
                if (ch == '\n')
                {
                    flag = true;
                }
            }
            return builder.ToString();
        }

        private string getDocumentName(XmlDocument printXml)
        {
            if (this.mJobConfig != null)
            {
                return this.mRecordAtb.titile1;
            }
            return XmlFunc.getStringAttribute(printXml.SelectSingleNode("jobform"), "jobname");
        }

        private PointF getDrawMargin(PrintPageEventArgs ev, bool is_preview)
        {
            PointF tf = new PointF(0f, 0f);
            if (is_preview)
            {
                tf.X = ev.MarginBounds.Left;
                tf.Y = ev.MarginBounds.Top;
                return tf;
            }
            tf.X = ev.MarginBounds.Left - ev.PageSettings.HardMarginX;
            tf.Y = ev.MarginBounds.Top - ev.PageSettings.HardMarginY;
            return tf;
        }

        private Font getFontAttribute(XmlNode source, Font defFont)
        {
            Font font;
            if (defFont == null)
            {
                font = new Font("Tahoma", 11f);
            }
            else
            {
                font = new Font(defFont.Name, defFont.Size);
            }
            string str = XmlFunc.getStringAttribute(source, "Font");
            if (str.Length == 0)
            {
                string str2 = this.defaultProerty(source.Name, "Font");
                if ((defFont == null) && (str2 != null))
                {
                    font = ChangeFont.ConvertFromString(str2);
                }
                return font;
            }
            return ChangeFont.ConvertFromString(str);
        }

        private Color getFontColorAttribute(XmlNode source)
        {
            Color component = new Color();
            component = Color.Black;
            string text = XmlFunc.getStringAttribute(source, "ForeColor");
            if (text.Length > 0)
            {
                component = (Color) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(text);
            }
            return component;
        }

        private string GetLineEditStr(XmlNode source, Font defaultFont)
        {
            SizeF component = new SizeF(0f, 0f);
            string text = XmlFunc.getStringAttribute(source, "Size");
            if (text.Length > 0)
            {
                component = (SizeF) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(text);
            }
            string str2 = XmlFunc.getStringAttribute(source, "BorderStyle").Trim();
            if (str2.Length == 0)
            {
                str2 = this.defaultProerty(source.Name, "BorderStyle");
                if (str2 == null)
                {
                    str2 = BorderStyle.None.ToString();
                }
            }
            if (str2 == BorderStyle.Fixed3D.ToString())
            {
                this.BaseTxtBox.BorderStyle = BorderStyle.Fixed3D;
            }
            else if (str2 == BorderStyle.FixedSingle.ToString())
            {
                this.BaseTxtBox.BorderStyle = BorderStyle.FixedSingle;
            }
            else
            {
                this.BaseTxtBox.BorderStyle = BorderStyle.None;
            }
            Font font = this.getFontAttribute(source, defaultFont);
            this.BaseTxtBox.Font = font;
            this.BaseTxtBox.Width = (int) component.Width;
            this.BaseTxtBox.Height = (int) component.Height;
            int byteCnt = ComponentProperty.getTurnLength(this.BaseTxtBox);
            StringBuilder builder = new StringBuilder(source.InnerText);
            StringBuilder builder2 = new StringBuilder();
            StringBuilder builder3 = new StringBuilder();
            while (builder.Length > 0)
            {
                builder3.Remove(0, builder3.Length);
                builder3.Append(this.SF.CutByteLength(builder.ToString(), byteCnt));
                builder2.AppendLine(builder3.ToString());
                builder.Remove(0, builder3.Length);
            }
            if (builder2.Length > 0)
            {
                builder2.Remove(builder2.Length - 2, 2);
            }
            return builder2.ToString();
        }

        private PointF getLocationAttribute(XmlNode source)
        {
            float num = ((((float) this.mTop) / 10f) * SizeDef.DisplayPixelPerMiliMeter) * SizeDef.PrintPixelPerDisplayPixel;
            float num2 = ((((float) this.mLeft) / 10f) * SizeDef.DisplayPixelPerMiliMeter) * SizeDef.PrintPixelPerDisplayPixel;
            PointF tf = new PointF(0f, 0f);
            string text = XmlFunc.getStringAttribute(source, "Location");
            if (text.Length > 0)
            {
                Point component = new Point(0, 0);
                component = (Point) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(text);
                tf.X = component.X;
                tf.Y = component.Y;
            }
            tf.X *= SizeDef.PrintPixelPerDisplayPixel;
            tf.Y *= SizeDef.PrintPixelPerDisplayPixel;
            tf.X += num2;
            tf.Y += num;
            return tf;
        }

        private XmlNodeList getPageList(XmlDocument printXml)
        {
            XmlNodeList elementsByTagName = null;
            XmlNodeList list2 = printXml.SelectNodes("jobform/layout");
            if (list2.Count > 0)
            {
                elementsByTagName = ((XmlElement) list2[0]).GetElementsByTagName("page");
            }
            return elementsByTagName;
        }

        private System.Drawing.Printing.PaperSize getPaperSizeAttribute(XmlNode source)
        {
            string str = XmlFunc.getStringAttribute(source, "PaperSize");
            System.Drawing.Printing.PaperSize size = new System.Drawing.Printing.PaperSize();
            switch (str)
            {
                case "A5":
                    foreach (System.Drawing.Printing.PaperSize size2 in this.mPaperSizes)
                    {
                        if (size2.Kind == PaperKind.A5)
                        {
                            return size2;
                        }
                    }
                    return size;

                case "A3":
                    foreach (System.Drawing.Printing.PaperSize size3 in this.mPaperSizes)
                    {
                        if (size3.Kind == PaperKind.A3)
                        {
                            return size3;
                        }
                    }
                    return size;
            }
            foreach (System.Drawing.Printing.PaperSize size4 in this.mPaperSizes)
            {
                if (size4.Kind == PaperKind.A4)
                {
                    return size4;
                }
            }
            return size;
        }

        private Pen getPenAttribute(XmlNode source)
        {
            Pen pen = new Pen(Color.Black) {
                Alignment = PenAlignment.Center
            };
            string text = XmlFunc.getStringAttribute(source, "LineStyle");
            if (text.Length > 0)
            {
                LineStyle component = new LineStyle();
                component = (LineStyle) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(text);
                pen.Alignment = component.Alignment;
                pen.Color = component.Color;
                pen.DashStyle = component.DashStyle;
                pen.Width = component.Width;
            }
            return pen;
        }

        private Margins getPrinterMarginAttribute(XmlNode source)
        {
            Margins margins = new Margins(SizeDef.MARGINS_LEFT, SizeDef.MARGINS_RIGHT, SizeDef.MARGINS_TOP, SizeDef.MARGINS_BOTTOM);
            string text = XmlFunc.getStringAttribute(source, "PrintMargin");
            if (text.Length > 0)
            {
                Padding padding = (Padding) TypeDescriptor.GetConverter(new Padding()).ConvertFromInvariantString(text);
                margins.Left = padding.Left;
                margins.Right = padding.Right;
                margins.Top = padding.Top;
                margins.Bottom = padding.Bottom;
            }
            margins.Top = (int) Math.Round((double) (((float) margins.Top) / 0.254f));
            margins.Left = (int) Math.Round((double) (((float) margins.Left) / 0.254f));
            margins.Bottom = (int) Math.Round((double) (((float) margins.Bottom) / 0.254f));
            margins.Right = (int) Math.Round((double) (((float) margins.Right) / 0.254f));
            return margins;
        }

        private SizeF getSizeAttribute(XmlNode source)
        {
            SizeF component = new SizeF(0f, 0f);
            string text = XmlFunc.getStringAttribute(source, "Size");
            if (text.Length > 0)
            {
                component = (SizeF) TypeDescriptor.GetConverter(component).ConvertFromInvariantString(text);
            }
            component.Width *= SizeDef.PrintPixelPerDisplayPixel;
            component.Height *= SizeDef.PrintPixelPerDisplayPixel;
            return component;
        }

        private StringFormat getStringFormatAttribute(XmlNode source)
        {
            StringFormat format;
            if (this.mConvMode == 0)
            {
                format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.NoWrap);
            }
            else
            {
                format = (StringFormat) StringFormat.GenericTypographic.Clone();
                format.FormatFlags |= StringFormatFlags.NoClip;
                format.FormatFlags |= StringFormatFlags.NoWrap;
                format.FormatFlags -= 0x2000;
            }
            string str = XmlFunc.getStringAttribute(source, "TextAlign");
            if (str.Length == 0)
            {
                str = this.defaultProerty(source.Name, "TextAlign");
            }
            if (source.Name == "prttextbox")
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
                if (str != null)
                {
                    if (str == HorizontalAlignment.Right.ToString())
                    {
                        format.Alignment = StringAlignment.Far;
                        format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                        return format;
                    }
                    if (str == HorizontalAlignment.Center.ToString())
                    {
                        format.Alignment = StringAlignment.Center;
                        format.FormatFlags |= StringFormatFlags.MeasureTrailingSpaces;
                    }
                }
                return format;
            }
            if ((source.Name == "checkbox") || (source.Name == "radio"))
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Center;
            }
            else
            {
                format.Alignment = StringAlignment.Near;
                format.LineAlignment = StringAlignment.Near;
            }
            if (str != null)
            {
                if (str == ContentAlignment.TopLeft.ToString())
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Near;
                    return format;
                }
                if (str == ContentAlignment.TopCenter.ToString())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Near;
                    return format;
                }
                if (str == ContentAlignment.TopRight.ToString())
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Near;
                    return format;
                }
                if (str == ContentAlignment.MiddleLeft.ToString())
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Center;
                    return format;
                }
                if (str == ContentAlignment.MiddleCenter.ToString())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Center;
                    return format;
                }
                if (str == ContentAlignment.MiddleRight.ToString())
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Center;
                    return format;
                }
                if (str == ContentAlignment.BottomLeft.ToString())
                {
                    format.Alignment = StringAlignment.Near;
                    format.LineAlignment = StringAlignment.Far;
                    return format;
                }
                if (str == ContentAlignment.BottomCenter.ToString())
                {
                    format.Alignment = StringAlignment.Center;
                    format.LineAlignment = StringAlignment.Far;
                    return format;
                }
                if (str == ContentAlignment.BottomRight.ToString())
                {
                    format.Alignment = StringAlignment.Far;
                    format.LineAlignment = StringAlignment.Far;
                }
            }
            return format;
        }

        private bool hasNextPages(PrinterSettings printerSettings, bool is_preview)
        {
            if (this.mDrawPage >= this.mPageList.Count)
            {
                return false;
            }
            if ((!is_preview && (printerSettings.PrintRange == PrintRange.SomePages)) && (this.mDrawPage >= printerSettings.ToPage))
            {
                return false;
            }
            return true;
        }

        private void pd_EndPrint(object sender, PrintEventArgs e)
        {
            int num;
            if (e.PrintAction == PrintAction.PrintToPreview)
            {
                num = 1;
            }
            else if (e.Cancel)
            {
                num = -1;
            }
            else
            {
                num = 0;
            }
            if (this.OnPrintEnd != null)
            {
                this.OnPrintEnd(this.mPrintKey, this.mPrintDlgMode, num);
            }
        }

        private void pdBeginPrint(object sender, PrintEventArgs ev)
        {
            this.mDrawPage = 0;
            PrintDocument document = (PrintDocument) sender;
            this.setPrinterSettings(sender);
            if ((ev.PrintAction != PrintAction.PrintToPreview) && (this.mPrintDlgMode == 0))
            {
                using (PrintDialog dialog = new PrintDialog())
                {
                    if (IntPtr.Size == 8)
                    {
                        dialog.UseEXDialog = true;
                    }
                    dialog.PrinterSettings = document.PrinterSettings;
                    dialog.AllowSomePages = true;
                    dialog.PrinterSettings.FromPage = dialog.PrinterSettings.MinimumPage;
                    dialog.PrinterSettings.ToPage = dialog.PrinterSettings.MaximumPage;
                    if (dialog.ShowDialog() == DialogResult.OK)
                    {
                        if (dialog.PrinterSettings.PrintRange == PrintRange.SomePages)
                        {
                            this.mDrawPage = dialog.PrinterSettings.FromPage - 1;
                        }
                        document.PrinterSettings = dialog.PrinterSettings;
                        this.setMarginPepar(document.PrinterSettings.PrinterName, document.DefaultPageSettings.PaperSource.RawKind);
                    }
                    else
                    {
                        ev.Cancel = true;
                    }
                }
            }
            this.mPaperSizes = document.PrinterSettings.PaperSizes;
        }

        private void pdPrintPage(object sender, PrintPageEventArgs ev)
        {
            try
            {
                PrintDocument document = (PrintDocument) sender;
                Graphics g = ev.Graphics;
                g.PageUnit = GraphicsUnit.Inch;
                g.PageScale = 0.01f;
                if (this.mIsNullData)
                {
                    this.drawPrintableArea(ev, document.PrintController.IsPreview);
                }
                PointF margin = this.getDrawMargin(ev, document.PrintController.IsPreview);
                this.drawStampMark(ev.Graphics);
                using (Font font = this.getFontAttribute(this.mPageList[this.mDrawPage], null))
                {
                    for (int i = this.mPageList[this.mDrawPage].ChildNodes.Count; i > 0; i--)
                    {
                        this.drawNode(g, this.mPageList[this.mDrawPage].ChildNodes[i - 1], margin, font);
                    }
                    this.mDrawPage++;
                    if (this.hasNextPages(document.PrinterSettings, document.PrintController.IsPreview))
                    {
                        ev.HasMorePages = true;
                    }
                    else
                    {
                        ev.HasMorePages = false;
                        this.mDrawPage = 0;
                    }
                }
            }
            catch (NaccsException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
        }

        private void pdQueryPageSettingsEvent(object sender, QueryPageSettingsEventArgs ev)
        {
            if (string.IsNullOrEmpty(this.mPaperSize))
            {
                ev.PageSettings.PaperSize = this.getPaperSizeAttribute(this.mPageList[this.mDrawPage]);
            }
            else
            {
                foreach (System.Drawing.Printing.PaperSize size in this.mPaperSizes)
                {
                    if (size.PaperName == this.mPaperSize)
                    {
                        ev.PageSettings.PaperSize = size;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(this.mOrientation))
            {
                ev.PageSettings.Landscape = this.getBoolAttribute(this.mPageList[this.mDrawPage], "LandScape");
            }
            else
            {
                ev.PageSettings.Landscape = this.mOrientation == Resources.ResourceManager.GetString("MSG0207");
            }
            ev.PageSettings.Margins = this.getPrinterMarginAttribute(this.mPageList[this.mDrawPage]);
        }

        private void pdQueryTextPageSettingsEvent(object sender, QueryPageSettingsEventArgs ev)
        {
            if (string.IsNullOrEmpty(this.mPaperSize))
            {
                foreach (System.Drawing.Printing.PaperSize size in ev.PageSettings.PrinterSettings.PaperSizes)
                {
                    if (size.Kind == PaperKind.A4)
                    {
                        ev.PageSettings.PaperSize = size;
                        break;
                    }
                }
            }
            else
            {
                foreach (System.Drawing.Printing.PaperSize size2 in ev.PageSettings.PrinterSettings.PaperSizes)
                {
                    if (size2.PaperName == this.mPaperSize)
                    {
                        ev.PageSettings.PaperSize = size2;
                        break;
                    }
                }
            }
            if (string.IsNullOrEmpty(this.mOrientation))
            {
                ev.PageSettings.Landscape = false;
            }
            else
            {
                ev.PageSettings.Landscape = this.mOrientation == Resources.ResourceManager.GetString("MSG0207");
            }
            ev.PageSettings.Margins.Left = (int) Math.Round((double) 39.370077483689094);
            ev.PageSettings.Margins.Top = (int) Math.Round((double) 39.370077483689094);
            ev.PageSettings.Margins.Right = (int) Math.Round((double) 39.370077483689094);
            ev.PageSettings.Margins.Bottom = (int) Math.Round((double) 39.370077483689094);
        }

        private void pdTextPrintPage(object sender, PrintPageEventArgs ev)
        {
            float num = 0f;
            int num2 = 0;
            float num3 = ((((float) this.mTop) / 10f) * SizeDef.DisplayPixelPerMiliMeter) * SizeDef.PrintPixelPerDisplayPixel;
            float num4 = ((((float) this.mLeft) / 10f) * SizeDef.DisplayPixelPerMiliMeter) * SizeDef.PrintPixelPerDisplayPixel;
            PointF tf = this.getDrawMargin(ev, ((PrintDocument) sender).PrintController.IsPreview);
            tf.X += num4;
            tf.Y += num3;
            string text = null;
            this.drawStampMark(ev.Graphics);
            PointF loc = new PointF(tf.X, tf.Y);
            Font font = new Font("Tahoma", 11f);
            using (font)
            {
                int num5 = (int) Math.Ceiling((double) font.GetHeight(ev.Graphics));
                num = ev.MarginBounds.Height / num5;
                SizeF size = new SizeF((float) ev.MarginBounds.Width, (float) num5);
                using (StringFormat format = new StringFormat())
                {
                    format.FormatFlags = StringFormatFlags.NoWrap;
                    using (SolidBrush brush = new SolidBrush(Color.Black))
                    {
                        while ((num2 < num) && ((num2 + (this.mDrawPage * ((int) num))) < this.mDataList.Count))
                        {
                            loc.Y = tf.Y + (num2 * num5);
                            text = this.mDataList[num2 + (this.mDrawPage * ((int) num))];
                            text = text.Replace("‐", "－");
                            this.drawText(ev.Graphics, text, font, brush, loc, size, format, false);
                            num2++;
                        }
                        loc.Y = tf.Y + (num * num5);
                        format.Alignment = StringAlignment.Center;
                        this.drawText(ev.Graphics, string.Format("- {0:d} -", this.mDrawPage + 1), font, brush, loc, size, format, false);
                    }
                }
            }
            if ((num2 + (this.mDrawPage * ((int) num))) < this.mDataList.Count)
            {
                this.mDrawPage++;
                ev.HasMorePages = true;
            }
            else
            {
                this.mDrawPage = 0;
                ev.HasMorePages = false;
            }
        }

        public void printDataView(List<string> data, bool previewFlg)
        {
            try
            {
                this.mConvMode = 0;
                if (this.mTimeStamp.Ticks == 0L)
                {
                    this.mTimeStamp = DateTime.Now;
                }
                if (data == null)
                {
                    this.mIsNullData = true;
                }
                PrintXml xml = new PrintXml();
                XmlDocument source = new XmlDocument();
                source.LoadXml(DllResource.DataViewListTmp);
                xml.PrintPageNo = 0;
                xml.ConvertMode = 0;
                xml.Contained = this.mContained;
                xml.TimeStamp = this.mTimeStamp;
                XmlDocument printXml = xml.createPrintXml(source, data);
                this.execPrint(printXml, previewFlg);
            }
            catch (NaccsException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
        }

        public void printDisplay(string templateFile, List<string> data, bool previewFlg)
        {
            this.mConvMode = 1;
            if (this.mTimeStamp.Ticks == 0L)
            {
                this.mTimeStamp = DateTime.Now;
            }
            XmlDocument source = ConvertTemplate.CreateInstance(true).convertDisplayToReport(templateFile);
            if (data == null)
            {
                this.mIsNullData = true;
            }
            XmlDocument printXml = new PrintXml { ConvertMode = this.mConvMode, JobConf = this.mJobConfig, InfoUniqueNo = this.mInputInfo, ServerRecvTime = this.mServerRecvTime, TimeStamp = this.mTimeStamp }.createPrintXml(source, data);
            this.execPrint(printXml, previewFlg);
        }

        private void printPreview_MouseWheel(object sender, MouseEventArgs e)
        {
            PrintPreviewControl printPreviewControl = ((PrintPreviewDialog) sender).PrintPreviewControl;
            if (e.Delta < 0)
            {
                SendMessage(printPreviewControl.Handle, 0x115, 3, 0);
            }
            else
            {
                SendMessage(printPreviewControl.Handle, 0x115, 2, 0);
            }
        }

        private void printPreview_Paint(object sender, PaintEventArgs e)
        {
            ((Form) sender).Activate();
        }

        public void printReport(string templateFile, List<string> data, bool previewFlg)
        {
            this.mConvMode = 0;
            if (this.mTimeStamp.Ticks == 0L)
            {
                this.mTimeStamp = DateTime.Now;
            }
            XmlDocument source = new XmlDocument();
            source.Load(templateFile);
            if (data == null)
            {
                this.mIsNullData = true;
            }
            XmlDocument printXml = new PrintXml { ConvertMode = 0, JobConf = this.mJobConfig, InfoUniqueNo = this.mInputInfo, ServerRecvTime = this.mServerRecvTime, TimeStamp = this.mTimeStamp }.createPrintXml(source, data);
            this.execPrint(printXml, previewFlg);
        }

        public void printTemplate(AbstractJobConfig jobconf, List<string> data, bool previewFlg)
        {
            try
            {
                if (jobconf != null)
                {
                    XmlDocument source = null;
                    this.mConvMode = 0;
                    if (data == null)
                    {
                        this.mIsNullData = true;
                    }
                    if (this.mTimeStamp.Ticks == 0L)
                    {
                        this.mTimeStamp = DateTime.Now;
                    }
                    this.JobConfig = jobconf;
                    PrintXml xml = new PrintXml();
                    if ((this.mJobConfig.jobStyle == JobStyle.normal) || (this.mJobConfig.jobStyle == JobStyle.combi))
                    {
                        string str;
                        if (this.mJobConfig.jobStyle == JobStyle.combi)
                        {
                            this.mJobConfig = ((CombiConfig) this.mJobConfig).configs[1].config;
                        }
                        NormalConfig mJobConfig = (NormalConfig) this.mJobConfig;
                        if (mJobConfig.Print.Trim().Length == 0)
                        {
                            ConvertTemplate template = ConvertTemplate.CreateInstance(true);
                            str = mJobConfig.Display.Trim();
                            if (this.mCustomTemplatePath.Length > 0)
                            {
                                string fileName = Path.GetFileName(str);
                                str = this.mCustomTemplatePath.Trim(new char[] { '\\' }) + @"\" + fileName;
                            }
                            source = template.convertDisplayToReport(str);
                            this.mConvMode = 1;
                        }
                        else
                        {
                            source = new XmlDocument();
                            str = mJobConfig.Print.Trim();
                            if (this.mCustomTemplatePath.Length > 0)
                            {
                                string str3 = Path.GetFileName(str);
                                str = this.mCustomTemplatePath.Trim(new char[] { '\\' }) + @"\" + str3;
                            }
                            source.Load(str);
                            this.mConvMode = 0;
                        }
                        xml.PrintPageNo = 0;
                        xml.ConvertMode = this.mConvMode;
                        xml.JobConf = jobconf;
                        xml.InfoUniqueNo = this.mInputInfo;
                        xml.ServerRecvTime = this.mServerRecvTime;
                        xml.Contained = this.mContained;
                        xml.TimeStamp = this.mTimeStamp;
                        XmlDocument printXml = xml.createPrintXml(source, data);
                        this.execPrint(printXml, previewFlg);
                    }
                    else if (this.mJobConfig.jobStyle == JobStyle.divide)
                    {
                        DivideConfig config2 = (DivideConfig) this.mJobConfig;
                        if (config2.printDivision == null)
                        {
                            throw new NaccsException(MessageKind.Error, 0x25c, Resources.ResourceManager.GetString("COM16"));
                        }
                        source = new XmlDocument();
                        XmlDocument[] documentArray = new XmlDocument[config2.printDivision.count];
                        XmlDocument itemxml = new XmlDocument();
                        itemxml.Load(config2.printDivision.iteminfo);
                        xml.PrintPageNo = 0;
                        for (int i = 0; i < config2.printDivision.count; i++)
                        {
                            string filename = config2.printDivision.templates[i].filename;
                            if (this.mCustomTemplatePath.Length > 0)
                            {
                                string str5 = Path.GetFileName(filename);
                                filename = this.mCustomTemplatePath.Trim(new char[] { '\\' }) + @"\" + str5;
                            }
                            source.Load(filename);
                            xml.ConvertMode = this.mConvMode;
                            xml.JobConf = jobconf;
                            xml.InfoUniqueNo = this.mInputInfo;
                            xml.ServerRecvTime = this.mServerRecvTime;
                            xml.Contained = this.mContained;
                            xml.TimeStamp = this.mTimeStamp;
                            documentArray[i] = xml.createPrintXml(source, itemxml, data);
                        }
                        XmlDocument document4 = documentArray[0];
                        for (int j = 1; j < config2.printDivision.count; j++)
                        {
                            foreach (XmlNode node in documentArray[j].SelectNodes("jobform/layout/page"))
                            {
                                XmlFunc.copyXmlNode(document4.SelectSingleNode("jobform/layout"), node);
                            }
                        }
                        this.execPrint(document4, previewFlg);
                    }
                }
            }
            catch (NaccsException)
            {
                throw;
            }
            catch (Exception exception)
            {
                throw new NaccsException(MessageKind.Error, 0x261, exception.Message);
            }
        }

        private void pvActivateEvent(object sender, EventArgs e)
        {
            ((Form) sender).ShowIcon = false;
        }

        [DllImport("user32.dll", CharSet=CharSet.Auto)]
        private static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        private void setMarginPepar(string PrinterName, int BinNo)
        {
            if (((this.mPrinterName != PrinterName) || (this.mBinNo != BinNo)) && (!string.IsNullOrEmpty(this.mPrinterSettingsPath) && File.Exists(this.mPrinterSettingsPath)))
            {
                XmlDocument document = new XmlDocument();
                document.Load(this.mPrinterSettingsPath);
                string xpath = "Printer/PrinterInfoList/PrinterInfo[@Name='" + PrinterName + "']/BinList/Bin[@No='" + BinNo.ToString() + "']";
                XmlNode node = document.SelectSingleNode(xpath);
                this.mTop = 0;
                this.mLeft = 0;
                this.mPaperSize = "";
                this.mOrientation = "";
                if (node != null)
                {
                    foreach (XmlNode node2 in node.ChildNodes)
                    {
                        string name = node2.Name;
                        if (name != null)
                        {
                            if (!(name == "T"))
                            {
                                if (name == "L")
                                {
                                    goto Label_0127;
                                }
                                if (name == "Size")
                                {
                                    goto Label_013A;
                                }
                                if (name == "Orientation")
                                {
                                    goto Label_0148;
                                }
                            }
                            else
                            {
                                this.mTop = int.Parse(node2.InnerText);
                            }
                        }
                        continue;
                    Label_0127:
                        this.mLeft = int.Parse(node2.InnerText);
                        continue;
                    Label_013A:
                        this.mPaperSize = node2.InnerText;
                        continue;
                    Label_0148:
                        this.mOrientation = node2.InnerText;
                    }
                }
            }
        }

        private void setPrinterSettings(object sender)
        {
            PrintDocument document = (PrintDocument) sender;
            if (this.mPrinterName.Trim().Length > 0)
            {
                document.PrinterSettings.PrinterName = this.mPrinterName;
            }
            if (this.mBinNo != 0)
            {
                foreach (PaperSource source in document.PrinterSettings.PaperSources)
                {
                    if (source.RawKind == this.mBinNo)
                    {
                        document.PrinterSettings.DefaultPageSettings.PaperSource = source;
                        document.DefaultPageSettings.PaperSource = source;
                        break;
                    }
                }
            }
            if (this.mDoublePrint.Trim().Length > 0)
            {
                string mDoublePrint = this.mDoublePrint;
                if (mDoublePrint == null)
                {
                    goto Label_012D;
                }
                if (!(mDoublePrint == "Default"))
                {
                    if (mDoublePrint == "Horizontal")
                    {
                        document.PrinterSettings.Duplex = Duplex.Horizontal;
                        goto Label_0139;
                    }
                    if (mDoublePrint == "Simplex")
                    {
                        document.PrinterSettings.Duplex = Duplex.Simplex;
                        goto Label_0139;
                    }
                    if (mDoublePrint == "Vertical")
                    {
                        document.PrinterSettings.Duplex = Duplex.Vertical;
                        goto Label_0139;
                    }
                    goto Label_012D;
                }
                document.PrinterSettings.Duplex = Duplex.Default;
            }
            goto Label_0139;
        Label_012D:
            document.PrinterSettings.Duplex = Duplex.Default;
        Label_0139:
            document.PrinterSettings.Copies = (short) this.mCopies;
        }

        public int BinNo
        {
            set
            {
                this.mBinNo = value;
            }
        }

        public int Contained
        {
            set
            {
                this.mContained = value;
            }
        }

        public int Copies
        {
            set
            {
                this.mCopies = value;
            }
        }

        public string CustomTemplatePath
        {
            set
            {
                this.mCustomTemplatePath = value;
            }
        }

        public string DoublePrint
        {
            set
            {
                this.mDoublePrint = value;
            }
        }

        public string FontFilePath
        {
            set
            {
                this.mFontFilePath = value;
            }
        }

        public string GStampDateAlign
        {
            set
            {
                this.mGStampDateAlign = value;
            }
        }

        public string GStampInfo
        {
            set
            {
                this.mGStampInfo = value;
            }
        }

        public string ImportFontFiles
        {
            set
            {
                this.mImportFontFiles = value;
            }
        }

        public string InputInfo
        {
            set
            {
                this.mInputInfo = value;
            }
        }

        public bool IsWriteGStamp
        {
            set
            {
                this.mIsWriteGStamp = value;
            }
        }

        public AbstractJobConfig JobConfig
        {
            set
            {
                this.mJobConfig = value;
                if (this.mJobConfig != null)
                {
                    switch (this.mJobConfig.jobStyle)
                    {
                        case JobStyle.combi:
                            this.mRecordAtb = ((NormalConfig) this.mJobConfig).record;
                            return;

                        case JobStyle.divide:
                            this.mRecordAtb = ((DivideConfig) this.mJobConfig).record;
                            return;
                    }
                    this.mRecordAtb = ((NormalConfig) this.mJobConfig).record;
                }
            }
        }

        public int Left
        {
            set
            {
                this.mLeft = value;
            }
        }

        public string OcrFontName
        {
            set
            {
                this.mOcrFontName = value;
            }
        }

        public float OcrFontSize
        {
            set
            {
                this.mOcrFontSize = value;
            }
        }

        public string Orientation
        {
            set
            {
                this.mOrientation = value;
            }
        }

        public object OwnerSender
        {
            set
            {
                this.mSender = value;
            }
        }

        public string PaperSize
        {
            set
            {
                this.mPaperSize = value;
            }
        }

        public int PrintDlgMode
        {
            set
            {
                this.mPrintDlgMode = value;
            }
        }

        public string PrinterName
        {
            set
            {
                this.mPrinterName = value;
            }
        }

        public string PrinterSettingsPath
        {
            set
            {
                this.mPrinterSettingsPath = value;
            }
        }

        public string PrintKey
        {
            set
            {
                this.mPrintKey = value;
            }
        }

        public string ServerRecvTime
        {
            set
            {
                this.mServerRecvTime = value;
            }
        }

        public string SpcFontName
        {
            set
            {
                this.mSpcFontName = value;
            }
        }

        public string StampMarkText
        {
            set
            {
                this.mStampMarkText = value;
            }
        }

        public DateTime TimeStamp
        {
            set
            {
                this.mTimeStamp = value;
            }
        }

        public int Top
        {
            set
            {
                this.mTop = value;
            }
        }
    }
}

