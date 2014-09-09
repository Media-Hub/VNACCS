using System.Drawing;
using DevComponents.DotNetBar.Metro.ColorTables;
using DevComponents.DotNetBar.Metro.Rendering;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Create the Metro Visual Style for SuperGridControl.
    /// </summary>
    public class MetroStyleFactory : VisualStyleFactory
    {
        private MetroPartColors _MetroPartColors;

        /// <summary>
        /// Initializes a new instance of the MetroStyleFactory class.
        /// </summary>
        public MetroStyleFactory()
        {
            _MetroPartColors = MetroRender.GetColorTable().MetroPartColors;
        }

        /// <summary>
        /// Initializes a new instance of the MetroStyleFactory class.
        /// </summary>
        /// <param name="metroPartColors">Metro Part Colors to Initialize Style with.</param>
        public MetroStyleFactory(MetroPartColors metroPartColors)
        {
            _MetroPartColors = metroPartColors;

            if (metroPartColors == null)
                _MetroPartColors = MetroRender.GetColorTable().MetroPartColors;
        }

        /// <summary>
        /// Create the DefaultVisualStyle for SuperGridControl.
        /// </summary>
        /// <param name="factory">Color-Factory used to generate colors.</param>
        /// <returns>New instance of DefaultVisualStyles class.</returns>s
        public override DefaultVisualStyles CreateStyle(ColorFactory factory)
        {
            DefaultVisualStyles visualStyle = new DefaultVisualStyles();

            InitGridPanelStyle(visualStyle, factory);

            InitCellStyles(visualStyle, factory);
            InitAltRowCellStyles(visualStyle, factory);

            InitColumnStyles(visualStyle, factory);
            InitAltColumnStyles(visualStyle, factory);
            InitColumnHeaderStyles(visualStyle, factory);

            InitRowStyles(visualStyle, factory);
            InitTextRowStyles(visualStyle, factory);

            InitGroupHeaderStyles(visualStyle, factory);

            InitFilterRowStyles(visualStyle, factory);
            InitFilterColumnHeaderStyles(visualStyle, factory);

            InitGroupByStyles(visualStyle, factory);

            return (visualStyle);
        }

        #region InitGridPanelStyle

        private void InitGridPanelStyle(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            GridPanelVisualStyle style = new GridPanelVisualStyle();

            MetroPartColors metroColors = _MetroPartColors;
            style.Background = new Background(factory.GetColor(metroColors.CanvasColor));
            style.BorderColor = new BorderColor(factory.GetColor(metroColors.CanvasColorDarkShade));
            style.BorderPattern.All = LinePattern.Solid;
            style.BorderThickness = new Thickness(1);
            style.TextColor = factory.GetColor(metroColors.TextColor);

            style.TreeLineColor = factory.GetColor(metroColors.CanvasColorLighterShade);
            style.HeaderLineColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            style.HorizontalLineColor = factory.GetColor(metroColors.CanvasColorLighterShade);
            style.VerticalLineColor = factory.GetColor(metroColors.CanvasColorLighterShade);

            style.TreeLinePattern = LinePattern.Solid;
            style.HorizontalLinePattern = LinePattern.Solid;
            style.VerticalLinePattern = LinePattern.Solid;
            style.HeaderHLinePattern = LinePattern.Solid;
            style.HeaderVLinePattern = LinePattern.Solid;

            BaseTreeButtonVisualStyle tstyle = new BaseTreeButtonVisualStyle();

            tstyle.BorderColor = factory.GetColor(metroColors.CanvasColorLighterShade);
            tstyle.HotBorderColor = factory.GetColor(metroColors.CanvasColorLighterShade);
            tstyle.LineColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            tstyle.HotLineColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            tstyle.Background = new Background(factory.GetColor(metroColors.CanvasColor), factory.GetColor(metroColors.CanvasColorDarkShade), 90);
            tstyle.HotBackground = new Background(factory.GetColor(metroColors.CanvasColor), factory.GetColor(metroColors.CanvasColorDarkShade), 90);

            style.CircleTreeButtonStyle.CollapseButton = tstyle;
            style.CircleTreeButtonStyle.ExpandButton = tstyle;

            style.SquareTreeButtonStyle.CollapseButton = tstyle;
            style.SquareTreeButtonStyle.ExpandButton = tstyle;

            style.TriangleTreeButtonStyle.CollapseButton = tstyle;
            style.TriangleTreeButtonStyle.ExpandButton = tstyle;

            style.TriangleTreeButtonStyle.ExpandButton = tstyle;
            visualStyle.GridPanelStyle = style;
        }

        #endregion

        #region InitColumnHeaderStyles

        private void InitColumnHeaderStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            ColumnHeaderRowVisualStyle style = new ColumnHeaderRowVisualStyle();
            MetroPartColors metroColors = _MetroPartColors;

            style.FilterBorderColor = factory.GetColor(metroColors.CanvasColorDarkShade);

            style.FilterBackground = new Background(
                Color.White, factory.GetColor(metroColors.CanvasColorDarkShade), 0);

            style.WhiteSpaceBackground = new Background(factory.GetColor(metroColors.CanvasColor));

            style.RowHeader.Background = new
                Background(factory.GetColor(metroColors.CanvasColorLighterShade));

            style.RowHeader.BorderHighlightColor = GetBorderHighlight();

            style.IndicatorBackground = new Background(factory.GetColor(metroColors.ComplementColor));
            style.IndicatorBorderColor = factory.GetColor(metroColors.ComplementColorDark);

            visualStyle.ColumnHeaderRowStyles[StyleType.Default] = style;

            style = style.Copy();

            style.FilterBorderColor = factory.GetColor(metroColors.ComplementColor);

            style.FilterBackground = new Background(
                Color.White, factory.GetColor(metroColors.ComplementColor), 45);

            visualStyle.ColumnHeaderRowStyles[StyleType.Selected] = style;

            style = style.Copy();

            style.FilterBorderColor = factory.GetColor(metroColors.CanvasColorDarkShade);
            
            style.FilterBackground = new Background(
                Color.White, factory.GetColor(metroColors.BaseColorDark), 0);

            style.RowHeader.Background = new Background(
                factory.GetColor(metroColors.CanvasColorLightShade));

            visualStyle.ColumnHeaderRowStyles[StyleType.MouseOver] = style;
            visualStyle.ColumnHeaderRowStyles[StyleType.SelectedMouseOver] = style;
        }

        #endregion

        #region InitTextRowStyles

        private void InitTextRowStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            TextRowVisualStyle style = new TextRowVisualStyle();
            MetroPartColors metroColors = _MetroPartColors;

            style.Alignment = Alignment.MiddleCenter;
            style.AllowWrap = Tbool.True;

            style.Background = new Background(factory.GetColor(metroColors.CanvasColor));
            style.BorderColor = new BorderColor(factory.GetColor(metroColors.CanvasColorLighterShade));

            style.Padding.All = 2;

            style.TextColor = factory.GetColor(metroColors.TextColor);

            style.RowHeaderStyle.Background = new Background(factory.GetColor(metroColors.CanvasColorLighterShade));
            style.RowHeaderStyle.BorderHighlightColor = GetBorderHighlight();

            visualStyle.TitleStyles[StyleType.Default] = style;
            visualStyle.HeaderStyles[StyleType.Default] = style.Copy();
            visualStyle.FooterStyles[StyleType.Default] = style.Copy();

            style = style.Copy();
            style.Background = new Background(Color.Transparent);

            visualStyle.CaptionStyles[StyleType.Default] = style;
        }

        #endregion

        #region InitCellStyles

        private void InitCellStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            CellVisualStyle style = new CellVisualStyle();
            MetroPartColors metroColors = _MetroPartColors;

            style.Alignment = Alignment.MiddleLeft;
            style.Background = new Background(factory.GetColor(metroColors.CanvasColor));
            style.BorderColor = new BorderColor(factory.GetColor(metroColors.CanvasColorLighterShade));
            style.BorderPattern.All = LinePattern.Solid;
            style.BorderThickness.All = 0;
            style.Font = SystemFonts.DefaultFont;
            style.ImageAlignment = Alignment.MiddleLeft;
            style.ImagePadding.All = 2;
            style.Margin.All = 0;
            style.ImageOverlay = ImageOverlay.None;
            style.Padding.All = 0;
            style.TextColor = metroColors.TextColor;

            visualStyle.CellStyles[StyleType.Default] = style;

            style = new CellVisualStyle();
            style.TextColor = metroColors.CanvasColorDarkShade;
            visualStyle.CellStyles[StyleType.ReadOnly] = style;

            style = new CellVisualStyle();
            style.Background = GetDefaultSelectedBackground(factory);
            style.TextColor = factory.GetColor(metroColors.BaseTextColor);

            visualStyle.CellStyles[StyleType.Selected] = style;
            visualStyle.CellStyles[StyleType.ReadOnlySelected] = style.Copy();

            style = new CellVisualStyle();
            style.Background = GetSelectedMouseOverBackground(factory);
            style.TextColor = factory.GetColor(metroColors.BaseColorLightText);

            visualStyle.CellStyles[StyleType.SelectedMouseOver] = style;
            visualStyle.CellStyles[StyleType.ReadOnlySelectedMouseOver] = style.Copy();

            style = new CellVisualStyle();
            style.Background = new Background(factory.GetColor(metroColors.CanvasColor));

            visualStyle.CellStyles[StyleType.Empty] = style;
        }

        #endregion

        #region InitAltRowCellStyles

        private void InitAltRowCellStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            MetroPartColors metroColors = _MetroPartColors;
            visualStyle.AlternateRowCellStyles[StyleType.Default].Background =
                new Background(factory.GetColor(metroColors.CanvasColorLighterShade));
        }

        #endregion

        #region InitColumnStyles

        private void InitColumnStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            ColumnHeaderVisualStyle colStyle = new ColumnHeaderVisualStyle();
            MetroPartColors metroColors = _MetroPartColors;

            colStyle.Alignment = Alignment.MiddleCenter;
            colStyle.BorderColor = new BorderColor(factory.GetColor(metroColors.CanvasColorLightShade));
            colStyle.BorderPattern.All = LinePattern.Solid;
            colStyle.BorderThickness.All = 0;
            colStyle.Font = SystemFonts.CaptionFont;
            colStyle.ImageAlignment = Alignment.MiddleLeft;
            colStyle.Margin.All = 0;
            colStyle.ImageOverlay = ImageOverlay.None;
            colStyle.TextColor = factory.GetColor(metroColors.TextColor);

            colStyle.Background = new Background(factory.GetColor(metroColors.CanvasColorLighterShade));

            visualStyle.ColumnHeaderStyles[StyleType.Default] = colStyle;
            visualStyle.ColumnHeaderStyles[StyleType.ReadOnly] = colStyle.Copy();
            visualStyle.ColumnHeaderStyles[StyleType.ReadOnly].Font = null;

            colStyle = new ColumnHeaderVisualStyle();
            colStyle.Background = new Background(factory.GetColor(metroColors.CanvasColorLightShade));
            visualStyle.ColumnHeaderStyles[StyleType.MouseOver] = colStyle;
            visualStyle.ColumnHeaderStyles[StyleType.ReadOnlyMouseOver] = colStyle.Copy();

            colStyle = new ColumnHeaderVisualStyle();
            colStyle.Background = GetDefaultSelectedColumnBackground(factory);
            visualStyle.ColumnHeaderStyles[StyleType.Selected] = colStyle;
            visualStyle.ColumnHeaderStyles[StyleType.ReadOnlySelected] = colStyle.Copy();

            colStyle = new ColumnHeaderVisualStyle();
            colStyle.Background = GetSelectedColumnMouseOverBackground();
            visualStyle.ColumnHeaderStyles[StyleType.SelectedMouseOver] = colStyle;
            visualStyle.ColumnHeaderStyles[StyleType.ReadOnlySelectedMouseOver] = colStyle.Copy();
        }

        #endregion

        #region InitAltColumnStyles

        private void InitAltColumnStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            MetroPartColors metroColors = _MetroPartColors;
            visualStyle.AlternateColumnCellStyles[StyleType.Default].Background =
                new Background(factory.GetColor(metroColors.CanvasColor));
        }

        #endregion

        #region InitRowStyles

        private void InitRowStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            visualStyle.RowStyles[StyleType.Default] = GetDefaultRowStyle();
            visualStyle.RowStyles[StyleType.MouseOver] = GetMouseOverRowStyle();
            visualStyle.RowStyles[StyleType.SelectedMouseOver] = GetSelectedMouseOverRowStyle(visualStyle, factory);
        }

        #region GetDefaultRowStyle

        private RowVisualStyle GetDefaultRowStyle()
        {
            MetroPartColors metroColors = _MetroPartColors;

            RowVisualStyle rowStyle = new RowVisualStyle();
            rowStyle.Background = new Background(metroColors.CanvasColor);

            RowHeaderVisualStyle style = new RowHeaderVisualStyle();

            style.Font = SystemFonts.DefaultFont;
            style.TextColor = metroColors.TextColor;

            Background bg = new Background(metroColors.CanvasColorLighterShade);
            style.Background = bg;

            bg = new Background(metroColors.BaseColor);
            style.ActiveRowBackground = bg;
            style.DirtyMarkerBackground = new Background(metroColors.BaseColorDarker);
            style.BorderHighlightColor = GetBorderHighlight();

            rowStyle.RowHeaderStyle = style;

            return (rowStyle);
        }

        #endregion

        #region GetMouseOverRowStyle

        private RowVisualStyle GetMouseOverRowStyle()
        {
            MetroPartColors metroColors = _MetroPartColors;

            RowVisualStyle rowStyle = new RowVisualStyle();
            rowStyle.Background = new Background(metroColors.CanvasColor);

            RowHeaderVisualStyle style = new RowHeaderVisualStyle();
            style.Background = new Background(metroColors.CanvasColorLighterShade);
            style.ActiveRowBackground = new Background(metroColors.CanvasColorLightShade);

            Background bg = new Background(metroColors.CanvasColorLightShade);
            style.Background = bg;

            bg = new Background(metroColors.BaseColor);
            style.ActiveRowBackground = bg;

            rowStyle.RowHeaderStyle = style;

            return (rowStyle);
        }

        #endregion

        #region GetSelectedMouseOverRowStyle

        private RowVisualStyle GetSelectedMouseOverRowStyle(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            MetroPartColors metroColors = _MetroPartColors;

            RowVisualStyle rowStyle = new RowVisualStyle();
            rowStyle.Background = GetDefaultSelectedBackground(factory);

            RowHeaderVisualStyle style = new RowHeaderVisualStyle();
            style.Background = new Background(metroColors.BaseColor);
            style.ActiveRowBackground = new Background(metroColors.BaseColor);

            rowStyle.RowHeaderStyle = style;

            visualStyle.RowStyles[StyleType.Selected] = rowStyle;

            rowStyle = new RowVisualStyle();
            rowStyle.Background = GetSelectedColumnMouseOverBackground();

            style = new RowHeaderVisualStyle();
            style.Background = new Background(metroColors.BaseColor);
            style.ActiveRowBackground = new Background(metroColors.BaseColor);

            rowStyle.RowHeaderStyle = style;

            return (rowStyle);
        }

        #endregion

        #endregion

        #region InitGroupHeaderStyles

        private void InitGroupHeaderStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            MetroPartColors metroColors = _MetroPartColors;

            GroupHeaderVisualStyle style = new GroupHeaderVisualStyle();

            style.AllowWrap = Tbool.True;
            style.Alignment = Alignment.MiddleLeft;
            style.Font = SystemFonts.DefaultFont;

            style.Background = new
                Background(factory.GetColor(metroColors.CanvasColor));

            style.UnderlineColor = factory.GetColor(metroColors.ComplementColor);

            style.RowHeaderStyle.Background = new Background(factory.GetColor(metroColors.CanvasColorLighterShade));
            style.RowHeaderStyle.BorderHighlightColor = GetBorderHighlight();
            style.RowHeaderStyle.Font = SystemFonts.DefaultFont;

            style.TextColor = metroColors.TextColor;
            
            visualStyle.GroupHeaderStyles[StyleType.Default] = style;
        }

        #endregion

        #region GetDefaultSelectedColumnBackground
        private Background GetDefaultSelectedColumnBackground(ColorFactory factory)
        {
            MetroPartColors metroColors = _MetroPartColors;

            Background bg = new Background(factory.GetColor(metroColors.CanvasColorLighterShade),
                factory.GetColor(metroColors.CanvasColorLightShade));

            return (bg);
        }
        #endregion

        #region GetDefaultSelectedBackground

        private Background GetDefaultSelectedBackground(ColorFactory factory)
        {
            MetroPartColors metroColors = _MetroPartColors;

            Background bg = new Background(factory.GetColor(metroColors.BaseColor));

            return (bg);
        }

        #endregion

        #region GetDefaultSelectedColumnMouseOverStyle

        private Background GetSelectedColumnMouseOverBackground()
        {
            MetroPartColors metroColors = _MetroPartColors;

            return (new Background(metroColors.BaseColorLight));
        }

        #endregion

        #region GetDefaultSelectedMouseOverStyle

        private Background GetSelectedMouseOverBackground(ColorFactory factory)
        {
            MetroPartColors metroColors = _MetroPartColors;

            return (new Background(factory.GetColor(metroColors.BaseColorLight)));
        }

        #endregion

        #region InitFilterColumnHeaderStyles

        private void InitFilterColumnHeaderStyles(
            DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            FilterColumnHeaderVisualStyle colStyle = new FilterColumnHeaderVisualStyle();
            MetroPartColors metroColors = _MetroPartColors;

            colStyle.Alignment = Alignment.MiddleCenter;
            colStyle.Font = SystemFonts.CaptionFont;

            colStyle.TextColor = factory.GetColor(metroColors.TextColor);
            colStyle.ErrorTextColor = factory.GetColor(metroColors.BaseColorDarker);

            colStyle.Background = new Background(factory.GetColor(metroColors.CanvasColorLighterShade));
            colStyle.GripBarBackground = new Background(factory.GetColor(metroColors.CanvasColor));
            visualStyle.FilterColumnHeaderStyles[StyleType.Default] = colStyle;

            colStyle = new FilterColumnHeaderVisualStyle();
            colStyle.Background = new Background(factory.GetColor(metroColors.CanvasColorLightShade));
            visualStyle.FilterColumnHeaderStyles[StyleType.MouseOver] = colStyle;

            colStyle = new FilterColumnHeaderVisualStyle();
            colStyle.Background = GetDefaultSelectedColumnBackground(factory);
            visualStyle.FilterColumnHeaderStyles[StyleType.Selected] = colStyle;

            colStyle = new FilterColumnHeaderVisualStyle();
            colStyle.Background = GetSelectedColumnMouseOverBackground();
            visualStyle.FilterColumnHeaderStyles[StyleType.SelectedMouseOver] = colStyle;
        }

        #endregion

        #region InitFilterRowStyles

        private void InitFilterRowStyles(
            DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            FilterRowVisualStyle style = new FilterRowVisualStyle();
            MetroPartColors metroColors = _MetroPartColors;

            style.FilterBorderColor = factory.GetColor(0x787D87);

            style.FilterBackground = new Background(
                Color.White, factory.GetColor(metroColors.CanvasColorDarkShade), 0);

            style.WhiteSpaceBackground = new Background(factory.GetColor(metroColors.CanvasColor));

            style.RowHeader.Background = new
                Background(factory.GetColor(metroColors.CanvasColorLighterShade));

            style.RowHeader.BorderHighlightColor = GetBorderHighlight();

            visualStyle.FilterRowStyles[StyleType.Default] = style;

            style = style.Copy();

            style.FilterBackground = new Background(
                Color.White, factory.GetColor(metroColors.ComplementColor), 45);

            visualStyle.FilterRowStyles[StyleType.Selected] = style;

            style = style.Copy();

            style.FilterBackground = new Background(
                Color.White, factory.GetColor(metroColors.BaseColorDark), 0);

            style.RowHeader.Background = new
                Background(factory.GetColor(metroColors.CanvasColorLightShade));

            visualStyle.FilterRowStyles[StyleType.MouseOver] = style;
            visualStyle.FilterRowStyles[StyleType.SelectedMouseOver] = style;
        }

        #endregion

        #region InitGroupByStyles

        private void InitGroupByStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            GroupByVisualStyle style = new GroupByVisualStyle();
            MetroPartColors metroColors = _MetroPartColors;

            style.Alignment = Alignment.MiddleCenter;
            style.GroupBoxConnectorColor = factory.GetColor(metroColors.BaseColorDark); // Panel border color
            style.GroupBoxBorderColor = factory.GetColor(metroColors.BaseColorDark);

            style.InsertMarkerBorderColor = factory.GetColor(metroColors.BaseColorDark);
            style.InsertMarkerBackground = GetSelectedColumnMouseOverBackground();

            style.RowHeaderStyle.Background = new Background(factory.GetColor(metroColors.CanvasColorLighterShade));
            style.RowHeaderStyle.BorderHighlightColor = GetBorderHighlight();

            style.TextColor = factory.GetColor(0x363636); // Col text color
            style.GroupBoxBackground = new Background(factory.GetColor(metroColors.CanvasColorLighterShade));  // col background
            visualStyle.GroupByStyles[StyleType.Default] = style;

            style = new GroupByVisualStyle();
            style.GroupBoxBackground = new Background(factory.GetColor(metroColors.CanvasColorLightShade));  // col background
            visualStyle.GroupByStyles[StyleType.MouseOver] = style;

            style = new GroupByVisualStyle();
            style.GroupBoxBackground = GetDefaultSelectedColumnBackground(factory);  // col background
            visualStyle.GroupByStyles[StyleType.Selected] = style;

            style = new GroupByVisualStyle();
            style.GroupBoxBackground = GetSelectedColumnMouseOverBackground();  // col background
            visualStyle.GroupByStyles[StyleType.SelectedMouseOver] = style;
        }

        #endregion

        #region GetBorderHighlight

        private Color GetBorderHighlight()
        {
            return (Color.Transparent);
        }

        #endregion
    }
}
