using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar.Controls
{
    /// <summary>
    /// A single horizontal or vertical line control.
    /// </summary>
    [ToolboxItem(true), Description("Horizontal or Vertical Line Control")]
    public class Line : Control
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the Line class.
        /// </summary>
        public Line()
        {
            this.SetStyle(ControlStyles.AllPaintingInWmPaint
                        | ControlStyles.OptimizedDoubleBuffer
                        | ControlStyles.UserPaint
                        | ControlStyles.SupportsTransparentBackColor
                        , true);
        }
        #endregion

        #region Implementation
        protected override void OnPaint(PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            SmoothingMode sm = g.SmoothingMode;
            g.SmoothingMode = SmoothingMode.None;

            using (Pen pen = new Pen(ForeColor, _Thickness))
            {
                pen.DashStyle = _DashStyle;
                pen.DashOffset = _DashOffset;

                Point lineStart = LineStartPoint;
                Point lineEnd = LineEndPoint;

                if (_StartLineCap != eLineEndType.None && _Thickness > 1)
                {
                    if (_VerticalLine)
                        lineStart.Y += _StartLineCapSize.Height / 2;
                    else
                        lineStart.X += _StartLineCapSize.Width / 2;
                }
                if (_EndLineCap != eLineEndType.None && _Thickness > 1)
                {
                    if (_VerticalLine)
                        lineEnd.Y -= _EndLineCapSize.Height / 2;
                    else
                        lineEnd.X -= _EndLineCapSize.Width / 2;
                }

                g.DrawLine(pen, lineStart, lineEnd);
            }

            if (_StartLineCap != eLineEndType.None && _StartLineCapSize.Width > 0 && _StartLineCapSize.Height > 0)
                DrawLineCap(g, LineStartPoint, _StartLineCap, _StartLineCapSize, true);

            if (_EndLineCap != eLineEndType.None && _EndLineCapSize.Width > 0 && _EndLineCapSize.Height > 0)
                DrawLineCap(g, LineEndPoint, _EndLineCap, _EndLineCapSize, false);

            g.SmoothingMode = sm;

            base.OnPaint(e);
        }
        private void DrawLineCap(Graphics g, Point linePoint, eLineEndType lineCap, Size capSize, bool isStartCap)
        {
            if (lineCap == eLineEndType.Arrow)
            {
                SmoothingMode sm = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.HighQuality;
                using (GraphicsPath path = new GraphicsPath())
                {
                    if (isStartCap)
                    {
                        if (VerticalLine)
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X - capSize.Width/2, linePoint.Y+capSize.Height),
                                new Point(linePoint.X+capSize.Width / 2, linePoint.Y+capSize.Height)});
                        else
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X + capSize.Width, linePoint.Y-capSize.Height/2),
                                new Point(linePoint.X+capSize.Width, linePoint.Y+ capSize.Height/2)});
                    }
                    else
                    {
                        if (VerticalLine)
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X + capSize.Width / 2, linePoint.Y - capSize.Height),
                                new Point(linePoint.X - capSize.Width / 2, linePoint.Y - capSize.Height)});
                        else
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X - capSize.Width, linePoint.Y + capSize.Height / 2),
                                new Point(linePoint.X - capSize.Width, linePoint.Y - capSize.Height/2)});
                    }
                    path.CloseAllFigures();
                    using (SolidBrush brush = new SolidBrush(ForeColor))
                        g.FillPath(brush, path);
                }
                g.SmoothingMode = sm;
            }
            else if (lineCap == eLineEndType.Circle)
            {
                SmoothingMode sm = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.HighQuality;
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    if (VerticalLine && isStartCap)
                        g.FillEllipse(brush, new Rectangle(linePoint.X - capSize.Width/2, linePoint.Y , capSize.Width, capSize.Height));
                    else if (VerticalLine)
                        g.FillEllipse(brush, new Rectangle(linePoint.X - capSize.Width/2, linePoint.Y - capSize.Height - 1, capSize.Width, capSize.Height));
                    else if (isStartCap)
                        g.FillEllipse(brush, new Rectangle(linePoint.X, linePoint.Y - capSize.Height / 2, capSize.Width, capSize.Height));
                    else
                        g.FillEllipse(brush, new Rectangle(linePoint.X - capSize.Width - 1, linePoint.Y - capSize.Height / 2, capSize.Width, capSize.Height));
                }
                g.SmoothingMode = sm;
            }
            else if (lineCap == eLineEndType.Diamond)
            {
                SmoothingMode sm = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.HighQuality;
                using (GraphicsPath path = new GraphicsPath())
                {
                    if (isStartCap)
                    {
                        if (VerticalLine)
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X - capSize.Width/2, linePoint.Y+capSize.Height / 2),
                                new Point(linePoint.X, linePoint.Y+capSize.Height),
                                new Point(linePoint.X+capSize.Width / 2, linePoint.Y+capSize.Height / 2)});
                        else
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X + capSize.Width/2, linePoint.Y-capSize.Height/2),
                                new Point(linePoint.X + capSize.Width, linePoint.Y),
                                new Point(linePoint.X+capSize.Width / 2, linePoint.Y+ capSize.Height/2)});
                    }
                    else
                    {
                        if (VerticalLine)
                        {
                            linePoint.Y--;
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X + capSize.Width / 2, linePoint.Y - capSize.Height / 2),
                                new Point(linePoint.X, linePoint.Y - capSize.Height),
                                new Point(linePoint.X - capSize.Width / 2, linePoint.Y - capSize.Height / 2)});
                        }
                        else
                        {
                            linePoint.X--;
                            path.AddLines(new Point[] { 
                                new Point(linePoint.X, linePoint.Y), 
                                new Point(linePoint.X - capSize.Width / 2, linePoint.Y - capSize.Height / 2),
                                new Point(linePoint.X - capSize.Width, linePoint.Y),
                                new Point(linePoint.X - capSize.Width / 2, linePoint.Y + capSize.Height/2)});
                        }
                    }
                    path.CloseAllFigures();
                    using (SolidBrush brush = new SolidBrush(ForeColor))
                        g.FillPath(brush, path);
                }
                g.SmoothingMode = sm;
            }
            else if (lineCap == eLineEndType.Rectangle)
            {
                SmoothingMode sm = g.SmoothingMode;
                g.SmoothingMode = SmoothingMode.HighQuality;
                using (SolidBrush brush = new SolidBrush(ForeColor))
                {
                    if (VerticalLine && isStartCap)
                        g.FillRectangle(brush, new Rectangle(linePoint.X - capSize.Width / 2, linePoint.Y, capSize.Width, capSize.Height));
                    else if (VerticalLine)
                        g.FillRectangle(brush, new Rectangle(linePoint.X - capSize.Width / 2, linePoint.Y - capSize.Height - 1, capSize.Width, capSize.Height));
                    else if (isStartCap)
                        g.FillRectangle(brush, new Rectangle(linePoint.X, linePoint.Y - capSize.Height / 2, capSize.Width, capSize.Height));
                    else
                        g.FillRectangle(brush, new Rectangle(linePoint.X - capSize.Width - 1, linePoint.Y - capSize.Height / 2, capSize.Width, capSize.Height));
                }
                g.SmoothingMode = sm;
            }
        }
        private Point LineStartPoint
        {
            get
            {
                if (!_VerticalLine)
                {
                    if (_LineAlignment == eItemAlignment.Center)
                        return new Point(0, this.Height / 2);
                    else if (_LineAlignment == eItemAlignment.Near)
                        return new Point(0, _Thickness / 2 + ((_StartLineCap != eLineEndType.None) ? _StartLineCapSize.Height / 2 : 0));
                    else if (_LineAlignment == eItemAlignment.Far)
                        return new Point(0, this.Height - _Thickness / 2 - ((_StartLineCap != eLineEndType.None) ? _StartLineCapSize.Height / 2 : 0));
                }
                else
                {
                    if (_LineAlignment == eItemAlignment.Center)
                        return new Point(this.Width / 2, 0);
                    else if (_LineAlignment == eItemAlignment.Near)
                    {
                        return new Point(_Thickness / 2 + ((_EndLineCap != eLineEndType.None) ? _EndLineCapSize.Width / 2 : 0), 0);
                    }
                    else if (_LineAlignment == eItemAlignment.Far)
                        return new Point(this.Width - _Thickness / 2 - ((_EndLineCap != eLineEndType.None) ? _EndLineCapSize.Width / 2 : 0), 0);
                }
                return Point.Empty;
            }
        }
        private Point LineEndPoint
        {
            get
            {
                if (!_VerticalLine)
                {
                    return new Point(this.Width, LineStartPoint.Y);
                }
                else
                {
                    return new Point(LineStartPoint.X, this.Height);
                }

            }
        }

        private eItemAlignment _LineAlignment = eItemAlignment.Center;
        /// <summary>
        /// Specifies the line alignment within control bounds.
        /// </summary>
        [DefaultValue(eItemAlignment.Center), Category("Appearance"), Description("Specifies the line alignment within control bounds.")]
        public eItemAlignment LineAlignment
        {
            get { return _LineAlignment; }
            set { _LineAlignment = value; this.Invalidate(); }
        }

        private float _DashOffset;
        /// <summary>
        /// Specifies distance from the start of a line to the beginning of a dash pattern.
        /// </summary>
        [DefaultValue(0f), Category("Appearance"), Description("Specifies distance from the start of a line to the beginning of a dash pattern.")]
        public float DashOffset
        {
            get { return _DashOffset; }
            set { _DashOffset = value; this.Invalidate(); }
        }

        private DashStyle _DashStyle = DashStyle.Solid;
        /// <summary>
        /// Specifies the line dash style.
        /// </summary>
        [DefaultValue(DashStyle.Solid), Category("Appearance"), Description("Specifies the line dash style.")]
        public DashStyle DashStyle
        {
            get { return _DashStyle; }
            set { _DashStyle = value; this.Invalidate(); }
        }

        //private LineCap _LineCap = System.Drawing.Drawing2D.LineCap.Round;
        ///// <summary>
        ///// Gets or sets the line cap i.e. line ending.
        ///// </summary>
        //[DefaultValue(LineCap.Round), Category("Appearance"), Description("Specifies line cap i.e. line ending.")]
        //public LineCap LineCap
        //{
        //    get { return _LineCap; }
        //    set { _LineCap = value; this.Invalidate(); }
        //}

        private int _Thickness = 1;
        /// <summary>
        /// Gets or sets the line thickness in pixels.
        /// </summary>
        [DefaultValue(1), Category("Appearance"), Description("Indicates line thickness in pixels.")]
        public int Thickness
        {
            get { return _Thickness; }
            set
            {
                _Thickness = value;
                this.Invalidate();
                if (this.AutoSize) AdjustSize();
            }
        }


        private bool _VerticalLine = false;
        /// <summary>
        /// Gets or sets whether vertical line is drawn. Default value is false which means horizontal line is drawn.
        /// </summary>
        [DefaultValue(false), Category("Appearance"), Description("Indicates whether vertical line is drawn.")]
        public bool VerticalLine
        {
            get { return _VerticalLine; }
            set
            {
                _VerticalLine = value;
                this.Invalidate();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether the control is automatically resized to display its entire contents. You can set MaximumSize.Width property to set the maximum width used by the control.
        /// </summary>
        [Browsable(true), DefaultValue(false), EditorBrowsable(EditorBrowsableState.Always), DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public override bool AutoSize
        {
            get
            {
                return base.AutoSize;
            }
            set
            {
                if (this.AutoSize != value)
                {
                    base.AutoSize = value;
                    AdjustSize();
                }
            }
        }
        private void AdjustSize()
        {
            this.Size = GetPreferredSize(this.Size);
        }
        public override Size GetPreferredSize(Size proposedSize)
        {
            if (this.VerticalLine)
                return new Size(_Thickness, this.Height);
            else
                return new Size(this.Width, _Thickness);
            //return base.GetPreferredSize(proposedSize);
        }

        private eLineEndType _StartLineCap = eLineEndType.None;
        /// <summary>
        /// Indicates the start of the line cap.
        /// </summary>
        [DefaultValue(eLineEndType.None), Category("Appearance"), Description("Indicates the start of the line cap.")]
        public eLineEndType StartLineCap
        {
            get { return _StartLineCap; }
            set
            {
                if (value != _StartLineCap)
                {
                    eLineEndType oldValue = _StartLineCap;
                    _StartLineCap = value;
                    OnStartLineCapChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when StartLineCap property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnStartLineCapChanged(eLineEndType oldValue, eLineEndType newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("StartLineCap"));
            this.Refresh();
        }
        private static readonly Size DefaultCapSize = new Size(6, 6);
        private Size _StartLineCapSize = DefaultCapSize;
        /// <summary>
        /// Indicates the size of the start cap.
        /// </summary>
        [Category("Appearance"), Description("Indicates the size of the start cap.")]
        public Size StartLineCapSize
        {
            get { return _StartLineCapSize; }
            set
            {
                if (value != _StartLineCapSize)
                {
                    Size oldValue = _StartLineCapSize;
                    _StartLineCapSize = value;
                    OnStartLineCapSizeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when StartLineCapSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnStartLineCapSizeChanged(Size oldValue, Size newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("StartLineCapSize"));
            this.Refresh();
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeStartLineCapSize()
        {
            return _StartLineCapSize != DefaultCapSize;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetStartLineCapSize()
        {
            this.StartLineCapSize = DefaultCapSize;
        }

        private eLineEndType _EndLineCap = eLineEndType.None;
        /// <summary>
        /// Indicates the start of the line cap.
        /// </summary>
        [DefaultValue(eLineEndType.None), Category("Appearance"), Description("Indicates the start of the line cap.")]
        public eLineEndType EndLineCap
        {
            get { return _EndLineCap; }
            set
            {
                if (value != _EndLineCap)
                {
                    eLineEndType oldValue = _EndLineCap;
                    _EndLineCap = value;
                    OnEndLineCapChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when EndLineCap property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnEndLineCapChanged(eLineEndType oldValue, eLineEndType newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("EndLineCap"));
            this.Refresh();
        }

        private Size _EndLineCapSize = DefaultCapSize;
        /// <summary>
        /// Indicates end line cap size.
        /// </summary>
        [Category("Appearance"), Description("Indicates end line cap size.")]
        public Size EndLineCapSize
        {
            get { return _EndLineCapSize; }
            set { _EndLineCapSize = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeEndLineCapSize()
        {
            return _EndLineCapSize != DefaultCapSize;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetEndLineCapSize()
        {
            this.EndLineCapSize = DefaultCapSize;
        }
        #endregion
    }
    /// <summary>
    /// Defined line end types.
    /// </summary>
    public enum eLineEndType
    {
        None,
        Arrow,
        Rectangle,
        Circle,
        Diamond
    }
}
