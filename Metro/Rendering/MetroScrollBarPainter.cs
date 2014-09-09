using System;
using System.Collections.Generic;
using System.Text;
using DevComponents.DotNetBar.ScrollBar;
using DevComponents.DotNetBar.Rendering;
using System.Drawing.Drawing2D;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroScrollBarPainter : ScrollBarPainter, IOffice2007Painter
    {
        #region Private Variables
        private Office2007ColorTable m_ColorTable = null;
        private bool m_AppStyleScrollBar = false;
        private Presentation.ShapeBorder m_ThumbOuterBorder = new Presentation.ShapeBorder(1);
        private Presentation.ShapeBorder m_ThumbInnerBorder = new Presentation.ShapeBorder(1);
        private Presentation.ShapeFill m_ThumbInnerFill = new Presentation.ShapeFill();
        private Presentation.ShapeFill m_ThumbSignFill = new Presentation.ShapeFill();
        private Presentation.Shape m_ThumbShape = null;
        private Presentation.ShapePath m_ThumbSignShape = null;
        private Size m_ThumbSignSize = new Size(9, 5);

        private Presentation.Shape m_TrackShape = null;
        private Presentation.ShapeBorder m_TrackOuterBorder = new Presentation.ShapeBorder(1);
        private Presentation.ShapeBorder m_TrackInnerBorder = new Presentation.ShapeBorder(1);
        private Presentation.ShapeFill m_TrackInnerFill = new Presentation.ShapeFill();

        private Presentation.Shape m_BackgroundShape = null;
        private Presentation.ShapeBorder m_BackgroundBorder = new Presentation.ShapeBorder(1);
        private Presentation.ShapeFill m_BackgroundFill = new Presentation.ShapeFill();
        #endregion

        #region IOffice2007Painter Members

        public DevComponents.DotNetBar.Rendering.Office2007ColorTable ColorTable
        {
            get
            {
                return m_ColorTable;
            }
            set
            {
                m_ColorTable = value;
            }
        }

        #endregion
        
        #region Internal Implemementation
        public MetroScrollBarPainter()
        {
            m_ThumbShape = GetThumbShape();
            m_TrackShape = GetTrackShape();
            m_BackgroundShape = GetBackgroundShape();
        }

        public override void PaintThumb(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, eScrollThumbPosition position, eScrollBarState state)
        {
            Office2007ScrollBarStateColorTable ct = GetColorTable(state);
            if (ct == null) return;

            // Initialize Colors
            m_ThumbOuterBorder.Apply(ct.ThumbOuterBorder);
            m_ThumbSignFill.Apply(ct.ThumbSignBackground);

            m_ThumbSignShape.Path = GetThumbSignPath(position);
            m_ThumbShape.Paint(new Presentation.ShapePaintInfo(g, bounds));
            m_ThumbSignShape.Path.Dispose();
            m_ThumbSignShape.Path = null;
        }

        private Office2007ScrollBarStateColorTable GetColorTable(eScrollBarState state)
        {
            Office2007ScrollBarColorTable csb = m_AppStyleScrollBar ? m_ColorTable.AppScrollBar : m_ColorTable.ScrollBar;
            if (state == eScrollBarState.Normal)
                return csb.Default;
            else if (state == eScrollBarState.Disabled)
                return csb.Disabled;
            else if (state == eScrollBarState.ControlMouseOver)
                return csb.MouseOverControl;
            else if (state == eScrollBarState.PartMouseOver)
                return csb.MouseOver;
            else if (state == eScrollBarState.Pressed)
                return csb.Pressed;

            return null;
        }

        public override void PaintTrackHorizontal(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, eScrollBarState state)
        {
            Office2007ScrollBarStateColorTable ct = GetColorTable(state);
            if (ct == null) return;

            // Apply Colors...
            // Apply Colors...
            m_TrackInnerFill.Apply(ct.TrackSignBackground);
            m_TrackShape.Paint(new Presentation.ShapePaintInfo(g, bounds));
        }

        public override void PaintTrackVertical(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, eScrollBarState state)
        {
            Office2007ScrollBarStateColorTable ct = GetColorTable(state);
            if (ct == null) return;

            // Apply Colors...
            m_TrackInnerFill.Apply(ct.TrackSignBackground);
            m_TrackShape.Paint(new Presentation.ShapePaintInfo(g, bounds));
        }

        public override void PaintBackground(Graphics g, Rectangle bounds, eScrollBarState state, bool horizontal, bool sideBorderOnly, bool rtl)
        {
            Office2007ScrollBarStateColorTable ct = GetColorTable(state);
            if (ct == null) return;

            // Apply Colors...
            if (sideBorderOnly)
                m_BackgroundBorder.Apply(null);
            else
                m_BackgroundBorder.Apply(ct.Border);

            m_BackgroundFill.Apply(ct.Background);
            m_BackgroundFill.GradientAngle = horizontal ? 90 : 0;

            m_BackgroundShape.Paint(new Presentation.ShapePaintInfo(g, bounds));
            if (sideBorderOnly && !ct.Border.Start.IsEmpty)
            {
                if (horizontal)
                    DisplayHelp.DrawLine(g, bounds.X, bounds.Y, bounds.Right, bounds.Y, ct.Border.Start, 1);
                else
                {
                    if (rtl)
                        DisplayHelp.DrawLine(g, bounds.Right - 1, bounds.Y, bounds.Right - 1, bounds.Bottom, ct.Border.Start, 1);
                    else
                        DisplayHelp.DrawLine(g, 0, bounds.Y, 0, bounds.Bottom, ct.Border.Start, 1);
                }
            }
        }

        private Presentation.Shape GetBackgroundShape()
        {
            Presentation.Rectangle b = new Presentation.Rectangle();
            b.Border = m_BackgroundBorder;
            b.Fill = m_BackgroundFill;
            return b;
        }

        private Presentation.Shape GetThumbShape()
        {
            Presentation.Rectangle thumb = new Presentation.Rectangle();
            //thumb.Border = m_ThumbOuterBorder;
            thumb.Padding = new Presentation.PaddingInfo(1,1,1,1);
            m_ThumbSignShape = new Presentation.ShapePath();
            m_ThumbSignShape.Fill = m_ThumbSignFill;
            thumb.Children.Add(m_ThumbSignShape);

            return thumb;
        }

        private Presentation.Shape GetTrackShape()
        {
            Presentation.Rectangle thumb = new Presentation.Rectangle();
            thumb.Padding = new DevComponents.DotNetBar.Presentation.PaddingInfo(1, 1, 1, 1);
            Presentation.Rectangle innerRect = new Presentation.Rectangle();
            innerRect.Padding = new Presentation.PaddingInfo(2, 2, 2, 2);
            innerRect.Fill = m_TrackInnerFill;
            thumb.Children.Add(innerRect);

            return thumb;
        }

        private GraphicsPath GetThumbSignPath(eScrollThumbPosition pos)
        {
            GraphicsPath path = new GraphicsPath();
            Size signSize = m_ThumbSignSize;

            if (pos == eScrollThumbPosition.Left || pos == eScrollThumbPosition.Right)
            {
                int w = signSize.Width;
                signSize.Width = signSize.Height;
                signSize.Height = w;
            }

            if (pos == eScrollThumbPosition.Top)
            {
                path.AddPolygon(new PointF[] {new PointF(-1, signSize.Height), 
                    new PointF(signSize.Width / 2 , -1), 
                    new PointF(signSize.Width / 2, -1), new PointF(signSize.Width, signSize.Height), 
                    new PointF(signSize.Width, signSize.Height), new PointF(-1, signSize.Height)});
                path.CloseAllFigures();
            }
            else if (pos == eScrollThumbPosition.Bottom)
            {
                path.AddLine(signSize.Width / 2, signSize.Height + 1, signSize.Width, 1);
                path.AddLine(signSize.Width, 1, 0, 1);
                path.AddLine(0, 1, signSize.Width / 2, signSize.Height + 1);
                path.CloseAllFigures();
            }
            else if (pos == eScrollThumbPosition.Left)
            {
                //signSize.Width++;
                signSize.Height++;
                int h2 = (int)(signSize.Height / 2);
                path.AddLine(0, h2, signSize.Width, 0);
                path.AddLine(signSize.Width, 0, signSize.Width, signSize.Height);
                path.AddLine(signSize.Width, signSize.Height, 0, h2);
                path.CloseAllFigures();
            }
            else if (pos == eScrollThumbPosition.Right)
            {
                signSize.Height++;
                path.AddLine(signSize.Width, signSize.Height / 2, 0, 0);
                path.AddLine(0, 0, 0, signSize.Height);
                path.AddLine(0, signSize.Height, signSize.Width, signSize.Height / 2);
                path.CloseAllFigures();
            }

            return path;
        }

        public bool AppStyleScrollBar
        {
            get { return m_AppStyleScrollBar; }
            set { m_AppStyleScrollBar = value; }
        }
        #endregion
    }
}
