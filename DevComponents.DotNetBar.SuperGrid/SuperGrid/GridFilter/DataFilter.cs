using System;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    /// DataBinding helper class
    ///</summary>
    internal static class DataFilter
    {
        #region Static data

        private static bool _lockFilter;

        #endregion

        ///<summary>
        /// FilterData
        ///</summary>
        ///<param name="panel"></param>
        internal static void FilterData(GridPanel panel)
        {
            if (_lockFilter == true)
                return;

            if (panel.SuperGrid.DoDataFilteringStartEvent(panel) == false)
            {
                _lockFilter = true;

                try
                {
                    if (panel.EnableFiltering == true &&
                        panel.FilterLevel != FilterLevel.None)
                    {
                        bool rowFiltered = IsRowFiltered(panel);
                        bool colFiltered = IsColumnFiltered(panel);

                        if (rowFiltered == true || colFiltered == true)
                        {
                            if (rowFiltered == true)
                                rowFiltered = UpdateRowFilter(panel);

                            if (colFiltered == true)
                                UpdateColumnFilter(panel);

                            UpdateFilterState(panel, rowFiltered, colFiltered);

                            return;
                        }
                    }

                    UpdateFilterState(panel, false, false);
                }
                finally
                {
                    _lockFilter = false;

                    panel.UpdateVisibleRowCount();
                    panel.SuperGrid.DoDataFilteringCompleteEvent(panel);
                }
            }
            else
            {
                panel.UpdateVisibleRowCount();
            }
        }

        #region UpdateRowFilter

        private static bool UpdateRowFilter(GridPanel panel)
        {
            if (panel.FilterEval == null)
            {
                try
                {
                    panel.FilterEval = new FilterEval(
                        panel, null, panel.GetFilterMatchType(), panel.FilterExpr);
                }
                catch
                {
                    return (false);
                }
            }

            return (true);
        }

        #endregion

        #region UpdateColumnFilter

        private static void UpdateColumnFilter(GridPanel panel)
        {
            foreach (GridColumn col in panel.Columns)
            {
                if (col.IsFiltered == true)
                {
                    col.FilterError = false;

                    if (col.FilterEval == null)
                    {
                        try
                        {
                            col.FilterEval = new FilterEval(
                                panel, col, col.GetFilterMatchType(), col.FilterExpr);
                        }
                        catch
                        {
                            col.FilterError = true;
                        }
                    }
                }
            }
        }

        #endregion

        #region UpdateFilterState

        private static void UpdateFilterState(
            GridPanel panel, bool rowFiltered, bool colFiltered)
        {
            if (panel.VirtualMode == true)
            {
                if (panel.SuperGrid.DoRefreshFilter(panel) == false)
                {
                    panel.VirtualRows.Clear();

                    panel.DataBinder.UpdateFilter();
                }
            }
            else
            {
                int count = panel.Rows.Count;

                if (panel.ShowInsertRow == true)
                    count--;

                bool root = (panel.FilterLevel &
                    (FilterLevel.Root | FilterLevel.RootConditional)) != 0;

                UpdateFilterStateEx(panel, root,
                    panel.Rows, count, rowFiltered, colFiltered);
            }
        }

        #region UpdateFilterStateEx

        private static void UpdateFilterStateEx(GridPanel panel, bool filter,
            GridItemsCollection items, int count, bool rowFiltered, bool colFiltered)
        {
            for (int i = 0; i < count; i++)
            {
                GridRow row = items[i] as GridRow;

                if (row != null)
                {
                    bool expanded = (panel.FilterLevel & FilterLevel.Expanded) != 0;

                    if (row.Rows.Count > 0)
                    {
                        UpdateFilterStateEx(panel, expanded,
                            row.Rows, row.Rows.Count, rowFiltered, colFiltered);
                    }

                    bool filteredOut = false;

                    if (filter == true)
                    {
                        if ((panel.FilterLevel & FilterLevel.RootConditional) != FilterLevel.RootConditional ||
                            row.AnyVisibleItems() == false)
                        {
                            if (rowFiltered == true)
                            {
                                if (EvalRowFilterState(panel, row, ref filteredOut) == true)
                                    break;
                            }

                            if (filteredOut == false && colFiltered == true)
                                EvalColumnFilterState(panel, row, ref filteredOut);
                        }
                    }

                    row.RowFilteredOut = filteredOut;

                }
                else
                {
                    GridContainer cont = items[i] as GridGroup;

                    if (cont != null)
                    {
                        UpdateFilterStateEx(panel,
                            true, cont.Rows, cont.Rows.Count, rowFiltered, colFiltered);
                    }
                }
            }
        }

        #endregion

        #region EvalRowFilterState

        private static bool EvalRowFilterState(
            GridPanel panel, GridRow row, ref bool filteredOut)
        {
            try
            {
                filteredOut = (panel.FilterEval.Evaluate(row) == false);
            }
            catch (Exception exp)
            {
                filteredOut = false;

                bool throwException = false;

                if (panel.SuperGrid.DoFilterRowErrorEvent(panel,
                    row, exp, ref filteredOut, ref throwException) == true)
                {
                    return (true);
                }

                if (throwException == true)
                    throw;

                MessageBoxEx.Show(exp.Message);

                return (true);
            }

            return (false);
        }

        #endregion

        #region EvalColumnFilterState

        private static bool EvalColumnFilterState(
            GridPanel panel, GridRow row, ref bool filteredOut)
        {
            foreach (GridColumn col in panel.Columns)
            {
                if (col.IsFiltered == true)
                {
                    if (col.FilterError == false)
                    {
                        try
                        {
                            filteredOut = (col.FilterEval.Evaluate(row) == false);
                        }
                        catch (Exception exp)
                        {
                            col.FilterError = true;

                            filteredOut = false;

                            bool throwException = false;

                            if (panel.SuperGrid.DoFilterColumnErrorEvent(panel,
                                row, col, exp, ref filteredOut, ref throwException) == true)
                            {
                                return (true);
                            }

                            if (throwException == true)
                                throw;

                            if (col.FilterEval.PostError == true)
                                MessageBoxEx.Show((col.Name ?? "[" + col.ColumnIndex + "]") + ":\n" + exp.Message);

                            return (true);
                        }
                    }
                }

                if (filteredOut == true)
                    break;
            }

            return (false);
        }

        #endregion

        #endregion

        #region UpdateRowFilterState

        internal static void UpdateRowFilterState(GridPanel panel, GridRow row)
        {
            bool isPrimary = (row.Parent is GridPanel);

            bool root = (panel.FilterLevel &
                (FilterLevel.Root | FilterLevel.RootConditional)) != 0;

            bool expanded = (panel.FilterLevel & FilterLevel.Expanded) != 0;

            if ((isPrimary == true && root == true) ||
                (isPrimary == false && expanded == true))
            {
                bool filteredOut = false;

                bool rowFiltered = IsRowFiltered(panel);
                bool colFiltered = IsColumnFiltered(panel);

                if (rowFiltered == true || colFiltered == true)
                {
                    if (rowFiltered == true)
                        rowFiltered = UpdateRowFilter(panel);

                    if (colFiltered == true)
                        UpdateColumnFilter(panel);

                    if (rowFiltered == true)
                    {
                        if (EvalRowFilterState(panel, row, ref filteredOut) == true)
                            return;
                    }

                    if (filteredOut == false && colFiltered == true)
                    {
                        if (EvalColumnFilterState(panel, row, ref filteredOut) == true)
                            return;
                    }
                }

                row.RowFilteredOut = filteredOut;

                panel.SuperGrid.NeedToUpdateIndicees = true;
            }
        }

        #endregion

        #region IsFiltered

        internal static bool IsFiltered(GridPanel panel)
        {
            return (IsRowFiltered(panel) || IsColumnFiltered(panel));
        }

        #endregion

        #region IsRowFiltered

        internal static bool IsRowFiltered(GridPanel panel)
        {
            return (panel.EnableFiltering == true &&
                panel.EnableRowFiltering == true &&
                String.IsNullOrEmpty(panel.FilterExpr) == false);
        }

        #endregion

        #region IsColumnFiltered

        internal static bool IsColumnFiltered(GridPanel panel)
        {
            if (panel.EnableFiltering == true)
            {
                foreach (GridColumn col in panel.Columns)
                {
                    if (col.IsFiltered == true)
                        return (true);
                }
            }

            return (false);
        }

        #endregion
    }
}
