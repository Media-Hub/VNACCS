namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;

    public class PageFormatConverter : StringConverter
    {
        public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
        {
            return new TypeConverter.StandardValuesCollection(new string[] { "Page", "PageMax", "PageProgress", "PageTitle", "PageTitle2", "PrintDate", "PrintTime", "FileName", "JobName", "JobCode", "ReceiveDateTime" });
        }

        public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
        {
            return false;
        }

        public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
        {
            return true;
        }
    }
}

