using System;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// IGridCellEditControl
    ///</summary>
    public interface IGridCellEditControl
    {
        #region Properties

        #region CanInterrupt

        ///<summary>
        /// Specifies whether the Grid can automatically interrupt the
        /// current NonModal cell edit operation when the mouse leaves the cell.
        /// 
        /// This is utilized in cases, such as the ButtonX editor, where multilevel
        /// controls may be involved. In the ButtonX case this specifically enables
        /// the mouse to to leave the main cell window and not be interrupted by the
        /// grid until the current operation is complete by the editor itself.
        /// 
        ///</summary>
        bool CanInterrupt { get; }

        #endregion

        #region CellEditMode

        ///<summary>
        /// Specifies the cell editor activation mode. There are three modes
        /// available for choice, and their usage depends upon the desired
        /// editor presentation, as well as how well the given underlying
        /// control interacts with the grid control.
        /// 
        /// The modes are as follows:
        /// 
        /// InPlace:
        /// Edit operations occur 'in place', always live, always
        /// in a state of interaction with the user. The CheckBoxEx,
        /// and ButtonX editors are examples of this.
        /// 
        /// Modal:
        /// Edit operations will be performed as a modal, widowed edit 
        /// controlled via calls to BeginEdit, EndEdit, and CancelEdit.
        /// The TextBoxEx and IntegerInput editors are examples of this.
        /// 
        /// NonModal:
        /// Edit operation will be performed as a nonNodal, windowed edit
        /// automatically invoked upon entry to the cell. The ButtonEx,
        /// BubbleBar, and TrackBar are examples of this. 
        /// 
        ///</summary>
        CellEditMode CellEditMode { get; }

        #endregion

        #region EditorCell

        ///<summary>
        /// Specifies the editor's current associated grid cell
        ///</summary>
        GridCell EditorCell { get; set; }

        #endregion

        #region EditorCellBitmap

        ///<summary>
        /// Called to get or set the editor's
        /// associated cell bitmap (internal use only)
        ///</summary>
        Bitmap EditorCellBitmap { get; set; }

        #endregion

        #region EditorFormattedValue

        ///<summary>
        /// Called to get the formatted editor value
        ///</summary>
        string EditorFormattedValue { get; }

        #endregion

        #region EditorPanel

        ///<summary>
        /// Called to get or set the editor's
        /// associated cell panel (internal use only)
        ///</summary>
        EditorPanel EditorPanel { get; set; }

        #endregion

        #region EditorValue

        ///<summary>
        /// Called to get or set the editor value
        ///</summary>
        object EditorValue { get; set;}

        #endregion

        #region EditorValueChanged

        ///<summary>
        /// Called to get or set whether the editor value has changed
        ///</summary>
        bool EditorValueChanged { get; set;}

        #endregion

        #region EditorValueType

        ///<summary>
        /// Called to get the editor's default value data type
        ///</summary>
        Type EditorValueType { get; }

        #endregion

        #region StretchBehavior

        ///<summary>
        /// Called to get the editor's desired 'stretch' behavior.
        /// 
        /// The StretchBehavior defines whether the editor wants to
        /// automatically have it's size stretched to fill the given cell.
        /// 
        /// Editors, such as ButtonX, want to fill the cell horizontally and
        /// vertically, whereas other editors, such as the Slider, may only
        /// want to stretch horizontally (or potentially not at all).
        ///</summary>
        StretchBehavior StretchBehavior { get; }

        #endregion

        #region SuspendUpdate

        ///<summary>
        /// Called to get or set whether updates to the grid
        /// state is suspended
        ///</summary>
        bool SuspendUpdate { get; set; }

        #endregion

        #region ValueChangeBehavior

        ///<summary>
        /// Called to get the behavior
        /// needed when a value changes in the editor.
        /// 
        /// For instance, CheckBoxEx value changes result in only requiring
        /// the cell to be redisplayed with the new state, whereas editors such
        /// as the TextBoxEx editor, may result in a new layout when the text changes.
        /// 
        ///</summary>
        ValueChangeBehavior ValueChangeBehavior { get; }

        #endregion

        #endregion

        #region CellRender

        ///<summary>
        ///  Called to initiate the actual rendering of the editor
        /// value into the grid cell.  In most cases this can be (and is)
        /// handled by a simple call to EditorCell.CellRender(this, graphics).
        /// If additional rendering is required by the editor, then the editor
        /// can completely render the cell contents itself (and never even call
        /// Editor.CellRender) or optionally perform additional rendering before
        /// or after the default cell rendering.
        ///</summary>
        ///<param name="g"></param>
        void CellRender(Graphics g);

        #endregion

        #region CellKeyDown

        ///<summary>
        /// Called when a KeyDown occurs and the
        /// CellEditMode is InPlace (otherwise the key event is
        /// automatically directed straight to the editor control).
        /// 
        ///</summary>
        void CellKeyDown(KeyEventArgs e);

        #endregion

        #region Edit support

        #region BeginEdit

        ///<summary>
        /// Called when a Modal cell edit is about to be initiated.
        ///</summary>
        ///<param name="selectAll">Signifies whether to select all editable content</param>
        ///<returns>true to cancel the edit operation</returns>
        bool BeginEdit(bool selectAll);

        #endregion

        #region EndEdit

        ///<summary>
        /// Called when the edit operation is about to end. 
        ///</summary>
        ///<returns>true to cancel the operation.</returns>
        bool EndEdit();

        #endregion

        #region CancelEdit

        ///<summary>
        /// Called when the edit operation is being cancelled.
        ///</summary>
        ///<returns>true to cancel the operation.</returns>
        bool CancelEdit();

        #endregion

        #endregion

        #region GetProposedSize

        ///<summary>
        /// Called to retrieve the editors proposed size
        /// for the given cell, using the provided effective
        /// style and size constraint.
        ///</summary>
        ///<param name="g">Graphics object</param>
        ///<param name="cell">Associated grid cell</param>
        ///<param name="style">Cell's effective style</param>
        ///<param name="constraintSize">The constraining cell size</param>
        ///<returns></returns>
        Size GetProposedSize(Graphics g, GridCell cell, CellVisualStyle style, Size constraintSize);

        #endregion

        #region InitializeContext

        ///<summary>
        /// Called to initialize the editor's context
        /// environment (value, properties, style, etc)
        ///</summary>
        ///<param name="cell"></param>
        ///<param name="style"></param>
        void InitializeContext(GridCell cell, CellVisualStyle style);

        #endregion

        #region Mouse support

        #region OnCellMouseMove

        ///<summary>
        /// Called when a MouseMove event occurs and the
        /// CellEditMode is InPlace (otherwise the event is
        /// automatically directed straight to the editor control).
        ///</summary>
        ///<param name="e"></param>
        void OnCellMouseMove(MouseEventArgs e);

        #endregion

        #region OnCellMouseEnter

        /// <summary>
        /// Called when a MouseEnter event occurs and the
        /// CellEditMode is InPlace (otherwise the event is
        /// automatically directed straight to the editor control).
        /// </summary>
        /// <param name="e"></param>
        void OnCellMouseEnter(EventArgs e);

        #endregion

        #region OnCellMouseLeave

        ///<summary>
        /// Called when a MouseLeave event occurs and the
        /// CellEditMode is InPlace (otherwise the event is
        /// automatically directed straight to the editor control).
        ///</summary>
        ///<param name="e"></param>
        void OnCellMouseLeave(EventArgs e);

        #endregion

        #region OnCellMouseUp

        /// <summary>
        /// Called when a MouseUp event occurs and the
        /// CellEditMode is InPlace (otherwise the event is
        /// automatically directed straight to the editor control).
        /// </summary>
        /// <param name="e"></param>
        void OnCellMouseUp(MouseEventArgs e);

        #endregion

        #region OnCellMouseDown

        /// <summary>
        /// Called when a MouseDown event occurs and the
        /// CellEditMode is InPlace (otherwise the event is
        /// automatically directed straight to the editor control).
        /// </summary>
        /// <param name="e"></param>
        void OnCellMouseDown(MouseEventArgs e);

        #endregion

        #endregion

        #region WantsInputKey

        ///<summary>
        /// Called to determine if the editor wants to process and
        /// handle the given key
        ///</summary>
        ///<param name="key">Key in question</param>
        ///<param name="gridWantsKey">Whether the grid, by default, wants the key</param>
        ///<returns>true is the control wants the key</returns>
        bool WantsInputKey(Keys key, bool gridWantsKey);

        #endregion
    }

    #region IGridCellConvertTo

    ///<summary>
    ///IGridCellConvertTo
    ///</summary>
    public interface IGridCellConvertTo
    {
        ///<summary>
        ///TryConvertTo
        ///</summary>
        ///<param name="value">Value to convert</param>
        ///<param name="dataType">Data type to convert to</param>
        ///<param name="result">Converted value</param>
        ///<returns></returns>
        bool TryConvertTo(object value, Type dataType, out object result);
    }

    #endregion

    #region IGridCellEditorFocus

    ///<summary>
    ///IGridCellEditorFocus
    ///</summary>
    public interface IGridCellEditorFocus
    {
        ///<summary>
        ///Indicates whether the editor has the input focus
        ///</summary>
        ///<returns></returns>
        bool IsEditorFocused { get; }

        ///<summary>
        ///Gives the editor the input focus
        ///</summary>
        void FocusEditor();
    }

    #endregion

    #region enums

    #region CellEditMode

    ///<summary>
    /// Specifies the mode of cell editor activation
    ///</summary>
    public enum CellEditMode
    {
        ///<summary>
        /// Edit operation will be performed as a modal, widowed
        /// edit controlled via BeginEdit, EndEdit, and CancelEdit.
        ///</summary>
        Modal,

        ///<summary>
        /// Edit operation will be performed as a nonNodal, windowed
        /// edit automatically invoked upon entry to the cell.
        ///</summary>
        NonModal,

        ///<summary>
        /// Edit operation will be performed in-place, as a non-windowed
        /// edit automatically invoked upon entry to the cell.
        ///</summary>
        InPlace,
    }

    #endregion

    #region StretchBehavior

    /// <summary>
    /// Defines how the editor is stretched to
    /// have it's size fill the given cell.
    /// </summary>
    public enum StretchBehavior
    {
        ///<summary>
        /// No stretching to fill the cell
        ///</summary>
        None,

        ///<summary>
        /// Auto stretch horizontally only
        ///</summary>
        HorizontalOnly,

        ///<summary>
        /// Auto stretch vertically only
        ///</summary>
        VerticalOnly,

        ///<summary>
        /// Auto stretch both horizontally and vertically
        ///</summary>
        Both,
    }

    #endregion

    #region ValueChangeBehavior

    /// <summary>
    /// Defines how the grid should respond to
    /// editor content / value changes
    /// </summary>
    public enum ValueChangeBehavior
    {
        ///<summary>
        /// No action needed
        ///</summary>
        None,

        ///<summary>
        /// Grid layout needs invalidated
        ///</summary>
        InvalidateLayout,

        ///<summary>
        /// Cell needs rendered
        ///</summary>
        InvalidateRender,
    }

    #endregion

    #endregion
}
