using System.ComponentModel;
using System.Drawing.Design;
using DevComponents.DotNetBar.SuperGrid.Primitives;

namespace DevComponents.DotNetBar.SuperGrid
{
    /// <summary>
    /// Represents the collection of grid items.
    /// </summary>
    [Editor("DevComponents.SuperGrid.Design.GridRowCollectionEditor, DevComponents.SuperGrid.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=26d81176cfa2b486", typeof(UITypeEditor))]
    public class GridItemsCollection : CustomCollection<GridElement>
    {
        protected override void ClearItems()
        {
            int n = Items.Count;

            if (FloatLastItem == true)
                n--;

            for (int i = 0; i < n; i++)
            {
                GridContainer item = Items[i] as GridContainer;

                if (item != null)
                    item.DetachNestedRows(false);
            }

            base.ClearItems();
        }

        protected override void RemoveItem(int index)
        {
            GridContainer item = Items[index] as GridContainer;

            if (item != null)
                item.DetachNestedRows(false);

            base.RemoveItem(index);
        }
    }
}
