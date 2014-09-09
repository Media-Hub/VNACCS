using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;

namespace DevComponents.SuperGrid.Design
{
    /// <summary>
    /// GridColumnCollectionEditor
    /// </summary>
    public class GridColumnCollectionEditor : CollectionEditor
    {
        #region Static variables

        static Size _lastSize = Size.Empty;
        static Point _lastLoc = Point.Empty;

        #endregion

        public GridColumnCollectionEditor(Type type)
            : base(type)
        {
        }

        #region CreateCollectionForm

        protected override CollectionForm CreateCollectionForm()
        {
            CollectionForm collectionForm = base.CreateCollectionForm();

            if (collectionForm.Controls[0] is TableLayoutPanel)
            {
                TableLayoutPanel tlpf =
                    collectionForm.Controls["overArchingTableLayoutPanel"] as TableLayoutPanel;

                if (tlpf != null)
                {
                    PropertyGrid propertyGrid =
                        tlpf.Controls["propertyBrowser"] as PropertyGrid;

                    if (propertyGrid != null)
                    {
                        propertyGrid.HelpVisible = true;

                        propertyGrid.GotFocus += PropertyGridGotFocus;
                        propertyGrid.SelectedObjectsChanged += PropertyGridSelectedObjectsChanged;
                    }
                }
            }

            collectionForm.Load += CollectionFormLoad;
            collectionForm.Resize += CollectionFormResize;
            collectionForm.LocationChanged += CollectionFormLocationChanged;

            return (collectionForm);
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
            PropertyGrid propertyGrid = sender as PropertyGrid;

            if (propertyGrid != null)
                UpdateDesignerFocus(propertyGrid);
        }

        #endregion

        #region PropertyGridSelectedObjectsChanged

        void PropertyGridSelectedObjectsChanged(object sender, EventArgs e)
        {
            PropertyGrid propertyGrid = sender as PropertyGrid;

            if (propertyGrid != null)
                UpdateDesignerFocus(propertyGrid);
        }

        #endregion

        #region UpdateDesignerFocus

        private void UpdateDesignerFocus(PropertyGrid propertyGrid)
        {
            GridContainer row = Context.Instance as GridContainer;

            if (row != null)
            {
                GridElement item = propertyGrid.SelectedObject as GridElement;

                if (item != null)
                    row.SuperGrid.DesignerElement = item;
            }
        }

        #endregion

        #endregion

        #region CreateCollectionItemType

        protected override Type CreateCollectionItemType()
        {
            return typeof(GridColumn);
        }

        #endregion

        #region CreateNewItemTypes

        protected override Type[] CreateNewItemTypes()
        {
            return new Type[]
            {
                typeof(GridTextBoxXEditControl),
                typeof(GridButtonXEditControl),
                typeof(GridCheckBoxXEditControl),
                typeof(GridComboBoxExEditControl),
                typeof(GridComboTreeEditControl),
                typeof(GridDateTimeInputEditControl),
                typeof(GridDateTimePickerEditControl),
                typeof(GridDoubleInputEditControl),
                typeof(GridImageEditControl),
                typeof(GridIntegerInputEditControl),
                typeof(GridIpAddressInputEditControl),
                typeof(GridLabelXEditControl),
                typeof(GridMaskedTextBoxEditControl),
                typeof(GridMicroChartEditControl),
                typeof(GridNumericUpDownEditControl),
                typeof(GridImageEditControl),
                typeof(GridProgressBarXEditControl),
                typeof(GridRatingStarEditControl),
                typeof(GridSliderEditControl),
                typeof(GridSwitchButtonEditControl),
                typeof(GridTextBoxDropDownEditControl),
            };
        }

        #endregion

        #region EditValue

        public override object EditValue(
            ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object o = base.EditValue(context, provider, value);

            if (this.Context != null)
            {
                GridPanel panel = this.Context.Instance as GridPanel;

                if (panel != null)
                {
                    if (panel.SuperGrid.DesignerElement is GridColumn)
                        panel.SuperGrid.DesignerElement = null;
                }
            }

            return (o);
        }

        #endregion

        #region CreateInstance

        protected override object CreateInstance(Type itemType)
        {
            GridColumn item = (GridColumn)base.CreateInstance(typeof(GridColumn));

            if (item != null)
            {
                item.EditorType = itemType;

                item.Name = GetColumnName();
            }

            return (item);
        }

        #region GetColumnName

        private string GetColumnName()
        {
            GridPanel panel = this.Context.Instance as GridPanel;

            if (panel != null)
            {
                for (int i = 1; i < 200; i++)
                {
                    string s = "Column" + i;

                    if (panel.Columns[s] == null)
                        return (s);
                }
            }

            return ("");
        }

        #endregion

        #endregion

    }
}
