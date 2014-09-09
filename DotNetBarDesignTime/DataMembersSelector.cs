using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Threading;
using System.Collections;

namespace DevComponents.DotNetBar.Design
{
    public class DataMembersSelector : UITypeEditor
    {
        #region Constructor
        private CheckedListBoxSelector _ListSelector;
        private BindingContext _BindingContext;
        /// <summary>
        /// Initializes a new instance of the DataMembersSelector class.
        /// </summary>
        public DataMembersSelector()
        {
            _ListSelector = new CheckedListBoxSelector();
            _BindingContext = new BindingContext();
        }
        
        #endregion

        #region Implementation
        public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            if (context != null
                && context.Instance != null
                && provider != null)
            {
                IWindowsFormsEditorService edSvc = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
                if (edSvc != null)
                {
                    PropertyDescriptor descriptor = TypeDescriptor.GetProperties(context.Instance)["DataSource"];
                    if (descriptor == null)
                    {
                        return value;
                    }
                    object dataSource = descriptor.GetValue(context.Instance);
                    LoadDataMembers(_ListSelector, dataSource, value);

                    edSvc.DropDownControl(_ListSelector);

                    string returnValue = "";
                    foreach (string item in _ListSelector.ListBox.CheckedItems)
                    {
                        if (returnValue.Length > 0) returnValue += ",";
                        returnValue += item;
                    }
                    return returnValue;
                }
            }
            return value;
        }

        private bool IsSelected(string str,ref string[] selected)
        {
            string strl = str.ToLower();
            for (int i = 0; i < selected.Length; i++)
            {
                string item = selected[i];
                if (item.Trim().ToLower() == strl)
                {
                    selected[i] = str;
                    return true;
                }
            }
            return false;
        }
        private void LoadDataMembers(CheckedListBoxSelector listSelector, object dataSource, object value)
        {
            listSelector.ListBox.Items.Clear();
            string v = value as string;
            if (v == null) v = "";
            string[] selected = v.Split(',');
            string dataMember = string.Empty;

            if (dataSource is Type)
            {
                try
                {
                    BindingSource source = new BindingSource();
                    source.DataSource = dataSource;
                    dataSource = source.List;
                }
                catch (Exception exception)
                {
                    if (IsCriticalException(exception))
                    {
                        throw;
                    }
                }
                catch
                {
                }
            }
            if (this.IsBindableDataSource(dataSource))
            {
                PropertyDescriptorCollection properties = GetItemProperties(dataSource, string.Empty);
                if (properties == null)
                    return;
                for (int i = 0; i < properties.Count; i++)
                {
                    PropertyDescriptor property = properties[i];
                    if (this.IsBindableDataMember(property))
                    {
                        string str = string.IsNullOrEmpty(dataMember) ? property.Name : (dataMember + "." + property.Name);
                        _ListSelector.ListBox.Items.Add(str, IsSelected(str,ref selected));
                    }
                }
                if (selected.Length > 1)
                {
                    for (int i = 0; i < selected.Length; i++)
                    {
                        string s1 = selected[i];
                        int i1 = _ListSelector.ListBox.Items.IndexOf(s1);
                        _ListSelector.ListBox.Items.RemoveAt(i1);
                        _ListSelector.ListBox.Items.Insert(i, s1);
                        _ListSelector.ListBox.SetItemChecked(i, true);
                    }
                }
            }
        }

        private bool IsBindableDataMember(PropertyDescriptor property)
        {
            if (!typeof(byte[]).IsAssignableFrom(property.PropertyType))
            {
                ListBindableAttribute attribute = (ListBindableAttribute)property.Attributes[typeof(ListBindableAttribute)];
                if ((attribute != null) && !attribute.ListBindable)
                {
                    return false;
                }
            }
            return true;
        }
        private PropertyDescriptorCollection GetItemProperties(object dataSource, string dataMember)
        {
            CurrencyManager manager = (CurrencyManager)_BindingContext[dataSource, dataMember];
            if (manager != null)
            {
                return manager.GetItemProperties();
            }
            return null;
        }
        private bool IsBindableDataSource(object dataSource)
        {
            if ((!(dataSource is IListSource) && !(dataSource is IList)) && !(dataSource is Array))
            {
                return false;
            }
            ListBindableAttribute attribute = (ListBindableAttribute)TypeDescriptor.GetAttributes(dataSource)[typeof(ListBindableAttribute)];
            if ((attribute != null) && !attribute.ListBindable)
            {
                return false;
            }
            return true;
        }
        private static bool IsCriticalException(Exception ex)
        {
            return (((((ex is NullReferenceException) || (ex is StackOverflowException)) || ((ex is OutOfMemoryException) ||
                (ex is ThreadAbortException))) || ((ex is ExecutionEngineException) ||
                (ex is IndexOutOfRangeException))) || (ex is AccessViolationException));
        }


        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        #endregion
    }
}
