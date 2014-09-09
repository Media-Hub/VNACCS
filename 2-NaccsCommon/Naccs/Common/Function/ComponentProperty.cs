namespace Naccs.Common.Function
{
    using Naccs.Common.Constant;
    using Naccs.Common.CustomControls;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;
    using System.Xml;

    public class ComponentProperty
    {
        private const int cnMaxTurnLength = 180;
        private bool isPrtLayout;

        public ComponentProperty()
        {
        }

        public ComponentProperty(bool isPrtLayout)
        {
            this.isPrtLayout = isPrtLayout;
        }

        private static int getDrawLength(Graphics g, Font fnt, RectangleF rect, StringFormat sf, int maxLen)
        {
            CharacterRange[] ranges = new CharacterRange[] { new CharacterRange(0, 1) };
            float num = 1f;
            float width = 0f;
            int num3 = 2;
            string text = StrFunc.CreateInstance().MakeCycleStr(maxLen, "X");
            while ((num3 <= maxLen) && (width != num))
            {
                num = width;
                ranges[0].Length = num3;
                sf.SetMeasurableCharacterRanges(ranges);
                width = g.MeasureCharacterRanges(text, fnt, rect, sf)[0].GetBounds(g).Width;
                num3++;
            }
            if (num3 == maxLen)
            {
                return maxLen;
            }
            return (num3 - 1);
        }

        public static string getPropertyDefault(System.Type type, string propName)
        {
            PropertyDescriptor descriptor = TypeDescriptor.GetProperties(type)[propName];
            if (descriptor != null)
            {
                DefaultValueAttribute attribute = descriptor.Attributes[typeof(DefaultValueAttribute)] as DefaultValueAttribute;
                if ((attribute != null) && (attribute.Value != null))
                {
                    return attribute.Value.ToString();
                }
            }
            return null;
        }

        public static int getTurnLength(Control ctl)
        {
            return getTurnLength(ctl, 180);
        }

        public static int getTurnLength(Control ctl, int maxLen)
        {
            int num3;
            TextBox box = ctl as TextBox;
            if (box == null)
            {
                return maxLen;
            }
            float width = box.Width;
            int num2 = SizeDef.prtTextMargin(box.BorderStyle.ToString());
            width -= num2 * 2;
            if (box.Font.Size == 8f)
            {
                width *= 0.907f;
            }
            else if (box.Font.Size == 9f)
            {
                width *= 1.02f;
            }
            else if (box.Font.Size == 10f)
            {
                width *= 0.9681f;
            }
            else if (box.Font.Size == 11f)
            {
                width *= 0.934f;
            }
            else if (box.Font.Size == 12f)
            {
                width *= 1.0143f;
            }
            else
            {
                width -= 6f;
            }
            RectangleF rect = new RectangleF(0f, 0f, width, (float) box.ClientSize.Height);
            using (PictureBox box2 = new PictureBox())
            {
                using (Graphics graphics = box2.CreateGraphics())
                {
                    using (StringFormat format = new StringFormat(StringFormatFlags.NoClip | StringFormatFlags.NoWrap))
                    {
                        num3 = getDrawLength(graphics, box.Font, rect, format, maxLen);
                    }
                }
            }
            return num3;
        }

        public static bool IsNewLineNeeded(string text, TextBox tb)
        {
            TextFormatFlags noPrefix = TextFormatFlags.NoPrefix;
            using (Graphics graphics = tb.CreateGraphics())
            {
                System.Drawing.Size proposedSize = new System.Drawing.Size(0x7fffffff, 0x7fffffff);
                System.Drawing.Size size2 = TextRenderer.MeasureText(graphics, text, tb.Font, proposedSize, noPrefix);
                return (tb.ClientSize.Width < size2.Width);
            }
        }

        public void setControlCommonProps(XmlReader Xml, Control control)
        {
            bool flag = false;
            for (int i = 0; i <= (Xml.AttributeCount - 1); i++)
            {
                Xml.MoveToAttribute(i);
                PropertyDescriptor descriptor = TypeDescriptor.GetProperties(control)[Xml.Name];
                if (descriptor != null)
                {
                    if (Xml.Name == "Font")
                    {
                        string str = Xml.Value.ToString();
                        if (!string.IsNullOrEmpty(str))
                        {
                            descriptor.SetValue(control, descriptor.Converter.ConvertFromInvariantString(str));
                            flag = true;
                        }
                    }
                    else
                    {
                        descriptor.SetValue(control, descriptor.Converter.ConvertFromInvariantString(Xml.Value));
                    }
                }
            }
            if (!flag)
            {
                float num2;
                if (this.isPrtLayout)
                {
                    num2 = 11f;
                }
                else
                {
                    num2 = 9f;
                }
                if (control is LBDataGridView)
                {
                    ((LBDataGridView) control).ColumnHeadersDefaultCellStyle.Font = new Font("Tahoma", num2, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                    control.Font = new Font("Courier New", num2, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                }
                else if ((((control is LBTextBox) || (control is LBMaskedTextBox)) || ((control is LBCommaTextBox) || (control is LBComboBox))) || (((control is LBCheckBox) || (control is LBRadioButton)) || (control is LBPrtTextBox)))
                {
                    control.Font = new Font("Courier New", num2, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                }
                else
                {
                    control.Font = new Font("Tahoma", num2, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                }
            }
            if (Xml.GetAttribute("id") != null)
            {
                control.Tag = Xml.GetAttribute("id");
            }
        }
    }
}

