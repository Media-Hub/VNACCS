using System;
using System.Text;
using System.Windows.Forms.Design;
using DevComponents.Editors.DateTimeAdv;
using System.Drawing;
using DevComponents.DotNetBar.Rendering;
using System.ComponentModel;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Design
{
    public class TimeSelectorDesigner : ControlDesigner
    {
        #region Private Variables
        #endregion

        #region Constructor
        #endregion

        #region Internal Implementation
        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            TimeSelector mc = this.Control as TimeSelector;
            if (mc != null)
            {
                mc.BackgroundStyle.Class = ElementStyleClassKeys.ItemPanelKey;
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
                                 "DisabledImagesGrayScale", 
                                 "DispatchShortcuts", 
                                 "Enabled",
                                 "Images", 
                                 "ImageSize", 
                                 "ImagesLarge", 
                                 "ImagesMedium", 
                                 "KeyTipsFont", 
                                 "BackColor", 
                                 "BackgroundImage", 
                                 "BackgroundImageLayout", 
                                 "Margin", 
                                 "MaximumSize", 
                                 "MinimumSize", 
                                 "Padding", 
                                 "RightToLeft", 
                                 "ShowShortcutKeysInToolTips", 
                                 "ShowToolTips", 
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
                new string[] { "BackColorChanged", 
                                 "BackgroundImageChanged", 
                                 "BackgroundImageLayoutChanged", 
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
                                 "ItemAdded", 
                                 "ItemClick", 
                                 "ItemDoubleClick", 
                                 "ItemLayoutUpdated", 
                                 "ItemRemoved", 
                                 "ItemTextChanged", 
                                 "KeyDown", 
                                 "KeyPress", 
                                 "KeyUp", 
                                 "Layout", 
                                 "MarginChanged", 
                                 "OptionGroupChanging", 
                                 "PaddingChanged", 
                                 "Paint", 
                                 "ParentChanged", 
                                 "PopupClose", 
                                 "PopupContainerLoad", 
                                 "PopupContainerUnload", 
                                 "PopupOpen", 
                                 "PopupShowing", 
                                 "RegionChanged", 
                                 "Scroll", 
                                 "RightToLeftChanged", 
                                 "StyleChanged", 
                                 "SystemColorsChanged", 
                                 "TabIndexChanged", 
                                 "TabStopChanged", 
                                 "TextChanged", 
                                 "Resize", 
                                 "ToolTipShowing", 
                                 "UserCustomize", 
                                 "VisibleChanged" });
            base.PreFilterEvents(events);
        }
        #endregion
    }
}
