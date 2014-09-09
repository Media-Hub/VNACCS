using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.Metro.ColorTables;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroTabStripPainter : MetroRenderer
    {
        public override void Render(MetroRendererInfo renderingInfo)
        {
            MetroTabStrip strip = (MetroTabStrip)renderingInfo.Control;
            Graphics g = renderingInfo.PaintEventArgs.Graphics;
            MetroTabStripColorTable ct = renderingInfo.ColorTable.MetroTab.TabStrip;
            Rectangle bounds = strip.ClientRectangle;

            if (ct.BackgroundStyle != null)
            {
                ElementStyleDisplayInfo di = new ElementStyleDisplayInfo(ct.BackgroundStyle, g, bounds);
                ElementStyleDisplay.PaintBackground(di);
            }

            if (strip.CaptionVisible)
            {
                if (strip.CaptionBounds.IsEmpty || strip.SystemCaptionItemBounds.IsEmpty || strip.QuickToolbarBounds.IsEmpty)
                    SetQatAndCaptionItemBounds(strip, renderingInfo);
                Color captionTextColor = renderingInfo.ColorTable.MetroTab.ActiveCaptionText;
                eTextFormat textFormat = renderingInfo.ColorTable.MetroTab.CaptionTextFormat;
                System.Windows.Forms.Form form = strip.FindForm();
                bool isFormActive = true;
                if (form != null && (form != System.Windows.Forms.Form.ActiveForm && form.MdiParent == null ||
                    form.MdiParent != null && form.MdiParent.ActiveMdiChild != form))
                {
                    captionTextColor = renderingInfo.ColorTable.MetroTab.InactiveCaptionText;
                    isFormActive = false;
                }

                Font font = SystemFonts.DefaultFont; // System.Windows.Forms.SystemInformation.MenuFont;
                bool disposeFont = true;
                if (strip.CaptionFont != null)
                {
                    font.Dispose();
                    font = strip.CaptionFont;
                    disposeFont = false;
                }
                string text = strip.TitleText;
                if (string.IsNullOrEmpty(text) && form != null) text = form.Text;
                bool isTitleTextMarkup = strip.TitleTextMarkupBody != null;
                Rectangle captionRect = strip.CaptionBounds;
                const int CAPTION_TEXT_PADDING = 12;
                captionRect.X += CAPTION_TEXT_PADDING;
                captionRect.Width -= CAPTION_TEXT_PADDING;

                if (!isTitleTextMarkup)
                    TextDrawing.DrawString(g, text, font, captionTextColor, captionRect, textFormat);
                else
                {
                    TextMarkup.MarkupDrawContext d = new TextMarkup.MarkupDrawContext(g, font, captionTextColor, strip.RightToLeft == System.Windows.Forms.RightToLeft.Yes, captionRect, false);
                    d.AllowMultiLine = false;
                    d.IgnoreFormattingColors = !isFormActive;
                    TextMarkup.BodyElement body = strip.TitleTextMarkupBody;
                    if (strip.TitleTextMarkupLastArrangeBounds != captionRect)
                    {
                        body.Measure(captionRect.Size, d);
                        body.Arrange(captionRect, d);
                        strip.TitleTextMarkupLastArrangeBounds = captionRect;
                        Rectangle mr = body.Bounds;
                        if (mr.Width < captionRect.Width)
                            mr.Offset((captionRect.Width - mr.Width) / 2, 0);
                        if (mr.Height < captionRect.Height)
                            mr.Offset(0, (captionRect.Height - mr.Height) / 2);
                        body.Bounds = mr;
                    }
                    Region oldClip = g.Clip;
                    g.SetClip(captionRect, CombineMode.Intersect);
                    body.Render(d);
                    g.Clip = oldClip;
                    if (oldClip != null) oldClip.Dispose();
                }

                if (disposeFont) font.Dispose();
            }

            //g.FillRectangle(Brushes.Yellow, strip.QuickToolbarBounds);
            //g.FillRectangle(Brushes.Green, strip.CaptionBounds);
            //g.FillRectangle(Brushes.Indigo, strip.SystemCaptionItemBounds);
            
        }

        private void SetQatAndCaptionItemBounds(MetroTabStrip strip,MetroRendererInfo renderingInfo)
        {
            if (!strip.CaptionVisible)
                return;
            bool rightToLeft = (strip.RightToLeft == System.Windows.Forms.RightToLeft.Yes);
            
            System.Windows.Forms.Form form = strip.FindForm();
            bool isMaximized = false;
            if (form != null) isMaximized = form.WindowState == System.Windows.Forms.FormWindowState.Maximized;

            // Get right most X position of the Quick Access Toolbar
            int right = 0, sysLeft = 0;
            Size qatSize = Size.Empty;
            for (int i = strip.QuickToolbarItems.Count - 1; i >= 0; i--)
            {
                BaseItem item = strip.QuickToolbarItems[i];
                if (!item.Visible || !item.Displayed)
                    continue;
                if (item is QatCustomizeItem) qatSize = item.DisplayRectangle.Size;
                if (item.ItemAlignment == eItemAlignment.Near && item.Visible && i > 0)
                {
                    if (rightToLeft)
                        right = item.DisplayRectangle.X;
                    else
                        right = item.DisplayRectangle.Right;
                    break;
                }
                else if (item.ItemAlignment == eItemAlignment.Far && item.Visible)
                {
                    if (rightToLeft)
                        sysLeft = item.DisplayRectangle.Right;
                    else
                        sysLeft = item.DisplayRectangle.X;
                }
            }
            
            if (strip.CaptionContainerItem is CaptionItemContainer && ((CaptionItemContainer)strip.CaptionContainerItem).MoreItems != null)
            {
                if (rightToLeft)
                    right = ((CaptionItemContainer)strip.CaptionContainerItem).MoreItems.DisplayRectangle.X;
                else
                    right = ((CaptionItemContainer)strip.CaptionContainerItem).MoreItems.DisplayRectangle.Right;
                qatSize = ((CaptionItemContainer)strip.CaptionContainerItem).MoreItems.DisplayRectangle.Size;
            }

            Rectangle r = new Rectangle(right, 2, strip.CaptionContainerItem.WidthInternal - right - (strip.CaptionContainerItem.WidthInternal-sysLeft), strip.GetTotalCaptionHeight());
            strip.CaptionBounds = r;

            if (sysLeft > 0)
            {
                if (rightToLeft)
                    strip.SystemCaptionItemBounds = new Rectangle(r.X, r.Y, sysLeft, r.Height);
                else
                    strip.SystemCaptionItemBounds = new Rectangle(sysLeft, r.Y, strip.CaptionContainerItem.WidthInternal - sysLeft, r.Height);
            }

            if (right == 0 || r.Height <= 0 || r.Width <= 0)
                return;

            BaseItem startButton = strip.GetApplicationButton();
            if (startButton != null)
            {
                int startIndex = strip.QuickToolbarItems.IndexOf(startButton);
                if (strip.QuickToolbarItems.Count - 1 > startIndex)
                {
                    BaseItem firstItem = strip.QuickToolbarItems[startIndex + 1];
                    if (rightToLeft)
                    {
                        r.Width -= r.Right - firstItem.DisplayRectangle.Right;
                    }
                    else
                    {
                        r.Width -= firstItem.DisplayRectangle.X - r.X;
                        r.X = firstItem.DisplayRectangle.X;
                    }
                }
            }

            r.Height = ((CaptionItemContainer)strip.CaptionContainerItem).MaxItemHeight + 6;
            r.X = 0;
            r.Width = right;
            strip.QuickToolbarBounds = r;
        }

        //private void PaintCaptionText(MetroTabStrip rs)
        //{
        //    if (!rs.CaptionVisible || rs.CaptionBounds.IsEmpty)
        //        return;

        //    Graphics g = e.Graphics;
        //    bool isMaximized = false;
        //    bool isFormActive = true;
        //    Rendering.Office2007FormStateColorTable formct = m_ColorTable.Form.Active;
        //    System.Windows.Forms.Form form = rs.FindForm();
        //    if (form != null && (form != System.Windows.Forms.Form.ActiveForm && form.MdiParent == null ||
        //        form.MdiParent != null && form.MdiParent.ActiveMdiChild != form))
        //    {
        //        formct = m_ColorTable.Form.Inactive;
        //        isFormActive = false;
        //    }
        //    string text = e.RibbonControl.TitleText;
        //    string plainText = text;
        //    bool isTitleTextMarkup = e.RibbonControl.RibbonStrip.TitleTextMarkupBody != null;
        //    if (isTitleTextMarkup)
        //        plainText = e.RibbonControl.RibbonStrip.TitleTextMarkupBody.PlainText;
        //    if (form != null)
        //    {
        //        if (text == "")
        //        {
        //            text = form.Text;
        //            plainText = text;
        //        }
        //        isMaximized = form.WindowState == System.Windows.Forms.FormWindowState.Maximized;
        //    }

        //    Rectangle captionRect = rs.CaptionBounds;

        //    // Exclude quick access toolbar if any
        //    if (!rs.QuickToolbarBounds.IsEmpty)
        //        DisplayHelp.ExcludeEdgeRect(ref captionRect, rs.QuickToolbarBounds);
        //    else
        //    {
        //        BaseItem sb = e.RibbonControl.GetApplicationButton();
        //        if (sb != null && sb.Visible && sb.Displayed)
        //            DisplayHelp.ExcludeEdgeRect(ref captionRect, sb.Bounds);
        //        else
        //            DisplayHelp.ExcludeEdgeRect(ref captionRect, new Rectangle(0, 0, 22, 22)); // The system button in top-left corner
        //    }

        //    if (!rs.SystemCaptionItemBounds.IsEmpty)
        //        DisplayHelp.ExcludeEdgeRect(ref captionRect, rs.SystemCaptionItemBounds);


        //    // Array of the rectangles after they are split
        //    ArrayList rects = new ArrayList(5);
        //    ArrayList tempRemoveList = new ArrayList(5);
        //    // Exclude Context Tabs Captions if any
        //    if (rs.TabGroupsVisible)
        //    {
        //        foreach (RibbonTabItemGroup group in rs.TabGroups)
        //        {
        //            foreach (Rectangle r in group.DisplayPositions)
        //            {
        //                if (rects.Count > 0)
        //                {
        //                    tempRemoveList.Clear();
        //                    Rectangle[] arrCopy = new Rectangle[rects.Count];
        //                    rects.CopyTo(arrCopy);
        //                    for (int irc = 0; irc < arrCopy.Length; irc++)
        //                    {
        //                        if (arrCopy[irc].IntersectsWith(r))
        //                        {
        //                            tempRemoveList.Add(irc);
        //                            Rectangle[] splitRects = DisplayHelp.ExcludeRectangle(arrCopy[irc], r);
        //                            rects.AddRange(splitRects);
        //                        }
        //                    }
        //                    foreach (int idx in tempRemoveList)
        //                        rects.RemoveAt(idx);
        //                }
        //                else
        //                {
        //                    if (r.IntersectsWith(captionRect))
        //                    {
        //                        Rectangle[] splitRects = DisplayHelp.ExcludeRectangle(captionRect, r);
        //                        if (splitRects.Length > 0)
        //                        {
        //                            rects.AddRange(splitRects);
        //                            break;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }

        //    Font font = System.Windows.Forms.SystemInformation.MenuFont;
        //    bool disposeFont = true;
        //    if (rs.CaptionFont != null)
        //    {
        //        font.Dispose();
        //        font = rs.CaptionFont;
        //        disposeFont = false;
        //    }
        //    Size textSize = Size.Empty;
        //    if (isTitleTextMarkup)
        //    {
        //        textSize = e.RibbonControl.RibbonStrip.TitleTextMarkupBody.Bounds.Size;
        //    }
        //    else
        //    {
        //        textSize = TextDrawing.MeasureString(g, plainText, font);
        //    }

        //    if (rects.Count > 0)
        //    {
        //        rs.CaptionTextBounds = (Rectangle[])rects.ToArray(typeof(Rectangle));
        //        if (e.RibbonControl.RightToLeft == System.Windows.Forms.RightToLeft.No)
        //            rects.Reverse();
        //        captionRect = Rectangle.Empty;
        //        foreach (Rectangle r in rects)
        //        {
        //            if (r.Width >= textSize.Width)
        //            {
        //                captionRect = r;
        //                break;
        //            }
        //            else if (r.Width > captionRect.Width)
        //                captionRect = r;
        //        }
        //    }
        //    else
        //        rs.CaptionTextBounds = new Rectangle[] { captionRect };

        //    if (captionRect.Width > 6 && captionRect.Height > 6)
        //    {
        //        if (e.GlassEnabled && e.ItemPaintArgs != null && e.ItemPaintArgs.ThemeWindow != null && !e.RibbonControl.IsDesignMode)
        //        {
        //            if (!e.ItemPaintArgs.CachedPaint || isMaximized)
        //                PaintGlassText(g, plainText, font, captionRect, isMaximized);
        //        }
        //        else
        //        {
        //            if (!isTitleTextMarkup)
        //                TextDrawing.DrawString(g, plainText, font, formct.CaptionText, captionRect, eTextFormat.VerticalCenter | eTextFormat.HorizontalCenter | eTextFormat.EndEllipsis | eTextFormat.NoPrefix);
        //            else
        //            {
        //                TextMarkup.MarkupDrawContext d = new TextMarkup.MarkupDrawContext(g, font, formct.CaptionText, e.RibbonControl.RightToLeft == System.Windows.Forms.RightToLeft.Yes, captionRect, false);
        //                d.AllowMultiLine = false;
        //                d.IgnoreFormattingColors = !isFormActive;
        //                TextMarkup.BodyElement body = e.RibbonControl.RibbonStrip.TitleTextMarkupBody;
        //                if (e.RibbonControl.RibbonStrip.TitleTextMarkupLastArrangeBounds != captionRect)
        //                {
        //                    body.Measure(captionRect.Size, d);
        //                    body.Arrange(captionRect, d);
        //                    e.RibbonControl.RibbonStrip.TitleTextMarkupLastArrangeBounds = captionRect;
        //                    Rectangle mr = body.Bounds;
        //                    if (mr.Width < captionRect.Width)
        //                        mr.Offset((captionRect.Width - mr.Width) / 2, 0);
        //                    if (mr.Height < captionRect.Height)
        //                        mr.Offset(0, (captionRect.Height - mr.Height) / 2);
        //                    body.Bounds = mr;
        //                }
        //                Region oldClip = g.Clip;
        //                g.SetClip(captionRect, CombineMode.Intersect);
        //                body.Render(d);
        //                g.Clip = oldClip;
        //                if (oldClip != null) oldClip.Dispose();
        //            }
        //        }
        //    }

        //    if (disposeFont) font.Dispose();
        //}

    }
}
