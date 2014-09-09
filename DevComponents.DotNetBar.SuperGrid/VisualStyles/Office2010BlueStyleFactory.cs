using System.Drawing;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Create the Office 2010 Blue Visual Style for SuperGridControl.
    /// </summary>
    public class Office2010BlueStyleFactory : VisualStyleFactory
    {
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

            style.Background = new Background(factory.GetColor(0xFFFFFF));
            style.BorderColor = new BorderColor(factory.GetColor(0x849DBD));
            style.BorderPattern.All = LinePattern.Solid;
            style.BorderThickness = new Thickness(1);
            style.TextColor = factory.GetColor(0x000000);

            style.TreeLineColor = factory.GetColor(0x484848);
            style.HeaderLineColor = factory.GetColor(0x849DBD);
            style.HorizontalLineColor = factory.GetColor(0xC4D9F6);
            style.VerticalLineColor = factory.GetColor(0xC4D9F6);

            style.TreeLinePattern = LinePattern.Solid;
            style.HorizontalLinePattern = LinePattern.Solid;
            style.VerticalLinePattern = LinePattern.Solid;
            style.HeaderHLinePattern = LinePattern.Solid;
            style.HeaderVLinePattern = LinePattern.Solid;

            BaseTreeButtonVisualStyle tstyle = new BaseTreeButtonVisualStyle();

            tstyle.BorderColor = factory.GetColor(Color.SlateGray);
            tstyle.HotBorderColor = factory.GetColor(Color.SlateGray);
            tstyle.LineColor = factory.GetColor(Color.DarkSlateGray);
            tstyle.HotLineColor = factory.GetColor(Color.DarkSlateGray);
            tstyle.Background = new Background(factory.GetColor(Color.White), factory.GetColor(Color.Gainsboro), 90);
            tstyle.HotBackground = new Background(factory.GetColor(Color.White), factory.GetColor(Color.Gainsboro), 90);

            style.CircleTreeButtonStyle.CollapseButton = tstyle;
            style.CircleTreeButtonStyle.ExpandButton = tstyle;

            style.SquareTreeButtonStyle.CollapseButton = tstyle;
            style.SquareTreeButtonStyle.ExpandButton = tstyle;

            tstyle = new BaseTreeButtonVisualStyle();

            tstyle.BorderColor = factory.GetColor(Color.Black);
            tstyle.HotBorderColor = factory.GetColor(0x27C7F7);
            tstyle.Background = new Background(factory.GetColor(Color.Black));
            tstyle.HotBackground = new Background(factory.GetColor(0xC7EBFA));

            style.TriangleTreeButtonStyle.CollapseButton = tstyle;

            tstyle = new BaseTreeButtonVisualStyle();

            tstyle.BorderColor = factory.GetColor(Color.DimGray);
            tstyle.HotBorderColor = factory.GetColor(0x27C7F7);
            tstyle.Background = new Background(factory.GetColor(Color.White));
            tstyle.HotBackground = new Background(factory.GetColor(0xC7EBFA));

            style.TriangleTreeButtonStyle.ExpandButton = tstyle;

            visualStyle.GridPanelStyle = style;
        }

        #endregion

        #region InitColumnHeaderStyles

        private void InitColumnHeaderStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            ColumnHeaderRowVisualStyle style = new ColumnHeaderRowVisualStyle();

            style.FilterBorderColor = factory.GetColor(0x697D96);
            style.FilterBackground = new Background(Color.White, factory.GetColor(0x5B92FF), 0);

            style.WhiteSpaceBackground = new Background(factory.GetColor(0xCFDDEE));

            style.RowHeader.Background = new Background(
                factory.GetColor(0xEFF5FB), factory.GetColor(0xE2EDFA), BackFillType.ForwardDiagonal);

            style.RowHeader.BorderHighlightColor = GetBorderHighlight(factory);

            style.IndicatorBackground = new Background(factory.GetColor(0x4488E5));
            style.IndicatorBorderColor = factory.GetColor(0x1F48A1);

            visualStyle.ColumnHeaderRowStyles[StyleType.Default] = style;

            style = style.Copy();

            style.FilterBorderColor = factory.GetColor(Color.Green);
            style.FilterBackground = new Background(Color.White, factory.GetColor(Color.Green), 0);

            visualStyle.ColumnHeaderRowStyles[StyleType.Selected] = style;

            style = style.Copy();

            style.FilterBorderColor = factory.GetColor(0x697D96);
            style.FilterBackground = new Background(Color.White, factory.GetColor(0xE2AA00), 0);

            style.RowHeader.Background = new Background(
                factory.GetColor(0xFFDF6B), factory.GetColor(0xFFFCE6), BackFillType.Angle);

            visualStyle.ColumnHeaderRowStyles[StyleType.MouseOver] = style;
            visualStyle.ColumnHeaderRowStyles[StyleType.SelectedMouseOver] = style;
        }

        #endregion

        #region InitTextRowStyles

        private void InitTextRowStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            TextRowVisualStyle style = new TextRowVisualStyle();

            style.Alignment = Alignment.MiddleCenter;
            style.AllowWrap = Tbool.True;

            style.Background = new Background(factory.GetColor(0xEFF5FB), factory.GetColor(0xE2ECFA), BackFillType.Angle);
            style.BorderColor = new BorderColor(factory.GetColor(0x849DBD));

            style.Padding.All = 2;

            style.TextColor = factory.GetColor(0x1E395B);

            style.RowHeaderStyle.Background = new Background(factory.GetColor(0xEFF5FB), factory.GetColor(0xE2ECFA), BackFillType.Angle);
            style.RowHeaderStyle.BorderHighlightColor = GetBorderHighlight(factory);

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

            style.Alignment = Alignment.MiddleLeft;
            style.Background = new Background(factory.GetColor(Color.White));
            style.BorderColor = new BorderColor(factory.GetColor(0xC4D9F6));
            style.BorderPattern.All = LinePattern.Solid;
            style.BorderThickness.All = 0;
            style.Font = SystemFonts.DefaultFont;
            style.ImageAlignment = Alignment.MiddleLeft;
            style.ImagePadding.All = 2;
            style.Margin.All = 0;
            style.ImageOverlay = ImageOverlay.None;
            style.Padding.All = 0;
            style.TextColor = Color.Black;

            visualStyle.CellStyles[StyleType.Default] = style;

            style = new CellVisualStyle();
            style.Background = GetDefaultSelectedBackground(factory);
            visualStyle.CellStyles[StyleType.Selected] = style;

            style = new CellVisualStyle();
            style.Background = GetSelectedMouseOverBackground(factory);
            visualStyle.CellStyles[StyleType.SelectedMouseOver] = style;

            style = new CellVisualStyle();
            style.Background = new Background(factory.GetColor(0xF1F8FF));
            visualStyle.CellStyles[StyleType.Empty] = style;
        }

        #endregion

        #region InitAltRowCellStyles

        private void InitAltRowCellStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            visualStyle.AlternateRowCellStyles[StyleType.Default].Background =
                new Background(factory.GetColor(0xF4F4F4));
        }

        #endregion

        #region InitColumnStyles

        private void InitColumnStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            ColumnHeaderVisualStyle colStyle = new ColumnHeaderVisualStyle();

            colStyle.Alignment = Alignment.MiddleCenter;
            colStyle.BorderColor = new BorderColor(factory.GetColor(0xA0B0C7));
            colStyle.BorderPattern.All = LinePattern.Solid;
            colStyle.BorderThickness.All = 0;
            colStyle.Font = SystemFonts.CaptionFont;
            colStyle.ImageAlignment = Alignment.MiddleLeft;
            colStyle.Margin.All = 0;
            colStyle.ImageOverlay = ImageOverlay.None;
            colStyle.TextColor = Color.Black;

            colStyle.Background = new Background(factory.GetColor(0xEFF4FB), factory.GetColor(0xE1ECFA));
            visualStyle.ColumnHeaderStyles[StyleType.Default] = colStyle;

            colStyle = new ColumnHeaderVisualStyle();
            colStyle.Background = new Background(factory.GetColor(0xFFDF6B), factory.GetColor(0xFFFCE6));
            visualStyle.ColumnHeaderStyles[StyleType.MouseOver] = colStyle;

            colStyle = new ColumnHeaderVisualStyle();
            colStyle.Background = GetDefaultSelectedColumnBackground(factory);
            visualStyle.ColumnHeaderStyles[StyleType.Selected] = colStyle;

            colStyle = new ColumnHeaderVisualStyle();
            colStyle.Background = GetSelectedColumnMouseOverBackground(factory);
            visualStyle.ColumnHeaderStyles[StyleType.SelectedMouseOver] = colStyle;
        }

        #endregion

        #region InitAltColumnStyles

        private void InitAltColumnStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            visualStyle.AlternateColumnCellStyles[StyleType.Default].Background =
                new Background(factory.GetColor(0xEAF2FB));
        }

        #endregion

        #region InitRowStyles

        private void InitRowStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            visualStyle.RowStyles[StyleType.Default] = GetDefaultRowStyle(factory);
            visualStyle.RowStyles[StyleType.MouseOver] = GetMouseOverRowStyle(factory);
            visualStyle.RowStyles[StyleType.SelectedMouseOver] = GetSelectedMouseOverRowStyle(visualStyle, factory);
        }

        #region GetDefaultRowStyle

        private RowVisualStyle GetDefaultRowStyle(ColorFactory factory)
        {
            RowVisualStyle rowStyle = new RowVisualStyle();
            rowStyle.Background = new Background(Color.White);

            RowHeaderVisualStyle style = new RowHeaderVisualStyle();

            style.Font = SystemFonts.DefaultFont;
            style.TextColor = factory.GetColor(0x1E395B);

            Background bg = new Background();
            BackColorBlend bcb = new BackColorBlend();

            bcb.Colors = new Color[3];
            bcb.Colors[0] = factory.GetColor(0xDAE7F5);
            bcb.Colors[1] = factory.GetColor(0xDAE7F5);
            bcb.Colors[2] = factory.GetColor(0xB4C6E0);

            bcb.Positions = new float[3];
            bcb.Positions[0] = 0f;
            bcb.Positions[1] = .75f;
            bcb.Positions[2] = 1f;

            bg.BackColorBlend = bcb;
            bg.GradientAngle = 0;

            style.Background = bg;

            bg = new Background(factory.GetColor(0xDAE7F5));

            style.ActiveRowBackground = bg;
            style.DirtyMarkerBackground = new Background(factory.GetColor(0xAE054F), factory.GetColor(0xE75E94), BackFillType.VerticalCenter);
            style.BorderHighlightColor = GetBorderHighlight(factory);

            rowStyle.RowHeaderStyle = style;

            return (rowStyle);
        }

        #endregion

        #region GetMouseOverRowStyle

        private RowVisualStyle GetMouseOverRowStyle(ColorFactory factory)
        {
            RowVisualStyle rowStyle = new RowVisualStyle();
            rowStyle.Background = new Background(Color.White);

            RowHeaderVisualStyle style = new RowHeaderVisualStyle();
            style.Background = new Background(Color.Plum);
            style.ActiveRowBackground = new Background(Color.Blue);

            Background bg = new Background();
            BackColorBlend bcb = new BackColorBlend();

            bcb.Colors = new Color[3];
            bcb.Colors[0] = factory.GetColor(0xFFE575);
            bcb.Colors[1] = factory.GetColor(0xFFE575);
            bcb.Colors[2] = factory.GetColor(0xF2CD66);

            bcb.Positions = new float[3];
            bcb.Positions[0] = 0f;
            bcb.Positions[1] = .75f;
            bcb.Positions[2] = 1f;

            bg.BackColorBlend = bcb;
            bg.GradientAngle = 0;

            style.Background = bg;

            bg = new Background();
            bcb = new BackColorBlend();

            bcb.Colors = new Color[3];
            bcb.Colors[0] = Color.FromArgb(254 - 20, 240 - 20, 214 - 20);
            bcb.Colors[1] = Color.FromArgb(254 - 30, 199 - 30, 104 - 30);
            bcb.Colors[2] = Color.FromArgb(229 - 30, 133 - 30, 0);

            bcb.Positions = new float[3];
            bcb.Positions[0] = 0f;
            bcb.Positions[1] = .75f;
            bcb.Positions[2] = 1f;

            bg.BackColorBlend = bcb;
            bg.GradientAngle = 0;

            style.ActiveRowBackground = bg;

            rowStyle.RowHeaderStyle = style;

            return (rowStyle);
        }

        #endregion

        #region GetSelectedMouseOverRowStyle

        private RowVisualStyle GetSelectedMouseOverRowStyle(
            DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            RowVisualStyle rowStyle = new RowVisualStyle();
            rowStyle.Background = GetDefaultSelectedBackground(factory);

            RowHeaderVisualStyle style = new RowHeaderVisualStyle();
            style.Background = new Background(factory.GetColor(0xC7CED6));
            style.ActiveRowBackground = new Background(factory.GetColor(0xC7CED6));

            rowStyle.RowHeaderStyle = style;

            visualStyle.RowStyles[StyleType.Selected] = rowStyle;

            rowStyle = new RowVisualStyle();
            rowStyle.Background = GetSelectedColumnMouseOverBackground(factory);

            style = new RowHeaderVisualStyle();
            style.Background = new Background(factory.GetColor(0xC7CED6));
            style.ActiveRowBackground = new Background(factory.GetColor(0xC7CED6));

            rowStyle.RowHeaderStyle = style;

            return (rowStyle);
        }

        #endregion

        #endregion

        #region InitGroupHeaderStyles

        private void InitGroupHeaderStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            GroupHeaderVisualStyle style = new GroupHeaderVisualStyle();

            style.AllowWrap = Tbool.True;
            style.Alignment = Alignment.MiddleLeft;
            style.Font = SystemFonts.DefaultFont;

            style.Background = new
                Background(factory.GetColor(0xFFFFFF));

            style.UnderlineColor = factory.GetColor(0xC4D9F6);

            style.RowHeaderStyle.Background = new Background(factory.GetColor(0xE3EDFA));
            style.RowHeaderStyle.BorderHighlightColor = GetBorderHighlight(factory);
            style.RowHeaderStyle.Font = SystemFonts.DefaultFont;

            visualStyle.GroupHeaderStyles[StyleType.Default] = style;
        }

        #endregion

        #region GetDefaultSelectedColumnBackground
        private Background GetDefaultSelectedColumnBackground(ColorFactory factory)
        {
            Background bg = new Background(factory.GetColor(0xFFD359), factory.GetColor(0xFFEF71));
            return bg;
        }
        #endregion

        #region GetDefaultSelectedBackground

        private Background GetDefaultSelectedBackground(ColorFactory factory)
        {
            return (new Background(factory.GetColor(0xB0D4F7)));
        }

        #endregion

        #region GetDefaultSelectedColumnMouseOverStyle

        private Background GetSelectedColumnMouseOverBackground(ColorFactory factory)
        {
            return (new Background(factory.GetColor(0xFFE063), factory.GetColor(0xFFF9A2)));
        }

        #endregion

        #region GetDefaultSelectedMouseOverStyle

        private Background GetSelectedMouseOverBackground(ColorFactory factory)
        {
            return (new Background(factory.GetColor(0xB0D4F7)));
        }

        #endregion

        #region InitFilterColumnHeaderStyles

        private void InitFilterColumnHeaderStyles(
            DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            FilterColumnHeaderVisualStyle style = new FilterColumnHeaderVisualStyle();

            style.Alignment = Alignment.MiddleLeft;
            style.Font = SystemFonts.CaptionFont;

            style.TextColor = Color.Black;
            style.ErrorTextColor = Color.Red;

            style.Background = new Background(factory.GetColor(0xEFF4FB), factory.GetColor(0xE1ECFA));
            style.GripBarBackground = new Background(factory.GetColor(0xCFDDEE));
            visualStyle.FilterColumnHeaderStyles[StyleType.Default] = style;

            style = new FilterColumnHeaderVisualStyle();
            style.Background = new Background(factory.GetColor(0xFFDF6B), factory.GetColor(0xFFFCE6));
            visualStyle.FilterColumnHeaderStyles[StyleType.MouseOver] = style;

            style = new FilterColumnHeaderVisualStyle();
            style.Background = GetDefaultSelectedColumnBackground(factory);
            visualStyle.FilterColumnHeaderStyles[StyleType.Selected] = style;

            style = new FilterColumnHeaderVisualStyle();
            style.Background = GetSelectedColumnMouseOverBackground(factory);
            visualStyle.FilterColumnHeaderStyles[StyleType.SelectedMouseOver] = style;
        }

        #endregion

        #region InitFilterRowStyles

        private void InitFilterRowStyles(
            DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            FilterRowVisualStyle style = new FilterRowVisualStyle();

            style.FilterBorderColor = factory.GetColor(0x697D96);
            style.FilterBackground = new Background(Color.White, factory.GetColor(0x4168B6), 0);

            style.WhiteSpaceBackground = new Background(factory.GetColor(0xCFDDEE));

            style.RowHeader.Background = new Background(factory.GetColor(0xEFF5FB),
                factory.GetColor(0xE2EDFA), BackFillType.ForwardDiagonal);

            style.RowHeader.BorderHighlightColor = GetBorderHighlight(factory);

            visualStyle.FilterRowStyles[StyleType.Default] = style;

            style = style.Copy();

            style.FilterBackground = new Background(Color.White, factory.GetColor(Color.Green), 0);

            visualStyle.FilterRowStyles[StyleType.Selected] = style;

            style = style.Copy();

            style.FilterBackground = new Background(Color.White, factory.GetColor(0xE2AA00), 0);

            style.RowHeader.Background = new
                Background(factory.GetColor(0xFFDF6B), factory.GetColor(0xFFFCE6), BackFillType.Angle);

            visualStyle.FilterRowStyles[StyleType.MouseOver] = style;
            visualStyle.FilterRowStyles[StyleType.SelectedMouseOver] = style;
        }

        #endregion

        #region InitGroupByStyles

        private void InitGroupByStyles(DefaultVisualStyles visualStyle, ColorFactory factory)
        {
            GroupByVisualStyle style = new GroupByVisualStyle();

            style.Alignment = Alignment.MiddleCenter;
            style.GroupBoxConnectorColor = factory.GetColor(0x849DBD); // Panel border color
            style.GroupBoxBorderColor = factory.GetColor(0x849DBD);

            style.InsertMarkerBorderColor = factory.GetColor(0x849DBD);
            style.InsertMarkerBackground = new Background(factory.GetColor(Color.Red));

            style.RowHeaderStyle.Background = new Background(factory.GetColor(0xEFF5FB), factory.GetColor(0xE2EDFA), BackFillType.ForwardDiagonal);
            style.RowHeaderStyle.BorderHighlightColor = GetBorderHighlight(factory);

            style.TextColor = factory.GetColor(0x363636); // Col text color
            style.GroupBoxBackground = new Background(factory.GetColor(0xEFF4FB), factory.GetColor(0xE1ECFA));  // col background
            visualStyle.GroupByStyles[StyleType.Default] = style;

            style = new GroupByVisualStyle();
            style.GroupBoxBackground = new Background(factory.GetColor(0xFFDF6B), factory.GetColor(0xFFFCE6));  // col background
            visualStyle.GroupByStyles[StyleType.MouseOver] = style;

            style = new GroupByVisualStyle();
            style.GroupBoxBackground = GetDefaultSelectedColumnBackground(factory);  // col background
            visualStyle.GroupByStyles[StyleType.Selected] = style;

            style = new GroupByVisualStyle();
            style.GroupBoxBackground = GetSelectedColumnMouseOverBackground(factory);  // col background
            visualStyle.GroupByStyles[StyleType.SelectedMouseOver] = style;
        }

        #endregion

        #region GetBorderHighlight

        private Color GetBorderHighlight(ColorFactory factory)
        {
            return factory.GetColor("80FFFFFF");
        }

        #endregion
    }
}
