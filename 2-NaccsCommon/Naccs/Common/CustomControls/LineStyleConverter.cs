namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing.Drawing2D;
    using System.Globalization;

    public class LineStyleConverter : ExpandableObjectConverter
    {
        public override bool CanConvertFrom(ITypeDescriptorContext context, Type sourceType)
        {
            return ((sourceType == typeof(string)) || base.CanConvertFrom(context, sourceType));
        }

        public override bool CanConvertTo(ITypeDescriptorContext context, Type destinationType)
        {
            return ((destinationType == typeof(LineStyle)) || base.CanConvertTo(context, destinationType));
        }

        public override object ConvertFrom(ITypeDescriptorContext context, CultureInfo culture, object value)
        {
            if (value is string)
            {
                string[] strArray = ((string) value).Split(new char[] { ',' });
                LineStyle component = new LineStyle();
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(LineStyle));
                PropertyDescriptor descriptor = properties["Alignment"];
                descriptor.SetValue(component, descriptor.Converter.ConvertFromInvariantString(strArray[0].Trim()));
                descriptor = properties["Color"];
                descriptor.SetValue(component, descriptor.Converter.ConvertFromInvariantString(strArray[1].Trim()));
                descriptor = properties["DashStyle"];
                descriptor.SetValue(component, descriptor.Converter.ConvertFromInvariantString(strArray[2].Trim()));
                descriptor = properties["Width"];
                descriptor.SetValue(component, descriptor.Converter.ConvertFromInvariantString(strArray[3].Trim()));
                return component;
            }
            return base.ConvertFrom(context, culture, value);
        }

        public override object ConvertTo(ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if ((destinationType != typeof(string)) || !(value is LineStyle))
            {
                return base.ConvertTo(context, culture, value, destinationType);
            }
            LineStyle component = (LineStyle) value;
            if (component.DashStyle == DashStyle.Custom)
            {
                component.DashStyle = DashStyle.Solid;
            }
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
            PropertyDescriptor descriptor = properties["Alignment"];
            string str = descriptor.Converter.ConvertToInvariantString(component.Alignment) + ", ";
            descriptor = properties["Color"];
            str = str + descriptor.Converter.ConvertToInvariantString(component.Color) + ", ";
            descriptor = properties["DashStyle"];
            str = str + descriptor.Converter.ConvertToInvariantString(component.DashStyle) + ", ";
            descriptor = properties["Width"];
            return (str + descriptor.Converter.ConvertToInvariantString(component.Width));
        }
    }
}

