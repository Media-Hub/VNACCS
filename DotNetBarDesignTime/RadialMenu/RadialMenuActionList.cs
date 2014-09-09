using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Design
{
    class RadialMenuActionList : DesignerActionList
    {
        private RadialMenuDesigner _Designer = null;

        /// <summary>
        /// Initializes a new instance of the AdvTreeActionList class.
        /// </summary>
        /// <param name="designer"></param>
        public RadialMenuActionList(RadialMenuDesigner designer)
            : base(designer.Component)
        {
            _Designer = designer;
        }

        public override DesignerActionItemCollection GetSortedActionItems()
        {
            DesignerActionItemCollection items = new DesignerActionItemCollection();
            items.Add(new DesignerActionHeaderItem("Menu Items"));
            items.Add(new DesignerActionHeaderItem("Menu Type"));
            items.Add(new DesignerActionHeaderItem("Appearance"));

            items.Add(new DesignerActionMethodItem(this, "EditItems", "Edit Items", "Menu Items", true));
            items.Add(new DesignerActionMethodItem(this, "RightSize", "Right Size Control", "Appearance", true));

            items.Add(new DesignerActionPropertyItem("MenuType", "Type of Radial Menu", "Menu Type", "Specifies the type of radial menu displayed."));
   
            return items;
        }

        public void EditItems()
        {
            _Designer.EditItems();
        }

        public void RightSize()
        {
            ((RadialMenu)_Designer.Control).Size = new System.Drawing.Size(28, 28);
        }


        public eRadialMenuType MenuType
        {
            get
            {
                return ((RadialMenu)base.Component).MenuType;
            }
            set
            {
                TypeDescriptor.GetProperties(base.Component)["MenuType"].SetValue(base.Component, value);
            }
        }


    }
}
