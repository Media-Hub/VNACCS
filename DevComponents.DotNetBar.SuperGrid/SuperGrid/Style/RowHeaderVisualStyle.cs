using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.IO;
using System.Reflection;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// RowHeaderVisualStyle
    ///</summary>
    public class RowHeaderVisualStyle : BaseRowHeaderVisualStyle
    {
        #region Static data

        ///<summary>
        /// Empty
        ///</summary>
        public new static RowHeaderVisualStyle Empty
        {
            get { return (new RowHeaderVisualStyle()); }
        }

        #endregion

        #region Private variables

        private Background _ActiveRowBackground;
        private Background _DirtyMarkerBackground;

        private Color _ActiveRowIndicatorColor = Color.Empty;

        private Image _ActiveRowImage;
        private Image _ActiveRowImageCache;
        private int _ActiveRowImageIndex = -1;

        private Image _EditingRowImage;
        private int _EditingRowImageIndex = -1;
        private Image _EditingRowImageCache;

        private Image _InfoRowImage;
        private Image _InfoRowImageCache;
        private int _InfoRowImageIndex = -1;

        #endregion

        #region Public properties

        #region ActiveRowBackground

        /// <summary>
        /// Gets or sets the ActiveRow background
        /// </summary>
        [Description("Indicates the ActiveRow background")]
        public Background ActiveRowBackground
        {
            get
            {
                if (_ActiveRowBackground == null)
                {
                    _ActiveRowBackground = Background.Empty;

                    UpdateChangeHandler(null, _ActiveRowBackground);
                }

                return (_ActiveRowBackground);
            }

            set
            {
                if (_ActiveRowBackground != value)
                {
                    UpdateChangeHandler(_ActiveRowBackground, value);

                    _ActiveRowBackground = value;

                    OnPropertyChangedEx("ActiveRowBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeActiveRowBackground()
        {
            return (_ActiveRowBackground != null && _ActiveRowBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetActiveRowBackground()
        {
            ActiveRowBackground = null;
        }

        #endregion

        #region ActiveRowImage

        /// <summary>
        /// Gets or sets the Active Row Image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Active Row image")]
        public Image ActiveRowImage
        {
            get { return (_ActiveRowImage); }

            set
            {
                if (_ActiveRowImage != value)
                {
                    _ActiveRowImage = value;

                    OnPropertyChangedEx("ActiveRowImage", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region ActiveRowImageIndex

        /// <summary>
        /// Gets or sets the Active Row image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Active Row image index")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int ActiveRowImageIndex
        {
            get { return (_ActiveRowImageIndex); }

            set
            {
                if (_ActiveRowImageIndex != value)
                {
                    _ActiveRowImageIndex = value;

                    OnPropertyChangedEx("ActiveRowImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetActiveRowImageIndex()
        {
            _ActiveRowImageIndex = -1;
        }

        #endregion

        #region ActiveRowIndicatorColor

        /// <summary>
        /// Gets or sets the Active Row Indicator color
        /// </summary>
        [Description("Indicates the Active Row Indicator color")]
        public Color ActiveRowIndicatorColor
        {
            get { return (_ActiveRowIndicatorColor); }

            set
            {
                if (_ActiveRowIndicatorColor != value)
                {
                    _ActiveRowIndicatorColor = value;

                    OnPropertyChangedEx("ActiveRowIndicatorColor", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeActiveRowIndicatorColor()
        {
            return (_ActiveRowIndicatorColor.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetActiveRowIndicatorColor()
        {
            _ActiveRowIndicatorColor = Color.Empty;
        }

        #endregion

        #region DirtyMarkerBackground

        /// <summary>
        /// Gets or sets the DirtyRow marker background
        /// </summary>
        [Description("Indicates the DirtyRow marker background")]
        public Background DirtyMarkerBackground
        {
            get
            {
                if (_DirtyMarkerBackground == null)
                {
                    _DirtyMarkerBackground = Background.Empty;

                    UpdateChangeHandler(null, _DirtyMarkerBackground);
                }

                return (_DirtyMarkerBackground);
            }

            set
            {
                if (_DirtyMarkerBackground != value)
                {
                    UpdateChangeHandler(_DirtyMarkerBackground, value);

                    _DirtyMarkerBackground = value;

                    OnPropertyChangedEx("DirtyMarkerBackground", VisualChangeType.Render);
                }
            }
        }

        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private bool ShouldSerializeDirtyMarkerBackground()
        {
            return (_DirtyMarkerBackground != null && _DirtyMarkerBackground.IsEmpty == false);
        }

        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetDirtyMarkerBackground()
        {
            DirtyMarkerBackground = null;
        }

        #endregion

        #region EditingRowImage

        /// <summary>
        /// Gets or sets the Editing Row Image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Editing Row image")]
        public Image EditingRowImage
        {
            get { return (_EditingRowImage); }

            set
            {
                if (_EditingRowImage != value)
                {
                    _EditingRowImage = value;

                    OnPropertyChangedEx("EditingRowImage", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region EditingRowImageIndex

        /// <summary>
        /// Gets or sets the Editing Row image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Editing Row image index")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int EditingRowImageIndex
        {
            get { return (_EditingRowImageIndex); }

            set
            {
                if (_EditingRowImageIndex != value)
                {
                    _EditingRowImageIndex = value;

                    OnPropertyChangedEx("EditingRowImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetEditingRowImageIndex()
        {
            _EditingRowImageIndex = -1;
        }

        #endregion

        #region InfoRowImage

        /// <summary>
        /// Gets or sets the Info Row Image
        /// </summary>
        [DefaultValue(null), Category("Appearance")]
        [Description("Indicates the Info Row image")]
        public Image InfoRowImage
        {
            get { return (_InfoRowImage); }

            set
            {
                if (_InfoRowImage != value)
                {
                    _InfoRowImage = value;

                    OnPropertyChangedEx("InfoRowImage", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region InfoRowImageIndex

        /// <summary>
        /// Gets or sets the Info Row image index
        /// </summary>
        [Browsable(true), DefaultValue(-1)]
        [Category("Appearance"), Description("Indicates the Info Row image index")]
        [Editor("DevComponents.SuperGrid.Design.ImageIndexEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
        [TypeConverter(typeof(ImageIndexConverter))]
        public int InfoRowImageIndex
        {
            get { return (_InfoRowImageIndex); }

            set
            {
                if (_InfoRowImageIndex != value)
                {
                    _InfoRowImageIndex = value;

                    OnPropertyChangedEx("InfoRowImageIndex", VisualChangeType.Layout);
                }
            }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        private void ResetInfoRowImageIndex()
        {
            _InfoRowImageIndex = -1;
        }

        #endregion

        #endregion

        #region GetActiveRowImage

        internal Image GetActiveRowImage(GridPanel panel)
        {
            if (_ActiveRowImage != null)
                return (_ActiveRowImage);

            if (_ActiveRowImageIndex >= 0 && panel != null)
            {
                ImageList imageList = panel.ImageList;

                if (imageList != null && _ActiveRowImageIndex < imageList.Images.Count)
                    return (imageList.Images[_ActiveRowImageIndex]);
            }

            return (GetActiveRowImage());
        }

        #region GetActiveRowImage

        private Image GetActiveRowImage()
        {
            if (_ActiveRowImage != null)
                return (_ActiveRowImage);

            if (_ActiveRowImageCache == null)
            {
                Rectangle r = new Rectangle(0, 0, 4, 7);
                Image image = new Bitmap(4, 7);

                using (Graphics g = Graphics.FromImage(image))
                {
                    using (GraphicsPath path = new GraphicsPath())
                    {
                        Point pt = new Point(r.Right, r.Y + r.Height / 2);

                        Point[] pts =
                            {
                                pt,
                                new Point(pt.X - 4, pt.Y + 4),
                                new Point(pt.X - 4, pt.Y - 4),
                                pt
                            };

                        path.AddLines(pts);

                        Color color = ActiveRowIndicatorColor;

                        if (color.IsEmpty)
                            color = Color.Black;

                        using (Brush br = new SolidBrush(color))
                            g.FillPath(br, path);
                    }
                }

                _ActiveRowImageCache = image;
            }

            return (_ActiveRowImageCache);
        }

        #endregion

        #endregion

        #region GetEditingRowImage

        internal Image GetEditingRowImage(GridPanel panel)
        {
            if (_EditingRowImage != null)
                return (_EditingRowImage);

            if (_EditingRowImageIndex >= 0 && panel != null)
            {
                ImageList imageList = panel.ImageList;

                if (imageList != null && _EditingRowImageIndex < imageList.Images.Count)
                    return (imageList.Images[_EditingRowImageIndex]);
            }

            return (GetEditingRowImage());
        }

        #region GetEditingRowImage

        private Image GetEditingRowImage()
        {
            if (_EditingRowImage != null)
                return (_EditingRowImage);

            if (_EditingRowImageCache == null)
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();

                using (Stream myStream =
                    myAssembly.GetManifestResourceStream("DevComponents.DotNetBar.SuperGrid.Pencil3.png"))
                {
                    if (myStream != null)
                        _EditingRowImageCache = new Bitmap(myStream);
                }
            }

            return (_EditingRowImageCache);
        }

        #endregion

        #endregion

        #region GetInfoRowImage

        internal Image GetInfoRowImage(GridPanel panel)
        {
            if (_InfoRowImage != null)
                return (_InfoRowImage);

            if (_InfoRowImageIndex >= 0 && panel != null)
            {
                ImageList imageList = panel.ImageList;

                if (imageList != null && _InfoRowImageIndex < imageList.Images.Count)
                    return (imageList.Images[_InfoRowImageIndex]);
            }

            return (GetInfoRowImage());
        }

        #region GetInfoRowImage

        private Image GetInfoRowImage()
        {
            if (_InfoRowImage != null)
                return (_InfoRowImage);

            if (_InfoRowImageCache == null)
            {
                Assembly myAssembly = Assembly.GetExecutingAssembly();

                using (Stream myStream =
                    myAssembly.GetManifestResourceStream("DevComponents.DotNetBar.SuperGrid.InfoImage.png"))
                {
                    if (myStream != null)
                        _InfoRowImageCache = new Bitmap(myStream);
                }
            }

            return (_InfoRowImageCache);
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(RowHeaderVisualStyle style)
        {
            if (style != null)
            {
                base.ApplyStyle(style);

                if (style._ActiveRowIndicatorColor.IsEmpty == false)
                    _ActiveRowIndicatorColor = style._ActiveRowIndicatorColor;

                if (style._ActiveRowBackground != null && style._ActiveRowBackground.IsEmpty == false)
                    _ActiveRowBackground = style._ActiveRowBackground.Copy();

                if (style._DirtyMarkerBackground != null && style._DirtyMarkerBackground.IsEmpty == false)
                    _DirtyMarkerBackground = style._DirtyMarkerBackground.Copy();

                if (style.ActiveRowImageIndex >= 0)
                {
                    _ActiveRowImage = null;
                    _ActiveRowImageIndex = style.ActiveRowImageIndex;
                }

                if (style.ActiveRowImage != null)
                {
                    _ActiveRowImage = style.ActiveRowImage;
                    _ActiveRowImageIndex = -1;
                }

                if (style.EditingRowImageIndex >= 0)
                {
                    _EditingRowImage = null;
                    _EditingRowImageIndex = style.EditingRowImageIndex;
                }

                if (style.EditingRowImage != null)
                {
                    _EditingRowImage = style.EditingRowImage;
                    _EditingRowImageIndex = -1;
                }

                if (style.InfoRowImageIndex >= 0)
                {
                    _InfoRowImage = null;
                    _InfoRowImageIndex = style.InfoRowImageIndex;
                }

                if (style.InfoRowImage != null)
                {
                    _InfoRowImage = style.InfoRowImage;
                    _InfoRowImageIndex = -1;
                }
            }
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public new RowHeaderVisualStyle Copy()
        {
            RowHeaderVisualStyle copy = new RowHeaderVisualStyle();

            CopyTo(copy);

            return (copy);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(RowHeaderVisualStyle copy)
        {
            base.CopyTo(copy);

            if (_ActiveRowIndicatorColor.IsEmpty == false)
                copy.ActiveRowIndicatorColor = _ActiveRowIndicatorColor;

            if (_ActiveRowBackground != null)
                copy.ActiveRowBackground = _ActiveRowBackground.Copy();

            if (_DirtyMarkerBackground != null)
                copy.DirtyMarkerBackground = _DirtyMarkerBackground.Copy();

            copy.ActiveRowImage = _ActiveRowImage;
            copy.ActiveRowImageIndex = _ActiveRowImageIndex;

            copy.EditingRowImage = _EditingRowImage;
            copy.EditingRowImageIndex = _EditingRowImageIndex;

            copy.InfoRowImage = _InfoRowImage;
            copy.InfoRowImageIndex = _InfoRowImageIndex;
        }

        #endregion
    }
}
