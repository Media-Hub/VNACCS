using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;

namespace DevComponents.SuperGrid.Design
{
    public class SuperGridActionList : DesignerActionList,
        ITypeDescriptorContext, IWindowsFormsEditorService, IServiceProvider
    {
        #region Private variables

        private SuperGridControl _SuperGrid;

        private PropertyDescriptor _PropertyDescriptor;
        private IComponentChangeService _ChangeService;
        
        #endregion

        /// <summary>
        /// SuperGridActionList
        /// </summary>
        /// <param name="superGrid">Associated SuperGridControl</param>
        public SuperGridActionList(SuperGridControl superGrid)
            : base(superGrid)
        {
            _SuperGrid = superGrid;

            _ChangeService = Component.Site.GetService(typeof(IComponentChangeService)) as IComponentChangeService;
        }

        #region Public properties

        /// <summary>
        /// Gets or sets the GridPanel DataSource
        /// </summary>
        [AttributeProvider(typeof(IListSource))]
        public object DataSource
        {
            get { return (_SuperGrid.PrimaryGrid.DataSource); }
            set { SetValue("DataSource", value); }
        }

        /// <summary>
        /// Gets or sets whether rows can be deleted
        /// </summary>
        public bool AllowRowDelete
        {
            get { return (_SuperGrid.PrimaryGrid.AllowRowDelete); }
            set { SetValue("AllowRowDelete", value); }
        }

        /// <summary>
        /// Gets or sets whether rows can be Inserted
        /// </summary>
        public bool AllowRowInsert
        {
            get { return (_SuperGrid.PrimaryGrid.AllowRowInsert); }
            set { SetValue("AllowRowInsert", value); }
        }

        /// <summary>
        /// Gets or sets whether the Insert Row is displayed at runtime
        /// </summary>
        public bool ShowInsertRow
        {
            get { return (_SuperGrid.PrimaryGrid.ShowInsertRow); }
            set { SetValue("ShowInsertRow", value); }
        }

        /// <summary>
        /// Gets or sets which Grid Lines are displayed
        /// </summary>
        public GridLines GridLines
        {
            get { return (_SuperGrid.PrimaryGrid.GridLines); }
            set { SetValue("GridLines", value); }
        }

        /// <summary>
        /// Gets or sets whether Tree Lines are displayed
        /// </summary>
        public bool ShowTreeLines
        {
            get { return (_SuperGrid.PrimaryGrid.ShowTreeLines); }
            set { SetValue("ShowTreeLines", value); }
        }

        /// <summary>
        /// Gets or sets whether Tree Buttons are displayed
        /// </summary>
        public bool ShowTreeButtons
        {
            get { return (_SuperGrid.PrimaryGrid.ShowTreeButtons); }
            set { SetValue("ShowTreeButtons", value); }
        }

        /// <summary>
        /// Gets or sets whether to use the Alternate Row Style
        /// </summary>
        public bool UseAlternateRowStyle
        {
            get { return (_SuperGrid.PrimaryGrid.UseAlternateRowStyle); }
            set { SetValue("UseAlternateRowStyle", value); }
        }

        /// <summary>
        /// Gets or sets whether to use the Alternate Column Style
        /// </summary>
        public bool UseAlternateColumnStyle
        {
            get { return (_SuperGrid.PrimaryGrid.UseAlternateColumnStyle); }
            set { SetValue("UseAlternateColumnStyle", value); }
        }

        /// <summary>
        /// Gets or sets whether to allow multi selection
        /// </summary>
        public bool MultiSelect
        {
            get { return (_SuperGrid.PrimaryGrid.MultiSelect); }
            set { SetValue("MultiSelect", value); }
        }

        /// <summary>
        /// Gets or sets the selection granularity
        /// </summary>
        public SelectionGranularity SelectionGranularity
        {
            get { return (_SuperGrid.PrimaryGrid.SelectionGranularity); }
            set { SetValue("SelectionGranularity", value); }
        }

        #endregion

        #region SetValue

        private void SetValue(string property, object value)
        {
            _ChangeService.OnComponentChanging(_SuperGrid.PrimaryGrid, null);

            GetPrimaryGridPropertyByName(property).SetValue(_SuperGrid.PrimaryGrid, value);

            _ChangeService.OnComponentChanged(_SuperGrid.PrimaryGrid, null, null, null);
        }

        #endregion

        #region GetPrimaryGridPropertyByName

        /// <summary>
        /// Gets the PrimaryGrid property via the given name
        /// </summary>
        /// <param name="propName">Property name</param>
        /// <returns>PropertyDescriptor</returns>
        private PropertyDescriptor GetPrimaryGridPropertyByName(string propName)
        {
            PropertyDescriptor prop =
                TypeDescriptor.GetProperties(_SuperGrid.PrimaryGrid)[propName];

            if (prop == null)
                throw new ArgumentException("Matching property not found.", propName);

            return (prop);
        }

        #endregion

        #region GetSortedActionItems

        /// <summary>
        /// Gets the SortedActionItems
        /// </summary>
        /// <returns>DesignerActionItemCollection</returns>
        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();

            items.Add(new DesignerActionPropertyItem("DataSource", "Data Source",
                "Data", "Sets the GridPanel Data Source."));

            items.Add(new DesignerActionPropertyItem("AllowRowDelete", "Allow Row Delete",
                "User Interaction", "Determines whether rows can be deleted."));

            items.Add(new DesignerActionPropertyItem("AllowRowInsert", "Allow Row Insert",
                "User Interaction", "Determines whether rows can be inserted."));

            items.Add(new DesignerActionPropertyItem("ShowInsertRow", "Show Insert Row",
                "User Interaction", "Determines whether the InsertRow is shown at runtime."));

            items.Add(new DesignerActionPropertyItem("GridLines", "Grid Lines",
                "Appearance", "Determines which Grid Lines are displayed."));

            items.Add(new DesignerActionPropertyItem("ShowTreeLines", "Show Tree Lines",
                "Appearance", "Determines whether Tree Lines are displayed."));

            items.Add(new DesignerActionPropertyItem("ShowTreeButtons", "Show Tree Buttons",
                "Appearance", "Determines whether Tree Buttons are displayed."));

            items.Add(new DesignerActionPropertyItem("UseAlternateColumnStyle", "Use Alternate Column Style",
                "Style", "Determines whether to use the defined Alternate Column Style."));

            items.Add(new DesignerActionPropertyItem("UseAlternateRowStyle", "Use Alternate Row Style",
                "Style", "Determines whether to use the defined Alternate Row Style."));

            items.Add(new DesignerActionPropertyItem("SelectionGranularity", "Selection Granularity",
                "Behavior", "Determines user selection granularity."));

            items.Add(new DesignerActionPropertyItem("MultiSelect", "Enable Multi Selection",
                "Behavior", "Determines whether Multi Selection is enabled."));

            items.Add(new DesignerActionMethodItem(this, "EditColumns", "Edit Columns...", "Data2"));
            items.Add(new DesignerActionMethodItem(this, "EditRows", "Edit Rows...", "Data2"));

            return (items);
        }

