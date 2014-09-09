using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlTypes;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// DataBinding helper class
    ///</summary>
    public class DataBinder : IDisposable
    {
        #region Private variables

        private readonly GridPanel _Panel;

        private CurrencyManager _CurrencyManager;
        private PropertyDescriptorCollection _Pdc;
        private DataView _DataView;

        private BindingSource _BindingSource;
        private BindingSource _BaseBindingSource;
        private object _BaseDataSource;
        private string _BaseDataMember;

        private bool _ConnectionInProgress;
        private bool _NotificationInProgress;
        private bool _UpdateInProgress;
        private bool _UpdatePosInProgress;

        private int _DataResetCount;
        private int _BeginUpdateCount;

        #endregion

        ///<summary>
        /// DataBinding
        ///</summary>
        ///<param name="panel"></param>
        internal DataBinder(GridPanel panel)
        {
            _Panel = panel;
        }

        #region Internal properties

        #region BaseDataMember

        internal string BaseDataMember
        {
            get { return (_BaseDataMember); }
            set { _BaseDataMember = value; }
        }

        #endregion

        #region BaseDataSource

        internal object BaseDataSource
        {
            get { return (_BaseDataSource); }
            set { _BaseDataSource = value; }
        }

        #endregion

        #region CanDeleteRow

        internal bool CanDeleteRow
        {
            get
            {
                if (CurrencyManager != null)
                {
                    if (CurrencyManager.List.Count <= 0)
                        return (false);

                    DataRowView drv = CurrencyManager.List[0] as DataRowView;

                    if (drv != null)
                        return (drv.DataView.AllowDelete);
                }

                return (true);
            }
        }

        #endregion

        #region CanInsertRow

        internal bool CanInsertRow
        {
            get
            {
                if (CurrencyManager != null)
                {
                    if (CurrencyManager.List.Count <= 0)
                        return (false);

                    DataRowView drv = CurrencyManager.List[0] as DataRowView;

                    if (drv != null)
                        return (drv.DataView.AllowNew);
                }

                return (true);
            }
        }

        #endregion

        #region ConnectionInProgress

        internal bool ConnectionInProgress
        {
            get { return (_ConnectionInProgress); }
            set { _ConnectionInProgress = value; }
        }

        #endregion

        #region CurrencyManager

        internal CurrencyManager CurrencyManager
        {
            get { return (_CurrencyManager); }

            set
            {
                if (_CurrencyManager != value)
                {
                    if (_CurrencyManager != null)
                    {
                        _CurrencyManager.ListChanged -= CmDataSetListChanged;
                        _CurrencyManager.PositionChanged -= CmPositionChanged;
                        _CurrencyManager.MetaDataChanged -= CurrencyManagerMetaDataChanged;

                        _Panel.SuperGrid.LoadVirtualRow -= SuperGridLoadVirtualRow;
                    }

                    _CurrencyManager = value;

                    if (_BaseBindingSource != null)
                        _BaseBindingSource.PositionChanged -= BsPositionChanged;

                    _BaseBindingSource = null;

                    if (_CurrencyManager != null)
                    {
                        _Pdc = CurrencyManager.GetItemProperties();

                        if (_Panel.IsSubPanel == false)
                            _CurrencyManager.PositionChanged += CmPositionChanged;

                        _BaseBindingSource = _Panel.DataSource as BindingSource;

                        if (_BaseBindingSource != null)
                            _BaseBindingSource.PositionChanged += BsPositionChanged;

                        _CurrencyManager.MetaDataChanged += CurrencyManagerMetaDataChanged;

                        if (_Panel.VirtualMode == true)
                        {
                            _Panel.VirtualRowCount = _CurrencyManager.Count;

                            _Panel.SuperGrid.LoadVirtualRow += SuperGridLoadVirtualRow;
                        }
                    }
                    else
                    {
                        _Pdc = null;
                    }
                }
            }
        }

        #region CurrencyManagerMetaDataChanged

        void CurrencyManagerMetaDataChanged(object sender, EventArgs e)
        {
            if (CurrencyManager != null)
            {
                _Pdc = CurrencyManager.GetItemProperties();

                _Panel.InvalidateLayout();
            }
        }

        #endregion

        #endregion

        #region DataResetCount

        internal int DataResetCount
        {
            get { return (_DataResetCount); }

            set
            {
                _DataResetCount = value;

                if (_Panel.VirtualMode == true)
                {
                    _Panel.VirtualRows.Clear();
                }
                else
                {
                    _Panel.NeedToUpdateBindings = true;
                    _Panel.NeedToUpdateDataFilter = true;
                }

                _Panel.UnDeleteAll();
                _Panel.InvalidateLayout();
            }
        }

        #endregion

        #region DataView

        internal DataView DataView
        {
            get { return (_DataView); }
            set { _DataView = value; }
        }

        #endregion

        #region GetValue

        internal object GetValue(GridCell cell, object value)
        {
            if (cell.GridRow.IsTempInsertRow == false)
            {
                if (CurrencyManager != null && _Pdc != null)
                {
                    if ((uint)cell.GridRow.DataItemIndex < CurrencyManager.List.Count)
                    {
                        GridColumn col = _Panel.Columns[cell.ColumnIndex];
                        PropertyDescriptor pd = FindPropertyDescriptor(col);

                        if (pd != null)
                        {
                            int index = cell.GridRow.DataItemIndex;

                            if (_Panel.VirtualMode == true && _Panel.VirtualTempInsertRow != null)
                            {
                                if (index > _Panel.VirtualTempInsertRow.RowIndex)
                                    index--;
                            }

                            return (pd.GetValue(CurrencyManager.List[index]));
                        }
                    }
                }
            }

            return (value);
        }

        #endregion

        #region IsSortable

        internal bool IsSortable
        {
            get
            {
                if (_BaseDataSource is DataSet)
                    return (true);

                if (_BaseDataSource is IBindingList)
                {
                    IBindingList list = (IBindingList)_BaseDataSource;

                    return (list.SupportsSorting);
                }
                
                if (_BaseDataSource is IListSource)
                {
                    IList list2 = ((IListSource)_BaseDataSource).GetList();

                    if (list2 is IBindingList)
                        return (((IBindingList)list2).SupportsSorting);
                }

                return (false);
            }
        }

        #endregion

        #region IsUpdateSuspended

        internal bool IsUpdateSuspended
        {
            get { return (_BeginUpdateCount > 0); }
        }

        #endregion

        #region NotificationInProgress

        internal bool NotificationInProgress
        {
            get { return (_NotificationInProgress); }
            set { _NotificationInProgress = value; }
        }

        #endregion

        #region Pdc

        internal PropertyDescriptorCollection Pdc
        {
            get { return (_Pdc); }
            set { _Pdc = value; }
        }

        #endregion

        #region RowCount

        internal int RowCount
        {
            get
            {
                if (CurrencyManager != null)
                    return (CurrencyManager.Count);

                return (0);
            }
        }

        #endregion

        #region SetValue

        internal void SetValue(GridCell cell, object value)
        {
            if (cell.GridRow.IsTempInsertRow == false)
            {
                if (CurrencyManager != null && _Pdc != null)
                {
                    if ((uint) cell.GridRow.DataItemIndex < CurrencyManager.List.Count)
                    {
                        GridColumn col = _Panel.Columns[cell.ColumnIndex];
                        PropertyDescriptor pd = FindPropertyDescriptor(col);

                        if (pd != null)
                        {
                            if (pd.IsReadOnly == false)
                            {
                                pd.SetValue(
                                    CurrencyManager.List[cell.GridRow.DataItemIndex], value);

                                UpdatePosInProgress = true;

                                CurrencyManager.EndCurrentEdit();

                                UpdatePosInProgress = false;
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region UpdateInProgress

        internal bool UpdateInProgress
        {
            get { return (_UpdateInProgress); }
            set { _UpdateInProgress = value; }
        }

        #endregion

        #region UpdatePosInProgress

        internal bool UpdatePosInProgress
        {
            get { return (_UpdatePosInProgress); }
            set { _UpdatePosInProgress = value; }
        }

        #endregion

        #endregion

        #region Clear

        internal void Clear()
        {
            _Panel.SuperGrid.BeforeExpand -= SuperGridBeforeExpand;

            CurrencyManager = null;

            _Panel.ActiveRow = null;

            if (_Panel.VirtualMode == true)
            {
                _Panel.VirtualTempInsertRow = null;
                _Panel.VirtualRows.Clear();
            }
        }

        #endregion

        #region DataConnect

        internal void DataConnect()
        {
            _BaseDataSource = _Panel.DataSource;
            _BaseDataMember = _Panel.DataMember;

            if (_BaseDataSource != null)
            {
                _BindingSource = _BaseDataSource as BindingSource;

                if (_BindingSource != null)
                {
                    if (_BindingSource.DataSource is BindingSource)
                    {
                        if (string.IsNullOrEmpty(_BindingSource.DataMember) == false)
                            _BaseDataMember = _BindingSource.DataMember;
                        else
                            _BindingSource = null;

                        DataSet dss = _BaseDataSource as DataSet;

                        if (dss != null)
                        {
                            DataRelation dsr = dss.Relations[_BaseDataMember];

                            if (dsr != null)
                                _BaseDataMember = dsr.ChildTable.TableName;
                        }
                    }
                    else
                    {
                        _BaseDataSource = _BindingSource.DataSource;
                        _BaseDataMember = _BindingSource.DataMember;
                    }
                }

                if (_BaseDataSource is DataSet)
                    AddDataSetTable((DataSet)_BaseDataSource, _BaseDataMember);

                else if (_BaseDataSource is IBindingList)
                    AddIBindingList((IBindingList)_BaseDataSource);

                else if (_BaseDataSource is IList)
                    AddIList((IList)_BaseDataSource);

                else if (_BaseDataSource is IListSource)
                    AddIList(((IListSource)_BaseDataSource).GetList());
            }
        }

        #endregion

        #region SetDataConnection

        private bool SetDataConnection(object dataSource)
        {
            if (ConnectionInProgress == false)
            {
                ClearDataNotification(dataSource);

                if (_BindingSource != null || _Panel.SuperGrid.BindingContext != null)
                {
                    ConnectionInProgress = true;

                    try
                    {
                        SetDataNotification(dataSource);
                    }
                    finally
                    {
                        ConnectionInProgress = false;
                    }
                }
            }

            return (NotificationInProgress == false);
        }

        #endregion

        #region SetDataNotification

        private void SetDataNotification(object dataSource)
        {
            ISupportInitializeNotification notification =
                dataSource as ISupportInitializeNotification;

            if (notification == null || notification.IsInitialized == true)
            {
                CurrencyManager = (_BindingSource != null)
                    ? _BindingSource.CurrencyManager
                    : _Panel.SuperGrid.BindingContext[DataView ?? dataSource] as CurrencyManager;
            }
            else
            {
                CurrencyManager = null;

                if (NotificationInProgress == false)
                {
                    notification.Initialized += DataSourceInitialized;
                    NotificationInProgress = true;
                }
            }
        }

        #endregion

        #region ClearDataNotification

        private void ClearDataNotification(object dataSource)
        {
            ISupportInitializeNotification notification =
                dataSource as ISupportInitializeNotification;

            if (notification != null && NotificationInProgress == true)
            {
                notification.Initialized -= DataSourceInitialized;

                NotificationInProgress = false;
            }
        }

        #endregion

        #region DataSourceInitialized

        private void DataSourceInitialized(object sender, EventArgs e)
        {
            ISupportInitializeNotification dataSource =
                _Panel.DataSource as ISupportInitializeNotification;

            if (dataSource != null)
            {
                NotificationInProgress = false;

                dataSource.Initialized -= DataSourceInitialized;

                CurrencyManager =
                    _Panel.SuperGrid.BindingContext[dataSource] as CurrencyManager;
            }
        }

        #endregion

        #region CmPositionChanged

        void CmPositionChanged(object sender, EventArgs e)
        {
            if (IsUpdateSuspended == false && UpdatePosInProgress == false)
            {
                CurrencyManager cm = sender as CurrencyManager;

                if (cm != null && cm == _CurrencyManager)
                    UpdatePosition(cm.Position);
            }
        }

        #region BsPositionChanged

        void BsPositionChanged(object sender, EventArgs e)
        {
            if (_UpdatePosInProgress == false)
            {
                BindingSource bs = sender as BindingSource;

                if (bs != null)
                    UpdatePosition(bs.Position);
            }
        }

        #endregion

        #region UpdatePosition

        private void UpdatePosition(int position)
        {
            if (_Panel.IsSubPanel == false)
            {
                GridRow row = FindDataGridRowIndex(position);

                if (row != null && _Panel.ActiveRow != row)
                {
                    try
                    {
                        _UpdatePosInProgress = true;

                        if (row.Visible == false)
                            row.Visible = true;

                        if (_Panel.AutoSelectNewBoundRows == true)
                        {
                            _Panel.ClearAll();

                            _Panel.SetActiveRow(row, true);
                            _Panel.SetSelectedRows(row.GridIndex, 1, true);

                            bool skey = (_Panel.MultiSelect == true) ?
                                ((Control.ModifierKeys & Keys.Shift) == Keys.Shift) : false;

                            if (skey == false || _Panel.SelectionRowAnchor == null) 
                                _Panel.SelectionRowAnchor = row;

                            row.EnsureVisible();
                        }
                    }
                    finally
                    {
                        _UpdatePosInProgress = false;
                    }
                }
            }
        }

        #endregion

        #endregion

        #region SetPosition

        internal bool SetPosition(GridRow row)
        {
            if (_UpdatePosInProgress == false)
            {
                if (row != null && row.IsTempInsertRow == false)
                {
                    if (CurrencyManager != null)
                    {
                        if (SetCmPosition(row) == false)
                            return (false);

                        _Panel.ActiveRow = row;

                        if (_Panel.IsSubPanel == false)
                        {
                            if (_BaseBindingSource != null)
                                _BaseBindingSource.Position = CurrencyManager.Position;
                        }
                    }
                }
            }

            return (true);
        }

        #region SetCmPosition

        private bool SetCmPosition(GridRow row)
        {
            if (CurrencyManager == null)
                return (false);

            while (true)
            {
                _UpdatePosInProgress = true;

                try
                {
                    CurrencyManager.Position = row.DataItemIndex;
                    break;
                }
                catch (Exception exp)
                {
                    GridRow crow = null;

                    if ((uint)CurrencyManager.Position < _Panel.Rows.Count)
                    {
                        crow = _Panel.Rows[CurrencyManager.Position] as GridRow;

                        if (crow != null)
                            ReloadRow(crow);
                    }

                    bool retry = false;
                    bool throwException = false;

                    if (_Panel.SuperGrid.HasDataErrorHandler == true)
                    {
                        object value = crow;

                        if (_Panel.SuperGrid.DoDataErrorEvent(_Panel, null, exp,
                            DataContext.SetRowPosition, ref value, ref throwException, ref retry) == true)
                        {
                            return (false);
                        }
                    }

                    if (throwException == true)
                        throw;

                    if (retry == false)
                        return (false);
                }
                finally
                {
                    _UpdatePosInProgress = false;
                }
            }

            return (true);
        }

        #endregion

        #endregion

        #region AddIList

        #region AddIList

        internal void AddIList(IList list)
        {
            if (SetDataConnection(list) == true)
            {
                if (_Pdc != null)
                {
                    if (_Panel.AutoGenerateColumns == true)
                        AutoGenerateColumns();

                    if (_Panel.VirtualMode == false)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            object o = list[i];

                            GridRow row = GetNewRow(o);
                            row.DataItem = o;
                            row.DataItemIndex = i;

                            _Panel.Rows.Add(row);
                        }
                    }

                    _Panel.SuperGrid.DoDataBindingCompleteEvent(_Panel);
                }

                if (CurrencyManager != null)
                {
                    CurrencyManager.ListChanged -= CurrencyManagerListChanged;
                    CurrencyManager.ListChanged += CurrencyManagerListChanged;
                }
            }
        }

        #endregion

        #region AutoGenerateColumns

        private void AutoGenerateColumns()
        {
            if (_Pdc != null)
            {
                foreach (PropertyDescriptor pd in _Pdc)
                {
                    if (pd.PropertyType.IsInterface == false)
                    {
                        if (FindGridColumn(_Panel, pd.Name) == null)
                        {
                            GridColumn gcol = new GridColumn();

                            gcol.DataPropertyName = pd.Name;
                            gcol.Name = pd.Name;
                            gcol.Width = 100;

                            SetColumnEditor(pd.PropertyType, gcol);

                            _Panel.Columns.Add(gcol);
                        }
                    }
                }
            }
        }

        #endregion

        #region GetNewRow

        private GridRow GetNewRow(object o)
        {
            GridRow row = new GridRow();

            foreach (GridColumn col in _Panel.Columns)
            {
                PropertyDescriptor pd = FindPropertyDescriptor(col);

                object value = (pd != null)
                    ? pd.GetValue(o) : null;

                row.Cells.Add(new GridCell(value));
            }

            return (row);
        }

        #endregion

        #region FindPropertyDescriptor

        private PropertyDescriptor FindPropertyDescriptor(GridColumn col)
        {
            if (_Pdc != null)
            {
                foreach (PropertyDescriptor pd in _Pdc)
                {
                    if (string.IsNullOrEmpty(col.DataPropertyName) == true)
                    {
                        if (pd.Name.Equals(col.Name) == true)
                            return (pd);
                    }
                    else
                    {
                        if (pd.Name.Equals(col.DataPropertyName) == true)
                            return (pd);
                    }
                }
            }

            return (null);
        }

        #endregion

        #endregion

        #region AddIBindingList

        internal void AddIBindingList(IBindingList list)
        {
            if (SetDataConnection(list) == true)
            {                
                if (_Pdc != null)
                {
                    if (_Panel.AutoGenerateColumns == true)
                        AutoGenerateColumns();

                    if (_Panel.VirtualMode == false)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            object o = list[i];

                            GridRow row = GetNewRow(o);
                            row.DataItem = o;
                            row.DataItemIndex = i;

                            _Panel.Rows.Add(row);
                        }
                    }

                    UpdateSuperGridSort(false);

                    _Panel.SuperGrid.DoDataBindingCompleteEvent(_Panel);

                    if (_Panel.ShowInsertRow == true)
                    {
                        _Panel.ShowInsertRow = false;
                        _Panel.ShowInsertRow = true;
                    }
                }

                if (CurrencyManager != null)
                {
                    CurrencyManager.ListChanged -= CurrencyManagerListChanged;
                    CurrencyManager.ListChanged += CurrencyManagerListChanged;
                }
            }
        }

        #endregion

        #region AddDataSetTable

        internal void AddDataSetTable(DataSet dataSet, string dataMember)
        {
            if (string.IsNullOrEmpty(dataMember) && dataSet.Tables.Count > 0)
                dataMember = dataSet.Tables[0].TableName;

            if (string.IsNullOrEmpty(dataMember) == false)
            {
                DataTable dt = dataSet.Tables[dataMember];

                bool autogen = _Panel.AutoGenerateColumns;
                ProcessChildRelations crVisibility = _Panel.ProcessChildRelations;

                if (_Panel.SuperGrid.DoDataBindingStartEvent(
                    _Panel, null, dt.TableName, ref autogen, ref crVisibility) == false)
                {
                    AddDataTable(dt, autogen, crVisibility);
                }
            }
        }

        #endregion

        #region AddDataTable

        internal void AddDataTable(
            DataTable dt, bool autogen, ProcessChildRelations crProcess)
        {
            if (SetDataConnection(dt) == true)
            {
                if (CurrencyManager != null)
                {
                    CurrencyManager.ListChanged -= CmDataSetListChanged;
                    CurrencyManager.ListChanged += CmDataSetListChanged;

                    if (autogen == true)
                        AutoGenerateColumns(dt);

                    if (_Panel.VirtualMode == false)
                    {
                        int pindex = (crProcess != ProcessChildRelations.Never) ? GetPrimaryColumnIndex(dt) : -1;

                        bool rowsUnresolved = (pindex >= 0 && dt.ChildRelations.Count > 0);

                        for (int i = 0; i < CurrencyManager.Count; i++)
                        {
                            DataRowView view = CurrencyManager.List[i] as DataRowView;

                            if (view != null)
                            {
                                GridRow row = GetNewRow(dt, view);
                                row.DataItem = view;
                                row.DataItemIndex = i;

                                row.RowsUnresolved = rowsUnresolved;

                                _Panel.Rows.Add(row);
                            }
                        }

                        if (rowsUnresolved == true)
                        {
                            _Panel.PrimaryColumnIndex = pindex;

                            _Panel.ShowTreeButtons = true;
                            _Panel.ShowTreeLines = true;

                            _Panel.SuperGrid.BeforeExpand -= SuperGridBeforeExpand;
                            _Panel.SuperGrid.BeforeExpand += SuperGridBeforeExpand;
                        }
                    }
                    else
                    {
                        dt.DefaultView.RowFilter = "";
                    }

                    UpdateSuperGridSort(false);

                    _Panel.SuperGrid.DoDataBindingCompleteEvent(_Panel);
                }
            }
        }

        #endregion

        #region GetPrimaryColumnIndex

        private int GetPrimaryColumnIndex(DataTable dt)
        {
            if (dt.ChildRelations.Count > 0)
            {
                DataRelation dr = dt.ChildRelations[0];

                if (dr.ParentColumns.Length > 0)
                {
                    GridColumn col =
                        FindGridColumn(_Panel, dr.ParentColumns[0].ColumnName);

                    if (col != null)
                        return (col.ColumnIndex);
                }
            }

            return (-1);
        }

        #endregion

        #region CmDataSetListChanged

        void CmDataSetListChanged(object sender, ListChangedEventArgs e)
        {
            if (IsUpdateSuspended == false && _UpdateInProgress == false)
            {
                CurrencyManager cm = sender as CurrencyManager;

                if (cm != null && cm == _CurrencyManager)
                {
                    switch (e.ListChangedType)
                    {
                        case ListChangedType.ItemMoved:
                            MoveDataGridRow(e.OldIndex, e.NewIndex);
                            break;

                        case ListChangedType.ItemDeleted:
                            DeleteDataGridRow(e.NewIndex);
                            break;

                        case ListChangedType.ItemChanged:
                            UpdateDataGridRow(CurrencyManager.List, e.NewIndex);
                            break;

                        case ListChangedType.ItemAdded:
                            AddDataGridRow(CurrencyManager.List, e.NewIndex);
                            break;

                        case ListChangedType.Reset:
                            DataResetCount++;
                            UpdateSuperGridSort(true);
                            break;
                    }
                }
            }
        }

        #endregion

        #region AutoGenerateColumns

        private void AutoGenerateColumns(DataTable dt)
        {
            foreach (DataColumn dtColumn in dt.Columns)
            {
                if (FindGridColumn(_Panel, dtColumn.ColumnName) == null)
                {
                    GridColumn gcol = new GridColumn();

                    gcol.DataPropertyName = dtColumn.ColumnName;
                    gcol.Name = dtColumn.ColumnName;
                    gcol.Width = 100;

                    gcol.DefaultNewRowCellValue = dtColumn.DefaultValue;
                    gcol.ReadOnly = dtColumn.ReadOnly;

                    if (String.IsNullOrEmpty(dtColumn.Caption) == false)
                        gcol.HeaderText = dtColumn.Caption;

                    SetColumnEditor(dtColumn.DataType, gcol);

                    _Panel.Columns.Add(gcol);
                }
            }
        }

        #region SetColumnEditor

        private void SetColumnEditor(Type type, GridColumn col)
        {
            col.DataType = type;

            if (type == typeof(String) || type == typeof(SqlString))
            {
                col.EditorType = typeof(GridTextBoxXEditControl);
            }
            else if (type == typeof(Boolean) || type == typeof(SqlBoolean))
            {
                col.EditorType = typeof(GridCheckBoxXEditControl);
            }
            else if (type == typeof(DateTime) || type == typeof(SqlDateTime)
                || type == typeof(TimeSpan))
            {
                col.EditorType = typeof(GridDateTimeInputEditControl);
            }
            else if (type == typeof(Decimal) || type == typeof(SqlDecimal) ||
                type == typeof(Double) || type == typeof(SqlDouble) ||
                type == typeof(Single) || type == typeof(SqlSingle))
            {
                col.EditorType = typeof(GridDoubleInputEditControl);
            }
            else if (type == typeof(Int64) || type == typeof(SqlInt64))
            {
                col.EditorType = typeof(GridDoubleIntInputEditControl);
            }
            else if (type == typeof(Int16) || (type == typeof(SqlInt16) ||
                type == typeof(Int32) || type == typeof(SqlInt32)))
            {
                col.EditorType = typeof(GridIntegerInputEditControl);
            }
        }

        #endregion

        #endregion

        #region GetNewRow

        private GridRow GetNewRow(DataTable dt, DataRowView view)
        {
            GridRow row = new GridRow();

            for (int i = 0; i < _Panel.Columns.Count; i++)
            {
                GridColumn col = _Panel.Columns[i];
                DataColumn dtColumn = FindDataColumn(dt, col);

                object o = (dtColumn != null)
                    ? view.Row.ItemArray[dtColumn.Ordinal] : null;

                row.Cells.Add(new GridCell(o));
            }

            return (row);
        }

        #endregion

        #region FindDataColumn

        private DataColumn FindDataColumn(DataTable dt, GridColumn col)
        {
            foreach (DataColumn dtColumn in dt.Columns)
            {
                if (string.IsNullOrEmpty(col.DataPropertyName) == true)
                {
                    if (dtColumn.ColumnName.Equals(col.Name) == true)
                        return (dtColumn);
                }
                else
                {
                    if (dtColumn.ColumnName.Equals(col.DataPropertyName) == true)
                        return (dtColumn);
                }
            }

            return (null);
        }

        #endregion

        #region FindGridColumn

        private GridColumn FindGridColumn(GridPanel panel, string name)
        {
            foreach (GridColumn col in panel.Columns)
            {
                if (string.IsNullOrEmpty(col.DataPropertyName) == true)
                {
                    if (name.Equals(col.Name) == true)
                        return (col);
                }
                else
                {
                    if (name.Equals(col.DataPropertyName) == true)
                        return (col);
                }
            }

            return (null);
        }

        #endregion

        #region SuperGridBeforeExpand

        void SuperGridBeforeExpand(object sender, GridBeforeExpandEventArgs e)
        {
            if (e.GridPanel == _Panel)
            {
                if (e.GridContainer.RowsUnresolved == true)
                {
                    e.GridContainer.RowsUnresolved = false;

                    GridRow row = e.GridContainer as GridRow;

                    if (row != null)
                    {
                        DataRowView view = row.DataItem as DataRowView;

                        if (view != null)
                        {
                            DataTable dt = view.DataView.Table;

                            StringBuilder sb = new StringBuilder();

                            foreach (DataRelation dr in dt.ChildRelations)
                                CreateNestedPanel(e.GridPanel, row, dr, sb);
                        }
                    }
                }
            }
        }

        #region CreateNestedPanel

        private void CreateNestedPanel(GridPanel panel,
            GridRow row, DataRelation dr, StringBuilder sb)
        {
            DataTable ct = dr.ChildTable;

            bool autogen = true;
            ProcessChildRelations crProcess = _Panel.ProcessChildRelations;

            if (_Panel.SuperGrid.DoDataBindingStartEvent(
                _Panel, row, ct.TableName, ref autogen, ref crProcess) == false)
            {
                sb.Length = 0;

                for (int i=0; i<dr.ParentColumns.Length; i++)
                {
                    DataColumn col = dr.ParentColumns[i];

                    GridColumn gc =
                        FindGridColumn(panel, col.ColumnName);

                    if (gc != null)
                    {
                        if (sb.Length > 0)
                            sb.Append(" AND ");

                        sb.Append("[" + dr.ChildColumns[i] + "]=");
                        sb.Append("'" + row.Cells[gc.ColumnIndex].Value + "'");
                    }
                }

                string t = sb.ToString();

                DataView view = new
                    DataView(ct, t, "", DataViewRowState.CurrentRows);

                if (view.Count > 0 ||
                    _Panel.ProcessChildRelations == ProcessChildRelations.Always)
                {
                    GridPanel ipanel = new GridPanel();

                    ipanel.Name = ct.TableName;
                    ipanel.AutoGenerateColumns = autogen;

                    ipanel.SetDataSource(panel.DataSource);
                    ipanel.SetDataMember(ct.TableName);

                    ipanel.DataBinder.DataView = view;

                    ipanel.DataBinder.BaseDataSource = ipanel.DataSource;
                    ipanel.DataBinder.BaseDataMember = ipanel.DataMember;

                    row.Rows.Add(ipanel);

                    ipanel.DataBinder.AddDataTable(ct, autogen, crProcess);
                }
                else
                {
                    view.Dispose();
                }
            }
        }

        #endregion

        #endregion

        #region CurrencyManagerListChanged

        void CurrencyManagerListChanged(object sender, ListChangedEventArgs e)
        {
            if (UpdateInProgress == false)
            {
                UpdateInProgress = true;

                CurrencyManager cm = sender as CurrencyManager;

                if (cm != null && cm == _CurrencyManager)
                {
                    IList list = cm.List;

                    switch (e.ListChangedType)
                    {
                        case ListChangedType.ItemChanged:
                            UpdateDataGridRow(list, e.NewIndex);
                            break;

                        case ListChangedType.ItemAdded:
                            AddDataGridRow(list, e.NewIndex);
                            break;

                        case ListChangedType.ItemDeleted:
                            DeleteDataGridRow(e.NewIndex);
                            break;

                        case ListChangedType.ItemMoved:
                            MoveDataGridRow(e.OldIndex, e.NewIndex);
                            break;

                        case ListChangedType.Reset:
                            DataResetCount++;
                            _Panel.Rows.Clear();
                            UpdateSuperGridSort(true);
                            break;
                    }
                }

                UpdateInProgress = false;
            }
        }

        #region AddDataGridRow

        private void AddDataGridRow(IList list, int index)
        {
            GridElement selItem = _Panel.SuperGrid.ActiveElement;

            if (_Panel.VirtualMode == true)
            {
                _Panel.VirtualRows.MaxRowIndex = index - 1;
                _Panel.VirtualRowCount = CurrencyManager.Count;

                _Panel.InvalidateRender();

                if (_Panel.AutoSelectNewBoundRows == false)
                {
                    if (_Panel.ActiveRow != null)
                        index = _Panel.ActiveRow.RowIndex;
                    else
                        index = -1;
                }

                if (index > _Panel.VirtualRowCount)
                    index = _Panel.VirtualRowCount - 1;

                if (index >= 0)
                    SelectRow(index);
            }
            else
            {
                object o = list[index];

                GridRow row = GetNewRow(o);
                row.DataItem = o;
                row.DataItemIndex = index;

                UpdateRowsDataIndex(index, 1);

                if (_Panel.IsGrouped == true)
                {
                    _Panel.Rows.Insert(0, row);
                }
                else
                {
                    _Panel.Rows.Insert(index, row);
                    _Panel.UpdateRowPosition(row);

                    index = row.RowIndex;
                }

                if (_Panel.AutoSelectNewBoundRows == true)
                {
                    SelectRow(index);
                }
                else
                {
                    _Panel.ClearAll();

                    SelectLastElement(selItem);
                }
            }
        }

        #endregion

        #region DeleteDataGridRow

        private void DeleteDataGridRow(int dataItemIndex)
        {
            GridRow row = FindDataGridRowIndex(dataItemIndex);

            if (row != null)
            {
                if (row.DataItem != null)
                {
                    GridElement selItem = _Panel.SuperGrid.ActiveElement;

                    UpdateRowsDataIndex(row.DataItemIndex, -1);

                    row.DataItem = null;
                    row.DataItemIndex = -1;

                    int index = row.RowIndex;

                    if (_Panel.VirtualMode == true)
                    {
                        _Panel.VirtualRows.MaxRowIndex = index - 1;
                        _Panel.VirtualRowCount--;

                        _Panel.InvalidateRender();

                        if (_Panel.AutoSelectDeleteBoundRows == false)
                        {
                            if (_Panel.ActiveRow != null)
                                index = _Panel.ActiveRow.RowIndex;
                            else
                                index = -1;
                        }

                        if (index > _Panel.VirtualRowCount)
                            index = _Panel.VirtualRowCount - 1;

                        if (index >= 0)
                            SelectRow(index);
                    }
                    else
                    {
                        GridContainer cont = (GridContainer) row.Parent;
                        cont.Rows.RemoveAt(index);

                        if (_Panel.AutoSelectDeleteBoundRows == true)
                        {
                            if (index >= cont.Rows.Count)
                                index--;

                            if ((uint) index < cont.Rows.Count)
                                SelectRow(index);
                        }
                        else
                        {
                            SelectLastElement(selItem);
                        }
                    }

                    if (_Panel.GroupColumns.Count > 0)
                    {
                        _Panel.NeedsGrouped = true;
                        _Panel.InvalidateLayout();
                    }
                }
            }
        }

        #endregion

        #region SelectLastElement

        private void SelectLastElement(GridElement selItem)
        {
            _Panel.SuperGrid.NeedToUpdateIndicees = true;
            _Panel.UpdateIndicees(_Panel, _Panel.Rows, true);

            if (selItem is GridCell)
                _Panel.SetSelected((GridCell)selItem, true);

            else if (selItem is GridRow)
                _Panel.SetSelected((GridRow)selItem, true);
        }

        #endregion

        #region MoveDataGridRow

        private void MoveDataGridRow(int oldDataIndex, int newDataIndex)
        {
            GridRow oldRow = FindDataGridRowIndex(oldDataIndex);
            GridRow newRow = FindDataGridRowIndex(newDataIndex);

            if (oldRow != null && newRow != null)
            {
                GridContainer cont = (GridContainer) oldRow.Parent;

                int newIndex = newRow.RowIndex;

                if (oldRow.RowIndex < newRow.RowIndex)
                {
                    for (int i = oldRow.RowIndex; i < newRow.RowIndex; i++)
                    {
                        cont.Rows[i] = cont.Rows[i + 1];

                        ((GridRow)cont.Rows[i]).DataItemIndex--;
                        ((GridRow)cont.Rows[i]).RowIndex--;
                    }
                }
                else
                {
                    for (int i = oldRow.RowIndex; i > newRow.RowIndex; i--)
                    {
                        cont.Rows[i] = cont.Rows[i - 1];

                        ((GridRow)cont.Rows[i]).DataItemIndex++;
                        ((GridRow)cont.Rows[i]).RowIndex++;
                    }
                }

                cont.Rows[newIndex] = newRow;

                newRow.RowIndex = newIndex;
                newRow.DataItemIndex = newDataIndex;
            }
        }

        #endregion

        #region UpdateDataGridRow

        private void UpdateDataGridRow(IList list, int dataIndex)
        {
            GridRow row = FindDataGridRowIndex(dataIndex);

            if (row != null)
                UpdateListRow(row, list[dataIndex]);
        }

        #endregion

        #region UpdateListRow

        private void UpdateListRow(GridRow row, object o)
        {
            foreach (GridColumn col in _Panel.Columns)
            {
                if (col.ColumnIndex < row.Cells.Count)
                {
                    PropertyDescriptor pd = FindPropertyDescriptor(col);

                    if (pd != null)
                    {
                        object value = pd.GetValue(o);

                        row.Cells[col.ColumnIndex].ValueEx = value;
                        row.Cells[col.ColumnIndex].InvalidateRender();

                        if (_Panel.KeepRowsSorted == true)
                        {
                            if (col.IsSortColumn == true)
                            {
                                if (row == _Panel.ActiveRow)
                                    row.RowNeedsSorted = true;
                                else
                                    _Panel.NeedsSorted = true;
                            }
                        }
                    }
                }
            }

            row.DataItem = o;

            DataRowView drv = o as DataRowView;

            if (drv != null)
            {
                if (drv.Row.RowState == DataRowState.Unchanged)
                    row.RowDirty = false;
            }

            _Panel.NeedsFilterScan = true;
        }

        #endregion

        #endregion

        #region FindDataGridRowIndex

        private GridRow FindDataGridRowIndex(int dataItemIndex)
        {
            if (_Panel.ActiveRow != null)
            {
                GridRow row = _Panel.ActiveRow as GridRow;

                if (row != null &&
                    row.IsTempInsertRow == false && row.IsInsertRow == false)
                {
                    if (row.DataItemIndex == dataItemIndex)
                        return (row);
                }
            }

            if (_Panel.VirtualMode == true)
                return (FindDataGridVirtualRowIndex(dataItemIndex));

            return (FindDataGridRealRowIndex(_Panel.Rows, dataItemIndex));
        }

        private GridRow FindDataGridVirtualRowIndex(int dataItemIndex)
        {
            if ((uint)dataItemIndex < _Panel.VirtualRowCount)
                return (_Panel.VirtualRows[dataItemIndex]);

            return (null);
        }

        private GridRow FindDataGridRealRowIndex(
            IEnumerable<GridElement> items, int dataItemIndex)
        {
            foreach (GridElement item in items)
            {
                if (item is GridRow)
                {
                    GridRow row = item as GridRow;

                    if (row.IsTempInsertRow == false && row.IsInsertRow == false)
                    {
                        if (row.DataItemIndex == dataItemIndex)
                            return (row);
                    }
                }
                else if (item is GridGroup)
                {
                    GridRow row = FindDataGridRealRowIndex(((GridGroup)item).Rows, dataItemIndex);

                    if (row != null)
                        return (row);
                }
            }

            return (null);
        }

        #endregion

        #region SelectRow

        private void SelectRow(int index)
        {
            _Panel.LatentActiveRowIndex = index;
        }

        #endregion

        #region UpdateRowsDataIndex

        private void UpdateRowsDataIndex(int index, int n)
        {
            if (_Panel.VirtualMode == false)
                UpdateRowsDataIndexEx(_Panel.Rows, index, n);
        }

        private void UpdateRowsDataIndexEx(
            IEnumerable<GridElement> items, int index, int n)
        {
            foreach (GridContainer item in items)
            {
                GridRow row = item as GridRow;

                if (row != null)
                {
                    if (row.DataItemIndex >= index)
                        row.DataItemIndex += n;
                }

                if (item.Rows.Count > 0)
                    UpdateRowsDataIndexEx(item.Rows, index, n);
            }
        }

        #endregion

        #region RemoveRow

        internal void RemoveRow(GridRow row)
        {
            if (CurrencyManager != null)
            {
                UpdateInProgress = true;
                UpdatePosInProgress = true;

                try
                {
                    int index = row.DataItemIndex;

                    CurrencyManager.RemoveAt(index);
                }
                finally
                {
                    UpdateInProgress = false;
                    UpdatePosInProgress = false;

                    _Panel.UpdateRowCount();
                }

                if (row.DataItem != null)
                    UpdateRowsDataIndex(row.DataItemIndex, -1);
            }
        }

        #endregion

        #region InsertRow

        internal void InsertRow(int index, GridRow row)
        {
            if (CurrencyManager != null && _Pdc != null)
            {
                if (index > CurrencyManager.Count)
                    index = CurrencyManager.Count;

                UpdateInProgress = true;
                UpdatePosInProgress = true;

                object dataSource = _Panel.DataSource;

                try
                {
                    if (dataSource is DataSet)
                    {
                        InsertDataSetRow((DataSet) _Panel.DataSource, _Panel.DataMember, index, row);
                    }
                    else if (dataSource is DataTable)
                    {
                        InsertDataTableRow((DataTable)dataSource, index, row);
                    }
                    else if (dataSource is BindingSource)
                    {
                        BindingSource bs = (BindingSource)dataSource;

                        if (bs.DataSource is DataSet)
                            InsertDataSetRow((DataSet)bs.DataSource, bs.DataMember, index, row);

                        else if (bs.DataSource is DataTable)
                            InsertDataTableRow((DataTable)bs.DataSource, index, row);

                        else
                            InsertListRow(index, row);
                    }
                    else
                    {
                        InsertListRow(index, row);
                    }
                }
                finally
                {
                    UpdateInProgress = false;
                    UpdatePosInProgress = false;
                }
            }
        }

        #region InsertDataSetRow

        private void InsertDataSetRow(DataSet dataSet,
            string dataMember, int index, GridRow row)
        {
            if (string.IsNullOrEmpty(dataMember) && dataSet.Tables.Count > 0)
                dataMember = dataSet.Tables[0].TableName;

            InsertDataTableRow(dataSet.Tables[dataMember], index, row);
        }

        #endregion

        #region InsertDataTableRow

        private void InsertDataTableRow(DataTable table, int index, GridRow row)
        {
            DataRow drow = table.NewRow();

            for (int i = 0; i < Pdc.Count; i++)
            {
                PropertyDescriptor pd = Pdc[i];

                GridColumn col = FindGridColumn(_Panel, pd.Name);

                if (col != null)
                {
                    object value =
                        row.Cells[col.ColumnIndex].ValueEx ?? DBNull.Value;

                    drow[i] = value;
                }
            }

            UpdateRowsDataIndex(index, 1);

            row.DataItem = drow;
            row.DataItemIndex = index;

            if (index < CurrencyManager.Count)
            {
                DataRowView drv = CurrencyManager.List[index] as DataRowView;

                if (drv != null)
                    index = table.Rows.IndexOf(drv.Row);

                table.Rows.InsertAt(drow, index);
            }
            else
            {
                table.Rows.Add(drow);
            }
        }

        #endregion

        #region InsertListRow

        private void InsertListRow(int index, GridRow row)
        {
            if (Pdc.Count > 0)
            {
                Type type = Pdc[0].ComponentType;

                object item;

                if (type == typeof(DataRowView))
                    item = ((DataView)_BaseDataSource).AddNew();
                else
                    item = Activator.CreateInstance(type);

                for (int i = 0; i < Pdc.Count; i++)
                {
                    PropertyDescriptor pd = Pdc[i];

                    GridColumn col = FindGridColumn(_Panel, pd.Name);

                    if (col != null)
                    {
                        object o = row.Cells[col.ColumnIndex].ValueEx;

                        if (o != null && o is DBNull == false)
                            pd.SetValue(item, o);
                    }
                }

                UpdateRowsDataIndex(index, 1);

                row.DataItem = item;
                row.DataItemIndex = index;

                _UpdatePosInProgress = true;

                if (type != typeof(DataRowView))
                {
                    if (CurrencyManager != null)
                        CurrencyManager.List.Insert(index, item);
                }
                else
                {
                    DataRowView dv = (DataRowView)item;

                    dv.EndEdit();
                }

                _UpdatePosInProgress = false;
            }
        }

        #endregion

        #endregion

        #region ReloadRow

        internal void ReloadRow(GridRow row)
        {
            if (CurrencyManager != null && _Pdc != null)
            {
                if ((uint)row.RowIndex < CurrencyManager.List.Count)
                {
                    foreach (GridCell cell in row.Cells)
                    {
                        GridColumn col = _Panel.Columns[cell.ColumnIndex];
                        PropertyDescriptor pd = FindPropertyDescriptor(col);

                        if (pd != null)
                        {
                            cell.ValueEx = (pd.GetValue(
                                CurrencyManager.List[row.RowIndex]));
                        }
                    }
                }
            }
        }

        #endregion

        #region UpdateSuperGridSort

        private void UpdateSuperGridSort(bool reset)
        {
            if (reset == true ||
                (_Panel.SortColumns == null || _Panel.SortColumns.Count == 0))
            {
                if (CurrencyManager != null && CurrencyManager.List.Count > 0)
                {
                    DataRowView drv = CurrencyManager.List[0] as DataRowView;

                    if (drv != null)
                        SetSuperGridDataSetSort(drv.DataView.Sort);

                    else if (_BaseDataSource is IBindingList)
                        SetSuperGridListSort((IBindingList)_BaseDataSource);

                    else if (_BaseDataSource is IListSource)
                    {
                        IList list = ((IListSource)_BaseDataSource).GetList();

                        if (list is IBindingList)
                            SetSuperGridListSort((IBindingList)list);
                    }
                }
            }
        }

        #region SetSuperGridDataSetSort

        private void SetSuperGridDataSetSort(string sort)
        {
            _Panel.ClearSort();

            if (string.IsNullOrEmpty(sort) == false)
            {
                const string cref = @"((\[(?<name>\w+)\])|(?<name>\w+))\s*(?<order>ASC|DESC)*";

                Regex p = new Regex(cref);
                MatchCollection mc = p.Matches(sort);

                foreach (Match ma in mc)
                {
                    GridColumn col = _Panel.Columns[ma.Groups["name"].Value];

                    if (col != null)
                    {
                        SortDirection dir = SortDirection.Ascending;

                        Group grp = ma.Groups["order"];

                        if (grp != null)
                        {
                            dir = grp.Value.Equals("DESC")
                                ? SortDirection.Descending : SortDirection.Ascending;
                        }

                        _Panel.AddSort(col, dir);
                    }
                }

                _Panel.SuperGrid.DoRowsSortedEvent(_Panel);
            }

            _Panel.NeedsSorted = false;
        }

        #endregion

        #region SetSuperGridListSort

        private void SetSuperGridListSort(IBindingList list)
        {
            _Panel.ClearSort();

            if (list.SupportsSorting == true && list.IsSorted == true)
            {
                string name = list.SortProperty.Name;
                
                GridColumn col = _Panel.Columns[name];

                if (col != null)
                {
                    SortDirection dir = (list.SortDirection == ListSortDirection.Ascending)
                                            ? SortDirection.Ascending
                                            : SortDirection.Descending;

                    _Panel.AddSort(col, dir);
                }

                _Panel.SuperGrid.DoRowsSortedEvent(_Panel);
            }

            _Panel.NeedsSorted = false;
        }

        #endregion

        #endregion

        #region UpdateDataSourceSort

        internal void UpdateDataSourceSort()
        {
            if (CurrencyManager != null)
            {
                if (CurrencyManager.List.Count > 0)
                {
                    DataRowView drv = CurrencyManager.List[0] as DataRowView;

                    if (drv != null)
                    {
                        string sort = GetSortString(drv);

                        drv.DataView.Sort = sort;
                    }
                    else if (_BaseDataSource is IBindingList)
                        UpdateListSort((IBindingList)_BaseDataSource);

                    else if (_BaseDataSource is IListSource)
                    {
                        IList list = ((IListSource)_BaseDataSource).GetList();

                        if (list is IBindingList)
                            UpdateListSort((IBindingList)list);
                    }
                }

                if (_Panel.VirtualMode == true)
                    _Panel.VirtualRows.Clear();
            }
        }

        #endregion

        #region UpdateListSort

        private void UpdateListSort(IBindingList iBindingList)
        {
            if (iBindingList.SupportsSorting == true)
            {
                List<GridColumn> sortColumns = _Panel.SortColumns;

                if (sortColumns != null && sortColumns.Count > 0)
                {
                    GridColumn col = sortColumns[0];

                    PropertyDescriptor pd = FindPropertyDescriptor(col);

                    if (pd != null)
                    {
                        ListSortDirection dir = (col.SortDirection == SortDirection.Ascending)
                                                    ? ListSortDirection.Ascending
                                                    : ListSortDirection.Descending;

                        iBindingList.ApplySort(pd, dir);
                    }
                }
                else if (iBindingList.IsSorted == true)
                {
                    iBindingList.RemoveSort();
                }
            }
        }

        #endregion

        #region GetSortString

        private string GetSortString(DataRowView drv)
        {
            List<GridColumn> sortColumns = _Panel.SortColumns;

            if (sortColumns != null && sortColumns.Count > 0)
            {
                StringBuilder sb = new StringBuilder();

                foreach (GridColumn column in sortColumns)
                    GetColumnSort(drv, column, sb);

                if (sb.Length > 0)
                    sb.Length--;

                return (sb.ToString());
            }

            return (null);
        }

        #endregion

        #region GetColumnSort

        private void GetColumnSort(
            DataRowView drv, GridColumn column, StringBuilder sb)
        {
            DataColumn dc = FindDataColumn(drv.DataView.Table, column);

            if (dc != null)
            {
                sb.Append("[");
                sb.Append(dc.ColumnName);
                sb.Append("]");
                sb.Append(column.SortDirection == SortDirection.Ascending ? " ASC," : " DESC,");
            }
        }

        #endregion

        #region FlushRow

        internal bool FlushRow()
        {
            if (CurrencyManager != null)
            {
                while (true)
                {
                    _UpdateInProgress = true;
                    _UpdatePosInProgress = true;

                    try
                    {
                        CurrencyManager.EndCurrentEdit();
                        break;
                    }
                    catch (Exception exp)
                    {
                        GridRow crow = null;

                        if ((uint) CurrencyManager.Position < _Panel.Rows.Count)
                        {
                            crow = _Panel.Rows[CurrencyManager.Position] as GridRow;

                            if (crow != null)
                                ReloadRow(crow);
                        }

                        bool retry = false;
                        bool throwException = false;

                        if (_Panel.SuperGrid.HasDataErrorHandler == true)
                        {
                            object value = crow;

                            if (_Panel.SuperGrid.DoDataErrorEvent(_Panel, null, exp,
                                DataContext.RowFlush, ref value,
                                ref throwException, ref retry) == true)
                            {
                                return (false);
                            }
                        }

                        if (throwException == true)
                            throw;

                        if (retry == false)
                            return (false);
                    }
                    finally
                    {
                        _UpdateInProgress = false;
                        _UpdatePosInProgress = false;
                    }
                }
            }

            return (true);
        }

        #endregion

        #region SuperGridLoadVirtualRow

        void SuperGridLoadVirtualRow(object sender, GridVirtualRowEventArgs e)
        {
            if (CurrencyManager != null && _Pdc != null)
            {
                GridRow row = e.GridRow;
                GridRow vrow = _Panel.VirtualTempInsertRow;

                int index = e.Index;

                if (vrow != null)
                {
                    if (index == vrow.RowIndex)
                    {
                        for (int i = 0; i < _Panel.Columns.Count; i++)
                            row.Cells[i].ValueEx = vrow.Cells[i].Value;

                        row.RowNeedsSorted = vrow.NeedsSorted;
                        row.IsTempInsertRow = vrow.IsTempInsertRow;

                        return;
                    }

                    if (index > vrow.RowIndex)
                        index--;
                }

                if (index == _Panel.VirtualRowCount)
                {
                    if (_Panel.ShowInsertRow == true)
                    {
                        vrow = _Panel.VirtualInsertRow;

                        if (vrow != null)
                        {
                            for (int i = 0; i < _Panel.Columns.Count; i++)
                                row.Cells[i].ValueEx = vrow.Cells[i].Value;

                            row.RowNeedsSorted = vrow.NeedsSorted;
                            row.IsTempInsertRow = vrow.IsTempInsertRow;

                            return;
                        }
                    }
                }

                if ((uint)index < CurrencyManager.List.Count)
                {
                    row.DataItem = CurrencyManager.List[index];
                    row.DataItemIndex = index;

                    foreach (GridColumn col in _Panel.Columns)
                    {
                        PropertyDescriptor pd = FindPropertyDescriptor(col);

                        if (pd != null)
                            row.Cells[col.ColumnIndex].ValueEx = pd.GetValue(row.DataItem);
                    }
                }
            }
        }

        #endregion

        #region Filter management

        #region UpdateFilter

        internal void UpdateFilter()
        {
            if (CurrencyManager != null)
            {
                DataTable dt = null;

                if (_BaseDataSource is DataSet)
                {
                    DataSet dataSet = _BaseDataSource as DataSet;
                    string dataMember = _BaseDataMember;

                    if (string.IsNullOrEmpty(dataMember) && dataSet.Tables.Count > 0)
                        dataMember = dataSet.Tables[0].TableName;

                    if (string.IsNullOrEmpty(dataMember) == false)
                        dt = dataSet.Tables[dataMember];
                }
                else if (_BaseDataSource is DataTable)
                {
                    dt = _BaseDataSource as DataTable;
                }

                if (dt != null)
                {
                    try
                    {
                        dt.DefaultView.RowFilter = GetDataViewRowFilter();
                    }
                    catch (Exception exp)
                    {
                        MessageBoxEx.Show(exp.Message);
                    }

                    Refresh();
                }
            }
        }

        #endregion

        #region GetDataViewRowFilter

        private string GetDataViewRowFilter()
        {
            StringBuilder sb = new StringBuilder();

            if (String.IsNullOrEmpty(_Panel.FilterExpr) == false)
            {
                sb.Append("(");
                sb.Append(_Panel.FilterExpr);
                sb.Append(") && ");
            }

            foreach (GridColumn col in _Panel.Columns)
            {
                if (String.IsNullOrEmpty(col.FilterExpr) == false)
                {
                    sb.Append("(");
                    sb.Append(col.FilterExpr);
                    sb.Append(") and ");
                }
            }

            if (sb.Length > 0)
                sb.Length -= 4;

            string s = sb.ToString();

            return (s);
        }

        #endregion

        #endregion

        #region Begin/EndUpdate

        internal void BeginUpdate()
        {
            _BeginUpdateCount++;
        }

        internal void EndUpdate()
        {
            if (_BeginUpdateCount > 0)
            {
                --_BeginUpdateCount;

                if (_BeginUpdateCount == 0)
                    Refresh();
            }
        }

        #endregion

        #region Refresh

        private void Refresh()
        {
            if (CurrencyManager != null)
            {
                CurrencyManager.Refresh();

                if (_Panel.VirtualMode == true)
                    _Panel.VirtualRowCount = _CurrencyManager.Count;
            }

            _Panel.UpdateRowCount();
        }

        #endregion

        #region Dispose

        public void Dispose()
        {
            CurrencyManager = null;
        }

        #endregion
    }
}
