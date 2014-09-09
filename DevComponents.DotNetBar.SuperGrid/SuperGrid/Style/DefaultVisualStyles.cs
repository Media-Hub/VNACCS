using System.ComponentModel;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Defines set of default visual styles that are defined on the container control.
    /// </summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class DefaultVisualStyles : INotifyPropertyChanged
    {
        #region Private variables

        private CellVisualStyles _CellStyles;
        private CellVisualStyles _AlternateRowCellStyles;
        private CellVisualStyles _AlternateColumnCellStyles;

        private ColumnHeaderVisualStyles _ColumnHeaderStyles;
        private ColumnHeaderRowVisualStyles _ColumnHeaderRowStyles;

        private RowVisualStyles _RowStyles;
        private GroupHeaderVisualStyles _GroupHeaderStyles;

        private TextRowVisualStyles _CaptionStyles;
        private TextRowVisualStyles _TitleStyles;
        private TextRowVisualStyles _HeaderStyles;
        private TextRowVisualStyles _FooterStyles;
        private GroupByVisualStyles _GroupByStyles;
        private GridPanelVisualStyle _GridPanelStyle;

        private FilterRowVisualStyles _FilterRowStyles;
        private FilterColumnHeaderVisualStyles _FilterColumnHeaderStyles;

        #endregion

        #region Public properties

        #region AlternateColumnCellStyles

        /// <summary>
        /// Gets or sets the visual styles to be used on
        /// alternating columns (UseAlternateColumnStyle must be enabled)
        /// </summary>
        [Category("Style")]
        [Description("Indicates the visual styles to be used on alternating columns (UseAlternateColumnStyle must be enabled)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CellVisualStyles AlternateColumnCellStyles
        {
            get
            {
                if (_AlternateColumnCellStyles == null)
                {
                    _AlternateColumnCellStyles = new CellVisualStyles();

                    StyleChangeHandler(null, _AlternateColumnCellStyles);
                }

                return (_AlternateColumnCellStyles);
            }

            set
            {
                if (_AlternateColumnCellStyles != value)
                {
                    CellVisualStyles oldValue = _AlternateColumnCellStyles;
                    _AlternateColumnCellStyles = value;

                    OnStyleChanged("AlternateColumnCellStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region AlternateRowCellStyles

        /// <summary>
        /// Gets or sets the visual styles to be used on 
        /// alternating rows (UseAlternateRowStyle must be enabled)
        /// </summary>
        [Category("Style")]
        [Description("Indicates Gets or sets the visual styles to be used on alternating rows (UseAlternateRowStyle must be enabled)")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CellVisualStyles AlternateRowCellStyles
        {
            get
            {
                if (_AlternateRowCellStyles == null)
                {
                    _AlternateRowCellStyles = new CellVisualStyles();

                    StyleChangeHandler(null, _AlternateRowCellStyles);
                }

                return (_AlternateRowCellStyles);
            }

            set
            {
                if (_AlternateRowCellStyles != value)
                {
                    CellVisualStyles oldValue = _AlternateRowCellStyles;
                    _AlternateRowCellStyles = value;

                    OnStyleChanged("AltRowVisualStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region CaptionStyles

        /// <summary>
        /// Gets or sets the visual styles to be used for the grid Caption 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid Caption")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextRowVisualStyles CaptionStyles
        {
            get
            {
                if (_CaptionStyles == null)
                {
                    _CaptionStyles = new TextRowVisualStyles();

                    StyleChangeHandler(null, _CaptionStyles);
                }

                return (_CaptionStyles);
            }

            set
            {
                if (_CaptionStyles != value)
                {
                    TextRowVisualStyles oldValue = _CaptionStyles;
                    _CaptionStyles = value;

                    OnStyleChanged("CaptionStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region CellStyles

        /// <summary>
        /// Gets or sets visual styles to be used for the grid Cells
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid Cells")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public CellVisualStyles CellStyles
        {
            get
            {
                if (_CellStyles == null)
                {
                    _CellStyles = new CellVisualStyles();

                    StyleChangeHandler(null, _CellStyles);
                }

                return (_CellStyles);
            }

            set
            {
                if (_CellStyles != value)
                {
                    CellVisualStyles oldValue = _CellStyles;
                    _CellStyles = value;

                    OnStyleChanged("CellStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region ColumnHeaderStyles

        /// <summary>
        /// Gets or sets visual styles to be used for the grid ColumnHeader 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid ColumnHeader")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColumnHeaderVisualStyles ColumnHeaderStyles
        {
            get
            {
                if (_ColumnHeaderStyles == null)
                {
                    _ColumnHeaderStyles = new ColumnHeaderVisualStyles();

                    StyleChangeHandler(null, _ColumnHeaderStyles);
                }

                return (_ColumnHeaderStyles);
            }

            set
            {
                if (_ColumnHeaderStyles != value)
                {
                    ColumnHeaderVisualStyles oldValue = _ColumnHeaderStyles;
                    _ColumnHeaderStyles = value;

                    OnStyleChanged("ColumnHeaderStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region ColumnHeaderRowStyles

        /// <summary>
        /// Gets or sets visual styles
        /// to be used for the grid ColumnHeader Row 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid ColumnHeader Row")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ColumnHeaderRowVisualStyles ColumnHeaderRowStyles
        {
            get
            {
                if (_ColumnHeaderRowStyles == null)
                {
                    _ColumnHeaderRowStyles = new ColumnHeaderRowVisualStyles();

                    StyleChangeHandler(null, _ColumnHeaderRowStyles);
                }

                return (_ColumnHeaderRowStyles);
            }

            set
            {
                if (_ColumnHeaderRowStyles != value)
                {
                    ColumnHeaderRowVisualStyles oldValue = _ColumnHeaderRowStyles;
                    _ColumnHeaderRowStyles = value;

                    OnStyleChanged("ColumnHeaderRowStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region FilterRowStyles

        /// <summary>
        /// Gets or sets visual styles to
        /// be used for the grid Filter column headers 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid Filter column headers.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FilterColumnHeaderVisualStyles FilterColumnHeaderStyles
        {
            get
            {
                if (_FilterColumnHeaderStyles == null)
                {
                    _FilterColumnHeaderStyles = new FilterColumnHeaderVisualStyles();

                    StyleChangeHandler(null, _FilterColumnHeaderStyles);
                }

                return (_FilterColumnHeaderStyles);
            }

            set
            {
                if (_FilterColumnHeaderStyles != value)
                {
                    FilterColumnHeaderVisualStyles oldValue = _FilterColumnHeaderStyles;
                    _FilterColumnHeaderStyles = value;

                    OnStyleChanged("FilterColumnHeaderStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region FilterRowStyles

        /// <summary>
        /// Gets or sets visual styles to be used for the grid Filter Row 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid Filter Row")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public FilterRowVisualStyles FilterRowStyles
        {
            get
            {
                if (_FilterRowStyles == null)
                {
                    _FilterRowStyles = new FilterRowVisualStyles();

                    StyleChangeHandler(null, _FilterRowStyles);
                }

                return (_FilterRowStyles);
            }

            set
            {
                if (_FilterRowStyles != value)
                {
                    FilterRowVisualStyles oldValue = _FilterRowStyles;
                    _FilterRowStyles = value;

                    OnStyleChanged("FilterRowStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region FooterStyles

        /// <summary>
        /// Gets or sets the visual styles to be used for the grid Footer 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid Footer")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextRowVisualStyles FooterStyles
        {
            get
            {
                if (_FooterStyles == null)
                {
                    _FooterStyles = new TextRowVisualStyles();

                    StyleChangeHandler(null, _FooterStyles);
                }

                return (_FooterStyles);
            }

            set
            {
                if (_FooterStyles != value)
                {
                    TextRowVisualStyles oldValue = _FooterStyles;
                    _FooterStyles = value;

                    OnStyleChanged("FooterStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region GridPanelStyle

        /// <summary>
        /// Gets or sets the visual styles to be used for the main grid panel
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the main grid panel")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridPanelVisualStyle GridPanelStyle
        {
            get
            {
                if (_GridPanelStyle == null)
                {
                    _GridPanelStyle = new GridPanelVisualStyle();

                    StyleChangeHandler(null, _GridPanelStyle);
                }

                return (_GridPanelStyle);
            }

            set
            {
                if (_GridPanelStyle != value)
                {
                    GridPanelVisualStyle oldValue = _GridPanelStyle;
                    _GridPanelStyle = value;

                    OnStyleChanged("GridPanelStyle", oldValue, value);
                }
            }
        }

        #endregion

        #region GroupByStyles

        /// <summary>
        /// Gets or sets the visual styles to be used for the grid GroupBy Row 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid GroupBy Row")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GroupByVisualStyles GroupByStyles
        {
            get
            {
                if (_GroupByStyles == null)
                {
                    _GroupByStyles = new GroupByVisualStyles();

                    StyleChangeHandler(null, _GroupByStyles);
                }

                return (_GroupByStyles);
            }

            set
            {
                if (_GroupByStyles != value)
                {
                    GroupByVisualStyles oldValue = _GroupByStyles;
                    _GroupByStyles = value;

                    OnStyleChanged("GroupByStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region GroupHeaderStyles

        /// <summary>
        /// Gets or sets the visual styles to be used for the Group Header
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the Group Header")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GroupHeaderVisualStyles GroupHeaderStyles
        {
            get
            {
                if (_GroupHeaderStyles == null)
                {
                    _GroupHeaderStyles = new GroupHeaderVisualStyles();

                    StyleChangeHandler(null, _GroupHeaderStyles);
                }

                return (_GroupHeaderStyles);
            }

            set
            {
                if (_GroupHeaderStyles != value)
                {
                    GroupHeaderVisualStyles oldValue = _GroupHeaderStyles;
                    _GroupHeaderStyles = value;

                    OnStyleChanged("GroupHeaderStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region HeaderStyles

        /// <summary>
        /// Gets or sets the visual styles to be used for the grid Header 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid Header")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextRowVisualStyles HeaderStyles
        {
            get
            {
                if (_HeaderStyles == null)
                {
                    _HeaderStyles = new TextRowVisualStyles();

                    StyleChangeHandler(null, _HeaderStyles);
                }

                return (_HeaderStyles);
            }

            set
            {
                if (_HeaderStyles != value)
                {
                    TextRowVisualStyles oldValue = _HeaderStyles;
                    _HeaderStyles = value;

                    OnStyleChanged("HeaderStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region RowStyles

        /// <summary>
        /// Gets or sets the visual styles to be used for the grid rows
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid rows")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RowVisualStyles RowStyles
        {
            get
            {
                if (_RowStyles == null)
                {
                    _RowStyles = new RowVisualStyles();

                    StyleChangeHandler(null, _RowStyles);
                }

                return (_RowStyles);
            }

            set
            {
                if (_RowStyles != value)
                {
                    RowVisualStyles oldValue = _RowStyles;
                    _RowStyles = value;

                    OnStyleChanged("RowStyles", oldValue, value);
                }
            }
        }

        #endregion

        #region TitleStyles

        /// <summary>
        /// Gets or sets the visual styles to be used for the grid Title 
        /// </summary>
        [Category("Style")]
        [Description("Indicates visual styles to be used for the grid Title")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public TextRowVisualStyles TitleStyles
        {
            get
            {
                if (_TitleStyles == null)
                {
                    _TitleStyles = new TextRowVisualStyles();

                    StyleChangeHandler(null, _TitleStyles);
                }

                return (_TitleStyles);
            }

            set
            {
                if (_TitleStyles != value)
                {
                    TextRowVisualStyles oldValue = _TitleStyles;
                    _TitleStyles = value;

                    OnStyleChanged("TitleStyles", oldValue, value);
                }
            }
        }

        #endregion

        #endregion

        #region OnStyleChanged

        private void OnStyleChanged(string property,
            INotifyPropertyChanged oldValue, INotifyPropertyChanged newValue)
        {
            StyleChangeHandler(oldValue, newValue);

            OnPropertyChanged(new VisualPropertyChangedEventArgs(property));
        }

        #endregion

        #region StyleChangeHandler

        private void StyleChangeHandler(
            INotifyPropertyChanged oldValue, INotifyPropertyChanged newValue)
        {
            if (oldValue != null)
                oldValue.PropertyChanged -= StyleChanged;

            if (newValue != null)
                newValue.PropertyChanged += StyleChanged;
        }

        #endregion

        #region StyleChanged

        /// <summary>
        /// Occurs when one of element visual styles has property changes.
        /// Default implementation invalidates visual appearance of element.
        /// </summary>
        /// <param name="sender">VisualStyle that changed.</param>
        /// <param name="e">Event arguments.</param>
        protected virtual void StyleChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        #endregion

        #region INotifyPropertyChanged Members

        /// <summary>
        /// Occurs when property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eh = PropertyChanged;

            if (eh != null)
                eh(this, e);
        }

        #endregion
    }
}
