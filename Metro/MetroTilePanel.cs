using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
    /// Represents panel for Metro Tiles.
    /// </summary>
    [ToolboxBitmap(typeof(MetroTilePanel), "Metro.MetroTilePanel.ico"), ToolboxItem(true), Designer(typeof(DevComponents.DotNetBar.Design.MetroTilePanelDesigner)), System.Runtime.InteropServices.ComVisible(false)]
    public class MetroTilePanel : ItemPanel
    {
        #region Events

        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the MetroTilePanel class.
        /// </summary>
        public MetroTilePanel()
        {
            this.ResizeItemsToFit = false;
            this.ItemSpacing = DefaultItemSpacing;
            this.AutoScroll = true;
        }
        #endregion

        #region Implementation
        protected override void OnHandleCreated(EventArgs e)
        {
            if (!this.DesignMode)
                this.GetBaseItemContainer().AllowDrop = false;
            base.OnHandleCreated(e);
        }
        protected override void OnDragInProgressChanged()
        {
            if (!this.DragInProgress)
                AnimateNextPaintRequest = true;
            this.Invalidate();
            base.OnDragInProgressChanged();
        }

        /// <summary>
        /// Gets or sets a value indicating whether the control enables the user to scroll to items placed outside of its visible boundaries.
        /// </summary>
        [Browsable(true), DefaultValue(true)]
        public new virtual bool AutoScroll
        {
            get { return base.AutoScroll; }
            set { base.AutoScroll = value; }
        }

        private const int DefaultItemSpacing = 32;
        /// <summary>
        /// Gets or sets spacing in pixels between items. Default value is 1.
        /// </summary>
        [Browsable(true), DefaultValue(DefaultItemSpacing), Category("Layout"), Description("Indicates spacing in pixels between items.")]
        public override int ItemSpacing
        {
            get { return base.ItemSpacing; }
            set
            {
                base.ItemSpacing = value;
            }
        }

        /// <summary>
        /// Gets or sets whether items contained by container are resized to fit the container bounds. When container is in horizontal
        /// layout mode then all items will have the same height. When container is in vertical layout mode then all items
        /// will have the same width. Default value is true.
        /// </summary>
        [Browsable(false), DefaultValue(false), Category("Layout")]
        public override bool ResizeItemsToFit
        {
            get { return base.ResizeItemsToFit; }
            set
            {
                base.ResizeItemsToFit = value;
            }
        }

        protected override Size DefaultSize
        {
            get
            {
                return new Size(600, 400);
            }
        }

        protected override void OnMouseDragOverProcessed(int x, int y, System.Windows.Forms.DragEventArgs dragArgs)
        {
            base.OnMouseDragOverProcessed(x, y, dragArgs);
            this.Invalidate();
        }
        #endregion
    }
}
