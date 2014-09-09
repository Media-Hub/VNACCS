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
    public class GridCellCollectionEditor : CollectionEditor
    {
        #region Static variables

        static Size _lastSize = Size.Empty;
        static Point _lastLoc = Point.Empty;

        #endregion

        #region Private variables

        private CollectionForm _CollectionForm;
        private TableLayoutPanel _LayoutPanel;

        #endregion

        public GridCellCollectionEditor(Type type)
            : base(type)
        {
        }

        #region CreateCollectionItemType

        protected override Type CreateCollectionItemType()
        {
            return typeof(GridCell);
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
                PropertyGrid propertyGrid =
                    _LayoutPanel.Controls["propertyBrowser"] as PropertyGrid;

                if (propertyGrid != null)
                {
                    propertyGrid.HelpVisible = true;
                    propertyGrid.GotFocus += PropertyGridGotFocus;
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
            _LayoutPanel.Controls[4].Refresh();
        }

        #endregion

        #endregion

        #region EditValue

        public override object EditValue(
            ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            object o = base.EditValue(context, provider, value);

            return (o);
        }

        #endregion

        #region CreateInstance

        protected override object CreateInstance(Type itemType)
        {
            object o = base.CreateInstance(itemType);

            return (o);
        }

        #endregion

    }
}
