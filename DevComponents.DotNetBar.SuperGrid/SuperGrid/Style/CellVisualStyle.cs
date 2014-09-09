using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// CellVisualStyles
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class CellVisualStyles : VisualStyles<CellVisualStyle>
    {
    }

    /// <summary>
    /// Represents the visual style of an element.
    /// </summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class CellVisualStyle : VisualStyle
    {
        #region Private variables

        private Tbool _AllowWrap = Tbool.NotSet;
        private Alignment _Alignment = Alignment.NotSet;

        private Image _Image;
        private int _ImageIndex = -1;
        private Padding _ImagePadding;
        private Alignment _ImageAlignment = Alignment.NotSet;
        private ImageHighlightMode _ImageHighlightMode = ImageHighlightMode.NotSet;
        private ImageOverlay _ImageOverlay = ImageOverlay.NotSet;

        #endregion

        #region Public properties

        #region Alignment

        /// <summary>
        /// Gets or sets the alignment of the content within the cell
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the alignment of the content within the cell.")]
        public Alignment Alignment
        {
            get { return (_Alignment); }

            set
            {
                if (_Alignment != value)
                {
                    _Alignment = value;

                    OnPropertyChangedEx("Alignment", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region AllowWrap

        /// <summary>
        /// Gets or sets whether text wrapping is permitted
        /// </summary>
        [DefaultValue(Tbool.NotSet), Category("Appearance")]
        [Description("Indicates whether text wrapping is permitted.")]
        public Tbool AllowWrap
        {
            get { return (_AllowWrap); }

            set
            {
                if (_AllowWrap != value)
                {
                    _AllowWrap = value;

                    OnPropertyChangedEx("AllowWrap", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region Image

        /// <summary>
        /// Gets or sets the element Image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the element image")]
        public Image Image
        {
            get { return (_Image); }

            set
            {
                if (_Image != value)
                {
                    _Image = value;

                    OnPropertyChangedEx("Image", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ImageAlignment

        /// <summary>
        /// Gets or sets the alignment of the Image within the cell
        /// </summary>
        [DefaultValue(Alignment.NotSet), Category("Appearance")]
        [Description("Indicates the alignment of the Image within the cell.")]
        public Alignment ImageAlignment
        {
            get { return (_ImageAlignment); }

            set
            {
                if (_ImageAlignment != value)
                {
                    _ImageAlignment = value;

                    OnPropertyChangedEx("ImageAlignment", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ImageHighlightMode

        /// <summary>
        /// Gets or sets how cell images are
        /// highlighted when the cell is selected
        /// </summary>
        [DefaultValue(ImageHighlightMode.NotSet), Category("Style")]
        [Description("Indicates how cell images are highlighted when the cell is selected")]
        public ImageHighlightMode ImageHighlightMode
        {
            get { return (_ImageHighlightMode); }

            set
            {
                if (value != _ImageHighlightMode)
                {
                    _ImageHighlightMode = value;

                    OnPropertyChangedEx("ImageHighlightMode", VisualChangeType.Render);
                }
            }
        }

        #endregion

        #region ImageIndex

        /// <summary>
        /// Gets or sets the image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the image index")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int ImageIndex
        {
            get { return (_ImageIndex); }

            set
            {
                if (_ImageIndex != value)
                {
                    _ImageIndex = value;

                    OnPropertyChangedEx("ImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetImageIndex()
        {
            ImageIndex = -1;
        }

        #endregion

        #region ImageOverlay

        /// <summary>
        /// Gets or sets how to overlay the cell image with respect to cell content
        /// </summary>
        [DefaultValue(ImageOverlay.NotSet), Category("Appearance")]
        [Description("Indicates how to overlay the cell image with respect to cell content.")]
        public ImageOverlay ImageOverlay
        {
            get { return (_ImageOverlay); }

            set
            {
                if (_ImageOverlay != value)
                {
                    _ImageOverlay = value;

                    OnPropertyChangedEx("ImageOverlay", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ImagePadding

        /// <summary>
        /// Gets or sets the spacing between content and edges of the Image
        /// </summary>
        [Description("Indicates the spacing between content and edges of the Image")]
        public Padding ImagePadding
        {
            get
            {
                if (_ImagePadding == null)
                {
                    _ImagePadding = Padding.Empty;

                    UpdateChangeHandler(null, _ImagePadding);
                }

                return (_ImagePadding);
            }

            set
            {
                if (_ImagePadding != value)
                {
                    UpdateChangeHandler(_ImagePadding, value);

                    _ImagePadding = value;

                    OnPropertyChangedEx("ImagePadding", VisualChangeType.Layout);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeImagePadding()
        {
            return (_ImagePadding != null && _ImagePadding.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetImagePadding()
        {
            ImagePadding = null;
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Gets whether the style is logically Empty.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gets whether the style is logically Empty.")]
        public override bool IsEmpty
        {
            get
            {
                return ((_Alignment == Alignment.NotSet) &&
                    (_AllowWrap == Tbool.NotSet) &&
                    (_Image == null) &&
                    (_ImageAlignment == Alignment.NotSet) &&
                    (_ImageHighlightMode == ImageHighlightMode.NotSet) &&
                    (_ImageIndex == -1) &&
                    (_ImageOverlay == ImageOverlay.NotSet) &&
                    (_ImagePadding == null || _ImagePadding.IsEmpty == true) &&

                    (base.IsEmpty == true));
            }
        }

        #endregion

        #endregion

        #region Internal properties

        #region IsOverlayImage

        internal bool IsOverlayImage
        {
            get
            {
                return (_ImageOverlay == ImageOverlay.Top ||
                    _ImageOverlay == ImageOverlay.Bottom);
            }
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(CellVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style.Alignment != Alignment.NotSet)
                    _Alignment = style.Alignment;

                if (style.AllowWrap != Tbool.NotSet)
                    _AllowWrap = style.AllowWrap;

                if (style.ImageIndex >= 0)
                {
                    _Image = null;
                    _ImageIndex = style.ImageIndex;
                }

                if (style.Image != null)
                {
                    _Image = style.Image;
                    _ImageIndex = -1;
                }

                if (style.ImageAlignment != Alignment.NotSet)
                    _ImageAlignment = style.ImageAlignment;

                if (style.ImageHighlightMode != ImageHighlightMode.NotSet)
                    _ImageHighlightMode = style.ImageHighlightMode;

                if (style._ImagePadding != null && style._ImagePadding.IsEmpty == false)
                    _ImagePadding = style._ImagePadding.Copy();

                if (style.ImageOverlay != ImageOverlay.NotSet)
                    _ImageOverlay = style.ImageOverlay;
            }
        }

        #endregion

        #region GetTextFormatFlags

        internal eTextFormat GetTextFormatFlags()
        {
            eTextFormat tf = eTextFormat.WordEllipsis |
                eTextFormat.NoPadding | eTextFormat.NoPrefix;

            if (AllowWrap == Tbool.True)
                tf |= eTextFormat.WordBreak;

            switch (Alignment)
            {
                case Alignment.TopCenter:
                    tf |= eTextFormat.HorizontalCenter;
                    break;

                case Alignment.TopRight:
                    tf |= eTextFormat.Right;
                    break;

                case Alignment.MiddleLeft:
                    tf |= eTextFormat.VerticalCenter;
                    break;

                case Alignment.MiddleCenter:
                    tf |= eTextFormat.HorizontalCenter;
                    tf |= eTextFormat.VerticalCenter;
                    break;

                case Alignment.MiddleRight:
                    tf |= eTextFormat.Right;
                    tf |= eTextFormat.VerticalCenter;
                    break;

                case Alignment.BottomLeft:
                    tf |= eTextFormat.Bottom;
                    break;

                case Alignment.BottomCenter:
                    tf |= eTextFormat.Bottom;
                    tf |= eTextFormat.HorizontalCenter;
                    break;

                case Alignment.BottomRight:
                    tf |= eTextFormat.Bottom;
                    tf |= eTextFormat.Right;
                    break;
            }

            return (tf);
        }

        #endregion

        #region GetImageBounds

        internal Rectangle GetImageBounds(Image image, Rectangle r)
        {
            if (image != null)
            {
                Size size = GetImageSize(image);

                bool overlay = IsOverlayImage;

                switch (ImageAlignment)
                {
                    case Alignment.TopLeft:
                        if (overlay == false)
                            r.X -= size.Width;
                        break;

                    case Alignment.NotSet:
                    case Alignment.MiddleLeft:
                        if (overlay == false)
                            r.X -= size.Width;

                        r.Y += (r.Height - size.Height)/2;
                        break;

                    case Alignment.BottomLeft:
                        if (overlay == false)
                            r.X -= size.Width;

                        r.Y = r.Bottom - size.Height;
                        break;

                    case Alignment.TopCenter:
                        r.X += (r.Width - size.Width)/2;

                        if (overlay == false)
                            r.Y -= size.Height;
                        break;

                    case Alignment.MiddleCenter:
                        r.X += (r.Width - size.Width)/2;
                        r.Y += (r.Height - size.Height)/2;
                        break;

                    case Alignment.BottomCenter:
                        r.X += (r.Width - size.Width)/2;
                        r.Y = r.Bottom;

                        if (overlay == true)
                            r.Y -= size.Height;
                        break;

                    case Alignment.TopRight:
                        r.X = r.Right;

                        if (overlay == true)
                            r.X -= size.Width;
                        break;

                    case Alignment.MiddleRight:
                        r.X = r.Right;

                        if (overlay == true)
                            r.X -= size.Width;

                        r.Y += (r.Height - size.Height)/2;

                        break;

                    case Alignment.BottomRight:
                        r.X = r.Right;
                        r.Y = r.Bottom - size.Height;

                        if (overlay == true)
                            r.X -= size.Width;
                        break;
                }

                r.Size = image.Size;

                r.X += ImagePadding.Left;
                r.Y += ImagePadding.Top;

                return (r);
            }

            return (Rectangle.Empty);
        }

        #endregion

        #region GetNonImageBounds

        internal Rectangle GetNonImageBounds(Image image, Rectangle r)
        {
            if (image != null)
            {
                if (IsOverlayImage == false)
                {
                    Size size = GetImageSize(image);

                    switch (ImageAlignment)
                    {
                        case Alignment.TopCenter:
                            r.Y += size.Height;
                            r.Height -= size.Height;
                            break;

                        case Alignment.MiddleCenter:
                            break;

                        case Alignment.BottomCenter:
                            r.Height -= size.Height;
                            break;

                        case Alignment.TopRight:
                        case Alignment.MiddleRight:
                        case Alignment.BottomRight:
                            r.Width -= size.Width;
                            break;

                        default:
                            r.X += size.Width;
                            r.Width -= size.Width;
                            break;
                    }
                }
            }

            return (r);
        }

        #endregion

        #region GetImage

        internal Image GetImage(GridPanel panel)
        {
            if (_Image != null)
                return (_Image);

            if (_ImageIndex >= 0 && panel != null)
            {
                ImageList imageList = panel.ImageList;

                if (imageList != null && _ImageIndex < imageList.Images.Count)
                    return (imageList.Images[_ImageIndex]);
            }

            return (null);
        }

        #endregion

        #region GetImageSize

        internal Size GetImageSize(Image image)
        {
            Size size = Size.Empty;

            if (image != null)
            {
                size.Width = (image.Width + ImagePadding.Horizontal);
                size.Height = (image.Height + ImagePadding.Vertical);
            }

            return (size);
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new CellVisualStyle Copy()
        {
            CellVisualStyle style = new CellVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(CellVisualStyle style)
        {
            base.CopyTo(style);

            style.Alignment = _Alignment;
            style.AllowWrap = _AllowWrap;

            style.Image = _Image;
            style.ImageAlignment = _ImageAlignment;
            style.ImageHighlightMode = _ImageHighlightMode;
            style.ImageIndex = _ImageIndex;
            style.ImageOverlay = _ImageOverlay;

            if (_ImagePadding != null)
                style.ImagePadding = _ImagePadding.Copy();
        }

        #endregion
    }

    #region VisualPropertyChangedEventArgs

    /// <summary>
    /// Represents visual property changed event arguments.
    /// </summary>
    public class VisualPropertyChangedEventArgs : PropertyChangedEventArgs
    {
        #region Public data

        /// <summary>
        /// Gets the change type.
        /// </summary>
        public readonly VisualChangeType ChangeType;

        #endregion

        /// <summary>
        /// Initializes a new instance of the VisualPropertyChangedEventArgs class.
        /// </summary>
        public VisualPropertyChangedEventArgs(string propertyName)
            : base(propertyName)
        {
            ChangeType = VisualChangeType.Layout;
        }

        /// <summary>
        /// Initializes a new instance of the VisualPropertyChangedEventArgs class.
        /// </summary>
        public VisualPropertyChangedEventArgs(string propertyName, VisualChangeType changeType)
            : base(propertyName)
        {
            ChangeType = changeType;
        }
    }

    #endregion

}
