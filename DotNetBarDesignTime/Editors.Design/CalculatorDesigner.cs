using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using DevComponents.Editors;

namespace DevComponents.DotNetBar.Design
{
    public class CalculatorDesigner : ControlDesigner
    {
        #region Internal Implementation
        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            Calculator mc = this.Control as Calculator;
            if (mc != null)
            {
                mc.AutoSize = true;
            }
            base.InitializeNewComponent(defaultValues);
        }

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);
            RemoveDescriptors(properties,
                new String[] { "AutoScrollMargin", 
                                 "AlwaysDisplayKeyAccelerators", 
                                 "AutoScroll", 
                                 "AutoScrollMinSize", 
                                 "AutoValidate", 
                                 "BorderStyle",
                                 "ForeColor",
                                 "DisabledImagesGrayScale", 
                                 "DispatchShortcuts", 
                                 "Enabled",
                                 "BackColor", 
                                 "BackgroundImage", 
                                 "BackgroundImageLayout", 
                                 "Margin", 
                                 "MaximumSize", 
                                 "MinimumSize", 
                                 "Padding", 
                                 "RightToLeft", 
                                 "TabStop",
                                 "TabIndex",
                                 "UseWaitCursor", 
                                 "ImeMode" });
        }
        private void RemoveDescriptors(System.Collections.IDictionary properties, String[] propNames)
        {
            foreach (String propName in propNames)
            {
                if (properties.Contains(propName))
                    properties.Remove(propName);
            }
        }
        protected override void PreFilterEvents(System.Collections.IDictionary events)
        {
            RemoveDescriptors(events,
                new string[] {   
                                 "AutoSizeChanged",
                                 "AutoValidate",
                                 "BackColorChanged", 
                                 "BackgroundImageChanged", 
                                 "BackgroundImageLayoutChanged", 
                                 "BindingContext",
                                 "BackgroundStyle", 
                                 "ButtonCheckedChanged", 
                                 "CausesValidationChanged", 
                                 "ChangeUICues", 
                                 "ClientSizeChanged", 
                                 "ContainerControlDeserialize", 
                                 "ContainerControlSerialize", 
                                 "ContainerLoadControl", 
                                 "Enter", 
                                 "Leave", 
                                 "ContextMenuStripChanged", 
                                 "ControlAdded", 
                                 "ControlRemoved", 
                                 "CursorChanged", 
                                 "DefinitionLoaded", 
                                 "DockChanged", 
                                 "EnabledChanged", 
                                 "ExpandedChange", 
                                 "FontChanged", 
                                 "ForeColorChanged", 
                                 "ImeModeChanged", 
                                 "KeyDown", 
                                 "KeyPress", 
                                 "KeyUp", 
                                 "Layout", 
                                 "MarginChanged", 
                                 "PaddingChanged", 
                                 "Paint", 
                                 "ParentChanged", 
                                 "PreviewKeyDown",
                                 "RegionChanged", 
                                 "Scroll", 
                                 "RightToLeftChanged", 
                                 "StyleChanged", 
                                 "SystemColorsChanged", 
                                 "TabIndexChanged", 
                                 "TabStopChanged", 
                                 "TextChanged", 
                                 "Resize", 
                                 "Validating",
                                 "Validated",
                                 "VisibleChanged" });
            base.PreFilterEvents(events);
        }
        #endregion
    }
}
