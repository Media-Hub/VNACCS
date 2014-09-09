using System;
using System.Collections.Generic;
using System.Threading;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// FilterScan
    ///</summary>
    public class FilterScan
    {
        #region Events

        ///<summary>
        /// ScanComplete
        ///</summary>
        public event EventHandler ScanComplete;

        private delegate void ScanListDelegate(List<object> items);

        #endregion

        #region Private variables

        private GridColumn _GridColumn;

        private bool _Scanning;
        private Thread _ScanThread;
        private EventHandler _OnScanComplete;
        private ScanListDelegate _ScanListDelegate;
        private List<object> _ScanItems;

        #endregion

        #region Public properties

        ///<summary>
        /// ScanItems
        ///</summary>
        public List<object> ScanItems
        {
            get { return (_ScanItems); }
        }

        #endregion

        ///<summary>
        /// FilterScan
        ///</summary>
        ///<param name="gridColumn"></param>
        public FilterScan(GridColumn gridColumn)
        {
            _GridColumn = gridColumn;

            _OnScanComplete = new EventHandler(OnScanComplete);
            _ScanListDelegate = new ScanListDelegate(AddScanItems);
        }

        #region BeginScan

        ///<summary>
        /// BeginScan
        ///</summary>
        public void BeginScan()
        {
            if (_Scanning == false)
            {
                _Scanning = true;
                _ScanItems = null;

                _ScanThread = new Thread(ScanThread);
                _ScanThread.Start();
            }
        }

        #endregion

        #region EndScan

        ///<summary>
        /// BeginScan
        ///</summary>
        public void EndScan()
        {
            if (_Scanning == true)
            {
                if (_ScanThread.IsAlive)
                {
                    _ScanThread.Abort();
                    _ScanThread.Join();
                }

                _ScanThread = null;
                _ScanItems = null;

                _Scanning = false;
            }
        }

        #endregion

        #region ScanThread

        private void ScanThread()
        {
            GridColumn gridColumn = _GridColumn;
            GridPanel panel = gridColumn.GridPanel;

            List<object> scanItems = new List<object>();

            try
            {
                if (panel != null)
                {
                    ScanColumn(panel.Rows, gridColumn, scanItems);

                    RemoveDuplicates(scanItems);

                    gridColumn.SuperGrid.BeginInvoke(
                        _ScanListDelegate, new object[] { scanItems });
                }
            }
            finally
            {
                _Scanning = false;

                gridColumn.SuperGrid.BeginInvoke(
                    _OnScanComplete, new object[] { this, EventArgs.Empty });
            }
        }

        #region ScanColumn

        private void ScanColumn(
            GridItemsCollection rows, GridColumn gridColumn, List<object> scanItems)
        {
            if (rows != null && rows.Count > 0)
            {
                for (int i = 0; i < rows.Count; i++)
                {
                    GridContainer cont = rows[i] as GridContainer;

                    if (cont != null)
                    {
                        GridRow row = cont as GridRow;

                        if (row != null)
                        {
                            GridCell cell = row.GetCell(gridColumn.ColumnIndex);

                            if (cell != null)
                            {
                                object value = cell.ValueEx;

                                if (value != null && value != DBNull.Value)
                                {
                                    if (value is DateTime)
                                        value = ((DateTime) value).Date;

                                    scanItems.Add(value);
                                }
                            }
                        }

                        if (cont is GridPanel == false)
                            ScanColumn(cont.Rows, gridColumn, scanItems);
                    }
                }
            }
        }

        #endregion

        #region RemoveDuplicates

        private void RemoveDuplicates(List<object> scanItems)
        {
            scanItems.Sort();

            object o = null;

            for (int i = scanItems.Count - 1; i >= 0; --i)
            {
                if (scanItems[i].Equals(o) == true)
                    scanItems.RemoveAt(i);
                else
                    o = scanItems[i];
            }
        }

        #endregion

        #endregion

        #region AddScanItems

        private void AddScanItems(List<object> items)
        {
            _ScanItems = items;
        }

        #endregion

        #region OnScanComplete

        private void OnScanComplete(object sender, EventArgs e)
        {
            if (ScanComplete != null)
                ScanComplete(sender, e);
        }

        #endregion
    }
}
