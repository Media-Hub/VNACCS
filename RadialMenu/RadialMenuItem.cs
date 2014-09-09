using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Defines Radial Menu Item.
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false), Designer(typeof(DevComponents.DotNetBar.Design.RadialMenuItemDesigner))]
    public class RadialMenuItem : BaseItem
    {
        #region Constructor
        protected override void Dispose(bool disposing)
        {
            if (_DisplayPath != null)
            {
                _DisplayPath.Dispose();
                _DisplayPath = null;
            }
            base.Dispose(disposing);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs after Checked property has changed.
        /// </summary>
        [Description("Occurs after Checked property has changed.")]
        public event EventHandler CheckedChanged;
        /// <summary>
        /// Raises CheckedChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnCheckedChanged(EventArgs e)
        {
            EventHandler handler = CheckedChanged;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Implementation
        public override void Paint(ItemPaintArgs p)
        {
            if (string.IsNullOrEmpty(this.Text) && string.IsNullOrEmpty(this.Symbol) && _Image == null) return; // This is just spacer item

            if ((eRadialMenuType)p.ContextData2 == eRadialMenuType.Circular)
            {
                PaintCircularItem(p);
                return;
            }

            Graphics g = p.Graphics;

            RadialMenuColorTable renderTable = RadialMenuContainer.GetColorTable();
            RadialMenuColorTable localTable = (RadialMenuColorTable)p.ContextData;

            Color foreColor = ColorScheme.GetColor(0x2B579A);
            Color mouseOverColor = Color.FromArgb(72, foreColor);

            if (_IsMouseOver && _TracksMouse && (!localTable.RadialMenuItemMouseOverForeground.IsEmpty || renderTable != null && !renderTable.RadialMenuItemMouseOverForeground.IsEmpty))
            {
                if (!localTable.RadialMenuItemMouseOverForeground.IsEmpty)
                    foreColor = localTable.RadialMenuItemMouseOverForeground;
                else if (renderTable != null && !renderTable.RadialMenuItemMouseOverForeground.IsEmpty)
                    foreColor = renderTable.RadialMenuItemMouseOverForeground;
            }
            else
            {
                if (!localTable.RadialMenuItemForeground.IsEmpty)
                    foreColor = localTable.RadialMenuItemForeground;
                else if (renderTable != null && !renderTable.RadialMenuItemForeground.IsEmpty)
                    foreColor = renderTable.RadialMenuItemForeground;
            }

            if (!localTable.RadialMenuItemMouseOverBackground.IsEmpty)
                mouseOverColor = localTable.RadialMenuItemMouseOverBackground;
            else if (renderTable != null && !renderTable.RadialMenuItemMouseOverBackground.IsEmpty)
                mouseOverColor = renderTable.RadialMenuItemMouseOverBackground;

            Size imageSize = Size.Empty;
            Size itemSize = Size.Empty;
            if (!string.IsNullOrEmpty(_Symbol))
            {
                imageSize = _SymbolTextSize;
            }
            else if (_Image != null)
            {
                imageSize = _Image.Size;
            }
            itemSize = imageSize;
            if (!string.IsNullOrEmpty(Text) && _TextVisible)
            {
                itemSize.Width = Math.Max(itemSize.Width, _TextSize.Width);
                itemSize.Height += _TextSize.Height + 1;
            }
            itemSize.Width += 2; // Padding
            itemSize.Height += 2;

            if (_IsMouseOver && _TracksMouse)
            {
                using (SolidBrush brush = new SolidBrush(mouseOverColor))
                    g.FillPath(brush, _DisplayPath);
            }


            // Calculate position for image/Symbol
            // Position item along the imaginary inner circle
            Rectangle innerCircle = _OutterBounds;
            innerCircle.Width -= itemSize.Width;
            innerCircle.Height -= itemSize.Height;
            innerCircle.Inflate(-2, -2); // Pad it
            if (innerCircle.Width < _CenterBounds.Width) innerCircle.Inflate((_CenterBounds.Width - innerCircle.Width) / 2, 0);
            if (innerCircle.Height < _CenterBounds.Height) innerCircle.Inflate(0, (_CenterBounds.Height - innerCircle.Height) / 2);
            Point imageLoc = new Point(innerCircle.X + (int)((innerCircle.Width / 2) * Math.Cos((_ItemAngle + _ItemPieAngle / 2) * (Math.PI / 180))),
                innerCircle.Y + (int)((innerCircle.Height / 2) * Math.Sin((_ItemAngle + _ItemPieAngle / 2) * (Math.PI / 180))));
            imageLoc.Offset(innerCircle.Width / 2, innerCircle.Height / 2);

            if (!string.IsNullOrEmpty(_Symbol))
            {
                TextDrawing.DrawStringLegacy(g, _Symbol, Symbols.GetFontAwesome(this.SymbolSize),
                    foreColor, new Rectangle(imageLoc.X + itemSize.Width / 2, imageLoc.Y + 2, 0, 0), eTextFormat.HorizontalCenter);
            }
            else if (_Image != null)
            {
                if (g.DpiX != 96f || g.DpiY != 96f)
                {
                    float scaleX = g.DpiX / 96f;
                    float scaleY = g.DpiY / 96f;
                    g.DrawImage(_Image, imageLoc.X + (itemSize.Width - imageSize.Width * scaleX) / 2, imageLoc.Y);
                }
                else
                    g.DrawImage(_Image, imageLoc.X + (itemSize.Width - imageSize.Width) / 2, imageLoc.Y);
            }

            if (!string.IsNullOrEmpty(Text) && _TextVisible)
            {
                TextDrawing.DrawString(g, GetText(this.Text), p.Font, foreColor, imageLoc.X + itemSize.Width / 2 + _TextOffset.X, imageLoc.Y + imageSize.Height + 3 + _TextOffset.Y, eTextFormat.HorizontalCenter);
            }

            if (_Checked)
            {
                using (Pen pen = new Pen(foreColor, 2))
                    g.DrawArc(pen, _OutterBounds, _ItemAngle, _ItemPieAngle);
            }
        }

        private void PaintCircularItem(ItemPaintArgs p)
        {
            Graphics g = p.Graphics;

            RadialMenuColorTable renderTable = RadialMenuContainer.GetColorTable();
            RadialMenuColorTable localTable = (RadialMenuColorTable)p.ContextData;

            Color backColor = ColorScheme.GetColor(0xD44F2E);
            Color foreColor = Color.White;
            Color borderColor = Color.White;

            if (!_CircularForeColor.IsEmpty)
                foreColor = _CircularForeColor;
            else if (!localTable.CircularForeColor.IsEmpty)
                foreColor = localTable.CircularForeColor;
            else if (renderTable != null && !renderTable.CircularForeColor.IsEmpty)
                foreColor = renderTable.CircularForeColor;

            if (!_CircularBackColor.IsEmpty)
                backColor = _CircularBackColor;
            else if (!localTable.CircularBackColor.IsEmpty)
                backColor = localTable.CircularBackColor;
            else if (renderTable != null && !renderTable.CircularBackColor.IsEmpty)
                backColor = renderTable.CircularBackColor;

            if (!_CircularBorderColor.IsEmpty)
                borderColor = _CircularBorderColor;
            else if (!localTable.CircularBorderColor.IsEmpty)
                borderColor = localTable.CircularBorderColor;
            else if (renderTable != null && !renderTable.CircularBorderColor.IsEmpty)
                borderColor = renderTable.CircularBorderColor;

            Color mouseOverColor = Color.FromArgb(192, backColor);

            if (_TracksMouse && _IsMouseOver)
                backColor = mouseOverColor;

            Size imageSize = Size.Empty;
            if (!string.IsNullOrEmpty(_Symbol))
            {
                imageSize = new Size(_SymbolTextSize.Height, _SymbolTextSize.Height); // For consistency use height as guideline
            }
            else if (_Image != null)
            {
                imageSize = _Image.Size;
            }

            if (imageSize.Width > imageSize.Height)
                imageSize.Height = imageSize.Width;
            else if (imageSize.Height > imageSize.Width)
                imageSize.Width = imageSize.Height;

            imageSize.Width += 16;
            imageSize.Height += 16;
            if (_CircularMenuDiameter > 0)
            {
                imageSize.Width = _CircularMenuDiameter;
                imageSize.Height = _CircularMenuDiameter;
            }

            // Calculate position for image/Symbol
            // Position item along the imaginary inner circle
            Rectangle innerCircle = _OutterBounds;
            innerCircle.Inflate(-imageSize.Width, -imageSize.Height);
            if (innerCircle.Width < _CenterBounds.Width) innerCircle.Inflate((_CenterBounds.Width - innerCircle.Width) / 2, 0);
            if (innerCircle.Height < _CenterBounds.Height) innerCircle.Inflate(0, (_CenterBounds.Height - innerCircle.Height) / 2);
            Point imageLoc = new Point(innerCircle.X + (int)((innerCircle.Width / 2) * Math.Cos((_ItemAngle + _ItemPieAngle / 2) * (Math.PI / 180))),
                innerCircle.Y + (int)((innerCircle.Height / 2) * Math.Sin((_ItemAngle + _ItemPieAngle / 2) * (Math.PI / 180))));
            imageLoc.Offset(innerCircle.Width/2, innerCircle.Height/2);
            Rectangle r = new Rectangle(imageLoc, Size.Empty);
            r.Inflate(imageSize.Width/2, imageSize.Height/2);
            r.Inflate(-1, -1);
            if (backColor.A < 255)
                g.FillEllipse(Brushes.White, r);
            using (SolidBrush brush = new SolidBrush(backColor))
                g.FillEllipse(brush, r);
            r.Inflate(1, 1);
            using (Pen pen = new Pen(borderColor, 3))
                g.DrawEllipse(pen, r);

            if (!string.IsNullOrEmpty(_Symbol))
            {
                TextDrawing.DrawStringLegacy(g, _Symbol, Symbols.GetFontAwesome(this.SymbolSize),
                    foreColor, new RectangleF(r.X + r.Width / 2, r.Y + r.Height / 2 + 3, 0, 0), eTextFormat.HorizontalCenter | eTextFormat.VerticalCenter | eTextFormat.SingleLine);
            }
            else if (_Image != null)
            {
                ImageHelper.DrawImageCenteredDpiAware(g, _Image, r);
                //g.DrawImage(_Image, r.X + (r.Width - _Image.Width) / 2, r.Y + (r.Height - _Image.Height) / 2);
            }
        }
        
        private Size _SymbolTextSize = Size.Empty;
        private Size _TextSize = Size.Empty;
        public override void RecalcSize()
        {
            _SymbolTextSize = Size.Empty;
            _TextSize = Size.Empty;

            Control c = this.ContainerControl as Control;
            if (c == null || !BarFunctions.IsHandleValid(c)) return;

            Graphics g = BarFunctions.CreateGraphics(c);
            if (g == null) return;

            try
            {
                if (!string.IsNullOrEmpty(_Symbol))
                {
                    Font symFont = Symbols.GetFontAwesome(_SymbolSize);
                    _SymbolTextSize = TextDrawing.MeasureStringLegacy(g, _Symbol, Symbols.GetFontAwesome(this.SymbolSize), Size.Empty, eTextFormat.Default);
                    int descent = (int)Math.Ceiling((symFont.FontFamily.GetCellDescent(symFont.Style) *
                    symFont.Size / symFont.FontFamily.GetEmHeight(symFont.Style)));
                    _SymbolTextSize.Height -= descent;
                    //_SymbolTextSize.Width += 4; // Add some padding
                    //_SymbolTextSize.Height += 4; // Add some padding
                }
                if (!string.IsNullOrEmpty(Text) && _TextVisible)
                {
                    string text = GetText(this.Text);
                    _TextSize = TextDrawing.MeasureString(g, text, c.Font, Size.Empty, eTextFormat.Default);
                    _TextSize.Width += 2; // Add some padding
                }
            }
            finally
            {
                g.Dispose();
            }

            base.RecalcSize();
        }

        private string GetText(string text)
        {
            if (text.Contains("\\r\\n")) text = text.Replace("\\r\\n", "\r\n"); // Escaped by designer fix it
            return text;
        }
        private GraphicsPath _DisplayPath;
        /// <summary>
        /// Gets display path of the item.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GraphicsPath DisplayPath
        {
            get { return _DisplayPath; }
            internal set
            {
                if (value != _DisplayPath)
                {
                    GraphicsPath oldValue = _DisplayPath;
                    _DisplayPath = value;
                    OnDisplayPathChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when DisplayPath property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnDisplayPathChanged(GraphicsPath oldValue, GraphicsPath newValue)
        {
            if (oldValue != null && oldValue != newValue) oldValue.Dispose();
            //this.Refresh();
            //OnPropertyChanged(new PropertyChangedEventArgs("DisplayPath"));
        }

        /// <summary>
        /// Returns copy of the item.
        /// </summary>
        public override BaseItem Copy()
        {
            RadialMenuItem copy = new RadialMenuItem();
            this.CopyToItem(copy);
            return copy;
        }

        /// <summary>
        /// Copies the RadialMenuItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New ProgressBarItem instance.</param>
        internal void InternalCopyToItem(RadialMenuItem copy)
        {
            CopyToItem(copy);
        }

        /// <summary>
        /// Copies the RadialMenuItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New RadialMenuItem instance.</param>
        protected override void CopyToItem(BaseItem copy)
        {
            base.CopyToItem(copy);

            RadialMenuItem item = copy as RadialMenuItem;
            item.Checked = _Checked;
            item.Symbol = _Symbol;
            item.SymbolSize = _SymbolSize;
            item.TextVisible = _TextVisible;
            item.TracksMouse = _TracksMouse;
            item.CircularBackColor = _CircularBackColor;
            item.CircularBorderColor = _CircularBorderColor;
            item.CircularForeColor = _CircularForeColor;
            item.CircularMenuDiameter = _CircularMenuDiameter;
            item.Image = _Image;
        }

        private string _Symbol = "";
        /// <summary>
        /// Indicates the symbol displayed on face of the button instead of the image. Setting the symbol overrides the image setting.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates the symbol displayed on face of the button instead of the image. Setting the symbol overrides the image setting.")]
        [Editor("DevComponents.DotNetBar.Design.SymbolTypeEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public string Symbol
        {
            get { return _Symbol; }
            set
            {
                if (value != _Symbol)
                {
                    string oldValue = _Symbol;
                    _Symbol = value;
                    OnSymbolChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Symbol property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSymbolChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Symbol"));
            NeedRecalcSize = true;
            OnAppearanceChanged();
            this.Refresh();
        }

        private float _SymbolSize = 0f;
        /// <summary>
        /// Indicates the size of the symbol in points.
        /// </summary>
        [DefaultValue(0f), Category("Appearance"), Description("Indicates the size of the symbol in points.")]
        public float SymbolSize
        {
            get { return _SymbolSize; }
            set
            {
                if (value != _SymbolSize)
                {
                    float oldValue = _SymbolSize;
                    _SymbolSize = value;
                    OnSymbolSizeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SymbolSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSymbolSizeChanged(float oldValue, float newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SymbolSize"));
            NeedRecalcSize = true;
            OnAppearanceChanged();
            this.Refresh();
        }

        private Image _Image;
        /// <summary>
        /// Gets or sets the image displayed on the item.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Indicates image displayed on the item.")]
        public Image Image
        {
            get { return _Image; }
            set
            {
                if (value != _Image)
                {
                    Image oldValue = _Image;
                    _Image = value;
                    OnImageChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Image property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnImageChanged(Image oldValue, Image newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Image"));
            NeedRecalcSize = true;
            OnAppearanceChanged();
            this.Refresh();
        }

        private Rectangle _OutterBounds;
        internal Rectangle OutterBounds
        {
            get { return _OutterBounds; }
            set { _OutterBounds = value; }
        }

        private Rectangle _CenterBounds;
        internal Rectangle CenterBounds
        {
            get { return _CenterBounds; }
            set { _CenterBounds = value; }
        }

        private int _ItemAngle;
        internal int ItemAngle
        {
            get { return _ItemAngle; }
            set { _ItemAngle = value; }
        }

        private int _ItemPieAngle;
        internal int ItemPieAngle
        {
            get { return _ItemPieAngle; }
            set { _ItemPieAngle = value; }
        }

        private bool _TextVisible = true;
        /// <summary>
        /// Indicates whether text on item is visible.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether text on item is visible")]
        public bool TextVisible
        {
            get { return _TextVisible; }
            set
            {
                if (value != _TextVisible)
                {
                    bool oldValue = _TextVisible;
                    _TextVisible = value;
                    OnTextVisibleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TextVisible property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTextVisibleChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TextVisible"));
            NeedRecalcSize = true;
            OnAppearanceChanged();
            this.Refresh();
        }

        public override void InternalMouseMove(MouseEventArgs objArg)
        {
            if (!_IsMouseOver)
                IsMouseOver = true;
            base.InternalMouseMove(objArg);
        }

        public override void InternalMouseLeave()
        {
            if (_IsMouseOver)
                IsMouseOver = false;
            base.InternalMouseLeave();
        }

        private bool _IsMouseOver;
        /// <summary>
        /// Gets whether mouse is over the item.
        /// </summary>
        [Browsable(false)]
        public bool IsMouseOver
        {
            get { return _IsMouseOver; }
            internal set
            {
                _IsMouseOver = value;
                OnIsMouseOverChanged();
            }
        }

        protected virtual void OnIsMouseOverChanged()
        {
            this.Refresh();
        }

        private bool _TracksMouse = true;
        /// <summary>
        /// Indicates whether item changes its appearance when mouse is over it or pressed
        /// </summary>
        [DefaultValue(true), Description("Indicates whether item changes its appearance when mouse is over it or pressed"), Category("Behavior")]
        public bool TracksMouse
        {
            get { return _TracksMouse; }
            set
            {
                if (value != _TracksMouse)
                {
                    bool oldValue = _TracksMouse;
                    _TracksMouse = value;
                    OnTracksMouseChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TracksMouse property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTracksMouseChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TracksMouse"));

        }

        protected internal override void OnExpandChange()
        {
            NeedRecalcSize = true;
            if (!this.Expanded && HotSubItem != null) HotSubItem.InternalMouseLeave();
            base.OnExpandChange();
        }

        private bool _Checked = false;
        /// <summary>
        /// Indicates whether item is in checked state.
        /// </summary>
        [DefaultValue(false), Category("Appearance"), Description("Indicates whether item is in checked state.")]
        public bool Checked
        {
            get { return _Checked; }
            set
            {
                if (value != _Checked)
                {
                    bool oldValue = _Checked;
                    _Checked = value;
                    OnCheckedChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Checked property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnCheckedChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Checked"));
            this.Refresh();
        }

        protected internal override void OnSubItemExpandChange(BaseItem item)
        {
            if (this.Parent != null)
                this.Parent.OnSubItemExpandChange(item); // Pass it up the chain
            base.OnSubItemExpandChange(item);
        }

        private Point _TextOffset = Point.Empty;
        /// <summary>
        /// Gets or sets the optional text offset for the item.
        /// </summary>
        [Category("Appearance"), Description("Specifies optional text offset.")]
        public Point TextOffset
        {
            get { return _TextOffset; }
            set
            {
                if (value != _TextOffset)
                {
                    Point oldValue = _TextOffset;
                    _TextOffset = value;
                    OnTextOffsetChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TextOffset property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTextOffsetChanged(Point oldValue, Point newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TextOffset"));
            //NeedRecalcSize = true;
            this.Invalidate();
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeTextOffset()
        {
            return !_TextOffset.IsEmpty;
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetTextOffset()
        {
            TextOffset = Point.Empty;
        }

        #region Circular Menu
        private int _CircularMenuDiameter = 0;
        /// <summary>
        /// Specifies explicit circular menu type diameter. Applies to circular menu type only.
        /// </summary>
        [DefaultValue(0), Category("Appearance"), Description("Specifies explicit circular menu type diameter. Applies to circular menu type only.")]
        public int CircularMenuDiameter
        {
            get { return _CircularMenuDiameter; }
            set
            {
                if (value < 0) value = 0;
                if (value != _CircularMenuDiameter)
                {
                    int oldValue = _CircularMenuDiameter;
                    _CircularMenuDiameter = value;
                    OnCircularMenuDiameterChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when CircularMenuDiameter property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnCircularMenuDiameterChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("CircularMenuDiameter"));
            this.Refresh();
        }

        private Color _CircularBackColor = Color.Empty;
        /// <summary>
        /// Gets or sets background color of the circular menu item type. Applies only to circular menu types.
        /// </summary>
        [Category("Appearance"), Description("Indicates background color of the circular menu item type.")]
        public Color CircularBackColor
        {
            get { return _CircularBackColor; }
            set { _CircularBackColor = value; this.Invalidate(); /*OnPropertyChanged("CircularBackColor");*/ }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCircularBackColor()
        {
            return !_CircularBackColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetCircularBackColor()
        {
            this.CircularBackColor = Color.Empty;
        }

        private Color _CircularForeColor = Color.Empty;
        /// <summary>
        /// Gets or sets text color of the circular menu item type. Applies only to circular menu types.
        /// </summary>
        [Category("Appearance"), Description("Indicates text color of the circular menu item type.")]
        public Color CircularForeColor
        {
            get { return _CircularForeColor; }
            set { _CircularForeColor = value; this.Invalidate(); /*OnPropertyChanged("CircularForeColor");*/ }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCircularForeColor()
        {
            return !_CircularForeColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetCircularForeColor()
        {
            this.CircularForeColor = Color.Empty;
        }

        private Color _CircularBorderColor = Color.Empty;
        /// <summary>
        /// Gets or sets border color of the circular menu item type. Applies only to circular menu types.
        /// </summary>
        [Category("Appearance"), Description("Indicates border color of the circular menu item type.")]
        public Color CircularBorderColor
        {
            get { return _CircularBorderColor; }
            set { _CircularBorderColor = value; this.Invalidate(); /*OnPropertyChanged("CircularBorderColor");*/ }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCircularBorderColor()
        {
            return !_CircularBorderColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetCircularBorderColor()
        {
            this.CircularBorderColor = Color.Empty;
        }
        #endregion
        #endregion
    }
}
