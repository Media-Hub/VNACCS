#if FRAMEWORK20
using System;
using System.Text;
using System.Windows.Forms.Design;
using DevComponents.Editors.DateTimeAdv;
using DevComponents.DotNetBar;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Design
{
    public class DateTimeInputDesigner : VisualControlBaseDesigner
    {
        #region Internal Implementation
        /// <summary>
        /// Initializes a new instance of the DateTimeInputDesigner class.
        /// </summary>
        public DateTimeInputDesigner()
        {
            this.AutoResizeHandles = true;
        }

        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            DateTimeInput c = this.Control as DateTimeInput;
            if (c != null)
            {
                c.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = eColorSchemePart.PanelBackground;
                c.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = eColorSchemePart.PanelBackground2;
                c.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
                c.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = eColorSchemePart.BarBackground;
                c.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = eColorSchemePart.BarBackground2;
                c.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
                c.MonthCalendar.CommandsBackgroundStyle.BorderTop = eStyleBorderType.Solid;
                c.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = eColorSchemePart.BarDockedBorder;
                c.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
                c.ButtonDropDown.Visible = true;
                c.MonthCalendar.TodayButtonVisible = true;
                c.MonthCalendar.ClearButtonVisible = true;
                c.ButtonDropDown.Shortcut = eShortcut.AltDown;
                c.Style = eDotNetBarStyle.StyleManagerControlled;
            }
            base.InitializeNewComponent(defaultValues);
        }

        public override System.Windows.Forms.Design.SelectionRules SelectionRules
        {
            get
            {
                SelectionRules rules = base.SelectionRules;
                if (!this.Control.AutoSize)
                {
                    rules &= ~(SelectionRules.BottomSizeable | SelectionRules.TopSizeable);
                }
                return rules;
            }
        }

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
                new String[] { "AllowDrop", 
                                 "BackColor",
                                 "MaximumSize", 
                                 "MinimumSize", 
                                 "RightToLeft", 
                                 "UseWaitCursor", 
                                 "ImeMode" });
        }

        protected override void PreFilterEvents(System.Collections.IDictionary events)
        {
            RemoveDescriptors(events,
                new string[] { "BackColorChanged", 
                                 "BackgroundImageChanged", 
                                 "BackgroundImageLayoutChanged", 
                                 "BackgroundStyle", 
                                 "CausesValidationChanged", 
                                 "ChangeUICues", 
                                 "ClientSizeChanged", 
                                 "Colors", 
                                 "ContextMenuStripChanged", 
                                 "ControlAdded", 
                                 "ControlRemoved", 
                                 "CursorChanged", 
                                 "DefinitionLoaded", 
                                 "DockChanged", 
                                 "EnabledChanged", 
                                 "FontChanged", 
                                 "ForeColorChanged", 
                                 "ImeModeChanged", 
                                 "KeyDown", 
                                 "KeyPress", 
                                 "KeyUp", 
                                 "Layout", 
                                 "MarginChanged", 
                                 "MonthCalendar",
                                 "PaddingChanged", 
                                 "Paint", 
                                 "ParentChanged", 
                                 "PopupClose", 
                                 "PopupContainerLoad", 
                                 "PopupContainerUnload", 
                                 "PopupOpen", 
                                 "PopupShowing", 
                                 "RegionChanged",                             
                                 "RightToLeftChanged", 
                                 "StyleChanged", 
                                 "SystemColorsChanged", 
                                 "TabIndexChanged", 
                                 "TabStopChanged", 
                                 "Resize", 
                                 "SizeChanged", 
                                 "VisibleChanged" });
            base.PreFilterEvents(events);
        }
        #endregion
    }
}
#endif

