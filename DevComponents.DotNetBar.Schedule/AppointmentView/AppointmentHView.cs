#if FRAMEWORK20
using System;
using DevComponents.Schedule.Model;
using System.Drawing;
using DevComponents.WinForms.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Schedule
{
    public class AppointmentHView : AppointmentView
    {
        #region Private variables
        private eViewEnds _ViewEnds = eViewEnds.Complete;
        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="baseView"></param>
        /// <param name="appointment"></param>
        public AppointmentHView(BaseView baseView, Appointment appointment)
            : base(baseView, appointment)
        {
        }

        #region Private properties

        /// <summary>
        /// Gets whether the appointment is mutable
        /// </summary> 
        private bool IsMutable
        {
            get
            {
                return (IsSelected == true &&
                    Appointment.Locked == false &&
                    Appointment.IsRecurringInstance == false);
            }
        }

        #endregion

        #region Protected properties

        protected eViewEnds ViewEnds
        {
            get { return (_ViewEnds); }
            set { _ViewEnds = value; }
        }

        protected virtual Rectangle ParentBounds
        {
            get { return (Bounds); }
        }

        #endregion

        #region Start/End TimeChanged event handling

        /// <summary>
        /// Handles StartTime value changes
        /// </summary>
        /// <param name="sender">CalendarItem</param>
        /// <param name="e">EventArgs</param>
        protected override void AppointmentView_StartTimeChanged(object sender, EventArgs e)
        {
            base.AppointmentView_StartTimeChanged(sender, e);

            SetViewEnds();
        }

        /// <summary>
        /// Handles EndTime value changes
        /// </summary>
        /// <param name="sender">CalendarItem</param>
        /// <param name="e">EventArgs</param>
        protected override void AppointmentView_EndTimeChanged(object sender, EventArgs e)
        {
            base.AppointmentView_EndTimeChanged(sender, e);

            SetViewEnds();
        }

        #endregion

        #region SetViewEnds

        /// <summary>
        /// Sets the view display end types
        /// </summary>
        protected virtual void SetViewEnds()
        {
            _ViewEnds = eViewEnds.Complete;
        }

        #endregion

        #region Appointment rendering

        /// <summary>
        /// Paint processing
        /// </summary>
        /// <param name="e">ItemPaintArgs</param>
        public override void Paint(ItemPaintArgs e)
        {
            SetViewEnds();

            AppointmentColor.SetColorTable();

            int n = (Bounds.Width < 20) ? 0 : 5;

            CornerRadius cornerRadius = new CornerRadius(n);
            Rectangle r = GetViewRect(ref cornerRadius);

            if (r.Width > 1 && r.Height > 0)
            {
                if (EffectiveStyle == eDotNetBarStyle.Office2010 || EffectiveStyle == eDotNetBarStyle.Metro)
                    DrawMetroAppointment(e, r);
                else
                    DrawDefaultAppointment(e, r, n, cornerRadius);

                if (IsMutable == true)
                    DrawGribits(e, r);
            }
        }

        #region DrawMetroAppointment

        private void DrawMetroAppointment(ItemPaintArgs e, Rectangle r)
        {
            Graphics g = e.Graphics;

            using (Brush br = BackBrush(r))
                e.Graphics.FillRectangle(br, r);

            if (BaseView.CalendarView.DoAppointmentViewPreRender(this, g, r, null) == false)
            {
                DrawContent(e, r);

                BaseView.CalendarView.DoAppointmentViewPostRender(this, g, r, null);
            }

            DrawMetroBorder(e, r);
        }

        #region DrawMetroBorder

        private void DrawMetroBorder(ItemPaintArgs e, Rectangle r)
        {
            Graphics g = e.Graphics;

            if ((_ViewEnds & eViewEnds.PartialLeft) != eViewEnds.PartialLeft)
            {
                if (r.Width > 10)
                    DrawTimeMarker(g, r, 0);
            }

            if (IsSelected == true)
            {
                if (r.Width > 10)
                {
                    using (Pen pen = new Pen(Color.Black, 2))
                        g.DrawRectangle(pen, r);
                }
                else
                {
                    g.DrawRectangle(Pens.Black, r);
                }
            }
            else
            {
                using (Pen pen = BorderPen)
                    g.DrawRectangle(pen, r);
            }
        }

        #endregion

        #endregion

        #region DrawDefaultAppointment

        private void DrawDefaultAppointment(ItemPaintArgs e,
            Rectangle r, int n, CornerRadius cornerRadius)
        {
            Graphics g = e.Graphics;

            using (GraphicsPath path = GetItemPath(r, n * 2, cornerRadius))
            {
                using (Brush br = BackBrush(r))
                    g.FillPath(br, path);

                if (BaseView.CalendarView.DoAppointmentViewPreRender(this, g, r, path) == false)
                {
                    DrawContent(e, r);

                    BaseView.CalendarView.DoAppointmentViewPostRender(this, g, r, path);
                }

                DrawDefaultBorder(e, r, path, n);
            }
        }

        #region DrawDefaultBorder

        private void DrawDefaultBorder(ItemPaintArgs e,
            Rectangle r, GraphicsPath path, int n)
        {
            Graphics g = e.Graphics;

            if ((_ViewEnds & eViewEnds.PartialLeft) != eViewEnds.PartialLeft)
            {
                if (r.Width > 10)
                    DrawTimeMarker(g, r, n);
            }

            if (IsSelected == true)
            {
                if (r.Width > 10)
                {
                    using (Pen pen = SelectedBorderPen)
                        g.DrawPath(pen, path);
                }
                else
                {
                    g.DrawPath(Pens.Black, path);
                }
            }
            else
            {
                using (Pen pen = BorderPen)
                    g.DrawPath(pen, path);
            }
        }

        #endregion

        #endregion

        #region DrawContent

        private void DrawContent(ItemPaintArgs e, Rectangle r)
        {
            Image image = Image;

            Rectangle rText = r;
            rText.X += 4;
            rText.Width -= 6;

            if (Appointment.TimeMarkedAs != null)
            {
                rText.X += 4;
                rText.Width -= 4;
            }

            Rectangle rImage = GetItemBounds(rText, ref rText, image);

            DrawContentImage(e, r, rImage, image);
            DrawContentText(e, rText);
        }

        #region DrawContentText

        /// <summary>
        /// DrawContentText
        /// </summary>
        /// <param name="e"></param>
        /// <param name="r"></param>
        private void DrawContentText(ItemPaintArgs e, Rectangle r)
        {
            Graphics g = e.Graphics;

            // Format the appointment text

            if (DisplayTemplateText(e, r) == false)
            {
                string s = (Appointment.IsMultiDayOrAllDayEvent)
                               ? Appointment.Subject
                               : String.Format("{0} {1}", Appointment.StartTime.ToShortTimeString(), Appointment.Subject);

                // Output the appointment text and
                // appropriately weighted bounding path

                Font font = Font ?? e.Font;
                const eTextFormat tf = eTextFormat.VerticalCenter |
                    eTextFormat.EndEllipsis | eTextFormat.NoPadding | eTextFormat.NoPrefix;

                if (r.Width > 10)
                    TextDrawing.DrawString(g, s, font, TextColor, r, tf);

                Size size = TextDrawing.MeasureString(g, s, font, r.Width, tf);

                IsTextClipped = (r.Width < size.Width || r.Height < size.Height);
            }
        }

        #endregion

        #endregion

        #region GetViewRect

        /// <summary>
        /// Gets the view rect for the appointment
        /// </summary>
        /// <param name="cornerRadius">Corner radius</param>
        /// <returns>View rect</returns>
        private Rectangle GetViewRect(ref CornerRadius cornerRadius)
        {
            Rectangle r = DisplayRectangle;

            r.Intersect(ParentBounds);

            if (r.Left == ParentBounds.Left)
            {
                r.X++;
                r.Width--;
            }

            if (r.Right == ParentBounds.Right)
                r.Width--;

            if ((_ViewEnds & eViewEnds.PartialLeft) == eViewEnds.PartialLeft)
                cornerRadius.TopLeft = cornerRadius.BottomLeft = 0;

            if ((_ViewEnds & eViewEnds.PartialRight) == eViewEnds.PartialRight)
                cornerRadius.TopRight = cornerRadius.BottomRight = 0;

            // If the view is selected, then allow
            // for a thicker selection rect

            if (IsSelected == true)
                r.Height--;

            return (r);
        }

        #endregion

        #region GetItemPath

        /// <summary>
        /// Gets a path defining the item
        /// </summary>
        /// <param name="viewRect"></param>
        /// <param name="radius"></param>
        /// <param name="cornerRadius"></param>
        /// <returns></returns>
        private GraphicsPath GetItemPath(Rectangle viewRect, int radius, CornerRadius cornerRadius)
        {
            GraphicsPath path = new GraphicsPath();

            Rectangle r = viewRect;

            Rectangle ar = new
                Rectangle(r.Right - radius, r.Bottom - radius, radius, radius);

            if (cornerRadius.BottomRight > 0)
                path.AddArc(ar, 0, 90);
            else
                path.AddLine(r.Right, r.Bottom, r.Right, r.Bottom);

            ar.X = r.X;

            if (cornerRadius.BottomLeft > 0)
                path.AddArc(ar, 90, 90);
            else
                path.AddLine(r.Left, r.Bottom, r.Left, r.Bottom);

            ar.Y = r.Y;

            if (cornerRadius.TopLeft > 0)
                path.AddArc(ar, 180, 90);
            else
                path.AddLine(r.Left, r.Top, r.Left, r.Top);

            ar.X = r.Right - radius;

            if (cornerRadius.TopRight > 0)
                path.AddArc(ar, 270, 90);
            else
                path.AddLine(r.Right, r.Top, r.Right, r.Top);

            path.CloseAllFigures();

            return (path);
        }

        #endregion

        #region DrawGribits

        /// <summary>
        /// Draws the resize gribits for the view
        /// </summary>
        /// <param name="e"></param>
        /// <param name="r">View rectangle</param>
        private void DrawGribits(ItemPaintArgs e, Rectangle r)
        {
            if (r.Width >= 8)
            {
                Graphics g = e.Graphics;

                Rectangle r2 =
                    new Rectangle(r.X, r.Y + (r.Height / 2) - 2, 5, 5);

                // Left gribit

                if ((_ViewEnds & eViewEnds.PartialLeft) == 0)
                {
                    r2.X = r.X - 3;

                    g.FillRectangle(Brushes.White, r2);
                    g.DrawRectangle(Pens.Black, r2);
                }

                // Right gribit

                if ((_ViewEnds & eViewEnds.PartialRight) == 0)
                {
                    r2.X = r.Right - 2;

                    g.FillRectangle(Brushes.White, r2);
                    g.DrawRectangle(Pens.Black, r2);
                }
            }
        }

        #endregion

        #endregion

        #region Mouse processing

        /// <summary>
        /// Handles mouseDown processing
        /// </summary>
        /// <param name="objArg">MouseEventArgs</param>
        public override void InternalMouseMove(MouseEventArgs objArg)
        {
            HitArea = GetHitArea(objArg);

            base.InternalMouseMove(objArg);
        }

        /// <summary>
        /// Gets the HitArea from the current
        /// mouse position
        /// </summary>
        /// <param name="objArg"></param>
        /// <returns></returns>
        private eHitArea GetHitArea(MouseEventArgs objArg)
        {
            if (IsMutable == true)
            {
                CornerRadius cornerRadius = new CornerRadius(5);
                Rectangle r = GetViewRect(ref cornerRadius);

                //if (r.Width > 10)
                {
                    Rectangle r2 =
                        new Rectangle(r.X, r.Y + (r.Height / 2) - 2, 5, 5);

                    if ((_ViewEnds & eViewEnds.PartialLeft) == 0)
                    {
                        r2.X = r.X - 3;

                        r2.Inflate(2, 2);

                        if (r2.Contains(objArg.Location))
                            return (eHitArea.LeftResize);
                    }

                    if ((_ViewEnds & eViewEnds.PartialRight) == 0)
                    {
                        r2.X = r.Right - 2;

                        r2.Inflate(2, 2);

                        if (r2.Contains(objArg.Location))
                            return (eHitArea.RightResize);
                    }
                }

                // By default we are in the move area

                return (eHitArea.Move);
            }

            return (eHitArea.None);
        }

        #endregion
    }
}
#endif

