using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;

namespace DevComponents.SuperGrid.Design
{
    /// <summary>
    /// GridRowCollectionEditor
    /// </summary>
    public class GridRowCollectionEditor : CollectionEditor
    {
        #region Static variables

        static Size _lastSize = Size.Empty;
        static Point _lastLoc = Point.Empty;

        #endregion

        #region Private variables

        private CollectionForm _CollectionForm;
        private TableLayoutPanel _LayoutPanel;
        private PropertyGrid _PropertyGrid;

        #endregion

        public GridRowCollectionEditor(Type type)
            : base(type)
        {
        }

        #region CreateCollectionItemType

        protected override Type CreateCollectionItemType()
        {
            return typeof(GridRow);
        }

        #endregion

        #region CreateCollectionForm

        protected override CollectionForm CreateCollectionForm()
        {
            _CollectionForm = base.CreateCollectionForm();

            _LayoutPanel = 
                _CollectionForm.Controls["overArchingTableLayoutPanel"] as TableLayoutPanel;

            if (_LayoutPanel != null)
            {
                _PropertyGrid =
                    _LayoutPanel.Controls["propertyBrowser"] as PropertyGrid;

                if (_PropertyGrid != null)
                {
                    _PropertyGrid.HelpVisible = true;

                    _PropertyGrid.GotFocus += PropertyGridGotFocus;
                    _PropertyGrid.SelectedObjectsChanged += PropertyGridSelectedObjectsChanged;
                }
            }

            _CollectionForm.Load += CollectionFormLoad;
            _CollectionForm.Resize += CollectionFormResize;
            _CollectionForm.LocationChanged += CollectionFormLocationChanged;

            return (_CollectionForm);
        }

        #region CollectionFormLoad

        void CollectionFormLoad(object sender, EventArgs e)
        {
            CollectionForm form = sender as CollectionForm;

            if (form != null)
            {
                if (_lastSize != Size.Empty)
                    form.Size = _lastSize;

                if (_lastLoc != Point.Empty)
                    form.Location = _lastLoc;
            }
        }

        #endregion

        #region CollectionFormResize

        void CollectionFormResize(object sender, EventArgs e)
        {
            CollectionForm form = sender as CollectionForm;

            if (form != null && form.Visible == true)
                _lastSize = form.Size;
        }

        #endregion

        #region CollectionFormLocationChanged

        void CollectionFormLocationChanged(object sender, EventArgs e)
        {
            CollectionForm form = sender as CollectionForm;

            if (form != null && form.Visible == true)
                _lastLoc = form.Location;
        }

        #endregion

        #region PropertyGridGotFocus

        void PropertyGridGotFocus(object sender, EventArgs e)
        {
            UpdateDesignerFocus();
            
            _LayoutPanel.Controls[4].Refresh();
        }

        #endregion

        #region PropertyGridSelectedObjectsChanged

        void PropertyGridSelectedObjectsChanged(object sender, EventArgs e)
        {
            UpdateDesignerFocus();
        }

        #endregion

        #region UpdateDesignerFocus

        private void UpdateDesignerFocus()
        {
            if (_PropertyGrid != null)
            {
                GridContainer row = Context.Instance as GridContainer;

                if (row != null)
                {
                    GridElement item = _PropertyGrid.SelectedObject as GridElement;

                    if (item != null)
                        row.SuperGrid.DesignerElement = item;
                }
            }
        }

        #endregion

        #endregion

        #region CreateNewItemTypes

        protected override Type[] CreateNewItemTypes()
        {
            GridPanel panel = Context.Instance as GridPanel;

            if (panel == null)
            {
                return new Type[]
                {
                    typeof (GridRow),
                    typeof (GridPanel),
                };
            }

            return (base.CreateNewItemTypes());
        }

        #endregion

        #region EditValue

        public override object EditValue(
            ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object o;

            if (_CollectionForm != null && _CollectionForm.Visible)
            {
                GridRowCollectionEditor editor =
                   new GridRowCollectionEditor(this.CollectionType);

                o = editor.EditValue(context, provider, value);
            }
            else
            {
                o = base.EditValue(context, provider, value);

                GridPanel panel = Context.Instance as GridPanel;

                if (panel != null && panel.Parent == null)
                    panel.SuperGrid.DesignerElement = null;
            }

            return (o);
        }

        #endregion

        #region CreateInstance

        protected override object CreateInstance(Type itemType)
        {
            object o = base.CreateInstance(itemType);

            GridContainer crow = Context.Instance as GridContainer;

            if (crow != null)
            {
                crow.Expanded = true;

                GridPanel cpanel = crow.GridPanel;

                GridRow row = o as GridRow;

                if (row != null)
                {
                    if (cpanel.Columns.Count == 0)
                        cpanel.Columns.Add(new GridColumn("Column1"));

                    row.Cells.Add(new GridCell());
                    row.InvalidateRender();
                }
                else
                {
                    GridPanel panel = o as GridPanel;

                    if (panel != null)
                    {
                        if (cpanel != null)
                        {
                            panel.Name = GetPanelName(cpanel);

                            panel.Columns.Add(new GridColumn("Column1"));
                        }
                    }
                }
            }

            return (o);
        }

        #region GetPanelName

        private string GetPanelName(GridPanel panel)
        {
            for (int i = 1; i < 200; i++)
            {
                string s = "GridPanel" + i;

                if (UnusedPanelName(panel, s) == true)
                    return (s);
            }

            return ("GridPanel");
        }

        #region UnusedPanelName

        private bool UnusedPanelName(GridPanel panel, string s)
        {
            foreach (GridContainer row in panel.Rows)
            {
                if (row is GridPanel)
                {
                    if (s.Equals(((GridPanel) row).Name) == true)
                        return (false);
                }
            }

            return (true);
        }

        #endregion

        #endregion

        #endregion

    }
}
