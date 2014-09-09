using System;
using System.Collections.Generic;
using System.Media;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Drawing;
using System.ComponentModel;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid.Style;
using DevComponents.DotNetBar.ScrollBar;
using DevComponents.SuperGrid.TextMarkup;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents a Super Grid Control.
    /// </summary>
    [ToolboxItem(true), ToolboxBitmap(typeof(SuperGridControl), "SuperGridControl.png")]
    [Designer(typeof(DevComponents.SuperGrid.Design.SuperGridDesigner))]
    public class SuperGridControl : Control, INotifyPropertyChanged, IMessageFilter, IOwnerLocalize
    {
        #region Events

        #region ActiveGridChanged

        /// <summary>
        /// Occurs when the active grid changes
        /// </summary>
        [Description("Occurs when the active grid changes.")]
        public event EventHandler<GridActiveGridChangedEventArgs> ActiveGridChanged;

        #endregion

        #region CellActivated

        /// <summary>
        /// Occurs when a cell has been made the Active Cell
        /// </summary>
        [Description("Occurs when a cell has been made the Active Cell.")]
        public event EventHandler<GridCellActivatedEventArgs> CellActivated;

        #endregion

        #region CellActivating

        /// <summary>
        /// Occurs when a cell is about to be made the Active Cell
        /// </summary>
        [Description("Occurs when a cell is about to be made the Active Cell.")]
        public event EventHandler<GridCellActivatingEventArgs> CellActivating;

        #endregion

        #region CellClick

        /// <summary>
        /// Occurs when a cell has been clicked
        /// </summary>
        [Description("Occurs when a cell has been clicked.")]
        public event EventHandler<GridCellClickEventArgs> CellClick;

        #endregion

        #region CellDoubleClick

        /// <summary>
        /// Occurs when a cell has been double clicked
        /// </summary>
        [Description("Occurs when a cell has been double clicked.")]
        public event EventHandler<GridCellDoubleClickEventArgs> CellDoubleClick;

        #endregion

        #region CellInfoClick

        /// <summary>
        /// Occurs when a cell InfoImage has been clicked
        /// </summary>
        [Description("Occurs when a cell InfoImage has been clicked.")]
        public event EventHandler<GridCellClickEventArgs> CellInfoClick;

        #endregion

        #region CellInfoDoubleClick

        /// <summary>
        /// Occurs when a cell InfoImage has been double clicked
        /// </summary>
        [Description("Occurs when a cell InfoImage has been double clicked.")]
        public event EventHandler<GridCellDoubleClickEventArgs> CellInfoDoubleClick;

        #endregion

        #region CellInfoEnter

        /// <summary>
        /// Occurs when a Cell InfoImage has been entered via the mouse
        /// </summary>
        [Description("Occurs when a Cell InfoImage has been entered via the mouse.")]
        public event EventHandler<GridCellInfoEnterEventArgs> CellInfoEnter;

        #endregion

        #region CellInfoLeave

        /// <summary>
        /// Occurs when a Cell InfoImage has been exited via the mouse
        /// </summary>
        [Description("Occurs when a Cell InfoImage has been exited via the mouse.")]
        public event EventHandler<GridCellInfoLeaveEventArgs> CellInfoLeave;

        #endregion

        #region CellMouseDown

        /// <summary>
        /// Occurs when a mouse button is pressed
        /// while the mouse pointer is within a cell
        /// </summary>
        [Description("Occurs when a mouse button is pressed while the mouse pointer is within a cell.")]
        public event EventHandler<GridCellMouseEventArgs> CellMouseDown;

        #endregion

        #region CellMouseEnter

        /// <summary>
        /// Occurs when the mouse pointer enters a cell
        /// </summary>
        [Description("Occurs when the mouse pointer enters a cell.")]
        public event EventHandler<GridCellEventArgs> CellMouseEnter;

        #endregion

        #region CellMouseLeave

        /// <summary>
        /// Occurs when the mouse pointer leaves a cell
        /// </summary>
        [Description("Occurs when the mouse pointer leaves a cell.")]
        public event EventHandler<GridCellEventArgs> CellMouseLeave;

        #endregion

        #region CellMouseMove

        /// <summary>
        /// Occurs when the mouse pointer moves within a cell
        /// </summary>
        [Description("Occurs when the mouse pointer leaves a cell.")]
        public event EventHandler<GridCellMouseEventArgs> CellMouseMove;

        #endregion

        #region CellMouseUp

        /// <summary>
        /// Occurs when a mouse button is released
        /// while the mouse pointer is within a cell
        /// </summary>
        [Description("Occurs when a mouse button is released while the mouse pointer is within a cell.")]
        public event EventHandler<GridCellMouseEventArgs> CellMouseUp;

        #endregion

        #region CellValidating

        /// <summary>
        /// Occurs when a cell needs validating
        /// </summary>
        [Description("Occurs when a cell needs validating.")]
        public event EventHandler<GridCellValidatingEventArgs> CellValidating;

        #endregion

        #region CellValidated

        /// <summary>
        /// Occurs when a cell has finished validating
        /// </summary>
        [Description("Occurs when a cell has finished validating.")]
        public event EventHandler<GridCellValidatedEventArgs> CellValidated;

        #endregion

        #region Check events

        /// <summary>
        /// Occurs when a row check state is about to change
        /// </summary>
        [Description("Occurs when a row check state is about to change.")]
        public event EventHandler<GridBeforeCheckEventArgs> BeforeCheck;

        /// <summary>
        /// Occurs when a row check state has changed
        /// </summary>
        [Description("Occurs when a row check state has changed.")]
        public event EventHandler<GridAfterCheckEventArgs> AfterCheck;

        #endregion

        #region Collapse events

        /// <summary>
        /// Occurs when a row is about to collapse
        /// </summary>
        [Description("Occurs when a row is about to collapse.")]
        public event EventHandler<GridBeforeCollapseEventArgs> BeforeCollapse;

        /// <summary>
        /// Occurs when a row has just been collapsed
        /// </summary>
        [Description("Occurs when a row has just been collapsed.")]
        public event EventHandler<GridAfterCollapseEventArgs> AfterCollapse;

        #endregion

        #region CellUserFunction

        /// <summary>
        /// Occurs when a cell User function needs evaluated
        /// </summary>
        [Description("Occurs when a cell User function needs evaluated.")]
        public event EventHandler<GridCellUserFunctionEventArgs> CellUserFunction;

        #endregion

        #region CellValueChanged

        /// <summary>
        /// Occurs when any cell Value changes
        /// </summary>
        [Description("Occurs when any cell Value changes.")]
        public event EventHandler<GridCellValueChangedEventArgs> CellValueChanged;

        #endregion

        #region ColumnGrouped

        /// <summary>
        /// Occurs when a column has been Grouped
        /// </summary>
        [Description("Occurs when a column has been Grouped.")]
        public event EventHandler<GridColumnGroupedEventArgs> ColumnGrouped;

        #endregion

        #region ColumnHeaderClick

        /// <summary>
        /// Occurs when a column header has been clicked
        /// </summary>
        [Description("Occurs when a column header has been clicked.")]
        public event EventHandler<GridColumnHeaderClickEventArgs> ColumnHeaderClick;

        #endregion

        #region ColumnHeaderDoubleClick

        /// <summary>
        /// Occurs when a column header has been double clicked
        /// </summary>
        [Description("Occurs when a column header has been double clicked.")]
        public event EventHandler<GridColumnHeaderDoubleClickEventArgs> ColumnHeaderDoubleClick;

        #endregion

        #region ColumnHeaderMarkupLinkClick

        /// <summary>
        /// Occurs when a GridColumn Header has a MarkupLink that has been clicked
        /// </summary>
        [Description("Occurs when a GridColumn Header has a MarkupLink that has been clicked.")]
        public event EventHandler<GridColumnHeaderMarkupLinkClickEventArgs> ColumnHeaderMarkupLinkClick;

        #endregion

        #region ColumnMoved

        /// <summary>
        /// Occurs when a column has been moved or reordered
        /// </summary>
        [Description("Occurs when a column has been moved or reordered.")]
        public event EventHandler<GridColumnEventArgs> ColumnMoved;

        #endregion

        #region ColumnResized

        /// <summary>
        /// Occurs when a column has been resized
        /// </summary>
        [Description("Occurs when a column has been resized.")]
        public event EventHandler<GridColumnEventArgs> ColumnResized;

        #endregion

        #region ColumnRowHeaderClick

        /// <summary>
        /// Occurs when a column RowHeader has been clicked
        /// </summary>
        [Description("Occurs when a column RowHeader has been clicked.")]
        public event EventHandler<GridEventArgs> ColumnRowHeaderClick;

        #endregion

        #region ColumnRowHeaderDoubleClick

        /// <summary>
        /// Occurs when a column RowHeader has been double clicked
        /// </summary>
        [Description("Occurs when a column RowHeader has been double clicked.")]
        public event EventHandler<GridEventArgs> ColumnRowHeaderDoubleClick;

        #endregion

        #region CompareElements

        /// <summary>
        /// Occurs when the grid needs to compare 1 element with another
        /// </summary>
        [Description("Occurs when the grid needs to compare 1 element with another.")]
        public event EventHandler<GridCompareElementsEventArgs> CompareElements;

        #endregion

        #region DataBindingStart

        /// <summary>
        /// Occurs when the grid is about to start a nested binding operation
        /// </summary>
        [Description("Occurs when the grid is about to start a nested binding operation.")]
        public event EventHandler<GridDataBindingStartEventArgs> DataBindingStart;

        #endregion

        #region DataBindingComplete

        /// <summary>
        /// Occurs when a grid's data binding is complete
        /// </summary>
        [Description("Occurs when a grid's data binding is complete.")]
        public event EventHandler<GridDataBindingCompleteEventArgs> DataBindingComplete;

        #endregion

        #region DataError

        /// <summary>
        /// Occurs when an error is encountered while dealing with data
        /// </summary>
        [Description("Occurs when an error is encountered while dealing with data.")]
        public event EventHandler<GridDataErrorEventArgs> DataError;

        #endregion

        #region DataFilteringStart

        /// <summary>
        /// Occurs when the grid is about to start a data filtering operation
        /// </summary>
        [Description("Occurs when the grid is about to start a data filtering operation.")]
        public event EventHandler<GridDataFilteringStartEventArgs> DataFilteringStart;

        #endregion

        #region DataFilteringComplete

        /// <summary>
        /// Occurs when a grid's data Filtering is complete
        /// </summary>
        [Description("Occurs when a grid's data filtering is complete.")]
        public event EventHandler<GridDataFilteringCompleteEventArgs> DataFilteringComplete;

        #endregion

        #region ItemDrag

        /// <summary>
        /// Occurs when a user begins dragging an item
        /// </summary>
        [Description("Occurs when a user begins dragging an item.")]
        public event EventHandler<GridItemDragEventArgs> ItemDrag;

        #endregion

        #region Editor events

        #region BeginEdit

        /// <summary>
        /// Occurs when a modal cell edit is about to begin
        /// </summary>
        [Description("Occurs when a modal cell edit is about to begin.")]
        public event EventHandler<GridEditEventArgs> BeginEdit;

        #endregion

        #region CancelEdit

        /// <summary>
        /// Occurs when a modal cell edit has been canceled
        /// </summary>
        [Description("Occurs when a modal cell edit has been canceled.")]
        public event EventHandler<GridEditEventArgs> CancelEdit;

        #endregion

        #region CloseEdit

        /// <summary>
        /// Occurs when a modal cell edit has closed
        /// </summary>
        [Description("Occurs when a modal cell edit has closed.")]
        public event EventHandler<GridCloseEditEventArgs> CloseEdit;

        #endregion

        #region EditorValueChanged

        /// <summary>
        /// Occurs when a cell editor value has changed
        /// </summary>
        [Description("Occurs when a cell editor value has changed.")]
        public event EventHandler<GridEditEventArgs> EditorValueChanged;

        #endregion

        #region EndEdit

        /// <summary>
        /// Occurs when a modal cell edit is ending
        /// </summary>
        [Description("Occurs when a modal cell edit is ending.")]
        public event EventHandler<GridEditEventArgs> EndEdit;

        #endregion

        #region GetEditor

        /// <summary>
        /// Occurs when a cell editor is needed
        /// </summary>
        [Description("Occurs when a cell editor is needed.")]
        public event EventHandler<GridGetEditorEventArgs> GetEditor;

        #endregion

        #region GetRenderer

        /// <summary>
        /// Occurs when a cell renderer is needed
        /// </summary>
        [Description("Occurs when a cell renderer is needed.")]
        public event EventHandler<GridGetRendererEventArgs> GetRenderer;

        #endregion

        #region InitEditContext

        /// <summary>
        /// Occurs when a cell editor needs it's context initialized
        /// </summary>
        [Description("Occurs when a cell editor needs it's context initialized.")]
        public event EventHandler<GridInitEditContextEventArgs> InitEditContext;

        #endregion

        #endregion

        #region Expand events

        /// <summary>
        /// Occurs when a row is about to be expanded
        /// </summary>
        [Description("Occurs when a row is about to be expanded.")]
        public event EventHandler<GridBeforeExpandEventArgs> BeforeExpand;

        /// <summary>
        /// Occurs when a row has just been expanded
        /// </summary>
        [Description("Occurs when a row has just been expanded.")]
        public event EventHandler<GridAfterExpandEventArgs> AfterExpand;

        #endregion

        #region Filter events

        #region FilterBeginEdit

        /// <summary>
        /// Occurs when a column filter edit is about to begin
        /// </summary>
        [Description("Occurs when a column filter edit is about to begin.")]
        public event EventHandler<GridFilterBeginEditEventArgs> FilterBeginEdit;

        #endregion

        #region FilterCancelEdit

        /// <summary>
        /// Occurs when a column filter edit has been canceled
        /// </summary>
        [Description("Occurs when a column filter edit has been canceled.")]
        public event EventHandler<GridFilterCancelEditEventArgs> FilterCancelEdit;

        #endregion

        #region FilterColumnError

        /// <summary>
        /// Occurs when a column filter error has occurred
        /// </summary>
        [Description("Occurs when a column filter error has ocurred.")]
        public event EventHandler<GridFilterColumnErrorEventArgs> FilterColumnError;

        #endregion

        #region FilterEditValueChanged

        /// <summary>
        /// Occurs when a filter edit value has changed
        /// </summary>
        [Description("Occurs when a filter edit value has changed.")]
        public event EventHandler<GridFilterEditValueChangedEventArgs> FilterEditValueChanged;

        #endregion

        #region FilterEndEdit

        /// <summary>
        /// Occurs when a column filter edit has ended
        /// </summary>
        [Description("Occurs when a column filter edit has ended.")]
        public event EventHandler<GridFilterEndEditEventArgs> FilterEndEdit;

        #endregion

        #region FilterHeaderClick

        /// <summary>
        /// Occurs when a Filter header has been clicked
        /// </summary>
        [Description("Occurs when a Filter header has been clicked.")]
        public event EventHandler<GridFilterHeaderClickEventArgs> FilterHeaderClick;

        #endregion

        #region FilterHelpClosing

        /// <summary>
        /// Occurs when a filter expression help window is about to close
        /// </summary>
        [Description("Occurs when a filter expression help window is about to close.")]
        public event EventHandler<GridFilterHelpClosingEventArgs> FilterHelpClosing;

        #endregion

        #region FilterHelpOpening

        /// <summary>
        /// Occurs when a filter expression help window is about to open
        /// </summary>
        [Description("Occurs when a filter expression help window is about to open.")]
        public event EventHandler<GridFilterHelpOpeningEventArgs> FilterHelpOpening;

        #endregion

        #region FilterItemsLoaded

        /// <summary>
        /// Occurs following the loading of the items in the
        /// ComboBox, when a ComboBox filter edit is about to begin
        /// </summary>
        [Description("Occurs following the loading of the items in the ComboBox, when a ComboBox filter edit is about to begin.")]
        public event EventHandler<GridFilterItemsLoadedEventArgs> FilterItemsLoaded;

        #endregion

        #region FilterLoadItems

        /// <summary>
        /// Occurs when a ComboBox filter edit is about to begin
        /// and the items in the comboBox need to be loaded
        /// </summary>
        [Description("Occurs when a ComboBox filter edit is about to begin and the items in the comboBox need to be loaded.")]
        public event EventHandler<GridFilterLoadItemsEventArgs> FilterLoadItems;

        #endregion

        #region FilterLoadUserData

        /// <summary>
        /// Occurs when user defined Filter data needs to be loaded
        /// </summary>
        [Description("Occurs when user defined Filter data needs to be loaded.")]
        public event EventHandler<GridFilterLoadUserDataEventArgs> FilterLoadUserData;

        #endregion

        #region FilterPopupClosing

        /// <summary>
        /// Occurs when a column filter menu is closing
        /// </summary>
        [Description("Occurs when a column filter menu is closing.")]
        public event EventHandler<GridFilterPopupClosingEventArgs> FilterPopupClosing;

        #endregion

        #region FilterPopupLoad

        /// <summary>
        /// Occurs when a column filter menu needs loaded
        /// </summary>
        [Description("Occurs when a column filter menu needs loaded.")]
        public event EventHandler<GridFilterPopupLoadEventArgs> FilterPopupLoad;

        #endregion

        #region FilterPopupLoaded

        /// <summary>
        /// Occurs after a column filter menu has been loaded
        /// </summary>
        [Description("Occurs after a column filter menu has been loaded.")]
        public event EventHandler<GridFilterPopupLoadedEventArgs> FilterPopupLoaded;

        #endregion

        #region FilterPopupOpening

        /// <summary>
        /// Occurs when a column filter menu is about to open
        /// </summary>
        [Description("Occurs when a column filter menu is about to open.")]
        public event EventHandler<GridFilterPopupOpeningEventArgs> FilterPopupOpening;

        #endregion

        #region FilterPopupValueChanged

        /// <summary>
        /// Occurs when a filter popup value has changed
        /// </summary>
        [Description("Occurs when a filter popup value has changed.")]
        public event EventHandler<GridFilterPopupValueChangedEventArgs> FilterPopupValueChanged;

        #endregion

        #region FilterRowError

        /// <summary>
        /// Occurs when a row filter error has occurred
        /// </summary>
        [Description("Occurs when a row filter error has occurred.")]
        public event EventHandler<GridFilterRowErrorEventArgs> FilterRowError;

        #endregion

        #region FilterRowHeaderClick

        /// <summary>
        /// Occurs when a Filter RowHeader has been clicked
        /// </summary>
        [Description("Occurs when a Filter RowHeader has been clicked.")]
        public event EventHandler<GridCancelEventArgs> FilterRowHeaderClick;

        #endregion

        #region FilterStoreUserData

        /// <summary>
        /// Occurs when user defined Filter data needs to be stored
        /// </summary>
        [Description("Occurs when user defined Filter data needs to be loaded.")]
        public event EventHandler<GridFilterStoreUserDataEventArgs> FilterStoreUserData;

        #endregion

        #region FilterUserFunction

        /// <summary>
        /// Occurs when a Filter User function needs evaluated
        /// </summary>
        [Description("Occurs when a Filter User function needs evaluated.")]
        public event EventHandler<GridFilterUserFunctionEventArgs> FilterUserFunction;

        #endregion

        #region GetFilterEditType

        /// <summary>
        /// Occurs when the column filter edit type is needed
        /// </summary>
        [Description("Occurs when the column filter edit type is needed.")]
        public event EventHandler<GridGetFilterEditTypeEventArgs> GetFilterEditType;

        #endregion

        #endregion

        #region GetCellFormattedValue

        /// <summary>
        /// Occurs when a Modal cell Value needs formatted
        /// </summary>
        [Description("Occurs when a Modal cell Value needs formatted.")]
        public event EventHandler<GridGetCellFormattedValueEventArgs> GetCellFormattedValue;

        #endregion

        #region GetCellStyle

        /// <summary>
        /// Occurs when a Cell Style is needed
        /// </summary>
        [Description("Occurs when a Cell Style is needed.")]
        public event EventHandler<GridGetCellStyleEventArgs> GetCellStyle;

        #endregion

        #region GetCellValue

        /// <summary>
        /// Occurs when a Cell Value is needed
        /// </summary>
        [Description("Occurs when a Cell Value is needed.")]
        public event EventHandler<GridGetCellValueEventArgs> GetCellValue;

        #endregion

        #region GetColumnHeaderRowHeaderStyle

        /// <summary>
        /// Occurs when a ColumnHeader RowHeader style is needed
        /// </summary>
        [Description("Occurs when a ColumnHeader RowHeader style is needed.")]
        public event EventHandler<GridGetColumnHeaderRowHeaderStyleEventArgs> GetColumnHeaderRowHeaderStyle;

        #endregion

        #region GetColumnHeaderStyle

        /// <summary>
        /// Occurs when a ColumnHeader style is needed
        /// </summary>
        [Description("Occurs when a ColumnHeader style is needed.")]
        public event EventHandler<GridGetColumnHeaderStyleEventArgs> GetColumnHeaderStyle;

        #endregion

        #region GetColumnHeaderToolTip

        /// <summary>
        /// Occurs when a ColumnHeader ToolTip is needed
        /// </summary>
        [Description("Occurs when a ColumnHeader ToolTip is needed.")]
        public event EventHandler<GridGetColumnHeaderToolTipEventArgs> GetColumnHeaderToolTip;

        #endregion

        #region GetFilterRowStyle

        /// <summary>
        /// Occurs when a FilterRow style is needed
        /// </summary>
        [Description("Occurs when a FilterRow style is needed.")]
        public event EventHandler<GridGetFilterRowStyleEventArgs> GetFilterRowStyle;

        #endregion

        #region GetFilterColumnHeaderStyle

        /// <summary>
        /// Occurs when a Filter ColumnHeader style is needed
        /// </summary>
        [Description("Occurs when a Filter ColumnHeader style is needed.")]
        public event EventHandler<GridGetFilterColumnHeaderStyleEventArgs> GetFilterColumnHeaderStyle;

        #endregion

        #region ConfigureGroupBox

        /// <summary>
        /// Occurs when a GroupBox Size is needed
        /// </summary>
        [Description("Occurs when a GroupBox needs configured.")]
        public event EventHandler<GridConfigureGroupBoxEventArgs> ConfigureGroupBox;

        #endregion

        #region GetGroupDetailRows

        /// <summary>
        /// Occurs when a list of group detail rows is needed
        /// </summary>
        [Description("Occurs when a list of group detail rows is needed.")]
        public event EventHandler<GridGetGroupDetailRowsEventArgs> GetGroupDetailRows;

        #endregion

        #region GetGroupHeaderStyle

        /// <summary>
        /// Occurs when an GroupHeader style is needed
        /// </summary>
        [Description("Occurs when a GroupHeader style is needed.")]
        public event EventHandler<GridGetGroupHeaderStyleEventArgs> GetGroupHeaderStyle;

        #endregion

        #region GetPanelStyle

        /// <summary>
        /// Occurs when a GridPanel style is needed
        /// </summary>
        [Description("Occurs when a GridPanel style is needed.")]
        public event EventHandler<GridGetPanelStyleEventArgs> GetPanelStyle;

        #endregion

        #region GetGroupId

        /// <summary>
        /// Occurs when an element Group identifier is needed
        /// </summary>
        [Description("Occurs when an element Group identifier is needed.")]
        public event EventHandler<GridGetGroupIdEventArgs> GetGroupId;

        #endregion

        #region GetRowHeaderStyle

        /// <summary>
        /// Occurs when a row RowHeader style is needed
        /// </summary>
        [Description("Occurs when a row RowHeader style is needed.")]
        public event EventHandler<GridGetRowHeaderStyleEventArgs> GetRowHeaderStyle;

        #endregion

        #region GetRowHeaderText

        /// <summary>
        /// Occurs when a row's header text is needed
        /// </summary>
        [Description("Occurs when a row's header text is needed.")]
        public event EventHandler<GridGetRowHeaderTextEventArgs> GetRowHeaderText;

        #endregion

        #region GetRowStyle

        /// <summary>
        /// Occurs when a row style is needed
        /// </summary>
        [Description("Occurs when a row style is needed.")]
        public event EventHandler<GridGetRowStyleEventArgs> GetRowStyle;

        #endregion

        #region GetTextRowStyle

        /// <summary>
        /// Occurs when a GridTextRow (Header, footer, etc) style is needed
        /// </summary>
        [Description("Occurs when a GridTextRow (Header, footer, etc) style is needed.")]
        public event EventHandler<GridGetTextRowStyleEventArgs> GetTextRowStyle;

        #endregion

        #region GroupHeaderClick

        /// <summary>
        /// Occurs when a Group Header has been clicked
        /// </summary>
        [Description("Occurs when a Group Header has been clicked.")]
        public event EventHandler<GridGroupHeaderClickEventArgs> GroupHeaderClick;

        #endregion

        #region GroupHeaderDoubleClick

        /// <summary>
        /// Occurs when a Group Header has been double clicked
        /// </summary>
        [Description("Occurs when a Group Header has been double clicked.")]
        public event EventHandler<GridGroupHeaderDoubleClickEventArgs> GroupHeaderDoubleClick;

        #endregion

        #region LoadVirtualRow

        /// <summary>
        /// Occurs when a virtual row needs loaded
        /// </summary>
        [Description("Occurs when a virtual row needs loaded.")]
        public event EventHandler<GridVirtualRowEventArgs> LoadVirtualRow;

        #endregion

        #region LocalizeString

        /// <summary>
        /// Occurs when the SuperGrid is looking for translated text for one of the internal text that are
        /// displayed on menus, toolbars and customize forms. You need to set Handled=true if you want
        /// your custom text to be used instead of the built-in system value.
        /// </summary>
        public event DotNetBarManager.LocalizeStringEventHandler LocalizeString;

        #endregion

        #region NoRowsMarkupLinkClick

        /// <summary>
        /// Occurs when a GridPanel's NoRowsText has a MarkupLink that has been clicked
        /// </summary>
        [Description("Occurs when a GridPanel's NoRowsText has a MarkupLink that has been clicked.")]
        public event EventHandler<GridNoRowsMarkupLinkClickEventArgs> NoRowsMarkupLinkClick;

        #endregion

        #region GridPreviewKeyDown

        ///<summary>
        /// GridPreviewKeyDown
        ///</summary>
        [Description("PreviewKeyDown event, with the ability to specify that the key has been handled.")]
        public event EventHandler<GridPreviewKeyDownEventArgs> GridPreviewKeyDown;

        #endregion

        #region PreviewKeyDown

        ///<summary>
        /// PreviewKeyDown
        ///</summary>
        [Description("Occurs before the System.Windows.Forms.Control.KeyDown event when a key is pressed while focus is on this control.")]
        public new event EventHandler<PreviewKeyDownEventArgs> PreviewKeyDown;

        #endregion

        #region RefreshFilter

        /// <summary>
        /// Occurs when the Virtual row filter needs refreshed
        /// </summary>
        [Description("Occurs when the Virtual row filter needs refreshed.")]
        public event EventHandler<GridRefreshFilterEventArgs> RefreshFilter;

        #endregion

        #region Render events

        #region PostRenderCell

        /// <summary>
        /// Occurs after a cell has been rendered
        /// </summary>
        [Description("Occurs after a cell has been rendered.")]
        public event EventHandler<GridPostRenderCellEventArgs> PostRenderCell;

        #endregion

        #region PreRenderCell

        /// <summary>
        /// Occurs when a cell is about to be rendered
        /// </summary>
        [Description("Occurs when a cell is about to be rendered.")]
        public event EventHandler<GridPreRenderCellEventArgs> PreRenderCell;

        #endregion

        #region PostRenderPanelRow

        /// <summary>
        /// Occurs when a nested Panel Row has been rendered
        /// </summary>
        [Description("Occurs when a nested Panel Row has been rendered.")]
        public event EventHandler<GridPostRenderRowEventArgs> PostRenderPanelRow;

        #endregion

        #region PreRenderPanelRow

        /// <summary>
        /// Occurs when a nested Panel Row is about to be rendered
        /// </summary>
        [Description("Occurs when a nested Panel Row is about to be rendered.")]
        public event EventHandler<GridPreRenderRowEventArgs> PreRenderPanelRow;

        #endregion

        #region PostRenderRow

        /// <summary>
        /// Occurs after a row has been rendered
        /// </summary>
        [Description("Occurs after a row has been rendered.")]
        public event EventHandler<GridPostRenderRowEventArgs> PostRenderRow;

        #endregion

        #region PreRenderRow

        /// <summary>
        /// Occurs when a row is about to be rendered
        /// </summary>
        [Description("Occurs when a row is about to be rendered.")]
        public event EventHandler<GridPreRenderRowEventArgs> PreRenderRow;

        #endregion

        #region PostRenderTextRow

        /// <summary>
        /// Occurs after a TextRow (Caption, Footer, etc) has been rendered
        /// </summary>
        [Description("Occurs after a TextRow has been rendered.")]
        public event EventHandler<GridPostRenderTextRowEventArgs> PostRenderTextRow;

        #endregion

        #region PreRenderTextRow

        /// <summary>
        /// Occurs when a TextRow (Caption, Footer, etc) is about to be rendered
        /// </summary>
        [Description("Occurs when a TextRow is about to be rendered.")]
        public event EventHandler<GridPreRenderTextRowEventArgs> PreRenderTextRow;

        #endregion

        #region PostRenderGroupHeader

        /// <summary>
        /// Occurs after a Group header has been rendered
        /// </summary>
        [Description("Occurs after a Group header has been rendered.")]
        public event EventHandler<GridPostRenderRowEventArgs> PostRenderGroupHeader;

        #endregion

        #region PreRenderGroupHeader

        /// <summary>
        /// Occurs when a Group header is about to be rendered
        /// </summary>
        [Description("Occurs when a Group header is about to be rendered.")]
        public event EventHandler<GridPreRenderRowEventArgs> PreRenderGroupHeader;

        #endregion

        #region PostRenderColumnHeader

        /// <summary>
        /// Occurs after a Column header has been rendered
        /// </summary>
        [Description("Occurs after a Column header has been rendered.")]
        public event EventHandler<GridPostRenderColumnHeaderEventArgs> PostRenderColumnHeader;

        #endregion

        #region PreRenderColumnHeader

        /// <summary>
        /// Occurs when a Column header is about to be rendered
        /// </summary>
        [Description("Occurs when a Column header is about to be rendered.")]
        public event EventHandler<GridPreRenderColumnHeaderEventArgs> PreRenderColumnHeader;

        #endregion

        #region PostRenderFilterPopupGripBar

        /// <summary>
        /// Occurs after a FilterPopup GripBar has been rendered
        /// </summary>
        [Description("Occurs after a FilterPopup GripBar has been rendered.")]
        public event EventHandler<GridPostRenderFilterPopupGripBarEventArgs> PostRenderFilterPopupGripBar;

        #endregion

        #region PreRenderFilterPopupGripBar

        /// <summary>
        /// Occurs when a FilterPopup GripBar is about to be rendered
        /// </summary>
        [Description("Occurs when a FilterPopup GripBar is about to be rendered.")]
        public event EventHandler<GridPreRenderFilterPopupGripBarEventArgs> PreRenderFilterPopupGripBar;

        #endregion

        #region PostRenderFilterRow

        /// <summary>
        /// Occurs after a Filter Row has been rendered
        /// </summary>
        [Description("Occurs after a Filter Row has been rendered.")]
        public event EventHandler<GridPostRenderFilterRowEventArgs> PostRenderFilterRow;

        #endregion

        #region PreRenderFilterRow

        /// <summary>
        /// Occurs when a Filter Row is about to be rendered
        /// </summary>
        [Description("Occurs when a Filter Row is about to be rendered.")]
        public event EventHandler<GridPreRenderFilterRowEventArgs> PreRenderFilterRow;

        #endregion

        #region PostRenderGroupBox

        /// <summary>
        /// Occurs after a GroupBox has been rendered
        /// </summary>
        [Description("Occurs after a GroupBox has been rendered.")]
        public event EventHandler<GridPostRenderGroupBoxEventArgs> PostRenderGroupBox;

        #endregion

        #region PreRenderGroupBox

        /// <summary>
        /// Occurs when a GroupBox is about to be rendered
        /// </summary>
        [Description("Occurs when a GroupBox is about to be rendered.")]
        public event EventHandler<GridPreRenderGroupBoxEventArgs> PreRenderGroupBox;

        #endregion

        #region PostRenderGroupBoxConnector

        /// <summary>
        /// Occurs after a GroupBox Connector been rendered
        /// </summary>
        [Description("Occurs after a GroupBox Connectorhas been rendered.")]
        public event EventHandler<GridPostRenderGroupBoxConnectorEventArgs> PostRenderGroupBoxConnector;

        #endregion

        #region PreRenderGroupBoxConnector

        /// <summary>
        /// Occurs when a GroupBox Connector is about to be rendered
        /// </summary>
        [Description("Occurs when a GroupBox Connector is about to be rendered.")]
        public event EventHandler<GridPreRenderGroupBoxConnectorEventArgs> PreRenderGroupBoxConnector;

        #endregion

        #endregion

        #region Row events

        #region RowActivated

        /// <summary>
        /// Occurs when a row has been made the Active Row
        /// </summary>
        [Description("Occurs when a row has been made the Active Row.")]
        public event EventHandler<GridRowActivatedEventArgs> RowActivated;

        #endregion

        #region RowActivating

        /// <summary>
        /// Occurs when a row is about to be made the Active Row
        /// </summary>
        [Description("Occurs when a row is about to be made the Active Row.")]
        public event EventHandler<GridRowActivatingEventArgs> RowActivating;

        #endregion

        #region RowAdded

        /// <summary>
        /// Occurs when a row has been added
        /// </summary>
        [Description("Occurs when a row has been added.")]
        public event EventHandler<GridRowAddedEventArgs> RowAdded;

        #endregion

        #region RowAdding

        /// <summary>
        /// Occurs when a row is about to be added
        /// </summary>
        [Description("Occurs when a row is about to be added.")]
        public event EventHandler<GridRowAddingEventArgs> RowAdding;

        #endregion

        #region RowDeleted

        /// <summary>
        /// Occurs when a row has been deleted
        /// </summary>
        [Description("Occurs when a row has been deleted.")]
        public event EventHandler<GridRowDeletedEventArgs> RowDeleted;

        #endregion

        #region RowDeleting

        /// <summary>
        /// Occurs when a row is about to be deleted
        /// </summary>
        [Description("Occurs when a row is about to be deleted.")]
        public event EventHandler<GridRowDeletingEventArgs> RowDeleting;

        #endregion

        #region GetDetailRowHeight

        /// <summary>
        /// Occurs when a row's 'detail' height is needed
        /// </summary>
        [Description("Occurs when a row's 'detail' height is needed.")]
        public event EventHandler<GridGetDetailRowHeightEventArgs> GetDetailRowHeight;

        #endregion

        #region RowClick

        /// <summary>
        /// Occurs when a row has been clicked
        /// </summary>
        [Description("Occurs when a row has been clicked.")]
        public event EventHandler<GridRowClickEventArgs> RowClick;

        #endregion

        #region RowDoubleClick

        /// <summary>
        /// Occurs when a row has been double clicked
        /// </summary>
        [Description("Occurs when a row has been double clicked.")]
        public event EventHandler<GridRowDoubleClickEventArgs> RowDoubleClick;

        #endregion

        #region RowHeaderClick

        /// <summary>
        /// Occurs when a row header has been clicked
        /// </summary>
        [Description("Occurs when a row header has been clicked.")]
        public event EventHandler<GridRowHeaderClickEventArgs> RowHeaderClick;

        #endregion

        #region RowHeaderDoubleClick

        /// <summary>
        /// Occurs when a row header has been double clicked
        /// </summary>
        [Description("Occurs when a row header has been double clicked.")]
        public event EventHandler<GridRowHeaderDoubleClickEventArgs> RowHeaderDoubleClick;

        #endregion

        #region RowHeaderResized

        /// <summary>
        /// Occurs when the grid Row Header has been resized
        /// </summary>
        [Description("Occurs when the grid Row Header has been resized.")]
        public event EventHandler<GridEventArgs> RowHeaderResized;

        #endregion

        #region RowInfoClick

        /// <summary>
        /// Occurs when a row InfoImage has been clicked
        /// </summary>
        [Description("Occurs when a row InfoImage has been clicked.")]
        public event EventHandler<GridRowClickEventArgs> RowInfoClick;

        #endregion

        #region RowInfoDoubleClick

        /// <summary>
        /// Occurs when a row InfoImage has been double clicked
        /// </summary>
        [Description("Occurs when a row InfoImage has been double clicked.")]
        public event EventHandler<GridRowDoubleClickEventArgs> RowInfoDoubleClick;

        #endregion

        #region RowInfoEnter

        /// <summary>
        /// Occurs when a row InfoImage has been entered via the mouse
        /// </summary>
        [Description("Occurs when a row InfoImage has been entered via the mouse.")]
        public event EventHandler<GridRowInfoEnterEventArgs> RowInfoEnter;

        #endregion

        #region RowInfoLeave

        /// <summary>
        /// Occurs when a row InfoImage has been exited via the mouse
        /// </summary>
        [Description("Occurs when a row InfoImage has been exited via the mouse.")]
        public event EventHandler<GridRowInfoLeaveEventArgs> RowInfoLeave;

        #endregion

        #region RowMarkedDirty

        /// <summary>
        /// Occurs when a cell editor marks a row as Dirty
        /// </summary>
        [Description("Occurs when a cell editor marks a row as Dirty.")]
        public event EventHandler<GridEditEventArgs> RowMarkedDirty;

        #endregion

        #region RowMouseDown

        /// <summary>
        /// Occurs when a mouse button is pressed
        /// while the mouse pointer is within a Row
        /// </summary>
        [Description("Occurs when a mouse button is pressed while the mouse pointer is within a Row.")]
        public event EventHandler<GridRowMouseEventArgs> RowMouseDown;

        #endregion

        #region RowMouseEnter

        /// <summary>
        /// Occurs when the mouse pointer enters a Row
        /// </summary>
        [Description("Occurs when the mouse pointer enters a Row.")]
        public event EventHandler<GridRowEventArgs> RowMouseEnter;

        #endregion

        #region RowMouseLeave

        /// <summary>
        /// Occurs when the mouse pointer leaves a Row
        /// </summary>
        [Description("Occurs when the mouse pointer leaves a Row.")]
        public event EventHandler<GridRowEventArgs> RowMouseLeave;

        #endregion

        #region RowMouseMove

        /// <summary>
        /// Occurs when the mouse pointer moves within a Row
        /// </summary>
        [Description("Occurs when the mouse pointer leaves a Row.")]
        public event EventHandler<GridRowMouseEventArgs> RowMouseMove;

        #endregion

        #region RowMouseUp

        /// <summary>
        /// Occurs when a mouse button is released
        /// while the mouse pointer is within a Row
        /// </summary>
        [Description("Occurs when a mouse button is released while the mouse pointer is within a Row.")]
        public event EventHandler<GridRowMouseEventArgs> RowMouseUp;

        #endregion

        #region RowMoved

        /// <summary>
        /// Occurs when a row has been moved or reordered
        /// </summary>
        [Description("Occurs when a row has been moved or reordered.")]
        public event EventHandler<GridRowMovedEventArgs> RowMoved;

        #endregion

        #region RowMoving

        /// <summary>
        /// Occurs when a row is about to be moved or reordered
        /// </summary>
        [Description("Occurs when a row is about to be moved or reordered.")]
        public event EventHandler<GridRowMovingEventArgs> RowMoving;

        #endregion

        #region RowResized

        /// <summary>
        /// Occurs when a row has been resized
        /// </summary>
        [Description("Occurs when a row has been resized.")]
        public event EventHandler<GridRowEventArgs> RowResized;

        #endregion

        #region RowRestored

        /// <summary>
        /// Occurs when a deleted row has been restored (undeleted)
        /// </summary>
        [Description("Occurs when a deleted row has been restored (undeleted).")]
        public event EventHandler<GridRowRestoredEventArgs> RowRestored;

        #endregion

        #region RowRestoring

        /// <summary>
        /// Occurs when a deleted row is about to be restored (undeleted)
        /// </summary>
        [Description("Occurs when a deleted row is about to be restored (undeleted).")]
        public event EventHandler<GridRowRestoringEventArgs> RowRestoring;

        #endregion

        #region RowSetDefaultValues

        /// <summary>
        /// Occurs when a user enters the Insertion Row
        /// or presses the 'insert' key to add a new row,
        /// permitting default values to be set for each cell
        /// </summary>
        [Description("Occurs when a user enters the Insertion Row or presses the 'insert' key to add a new row, permitting default values to be set for each cell.")]
        public event EventHandler<GridRowSetDefaultValuesEventArgs> RowSetDefaultValues;

        #endregion

        #region RowsGrouped

        /// <summary>
        /// Occurs when the grid Rows have been grouped (or ungrouped)
        /// </summary>
        [Description("Occurs when the grids Row have been grouped (or ungrouped).")]
        public event EventHandler<GridEventArgs> RowsGrouped;

        #endregion

        #region RowsPurged

        /// <summary>
        /// Occurs when grid rows have been purged
        /// </summary>
        [Description("Occurs when grid rows have been purged.")]
        public event EventHandler<GridRowEventArgs> RowsPurged;

        #endregion

        #region RowsPurging

        /// <summary>
        /// Occurs when grid rows are about to be purged
        /// </summary>
        [Description("Occurs when grid rows are about to be purged.")]
        public event EventHandler<GridRowCancelEventArgs> RowsPurging;

        #endregion

        #region RowsSorting

        /// <summary>
        /// Occurs when the grid Rows are about to be sorted
        /// </summary>
        [Description("Occurs when the grid Rows are about to be sorted.")]
        public event EventHandler<GridCancelEventArgs> RowsSorting;

        #endregion

        #region RowsSorted

        /// <summary>
        /// Occurs when the grid Rows have been sorted
        /// </summary>
        [Description("Occurs when the grid Rows have been sorted.")]
        public event EventHandler<GridEventArgs> RowsSorted;

        #endregion

        #region RowValidating

        /// <summary>
        /// Occurs when a row needs validating
        /// </summary>
        [Description("Occurs when a row needs validating.")]
        public event EventHandler<GridRowValidatingEventArgs> RowValidating;

        #endregion

        #region RowValidated

        /// <summary>
        /// Occurs after a row has been validated
        /// </summary>
        [Description("Occurs after a row has been validated.")]
        public event EventHandler<GridRowValidatedEventArgs> RowValidated;

        #endregion

        #endregion

        #region Scroll

        /// <summary>
        /// Occurs when the Horizontal or Vertical scrollbar has been scrolled
        /// </summary>
        [Description("Occurs when the Horizontal or Vertical scrollbar has been scrolled.")]
        public event EventHandler<GridScrollEventArgs> Scroll;

        #endregion

        #region ScrollMin

        /// <summary>
        /// Occurs when the Horizontal or Vertical
        /// scrollbar has been scrolled to the Minimum and released
        /// </summary>
        [Description("Occurs when the Horizontal or Vertical scrollbar has been scrolled to the Minimum and released.")]
        public event EventHandler<GridScrollEventArgs> ScrollMin;

        #endregion

        #region ScrollMax

        /// <summary>
        /// Occurs when the Horizontal or Vertical scrollbar has been scrolled to the Maximum and released
        /// </summary>
        [Description("Occurs when the Horizontal or Vertical scrollbar has been scrolled to the Maximum and released.")]
        public event EventHandler<GridScrollEventArgs> ScrollMax;

        #endregion

        #region SelectionChanged

        /// <summary>
        /// Occurs when the selected items in the grid has changed
        /// </summary>
        [Description("Occurs when the selected items in the grid has changed.")]
        public event EventHandler<GridEventArgs> SelectionChanged;

        #endregion

        #region SortChanged

        /// <summary>
        /// Occurs when the grid sort order has changed
        /// </summary>
        [Description("Occurs when the grid sort order has changed.")]
        public event EventHandler<GridEventArgs> SortChanged;

        #endregion

        #region StoreVirtualRow

        /// <summary>
        /// Occurs when a virtual row has changed and
        /// it's contents need stored
        /// </summary>
        [Description("Occurs when a virtual row has changed and it's contents need stored.")]
        public event EventHandler<GridVirtualRowEventArgs> StoreVirtualRow;

        #endregion

        #region TextRowClick

        /// <summary>
        /// Occurs when a GridTextRow (Title, footer, ...) has been clicked
        /// </summary>
        [Description("Occurs when a GridTextRow (Title, footer, ...) has been clicked.")]
        public event EventHandler<GridTextRowEventArgs> TextRowClick;

        #endregion

        #region TextRowHeaderClick

        /// <summary>
        /// Occurs when a GridTextRow (Title, footer, ...) Row Header has been clicked
        /// </summary>
        [Description("Occurs when a GridTextRow (Title, footer, ...) Row Header has been clicked.")]
        public event EventHandler<GridTextRowEventArgs> TextRowHeaderClick;

        #endregion

        #region TextRowMarkupLinkClick

        /// <summary>
        /// Occurs when a GridTextRow (Title, footer, ...) has a MarkupLink that has been clicked
        /// </summary>
        [Description("Occurs when a GridTextRow (Title, footer, ...) has a MarkupLink that has been clicked.")]
        public event EventHandler<GridTextRowMarkupLinkClickEventArgs> TextRowMarkupLinkClick;

        #endregion

        #endregion

        #region Constants

        const int WmSetFocus = 0x7;

        const int WmKeyDown = 0x100;
        const int WmKeyUp = 0x101;

        const int WmMouseMove = 0x200;
        const int WmMouseDown = 0x201;
        const int WmMouseWheel = 0x20a;

        const int InputKeyboard = 1;

        #endregion

        #region DllImports

        #region SendInput

        #region SendInput32

        [StructLayout(LayoutKind.Explicit)]
        struct Input32
        {
            [FieldOffset(0)]
            public int type;

            [FieldOffset(4)]
            public MouseInput mi;

            [FieldOffset(4)]
            public KeyBoardInput ki;

            [FieldOffset(4)]
            public HardwareInput hi;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct KeyBoardInput
        {
            private ushort vk;
            private ushort scan;
            private uint flags;
            private uint time;
            private IntPtr extraInfo;

            public KeyBoardInput(ushort key)
            {
                vk = key;

                scan = 0;
                time = 0;
                extraInfo = IntPtr.Zero;
                flags = 0;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        struct MouseInput
        {
            public int dx;
            public int dy;
            public uint mouseData;
            public uint dwFlags;
            public uint time;
            public IntPtr dwExtraInfo;
        }

        [StructLayout(LayoutKind.Sequential)]
        struct HardwareInput
        {
            public uint uMsg;
            public ushort wParamL;
            public ushort wParamH;
        }

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input32[] pInputs, int cbSize);

        #endregion

        #region SendInput64

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        internal struct Input64
        {
            [FieldOffset(0)]
            public uint type;

            [FieldOffset(8)]
            public KeyboardInput64 ki;
        }

        [StructLayout(LayoutKind.Explicit, Pack = 1)]
        internal struct KeyboardInput64
        {
            [FieldOffset(0)]
            public ushort vk;

            [FieldOffset(2)]
            public ushort scan;

            [FieldOffset(4)]
            public uint flags;

            [FieldOffset(12)]
            public uint time;

            [FieldOffset(20)]
            public uint extraInfo1;

            [FieldOffset(28)]
            public uint extraInfo2;

            public KeyboardInput64(ushort key)
            {
                vk = key;

                scan = 0;
                time = 0;
                extraInfo1 = 0;
                extraInfo2 = 0;
                flags = 0;
            }
        } 

        [DllImport("user32.dll", SetLastError = true)]
        private static extern uint SendInput(uint nInputs, Input64[] pInputs, int cbSize);

        #endregion

        #endregion

        #endregion

        #region Private variables

        private readonly GridPanel _PrimaryGrid;

        private HScrollBarAdv _HScrollBar;
        private VScrollBarAdv _VScrollBar;

        private int _HScrollOffset;
        private int _VScrollOffset;

        private bool _HScrollBarEnabled;
        private bool _VScrollBarEnabled;

        private Rectangle _ViewRect = Rectangle.Empty;
        private Rectangle _CViewRect = Rectangle.Empty;

        private GridElement _CapturedItem;

        private Timer _AutoScrollTimer;
        private AutoScrollEnable _AutoScrollEnable;
        private Rectangle _ScrollRect;

        private bool _PostMouseMove;
        private int _BoundsUpdateCount;
        private int _BeginUpdateCount;
        private Cursor _GridCursor = Cursors.Default;

        private GridCell _EditorCell;
        private GridCell _NonModalEditorCell;
        private IGridCellEditControl _ActiveEditor;
        private IGridCellEditControl _ActiveNonModalEditor;
        private FilterPanel _ActiveFilterPanel;

        private GridPanel _ActiveGrid;
        private GridContainer _ActiveRow;
        private GridCell _ActiveCell;
        private GridElement _LastProcessedItem;
        private int _LastCellIndex = -1;
        private GridCell _LastActiveCell;

        private string _ToolTipText;
        private System.Windows.Forms.ToolTip _ToolTip;

        private StyleType _SizingStyle = StyleType.NotSet;
        private DefaultVisualStyles _DefaultVisualStyles;
        private DefaultVisualStyles _BaseVisualStyles;

        private int _StyleUpdateCount = 1;
        private int _StyleMouseOverUpdateCount = 1;
        private int _StyleSelectedUpdateCount = 1;
        private int _StyleSelectedMouseOverUpdateCount = 1;
        private int _ArrangeLayoutCount = 1;

        private bool _NeedToUpdateIndicees;
        private int _IndiceesUpdateCount;

        private GridElement _DesignerElement;

        private Keys _LastKeyDown;
        private Keys _LastModifierKeys;

        private TabSelection _TabSelection = TabSelection.Cell;

        private bool _KeyPressSent;
        private bool _IsInputKey;

        private string _FilterShowAllString = "Show all";
        private string _FilterShowNullString = "Show null";
        private string _FilterShowNotNullString = "Show not null";
        private string _FilterCustomString = "Custom";

        private string _FilterApplyString = "Apply";
        private string _FilterOkString = "OK";
        private string _FilterCancelString = "Cancel";
        private string _FilterCloseString = "Close";

        private bool _FilterColorizeCustomExpr = true;
        private bool _ShowCustomFilterHelp = true;
        private bool _FilterUseExtendedCustomDialog;

        private int _FilterMaxDropDownHeight = 300;

        private ExpressionColors _FilterExprColors = new ExpressionColors();
        private PopupControl _PopupControl;

        private bool _LocalizedStringsLoaded;

        private ExpandButtonType _ExpandButtonType = ExpandButtonType.NotSet;

        private Cursor _DefaultCursor = Cursors.Default;

        private bool _HScrollBarVisible = true;
        private bool _VScrollBarVisible = true;

        #endregion

        /// <summary>
        /// Initializes a new instance of the SuperGridControl class.
        /// </summary>
        public SuperGridControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint |
                ControlStyles.Opaque | ControlStyles.ResizeRedraw |
                ControlStyles.OptimizedDoubleBuffer | ControlStyles.SupportsTransparentBackColor, true);

            _PrimaryGrid = new GridPanel();

            _PrimaryGrid.SuperGrid = this;
            _PrimaryGrid.ContainerBounds = ClientRectangle;

            SetupScrollBars();

            if (DesignMode == false)
            {
                Application.AddMessageFilter(this);

                base.PreviewKeyDown += SuperGridControlPreviewKeyDown;
            }

            UpdateGridStyle();

            StyleManager.Register(this);

            if (BarFunctions.IsWindows7 && Touch.TouchHandler.IsTouchEnabled)
            {
                _TouchHandler = new Touch.TouchHandler(this, Touch.eTouchHandlerType.Gesture);

                _TouchHandler.PanBegin += TouchHandlerPanBegin;
                _TouchHandler.Pan += TouchHandlerPan;
                _TouchHandler.PanEnd += TouchHandlerPanEnd;
            }
        }

        #region OnEnabledChanged

        protected override void OnEnabledChanged(EventArgs e)
        {
            base.OnEnabledChanged(e);

            Refresh();
        }

        #endregion

        #region StyleManagerStyleChanged

        /// <summary>
        /// Called by StyleManager to notify control that style on
        /// manager has changed and that control should refresh its
        /// appearance if its style is controlled by StyleManager.
        /// </summary>
        /// <param name="newStyle">New active style.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void StyleManagerStyleChanged(eDotNetBarStyle newStyle)
        {
            UpdateGridStyle();
        }

        private void UpdateGridStyle()
        {
            SuperGridStyle style = SuperGridStyle.Office2010Blue;

            switch (StyleManager.Style)
            {
                case eStyle.Office2007VistaGlass:
                case eStyle.VisualStudio2010Blue:
                case eStyle.Windows7Blue:
                case eStyle.Office2007Blue:
                case eStyle.Office2010Blue:
                    style = SuperGridStyle.Office2010Blue;
                    break;

                case eStyle.Office2007Black:
                case eStyle.Office2010Black:
                    style = SuperGridStyle.Office2010Black;
                    break;

                case eStyle.Office2007Silver:
                case eStyle.Office2010Silver:
                    style = SuperGridStyle.Office2010Silver;
                    break;

                case eStyle.Metro:
                    style = SuperGridStyle.Metro;
                    break;
            }

            _BaseVisualStyles = VisualStylesTable.GetStyle(style);

            if (IsHandleCreated == true)
            {
                UpdateStyleCount();

                Invalidate(true);
            }
        }

        #endregion

        #region Public properties

        #region ActiveCell

        ///<summary>
        /// Gets the current active cell
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell ActiveCell
        {
            get { return (_ActiveCell); }

            internal set
            {
                if (_ActiveCell != value)
                {
                    _LastActiveCell = _ActiveCell;

                    if (_ActiveCell != null)
                        _ActiveCell.EditorDirty = false;

                    _ActiveCell = value;

                    OnActiveCellChanged(_LastActiveCell, _ActiveCell);
                }
            }
        }

        private void OnActiveCellChanged(
            GridCell oldValue, GridCell newValue)
        {
            GridPanel panel =
                (oldValue != null) ? oldValue.GridPanel :
                (newValue != null) ? newValue.GridPanel : null;

            DoCellActivatedEvent(panel, oldValue, newValue);
        }

        #endregion

        #region ActiveEditor

        ///<summary>
        /// Gets the currently active cell editor, or null if
        /// no edit is in progress
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public IGridCellEditControl ActiveEditor
        {
            get { return (_ActiveEditor); }
            internal set {_ActiveEditor = value; }
        }

        #endregion

        #region ActiveElement

        ///<summary>
        /// Gets the current active grid element (Row/Cell), or null if
        /// there is no current active element
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridElement ActiveElement
        {
            get { return (_LastProcessedItem); }
            
            internal set
            {
                if (_LastProcessedItem != value)
                {
                    if (_LastProcessedItem != null)
                        _LastProcessedItem.InvalidateRender();

                    _LastProcessedItem = value;

                    if (_LastProcessedItem != null)
                        _LastProcessedItem.InvalidateRender();
                }
            }
        }

        #endregion

        #region ActiveFilterPanel

        ///<summary>
        /// Gets the currently active FilterPanel
        /// editor, or null if no edit is in progress
        ///</summary>
        public FilterPanel ActiveFilterPanel
        {
            get { return (_ActiveFilterPanel); }
            internal set { _ActiveFilterPanel = value; }
        }

        #endregion

        #region ActiveGrid

        ///<summary>
        /// Gets the current active grid
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridPanel ActiveGrid
        {
            get { return (_ActiveGrid); }

            internal set
            {
                if (_ActiveGrid != value)
                {
                    if (_ActiveGrid != null)
                    {
                        if (_ActiveGrid.ActiveRow is GridRow)
                            ((GridRow)_ActiveGrid.ActiveRow).EditorDirty = false;
                    }

                    GridPanel oldValue = _ActiveGrid;
                    _ActiveGrid = value;

                    OnActiveGridChanged(oldValue);

                    ActiveRow = (_ActiveGrid != null) ? _ActiveGrid.ActiveRow : null;
                }
            }
        }

        private void OnActiveGridChanged(GridPanel oldPanel)
        {
            DoActiveGridChangedEvent(oldPanel);
        }

        #endregion

        #region ActiveRow

        ///<summary>
        /// Gets the current active row, or null if no row
        /// is defined or active
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridContainer ActiveRow
        {
            get
            {
                if (_ActiveGrid != null)
                    return (_ActiveGrid.ActiveRow);

                return (null);
            }

            internal set
            {
                if (_ActiveRow != value)
                {
                    if (_ActiveRow != null)
                        _ActiveRow.FlushRow();

                    GridContainer oldValue = _ActiveRow;
                    _ActiveRow = value;

                    if (_ActiveRow != null)
                        ActiveGrid = _ActiveRow.GridPanel;

                    OnActiveRowChanged(oldValue, _ActiveRow);
                }
            }
        }

        private void OnActiveRowChanged(
            GridContainer oldValue, GridContainer newValue)
        {
            GridPanel panel =
                (oldValue != null) ? oldValue.GridPanel :
                (newValue != null) ? newValue.GridPanel : null;

            DoRowActivatedEvent(panel, oldValue, newValue);
        }

        #endregion

        #region BaseVisualStyles

        ///<summary>
        /// BaseVisualStyles - the SuperGrid starting base styles
        ///</summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DefaultVisualStyles BaseVisualStyles
        {
            get { return (_BaseVisualStyles); }
            internal set { _BaseVisualStyles = value; }
        }

        #endregion

        #region DefaultVisualStyles

        ///<summary>
        /// Gets or sets the Default Visual Styles for each grid element
        ///</summary>
        [Browsable(true), Category("Appearance"), DefaultValue(null)]
        [Description("Indicates the Default Visual Styles for each grid elements.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public DefaultVisualStyles DefaultVisualStyles
        {
            get
            {
                if (_DefaultVisualStyles == null)
                {
                    _DefaultVisualStyles = new DefaultVisualStyles();

                    _DefaultVisualStyles.PropertyChanged += DefaultVisualStylesPropertyChanged;
                }

                return (_DefaultVisualStyles);
            }

            set
            {
                if (_DefaultVisualStyles != value)
                {
                    if (_DefaultVisualStyles != null)
                        _DefaultVisualStyles.PropertyChanged -= DefaultVisualStylesPropertyChanged;

                    _DefaultVisualStyles = value;

                    if (_DefaultVisualStyles != null)
                        _DefaultVisualStyles.PropertyChanged += DefaultVisualStylesPropertyChanged;
                }
            }
        }

        #region DefaultVisualStylesPropertyChanged

        private void DefaultVisualStylesPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateStyleCount();

            VisualChangeType changeType = ((VisualPropertyChangedEventArgs)e).ChangeType;

            switch (changeType)
            {
                case VisualChangeType.Layout:
                    _PrimaryGrid.InvalidateLayout();
                    break;

                case VisualChangeType.Render:
                    Invalidate();
                    break;
            }
        }

        #endregion

        #endregion

        #region DesignerElement

        ///<summary>
        /// For internal use only
        ///</summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridElement DesignerElement
        {
            get { return (_DesignerElement); }

            set
            {
                _DesignerElement = value;

                if (value != null)
                {
                    if (value.NeedsMeasured == true)
                        ArrangeGrid();

                    value.EnsureVisible(true);
                }

                Refresh();
            }
        }

        #endregion

        #region EditorActive

        ///<summary>
        /// Gets whether a cell editor is currently active
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool EditorActive
        {
            get { return (_ActiveEditor != null); }
        }

        #endregion

        #region EditorCell

        ///<summary>
        /// Gets the currently active editor cell, or null
        /// if no cell is currently being edited
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell EditorCell
        {
            get { return (_EditorCell); }
            internal set { _EditorCell = value; }
        }

        #endregion

        #region EditorColumn

        ///<summary>
        /// Gets the column containing the current edit
        /// cell, or null if no edit is in progress
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn EditorColumn
        {
            get
            {
                if (_EditorCell != null)
                    return (_EditorCell.GridColumn);

                return (null);
            }
        }

        #endregion

        #region EditorGrid

        ///<summary>
        /// Gets the grid containing the cell currently
        /// being edited, or null if no edit is in progress
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridPanel EditorGrid
        {
            get
            {
                if (_EditorCell != null)
                    return (_EditorCell.GridPanel);

                return (null);
            }
        }

        #endregion

        #region EditorRow

        ///<summary>
        /// Gets the row containing the cell currently
        /// being edited, or null if no edit is in progress
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridRow EditorRow
        {
            get
            {
                if (_EditorCell != null)
                    return (_EditorCell.GridRow);

                return (null);
            }
        }

        #endregion

        #region ExpandButtonType

        /// <summary>
        /// Gets or sets the ExpandButton Type
        /// </summary>
        [DefaultValue(ExpandButtonType.NotSet), Category("Appearance")]
        [Description("Indicates the ExpandButton Type.")]
        public ExpandButtonType ExpandButtonType
        {
            get { return (_ExpandButtonType); }

            set
            {
                if (_ExpandButtonType != value)
                {
                    _ExpandButtonType = value;

                    UpdateStyleCount();

                    OnPropertyChanged(new PropertyChangedEventArgs("ExpandButtonType"));
                }
            }
        }

        #endregion

        #region FilterColorizeCustomExpr

        /// <summary>
        /// Gets or sets whether the Custom Expression
        /// dialog colorizes the output expression
        /// </summary>
        [DefaultValue(true), Category("Filtering")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        [Description("Indicates whether the Custom Expression dialog colorizes the output expression.")]
        public bool FilterColorizeCustomExpr
        {
            get { return (_FilterColorizeCustomExpr); }

            set
            {
                if (_FilterColorizeCustomExpr != value)
                {
                    _FilterColorizeCustomExpr = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("FilterColorizeCustomExpr"));
                }
            }
        }

        #endregion

        #region FilterExprColors

        /// <summary>
        /// Gets or sets the expression colors used in the Custom Expression dialog
        /// </summary>
        [Category("Filtering")]
        [Description("Indicates the expression colors used in the Custom Expression dialog.")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public ExpressionColors FilterExprColors
        {
            get { return (_FilterExprColors); }

            set
            {
                if (_FilterExprColors != value)
                {
                    _FilterExprColors = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("FilterExprColors"));
                }
            }
        }

        #endregion

        #region FilterMaxDropDownHeight

        /// <summary>
        /// Gets or sets the max height of
        /// the filter panel and filter popup drop-down
        /// </summary>
        [DefaultValue(300), Category("Filtering")]
        [Description("Indicates the max height of the filter panel and filter popup dropdown.")]
        public int FilterMaxDropDownHeight
        {
            get { return (_FilterMaxDropDownHeight); }

            set
            {
                if (_FilterMaxDropDownHeight != value)
                {
                    _FilterMaxDropDownHeight = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("FilterMaxDropDownHeight"));
                }
            }
        }

        #endregion

        #region FilterUseExtendedCustomDialog

        /// <summary>
        /// Gets or sets whether the Extended Custom Expression dialog
        /// is used (permits user filter definition persistence)
        /// </summary>
        [DefaultValue(false), Category("Filtering")]
        [Description("Indicates whether the Extended Custom Expression dialog is used (permits user filter definition persistence).")]
        public bool FilterUseExtendedCustomDialog
        {
            get { return (_FilterUseExtendedCustomDialog); }

            set
            {
                if (_FilterUseExtendedCustomDialog != value)
                {
                    _FilterUseExtendedCustomDialog = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("FilterUseExtendedCustomDialog"));
                }
            }
        }

        #endregion

        #region GridCursor

        ///<summary>
        /// Gets or sets the logical grid cursor
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Cursor GridCursor
        {
            get { return (_GridCursor); }

            set
            {
                if (value == null || value == Cursors.Default)
                    _GridCursor = _DefaultCursor;
                else
                    _GridCursor = value;
            }
        }

        #endregion

        #region HScrollBar

        ///<summary>
        /// Gets a reference to the grid’s horizontal scrollbar
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public HScrollBarAdv HScrollBar
        {
            get { return (_HScrollBar); }
        }

        #endregion

        #region HScrollMaximum

        ///<summary>
        /// Gets the horizontal scrollbar maximum value
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int HScrollMaximum
        {
            get { return (_HScrollBar.Maximum); }
        }

        #endregion

        #region HScrollOffset

        ///<summary>
        /// Gets or sets the horizontal scrollbar offset
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int HScrollOffset
        {
            get { return (_HScrollOffset); }
            set { SetHScrollValue(value); }
        }

        #endregion

        #region IsUpdateSuspended

        ///<summary>
        /// Gets whether grid updating / rendering is suspended
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public bool IsUpdateSuspended
        {
            get { return (_BeginUpdateCount > 0); }
        }

        #endregion

        #region NonModalEditorCell

        ///<summary>
        /// Gets the current NonModal cell being edited, or
        /// null if no NonModel cell edit is in progress
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridCell NonModalEditorCell
        {
            get { return (_NonModalEditorCell); }
            internal set { _NonModalEditorCell = value; }
        }

        #endregion

        #region PrimaryGrid

        ///<summary>
        /// Gets the primary, root grid.
        ///</summary>
        [Browsable(true)]
        [Description("Indicates the primary, root grid")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public GridPanel PrimaryGrid
        {
            get { return (_PrimaryGrid); }
        }

        #endregion

        #region ShowCustomFilterHelp

        /// <summary>
        /// Gets or sets whether the Custom
        /// Expression dialog shows user help
        /// </summary>
        [DefaultValue(true), Category("Filtering")]
        [Description("Indicates whether the Custom Expression dialog shows user help.")]
        public bool ShowCustomFilterHelp
        {
            get { return (_ShowCustomFilterHelp); }

            set
            {
                if (_ShowCustomFilterHelp != value)
                {
                    _ShowCustomFilterHelp = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("ShowCustomFilterHelp"));
                }
            }
        }

        #endregion

        #region SizingStyle

        /// <summary>
        /// Gets or sets which StyleType (Default, MouseOver, etc) to use for element sizing
        /// </summary>
        [Browsable(true), DefaultValue(StyleType.NotSet), Category("Appearance")]
        [Description("Indicates which StyleType (Default, MouseOver, etc) to use for element sizing")]
        public StyleType SizingStyle
        {
            get { return (_SizingStyle); }

            set
            {
                if (_SizingStyle != value)
                {
                    _SizingStyle = value;

                    OnSizingStyleChanged();
                }
            }
        }

        private void OnSizingStyleChanged()
        {
            _PrimaryGrid.InvalidateLayout();

            OnPropertyChanged(new PropertyChangedEventArgs("SizingStyle"));
        }

        #endregion

        #region TabSelection

        ///<summary>
        /// Gets or sets how the TAB key moves the focus when pressed. 
        ///</summary>
        [Browsable(true), DefaultValue(TabSelection.Cell), Category("Behavior")]
        [Description("Indicates how the TAB key moves the focus when pressed.")]
        public TabSelection TabSelection
        {
            get { return (_TabSelection); }
                        
            set
            {
                if (_TabSelection != value)
                {
                    _TabSelection = value;

                    OnPropertyChanged(new PropertyChangedEventArgs("TabSelection"));
                }
            }
        }

        #endregion

        #region VScrollBar

        ///<summary>
        /// Gets a reference to the grid’s vertical scrollbar
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public VScrollBarAdv VScrollBar
        {
            get { return (_VScrollBar); }
        }

        #endregion

        #region VScrollMaximum

        ///<summary>
        /// Gets the vertical scrollbar maximum value
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VScrollMaximum
        {
            get { return (_VScrollBar.Maximum); }
        }

        #endregion

        #region VScrollOffset

        ///<summary>
        /// Gets the vertical scrollbar offset
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int VScrollOffset
        {
            get { return (_VScrollOffset); }
            set { SetVScrollValue(value); }
        }

        #endregion

        #endregion

        #region Internal properties

        #region ActiveNonModalEditor

        ///<summary>
        /// ActiveNonModalEditor
        ///</summary>
        internal IGridCellEditControl ActiveNonModalEditor
        {
            get { return (_ActiveNonModalEditor); }
            set { _ActiveNonModalEditor = value; }
        }

        #endregion

        #region ArrangeLayoutCount

        internal int ArrangeLayoutCount
        {
            get { return (_ArrangeLayoutCount); }
            set { _ArrangeLayoutCount = value; }
        }

        #endregion

        #region BoundsUpdateCount

        internal int BoundsUpdateCount
        {
            get {return (_BoundsUpdateCount); }
            set { _BoundsUpdateCount = value; }
        }

        #endregion

        #region CapturedItem

        internal GridElement CapturedItem
        {
            get
            {
                if (Capture == false)
                    _CapturedItem = null;

                return (_CapturedItem);
            }

            set { Capture = ((_CapturedItem = value) != null); }
        }

        #endregion

        #region CViewRect

        internal Rectangle CViewRect
        {
            get
            {
                if (_CViewRect.IsEmpty == true)
                {
                    _CViewRect = ClientRectangle;

                    int n =_PrimaryGrid.FixedHeaderHeight;

                    _CViewRect.Y += n;
                    _CViewRect.Height -= n;

                    if (_PrimaryGrid.Footer != null && _PrimaryGrid.Footer.Visible == true)
                        _CViewRect.Height -= _PrimaryGrid.Footer.Size.Height;

                    if (_VScrollBar != null && _VScrollBar.Visible == true)
                        _CViewRect.Width -= VScrollBarWidth;

                    if (_HScrollBar != null && _HScrollBar.Visible == true)
                        _CViewRect.Height -= HScrollBarHeight;
                }

                return (_CViewRect);
            }
        }

        #endregion

        #region FilterApplyString

        internal string FilterApplyString
        {
            get { return (LocalizedString(ref _FilterApplyString)); }
        }

        #endregion

        #region FilterCancelString

        internal string FilterCancelString
        {
            get { return (LocalizedString(ref _FilterCancelString)); }
        }

        #endregion

        #region FilterCloseString

        internal string FilterCloseString
        {
            get { return (LocalizedString(ref _FilterCloseString)); }
        }

        #endregion

        #region FilterCustomString

        internal string FilterCustomString
        {
            get { return (LocalizedString(ref _FilterCustomString)); }
        }

        #endregion

        #region FilterOkString

        internal string FilterOkString
        {
            get { return (LocalizedString(ref _FilterOkString)); }
        }

        #endregion

        #region FilterShowAllString

        internal string FilterShowAllString
        {
            get { return (LocalizedString(ref _FilterShowAllString)); }
        }

        #endregion

        #region FilterShowNotNullString

        internal string FilterShowNotNullString
        {
            get { return (LocalizedString(ref _FilterShowNotNullString)); }
        }

        #endregion

        #region FilterShowNullString

        internal string FilterShowNullString
        {
            get { return (LocalizedString(ref _FilterShowNullString)); }
        }

        #endregion

        #region HasDataErrorHandler

        internal bool HasDataErrorHandler
        {
            get { return (DataError != null); }
        }

        #endregion

        #region HScrollBarHeight

        internal int HScrollBarHeight
        {
            get { return (SystemInformation.HorizontalScrollBarHeight); }
        }

        #endregion

        #region SViewRect

        internal Rectangle SViewRect
        {
            get
            {
                Rectangle r = ViewRect;

                if (PrimaryGrid.ShowRowHeaders == true)
                {
                    r.X += PrimaryGrid.RowHeaderWidth;
                    r.Width -= PrimaryGrid.RowHeaderWidth;

                    if (r.Width < 0)
                        r.Width = 0;
                }

                return (r);
            }
        }

        #endregion

        #region IndiceesUpdateCount

        internal int IndiceesUpdateCount
        {
            get { return (_IndiceesUpdateCount); }
        }

        #endregion

        #region IsDesignerHosted

        internal bool IsDesignerHosted
        {
            get { return (DesignMode); }
        }

        #endregion

        #region IsHScrollBarVisible

        internal bool IsHScrollBarVisible
        {
            get { return (_HScrollBar != null && _HScrollBar.Visible); }
        }

        #endregion

        #region IsVScrollBarVisible

        internal bool IsVScrollBarVisible
        {
            get { return (_VScrollBar != null && _VScrollBar.Visible); }
        }

        #endregion

        #region LastActiveCell

        internal GridCell LastActiveCell
        {
            get { return (_LastActiveCell); }
            set { _LastActiveCell = value; }
        }

        #endregion

        #region NeedToUpdateIndicees

        internal bool NeedToUpdateIndicees
        {
            get { return (_NeedToUpdateIndicees); }

            set
            {
                _NeedToUpdateIndicees = value;

                if (value == true)
                    _IndiceesUpdateCount++;
            }
        }

        #endregion

        #region PopupControl

        internal PopupControl PopupControl
        {
            get { return (_PopupControl); }
            set { _PopupControl = value; }
        }

        #endregion

        #region StyleMouseOverUpdateCount

        internal int StyleMouseOverUpdateCount
        {
            get { return (_StyleMouseOverUpdateCount); }
            set { _StyleMouseOverUpdateCount = value; }
        }

        #endregion

        #region StyleSelectedUpdateCount

        internal int StyleSelectedUpdateCount
        {
            get { return (_StyleSelectedUpdateCount); }
            set { _StyleSelectedUpdateCount = value; }
        }

        #endregion

        #region StyleSelectedMouseOverUpdateCount

        internal int StyleSelectedMouseOverUpdateCount
        {
            get { return (_StyleSelectedMouseOverUpdateCount); }
            set { _StyleSelectedMouseOverUpdateCount = value; }
        }

        #endregion

        #region StyleUpdateCount

        internal int StyleUpdateCount
        {
            get { return (_StyleUpdateCount); }
            set { _StyleUpdateCount = value; }
        }

        #endregion

        #region ToolTip

        internal System.Windows.Forms.ToolTip ToolTip
        {
            get
            {
                if (_ToolTip == null)
                    _ToolTip = new System.Windows.Forms.ToolTip();

                return (_ToolTip);
            }
        }

        #endregion

        #region ToolTipText

        internal string ToolTipText
        {
            get { return (_ToolTipText); }

            set
            {
                if (_ToolTipText != value)
                {
                    _ToolTipText = value;

                    ToolTip.SetToolTip(this, value);

                    ToolTip.Active =
                        (string.IsNullOrEmpty(value) == false);
                }
            }
        }

        #endregion

        #region PrimaryGridSize

        internal Size PrimaryGridSize
        {
            get
            {
                Size size = _PrimaryGrid.SizeNeeded;

                int n = (_VScrollOffset > 0) ?
                    _PrimaryGrid.FixedRowHeight : _PrimaryGrid.FixedHeaderHeight;

                size.Height -= (n + 1);

                if (_PrimaryGrid.Footer != null && _PrimaryGrid.Footer.Visible == true)
                    size.Height -= _PrimaryGrid.Footer.Size.Height;

                if (PrimaryGrid.ShowRowHeaders == true)
                    size.Width -= PrimaryGrid.RowHeaderWidth;

                return (size);
            }
        }

        #endregion

        #region ViewRect

        internal Rectangle ViewRect
        {
            get
            {
                if (_ViewRect.IsEmpty == true)
                    _ViewRect = GetAdjustedBounds(ClientRectangle);

                return (_ViewRect);
            }

            set
            {
                _BoundsUpdateCount++;

                _ViewRect = value;
                _CViewRect = value;
            }
        }

        #endregion

        #region GetAdjustedBounds

        private Rectangle GetAdjustedBounds(Rectangle r)
        {
            int n = (_VScrollOffset > 0) ?
                _PrimaryGrid.FixedRowHeight : _PrimaryGrid.FixedHeaderHeight;

            r.Y += n;
            r.Height -= (n + 1);

            if (_PrimaryGrid.Footer != null && _PrimaryGrid.Footer.Visible == true)
                r.Height -= _PrimaryGrid.Footer.Size.Height;

            if (_VScrollBar != null && _VScrollBar.Visible == true)
                r.Width -= VScrollBarWidth;

            if (_HScrollBar != null && _HScrollBar.Visible == true)
                r.Height -= HScrollBarHeight;

            if (r.Width < 0)
                r.Width = 0;

            if (r.Height < 0)
                r.Height = 0;

            return (r);
        }

        #endregion

        #region ViewRectEx

        internal Rectangle ViewRectEx
        {
            get
            {
                Rectangle r = ViewRect;

                int n = (_VScrollOffset > 0) ?
                   _PrimaryGrid.FixedRowHeight - _PrimaryGrid.FixedHeaderHeight : 0;

                r.Y -= n;
                r.Height += n;

                return (r);
            }
        }

        #endregion

        #region VScrollBarWidth

        internal int VScrollBarWidth
        {
            get { return (SystemInformation.VerticalScrollBarWidth); }
        }

        #endregion

        #region DefaultSize

        protected override Size DefaultSize
        {
            get { return new Size(200, 200); }
        }

        #endregion

        #endregion

        #region OnPaint

        #region OnPaint

        /// <summary>
        /// Renders the control.
        /// </summary>
        /// <param name="e">Paint arguments.</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            if (IsUpdateSuspended == false)
            {
                SmoothingMode sm = e.Graphics.SmoothingMode;
                TextRenderingHint th = e.Graphics.TextRenderingHint;

                base.OnPaintBackground(e);

                if (_PrimaryGrid.IsLayoutValid == false)
                    ArrangeGrid(e.Graphics);

                GridRenderInfo renderInfo = GetGridRenderInfo(e);

                PaintControl(renderInfo);

                e.Graphics.SmoothingMode = sm;
                e.Graphics.TextRenderingHint = th;

                if (_PostMouseMove == true)
                {
                    _PostMouseMove = false;

                    Cursor.Position = Cursor.Position;
                }

                base.OnPaint(e);

#if !TRIAL
                if (NativeFunctions.keyValidated2 != 266)
                {
                    //TextDrawing.DrawString(e.Graphics, "Invalid License", Font,
                    //                       Color.FromArgb(180, Color.Red), ClientRectangle,
                    //                       eTextFormat.Bottom | eTextFormat.HorizontalCenter);
                }
#else
                RenderWaterMark(renderInfo);
#endif
            }
        }

        #endregion

        #region PaintControl

        private void PaintControl(GridRenderInfo renderInfo)
        {
            Rectangle r = ClientRectangle;
            r.Intersect(renderInfo.ClipRectangle);

            if (r.Width > 0 && r.Height > 0)
                _PrimaryGrid.Render(renderInfo);

            RenderScrollBoxArea(renderInfo);
        }

        #endregion

        #region RenderScrollBoxArea

        private void RenderScrollBoxArea(GridRenderInfo renderInfo)
        {
            if (_HScrollBar.Visible && _VScrollBar.Visible)
            {
                Rectangle r = _HScrollBar.Bounds;
                r.X = _VScrollBar.Bounds.X;
                r.Width = _VScrollBar.Bounds.Width;

                if (r.IntersectsWith(renderInfo.ClipRectangle))
                    renderInfo.Graphics.FillRectangle(SystemBrushes.Control, r);
            }
        }

        #endregion

        #region RenderWaterMark

        private void RenderWaterMark(GridRenderInfo renderInfo)
        {
            Rectangle r = ViewRect;
            r.X = r.Right - 30;
            r.Width = 30;

            if (r.IntersectsWith(renderInfo.ClipRectangle) == true)
            {
                Graphics g = renderInfo.Graphics;

                using (StringFormat sf = new StringFormat())
                {
                    sf.Alignment = StringAlignment.Center;
                    sf.LineAlignment = StringAlignment.Center;

                    sf.FormatFlags = StringFormatFlags.NoWrap | StringFormatFlags.DirectionVertical;

                    using (Font font = new Font("Times New Roman", 9))
                    {
                        Rectangle t = r;
                        t.X++;
                        t.Y++;

                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Gray)))
                        {
                            g.DrawString("DevComponents SuperGrid Trial",
                                         font, brush, t, sf);
                        }

                        using (SolidBrush brush = new SolidBrush(Color.FromArgb(128, Color.Gainsboro)))
                        {
                            g.DrawString("DevComponents SuperGrid Trial",
                                         font, brush, r, sf);
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region ArrangeGrid

        protected override void OnHandleCreated(EventArgs e)
        {
            if (_PrimaryGrid.IsLayoutValid == false)
                ArrangeGrid();

            base.OnHandleCreated(e);
        }

        /// <summary>
        /// Performs grid layout.
        /// </summary>
        public void ArrangeGrid()
        {
            using (Graphics g = CreateGraphics())
                ArrangeGrid(g);
        }

        private void ArrangeGrid(Graphics g)
        {
            ArrangeLayoutCount++;

            GridLayoutInfo layoutInfo = GetGridLayoutInfo(g);
            GridLayoutStateInfo stateInfo = new GridLayoutStateInfo(_PrimaryGrid, 0);

            Size constraintSize = Size.Empty;

            _PrimaryGrid.Measure(layoutInfo, stateInfo, constraintSize);

            stateInfo.PassCount++;

            NeedToUpdateIndicees = false;

            if (HorizontalIsOver(layoutInfo) == true && _HScrollBarVisible)
            {
                if(_HScrollBarVisible)
                    layoutInfo.ClientBounds.Height -= HScrollBarHeight;

                if (VerticalIsOver(layoutInfo) == true && _VScrollBarVisible)
                    layoutInfo.ClientBounds.Width -= VScrollBarWidth;

                _PrimaryGrid.Measure(layoutInfo, stateInfo, constraintSize);
            }
            else
            {
                if (VerticalIsOver(layoutInfo) == true && _VScrollBarVisible)
                {
                    layoutInfo.ClientBounds.Width -= VScrollBarWidth;

                    _PrimaryGrid.Measure(layoutInfo, stateInfo, constraintSize);

                    if (HorizontalIsOver(layoutInfo) == true && _HScrollBarVisible)
                        layoutInfo.ClientBounds.Height -= HScrollBarHeight;
                }
            }

            layoutInfo.ClientBounds.Width = Math.Max(0, layoutInfo.ClientBounds.Width);
            layoutInfo.ClientBounds.Height = Math.Max(0, layoutInfo.ClientBounds.Height);

            EnableVScrollBar(layoutInfo);
            EnableHScrollBar(layoutInfo);

            ViewRect = Rectangle.Empty;
            _PrimaryGrid.Arrange(layoutInfo, stateInfo, layoutInfo.ClientBounds);
            ViewRect = Rectangle.Empty;

            UpdateScrollBars(layoutInfo);

            if (_ActiveEditor != null)
                EditorCell.PositionEditPanel(_ActiveEditor);

            if (NonModalEditorCell != null)
                NonModalEditorCell.PositionEditPanel(_ActiveNonModalEditor);

            if (ActiveFilterPanel != null)
                _ActiveFilterPanel.PositionEditPanel();
        }

        #region GetGridLayoutInfo

        private GridLayoutInfo GetGridLayoutInfo(Graphics g)
        {
            GridLayoutInfo layoutInfo = new GridLayoutInfo(g, GetGridItemBounds());

            layoutInfo.RightToLeft = (RightToLeft == RightToLeft.No);
            layoutInfo.DefaultVisualStyles = _DefaultVisualStyles; 

            return (layoutInfo);
        }

        #endregion

        #region GetGridItemBounds

        private Rectangle GetGridItemBounds()
        {
            return (ClientRectangle);
        }

        #endregion

        #region GetGridRenderInfo

        private GridRenderInfo GetGridRenderInfo(PaintEventArgs e)
        {
            GridRenderInfo renderInfo =
                new GridRenderInfo(e.Graphics, Rectangle.Ceiling(e.ClipRectangle));

            renderInfo.RightToLeft = (RightToLeft == RightToLeft.Yes);

            return (renderInfo);
        }

        #endregion

        #endregion

        #region Scrollbar support

        #region UpdateScrollBars

        private void UpdateScrollBars(GridLayoutInfo layoutInfo)
        {
            if (_HScrollBarEnabled == true)
            {
                _HScrollBar.SmallChange = layoutInfo.ClientBounds.Width / 20;
                _HScrollBar.LargeChange = SViewRect.Width;
                _HScrollBar.Maximum = Math.Max(0, PrimaryGridSize.Width - 1);

                if (_HScrollBar.Value + SViewRect.Width > _HScrollBar.Maximum)
                {
                    _HScrollBar.Value = _HScrollBar.Maximum - SViewRect.Width;
                    _HScrollOffset = _HScrollBar.Value;
                }

                _HScrollBar.Refresh();
            }

            if (_VScrollBarEnabled == true)
            {
                _VScrollBar.SmallChange = layoutInfo.ClientBounds.Height / 20;
                _VScrollBar.LargeChange = SViewRect.Height;
                _VScrollBar.Maximum = Math.Max(0, PrimaryGridSize.Height - 1);

                if (_VScrollBar.Value + SViewRect.Height - 1 > _VScrollBar.Maximum)
                {
                    _VScrollBar.Value = _VScrollBar.Maximum - SViewRect.Height + 1;
                    _VScrollOffset = _VScrollBar.Value;
                }

                _VScrollBar.Refresh();
            }
        }

        #endregion

        #region EnableHScrollBar

        private void EnableHScrollBar(GridLayoutInfo layoutInfo)
        {
            bool enable = _HScrollBarVisible && (_PrimaryGrid.Size.Width > layoutInfo.ClientBounds.Width);

            if (enable == true)
            {
                _HScrollBar.Location = new
                    Point(layoutInfo.ClientBounds.Left + 1,
                    layoutInfo.ClientBounds.Bottom - 1);

                int n = layoutInfo.ClientBounds.Width - 2;

                if (_VScrollBar.Visible == true)
                    n++;

                _HScrollBar.Width = n;
            }
            else
            {
                _HScrollBar.Value = 0;
                _HScrollOffset = 0;
            }

            _HScrollBarEnabled = enable;

            _HScrollBar.Enabled = enable;
            _HScrollBar.Visible = enable;
        }

        #endregion

        #region EnableVScrollBar

        private void EnableVScrollBar(GridLayoutInfo layoutInfo)
        {
            bool enable = _VScrollBarVisible && (_PrimaryGrid.Size.Height > layoutInfo.ClientBounds.Height);

            if (enable == true)
            {
                _VScrollBar.Location = new
                    Point(layoutInfo.ClientBounds.Right - 1,
                    layoutInfo.ClientBounds.Top + 1);

                _VScrollBar.Height = layoutInfo.ClientBounds.Height - 2;
            }
            else
            {
                _VScrollBar.Value = 0;
                _VScrollOffset = 0;
            }

            _VScrollBarEnabled = enable;

            _VScrollBar.Enabled = enable;
            _VScrollBar.Visible = enable;
        }

        #endregion

        #region HScrollBarScroll

        private void HScrollBarScroll(object sender, ScrollEventArgs e)
        {
            ScrollBarAdv sbar =
                sender as ScrollBarAdv;

            if (sbar != null)
            {
                if (_HScrollOffset != e.NewValue)
                {
                    _HScrollOffset = e.NewValue;

                    PostScrollUpdate();

                    DoScrollEvent(_PrimaryGrid, e, sbar);
                }
            }
        }

        #endregion

        #region VScrollBarScroll

        private void VScrollBarScroll(object sender, ScrollEventArgs e)
        {
            ScrollBarAdv sbar =
                sender as ScrollBarAdv;

            if (sbar != null)
            {
                if (_VScrollOffset != e.NewValue)
                {
                    _VScrollOffset = e.NewValue;

                    PostScrollUpdate();

                    DoScrollEvent(_PrimaryGrid, e, sbar);
                }
            }
        }

        #endregion

        #region HorizontalIsOver

        private bool HorizontalIsOver(GridLayoutInfo layoutInfo)
        {
            return (_PrimaryGrid.Size.Width > layoutInfo.ClientBounds.Width);
        }

        #endregion

        #region VerticalIsOver

        private bool VerticalIsOver(GridLayoutInfo layoutInfo)
        {
            return (_PrimaryGrid.Size.Height > layoutInfo.ClientBounds.Height);
        }

        #endregion

        #region SetupScrollBars

        private void SetupScrollBars()
        {
            _VScrollBar = new VScrollBarAdv();
            _VScrollBar.Width = SystemInformation.VerticalScrollBarWidth;
            _VScrollBar.Scroll += VScrollBarScroll;
            _VScrollBar.MouseEnter += VScrollBarMouseEnter;

            _HScrollBar = new HScrollBarAdv();
            _HScrollBar.Height = SystemInformation.HorizontalScrollBarHeight;
            _HScrollBar.Scroll += HScrollBarScroll;
            _HScrollBar.MouseEnter += HScrollBarMouseEnter;

            _HScrollBar.Visible = false;
            _VScrollBar.Visible = false;

            Controls.Add(_HScrollBar);
            Controls.Add(_VScrollBar);
        }

        void HScrollBarMouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        void VScrollBarMouseEnter(object sender, EventArgs e)
        {
            Cursor = Cursors.Default;
        }

        #endregion

        #region SetVScrollValue

        internal void SetVScrollValue(int value)
        {
            if (_VScrollBar.Visible == true)
            {
                int oldValue = _VScrollBar.Value;

                value = Math.Max(value, 0);
                value = Math.Min(value, _VScrollBar.Maximum);

                if (value + _VScrollBar.LargeChange - 1 > _VScrollBar.Maximum)
                    value = _VScrollBar.Maximum - _VScrollBar.LargeChange + 1;

                value = Math.Max(value, 0);

                if (_VScrollBar.Value != value)
                {
                    _VScrollBar.Value = value;
                    _VScrollOffset = value;

                    PostScrollUpdate();

                    if (Scroll != null || ScrollMin != null || ScrollMin != null)
                    {
                        ScrollEventArgs e = new ScrollEventArgs(ScrollEventType.EndScroll,
                            oldValue, value, ScrollOrientation.VerticalScroll);

                        DoScrollEvent(_PrimaryGrid, e, _VScrollBar);
                    }
                }
            }
        }

        #endregion

        #region SetHScrollValue

        internal void SetHScrollValue(int value)
        {
            if (_HScrollBar.Visible == true)
            {
                int oldValue = _HScrollBar.Value;

                value = Math.Max(value, 0);
                value = Math.Min(value, _HScrollBar.Maximum);

                if (value + _HScrollBar.LargeChange > _HScrollBar.Maximum)
                    value = _HScrollBar.Maximum - _HScrollBar.LargeChange;

                value = Math.Max(value, 0);

                if (_HScrollBar.Value != value)
                {
                    _HScrollBar.Value = value;
                    _HScrollOffset = value;

                    PostScrollUpdate();

                    if (Scroll != null || ScrollMin != null || ScrollMin != null)
                    {
                        ScrollEventArgs e = new ScrollEventArgs(ScrollEventType.EndScroll,
                            oldValue, value, ScrollOrientation.HorizontalScroll);

                        DoScrollEvent(_PrimaryGrid, e, _HScrollBar);
                    }
                }
            }
        }

        #endregion

        #region PostScrollUpdate

        private void PostScrollUpdate()
        {
            ViewRect = Rectangle.Empty;

            if (_EditorCell != null)
                _EditorCell.PositionEditPanel(_ActiveEditor);

            if (_ActiveFilterPanel != null)
                _ActiveFilterPanel.PositionEditPanel();

            if (_ActiveNonModalEditor != null)
                DeactivateNonModalEditor();

            PostInternalMouseMove();

            if (_PrimaryGrid.VirtualMode == true)
                _PrimaryGrid.InvalidateRender();
            else
                _PrimaryGrid.InvalidateRender();
        }

        #endregion

        #region VScrollBarVisible

        /// <summary>
        /// Gets or sets whether Vertical Scroll-bar is shown if needed because content of the control exceeds available height. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether Vertical Scroll-bar is shown if needed because content of the control exceeds available height.")]
        public bool VScrollBarVisible
        {
            get { return _VScrollBarVisible; }

            set
            {
                _VScrollBarVisible = value;

                if (_VScrollBar != null && _VScrollBar.Visible != _VScrollBarVisible)
                {
                    _VScrollBar.Visible = _VScrollBarVisible;

                    PrimaryGrid.InvalidateLayout();
                    Invalidate();
                }
            }
        }

        #endregion

        #region HScrollBarVisible

        /// <summary>
        /// Gets or sets whether Horizontal Scroll-bar is shown if needed because content of the control exceeds available width. Default value is true.
        /// </summary>
        [DefaultValue(true), Category("Appearance"), Description("Indicates whether Vertical Scroll-bar is shown if needed because content of the control exceeds available height.")]
        public bool HScrollBarVisible
        {
            get { return _HScrollBarVisible; }

            set
            {
                _HScrollBarVisible = value;

                if (_HScrollBar != null && _HScrollBar.Visible != _HScrollBarVisible)
                {
                    _HScrollBar.Visible = _HScrollBarVisible;

                    PrimaryGrid.InvalidateLayout();
                    Invalidate();
                }
            }
        }

        #endregion

        #endregion

        #region InvalidateRender

        /// <summary>
        /// Invalidates render of the grid element.
        /// </summary>
        /// <param name="gridElement">Element to invalidate rendering for.</param>
        internal void InvalidateRender(GridElement gridElement)
        {
            Rectangle bounds = gridElement.BoundsRelative;

            if (gridElement.Parent != null)
            {
                bounds.X -= _HScrollOffset;

                if (bounds.Y > _PrimaryGrid.FixedRowHeight)
                    bounds.Y -= _VScrollOffset;
            }

            Invalidate(bounds, true);
        }

        internal void InvalidateRender(Rectangle bounds)
        {
            Invalidate(bounds, InvokeRequired == false);
        }

        #endregion

        #region Mouse Support

        #region OnMouseLeave

        protected override void OnMouseLeave(EventArgs e)
        {
            _PrimaryGrid.InternalMouseLeave(e);

            base.OnMouseLeave(e);
        }

        #endregion

        #region OnMouseMove

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (CapturedItem != null)
                CapturedItem.InternalMouseMove(e);
            else
                _PrimaryGrid.InternalMouseMove(e);

            base.OnMouseMove(e);

            base.Cursor = _GridCursor;

            if (_PrimaryGrid.IsDesignerHosted == true)
                Cursor = Cursors.Arrow;
        }

        #endregion

        #region OnMouseDown

        protected override void OnMouseDown(MouseEventArgs e)
        {
            Focus();

            base.OnMouseDown(e);

            _PrimaryGrid.InternalMouseDown(e);
        }

        #endregion

        #region OnMouseUp

        protected override void OnMouseUp(MouseEventArgs e)
        {
            DisableAutoScrolling();

            if (CapturedItem != null)
                CapturedItem.InternalMouseUp(e);
            else
                _PrimaryGrid.InternalMouseUp(e);
            
            base.OnMouseUp(e);

            if (_ActiveEditor != null)
                _ActiveEditor.EditorCell.FocusEditor(_ActiveEditor);
        }

        #endregion

        #region OnMouseClick

        protected override void OnMouseClick(MouseEventArgs e)
        {
            _PrimaryGrid.InternalMouseClick(e);

            base.OnMouseClick(e);
        }

        #endregion

        #region OnMouseDoubleClick

        protected override void OnMouseDoubleClick(MouseEventArgs e)
        {
            _PrimaryGrid.InternalMouseDoubleClick(e);

            base.OnMouseDoubleClick(e);
        }

        #endregion

        #endregion

        #region Keyboard support

        #region IsInputChar

        protected override bool IsInputChar(char charCode)
        {
            if (char.IsLetterOrDigit(charCode))
                return (true);

            return (base.IsInputChar(charCode));
        }

        #endregion

        #region OnKeyDown

        /// <summary>
        /// Handles KeyDown events
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyDown(KeyEventArgs e)
        {
            base.OnKeyDown(e);

            if (e.Handled == false)
            {
                if (e.Handled == true ||
                    ProcessInCellKeyDown(e.KeyData) == true)
                {
                    e.SuppressKeyPress = true;
                }
            }
        }

        #endregion

        #region OnKeyPress

        /// <summary>
        /// Handles KeyPress events
        /// </summary>
        /// <param name="e"></param>
        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            GridCell cell = GetCellToEdit();

            if (cell != null)
            {
                if (cell.CellEditMode == CellEditMode.Modal)
                {
                    switch (cell.GridPanel.KeyboardEditMode)
                    {
                        case KeyboardEditMode.EditOnKeystroke:
                        case KeyboardEditMode.EditOnKeystrokeOrF2:
                            if (cell.CanSetActiveCell(cell.GridPanel, cell) == true)
                            {
                                if (cell.BeginEdit(true) != null)
                                {
                                    Keys modifiers = ModifierKeys;

                                    bool shift = ((modifiers & Keys.Shift) == Keys.Shift) ^
                                                 (Console.CapsLock == true);

                                    if (shift == true)
                                        modifiers |= Keys.Shift;
                                    else
                                        modifiers &= ~Keys.Shift;

                                    PressKeyDown((ushort) (_LastKeyDown | modifiers));
                                }
                            }
                            break;
                    }
                }
                else
                {
                    cell.PostCellKeyDown(_LastKeyDown);
                }
            }

            base.OnKeyPress(e);
        }

        #endregion

        #region ProcessInCellKeyDown

        private bool ProcessInCellKeyDown(Keys keyData)
        {
            if (ProcessGridKeyDown(keyData) == false)
            {
                if (keyData == Keys.F2)
                {
                    GridCell cell = GetCellToEdit();

                    if (cell != null)
                    {
                        if (cell.CellEditMode == CellEditMode.Modal)
                        {
                            switch (cell.GridPanel.KeyboardEditMode)
                            {
                                case KeyboardEditMode.EditOnF2:
                                case KeyboardEditMode.EditOnKeystrokeOrF2:
                                    if (cell.IsEmptyCell == false)
                                    {
                                        if (cell.CanSetActiveCell(cell.GridPanel, cell) == true)
                                        {
                                            cell.BeginEdit(false);
                                            return (true);
                                        }
                                    }
                                    break;
                            }
                        }
                    }
                }

                return (false);
            }

            return (true);
        }

        #endregion 

        #region PressKeyDown

        ///<summary>
        /// Sends the given input scanCode to the
        /// current active edit control
        ///</summary>
        ///<param name="scanCode"></param>
        public void PressKeyDown(ushort scanCode)
        {
            _KeyPressSent = true;

            if (Marshal.SizeOf(new IntPtr()) == 8)
                SendInput64(scanCode);
            else
                SendInput32(scanCode);
        }

        private void SendInput32(ushort scanCode)
        {
            Input32[] inputs = new Input32[1];

            inputs[0].type = InputKeyboard;
            inputs[0].ki = new KeyBoardInput(scanCode);

            SendInput(1, inputs, Marshal.SizeOf(inputs[0]));
        }

        private void SendInput64(ushort scanCode)
        {
            Input64[] inputs = new Input64[1];

            inputs[0].type = InputKeyboard;
            inputs[0].ki = new KeyboardInput64(scanCode);

            SendInput(1, inputs, Marshal.SizeOf(inputs[0]));
        }

        #endregion

        #region GetCellToEdit

        private GridCell GetCellToEdit()
        {
            GridPanel panel = _ActiveGrid;

            if (panel != null && panel.SuperGrid.Focused == true)
            {
                if (panel.ReadOnly == true)
                    return (null);

                if (panel.SelectionGranularity != SelectionGranularity.Cell)
                {
                    GridRow row = panel.ActiveRow as GridRow;

                    if (row != null)
                    {
                        if (panel.PrimaryColumn != null &&
                            panel.PrimaryColumn.Visible == true)
                        {
                            if (row.Cells.Count > panel.PrimaryColumnIndex)
                                return (row.Cells[panel.PrimaryColumnIndex]);
                        }

                        return (row.FirstVisibleCell);
                    }
                }
                else
                {
                    GridCell cell = ActiveElement as GridCell;

                    if (cell != null)
                    {
                        if (cell.AllowSelection == true)
                            return (cell);
                    }
                }
            }

            return (null);
        }

        #endregion

        #region SuperGridControlPreviewKeyDown

        void SuperGridControlPreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            DoPreviewKeyDown(e.KeyData);

            if (e.KeyData == Keys.Tab ||
                e.KeyData == (Keys.Tab | Keys.Shift))
            {
                if (_TabSelection != TabSelection.Control)
                    e.IsInputKey = true;
            }
            else
            {
                if (GridWantsKey(e.KeyData) == true)
                    e.IsInputKey = true;
            }
        }

        #endregion

        #endregion

        #region ProcessInFilterEditKeyDown

        private bool ProcessInFilterEditKeyDown(Keys keyData)
        {
            bool wantsKey = GridWantsKey(keyData);

            wantsKey = ActiveFilterPanel.WantsInputKey(keyData, wantsKey);

            if (wantsKey == false)
                return (ProcessFilterEditKeyDown(keyData));

            return (false);
        }

        #endregion

        #region ProcessInEditKeyDown

        private bool ProcessInEditKeyDown(Keys keyData)
        {
            bool wantsKey = GridWantsKey(keyData);

            wantsKey = _ActiveEditor.WantsInputKey(keyData, wantsKey);

            if (wantsKey == false)
                ProcessInEditGridKeyDown(keyData);

            return (wantsKey == false);
        }

        #endregion

        #region ProcessInEditGridKeyDown

        private void ProcessInEditGridKeyDown(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    _EditorCell.CancelEdit();
                    break;

                case Keys.Enter:
                    if (_EditorCell.EndEdit() == true)
                        ProcessGridKeyDown(keyData);
                    break;

                default:
                    if (GridWantsKey(keyData) == true)
                    {
                        if (_EditorCell.EndEdit() == true)
                            ProcessGridKeyDown(keyData);
                    }
                    break;
            }
        }

        #endregion

        #region ProcessGridKeyDown

        #region ProcessGridKeyDown

        private bool ProcessGridKeyDown(Keys keyData)
        {
            if (ActiveElement == null)
            {
                switch (_PrimaryGrid.SelectionGranularity)
                {
                    case SelectionGranularity.Cell:
                        SetFirstAvailableCell(_PrimaryGrid, false);
                        break;

                    default:
                        SetFirstAvailableRow(_PrimaryGrid, false);
                        break;
                }

                switch (keyData)
                {
                    case Keys.Tab | Keys.Control:
                        Parent.SelectNextControl(this, true, true, true, true);
                        return (true);

                    case Keys.Tab | Keys.Shift | Keys.Control:
                        Parent.SelectNextControl(this, false, true, true, true);
                        return (true);
                }

                return (false);
            }

            bool processed = true;

            if (keyData == Keys.Escape)
                CancelCapture();

            bool ms = ActiveElement.GridPanel.MultiSelect;

            switch (keyData)
            {
                case Keys.NumLock:
                    break;

                case Keys.Down:
                    ProcessDownKey(true, false);
                    break;

                case Keys.Down | Keys.Control:
                    ProcessDownKey(false, false);
                    break;

                case Keys.Down | Keys.Shift:
                case Keys.Down | Keys.Shift | Keys.Control:
                    ProcessDownKey(true, ms);
                    break;

                case Keys.End:
                    ProcessEndKey(false);
                    break;

                case Keys.End | Keys.Shift:
                    ProcessEndKey(ms);
                    break;

                case Keys.End | Keys.Control:
                    ProcessCtrlEndKey(false);
                    break;

                case Keys.End | Keys.Control | Keys.Shift:
                    ProcessCtrlEndKey(ms);
                    break;

                case Keys.Enter:
                    ProcessEnterKey(true, false);
                    break;

                case Keys.Enter | Keys.Shift:
                case Keys.Enter | Keys.Control:
                case Keys.Enter | Keys.Shift | Keys.Control:
                    ProcessShiftEnterKey();
                    break;

                case Keys.Escape:
                    if (ProcessEscapeKey() == false)
                    {
                        Form form = FindForm();

                        if (form != null && form.CancelButton != null)
                            form.CancelButton.PerformClick();
                    }
                    break;

                case Keys.Home:
                    ProcessHomeKey(false);
                    break;

                case Keys.Home | Keys.Shift:
                    ProcessHomeKey(ms);
                    break;

                case Keys.Home | Keys.Control:
                    ProcessCtrlHomeKey(false);
                    break;

                case Keys.Home | Keys.Control | Keys.Shift:
                    ProcessCtrlHomeKey(ms);
                    break;

                case Keys.Left:
                    ProcessLeftKey(true, false);
                    break;

                case Keys.Left | Keys.Shift:
                case Keys.Left | Keys.Shift | Keys.Control:
                    ProcessLeftKey(true, ms);
                    break;

                case Keys.Left | Keys.Control:
                    ProcessLeftKey(false, false);
                    break;

                case Keys.PageUp:
                    ProcessPageUpKey(false);
                    break;

                case Keys.PageUp | Keys.Shift:
                case Keys.PageUp | Keys.Shift | Keys.Control:
                    ProcessPageUpKey(ms);
                    break;

                case Keys.PageDown:
                    ProcessPageDownKey(false);
                    break;

                case Keys.PageDown | Keys.Shift:
                case Keys.PageDown | Keys.Shift | Keys.Control:
                    ProcessPageDownKey(ms);
                    break;

                case Keys.Tab:
                    processed = ProcessTabKey();
                    break;

                case Keys.Tab | Keys.Shift:
                    processed = ProcessShiftTabKey();
                    break;

                case Keys.Tab | Keys.Control:
                    Parent.SelectNextControl(this, true, true, true, true);
                    break;

                case Keys.Tab | Keys.Shift | Keys.Control:
                    Parent.SelectNextControl(this, false, true, true, true);
                    break;

                case Keys.Right:
                    ProcessRightKey(true, false);
                    break;

                case Keys.Right | Keys.Shift:
                case Keys.Right | Keys.Shift | Keys.Control:
                    ProcessRightKey(true, ms);
                    break;

                case Keys.Right | Keys.Control:
                    ProcessRightKey(false, false);
                    break;

                case Keys.Space:
                    if (ActiveElement is GridContainer)
                    {
                        GridContainer item = (GridContainer) ActiveElement;

                        item.Expanded = !item.Expanded;
                    }
                    else
                    {
                        processed = false;
                    }
                    break;

                case Keys.Space | Keys.Shift:
                    ProcessShiftSpaceKey(ms);
                    break;

                case Keys.Space | Keys.Control:
                    ProcessCtrlSpaceKey(ms);
                    break;

                case Keys.Up:
                    ProcessUpKey(true, false);
                    break;

                case Keys.Up | Keys.Shift:
                case Keys.Up | Keys.Shift | Keys.Control:
                    ProcessUpKey(true, ms);
                    break;

                case Keys.Up | Keys.Control:
                    ProcessUpKey(false, false);
                    break;

                case Keys.Delete:
                    ProcessDeleteKey();
                    break;

                case Keys.Delete | Keys.Shift:
                    ProcessShiftDeleteKey();
                    break;

                case Keys.Insert:
                    ProcessInsertKey();
                    break;

                case Keys.Insert | Keys.Shift:
                    ProcessShiftInsertKey();
                    break;

                default:
                    processed = false;
                    break;
            }

            return (processed);
        }

        #endregion

        #region ProcessEscapeKey

        private bool ProcessEscapeKey()
        {
            GridRow row = ActiveRow as GridRow;

            if (row != null)
            {
                GridPanel panel = row.GridPanel;

                if (row.IsInsertRow == false &&
                    row.IsTempInsertRow == true)
                {
                    row.RowNeedsStored = false;

                    if (panel.VirtualMode == true)
                    {
                        panel.VirtualRowCountEx--;
                        panel.VirtualRows.MaxRowIndex = row.RowIndex - 1;
                        panel.LatentActiveRowIndex = row.RowIndex;
                        panel.VirtualTempInsertRow = null;

                        panel.InvalidateLayout();
                        row.Dispose();
                    }
                    else
                    {
                        panel.LatentActiveRowIndex = row.RowIndex;
                        panel.Rows.Remove(row);
                    }

                    ActiveRow = null;
                    panel.ActiveRow = null;

                    return (true);
                }
            }

            return (false);
        }

        #endregion

        #region ProcessPageUpKey

        private void ProcessPageUpKey(bool extend)
        {
            if (ActiveElement is GridCell)
                ProcessCellPageUpKey(extend);

            else if (ActiveElement is GridContainer)
                ProcessRowPageUpKey(extend);
        }

        #region ProcessCellPageUpKey

        private void ProcessCellPageUpKey(bool extend)
        {
            GridCell cell = (GridCell)ActiveElement;

            GridPanel panel = cell.GridPanel;
            GridContainer lrow = cell.GridRow;

            GridContainer container = panel.FirstOnScreenRow;

            if (lrow == container)
            {
                lrow.ScrollToBottom();

                container = panel.FirstOnScreenRow;
            }

            while (container != null)
            {
                GridRow row = container as GridRow;

                if (row != null)
                {
                    GridCell ecell = row.GetCell(cell.ColumnIndex,
                        panel.AllowEmptyCellSelection);

                    if (ecell != null)
                    {
                        if (ecell.AllowSelection == true)
                        {
                            KeySelectCell(panel, ecell, true, extend);
                            break;
                        }
                    }
                }

                container = container.PrevVisibleRow;
            }
        }

        #endregion

        #region ProcessRowPageUpKey

        private void ProcessRowPageUpKey(bool extend)
        {
            GridContainer lrow = (GridContainer)ActiveElement;
            GridPanel panel = lrow.GridPanel;

            GridContainer row = panel.FirstOnScreenRow;

            if (lrow != row)
            {
                KeySelectRow(panel, row, true, extend);
            }
            else
            {
                lrow.ScrollToBottom();

                KeySelectRow(panel, panel.FirstOnScreenRow, true, extend);
            }
        }

        #endregion

        #endregion

        #region ProcessPageDownKey

        private void ProcessPageDownKey(bool extend)
        {
            if (ActiveElement is GridCell)
                ProcessCellPageDownKey(extend);

            else if (ActiveElement is GridContainer)
                ProcessRowPageDownKey(extend);
        }

        #region ProcessCellPageDownKey

        private void ProcessCellPageDownKey(bool extend)
        {
            GridCell cell = (GridCell)ActiveElement;

            GridPanel panel = cell.GridPanel;
            GridContainer lrow = cell.GridRow;

            GridContainer container = panel.LastOnScreenRow;

            if (lrow.GridIndex == container.GridIndex)
            {
                lrow.ScrollToTop();

                container = panel.LastOnScreenRow;
            }

            while (container != null)
            {
                GridRow row = container as GridRow;

                if (row != null)
                {
                    GridCell ecell = row.GetCell(cell.ColumnIndex,
                        panel.AllowEmptyCellSelection);

                    if (ecell != null)
                    {
                        if (ecell.AllowSelection == true)
                        {
                            KeySelectCell(panel, ecell, true, extend);
                            break;
                        }
                    }
                }

                container = container.NextVisibleRow;
            }
        }

        #endregion

        #region ProcessRowPageDownKey

        private void ProcessRowPageDownKey(bool extend)
        {
            GridContainer lrow = (GridContainer)ActiveElement;
            GridPanel panel = lrow.GridPanel;

            GridContainer row = panel.LastOnScreenRow;

            if (lrow != row)
            {
                KeySelectRow(panel, row, true, extend);
            }
            else
            {
                lrow.ScrollToTop();

                row = panel.LastOnScreenRow;

                KeySelectRow(panel, row, true, extend);
            }
        }

        #endregion

        #endregion

        #region ProcessEnterKey

        private void ProcessEnterKey(bool select, bool extend)
        {
            if (ActiveElement is GridCell)
            {
                GridCell cell = (GridCell) ActiveElement;
                GridPanel panel = cell.GridPanel;

                if (cell.GridRow.IsInsertRow == true)
                {
                    cell.BeginEdit(false);
                    cell.EditorDirty = true;
                    cell.EndEdit();
                }
                else
                {
                    if (panel.EnterKeySelectsNextRow == true)
                        ProcessCellDownKey(select, extend);
                    else
                        cell.EndEdit();
                }
            }
            else if (ActiveElement is GridContainer)
            {
                GridContainer row = (GridContainer)ActiveElement;
                GridPanel panel = row.GridPanel;

                if (panel.EnterKeySelectsNextRow == true)
                    ProcessRowDownKey(select, extend);
            }
        }

        #endregion

        #region ProcessShiftEnterKey

        private void ProcessShiftEnterKey()
        {
            if (ActiveElement is GridCell)
                ProcessCellShiftEnterKey();

            else if (ActiveElement is GridContainer)
                ProcessRowShiftEnterKey();
        }

        #region ProcessCellShiftEnterKey

        private void ProcessCellShiftEnterKey()
        {
            GridCell cell = (GridCell)ActiveElement;

            KeySelectRow(cell.GridPanel, cell.GridRow, true, false);
        }

        #endregion

        #region ProcessRowShiftEnterKey

        private void ProcessRowShiftEnterKey()
        {
            GridRow row = (GridRow)ActiveElement;

            GridPanel panel = row.GridPanel;
            GridColumn column = panel.Columns.FirstVisibleColumn;

            if (column != null)
                KeySelectCell(panel, row[column.ColumnIndex], true, false);
        }

        #endregion

        #endregion

        #region ProcessCtrlSpaceKey

        private void ProcessCtrlSpaceKey(bool extend)
        {
            if (ActiveElement is GridCell)
                ProcessCellCtrlSpaceKey(extend);

            else if (ActiveElement is GridContainer)
                ProcessRowCtrlSpaceKey(extend);
        }

        #region ProcessCellCtrlSpaceKey

        private void ProcessCellCtrlSpaceKey(bool extend)
        {
            GridCell cell = (GridCell) ActiveElement;

            if (extend == true)
                KeyCtrlSelectCell(cell);
            else
                KeySelectCell(cell.GridPanel, cell, true, false);
        }

        #endregion

        #region ProcessRowCtrlSpaceKey

        private void ProcessRowCtrlSpaceKey(bool extend)
        {
            GridContainer row = (GridContainer)ActiveElement;

            if (extend == true)
                KeyCtrlSelectRow(row);
            else
                KeySelectRow(row.GridPanel, row, true, false);
        }

        #endregion

        #endregion

        #region ProcessShiftSpaceKey

        private void ProcessShiftSpaceKey(bool extend)
        {
            if (ActiveElement is GridCell)
                ProcessCellShiftSpaceKey(extend);

            else if (ActiveElement is GridContainer)
                ProcessRowShiftSpaceKey(extend);
        }

        #region ProcessCellShiftSpaceKey

        private void ProcessCellShiftSpaceKey(bool extend)
        {
            GridCell cell = (GridCell)ActiveElement;
            GridPanel panel = cell.GridPanel;

            KeySelectCell(panel, cell, true, extend);
        }

        #endregion

        #region ProcessRowShiftSpaceKey

        private void ProcessRowShiftSpaceKey(bool extend)
        {
            GridContainer lrow = (GridContainer)ActiveElement;
            GridPanel panel = (lrow is GridPanel) ? lrow.GetParentPanel() : lrow.GridPanel;

            KeySelectRow(panel, lrow, true, extend);
        }

        #endregion

        #endregion

        #region ProcessUpKey

        private void ProcessUpKey(bool select, bool extend)
        {
            if (ActiveElement is GridCell)
                ProcessCellUpKey(select, extend);

            else if (ActiveElement is GridContainer)
                ProcessRowUpKey(select, extend);
        }

        #region ProcessCellUpKey

        private void ProcessCellUpKey(bool select, bool extend)
        {
            GridCell cell = (GridCell)ActiveElement;
            GridPanel panel = cell.GridPanel;

            _LastCellIndex = cell.ColumnIndex;

            GridContainer prow = GetPrevRow(cell.GridRow);

            if (panel.GroupHeaderKeyBehavior == GroupHeaderKeyBehavior.Select)
            {
                if (prow is GridGroup)
                {
                    KeySelectRow(panel, (GridGroup)cell.GridRow.Parent, select, extend);
                    return;
                }
            }

            GridCell pcell = GetPrevRowCell(prow, cell.ColumnIndex);

            if (KeySelectCell(panel, pcell, select, extend) == false)
            {
                if (VScrollOffset > 0)
                {
                    VScrollOffset -= _VScrollBar.SmallChange;
                }
                else
                {
                    if (select == false && panel.Filter.Visible == true &&
                        cell.GridColumn.IsFilteringEnabled == true)
                    {
                        panel.Filter.ActivateFilterEdit(cell.GridColumn);
                    }
                }
            }
        }

        #region GetPrevRowCell

        private GridCell GetPrevRowCell(GridContainer item, int index)
        {
            while (item != null)
            {
                GridRow row = item as GridRow;

                if (row != null)
                {
                    if (item.AllowSelection == true)
                    {
                        GridCell cell = row.GetCell(index,
                            row.GridPanel.AllowEmptyCellSelection);

                        if (cell != null)
                        {
                            if (cell.AllowSelection == true)
                                return (cell);
                        }
                    }
                }

                item = item.PrevVisibleRow;
            }

            return (null);
        }

        #endregion

        #endregion

        #region ProcessRowUpKey

        private void ProcessRowUpKey(bool select, bool extend)
        {
            GridContainer lrow = (GridContainer)ActiveElement;
            GridPanel panel = (lrow is GridPanel) ? lrow.GetParentPanel() : lrow.GridPanel;

            GridContainer row = GetPrevRow(lrow);

            if (lrow is GridGroup)
            {
                if (_LastCellIndex >= 0 && (row is GridGroup == false))
                {
                    GridCell cell = GetPrevRowCell(row, _LastCellIndex);

                    KeySelectCell(panel, cell, select, extend);
                    return;
                }
            }
            else
            {
                _LastCellIndex = -1;
            }

            if (KeySelectRow(panel, row, select, extend) == false)
            {
                if (VScrollOffset > 0)
                {
                    VScrollOffset -= _VScrollBar.SmallChange;
                }
                else
                {
                    if (select == false && panel.Filter.Visible == true)
                    {
                        GridColumn column = panel.Columns.FirstVisibleColumn;

                        while (column != null)
                        {
                            if (column.IsFilteringEnabled == true)
                            {
                                ActivateFilterPanel(column);
                                break;
                            }

                            column = column.NextVisibleColumn;
                        }
                    }
                }
            }
        }

        #endregion

        #region GetPrevRow

        private GridContainer GetPrevRow(GridContainer lrow)
        {
            GridContainer row = lrow.PrevVisibleRow;

            while (row != null)
            {
                if (row.AllowSelection == true)
                    return (row);

                row = row.PrevVisibleRow;
            }

            return (null);
        }

        #endregion

        #endregion

        #region ProcessDownKey

        private void ProcessDownKey(bool select, bool extend)
        {
            if (ActiveElement is GridCell)
                ProcessCellDownKey(select, extend);

            else if (ActiveElement is GridContainer)
                ProcessRowDownKey(select, extend);
        }

        #region ProcessCellDownKey

        private void ProcessCellDownKey(bool select, bool extend)
        {
            GridCell cell = (GridCell) ActiveElement;
            GridPanel panel = cell.GridPanel;

            _LastCellIndex = cell.ColumnIndex;

            GridContainer nrow = GetNextRow(cell.GridRow);

            if (panel.GroupHeaderKeyBehavior == GroupHeaderKeyBehavior.Select)
            {
                if (nrow is GridGroup)
                {
                    if (KeySelectRow(panel, nrow, select, extend) == false)
                        VScrollOffset += _VScrollBar.SmallChange;

                    return;
                }
            }

            GridCell ncell = GetNextRowCell(nrow, _LastCellIndex);

            if (KeySelectCell(panel, ncell, select, extend) == false)
            {
                if (nrow == null)
                    VScrollOffset += _VScrollBar.SmallChange;
            }
        }

        #region GetNextRowCell

        private GridCell GetNextRowCell(GridContainer item, int index)
        {
            while (item != null)
            {
                GridRow row = item as GridRow;

                if (row != null)
                {
                    if (row.AllowSelection == true)
                    {
                        GridCell cell = row.GetCell(index,
                            row.GridPanel.AllowEmptyCellSelection);

                        if (cell != null)
                        {
                            if (cell.AllowSelection == true)
                                return (cell);
                        }
                    }
                }

                item = item.NextVisibleRow;
            }

            return (null);
        }

        #endregion

        #endregion

        #region ProcessRowDownKey

        private void ProcessRowDownKey(bool select, bool extend)
        {
            GridContainer lrow = (GridContainer)ActiveElement;
            GridPanel panel = (lrow is GridPanel) ? lrow.GetParentPanel() : lrow.GridPanel;

            GridContainer row = GetNextRow(lrow);

            if (lrow is GridGroup)
            {
                if (_LastCellIndex >= 0 && (row is GridGroup == false))
                {
                    GridCell cell = GetNextRowCell(row, _LastCellIndex);

                    KeySelectCell(panel, cell, select, extend);
                    return;
                }
            }
            else
            {
                _LastCellIndex = -1;
            }

            if (KeySelectRow(panel, row, select, extend) == false)
                VScrollOffset += _VScrollBar.SmallChange;
        }

        #endregion

        #region GetNextRow

        private GridContainer GetNextRow(GridContainer lrow)
        {
            GridContainer row = lrow.NextVisibleRow;

            while (row != null)
            {
                if (row.AllowSelection == true)
                    return (row);

                row = row.NextVisibleRow;
            }

            return (null);
        }

        #endregion

        #endregion

        #region ProcessLeftKey

        private bool ProcessLeftKey(bool select, bool extend)
        {
            if (ActiveElement is GridCell)
                return (ProcessCellLeftKey(select, extend));

            if (ActiveElement is GridContainer)
                ProcessRowLeftKey();

            return (true);
        }

        #region ProcessCellLeftKey

        private bool ProcessCellLeftKey(bool select, bool extend)
        {
            GridCell lcell = (GridCell) ActiveElement;
            GridPanel panel = lcell.GridPanel;

            if ((ModifierKeys & (Keys.Shift | Keys.Control)) == (Keys.Shift | Keys.Control))
            {
                KeySelectRow(panel, lcell.GridRow, true, false);

                return (true);
            }
            
            GridCell cell = lcell.GetPreviousCell(true, !extend);

            bool selected = KeySelectCell(panel, cell, select, extend);

            if (selected == false)
                HScrollOffset -= _HScrollBar.SmallChange;

            return (selected);
        }

        #endregion

        #region ProcessRowLeftKey

        private void ProcessRowLeftKey()
        {
            GridContainer lrow = (GridContainer)ActiveElement;

            if (lrow.Expanded == true)
            {
                lrow.Expanded = false;
            }
            else
            {
                GridContainer crow = lrow.Parent as GridContainer;

                if (crow != null && crow.Parent != null)
                {
                    ActiveElement = crow;

                    crow.GridPanel.ClearAll(true);
                    crow.IsSelected = true;

                    crow.EnsureVisible();
                }
                else
                {
                    GridRow row = lrow as GridRow;

                    if (row != null)
                    {
                        if (lrow.GridPanel.SelectionGranularity == SelectionGranularity.Cell)
                        {
                            GridCell cell = row.LastSelectableCell;

                            if (cell != null)
                                KeySelectCell(row.GridPanel, cell, true, false);
                        }
                        else
                        {
                            HScrollOffset -= _HScrollBar.SmallChange;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region ProcessRightKey

        private bool ProcessRightKey(bool select, bool extend)
        {
            if (ActiveElement is GridCell)
                return (ProcessCellRightKey(select, extend));

            if (ActiveElement is GridContainer)
                ProcessRowRightKey();

            return (true);
        }

        #region ProcessCellRightKey

        private bool ProcessCellRightKey(bool select, bool extend)
        {
            GridCell lcell = (GridCell) ActiveElement;
            GridPanel panel = lcell.GridPanel;

            GridCell cell = lcell.GetNextCell(true, !extend);

            bool selected = KeySelectCell(panel, cell, select, extend);
            
            if (selected == false)
                HScrollOffset += _HScrollBar.SmallChange;

            return (selected);
        }

        #endregion

        #region ProcessRowRightKey

        private void ProcessRowRightKey()
        {
            GridContainer lrow = (GridContainer)ActiveElement;

            if (lrow is GridPanel)
            {
                GridPanel panel = (GridPanel)lrow;
                GridRow row = panel.GetRowFromIndex(0) as GridRow;

                if (row != null)
                {
                    ActiveElement = row;

                    lrow.GetParentPanel().ClearAll(true);
                    lrow.GridPanel.ClearAll(true);

                    row.IsSelected = true;

                    row.EnsureVisible();
                }
            }
            else
            {
                if (lrow.Rows.Count > 0 || lrow.RowsUnresolved == true)
                {
                    if (lrow.Expanded == false)
                        lrow.Expanded = true;
                }
                else
                {
                    GridRow row = lrow as GridRow;

                    if (row != null)
                    {
                        if (lrow.GridPanel.SelectionGranularity == SelectionGranularity.Cell)
                        {
                            GridCell cell = row.FirstSelectableCell;

                            if (cell != null)
                                KeySelectCell(row.GridPanel, cell, true, false);
                        }
                        else
                        {
                            HScrollOffset += _HScrollBar.SmallChange;
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region ProcessTabKey

        private bool ProcessTabKey()
        {
            if (_TabSelection == TabSelection.Control)
                return (false);

            if (ActiveElement is GridCell)
            {
                GridCell lcell = (GridCell)ActiveElement;
                GridPanel panel = lcell.GridPanel;

                GridCell cell =
                    lcell.GetNextCell(true, _TabSelection == TabSelection.Cell);

                KeySelectCell(panel, cell, true, false);

                return (true);
            }

            GridRow row = ActiveElement as GridRow;

            if (row != null)
            {
                if (row.GridPanel.SelectionGranularity != SelectionGranularity.Cell)
                {
                    GridCell cell = row.FirstOnScreenCell;

                    if (cell != null)
                    {
                        GridCell cellNext = cell.GetNextCell(false, false);

                        if (cellNext != null)
                        {
                            int n = 0;

                            GridColumn col = row.GridPanel.Columns.FirstVisibleColumn;

                            while (col != null && col.ColumnIndex != cellNext.ColumnIndex)
                            {
                                n += col.Bounds.Width;

                                col = col.NextVisibleColumn;
                            }

                            HScrollOffset = n;
                        }
                    }

                    return (true);
                }
            }

            return (ProcessRightKey(true, false));
        }

        #endregion

        #region ProcessShiftTabKey

        private bool ProcessShiftTabKey()
        {
            if (_TabSelection == TabSelection.Control)
                return (false);

            if (ActiveElement is GridCell)
            {
                GridCell lcell = (GridCell)ActiveElement;
                GridPanel panel = lcell.GridPanel;

                GridCell cell =
                    lcell.GetPreviousCell(true, _TabSelection == TabSelection.Cell);

                return (KeySelectCell(panel, cell, true, false));
            }

            GridRow row = ActiveElement as GridRow;

            if (row != null)
            {
                if (row.GridPanel.SelectionGranularity != SelectionGranularity.Cell)
                {
                    if (_HScrollOffset > 0)
                    {
                        GridCell cell = row.FirstOnScreenCell;

                        if (cell != null)
                        {
                            Rectangle t = row.GridPanel.ViewRect;
                            Rectangle r = cell.Bounds;

                            GridCell fcell = row.LastFrozenCell;

                            if (fcell != null)
                                t.X = fcell.Bounds.Right;

                            if (r.Left >= t.Left)
                                cell = cell.GetPreviousCell(false, false);

                            if (cell != null)
                                cell.EnsureVisible();

                            return (true);
                        }
                    }
                }
            }

            return (ProcessLeftKey(true, false));
        }

        #endregion

        #region ProcessHomeKey

        private void ProcessHomeKey(bool extend)
        {
            if (ActiveElement is GridCell)
            {
                GridCell lcell = (GridCell)ActiveElement;
                GridPanel panel = lcell.GridPanel;

                GridCell cell = lcell.GridRow.FirstSelectableCell;

                if (cell != lcell)
                    KeySelectCell(panel, cell, true, extend);
            }
            else if (ActiveElement is GridRow)
            {
                GridRow row = (GridRow)ActiveElement;
                GridPanel panel = row.GridPanel;

                KeySelectRow(panel, panel.FirstSelectableRow, true, extend);
            }
        }

        #endregion

        #region ProcessCtrlHomeKey

        private void ProcessCtrlHomeKey(bool extend)
        {
            if (ActiveElement is GridCell)
            {
                GridCell cell = (GridCell)ActiveElement;
                GridPanel panel = cell.GridPanel;

                SetFirstAvailableCell(panel, extend);
            }
            else if (ActiveElement is GridRow)
            {
                KeySelectRow(_PrimaryGrid, _PrimaryGrid.FirstSelectableRow, true, extend);
            }
        }

        #endregion

        #region ProcessEndKey

        private void ProcessEndKey(bool extend)
        {
            if (ActiveElement is GridCell)
            {
                GridCell lcell = (GridCell)ActiveElement;

                GridPanel panel = lcell.GridPanel;
                GridCell cell = lcell.GridRow.LastSelectableCell;

                if (cell != lcell)
                    KeySelectCell(panel, cell, true, extend);
            }
            else if (ActiveElement is GridRow)
            {
                GridRow row = (GridRow)ActiveElement;
                GridPanel panel = row.GridPanel;

                KeySelectRow(panel, panel.LastSelectableRow, true, extend);
            }
        }

        #endregion

        #region ProcessCtrlEndKey

        private void ProcessCtrlEndKey(bool extend)
        {
            if (ActiveElement is GridCell)
            {
                GridCell cell = (GridCell)ActiveElement;
                GridPanel panel = cell.GridPanel;

                SetLastAvailableCell(panel, extend);
            }
            else if (ActiveElement is GridRow)
            {
                KeySelectRow(_PrimaryGrid, _PrimaryGrid.LastVisibleRow, true, extend);
            }
        }

        #endregion

        #region SetFirstAvailableRow

        private void SetFirstAvailableRow(GridPanel panel, bool extend)
        {
            GridContainer container = panel.FirstVisibleRow;
            
            while (container != null)
            {
                GridRow row = container as GridRow;

                if (row != null)
                {
                    if (row.AllowSelection == true)
                    {
                        KeySelectRow(panel, row, true, extend);
                        break;
                    }
                }

                container = container.NextVisibleRow;
            }
        }

        #endregion

        #region SetFirstAvailableCell

        private void SetFirstAvailableCell(GridPanel panel, bool extend)
        {
            GridColumn column = panel.Columns.FirstVisibleColumn;
            GridContainer container = panel.FirstVisibleRow;

            if (column != null)
            {
                while (container != null)
                {
                    GridRow row = container as GridRow;

                    if (row != null)
                    {
                        if (row.AllowSelection == true)
                        {
                            GridCell cell = row.GetCell(column.ColumnIndex,
                                panel.AllowEmptyCellSelection);

                            if (cell != null)
                            {
                                if (cell.AllowSelection == false)
                                    cell = cell.GetNextCell(true);

                                KeySelectCell(panel, cell, true, extend);
                            }
                            break;
                        }
                    }

                    container = container.NextVisibleRow;
                }
            }
        }

        #endregion

        #region SetLastAvailableCell

        private void SetLastAvailableCell(GridPanel panel, bool extend)
        {
            GridColumn column = panel.Columns.LastVisibleColumn;
            GridContainer container = panel.LastVisibleRow;

            while (container != null)
            {
                GridRow row = container as GridRow;

                if (row != null)
                {
                    if (row.AllowSelection == true)
                    {
                        GridCell cell = row.GetCell(column.ColumnIndex,
                            panel.AllowEmptyCellSelection);

                        if (cell != null)
                        {
                            if (cell.AllowSelection == false)
                                cell = cell.GetPreviousCell(true);

                            KeySelectCell(panel, cell, true, extend);
                        }
                        break;
                    }
                }

                container = container.PrevVisibleRow;
            }
        }

        #endregion

        #region KeySelectCell

        internal bool KeySelectCell(
            GridPanel panel, GridCell cell, bool select, bool extend)
        {
            if (cell != null)
            {
                int rowIndex = cell.GridRow.RowIndex;

                if (cell.CanSetActiveCell(panel, cell) == true)
                {
                    if (select == true && extend == false)
                    {
                        panel.SelectionRowAnchor = cell.GridRow;
                        panel.SelectionColumnAnchor = cell.GridColumn;
                    }

                    if (select == true)
                    {
                        if (rowIndex != cell.GridRow.RowIndex)
                            ArrangeGrid();

                        cell.ExtendSelection(panel, cell, extend);

                        if (cell.CellEditMode == CellEditMode.Modal)
                        {
                            if (panel.KeyboardEditMode == KeyboardEditMode.EditOnEntry)
                                cell.BeginEdit(true);
                        }
                    }
                    else
                    {
                        if (ActiveElement != null)
                            ActiveElement.InvalidateRender();

                        cell.InvalidateRender();
                    }

                    panel.LastProcessedItem = cell;

                    cell.EnsureVisible();

                    return (true);
                }
            }

            return (false);
        }

        #endregion

        #region KeyCtrlSelectCell

        private void KeyCtrlSelectCell(GridCell cell)
        {
            if (cell != null)
            {
                ActiveElement = cell;

                cell.IsSelected = !cell.IsSelected;

                cell.EnsureVisible();
            }
        }

        #endregion

        #region KeySelectRow

        internal bool KeySelectRow(GridPanel panel,
            GridContainer row, bool select, bool extend)
        {
            if (row != null)
            {
                int rowIndex = row.RowIndex;

                if (row.CanSetActiveRow(false) == true)
                {
                    if (panel.SetActiveRow(row, true) == true)
                    {
                        if (select == true && extend == false)
                            panel.SelectionRowAnchor = row;
                    }

                    if (select == true)
                    {
                        if (rowIndex != row.RowIndex)
                            ArrangeGrid();

                        row.ExtendSelection(panel, row, extend);

                        panel.InvalidateRender();
                    }
                    else
                    {
                        if (ActiveElement != null)
                            ActiveElement.InvalidateRender();

                        row.InvalidateRender();
                    }

                    panel.LastProcessedItem = row;

                    row.EnsureVisible();
                }

                return (true);
            }

            return (false);
        }

        #endregion

        #region KeyCtrlSelectRow

        private void KeyCtrlSelectRow(GridContainer row)
        {
            if (row != null)
            {
                ActiveElement = row;

                row.IsSelected = !row.IsSelected;

                row.EnsureVisible();
            }
        }

        #endregion

        #region ProcessDeleteKey

        private void ProcessDeleteKey()
        {
            if (CanDeleteRows() == true)
            {
                SelectedElements selRows =
                    _ActiveGrid.InternalSelectedRows.Copy();

                if (selRows.Count > 0)
                {
                    if (_ActiveGrid.ShowInsertRow == true)
                    {
                        if (_ActiveGrid.VirtualMode == true)
                        {
                            selRows.RemoveItem(_ActiveGrid.VirtualRowCountEx - 1);
                        }
                        else
                        {
                            int n = _ActiveGrid.Rows.Count - 1;

                            GridRow row = _ActiveGrid.Rows[n] as GridRow;

                            if (row != null)
                            {
                                if (row.IsInsertRow == false)
                                    throw new Exception();

                                selRows.RemoveItem(row.GridIndex);
                            }
                        }
                    }
                }

                if (selRows.Count > 0)
                {
                    if (DoRowDeletingEvent(_ActiveGrid, selRows) == false)
                    {
                        if (selRows.Count > 0)
                            DeleteRows(_ActiveGrid, selRows);
                    }
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        #region CanDeleteRows

        private bool CanDeleteRows()
        {
            if (_ActiveGrid == null)
                return (false);

            return (_ActiveGrid.CanDeleteRow);
        }

        #endregion

        #region DeleteRows

        private void DeleteRows(GridPanel panel, SelectedElements selRows)
        {
            BeginUpdate();

            for (int i = selRows.Ranges.Count - 1; i >= 0; --i)
            {
                SelectedRange range = selRows.Ranges[i];

                if (panel.VirtualMode == true)
                {
                    panel.SetDeletedRows(range.StartIndex, range.Count, true);
                }
                else
                {
                    for (int j = range.EndIndex; j >= range.StartIndex; --j)
                    {
                        GridContainer row = panel.GetRowFromIndex(j);

                        if (row != null)
                            row.IsDeleted = true;
                    }
                }
            }

            if (_ActiveRow != null && _ActiveRow.IsDeleted == true)
            {
                if (_ActiveRow.Parent != null)
                    ActiveRow = ((GridContainer)_ActiveRow.Parent).GetNextLocalItem(_ActiveRow.RowIndex);
            }

            //panel.UpdateRowCountEx();

            EndUpdate();

            DoRowDeletedEvent(panel, selRows);
        }

        #endregion

        #endregion

        #region ProcessShiftDeleteKey

        private void ProcessShiftDeleteKey()
        {
            if (_ActiveGrid != null)
            {
                if (_ActiveGrid.AllowRowDelete == true)
                {
                    BeginUpdate();

                    SelectedElements selRows =
                        _ActiveGrid.InternalSelectedRows.Copy();

                    if (DoRowRestoringEvent(_ActiveGrid, selRows) == false)
                    {
                        if (selRows.Count > 0)
                            RestoreRows(_ActiveGrid, selRows);
                    }

                    EndUpdate();
                }
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        #region RestoreRows

        private void RestoreRows(GridPanel panel, SelectedElements selRows)
        {
            for (int i = selRows.Ranges.Count - 1; i >= 0; --i)
            {
                SelectedRange range = selRows.Ranges[i];

                if (panel.VirtualMode == true)
                {
                    panel.SetDeletedRows(range.StartIndex, range.Count, false);
                }
                else
                {
                    for (int j = range.EndIndex; j >= range.StartIndex; --j)
                    {
                        GridContainer row = panel.GetRowFromIndex(j);

                        if (row != null)
                            row.IsDeleted = false;
                    }
                }
            }

            DoRowRestoredEvent(panel, selRows);
        }

        #endregion

        #endregion

        #region ProcessInsertKey

        private void ProcessInsertKey()
        {
            ProcessInsert(0);
        }

        #endregion

        #region ProcessShiftInsertKey

        private void ProcessShiftInsertKey()
        {
            ProcessInsert(1);
        }

        #endregion

        #region ProcessInsert

        private void ProcessInsert(int offset)
        {
            if (CanInsertRow() == true)
            {
                int index = ActiveRow.RowIndex + offset;

                if (index >= 0)
                    _ActiveGrid.InsertRow(index);
            }
            else
            {
                SystemSounds.Beep.Play();
            }
        }

        #region CanInsertRow

        private bool CanInsertRow()
        {
            if (_ActiveGrid == null ||
                _ActiveRow == null || _ActiveRow.IsDeleted == true)
            {
                return (false);
            }

            return (_ActiveGrid.CanInsertRow);
        }

        #endregion

        #endregion

        #endregion

        #region ProcessFilterEditKeyDown

        #region ProcessFilterEditKeyDown

        private bool ProcessFilterEditKeyDown(Keys keyData)
        {
            bool processed = true;

            switch (keyData)
            {
                case Keys.Enter:
                    ExitFilterEdit(false);
                    break;

                case Keys.Escape:
                    ExitFilterEdit(true);
                    break;

                case Keys.Right:
                    ProcessFilterKeyRight();
                    break;

                case Keys.Left:
                    ProcessFilterKeyLeft();
                    break;

                case Keys.Tab:
                    processed = ProcessFilterTabKey();
                    break;

                case Keys.Tab | Keys.Shift:
                    processed = ProcessFilterShiftTabKey();
                    break;

                case Keys.Down | Keys.Control:
                    ExitFilterEdit(false);
                    break;

                default:
                    processed = false;
                    break;
            }

            return (processed);
        }

        #region ProcessFilterKeyLeft

        private void ProcessFilterKeyLeft()
        {
            GridColumn column = ActiveFilterPanel.GridColumn.PrevVisibleColumn;

            while (column != null)
            {
                if (column.IsFilteringEnabled == true)
                {
                    ActivateFilterPanel(column);
                    break;
                }

                column = column.PrevVisibleColumn;
            }
        }

        #endregion

        #region ProcessFilterKeyRight

        private void ProcessFilterKeyRight()
        {
            GridColumn column = ActiveFilterPanel.GridColumn.NextVisibleColumn;

            while (column != null)
            {
                if (column.IsFilteringEnabled == true)
                {
                    ActivateFilterPanel(column);
                    break;
                }

                column = column.NextVisibleColumn;
            }
        }

        #endregion

        #region ActivateFilterPanel

        private void ActivateFilterPanel(GridColumn col)
        {
            if (col != null)
            {
                if (ActiveFilterPanel != null)
                    ActiveFilterPanel.EndEdit();

                FilterPanel fp = FilterPanel.GetFilterPanel(col);

                if (fp != null)
                    fp.BeginEdit();
            }
        }

        #endregion

        #region ExitFilterEdit

        private void ExitFilterEdit(bool cancel)
        {
            GridColumn column = ActiveFilterPanel.GridColumn;
            GridPanel panel = column.GridPanel;

            if (cancel == true)
                ActiveFilterPanel.CancelEdit();
            else
                ActiveFilterPanel.EndEdit();

            GridRow row = panel.FirstSelectableRow as GridRow;

            if (row != null)
            {
                if (_LastProcessedItem is GridContainer)
                {
                    row.SetActive();
                    row.IsSelected = true;
                }
                else if (_LastProcessedItem is GridCell)
                {
                    GridCell cell = GetNextRowCell(row, column.ColumnIndex);

                    if (cell != null)
                    {
                        cell.SetActive();
                        cell.IsSelected = true;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region ProcessFilterTabKey

        private bool ProcessFilterTabKey()
        {
            if (_TabSelection == TabSelection.Control)
                return (false);

            ProcessFilterKeyRight();

            return (true);
        }

        #endregion

        #region ProcessFilterShiftTabKey

        private bool ProcessFilterShiftTabKey()
        {
            if (_TabSelection == TabSelection.Control)
                return (false);

            ProcessFilterKeyLeft();

            return (true);
        }

        #endregion

        #endregion

        #region GridWantsKey

        private bool GridWantsKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:

                case Keys.Up | Keys.Shift:
                case Keys.Up:

                case Keys.Down:
                case Keys.Down | Keys.Shift:

                case Keys.Left:
                case Keys.Left | Keys.Shift:

                case Keys.Right:
                case Keys.Right | Keys.Shift:

                case Keys.Enter:

                case Keys.Tab:
                case Keys.Tab | Keys.Shift:
                    return (true);
            }

            return (false);
        }

        #endregion

        #region DoEvent support

        #region ActiveGridChangedEvent

        /// <summary>
        /// Handles invocation of ActiveGridChanged events
        /// </summary>
        internal void DoActiveGridChangedEvent(GridPanel oldPanel)
        {
            if (ActiveGridChanged != null)
            {
                GridActiveGridChangedEventArgs ev = new
                    GridActiveGridChangedEventArgs(oldPanel, _ActiveGrid);

                ActiveGridChanged(this, ev);
            }
        }

        #endregion

        #region CellActivatedEvent

        /// <summary>
        /// Handles invocation of CellActivated events
        /// </summary>
        internal void DoCellActivatedEvent(
            GridPanel gridPanel, GridCell oldCell, GridCell newCell)
        {
            if (CellActivated != null)
            {
                GridCellActivatedEventArgs ev = new
                    GridCellActivatedEventArgs(gridPanel, oldCell, newCell);

                CellActivated(this, ev);
            }
        }

        #endregion

        #region CellActivatingEvent

        /// <summary>
        /// Handles invocation of CellActivating events
        /// </summary>
        internal bool DoCellActivatingEvent(
            GridPanel gridPanel, GridCell oldCell, GridCell newCell)
        {
            if (CellActivating != null)
            {
                GridCellActivatingEventArgs ev = new
                    GridCellActivatingEventArgs(gridPanel, oldCell, newCell);

                CellActivating(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region CellClickEvent

        /// <summary>
        /// Handles invocation of CellClick events
        /// </summary>
        internal bool DoCellClickEvent(GridCell gridCell, MouseEventArgs e)
        {
            if (CellClick != null)
            {
                GridCellClickEventArgs ev = new
                    GridCellClickEventArgs(gridCell.GridPanel, gridCell, e);

                CellClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region CellDoubleClickEvent

        /// <summary>
        /// Handles invocation of CellDoubleClick events
        /// </summary>
        internal bool DoCellDoubleClickEvent(GridCell gridCell, MouseEventArgs e)
        {
            if (CellDoubleClick != null)
            {
                GridCellDoubleClickEventArgs ev = new
                    GridCellDoubleClickEventArgs(gridCell.GridPanel, gridCell, e);

                CellDoubleClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region CellInfoClickEvent

        /// <summary>
        /// Handles invocation of CellInfoClick events
        /// </summary>
        internal void DoCellInfoClickEvent(GridCell gridCell, MouseEventArgs e)
        {
            if (CellInfoClick != null)
            {
                GridCellClickEventArgs ev = new
                    GridCellClickEventArgs(gridCell.GridPanel, gridCell, e);

                CellInfoClick(this, ev);
            }
        }

        #endregion

        #region CellInfoDoubleClickEvent

        /// <summary>
        /// Handles invocation of CellInfoDoubleClick events
        /// </summary>
        internal void DoCellInfoDoubleClickEvent(GridCell gridCell, MouseEventArgs e)
        {
            if (CellInfoDoubleClick != null)
            {
                GridCellDoubleClickEventArgs ev = new
                    GridCellDoubleClickEventArgs(gridCell.GridPanel, gridCell, e);

                CellInfoDoubleClick(this, ev);
            }
        }

        #endregion

        #region CellInfoEnterEvent

        /// <summary>
        /// Handles invocation of CellInfoEnter events
        /// </summary>
        internal bool DoCellInfoEnterEvent(GridCell gridCell, Point pt)
        {
            if (CellInfoEnter != null)
            {
                GridCellInfoEnterEventArgs ev = new
                    GridCellInfoEnterEventArgs(gridCell.GridPanel, gridCell, pt);

                CellInfoEnter(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region CellInfoLeaveEvent

        /// <summary>
        /// Handles invocation of CellInfoLeave events
        /// </summary>
        internal void DoCellInfoLeaveEvent(GridCell gridCell)
        {
            if (CellInfoLeave != null)
            {
                GridCellInfoLeaveEventArgs ev = new
                    GridCellInfoLeaveEventArgs(gridCell.GridPanel, gridCell);

                CellInfoLeave(this, ev);
            }
        }

        #endregion

        #region CellMouseDownEvent

        /// <summary>
        /// Handles invocation of CellMouseDown events
        /// </summary>
        internal void DoCellMouseDownEvent(GridCell gridCell, MouseEventArgs args)
        {
            if (CellMouseDown != null)
            {
                GridCellMouseEventArgs ev = new
                    GridCellMouseEventArgs(gridCell.GridPanel, gridCell, args);

                CellMouseDown(this, ev);
            }
        }

        #endregion

        #region CellMouseEnterEvent

        /// <summary>
        /// Handles invocation of CellMouseEnter events
        /// </summary>
        internal void DoCellMouseEnterEvent(GridCell gridCell)
        {
            if (CellMouseEnter != null)
            {
                GridCellEventArgs ev = new
                    GridCellEventArgs(gridCell.GridPanel, gridCell);

                CellMouseEnter(this, ev);
            }
        }

        #endregion

        #region CellMouseLeaveEvent

        /// <summary>
        /// Handles invocation of CellMouseLeave events
        /// </summary>
        internal void DoCellMouseLeaveEvent(GridCell gridCell)
        {
            if (CellMouseLeave != null)
            {
                GridCellEventArgs ev = new
                    GridCellEventArgs(gridCell.GridPanel, gridCell);

                CellMouseLeave(this, ev);
            }
        }

        #endregion

        #region CellMouseMoveEvent

        /// <summary>
        /// Handles invocation of CellMouseMove events
        /// </summary>
        internal void DoCellMouseMoveEvent(GridCell gridCell, MouseEventArgs args)
        {
            if (CellMouseMove != null)
            {
                GridCellMouseEventArgs ev = new 
                    GridCellMouseEventArgs(gridCell.GridPanel, gridCell, args);

                CellMouseMove(this, ev);
            }
        }

        #endregion

        #region CellMouseUpEvent

        /// <summary>
        /// Handles invocation of CellMouseUp events
        /// </summary>
        internal void DoCellMouseUpEvent(GridCell gridCell, MouseEventArgs args)
        {
            if (CellMouseUp != null)
            {
                GridCellMouseEventArgs ev = new
                    GridCellMouseEventArgs(gridCell.GridPanel, gridCell, args);

                CellMouseUp(this, ev);
            }
        }

        #endregion

        #region CellUserFunctionEvent

        /// <summary>
        /// Handles invocation of CellUserFunction events
        /// </summary>
        internal void DoCellUserFunctionEvent(
            GridCell gridCell, object[] args, ref object result)
        {
            if (CellUserFunction != null)
            {
                GridCellUserFunctionEventArgs ev = new
                    GridCellUserFunctionEventArgs(gridCell.GridPanel, gridCell, args, result);

                CellUserFunction(this, ev);

                result = ev.Result;
            }
        }

        #endregion

        #region CellValidatingEvent

        /// <summary>
        /// Handles invocation of CellValidating events
        /// </summary>
        internal bool DoCellValidatingEvent(
            GridCell gridCell, object value, object formattedValue)
        {
            if (CellValidating != null)
            {
                GridCellValidatingEventArgs ev = new GridCellValidatingEventArgs(
                    gridCell.GridPanel, gridCell, value, formattedValue);

                CellValidating(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region CellValidatedEvent

        /// <summary>
        /// Handles invocation of CellValidated events
        /// </summary>
        internal void DoCellValidatedEvent(GridCell gridCell)
        {
            if (CellValidated != null)
            {
                GridCellValidatedEventArgs ev = new
                    GridCellValidatedEventArgs(gridCell.GridPanel, gridCell);

                CellValidated(this, ev);
            }
        }

        #endregion

        #region CellValueChangedEvent

        /// <summary>
        /// Handles invocation of CellValueChanged events
        /// </summary>
        internal void DoCellValueChangedEvent(
            GridCell gridCell, object oldValue, object newValue, DataContext context)
        {
            if (CellValueChanged != null)
            {
                GridCellValueChangedEventArgs ev = new
                    GridCellValueChangedEventArgs(gridCell.GridPanel, gridCell, oldValue, newValue, context);

                CellValueChanged(this, ev);
            }
        }

        #endregion

        #region Check events

        #region AfterCheckEvent

        /// <summary>
        /// Handles invocation of AfterCheckEvent events
        /// </summary>
        internal void DoAfterCheckEvent(GridPanel gridPanel, GridElement item)
        {
            if (AfterCheck != null)
            {
                GridAfterCheckEventArgs ev = new
                    GridAfterCheckEventArgs(gridPanel, item);

                AfterCheck(this, ev);
            }
        }

        #endregion

        #region BeforeCheckEvent

        /// <summary>
        /// Handles invocation of BeforeCheck events
        /// </summary>
        internal bool DoBeforeCheckEvent(GridPanel gridPanel, GridElement gridRow)
        {
            if (BeforeCheck != null)
            {
                GridBeforeCheckEventArgs ev = new
                    GridBeforeCheckEventArgs(gridPanel, gridRow);

                BeforeCheck(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #endregion

        #region Collapse events

        #region AfterCollapseEvent

        /// <summary>
        /// Handles invocation of AfterCollapse events
        /// </summary>
        internal void DoAfterCollapseEvent(GridPanel gridPanel, GridContainer gridContainer, ExpandSource expandSource)
        {
            if (AfterCollapse != null)
            {
                GridAfterCollapseEventArgs ev = new
                    GridAfterCollapseEventArgs(gridPanel, gridContainer, expandSource);

                AfterCollapse(this, ev);
            }
        }

        #endregion

        #region BeforeCollapseEvent

        /// <summary>
        /// Handles invocation of BeforeCollapse events
        /// </summary>
        internal bool DoBeforeCollapseEvent(GridPanel gridPanel, GridContainer gridContainer, ExpandSource expandSource)
        {
            if (BeforeCollapse != null)
            {
                GridBeforeCollapseEventArgs ev = new
                    GridBeforeCollapseEventArgs(gridPanel, gridContainer, expandSource);

                BeforeCollapse(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #endregion

        #region Column events

        #region ColumnGroupedEvent

        /// <summary>
        /// Handles invocation of ColumnGrouped events
        /// </summary>
        internal void DoColumnGroupedEvent(
            GridPanel gridPanel, GridColumn gridColumn, GridGroup gridGroup)
        {
            if (ColumnGrouped != null)
            {
                GridColumnGroupedEventArgs ev = new
                    GridColumnGroupedEventArgs(gridPanel, gridColumn, gridGroup);

                ColumnGrouped(this, ev);
            }
        }

        #endregion

        #region ColumnHeaderClickEvent

        /// <summary>
        /// Handles invocation of ColumnHeaderClick events
        /// </summary>
        internal bool DoColumnHeaderClickEvent(
            GridPanel gridPanel, GridColumn gridColumn, MouseEventArgs e)
        {
            if (ColumnHeaderClick != null)
            {
                GridColumnHeaderClickEventArgs ev = new
                    GridColumnHeaderClickEventArgs(gridPanel, gridColumn, e);

                ColumnHeaderClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region ColumnHeaderDoubleClickEvent

        /// <summary>
        /// Handles invocation of ColumnHeaderDoubleClick events
        /// </summary>
        internal bool DoColumnHeaderDoubleClickEvent(GridColumn gridColumn, MouseEventArgs e)
        {
            if (ColumnHeaderDoubleClick != null)
            {
                GridColumnHeaderDoubleClickEventArgs ev = new
                    GridColumnHeaderDoubleClickEventArgs(gridColumn.GridPanel, gridColumn, e);

                ColumnHeaderDoubleClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region ColumnHeaderMarkupLinkClick Event

        /// <summary>
        /// Handles invocation of ColumnHeaderMarkupLinkClick events
        /// </summary>
        internal void DoColumnHeaderMarkupLinkClickEvent(
            GridColumn gridColumn, HyperLink hyperLink)
        {
            if (ColumnHeaderMarkupLinkClick != null)
            {
                GridColumnHeaderMarkupLinkClickEventArgs ev = new GridColumnHeaderMarkupLinkClickEventArgs(
                    gridColumn.GridPanel, gridColumn, hyperLink.Name, hyperLink.HRef);

                ColumnHeaderMarkupLinkClick(this, ev);
            }
        }

        #endregion

        #region ColumnRowHeaderClickEvent

        /// <summary>
        /// Handles invocation of ColumnRowHeaderClick events
        /// </summary>
        internal void DoColumnRowHeaderClickEvent(GridPanel gridPanel)
        {
            if (ColumnRowHeaderClick != null)
            {
                GridEventArgs ev = new GridEventArgs(gridPanel);

                ColumnRowHeaderClick(this, ev);
            }
        }

        #endregion

        #region ColumnRowHeaderDoubleClickEvent

        /// <summary>
        /// Handles invocation of ColumnRowHeaderDoubleClick events
        /// </summary>
        internal void DoColumnRowHeaderDoubleClickEvent(GridPanel gridPanel)
        {
            if (ColumnRowHeaderDoubleClick != null)
            {
                GridEventArgs ev = new GridEventArgs(gridPanel);

                ColumnRowHeaderDoubleClick(this, ev);
            }
        }

        #endregion

        #region ColumnMovedEvent

        /// <summary>
        /// Handles invocation of ColumnMoved events
        /// </summary>
        internal void DoColumnMovedEvent(GridPanel gridPanel, GridColumn gridColumn)
        {
            if (ColumnMoved != null)
            {
                GridColumnEventArgs ev = new
                    GridColumnEventArgs(gridPanel, gridColumn);

                ColumnMoved(this, ev);
            }
        }

        #endregion

        #region ColumnResizedEvent

        /// <summary>
        /// Handles invocation of ColumnResized events
        /// </summary>
        internal void DoColumnResizedEvent(GridPanel gridPanel, GridColumn gridColumn)
        {
            if (ColumnResized != null)
            {
                GridColumnEventArgs ev = new
                    GridColumnEventArgs(gridPanel, gridColumn);

                ColumnResized(this, ev);
            }
        }

        #endregion

        #endregion

        #region CompareElementsEvent

        /// <summary>
        /// Handles invocation of CompareElementsEvent events
        /// </summary>
        internal bool DoCompareElementsEvent(GridPanel panel,
            GridElement a, GridElement b, ref int result)
        {
            if (CompareElements != null)
            {
                GridCompareElementsEventArgs ev = new
                    GridCompareElementsEventArgs(panel, a, b);

                CompareElements(this, ev);

                result = ev.Result;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region DataBindingStartEvent

        /// <summary>
        /// Handles invocation of DataBindingStart events
        /// </summary>
        internal bool DoDataBindingStartEvent(GridPanel gridPanel,
            GridRow row, string tableName, ref bool autogen, ref ProcessChildRelations crProcess)
        {
            if (DataBindingStart != null)
            {
                GridDataBindingStartEventArgs ev = new
                    GridDataBindingStartEventArgs(gridPanel, row, tableName, autogen, crProcess);

                DataBindingStart(this, ev);

                autogen = ev.AutoGenerateColumns;
                crProcess = ev.ProcessChildRelations;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region DataBindingCompleteEvent

        /// <summary>
        /// Handles invocation of DataBindingComplete events
        /// </summary>
        internal void DoDataBindingCompleteEvent(GridPanel gridPanel)
        {
            if (DataBindingComplete != null)
            {
                GridDataBindingCompleteEventArgs ev = new
                    GridDataBindingCompleteEventArgs(gridPanel);

                DataBindingComplete(this, ev);
            }
        }

        #endregion

        #region DataErrorEvent

        /// <summary>
        /// Handles invocation of DataError events
        /// </summary>
        internal bool DoDataErrorEvent(GridPanel gridPanel, GridCell gridCell, Exception exception,
            DataContext errorContext, ref object value, ref bool throwException, ref bool retry)
        {
            if (DataError != null)
            {
                GridDataErrorEventArgs ev = new
                    GridDataErrorEventArgs(gridPanel, gridCell, exception, errorContext, value);

                DataError(this, ev);

                value = ev.Value;
                throwException = ev.ThrowException;
                retry = ev.Retry;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region DataFilteringStartEvent

        /// <summary>
        /// Handles invocation of DataFilteringStart events
        /// </summary>
        internal bool DoDataFilteringStartEvent(GridPanel gridPanel)
        {
            if (DataFilteringStart != null)
            {
                GridDataFilteringStartEventArgs ev = new
                    GridDataFilteringStartEventArgs(gridPanel);

                DataFilteringStart(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region DataFilteringCompleteEvent

        /// <summary>
        /// Handles invocation of DataFilteringCompleteEvent events
        /// </summary>
        internal void DoDataFilteringCompleteEvent(GridPanel gridPanel)
        {
            if (DataFilteringComplete != null)
            {
                GridDataFilteringCompleteEventArgs ev = new
                    GridDataFilteringCompleteEventArgs(gridPanel);

                DataFilteringComplete(this, ev);
            }
        }

        #endregion

        #region ItemDragEvent

        /// <summary>
        /// Handles ItemDrag events
        /// </summary>
        internal bool DoItemDragEvent(GridElement element, MouseEventArgs e)
        {
            if (ItemDrag != null)
            {
                GridItemDragEventArgs ev = new
                    GridItemDragEventArgs(element.GridPanel, element, e);

                ItemDrag(this, ev);

                return (ev.Cancel);
            }

            return (true);
        }

        #endregion

        #region Editor events

        #region BeginEditEvent

        /// <summary>
        /// Handles invocation of BeginEdit events
        /// </summary>
        internal bool DoBeginEditEvent(
            GridPanel gridPanel, GridCell cell, IGridCellEditControl editor)
        {
            if (BeginEdit != null)
            {
                GridEditEventArgs ev = new
                    GridEditEventArgs(gridPanel, cell, editor);

                BeginEdit(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region CancelEditEvent

        /// <summary>
        /// Handles invocation of CancelEdit events
        /// </summary>
        internal bool DoCancelEditEvent(
            GridPanel gridPanel, GridCell cell, IGridCellEditControl editor)
        {
            if (CancelEdit != null)
            {
                GridEditEventArgs ev = new
                    GridEditEventArgs(gridPanel, cell, editor);

                CancelEdit(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region CloseEdit

        /// <summary>
        /// Handles invocation of CloseEdit events
        /// </summary>
        internal void DoCloseEditEvent(GridPanel gridPanel, GridCell cell)
        {
            if (CloseEdit != null)
            {
                GridCloseEditEventArgs ev = new
                    GridCloseEditEventArgs(gridPanel, cell);

                CloseEdit(this, ev);
            }
        }

        #endregion

        #region EditorValueChangedEvent

        /// <summary>
        /// Handles invocation of EditorValueChanged events
        /// </summary>
        internal void DoEditorValueChangedEvent(
            GridCell gridCell, IGridCellEditControl editor)
        {
            if (EditorValueChanged != null)
            {
                GridEditEventArgs ev = new
                    GridEditEventArgs(gridCell.GridPanel, gridCell, editor);

                EditorValueChanged(this, ev);
            }
        }

        #endregion

        #region EndEditEvent

        /// <summary>
        /// Handles invocation of EndEdit events
        /// </summary>
        internal bool DoEndEditEvent(
            GridPanel gridPanel, GridCell cell, IGridCellEditControl editor)
        {
            if (EndEdit != null)
            {
                GridEditEventArgs ev = new
                    GridEditEventArgs(gridPanel, cell, editor);

                EndEdit(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region GetEditorEvent

        /// <summary>
        /// Handles invocation of GetEditor events
        /// </summary>
        internal void DoGetEditorEvent(
            GridPanel gridPanel, GridCell cell, ref Type type, ref object[] editorParams)
        {
            if (GetEditor != null)
            {
                GridGetEditorEventArgs ev = new
                    GridGetEditorEventArgs(gridPanel, cell, type, editorParams);

                GetEditor(this, ev);

                if (ev.EditorType != null)
                    type = ev.EditorType;

                editorParams = ev.EditorParams;
            }
        }

        #endregion

        #region GetRendererEvent

        /// <summary>
        /// Handles invocation of GetRenderer events
        /// </summary>
        internal void DoGetRendererEvent(
            GridPanel gridPanel, GridCell cell, ref Type type, ref object[] renderParams)
        {
            if (GetRenderer != null)
            {
                GridGetRendererEventArgs ev = new
                    GridGetRendererEventArgs(gridPanel, cell, type, renderParams);

                GetRenderer(this, ev);

                type = ev.RenderType;
                renderParams = ev.RenderParams;
            }
        }

        #endregion

        #endregion

        #region Expand events

        #region AfterExpandEvent

        /// <summary>
        /// Handles invocation of AfterExpand events
        /// </summary>
        internal void DoAfterExpandEvent(
            GridPanel gridPanel, GridContainer container, ExpandSource expandSource)
        {
            if (AfterExpand != null)
            {
                GridAfterExpandEventArgs ev = new
                    GridAfterExpandEventArgs(gridPanel, container, expandSource);

                AfterExpand(this, ev);
            }
        }

        #endregion

        #region BeforeExpandEvent

        /// <summary>
        /// Handles invocation of BeforeExpand events
        /// </summary>
        internal bool DoBeforeExpandEvent(
            GridPanel gridPanel, GridContainer container, ExpandSource expandSource)
        {
            if (BeforeExpand != null)
            {
                GridBeforeExpandEventArgs ev = new
                    GridBeforeExpandEventArgs(gridPanel, container, expandSource);

                BeforeExpand(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #endregion

        #region Filter events

        #region FilterBeginEditEvent

        /// <summary>
        /// Handles invocation of FilterBeginEdit events
        /// </summary>
        internal bool DoFilterBeginEditEvent(GridPanel panel, GridColumn column)
        {
            if (FilterBeginEdit != null)
            {
                GridFilterBeginEditEventArgs ev = new
                    GridFilterBeginEditEventArgs(panel, column);

                FilterBeginEdit(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterCancelEditEvent

        /// <summary>
        /// Handles invocation of FilterCancelEdit events
        /// </summary>
        internal void DoFilterCancelEditEvent(GridColumn column)
        {
            if (FilterCancelEdit != null)
            {
                GridFilterCancelEditEventArgs ev = new
                    GridFilterCancelEditEventArgs(column.GridPanel, column);

                FilterCancelEdit(this, ev);
            }
        }

        #endregion

        #region FilterColumnErrorEvent

        /// <summary>
        /// Handles invocation of FilterColumnError events
        /// </summary>
        internal bool DoFilterColumnErrorEvent(GridPanel panel, GridRow row,
            GridColumn col, Exception exp, ref bool filteredOut, ref bool throwException)
        {
            if (FilterColumnError != null)
            {
                GridFilterColumnErrorEventArgs ev = new GridFilterColumnErrorEventArgs(
                    panel, row, col, exp, ref filteredOut, ref throwException);

                FilterColumnError(this, ev);

                filteredOut = ev.FilteredOut;
                throwException = ev.ThrowException;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterEditValueChangedEvent

        /// <summary>
        /// Handles invocation of FilterEditValueChanged events
        /// </summary>
        internal bool DoFilterEditValueChangedEvent(GridPanel panel, GridColumn column, FilterPanel fp,
            object oldValue, ref object newValue, ref object newDisplayValue, ref string newExpr)
        {
            if (FilterEditValueChanged != null)
            {
                GridFilterEditValueChangedEventArgs ev = new
                    GridFilterEditValueChangedEventArgs(panel,
                    column, fp, oldValue, newValue, newDisplayValue, newExpr);

                FilterEditValueChanged(this, ev);

                newValue = ev.NewValue;
                newDisplayValue = ev.NewDisplayValue;
                newExpr = ev.NewExpr;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterEndEditEvent

        /// <summary>
        /// Handles invocation of FilterEndEdit events
        /// </summary>
        internal void DoFilterEndEditEvent(GridColumn column)
        {
            if (FilterEndEdit != null)
            {
                GridFilterEndEditEventArgs ev = new
                    GridFilterEndEditEventArgs(column.GridPanel, column);

                FilterEndEdit(this, ev);
            }
        }

        #endregion

        #region FilterHeaderClickEvent

        /// <summary>
        /// Handles invocation of FilterHeaderClick events
        /// </summary>
        internal bool DoFilterHeaderClickEvent(
            GridPanel gridPanel, GridColumn gridColumn, MouseEventArgs e)
        {
            if (FilterHeaderClick != null)
            {
                GridFilterHeaderClickEventArgs ev = new
                    GridFilterHeaderClickEventArgs(gridPanel, gridColumn, e);

                FilterHeaderClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterHelpClosingEvent

        /// <summary>
        /// Handles invocation of FilterHelpClosing events
        /// </summary>
        internal void DoFilterHelpClosingEvent(GridPanel panel, GridColumn column, SampleExpr sampleExpr)
        {
            if (FilterHelpClosing != null)
            {
                GridFilterHelpClosingEventArgs ev = new
                    GridFilterHelpClosingEventArgs(panel, column, sampleExpr);

                FilterHelpClosing(this, ev);
            }
        }

        #endregion

        #region FilterHelpOpeningEvent

        /// <summary>
        /// Handles invocation of FilterHelpOpening events
        /// </summary>
        internal bool DoFilterHelpOpeningEvent(GridPanel panel, GridColumn column, SampleExpr sampleExpr)
        {
            if (FilterHelpOpening != null)
            {
                GridFilterHelpOpeningEventArgs ev = new
                    GridFilterHelpOpeningEventArgs(panel, column, sampleExpr);

                FilterHelpOpening(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterItemsLoadedEvent

        /// <summary>
        /// Handles invocation of FilterItemsLoaded events
        /// </summary>
        internal void DoFilterItemsLoadedEvent(GridColumn column, ComboBoxEx comboBox)
        {
            if (FilterItemsLoaded != null)
            {
                GridFilterItemsLoadedEventArgs ev = new
                    GridFilterItemsLoadedEventArgs(column.GridPanel, column, comboBox);

                FilterItemsLoaded(this, ev);
            }
        }

        #endregion

        #region FilterLoadItemsEvent

        /// <summary>
        /// Handles invocation of FilterLoadItems events
        /// </summary>
        internal bool DoFilterLoadItemsEvent(GridColumn column, ComboBoxEx comboBox)
        {
            if (FilterLoadItems != null)
            {
                GridFilterLoadItemsEventArgs ev = new
                    GridFilterLoadItemsEventArgs(column.GridPanel, column, comboBox);

                FilterLoadItems(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterLoadUserDataEvent

        /// <summary>
        /// Handles invocation of FilterLoadUserData events
        /// </summary>
        internal bool DoFilterLoadUserDataEvent(
            GridPanel gridPanel, ref string filterPath, ref List<UserFilterData> filterData)
        {
            if (FilterLoadUserData != null)
            {
                GridFilterLoadUserDataEventArgs ev = new
                    GridFilterLoadUserDataEventArgs(gridPanel, filterPath, filterData);

                FilterLoadUserData(this, ev);

                filterPath = ev.FilterPath;
                filterData = ev.FilterData;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterPopupClosingEvent

        /// <summary>
        /// Handles invocation of FilterPopupClosing events
        /// </summary>
        internal void DoFilterPopupClosingEvent(GridColumn column, FilterPopup filterPopup)
        {
            if (FilterPopupClosing != null)
            {
                GridFilterPopupClosingEventArgs ev = new
                    GridFilterPopupClosingEventArgs(column.GridPanel, column, filterPopup);

                FilterPopupClosing(this, ev);
            }
        }

        #endregion

        #region FilterPopupLoadEvent

        /// <summary>
        /// Handles invocation of FilterPopupLoad events
        /// </summary>
        internal bool DoFilterPopupLoadEvent(GridColumn column, FilterPopup filterPopup)
        {
            if (FilterPopupLoad != null)
            {
                GridFilterPopupLoadEventArgs ev = new
                    GridFilterPopupLoadEventArgs(column.GridPanel, column, filterPopup);

                FilterPopupLoad(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterPopupLoadedEvent

        /// <summary>
        /// Handles invocation of FilterPopupLoaded events
        /// </summary>
        internal void DoFilterPopupLoadedEvent(GridColumn column, FilterPopup filterPopup)
        {
            if (FilterPopupLoaded != null)
            {
                GridFilterPopupLoadedEventArgs ev = new
                    GridFilterPopupLoadedEventArgs(column.GridPanel, column, filterPopup);

                FilterPopupLoaded(this, ev);
            }
        }

        #endregion

        #region FilterPopupOpeningEvent

        /// <summary>
        /// Handles invocation of FilterPopupOpening events
        /// </summary>
        internal bool DoFilterPopupOpeningEvent(GridColumn column, FilterPopup filterPopup)
        {
            if (FilterPopupOpening != null)
            {
                GridFilterPopupOpeningEventArgs ev = new
                    GridFilterPopupOpeningEventArgs(column.GridPanel, column, filterPopup);

                FilterPopupOpening(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterPopupValueChangedEvent

        /// <summary>
        /// Handles invocation of FilterPopupValueChanged events
        /// </summary>
        internal bool DoFilterPopupValueChangedEvent(GridColumn column, FilterItemType filterItemType,
            ref object filterValue, ref object filterDisplayValue, ref string filterExpr)
        {
            if (FilterPopupValueChanged != null)
            {
                GridFilterPopupValueChangedEventArgs ev = 
                    new GridFilterPopupValueChangedEventArgs(column.GridPanel,
                    column, filterItemType, filterValue, filterDisplayValue, filterExpr);

                FilterPopupValueChanged(this, ev);

                filterValue = ev.FilterValue;
                filterDisplayValue = ev.FilterDisplayValue;
                filterExpr = ev.FilterExpr;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterRowErrorEvent

        /// <summary>
        /// Handles invocation of FilterRowError events
        /// </summary>
        internal bool DoFilterRowErrorEvent(GridPanel panel, GridRow row,
            Exception exp, ref bool filteredOut, ref bool throwException)
        {
            if (FilterRowError != null)
            {
                GridFilterRowErrorEventArgs ev = new GridFilterRowErrorEventArgs(
                    panel, row, exp, ref filteredOut, ref throwException);

                FilterRowError(this, ev);

                filteredOut = ev.FilteredOut;
                throwException = ev.ThrowException;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterRowHeaderClickEvent

        /// <summary>
        /// Handles invocation of FilterRowHeaderClick events
        /// </summary>
        internal bool DoFilterRowHeaderClickEvent(GridPanel gridPanel)
        {
            if (FilterRowHeaderClick != null)
            {
                GridCancelEventArgs ev = new GridCancelEventArgs(gridPanel);

                FilterRowHeaderClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterStoreUserDataEvent

        /// <summary>
        /// Handles invocation of FilterStoreUserData events
        /// </summary>
        internal bool DoFilterStoreUserDataEvent(
            GridPanel gridPanel, ref string filterPath, ref List<UserFilterData> filterData)
        {
            if (FilterStoreUserData != null)
            {
                GridFilterStoreUserDataEventArgs ev = new
                    GridFilterStoreUserDataEventArgs(gridPanel, filterPath, filterData);

                FilterStoreUserData(this, ev);

                filterPath = ev.FilterPath;
                filterData = ev.FilterData;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region FilterUserFunctionEvent

        /// <summary>
        /// Handles invocation of FilterUserFunction events
        /// </summary>
        internal bool DoFilterUserFunctionEvent(
            GridRow gridRow, object[] args, ref object result)
        {
            if (FilterUserFunction != null)
            {
                GridFilterUserFunctionEventArgs ev = new
                    GridFilterUserFunctionEventArgs(gridRow.GridPanel, gridRow, args, result);

                FilterUserFunction(this, ev);

                if (ev.Handled == true)
                {
                    result = ev.Result;

                    return (true);
                }
            }

            return (false);
        }

        #endregion

        #region GetFilterColumnHeaderStyleEvent

        /// <summary>
        /// Handles invocation of GetFilterColumnHeaderStyle events
        /// </summary>
        internal void DoGetFilterColumnHeaderStyleEvent(
            GridColumn gridColumn, StyleType eStyle, ref FilterColumnHeaderVisualStyle style)
        {
            if (GetFilterColumnHeaderStyle != null)
            {
                GridGetFilterColumnHeaderStyleEventArgs ev = new
                    GridGetFilterColumnHeaderStyleEventArgs(gridColumn.GridPanel, gridColumn, eStyle, style);

                GetFilterColumnHeaderStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetFilterEditTypeEvent

        /// <summary>
        /// Handles invocation of GetFilterEditType events
        /// </summary>
        internal void DoGetFilterEditTypeEvent(
            GridColumn gridColumn, ref FilterEditType filterType)
        {
            if (GetFilterEditType != null)
            {
                GridGetFilterEditTypeEventArgs ev = new
                    GridGetFilterEditTypeEventArgs(gridColumn.GridPanel, gridColumn, filterType);

                GetFilterEditType(this, ev);

                filterType = ev.FilterEditType;
            }
        }

        #endregion

        #region GetFilterRowStyleEvent

        /// <summary>
        /// Handles invocation of GetFilterRowStyle events
        /// </summary>
        internal void DoGetFilterRowStyleEvent(
            GridFilter gridFilter, StyleType eStyle, ref FilterRowVisualStyle style)
        {
            if (GetFilterRowStyle != null)
            {
                GridGetFilterRowStyleEventArgs ev = new
                    GridGetFilterRowStyleEventArgs(gridFilter.GridPanel, gridFilter, eStyle, style);

                GetFilterRowStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #endregion

        #region GetCellFormattedValueEvent

        /// <summary>
        /// Handles invocation of GetCellFormattedValue events
        /// </summary>
        internal void DoGetCellFormattedValueEvent(
            GridCell gridCell, ref string formattedValue)
        {
            if (GetCellFormattedValue != null)
            {
                GridGetCellFormattedValueEventArgs ev = new
                    GridGetCellFormattedValueEventArgs(gridCell.GridPanel, gridCell, formattedValue);

                GetCellFormattedValue(this, ev);

                formattedValue = ev.FormattedValue;
            }
        }

        #endregion

        #region GetCellStyleEvent

        /// <summary>
        /// Handles invocation of GetCellStyle events
        /// </summary>
        internal void DoGetCellStyleEvent(
            GridCell gridCell, StyleType eStyle, ref CellVisualStyle style)
        {
            if (GetCellStyle != null)
            {
                GridGetCellStyleEventArgs ev = new
                    GridGetCellStyleEventArgs(gridCell.GridPanel, gridCell, eStyle, style);

                GetCellStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetCellValueEvent

        /// <summary>
        /// Handles invocation of GetCellValue events
        /// </summary>
        internal void DoGetCellValueEvent(GridCell gridCell, ref object value)
        {
            if (GetCellValue != null)
            {
                GridGetCellValueEventArgs ev = new
                    GridGetCellValueEventArgs(gridCell.GridPanel, gridCell, value);

                GetCellValue(this, ev);

                value = ev.Value;
            }
        }

        #endregion

        #region GetColumnHeaderRowHeaderStyleEvent

        /// <summary>
        /// Handles invocation of GetColumnHeader RowHeader events
        /// </summary>
        internal void DoGetColumnHeaderRowHeaderStyleEvent(
            GridColumnHeader columnHeader, StyleType eStyle, ref ColumnHeaderRowVisualStyle style)
        {
            if (GetColumnHeaderRowHeaderStyle != null)
            {
                GridGetColumnHeaderRowHeaderStyleEventArgs ev = new
                    GridGetColumnHeaderRowHeaderStyleEventArgs(columnHeader.GridPanel, columnHeader, eStyle, style);

                GetColumnHeaderRowHeaderStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetColumnHeaderStyleEvent

        /// <summary>
        /// Handles invocation of GetColumnHeader events
        /// </summary>
        internal void DoGetColumnHeaderStyleEvent(
            GridColumn gridColumn, StyleType eStyle, ref ColumnHeaderVisualStyle style)
        {
            if (GetColumnHeaderStyle != null)
            {
                GridGetColumnHeaderStyleEventArgs ev = new
                    GridGetColumnHeaderStyleEventArgs(gridColumn.GridPanel, gridColumn, eStyle, style);

                GetColumnHeaderStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetColumnHeaderToolTipEvent

        /// <summary>
        /// Handles invocation of GetColumnHeaderToolTip events
        /// </summary>
        internal bool DoGetColumnHeaderToolTipEvent(GridPanel panel,
            GridColumn gridColumn, HeaderArea hitArea, ref string toolTip)
        {
            if (GetColumnHeaderToolTip != null)
            {
                GridGetColumnHeaderToolTipEventArgs ev = new
                    GridGetColumnHeaderToolTipEventArgs(panel, gridColumn, hitArea, toolTip);

                GetColumnHeaderToolTip(this, ev);

                toolTip = ev.ToolTip;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region ConfigureGroupBox

        /// <summary>
        /// Handles invocation of ConfigureGroupBox events
        /// </summary>
        internal void DoConfigureGroupBoxEvent(
            GridGroupByRow groupByRow, GridGroupBox gridGroupBox)
        {
            if (ConfigureGroupBox != null)
            {
                GridConfigureGroupBoxEventArgs ev = new
                    GridConfigureGroupBoxEventArgs(groupByRow.GridPanel, groupByRow, gridGroupBox);

                ConfigureGroupBox(this, ev);
            }
        }

        #endregion

        #region GetGroupedDetailRowsEvent

        /// <summary>
        /// Handles invocation of GetGroupedDetailRows events
        /// </summary>
        internal bool DoGetGroupedDetailRowsEvent(GridPanel gridPanel, GridColumn gridColumn,
            GridGroup gridGroup, out List<GridRow> preRows, out List<GridRow> postRows)
        {
            if (GetGroupDetailRows != null)
            {
                GridGetGroupDetailRowsEventArgs ev = new
                    GridGetGroupDetailRowsEventArgs(gridPanel, gridColumn, gridGroup);

                GetGroupDetailRows(this, ev);

                preRows = ev.PreDetailRows;
                postRows = ev.PostDetailRows;

                return (true);
            }

            preRows = null;
            postRows = null;

            return (false);
        }

        #endregion

        #region GetGroupHeaderStyleEvent

        /// <summary>
        /// Handles invocation of GetGroupHeaderStyle events
        /// </summary>
        internal void DoGetGroupHeaderStyleEvent(
            GridGroup gridRow, StyleType eStyle, ref GroupHeaderVisualStyle style)
        {
            if (GetGroupHeaderStyle != null)
            {
                GridGetGroupHeaderStyleEventArgs ev = new
                    GridGetGroupHeaderStyleEventArgs(gridRow.GridPanel, gridRow, eStyle, style);

                GetGroupHeaderStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetGroupIdEvent

        /// <summary>
        /// Handles invocation of GetGroupId events
        /// </summary>
        internal void DoGetGroupIdEvent(
            GridElement row, GridColumn column, ref object groupId)
        {
            if (GetGroupId != null)
            {
                GridGetGroupIdEventArgs ev = new
                    GridGetGroupIdEventArgs(row.GridPanel, row, column, groupId);

                GetGroupId(this, ev);

                groupId = ev.GroupId ?? "<null>";
            }
        }

        #endregion

        #region GetPanelStyleEvent

        /// <summary>
        /// Handles invocation of GetPanelStyle events
        /// </summary>
        internal void DoGetPanelStyleEvent(
            GridPanel gridPanel, ref GridPanelVisualStyle style)
        {
            if (GetPanelStyle != null)
            {
                GridGetPanelStyleEventArgs ev = new
                    GridGetPanelStyleEventArgs(gridPanel, style);

                GetPanelStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetRowHeaderStyleEvent

        /// <summary>
        /// Handles invocation of GetRowHeader events
        /// </summary>
        internal void DoGetRowHeaderStyleEvent(
            GridContainer gridRow, StyleType eStyle, ref RowHeaderVisualStyle style)
        {
            if (GetRowHeaderStyle != null)
            {
                GridGetRowHeaderStyleEventArgs ev = new
                    GridGetRowHeaderStyleEventArgs(gridRow.GridPanel, gridRow, eStyle, style);

                GetRowHeaderStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetRowStyleEvent

        /// <summary>
        /// Handles invocation of GetRowStyle events
        /// </summary>
        internal void DoGetRowStyleEvent(
            GridContainer gridRow, StyleType eStyle, ref RowVisualStyle style)
        {
            if (GetRowStyle != null)
            {
                GridGetRowStyleEventArgs ev = new
                    GridGetRowStyleEventArgs(gridRow.GridPanel, gridRow, eStyle, style);

                GetRowStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GetRowHeaderTextEvent

        /// <summary>
        /// Handles invocation of GetRowHeaderText events
        /// </summary>
        internal void DoGetRowHeaderTextEvent(GridContainer row, ref string text)
        {
            if (GetRowHeaderText != null)
            {
                GridGetRowHeaderTextEventArgs ev = new
                    GridGetRowHeaderTextEventArgs(row.GridPanel, row, text);

                GetRowHeaderText(this, ev);

                text = ev.Text;
            }
        }

        #endregion

        #region GetTextRowStyleEvent

        /// <summary>
        /// Handles invocation of GetTextRowStyle events
        /// </summary>
        internal void DoGetTextRowStyleEvent(
            GridTextRow gridTextRow, StyleType eStyle, ref TextRowVisualStyle style)
        {
            if (GetTextRowStyle != null)
            {
                GridGetTextRowStyleEventArgs ev = new
                    GridGetTextRowStyleEventArgs(gridTextRow.GridPanel, gridTextRow, eStyle, style);

                GetTextRowStyle(this, ev);

                style = ev.Style;
            }
        }

        #endregion

        #region GridPreviewKeyDown

        private bool DoGridPreviewKeyDown(Keys keyData)
        {
            if (GridPreviewKeyDown != null)
            {
                GridPreviewKeyDownEventArgs args =
                    new GridPreviewKeyDownEventArgs(keyData | ModifierKeys);

                args.IsInputKey = _IsInputKey;

                GridPreviewKeyDown(this, args);

                _IsInputKey = args.IsInputKey;

                return (args.Handled);
            }

            return (false);
        }

        #endregion

        #region GroupHeaderClickEvent

        /// <summary>
        /// Handles invocation of GroupHeaderClick events
        /// </summary>
        internal bool DoGroupHeaderClickEvent(GridGroup group, GroupArea area, MouseEventArgs e)
        {
            if (GroupHeaderClick != null)
            {
                GridGroupHeaderClickEventArgs ev =
                    new GridGroupHeaderClickEventArgs(group.GridPanel, group, area, e);

                GroupHeaderClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region GroupHeaderDoubleClickEvent

        /// <summary>
        /// Handles invocation of GroupHeaderDoubleClick events
        /// </summary>
        internal bool DoGroupHeaderDoubleClickEvent(GridGroup group, GroupArea area, MouseEventArgs e)
        {
            if (GroupHeaderDoubleClick != null)
            {
                GridGroupHeaderDoubleClickEventArgs ev =
                    new GridGroupHeaderDoubleClickEventArgs(group.GridPanel, group, area, e);

                GroupHeaderDoubleClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region InitEditContext

        /// <summary>
        /// Handles invocation of InitEditContext events
        /// </summary>
        internal void DoInitEditContextEvent(
            GridCell gridCell, IGridCellEditControl editControl)
        {
            if (InitEditContext != null)
            {
                GridInitEditContextEventArgs ev = new
                    GridInitEditContextEventArgs(gridCell.GridPanel, gridCell, editControl);

                InitEditContext(this, ev);
            }
        }

        #endregion

        #region NoRowsMarkupLinkClick Event

        /// <summary>
        /// Handles invocation of NoRowsTextMarkupLinkClick events
        /// </summary>
        internal void DoNoRowsMarkupLinkClickEvent(
            GridPanel gridPanel, HyperLink hyperLink)
        {
            if (NoRowsMarkupLinkClick != null)
            {
                GridNoRowsMarkupLinkClickEventArgs ev = new GridNoRowsMarkupLinkClickEventArgs(
                    gridPanel, hyperLink.Name, hyperLink.HRef);

                NoRowsMarkupLinkClick(this, ev);
            }
        }

        #endregion

        #region PreviewKeyDown

        private void DoPreviewKeyDown(Keys keyData)
        {
            if (PreviewKeyDown != null)
            {
                PreviewKeyDownEventArgs args =
                    new PreviewKeyDownEventArgs(keyData);

                args.IsInputKey = _IsInputKey;

                PreviewKeyDown(this, args);

                _IsInputKey = args.IsInputKey;
            }
        }

        #endregion

        #region RefreshFilter

        internal bool DoRefreshFilter(GridPanel panel)
        {
            if (RefreshFilter != null)
            {
                GridRefreshFilterEventArgs args =
                    new GridRefreshFilterEventArgs(panel);

                RefreshFilter(this, args);

                return (args.Cancel);
            }

            return (false);
        }

        #endregion

        #region Render events

        #region PostRenderCellEvent

        /// <summary>
        /// Handles invocation of PostRenderCell events
        /// </summary>
        internal void DoPostRenderCellEvent(Graphics g,
            GridCell gridCell, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderCell != null)
            {
                GridPostRenderCellEventArgs ev = new
                    GridPostRenderCellEventArgs(g, gridCell.GridPanel, gridCell, parts, bounds);

                PostRenderCell(this, ev);
            }
        }

        #endregion

        #region PreRenderCellEvent

        /// <summary>
        /// Handles invocation of PreRenderCell events
        /// </summary>
        internal bool DoPreRenderCellEvent(Graphics g,
            GridCell gridCell, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderCell != null)
            {
                GridPreRenderCellEventArgs ev = new
                    GridPreRenderCellEventArgs(g, gridCell.GridPanel, gridCell, parts, bounds);

                PreRenderCell(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderColumnHeaderEvent

        /// <summary>
        /// Handles invocation of PostRenderColumnHeader events
        /// </summary>
        internal void DoPostRenderColumnHeaderEvent(Graphics g,
            GridColumnHeader header, GridColumn column, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderColumnHeader != null)
            {
                GridPanel panel = header.GridPanel;

                GridPostRenderColumnHeaderEventArgs ev = new
                    GridPostRenderColumnHeaderEventArgs(g, panel, header, column, parts, bounds);

                PostRenderColumnHeader(this, ev);
            }
        }

        #endregion

        #region PreRenderColumnHeaderEvent

        /// <summary>
        /// Handles invocation of PreRenderColumnHeaderEvent events
        /// </summary>
        internal bool DoPreRenderColumnHeaderEvent(Graphics g,
            GridColumnHeader header, GridColumn column, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderColumnHeader != null)
            {
                GridPanel panel = header.GridPanel;

                GridPreRenderColumnHeaderEventArgs ev = new
                    GridPreRenderColumnHeaderEventArgs(g, panel, header, column, parts, bounds);

                PreRenderColumnHeader(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderFilterPopupGripBarEvent

        /// <summary>
        /// Handles invocation of PostRenderFilterPopupGripBar events
        /// </summary>
        internal void DoPostRenderFilterPopupGripBarEvent(
            Graphics g, FilterPopup filterPopup, GridColumn gridColumn, Rectangle bounds)
        {
            if (PostRenderFilterPopupGripBar != null)
            {
                GridPostRenderFilterPopupGripBarEventArgs ev = new
                    GridPostRenderFilterPopupGripBarEventArgs(g, filterPopup, gridColumn, bounds);

                PostRenderFilterPopupGripBar(this, ev);
            }
        }

        #endregion

        #region PreRenderFilterPopupGripBarEvent

        /// <summary>
        /// Handles invocation of PreRenderFilterPopupGripBar events
        /// </summary>
        internal bool DoPreRenderFilterPopupGripBarEvent(
            Graphics g, FilterPopup filterPopup, GridColumn gridColumn, Rectangle bounds)
        {
            if (PreRenderFilterPopupGripBar != null)
            {
                GridPreRenderFilterPopupGripBarEventArgs ev = new
                    GridPreRenderFilterPopupGripBarEventArgs(g, filterPopup, gridColumn, bounds);

                PreRenderFilterPopupGripBar(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderFilterRowEvent

        /// <summary>
        /// Handles invocation of PostRenderFilterRow events
        /// </summary>
        internal void DoPostRenderFilterRowEvent(Graphics g,
            GridFilter filter, GridColumn column, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderFilterRow != null)
            {
                GridPanel panel = filter.GridPanel;

                GridPostRenderFilterRowEventArgs ev = new
                    GridPostRenderFilterRowEventArgs(g, panel, filter, column, parts, bounds);

                PostRenderFilterRow(this, ev);
            }
        }

        #endregion

        #region PreRenderFilterRowEvent

        /// <summary>
        /// Handles invocation of PreRenderFilterRowEvent events
        /// </summary>
        internal bool DoPreRenderFilterRowEvent(Graphics g,
            GridFilter filter, GridColumn column, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderFilterRow != null)
            {
                GridPanel panel = filter.GridPanel;

                GridPreRenderFilterRowEventArgs ev = new
                    GridPreRenderFilterRowEventArgs(g, panel, filter, column, parts, bounds);

                PreRenderFilterRow(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderGroupBoxConnectorEvent

        /// <summary>
        /// Handles invocation of PostRenderGroupBoxConnector events
        /// </summary>
        internal void DoPostRenderGroupBoxConnectorEvent(Graphics g,
            GridGroupByRow groupByRow, GridGroupBox box1, GridGroupBox box2)
        {
            if (PostRenderGroupBoxConnector != null)
            {
                GridPostRenderGroupBoxConnectorEventArgs ev = new
                    GridPostRenderGroupBoxConnectorEventArgs(g, groupByRow.GridPanel, groupByRow, box1, box2);

                PostRenderGroupBoxConnector(this, ev);
            }
        }

        #endregion

        #region PreRenderGroupBoxConnectorEvent

        /// <summary>
        /// Handles invocation of PreRenderGroupBoxConnector events
        /// </summary>
        internal bool DoPreRenderGroupBoxConnectorEvent(Graphics g,
            GridGroupByRow groupByRow, GridGroupBox box1, GridGroupBox box2)
        {
            if (PreRenderGroupBoxConnector != null)
            {
                GridPreRenderGroupBoxConnectorEventArgs ev = new
                    GridPreRenderGroupBoxConnectorEventArgs(g, groupByRow.GridPanel, groupByRow, box1, box2);

                PreRenderGroupBoxConnector(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderGroupBoxEvent

        /// <summary>
        /// Handles invocation of PostRenderGroupBox events
        /// </summary>
        internal void DoPostRenderGroupBoxEvent(Graphics g,
            GridGroupByRow groupByRow, GridGroupBox box, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderGroupBox != null)
            {
                GridPostRenderGroupBoxEventArgs ev = new
                    GridPostRenderGroupBoxEventArgs(g, groupByRow.GridPanel, groupByRow, box, parts, bounds);

                PostRenderGroupBox(this, ev);
            }
        }

        #endregion

        #region PreRenderGroupBoxEvent

        /// <summary>
        /// Handles invocation of PreRenderGroupBox events
        /// </summary>
        internal bool DoPreRenderGroupBoxEvent(Graphics g,
            GridGroupByRow groupByRow, GridGroupBox box, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderGroupBox != null)
            {
                GridPreRenderGroupBoxEventArgs ev = new
                    GridPreRenderGroupBoxEventArgs(g, groupByRow.GridPanel, groupByRow, box, parts, bounds);

                PreRenderGroupBox(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderGroupHeaderEvent

        /// <summary>
        /// Handles invocation of PostRenderGroupHeader events
        /// </summary>
        internal void DoPostRenderGroupHeaderEvent(
            Graphics g, GridGroup gridGroup, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderGroupHeader != null)
            {
                GridPanel panel = gridGroup.GridPanel;

                GridPostRenderRowEventArgs ev = new
                    GridPostRenderRowEventArgs(g, panel, gridGroup, parts, bounds);

                PostRenderGroupHeader(this, ev);
            }
        }

        #endregion

        #region PreRenderGroupHeaderEvent

        /// <summary>
        /// Handles invocation of PreRenderGroupHeader events
        /// </summary>
        internal bool DoPreRenderGroupHeaderEvent(
            Graphics g, GridGroup gridGroup, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderGroupHeader != null)
            {
                GridPanel panel = gridGroup.GridPanel;

                GridPreRenderRowEventArgs ev = new
                    GridPreRenderRowEventArgs(g, panel, gridGroup, parts, bounds);

                PreRenderGroupHeader(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderPanelRowEvent

        /// <summary>
        /// Handles invocation of PostRenderPanelRow events
        /// </summary>
        internal void DoPostRenderPanelRowEvent(
            Graphics g, GridContainer gridRow, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderPanelRow != null)
            {
                GridPostRenderRowEventArgs ev = new
                    GridPostRenderRowEventArgs(g, gridRow.GridPanel, gridRow, parts, bounds);

                PostRenderPanelRow(this, ev);
            }
        }

        #endregion

        #region PreRenderPanelRowEvent

        /// <summary>
        /// Handles invocation of PreRenderPanelRow events
        /// </summary>
        internal bool DoPreRenderPanelRowEvent(
            Graphics g, GridContainer gridRow, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderPanelRow != null)
            {
                GridPreRenderRowEventArgs ev = new
                    GridPreRenderRowEventArgs(g, gridRow.GridPanel, gridRow, parts, bounds);

                PreRenderPanelRow(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderRowEvent

        /// <summary>
        /// Handles invocation of PostRenderRow events
        /// </summary>
        internal void DoPostRenderRowEvent(
            Graphics g, GridRow gridRow, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderRow != null)
            {
                GridPostRenderRowEventArgs ev = new
                    GridPostRenderRowEventArgs(g, gridRow.GridPanel, gridRow, parts, bounds);

                PostRenderRow(this, ev);
            }
        }

        #endregion

        #region PreRenderRowEvent

        /// <summary>
        /// Handles invocation of PreRenderRow events
        /// </summary>
        internal bool DoPreRenderRowEvent(Graphics g, 
            GridRow gridRow, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderRow != null)
            {
                GridPreRenderRowEventArgs ev = new
                    GridPreRenderRowEventArgs(g, gridRow.GridPanel, gridRow, parts, bounds);

                PreRenderRow(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region PostRenderTextRowEvent

        /// <summary>
        /// Handles invocation of PostRenderTextRow events
        /// </summary>
        internal void DoPostRenderTextRowEvent(
            Graphics g, GridTextRow gridTextRow, RenderParts parts, Rectangle bounds)
        {
            if (PostRenderTextRow != null)
            {
                GridPostRenderTextRowEventArgs ev = new
                    GridPostRenderTextRowEventArgs(g, gridTextRow.GridPanel, gridTextRow, parts, bounds);

                PostRenderTextRow(this, ev);
            }
        }

        #endregion

        #region PreRenderTextRowEvent

        /// <summary>
        /// Handles invocation of PreRenderTextRow events
        /// </summary>
        internal bool DoPreRenderTextRowEvent(Graphics g,
            GridTextRow gridTextRow, RenderParts parts, Rectangle bounds)
        {
            if (PreRenderTextRow != null)
            {
                GridPreRenderTextRowEventArgs ev = new
                    GridPreRenderTextRowEventArgs(g, gridTextRow.GridPanel, gridTextRow, parts, bounds);

                PreRenderTextRow(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #endregion

        #region Row events

        #region RowActivatedEvent

        /// <summary>
        /// Handles invocation of RowActivated events
        /// </summary>
        internal void DoRowActivatedEvent(
            GridPanel gridPanel, GridContainer oldRow, GridContainer newRow)
        {
            if (RowActivated != null)
            {
                GridRowActivatedEventArgs ev = new
                    GridRowActivatedEventArgs(gridPanel, oldRow, newRow);

                RowActivated(this, ev);
            }
        }

        #endregion

        #region RowActivatingEvent

        /// <summary>
        /// Handles invocation of RowActivating events
        /// </summary>
        internal bool DoRowActivatingEvent(
            GridPanel panel, GridContainer orow, GridContainer nrow)
        {
            if (RowActivating != null)
            {
                GridRowActivatingEventArgs ev = new
                    GridRowActivatingEventArgs(panel, orow, nrow);

                RowActivating(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowAddedEvent

        /// <summary>
        /// Handles invocation of RowAdded events
        /// </summary>
        internal void DoRowAddedEvent(GridPanel panel, int index)
        {
            if (RowAdded != null)
            {
                GridRowAddedEventArgs ev = new
                    GridRowAddedEventArgs(panel, index);

                RowAdded(this, ev);
            }
        }

        #endregion

        #region RowAddingEvent

        /// <summary>
        /// Handles invocation of DoRowAdding events
        /// </summary>
        internal bool DoRowAddingEvent(GridPanel panel, ref int index)
        {
            if (RowAdding != null)
            {
                GridRowAddingEventArgs ev = new
                    GridRowAddingEventArgs(panel, index);

                RowAdding(this, ev);

                index = ev.Index;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowClickEvent

        /// <summary>
        /// Handles invocation of RowClick events
        /// </summary>
        internal bool DoRowClickEvent(GridRow row, RowArea rowArea, MouseEventArgs e)
        {
            if (RowClick != null)
            {
                GridRowClickEventArgs ev =
                    new GridRowClickEventArgs(row.GridPanel, row, rowArea, e);

                RowClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowDeletedEvent

        /// <summary>
        /// Handles invocation of RowDeleted events
        /// </summary>
        internal void DoRowDeletedEvent(GridPanel panel, SelectedElements selRows)
        {
            if (RowDeleted != null)
            {
                GridRowDeletedEventArgs ev = new
                    GridRowDeletedEventArgs(panel, selRows);

                RowDeleted(this, ev);
            }
        }

        #endregion

        #region RowDeletingEvent

        /// <summary>
        /// Handles invocation of RowDeleting events
        /// </summary>
        internal bool DoRowDeletingEvent(GridPanel panel, SelectedElements selRows)
        {
            if (RowDeleting != null)
            {
                GridRowDeletingEventArgs ev = new
                    GridRowDeletingEventArgs(panel, selRows);

                RowDeleting(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowDoubleClickEvent

        /// <summary>
        /// Handles invocation of RowDoubleClick events
        /// </summary>
        internal bool DoRowDoubleClickEvent(GridRow row, RowArea rowArea, MouseEventArgs e)
        {
            if (RowDoubleClick != null)
            {
                GridRowDoubleClickEventArgs ev = new
                    GridRowDoubleClickEventArgs(row.GridPanel, row, rowArea, e);

                RowDoubleClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowGetDetailHeightEvent

        /// <summary>
        /// Handles invocation of RowGetDetailHeight events
        /// </summary>
        internal void DoRowGetDetailHeightEvent(GridRow row, 
            GridLayoutInfo layoutInfo, Size sizeNeeded, ref int preHeight, ref int postHeight)
        {
            if (GetDetailRowHeight != null)
            {
                GridGetDetailRowHeightEventArgs ev = new
                    GridGetDetailRowHeightEventArgs(row.GridPanel, row, layoutInfo, sizeNeeded, preHeight, postHeight);

                GetDetailRowHeight(this, ev);

                preHeight = ev.PreDetailHeight;
                postHeight = ev.PostDetailHeight;
            }
        }

        #endregion

        #region RowHeaderClickEvent

        /// <summary>
        /// Handles invocation of RowHeaderClick events
        /// </summary>
        internal void DoRowHeaderClickEvent(
            GridPanel gridPanel, GridContainer row, MouseEventArgs e)
        {
            if (RowHeaderClick != null)
            {
                GridRowHeaderClickEventArgs ev =
                    new GridRowHeaderClickEventArgs(gridPanel, row, e);

                RowHeaderClick(this, ev);
            }
        }

        #endregion

        #region RowHeaderDoubleClickEvent

        /// <summary>
        /// Handles invocation of RowHeaderDoubleClick events
        /// </summary>
        internal bool DoRowHeaderDoubleClickEvent(GridContainer row, MouseEventArgs e)
        {
            if (RowHeaderDoubleClick != null)
            {
                GridRowHeaderDoubleClickEventArgs ev = new
                    GridRowHeaderDoubleClickEventArgs(row.GridPanel, row, e);

                RowHeaderDoubleClick(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowHeaderResizedEvent

        /// <summary>
        /// Handles invocation of RowHeaderResized events
        /// </summary>
        internal void DoRowHeaderResizedEvent(GridPanel gridPanel)
        {
            if (RowHeaderResized != null)
            {
                GridEventArgs ev = new GridEventArgs(gridPanel);

                RowHeaderResized(this, ev);
            }
        }

        #endregion

        #region RowInfoClickEvent

        /// <summary>
        /// Handles invocation of RowInfoClick events
        /// </summary>
        internal void DoRowInfoClickEvent(GridRow gridRow, MouseEventArgs e)
        {
            if (RowInfoClick != null)
            {
                GridRowClickEventArgs ev = new
                    GridRowClickEventArgs(gridRow.GridPanel, gridRow, RowArea.InRowInfo, e);

                RowInfoClick(this, ev);
            }
        }

        #endregion

        #region RowInfoEnterEvent

        /// <summary>
        /// Handles invocation of RowInfoEnter events
        /// </summary>
        internal bool DoRowInfoEnterEvent(GridRow gridRow, Point pt)
        {
            if (RowInfoEnter != null)
            {
                GridRowInfoEnterEventArgs ev = new
                    GridRowInfoEnterEventArgs(gridRow.GridPanel, gridRow, pt);

                RowInfoEnter(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowInfoLeaveEvent

        /// <summary>
        /// Handles invocation of RowInfoLeave events
        /// </summary>
        internal void DoRowInfoLeaveEvent(GridRow gridRow)
        {
            if (RowInfoLeave != null)
            {
                GridRowInfoLeaveEventArgs ev = new
                    GridRowInfoLeaveEventArgs(gridRow.GridPanel, gridRow);

                RowInfoLeave(this, ev);
            }
        }

        #endregion

        #region RowInfoDoubleClickEvent

        /// <summary>
        /// Handles invocation of RowInfoDoubleClick events
        /// </summary>
        internal void DoRowInfoDoubleClickEvent(GridRow gridRow, MouseEventArgs e)
        {
            if (RowInfoDoubleClick != null)
            {
                GridRowDoubleClickEventArgs ev = new
                    GridRowDoubleClickEventArgs(gridRow.GridPanel, gridRow, RowArea.InRowInfo, e);

                RowInfoDoubleClick(this, ev);
            }
        }

        #endregion

        #region RowMarkedDirtyEvent

        /// <summary>
        /// Handles invocation of RowMarkedDirty events
        /// </summary>
        internal bool DoRowMarkedDirtyEvent(
            GridCell gridCell, IGridCellEditControl editor)
        {
            if (RowMarkedDirty != null)
            {
                GridEditEventArgs ev = new
                    GridEditEventArgs(gridCell.GridPanel, gridCell, editor);

                RowMarkedDirty(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowMouseDownEvent

        /// <summary>
        /// Handles invocation of RowMouseDown events
        /// </summary>
        internal void DoRowMouseDownEvent(
            GridRow gridRow, MouseEventArgs args, RowArea area)
        {
            if (RowMouseDown != null)
            {
                GridRowMouseEventArgs ev = new
                    GridRowMouseEventArgs(gridRow.GridPanel, gridRow, args, area);

                RowMouseDown(this, ev);
            }
        }

        #endregion

        #region RowMouseEnterEvent

        /// <summary>
        /// Handles invocation of RowMouseEnter events
        /// </summary>
        internal void DoRowMouseEnterEvent(GridRow gridRow)
        {
            if (RowMouseEnter != null)
            {
                GridRowEventArgs ev = new
                    GridRowEventArgs(gridRow.GridPanel, gridRow);

                RowMouseEnter(this, ev);
            }
        }

        #endregion

        #region RowMouseLeaveEvent

        /// <summary>
        /// Handles invocation of RowMouseLeave events
        /// </summary>
        internal void DoRowMouseLeaveEvent(GridRow gridRow)
        {
            if (RowMouseLeave != null)
            {
                GridRowEventArgs ev = new
                    GridRowEventArgs(gridRow.GridPanel, gridRow);

                RowMouseLeave(this, ev);
            }
        }

        #endregion

        #region RowMouseMoveEvent

        /// <summary>
        /// Handles invocation of RowMouseMove events
        /// </summary>
        internal void DoRowMouseMoveEvent(GridRow gridRow, MouseEventArgs args, RowArea area)
        {
            if (RowMouseMove != null)
            {
                GridRowMouseEventArgs ev = new
                    GridRowMouseEventArgs(gridRow.GridPanel, gridRow, args, area);

                RowMouseMove(this, ev);
            }
        }

        #endregion

        #region RowMouseUpEvent

        /// <summary>
        /// Handles invocation of RowMouseUp events
        /// </summary>
        internal void DoRowMouseUpEvent(GridRow gridRow, MouseEventArgs args, RowArea area)
        {
            if (RowMouseUp != null)
            {
                GridRowMouseEventArgs ev = new
                    GridRowMouseEventArgs(gridRow.GridPanel, gridRow, args, area);

                RowMouseUp(this, ev);
            }
        }

        #endregion

        #region RowMovedEvent

        /// <summary>
        /// Handles invocation of RowMoved events
        /// </summary>
        internal void DoRowMovedEvent(
            GridPanel gridPanel, GridContainer gridContainer, GridContainer gridRow)
        {
            if (RowMoved != null)
            {
                GridRowMovedEventArgs ev = new
                    GridRowMovedEventArgs(gridPanel, gridContainer, gridRow);

                RowMoved(this, ev);
            }
        }

        #endregion

        #region RowMovingEvent

        /// <summary>
        /// Handles invocation of RowMoving events
        /// </summary>
        internal bool DoRowMovingEvent(GridRow gridRow,
            GridContainer srcCont, int srcIndex, ref GridContainer destCont, ref int destIndex)
        {
            if (RowMoving != null)
            {
                GridRowMovingEventArgs ev = new GridRowMovingEventArgs(
                    gridRow.GridPanel, gridRow, srcCont, srcIndex, destCont, destIndex);

                RowMoving(this, ev);

                destIndex = ev.DestIndex;
                destCont = ev.DestContainer;

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowResizedEvent

        /// <summary>
        /// Handles invocation of RowResized events
        /// </summary>
        internal void DoRowResizedEvent(GridPanel gridPanel, GridContainer gridRow)
        {
            if (RowResized != null)
            {
                GridRowEventArgs ev = new
                    GridRowEventArgs(gridPanel, gridRow);

                RowResized(this, ev);
            }
        }

        #endregion

        #region RowRestoredEvent

        /// <summary>
        /// Handles invocation of RowRestored events
        /// </summary>
        internal void DoRowRestoredEvent(GridPanel panel, SelectedElements selRows)
        {
            if (RowRestored != null)
            {
                GridRowRestoredEventArgs ev = new
                    GridRowRestoredEventArgs(panel, selRows);

                RowRestored(this, ev);
            }
        }

        #endregion

        #region RowRestoringEvent

        /// <summary>
        /// Handles invocation of RowRestoring events
        /// </summary>
        internal bool DoRowRestoringEvent(GridPanel panel, SelectedElements selRows)
        {
            if (RowRestoring != null)
            {
                GridRowRestoringEventArgs ev = new
                    GridRowRestoringEventArgs(panel, selRows);

                RowRestoring(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowSetDefaultValuesEvent

        /// <summary>
        /// Handles invocation of RowSetDefaultValues events
        /// </summary>
        internal void DoRowSetDefaultValuesEvent(
            GridPanel panel, GridRow row, NewRowContext context)
        {
            if (RowSetDefaultValues != null)
            {
                GridRowSetDefaultValuesEventArgs ev = new
                    GridRowSetDefaultValuesEventArgs(panel, row, context);

                bool loading = row.Loading;
                row.Loading = true;

                try
                {
                    RowSetDefaultValues(this, ev);
                }
                finally
                {
                    row.Loading = loading;
                }
            }
        }

        #endregion

        #region RowsGroupedEvent

        /// <summary>
        /// Handles invocation of RowsGroupedEvent events
        /// </summary>
        internal void DoRowsGroupedEvent(GridPanel panel)
        {
            if (RowsGrouped != null)
            {
                GridEventArgs ev = new GridEventArgs(panel);

                RowsGrouped(this, ev);
            }
        }

        #endregion

        #region RowsPurgingEvent

        /// <summary>
        /// Handles invocation of RowsPurging events
        /// </summary>
        internal bool DoRowsPurgingEvent(GridContainer row)
        {
            if (RowsPurging != null)
            {
                GridRowCancelEventArgs ev = new
                    GridRowCancelEventArgs(row.GridPanel, row);

                RowsPurging(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowsPurgedEvent

        /// <summary>
        /// Handles invocation of RowsPurged events
        /// </summary>
        internal void DoRowsPurgedEvent(GridContainer row)
        {
            if (RowsPurged != null)
            {
                GridRowEventArgs ev = new GridRowEventArgs(row.GridPanel, row);

                RowsPurged(this, ev);
            }
        }

        #endregion
        
        #region RowsSortingEvent

        /// <summary>
        /// Handles invocation of RowsSorting events
        /// </summary>
        internal bool DoRowsSortingEvent(GridPanel panel)
        {
            if (RowsSorting != null)
            {
                GridCancelEventArgs ev = new GridCancelEventArgs(panel);

                RowsSorting(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowsSortedEvent

        /// <summary>
        /// Handles invocation of RowsSortedEvent events
        /// </summary>
        internal void DoRowsSortedEvent(GridPanel panel)
        {
            if (RowsSorted != null)
            {
                GridEventArgs ev = new GridEventArgs(panel);

                RowsSorted(this, ev);
            }
        }

        #endregion
        
        #region RowValidatingEvent

        /// <summary>
        /// Handles invocation of RowValidating events
        /// </summary>
        internal bool DoRowValidatingEvent(GridRow gridRow)
        {
            if (RowValidating != null)
            {
                GridRowValidatingEventArgs ev = new
                    GridRowValidatingEventArgs(gridRow.GridPanel, gridRow);

                RowValidating(this, ev);

                return (ev.Cancel);
            }

            return (false);
        }

        #endregion

        #region RowValidatedEvent

        /// <summary>
        /// Handles invocation of RowValidated events
        /// </summary>
        internal void DoRowValidatedEvent(GridRow gridRow)
        {
            if (RowValidated != null)
            {
                GridRowValidatedEventArgs ev = new
                    GridRowValidatedEventArgs(gridRow.GridPanel, gridRow);

                RowValidated(this, ev);
            }
        }

        #endregion

        #endregion

        #region ScrollEvent

        /// <summary>
        /// Handles invocation of Scroll events
        /// </summary>
        internal void DoScrollEvent(GridPanel gridPanel,
            ScrollEventArgs args, ScrollBarAdv sbar)
        {
            if (Scroll != null)
            {
                GridScrollEventArgs ev = new
                    GridScrollEventArgs(gridPanel, args);

                Scroll(this, ev);
            }

            if (args.Type == ScrollEventType.EndScroll)
            {
                if (args.NewValue == 0)
                    DoScrollMaxEvent(gridPanel, args);

                else if (args.NewValue + sbar.LargeChange >= sbar.Maximum)
                    DoScrollMaxEvent(gridPanel, args);
            }
        }

        #endregion

        #region ScrollMinEvent

        /// <summary>
        /// Handles invocation of ScrollMin events
        /// </summary>
        internal void DoScrollMinEvent(GridPanel gridPanel, ScrollEventArgs args)
        {
            if (ScrollMin != null)
            {
                GridScrollEventArgs ev = new
                    GridScrollEventArgs(gridPanel, args);

                ScrollMin(this, ev);
            }
        }

        #endregion

        #region ScrollMaxEvent

        /// <summary>
        /// Handles invocation of ScrollMax events
        /// </summary>
        internal void DoScrollMaxEvent(GridPanel gridPanel, ScrollEventArgs args)
        {
            if (ScrollMax != null)
            {
                GridScrollEventArgs ev = new
                    GridScrollEventArgs(gridPanel, args);

                ScrollMax(this, ev);
            }
        }

        #endregion

        #region SelectionChanged Event

        /// <summary>
        /// Handles invocation of SelectionChanged events
        /// </summary>
        internal void DoSelectionChangedEvent(GridPanel gridPanel)
        {
            if (SelectionChanged != null)
            {
                GridEventArgs ev = new
                    GridEventArgs(gridPanel);

                SelectionChanged(this, ev);
            }
        }

        #endregion

        #region SortChanged Event

        /// <summary>
        /// Handles invocation of SortChanged events
        /// </summary>
        internal void DoSortChangedEvent(GridPanel gridPanel)
        {
            if (SortChanged != null)
            {
                GridEventArgs ev = new
                    GridEventArgs(gridPanel);

                SortChanged(this, ev);
            }
        }

        #endregion

        #region TextRowClickEvent

        /// <summary>
        /// Handles invocation of TextRowClick events
        /// </summary>
        internal void DoTextRowClickEvent(GridTextRow textRow, MouseEventArgs e)
        {
            if (TextRowClick != null)
            {
                GridTextRowEventArgs ev = new
                    GridTextRowEventArgs(textRow.GridPanel, textRow, e);

                TextRowClick(this, ev);
            }
        }

        #endregion

        #region TextRowHeaderClickEvent

        /// <summary>
        /// Handles invocation of TextRowHeaderClick events
        /// </summary>
        internal void DoTextRowHeaderClickEvent(GridTextRow textRow, MouseEventArgs e)
        {
            if (TextRowHeaderClick != null)
            {
                GridTextRowEventArgs ev = new
                    GridTextRowEventArgs(textRow.GridPanel, textRow, e);

                TextRowHeaderClick(this, ev);
            }
        }

        #endregion

        #region TextRowMarkupLinkClickEvent

        /// <summary>
        /// Handles invocation of TextRowMarkupLinkClick events
        /// </summary>
        internal void DoTextRowMarkupLinkClickEvent(
            GridTextRow textRow, HyperLink hyperLink)
        {
            if (TextRowMarkupLinkClick != null)
            {
                GridTextRowMarkupLinkClickEventArgs ev = new GridTextRowMarkupLinkClickEventArgs(
                    textRow.GridPanel, textRow, hyperLink.Name, hyperLink.HRef);

                TextRowMarkupLinkClick(this, ev);
            }
        }

        #endregion

        #region Virtual Row events

        #region DoLoadVirtualRowEvent

        /// <summary>
        /// Handles invocation of LoadVirtualRow events
        /// </summary>
        internal void DoLoadVirtualRowEvent(GridPanel panel, GridRow row)
        {
            if (LoadVirtualRow != null)
            {
                GridVirtualRowEventArgs ev = new
                    GridVirtualRowEventArgs(panel, row, row.Index);

                LoadVirtualRow(this, ev);
            }
        }

        #endregion

        #region DoStoreVirtualRowEvent

        /// <summary>
        /// Handles invocation of StoreVirtualRow events
        /// </summary>
        internal void DoStoreVirtualRowEvent(GridPanel gridPanel, GridRow gridRow)
        {
            if (StoreVirtualRow != null)
            {
                GridVirtualRowEventArgs ev = new
                    GridVirtualRowEventArgs(gridPanel, gridRow, gridRow.Index);

                StoreVirtualRow(this, ev);
            }
        }

        #endregion

        #endregion

        #endregion

        #region GetElementAt

        ///<summary>
        /// Gets the GridElement at the given coordinates
        ///</summary>
        ///<param name="pt"></param>
        ///<returns></returns>
        public GridElement GetElementAt(Point pt)
        {
            return (GetElementAt(pt.X, pt.Y));
        }

        ///<summary>
        /// Gets the GridElement at the given coordinates
        ///</summary>
        ///<param name="x"></param>
        ///<param name="y"></param>
        ///<returns></returns>
        public GridElement GetElementAt(int x, int y)
        {
            GridElement item = _PrimaryGrid.GetElementAt(x, y);

            if (item == null)
                return (_PrimaryGrid);

            while (item is GridContainer)
            {
                GridElement subItem = ((GridContainer) item).GetElementAt(x, y);

                if (subItem == null)
                    break;

                item = subItem;
            }

            return (item);
        }

        #endregion

        #region FindGridPanel

        ///<summary>
        /// Finds the defined GridPanel with the given Name.
        /// Nested GridPanels will not be searched.
        ///</summary>
        ///<returns>GridPanel or null</returns>
        public GridPanel FindGridPanel(string name)
        {
            return (FindGridPanel(name, false));
        }

        ///<summary>
        /// Finds the defined GridPanel with the given Name.
        /// If 'includeNested' is true, then nested GridPanels
        /// will also be searched.
        ///</summary>
        ///<returns></returns>
        public GridPanel FindGridPanel(string name, bool includeNested)
        {
            if (_PrimaryGrid != null)
            {
                if (_PrimaryGrid.Name != null && _PrimaryGrid.Name.Equals(name) == true)
                    return (_PrimaryGrid);

                return (_PrimaryGrid.FindGridPanel(name, includeNested));
            }

            return (null);
        }

        #endregion

        #region GetSelectedElements

        ///<summary>
        /// This routine returns a SelectedElementCollection,
        /// containing a list of the currently selected elements.
        ///</summary>
        ///<returns>SelectedElementCollection</returns>
        public SelectedElementCollection GetSelectedElements()
        {
            SelectedElementCollection items =
                _PrimaryGrid.GetSelectedElements();

            GetSelectedElements(_PrimaryGrid, items);

            return (items);
        }

        private void GetSelectedElements(
            GridContainer container, SelectedElementCollection items)
        {
            foreach (GridElement item in container.Rows)
            {
                if (item.Visible == true)
                {
                    GridContainer citem = item as GridContainer;

                    if (citem != null)
                    {
                        GridPanel panel = citem as GridPanel;

                        if (panel != null)
                        {
                            if (panel.VirtualMode == false)
                                panel.GetSelectedElements(items);
                        }

                        if (citem.Rows.Count > 0)
                            GetSelectedElements(citem, items);
                    }
                }
            }
        }

        #endregion

        #region GetSelectedRows

        ///<summary>
        /// This routine returns a SelectedElementCollection,
        /// containing a list of the currently selected rows.
        ///</summary>
        ///<returns>SelectedElementCollection</returns>
        public SelectedElementCollection GetSelectedRows()
        {
            SelectedElementCollection items =
                _PrimaryGrid.GetSelectedRows();             
            
            GetSelectedRows(_PrimaryGrid, items);

            return (items);
        }

        private void GetSelectedRows(
            GridContainer container, ICollection<GridElement> items)
        {
            foreach (GridElement item in container.Rows)
            {
                if (item.Visible == true)
                {
                    GridContainer citem = item as GridContainer;

                    if (citem != null)
                    {
                        GridPanel panel = citem as GridPanel;

                        if (panel != null)
                        {
                            if (panel.VirtualMode == false)
                                panel.GetSelectedRows(items);
                        }

                        if (citem.Rows.Count > 0)
                            GetSelectedRows(citem, items);
                    }
                }
            }
        }

        #endregion

        #region GetSelectedColumns

        ///<summary>
        /// This routine returns a SelectedElementCollection,
        /// containing a list of the currently selected columns.
        ///</summary>
        ///<returns>SelectedElementCollection</returns>
        public SelectedElementCollection GetSelectedColumns()
        {
            SelectedElementCollection items = 
                _PrimaryGrid.GetSelectedColumns();

            GetSelectedColumns(_PrimaryGrid, items);

            return (items);
        }

        private void GetSelectedColumns(GridContainer container, ICollection<GridElement> items)
        {
            foreach (GridElement item in container.Rows)
            {
                if (item.Visible == true)
                {
                    GridContainer citem = item as GridContainer;

                    if (citem != null)
                    {
                        GridPanel panel = citem as GridPanel;

                        if (panel != null)
                        {
                            if (panel.VirtualMode == false)
                                panel.GetSelectedColumns(items);
                        }

                        if (citem.Rows.Count > 0)
                            GetSelectedColumns(citem, items);
                    }
                }
            }
        }

        #endregion

        #region GetSelectedCells

        ///<summary>
        /// This routine returns a SelectedElementCollection,
        /// containing a list of the currently selected cells.
        ///</summary>
        ///<returns>SelectedElementCollection</returns>
        public SelectedElementCollection GetSelectedCells()
        {
            SelectedElementCollection items =
                _PrimaryGrid.GetSelectedCells();

            GetSelectedCells(_PrimaryGrid, items);

            return (items);
        }

        private void GetSelectedCells(GridContainer container, SelectedElementCollection items)
        {
            foreach (GridElement item in container.Rows)
            {
                if (item.Visible == true)
                {
                    GridContainer citem = item as GridContainer;

                    if (citem != null)
                    {
                        GridPanel panel = citem as GridPanel;

                        if (panel != null)
                        {
                            if (panel.VirtualMode == false)
                                panel.GetSelectedCells(items);
                        }

                        if (citem.Rows.Count > 0)
                            GetSelectedCells(citem, items);
                    }
                }
            }
        }

        #endregion

        #region GetCell

        ///<summary>
        /// Gets the GridCell for the given
        /// row and column index of the PrimaryGrid
        ///</summary>
        ///<param name="rowIndex"></param>
        ///<param name="columnIndex"></param>
        ///<returns>GridCell, or null if not a valid cell</returns>
        public GridCell GetCell(int rowIndex, int columnIndex)
        {
            return (PrimaryGrid.GetCell(rowIndex, columnIndex));
        }

        ///<summary>
        /// Gets the GridCell for the given
        /// row and column index of the given GridPanel
        ///</summary>
        ///<param name="panel"></param>
        ///<param name="rowIndex"></param>
        ///<param name="columnIndex"></param>
        ///<returns>GridCell, or null if not a valid cell</returns>
        public GridCell GetCell(GridPanel panel, int rowIndex, int columnIndex)
        {
            return (panel.GetCell(rowIndex, columnIndex));
        }

        #endregion

        #region AutoScrolling support

        #region EnableAutoScrolling

        internal void EnableAutoScrolling(
            AutoScrollEnable enable, Rectangle scrollRect)
        {
            if (Focused == true)
            {
                _AutoScrollEnable = enable;

                if ((_HScrollBar != null && _HScrollBar.Visible == true) ||
                    (_VScrollBar != null && _VScrollBar.Visible == true))
                {
                    _ScrollRect = scrollRect;

                    if (_AutoScrollTimer == null)
                    {
                        _AutoScrollTimer = new Timer();

                        _AutoScrollTimer.Interval = 10;
                        _AutoScrollTimer.Tick += AutoScrollTimerTick;
                        _AutoScrollTimer.Start();
                    }
                }
            }
        }

        #endregion

        #region DisableAutoScrolling

        internal void DisableAutoScrolling()
        {
            if (_AutoScrollTimer != null)
            {
                _AutoScrollTimer.Stop();
                _AutoScrollTimer.Tick -= AutoScrollTimerTick;

                _AutoScrollTimer = null;
            }
        }

        #endregion

        #region AutoScrollTimerTick

        private void AutoScrollTimerTick(object sender, EventArgs e)
        {
            Point pt = PointToClient(Cursor.Position);
            Rectangle t = _ScrollRect;

            if ((_AutoScrollEnable & AutoScrollEnable.Horizontal) == AutoScrollEnable.Horizontal &&
                (_HScrollBar != null && _HScrollBar.Visible == true))
            {
                int dx = (pt.X < t.X)
                    ? ScrollAmount(pt.X - t.X)
                    : (pt.X >= t.Right) ? ScrollAmount(pt.X - t.Right) : 0;

                SetHScrollValue(_HScrollOffset + dx);
            }

            if ((_AutoScrollEnable & AutoScrollEnable.Vertical) == AutoScrollEnable.Vertical &&
                (_VScrollBar != null && _VScrollBar.Visible == true))
            {
                int dy = (pt.Y < t.Top)
                    ? ScrollAmount(pt.Y - t.Top)
                    : (pt.Y >= t.Bottom) ? ScrollAmount(pt.Y - t.Bottom) : 0;

                SetVScrollValue(_VScrollOffset + dy);
            }
        }

        #endregion

        #region ScrollAmount

        private int ScrollAmount(int delta)
        {
            int n = Math.Abs(delta);
            int amt = 1 << ((n / 16) + 1);

            return (delta < 0 ? -amt : amt);
        }

        #endregion

        #endregion

        #region PostInternalMouseMove

        internal void PostInternalMouseMove()
        {
            _PostMouseMove = true;
        }

        #endregion

        #region Cursor

        public override Cursor Cursor
        {
            get { return base.Cursor; }

            set
            {
                base.Cursor = value;

                _DefaultCursor = value;
            }
        }

        #endregion

        #region OnGotFocus

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (_ActiveRow != null)
                _ActiveRow.InvalidateRender();
        }

        #endregion

        #region OnLostFocus

        protected override void OnLostFocus(EventArgs e)
        {
            CancelCapture();

            if (_ActiveRow != null)
                _ActiveRow.InvalidateRender();

            base.OnLostFocus(e);
        }

        #endregion

        #region OnResize

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (_PrimaryGrid != null)
            {
                _PrimaryGrid.InvalidateLayout();
                _PrimaryGrid.ContainerBounds = ClientRectangle;
            }
        }

        #endregion

        #region OnValidating

        protected override void OnValidating(CancelEventArgs e)
        {
            if (_ActiveRow != null)
            {
                if (_ActiveRow.GridPanel.SetActiveRow(null, false) == false)
                {
                    e.Cancel = true;

                    return;
                }
            }

            base.OnValidating(e);
        }

        #endregion

        #region Update support

        ///<summary>
        /// Calling the BeginUpdate routine informs the grid
        /// that an extended update phase has begun. The SuperGrid
        /// will suspend all layout calculations and display updates
        /// until the corresponding EndUpdate routine is called.
        /// 
        /// BeginUpdate / EndUpdate can be nested and must be
        /// called in pairs – every BeginUpdate must have a
        /// matching EndUpdate call.
        ///</summary>
        public void BeginUpdate()
        {
            _BeginUpdateCount++;
        }

        ///<summary>
        /// Calling the EndUpdate routine informs the grid
        /// that an extended update phase has ended.
        /// 
        /// BeginUpdate / EndUpdate can be nested and must be
        /// called in pairs – every EndUpdate must have a
        /// matching BeginUpdate call.
        ///</summary>
        public void EndUpdate()
        {
            if (_BeginUpdateCount > 0)
            {
                if (--_BeginUpdateCount == 0)
                    _PrimaryGrid.InvalidateLayout();
            }
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
            PropertyChangedEventHandler handler = PropertyChanged;

            if (handler != null)
                handler(this, e);
        }

        #endregion

        #region IMessageFilter Members

        /// <summary>
        /// PreFilterMessage
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public bool PreFilterMessage(ref Message m)
        {
            if (DesignMode == true || Enabled == false)
                return (false);

            if (Focused == true || (_ActiveEditor != null ||
                _ActiveNonModalEditor != null || _ActiveFilterPanel != null))
            {
                switch (m.Msg)
                {
                    case WmKeyDown:
                        TrackModifiers();

                        if (_KeyPressSent == true)
                        {
                            _KeyPressSent = false;
                        }
                        else
                        {
                            _IsInputKey = false;

                            Keys keyData = (Keys) (int) m.WParam & Keys.KeyCode;

                            if (DoGridPreviewKeyDown(keyData) == true)
                                return (true);

                            if ((int) keyData >= (int) Keys.NumPad0 && (int) keyData <= (int) Keys.NumPad9)
                                keyData = (Keys) ((int) Keys.D0 + (int) keyData - (int) Keys.NumPad0);

                            keyData |= ModifierKeys;

                            _LastKeyDown = keyData;

                            if (_ActiveEditor != null && _EditorCell != null)
                                return (ProcessInEditKeyDown(keyData));

                            if (_ActiveFilterPanel != null)
                                return (ProcessInFilterEditKeyDown(keyData));
                        }

                        return (false);

                    case WmKeyUp:
                        TrackModifiers();
                        break;

                    case WmMouseMove:
                        if (_ActiveNonModalEditor != null)
                        {
                            if (IsOverNonModalEditorWindow() == false)
                            {
                                if (_ActiveNonModalEditor.CanInterrupt == true)
                                    DeactivateNonModalEditor();
                            }
                        }
                        break;

                    case WmMouseDown:
                        if (IsOverNonModalEditorWindow() == true)
                            ExtendNonModalSelection();
                        break;

                    case WmMouseWheel:
                        if (_ActiveFilterPanel == null)
                        {
                            if ((LoWord(m.WParam) & 0x8) == 0)
                            {
                                if (_VScrollBar.Visible == true)
                                {
                                    Rectangle r = DisplayRectangle;
                                    r.Location = PointToScreen(r.Location);

                                    Point mousePos = new Point(LoWord(m.LParam), HiWord(m.LParam));

                                    if (r.Contains(mousePos))
                                    {
                                        int value = -(HiWord(m.WParam)*
                                                      SystemInformation.MouseWheelScrollLines)/120;

                                        value *= _VScrollBar.SmallChange;
                                        value += _VScrollBar.Value;

                                        SetVScrollValue(value);

                                        return (true);
                                    }
                                }
                            }
                        }
                        break;
                }
            }

            return (false);
        }

        #region HiWord / LoWord

        private int LoWord(int n)
        {
            return (short)(n & 0xffff);
        }

        private int HiWord(int n)
        {
            return (n >> 0x10);
        }

        private int LoWord(IntPtr n)
        {
            return LoWord((int)((long)n));
        }

        private int HiWord(IntPtr n)
        {
            return HiWord((int)((long)n));
        }

        #endregion

        #region TrackModifiers

        private void TrackModifiers()
        {
            if ((_LastModifierKeys & Keys.Control) != (ModifierKeys & Keys.Control))
            {
                _LastModifierKeys = ModifierKeys;

                Cursor.Position = Cursor.Position;
            }
        }

        #endregion

        #region IsOverNonModalEditorWindow

        private bool IsOverNonModalEditorWindow()
        {
            if (_ActiveNonModalEditor != null)
            {
                Point pt = MousePosition;

                Rectangle r = NonModalEditorCell.CellBounds;
                r.Location = PointToScreen(r.Location);

                bool hit = r.Contains(pt);

                return (hit);
            }

            return (false);
        }

        #endregion

        #endregion

        #region WndProc

        protected override void WndProc(ref Message m)
        {
            switch (m.Msg)
            {
                case WmSetFocus:
                    if (_PopupControl != null)
                        _PopupControl.Hide();
                    break;
            }

            base.WndProc(ref m);
        }

        #endregion

        #region ActivateNonModalEditor

        internal void ActivateNonModalEditor(
            IGridCellEditControl editor, GridCell cell)
        {
            ActiveNonModalEditor = editor;
            NonModalEditorCell = cell;
        }

        #endregion

        #region DeactivateNonModalEditor

        ///<summary>
        /// Deactivates any active NonModal editor.
        ///</summary>
        public void DeactivateNonModalEditor()
        {
            if (_ActiveNonModalEditor != null)
            {
                _ActiveNonModalEditor.EditorPanel.Hide();
                _ActiveNonModalEditor.OnCellMouseLeave(EventArgs.Empty);

                if (_NonModalEditorCell.GridPanel.SelectionGranularity != SelectionGranularity.Cell)
                    _NonModalEditorCell.GridRow.InvalidateRender();
                else
                    _NonModalEditorCell.InvalidateRender();

                if (_NonModalEditorCell.SuperGrid.ContainsFocus == true)
                    Focus();

                _ActiveNonModalEditor = null;
                _NonModalEditorCell = null;
            }
        }

        #region ExtendNonModalSelection

        ///<summary>
        /// ExtendNonModalSelection
        ///</summary>
        internal void ExtendNonModalSelection()
        {
            GridPanel panel = _NonModalEditorCell.GridPanel;

            if (panel != null)
            {
                GridCell cell = _NonModalEditorCell;
                GridRow row = cell.GridRow;

                panel.SetActiveRow(row, true);

                switch (panel.SelectionGranularity)
                {
                    case SelectionGranularity.Row:
                    case SelectionGranularity.RowWithCellHighlight:
                        row.ExtendSelection(panel);
                        break;

                    case SelectionGranularity.Cell:
                        cell.ExtendSelection(panel);

                        if (_ActiveNonModalEditor != null)
                            cell.PositionEditPanel(_ActiveNonModalEditor);
                        break;
                }
            }
        }

        #endregion

        #endregion

        #region UpdateStyleCount

        internal int UpdateStyleCount()
        {
            StyleUpdateCount++;

            return (StyleUpdateCount);
        }

        #endregion

        #region CancelCapture

        ///<summary>
        /// Cancels any in-progress operations that
        /// may have the mouse captured (and releases the capture).
        ///</summary>
        public void CancelCapture()
        {
            if (_CapturedItem != null)
                _CapturedItem.CancelCapture();
        }

        #endregion

        #region Dispose

        protected override void Dispose(bool disposing)
        {
            Application.RemoveMessageFilter(this);
            StyleManager.Unregister(this);

            PrimaryGrid.Dispose();
            
            base.Dispose(disposing);
        }

        #endregion

        #region Licensing
#if !TRIAL
        private string _LicenseKey = "";
        ///<summary>
        /// LicenseKey
        ///</summary>
        [Browsable(false), DefaultValue("")]
        public string LicenseKey
        {
            get { return _LicenseKey; }
            set
            {
                if (NativeFunctions.ValidateLicenseKey(value))
                    return;
                _LicenseKey = (!NativeFunctions.CheckLicenseKey(value) ? "9dsjkhds7" : value);
            }
        }
#endif
        #endregion

        #region Touch Handling

        private Touch.TouchHandler _TouchHandler;
        private bool _TouchEnabled = true;

        /// <summary>
        /// Indicates whether touch support for scrolling is enabled.
        /// </summary>
        [DefaultValue(true), Category("Behavior")]
        [Description("Indicates whether touch support for scrolling is enabled.")]
        public bool TouchEnabled
        {
            get { return _TouchEnabled; }

            set
            {
                if (value != _TouchEnabled)
                {
                    bool oldValue = _TouchEnabled;
                    _TouchEnabled = value;

                    OnTouchEnabledChanged(oldValue, value);
                }
            }
        }

        /// <summary>
        /// Called when TouchEnabled property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnTouchEnabledChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("TouchEnabled"));
        }

        //private int TriggerPageChangeOffset
        //{
        //    get { return 32; }
        //}

        private int MaximumReversePageOffset
        {
            get { return 0; } //Math.Min(32, this.Width / 6);
        }

        private bool _TouchDrag;
        private Point _TouchStartLocation = Point.Empty;
        private Point _TouchStartScrollPosition = Point.Empty;
        private Rectangle _TouchInnerBounds = Rectangle.Empty;

        private void TouchHandlerPanBegin(object sender, Touch.GestureEventArgs e)
        {
            if (_TouchEnabled)
            {
                _TouchInnerBounds = ClientRectangle;
                _TouchStartLocation = e.Location;
                _TouchStartScrollPosition = AutoScrollPosition;
                _TouchDrag = true;

                e.Handled = true;
            }
        }

        private void TouchHandlerPanEnd(object sender, Touch.GestureEventArgs e)
        {
            if (_TouchDrag)
            {
                EndTouchPan();

                e.Handled = true;
            }
        }

        private Point AutoScrollPosition
        {
            get { return new Point(-_HScrollOffset, -_VScrollOffset); }

            set
            {
                if (value.X != _HScrollOffset)
                    SetHScrollValue(-value.X);

                if (value.Y != _VScrollOffset)
                    SetVScrollValue(-value.Y);
            }
        }

        private void EndTouchPan()
        {
            _TouchDrag = false;

            Point autoScrollPosition = AutoScrollPosition;
            Size autoScrollMinSize = new Size(_HScrollBar.Maximum, _VScrollBar.Maximum);

            if (autoScrollMinSize.Width > _TouchInnerBounds.Width)
            {
                if (autoScrollMinSize.Width - _TouchInnerBounds.Width < -autoScrollPosition.X)
                    autoScrollPosition = new Point(autoScrollMinSize.Width - _TouchInnerBounds.Width, autoScrollPosition.Y);
                else if (-autoScrollPosition.X < 0)
                    autoScrollPosition = new Point(0, autoScrollPosition.Y);
            }

            if (autoScrollMinSize.Height > _TouchInnerBounds.Height)
            {
                if (autoScrollMinSize.Height - _TouchInnerBounds.Height < -autoScrollPosition.Y)
                    autoScrollPosition = new Point(autoScrollPosition.X, autoScrollMinSize.Height - _TouchInnerBounds.Height);
                else if (-autoScrollPosition.Y < 0)
                    autoScrollPosition = new Point(autoScrollPosition.X, 0);
            }

            if (AutoScrollPosition != autoScrollPosition)
                AutoScrollPosition = autoScrollPosition;

            //ApplyScrollChange();

        }

        private void TouchHandlerPan(object sender, Touch.GestureEventArgs e)
        {
            if (_TouchDrag)
            {
                Point autoScrollPosition = AutoScrollPosition;
                Size autoScrollMinSize = new Size(_HScrollBar.Maximum, _VScrollBar.Maximum);
                int offset = (e.Location.X - _TouchStartLocation.X);
                int offsetChange = offset + _TouchStartScrollPosition.X;

                bool overflowH = false;

                if (autoScrollMinSize.Width > _TouchInnerBounds.Width)
                {
                    if (-offsetChange + MaximumReversePageOffset > autoScrollMinSize.Width - _TouchInnerBounds.Width)
                    {
                        autoScrollPosition.X = -(autoScrollMinSize.Width + MaximumReversePageOffset - _TouchInnerBounds.Width);
                        overflowH = true;
                    }
                    else if (offsetChange > MaximumReversePageOffset)
                    {
                        autoScrollPosition.X = MaximumReversePageOffset;
                        overflowH = true;
                    }
                    else
                        autoScrollPosition.X = offsetChange;
                }

                // Y Scroll
                bool overflowV = false;
                if (autoScrollMinSize.Height > _TouchInnerBounds.Height)
                {
                    offset = (e.Location.Y - _TouchStartLocation.Y);
                    offsetChange = offset + _TouchStartScrollPosition.Y;

                    if (-offsetChange + MaximumReversePageOffset > autoScrollMinSize.Height - _TouchInnerBounds.Height)
                    {
                        autoScrollPosition.Y = -(autoScrollMinSize.Height + MaximumReversePageOffset - _TouchInnerBounds.Height);
                        overflowV = true;
                    }
                    else if (offsetChange > MaximumReversePageOffset)
                    {
                        autoScrollPosition.Y = MaximumReversePageOffset;
                        overflowV = true;
                    }
                    else
                        autoScrollPosition.Y = offsetChange;
                }
                
                if (AutoScrollPosition != autoScrollPosition)
                {
                    AutoScrollPosition = autoScrollPosition;
                    Update();
                }

                if (overflowH && overflowV && e.IsInertia)
                    EndTouchPan();

                e.Handled = true;
            }
        }

        #endregion

        #region Localize Members

        #region InvokeLocalizeString

        ///<summary>
        /// InvokeLocalizeString
        ///</summary>
        ///<param name="e"></param>
        public void InvokeLocalizeString(LocalizeEventArgs e)
        {
            if (LocalizeString != null)
                LocalizeString(this, e);
        }

        #endregion

        #region LocalizedString

        private string LocalizedString(ref string lstring)
        {
            LoadLocalizedStrings();

            return (lstring);
        }

        #endregion

        #region LoadLocalizedStrings

        private void LoadLocalizedStrings()
        {
            if (_LocalizedStringsLoaded == false)
            {
                using (LocalizationManager lm = new LocalizationManager(this))
                {
                    string s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterShowAll)) != "")
                        _FilterShowAllString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterCustom)) != "")
                        _FilterCustomString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterShowNull)) != "")
                        _FilterShowNullString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterShowNotNull)) != "")
                        _FilterShowNotNullString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterApply)) != "")
                        _FilterApplyString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterOk)) != "")
                        _FilterOkString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterCancel)) != "")
                        _FilterCancelString = s;

                    if ((s = lm.GetLocalizedString(LocalizationKeys.SuperGridFilterClose)) != "")
                        _FilterCloseString = s;
                }

                _LocalizedStringsLoaded = true;
            }
        }

        #endregion

        #endregion
    }

    #region enums

    #region AutoScrollEnable

    [Flags]
    internal enum AutoScrollEnable
    {
        Vertical = 0,
        Horizontal = 1,
    }

    #endregion

    #region DataContext

    ///<summary>
    /// Context under which data is being accessed
    ///</summary>
    public enum DataContext
    {
        ///<summary>
        /// CellEdit
        ///</summary>
        CellEdit,

        ///<summary>
        /// CellKeyEvent
        ///</summary>
        CellKeyEvent,

        ///<summary>
        /// CellMouseEvent
        ///</summary>
        CellMouseEvent,

        ///<summary>
        /// CellProposedSize
        ///</summary>
        CellProposedSize,

        ///<summary>
        /// CellRender
        ///</summary>
        CellRender,

        ///<summary>
        /// CellValueLoad
        ///</summary>
        CellValueLoad,

        ///<summary>
        /// CellValueStore
        ///</summary>
        CellValueStore,

        ///<summary>
        /// CellExpressionParse
        ///</summary>
        CellExpressionParse,

        ///<summary>
        /// CellExpressionEval
        ///</summary>
        CellExpressionEval,

        ///<summary>
        /// SetRowPosition
        ///</summary>
        SetRowPosition,

        ///<summary>
        /// RowFlush
        ///</summary>
        RowFlush,

        ///<summary>
        /// InsertRow
        ///</summary>
        InsertRow,
    }

    #endregion

    #region ExpandButtonType

    ///<summary>
    /// Expand button type
    ///</summary>
    public enum ExpandButtonType
    {
        ///<summary>
        /// NotSet
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// None
        ///</summary>
        None,

        ///<summary>
        /// Circle
        ///</summary>
        Circle,

        ///<summary>
        /// Square
        ///</summary>
        Square,

        ///<summary>
        /// Triangle
        ///</summary>
        Triangle,
    }

    #endregion

    #region NewRowContext

    ///<summary>
    /// Context under which the New
    /// row is being accessed / created
    ///</summary>
    public enum NewRowContext
    {
        ///<summary>
        /// RowInit
        ///</summary>
        RowInit,

        ///<summary>
        /// RowActivate
        ///</summary>
        RowActivate,

        ///<summary>
        /// RowDeactivate
        ///</summary>
        RowDeactivate,
    }

    #endregion

    #region RenderParts

    ///<summary>
    /// Identifies grid 'parts' to be rendered
    ///</summary>
    [Flags]
    public enum RenderParts
    {
        ///<summary>
        /// Nothing to render
        ///</summary>
        Nothing = 0,

        ///<summary>
        /// Background needs to be rendered
        ///</summary>
        Background = (1 << 0),

        ///<summary>
        /// Border needs to be rendered
        ///</summary>
        Border = (1 << 1),

        ///<summary>
        /// Content needs to be rendered
        ///</summary>
        Content = (1 << 2),

        ///<summary>
        /// RowHeader needs to be rendered
        ///</summary>
        RowHeader = (1 << 3),

        ///<summary>
        /// Whitespace needs to be rendered
        ///</summary>
        Whitespace = (1 << 4),
    }

    #endregion

    #region TabSelection

    ///<summary>
    /// Identifies selection style when the Tab key is pressed
    ///</summary>
    public enum TabSelection
    {
        ///<summary>
        /// Previous / Next cell.
        ///</summary>
        Cell,

        ///<summary>
        /// Previous / Next cell in the same row.
        ///</summary>
        CellSameRow,

        ///<summary>
        /// Previous / Next Control.
        ///</summary>
        Control,
    }

    #endregion

    #endregion

    #region EventArgs

    #region GridCellActivatedEventArgs

    /// <summary>
    /// GridCellActivatedEventArgs
    /// </summary>
    public class GridCellActivatedEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridCell _OldActiveCell;
        private readonly GridCell _NewActiveCell;

        #endregion

        ///<summary>
        /// GridCellActivatedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="oldCell"></param>
        ///<param name="newCell"></param>
        public GridCellActivatedEventArgs(
            GridPanel gridPanel, GridCell oldCell, GridCell newCell)
            : base(gridPanel)
        {
            _OldActiveCell = oldCell;
            _NewActiveCell = newCell;
        }

        #region Public properties

        /// <summary>
        /// Gets the old (previous) ActiveCell
        /// </summary>
        public GridCell OldActiveCell
        {
            get { return (_OldActiveCell); }
        }

        /// <summary>
        /// Gets the new (current) ActiveCell
        /// </summary>
        public GridCell NewActiveCell
        {
            get { return (_NewActiveCell); }
        }

        #endregion
    }

    #endregion

    #region GridCellActivatingEventArgs

    /// <summary>
    /// GridCellActivatingEventArgs
    /// </summary>
    public class GridCellActivatingEventArgs : GridCellActivatedEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridCellActivatedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="oldCell"></param>
        ///<param name="newCell"></param>
        public GridCellActivatingEventArgs(
            GridPanel gridPanel, GridCell oldCell, GridCell newCell)
            : base(gridPanel, oldCell, newCell)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to Cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridActiveGridChangedEventArgs

    /// <summary>
    /// ActiveGridChangedEventArgs
    /// </summary>
    public class GridActiveGridChangedEventArgs : EventArgs
    {
        #region Private variables

        private readonly GridPanel _NewGridPanel;
        private readonly GridPanel _OldGridPanel;

        #endregion

        ///<summary>
        /// GridActiveGridChangedEventArgs
        ///</summary>
        ///<param name="oldGridPanel"></param>
        ///<param name="newGridPanel"></param>
        public GridActiveGridChangedEventArgs(
            GridPanel oldGridPanel, GridPanel newGridPanel)
        {
            _OldGridPanel = oldGridPanel;
            _NewGridPanel = newGridPanel;
        }

        #region Public properties

        /// <summary>
        /// Gets the old (previous) active GridPanel
        /// </summary>
        public GridPanel OldGridPanel
        {
            get { return (_OldGridPanel); }
        }

        /// <summary>
        /// Gets the new (current) active GridPanel
        /// </summary>
        public GridPanel NewGridPanel
        {
            get { return (_NewGridPanel); }
        }

        #endregion
    }

    #endregion

    #region GridAfterCheckEventArgs

    /// <summary>
    /// GridAfterCheckEventArgs
    /// </summary>
    public class GridAfterCheckEventArgs : GridEventArgs
    {
        #region Private variables

        private GridElement _Item;

        #endregion

        ///<summary>
        /// GridAfterCheckEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="item"></param>
        public GridAfterCheckEventArgs(GridPanel gridPanel, GridElement item)
            : base(gridPanel)
        {
            _Item = item;
        }

        #region Public properties

        /// <summary>
        /// Gets the Item being checked or unchecked
        /// </summary>
        public GridElement Item
        {
            get { return (_Item); }
        }

        #endregion
    }

    #endregion

    #region GridAfterCollapseEventArgs

    /// <summary>
    /// GridAfterCollapseEventArgs
    /// </summary>
    public class GridAfterCollapseEventArgs : GridAfterExpandEventArgs
    {
        ///<summary>
        /// GridRowAfterCollapseEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridContainer"></param>
        ///<param name="expandSource"></param>
        public GridAfterCollapseEventArgs(GridPanel gridPanel, GridContainer gridContainer, ExpandSource expandSource)
            : base(gridPanel, gridContainer, expandSource)
        {
        }
    }

    #endregion

    #region GridAfterExpandEventArgs

    /// <summary>
    /// GridAfterExpandChangeEventArgs
    /// </summary>
    public class GridAfterExpandEventArgs : GridEventArgs
    {
        #region Private variables

        private GridContainer _GridContainer;
        private ExpandSource _ExpandSource;

        #endregion

        ///<summary>
        /// GridAfterExpandChangeEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridContainer"></param>
        ///<param name="expandSource"></param>
        public GridAfterExpandEventArgs(
            GridPanel gridPanel, GridContainer gridContainer, ExpandSource expandSource)
            : base(gridPanel)
        {
            _GridContainer = gridContainer;
            _ExpandSource = expandSource;
        }

        #region Public properties

        /// <summary>
        /// Gets the GridContainer being expanded or collapsed
        /// </summary>
        public GridContainer GridContainer
        {
            get { return (_GridContainer); }
        }

        /// <summary>
        /// Returns the source of the operation
        /// </summary>
        public ExpandSource ExpandSource
        {
            get { return (_ExpandSource); }
        }

        #endregion
    }

    #endregion

    #region GridBeforeCheckEventArgs

    /// <summary>
    /// GridRowBeforeExpandEventArgs
    /// </summary>
    public class GridBeforeCheckEventArgs : GridAfterCheckEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridBeforeCheckEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        public GridBeforeCheckEventArgs(GridPanel gridPanel, GridElement gridRow)
            : base(gridPanel, gridRow)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to Cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridBeforeCollapseEventArgs

    /// <summary>
    /// GridBeforeExpandEventArgs
    /// </summary>
    public class GridBeforeCollapseEventArgs : GridBeforeExpandEventArgs
    {
        ///<summary>
        /// GridBeforeExpandEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridContainer"></param>
        ///<param name="expandSource"></param>
        public GridBeforeCollapseEventArgs(GridPanel gridPanel, GridContainer gridContainer, ExpandSource expandSource)
            : base(gridPanel, gridContainer, expandSource)
        {
        }
    }

    #endregion

    #region GridBeforeExpandEventArgs

    /// <summary>
    /// GridBeforeExpandEventArgs
    /// </summary>
    public class GridBeforeExpandEventArgs : GridAfterExpandEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridRowBeforeExpandEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridContainer"></param>
        ///<param name="expandSource"></param>
        public GridBeforeExpandEventArgs(GridPanel gridPanel, GridContainer gridContainer, ExpandSource expandSource)
            : base(gridPanel, gridContainer, expandSource)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to Cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridCellEventArgs

    /// <summary>
    /// GridCellEventArgs
    /// </summary>
    public class GridCellEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridCell _GridCell;

        #endregion

        ///<summary>
        /// GridCellEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        public GridCellEventArgs(GridPanel gridPanel, GridCell gridCell)
            : base(gridPanel)
        {
            _GridCell = gridCell;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridCell
        /// </summary>
        public GridCell GridCell
        {
            get { return (_GridCell); }
        }

        #endregion
    }

    #endregion

    #region GridCellClickEventArgs

    /// <summary>
    /// GridCellClickEventArgs
    /// </summary>
    public class GridCellClickEventArgs : GridCellEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private MouseEventArgs _MouseEventArgs;

        #endregion

        ///<summary>
        /// GridCellClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="e"></param>
        public GridCellClickEventArgs(
            GridPanel gridPanel, GridCell gridCell, MouseEventArgs e)
            : base(gridPanel, gridCell)
        {
            _MouseEventArgs = e;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated MouseEventArgs
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return (_MouseEventArgs); }
        }

        #endregion
    }

    #endregion

    #region GridCellDoubleClickEventArgs

    /// <summary>
    /// GridCellDoubleClickEventArgs
    /// </summary>
    public class GridCellDoubleClickEventArgs : GridCellClickEventArgs
    {
        ///<summary>
        /// GridCellDoubleClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="e"></param>
        public GridCellDoubleClickEventArgs(GridPanel gridPanel, GridCell gridCell, MouseEventArgs e)
            : base(gridPanel, gridCell, e)
        {
        }
    }

    #endregion

    #region GridCellInfoEnterEventArgs

    /// <summary>
    /// GridCellInfoEnterEventArgs
    /// </summary>
    public class GridCellInfoEnterEventArgs : GridCellEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private Point _Location;

        #endregion

        ///<summary>
        /// GridRowInfoEnterEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="pt"></param>
        public GridCellInfoEnterEventArgs(
            GridPanel gridPanel, GridCell gridCell, Point pt)
            : base(gridPanel, gridCell)
        {
            _Location = pt;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the default operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated event Location
        /// </summary>
        public Point Location
        {
            get { return (_Location); }
        }

        #endregion
    }

    #endregion

    #region GridCellInfoLeaveEventArgs

    /// <summary>
    /// GridCellInfoLeaveEventArgs
    /// </summary>
    public class GridCellInfoLeaveEventArgs : GridCellEventArgs
    {
        ///<summary>
        /// GridRowInfoLeaveEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        public GridCellInfoLeaveEventArgs(
            GridPanel gridPanel, GridCell gridCell)
            : base(gridPanel, gridCell)
        {
        }
    }

    #endregion

    #region GridCellMouseEventArgs

    /// <summary>
    /// GridCellMouseEventArgs
    /// </summary>
    public class GridCellMouseEventArgs : MouseEventArgs
    {
        #region Private variables

        private readonly GridPanel _GridPanel;
        private readonly GridCell _GridCell;

        #endregion

        ///<summary>
        /// GridCellMouseEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="ev"></param>
        public GridCellMouseEventArgs(GridPanel gridPanel, GridCell gridCell, MouseEventArgs ev)
            : base(ev.Button, ev.Clicks, ev.X, ev.Y, ev.Delta)
        {
            _GridPanel = gridPanel;
            _GridCell = gridCell;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridCell
        /// </summary>
        public GridCell GridCell
        {
            get { return (_GridCell); }
        }

        /// <summary>
        /// Gets the associated GridPanel
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        #endregion
    }

    #endregion

    #region GridCellUserFunctionEventArgs

    /// <summary>
    /// GridCellUserFunctionEventArgs
    /// </summary>
    public class GridCellUserFunctionEventArgs : GridCellEventArgs
    {
        #region Private variables

        private object[] _Args;
        private object _Result;

        #endregion

        ///<summary>
        /// GridCellUserFunctionEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="args"></param>
        ///<param name="result"></param>
        public GridCellUserFunctionEventArgs(
            GridPanel gridPanel, GridCell gridCell, object[] args, object result)
            : base(gridPanel, gridCell)
        {
            _Args = args;
            _Result = result;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated function arguments
        /// </summary>
        public object[] Args
        {
            get { return (_Args); }
            set { _Args = value; }
        }

        /// <summary>
        /// Gets or sets the associated function result
        /// </summary>
        public object Result
        {
            get { return (_Result); }
            set { _Result = value; }
        }

        #endregion
    }

    #endregion

    #region GridCellValidatedEventArgs

    /// <summary>
    /// GridCellValidatedEventArgs
    /// </summary>
    public class GridCellValidatedEventArgs : GridCellEventArgs
    {
        ///<summary>
        /// GridCellValidatedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        public GridCellValidatedEventArgs(GridPanel gridPanel, GridCell gridCell)
            : base(gridPanel, gridCell)
        {
        }
    }

    #endregion

    #region GridCellValidatingEventArgs

    /// <summary>
    /// GridCellValidatingEventArgs
    /// </summary>
    public class GridCellValidatingEventArgs : GridCellEventArgs
    {
        #region Private variables

        private bool _Cancel;

        private object _Value;
        private object _FormattedValue;

        #endregion

        ///<summary>
        /// GridCellValidatingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="value"></param>
        ///<param name="formattedValue"></param>
        public GridCellValidatingEventArgs(GridPanel gridPanel,
            GridCell gridCell, object value, object formattedValue)
            : base(gridPanel, gridCell)
        {
            _Value = value;
            _FormattedValue = formattedValue;
        }

        #region Public properties

        /// <summary>
        /// Gets the Value to validate
        /// </summary>
        public object Value
        {
            get { return (_Value); }
        }

        /// <summary>
        /// Gets the formatted Value
        /// </summary>
        public object FormattedValue
        {
            get { return (_FormattedValue); }
        }

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// resulting in the cell validation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridCellValueChangedEventArgs

    /// <summary>
    /// GridCellValueChangedEventArgs
    /// </summary>
    public class GridCellValueChangedEventArgs : GridCellEventArgs
    {
        #region Private variables

        private object _OldValue;
        private object _NewValue;
        private DataContext _DataContext;

        #endregion

        ///<summary>
        /// GridCellValueChangedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="oldValue"></param>
        ///<param name="newValue"></param>
        ///<param name="context"></param>
        public GridCellValueChangedEventArgs(GridPanel gridPanel,
            GridCell gridCell, object oldValue, object newValue, DataContext context)
            : base(gridPanel, gridCell)
        {
            _OldValue = oldValue;
            _NewValue = newValue;

            _DataContext = context;
        }

        #region Public properties

        ///<summary>
        /// Gets the context under which
        /// the call value was changed
        ///</summary>
        public DataContext DataContext
        {
            get { return (_DataContext); }
        }      

        ///<summary>
        /// Gets the old cell Value
        ///</summary>
        public object OldValue
        {
            get { return (_OldValue); }
        }

        ///<summary>
        /// Gets the new cell Value
        ///</summary>
        public object NewValue
        {
            get { return (_NewValue); }
        }      

        #endregion
    }

    #endregion

    #region GridColumnEventArgs

    /// <summary>
    /// GridColumnEventArgs
    /// </summary>
    public class GridColumnEventArgs : EventArgs
    {
        #region Private variables

        private GridPanel _GridPanel;
        private GridColumn _GridColumn;

        #endregion

        ///<summary>
        /// GridColumnEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        public GridColumnEventArgs(GridPanel gridPanel, GridColumn gridColumn)
        {
            _GridPanel = gridPanel;
            _GridColumn = gridColumn;
        }

        #region Public properties

        /// <summary>
        /// Gets th associated GridPanel
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        /// <summary>
        /// Gets the associated GridColumn
        /// </summary>
        public GridColumn GridColumn
        {
            get { return (_GridColumn); }
        }

        #endregion
    }

    #endregion

    #region GridColumnHeaderClickEventArgs

    /// <summary>
    /// GridColumnHeaderClickEventArgs
    /// </summary>
    public class GridColumnHeaderClickEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private MouseEventArgs _MouseEventArgs;

        #endregion

        ///<summary>
        /// GridColumnHeaderClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="e"></param>
        public GridColumnHeaderClickEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, MouseEventArgs e)
            : base(gridPanel, gridColumn)
        {
            _MouseEventArgs = e;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated MouseEventArgs
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return (_MouseEventArgs); }
        }

        #endregion
    }

    #endregion

    #region GridColumnHeaderMarkupLinkClickEventArgs

    /// <summary>
    /// GridColumnHeaderMarkupLinkClickEventArgs
    /// </summary>
    public class GridColumnHeaderMarkupLinkClickEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private string _HRef;
        private string _Name;

        #endregion

        ///<summary>
        /// GridColumnHeaderMarkupLinkClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="name"></param>
        ///<param name="href"></param>
        public GridColumnHeaderMarkupLinkClickEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, string name, string href)
            : base(gridPanel, gridColumn)
        {
            _HRef = href;
            _Name = name;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated HyperLink HRef
        /// </summary>
        public string HRef
        {
            get { return (_HRef); }
        }

        /// <summary>
        /// Gets the associated HyperLink Name
        /// </summary>
        public string Name
        {
            get { return (_Name); }
        }

        #endregion
    }

    #endregion

    #region GridColumnHeaderDoubleClickEventArgs

    /// <summary>
    /// GridColumnHeaderDoubleClickEventArgs
    /// </summary>
    public class GridColumnHeaderDoubleClickEventArgs : GridColumnHeaderClickEventArgs
    {
        ///<summary>
        /// GridColumnHeaderDoubleClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="e"></param>
        public GridColumnHeaderDoubleClickEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, MouseEventArgs e)
            : base(gridPanel, gridColumn, e)
        {
        }
    }

    #endregion

    #region GridColumnGroupedEventArgs

    /// <summary>
    /// GridColumnGroupedEventArgs
    /// </summary>
    public class GridColumnGroupedEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private GridGroup _GridGroup;

        #endregion

        ///<summary>
        /// GridColumnEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="gridGroup"></param>
        public GridColumnGroupedEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, GridGroup gridGroup)
            : base(gridPanel, gridColumn)
        {
            _GridGroup = gridGroup;
        }

        #region Public properties

        /// <summary>
        /// Gets th associated GridGroup
        /// </summary>
        public GridGroup GridGroup
        {
            get { return (_GridGroup); }
        }

        #endregion
    }

    #endregion

    #region GridCompareElementsEventArgs

    /// <summary>
    /// GridCompareElementsEventArgs
    /// </summary>
    public class GridCompareElementsEventArgs : GridCancelEventArgs
    {
        #region Private variables

        private readonly GridElement _ElementA;
        private readonly GridElement _ElementB;

        private int _Result;

        #endregion

        ///<summary>
        /// GridRowMovingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="a"></param>
        ///<param name="b"></param>
        public GridCompareElementsEventArgs(
            GridPanel gridPanel, GridElement a, GridElement b)
            : base(gridPanel)
        {
            _ElementA = a;
            _ElementB = b;
        }

        #region Public properties

        /// <summary>
        /// Gets the left-hand element of the comparison
        /// </summary>
        public GridElement ElementA
        {
            get { return (_ElementA); }
        }

        /// <summary>
        /// Gets the right-hand element of the comparison
        /// </summary>
        public GridElement ElementB
        {
            get { return (_ElementB); }
        }

        /// <summary>
        /// Gets or sets the result of the element compare.
        /// -1 = ElementA is less than ElementB
        /// 0 = ElementA is equal to ElementB
        /// +1 = ElementA is greater than ElementB
        /// </summary>
        public int Result
        {
            get { return (_Result); }
            set { _Result = value; }
        }

        #endregion
    }

    #endregion

    #region GridDataBindingStartEventArgs

    /// <summary>
    /// GridDataBindingStartEventArgs
    /// </summary>
    public class GridDataBindingStartEventArgs : GridRowCancelEventArgs
    {
        #region Private variables

        private string _TableName;
        private bool _AutoGenerateColumns;
        private ProcessChildRelations _ProcessChildRelations;

        #endregion

        ///<summary>
        /// GridDataBindingStartEventArgs
        ///</summary>
        ///<param name="gridPanel">Associated GridPanel</param>
        ///<param name="row">Associated GridRow</param>
        ///<param name="tableName">Name of table being bound to</param>
        ///<param name="autoGenerateColumns">Whether to auto-generate columns</param>
        ///<param name="crProcess"></param>
        public GridDataBindingStartEventArgs(GridPanel gridPanel,
            GridRow row, string tableName, bool autoGenerateColumns, ProcessChildRelations crProcess)
            : base(gridPanel, row)
        {
            _TableName = tableName;
            _AutoGenerateColumns = autoGenerateColumns;
            _ProcessChildRelations = crProcess;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to auto-generate
        /// the nested table columns
        /// </summary>
        public bool AutoGenerateColumns
        {
            get { return (_AutoGenerateColumns); }
            set { _AutoGenerateColumns = value; }
        }

        /// <summary>
        /// Gets or sets how Child Relations
        /// are processed by the SuperGrid
        /// </summary>
        public ProcessChildRelations ProcessChildRelations
        {
            get { return (_ProcessChildRelations); }
            set { _ProcessChildRelations = value; }
        }

        /// <summary>
        /// Gets the nested table name being bound
        /// </summary>
        public string TableName
        {
            get { return (_TableName); }
        }

        #endregion
    }

    #endregion

    #region GridDataBindingCompleteEventArgs

    /// <summary>
    /// GridDataBindingCompleteEventArgs
    /// </summary>
    public class GridDataBindingCompleteEventArgs : GridEventArgs
    {
        #region Private variables

        private ListChangedType _ListChangedType;

        #endregion

        ///<summary>
        /// GridDataBindingCompleteEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        public GridDataBindingCompleteEventArgs(GridPanel gridPanel)
            : base(gridPanel)
        {
            _ListChangedType = ListChangedType.Reset;
        }

        #region Public properties

        /// <summary>
        /// Gets how the list was changed
        /// </summary>
        public ListChangedType ListChangedType
        {
            get { return (_ListChangedType); }
        }

        #endregion
    }

    #endregion

    #region GridDataErrorEventArgs

    /// <summary>
    /// GridDataErrorEventArgs
    /// </summary>
    public class GridDataErrorEventArgs : CancelEventArgs
    {
        #region Private variables

        private GridPanel _GridPanel;
        private GridCell _GridCell;
        private Exception _Exception;
        private DataContext _ErrorContext;

        private object _Value;

        private bool _Retry;
        private bool _ThrowException;

        #endregion

        ///<summary>
        /// GridDataErrorEventArgs
        ///</summary>
        public GridDataErrorEventArgs(
            GridPanel gridPanel, GridCell gridCell,
            Exception exception, DataContext context, object value)
        {
            _GridPanel = gridPanel;
            _GridCell = gridCell;
            _Exception = exception;
            _ErrorContext = context;
            _Value = value;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridPanel
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        /// <summary>
        /// Gets the associated cell
        /// </summary>
        public GridCell GridCell
        {
            get { return (_GridCell); }
        }

        /// <summary>
        /// Gets the context under which the Exception was thrown
        /// </summary>
        public DataContext ErrorContext
        {
            get { return (_ErrorContext); }
        }

        /// <summary>
        /// Gets the Exception that was thrown
        /// </summary>
        public Exception Exception
        {
            get { return (_Exception); }
        }

        /// <summary>
        /// Gets or sets whether the grid should retry the operation
        /// </summary>
        public bool Retry
        {
            get { return (_Retry); }
            set { _Retry = value; }
        }

        /// <summary>
        /// Gets whether the exception should be re-thrown by the grid
        /// </summary>
        public bool ThrowException
        {
            get { return (_ThrowException); }
            set { _ThrowException = value; }
        }

        /// <summary>
        /// Gets the value that caused the Exception
        /// </summary>
        public object Value
        {
            get { return (_Value); }
            set { _Value = value; }
        }

        #endregion
    }

    #endregion

    #region GridDataFilteringStartEventArgs

    /// <summary>
    /// GridDataFilteringStartEventArgs
    /// </summary>
    public class GridDataFilteringStartEventArgs : GridCancelEventArgs
    {
        ///<summary>
        /// GridDataFilteringStartEventArgs
        ///</summary>
        ///<param name="gridPanel">Associated GridPanel</param>
        public GridDataFilteringStartEventArgs(GridPanel gridPanel)
            : base(gridPanel)
        {
        }
    }

    #endregion

    #region GridDataFilteringCompleteEventArgs

    /// <summary>
    /// GridDataFilteringCompleteEventArgs
    /// </summary>
    public class GridDataFilteringCompleteEventArgs : GridEventArgs
    {
        ///<summary>
        /// GridDataFilteringCompleteEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        public GridDataFilteringCompleteEventArgs(GridPanel gridPanel)
            : base(gridPanel)
        {
        }
    }

    #endregion

    #region GridFilterBeginEditEventArgs

    /// <summary>
    /// GridFilterBeginEditEventArgs
    /// </summary>
    public class GridFilterBeginEditEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridFilterBeginEditEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        public GridFilterBeginEditEventArgs(GridPanel gridPanel, GridColumn gridColumn)
            : base(gridPanel, gridColumn)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridFilterCancelEditEventArgs

    /// <summary>
    /// GridFilterCancelEditEventArgs
    /// </summary>
    public class GridFilterCancelEditEventArgs : GridFilterEventArgs
    {
        ///<summary>
        /// GridFilterCancelEditEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        public GridFilterCancelEditEventArgs(GridPanel gridPanel, GridColumn gridColumn)
            : base(gridPanel, gridColumn)
        {
        }
    }

    #endregion

    #region GridFilterColumnErrorEventArgs

    /// <summary>
    /// GridFilterColumnErrorEventArgs
    /// </summary>
    public class GridFilterColumnErrorEventArgs : GridFilterRowErrorEventArgs
    {
        #region Private variables

        private GridColumn _GridColumn;

        #endregion

        ///<summary>
        /// GridFilterColumnErrorEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="gridColumn"></param>
        ///<param name="exp"></param>
        ///<param name="filteredOut"></param>
        ///<param name="throwException"></param>
        public GridFilterColumnErrorEventArgs(GridPanel gridPanel, GridRow gridRow,
            GridColumn gridColumn, Exception exp, ref bool filteredOut, ref bool throwException)
            : base(gridPanel, gridRow, exp, ref filteredOut, ref throwException)
        {
            _GridColumn = gridColumn;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridColumn
        /// </summary>
        public GridColumn GridColumn
        {
            get { return (_GridColumn); }
        }

        #endregion
    }

    #endregion

    #region GridFilterEditValueChangedEventArgs

    /// <summary>
    /// GridFilterEditValueChangedEventArgs
    /// </summary>
    public class GridFilterEditValueChangedEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private object _OldValue;

        private object _NewValue;
        private object _NewDisplayValue;
        private string _NewExpr;

        private FilterPanel _FilterPanel;

        private bool _Cancel;

        #endregion

        ///<summary>
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="fp"></param>
        ///<param name="oldValue"></param>
        ///<param name="newValue"></param>
        ///<param name="newDisplayValue"></param>
        ///<param name="newExpr"></param>
        public GridFilterEditValueChangedEventArgs(GridPanel gridPanel, GridColumn gridColumn,
            FilterPanel fp, object oldValue, object newValue, object newDisplayValue, string newExpr)
            : base(gridPanel, gridColumn)
        {
            _FilterPanel = fp;

            _OldValue = oldValue;

            _NewValue = newValue;
            _NewDisplayValue = newDisplayValue;
            _NewExpr = newExpr;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        ///<summary>
        /// Gets the associated FilterPanel
        ///</summary>
        public FilterPanel FilterPanel
        {
            get { return (_FilterPanel); }
        }

        ///<summary>
        /// Gets the old filter value
        ///</summary>
        public object OldValue
        {
            get { return (_OldValue); }
        }

        ///<summary>
        /// Gets or sets the new filter value
        ///</summary>
        public object NewValue
        {
            get { return (_NewValue); }
            set { _NewValue = value; }
        }

        ///<summary>
        /// Gets or sets the new filter display value
        ///</summary>
        public object NewDisplayValue
        {
            get { return (_NewDisplayValue); }
            set { _NewDisplayValue = value; }
        }

        ///<summary>
        /// Gets or sets the new filter expression
        ///</summary>
        public string NewExpr
        {
            get { return (_NewExpr); }
            set { _NewExpr = value; }
        }

        #endregion
    }

    #endregion

    #region GridFilterEndEditEventArgs

    /// <summary>
    /// GridFilterEndEditEventArgs
    /// </summary>
    public class GridFilterEndEditEventArgs : GridFilterEventArgs
    {
        ///<summary>
        /// GridFilterEndEditEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        public GridFilterEndEditEventArgs(GridPanel gridPanel, GridColumn gridColumn)
            : base(gridPanel, gridColumn)
        {
        }
    }

    #endregion

    #region GridFilterEventArgs

    /// <summary>
    /// GridFilterEventArgs
    /// </summary>
    public class GridFilterEventArgs : GridColumnEventArgs
    {
        ///<summary>
        /// GridFilterEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        public GridFilterEventArgs(GridPanel gridPanel, GridColumn gridColumn)
            : base(gridPanel, gridColumn)
        {
        }
    }

    #endregion

    #region GridFilterHeaderClickEventArgs

    /// <summary>
    /// GridFilterHeaderClickEventArgs
    /// </summary>
    public class GridFilterHeaderClickEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private MouseEventArgs _MouseEventArgs;

        #endregion

        ///<summary>
        /// GridFilterHeaderClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="e"></param>
        public GridFilterHeaderClickEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, MouseEventArgs e)
            : base(gridPanel, gridColumn)
        {
            _MouseEventArgs = e;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated MouseEventArgs
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return (_MouseEventArgs); }
        }

        #endregion
    }

    #endregion

    #region GridFilterHelpClosingEventArgs

    /// <summary>
    /// GridFilterHelpClosingEventArgs
    /// </summary>
    public class GridFilterHelpClosingEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private SampleExpr _SampleExpr;

        #endregion

        ///<summary>
        /// GridFilterHelpClosingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="sampleExpr"></param>
        public GridFilterHelpClosingEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, SampleExpr sampleExpr)
            : base(gridPanel, gridColumn)
        {
            _SampleExpr = sampleExpr;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated Sample Expression help window
        /// </summary>
        public SampleExpr SampleExpr
        {
            get { return (_SampleExpr); }
        }

        #endregion
    }

    #endregion

    #region GridFilterHelpOpeningEventArgs

    /// <summary>
    /// GridFilterHelpOpeningEventArgs
    /// </summary>
    public class GridFilterHelpOpeningEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private SampleExpr _SampleExpr;

        #endregion

        ///<summary>
        /// GridFilterHelpOpeningEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="sampleExpr"></param>
        public GridFilterHelpOpeningEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, SampleExpr sampleExpr)
            : base(gridPanel, gridColumn)
        {
            _SampleExpr = sampleExpr;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated Sample Expression help window
        /// </summary>
        public SampleExpr SampleExpr
        {
            get { return (_SampleExpr); }
        }

        #endregion
    }

    #endregion

    #region GridFilterItemsLoadedEventArgs

    /// <summary>
    /// GridFilterItemsLoadedEventArgs
    /// </summary>
    public class GridFilterItemsLoadedEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private ComboBoxEx _ComboBox;

        #endregion

        ///<summary>
        /// GridFilterItemsLoadedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="comboBox"></param>
        public GridFilterItemsLoadedEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, ComboBoxEx comboBox)
            : base(gridPanel, gridColumn)
        {
            _ComboBox = comboBox;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated ComboBoxEx
        /// </summary>
        public ComboBoxEx ComboBox
        {
            get { return (_ComboBox); }
        }

        #endregion
    }

    #endregion

    #region GridFilterLoadItemsEventArgs

    /// <summary>
    /// GridFilterLoadItemsEventArgs
    /// </summary>
    public class GridFilterLoadItemsEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private ComboBoxEx _ComboBox;

        #endregion

        ///<summary>
        /// GridFilterBeginEditEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="comboBox"></param>
        public GridFilterLoadItemsEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, ComboBoxEx comboBox)
            : base(gridPanel, gridColumn)
        {
            _ComboBox = comboBox;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated ComboBoxEx
        /// </summary>
        public ComboBoxEx ComboBox
        {
            get { return (_ComboBox); }
        }

        #endregion

    }

    #endregion

    #region GridFilterLoadUserDataEventArgs

    /// <summary>
    /// GridFilterLoadUserDataEventArgs
    /// </summary>
    public class GridFilterLoadUserDataEventArgs : GridCancelEventArgs
    {
        #region Private variables

        private string _FilterPath;
        private List<UserFilterData> _FilterData;

        #endregion

        ///<summary>
        /// GridFilterLoadUserDataEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="filterPath"></param>
        ///<param name="filterData"></param>
        public GridFilterLoadUserDataEventArgs(
            GridPanel gridPanel, string filterPath, List<UserFilterData> filterData)
            : base(gridPanel)
        {
            _FilterPath = filterPath;
            _FilterData = filterData;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated FilterData (user defined
        /// Custom filter expressions)
        /// </summary>
        public List<UserFilterData> FilterData
        {
            get { return (_FilterData); }
            set { _FilterData = value; }
        }

        /// <summary>
        /// Gets or sets the associated Path to
        /// the user defined filter data expression file
        /// </summary>
        public string FilterPath
        {
            get { return (_FilterPath); }
            set { _FilterPath = value; }
        }

        #endregion
    }

    #endregion

    #region GridFilterPopupClosingEventArgs

    /// <summary>
    /// GridFilterPopupClosingEventArgs
    /// </summary>
    public class GridFilterPopupClosingEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private FilterPopup _FilterPopup;

        #endregion

        ///<summary>
        /// GridFilterPopupClosingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="filterPopup"></param>
        public GridFilterPopupClosingEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, FilterPopup filterPopup)
            : base(gridPanel, gridColumn)
        {
            _FilterPopup = filterPopup;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated filter popup
        /// </summary>
        public FilterPopup FilterPopup
        {
            get { return (_FilterPopup); }
        }

        #endregion
    }

    #endregion

    #region GridFilterPopupLoadEventArgs

    /// <summary>
    /// GridFilterPopupLoadEventArgs
    /// </summary>
    public class GridFilterPopupLoadEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private bool _Cancel;

        private FilterPopup _FilterPopup;

        #endregion

        ///<summary>
        /// GridFilterPopupLoadEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="filterPopup"></param>
        public GridFilterPopupLoadEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, FilterPopup filterPopup)
            : base(gridPanel, gridColumn)
        {
            _FilterPopup = filterPopup;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated filter popup
        /// </summary>
        public FilterPopup FilterPopup
        {
            get { return (_FilterPopup); }
        }

        #endregion
    }

    #endregion

    #region GridFilterPopupLoadedEventArgs

    /// <summary>
    /// GridFilterPopupLoadedEventArgs
    /// </summary>
    public class GridFilterPopupLoadedEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private FilterPopup _FilterPopup;

        #endregion

        ///<summary>
        /// GridFilterPopupLoadedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="filterPopup"></param>
        public GridFilterPopupLoadedEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, FilterPopup filterPopup)
            : base(gridPanel, gridColumn)
        {
            _FilterPopup = filterPopup;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated filter popup
        /// </summary>
        public FilterPopup FilterPopup
        {
            get { return (_FilterPopup); }
        }

        #endregion
    }

    #endregion

    #region GridFilterPopupOpeningEventArgs

    /// <summary>
    /// GridFilterPopupOpeningEventArgs
    /// </summary>
    public class GridFilterPopupOpeningEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private bool _Cancel;

        private FilterPopup _FilterPopup;

        #endregion

        ///<summary>
        /// GridFilterPopupOpeningEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="filterPopup"></param>
        public GridFilterPopupOpeningEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, FilterPopup filterPopup)
            : base(gridPanel, gridColumn)
        {
            _FilterPopup = filterPopup;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated filter popup
        /// </summary>
        public FilterPopup FilterPopup
        {
            get { return (_FilterPopup); }
        }

        #endregion
    }

    #endregion

    #region GridFilterPopupValueChangedEventArgs

    /// <summary>
    /// GridFilterPopupValueChangedEventArgs
    /// </summary>
    public class GridFilterPopupValueChangedEventArgs : GridFilterEventArgs
    {
        #region Private variables

        private object _FilterValue;
        private object _FilterDisplayValue;
        private string _FilterExpr;

        private FilterItemType _FilterItemType;

        private bool _Cancel;

        #endregion

        ///<summary>
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="filterItemType"></param>
        ///<param name="filterValue"></param>
        ///<param name="filterDisplayValue"></param>
        ///<param name="filterExpr"></param>
        public GridFilterPopupValueChangedEventArgs(GridPanel gridPanel, GridColumn gridColumn,
            FilterItemType filterItemType, object filterValue, object filterDisplayValue, string filterExpr)
            : base(gridPanel, gridColumn)
        {
            _FilterItemType = filterItemType;
            _FilterValue = filterValue;
            _FilterDisplayValue = filterDisplayValue;
            _FilterExpr = filterExpr;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        ///<summary>
        /// Gets or sets the new filter value
        ///</summary>
        public object FilterValue
        {
            get { return (_FilterValue); }
            set { _FilterValue = value; }
        }

        ///<summary>
        /// Gets or sets the new filter display value
        ///</summary>
        public object FilterDisplayValue
        {
            get { return (_FilterDisplayValue); }
            set { _FilterDisplayValue = value; }
        }

        ///<summary>
        /// Gets or sets the new filter expression
        ///</summary>
        public string FilterExpr
        {
            get { return (_FilterExpr); }
            set { _FilterExpr = value; }
        }

        ///<summary>
        /// Gets the filter popup item selected
        ///</summary>
        public FilterItemType FilterItemType
        {
            get { return (_FilterItemType); }
        }

        #endregion
    }

    #endregion

    #region GridFilterRowErrorEventArgs

    /// <summary>
    /// GridFilterRowErrorEventArgs
    /// </summary>
    public class GridFilterRowErrorEventArgs : GridRowEventArgs
    {
        #region Private variables

        private bool _Cancel;

        private bool _FilteredOut;
        private bool _ThrowException;

        private Exception _Exception;

        #endregion

        ///<summary>
        /// GridFilterRowErrorEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="exp"></param>
        ///<param name="filteredOut"></param>
        ///<param name="throwException"></param>
        public GridFilterRowErrorEventArgs(GridPanel gridPanel,
            GridRow gridRow, Exception exp, ref bool filteredOut, ref bool throwException)
            : base(gridPanel, gridRow)
        {
            _Exception = exp;
            _FilteredOut = filteredOut;
            _ThrowException = throwException;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated filter Exception
        /// </summary>
        public Exception Exception
        {
            get { return (_Exception); }
        }

        /// <summary>
        /// Gets or sets whether the row is filtered out
        /// </summary>
        public bool FilteredOut
        {
            get { return (_FilteredOut); }
            set { _FilteredOut = value; }
        }

        /// <summary>
        /// Gets or sets whether to re-throw the exception
        /// </summary>
        public bool ThrowException
        {
            get { return (_ThrowException); }
            set { _ThrowException = value; }
        }

        #endregion
    }

    #endregion

    #region GridFilterStoreUserDataEventArgs

    /// <summary>
    /// GridFilterStoreUserDataEventArgs
    /// </summary>
    public class GridFilterStoreUserDataEventArgs : GridCancelEventArgs
    {
        #region Private variables

        private string _FilterPath;
        private List<UserFilterData> _FilterData;

        #endregion

        ///<summary>
        /// GridFilterStoreUserDataEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="filterPath"></param>
        ///<param name="filterData"></param>
        public GridFilterStoreUserDataEventArgs(
            GridPanel gridPanel, string filterPath, List<UserFilterData> filterData)
            : base(gridPanel)
        {
            _FilterPath = filterPath;
            _FilterData = filterData;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated FilterData (user defined
        /// Custom filter expressions)
        /// </summary>
        public List<UserFilterData> FilterData
        {
            get { return (_FilterData); }
            set { _FilterData = value; }
        }

        /// <summary>
        /// Gets or sets the associated Path to
        /// the user defined filter data expression file
        /// </summary>
        public string FilterPath
        {
            get { return (_FilterPath); }
            set { _FilterPath = value; }
        }

        #endregion
    }

    #endregion

    #region GridFilterUserFunctionEventArgs

    /// <summary>
    /// GridFilterUserFunctionEventArgs
    /// </summary>
    public class GridFilterUserFunctionEventArgs : GridRowEventArgs
    {
        #region Private variables

        private object[] _Args;
        private object _Result;

        private bool _Handled;

        #endregion

        ///<summary>
        /// GridFilterUserFunctionEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="args"></param>
        ///<param name="result"></param>
        public GridFilterUserFunctionEventArgs(
            GridPanel gridPanel, GridRow gridRow, object[] args, object result)
            : base(gridPanel, gridRow)
        {
            _Args = args;
            _Result = result;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated function arguments
        /// </summary>
        public object[] Args
        {
            get { return (_Args); }
            set { _Args = value; }
        }

        /// <summary>
        /// Gets or sets whether the function was handled
        /// </summary>
        public bool Handled
        {
            get { return (_Handled); }
            set { _Handled = value; }
        }

        /// <summary>
        /// Gets or sets the associated function result
        /// </summary>
        public object Result
        {
            get { return (_Result); }
            set { _Result = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetGroupDetailRowsEventArgs

    /// <summary>
    /// GridGetGroupDetailRowsEventArgs
    /// </summary>
    public class GridGetGroupDetailRowsEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private GridGroup _GridGroup;
        private List<GridRow> _PreDetailRows;
        private List<GridRow> _PostDetailRows;

        #endregion

        ///<summary>
        /// GridGetGroupDetailRowsEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="gridGroup"></param>
        public GridGetGroupDetailRowsEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, GridGroup gridGroup)
            : base(gridPanel, gridColumn)
        {
            _GridGroup = gridGroup;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the list of Post grouping DetailRows
        /// </summary>
        [Obsolete]
        public List<GridRow> DetailRows
        {
            get { return (_PostDetailRows); }
            set { _PostDetailRows = value; }
        }

        /// <summary>
        /// Gets or sets the list of Post grouping DetailRows
        /// </summary>
        public List<GridRow> PostDetailRows
        {
            get { return (_PostDetailRows); }
            set { _PostDetailRows = value; }
        }

        /// <summary>
        /// Gets or sets the list of Pre grouping DetailRows
        /// </summary>
        public List<GridRow> PreDetailRows
        {
            get { return (_PreDetailRows); }
            set { _PreDetailRows = value; }
        }

        /// <summary>
        /// Gets the associated GridGroup
        /// </summary>
        public GridGroup GridGroup
        {
            get { return (_GridGroup); }
        }

        #endregion
    }

    #endregion

    #region GridItemDragEventArgs

    /// <summary>
    /// GridItemDragEventArgs
    /// </summary>
    public class GridItemDragEventArgs : ItemDragEventArgs
    {
        #region Private variables

        private GridPanel _GridPanel;
        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridDragStartedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="item"></param>
        ///<param name="e"></param>
        public GridItemDragEventArgs(GridPanel gridPanel, GridElement item, MouseEventArgs e)
            : base(e.Button, item)
        {
            _GridPanel = gridPanel;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated GridPanel
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        #endregion
    }

    #endregion

    #region GridGetCellFormattedValueEventArgs

    /// <summary>
    /// GridGetCellFormattedValueEventArgs
    /// </summary>
    public class GridGetCellFormattedValueEventArgs : GridCellEventArgs
    {
        #region Private variables

        private string _FormattedValue;

        #endregion

        ///<summary>
        /// GridGetCellStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="formattedValue"></param>
        public GridGetCellFormattedValueEventArgs(
            GridPanel gridPanel, GridCell gridCell, string formattedValue)
            : base(gridPanel, gridCell)
        {
            _FormattedValue = formattedValue;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated FormattedValue
        /// </summary>
        public string FormattedValue
        {
            get { return (_FormattedValue); }
            set { _FormattedValue = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetCellStyleEventArgs

    /// <summary>
    /// GridGetCellStyleEventArgs
    /// </summary>
    public class GridGetCellStyleEventArgs : GridCellEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private CellVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetCellStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetCellStyleEventArgs(
            GridPanel gridPanel, GridCell gridCell, StyleType styleType, CellVisualStyle style)
            : base(gridPanel, gridCell)
        {
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public CellVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetCellValueEventArgs

    /// <summary>
    /// GridGetCellValueEventArgs
    /// </summary>
    public class GridGetCellValueEventArgs : GridCellEventArgs
    {
        #region Private variables

        private object _Value;

        #endregion

        ///<summary>
        /// GridGetCellValueEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="value"></param>
        public GridGetCellValueEventArgs(
            GridPanel gridPanel, GridCell gridCell, object value)
            : base(gridPanel, gridCell)
        {
            _Value = value;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated Value
        /// </summary>
        public object Value
        {
            get { return (_Value); }
            set { _Value = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetColumnHeaderRowHeaderStyleEventArgs

    /// <summary>
    /// GridGetColumnHeaderRowHeaderStyleEventArgs
    /// </summary>
    public class GridGetColumnHeaderRowHeaderStyleEventArgs : GridEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private GridColumnHeader _ColumnHeader;
        private ColumnHeaderRowVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetColumnHeaderRowHeaderStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="columnHeader"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetColumnHeaderRowHeaderStyleEventArgs(GridPanel gridPanel,
            GridColumnHeader columnHeader, StyleType styleType, ColumnHeaderRowVisualStyle style)
            : base(gridPanel)
        {
            _ColumnHeader = columnHeader;
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated ColumnHeader
        /// </summary>
        public GridColumnHeader ColumnHeader
        {
            get { return (_ColumnHeader); }
        }

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public ColumnHeaderRowVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetColumnHeaderStyleEventArgs

    /// <summary>
    /// GridGetColumnHeaderStyleEventArgs
    /// </summary>
    public class GridGetColumnHeaderStyleEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private ColumnHeaderVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetColumnHeaderStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetColumnHeaderStyleEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, StyleType styleType, ColumnHeaderVisualStyle style)
            : base(gridPanel, gridColumn)
        {
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public ColumnHeaderVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetColumnHeaderToolTipEventArgs

    /// <summary>
    /// GridGetColumnHeaderToolTipEventArgs
    /// </summary>
    public class GridGetColumnHeaderToolTipEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private string _ToolTip;
        private HeaderArea _HitArea;

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridGetColumnHeaderToolTipEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="hitArea"></param>
        ///<param name="toolTip"></param>
        public GridGetColumnHeaderToolTipEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, HeaderArea hitArea, string toolTip)
            : base(gridPanel, gridColumn)
        {
            _HitArea = hitArea;
            _ToolTip = toolTip;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated Header hit area
        /// </summary>
        public HeaderArea HitArea
        {
            get { return (_HitArea); }
        }

        /// <summary>
        /// Gets or sets the associated ToolTip text
        /// </summary>
        public string ToolTip
        {
            get { return (_ToolTip); }
            set { _ToolTip = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetFilterRowStyleEventArgs

    /// <summary>
    /// GridGetFilterRowStyleEventArgs
    /// </summary>
    public class GridGetFilterRowStyleEventArgs : GridEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private GridFilter _GridFilter;
        private FilterRowVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetFilterRowStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridFilter"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetFilterRowStyleEventArgs(GridPanel gridPanel,
            GridFilter gridFilter, StyleType styleType, FilterRowVisualStyle style)
            : base(gridPanel)
        {
            _GridFilter = gridFilter;
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridFilter
        /// </summary>
        public GridFilter GridFilter
        {
            get { return (_GridFilter); }
        }

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public FilterRowVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetFilterColumnHeaderStyleEventArgs

    /// <summary>
    /// GridGetFilterColumnHeaderStyleEventArgs
    /// </summary>
    public class GridGetFilterColumnHeaderStyleEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private FilterColumnHeaderVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetFilterColumnHeaderStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetFilterColumnHeaderStyleEventArgs(GridPanel gridPanel,
            GridColumn gridColumn, StyleType styleType, FilterColumnHeaderVisualStyle style)
            : base(gridPanel, gridColumn)
        {
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public FilterColumnHeaderVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridConfigureGroupBoxEventArgs

    /// <summary>
    /// GridConfigureGroupBoxEventArgs
    /// </summary>
    public class GridConfigureGroupBoxEventArgs : GridEventArgs
    {
        #region Private variables

        private GridGroupByRow _GroupByRow;
        private GridGroupBox _GridGroupBox;

        #endregion

        ///<summary>
        /// GridConfigureGroupBoxEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="groupByRow"></param>
        ///<param name="gridGroupBox"></param>
        public GridConfigureGroupBoxEventArgs(GridPanel gridPanel,
            GridGroupByRow groupByRow, GridGroupBox gridGroupBox)
            : base(gridPanel)
        {
            _GroupByRow = groupByRow;
            _GridGroupBox = gridGroupBox;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GroupByRow
        /// </summary>
        public GridGroupByRow GroupByRow
        {
            get { return (_GroupByRow); }
        }

        /// <summary>
        /// Gets the associated GridColumn
        /// </summary>
        public GridGroupBox GridGroupBox
        {
            get { return (_GridGroupBox); }
        }

        #endregion
    }

    #endregion

    #region GridGetGroupHeaderStyleEventArgs

    /// <summary>
    /// GridGetGroupHeaderStyleEventArgs
    /// </summary>
    public class GridGetGroupHeaderStyleEventArgs : GridEventArgs
    {
        #region Private variables

        private GridGroup _GridRow;
        private StyleType _StyleType;

        private GroupHeaderVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetGroupHeaderStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetGroupHeaderStyleEventArgs(
            GridPanel gridPanel, GridGroup gridRow, StyleType styleType, GroupHeaderVisualStyle style)
            : base(gridPanel)
        {
            _GridRow = gridRow;
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets the associated Group Header Row
        /// </summary>
        public GridGroup GridRow
        {
            get { return (_GridRow); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public GroupHeaderVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetPanelStyleEventArgs

    /// <summary>
    /// GridGetPanelStyleEventArgs
    /// </summary>
    public class GridGetPanelStyleEventArgs : GridEventArgs
    {
        #region Private variables

        private GridPanelVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetPanelStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="style"></param>
        public GridGetPanelStyleEventArgs(GridPanel gridPanel, GridPanelVisualStyle style)
            : base(gridPanel)
        {
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public GridPanelVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetRowHeaderStyleEventArgs

    /// <summary>
    /// GridGetRowHeaderStyleEventArgs
    /// </summary>
    public class GridGetRowHeaderStyleEventArgs : GridRowEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private RowHeaderVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetRowHeaderStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="row"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetRowHeaderStyleEventArgs(
            GridPanel gridPanel, GridContainer row, StyleType styleType, RowHeaderVisualStyle style)
            : base(gridPanel, row)
        {
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public RowHeaderVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetRowHeaderTextEventArgs

    /// <summary>
    /// GridGetRowHeaderTextEventArgs
    /// </summary>
    public class GridGetRowHeaderTextEventArgs : GridRowEventArgs
    {
        #region Private variables

        private string _Text;

        #endregion

        ///<summary>
        /// GridGetRowHeaderTextEventArgs
        ///</summary>
        ///<param name="gridPanel">Associated GridPanel</param>
        ///<param name="row">Associated container row</param>
        ///<param name="text">Text to display in row header</param>
        public GridGetRowHeaderTextEventArgs(
            GridPanel gridPanel, GridContainer row, string text)
            : base(gridPanel, row)
        {
            _Text = text;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the associated header text
        /// </summary>
        public string Text
        {
            get { return (_Text); }
            set { _Text = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetRowStyleEventArgs

    /// <summary>
    /// GridGetRowStyleEventArgs
    /// </summary>
    public class GridGetRowStyleEventArgs : GridRowEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private RowVisualStyle _Style;

        #endregion

        ///<summary>
        /// GridGetCellStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetRowStyleEventArgs(
            GridPanel gridPanel, GridContainer gridRow, StyleType styleType, RowVisualStyle style)
            : base(gridPanel, gridRow)
        {
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public RowVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        #endregion
    }

    #endregion

    #region GridGetTextRowStyleEventArgs

    /// <summary>
    /// GridGetTextRowStyleEventArgs
    /// </summary>
    public class GridGetTextRowStyleEventArgs : GridEventArgs
    {
        #region Private variables

        private StyleType _StyleType;
        private TextRowVisualStyle _Style;
        private readonly GridTextRow _GridTextRow;

        #endregion

        ///<summary>
        /// GridGetTextRowStyleEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridTextRow"></param>
        ///<param name="styleType"></param>
        ///<param name="style"></param>
        public GridGetTextRowStyleEventArgs(
            GridPanel gridPanel, GridTextRow gridTextRow, StyleType styleType, TextRowVisualStyle style)
            : base(gridPanel)
        {
            _GridTextRow = gridTextRow;
            _StyleType = styleType;
            _Style = style;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated StyleType
        /// </summary>
        public StyleType StyleType
        {
            get { return (_StyleType); }
        }

        /// <summary>
        /// Gets or sets the associated VisualStyle
        /// </summary>
        public TextRowVisualStyle Style
        {
            get { return (_Style); }

            set
            {
                if (value == null)
                    throw new Exception("Style cannot by null.");

                _Style = value;
            }
        }

        /// <summary>
        /// Gets the associated GridTextRow
        /// </summary>
        public GridTextRow GridTextRow
        {
            get { return (_GridTextRow); }
        }

        #endregion
    }

    #endregion

    #region GridEditEventArgs

    /// <summary>
    /// GridEditEventArgs
    /// </summary>
    public class GridEditEventArgs : CancelEventArgs
    {
        #region Private variables

        private GridPanel _GridPanel;
        private GridCell _GridCell;
        private IGridCellEditControl _EditControl;

        #endregion

        ///<summary>
        /// GridEditEventArgs
        ///</summary>
        public GridEditEventArgs(
            GridPanel gridPanel, GridCell gridCell, IGridCellEditControl editControl)
        {
            _GridPanel = gridPanel;
            _GridCell = gridCell;
            _EditControl = editControl;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridPanel
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        /// <summary>
        /// Gets the associated cell
        /// </summary>
        public GridCell GridCell
        {
            get { return (_GridCell); }
        }

        /// <summary>
        /// Gets the associated cell EditControl
        /// </summary>
        public IGridCellEditControl EditControl
        {
            get { return (_EditControl); }
        }

        #endregion
    }

    #endregion

    #region GridEventArgs

    /// <summary>
    /// GridEventArgs
    /// </summary>
    public class GridEventArgs : EventArgs
    {
        #region Private variables

        private readonly GridPanel _GridPanel;

        #endregion

        ///<summary>
        /// GridEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        public GridEventArgs(GridPanel gridPanel)
        {
            _GridPanel = gridPanel;
        }

        #region Public properties

        /// <summary>
        /// Gets the event Grid
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        #endregion
    }

    #endregion

    #region GridCancelEventArgs

    /// <summary>
    /// GridCancelEventArgs
    /// </summary>
    public class GridCancelEventArgs : GridEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        public GridCancelEventArgs(GridPanel gridPanel)
            : base(gridPanel)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridCloseEditEventArgs

    /// <summary>
    /// GridCloseEditEventArgs
    /// </summary>
    public class GridCloseEditEventArgs : GridCellEventArgs
    {
        ///<summary>
        /// GridCloseEditEventArgs
        ///</summary>
        public GridCloseEditEventArgs(GridPanel gridPanel, GridCell gridCell)
            : base(gridPanel, gridCell)
        {
        }
    }

    #endregion

    #region GridGetEditorEventArgs

    /// <summary>
    /// GridGetEditorEventArgs
    /// </summary>
    public class GridGetEditorEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridCell _GridCell;
        private Type _EditorType;
        private object[] _EditorParams;

        #endregion

        ///<summary>
        /// GridGetEditorEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="editorType"></param>
        ///<param name="editorParams"></param>
        public GridGetEditorEventArgs(
            GridPanel gridPanel, GridCell gridCell, Type editorType, object[] editorParams)
            : base(gridPanel)
        {
            _GridCell = gridCell;
            _EditorType = editorType;
            _EditorParams = editorParams;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridCell
        /// </summary>
        public GridCell GridCell
        {
            get { return (_GridCell); }
        }

        /// <summary>
        /// Gets or sets the associated cell editor Type
        /// </summary>
        public Type EditorType
        {
            get { return (_EditorType); }
            set { _EditorType = value; }
        }

        /// <summary>
        /// Gets or sets the associated cell editor params
        /// </summary>
        public object[] EditorParams
        {
            get { return (_EditorParams); }
            set { _EditorParams = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetRendererEventArgs

    /// <summary>
    /// GridGetRendererEventArgs
    /// </summary>
    public class GridGetRendererEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridCell _GridCell;
        private Type _RenderType;
        private object[] _RenderParams;

        #endregion

        ///<summary>
        /// GridGetRendererEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="renderType"></param>
        ///<param name="renderParams"></param>
        public GridGetRendererEventArgs(
            GridPanel gridPanel, GridCell gridCell, Type renderType, object[] renderParams)
            : base(gridPanel)
        {
            _GridCell = gridCell;
            _RenderType = renderType;
            _RenderParams = renderParams;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridCell
        /// </summary>
        public GridCell GridCell
        {
            get { return (_GridCell); }
        }

        /// <summary>
        /// Gets or sets the associated cell Render Type
        /// </summary>
        public Type RenderType
        {
            get { return (_RenderType); }
            set { _RenderType = value; }
        }

        /// <summary>
        /// Gets or sets the associated cell Render params
        /// </summary>
        public object[] RenderParams
        {
            get { return (_RenderParams); }
            set { _RenderParams = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetFilterEditTypeEventArgs

    /// <summary>
    /// GridGetFilterEditTypeEventArgs
    /// </summary>
    public class GridGetFilterEditTypeEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private FilterEditType _FilterEditType;

        #endregion

        ///<summary>
        /// GridGetFilterEditTypeEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridColumn"></param>
        ///<param name="filterEditType"></param>
        public GridGetFilterEditTypeEventArgs(
            GridPanel gridPanel, GridColumn gridColumn, FilterEditType filterEditType)
            : base(gridPanel, gridColumn)
        {
            _FilterEditType = filterEditType;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated Filter edit type
        /// </summary>
        public FilterEditType FilterEditType
        {
            get { return (_FilterEditType); }
            set { _FilterEditType = value; }
        }

        #endregion
    }

    #endregion

    #region GridGroupHeaderClickEventArgs

    /// <summary>
    /// GridGroupHeaderClickEventArgs
    /// </summary>
    public class GridGroupHeaderClickEventArgs : GridEventArgs
    {
        #region Private variables

        private GridGroup _GridGroup;

        private bool _Cancel;
        private GroupArea _GroupArea;
        private MouseEventArgs _MouseEventArgs;

        #endregion

        ///<summary>
        /// GridRowClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridGroup"></param>
        ///<param name="area"></param>
        ///<param name="e"></param>
        public GridGroupHeaderClickEventArgs(
            GridPanel gridPanel, GridGroup gridGroup, GroupArea area, MouseEventArgs e)
            : base(gridPanel)
        {
            _GridGroup = gridGroup;
            _GroupArea = area;

            _MouseEventArgs = e;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridGroup
        /// </summary>
        public GridGroup GridGroup
        {
            get { return (_GridGroup); }
        }

        /// <summary>
        /// Gets or sets whether to cancel the default operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated MouseEventArgs
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return (_MouseEventArgs); }
        }

        /// <summary>
        /// Gets the associated GroupArea
        /// </summary>
        public GroupArea GroupArea
        {
            get { return (_GroupArea); }
        }

        #endregion
    }

    #endregion

    #region GridGroupHeaderDoubleClickEventArgs

    /// <summary>
    /// GridGroupHeaderDoubleClickEventArgs
    /// </summary>
    public class GridGroupHeaderDoubleClickEventArgs : GridGroupHeaderClickEventArgs
    {
        ///<summary>
        /// GridRowClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridGroup"></param>
        ///<param name="area"></param>
        ///<param name="e"></param>
        public GridGroupHeaderDoubleClickEventArgs(
            GridPanel gridPanel, GridGroup gridGroup, GroupArea area, MouseEventArgs e)
            : base(gridPanel, gridGroup, area, e)
        {
        }
    }

    #endregion

    #region GridInitEditContextEventArgs

    /// <summary>
    /// GridInitEditContextEventArgs
    /// </summary>
    public class GridInitEditContextEventArgs : GridCellEventArgs
    {
        #region Private variables

        private readonly IGridCellEditControl _EditControl;

        #endregion

        ///<summary>
        /// GridInitEditContextEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="editControl"></param>
        public GridInitEditContextEventArgs(GridPanel gridPanel,
            GridCell gridCell, IGridCellEditControl editControl)
            : base(gridPanel, gridCell)
        {
            _EditControl = editControl;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridCell
        /// </summary>
        public IGridCellEditControl EditControl
        {
            get { return (_EditControl); }
        }

        #endregion
    }

    #endregion

    #region GridNoRowsTextMarkupLinkClickEventArgs

    /// <summary>
    /// GridNoRowsMarkupLinkClickEventArgs
    /// </summary>
    public class GridNoRowsMarkupLinkClickEventArgs : GridEventArgs
    {
        #region Private variables

        private string _HRef;
        private string _Name;

        #endregion

        ///<summary>
        /// GridNoRowsMarkupLinkClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="name"></param>
        ///<param name="href"></param>
        public GridNoRowsMarkupLinkClickEventArgs(
            GridPanel gridPanel, string name, string href)
            : base(gridPanel)
        {
            _HRef = href;
            _Name = name;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated HyperLink HRef
        /// </summary>
        public string HRef
        {
            get { return (_HRef); }
        }

        /// <summary>
        /// Gets the associated HyperLink Name
        /// </summary>
        public string Name
        {
            get { return (_Name); }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderCellEventArgs

    /// <summary>
    /// GridPostRenderCellEventArgs
    /// </summary>
    public class GridPostRenderCellEventArgs : GridCellEventArgs
    {
        #region Private variables

        private Rectangle _Bounds;
        private Graphics _Graphics;
        private RenderParts _RenderParts;

        #endregion

        ///<summary>
        /// GridPostRenderCellEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPostRenderCellEventArgs(Graphics graphics,
            GridPanel gridPanel, GridCell gridCell, RenderParts parts, Rectangle bounds)
            : base(gridPanel, gridCell)
        {
            _Bounds = bounds;
            _Graphics = graphics;
            _RenderParts = parts;
        }

        #region Public properties

        /// <summary>
        /// Gets the cell bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        /// <summary>
        /// Gets the cell parts to render
        /// </summary>
        public RenderParts RenderParts
        {
            get { return (_RenderParts); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderCellEventArgs

    /// <summary>
    /// GridPreRenderCellEventArgs
    /// </summary>
    public class GridPreRenderCellEventArgs : GridPostRenderCellEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderCellEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="gridCell"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPreRenderCellEventArgs(Graphics graphics,
            GridPanel gridPanel, GridCell gridCell, RenderParts parts, Rectangle bounds)
            : base(graphics, gridPanel, gridCell, parts, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderRowEventArgs

    /// <summary>
    /// GridPostRenderRowEventArgs
    /// </summary>
    public class GridPostRenderRowEventArgs : GridRowEventArgs
    {
        #region Private variables

        private Graphics _Graphics;
        private Rectangle _Bounds;
        private RenderParts _RenderParts;

        #endregion

        ///<summary>
        /// GridPostRenderEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPostRenderRowEventArgs(Graphics graphics, GridPanel gridPanel,
            GridContainer gridRow, RenderParts parts, Rectangle bounds)
            : base(gridPanel, gridRow)
        {
            _Graphics = graphics;
            _RenderParts = parts;
            _Bounds = bounds;
        }

        #region Public properties

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        /// <summary>
        /// Gets the bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the parts to render
        /// </summary>
        public RenderParts RenderParts
        {
            get { return (_RenderParts); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderRowEventArgs

    /// <summary>
    /// GridPreRenderRowEventArgs
    /// </summary>
    public class GridPreRenderRowEventArgs : GridPostRenderRowEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderRowEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPreRenderRowEventArgs(Graphics graphics, GridPanel gridPanel,
            GridContainer gridRow, RenderParts parts, Rectangle bounds)
            : base(graphics, gridPanel, gridRow, parts, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderColumnHeaderEventArgs

    /// <summary>
    /// GridPostRenderColumnHeaderEventArgs
    /// </summary>
    public class GridPostRenderColumnHeaderEventArgs : GridEventArgs
    {
        #region Private variables

        private Rectangle _Bounds;
        private Graphics _Graphics;
        private GridColumnHeader _ColumnHeader;
        private GridColumn _GridColumn;
        private RenderParts _RenderParts;

        #endregion

        ///<summary>
        /// GridPostRenderColumnHeaderEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="columnHeader"></param>
        ///<param name="column"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPostRenderColumnHeaderEventArgs(Graphics graphics, GridPanel gridPanel,
            GridColumnHeader columnHeader, GridColumn column, RenderParts parts, Rectangle bounds)
            : base(gridPanel)
        {
            _Bounds = bounds;
            _Graphics = graphics;
            _ColumnHeader = columnHeader;
            _GridColumn = column;
            _RenderParts = parts;
        }

        #region Public properties

        /// <summary>
        /// Gets the cell bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        /// <summary>
        /// Gets the associated GridColumnHeader
        /// </summary>
        public GridColumnHeader ColumnHeader
        {
            get { return (_ColumnHeader); }
        }

        /// <summary>
        /// Gets the associated GridColumn (which can be null
        /// if the ColumnHeader's RowHeader is being rendered)
        /// </summary>
        public GridColumn GridColumn
        {
            get { return (_GridColumn); }
        }

        /// <summary>
        /// Gets the cell parts to render
        /// </summary>
        public RenderParts RenderParts
        {
            get { return (_RenderParts); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderColumnHeaderEventArgs

    /// <summary>
    /// GridPreRenderColumnHeaderEventArgs
    /// </summary>
    public class GridPreRenderColumnHeaderEventArgs : GridPostRenderColumnHeaderEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderColumnHeaderEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="header"></param>
        ///<param name="column"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPreRenderColumnHeaderEventArgs(Graphics graphics, GridPanel gridPanel,
            GridColumnHeader header, GridColumn column, RenderParts parts, Rectangle bounds)
            : base(graphics, gridPanel, header, column, parts, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderFilterPopupGripBarEventArgs

    /// <summary>
    /// GridPostRenderFilterPopupGripBarEventArgs
    /// </summary>
    public class GridPostRenderFilterPopupGripBarEventArgs : GridColumnEventArgs
    {
        #region Private variables

        private Graphics _Graphics;
        private FilterPopup _FilterPopup;
        private Rectangle _Bounds;

        #endregion

        ///<summary>
        /// GridPostRenderFilterPopupGripBarEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="filterPopup"></param>
        ///<param name="gridColumn"></param>
        ///<param name="bounds"></param>
        public GridPostRenderFilterPopupGripBarEventArgs(Graphics graphics,
            FilterPopup filterPopup, GridColumn gridColumn, Rectangle bounds)
            : base(gridColumn.GridPanel, gridColumn)
        {
            _Graphics = graphics;
            _FilterPopup = filterPopup;
            _Bounds = bounds;
        }

        #region Public properties

        /// <summary>
        /// Gets the GripBar bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the FilterPopup object
        /// </summary>
        public FilterPopup FilterPopup
        {
            get { return (_FilterPopup); }
        }

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderFilterPopupGripBarEventArgs

    /// <summary>
    /// GridPreRenderFilterPopupGripBarEventArgs
    /// </summary>
    public class GridPreRenderFilterPopupGripBarEventArgs : GridPostRenderFilterPopupGripBarEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderFilterPopupGripBarEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="filterPopup"></param>
        ///<param name="gridColumn"></param>
        ///<param name="bounds"></param>
        public GridPreRenderFilterPopupGripBarEventArgs(Graphics graphics,
            FilterPopup filterPopup, GridColumn gridColumn, Rectangle bounds)
            : base(graphics, filterPopup, gridColumn, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderFilterRowEventArgs

    /// <summary>
    /// GridPostRenderFilterRowEventArgs
    /// </summary>
    public class GridPostRenderFilterRowEventArgs : GridEventArgs
    {
        #region Private variables

        private Rectangle _Bounds;
        private Graphics _Graphics;
        private GridFilter _GridFilter;
        private GridColumn _GridColumn;
        private RenderParts _RenderParts;

        #endregion

        ///<summary>
        /// GridPostRenderFilterRowEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="filter"></param>
        ///<param name="column"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPostRenderFilterRowEventArgs(Graphics graphics, GridPanel gridPanel,
            GridFilter filter, GridColumn column, RenderParts parts, Rectangle bounds)
            : base(gridPanel)
        {
            _Bounds = bounds;
            _Graphics = graphics;
            _GridFilter = filter;
            _GridColumn = column;
            _RenderParts = parts;
        }

        #region Public properties

        /// <summary>
        /// Gets the bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        /// <summary>
        /// Gets the associated GridFilter
        /// </summary>
        public GridFilter GridFilter
        {
            get { return (_GridFilter); }
        }

        /// <summary>
        /// Gets the associated GridColumn (which can be null
        /// if the Filter's RowHeader is being rendered)
        /// </summary>
        public GridColumn GridColumn
        {
            get { return (_GridColumn); }
        }

        /// <summary>
        /// Gets the parts to render
        /// </summary>
        public RenderParts RenderParts
        {
            get { return (_RenderParts); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderFilterRowEventArgs

    /// <summary>
    /// GridPreRenderFilterRowEventArgs
    /// </summary>
    public class GridPreRenderFilterRowEventArgs : GridPostRenderFilterRowEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderFilterRowEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="filter"></param>
        ///<param name="column"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPreRenderFilterRowEventArgs(Graphics graphics, GridPanel gridPanel,
            GridFilter filter, GridColumn column, RenderParts parts, Rectangle bounds)
            : base(graphics, gridPanel, filter, column, parts, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderGroupBoxConnectorEventArgs

    /// <summary>
    /// GridPostRenderGroupBoxConnectorEventArgs
    /// </summary>
    public class GridPostRenderGroupBoxConnectorEventArgs : GridEventArgs
    {
        #region Private variables

        private Graphics _Graphics;
        private GridGroupByRow _GroupByRow;
        private GridGroupBox _GridGroupBox1;
        private GridGroupBox _GridGroupBox2;

        #endregion

        ///<summary>
        /// GridPostRenderGroupBoxConnectorEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="groupByRow"></param>
        ///<param name="groupBox1"></param>
        ///<param name="groupBox2"></param>
        public GridPostRenderGroupBoxConnectorEventArgs(Graphics graphics,
            GridPanel gridPanel, GridGroupByRow groupByRow, GridGroupBox groupBox1, GridGroupBox groupBox2)
            : base(gridPanel)
        {
            _Graphics = graphics;
            _GroupByRow = groupByRow;
            _GridGroupBox1 = groupBox1;
            _GridGroupBox2 = groupBox2;
        }

        #region Public properties

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        /// <summary>
        /// Gets the associated GroupByRow
        /// </summary>
        public GridGroupByRow GroupByRow
        {
            get { return (_GroupByRow); }
        }

        /// <summary>
        /// Gets the first associated GridGroupBox
        /// </summary>
        public GridGroupBox GridGroupBox1
        {
            get { return (_GridGroupBox1); }
        }

        /// <summary>
        /// Gets the second associated GridGroupBox
        /// </summary>
        public GridGroupBox GridGroupBox2
        {
            get { return (_GridGroupBox2); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderGroupBoxConnectorEventArgs

    /// <summary>
    /// GridPreRenderGroupBoxConnectorEventArgs
    /// </summary>
    public class GridPreRenderGroupBoxConnectorEventArgs : GridPostRenderGroupBoxConnectorEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderGroupBoxConnectorEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="groupByRow"></param>
        ///<param name="groupBox1"></param>
        ///<param name="groupBox2"></param>
        public GridPreRenderGroupBoxConnectorEventArgs(Graphics graphics, GridPanel gridPanel,
            GridGroupByRow groupByRow, GridGroupBox groupBox1, GridGroupBox groupBox2)
            : base(graphics, gridPanel, groupByRow, groupBox1, groupBox2)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderGroupBoxEventArgs

    /// <summary>
    /// GridPostRenderGroupBoxEventArgs
    /// </summary>
    public class GridPostRenderGroupBoxEventArgs : GridEventArgs
    {
        #region Private variables

        private Graphics _Graphics;
        private Rectangle _Bounds;
        private RenderParts _RenderParts;
        private GridGroupByRow _GroupByRow;
        private GridGroupBox _GridGroupBox;

        #endregion

        ///<summary>
        /// GridPostRenderGroupBoxEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="groupByRow"></param>
        ///<param name="gridGroupBox"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPostRenderGroupBoxEventArgs(Graphics graphics, GridPanel gridPanel,
            GridGroupByRow groupByRow, GridGroupBox gridGroupBox, RenderParts parts, Rectangle bounds)
            : base(gridPanel)
        {
            _Graphics = graphics;
            _GroupByRow = groupByRow;
            _GridGroupBox = gridGroupBox;
            _RenderParts = parts;
            _Bounds = bounds;
        }

        #region Public properties

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        /// <summary>
        /// Gets the bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the parts to render
        /// </summary>
        public RenderParts RenderParts
        {
            get { return (_RenderParts); }
        }

        /// <summary>
        /// Gets the associated GroupByRow
        /// </summary>
        public GridGroupByRow GroupByRow
        {
            get { return (_GroupByRow); }
        }

        /// <summary>
        /// Gets the associated GridGroupBox
        /// </summary>
        public GridGroupBox GridGroupBox
        {
            get { return (_GridGroupBox); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderGroupBoxEventArgs

    /// <summary>
    /// GridPreRenderGroupBoxEventArgs
    /// </summary>
    public class GridPreRenderGroupBoxEventArgs : GridPostRenderGroupBoxEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderGroupBoxEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="groupByRow"></param>
        ///<param name="gridGroupBox"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPreRenderGroupBoxEventArgs(Graphics graphics, GridPanel gridPanel,
            GridGroupByRow groupByRow, GridGroupBox gridGroupBox, RenderParts parts, Rectangle bounds)
            : base(graphics, gridPanel, groupByRow, gridGroupBox, parts, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPostRenderTextRowEventArgs

    /// <summary>
    /// GridPostRenderTextRowEventArgs
    /// </summary>
    public class GridPostRenderTextRowEventArgs : GridEventArgs
    {
        #region Private variables

        private Graphics _Graphics;
        private Rectangle _Bounds;
        private RenderParts _RenderParts;
        private GridTextRow _GridTextRow;

        #endregion

        ///<summary>
        /// GridPostRenderTextRowEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="gridTextRow"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPostRenderTextRowEventArgs(Graphics graphics, GridPanel gridPanel,
            GridTextRow gridTextRow, RenderParts parts, Rectangle bounds)
            : base(gridPanel)
        {
            _Graphics = graphics;
            _RenderParts = parts;
            _Bounds = bounds;
            _GridTextRow = gridTextRow;
        }

        #region Public properties

        /// <summary>
        /// Gets the Graphics object
        /// </summary>
        public Graphics Graphics
        {
            get { return (_Graphics); }
        }

        /// <summary>
        /// Gets the bounding rectangle
        /// </summary>
        public Rectangle Bounds
        {
            get { return (_Bounds); }
        }

        /// <summary>
        /// Gets the parts to render
        /// </summary>
        public RenderParts RenderParts
        {
            get { return (_RenderParts); }
        }

        /// <summary>
        /// Gets the associated GridTextRow
        /// </summary>
        public GridTextRow GridTextRow
        {
            get { return (_GridTextRow); }
        }

        #endregion
    }

    #endregion

    #region GridPreRenderTextRowEventArgs

    /// <summary>
    /// GridPreRenderTextRowEventArgs
    /// </summary>
    public class GridPreRenderTextRowEventArgs : GridPostRenderTextRowEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridPreRenderTextRowEventArgs
        ///</summary>
        ///<param name="graphics"></param>
        ///<param name="gridPanel"></param>
        ///<param name="gridTextRow"></param>
        ///<param name="parts"></param>
        ///<param name="bounds"></param>
        public GridPreRenderTextRowEventArgs(Graphics graphics, GridPanel gridPanel,
            GridTextRow gridTextRow, RenderParts parts, Rectangle bounds)
            : base(graphics, gridPanel, gridTextRow, parts, bounds)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridPreviewKeyDownEventArgs

    ///<summary>
    /// PreviewKeyDown event, with the ability to specify that the key has been handled
    ///</summary>
    public class GridPreviewKeyDownEventArgs : PreviewKeyDownEventArgs
    {
        #region Private variables

        private bool _Handled;

        #endregion

        ///<summary>
        /// PreviewKeyDown event, with the ability to specify that the key has been handled
        ///</summary>
        ///<param name="keyData"></param>
        public GridPreviewKeyDownEventArgs(Keys keyData)
            : base(keyData)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether the key was handled
        /// </summary>
        public bool Handled
        {
            get { return (_Handled); }
            set { _Handled = value; }
        }

        #endregion
    }

    #endregion

    #region GridRefreshFilterEventArgs

    /// <summary>
    /// GridRefreshFilterEventArgs
    /// </summary>
    public class GridRefreshFilterEventArgs : GridCancelEventArgs
    {
        ///<summary>
        /// GridRefreshFilterEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        public GridRefreshFilterEventArgs(GridPanel gridPanel)
            : base(gridPanel)
        {
        }
    }

    #endregion

    #region GridRowActivatedEventArgs

    /// <summary>
    /// GridRowActivatedEventArgs
    /// </summary>
    public class GridRowActivatedEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridContainer _OldActiveRow;
        private readonly GridContainer _NewActiveRow;

        #endregion

        ///<summary>
        /// GridRowActivatedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="oldRow"></param>
        ///<param name="newRow"></param>
        public GridRowActivatedEventArgs(GridPanel gridPanel,
            GridContainer oldRow, GridContainer newRow)
            : base(gridPanel)
        {
            _OldActiveRow = oldRow;
            _NewActiveRow = newRow;
        }

        #region Public properties

        /// <summary>
        /// Gets the old (previous) ActiveRow
        /// </summary>
        public GridContainer OldActiveRow
        {
            get { return (_OldActiveRow); }
        }

        /// <summary>
        /// Gets the new (current) ActiveRow
        /// </summary>
        public GridContainer NewActiveRow
        {
            get { return (_NewActiveRow); }
        }

        #endregion
    }

    #endregion

    #region GridRowActivatingEventArgs

    /// <summary>
    /// GridRowActivatingEventArgs
    /// </summary>
    public class GridRowActivatingEventArgs : GridRowActivatedEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridRowActivatingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="oldRow"></param>
        ///<param name="newRow"></param>
        public GridRowActivatingEventArgs(GridPanel gridPanel, GridContainer oldRow, GridContainer newRow)
            : base(gridPanel, oldRow, newRow)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridRowAddedEventArgs

    /// <summary>
    /// GridRowAddedEventArgs
    /// </summary>
    public class GridRowAddedEventArgs : GridEventArgs
    {
        #region Private variables

        private int _Index;

        #endregion

        ///<summary>
        /// GridRowAddedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="index"></param>
        public GridRowAddedEventArgs(GridPanel gridPanel, int index)
            : base(gridPanel)
        {
            _Index = index;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridPanel.Rows
        /// index where the row is to be inserted
        /// </summary>
        public int Index
        {
            get { return (_Index); }
            protected set { _Index = value; }
        }

        #endregion
    }

    #endregion

    #region GridRowAddingEventArgs

    /// <summary>
    /// GridRowAddingEventArgs
    /// </summary>
    public class GridRowAddingEventArgs : GridRowAddedEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridRowAddingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="index"></param>
        public GridRowAddingEventArgs(GridPanel gridPanel, int index)
            : base(gridPanel, index)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated GridPanel.Rows
        /// index where the row is to be inserted
        /// </summary>
        public new int Index
        {
            get { return (base.Index); }
            set { base.Index = value; }
        }

        #endregion
    }

    #endregion

    #region GridRowCancelEventArgs

    /// <summary>
    /// GridRowCancelEventArgs
    /// </summary>
    public class GridRowCancelEventArgs : GridRowEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridRowCancelEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        public GridRowCancelEventArgs(GridPanel gridPanel, GridContainer gridRow)
            : base(gridPanel, gridRow)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// resulting in the row validation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridRowDeletedEventArgs

    /// <summary>
    /// GridRowMovedEventArgs
    /// </summary>
    public class GridRowDeletedEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly SelectedElements _SelectedRows;

        #endregion

        ///<summary>
        /// GridRowDeletedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="selRows"></param>
        public GridRowDeletedEventArgs(GridPanel gridPanel, SelectedElements selRows)
            : base(gridPanel)
        {
            _SelectedRows = selRows;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated selection of row indices to delete
        /// </summary>
        public SelectedElements SelectedRows
        {
            get { return (_SelectedRows); }
        }

        #endregion
    }

    #endregion

    #region GridRowDeletingEventArgs

    /// <summary>
    /// GridRowDeletingEventArgs
    /// </summary>
    public class GridRowDeletingEventArgs : GridRowDeletedEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridRowDeletingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="selRows"></param>
        public GridRowDeletingEventArgs(GridPanel gridPanel, SelectedElements selRows)
            : base(gridPanel, selRows)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridRowEventArgs

    /// <summary>
    /// GridRowEventArgs
    /// </summary>
    public class GridRowEventArgs : GridEventArgs
    {
        #region Private variables

        private GridContainer _GridRow;

        #endregion

        ///<summary>
        /// GridRowEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        public GridRowEventArgs(GridPanel gridPanel, GridContainer gridRow)
            : base(gridPanel)
        {
            _GridRow = gridRow;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridRow
        /// </summary>
        public GridContainer GridRow
        {
            get { return (_GridRow); }
        }

        #endregion
    }

    #endregion

    #region GridRowClickEventArgs

    /// <summary>
    /// GridRowClickEventArgs
    /// </summary>
    public class GridRowClickEventArgs : GridRowEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private RowArea _RowArea;
        private MouseEventArgs _MouseEventArgs;

        #endregion

        ///<summary>
        /// GridRowClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="rowArea"></param>
        ///<param name="e"></param>
        public GridRowClickEventArgs(
            GridPanel gridPanel, GridRow gridRow, RowArea rowArea, MouseEventArgs e)
            : base(gridPanel, gridRow)
        {
            _RowArea = rowArea;
            _MouseEventArgs = e;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the default operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated MouseEventArgs
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return (_MouseEventArgs); }
        }

        /// <summary>
        /// Gets the associated RowArea
        /// </summary>
        public RowArea RowArea
        {
            get { return (_RowArea); }
        }

        #endregion
    }

    #endregion

    #region GridRowDoubleClickEventArgs

    /// <summary>
    /// GridRowDoubleClickEventArgs
    /// </summary>
    public class GridRowDoubleClickEventArgs : GridRowClickEventArgs
    {
        ///<summary>
        /// GridRowDoubleClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="rowArea"></param>
        ///<param name="e"></param>
        public GridRowDoubleClickEventArgs(
            GridPanel gridPanel, GridRow gridRow, RowArea rowArea, MouseEventArgs e)
            : base(gridPanel, gridRow, rowArea, e)
        {
        }
    }

    #endregion

    #region GridGetDetailRowHeightEventArgs

    /// <summary>
    /// GridGetDetailRowHeightEventArgs
    /// </summary>
    public class GridGetDetailRowHeightEventArgs : EventArgs
    {
        #region Private variables

        private GridRow _GridRow;
        private GridPanel _GridPanel;

        private int _PreDetailHeight;
        private int _PostDetailHeight;

        private GridLayoutInfo _LayoutInfo;
        private Size _SizeNeeded;

        #endregion

        ///<summary>
        /// GridGetDetailRowHeightEventArgs
        ///</summary>
        ///<param name="gridPanel">Associated GridPanel</param>
        ///<param name="gridRow">Associated container row</param>
        ///<param name="layoutInfo"></param>
        ///<param name="sizeNeeded"></param>
        ///<param name="preHeight"></param>
        ///<param name="postHeight"></param>
        public GridGetDetailRowHeightEventArgs(GridPanel gridPanel,
            GridRow gridRow, GridLayoutInfo layoutInfo, Size sizeNeeded, int preHeight, int postHeight)
        {
            _GridPanel = gridPanel;
            _GridRow = gridRow;

            _SizeNeeded = sizeNeeded;
            _LayoutInfo = layoutInfo;

            _PreDetailHeight = preHeight;
            _PostDetailHeight = postHeight;
        }

        #region Public properties

        /// <summary>
        /// Gets the event Grid
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        /// <summary>
        /// Gets the associated GridRow
        /// </summary>
        public GridRow GridRow
        {
            get { return (_GridRow); }
        }

        /// <summary>
        /// Gets or sets the associated row PreDetail Height
        /// </summary>
        public int PreDetailHeight
        {
            get { return (_PreDetailHeight); }
            set { _PreDetailHeight = value; }
        }

        /// <summary>
        /// Gets or sets the associated row PostDetail Height
        /// </summary>
        public int PostDetailHeight
        {
            get { return (_PostDetailHeight); }
            set { _PostDetailHeight = value; }
        }

        /// <summary>
        /// Gets the associated row LayoutInfo
        /// </summary>
        public GridLayoutInfo LayoutInfo
        {
            get { return (_LayoutInfo); }
        }

        /// <summary>
        /// Gets or sets the associated row's needed size
        /// </summary>
        public Size SizeNeeded
        {
            get { return (_SizeNeeded); }
            set { _SizeNeeded = value; }
        }

        #endregion
    }

    #endregion

    #region GridGetGroupIdEventArgs

    /// <summary>
    /// GridGetGroupIdEventArgs
    /// </summary>
    public class GridGetGroupIdEventArgs : EventArgs
    {
        #region Private variables

        private GridPanel _GridPanel;
        private GridElement _GridItem;
        private GridColumn _GridColumn;

        private object _GroupId;

        #endregion

        ///<summary>
        /// GridGetGroupIdEventArgs
        ///</summary>
        ///<param name="gridPanel">Associated GridPanel</param>
        ///<param name="gridItem">Associated grid element</param>
        ///<param name="column">Associated grid column</param>
        ///<param name="groupId">Associated Group identifier</param>
        public GridGetGroupIdEventArgs(
            GridPanel gridPanel, GridElement gridItem, GridColumn column, object groupId)
        {
            _GridPanel = gridPanel;
            _GridColumn = column;
            _GridItem = gridItem;

            _GroupId = groupId;
        }

        #region Public properties

        /// <summary>
        /// Gets the event Grid
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        /// <summary>
        /// Gets the associated GridElement
        /// </summary>
        public GridElement GridItem
        {
            get { return (_GridItem); }
        }

        /// <summary>
        /// Gets the associated GridColumn
        /// </summary>
        public GridColumn GridColumn
        {
            get { return (_GridColumn); }
        }

        /// <summary>
        /// Gets or sets the associated row Group Identifier
        /// </summary>
        public object GroupId
        {
            get { return (_GroupId); }
            set { _GroupId = value; }
        }

        #endregion
    }

    #endregion

    #region GridRowHeaderClickEventArgs

    /// <summary>
    /// GridRowHeaderClickEventArgs
    /// </summary>
    public class GridRowHeaderClickEventArgs : GridRowEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private MouseEventArgs _MouseEventArgs;

        #endregion

        ///<summary>
        /// GridRowHeaderClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="row"></param>
        ///<param name="e"></param>
        public GridRowHeaderClickEventArgs(
            GridPanel gridPanel, GridContainer row, MouseEventArgs e)
            : base(gridPanel, row)
        {
            _MouseEventArgs = e;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated MouseEventArgs
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return (_MouseEventArgs); }
        }

        /// <summary>
        /// Gets or sets whether to cancel the operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridRowHeaderDoubleClickEventArgs

    /// <summary>
    /// GridRowHeaderDoubleClickEventArgs
    /// </summary>
    public class GridRowHeaderDoubleClickEventArgs : GridRowHeaderClickEventArgs
    {
        ///<summary>
        /// GridRowHeaderDoubleClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="row"></param>
        ///<param name="e"></param>
        public GridRowHeaderDoubleClickEventArgs(
            GridPanel gridPanel, GridContainer row, MouseEventArgs e)
            : base(gridPanel, row, e)
        {
        }
    }

    #endregion

    #region GridRowInfoEnterEventArgs

    /// <summary>
    /// GridRowInfoEnterEventArgs
    /// </summary>
    public class GridRowInfoEnterEventArgs : GridRowEventArgs
    {
        #region Private variables

        private bool _Cancel;
        private Point _Location;

        #endregion

        ///<summary>
        /// GridRowInfoEnterEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="pt"></param>
        public GridRowInfoEnterEventArgs(
            GridPanel gridPanel, GridRow gridRow, Point pt)
            : base(gridPanel, gridRow)
        {
            _Location = pt;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether to cancel the default operation
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        /// <summary>
        /// Gets the associated event Location
        /// </summary>
        public Point Location
        {
            get { return (_Location); }
        }

        #endregion
    }

    #endregion

    #region GridRowInfoLeaveEventArgs

    /// <summary>
    /// GridRowInfoLeaveEventArgs
    /// </summary>
    public class GridRowInfoLeaveEventArgs : GridRowEventArgs
    {
        ///<summary>
        /// GridRowInfoLeaveEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        public GridRowInfoLeaveEventArgs(
            GridPanel gridPanel, GridRow gridRow)
            : base(gridPanel, gridRow)
        {
        }
    }

    #endregion

    #region GridRowMouseEventArgs

    /// <summary>
    /// GridRowMouseEventArgs
    /// </summary>
    public class GridRowMouseEventArgs : MouseEventArgs
    {
        #region Private variables

        private readonly GridPanel _GridPanel;
        private readonly GridContainer _GridRow;
        private readonly RowArea _RowArea;

        #endregion

        ///<summary>
        /// GridRowMouseEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="ev"></param>
        ///<param name="rowArea"></param>
        public GridRowMouseEventArgs(GridPanel gridPanel, GridContainer gridRow, MouseEventArgs ev, RowArea rowArea)
            : base(ev.Button, ev.Clicks, ev.X, ev.Y, ev.Delta)
        {
            _GridPanel = gridPanel;
            _GridRow = gridRow;
            _RowArea = rowArea;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridRow
        /// </summary>
        public GridContainer GridRow
        {
            get { return (_GridRow); }
        }

        /// <summary>
        /// Gets the associated GridPanel
        /// </summary>
        public GridPanel GridPanel
        {
            get { return (_GridPanel); }
        }

        /// <summary>
        /// Gets the associated RowArea
        /// </summary>
        public RowArea RowArea
        {
            get { return (_RowArea); }
        }

        #endregion
    }

    #endregion

    #region GridRowMovedEventArgs

    /// <summary>
    /// GridRowMovedEventArgs
    /// </summary>
    public class GridRowMovedEventArgs : GridRowEventArgs
    {
        #region Private variables

        private readonly GridContainer _GridContainer;

        #endregion

        ///<summary>
        /// GridRowMovedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridContainer"></param>
        ///<param name="gridRow"></param>
        public GridRowMovedEventArgs(GridPanel gridPanel,
            GridContainer gridContainer, GridContainer gridRow)
            : base(gridPanel, gridRow)
        {
            _GridContainer = gridContainer;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridContainer
        /// </summary>
        public GridContainer GridContainer
        {
            get { return (_GridContainer); }
        }

        #endregion
    }

    #endregion

    #region GridRowMovingEventArgs

    /// <summary>
    /// GridRowMovingEventArgs
    /// </summary>
    public class GridRowMovingEventArgs : GridRowCancelEventArgs
    {
        #region Private variables

        private int _SrcIndex;
        private GridContainer _SrcContainer;

        private int _DestIndex;
        private GridContainer _DestContainer;

        #endregion

        ///<summary>
        /// GridRowMovingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="srcCont"></param>
        ///<param name="srcIndex"></param>
        ///<param name="destCont"></param>
        ///<param name="destIndex"></param>
        public GridRowMovingEventArgs(GridPanel gridPanel, GridRow gridRow,
            GridContainer srcCont, int srcIndex, GridContainer destCont, int destIndex)
            : base(gridPanel, gridRow)
        {
            _SrcIndex = srcIndex;
            _SrcContainer = srcCont;

            _DestIndex = destIndex;
            _DestContainer = destCont;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the Destination Container for the row
        /// being moved.
        /// </summary>
        public GridContainer DestContainer
        {
            get { return (_DestContainer); }
            set { _DestContainer = value; }
        }

        /// <summary>
        /// Gets or sets the Destination index for the row
        /// being moved.
        /// </summary>
        public int DestIndex
        {
            get { return (_DestIndex); }
            set { _DestIndex = value; }
        }

        /// <summary>
        /// Gets the Source Container of the row
        /// being moved.
        /// </summary>
        public GridContainer SrcContainer
        {
            get { return (_SrcContainer); }
        }

        /// <summary>
        /// Gets the Source index of the row being moved.
        /// </summary>
        public int SrcIndex
        {
            get { return (_SrcIndex); }
        }

        #endregion
    }

    #endregion

    #region GridRowRestoredEventArgs

    /// <summary>
    /// GridRowRestoredEventArgs
    /// </summary>
    public class GridRowRestoredEventArgs : GridRowDeletedEventArgs
    {
        ///<summary>
        /// GridRowRestoredEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="selRows"></param>
        public GridRowRestoredEventArgs(GridPanel gridPanel, SelectedElements selRows)
            : base(gridPanel, selRows)
        {
        }
    }

    #endregion

    #region GridRowRestoringEventArgs

    /// <summary>
    /// GridRowRestoringEventArgs
    /// </summary>
    public class GridRowRestoringEventArgs : GridRowDeletingEventArgs
    {
        ///<summary>
        /// GridRowRestoringEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="selRows"></param>
        public GridRowRestoringEventArgs(GridPanel gridPanel, SelectedElements selRows)
            : base(gridPanel, selRows)
        {
        }
    }

    #endregion

    #region GridRowSetDefaultValuesEventArgs

    /// <summary>
    /// GridRowSetDefaultValuesEventArgs
    /// </summary>
    public class GridRowSetDefaultValuesEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridRow _GridRow;
        private readonly NewRowContext _NewRowContext;

        #endregion

        ///<summary>
        /// GridRowDeletedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="context"></param>
        public GridRowSetDefaultValuesEventArgs(
            GridPanel gridPanel, GridRow gridRow, NewRowContext context)
            : base(gridPanel)
        {
            _GridRow = gridRow;
            _NewRowContext = context;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridRow
        /// </summary>
        public GridRow GridRow
        {
            get { return (_GridRow); }
        }

        /// <summary>
        /// Gets the associated context under which
        /// the new row data needs to be initialized
        /// </summary>
        public NewRowContext NewRowContext
        {
            get { return (_NewRowContext); }
        }

        #endregion
    }

    #endregion

    #region GridRowValidatedEventArgs

    /// <summary>
    /// GridRowValidatedEventArgs
    /// </summary>
    public class GridRowValidatedEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridRow _GridRow;

        #endregion

        ///<summary>
        /// GridRowDeletedEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        public GridRowValidatedEventArgs(GridPanel gridPanel, GridRow gridRow)
            : base(gridPanel)
        {
            _GridRow = gridRow;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridRow
        /// </summary>
        public GridRow GridRow
        {
            get { return (_GridRow); }
        }

        #endregion
    }

    #endregion

    #region GridRowValidatingEventArgs

    /// <summary>
    /// GridRowValidatingEventArgs
    /// </summary>
    public class GridRowValidatingEventArgs : GridRowValidatedEventArgs
    {
        #region Private variables

        private bool _Cancel;

        #endregion

        ///<summary>
        /// GridRowValidatingEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        public GridRowValidatingEventArgs(GridPanel gridPanel, GridRow gridRow)
            : base(gridPanel, gridRow)
        {
        }

        #region Public properties

        /// <summary>
        /// Gets or sets whether
        /// to cancel the operation entirely
        /// </summary>
        public bool Cancel
        {
            get { return (_Cancel); }
            set { _Cancel = value; }
        }

        #endregion
    }

    #endregion

    #region GridScrollEventArgs

    /// <summary>
    /// GridScrollEventArgs
    /// </summary>
    public class GridScrollEventArgs : GridEventArgs
    {
        #region Private variables

        private ScrollEventArgs _ScrollEventArgs;

        #endregion

        ///<summary>
        /// GridScrollEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="scrollEventArgs"></param>
        public GridScrollEventArgs(
            GridPanel gridPanel, ScrollEventArgs scrollEventArgs)
            : base(gridPanel)
        {
            _ScrollEventArgs = scrollEventArgs;
        }

        #region Public properties

        /// <summary>
        /// Gets the scroll event args
        /// </summary>
        public ScrollEventArgs ScrollEventArgs
        {
            get { return (_ScrollEventArgs); }
        }

        #endregion
    }

    #endregion

    #region GridTextRowEventArgs

    /// <summary>
    /// GridTextRowEventArgs
    /// </summary>
    public class GridTextRowEventArgs : GridEventArgs
    {
        #region Private variables

        private readonly GridTextRow _GridTextRow;
        private MouseEventArgs _MouseEventArgs;

        #endregion

        ///<summary>
        /// GridTextRowEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="textRow"></param>
        ///<param name="e"></param>
        public GridTextRowEventArgs(
            GridPanel gridPanel, GridTextRow textRow, MouseEventArgs e)
            : base(gridPanel)
        {
            _GridTextRow = textRow;
            _MouseEventArgs = e;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated GridTextRow
        /// </summary>
        public GridTextRow GridTextRow
        {
            get { return (_GridTextRow); }
        }

        /// <summary>
        /// Gets the associated MouseEventArgs
        /// </summary>
        public MouseEventArgs MouseEventArgs
        {
            get { return (_MouseEventArgs); }
        }

        #endregion
    }

    #endregion

    #region GridTextRowMarkupLinkClickEventArgs

    /// <summary>
    /// GridTextRowMarkupLinkClickEventArgs
    /// </summary>
    public class GridTextRowMarkupLinkClickEventArgs : GridEventArgs
    {
        #region Private variables

        private string _HRef;
        private string _Name;
        private readonly GridTextRow _GridTextRow;

        #endregion

        ///<summary>
        /// GridTextRowMarkupLinkClickEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridTextRow"></param>
        ///<param name="name"></param>
        ///<param name="href"></param>
        public GridTextRowMarkupLinkClickEventArgs(GridPanel gridPanel,
            GridTextRow gridTextRow, string name, string href)
            : base(gridPanel)
        {
            _GridTextRow = gridTextRow;

            _HRef = href;
            _Name = name;
        }

        #region Public properties

        /// <summary>
        /// Gets the associated HyperLink HRef
        /// </summary>
        public string HRef
        {
            get { return (_HRef); }
        }

        /// <summary>
        /// Gets the associated HyperLink Name
        /// </summary>
        public string Name
        {
            get { return (_Name); }
        }

        /// <summary>
        /// Gets the associated GridTextRow
        /// </summary>
        public GridTextRow GridTextRow
        {
            get { return (_GridTextRow); }
        }

        #endregion
    }

    #endregion

    #region GridVirtualRowEventArgs

    /// <summary>
    /// GridVirtualRowEventArgs
    /// </summary>
    public class GridVirtualRowEventArgs : GridEventArgs
    {
        #region Private variables

        private GridRow _GridRow;
        private int _Index;

        #endregion

        ///<summary>
        /// GridVirtualRowEventArgs
        ///</summary>
        ///<param name="gridPanel"></param>
        ///<param name="gridRow"></param>
        ///<param name="index"></param>
        public GridVirtualRowEventArgs(GridPanel gridPanel, GridRow gridRow, int index)
            : base(gridPanel)
        {
            _GridRow = gridRow;
            _Index = index;
        }

        #region Public properties

        /// <summary>
        /// Gets the GridRow
        /// </summary>
        public GridRow GridRow
        {
            get { return (_GridRow); }
        }

        /// <summary>
        /// Gets the associated row index
        /// </summary>
        public int Index
        {
            get { return (_Index); }
        }

        #endregion
    }

    #endregion

    #endregion
}
