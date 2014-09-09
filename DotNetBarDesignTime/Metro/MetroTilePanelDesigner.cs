using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.Design;
using DevComponents.DotNetBar.Rendering;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Design
{
    public class MetroTilePanelDesigner : ItemPanelDesigner
    {
        public override DesignerVerbCollection Verbs
        {
            get
            {
                Bar bar = this.Control as Bar;
                DesignerVerb[] verbs = null;
                verbs = new DesignerVerb[]
						{
                            new DesignerVerb("Add Metro Tile", new EventHandler(CreateMetroTile)),
                            new DesignerVerb("Add Tile Group", new EventHandler(CreateHorizontalContainer))
                        };
                return new DesignerVerbCollection(verbs);
            }
        }

        protected override void SetDesignTimeDefaults()
        {
            ItemPanel panel = this.Control as ItemPanel;
            panel.BackgroundStyle.Class = ElementStyleClassKeys.MetroTilePanelKey;

            ItemContainer container = CreateContainer(this.GetItemContainer(), eOrientation.Horizontal);
            container.TitleStyle.Class = ElementStyleClassKeys.MetroTileGroupTitleKey;
            container.TitleText = "First";
            //LabelItem label = CreateLabel(container);
            ////label.Width = 530;
            //label.ContainerNewLineAfter = true;
            //label.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //label.Text = "First";

            CreateMetroTile(container);
            CreateMetroTile(container);
            CreateMetroTile(container);
            CreateMetroTile(container);

            container = CreateContainer(this.GetItemContainer(), eOrientation.Horizontal);
            container.TitleStyle.Class = ElementStyleClassKeys.MetroTileGroupTitleKey;
            container.TitleText = "Second";
            //label = CreateLabel(container);
            ////label.Width = 530;
            //label.ContainerNewLineAfter = true;
            //label.Font = new System.Drawing.Font("Segoe UI Semibold", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            //label.Text = "Second";

            CreateMetroTile(container);
            CreateMetroTile(container);
            CreateMetroTile(container);
            CreateMetroTile(container);
#if !TRIAL
            string key = GetLicenseKey();
            panel.LicenseKey = key;
#endif
        }

        protected override ItemContainer CreateContainer(BaseItem parent, eOrientation orientation)
        {
            ItemContainer container = base.CreateContainer(parent, orientation);
            if (container != null)
                TypeDescriptor.GetProperties(container)["MultiLine"].SetValue(container, true);
            return container;
        }

    }
}
