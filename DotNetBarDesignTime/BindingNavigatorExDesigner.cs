using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;
using System.ComponentModel.Design.Serialization;
using System.Collections;
using System.Globalization;
using DevComponents.DotNetBar.Controls;

namespace DevComponents.DotNetBar.Design
{
    /// <summary>
    /// Provides design-time support for BindingNavigatorEx control.
    /// </summary>
    public class BindingNavigatorExDesigner : BarDesigner
    {
        #region Internal Implementation
        private static readonly string[] ItemPropertyNames =
            new string[] { 
                "MovePreviousButton", 
                "MoveFirstButton", 
                "MoveNextButton", 
                "MoveLastButton", 
                "AddNewRecordButton", 
                "DeleteButton", 
                "PositionTextBox", 
                "CountLabel" };

        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            BindingNavigatorEx navigator = (BindingNavigatorEx)base.Component;
            IDesignerHost designerHost = (IDesignerHost)base.Component.Site.GetService(typeof(IDesignerHost));
            navigator.Dock = System.Windows.Forms.DockStyle.Top;
            navigator.SuspendLayout();
            navigator.AddDefaultItems();
            this.SiteItems(designerHost, navigator.Items);
            this.RaiseItemsChanged();
            navigator.ResumeLayout();
            navigator.RecalcLayout();
        }

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            if (service != null)
            {
                service.ComponentRemoved += ComponentRemoved;
                service.ComponentChanged += ComponentChanged;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
                if (service != null)
                {
                    service.ComponentRemoved -= this.ComponentRemoved;
                    service.ComponentChanged -= this.ComponentChanged;
                }
            }
            base.Dispose(disposing);
        }


        private void ComponentChanged(object sender, ComponentChangedEventArgs e)
        {
            BindingNavigatorEx component = (BindingNavigatorEx)base.Component;
            if (((e.Component != null) && (e.Component == component.CountLabel)) && ((e.Member != null) && (e.Member.Name == "Text")))
            {
                component.CountLabelFormat = component.CountLabel.Text;
            }
        }

        private void ComponentRemoved(object sender, ComponentEventArgs e)
        {
            BaseItem item = e.Component as BaseItem;
            if (item != null)
            {
                BindingNavigatorEx navigator = (BindingNavigatorEx)base.Component;
                if (item == navigator.MoveFirstButton)
                {
                    navigator.MoveFirstButton = null;
                }
                else if (item == navigator.MovePreviousButton)
                {
                    navigator.MovePreviousButton = null;
                }
                else if (item == navigator.MoveNextButton)
                {
                    navigator.MoveNextButton = null;
                }
                else if (item == navigator.MoveLastButton)
                {
                    navigator.MoveLastButton = null;
                }
                else if (item == navigator.PositionTextBox)
                {
                    navigator.PositionTextBox = null;
                }
                else if (item == navigator.CountLabel)
                {
                    navigator.CountLabel = null;
                }
                else if (item == navigator.AddNewRecordButton)
                {
                    navigator.AddNewRecordButton = null;
                }
                else if (item == navigator.DeleteButton)
                {
                    navigator.DeleteButton = null;
                }
            }
        }

        private void RaiseItemsChanged()
        {
            BindingNavigatorEx component = (BindingNavigatorEx)base.Component;
            IComponentChangeService service = (IComponentChangeService)this.GetService(typeof(IComponentChangeService));
            if (service != null)
            {
                MemberDescriptor member = TypeDescriptor.GetProperties(component)["Items"];
                service.OnComponentChanging(component, member);
                service.OnComponentChanged(component, member, null, null);
                foreach (string str in ItemPropertyNames)
                {
                    PropertyDescriptor itemsDescriptor = TypeDescriptor.GetProperties(component)[str];
                    if (itemsDescriptor != null)
                    {
                        service.OnComponentChanging(component, itemsDescriptor);
                        service.OnComponentChanged(component, itemsDescriptor, null, null);
                    }
                }
            }
        }
        private void SiteItems(IDesignerHost host, SubItemsCollection items)
        {
            foreach (BaseItem item in items)
            {
                this.SiteItem(host, item);
            }
        }
        private void SiteItem(IDesignerHost host, BaseItem item)
        {
            host.Container.Add(item, GetUniqueSiteName(host, item.Name));
            item.Name = item.Site.Name;
            if (item.SubItems.Count > 0)
            {
                this.SiteItems(host, item.SubItems);
            }
        }

        internal static string GetUniqueSiteName(IDesignerHost host, string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                return null;
            }
            INameCreationService service = (INameCreationService)host.GetService(typeof(INameCreationService));
            if (service == null)
            {
                return null;
            }
            if (host.Container.Components[name] == null)
            {
                if (!service.IsValidName(name))
                {
                    return null;
                }
                return name;
            }
            string str = name;
            for (int i = 1; !service.IsValidName(str); i++)
            {
                str = name + i.ToString(CultureInfo.InvariantCulture);
            }
            return str;
        }

        protected override void PreFilterProperties(System.Collections.IDictionary properties)
        {
            base.PreFilterProperties(properties);
            RemoveDescriptors(properties,
                new String[] { "AlwaysDisplayDockTab", 
                                 "AlwaysDisplayKeyAccelerators", 
                                 "AutoCreateCaptionMenu", 
                                 "AutoHide", 
                                 "AutoHideAnimationTime", 
                                 "AutoHideTabTextAlwaysVisible", 
                                 "AutoSyncBarCaption",
                                 "BarType",
                                 "CanAutoHide",
                                 "CanCustomize",
                                 "CanDockBottom",
                                 "CanDockDocument",
                                 "CanDockLeft",
                                 "CanDockRight",
                                 "CanDockTab",
                                 "CanDockTop",
                                 "CanDockHide",
                                 "CanReorderTabs",
                                 "CanUndock",
                                 "CaptionHeight",
                                 "CloseSingleTab",
                                 "DisplayMoreItemsOnMenu",
                                 "DockOrientation",
                                 "DockTabAlignment",
                                 "DockTabCloseButtonVisible",
                                 "DockTabStripHeight",
                                 "EqualButtonSize",
                                 "LayoutType",
                                 "MenuBar",
                                 "SaveLayoutChanges",
                                 "SelectedDockTab",
                                 "TabNavigation",
                                 "WrapItemsDock" });
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
                    "AutoHideChanged", 
                    "AutoHideDisplay", 
                    "BarDock", 
                    "BarUndock", 
                    "BeforeAutoHideDisplayed", 
                    "BeforeAutoHideHidden", 
                    "BeforeDockTabDisplayed",
                    "Closing",
                    "DefinitionLoaded",
                    "DeserializeItem",
                    "DockTabChange",
                    "DockTabClosed",
                    "DockTabClosing",
                    "SerializeItem",
                });
            base.PreFilterEvents(events);
        }
        #endregion
    }
}
