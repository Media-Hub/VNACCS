using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Globalization;
using System.Drawing;
using System.Security;
using System.Reflection;
using System.Collections;
using System.ComponentModel.Design.Serialization;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Defines class which described single binding.
    /// </summary>
    [TypeConverter(typeof(BindingDefConverer)), DesignTimeVisible(false), ToolboxItem(false)]
    public class BindingDef : INotifyPropertyChanged
    {
        #region Dependency Properties & Events
        public event DataConvertEventHandler Format;
        /// <summary>
        /// Raises Format event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnFormat(DataConvertEventArgs e)
        {
            DataConvertEventHandler handler = Format;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the BindingDef class.
        /// </summary>
        public BindingDef()
        {

        }
        /// <summary>
        /// Initializes a new instance of the BindingDef class.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="dataMember"></param>
        public BindingDef(string propertyName, string dataMember)
        {
            _PropertyName = propertyName;
            _PropertyPath = propertyName.Split('.');
            _DataMember = dataMember;
            _BindingMemberInfo = new BindingMemberInfo(dataMember);
        }
        #endregion

        #region Implementation
        /// <summary>
        /// Updates specified item PropertyName with the data from data object property specified by DataMember. 
        /// </summary>
        /// <param name="item">Item to set PropertyName on.</param>
        /// <param name="data">Data to retrieve DataMember value from.</param>
        public void Update(object item, object data)
        {
            Update(null, item, data);
        }

        /// <summary>
        /// Updates specified item PropertyName with the data from data object property specified by DataMember. 
        /// </summary>
        /// <param name="dataManager">CurrencyManager to use</param>
        /// <param name="item">Item to set PropertyName on.</param>
        /// <param name="data">Data to retrieve DataMember value from.</param>
        public void Update(ItemVisualGenerator generator, object item, object data)
        {
            string propertyName = _PropertyName;

            if (item is BaseItem && _PropertyPath.Length > 1)
            {
                // Dig into child items
                BaseItem parent = (BaseItem)item;
                for (int i = 0; i < _PropertyPath.Length - 1; i++)
                {
                    parent = parent.SubItems[_PropertyPath[i]];
                }
                item = parent;
                propertyName = _PropertyPath[_PropertyPath.Length - 1];
            }
            
            PropertyInfo targetProp = item.GetType().GetProperty(propertyName);
            if (targetProp.PropertyType == typeof(string))
                targetProp.SetValue(item, GetDataText(generator, data, _DataMember), null);
            else
                targetProp.SetValue(item, GetPropertyValue(generator, data, _DataMember), null);
        }

        private static TypeConverter stringTypeConverter;
        private string GetDataText(ItemVisualGenerator generator, object data, string fieldName)
        {
            object propertyValue = GetPropertyValue(generator, data, fieldName);
            return GetDataText(generator, data, fieldName, propertyValue);
        }
        private string GetDataText(ItemVisualGenerator generator, object data, string fieldName, object propertyValue)
        {
            if (!_FormattingEnabled)
            {
                if (data == null)
                {
                    return string.Empty;
                }
                if (propertyValue == null)
                {
                    return "";
                }
                return Convert.ToString(propertyValue, CultureInfo.CurrentCulture);
            }

            DataConvertEventArgs e = new DataConvertEventArgs(propertyValue, typeof(string), data, fieldName);
            this.OnFormat(e);
            if ((e.Value != data) && (e.Value is string))
            {
                return (string)e.Value;
            }
            if (stringTypeConverter == null)
            {
                stringTypeConverter = TypeDescriptor.GetConverter(typeof(string));
            }
            try
            {
                return (string)FormatHelper.FormatObject(propertyValue, typeof(string), generator.GetFieldConverter(fieldName), stringTypeConverter, _FormatString, _FormatInfo, null, DBNull.Value);
            }
            catch (Exception ex)
            {
                if (ex is SecurityException || IsCriticalException(ex))
                {
                    throw;
                }
                return ((propertyValue != null) ? Convert.ToString(data, CultureInfo.CurrentCulture) : "");
            }
        }

        private static bool IsCriticalException(Exception ex)
        {
            return (((((ex is NullReferenceException) || (ex is StackOverflowException)) || ((ex is OutOfMemoryException) || (ex is System.Threading.ThreadAbortException))) || ((ex is ExecutionEngineException) || (ex is IndexOutOfRangeException))) || (ex is AccessViolationException));
        }

        private object GetPropertyValue(ItemVisualGenerator generator, object data, string fieldName)
        {
            if ((data != null) && (fieldName.Length > 0))
            {
                try
                {
                    PropertyDescriptor descriptor;
                    if (generator.DataManager != null)
                    {
                        descriptor = generator.DataManager.GetItemProperties().Find(fieldName, true);
                    }
                    else
                    {
                        descriptor = TypeDescriptor.GetProperties(data).Find(fieldName, true);
                    }
                    if (descriptor != null)
                    {
                        data = descriptor.GetValue(data);
                    }
                }
                catch
                {
                }
            }

            if (data == DBNull.Value && _NullValue != null)
                return _NullValue;

            return data;
        }
        #endregion

        #region Properties
        private string[] _PropertyPath = new string[0];
        private string _PropertyName = "";
        /// <summary>
        /// Gets or sets the property name binding is attached to.
        /// </summary>
        [DefaultValue(""), Category("Data"), Description("Indicates property name binding is attached to.")]
        public string PropertyName
        {
            get { return _PropertyName; }
            set
            {
                if (value != _PropertyName)
                {
                    string oldValue = _PropertyName;
                    _PropertyName = value;
                    OnPropertyNameChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when PropertyName property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnPropertyNameChanged(string oldValue, string newValue)
        {
            _PropertyPath = newValue.Split('.');
            OnPropertyChanged(new PropertyChangedEventArgs("PropertyName"));
        }

        /// <summary>
        /// Gets the reference to BindingMemberInfo created based on DataMember.
        /// </summary>
        [Browsable(false)]
        public BindingMemberInfo BindingMemberInfo
        {
            get
            {
                return _BindingMemberInfo;
            }
        }

        private BindingMemberInfo _BindingMemberInfo;
        private string _DataMember = "";
        /// <summary>
        /// Gets or sets the data member name which holds data that PropertyName is populated with.
        /// </summary>
        [DefaultValue(""), Category("Data"), Description("Indicates data member name which holds data that PropertyName is populated with.")]
        public string DataMember
        {
            get { return _DataMember; }
            set
            {
                if (value == null) value = "";
                if (value != _DataMember)
                {
                    string oldValue = _DataMember;
                    _DataMember = value;
                    OnDataMemberChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when DataMember property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnDataMemberChanged(string oldValue, string newValue)
        {
            if (!string.IsNullOrEmpty(newValue))
                _BindingMemberInfo = new BindingMemberInfo(newValue);
            else
                _BindingMemberInfo = new BindingMemberInfo();
            OnPropertyChanged(new PropertyChangedEventArgs("DataMember"));
        }

        private bool _FormattingEnabled = false;
        /// <summary>
        /// Gets or sets whether type conversion and formatting is applied to the property data.
        /// </summary>
        [DefaultValue(false), Category("Data"), Description("Indicates whether type conversion and formatting is applied to the property data.")]
        public bool FormattingEnabled
        {
            get { return _FormattingEnabled; }
            set
            {
                if (value != _FormattingEnabled)
                {
                    bool oldValue = _FormattingEnabled;
                    _FormattingEnabled = value;
                    OnFormattingEnabledChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when FormattingEnabled property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnFormattingEnabledChanged(bool oldValue, bool newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("FormattingEnabled"));

        }

        private string _FormatString = "";
        /// <summary>
        /// Gets or sets format specifier characters that indicate how a value is to be displayed.
        /// For more information, see Formatting Overview: http://msdn.microsoft.com/en-us/library/26etazsy%28v=vs.71%29.aspx 
        /// </summary>
        [DefaultValue(""), Category("Data"), Description("Indicates format specifier characters that indicate how a value is to be displayed.")]
        public string FormatString
        {
            get { return _FormatString; }
            set
            {
                if (value != _FormatString)
                {
                    string oldValue = _FormatString;
                    _FormatString = value;
                    OnFormatStringChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when FormatString property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnFormatStringChanged(string oldValue, string newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("FormatString"));

        }

        private IFormatProvider _FormatInfo = null;
        /// <summary>
        /// Gets or sets the IFormatProvider that provides custom formatting behavior. 
        /// </summary>
        [DefaultValue(null), Category("Data"), Description("Indicates IFormatProvider that provides custom formatting behavior.")]
        public IFormatProvider FormatInfo
        {
            get { return _FormatInfo; }
            set
            {
                if (value != _FormatInfo)
                {
                    IFormatProvider oldValue = _FormatInfo;
                    _FormatInfo = value;
                    OnFormatInfoChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when FormatInfo property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnFormatInfoChanged(IFormatProvider oldValue, IFormatProvider newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("FormatInfo"));

        }

        private object _NullValue = null;
        /// <summary>
        /// Gets or sets the Object to be set as the target property when the data source contains a DBNull value. 
        /// The data source must contain DBNull for the NullValue property to be correctly applied. 
        /// If the data source type is a type such as a string or integer the value of the NullValue property will be ignored. 
        /// Also, the NullValue property is ignored if it is set to null. 
        /// </summary>
        [DefaultValue(null), Category("Data"), Description("Indicates Object to be set as the control property when the data source contains a DBNull value. ")]
        [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.StringConverter))]
        public object NullValue
        {
            get { return _NullValue; }
            set
            {
                if (value != _NullValue)
                {
                    object oldValue = _NullValue;
                    _NullValue = value;
                    OnNullValueChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when NullValue property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnNullValueChanged(object oldValue, object newValue)
        {
            OnPropertyChanged(new PropertyChangedEventArgs("NullValue"));
        }
        #endregion

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, e);
        }
        /// <summary>
        /// Occurs when property on BindingDef object has changed.
        /// </summary>
        [Description("Occurs when property on BindingDef object has changed.Occurs when property on BindingDef object has changed.")]
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion
    }

    /// <summary>
    /// Represents the method that will handle converting for bindings.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    public delegate void DataConvertEventHandler(object sender, DataConvertEventArgs e);
    public class DataConvertEventArgs : ConvertEventArgs
    {
        // Fields
        private object _DataItem;

        // Methods
        public DataConvertEventArgs(object value, Type desiredType, object dataItem, string fieldName)
            : base(value, desiredType)
        {
            _DataItem = dataItem;
            _FieldName = fieldName;
        }

        /// <summary>
        /// Gets the reference to the item being converted.
        /// </summary>
        public object DataItem
        {
            get
            {
                return _DataItem;
            }
        }

        private string _FieldName = "";
        /// <summary>
        /// Get the reference to the name of the field or property on the item that needs conversion.
        /// </summary>
        public string FieldName
        {
            get { return _FieldName; }
        }
    }

    /// <summary>
    /// Represents BindingDefConverer converter.
    /// </summary>
    public class BindingDefConverer : TypeConverter
    {
        /// <summary>
        /// Creates new instance of the class.
        /// </summary>
        public BindingDefConverer() { }

        /// <summary>
        /// Checks whether conversion can be made to specified type.
        /// </summary>
        /// <param name="context">Context Information.</param>
        /// <param name="destinationType">Destination type.</param>
        /// <returns></returns>
        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            if (destinationType == typeof(InstanceDescriptor))
                return true;
            return base.CanConvertTo(context, destinationType);
        }

        /// <summary>
        /// Converts object to specified type.
        /// </summary>
        /// <param name="context">Context information.</param>
        /// <param name="culture">Culture information.</param>
        /// <param name="value">Object to convert.</param>
        /// <param name="destinationType">Destination type.</param>
        /// <returns>Object converted to destination type.</returns>
        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == null)
                throw new ArgumentNullException("destinationType");

            if ((destinationType == typeof(InstanceDescriptor)) && (value is BindingDef))
            {
                BindingDef info = (BindingDef)value;
                Type[] constructorParams = null;
                MemberInfo constructorMemberInfo = null;
                object[] constructorValues = null;

                constructorParams = new Type[] { typeof(string), typeof(string) };
                constructorMemberInfo = typeof(BindingDef).GetConstructor(constructorParams);
                constructorValues = new object[] { info.PropertyName, info.DataMember };

                if (constructorMemberInfo != null)
                {
                    return new InstanceDescriptor(constructorMemberInfo, constructorValues);
                }
            }

            return base.ConvertTo(context, culture, value, destinationType);
        }
    }
}
