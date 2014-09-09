using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Rendering;
using System.Globalization;

namespace DevComponents.DotNetBar.Design
{
    public class ColumnNamesEditor : UITypeEditor
    {
        #region Constructor
        private TextBoxX _TextBox = null;
        /// <summary>
        /// Initializes a new instance of the ColumnNamesEditor class.
        /// </summary>
        public ColumnNamesEditor()
        {
            _TextBox = new TextBoxX();
            _TextBox.Multiline = true;
            _TextBox.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            _TextBox.WatermarkBehavior = eWatermarkBehavior.HideNonEmpty;
            _TextBox.WatermarkText = "Use Enter key to separate custom column names";
            _TextBox.Border.Class = ElementStyleClassKeys.TextBoxBorderKey;
            _TextBox.AcceptsReturn = true;
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

                    _TextBox.Text = (string)value;
                    _TextBox.MinimumSize = new System.Drawing.Size(24, 120);
                    edSvc.DropDownControl(_TextBox);

                    return _TextBox.Text;
                }
            }
            return value;
        }

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            return UITypeEditorEditStyle.DropDown;
        }
        #endregion
    }

    #region ColumnNamesConverter
    public class ColumnNamesConverter : TypeConverter
    {
        // Methods
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }
        const string tempCommaReplacement = "```";
        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value != null && value is string)
            {
                string s = ((string)value).Replace(",,", tempCommaReplacement);
                s = s.Replace(",", "\r\n");
                s = s.Replace(tempCommaReplacement, ",");
                return s;
            }
            return value;
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType != typeof(string)) || !(value is string))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            if (value != null)
            {
                string s = ((string)value).Replace(",", tempCommaReplacement);
                s = s.Replace("\r\n", ",");
                s = s.Replace(tempCommaReplacement, ",");
                return s;
            }
            return "";
        }
    }
    #endregion

 

}
