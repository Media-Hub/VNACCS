using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Metro.ColorTables;
using System.Drawing;
using System.Drawing.Imaging;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroTabItemPainter : MetroRenderer
    {
        public override void Render(MetroRendererInfo renderingInfo)
        {
            MetroTabItem tab = (MetroTabItem)renderingInfo.Control;
            Graphics g = renderingInfo.PaintEventArgs.Graphics;
            MetroTabItemColorTable cti = renderingInfo.ColorTable.MetroTab.MetroTabItem;
            MetroTabItemStateColorTable color = cti.Default;
            if (!tab.Enabled)
                color = cti.Disabled;
            else if (tab.Checked)
                color = cti.Selected;
            else if (tab.IsMouseDown && cti.Pressed != null)
                color = cti.Pressed;
            else if (tab.IsMouseOver && cti.MouseOver != null)
                color = cti.MouseOver;

            Rectangle bounds = tab.Bounds;
            Rectangle textBounds = tab.TextRenderBounds;
            Rectangle imageBounds = tab.ImageRenderBounds;
            CompositeImage image = tab.GetImage();

            if (color.Background != null)
            {
                Font font = renderingInfo.DefaultFont;
                ElementStyleDisplayInfo di = new ElementStyleDisplayInfo(color.Background, g, bounds);
                ElementStyleDisplay.Paint(di);

                if (image != null && tab.ButtonStyle != eButtonStyle.TextOnlyAlways)
                {
                    if (imageBounds.IsEmpty)
                        imageBounds = GetImageRectangle(tab, image);
                    if (textBounds.IsEmpty)
                        textBounds = GetTextRectangle(tab, image, imageBounds);

                }
                else if (textBounds.IsEmpty)
                    textBounds = bounds;

                if (tab.TextMarkupBody == null)
                {
                    di.Bounds = textBounds;
                    
                    ElementStyleDisplay.PaintText(di, tab.Text, font);
                }
                else
                {
                    eTextFormat stringFormat = eTextFormat.HorizontalCenter | eTextFormat.VerticalCenter;
                    TextMarkup.MarkupDrawContext d = new TextMarkup.MarkupDrawContext(g, font, color.Background.TextColor, renderingInfo.RightToLeft);
                    d.HotKeyPrefixVisible = !((stringFormat & eTextFormat.HidePrefix) == eTextFormat.HidePrefix);
                    d.ContextObject = tab;
                    Rectangle mr = new Rectangle(textBounds.X, textBounds.Y + (textBounds.Height - tab.TextMarkupBody.Bounds.Height) / 2 + 1, tab.TextMarkupBody.Bounds.Width, tab.TextMarkupBody.Bounds.Height);
                    if ((stringFormat & eTextFormat.HorizontalCenter) != 0)
                        mr.Offset((textBounds.Width - mr.Width) / 2, 0);
                    if (tab._FixedSizeCenterText) mr.Y--;
                    tab.TextMarkupBody.Bounds = mr;
                    tab.TextMarkupBody.Render(d);
                }
                tab.TextRenderBounds = textBounds;
                tab.ImageRenderBounds = imageBounds;
            }

            if (image != null)
            {
                if (!tab.IsMouseOver && tab.HotTrackingStyle == eHotTrackingStyle.Color)
                {
                    // Draw gray-scale image for this hover style...
                    float[][] array = new float[5][];
                    array[0] = new float[5] { 0.2125f, 0.2125f, 0.2125f, 0, 0 };
                    array[1] = new float[5] { 0.5f, 0.5f, 0.5f, 0, 0 };
                    array[2] = new float[5] { 0.0361f, 0.0361f, 0.0361f, 0, 0 };
                    array[3] = new float[5] { 0, 0, 0, 1, 0 };
                    array[4] = new float[5] { 0.2f, 0.2f, 0.2f, 0, 1 };
                    ColorMatrix grayMatrix = new ColorMatrix(array);
                    ImageAttributes att = new ImageAttributes();
                    att.SetColorMatrix(grayMatrix);
                    image.DrawImage(g, imageBounds, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, att);
                }
                else
                {
                    image.DrawImage(g, imageBounds);
                }
            }

            //g.FillRectangle(Brushes.Red, bounds);
        }

        private Rectangle GetImageRectangle(MetroTabItem tab, CompositeImage image)
        {
            Rectangle imageRect = Rectangle.Empty;
            // Calculate image position
            if (image != null)
            {
                Size imageSize = tab.ImageSize;

                if (tab.ImagePosition == eImagePosition.Top || tab.ImagePosition == eImagePosition.Bottom)
                    imageRect = new Rectangle(tab.ImageDrawRect.X, tab.ImageDrawRect.Y, tab.DisplayRectangle.Width, tab.ImageDrawRect.Height);
                else if(tab.ImagePosition == eImagePosition.Left)
                    imageRect = new Rectangle(tab.ImageDrawRect.X+4, tab.ImageDrawRect.Y, tab.ImageDrawRect.Width, tab.ImageDrawRect.Height);
                else if (tab.ImagePosition == eImagePosition.Right)
                    imageRect = new Rectangle(tab.ImageDrawRect.X + tab.ImagePaddingHorizontal+4, tab.ImageDrawRect.Y, tab.ImageDrawRect.Width, tab.ImageDrawRect.Height);
                imageRect.Offset(tab.DisplayRectangle.Left, tab.DisplayRectangle.Top);
                imageRect.Offset((imageRect.Width - imageSize.Width) / 2, (imageRect.Height - imageSize.Height) / 2);

                imageRect.Width = imageSize.Width;
                imageRect.Height = imageSize.Height;
            }

            return imageRect;
        }

        private Rectangle GetTextRectangle(MetroTabItem tab, CompositeImage image, Rectangle imageBounds)
        {
            Rectangle itemRect = tab.DisplayRectangle;
            Rectangle textRect = tab.TextDrawRect;

            if (tab.ImagePosition == eImagePosition.Top || tab.ImagePosition == eImagePosition.Bottom)
            {
                textRect = new Rectangle(1, textRect.Y, itemRect.Width - 2, textRect.Height);
            }
            textRect.Offset(itemRect.Left, itemRect.Top);

            if (tab.ImagePosition == eImagePosition.Left)
                textRect.X = imageBounds.Right + tab.ImagePaddingHorizontal;

            if (textRect.Right > itemRect.Right)
                textRect.Width = itemRect.Right - textRect.Left;

            return textRect;
        }
    }
}
