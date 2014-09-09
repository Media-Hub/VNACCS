namespace Naccs.Common.Function
{
    using Naccs.Common.Constant;
    using Naccs.Common.CustomControls;
    using System;

    public class PrtProperties : PropertiesCollection
    {
        public PrtProperties()
        {
            base.LoadProperties(DllResource.PrtPropertyList);
        }

        public static string getControlName(string tagName)
        {
            string name = null;
            if (tagName == "label")
            {
                return DesignControls.Label.Name;
            }
            if (tagName == "ranlabel")
            {
                return DesignControls.RanLabel.Name;
            }
            if (tagName == "checkbox")
            {
                return DesignControls.CheckBox.Name;
            }
            if (tagName == "radio")
            {
                return DesignControls.RadioButton.Name;
            }
            if (tagName == "page")
            {
                return DesignControls.Page.Name;
            }
            if (tagName == "ran")
            {
                return DesignControls.Ran.Name;
            }
            if (tagName == "prttextbox")
            {
                return DesignControls.PrtTextBox.Name;
            }
            if (tagName == "barcode")
            {
                return DesignControls.Barcode.Name;
            }
            if (tagName == "hand-ocr")
            {
                return DesignControls.HandOcr.Name;
            }
            if (tagName == "comp-ocr")
            {
                return DesignControls.CompOcr.Name;
            }
            if (tagName == "line")
            {
                return DesignControls.Line.Name;
            }
            if (tagName == "rectangle")
            {
                return DesignControls.Rectangle.Name;
            }
            if (tagName == "pagelabel")
            {
                return DesignControls.PageLabel.Name;
            }
            if (tagName == "gstomp")
            {
                name = DesignControls.GStamp.Name;
            }
            return name;
        }
    }
}

