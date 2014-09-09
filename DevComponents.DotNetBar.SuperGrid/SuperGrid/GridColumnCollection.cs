using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing.Design;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Defines the collection of grid columns
    /// </summary>
    [Editor("DevComponents.SuperGrid.Design.GridColumnCollectionEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
    public class GridColumnCollection : CollectionBase
    {
        #region Events

        /// <summary>
        /// Occurs when the collection has changed
        /// </summary>
        [Description("Occurs when collection has changed.")]
        public CollectionChangeEventHandler CollectionChanged;

        #endregion

        #region Private Variables

        private GridElement _ParentItem;
        private int[] _DisplayIndexMap;
        private bool _IsDisplayIndexValid;

        #endregion

        #region Public properties

        #region FirstSelectableColumn

        /// <summary>
        /// Gets reference to first selectable column
        /// or null if there is no first selectable column.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn FirstSelectableColumn
        {
            get
            {
                int[] displayMap = DisplayIndexMap;

                for (int i = 0; i < displayMap.Length; i++)
                {
                    GridColumn column = this[displayMap[i]];

                    if (column.Visible == true && column.AllowSelection == true)
                        return (column);
                }

                return (null);
            }
        }

        #endregion

        #region FirstVisibleColumn

        /// <summary>
        /// Gets reference to first visible column
        /// or null if there is no first visible column.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn FirstVisibleColumn
        {
            get
            {
                int[] displayMap = DisplayIndexMap;

                for (int i = 0; i < displayMap.Length; i++)
                {
                    if (this[displayMap[i]].Visible)
                        return (this[displayMap[i]]);
                }

                return (null);
            }
        }

        #endregion

        #region Index indexer

        /// <summary>
        /// Returns reference to the object in collection based on it's index.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn this[int index]
        {
            get { return (GridColumn)(List[index]); }
            set { List[index] = value; }
        }

        #endregion

        #region Name indexer

        ///<summary>
        /// Name indexer
        ///</summary>
        ///<param name="name"></param>
        ///<exception cref="Exception"></exception>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn this[string name]
        {
            get
            {
                int index = FindIndexByName(name);

                return (index >= 0 ? this[index] : null);
            }

            set
            {
                int index = FindIndexByName(name);

                if (index < 0)
                    throw new Exception("Column Name not defined (" + name + ").");

                List[index] = value;
            }
        }

        #region FindIndexByName

        private int FindIndexByName(string name)
        {
            for (int i=0; i<List.Count; i++)
            {
                GridColumn item = (GridColumn)List[i];

                if (name != null)
                    name = name.ToUpper();

                if (item.Name != null && item.Name.ToUpper().Equals(name))
                    return (i);
            }

            return (-1);
        }

        #endregion

        #endregion

        #region LastSelectableColumn

        /// <summary>
        /// Gets reference to last selectable column
        /// or null if there is no last selectable column.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn LastSelectableColumn
        {
            get
            {
                int[] displayMap = DisplayIndexMap;

                for (int i = displayMap.Length - 1; i >= 0; i--)
                {
                    GridColumn column = this[displayMap[i]];

                    if (column.Visible == true && column.AllowSelection == true)
                        return (column);
                }

                return (null);
            }
        }

        #endregion

        #region LastVisibleColumn

        /// <summary>
        /// Gets reference to last visible column
        /// or null if there is no last visible column.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridColumn LastVisibleColumn
        {
            get
            {
                int[] displayMap = DisplayIndexMap;

                for (int i = displayMap.Length - 1; i >= 0; i--)
                {
                    if (this[displayMap[i]].Visible)
                        return (this[displayMap[i]]);
                }

                return (null);
            }
        }

        #endregion

        #region ParentItem

        /// <summary>
        /// Gets or sets the node this collection is associated with.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GridElement ParentItem
        {
            get { return _ParentItem; }

            internal set
            {
            	 _ParentItem = value;

                 OnParentItemChanged();
            }
        }

        private void OnParentItemChanged()
        {
        }

        #endregion

        #endregion

        #region Add

        /// <summary>
        /// Adds new object to the collection.
        /// </summary>
        /// <param name="ch">Object to add.</param>
        /// <returns>Index of newly added object.</returns>
        public int Add(GridColumn ch)
        {
            ch.ColumnIndex = List.Add(ch);

            return (ch.ColumnIndex);
        }

        #endregion

        #region Insert

        /// <summary>
        /// Inserts new object into the collection.
        /// </summary>
        /// <param name="index">Position of the object.</param>
        /// <param name="value">Object to insert.</param>
        public void Insert(int index, GridColumn value)
        {
            List.Insert(index, value);
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// Returns index of the object inside of the collection.
        /// </summary>
        /// <param name="value">Reference to the object.</param>
        /// <returns>Index of the object.</returns>
        public int IndexOf(GridColumn value)
        {
            return (List.IndexOf(value));
        }

        #endregion

        #region Contains

        /// <summary>
        /// Returns whether collection contains specified object.
        /// </summary>
        /// <param name="value">Object to look for.</param>
        /// <returns>true if object is part of the collection, otherwise false.</returns>
        public bool Contains(GridColumn value)
        {
            return (List.Contains(value));
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes specified object from the collection.
        /// </summary>
        /// <param name="value"></param>
        public void Remove(GridColumn value)
        {
            List.Remove(value);

            for (int i = 0; i < Count; i++)
                ((GridColumn)List[i]).ColumnIndex = i;
        }

        #endregion

        #region OnRemoveComplete

        /// <summary>
        /// Called when remove of an item is completed.
        /// </summary>
        /// <param name="index">Index of removed item</param>
        /// <param name="value">Removed item.</param>
        protected override void OnRemoveComplete(int index, object value)
        {
            if (value is GridColumn)
            {
                GridColumn column = (GridColumn)value;
                GridPanel panel = column.GridPanel;

                ResetColumn(panel, column);
            }

            OnCollectionChanged(CollectionChangeAction.Remove, value);

            base.OnRemoveComplete(index, value);
        }

        #endregion

        #region Clear

        /// <summary>
        /// Clears all objects from the collection.
        /// </summary>
        public new void Clear()
        {
            foreach (GridColumn column in this)
                ResetColumn(column.GridPanel, column);

            List.Clear();
        }

        #endregion

        #region OnClearComplete

        protected override void OnClearComplete()
        {
            InvalidateDisplayIndexes();
            InvalidateLayout();

            base.OnClearComplete();
        }

        #endregion

        #region ResetColumn

        private void ResetColumn(GridPanel panel, GridColumn column)
        {
            column.DisplayIndexChanged -= ColumnDisplayIndexChanged;
            column.PropertyChanged -= column.StylePropertyChanged;

            if (panel.DataSource != null)
                panel.DataBinder.DataResetCount++;

            GridCell cell = column.SuperGrid.ActiveElement as GridCell;

            if (cell != null)
            {
                if (cell.ColumnIndex == column.ColumnIndex)
                    column.SuperGrid.ActiveElement = null;
            }

            if (panel.SortColumns.Contains(column))
                panel.SortColumns.Remove(column);

            if (panel.GroupColumns.Contains(column))
                panel.GroupColumns.Remove(column);

            FilterPanel fp = column.SuperGrid.ActiveFilterPanel;

            if (fp != null && fp.GridColumn == column)
                fp.EndEdit();
        }

        #endregion

        #region OnInsertComplete

        /// <summary>
        /// Called when insertion of an item is completed.
        /// </summary>
        /// <param name="index">Insert index.</param>
        /// <param name="value">Inserted item.</param>
        protected override void OnInsertComplete(int index, object value)
        {
            if (value is GridColumn)
            {
                GridColumn column = (GridColumn)value;
                column.DisplayIndexChanged += ColumnDisplayIndexChanged;
                column.PropertyChanged += column.StylePropertyChanged;

                column.Parent = _ParentItem;
            }

            OnCollectionChanged(CollectionChangeAction.Add, value);

            base.OnInsertComplete(index, value);
        }

        #endregion

        #region OnCollectionChanged

        private void OnCollectionChanged(
            CollectionChangeAction action, object value)
        {
            for (int i = 0; i < Count; i++)
                ((GridColumn)List[i]).ColumnIndex = i;

            InvalidateDisplayIndexes();
            InvalidateLayout();

            if (CollectionChanged != null)
            {
                CollectionChangeEventArgs e = new
                    CollectionChangeEventArgs(action, value);

                CollectionChanged(this, e);
            }
        }

        #endregion

        #region ColumnDisplayIndexChanged

        private void ColumnDisplayIndexChanged(object sender, EventArgs e)
        {
            InvalidateDisplayIndexes();
            InvalidateLayout();
        }

        #endregion

        #region InvalidateDisplayIndexes

        /// <summary>
        /// Invalidates the display indexes and
        /// causes them to be re-evaluated on next layout.
        /// </summary>
        public void InvalidateDisplayIndexes()
        {
            _IsDisplayIndexValid = false;
        }

        #endregion

        #region InvalidateLayout

        private void InvalidateLayout()
        {
            if (_ParentItem != null)
                _ParentItem.InvalidateLayout();
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Copies collection into the specified array.
        /// </summary>
        /// <param name="array">Array to copy collection to.</param>
        /// <param name="index">Starting index.</param>
        public void CopyTo(GridColumn[] array, int index)
        {
            List.CopyTo(array, index);
        }

        /// <summary>
        /// Copies contained items to the ColumnHeader array.
        /// </summary>
        /// <param name="array">Array to copy to.</param>
        internal void CopyTo(GridColumn[] array)
        {
            List.CopyTo(array, 0);
        }

        #endregion

        #region OnClear

        /// <summary>
        /// Called when collection is cleared.
        /// </summary>
        protected override void OnClear()
        {
            if (Count > 0)
            {
                GridPanel panel = this[0].Parent as GridPanel;

                if (panel != null)
                {
                    panel.SuperGrid.ActiveElement = null;

                    foreach (GridElement item in panel.Rows)
                    {
                        GridRow row = item as GridRow;

                        if (row != null)
                        {
                            foreach (GridCell cell in row.Cells)
                            {
                                cell.EditControl = null;
                                cell.RenderControl = null;
                            }
                        }
                    }
                }
            }

            foreach (GridColumn item in this)
            {
                item.EditControl = null;
                item.RenderControl = null;
            }

            base.OnClear();
        }

        #endregion

        #region DisplayIndexMap

        /// <summary>
        /// A map of display index (key) to index in the column collection
        /// (value). Used to quickly find a column from its display index.
        /// </summary>
        internal int[] DisplayIndexMap
        {
            get
            {
                if (!_IsDisplayIndexValid)
                    UpdateDisplayIndexMap();

                return (_DisplayIndexMap);
            }
        }

        #endregion

        #region GetDisplayIndex

        /// <summary>
        /// Gets the display index for specified column.
        /// </summary>
        /// <param name="column">Column that is part of ColumnHeaderCollection</param>
        /// <returns>Display index or -1 column is not part of this collection.</returns>
        public int GetDisplayIndex(GridColumn column)
        {
            int index = IndexOf(column);

            UpdateDisplayIndexMap();

            for (int i = 0; i < _DisplayIndexMap.Length; i++)
            {
                if (_DisplayIndexMap[i] == index)
                    return (i);
            }

            return (-1);
        }

        #endregion

        #region ColumnAtDisplayIndex

        /// <summary>
        /// Returns the column that is displayed at specified display index.
        /// </summary>
        /// <param name="displayIndex">0 based display index.</param>
        /// <returns>ColumnHeader</returns>
        public GridColumn ColumnAtDisplayIndex(int displayIndex)
        {
            UpdateDisplayIndexMap();

            return (this[_DisplayIndexMap[displayIndex]]);
        }

        #endregion

        #region UpdateDisplayIndexMap

        private void UpdateDisplayIndexMap()
        {
            if (_IsDisplayIndexValid == false)
            {
                _IsDisplayIndexValid = true;

                _DisplayIndexMap = new int[Count];

                for (int i = 0; i < Count; i++)
                    _DisplayIndexMap[i] = -1;

                for (int i = 0; i < Count; i++)
                {
                    int di = this[i].DisplayIndex;

                    if (di != -1)
                        AddIndexToMap(i, di);
                }

                int n = 0;

                for (int i = 0; i < Count; i++)
                {
                    int di = this[i].DisplayIndex;

                    if (di == -1)
                        AddIndexToMap(i, n++);
                }
            }
        }

        private void AddIndexToMap(int i, int di)
        {
            for (int j = 0; j < Count; j++)
            {
                int n = (di + j) % Count;

                if (_DisplayIndexMap[n] == -1)
                {
                    _DisplayIndexMap[n] = i;
                    break;
                }
            }
        }

        #endregion

        #region GetNextVisibleColumn

        /// <summary>
        /// Gets reference to next visible column
        /// or null if there is no next visible column
        /// </summary>
        public GridColumn GetNextVisibleColumn(GridColumn column)
        {
            int[] displayMap = DisplayIndexMap;

            int index = GetDisplayIndex(column);

            while (++index < Count)
            {
                if (this[displayMap[index]].Visible)
                    return (this[displayMap[index]]);
            }

            return (null);
        }

        #endregion

        #region GetPrevVisibleColumn

        /// <summary>
        /// Gets reference to previous visible column
        /// or null if there is no last visible previous column
        /// </summary>
        public GridColumn GetPrevVisibleColumn(GridColumn column)
        {
            int[] displayMap = DisplayIndexMap;

            int index = GetDisplayIndex(column);

            while (--index >= 0)
            {
                if (this[displayMap[index]].Visible)
                    return (this[displayMap[index]]);
            }

            return (null);
        }

        #endregion

        #region GetLastVisibleFrozenColumn

        /// <summary>
        /// Gets reference to the last visible frozen column
        /// or null if there is none
        /// </summary>
        public GridColumn GetLastVisibleFrozenColumn()
        {
            int[] displayMap = DisplayIndexMap;

            if (displayMap.Length > 0)
            {
                GridPanel panel = this[displayMap[0]].GridPanel;

                if (panel != null &&
                    panel.FrozenColumnCount > 0 && panel.IsSubPanel == false)
                {
                    for (int i = displayMap.Length - 1; i >= 0; i--)
                    {
                        GridColumn column = this[displayMap[i]];

                        if (column.Visible == true)
                        {
                            if (i < panel.FrozenColumnCount)
                                return (column);
                        }
                    }
                }
            }

            return (null);
        }

        #endregion
    }
}
