using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;

namespace DevComponents.DotNetBar.Design
{
    public class RadialMenuItemDesigner : BaseItemDesigner
    {
        #region Implementation
        private void RemoveDescriptors(System.Collections.IDictionary properties, String[] propNames)
        {
            foreach (String propName in propNames)
            {
                if (properties.Contains(propName))
                {
                    if (properties[propName] is PropertyDescriptor)
                        properties[propName] = TypeDescriptor.CreateProperty(this.Component.GetType(), (PropertyDescriptor)properties[propName], new BrowsableAttribute(false));
                    else if (properties[propName] is EventDescriptor)
                        properties[propName] = TypeDescriptor.CreateEvent(this.Component.GetType(), (EventDescriptor)properties[propName], new BrowsableAttribute(false));
                    else
                        properties.Remove(propName);
                }
            }
        }

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);
            RemoveDescriptors(properties,
                new String[] {  "ItemAlignment", 
                                "GlobalItem",
                                "AccessibleDefaultActionDescription",
                                "AccessibleDescription",
                                "AccessibleName",
                                "AccessibleRole",
                                "CanCustomize",
                                "Category",
                                "KeyTips",
                                "Shortcuts",
                                "ThemeAware",
                                "GlobalName", 
                                "Stretch", 
                                "BeginGroup" });
        }
        #endregion
    }
}
