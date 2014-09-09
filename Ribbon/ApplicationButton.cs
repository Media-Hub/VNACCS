using System;
using System.Text;
using System.ComponentModel;
using System.Drawing;
using DevComponents.DotNetBar.Rendering;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Represents the Office Application Button displayed in the top-left corner of the Ribbon Control.
    /// </summary>
    [System.ComponentModel.ToolboxItem(false), System.ComponentModel.DesignTimeVisible(false), DefaultEvent("Click"), Designer(typeof(DevComponents.DotNetBar.Design.ApplicationButtonDesigner))]
    public class ApplicationButton : ButtonItem
    {
        private const string SysBackstageBackButtonName = "sys_backstage_back_button";
        private const string SysBackstagePanelName = "sys_backstage_panel";
        #region Private Variables
        private bool m_ThumbTucked = false;
        #endregion

        #region Internal Implementation
        protected override void Dispose(bool disposing)
        {
            if(_BackstageTab!=null)
                _BackstageTab.Leave -= BackstageTabLeave;
            base.Dispose(disposing);
        }
        public override void RecalcSize()
        {
            ButtonItemLayout.LayoutButton(this, true);
            m_NeedRecalcSize = false;
        }
        /// <summary>
        /// Gets or sets a value indicating whether the item is expanded or not. For Popup items this would indicate whether the item is popped up or not.
        /// </summary>
        [System.ComponentModel.Browsable(false), System.ComponentModel.DefaultValue(false), System.ComponentModel.DesignerSerializationVisibility(System.ComponentModel.DesignerSerializationVisibility.Hidden)]
        public override bool Expanded
        {
            get { return base.Expanded; }

            set
            {
                if (base.Expanded != value)
                {
                    if (!value)
                    {
                        RibbonStrip container = this.ContainerControl as RibbonStrip;
                        if (container != null && container.MouseDownOnCaption)
                            return;
                    }
                    base.Expanded = value;
                }
            }
        }

        protected internal override void OnExpandChange()
        {
            m_ThumbTucked = false;
            if (!this.DesignMode && this.Expanded && this.PopupLocation.IsEmpty && this.PopupSide == ePopupSide.Default &&
                (this.Parent is CaptionItemContainer ||
                this.Parent is RibbonTabItemContainer))
            {
                if (this.SubItems.Count > 0 && this.SubItems[0] is ItemContainer &&
                    ((ItemContainer)this.SubItems[0]).BackgroundStyle.Class == ElementStyleClassKeys.RibbonFileMenuContainerKey)
                {
                    RibbonStrip rs = this.ContainerControl as RibbonStrip;
                    if (rs != null)
                    {
                        if (this.Parent is RibbonTabItemContainer)
                        {
                            if (this.EffectiveStyle == eDotNetBarStyle.Windows7)
                                this.PopupLocation = new Point(this.IsRightToLeft ? this.Bounds.Right : this.LeftInternal, this.TopInternal - 1);
                            else if(this.EffectiveStyle == eDotNetBarStyle.Metro)
                                this.PopupLocation = new Point(this.IsRightToLeft ? this.Bounds.Right : this.LeftInternal - 4, this.TopInternal - 1);
                            else
                                this.PopupLocation = new Point(this.IsRightToLeft ? this.Bounds.Right : this.LeftInternal, this.TopInternal);
                        }
                        else
                            this.PopupLocation = new Point(this.IsRightToLeft ? this.Bounds.Right : this.LeftInternal, rs.GetItemContainerBounds().Y - 1);
                        m_ThumbTucked = true;
                    }
                }
            }

            _WasMaximized = false;
            if (_BackstageTab != null && _BackstageTabEnabled)
            {
                if (this.Expanded)
                {
                    PopupOpenEventArgs args = new PopupOpenEventArgs();
                    OnPopupOpen(args);
                    if (args.Cancel)
                    {
                        this.Expanded = false;
                        return;
                    }

                    if (EffectiveStyle == eDotNetBarStyle.Metro)
                    {
                        CreateBackstageBackButton();
                        if (_BackstageTab.Parent is Form && ((Form)_BackstageTab.Parent).WindowState == FormWindowState.Maximized)
                            _WasMaximized = true;
                    }
                    else
                        RemoveBackstageBackButton();
                    
                    UpdateBackstageTabSize();

                    if (this.ContainerControl is IKeyTipsControl)
                    {
                        IKeyTipsControl kc = this.ContainerControl as IKeyTipsControl;
                        _BackstageTab.TabStrip.ShowKeyTips = kc.ShowKeyTips;
                        kc.ShowKeyTips = false;
                    }
                    OnPopupShowing(EventArgs.Empty);
                    _BackstageTab.Visible = true;
                    _BackstageTab.BringToFront();
                    _BackstageTab.Focus();

                    if (_BackstageTab.SelectedPanel != null)
                        _BackstageTab.SelectedPanel.BringToFront();
                    
                    RibbonStrip strip = this.RibbonStrip;
                    if (EffectiveStyle == eDotNetBarStyle.Metro && strip != null)
                    {
                        ((RibbonStripContainerItem)strip.GetBaseItemContainer()).MetroBackstageOpen = true;
                        strip.TabGroupPaintSuspended = true;
                    }
                    if (strip != null && strip.SelectedRibbonTabItem != null)
                    {
                        _LastSelectedRibbonTabItem = strip.SelectedRibbonTabItem;
                        if (this.DesignMode)
                            _LastSelectedRibbonTabItem.RenderTabState = false;
                        else
                            _LastSelectedRibbonTabItem.Checked = false;
                    }


                }
                else
                {
                    OnPopupClose(EventArgs.Empty);
                    if (_LastSelectedRibbonTabItem != null)
                    {
                        if (this.DesignMode)
                            _LastSelectedRibbonTabItem.RenderTabState = true;
                        else
                            _LastSelectedRibbonTabItem.Checked = true;
                        _LastSelectedRibbonTabItem = null;
                    }
                    if (this.ContainerControl is IKeyTipsControl)
                    {
                        IKeyTipsControl kc = this.ContainerControl as IKeyTipsControl;
                        kc.ShowKeyTips = _BackstageTab.TabStrip.ShowKeyTips;
                        _BackstageTab.TabStrip.ShowKeyTips = false;
                    }
                    _BackstageTab.Visible = false;
                    RibbonStrip strip = this.ContainerControl as RibbonStrip;

                    if (EffectiveStyle == eDotNetBarStyle.Metro)
                    {
                        _BackstageTab.Resize -= BackstageTabResized;
                        if (strip != null)
                        {
                            ((RibbonStripContainerItem)strip.GetBaseItemContainer()).MetroBackstageOpen = false;
                            strip.TabGroupPaintSuspended = false;
                        }
                    }

                    if (strip != null)
                    {
                        strip.BackstageTabClosed(_BackstageTab);
                        strip.Invalidate();
                    }
                    OnPopupFinalized(EventArgs.Empty);
                }
                RaiseExpandChange(EventArgs.Empty);
                return;
            }

            base.OnExpandChange();
        }

        private void CreateBackstageBackButton()
        {
            if (_BackstageTab.Tabs.Contains(SysBackstageBackButtonName))
                return;
            ButtonItem back = new ButtonItem(SysBackstageBackButtonName);
            back.ColorTable = eButtonColor.BlueOrb;
            back.Symbol = "\uf060";
            back.Shape = new EllipticalShapeDescriptor();
            back.Click += new EventHandler(BackButtonClick);
            back.FixedSize = new Size(35, 35);
            back.Margin = new Padding(10, 0, 15, 15);
            back.SetSystemItem(true);
            _BackstageTab.Tabs.Insert(0, back);
            back.SetDesignMode(false);
            // Insert top dock panel which is excluded by region but will move the tab content down
            Panel panel = new Panel();
            panel.Dock = DockStyle.Top;
            panel.Name = SysBackstagePanelName;
            _BackstageTab.Controls.Add(panel);
            panel.BringToFront();
        }
        private void RemoveBackstageBackButton()
        {
            if (!_BackstageTab.Tabs.Contains(SysBackstageBackButtonName))
                return;
            _BackstageTab.Tabs.Remove(SysBackstageBackButtonName);
            Panel panel = (Panel)_BackstageTab.Controls[SysBackstagePanelName];
            _BackstageTab.Controls.Remove(panel);
            panel.Dispose();
        }

        void BackButtonClick(object sender, EventArgs e)
        {
            this.Expanded = false;
        }

        public override void ContainerLostFocus(bool appLostFocus)
        {
            if (_BackstageTabEnabled && _BackstageTab != null && this.DesignMode && this.Expanded && appLostFocus) return;

            base.ContainerLostFocus(appLostFocus);
            
        }

        internal override bool PopupPositionAdjusted
        {
            get { return base.PopupPositionAdjusted; }
            set
            {
                base.PopupPositionAdjusted = value;
                if (base.PopupPositionAdjusted && m_ThumbTucked)
                    m_ThumbTucked = false;
            }
        }

        internal void OnMenuPaint(ItemPaintArgs pa)
        {
            if (!m_ThumbTucked) return;

            Graphics g = pa.Graphics;
            RibbonStrip rs = this.ContainerControl as RibbonStrip;
            if (rs != null)
            {
                if (this.Parent is RibbonTabItemContainer)
                {
                    if (pa.RightToLeft)
                        g.TranslateTransform(-(this.LeftInternal - pa.ContainerControl.Width + this.WidthInternal), -(this.TopInternal));
                    else
                    {
                        if (this.EffectiveStyle == eDotNetBarStyle.Windows7)
                            g.TranslateTransform(-this.LeftInternal, -(this.TopInternal - 1));
                        else
                            g.TranslateTransform(-this.LeftInternal, -(this.TopInternal));
                    }
                }
                else
                {
                    if (pa.RightToLeft)
                        g.TranslateTransform(-(this.LeftInternal - pa.ContainerControl.Width + this.WidthInternal), -(rs.GetItemContainerBounds().Y - 1));
                    else
                        g.TranslateTransform(-this.LeftInternal, -(rs.GetItemContainerBounds().Y - 1));
                }

                g.ResetClip();
                Control c = pa.ContainerControl;
                pa.ContainerControl = rs;
                pa.IsOnMenu = false;
                this.IgnoreAlpha = true;
                bool oldGlassEnabled = pa.GlassEnabled;
                pa.GlassEnabled = rs.IsGlassEnabled;
                this.Paint(pa);
                pa.GlassEnabled = oldGlassEnabled;
                this.IgnoreAlpha = false;
                pa.ContainerControl = c;
                pa.IsOnMenu = true;
                g.ResetTransform();
            }
        }
        protected override void OnCommandChanged()
        {
        }

        protected override bool IsRightHanded
        {
            get
            {
                return false;
            }
        }

        protected override void OnStyleChanged()
        {
            if (this.EffectiveStyle == eDotNetBarStyle.Office2010)
            {
                this.ImagePaddingHorizontal = 0;
                this.ImagePaddingVertical = 0;
            }
            else if (this.EffectiveStyle == eDotNetBarStyle.Windows7)
            {
                this.ImagePaddingHorizontal = 0;
                this.ImagePaddingVertical = 0;
            }
            else if (this.EffectiveStyle == eDotNetBarStyle.Metro)
            {
                this.ImagePaddingHorizontal = 0;
                this.ImagePaddingVertical = 1;
            }
            else
            {
                if (ImagePaddingVertical == 0) ImagePaddingVertical = 2;
                if (ImagePaddingHorizontal == 0) ImagePaddingHorizontal = 2;
            }
            base.OnStyleChanged();
        }

        protected override bool IsPulseEnabed
        {
            get
            {
                if ((this.EffectiveStyle == eDotNetBarStyle.Office2010 || this.EffectiveStyle == eDotNetBarStyle.Metro) && WinApi.IsGlassEnabled) return false;
                return base.IsPulseEnabed;
            }
        }

        private void UpdateBackstageTabSize()
        {
            if (_BackstageTab == null) return;
            Control parentForm = _BackstageTab.Parent;
            if (parentForm == null) throw new InvalidOperationException("BackstageTab control does not have a parent");

            Rectangle tabBounds = parentForm.ClientRectangle;
            RibbonStrip strip = this.RibbonStrip;
            if (strip != null)
            {
                if (this.EffectiveStyle == eDotNetBarStyle.Metro)
                {
                    tabBounds = new Rectangle(1, 1, parentForm.Width - 2, parentForm.Height - 2);
                    Form form = parentForm as Form;
                    if (form != null && form.WindowState == FormWindowState.Maximized)
                    {
                        if (form is RibbonForm)
                        {
                            NonClientInfo nci = ((RibbonForm)form).GetNonClientInfo();
                            tabBounds.Width -= nci.LeftBorder;
                            tabBounds.Height -= nci.BottomBorder;
                        }
                        else
                        {
                            tabBounds.Width -= SystemInformation.FrameBorderSize.Width;
                            tabBounds.Height -= SystemInformation.FrameBorderSize.Height;
                        }
                    }
                }
                else
                {
                    Point p = parentForm.PointToClient(strip.PointToScreen(this.Bounds.Location));
                    p.Y += this.Bounds.Height;// +1;
                    tabBounds = new Rectangle(p.X, p.Y, parentForm.ClientRectangle.Width, parentForm.ClientRectangle.Height - p.Y);
                    RibbonForm ribbonForm = parentForm as RibbonForm;
                    if (ribbonForm != null)
                    {
                        if (!ribbonForm.IsGlassEnabled)
                        {
                            tabBounds.Width -= ribbonForm.BorderSize * 2;
                            tabBounds.Height -= ribbonForm.BorderSize;
                        }
                    }
                }
            }
        
            _BackstageTab.Bounds = tabBounds;
            if (this.EffectiveStyle == eDotNetBarStyle.Metro)
            {
                _BackstageTab.Resize += BackstageTabResized;
                _BackstageTab.RecalcLayout();
                UpdateBackstageTabMetroRegion();
            }
            else if(_BackstageTab.Region != null)
                _BackstageTab.Region = null;
            _BackstageTab.Anchor = AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom | AnchorStyles.Top;
        }
        private bool _WasMaximized = false;
        private void BackstageTabResized(object sender, EventArgs e)
        {
            if (this.EffectiveStyle == eDotNetBarStyle.Metro)
            {
                Form parentForm = _BackstageTab.Parent as Form;
                if (parentForm != null && parentForm.WindowState == FormWindowState.Maximized)
                    _WasMaximized = true;
                else if (_WasMaximized)
                {
                    _WasMaximized = false;
                    UpdateBackstageTabSize();
                    return;
                }
            }
            UpdateBackstageTabMetroRegion();
        }
        private void UpdateBackstageTabMetroRegion()
        {
            int captionHeight = 28;
            RibbonStrip strip = RibbonStrip;
            if (strip != null) captionHeight = strip.GetTotalCaptionHeight();

            _BackstageTab.Controls[SysBackstagePanelName].Height = captionHeight;

            Rectangle tabBounds = _BackstageTab.ClientRectangle;
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            path.AddLine(0, 0, _BackstageTab.TabStrip.Width, 0);
            path.AddLine(_BackstageTab.TabStrip.Width, 0, _BackstageTab.TabStrip.Width, captionHeight);
            path.AddLine(_BackstageTab.TabStrip.Width, captionHeight, tabBounds.Width, captionHeight);
            path.AddLine(tabBounds.Width, tabBounds.Height, 0, tabBounds.Height);
            path.CloseAllFigures();
            Region reg = new Region(path);
            _BackstageTab.Region = reg;
            path.Dispose();
        }
        private RibbonTabItem _LastSelectedRibbonTabItem = null;
        private RibbonStrip RibbonStrip
        {
            get
            {
                return this.ContainerControl as RibbonStrip;
            }
        }

        private bool _BackstageTabEnabled = true;
        /// <summary>
        /// Gets or sets whether control set on BackstageTab property is used on application menu popup.
        /// </summary>
        [DefaultValue(true), Category("Behavior"), Description("Indicates whether control set on BackstageTab property is used on application menu popup.")]
        public bool BackstageTabEnabled
        {
            get { return _BackstageTabEnabled; }
            set
            {
                this.Expanded = false;
                _BackstageTabEnabled = value;
            }
        }
        

        private SuperTabControl _BackstageTab = null;
        /// <summary>
        /// Gets or sets the backstage tab that is displayed instead of popup menu.
        /// </summary>
        [DefaultValue(null), Category("Behavior"), Description("Indicates backstage tab that is displayed instead of popup menu.")]
        public SuperTabControl BackstageTab
        {
            get
            {
                return _BackstageTab;
            }
            set
            {
                SuperTabControl oldValue = _BackstageTab;
                _BackstageTab = value;
                OnBackstageTabChanged(oldValue, value);
            }
        }

        private void OnBackstageTabChanged(SuperTabControl oldValue, SuperTabControl newValue)
        {
            if (oldValue != null)
            {
                oldValue.Leave -= BackstageTabLeave;
                if (oldValue.TabStrip != null) oldValue.TabStrip.ApplicationButton = null;
            }

            if (this.Expanded) this.Expanded = false;
            if (newValue != null)
            {
                newValue.Visible = false;
                newValue.Leave += BackstageTabLeave;
                if (newValue.TabStrip != null) newValue.TabStrip.ApplicationButton = this;
            }
        }

        private void BackstageTabLeave(object sender, EventArgs e)
        {
            if (this.Expanded)
                this.Expanded = false;
        }

        /// <summary>
        /// Processes the Escape key when Application Button is hosting the backstage tab and uses it to close the tab if open.
        /// This method is called from ProcessDialogKey method of Office2007RibbonForm.
        /// </summary>
        /// <param name="keyData">Key data</param>
        /// <returns>true if key was used to close backstage tab</returns>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ProcessEscapeKey(Keys keyData)
        {
            if (keyData == Keys.Escape && this.Expanded)
            {
                this.Expanded = false;
                return true;
            }
            return false;
        }
        internal void BackstageMnemonicProcessed(char charCode)
        {
        }

        protected override bool CanShowPopup
        {
            get
            {
                return (this.ShowSubItems || ShouldAutoExpandOnClick) && (this.SubItems.Count > 0 || this.PopupType == ePopupType.Container || BackstageTab != null && BackstageTabEnabled);
            }
        }

        public override void Refresh()
        {
            if (this.EffectiveStyle == eDotNetBarStyle.Metro)
            {
                Control container = this.ContainerControl as Control;
                if (container != null)
                {
                    RibbonForm form = container.FindForm() as RibbonForm;
                    if (form != null)
                    {
                        Rectangle r = this.Bounds;
                        r.Height++;
                        r.Width++;
                        r.Inflate(1, 1);
                        form.Invalidate(r, true);
                    } 
                }
            }
            base.Refresh();
        }
        #endregion
    }

    public class Office2007StartButton : ApplicationButton
    {
    }
}
