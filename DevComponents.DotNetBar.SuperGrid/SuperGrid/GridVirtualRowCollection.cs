using System;
using System.Drawing;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Defines the collection of Virtual Rows
    /// </summary>
    public class GridVirtualRows
    {
        #region Private Variables

        private GridContainer _Container;

        private const int MaxRows = 50;
        private const int MaxBuckets = 100;

        private int _BucketHead;
        private VPageBucket _LastAccessedBucket;
        private VPageBucket[] _PageBuckets;

        #endregion

        ///<summary>
        /// GridVirtualRowList
        ///</summary>
        ///<param name="container"></param>
        public GridVirtualRows(GridContainer container)
        {
            _Container = container;

            _PageBuckets = new VPageBucket[MaxBuckets];
            _PageBuckets[0] = new VPageBucket(_Container, 0);
        }

        #region Public properties

        #region MaxRowIndex

        ///<summary>
        /// MaxRowIndex
        ///</summary>
        ///<exception cref="Exception"></exception>
        public int MaxRowIndex
        {
            get { return (GetMaxRowIndex()); }
            set { TruncateRowIndex(value); }
        }

        #region TruncateRowIndex

        private void TruncateRowIndex(int index)
        {
            _LastAccessedBucket = null;

            for (int i = 0; i < MaxBuckets; i++)
            {
                int n = (_BucketHead + i) % MaxBuckets;

                if (_PageBuckets[n] != null)
                {
                    if (_PageBuckets[n].StartIndex >= index)
                        _PageBuckets[n] = null;

                    else if (_PageBuckets[n].EndIndex > index)
                    {
                        for (int j = 0; j < MaxRows; j++)
                        {
                            if (_PageBuckets[n].StartIndex + j > index)
                                _PageBuckets[n].Rows[j] = null;
                        }
                    }
                }
            }
        }

        #endregion

        #region GetMaxRowIndex

        private int GetMaxRowIndex()
        {
            int maxIndex = 0;

            for (int i = 0; i < MaxBuckets; i++)
            {
                int n = (_BucketHead + i) % MaxBuckets;

                VPageBucket bucket = _PageBuckets[n];

                if (bucket != null)
                {
                    int index = bucket.StartIndex;

                    if (index >= maxIndex)
                    {
                        foreach (GridRow row in bucket.Rows)
                        {
                            if (row != null)
                            {
                                if (row.GridIndex > maxIndex)
                                    maxIndex = row.GridIndex;
                            }
                        }
                    }
                }
            }

            return (maxIndex);
        }

        #endregion

        #endregion

        #region Row Indexer

        ///<summary>
        /// Row indexer
        ///</summary>
        ///<param name="index"></param>
        public GridRow this[int index]
        {
            get { return (GetRow(index)); }
        }

        #endregion

        #endregion

        #region FindBucket

        private VPageBucket FindBucket(int index)
        {
            if (_LastAccessedBucket != null)
            {
                if (_LastAccessedBucket.Contains(index))
                    return (_LastAccessedBucket);
            }

            for (int i = 0; i < MaxBuckets; i++)
            {
                int n = (_BucketHead + i) % MaxBuckets;

                if (_PageBuckets[n] != null)
                {
                    if (_PageBuckets[n].Contains(index))
                        return (_PageBuckets[n]);
                }
            }

            return (null);
        }

        #endregion

        #region FreeRow

        ///<summary>
        /// FreeRow
        ///</summary>
        ///<param name="index"></param>
        ///<returns></returns>
        ///<exception cref="Exception"></exception>
        public void FreeRow(int index)
        {
            VPageBucket bucket = FindBucket(index);

            if (bucket != null)
            {
                GridRow[] rows = bucket.Rows;
                GridRow row = rows[index - bucket.StartIndex];

                if (row != null)
                {
                    row.Dispose();
                    rows[index - bucket.StartIndex] = null;
                }
            }
        }

        #endregion

        #region IsCachedRow

        ///<summary>
        /// Gets whether the row is currently cached
        ///</summary>
        public bool IsCachedRow(int index)
        {
            if (_LastAccessedBucket != null)
            {
                if (_LastAccessedBucket.Contains(index) == true)
                    return (_LastAccessedBucket.IsCached(index));
            }

            for (int i = 0; i < MaxBuckets; i++)
            {
                int n = (_BucketHead + i) % MaxBuckets;

                if (_PageBuckets[n] != null)
                {
                    if (_PageBuckets[n].Contains(index) == true)
                        return (_PageBuckets[n].IsCached(index));
                }
            }

            return (false);
        }

        #endregion

        #region GetRow

        private GridRow GetRow(int index)
        {
            if (_LastAccessedBucket != null)
            {
                if (_LastAccessedBucket.Contains(index))
                    return (_LastAccessedBucket[index]);
            }

            for (int i = 0; i < MaxBuckets; i++)
            {
                int n = (_BucketHead + i) % MaxBuckets;

                if (_PageBuckets[n] == null)
                    return (NewBucketRow(n, index));

                if (_PageBuckets[n].Contains(index))
                {
                    _LastAccessedBucket = _PageBuckets[n];

                    return (_LastAccessedBucket[index]);
                }
            }

            int bucket = _BucketHead;

            GridPanel panel = _Container.GridPanel;

            if (panel != null)
            {
                for (int i = 0; i < MaxBuckets; i++)
                {
                    bucket = (_BucketHead + i) % MaxBuckets;

                    if (panel.FrozenRowCount > 0)
                    {
                        if (_PageBuckets[bucket].Contains(0))
                            continue;
                    }

                    if (panel.ActiveRow != null)
                    {
                        if (_PageBuckets[bucket].Contains(panel.ActiveRow.Index))
                            continue;
                    }
                    break;
                }
            }

            _BucketHead = (bucket + 1) % MaxBuckets;

            return (NewBucketRow(bucket, index));
        }

        #endregion

        #region NewBucketRow

        private GridRow NewBucketRow(int n, int index)
        {
            int startIndex = (index / MaxRows) * MaxRows;

            if (_PageBuckets[n] != null)
                _PageBuckets[n].Reset(_Container, startIndex);
            else
                _PageBuckets[n] = new VPageBucket(_Container, startIndex);

            _LastAccessedBucket = _PageBuckets[n];

            return (_LastAccessedBucket[index]);
        }

        #endregion

        #region Clear

        ///<summary>
        /// Clear
        ///</summary>
        public void Clear()
        {
            for (int i = 0; i < MaxBuckets; i++)
            {
                if (_PageBuckets[i] != null)
                    _PageBuckets[i].Reset(null, 0);

                _PageBuckets[i] = null;
            }

            _BucketHead = 0;
            _LastAccessedBucket = null;
        }

        #endregion

        #region VPageBucket

        private class VPageBucket
        {
            #region Static data

            static private GridLayoutInfo _layoutInfo = new GridLayoutInfo(null, Rectangle.Empty);
            static private GridLayoutStateInfo _stateInfo = new GridLayoutStateInfo(null, 0);

            #endregion

            #region Private variables

            private GridContainer _Container;
            private int _StartIndex;
            private int _EndIndex;

            private GridRow[] _Rows;

            #endregion

            public VPageBucket(GridContainer container, int startIndex)
            {
                _Container = container;
                _StartIndex = startIndex;
                _EndIndex = startIndex + MaxRows - 1;

                _Rows = new GridRow[MaxRows];
            }

            #region Public properties

            #region EndIndex

            public int EndIndex
            {
                get { return (_EndIndex); }
            }

            #endregion

            #region Row Indexer

            ///<summary>
            /// Row indexer
            ///</summary>
            ///<param name="index"></param>
            public GridRow this[int index]
            {
                get { return (GetRow(index)); }
            }

            #endregion

            #region Rows

            public GridRow[] Rows
            {
                get { return (_Rows); }
            }

            #endregion

            #region StartIndex

            public int StartIndex
            {
                get { return (_StartIndex); }
            }

            #endregion

            #endregion

            #region Contains

            public bool Contains(int index)
            {
                return (index >= _StartIndex && index <= _EndIndex);
            }

            #endregion

            #region IsCached

            public bool IsCached(int index)
            {
                if (Contains(index) == true)
                    return (_Rows[index - _StartIndex] != null);

                return (false);
            }

            #endregion

            #region GetRow

            private GridRow GetRow(int index)
            {
                int n = index - _StartIndex;

                if (_Rows[n] == null)
                    _Rows[n] = AllocNewGridRow(index);

                GridRow row = _Rows[n];

                if (row.ArrangeLayoutCount != row.SuperGrid.ArrangeLayoutCount)
                    ArrangeRow(row.GridPanel, row);

                return (row);
            }

            #endregion

            #region Reset

            public void Reset(GridContainer container, int startIndex)
            {
                for (int i = 0; i < MaxRows; i++)
                {
                    GridRow row = _Rows[i];

                    if (row != null)
                    {
                        row.Dispose();

                        _Rows[i] = null;
                    }
                }

                _Container = container;
                _StartIndex = startIndex;
                _EndIndex = startIndex + MaxRows - 1;
            }

            #endregion

            #region AllocNewGridRow

            private GridRow AllocNewGridRow(int index)
            {
                GridPanel panel = _Container.GridPanel;

                if (panel != null)
                {
                    GridRow row = new GridRow();

                    row.Parent = _Container;

                    row.GridIndex = index;
                    row.Index = index;
                    row.RowIndex = index;
                    row.FullIndex = index;

                    row.Loading = true;

                    try
                    {
                        GridColumnCollection columns = panel.Columns;

                        if (columns.Count > 0)
                        {
                            for (int i = 0; i < columns.Count; i++)
                            {
                                GridColumn col = panel.Columns[i];

                                GridCell cell = new GridCell(col.DefaultNewRowCellValue);

                                cell.Parent = row;
                                cell.ColumnIndex = i;

                                row.Cells.Add(cell);
                            }
                        }

                        ArrangeRow(panel, row);

                        if (columns.Count > 0)
                            _Container.SuperGrid.DoLoadVirtualRowEvent(panel, row);
                    }
                    finally
                    {
                        row.Loading = false;
                    }

                    return (row);
                }

                return (null);
            }

            #region ArrangeRow

            private void ArrangeRow(GridPanel panel, GridRow row)
            {
                Rectangle r = panel.BoundsRelative;

                r.Y += (panel.FixedRowHeight - (panel.FrozenRowCount * panel.VirtualRowHeight));
                r.Y += (row.Index * panel.VirtualRowHeight);

                r.Size = new Size(panel.ColumnHeader.Size.Width, panel.VirtualRowHeight);

                _layoutInfo.ClientBounds = r;
                _stateInfo.GridPanel = panel;

                row.FixedRowHeight = r.Size.Height;
                row.Arrange(_layoutInfo, _stateInfo, r);
            }

            #endregion

            #endregion
        }

        #endregion
    }
}
