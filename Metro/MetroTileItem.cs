using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using DevComponents.DotNetBar.Metro.Rendering;
using System.Windows.Forms;
using DevComponents.DotNetBar.Primitives;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
    /// Represents Metro Tile.
    /// </summary>
    [DefaultEvent("Click"), DefaultProperty("Text")]
    [ToolboxItem(false), Designer(typeof(DevComponents.DotNetBar.Design.SimpleItemDesigner))]
    public class MetroTileItem : BaseItem
    {
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

        /// <summary>
        /// Occurs before an item in option group is checked and provides opportunity to cancel that.
        /// </summary>
        [Description("Occurs before an item in option group is checked and provides opportunity to cancel that.")]
        public event OptionGroupChangingEventHandler OptionGroupChanging;
        /// <summary>
        /// Raises OptionGroupChanging event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnOptionGroupChanging(OptionGroupChangingEventArgs e)
        {
            OptionGroupChangingEventHandler handler = OptionGroupChanging;
            if (handler != null)
                handler(this, e);

            if (!e.Cancel)
            {
                IOwnerItemEvents owner = this.GetIOwnerItemEvents();
                if (owner != null)
                    owner.InvokeOptionGroupChanging(this, e);
            }
        }
        #endregion

        #region Constructor
        private static readonly Size DefaultTileSize = new Size(180, 90);
        /// <summary>
        /// Creates new instance of metro tile.
        /// </summary>
        public MetroTileItem() : this("", "") { }
        /// <summary>
        /// Creates new instance of metro tile and assigns the name to it.
        /// </summary>
        /// <param name="sItemName">Item name.</param>
        public MetroTileItem(string sItemName) : this(sItemName, "") { }
        /// <summary>
        /// Creates new instance of metro tile and assigns the name and text to it.
        /// </summary>
        /// <param name="sItemName">Item name.</param>
        /// <param name="ItemText">item text.</param>
        public MetroTileItem(string sItemName, string ItemText)
            : base(sItemName, ItemText)
        {
            _Frames = new CustomCollection<MetroTileFrame>();
            _Frames.CollectionChanged += new NotifyCollectionChangedEventHandler(FramesCollectionChanged);
            MetroTileFrame frame = new MetroTileFrame();
            frame.MarkupLinkClick += new MarkupLinkClickEventHandler(MarkupLinkClick);
            _Frames.Add(frame);
        }

        private void MarkupLinkClick(object sender, MarkupLinkClickEventArgs e)
        {
            OnTitleTextMarkupLinkClick(e);
        }
        /// <summary>
        /// Returns copy of the item.
        /// </summary>
        public override BaseItem Copy()
        {
            MetroTileItem objCopy = new MetroTileItem(m_Name);
            this.CopyToItem(objCopy);
            return objCopy;
        }

        /// <summary>
        /// Copies the MetroTileItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New ProgressBarItem instance.</param>
        internal void InternalCopyToItem(MetroTileItem copy)
        {
            CopyToItem(copy);
        }

        /// <summary>
        /// Copies the MetroTileItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New MetroTileItem instance.</param>
        protected override void CopyToItem(BaseItem copy)
        {
            base.CopyToItem(copy);

            MetroTileItem tile = copy as MetroTileItem;
            tile.AnimationDuration = this.AnimationDuration;
            tile.AnimationEnabled = this.AnimationEnabled;
            tile.AutoRotateFramesInterval = this.AutoRotateFramesInterval;
            tile.CheckBehavior = this.CheckBehavior;
            tile.Checked = this.Checked;
            tile.DisabledBackColor = this.DisabledBackColor;
            tile.EnableMarkup = this.EnableMarkup;
            tile.Image = this.Image;
            tile.Symbol = this.Symbol;
            tile.SymbolSize = this.SymbolSize;
            tile.ImageIndent = this.ImageIndent;
            tile.ImageTextAlignment = this.ImageTextAlignment;
            tile.OptionGroup = this.OptionGroup;
            tile.TileColor = this.TileColor;
            tile.TileSize = this.TileSize;
            tile.TitleText = this.TitleText;
            tile.TitleTextAlignment = this.TitleTextAlignment;
            tile.TitleTextColor = this.TitleTextColor;
            tile.TitleTextFont = this.TitleTextFont;
            tile.Text = this.Text;

            tile.NotificationMarkColor = this.NotificationMarkColor;
            tile.NotificationMarkPosition = this.NotificationMarkPosition;
            tile.NotificationMarkSize = this.NotificationMarkSize;
            tile.NotificationMarkText = this.NotificationMarkText;

            for (int i = 1; i < this.Frames.Count; i++)
            {
                tile.Frames.Add((MetroTileFrame)this.Frames[i].Clone());
            }
        }

        protected override void Dispose(bool disposing)
        {
            DisposeCurrentAnimation();
            DestroyAutoRotateTimer();
            base.Dispose(disposing);
        }
        #endregion

        #region Implementation

        private string _NotificationMarkText = "";
        /// <summary>
        /// Specifies maximum of 2 character text displayed inside of the notification mark on top of the button.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Specifies maximum of 2 character text displayed inside of the notification mark on top of the button.")]
        public string NotificationMarkText
        {
            get { return _NotificationMarkText; }
            set
            {
                if (value != _NotificationMarkText)
                {
                    string oldValue = _NotificationMarkText;
                    _NotificationMarkText = value;
                    OnNotificationMarkTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when NotificationMarkText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnNotificationMarkTextChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(newValue)) _NotificationMarkWasDisplayed = true; // Make sure button gets invalidated properly
            this.Refresh();
        }

        private eNotificationMarkPosition _NotificationMarkPosition = eNotificationMarkPosition.TopLeft;
        /// <summary>
        /// Indicates the position of the notification marker within the bounds of the button.
        /// </summary>
        [DefaultValue(eNotificationMarkPosition.TopLeft), Category("Appearance"), Description("Indicates the position of the notification marker within the bounds of the button.")]
        public eNotificationMarkPosition NotificationMarkPosition
        {
            get { return _NotificationMarkPosition; }
            set
            {
                if (value != _NotificationMarkPosition)
                {
                    eNotificationMarkPosition oldValue = _NotificationMarkPosition;
                    _NotificationMarkPosition = value;
                    OnNotificationMarkPositionChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when NotificationMarkPosition property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnNotificationMarkPositionChanged(eNotificationMarkPosition oldValue, eNotificationMarkPosition newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("NotificationMarkPosition"));
            if (!string.IsNullOrEmpty(_NotificationMarkText))
                this.Refresh();
        }
        private int _NotificationMarkSize = 0;
        /// <summary>
        /// Specifies diameter of notification mark. When set to 0 system default value is used.
        /// </summary>
        [DefaultValue(0), Category("Appearance"), Description("Specifies diameter of notification mark. When set to 0 system default value is used.")]
        public int NotificationMarkSize
        {
            get { return _NotificationMarkSize; }
            set
            {
                if (value != _NotificationMarkSize)
                {
                    int oldValue = _NotificationMarkSize;
                    _NotificationMarkSize = value;
                    OnNotificationMarkSizeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when NotificationMarkSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnNotificationMarkSizeChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("NotificationMarkSize"));
            this.Refresh();
        }
        private Color _NotificationMarkColor = Color.Empty;
        /// <summary>
        /// Gets or sets background color of the notification mark.
        /// </summary>
        [Category("Appearance"), Description("Indicates background color of the notification mark.")]
        public Color NotificationMarkColor
        {
            get { return _NotificationMarkColor; }
            set { _NotificationMarkColor = value; this.Refresh(); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeNotificationMarkColor()
        {
            return !_NotificationMarkColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetNotificationMarkColor()
        {
            this.NotificationMarkColor = Color.Empty;
        }
        private static readonly Point DefaultNotificationMarkOffset = new Point(-2, -2);
        private Point _NotificationMarkOffset = DefaultNotificationMarkOffset;
        /// <summary>
        /// Specifies the offset for the notification mark relative to its position.
        /// </summary>
        [Category("Appearance"), Description("Specifies the offset for the notification mark relative to its position.")]
        public Point NotificationMarkOffset
        {
            get { return _NotificationMarkOffset; }
            set
            {
                if (value != _NotificationMarkOffset)
                {
                    Point oldValue = _NotificationMarkOffset;
                    _NotificationMarkOffset = value;
                    OnNotificationMarkOffsetChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when NotificationMarkOffset property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnNotificationMarkOffsetChanged(Point oldValue, Point newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("NotificationMarkOffset"));
            this.Invalidate();
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeNotificationMarkOffset()
        {
            return _NotificationMarkOffset != DefaultNotificationMarkOffset;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetNotificationMarkOffset()
        {
            this.NotificationMarkOffset = DefaultNotificationMarkOffset;
        }
        private bool _NotificationMarkWasDisplayed = false;
        protected override Rectangle GetInvalidateBounds()
        {
            if (_NotificationMarkWasDisplayed || !string.IsNullOrEmpty(_NotificationMarkText))
            {
                _NotificationMarkWasDisplayed = false;
                Rectangle r = base.GetInvalidateBounds();
                r.Inflate(6, 6);
                return r;
            }
            return base.GetInvalidateBounds();
        }

        private void FramesCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.OldItems != null && e.OldItems.Count > 0)
            {
                foreach (MetroTileFrame item in e.OldItems)
                {
                    item.PropertyChanged -= FramePropertyChanged;
                }
            }
            if (e.NewItems != null && e.NewItems.Count > 0)
            {
                foreach (MetroTileFrame item in e.NewItems)
                {
                    item.PropertyChanged += FramePropertyChanged;
                }
            }

            if (_CurrentFrame >= _Frames.Count)
            {
                _CurrentFrame = 0;
                this.Refresh();
            }
        }
        private void FramePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "TileStyle" || e.PropertyName == "TitleText" || e.PropertyName == "TitleTextAlignment"
                || e.PropertyName == "Image" || e.PropertyName == "ImageIndent" || e.PropertyName == "Text" || e.PropertyName == "TextMarkupEnabled"
                || e.PropertyName == "ImageTextAlignment" || e.PropertyName == "Symbol" || e.PropertyName == "SymbolSize")
                this.NeedRecalcSize = true;
            this.Refresh();
        }

        /// <summary>
        /// Occurs just before Click event is fired.
        /// </summary>
        protected override void OnClick()
        {
            base.OnClick();
            ExecuteCommand();
        }
        private Rectangle _LastRenderBounds = Rectangle.Empty;
        public override void Paint(ItemPaintArgs p)
        {
            if (p.AnimationEnabled && p.AnimationRequests != null && !_LastRenderBounds.IsEmpty && m_Rect != _LastRenderBounds)
            {
                // Animate transition
                Rectangle newBounds = m_Rect;
                m_Rect = _LastRenderBounds;
                _LastRenderBounds = Rectangle.Empty;
                Animation.AnimationRequest ar = new DevComponents.DotNetBar.Animation.AnimationRequest(this, "Bounds", m_Rect, newBounds);
                p.AnimationRequests.Add(ar);
                return;
            }
            MetroRender.Paint(this, p);

            if (this.Focused && this.DesignMode)
            {
                Rectangle r = m_Rect;
                r.Inflate(-1, -1);
                DesignTime.DrawDesignTimeSelection(p.Graphics, r, p.Colors.ItemDesignTimeBorder);
            }
            _LastRenderBounds = m_Rect;

            if (!string.IsNullOrEmpty(_NotificationMarkText))
                DevComponents.DotNetBar.Rendering.NotificationMarkPainter.Paint(p.Graphics, this.Bounds,
                    _NotificationMarkPosition, _NotificationMarkText,
                    (_NotificationMarkSize > 0 ? new Size(_NotificationMarkSize, _NotificationMarkSize) : DefaultMarkSize), _NotificationMarkOffset, _NotificationMarkColor);

            if (!(p.ContainerControl is MetroTilePanel))
                this.DrawInsertMarker(p.Graphics);
        }
        private static readonly Size DefaultMarkSize = new Size(16, 16);

        public override void RecalcSize()
        {
            Control control = this.ContainerControl as Control;
            if (control == null || control.Disposing || control.IsDisposed)
                return;

            Graphics g = BarFunctions.CreateGraphics(control);
            if (g == null) return;
            bool rightToLeft = (control.RightToLeft == RightToLeft.Yes);

            for (int i = 0; i < _Frames.Count; i++)
            {
                MetroTileFrame frame = _Frames[i];

                bool dispose = false;
                ElementStyle style = ElementStyleDisplay.GetElementStyle(frame.EffectiveStyle, out dispose);
                int textWidth = _TileSize.Width - style.PaddingHorizontal;
                int maxItemWidth = 0;
                // Child items positioned in top-left corner
                if (m_SubItems != null && i < m_SubItems.Count)
                {
                    BaseItem item = m_SubItems[i];
                    if (item.Visible)
                    {
                        item.LeftInternal = m_Rect.Left + style.PaddingLeft;
                        item.TopInternal = m_Rect.Top + style.PaddingTop;
                        item.WidthInternal = _TileSize.Width - style.PaddingHorizontal;
                        item.HeightInternal = _TileSize.Height - style.PaddingVertical;
                        item.RecalcSize();
                        item.Displayed = true;
                        maxItemWidth = Math.Max(maxItemWidth, item.WidthInternal);
                    }
                    else
                        item.Displayed = false;
                }
                textWidth = Math.Max(12, textWidth - maxItemWidth);

                ContentAlignment imageTextAlign = frame.ImageTextAlignment;
                if (!string.IsNullOrEmpty(frame.Symbol))
                {
                    Font symFont = Symbols.GetFontAwesome(frame.SymbolSize);
                    Size imageSize = TextDrawing.MeasureString(g, "\uF00A", symFont); // Need to do this to get consistent size for the symbol since they are not all the same width we pick widest
                    //imageSize = TextDrawing.MeasureString(g, button.Symbol, symFont);
                    int descent = (int)Math.Ceiling((symFont.FontFamily.GetCellDescent(symFont.Style) *
                        symFont.Size / symFont.FontFamily.GetEmHeight(symFont.Style)));
                    imageSize.Height -= descent;

                    if (imageTextAlign == ContentAlignment.TopLeft || imageTextAlign == ContentAlignment.TopRight ||
                        imageTextAlign == ContentAlignment.BottomLeft || imageTextAlign == ContentAlignment.BottomRight ||
                        imageTextAlign == ContentAlignment.MiddleLeft || imageTextAlign == ContentAlignment.MiddleRight)
                        textWidth = Math.Max(12, textWidth - imageSize.Width);
                }
                else if (frame.Image != null)
                {
                    if (imageTextAlign == ContentAlignment.TopLeft || imageTextAlign == ContentAlignment.TopRight ||
                        imageTextAlign == ContentAlignment.BottomLeft || imageTextAlign == ContentAlignment.BottomRight ||
                        imageTextAlign == ContentAlignment.MiddleLeft || imageTextAlign == ContentAlignment.MiddleRight)
                        textWidth = Math.Max(12, textWidth - frame.Image.Width);
                }

                if (frame.Text != "")
                    MetroTileFrame.MeasureText(frame, g, textWidth, control.Font, eTextFormat.Default, rightToLeft);
                Size titleSize = Size.Empty;
                frame.TitleTextBounds = Rectangle.Empty;
                if (frame.TitleTextMarkupBody != null)
                {
                    Font titleFont = GetTitleTextFont(frame, style, control);
                    Size availSize = new Size(textWidth, 1);
                    TextMarkup.MarkupDrawContext d = new TextMarkup.MarkupDrawContext(g, titleFont, System.Drawing.Color.Empty, false);
                    frame.TitleTextMarkupBody.Measure(availSize, d);
                    availSize.Height = frame.TitleTextMarkupBody.Bounds.Size.Height;
                    d.RightToLeft = rightToLeft;
                    frame.TitleTextMarkupBody.Arrange(new Rectangle(0, 0, availSize.Width, availSize.Height), d);
                    titleSize = frame.TitleTextMarkupBody.Bounds.Size;
                }
                else if (!string.IsNullOrEmpty(frame.TitleText))
                {
                    Font titleFont = GetTitleTextFont(frame, style, control);
                    Size availSize = new Size(_TileSize.Width - style.PaddingHorizontal, _TileSize.Height);
                    titleSize = TextDrawing.MeasureString(g, frame.TitleText, titleFont, availSize.Width, eTextFormat.Default | eTextFormat.SingleLine);
                }

                if (!titleSize.IsEmpty)
                {
                    titleSize.Width = Math.Min(titleSize.Width + 2, _TileSize.Width);
                    if (frame.TitleTextAlignment == ContentAlignment.BottomCenter)
                        frame.TitleTextBounds = new Rectangle(style.PaddingLeft + (_TileSize.Width - style.PaddingHorizontal - titleSize.Width) / 2, _TileSize.Height - titleSize.Height - style.PaddingBottom, _TileSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.BottomLeft)
                        frame.TitleTextBounds = new Rectangle(style.PaddingLeft, _TileSize.Height - titleSize.Height - style.PaddingBottom, titleSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.BottomRight)
                        frame.TitleTextBounds = new Rectangle(_TileSize.Width - style.PaddingRight - titleSize.Width, _TileSize.Height - titleSize.Height - style.PaddingBottom, titleSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.MiddleCenter)
                        frame.TitleTextBounds = new Rectangle(style.PaddingLeft + (_TileSize.Width - style.PaddingHorizontal - titleSize.Width) / 2, style.PaddingTop + (_TileSize.Height - titleSize.Height) / 2, _TileSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.MiddleLeft)
                        frame.TitleTextBounds = new Rectangle(style.PaddingLeft, style.PaddingTop + (_TileSize.Height - titleSize.Height) / 2, titleSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.MiddleRight)
                        frame.TitleTextBounds = new Rectangle(_TileSize.Width - style.PaddingRight - titleSize.Width, style.PaddingTop + (_TileSize.Height - titleSize.Height) / 2, titleSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.TopCenter)
                        frame.TitleTextBounds = new Rectangle(style.PaddingLeft + (_TileSize.Width - style.PaddingHorizontal - titleSize.Width) / 2, style.PaddingTop, titleSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.TopLeft)
                        frame.TitleTextBounds = new Rectangle(style.PaddingLeft, style.PaddingTop, titleSize.Width, titleSize.Height + 1);
                    else if (frame.TitleTextAlignment == ContentAlignment.TopRight)
                        frame.TitleTextBounds = new Rectangle(_TileSize.Width - style.PaddingRight - titleSize.Width, style.PaddingTop, titleSize.Width, titleSize.Height + 1);
                }

                if (dispose) style.Dispose();
            }

            m_Rect = new Rectangle(m_Rect.X, m_Rect.Y, _TileSize.Width, _TileSize.Height);
            base.RecalcSize();
        }

        internal Font GetTitleTextFont(MetroTileFrame frame, ElementStyle style, Control control)
        {
            Font titleFont = frame.TitleTextFont;
            if (titleFont == null && style != null) titleFont = style.Font;
            if (titleFont == null && control != null) titleFont = control.Font;
            if (titleFont == null) titleFont = SystemFonts.DefaultFont; // SystemInformation.MenuFont;

            return titleFont;
        }
        private void TileStyleChanged(object sender, EventArgs e)
        {
            this.NeedRecalcSize = true;
            this.OnAppearanceChanged();
        }

        private Size _TileSize = DefaultTileSize;
        /// <summary>
        /// Gets or sets the tile size.
        /// </summary>
        [Description("Indicates Tile Size"), Category("Appearance")]
        public Size TileSize
        {
            get { return _TileSize; }
            set
            {
                if (value != _TileSize)
                {
                    Size oldValue = _TileSize;
                    _TileSize = value;
                    OnTileSizeChanged(oldValue, value);
                }
            }
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeTileSize()
        {
            return _TileSize != DefaultTileSize;
        }
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetTileSize()
        {
            TileSize = DefaultTileSize;
        }
        /// <summary>
        /// Called when TileSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTileSizeChanged(Size oldValue, Size newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TileSize"));
            NeedRecalcSize = true;
            this.OnAppearanceChanged();
        }

        /// <summary>
        /// Gets or sets the predefined tile color for default tile frame.
        /// </summary>
        [Category("Appearance"), Description("Indicates predefined tile color for default tile frame.")]
        public eMetroTileColor TileColor
        {
            get { return _Frames[0].TileColor; }
            set { _Frames[0].TileColor = value; }
        }

        /// <summary>
        /// Specifies the Tile style default tile frame.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), Category("Style"), Description("Gets or sets Tile style for default tile frame."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ElementStyle TileStyle
        {
            get { return _Frames[0].TileStyle; }
        }

        /// <summary>
        /// Indicates the symbol displayed on face of the tile instead of the image. Setting the symbol overrides the image setting.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates the symbol displayed on face of the tile instead of the image. Setting the symbol overrides the image setting.")]
        [Editor("DevComponents.DotNetBar.Design.SymbolTypeEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public string Symbol
        {
            get { return _Frames[0].Symbol; }
            set { _Frames[0].Symbol = value;  }
        }

        /// <summary>
        /// Indicates the size of the symbol in points.
        /// </summary>
        [DefaultValue(0f), Category("Appearance"), Description("Indicates the size of the symbol in points.")]
        public float SymbolSize
        {
            get { return _Frames[0].SymbolSize; }
            set { _Frames[0].SymbolSize = value; }
        }
        /// <summary>
        /// Gets or sets the color of the symbol.
        /// </summary>
        [Category("Columns"), Description("Indicates color of the symbol.")]
        public Color SymbolColor
        {
            get { return _Frames[0].SymbolColor; }
            set { _Frames[0].SymbolColor = value; }
        }

        /// <summary>
        /// Gets or sets the image displayed on the tile.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Indicates image displayed on the tile.")]
        public Image Image
        {
            get { return _Frames[0].Image; }
            set { _Frames[0].Image = value; }
        }

        /// <summary>
        /// Gets or sets image alignment in relation to text.
        /// </summary>
        [DefaultValue(ContentAlignment.TopLeft), Category("Appearance"), Description("Indicates the image alignment in relation to text.")]
        public ContentAlignment ImageTextAlignment
        {
            get { return _Frames[0].ImageTextAlignment; }
            set { _Frames[0].ImageTextAlignment = value; }
        }

        /// <summary>
        /// Gets or sets the text associated with this item.
        /// </summary>
        [Browsable(true), Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.Category("Appearance"), System.ComponentModel.Description("The text contained in the item."), Localizable(true), DefaultValue("")]
        public override string Text
        {
            get
            {
                return _Frames[0].Text;
            }
            set
            {
                _Frames[0].Text = value;
            }
        }

        public override void InternalMouseDown(System.Windows.Forms.MouseEventArgs objArg)
        {
            if (objArg.Button == System.Windows.Forms.MouseButtons.Left && GetEnabled())
                IsLeftMouseButtonDown = true;
            eMetroTileCheckBehavior checkBehavior = EffectiveCheckBehavior;
            if (checkBehavior == eMetroTileCheckBehavior.LeftMouseButtonClick && objArg.Button == MouseButtons.Left)
                this.Checked = !this.Checked;
            else if (checkBehavior == eMetroTileCheckBehavior.MiddleMouseButtonClick && objArg.Button == MouseButtons.Middle)
                this.Checked = !this.Checked;
            else if (checkBehavior == eMetroTileCheckBehavior.RightMouseButtonClick && objArg.Button == MouseButtons.Right)
                this.Checked = !this.Checked;

            base.InternalMouseDown(objArg);
        }
        public override void InternalMouseUp(System.Windows.Forms.MouseEventArgs objArg)
        {
            if (objArg.Button == System.Windows.Forms.MouseButtons.Left || Control.MouseButtons != MouseButtons.Left) IsLeftMouseButtonDown = false;
            base.InternalMouseUp(objArg);
        }
        public override void InternalMouseMove(System.Windows.Forms.MouseEventArgs objArg)
        {
            if (GetEnabled())
                IsMouseOver = true;
            try
            {
                if (!this.DesignMode)
                    SetIsContainer(true);
                base.InternalMouseMove(objArg);
            }
            finally
            {
                SetIsContainer(false);
            }
        }
        public override void InternalMouseLeave()
        {
            IsMouseOver = false;
            IsLeftMouseButtonDown = false;
            base.InternalMouseLeave();
        }

        private bool _IsLeftMouseButtonDown = false;
        /// <summary>
        /// Gets whether left mouse button is pressed over the tile.
        /// </summary>
        [Browsable(false)]
        public bool IsLeftMouseButtonDown
        {
            get { return _IsLeftMouseButtonDown; }
            internal set
            {
                if (value != _IsLeftMouseButtonDown)
                {
                    bool oldValue = _IsLeftMouseButtonDown;
                    _IsLeftMouseButtonDown = value;
                    OnIsLeftMouseButtonDownChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when IsLeftMouseButtonDown property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnIsLeftMouseButtonDownChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("IsLeftMouseButtonDown"));
            this.Refresh();
        }

        private bool _IsMouseOver = false;
        /// <summary>
        /// Gets whether mouse is over the item.
        /// </summary>
        [Browsable(false)]
        public bool IsMouseOver
        {
            get { return _IsMouseOver; }
            internal set
            {
                if (value != _IsMouseOver)
                {
                    bool oldValue = _IsMouseOver;
                    _IsMouseOver = value;
                    OnIsMouseOverChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when IsMouseOver property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnIsMouseOverChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("IsMouseOver"));
            this.Refresh();
        }

        /// <summary>
        /// Gets or sets the tile title text displayed by default in lower left corner.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates tile title text displayed by default in lower left corner"), Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public string TitleText
        {
            get { return _Frames[0].TitleText; }
            set { _Frames[0].TitleText = value; }
        }
        /// <summary>
        /// Occurs when an hyperlink in title text markup is clicked.
        /// </summary>
        public event EventHandler TitleTextMarkupLinkClick;
        /// <summary>
        /// Raises TitleTextMarkupLinkClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnTitleTextMarkupLinkClick(EventArgs e)
        {
            EventHandler handler = TitleTextMarkupLinkClick;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Gets or sets the title text font.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Gets or sets the title text font.")]
        public Font TitleTextFont
        {
            get { return _Frames[0].TitleTextFont; }
            set { _Frames[0].TitleTextFont = value; }
        }

        /// <summary>
        /// Gets or sets the color of the title text.
        /// </summary>
        [Category("Columns"), Description("Indicates color of title text.")]
        public Color TitleTextColor
        {
            get { return _Frames[0].TitleTextColor; }
            set { _Frames[0].TitleTextColor = value; }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeTitleTextColor()
        {
            return _Frames[0].ShouldSerializeTitleTextColor();
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetTitleTextColor()
        {
            _Frames[0].ResetTitleTextColor();
        }

        /// <summary>
        /// Gets or sets title text alignment.
        /// </summary>
        [DefaultValue(ContentAlignment.BottomLeft), Category("Appearance"), Description("Indicates title text alignment.")]
        public ContentAlignment TitleTextAlignment
        {
            get { return _Frames[0].TitleTextAlignment; }
            set { _Frames[0].TitleTextAlignment = value; }
        }

        /// <summary>
        /// Gets or sets the top-left location of the image.
        /// </summary>
        [Category("Appearance"), Description("Indicates  top-left location of the image.")]
        public Point ImageIndent
        {
            get { return _Frames[0].ImageIndent; }
            set { _Frames[0].ImageIndent = value; }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeImageIndent()
        {
            return _Frames[0].ShouldSerializeImageIndent();
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetImageIndent()
        {
            _Frames[0].ResetImageIndent();
        }

        private bool _Checked = false;
        /// <summary>
        /// Gets or sets whether tile is checked.
        /// </summary>
        [DefaultValue(false), Category("Appearance"), Description("Indicates whether tile is checked.")]
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
            // Allow user to cancel the checking
            if (newValue && _OptionGroup.Length > 0 && this.Parent != null)
            {
                MetroTileItem b = null;
                foreach (BaseItem item in this.Parent.SubItems)
                {
                    if (item == this)
                        continue;
                    b = item as MetroTileItem;
                    if (b != null && b.OptionGroup == _OptionGroup && b.Checked)
                    {
                        break;
                    }
                }
                OptionGroupChangingEventArgs e = new OptionGroupChangingEventArgs(b, this);
                OnOptionGroupChanging(e);
                if (e.Cancel)
                {
                    _Checked = oldValue;
                    return;
                }
            }

            if (ShouldSyncProperties)
                BarFunctions.SyncProperty(this, "Checked");

            if (_OptionGroup != "" && newValue && this.Parent != null)
            {
                foreach (BaseItem item in this.Parent.SubItems)
                {
                    if (item == this)
                        continue;
                    MetroTileItem b = item as MetroTileItem;
                    if (b != null && b.OptionGroup == _OptionGroup && b.Checked)
                        b.Checked = false;
                }
            }

            //OnPropertyChanged(new PropertyChangedEventArgs("Checked"));
            this.Refresh();
            OnCheckedChanged(EventArgs.Empty);
        }

        private eMetroTileCheckBehavior _CheckBehavior = eMetroTileCheckBehavior.Inherit;
        /// <summary>
        /// Gets or sets the automatic check behavior of metro tile.
        /// </summary>
        [DefaultValue(eMetroTileCheckBehavior.Inherit), Category("Behavior"), Description("Indicates automatic check behavior of metro tile.")]
        public eMetroTileCheckBehavior CheckBehavior
        {
            get { return _CheckBehavior; }
            set
            {
                if (value != _CheckBehavior)
                {
                    eMetroTileCheckBehavior oldValue = _CheckBehavior;
                    _CheckBehavior = value;
                    OnCheckBehaviorChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when CheckBehavior property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnCheckBehaviorChanged(eMetroTileCheckBehavior oldValue, eMetroTileCheckBehavior newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("CheckBehavior"));
        }
        private eMetroTileCheckBehavior EffectiveCheckBehavior
        {
            get
            {
                if (_CheckBehavior == eMetroTileCheckBehavior.Inherit)
                {
                    Control parent = this.ContainerControl as Control;
                    if (parent is ItemPanel)
                        return eMetroTileCheckBehavior.RightMouseButtonClick;
                }
                return _CheckBehavior;
            }
        }

        private string _OptionGroup = "";
        /// <summary>
        /// Gets or set the Group item belongs to. The groups allows a user to choose from mutually exclusive options within the group. The choice is reflected by Checked property.
        /// </summary>
        [Browsable(true), Category("Behavior"), Description("Gets or set the Group item belongs to. The groups allows a user to choose from mutually exclusive options within the group."), System.ComponentModel.DefaultValue("")]
        public virtual string OptionGroup
        {
            get
            {
                return _OptionGroup;
            }
            set
            {
                if (_OptionGroup != value)
                {
                    _OptionGroup = value;
                    if (_OptionGroup != "" && _Checked && this.Parent != null)
                    {
                        foreach (BaseItem item in this.Parent.SubItems)
                        {
                            if (item == this)
                                continue;
                            MetroTileItem b = item as MetroTileItem;
                            if (b != null && b.OptionGroup == _OptionGroup && b.Checked)
                                this.Checked = false;
                        }
                    }
                    OnAppearanceChanged();
                    this.Refresh();
                }
            }
        }

        private CustomCollection<MetroTileFrame> _Frames = null;
        /// <summary>
        /// Gets the list of tile frames that are displayed when frame animation is enabled using AnimationEnabled property.
        /// </summary>
        [Category("Appearance"), Description("List of tile frames that are displayed when frame animation is enabled using AnimationEnabled property."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CustomCollection<MetroTileFrame> Frames
        {
            get { return _Frames; }
        }

        private int _CurrentFrame = 0;
        /// <summary>
        /// Gets or sets index of currently displayed frame in Frames collection.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), DefaultValue(0), Category("Appearance"), Description("Indicates index of currently displayed frame in Frames collection.")]
        public int CurrentFrame
        {
            get { return _CurrentFrame; }
            set
            {
                if (value != _CurrentFrame)
                {
                    if (value < 0 || value >= _Frames.Count)
                        throw new ArgumentException("Frame value is invalid. It must be greater or equal than zero and less than Frames.Count");
                    int oldValue = _CurrentFrame;
                    _CurrentFrame = value;
                    OnCurrentFrameChanged(oldValue, value);
                }
            }
        }

        private int _LastFrame = 0;
        /// <summary>
        /// Gets the index of last selected frame, i.e. before CurrentFrame was set with new value.
        /// </summary>
        internal int LastFrame
        {
            get
            {
                return _LastFrame;
            }
        }

        private Animation.Animation _CurrentAnimation = null;
        private void DisposeCurrentAnimation()
        {
            Animation.Animation anim = _CurrentAnimation;
            _CurrentAnimation = null;
            if (anim != null)
            {
                anim.Stop();
                anim.Dispose();
            }
        }
        private bool GetDesignMode()
        {
            if (this.DesignMode) return true;
            if(this.Site!=null && this.Site.DesignMode) return true;
            return false;
        }
        /// <summary>
        /// Called when CurrentFrame property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnCurrentFrameChanged(int oldValue, int newValue)
        {
            _LastFrame = oldValue;
            if (_AnimationEnabled && _AnimationDuration > 0 && !GetDesignMode()) // Animate transition from current frame to new frame
            {
                DisposeCurrentAnimation();
                int targetValue = _TileSize.Height;
                Animation.AnimationInt anim = new DevComponents.DotNetBar.Animation.AnimationInt(new Animation.AnimationRequest(this, "CurrentFrameOffset", targetValue, 0),
                    Animation.AnimationEasing.EaseOutExpo, _AnimationDuration);
                _CurrentAnimation = anim;
                anim.Start();
            }
            else
                this.Refresh();
            //OnPropertyChanged(new PropertyChangedEventArgs("CurrentFrame"));
        }

        private int _CurrentFrameOffset = 0;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public int CurrentFrameOffset
        {
            get { return _CurrentFrameOffset; }
            set
            {
                if (_CurrentFrameOffset != value)
                {
                    _CurrentFrameOffset = value;
                    this.Invalidate();
                    this.Update();
                }
            }
        }

        private bool _AnimationEnabled = true;
        /// <summary>
        /// Gets or sets whether Frame animation is enabled when CurrentFrame has changed. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether Frame animation is enabled when CurrentFrame has changed.")]
        public bool AnimationEnabled
        {
            get { return _AnimationEnabled; }
            set { _AnimationEnabled = value; }
        }

        private int _AnimationDuration = 800;
        /// <summary>
        /// Gets or sets the frames animation duration in milliseconds. Default value is 800.
        /// </summary>
        [DefaultValue(800), Category("Behavior"), Description("Indicates frames animation duration in milliseconds.")]
        public int AnimationDuration
        {
            get { return _AnimationDuration; }
            set
            {
                if (value < 0) value = 0;
                _AnimationDuration = value;
            }
        }

        private int _AutoRotateFramesInterval = 0;
        /// <summary>
        /// Gets or sets the automatic tile frame rotation interval in milliseconds. When set it will change the CurrentFrame property so each frame from Frames collection is displayed after interval set here.
        /// </summary>
        [DefaultValue(0), Category("Behavior"), Description("Indicates automatic tile frame rotation interval in milliseconds. When set it will change the CurrentFrame property so each frame from Frames collection is displayed after interval set here.")]
        public int AutoRotateFramesInterval
        {
            get { return _AutoRotateFramesInterval; }
            set
            {
                if (value < 0) value = 0;
                if (value != _AutoRotateFramesInterval)
                {
                    int oldValue = _AutoRotateFramesInterval;
                    _AutoRotateFramesInterval = value;
                    OnAutoRotateFramesIntervalChanged(oldValue, value);
                }
            }
        }
        private Timer _AutoRotateTimer = null;
        private void DestroyAutoRotateTimer()
        {
            Timer timer = _AutoRotateTimer;
            _AutoRotateTimer = null;
            if (timer != null)
            {
                timer.Stop();
                timer.Dispose();
            }
        }
        private void CreateAutoRotateTimer()
        {
            if (_AutoRotateTimer != null) return;
            _AutoRotateTimer = new Timer();
            _AutoRotateTimer.Interval = _AutoRotateFramesInterval;
            if (_Frames[_CurrentFrame].FrameDisplayDuration > 0)
                _AutoRotateTimer.Interval = _Frames[_CurrentFrame].FrameDisplayDuration;
            _AutoRotateTimer.Tick += new EventHandler(AutoRotateTimerTick);
            _AutoRotateTimer.Start();
        }

        private void AutoRotateTimerTick(object sender, EventArgs e)
        {
            if (this.Frames.Count <= 1) return;
            int frame = this.CurrentFrame + 1;
            if (frame >= this.Frames.Count)
                frame = 0;
            this.CurrentFrame = frame;
            if (_Frames[frame].FrameDisplayDuration > 0)
                _AutoRotateTimer.Interval = _Frames[frame].FrameDisplayDuration;
            else
                _AutoRotateTimer.Interval = _AutoRotateFramesInterval;
        }
        /// <summary>
        /// Called when AutoRotateFramesInterval property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnAutoRotateFramesIntervalChanged(int oldValue, int newValue)
        {
            if (newValue == 0 || GetDesignMode())
                DestroyAutoRotateTimer();
            else if (this.Visible)
                CreateAutoRotateTimer();
            //OnPropertyChanged(new PropertyChangedEventArgs("AutoRotateFramesInterval"));
        }

        protected internal override void OnVisibleChanged(bool newValue)
        {
            if (newValue && !GetDesignMode())
            {
                if (_AutoRotateFramesInterval > 0 && _Frames.Count > 1 && _AutoRotateTimer == null)
                    CreateAutoRotateTimer();
            }
            else if (_AutoRotateTimer != null)
                DestroyAutoRotateTimer();

            base.OnVisibleChanged(newValue);
        }

        /// <summary>
        /// Called when DragStartPoint property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected override void OnDragStartPointChanged(Point oldValue, Point newValue)
        {
            if (newValue.IsEmpty)
            {
                if (_AutoRotateTimer != null)
                    _AutoRotateTimer.Enabled = true;
            }
            else
            {
                if (_AutoRotateTimer != null)
                    _AutoRotateTimer.Enabled = false;
            }
            base.OnDragStartPointChanged(oldValue, newValue);
        }

        private static readonly Color DefaultDisabledBackColor = Color.Empty;
        private Color _DisabledBackColor = DefaultDisabledBackColor;
        /// <summary>
        /// Gets or sets tile background color when Enabled=false.
        /// </summary>
        [Category("Columns"), Description("Indicates tile background color when Enabled=false.")]
        public  Color DisabledBackColor
        {
        	get {	return _DisabledBackColor; }
        	set { _DisabledBackColor = value;	}
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeDisabledBackColor()
        {
            return _DisabledBackColor != DefaultDisabledBackColor;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetDisabledBackColor()
        {
            this.DisabledBackColor =  DefaultDisabledBackColor;
        }
        #endregion

        #region Markup Implementation
        /// <summary>
        /// Gets whether item supports text markup. Default is false.
        /// </summary>
        protected override bool IsMarkupSupported
        {
            get { return _EnableMarkup; }
        }

        private bool _EnableMarkup = true;
        /// <summary>
        /// Gets or sets whether text-markup support is enabled for items Text property. Default value is true.
        /// Set this property to false to display HTML or other markup in the item instead of it being parsed as text-markup.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether text-markup support is enabled for items Text property.")]
        public bool EnableMarkup
        {
            get { return _EnableMarkup; }
            set
            {
                if (_EnableMarkup != value)
                {
                    _EnableMarkup = value;
                    NeedRecalcSize = true;
                    OnTextChanged();
                    OnAppearanceChanged();
                }
            }
        }

        #endregion
    }

    #region eMetroTileColor
    public enum eMetroTileColor
    {
        Default,
        Green,
        Orange,
        Magenta,
        Blue,
        Teal,
        Plum,
        Coffee,
        RedOrange,
        RedViolet,
        Olive,
        DarkOlive,
        Rust,
        Maroon,
        Yellowish,
        Blueish,
        DarkBlue,
        Yellow,
        Gray,
        DarkGreen,
        MaroonWashed,
        PlumWashed,
        Azure
    }
    #endregion

    #region eMetroTileCheckBehavior
    /// <summary>
    /// Specifies how MetroTileItem is checked.
    /// </summary>
    public enum eMetroTileCheckBehavior
    {
        /// <summary>
        /// Metro tile item inherits the check behavior from host control.
        /// </summary>
        Inherit,
        /// <summary>
        /// Metro tile item cannot be checked.
        /// </summary>
        None,
        /// <summary>
        /// Metro tile item is checked using right mouse button.
        /// </summary>
        RightMouseButtonClick,
        /// <summary>
        /// Metro tile item is checked using middle mouse button.
        /// </summary>
        MiddleMouseButtonClick,
        /// <summary>
        /// Metro tile item is checked using left mouse button.
        /// </summary>
        LeftMouseButtonClick
    }
    #endregion

    #region MetroTileFrame
    /// <summary>
    /// Defines single frame for metro-tile item.
    /// </summary>
    [DesignTimeVisible(false), ToolboxItem(false), Designer(typeof(DevComponents.DotNetBar.Design.MetroTileFrameDesigner))]
    public class MetroTileFrame : Component, ICloneable, INotifyPropertyChanged
    {
        /// <summary>
        /// Initializes a new instance of the MetroTileFrame class.
        /// </summary>
        public MetroTileFrame()
        {
            _TileStyle = new ElementStyle();
            UpdateEffectiveStyle();
            _TileStyle.StyleChanged += TileStyleChanged;
        }
        private void TileStyleChanged(object sender, EventArgs e)
        {
            UpdateEffectiveStyle();
            OnPropertyChanged(new PropertyChangedEventArgs("TileStyle"));
        }

        private string _TitleText = "";
        /// <summary>
        /// Gets or sets the tile title text displayed by default in lower left corner.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates tile title text displayed by default in lower left corner"), Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public string TitleText
        {
            get { return _TitleText; }
            set
            {
                if (value != _TitleText)
                {
                    string oldValue = _TitleText;
                    _TitleText = value;
                    OnTitleTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TitleText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTitleTextChanged(string oldValue, string newValue)
        {
            TitleTextMarkupUpdate();
            OnPropertyChanged(new PropertyChangedEventArgs("TitleText"));
        }
        /// <summary>
        /// Gets reference to parsed markup body element if text was markup otherwise returns null.
        /// </summary>
        internal TextMarkup.BodyElement TitleTextMarkupBody
        {
            get { return _TitleTextMarkup; }
        }
        private TextMarkup.BodyElement _TitleTextMarkup = null;
        private void TitleTextMarkupUpdate()
        {
            if (_TitleTextMarkup != null)
                _TitleTextMarkup.HyperLinkClick -= TitleTextMarkupLinkClicked;
            _TitleTextMarkup = null;

            if (!_TextMarkupEnabled)
                return;

            if (!TextMarkup.MarkupParser.IsMarkup(ref _TitleText))
                return;

            _TitleTextMarkup = TextMarkup.MarkupParser.Parse(_TitleText);

            if (_TitleTextMarkup != null)
                _TitleTextMarkup.HyperLinkClick += TitleTextMarkupLinkClicked;
        }
        private void TitleTextMarkupLinkClicked(object sender, EventArgs e)
        {
            DevComponents.DotNetBar.TextMarkup.HyperLink link = sender as DevComponents.DotNetBar.TextMarkup.HyperLink;

            if (link != null)
                OnTitleTextMarkupLinkClick(new MarkupLinkClickEventArgs(link.Name, link.HRef));
            else
               OnTitleTextMarkupLinkClick(e);
        }
        /// <summary>
        /// Occurs when an hyperlink in title text markup is clicked.
        /// </summary>
        public event EventHandler TitleTextMarkupLinkClick;
        /// <summary>
        /// Raises TitleTextMarkupLinkClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnTitleTextMarkupLinkClick(EventArgs e)
        {
            EventHandler handler = TitleTextMarkupLinkClick;
            if (handler != null)
                handler(this, e);
        }


        private Font _TitleTextFont = null;
        /// <summary>
        /// Gets or sets the title text font.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Gets or sets the title text font.")]
        public Font TitleTextFont
        {
            get { return _TitleTextFont; }
            set
            {
                if (value != _TitleTextFont)
                {
                    Font oldValue = _TitleTextFont;
                    _TitleTextFont = value;
                    OnTitleTextFontChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TitleTextFont property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTitleTextFontChanged(Font oldValue, Font newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("TitleTextFont"));
        }
        private Rectangle _TitleTextBounds = Rectangle.Empty;
        internal Rectangle TitleTextBounds
        {
            get
            {
                return _TitleTextBounds;
            }
            set
            {
                _TitleTextBounds = value;
            }
        }

        private Color _TitleTextColor = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the title text.
        /// </summary>
        [Category("Columns"), Description("Indicates color of title text.")]
        public Color TitleTextColor
        {
            get { return _TitleTextColor; }
            set
            {
                if (_TitleTextColor != value)
                {
                    Color oldValue = _TitleTextColor;
                    _TitleTextColor = value;
                    OnTitleTextColorChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TitleTextColor property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTitleTextColorChanged(Color oldValue, Color newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("TitleTextColor"));
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeTitleTextColor()
        {
            return !_TitleTextColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetTitleTextColor()
        {
            this.TitleTextColor = Color.Empty;
        }

        private ContentAlignment _TitleTextAlignment = ContentAlignment.BottomLeft;
        /// <summary>
        /// Gets or sets title text alignment.
        /// </summary>
        [DefaultValue(ContentAlignment.BottomLeft), Category("Appearance"), Description("Indicates title text alignment.")]
        public ContentAlignment TitleTextAlignment
        {
            get { return _TitleTextAlignment; }
            set
            {
                if (value != _TitleTextAlignment)
                {
                    ContentAlignment oldValue = _TitleTextAlignment;
                    _TitleTextAlignment = value;
                    OnTitleTextAlignmentChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TitleTextAlignment property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTitleTextAlignmentChanged(ContentAlignment oldValue, ContentAlignment newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("TitleTextAlignment"));
        }

        /// <summary>
        /// Gets the effective style for the tile when TileColor property is set to predefined tile color.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ElementStyle EffectiveStyle
        {
            get
            {
                return _EffectiveStyle;
            }
        }

        private ElementStyle _TileStyle = null;
        /// <summary>
        /// Specifies the Tile style of the item.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), Category("Style"), Description("Gets or sets Tile style of the item."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ElementStyle TileStyle
        {
            get { return _TileStyle; }
        }

        private string _Symbol = "";
        /// <summary>
        /// Indicates the symbol displayed on face of the tile instead of the image. Setting the symbol overrides the image setting.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates the symbol displayed on face of the tile instead of the image. Setting the symbol overrides the image setting.")]
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
            OnPropertyChanged(new PropertyChangedEventArgs("Symbol"));
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
            OnPropertyChanged(new PropertyChangedEventArgs("SymbolSize"));
        }

        private Color _SymbolColor = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the symbol.
        /// </summary>
        [Category("Columns"), Description("Indicates color of the symbol.")]
        public Color SymbolColor
        {
            get { return _SymbolColor; }
            set { _SymbolColor = value; OnPropertyChanged(new PropertyChangedEventArgs("SymbolColor")); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeSymbolColor()
        {
            return !_SymbolColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetSymbolColor()
        {
            this.SymbolColor = Color.Empty;
        }

        private Image _Image = null;
        /// <summary>
        /// Gets or sets the image displayed on the tile.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Indicates image displayed on the tile.")]
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
            OnPropertyChanged(new PropertyChangedEventArgs("Image"));
        }

        private static readonly Point DefaultImageIndent = new Point(2, 2);
        private Point _ImageIndent = DefaultImageIndent;
        /// <summary>
        /// Gets or sets the top-left location of the image.
        /// </summary>
        [Category("Appearance"), Description("Indicates  top-left location of the image.")]
        public Point ImageIndent
        {
            get { return _ImageIndent; }
            set
            {
                if (value != _ImageIndent)
                {
                    Point oldValue = _ImageIndent;
                    _ImageIndent = value;
                    OnImageIndentChanged(oldValue, value);
                }
            }
        }
        protected virtual void OnImageIndentChanged(Point oldValue, Point newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("ImageIndent"));
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeImageIndent()
        {
            return _ImageIndent != DefaultImageIndent;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetImageIndent()
        {
            this.ImageIndent = DefaultImageIndent;
        }

        private string _Text = "";
        /// <summary>
        /// Gets or sets the text associated with this item.
        /// </summary>
        [Browsable(true), Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor)), System.ComponentModel.Category("Appearance"), Description("The text contained in the item."), Localizable(true), DefaultValue("")]
        public string Text
        {
            get { return _Text; }
            set
            {
                if (value != _Text)
                {
                    string oldValue = _Text;
                    _Text = value;
                    OnTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Text property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTextChanged(string oldValue, string newValue)
        {
            MarkupTextChanged();
            OnPropertyChanged(new PropertyChangedEventArgs("Text"));
        }

        private eMetroTileColor _TileColor = eMetroTileColor.Default;
        /// <summary>
        /// Gets or sets the predefined tile color.
        /// </summary>
        [Category("Appearance"), Description("Indicates predefined tile color.")]
        public eMetroTileColor TileColor
        {
            get { return _TileColor; }
            set
            {
                if (value != _TileColor)
                {
                    eMetroTileColor oldValue = _TileColor;
                    _TileColor = value;
                    OnTileColorChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Color property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTileColorChanged(eMetroTileColor oldValue, eMetroTileColor newValue)
        {
            UpdateEffectiveStyle();
            OnPropertyChanged(new PropertyChangedEventArgs("TileColor"));
        }

        private ElementStyle _EffectiveStyle = new ElementStyle();
        private void UpdateEffectiveStyle()
        {
            ElementStyle style = GetPredefinedTileStyle(_TileColor);
            style.ApplyStyle(_TileStyle);
            _EffectiveStyle = style;
        }

        private ElementStyle GetPredefinedTileStyle(eMetroTileColor tileColor)
        {
            ElementStyle style = new ElementStyle();
            style.TextColor = System.Drawing.Color.White;
            style.PaddingTop = 4;
            style.PaddingRight = 4;
            style.PaddingBottom = 4;
            style.PaddingLeft = 4;

            if (tileColor == eMetroTileColor.Default)
            {
                style.BackColor = ColorScheme.GetColor(0x245375);
                style.BackColor2 = ColorScheme.GetColor(0x30679B);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Blue)
            {
                style.BackColor = ColorScheme.GetColor(0x4C66A8);
                style.BackColor2 = ColorScheme.GetColor(0x5B78BE);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Green)
            {
                style.BackColor = ColorScheme.GetColor(0x54AF0E);
                style.BackColor2 = ColorScheme.GetColor(0x68C210);
                style.BackColorGradientAngle = 45;
                style.Border = eStyleBorderType.Solid;
                style.BorderColor = ColorScheme.GetColor(0x65B727);
                style.BorderWidth = 1;
            }
            else if (tileColor == eMetroTileColor.Magenta)
            {
                style.BackColor = ColorScheme.GetColor(0x765594);
                style.BackColor2 = ColorScheme.GetColor(0x8562B9);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Orange)
            {
                style.BackColor = ColorScheme.GetColor(0xDF8300);
                style.BackColor2 = ColorScheme.GetColor(0xE88800);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Plum)
            {
                style.BackColor = ColorScheme.GetColor(0x6D3453);
                style.BackColor2 = ColorScheme.GetColor(0x884371);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Teal)
            {
                style.BackColor = ColorScheme.GetColor(0x45A98E);
                style.BackColor2 = ColorScheme.GetColor(0x50BB9E);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Coffee)
            {
                style.BackColor = ColorScheme.GetColor(0x734C29);
                style.BackColor2 = ColorScheme.GetColor(0x664325);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.RedOrange)
            {
                style.BackColor = ColorScheme.GetColor(0xE65E20);
                style.BackColor2 = ColorScheme.GetColor(0xEB7427);
                style.BackColorGradientAngle = 45;
                style.Border = eStyleBorderType.Solid;
                style.BorderColor = ColorScheme.GetColor(0xED823D);
                style.BorderWidth = 1;
            }
            else if (tileColor == eMetroTileColor.RedViolet)
            {
                style.BackColor = ColorScheme.GetColor(0x730046);
                style.BackColor2 = ColorScheme.GetColor(0x66003D);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Olive)
            {
                style.BackColor = ColorScheme.GetColor(0xBFBB11);
                style.BackColor2 = ColorScheme.GetColor(0xB2B010);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.DarkOlive)
            {
                style.BackColor = ColorScheme.GetColor(0x787860);
                style.BackColor2 = ColorScheme.GetColor(0x6B6B56);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Rust)
            {
                style.BackColor = ColorScheme.GetColor(0x301818);
                style.BackColor2 = ColorScheme.GetColor(0x1F0F0F);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Maroon)
            {
                style.BackColor = ColorScheme.GetColor(0x603030);
                style.BackColor2 = ColorScheme.GetColor(0x4F2828);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Yellowish)
            {
                style.BackColor = ColorScheme.GetColor(0xF2B705);
                style.BackColor2 = ColorScheme.GetColor(0xE0A904);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Blueish)
            {
                style.BackColor = ColorScheme.GetColor(0x03658C);
                style.BackColor2 = ColorScheme.GetColor(0x02587A);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.DarkBlue)
            {
                style.BackColor = ColorScheme.GetColor(0x034079);
                style.BackColor2 = ColorScheme.GetColor(0x034F96);
                style.BackColorGradientAngle = 45;
                style.Border = eStyleBorderType.Solid;
                style.BorderColor = ColorScheme.GetColor(0x1D5C96);
                style.BorderWidth = 1;
            }
            else if (tileColor == eMetroTileColor.Yellow)
            {
                style.BackColor = ColorScheme.GetColor(0xFFCA08);
                style.BackColor2 = ColorScheme.GetColor(0xFFCA08);
                style.BackColorGradientAngle = 45;
                style.TextColor = Color.Black;
            }
            else if (tileColor == eMetroTileColor.Gray)
            {
                style.BackColor = ColorScheme.GetColor(0x7B7F7E);
                style.BackColor2 = ColorScheme.GetColor(0x5E6160);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.DarkGreen)
            {
                style.BackColor = ColorScheme.GetColor(0x17934B);
                style.BackColor2 = ColorScheme.GetColor(0x1BAD5C);
                style.BackColorGradientAngle = 45;
                style.Border = eStyleBorderType.Solid;
                style.BorderColor = ColorScheme.GetColor(0x32B56D);
                style.BorderWidth = 1;
            }
            else if (tileColor == eMetroTileColor.MaroonWashed)
            {
                style.BackColor = ColorScheme.GetColor(0x965155);
                style.BackColor2 = ColorScheme.GetColor(0x8A4A4E);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.PlumWashed)
            {
                style.BackColor = ColorScheme.GetColor(0x40374C);
                style.BackColor2 = ColorScheme.GetColor(0x362E40);
                style.BackColorGradientAngle = 45;
            }
            else if (tileColor == eMetroTileColor.Azure)
            {
                style.BackColor = ColorScheme.GetColor(0x00ABDC);
                style.BackColor2 = ColorScheme.GetColor(0x00BFE3);
                style.BackColorGradientAngle = 45;
                style.Border = eStyleBorderType.Solid;
                style.BorderColor = ColorScheme.GetColor(0x1AC6E6);
                style.BorderWidth = 1;
            }

            return style;
        }

        private int _FrameDisplayDuration = 0;
        /// <summary>
        /// Gets or sets the frame display duration in milliseconds during metro-tile frame animation. When not set then each frame will stay visible for duration set on MetroTileItem.AutoRotateFramesInterval.
        /// </summary>
        [DefaultValue(0), Category("Behavior"), Description("Indicates frame display duration in milliseconds during metro-tile frame animation. When not set then each frame will stay visible for duration set on MetroTileItem.AutoRotateFramesInterval.")]
        public int FrameDisplayDuration
        {
            get { return _FrameDisplayDuration; }
            set
            {
                if (value != _FrameDisplayDuration)
                {
                    int oldValue = _FrameDisplayDuration;
                    _FrameDisplayDuration = value;
                    OnFrameDisplayDurationChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when FrameDisplayDuration property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnFrameDisplayDurationChanged(int oldValue, int newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("FrameDisplayDuration"));
        }

        #region Markup Implementation
        private TextMarkup.BodyElement _TextMarkup = null;

        private void MarkupTextChanged()
        {
            if (_TextMarkup != null)
                _TextMarkup.HyperLinkClick -= TextMarkupLinkClick;

            _TextMarkup = null;

            if (!_TextMarkupEnabled)
                return;

            if (!TextMarkup.MarkupParser.IsMarkup(ref _Text))
                return;

            _TextMarkup = TextMarkup.MarkupParser.Parse(_Text);

            if (_TextMarkup != null)
                _TextMarkup.HyperLinkClick += TextMarkupLinkClick;
        }

        /// <summary>
        /// Occurs when text markup link is clicked.
        /// </summary>
        protected virtual void TextMarkupLinkClick(object sender, EventArgs e)
        {
            TextMarkup.HyperLink link = sender as TextMarkup.HyperLink;
            if (link != null)
                OnMarkupLinkClick(new MarkupLinkClickEventArgs(link.Name, link.HRef));
        }
        /// <summary>
        /// Occurs when text markup link is clicked. Markup links can be created using "a" tag, for example:
        /// <a name="MyLink">Markup link</a>
        /// </summary>
        [Description("Occurs when text markup link is clicked. Markup links can be created using a tag.")]
        public event MarkupLinkClickEventHandler MarkupLinkClick;
        /// <summary>
        /// Raises MarkupLinkClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnMarkupLinkClick(MarkupLinkClickEventArgs e)
        {
            MarkupLinkClickEventHandler handler = MarkupLinkClick;
            if (handler != null)
                handler(this, e);
        }

        /// <summary>
        /// Gets reference to parsed markup body element if text was markup otherwise returns null.
        /// </summary>
        internal TextMarkup.BodyElement TextMarkupBody
        {
            get { return _TextMarkup; }
        }

        private bool _TextMarkupEnabled = true;
        /// <summary>
        /// Gets or sets whether text-markup can be used in Text property.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether text-markup can be used in Text property.")]
        public bool TextMarkupEnabled
        {
            get { return _TextMarkupEnabled; }
            set
            {
                if (value != _TextMarkupEnabled)
                {
                    bool oldValue = _TextMarkupEnabled;
                    _TextMarkupEnabled = value;
                    OnTextMarkupEnabledChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when TextMarkupEnabled property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTextMarkupEnabledChanged(bool oldValue, bool newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("TextMarkupEnabled"));
        }

        internal static Size MeasureText(MetroTileFrame item, Graphics g, int containerWidth, Font font, eTextFormat stringFormat, bool rightToLeft)
        {
            if (item.Text == "" && item.TextMarkupBody == null) return Size.Empty;

            Size textSize = Size.Empty;

            if (item.TextMarkupBody == null)
            {
                textSize = TextDrawing.MeasureString(g, ButtonItemPainter.GetDrawText(item.Text), font, containerWidth, stringFormat);
            }
            else
            {
                Size availSize = new Size(containerWidth, 1);
                if (containerWidth == 0)
                    availSize.Width = 1600;
                TextMarkup.MarkupDrawContext d = new TextMarkup.MarkupDrawContext(g, font, Color.Empty, false);
                item.TextMarkupBody.Measure(availSize, d);
                availSize = item.TextMarkupBody.Bounds.Size;
                if (containerWidth != 0)
                    availSize.Width = containerWidth;
                d.RightToLeft = rightToLeft;
                item.TextMarkupBody.Arrange(new Rectangle(0, 0, availSize.Width, availSize.Height), d);

                textSize = item.TextMarkupBody.Bounds.Size;
            }

            return textSize;
        }

        private ContentAlignment _ImageTextAlignment = ContentAlignment.TopLeft;
        /// <summary>
        /// Gets or sets image alignment in relation to text.
        /// </summary>
        [DefaultValue(ContentAlignment.TopLeft), Category("Appearance"), Description("Indicates the image alignment in relation to text.")]
        public ContentAlignment ImageTextAlignment
        {
            get { return _ImageTextAlignment; }
            set
            {
                if (value != _ImageTextAlignment)
                {
                    ContentAlignment oldValue = _ImageTextAlignment;
                    _ImageTextAlignment = value;
                    OnImageTextAlignmentChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when ImageTextAlignment property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnImageTextAlignmentChanged(ContentAlignment oldValue, ContentAlignment newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("ImageTextAlignment"));
        }
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
        /// <summary>
        /// Occurs when property on BindingDef object has changed.
        /// </summary>
        [Description("Occurs when property on BindingDef object has changed.Occurs when property on BindingDef object has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region IDisposable Members
        protected override void Dispose(bool disposing)
        {
            if (disposing)
                _TileStyle.StyleChanged -= TileStyleChanged;
            base.Dispose(disposing);
        }
        #endregion

        #region ICloneable Members

        public object Clone()
        {
            MetroTileFrame frame = new MetroTileFrame();
            if (this.Image != null)
                frame.Image = (Image)this.Image.Clone();
            frame.ImageIndent = this.ImageIndent;
            frame.Text = this.Text;
            frame.TextMarkupEnabled = this.TextMarkupEnabled;
            //frame.TileStyle.ApplyFontStyle(this.TileStyle);
            frame.TileStyle.ApplyStyle(this.TileStyle);
            frame.TileColor = this.TileColor;
            frame.TitleText = this.TitleText;
            frame.TitleTextAlignment = this.TitleTextAlignment;
            frame.TitleTextColor = this.TitleTextColor;
            if (this.TitleTextFont != null)
                frame.TitleTextFont = (Font)this.TitleTextFont.Clone();
            frame.ImageTextAlignment = this.ImageTextAlignment;
            frame.FrameDisplayDuration = this.FrameDisplayDuration;
            frame.Symbol = this.Symbol;
            frame.SymbolSize = this.SymbolSize;
            return frame;
        }

        #endregion
    }
    #endregion
}
