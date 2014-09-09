using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.Reflection;
using System.Collections;

namespace DevComponents.DotNetBar.Design
{
    public class MetroTileFrameDesigner : ComponentDesigner
    {
        private static bool SetGenerateMemberProperty(
         IExtenderListService service,
         IComponent component,
         bool value)
        {
            IExtenderProvider provider = null;
            IExtenderProvider[] providers = service.GetExtenderProviders();
            foreach (IExtenderProvider item in providers)
            {
                if (item.GetType().FullName ==
                       "System.ComponentModel.Design.Serialization.CodeDomDesignerLoader+ModifiersExtenderProvider")
                {
                    provider = item;
                    break;
                }
            }

            if (provider == null) return false;

            MethodInfo methodInfo =
               provider.GetType().GetMethod(
                  "SetGenerateMember", BindingFlags.Public |
                                       BindingFlags.Instance);

            if (methodInfo != null)
            {
                methodInfo.Invoke(
                   provider, new object[]
                            {
                               component,
                               value
                            });
                return true;
            }

            return false;
        }

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            // no reason to create a member since container has no use aside from logical grouping at design time
            SetGenerateMemberProperty(
               (IExtenderListService)GetService(typeof(IExtenderListService)),
               this.Component,
               false);

            base.InitializeNewComponent(defaultValues);
        }
    }
}
