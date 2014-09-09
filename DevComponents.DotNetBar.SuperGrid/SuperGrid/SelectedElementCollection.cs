using System.Collections.Generic;
using DevComponents.DotNetBar.SuperGrid.Primitives;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// SelectedElementCollection
    ///</summary>
    public class SelectedElementCollection : CustomCollection<GridElement>
    {
        #region Constructors

        ///<summary>
        /// SelectedElementCollection
        ///</summary>
        public SelectedElementCollection()
        {
        }

        ///<summary>
        /// SelectedElementCollection
        ///</summary>
        ///<param name="capacity"></param>
        public SelectedElementCollection(int capacity)
            : base(capacity)
        {
        }

        #endregion

        #region GetCells

        #region GetCells

        ///<summary>
        /// GetCells
        ///</summary>
        ///<returns></returns>
        public List<GridCell> GetCells()
        {
            List<GridCell> cells = new List<GridCell>();

            bool hasRows = AddRowCells(cells);
            bool hasColumns = AddColumnCells(cells, hasRows);

            AddCellCells(cells, hasRows | hasColumns);

            return (cells);
        }

        #region AddRowCells

        private bool AddRowCells(List<GridCell> cells)
        {
            bool hasRows = false;

            foreach (GridElement item in Items)
            {
                GridRow row = item as GridRow;

                if (row != null)
                {
                    GridPanel panel = row.GridPanel;

                    for (int i = 0; i < panel.Columns.Count; i++)
                    {
                        GridCell cell =
                            row.GetCell(i, panel.AllowEmptyCellSelection);

                        if (cell != null)
                        {
                            if (cell.AllowSelection == true)
                                cells.Add(cell);
                        }
                    }

                    hasRows = true;
                }
            }

            return (hasRows);
        }

        #endregion

        #region AddColumnCells

        private bool AddColumnCells(List<GridCell> cells, bool hasRows)
        {
            bool hasColumns = false;

            foreach (GridElement item in Items)
            {
                GridColumn column = item as GridColumn;

                if (column != null)
                {
                    GridContainer container = 
                        column.Parent as GridContainer;

                    if (container != null)
                    {
                        hasColumns = true;

                        AddUniqueColumnCells(container,
                            cells, column.ColumnIndex, hasRows);
                    }
                }
            }

            return (hasColumns);
        }

        #region AddUniqueColumnCells

        private void AddUniqueColumnCells(GridContainer container,
            ICollection<GridCell> cells, int columnIndex, bool hasRows)
        {
            if (container != null)
            {
                GridPanel panel = container.GridPanel;

                if (panel != null)
                {
                    if (panel.VirtualMode == true)
                    {
                        for (int i = 0; i < panel.VirtualRowCountEx; i++)
                        {
                            GridRow row = panel.VirtualRows[i];

                            if (hasRows == false || row.IsSelected == false)
                            {
                                if (row.Cells[columnIndex].AllowSelection == true)
                                    cells.Add(row.Cells[columnIndex]);
                            }
                        }
                    }
                    else
                    {
                        foreach (GridContainer item in container.Rows)
                        {
                            GridRow row = item as GridRow;

                            if (row != null)
                            {
                                if (hasRows == false || row.IsSelected == false)
                                {
                                    GridCell cell = row.GetCell(columnIndex,
                                        panel.AllowEmptyCellSelection);

                                    if (cell != null)
                                    {
                                        if (cell.AllowSelection == true)
                                            cells.Add(cell);
                                    }
                                }

                                if (row.Rows.Count > 0 && row.Expanded == true)
                                    AddUniqueColumnCells(item, cells, columnIndex, hasRows);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #endregion

        #region AddCellCells

        private void AddCellCells(List<GridCell> cells, bool hasRowCol)
        {
            foreach (GridElement item in Items)
            {
                GridCell cell = item as GridCell;

                if (cell != null)
                {
                    if (cell.AllowSelection == true)
                    {
                        if (hasRowCol == true)
                            AddUniqueCells(cells, cell);
                        else
                            cells.Add(cell);
                    }
                }
            }
        }

        #region AddUniqueCells

        private void AddUniqueCells(
            ICollection<GridCell> cells, GridCell cell)
        {
            GridPanel panel = cell.GridColumn.Parent as GridPanel;

            if (panel != null)
            {
                if (cell.GridRow.IsSelected == false)
                {
                    if (cell.GridColumn.IsSelected == false)
                        cells.Add(cell);
                }
            }
        }

        #endregion

        #endregion

        #endregion

        #endregion

        #region Select

        ///<summary>
        ///</summary>
        public void Select(bool value)
        {
            foreach (GridElement item in Items)
            {
                if (item is GridPanel)
                    ((GridPanel)item).IsSelected = value;

                else if (item is GridRow)
                    ((GridRow)item).IsSelected = value;

                else if (item is GridColumn)
                    ((GridColumn)item).IsSelected = value;

                else if (item is GridCell)
                    ((GridCell)item).IsSelected = value;
            }
        }

        #endregion
    }
}
