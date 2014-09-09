using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using DevComponents.DotNetBar.Metro.Rendering;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
    /// Defines the internal container item for the MetroToolbar control.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false), System.ComponentModel.DesignTimeVisible(false)]
    public class MetroToolbarContainer : ImageItem, IDesignTimeProvider
    {
        #region Internal Implementation
        private ItemContainer _MainItemsContainer = null;
        private ButtonItem _ExpandButton = null;
        private ItemContainer _ExtraItemsContainer = null;
        private MetroToolbar _MetroToolbar = null;
        /// <summary>
        /// Creates new instance of the class and initializes it with the parent RibbonStrip control.
        /// </summary>
        /// <param name="parent">Reference to parent MetroToolbar control</param>
        public MetroToolbarContainer(MetroToolbar parent)
        {
            _MetroToolbar = parent;

            // We contain other controls
            m_IsContainer = true;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;

            _MainItemsContainer = new ItemContainer();
            _MainItemsContainer.LayoutOrientation = eOrientation.Horizontal;
            _MainItemsContainer.GlobalItem = false;
            _MainItemsContainer.Displayed = true;
            _MainItemsContainer.ItemSpacing = 0;
            _MainItemsContainer.SetSystemContainer(true);
            _MainItemsContainer.ContainerControl = parent;
            _MainItemsContainer.DesignTimeVisible = false;
            _MainItemsContainer.Name = "_MainItemsContainer";
            this.SubItems.Add(_MainItemsContainer);

            _ExpandButton = new ButtonItem();
            _ExpandButton.Click += new EventHandler(ExpandButtonClick);
            _ExpandButton.ImagePaddingHorizontal = 12;
            _ExpandButton.ImagePaddingVertical = 10;
            _ExpandButton.Name = "sysMetroToolbarExpandButton";
            _ExpandButton.SetSystemItem(true);
            _ExpandButton.ContainerControl = parent;
            _ExpandButton.DesignTimeVisible = false;
            UpdateExpandButtonImage();
            this.SubItems.Add(_ExpandButton);

            _ExtraItemsContainer = new ItemContainer();
            _ExtraItemsContainer.LayoutOrientation = eOrientation.Horizontal;
            _ExtraItemsContainer.GlobalItem = false;
            _ExtraItemsContainer.Displayed = true;
            _ExtraItemsContainer.MultiLine = true;
            _ExtraItemsContainer.SetSystemContainer(true);
            _ExtraItemsContainer.ContainerControl = parent;
            _ExtraItemsContainer.DesignTimeVisible = false;
            _ExtraItemsContainer.Name = "_ExtraItemsContainer";
            this.SubItems.Add(_ExtraItemsContainer);
        }
        protected override void Dispose(bool disposing)
        {
            if (_ExpandButton.Image != null)
            {
                Image image = _ExpandButton.Image;
                _ExpandButton.Image = null;
                image.Dispose();
            }
            base.Dispose(disposing);
        }
        private void UpdateExpandButtonImage()
        {
            if (_ExpandButton.Image != null)
            {
                Image image = _ExpandButton.Image;
                _ExpandButton.Image = null;
                image.Dispose();
            }

            Bitmap bitmap = new Bitmap(16, 16);
            using (Graphics g = Graphics.FromImage(bitmap))
            {
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                using (SolidBrush brush = new SolidBrush(MetroRender.GetColorTable().ForeColor))
                {
                    g.FillEllipse(brush, new Rectangle(0, 2, 4, 4));
                    g.FillEllipse(brush, new Rectangle(6, 2, 4, 4));
                    g.FillEllipse(brush, new Rectangle(12, 2, 4, 4));
                }
            }
            _ExpandButton.Image = bitmap;
        }
        void ExpandButtonClick(object sender, EventArgs e)
        {
            _MetroToolbar.Expanded = !_MetroToolbar.Expanded;
        }

        /// <summary>
        /// Paints this base container
        /// </summary>
        public override void Paint(ItemPaintArgs pa)
        {
            if (this.SuspendLayout)
                return;

            _MainItemsContainer.Paint(pa);
            _ExpandButton.Paint(pa);
            _ExtraItemsContainer.Paint(pa);
        }

        public override void RecalcSize()
        {
            if (ExpandButtonVisible)
            {
                _ExpandButton.Displayed = true;
                _ExpandButton.RecalcSize();
                _ExpandButton.SetDisplayRectangle(new Rectangle(this.WidthInternal - _ExpandButton.WidthInternal, this.TopInternal, _ExpandButton.WidthInternal, _ExpandButton.HeightInternal));
            }
            else
            {
                _ExpandButton.Displayed = false;
                _ExpandButton.HeightInternal = 0;
                _ExpandButton.WidthInternal = 0;
            }

            _MainItemsContainer.LeftInternal = this.LeftInternal;
            _MainItemsContainer.TopInternal = this.TopInternal;
            if (_MainItemsContainer.VisibleSubItems == 0)
            {
                _MainItemsContainer.MinimumSize = new Size(this.WidthInternal - _ExpandButton.WidthInternal, 28);
            }
            else
            {
                _MainItemsContainer.MinimumSize = Size.Empty;
                _MainItemsContainer.WidthInternal = this.WidthInternal - _ExpandButton.WidthInternal;
                _MainItemsContainer.HeightInternal = 0;
            }
            _MainItemsContainer.RecalcSize();
            

            const int MainExtraItemsSpacing = 1;
            _ExtraItemsContainer.TopInternal = this.TopInternal + Math.Max(_MainItemsContainer.HeightInternal, _ExpandButton.HeightInternal) + MainExtraItemsSpacing;
            if (_ExtraItemsContainer.VisibleSubItems == 0)
            {
                _ExtraItemsContainer.MinimumSize = new Size(this.WidthInternal, 28);
            }
            else
            {
                _ExtraItemsContainer.MinimumSize = Size.Empty;
                _ExtraItemsContainer.WidthInternal = this.WidthInternal;
                _ExtraItemsContainer.HeightInternal = 0;
            }
            _ExtraItemsContainer.RecalcSize();


            this.HeightInternal = Math.Max(_ExpandButton.HeightInternal, _MainItemsContainer.HeightInternal) + _ExtraItemsContainer.HeightInternal + MainExtraItemsSpacing;

            base.RecalcSize();
        }

        /// <summary>
        /// Returns copy of GenericItemContainer item
        /// </summary>
        public override BaseItem Copy()
        {
            MetroToolbarContainer objCopy = new MetroToolbarContainer(_MetroToolbar);
            this.CopyToItem(objCopy);

            return objCopy;
        }
        protected override void CopyToItem(BaseItem copy)
        {
            RibbonStripContainerItem objCopy = copy as RibbonStripContainerItem;
            base.CopyToItem(objCopy);
        }

        private bool _ExpandButtonVisible = true;
        /// <summary>
        /// Gets or sets whether Expand button is visible. Default value is true.
        /// </summary>
        [DefaultValue(true)]
        public bool ExpandButtonVisible
        {
            get { return _ExpandButtonVisible; }
            set
            {
                if (value != _ExpandButtonVisible)
                {
                    bool oldValue = _ExpandButtonVisible;
                    _ExpandButtonVisible = value;
                    OnExpandButtonVisibleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when ExpandButtonVisible property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnExpandButtonVisibleChanged(bool oldValue, bool newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("ExpandButtonVisible"));
            this.NeedRecalcSize = true;
            _MetroToolbar.RecalcLayout();
        }

        /// <summary>
        /// Gets reference to container that host items.
        /// </summary>
        public ItemContainer MainItemsContainer
        {
            get
            {
                return _MainItemsContainer;
            }
        }

        /// <summary>
        /// Gets reference to container that hosts extra items.
        /// </summary>
        public ItemContainer ExtraItemsContainer
        {
            get
            {
                return _ExtraItemsContainer;
            }
        }

        public void UpdateSystemColors()
        {
            UpdateExpandButtonImage();
        }
        #endregion

        #region IDesignTimeProvider Members

        public InsertPosition GetInsertPosition(Point pScreen, BaseItem DragItem)
        {
            InsertPosition pos = null;
            Point clientPoint = _MetroToolbar.PointToClient(pScreen);
            if(_MainItemsContainer.DisplayRectangle.Contains(clientPoint) || !_MetroToolbar.Expanded)
                pos = ((IDesignTimeProvider)_MainItemsContainer).GetInsertPosition(pScreen, DragItem);

            if (pos == null && _MetroToolbar.Expanded)
            {
                pos = ((IDesignTimeProvider)_ExtraItemsContainer).GetInsertPosition(pScreen, DragItem);
            }
            return pos;
        }

        public void DrawReversibleMarker(int iPos, bool Before)
        {
            DesignTimeProviderContainer.DrawReversibleMarker(this, iPos, Before);
        }

        public void InsertItemAt(BaseItem objItem, int iPos, bool Before)
        {
            DesignTimeProviderContainer.InsertItemAt(this, objItem, iPos, Before);
        }

        /// <summary>
        /// Gets or sets a value indicating whether the item is expanded or not. For Popup items this would indicate whether the item is popped up or not.
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.DefaultValue(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public override bool Expanded
        {
            get
            {
                return base.Expanded;
            }
            set
            {
                base.Expanded = value;
                if (!value)
                {
                    foreach (BaseItem item in this.SubItems)
                        item.Expanded = false;
                }
            }
        }

        /// <summary>
        /// When parent items does recalc size for its sub-items it should query
        /// image size and store biggest image size into this property.
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden), System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public override System.Drawing.Size SubItemsImageSize
        {
            get
            {
                return base.SubItemsImageSize;
            }
            set
            {
                //m_SubItemsImageSize = value;
            }
        }
        #endregion
    }
}
