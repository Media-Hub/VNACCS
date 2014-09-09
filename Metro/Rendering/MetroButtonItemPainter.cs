using System;
using System.Collections.Generic;
using System.Text;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    internal class MetroButtonItemPainter : Office2007ButtonItemPainter
    {
        private static RoundRectangleShapeDescriptor _DefaultMetroShape = new RoundRectangleShapeDescriptor(0);
        protected override IShapeDescriptor GetButtonShape(ButtonItem button, ItemPaintArgs pa)
        {
            IShapeDescriptor shape = MetroButtonItemPainter.GetButtonShape(button);

            if (pa.ContainerControl is ButtonX)
                shape = ((ButtonX)pa.ContainerControl).GetButtonShape();
            else if (pa.ContainerControl is NavigationBar)
                shape = ((NavigationBar)pa.ContainerControl).ButtonShape;
            return shape;
        }
        private static IShapeDescriptor GetButtonShape(ButtonItem button)
        {
            if (button.Shape != null)
                return button.Shape;
            return _DefaultMetroShape;
        }
    }
}