        #endregion

        #region EditColumns

        /// <summary>
        /// EditColumns
        /// </summary>
        private void EditColumns()
        {
            _PropertyDescriptor = TypeDescriptor.GetProperties(_SuperGrid.PrimaryGrid)["Columns"];

            UITypeEditor editor = (UITypeEditor)_PropertyDescriptor.GetEditor(typeof(UITypeEditor));

            if (editor != null)
                editor.EditValue(this, this, _SuperGrid.PrimaryGrid.Columns);
        }

        #endregion

        #region EditRows

        /// <summary>
        /// EditRows
        /// </summary>
        private void EditRows()
        {
            _PropertyDescriptor = TypeDescriptor.GetProperties(_SuperGrid.PrimaryGrid)["Rows"];

            UITypeEditor editor = (UITypeEditor)_PropertyDescriptor.GetEditor(typeof(UITypeEditor));

            if (editor != null)
                editor.EditValue(this, this, _SuperGrid.PrimaryGrid.Rows);
        }

        #endregion

        #region ITypeDescriptorContext Members

        public IContainer Container
        {
            get { return (Component.Site.Container); }
        }

        public object Instance
        {
            get { return (Component); }
        }

        public void OnComponentChanged()
        {
            object value = _SuperGrid.PrimaryGrid.Columns;

            _ChangeService.OnComponentChanged(Component, _PropertyDescriptor, value, value);
        }

        public bool OnComponentChanging()
        {
            _ChangeService.OnComponentChanging(Component, _PropertyDescriptor);

            return true;
        }

        public PropertyDescriptor PropertyDescriptor
        {
            get { return (_PropertyDescriptor); }
        }

        #endregion

        #region IWindowsFormsEditorService Members

        public void CloseDropDown()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public void DropDownControl(Control control)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        public DialogResult ShowDialog(Form dialog)
        {
            return (dialog.ShowDialog());
        }

        #endregion

        #region IServiceProvider

        object IServiceProvider.GetService(Type serviceType)
        {
            if (serviceType.Equals(typeof(IWindowsFormsEditorService)))
                return (this);

            return GetService(serviceType);
        }

        #endregion
    }
}
