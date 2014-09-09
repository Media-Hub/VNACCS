using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.Windows.Forms;
using System.Reflection;
using System.Globalization;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Provides binding support for ItemPanel control.
    /// </summary>
    public class ItemVisualGenerator
    {
        #region Constructor
        private IBindingSupport _Parent = null;

        /// <summary>
        /// Initializes a new instance of the ItemVisualGenerator class.
        /// </summary>
        /// <param name="parent"></param>
        public ItemVisualGenerator(IBindingSupport parent)
        {
            _Parent = parent;
        }

        #endregion

        #region Implementation
        private object _DataSource = null;
        /// <summary>
        /// Gets or sets the data source for the ComboTree. Expected is an object that implements the IList or IListSource interfaces, 
        /// such as a DataSet or an Array. The default is null.
        /// </summary>
        [AttributeProvider(typeof(IListSource)), Description("Indicates data source for the ComboTree."), Category("Data"), DefaultValue(null), RefreshProperties(RefreshProperties.Repaint)]
        public object DataSource
        {
            get
            {
                return _DataSource;
            }
            set
            {
                if (((value != null) && !(value is IList)) && !(value is IListSource))
                {
                    throw new ArgumentException("Data type is not supported for complex data binding");
                }
                if (_DataSource != value)
                {
                    if (DesignMode)
                        _DataSource = value;
                    else
                        this.SetDataConnection(value, true);
                }
            }
        }

        private bool DesignMode
        {
            get
            {
                if (_Parent is IComponent && ((IComponent)_Parent).Site != null)
                    return ((IComponent)_Parent).Site.DesignMode;
                if (_Parent is Control && ((Control)_Parent).Site != null)
                    return ((Control)_Parent).Site.DesignMode;
                return false;
            }
        }

        private bool _InSetDataConnection = false;
        private void SetDataConnection(object newDataSource, bool force)
        {
            bool dataSourceChanged = _DataSource != newDataSource;

            if (!_InSetDataConnection)
            {
                try
                {
                    if (force || dataSourceChanged)
                    {
                        _InSetDataConnection = true;
                        IList list = (this.DataManager != null) ? this.DataManager.List : null;
                        bool isDataManagerNull = this.DataManager == null;
                        this.UnwireDataSource();
                        _DataSource = newDataSource;
                        this.WireDataSource();
                        if (_IsDataSourceInitialized)
                        {
                            CurrencyManager manager = null;
                            if (((newDataSource != null) && (_Parent.BindingContext != null)) && (newDataSource != Convert.DBNull))
                            {
                                string bindingPath = "";
                                if (_Bindings != null && _Bindings.Count > 0)
                                    bindingPath = _Bindings[0].BindingMemberInfo.BindingPath;
                                manager = (CurrencyManager)_Parent.BindingContext[newDataSource, bindingPath];
                            }
                            if (_DataManager != manager)
                            {
                                if (_DataManager != null)
                                {
                                    _DataManager.ItemChanged -= new ItemChangedEventHandler(this.DataManager_ItemChanged);
                                    _DataManager.PositionChanged -= new EventHandler(this.DataManager_PositionChanged);
                                }
                                _DataManager = manager;
                                if (_DataManager != null)
                                {
                                    _DataManager.ItemChanged += new ItemChangedEventHandler(this.DataManager_ItemChanged);
                                    _DataManager.PositionChanged += new EventHandler(this.DataManager_PositionChanged);
                                }
                            }
                            if (((_DataManager != null) && (dataSourceChanged)) && !ValidateDisplayMembers(_Bindings))
                            {
                                throw new ArgumentException("Wrong Bindings parameter", "newBindings");
                            }
                            if ((_DataManager != null && (dataSourceChanged || force))
                                && (dataSourceChanged || (force && ((list != _DataManager.List) || isDataManagerNull))))
                            {
                                DataManager_ItemChanged(_DataManager, null);
                            }
                        }
                        _Converters.Clear();
                    }
                }
                finally
                {
                    _InSetDataConnection = false;
                }
            }
        }

        private bool ValidateDisplayMembers(List<BindingDef> members)
        {
            if (members == null || members.Count == 0) return true;

            foreach (BindingDef item in members)
            {
                if (item.BindingMemberInfo != null && !BindingMemberInfoInDataManager(item.BindingMemberInfo))
                    return false;
            }
            return true;
        }
        private bool BindingMemberInfoInDataManager(BindingMemberInfo bindingMemberInfo)
        {
            if (_DataManager != null)
            {
                PropertyDescriptorCollection itemProperties = _DataManager.GetItemProperties();
                int count = itemProperties.Count;
                for (int i = 0; i < count; i++)
                {
                    if (!typeof(IList).IsAssignableFrom(itemProperties[i].PropertyType) && itemProperties[i].Name.Equals(bindingMemberInfo.BindingField))
                    {
                        return true;
                    }
                }
                for (int j = 0; j < count; j++)
                {
                    if (!typeof(IList).IsAssignableFrom(itemProperties[j].PropertyType) && (string.Compare(itemProperties[j].Name, bindingMemberInfo.BindingField, true, CultureInfo.CurrentCulture) == 0))
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private List<BindingDef> _Bindings = new List<BindingDef>();
        public List<BindingDef> Bindings
        {
            get { return _Bindings; }
            set
            {
                if (value != _Bindings)
                {
                    List<BindingDef> oldValue = _Bindings;
                    _Bindings = value;
                    OnBindingsChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Bindings property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnBindingsChanged(List<BindingDef> oldValue, List<BindingDef> newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Bindings"));
            
        }

        private CurrencyManager _DataManager = null;
        internal CurrencyManager DataManager
        {
            get
            {
                return _DataManager;
            }
        }
        private bool _IsDataSourceInitEventHooked = false;
        private void UnwireDataSource()
        {
            if (_DataSource is IComponent)
            {
                ((IComponent)_DataSource).Disposed -= new EventHandler(DataSourceDisposed);
            }
            ISupportInitializeNotification dataSource = _DataSource as ISupportInitializeNotification;
            if ((dataSource != null) && _IsDataSourceInitEventHooked)
            {
                dataSource.Initialized -= new EventHandler(DataSourceInitialized);
                _IsDataSourceInitEventHooked = false;
            }
        }
        private void DataSourceDisposed(object sender, EventArgs e)
        {
            this.SetDataConnection(null, true);
        }
        private bool _IsDataSourceInitialized = false;
        private void WireDataSource()
        {
            if (_DataSource is IComponent)
            {
                ((IComponent)_DataSource).Disposed += new EventHandler(DataSourceDisposed);
            }
            ISupportInitializeNotification dataSource = _DataSource as ISupportInitializeNotification;
            if ((dataSource != null) && !dataSource.IsInitialized)
            {
                dataSource.Initialized += new EventHandler(DataSourceInitialized);
                _IsDataSourceInitEventHooked = true;
                _IsDataSourceInitialized = false;
            }
            else
            {
                _IsDataSourceInitialized = true;
            }
        }
        private void DataSourceInitialized(object sender, EventArgs e)
        {
            this.SetDataConnection(_DataSource, true);
        }

        private void DataManager_ItemChanged(object sender, ItemChangedEventArgs e)
        {
            if (_Parent is Control)
            {
                Control parentControl = (Control)_Parent;
                if (parentControl.InvokeRequired)
                {
                    parentControl.Invoke(new ItemChangedEventHandler(DataManager_ItemChanged), sender, e);
                    return;
                }
            }
            if (_DataManager != null)
            {
                if (e == null || e.Index == -1)
                {
                    this.SetItemsCore(_DataManager.List);
                    if (_Parent.AllowSelection)
                    {
                        _Parent.SelectedIndex = _DataManager.Position;
                    }
                }
                else
                {
                    this.SetItemCore(e.Index, _DataManager.List[e.Index]);
                }
            }
        }
        private void DataManager_PositionChanged(object sender, EventArgs e)
        {
            if ((_DataManager != null) && _Parent.AllowSelection)
            {
                _Parent.SelectedIndex = _DataManager.Position;
            }
        }

        private ICloneable _VisualTemplate = null;
        /// <summary>
        /// Gets or sets the visual template that is generated for each data item.
        /// </summary>
        public ICloneable VisualTemplate
        {
            get { return _VisualTemplate; }
            set
            {
                if (value != _VisualTemplate)
                {
                    ICloneable oldValue = _VisualTemplate;
                    _VisualTemplate = value;
                    OnVisualTemplateChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when VisualTemplate property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnVisualTemplateChanged(ICloneable oldValue, ICloneable newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("VisualTemplate"));
        }

        /// <summary>
        /// When overridden in a derived class, sets the specified array of objects in a collection in the derived class.
        /// </summary>
        /// <param name="items">An array of items.</param>
        protected virtual void SetItemsCore(IList items)
        {
            _Parent.BeginUpdate();

            _Parent.ItemsCollection.Clear();

            for (int i = 0; i < items.Count; i++)
            {
                object item = items[i];
                CreateVisualItem(_Parent.ItemsCollection, item, i, _Bindings);
            }

            _Parent.EndUpdate();
        }

        /// <summary>
        /// Creates a new item from template for the data.
        /// </summary>
        /// <param name="item">Data to create item for.</param>
        /// <returns>New instance of the BaseItem.</returns>
        private object CreateVisualItem(IList parent, object item, int itemIndex, List<BindingDef> bindings)
        {
            object visualItem = _VisualTemplate.Clone();
            parent.Add(visualItem);

            SetVisualItemData(visualItem, item, bindings, itemIndex);

            DataItemVisualEventArgs eventArgs = new DataItemVisualEventArgs(visualItem, item);
            _Parent.InvokeDataNodeCreated(eventArgs);

            return eventArgs.Visual;
        }

        private void SetVisualItemData(object visualItem, object item, List<BindingDef> bindings, int bindingIndex)
        {
            PropertyInfo tagProperty = visualItem.GetType().GetProperty("Tag");
            if (tagProperty != null)
                tagProperty.SetValue(visualItem, new ItemBindingData(item, bindingIndex), null);

            foreach (BindingDef binding in bindings)
            {
                binding.Update(this, visualItem, item);
            }
        }

        /// <summary>
        /// When overridden in a derived class, sets the object with the specified index in the derived class.
        /// </summary>
        /// <param name="index">The array index of the object.</param>
        /// <param name="value">The object.</param>
        protected virtual void SetItemCore(int index, object value)
        {
            object visual = _Parent.ItemsCollection[index];
            if (visual == null) return;

            _Parent.BeginUpdate();
            SetVisualItemData(visual, value, _Bindings, index);
            _Parent.EndUpdate();
        }

        /// <summary>
        /// When overridden in a derived class, resynchronizes the item data with the contents of the data source.
        /// </summary>
        public virtual void RefreshItems()
        {
            if (_DataManager != null)
            {
                SetItemsCore(_DataManager.List);
                if (_Parent.AllowSelection)
                {
                    _Parent.SelectedIndex = _DataManager.Position;
                }
            }
        }

        private Hashtable _Converters = new Hashtable();
        internal TypeConverter GetFieldConverter(string fieldName)
        {
            if (_Converters.ContainsKey(fieldName))
                return (TypeConverter)_Converters[fieldName];
            if (_DataManager != null)
            {
                PropertyDescriptorCollection itemProperties = _DataManager.GetItemProperties();
                if (itemProperties != null)
                {
                    PropertyDescriptor descriptor = itemProperties.Find(fieldName, true);
                    if (descriptor != null)
                    {
                        _Converters.Add(fieldName, descriptor.Converter);
                        return descriptor.Converter;
                    }
                }
            }

            return null;
        }
        #endregion
    }

    public interface IBindingSupport
    {
        /// <summary>
        /// Gets or sets whether selection is allowed.
        /// </summary>
        bool AllowSelection { get;set;}
        /// <summary>
        /// Gets or sets the selected item index.
        /// </summary>
        int SelectedIndex { get;set;}
        /// <summary>
        /// Gets or sets the BindingContext.
        /// </summary>
        BindingContext BindingContext { get;set;}
        IList ItemsCollection { get; }
        void BeginUpdate();
        void EndUpdate();
        void InvokeDataNodeCreated(DataItemVisualEventArgs e);
    }

    /// <summary>
    /// Defines delegate for data visual creation based events.
    /// </summary>
    public delegate void DataItemVisualEventHandler(object sender, DataItemVisualEventArgs e);
    /// <summary>
    /// Defines event arguments for data visual creation based events.
    /// </summary>
    public class DataItemVisualEventArgs : EventArgs
    {
        /// <summary>
        /// Gets or sets the visual that is created for data item.
        /// </summary>
        public object Visual = null;
        /// <summary>
        /// Gets the data-item node is being created for.
        /// </summary>
        public readonly object DataItem = null;

        /// <summary>
        /// Initializes a new instance of the DataNodeEventArgs class.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="dataItem"></param>
        public DataItemVisualEventArgs(object visual, object dataItem)
        {
            Visual = visual;
            DataItem = dataItem;
        }
    }
}
