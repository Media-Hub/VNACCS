using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar
{
    public class RadialMenuContainer : BaseItem
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the RadialMenuContainer class.
        /// </summary>
        public RadialMenuContainer()
        {
            _Colors = new RadialMenuColorTable();
            _Colors.PropertyChanged += new PropertyChangedEventHandler(ColorsPropertyChanged);
            m_IsContainer = true;

            this.AutoCollapseOnClick = false;
            this.AccessibleRole = System.Windows.Forms.AccessibleRole.Grouping;
        }

        void ColorsPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            this.Invalidate();
        }
        /// <summary>
        /// Returns copy of the item.
        /// </summary>
        public override BaseItem Copy()
        {
            RadialMenuContainer copy = new RadialMenuContainer();
            this.CopyToItem(copy);
            return copy;
        }

        /// <summary>
        /// Copies the RadialMenuItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New ProgressBarItem instance.</param>
        internal void InternalCopyToItem(RadialMenuContainer copy)
        {
            CopyToItem(copy);
        }

        /// <summary>
        /// Copies the RadialMenuItem specific properties to new instance of the item.
        /// </summary>
        /// <param name="copy">New RadialMenuItem instance.</param>
        protected override void CopyToItem(BaseItem copy)
        {
            base.CopyToItem(copy);

            RadialMenuContainer item = copy as RadialMenuContainer;
            item.BackButtonSymbol = _BackSymbol;
            item.BackButtonSymbolSize = _BackSymbolSize;
            item.CenterButtonDiameter = _CenterButtonDiameter;
            item.Colors.Apply(_Colors);
            item.Diameter = _Diameter;
            item.MenuLocation = _MenuLocation;
            item.MenuType = _MenuType;
            item.SubMenuEdgeItemSpacing = _SubMenuEdgeItemSpacing;
            item.SubMenuEdgeWidth = _SubMenuEdgeWidth;
        }

        protected override void Dispose(bool disposing)
        {
            if (_Popup != null)
            {
                _Popup.Dispose();
                _Popup = null;
            }

            if (_PopupProxy != null)
            {
                _PopupProxy._ParentRadialMenu = null;
                _PopupProxy.Dispose();
                _PopupProxy = null;
            }

            if (_CurrentAnimation != null) _CurrentAnimation.Stop();
            base.Dispose(disposing);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs after radial menu is opened.
        /// </summary>
        [Description("Occurs after radial menu is opened.")]
        public event EventHandler Opened;
        /// <summary>
        /// Raises Opened event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnOpened(EventArgs e)
        {
            EventHandler handler = Opened;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs after radial menu is closed.
        /// </summary>
        [Description("Occurs after radial menu is closed.")]
        public event EventHandler Closed;
        /// <summary>
        /// Raises Closed event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnClosed(EventArgs e)
        {
            EventHandler handler = Closed;
            if (handler != null)
                handler(this, e);
        }
        #endregion

        #region Implementation
        private RadialSubItemInfo[] _SubItemsInfo = null;
        private bool _RecalcSizeInProgress = false;
        private const int MinItemRadialAngle = 18; // Minimum space in angles that can be occupied by single item. 18 gives 20 items on radial menu.
        public override void RecalcSize()
        {
            if (_RecalcSizeInProgress) return; // Prevent re-entrancy
            _RecalcSizeInProgress = true;

            try
            {
                DisposeSubItemsInfo();

                m_Rect = new Rectangle(0, 0, _Diameter, _Diameter);
                //this.WidthInternal = _Diameter;
                //this.HeightInternal = _Diameter;

                Rectangle bounds = new Rectangle(this.LeftInternal, this.TopInternal, _Diameter, _Diameter);
                bounds.Width--;
                bounds.Height--;
                Rectangle radialMenuBounds = bounds;

                Rectangle centerBounds = bounds;
                centerBounds.Inflate(-((_Diameter - _CenterButtonDiameter) / 2 + 1), -((_Diameter - _CenterButtonDiameter) / 2 + 1));

                if (_MenuType == eRadialMenuType.Segment)
                {
                    bounds.Inflate(-_SubMenuEdgeWidth, -_SubMenuEdgeWidth);
                    bounds.Inflate(-_SubMenuEdgeItemSpacing, -_SubMenuEdgeItemSpacing);
                }

                SubItemsCollection displayCollection = GetDisplayCollection();

                int visibleCount = GetVisibleItemsCount(displayCollection);
                if (visibleCount > 0)
                {
                    int maxItemRadialAngle = _MaxItemRadialAngle;
                    if (maxItemRadialAngle == 0) maxItemRadialAngle = 180;
                    int itemPieAngle = Math.Max(MinItemRadialAngle, Math.Min(maxItemRadialAngle, 360 / visibleCount));
                    if (itemPieAngle > _MaxItemPieAngle) itemPieAngle = _MaxItemPieAngle; // Single item can consume at most half of the circle
                    int currentAngle = -90 - itemPieAngle / 2;

                    _SubItemsInfo = new RadialSubItemInfo[displayCollection.Count];
                    for (int i = 0; i < displayCollection.Count; i++)
                    {
                        BaseItem item = displayCollection[i];
                        if (!item.Visible) continue;
                        RadialMenuItem menuItem = item as RadialMenuItem;
                        if (menuItem != null)
                        {
                            menuItem.RecalcSize();
                            GraphicsPath path = new GraphicsPath();
                            path.AddArc(bounds, currentAngle, itemPieAngle);
                            Rectangle cb = centerBounds;
                            cb.Inflate(4, 4);
                            path.AddArc(cb, currentAngle + itemPieAngle, -itemPieAngle);
                            path.CloseAllFigures();
                            menuItem.DisplayPath = path;
                            menuItem.OutterBounds = bounds;
                            menuItem.CenterBounds = centerBounds;
                            menuItem.ItemAngle = currentAngle;
                            menuItem.ItemPieAngle = itemPieAngle;
                            menuItem.Bounds = Rectangle.Round(path.GetBounds());
                        }
                        else
                        {
                            item.RecalcSize();
                            // Position item along the imaginary inner circle
                            Rectangle innerCircle = bounds;
                            innerCircle.Width -= item.WidthInternal;
                            innerCircle.Height -= item.HeightInternal;
                            if (innerCircle.Width < centerBounds.Width) innerCircle.Inflate((centerBounds.Width - innerCircle.Width) / 2, 0);
                            if (innerCircle.Height < centerBounds.Height) innerCircle.Inflate(0, (centerBounds.Height - innerCircle.Height) / 2);
                            Point p = new Point(innerCircle.X + (int)((innerCircle.Width / 2) * Math.Cos((currentAngle + itemPieAngle / 2) * (Math.PI / 180))),
                                innerCircle.Y + (int)((innerCircle.Height / 2) * Math.Sin((currentAngle + itemPieAngle / 2) * (Math.PI / 180))));
                            p.Offset(innerCircle.Width / 2, innerCircle.Height / 2);

                            item.TopInternal = p.Y;
                            item.LeftInternal = p.X;
                        }

                        if (item != null && AnyVisibleSubItems(item))
                        {
                            Rectangle outerBounds = bounds;
                            outerBounds.Inflate(_SubMenuEdgeItemSpacing, _SubMenuEdgeItemSpacing);
                            GraphicsPath subPath = new GraphicsPath();
                            int expandAngle = currentAngle + 1;
                            int expandPieAngle = itemPieAngle - 1;
                            subPath.AddArc(outerBounds, expandAngle, expandPieAngle);
                            subPath.AddArc(radialMenuBounds, expandAngle + expandPieAngle, -expandPieAngle);
                            subPath.CloseAllFigures();
                            Rectangle signPathBounds = radialMenuBounds;
                            signPathBounds.Inflate(-4, -4);
                            Point p = new Point(signPathBounds.X + (int)((signPathBounds.Width / 2) * Math.Cos((currentAngle + itemPieAngle / 2) * (Math.PI / 180))),
                                signPathBounds.Y + (int)((signPathBounds.Height / 2) * Math.Sin((currentAngle + itemPieAngle / 2) * (Math.PI / 180))));
                            p.Offset(signPathBounds.Width / 2, signPathBounds.Height / 2);
                            GraphicsPath signPath = UIGraphics.GetTrianglePath(new Point(-6, -6), 12, eTriangleDirection.Right);
                            Matrix m = new Matrix();
                            if (currentAngle + itemPieAngle / 2 != 0)
                            {
                                m.Rotate(currentAngle + itemPieAngle / 2);
                                m.Translate(p.X, p.Y, MatrixOrder.Append);
                            }
                            else
                                m.Translate(p.X, p.Y);
                            signPath.Transform(m);
                            _SubItemsInfo[i] = new RadialSubItemInfo(subPath, signPath, currentAngle, itemPieAngle);
                        }

                        currentAngle += itemPieAngle;
                        item.Displayed = true;
                    }
                }
            }
            finally
            {
                _RecalcSizeInProgress = false;
            }

            base.RecalcSize();
        }

        private SubItemsCollection GetDisplayCollection()
        {
            SubItemsCollection displayCollection = this.SubItems;
            BaseItem expanded = this.ExpandedItem();
            if (expanded != null) displayCollection = expanded.SubItems;
            return displayCollection;
        }

        public override void Paint(ItemPaintArgs p)
        {
            p.ContextData = _Colors; // Pass to the children
            p.ContextData2 = _MenuType;

#if TRIAL
            if (NativeFunctions.ColorExpAlt())
			{
				StringFormat format=new StringFormat(StringFormat.GenericDefault);
				format.Alignment=StringAlignment.Center;
				format.FormatFlags=format.FormatFlags & ~(format.FormatFlags & StringFormatFlags.NoWrap);	
				p.Graphics.DrawString("Trial period is over.",p.Font,SystemBrushes.Highlight,this.Bounds,format);
				format.Dispose();
				return;
			}
#endif

            if (_MenuType == eRadialMenuType.Circular)
            {
                PaintCircular(p);
                return;
            }

            Graphics g = p.Graphics;
            if (_OpenState < 100)  // Open animation transform setup
            {
                //p.Graphics.RotateTransform(-45 * (float)(100 - _OpenState) / 100);
                int openState = Math.Max(5, _OpenState);
                g.ScaleTransform(((float)openState) / 100, ((float)openState) / 100, MatrixOrder.Append);
                g.TranslateTransform((this.WidthInternal / 2) * (float)(100 - openState) / 100, (this.HeightInternal / 2) * (float)(100 - openState) / 100, MatrixOrder.Append);
            }
            bool drawBackButton = (this.ExpandedItem() != null);

            RadialMenuColorTable renderTable = RadialMenuContainer.GetColorTable();
            RadialMenuColorTable localTable = _Colors;

            Color borderColor = ColorScheme.GetColor(0x2B579A);
            Color borderInactiveColor = Color.FromArgb(128, borderColor);
            Color expandSignForeground = Color.White;
            Color expandMouseOverColor = Color.FromArgb(200, borderColor);
            Color backColor = Color.White;

            if (!localTable.RadialMenuBorder.IsEmpty)
                borderColor = localTable.RadialMenuBorder;
            else if (renderTable != null && !renderTable.RadialMenuBorder.IsEmpty)
                borderColor = renderTable.RadialMenuBorder;

            if (!localTable.RadialMenuBackground.IsEmpty)
                backColor = localTable.RadialMenuBackground;
            else if (renderTable != null && !renderTable.RadialMenuBackground.IsEmpty)
                backColor = renderTable.RadialMenuBackground;

            if (!localTable.RadialMenuInactiveBorder.IsEmpty)
                borderInactiveColor = localTable.RadialMenuInactiveBorder;
            else if (renderTable != null && !renderTable.RadialMenuInactiveBorder.IsEmpty)
                borderInactiveColor = renderTable.RadialMenuInactiveBorder;

            if (!localTable.RadialMenuExpandForeground.IsEmpty)
                expandSignForeground = localTable.RadialMenuExpandForeground;
            else if (renderTable != null && !renderTable.RadialMenuExpandForeground.IsEmpty)
                expandSignForeground = renderTable.RadialMenuExpandForeground;

            if (!localTable.RadialMenuMouseOverBorder.IsEmpty)
                expandMouseOverColor = localTable.RadialMenuMouseOverBorder;
            else if (renderTable != null && !renderTable.RadialMenuMouseOverBorder.IsEmpty)
                expandMouseOverColor = renderTable.RadialMenuMouseOverBorder;


            Brush expandSignBrush = new SolidBrush(expandSignForeground);
            Brush expandMouseOverBrush = new SolidBrush(expandMouseOverColor);
            Brush borderBrush = new SolidBrush(borderColor);


            Rectangle radialMenuBounds = this.Bounds;
            radialMenuBounds.Width--;
            radialMenuBounds.Height--;

            using (GraphicsPath path = new GraphicsPath())
            {
                Rectangle b = radialMenuBounds;
                path.AddEllipse(b);
                Rectangle inner = b;
                inner.Inflate(-(_Diameter - _CenterButtonDiameter) / 2, -(_Diameter - _CenterButtonDiameter) / 2);
                b.Inflate(-_SubMenuEdgeWidth, -_SubMenuEdgeWidth);
                using (GraphicsPath clipPath = new GraphicsPath())
                {
                    if (!drawBackButton && this.Parent == null && string.IsNullOrEmpty(_Symbol) && _Image == null)
                    {
                        clipPath.AddEllipse(inner);
                        g.SetClip(clipPath, CombineMode.Exclude);
                    }
                    using (SolidBrush brush = new SolidBrush(backColor))
                        p.Graphics.FillPath(brush, path);
                    if (!drawBackButton)
                        g.ResetClip();
                }
                b.Inflate(-_SubMenuEdgeWidthAnimated, -_SubMenuEdgeWidthAnimated); // Used by animation to show transition from to sub-items
                path.AddEllipse(b);
                //p.Graphics.DrawEllipse(Pens.Red, this.Bounds);
                using (Brush brush = new SolidBrush(borderInactiveColor))
                {
                    g.FillPath(brush, path);
                    using (Pen pen = new Pen(borderColor, 2))
                        g.DrawEllipse(pen, inner);
                }

                if (drawBackButton)
                {
                    Font symFont = Symbols.GetFontAwesome(_BackSymbolSize);
                    Size symSize = TextDrawing.MeasureString(g, _BackSymbol, symFont);
                    int descent = (int)Math.Ceiling((symFont.FontFamily.GetCellDescent(symFont.Style) *
                        symFont.Size / symFont.FontFamily.GetEmHeight(symFont.Style)));
                    symSize.Height -= descent;
                    TextDrawing.DrawStringLegacy(g, _BackSymbol, symFont, borderColor,
                        new Rectangle(inner.X + (inner.Width - symSize.Width) / 2 + 1, inner.Y + (inner.Height - symSize.Height) / 2 + 1, 0, 0),
                        eTextFormat.Default);
                }
                else if (!string.IsNullOrEmpty(_Symbol))
                {
                    Font symFont = Symbols.GetFontAwesome(_SymbolSize);
                    if (_SymbolTextSize.IsEmpty)
                    {
                        _SymbolTextSize = TextDrawing.MeasureString(g, _Symbol, symFont);
                        int descent = (int)Math.Ceiling((symFont.FontFamily.GetCellDescent(symFont.Style) *
                            symFont.Size / symFont.FontFamily.GetEmHeight(symFont.Style)));
                        _SymbolTextSize.Height -= descent;
                    }
                    TextDrawing.DrawStringLegacy(g, _Symbol, symFont, borderColor,
                        new Rectangle(inner.X + (inner.Width - _SymbolTextSize.Width) / 2 + 1, inner.Y + (inner.Height - _SymbolTextSize.Height) / 2 + 1, 0, 0),
                        eTextFormat.Default);
                }
                else if (_Image != null)
                {
                    g.DrawImage(_Image, inner.X + (inner.Width - _Image.Width) / 2, inner.Y + (inner.Height - _Image.Height) / 2);
                }
            }

            if (_OpenState < 100)  // Open animation transform setup
            {
                g.ResetTransform();
                g.RotateTransform(-16 * (float)(100 - _OpenState) / 100);
                g.ScaleTransform(((float)Math.Max(1, _OpenState)) / 100, ((float)Math.Max(1, _OpenState)) / 100, MatrixOrder.Append);
                g.TranslateTransform((this.WidthInternal / 2) * (float)(100 - _OpenState) / 100, (this.HeightInternal / 2) * (float)(100 - _OpenState) / 100, MatrixOrder.Append);
            }
            else if (_InnerScale < 100) // For expand animation of inner content
            {
                int openState = _InnerScale;
                g.ScaleTransform(((float)openState) / 100, ((float)openState) / 100, MatrixOrder.Append);
                g.TranslateTransform((this.WidthInternal / 2) * (float)(100 - openState) / 100, (this.HeightInternal / 2) * (float)(100 - openState) / 100, MatrixOrder.Append);
            }
            SubItemsCollection displayCollection = GetDisplayCollection();
            for (int i = 0; i < displayCollection.Count; i++)
            {
                BaseItem item = displayCollection[i];
                if (item.Visible && item.Displayed)
                {
                    if (p.ClipRectangle.IsEmpty || p.ClipRectangle.IntersectsWith(item.DisplayRectangle))
                    {
                        Region oldClip = g.Clip as Region;
                        g.SetClip(item.DisplayRectangle, CombineMode.Intersect);
                        if (!g.IsClipEmpty)
                            item.Paint(p);
                        g.Clip = oldClip;
                        if (oldClip != null) oldClip.Dispose();
                    }
                    if (_SubItemsInfo != null && _SubItemsInfo[i] != null)
                    {
                        if (_HotSubItemInfoIndex == i)
                        {
                            g.FillPath(expandMouseOverBrush, _SubItemsInfo[i].Path);
                            g.FillPath(expandSignBrush, _SubItemsInfo[i].SignPath);
                        }
                        else
                        {
                            g.FillPath(borderBrush, _SubItemsInfo[i].Path);
                            g.FillPath(expandSignBrush, _SubItemsInfo[i].SignPath);
                        }
                    }
                }
            }

            expandSignBrush.Dispose();
            expandSignBrush = null;
            expandMouseOverBrush.Dispose();
            expandMouseOverBrush = null;
            borderBrush.Dispose();
            borderBrush = null;

            g.ResetTransform();
        }

        private void PaintCircular(ItemPaintArgs p)
        {
            Graphics g = p.Graphics;

            Rectangle radialMenuBounds = this.Bounds;
            radialMenuBounds.Width--;
            radialMenuBounds.Height--;

            if (_OpenState < 100)  // Open animation transform setup
            {
                g.ResetTransform();
                g.RotateTransform(-16 * (float)(100 - _OpenState) / 100);
                g.ScaleTransform(((float)Math.Max(1, _OpenState)) / 100, ((float)Math.Max(1, _OpenState)) / 100, MatrixOrder.Append);
                g.TranslateTransform((this.WidthInternal / 2) * (float)(100 - _OpenState) / 100, (this.HeightInternal / 2) * (float)(100 - _OpenState) / 100, MatrixOrder.Append);
            }
            else if (_InnerScale < 100) // For expand animation of inner content
            {
                int openState = _InnerScale;
                g.ScaleTransform(((float)openState) / 100, ((float)openState) / 100, MatrixOrder.Append);
                g.TranslateTransform((this.WidthInternal / 2) * (float)(100 - openState) / 100, (this.HeightInternal / 2) * (float)(100 - openState) / 100, MatrixOrder.Append);
            }
            SubItemsCollection displayCollection = GetDisplayCollection();
            for (int i = 0; i < displayCollection.Count; i++)
            {
                BaseItem item = displayCollection[i];
                if (item.Visible && item.Displayed)
                {
                    if (p.ClipRectangle.IsEmpty || p.ClipRectangle.IntersectsWith(item.DisplayRectangle))
                    {
                        Region oldClip = g.Clip as Region;
                        g.SetClip(item.DisplayRectangle, CombineMode.Intersect);
                        if (!g.IsClipEmpty)
                            item.Paint(p);
                        g.Clip = oldClip;
                        if (oldClip != null) oldClip.Dispose();
                    }
                    //if (_SubItemsInfo != null && _SubItemsInfo[i] != null)
                    //{
                    //    if (_HotSubItemInfoIndex == i)
                    //    {
                    //        g.FillPath(expandMouseOverBrush, _SubItemsInfo[i].Path);
                    //        g.FillPath(expandSignBrush, _SubItemsInfo[i].SignPath);
                    //    }
                    //    else
                    //    {
                    //        g.FillPath(borderBrush, _SubItemsInfo[i].Path);
                    //        g.FillPath(expandSignBrush, _SubItemsInfo[i].SignPath);
                    //    }
                    //}
                }
            }

            g.ResetTransform();
        }

        private bool AnyVisibleSubItems(BaseItem item)
        {
            if (!item.ShowSubItems) return false;
            foreach (BaseItem t in item.SubItems)
            {
                if (t.Visible) return true;
            }
            return false;
        }
        private int GetVisibleItemsCount(SubItemsCollection displayCollection)
        {
            int c = 0;
            foreach (BaseItem item in displayCollection)
            {
                if (item.Visible) c++;
            }
            return c;
        }
        private void DisposeSubItemsInfo()
        {
            if (_SubItemsInfo != null)
            {
                for (int i = 0; i < _SubItemsInfo.Length; i++)
                {
                    if (_SubItemsInfo[i] != null) _SubItemsInfo[i].Dispose();
                }
            }
            _SubItemsInfo = null;
        }
        protected internal override void OnExpandChange()
        {
            if (this.Expanded)
                Open();
            else
            {
                if (HotSubItem != null) HotSubItem.InternalMouseLeave();
                Close();
                // Collapse all sub-items
                CollapseRecursively(this);
            }
            base.OnExpandChange();
        }

        private void CollapseRecursively(BaseItem item)
        {
            foreach (BaseItem sub in item.SubItems)
            {
                sub.Expanded = false;
                CollapseRecursively(sub);
            }
        }
        private int _OpenState = 0;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public int OpenState
        {
            get { return _OpenState; }
            set
            {
                if (this.IsDisposed | _Popup == null || _Popup != null && _Popup.IsDisposed)
                {
                    _OpenState = value;
                    return;
                }
                if (_Popup != null && _Popup.InvokeRequired)
                {
                    _Popup.Invoke(new MethodInvoker(delegate { this.OpenState = value; }));
                    return;
                }
                if (value < 0)
                    value = 0;
                else if (value > 100)
                    value = 100;
                if (value != _OpenState)
                {
                    int oldValue = _OpenState;
                    _OpenState = value;
                    OnOpenStateChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when OpenState property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnOpenStateChanged(int oldValue, int newValue)
        {
            //Application.DoEvents();
            //OnPropertyChanged(new PropertyChangedEventArgs("OpenState"));
            this.Refresh();

            if (newValue == 0)
            {
                RadialMenuPopup popup = _Popup;
                _Popup = null;
                if (popup != null)
                {
                    popup.Hide();
                    popup.Dispose();
                }
                OnClosed(EventArgs.Empty);
            }
            else if (newValue == 100)
            {
                OnOpened(EventArgs.Empty);
            }
        }

        private int _InnerScale = 100;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public int InnerScale
        {
            get { return _InnerScale; }
            set
            {
                if (value > 100)
                    value = 100;
                else if (value < 0)
                    value = 0;
                if (_InnerScale != value)
                {
                    _InnerScale = value;
                    this.Refresh();
                }
            }
        }

        //private bool _IsOpen;
        //internal bool IsOpen
        //{
        //    get { return _IsOpen; }
        //    set
        //    {
        //        if (_IsOpen != value)
        //        {
        //            _IsOpen = value;
        //            if (_IsOpen)
        //                Open();
        //            else
        //                Close();
        //        }
        //    }
        //}
        /// <summary>
        /// Returns internal popup control for radial menu.
        /// </summary>
        [Browsable(false), EditorBrowsable(EditorBrowsableState.Advanced)]
        public RadialMenuPopup Popup
        {
            get
            {
                return _Popup;
            }
        }

        private Animation.Animation _CurrentAnimation = null;
        private RadialMenuPopup _Popup = null;
        private bool _IsOpening = false;
        private RadialMenuPopupProxy _PopupProxy = null;
        #region Open
        internal void Open()
        {
            if (_IsOpening) return;
            try
            {
                _IsOpening = true;

                this.Displayed = true;

                WaitForCurrentAnimationToComplete();

                if (this.Parent != null) // Hosted elsewhere other than RadialMenu
                {
                    IOwnerMenuSupport ownerMenu = this.GetIOwnerMenuSupport();
                    if (ownerMenu != null)
                    {
                        if (_PopupProxy == null) _PopupProxy = new RadialMenuPopupProxy(this);
                        _PopupProxy.Expanded = true;
                        ownerMenu.RegisterPopup(_PopupProxy);
                    }
                }


                if (_Popup == null)
                {
                    _Popup = new RadialMenuPopup();
                    _Popup.DisplayItem = this;
                    if (_Font != null)
                        _Popup.Font = _Font;
                    else if (_RadialMenu != null)
                        _Popup.Font = _RadialMenu.Font;
                    _Popup.CreateControl();
                }

                if (_MenuLocation.IsEmpty)
                {
                    if (_RadialMenu != null)
                    {
                        Point p = _RadialMenu.PointToScreen(
                            new Point(_RadialMenu.Width / 2, _RadialMenu.Height / 2));
                        p.X -= _Diameter / 2;
                        p.Y -= _Diameter / 2;
                        _Popup.Location = p;
                    }
                    else
                    {
                        Point p = Control.MousePosition;
                        p.X -= _Diameter / 2;
                        p.Y -= _Diameter / 2;
                        _Popup.Location = p;
                    }
                }
                else
                    _Popup.Location = _MenuLocation;
                _Popup.Visible = true;
                _Popup.Refresh();

                Animation.AnimationInt anim = new DevComponents.DotNetBar.Animation.AnimationInt(
                       new Animation.AnimationRequest(this, "OpenState", 0, 100),
                       Animation.AnimationEasing.EaseOutCubic, 300);
                anim.AutoDispose = true;
                _CurrentAnimation = anim;
                anim.Start();
            }
            finally
            {
                _IsOpening = false;
            }
        }
        #endregion

        private void WaitForCurrentAnimationToComplete()
        {
            if (_CurrentAnimation != null)
            {
                DateTime start = DateTime.Now;
                while (!_CurrentAnimation.IsDisposed)
                {
                    Application.DoEvents();
                    if (DateTime.Now.Subtract(start).TotalMilliseconds > 1000)
                    {
                        AbortCurrentAnimation();
                        break;
                    }
                }
                _CurrentAnimation = null;
            }
        }

        private void AbortCurrentAnimation()
        {
            Animation.Animation anim = _CurrentAnimation;
            _CurrentAnimation = null;
            if (anim != null)
            {
                anim.Stop();
                anim.Dispose();
            }
        }
        /// <summary>
        /// Closes currently open popup.
        /// </summary>
        internal void Close()
        {
            WaitForCurrentAnimationToComplete();

            Animation.AnimationInt anim = new DevComponents.DotNetBar.Animation.AnimationInt(
                   new Animation.AnimationRequest(this, "OpenState", 100, 0),
                   Animation.AnimationEasing.EaseOutCubic, 300);
            anim.AutoDispose = true;
            anim.Start();

            if (_PopupProxy != null) // Hosted elsewhere other than RadialMenu
            {
                IOwnerMenuSupport ownerMenu = this.GetIOwnerMenuSupport();
                if (ownerMenu != null)
                {
                    ownerMenu.UnregisterPopup(_PopupProxy);
                    _PopupProxy.Expanded = false;
                }
            }
        }

        protected internal IOwnerMenuSupport GetIOwnerMenuSupport()
        {
            return (this.GetOwner() as IOwnerMenuSupport);
        }

        private int _MaxItemRadialAngle = 0;
        /// <summary>
        /// Indicates maximum radial angle single item in menu can consume. By default this property is set to zero which indicates that
        /// radial menu is equally divided between visible menu items.
        /// </summary>
        [DefaultValue(0), Category("Appearance"), Description("Indicates maximum radial angle single item in menu can consume. Max value is 180.")]
        public int MaxItemRadialAngle
        {
            get { return _MaxItemRadialAngle; }
            set
            {
                if (value < 0)
                    value = 0;
                else if (value > 180)
                    value = 180;
                if (value != _MaxItemRadialAngle)
                {
                    int oldValue = _MaxItemRadialAngle;
                    _MaxItemRadialAngle = value;
                    OnMaxItemRadialAngleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when MaxItemRadialAngle property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnMaxItemRadialAngleChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("MaxItemRadialAngle"));
            NeedRecalcSize = true;
            this.Refresh();
        }

        private int _Diameter = 180;
        /// <summary>
        /// Gets or sets radial menu diameter. Minimum value is 64.
        /// </summary>
        [DefaultValue(180), Category("Appearance"), Description("Radial menu diameter.")]
        public int Diameter
        {
            get { return _Diameter; }
            set
            {
                if (value < 64) value = 64;
                if (value != _Diameter)
                {
                    int oldValue = _Diameter;
                    _Diameter = value;
                    OnDiameterChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Diameter property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnDiameterChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Diameter"));
            NeedRecalcSize = true;
            this.Refresh();
        }

        private int _CenterButtonDiameter = 32;
        /// <summary>
        /// Indicates diameter of center button of radial menu.
        /// </summary>
        [DefaultValue(32), Category("Appearance"), Description("Indicates diameter of center button of radial menu.")]
        public int CenterButtonDiameter
        {
            get { return _CenterButtonDiameter; }
            set
            {
                if (value != _CenterButtonDiameter)
                {
                    int oldValue = _CenterButtonDiameter;
                    _CenterButtonDiameter = value;
                    OnCenterButtonDiameterChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when CenterButtonDiameter property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnCenterButtonDiameterChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("CenterButtonDiameter"));

        }

        private int _SubMenuEdgeWidth = 18;
        /// <summary>
        /// Specifies the width of the sub-menu edge around the radial menu.
        /// </summary>
        [DefaultValue(18), Category("Appearance"), Description("Specifies the width of the sub-menu edge around the radial menu.")]
        public int SubMenuEdgeWidth
        {
            get { return _SubMenuEdgeWidth; }
            set
            {
                if (value < 0) value = 0;
                if (value != _SubMenuEdgeWidth)
                {
                    int oldValue = _SubMenuEdgeWidth;
                    _SubMenuEdgeWidth = value;
                    OnSubMenuEdgeWidthChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SubMenuEdgeWidth property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSubMenuEdgeWidthChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SubMenuEdgeWidth"));
            NeedRecalcSize = true;
            this.Refresh();
        }

        private int _SubMenuEdgeItemSpacing = 3;
        /// <summary>
        /// Indicates spacing between sub-menu marking edge and the item.
        /// </summary>
        [DefaultValue(3), Category("Appearance"), Description("Indicates spacing between sub-menu marking edge and the item.")]
        public int SubMenuEdgeItemSpacing
        {
            get { return _SubMenuEdgeItemSpacing; }
            set
            {
                if (value != _SubMenuEdgeItemSpacing)
                {
                    int oldValue = _SubMenuEdgeItemSpacing;
                    _SubMenuEdgeItemSpacing = value;
                    OnSubMenuEdgeItemSpacingChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SubMenuEdgeItemSpacing property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSubMenuEdgeItemSpacingChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SubMenuEdgeItemSpacing"));
            NeedRecalcSize = true;
            this.Refresh();
        }

        /// <summary>
        /// Return Sub Item at specified location
        /// </summary>
        public override BaseItem ItemAtLocation(int x, int y)
        {
            SubItemsCollection subItems = GetDisplayCollection();
            foreach (BaseItem item in subItems)
            {
                if (!item.Visible || !item.Displayed) continue;
                RadialMenuItem rm = item as RadialMenuItem;
                if (rm != null)
                {
                    if (rm.DisplayPath.IsVisible(x, y))
                        return rm;
                }
                else if ((item.Visible || IsOnCustomizeMenu) && item.Displayed && item.DisplayRectangle.Contains(x, y))
                {
                    if (m_ViewRectangle.IsEmpty || m_ViewRectangle.Contains(x, y))
                        return item;
                }
            }
            return null;
            //return base.ItemAtLocation(x, y);
        }

        protected override void Invalidate(Control containerControl)
        {
            if (this.DesignMode)
            {
                if (containerControl.InvokeRequired)
                    containerControl.BeginInvoke(new MethodInvoker(delegate { containerControl.Invalidate(); }));
                else
                    containerControl.Invalidate();
            }
            else
            {
                if (containerControl.InvokeRequired)
                    containerControl.BeginInvoke(new MethodInvoker(delegate { containerControl.Invalidate(m_Rect, true); }));
                else
                    containerControl.Invalidate(m_Rect, true);
            }
        }

        private Point _MenuLocation = Point.Empty;
        /// <summary>
        /// Indicates the position of top-left corner of radial menu when shown in screen coordinates
        /// </summary>
        [Category("Behavior"), Description("Indicates the position of top-left corner of radial menu when shown in screen coordinates")]
        public Point MenuLocation
        {
            get { return _MenuLocation; }
            set
            {
                if (value != _MenuLocation)
                {
                    Point oldValue = _MenuLocation;
                    _MenuLocation = value;
                    OnMenuLocationChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when MenuLocation property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnMenuLocationChanged(Point oldValue, Point newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("MenuLocation"));
            if (_Popup != null) _Popup.Location = newValue;
        }

        private RadialMenu _RadialMenu;
        internal RadialMenu RadialMenu
        {
            get { return _RadialMenu; }
            set
            {
                if (value != _RadialMenu)
                {
                    RadialMenu oldValue = _RadialMenu;
                    _RadialMenu = value;
                    OnRadialMenuChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when RadialMenu property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnRadialMenuChanged(RadialMenu oldValue, RadialMenu newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("RadialMenu"));

        }

        public override void InternalMouseMove(MouseEventArgs objArg)
        {
            base.InternalMouseMove(objArg);
            UpateHotSubItemInfoIndex(objArg);
        }
        private void UpateHotSubItemInfoIndex(MouseEventArgs objArg)
        {
            if (HotSubItem == null && _SubItemsInfo != null)
            {
                bool found = false;
                // Check sub items
                for (int i = 0; i < _SubItemsInfo.Length; i++)
                {
                    RadialSubItemInfo info = _SubItemsInfo[i];
                    if (info != null && info.Path.IsVisible(objArg.X, objArg.Y))
                    {
                        HotSubItemInfoIndex = i;
                        found = true;
                        break;
                    }
                }
                if (!found)
                    HotSubItemInfoIndex = -1;
            }
            else
                HotSubItemInfoIndex = -1;
        }
        public override void InternalMouseLeave()
        {
            HotSubItemInfoIndex = -1;
            base.InternalMouseLeave();
        }
        public override void InternalMouseDown(MouseEventArgs objArg)
        {
            UpateHotSubItemInfoIndex(objArg);
            int hotSubItemInfoIndex = HotSubItemInfoIndex;
            if (hotSubItemInfoIndex >= 0)
            {
                SubItemsCollection displayCollection = GetDisplayCollection();
                if (hotSubItemInfoIndex < displayCollection.Count)
                {
                    BaseItem expand = displayCollection[hotSubItemInfoIndex];
                    if (expand.ShowSubItems && expand.VisibleSubItems > 0)
                    {
                        expand.Expanded = true;
                        return;
                    }
                }
            }

            base.InternalMouseDown(objArg);
        }

        Animation.Storyline _CurrentStoryline = null;
        private void RunExpandItemAnimation()
        {
            Animation.Storyline story = new DevComponents.DotNetBar.Animation.Storyline();
            story.AutoDispose = true;
            Animation.AnimationInt anim = new DevComponents.DotNetBar.Animation.AnimationInt(
                   new Animation.AnimationRequest(this, "InnerScale", 100, 85),
                   Animation.AnimationEasing.EaseInCubic, 250);
            anim.Animations.Add(new DevComponents.DotNetBar.Animation.AnimationRequest(this, "SubMenuEdgeWidthAnimated", 0, 12));
            anim.AutoDispose = true;
            story.Animations.Add(anim);
            anim = new DevComponents.DotNetBar.Animation.AnimationInt(
                   new Animation.AnimationRequest(this, "InnerScale", 85, 100),
                   Animation.AnimationEasing.EaseOutCubic, 250);
            anim.Animations.Add(new DevComponents.DotNetBar.Animation.AnimationRequest(this, "SubMenuEdgeWidthAnimated", 12, 0));
            anim.AutoDispose = true;
            story.Animations.Add(anim);
            _CurrentStoryline = story;
            story.Run();
        }
        protected internal override void OnSubItemExpandChange(BaseItem item)
        {
            NeedRecalcSize = true;
            if (this.Expanded)
            {
                if (_CurrentStoryline == null || _CurrentStoryline.IsDisposed)
                {
                    WaitForCurrentAnimationToComplete();
                    RunExpandItemAnimation();
                }
                else
                    this.Invalidate();
            }
            HotSubItemInfoIndex = -1;
            base.OnSubItemExpandChange(item);
        }
        private int _HotSubItemInfoIndex = -1;
        private int HotSubItemInfoIndex
        {
            get { return _HotSubItemInfoIndex; }
            set
            {
                if (_HotSubItemInfoIndex != value)
                {
                    _HotSubItemInfoIndex = value;
                    this.Refresh();
                }
            }
        }

        private int _SubMenuEdgeWidthAnimated = 0;
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public int SubMenuEdgeWidthAnimated
        {
            get { return _SubMenuEdgeWidthAnimated; }
            set
            {
                if (_SubMenuEdgeWidthAnimated != value)
                {
                    _SubMenuEdgeWidthAnimated = value;
                    this.Refresh();
                }
            }
        }

        protected internal override void OnContainerChanged(object objOldContainer)
        {
            object newContainer = this.ContainerControl;
            SetContainerRecursevly(this, newContainer);
            //base.OnContainerChanged(objOldContainer);
        }
        private void SetContainerRecursevly(BaseItem item, object newContainer)
        {
            foreach (BaseItem c in item.SubItems)
            {
                c.ContainerControl = newContainer;
                SetContainerRecursevly(c, newContainer);
            }
        }

        /// <summary>
        /// Gets the current expanded subitem.
        /// </summary>
        /// <returns></returns>
        protected internal override BaseItem ExpandedItem()
        {
            return GetExpandedItemRecursevly(this);
        }
        private BaseItem GetExpandedItemRecursevly(BaseItem item)
        {
            foreach (BaseItem sub in item.SubItems)
            {
                if (sub.Expanded)
                {
                    BaseItem inner = GetExpandedItemRecursevly(sub);
                    if (inner != null) return inner;
                    return sub;
                }
            }
            return null;
        }

        private int _BackSymbolSize = 18;
        /// <summary>
        /// Indicates the size of the back symbol that is displayed on center of radial menu
        /// </summary>
        [DefaultValue(18), Browsable(false), Description("Indicates the size of the back symbol that is displayed on center of radial menu")]
        public int BackButtonSymbolSize
        {
            get { return _BackSymbolSize; }
            set
            {
                if (value != _BackSymbolSize)
                {
                    int oldValue = _BackSymbolSize;
                    _BackSymbolSize = value;
                    OnBackSymbolSizeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when BackSymbolSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnBackSymbolSizeChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("BackSymbolSize"));
            this.Invalidate();
        }

        private string _BackSymbol = "\uf060";
        /// <summary>
        /// Specifies the back button symbol.
        /// </summary>
        [DefaultValue("\uf060"), Category("Appearance"), Description("Specifies the back button symbol.")]
        public string BackButtonSymbol
        {
            get { return _BackSymbol; }
            set
            {
                if (value != _BackSymbol)
                {
                    string oldValue = _BackSymbol;
                    _BackSymbol = value;
                    OnBackSymbolChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when BackSymbol property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnBackSymbolChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("BackSymbol"));
            this.Invalidate();
        }

        private int _MaxItemPieAngle = 90;
        /// <summary>
        /// Specifies the maximum pie part angle an item will occupy. Maximum is 180, minimum is 1 degree.
        /// </summary>
        [DefaultValue(90), Category("Appearance"), Description("Specifies the maximum pie part angle an item will occupy. Maximum is 180, minimum is 1 degree.")]
        public int MaxItemPieAngle
        {
            get { return _MaxItemPieAngle; }
            set
            {
                if (value < 0) value = 90;
                if (value > 180) value = 180;
                if (value != _MaxItemPieAngle)
                {
                    int oldValue = _MaxItemPieAngle;
                    _MaxItemPieAngle = value;
                    OnMaxItemPieAngleChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when MaxItemPieAngle property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnMaxItemPieAngleChanged(int oldValue, int newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("MaxItemPieAngle"));
            NeedRecalcSize = true;
            this.Refresh();
        }

        private RadialMenuColorTable _Colors = null;
        /// <summary>
        /// Gets reference to colors used by the radial menu.
        /// </summary>
        [Category("Appearance"), Description("Gets reference to colors used by the radial menu."), DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public RadialMenuColorTable Colors
        {
            get
            {
                return _Colors;
            }
        }

        internal static RadialMenuColorTable GetColorTable()
        {
            if (GlobalManager.Renderer is Office2007Renderer)
                return ((Office2007Renderer)GlobalManager.Renderer).ColorTable.RadialMenu;
            else
                return null;
        }

        private eRadialMenuType _MenuType = eRadialMenuType.Segment;
        /// <summary>
        /// Indicates the radial menu type. eRadialMenuType.Radial menu type allows for display of image, text and any sub-menu items.
        /// eRadialMenuType.Circular allows only for display of Symbol or Image and it does not display either text or sub-menu items. 
        /// eRadialMenuType.Circular is designed to be used for single level menus only that don't need text.
        /// </summary>
        [DefaultValue(eRadialMenuType.Segment), Category("Appearance"), Description("Indicates the radial menu type.")]
        public eRadialMenuType MenuType
        {
            get { return _MenuType; }
            set
            {
                if (value != _MenuType)
                {
                    eRadialMenuType oldValue = _MenuType;
                    _MenuType = value;
                    OnMenuTypeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when MenuType property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnMenuTypeChanged(eRadialMenuType oldValue, eRadialMenuType newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("MenuType"));
            this.Refresh();
        }

        private Size _SymbolTextSize = Size.Empty;
        private string _Symbol = "";
        /// <summary>
        /// Indicates the symbol displayed on face of the button instead of the image. Setting the symbol overrides the image setting.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates the symbol displayed on face of the button instead of the image. Setting the symbol overrides the image setting.")]
        [Editor("DevComponents.DotNetBar.Design.SymbolTypeEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public string Symbol
        {
            get { return _Symbol; }
            set
            {
                if (value != _Symbol)
                {
                    string oldValue = _Symbol;
                    _Symbol = value;
                    OnSymbolChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Symbol property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSymbolChanged(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Symbol"));
            _SymbolTextSize = Size.Empty;
            NeedRecalcSize = true;
            OnAppearanceChanged();
            this.Refresh();
        }

        private float _SymbolSize = 0f;
        /// <summary>
        /// Indicates the size of the symbol in points.
        /// </summary>
        [DefaultValue(0f), Category("Appearance"), Description("Indicates the size of the symbol in points.")]
        public float SymbolSize
        {
            get { return _SymbolSize; }
            set
            {
                if (value != _SymbolSize)
                {
                    float oldValue = _SymbolSize;
                    _SymbolSize = value;
                    OnSymbolSizeChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SymbolSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSymbolSizeChanged(float oldValue, float newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("SymbolSize"));
            _SymbolTextSize = Size.Empty;
            NeedRecalcSize = true;
            OnAppearanceChanged();
            this.Refresh();
        }

        private Image _Image;
        /// <summary>
        /// Gets or sets the image displayed on the item.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Indicates image displayed on the item.")]
        public Image Image
        {
            get { return _Image; }
            set
            {
                if (value != _Image)
                {
                    Image oldValue = _Image;
                    _Image = value;
                    OnImageChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Image property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnImageChanged(Image oldValue, Image newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Image"));
            NeedRecalcSize = true;
            OnAppearanceChanged();
            this.Refresh();
        }

        #region RadialSubItemInfo
        private class RadialSubItemInfo
        {
            /// <summary>
            /// Initializes a new instance of the RadialSubItemInfo class.
            /// </summary>
            /// <param name="path"></param>
            /// <param name="signPath"></param>
            /// <param name="itemAngle"></param>
            /// <param name="itemPieAngle"></param>
            public RadialSubItemInfo(GraphicsPath path, GraphicsPath signPath, int itemAngle, int itemPieAngle)
            {
                Path = path;
                SignPath = signPath;
                ItemAngle = itemAngle;
                ItemPieAngle = itemPieAngle;
            }
            public GraphicsPath Path;
            public GraphicsPath SignPath;
            public int ItemAngle;
            public int ItemPieAngle;

            public void Dispose()
            {
                if (Path != null)
                    Path.Dispose();
                if (SignPath != null)
                    SignPath.Dispose();
                Path = null;
            }
        }
        #endregion
        #endregion

        #region RadialMenuPopupProxy
        private class RadialMenuPopupProxy : PopupItem
        {
            internal RadialMenuContainer _ParentRadialMenu = null;
            /// <summary>
            /// Initializes a new instance of the RadialMenuPopupProxy class.
            /// </summary>
            /// <param name="parentRadialMenu"></param>
            public RadialMenuPopupProxy(RadialMenuContainer parentRadialMenu)
            {
                _ParentRadialMenu = parentRadialMenu;
            }
            protected internal override bool IsAnyOnHandle(IntPtr iHandle)
            {
                if (_ParentRadialMenu._Popup != null && _ParentRadialMenu._Popup.Handle == iHandle)
                    return true;
                return false;
            }
            private bool _Expanded = false;
            public override bool Expanded
            {
                get
                {
                    return _Expanded;
                }
                set
                {
                    if (_Expanded != value)
                    {
                        _Expanded = value;
                        if (!_Expanded && _ParentRadialMenu.Expanded)
                            _ParentRadialMenu.Expanded = false;
                    }
                }
            }
            public override void Paint(ItemPaintArgs p)
            {
            }
            public override BaseItem Copy()
            {
                return new RadialMenuPopupProxy(_ParentRadialMenu);
            }
        }

        private Font _Font;
        /// <summary>
        /// Specifies font for menu items.
        /// </summary>
        [DefaultValue(null), Description("Specifies font for menu items."), Category("Appearance")]
        public Font Font
        {
            get { return _Font; }
            set
            {
                if (value != _Font)
                {
                    Font oldValue = _Font;
                    _Font = value;
                    OnFontChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Font property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnFontChanged(Font oldValue, Font newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Font"));
            if (_Popup != null && this.Expanded) _Popup.Invalidate();
        }
        #endregion
    }

    /// <summary>
    /// Defines available radial menu type.
    /// </summary>
    public enum eRadialMenuType
    {
        Segment,
        Circular
    }
}
