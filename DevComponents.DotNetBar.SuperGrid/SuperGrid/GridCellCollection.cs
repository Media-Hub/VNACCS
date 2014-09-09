using System;
using System.ComponentModel;
using System.Drawing.Design;
using DevComponents.DotNetBar.SuperGrid.Primitives;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents the collection of grid cells.
    /// </summary>
    [Editor("DevComponents.SuperGrid.Design.GridCellCollectionEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
    public class GridCellCollection : CustomCollection<GridCell>
    {
        #region Indexer (string)

        /// <summary>
        /// Gets or sets item at ColumnName index
        /// </summary>
        /// <param name="columnName">Name of Column containing the cell</param>
        public GridCell this[string columnName]
        {
            get
            {
                GridCell cell = FindCellItem(columnName);

                if (cell != null)
                    return (Items[cell.ColumnIndex]);

                return (null);
            }

            set
            {
                GridCell cell = FindCellItem(columnName);

                if (cell == null)
                    throw new Exception("Column \"" + columnName + "\" not found.");

                Items[cell.ColumnIndex] = value;
            }
        }

        #endregion

        #region Indexer (Column)

        /// <summary>
        /// Gets or sets item at Column index
        /// </summary>
        /// <param name="column">Column containing the cell</param>
        public GridCell this[GridColumn column]
        {
            get { return (Items[column.ColumnIndex]); }
            set { Items[column.ColumnIndex] = value; }
        }

        #endregion

        #region FindCellItem

        private GridCell FindCellItem(string columnName)
        {
            if (string.IsNullOrEmpty(columnName) == true)
                throw new Exception("Invalid Column Name.");

            foreach (GridCell cell in Items)
            {
                if (columnName.Equals(cell.GridColumn.Name) == true)
                    return (cell);
            }

            return (null);
        }

        #endregion
    }
}
