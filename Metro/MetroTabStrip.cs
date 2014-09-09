using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections;
using DevComponents.DotNetBar.Metro.Rendering;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
    /// Represents Metro-UI TabStrip control.
    /// </summary>
    [ToolboxItem(false), ComVisible(false)]
    public class MetroTabStrip : ItemControl
    {
        #region Private Variables & Constructor
        private MetroStripContainerItem _StripContainer = null;
        private int _CaptionHeight = 0;
        private bool _CaptionVisible = false;
        private Rectangle _CaptionBounds = Rectangle.Empty;
        private Rectangle _QuickToolbarBounds = Rectangle.Empty;
        private Rectangle _SystemCaptionItemBounds = Rectangle.Empty;
        private Font _CaptionFont = null;
        private bool _CanCustomize = true;
        private ElementStyle _DefaultBackgroundStyle = new ElementStyle();
        private bool _KeyTipsEnabled = true;
        private string _TitleText = "";

        public MetroTabStrip()
        {
            this.SetStyle(ControlStyles.StandardDoubleClick, true);

            _StripContainer = new MetroStripContainerItem(this);
            _StripContainer.GlobalItem = false;
            _StripContainer.ContainerControl = this;
            _StripContainer.Displayed = true;
            _StripContainer.SetOwner(this);
            _StripContainer.Style = eDotNetBarStyle.Metro;
            this.SetBaseItemContainer(_StripContainer);

            this.ColorScheme.Style = eDotNetBarStyle.Office2003;

            this.AutoSize = true;

            // Setup system caption item
            _StripContainer.SystemCaptionItem.Click += new EventHandler(SystemCaptionClick);
        }
        #endregion

        #region Events
        /// <summary>
        /// Occurs when text markup link from TitleText markup is clicked. Markup links can be created using "a" tag, for example:
        /// <a name="MyLink">Markup link</a>
        /// </summary>
        [Description("Occurs when text markup link from TitleText markup is clicked.")]
        public event MarkupLinkClickEventHandler TitleTextMarkupLinkClick;
        #endregion

        #region Internal Implementation
        private TextMarkup.BodyElement _TitleTextMarkup = null;
        /// <summary>
        /// Gets or sets the rich text displayed on Ribbon Title instead of the Form.Text property. This property supports text-markup.
        /// You can use <font color="SysCaptionTextExtra"> markup to instruct the markup renderer to use Office 2007 system caption extra text color which
        /// changes depending on the currently selected color table. Note that when using this property you should manage also the Form.Text property since
        /// that is the text that will be displayed in Windows task-bar and elsewhere where system Form.Text property is used.
        /// You can also use the hyperlinks as part of the text markup and handle the TitleTextMarkupLinkClick event to be notified when they are clicked.
        /// </summary>
        [Browsable(true), DefaultValue(""), Editor("DevComponents.DotNetBar.Design.TextMarkupUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor)), EditorBrowsable(EditorBrowsableState.Always), Category("Appearance"), Description("Indicates text displayed on Ribbon Title instead of the Form.Text property.")]
        public string TitleText
        {
            get { return _TitleText; }
            set
            {
                if (value == null) value = "";
                _TitleText = value;
                _TitleTextMarkup = null;

                if (!TextMarkup.MarkupParser.IsMarkup(ref _TitleText))
                    return;

                _TitleTextMarkup = TextMarkup.MarkupParser.Parse(_TitleText);

                if (_TitleTextMarkup != null)
                    _TitleTextMarkup.HyperLinkClick += new EventHandler(InternalTitleTextMarkupLinkClick);
                TitleTextMarkupLastArrangeBounds = Rectangle.Empty;
                this.Invalidate();
            }
        }

        /// <summary>
        /// Occurs when text markup link is clicked.
        /// </summary>
        private void InternalTitleTextMarkupLinkClick(object sender, EventArgs e)
        {
            TextMarkup.HyperLink link = sender as TextMarkup.HyperLink;
            if (link != null)
            {
                if (TitleTextMarkupLinkClick != null)
                    TitleTextMarkupLinkClick(this, new MarkupLinkClickEventArgs(link.Name, link.HRef));
            }
        }

        /// <summary>
        /// Gets reference to parsed markup body element if text was markup otherwise returns null.
        /// </summary>
        internal TextMarkup.BodyElement TitleTextMarkupBody
        {
            get { return _TitleTextMarkup; }
        }

        internal Rectangle TitleTextMarkupLastArrangeBounds = Rectangle.Empty;
        /// <summary>
        /// Gets or sets whether KeyTips functionality is enabled. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Behavior"), Description("Indicates whether KeyTips functionality is enabled.")]
        public bool KeyTipsEnabled
        {
            get { return _KeyTipsEnabled; }
            set { _KeyTipsEnabled = value; }
        }

        /// <summary>
        /// Gets or sets whether control can be customized and items added by end-user using context menu to the quick access toolbar.
        /// Caption of the control must be visible for customization to be enabled. Default value is true.
        /// </summary>
        [DefaultValue(true), Browsable(true), Category("Customization"), Description("Indicates whether control can be customized. Caption must be visible for customization to be fully enabled.")]
        public bool CanCustomize
        {
            get { return _CanCustomize; }
            set { _CanCustomize = value; }
        }

        /// <summary>
        /// Gets or sets the explicit height of the caption provided by control. Caption height when set is composed of the TabGroupHeight and
        /// the value specified here. Default value is 0 which means that system default caption size is used.
        /// </summary>
        [Browsable(true), DefaultValue(0), Category("Appearance"), Description("Indicates explicit height of the caption provided by control")]
        public int CaptionHeight
        {
            get { return _CaptionHeight; }
            set
            {
                _CaptionHeight = value;
                _StripContainer.NeedRecalcSize = true;
            }
        }

        internal bool HasVisibleTabs
        {
            get
            {
                foreach (BaseItem item in this.Items)
                {
                    if (item.Visible) return true;
                }
                return false;
            }
        }

        internal Rectangle CaptionBounds
        {
            get { return _CaptionBounds; }
            set { _CaptionBounds = value; }
        }
        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public Rectangle QuickToolbarBounds
        {
            get { return _QuickToolbarBounds; }
            set { _QuickToolbarBounds = value; }
        }

        internal Rectangle SystemCaptionItemBounds
        {
            get { return _SystemCaptionItemBounds; }
            set { _SystemCaptionItemBounds = value; }
        }

        internal SystemCaptionItem SystemCaptionItem
        {
            get { return _StripContainer.SystemCaptionItem; }
        }

        /// <summary>
        /// Gets or sets whether custom caption line provided by the control is visible. Default value is false.
        /// This property should be set to true when control is used on Office2007RibbonForm.
        /// </summary>
        [Browsable(true), DefaultValue(false), Category("Appearance"), Description("Indicates whether custom caption line provided by the control is visible.")]
        public bool CaptionVisible
        {
            get { return _CaptionVisible; }
            set
            {
                _CaptionVisible = value;
                OnCaptionVisibleChanged();
            }
        }

        /// <summary>
        /// Gets or sets the font for the form caption text when CaptionVisible=true. Default value is NULL which means that system font is used.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Appearance"), Description("")]
        public Font CaptionFont
        {
            get { return _CaptionFont; }
            set
            {
                _CaptionFont = value;
                _StripContainer.NeedRecalcSize = true;
                this.Invalidate();
            }
        }

        private void OnCaptionVisibleChanged()
        {
            _StripContainer.NeedRecalcSize = true;
            this.RecalcLayout();
        }

        internal eDotNetBarStyle EffectiveStyle
        {
            get
            {
                return _StripContainer.EffectiveStyle;
            }
        }
        /// <summary>
        /// Gets/Sets the visual style of the control.
        /// </summary>
        [Browsable(true), DevCoBrowsable(true), Category("Appearance"), Description("Specifies the visual style of the control."), DefaultValue(eDotNetBarStyle.Metro)]
        public eDotNetBarStyle Style
        {
            get
            {
                return _StripContainer.Style;
            }
            set
            {
                this.ColorScheme.Style = value;
                _StripContainer.Style = value;
                this.Invalidate();
                this.RecalcLayout();
            }
        }

        /// <summary>
        /// Returns collection of items on a bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection Items
        {
            get
            {
                return _StripContainer.RibbonStripContainer.SubItems;
            }
        }

        /// <summary>
        /// Returns currently selected MetroTabItem. MetroTabItems are selected using the Checked property. Only a single
        /// MetroTabItem can be Checked at any given time.
        /// </summary>
        [Browsable(false)]
        public MetroTabItem SelectedTab
        {
            get
            {
                foreach (BaseItem item in this.Items)
                {
                    if (item is MetroTabItem && ((MetroTabItem)item).Checked)
                        return (MetroTabItem)item;
                }
                return null;
            }
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == (int)WinApi.WindowsMessages.WM_NCHITTEST)
            {
                // Get position being tested...
                int x = WinApi.LOWORD(m.LParam);
                int y = WinApi.HIWORD(m.LParam);
                Point p = PointToClient(new Point(x, y));
                if (IsGlassEnabled && this.CaptionVisible)
                {
                    Rectangle r = new Rectangle(this.Width - SystemInformation.CaptionButtonSize.Width * 3, 0, SystemInformation.CaptionButtonSize.Width * 3, SystemInformation.CaptionButtonSize.Height + 6);
                    if (r.Contains(p))
                    {
                        m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                        return;
                    }

                    r = new Rectangle(0, 0, this.Width, 4);
                    if (r.Contains(p))
                    {
                        m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                        return;
                    }

                    if (_CaptionBounds.Contains(p))
                    {
                        m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                        return;
                    }
                }
                else if (this.CaptionVisible && !this.IsMaximized)
                {
                    Rectangle r = new Rectangle(0, 0, this.Width, 4);
                    if (r.Contains(p))
                    {
                        m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                        return;
                    }
                }
                if (BarFunctions.IsWindows7 && this.IsMaximized)
                {
                    if(this.CaptionBounds.Contains(p))
                    {
                        m.Result = new IntPtr((int)WinApi.WindowHitTestRegions.TransparentOrCovered);
                        return;
                    }
                }
            }

            base.WndProc(ref m);
        }

        internal bool IsMaximized
        {
            get
            {
                Form f = this.FindForm();
                return (f != null && f.WindowState == FormWindowState.Maximized);
            }
        }

        /// <summary>
        /// Returns the color scheme used by control. Color scheme for Office2007 style will be retrived from the current renderer instead of
        /// local color scheme referenced by ColorScheme property.
        /// </summary>
        /// <returns>An instance of ColorScheme object.</returns>
        protected override ColorScheme GetColorScheme()
        {
            BaseRenderer r = GetRenderer();
            if (r is Office2007Renderer)
                return ((Office2007Renderer)r).ColorTable.LegacyColors;
            return base.GetColorScheme();
        }

        protected override void PaintControlBackground(ItemPaintArgs pa)
        {
            base.PaintControlBackground(pa);

            _QuickToolbarBounds = Rectangle.Empty;
            _CaptionBounds = Rectangle.Empty;
            _SystemCaptionItemBounds = Rectangle.Empty;

            MetroRender.Paint(this, pa);
            //DevComponents.DotNetBar.Rendering.BaseRenderer renderer = GetRenderer();
            //if (renderer != null && this.Parent is MetroTab)
            //{
            //    renderer.DrawMetroTabBackground(new MetroTabRendererEventArgs(pa.Graphics, this.Parent as MetroTab, pa.GlassEnabled));

            //    if (_CaptionVisible)
            //        renderer.DrawQuickAccessToolbarBackground(new MetroTabRendererEventArgs(pa.Graphics, this.Parent as MetroTab, pa.GlassEnabled));
            //}

            //// Paint form caption text
            //if (renderer != null && _CaptionVisible)
            //{
            //    MetroTabRendererEventArgs rer = new MetroTabRendererEventArgs(pa.Graphics, this.Parent as MetroTab, pa.GlassEnabled);
            //    rer.ItemPaintArgs = pa;
            //    renderer.DrawRibbonFormCaptionText(rer);
            //}

#if TRIAL
            if (NativeFunctions.ColorExpAlt())
				{
					pa.Graphics.Clear(Color.White);
					TextDrawing.DrawString(pa.Graphics, "Your DotNetBar trial has expired :-(", this.Font, Color.FromArgb(128, Color.Black), this.ClientRectangle, eTextFormat.HorizontalCenter | eTextFormat.VerticalCenter);
				}
                //else
                //{
                //    TextDrawing.DrawString(pa.Graphics, "Trial Version", this.Font, Color.FromArgb(128, Color.Black), new Rectangle(0, 0, this.Width - 12, this.Height-4), eTextFormat.Right | eTextFormat.Bottom);
                //}
#endif
        }

        protected override ElementStyle GetBackgroundStyle()
        {
            if (this.BackgroundStyle.Custom)
                return base.GetBackgroundStyle();
            return _DefaultBackgroundStyle;
        }

        internal void InitDefaultStyles()
        {
            _DefaultBackgroundStyle = MetroRender.GetColorTable().MetroTab.TabStrip.BackgroundStyle;
        }

        internal ElementStyle InternalGetBackgroundStyle()
        {
            return this.GetBackgroundStyle();
        }

        internal Rectangle GetItemContainerBounds()
        {
            Rectangle r = base.GetItemContainerRectangle();
            if (_CaptionVisible)
            {
                r.Y += GetAbsoluteCaptionHeight();
            }
            return r;
        }

        internal Rectangle GetCaptionContainerBounds()
        {
            Rectangle baseRect = base.GetItemContainerRectangle();
            if (this.IsGlassEnabled)
                baseRect.Y += 3;
            return new Rectangle(baseRect.X, baseRect.Y, baseRect.Width, GetAbsoluteCaptionHeight());
        }
        /// <summary>
        /// Returns effective caption height.
        /// </summary>
        /// <returns>Caption height.</returns>
        public int GetCaptionHeight()
        {
            if (_CaptionHeight == 0)
            {
                return 25;
            }
            else
            {
                return _CaptionHeight;
            }
        }

        internal int GetAbsoluteCaptionHeight()
        {
            return GetCaptionHeight();
        }

        internal int GetTotalCaptionHeight()
        {
            return GetCaptionHeight();
        }

        internal override bool IsGlassEnabled
        {
            get
            {
                return false;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (_StripContainer.NeedRecalcSize) this.RecalcSize();
            InitDefaultStyles();
            base.OnPaint(e);
        }

        protected override void RecalcSize()
        {
            _CaptionBounds = Rectangle.Empty;
            _SystemCaptionItemBounds = Rectangle.Empty;
            _QuickToolbarBounds = Rectangle.Empty;
            InitDefaultStyles();
            base.RecalcSize();
        }

        /// <summary>
        /// Returns automatically calculated height of the control given current content.
        /// </summary>
        /// <returns>Height in pixels.</returns>
        public override int GetAutoSizeHeight()
        {
            int i = base.GetAutoSizeHeight();
            return i;
        }

        protected override bool OnMouseWheel(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            Rectangle r = this.DisplayRectangle;
            r.Location = this.PointToScreen(r.Location);

            MetroShell rc = this.Parent as MetroShell;

            if (this.Parent is MetroShell)
            {
                r = this.Parent.DisplayRectangle;
                r.Location = this.Parent.PointToScreen(r.Location);
                if (rc.SelectedTab != null && rc.SelectedTab.Panel is IKeyTipsControl && ((IKeyTipsControl)rc.SelectedTab.Panel).ShowKeyTips)
                    return false;
            }

            if (rc != null && !rc.MouseWheelTabScrollEnabled) return false;

            Point mousePos = Control.MousePosition;

            bool parentActive = true;
            Form parentForm = this.FindForm();
            if (parentForm != null && !BarFunctions.IsFormActive(parentForm))
                parentActive = false;

            if (parentActive && r.Contains(mousePos) && !this.ShowKeyTips && this.Items.Count > 0)
            {
                IntPtr handle = NativeFunctions.WindowFromPoint(new NativeFunctions.POINT(mousePos));
                Control c = Control.FromChildHandle(handle);
                if (c == null)
                    c = Control.FromHandle(handle);
                if (c is MetroTabStrip || c is MetroShell || c is MetroTabPanel)
                {
                    MetroTabItem selectedTab = this.SelectedTab;
                    int start = 0;
                    int end = this.Items.Count - 1;

                    int direction = 1;
                    if (wParam.ToInt64() > 0)
                    {
                        direction = -1;
                        end = 0;
                    }
                    if (selectedTab != null)
                    {
                        start = this.Items.IndexOf(selectedTab) + direction;
                        if (direction < 0 && start < 0) return false;

                        if (start == this.Items.Count)
                            start = 0;
                        else if (start < 0)
                            start = this.Items.Count - 1;
                    }

                    int index = start - direction;
                    do
                    {
                        index += direction;
                        if (index < 0 || index > this.Items.Count - 1) break;

                        if (this.Items[index] is MetroTabItem && this.Items[index].Visible)
                        {
                            ((MetroTabItem)this.Items[index]).Checked = true;
                            return true;
                        }
                    } while (index != end);
                }
            }

            return false;
        }

        /// <summary>
        /// Returns the collection of items with the specified name.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <returns></returns>
        public override ArrayList GetItems(string ItemName)
        {
            ArrayList list = new ArrayList(15);
            BarFunctions.GetSubItemsByName(GetBaseItemContainer(), ItemName, list);

            MetroShell rc = this.GetMetroTab();

            if (rc != null && rc.GlobalContextMenuBar != null)
                BarFunctions.GetSubItemsByName(rc.GlobalContextMenuBar.ItemsContainer, ItemName, list);

            return list;
        }

        /// <summary>
        /// Returns the collection of items with the specified name and type.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <param name="itemType">Item type to look for.</param>
        /// <returns></returns>
        public override ArrayList GetItems(string ItemName, Type itemType)
        {
            ArrayList list = new ArrayList(15);
            BarFunctions.GetSubItemsByNameAndType(GetBaseItemContainer(), ItemName, list, itemType);

            MetroShell rc = this.GetMetroTab();
            if (rc != null && rc.GlobalContextMenuBar != null)
                BarFunctions.GetSubItemsByNameAndType(rc.GlobalContextMenuBar.ItemsContainer, ItemName, list, itemType);

            return list;
        }

        /// <summary>
        /// Returns the collection of items with the specified name and type.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <param name="itemType">Item type to look for.</param>
        /// <param name="useGlobalName">Indicates whether GlobalName property is used for searching.</param>
        /// <returns></returns>
        public override ArrayList GetItems(string ItemName, Type itemType, bool useGlobalName)
        {
            ArrayList list = new ArrayList(15);
            BarFunctions.GetSubItemsByNameAndType(GetBaseItemContainer(), ItemName, list, itemType, useGlobalName);

            MetroShell rc = this.GetMetroTab();

            if (rc != null && rc.GlobalContextMenuBar != null)
                BarFunctions.GetSubItemsByNameAndType(rc.GlobalContextMenuBar.ItemsContainer, ItemName, list, itemType, useGlobalName);

            return list;
        }

        /// <summary>
        /// Returns the first item that matches specified name.
        /// </summary>
        /// <param name="ItemName">Item name to look for.</param>
        /// <returns></returns>
        public override BaseItem GetItem(string ItemName)
        {
            BaseItem item = BarFunctions.GetSubItemByName(GetBaseItemContainer(), ItemName);
            if (item != null)
                return item;

            MetroShell rc = this.GetMetroTab();
            if (rc != null && rc.GlobalContextMenuBar != null)
                return BarFunctions.GetSubItemByName(rc.GlobalContextMenuBar.ItemsContainer, ItemName);

            return null;
        }
        #endregion

        #region KeyTips Support
        private bool _AltFocus = false;
        private IKeyTipsControl _AltFocusedControl = null;

        /// <summary>
        /// Called when ShowKeyTips on RibbonBar contained by this Ribbon is set to true
        /// </summary>
        internal void OnRibbonBarShowKeyTips(RibbonBar bar)
        {
            if (!_AltFocus)
            {
                _AltFocus = true;
            }

            if (_AltFocusedControl != bar.Parent && bar.Parent is RibbonPanel)
            {
                if (_AltFocusedControl != null)
                    _AltFocusedControl.ShowKeyTips = false;
                _AltFocusedControl = bar.Parent as IKeyTipsControl;
                if (_AltFocusedControl != null)
                    _AltFocusedControl.ShowKeyTips = true;
            }
        }

        protected override void OnShowKeyTipsChanged()
        {
            MetroShell rc = this.GetMetroTab();
            base.OnShowKeyTipsChanged();

            if (this.ShowKeyTips && !_AltFocus)
                GiveKeyTipsAltFocus();
        }

        internal void BackstageTabClosed(SuperTabControl tabControl)
        {
            if (_AltFocusedControl == tabControl || _AltFocusedControl == tabControl.TabStrip)
                _AltFocusedControl = null;
        }

        protected override bool OnKeyDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            if (_AltFocus)
            {
                if (HasRegisteredPopups)
                {
                    bool ret = base.OnKeyDown(hWnd, wParam, lParam);
                    if (!HasRegisteredPopups) ReleaseAltFocus();
                }
                Keys key = (Keys)NativeFunctions.MapVirtualKey((uint)wParam, 2);
                if (key == Keys.None)
                {
                    int wParamInt = WinApi.ToInt(wParam);
                    key = (Keys)wParamInt;
                }

                if (key == Keys.Escape)
                {
                    bool reShowKeyTips = false;
                    if (!this.ShowKeyTips && _AltFocusedControl != null)
                    {
                        if (_AltFocusedControl is RibbonPanel)
                            reShowKeyTips = true;
                    }

                    ReleaseAltFocus();

                    if (reShowKeyTips)
                    {
                        GiveKeyTipsAltFocus();
                    }

                    return base.OnKeyDown(hWnd, wParam, lParam);
                }

                char accessKey = CharFromInt((int)key);
                if (accessKey == char.MinValue)
                {
                    ReleaseAltFocus();
                    return true;
                }
                else if (key == Keys.Space || key == Keys.Down || key == Keys.Right || key == Keys.Tab)
                {
                    ReleaseAltFocus();
                    return false;
                }

                if (this.ProcessMnemonic(accessKey))
                    return true;

                return true;
            }

            return base.OnKeyDown(hWnd, wParam, lParam);
        }

        private char CharFromInt(int key)
        {
            char[] ch = new char[1];
            byte[] by = new byte[1];
            try
            {
                by[0] = System.Convert.ToByte(key);
                System.Text.Encoding.Default.GetDecoder().GetChars(by, 0, 1, ch, 0);
            }
            catch (Exception)
            {
                return char.MinValue;
            }

            return ch[0];
        }

        protected override bool OnSysKeyUp(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            int wParamInt = WinApi.ToInt(wParam);
            if (wParamInt == 18 || wParamInt == 121)
            {
                bool callBase = true;
                if (_AltFocus)
                {
                    if (wParamInt == 18 || wParamInt == 121)
                        callBase = false;
                    ReleaseAltFocus();
                }
                else
                {
                    Form f = this.FindForm();
                    if (f == Form.ActiveForm)
                    {
                        callBase = false;
                        GiveKeyTipsAltFocus();
                    }
                }
                if (!callBase)
                    return true;
            }
            return base.OnSysKeyUp(hWnd, wParam, lParam);
        }

        private void GiveKeyTipsAltFocus()
        {
            if (!_KeyTipsEnabled) return;

            _AltFocus = true;
            this.ShowKeyTips = true;
            SetupActiveWindowTimer();
        }

        private void ReleaseAltFocus()
        {
            if (_AltFocus)
            {
                ReleaseActiveWindowTimer();
                if (!this.ShowKeyTips && _AltFocusedControl != null)
                {
                    _AltFocusedControl.ShowKeyTips = false;
                    _AltFocusedControl = null;
                }
                this.ShowKeyTips = false;
            }

            _AltFocus = false;
        }

        /// <summary>
        /// Gets whether Ribbon is in key-tips mode including its child controls.
        /// </summary>
        [Browsable(false)]
        public bool IsInKeyTipsMode
        {
            get
            {
                return _AltFocus | this.ShowKeyTips;
            }
        }

        /// <summary>
        /// Forces the control to exit Ribbon Key-Tips mode.
        /// </summary>
        public void ExitKeyTipsMode()
        {
            ReleaseAltFocus();
        }

        internal void OnChildItemClick(BaseItem item)
        {
            if (this.ShowKeyTips || _AltFocusedControl != null && _AltFocusedControl.ShowKeyTips)
            {
                this.ShowKeyTips = false;
                ReleaseAltFocus();
            }
        }

        protected override bool OnSysMouseDown(IntPtr hWnd, IntPtr wParam, IntPtr lParam)
        {
            if (this.ShowKeyTips || _AltFocusedControl != null && _AltFocusedControl.ShowKeyTips)
            {
                this.ShowKeyTips = false;
                ReleaseAltFocus();
            }

            return base.OnSysMouseDown(hWnd, wParam, lParam);
        }

        protected override bool ProcessMnemonic(char charCode)
        {
            if (_AltFocusedControl != null)
            {
                if (_AltFocusedControl.ProcessMnemonicEx(charCode))
                {
                    ReleaseAltFocus();
                    return true;
                }
                return false;
            }
            if (!_AltFocus && (Control.ModifierKeys & Keys.Alt) != Keys.Alt)
                return false;
            return base.ProcessMnemonic(charCode);
        }

        public override bool ProcessMnemonicEx(char charCode)
        {
            // If different ribbon tab item is selected we should not exit the Alt mode...
            BaseItem item = null;

            if (_CaptionVisible)
            {
                item = GetItemForMnemonic(_StripContainer.CaptionContainer, charCode, false, true);
                if (item != null)
                {
                    if (ProcessItemMnemonicKey(item))
                    {
                        ReleaseAltFocus();
                        return true;
                    }
                }
            }

            MetroShell rc = this.GetMetroTab();

            if (item == null)
            {
                item = GetItemForMnemonic(this.GetBaseItemContainer(), charCode, true, true);
            }

            if (item is MetroTabItem && item.Visible)
            {
                if (!_AltFocus)
                {
                    GiveKeyTipsAltFocus();
                }
                this.ShowKeyTips = false;
                if (item != this.SelectedTab)
                {
                    // Switch the tab
                    item.RaiseClick();
                    //this.ShowKeyTips = true;
                }
                if (this.SelectedTab != null && _KeyTipsEnabled)
                {
                    if (_AltFocusedControl != null)
                        _AltFocusedControl.ShowKeyTips = false;
                    _AltFocusedControl = this.SelectedTab.Panel;
                    if (_AltFocusedControl != null)
                        _AltFocusedControl.ShowKeyTips = true;
                }

                // Keep the Alt focus
                return true;
            }
            else if (item != null && ProcessItemMnemonicKey(item))
            {
                this.ShowKeyTips = false;
                if (item is MetroAppButton)
                {
                    MetroAppButton appButton = (MetroAppButton)item;
                    if (appButton.BackstageTab != null)
                    {
                        if (_AltFocusedControl != null)
                            _AltFocusedControl.ShowKeyTips = false;
                        _AltFocusedControl = appButton.BackstageTab.TabStrip;
                        if (!_AltFocusedControl.ShowKeyTips)
                            _AltFocusedControl.ShowKeyTips = true;
                    }

                }
                return true;
            }

            //if (base.ProcessMnemonicEx(charCode))
            //{
            //    ReleaseAltFocus();
            //    return true;
            //}

            if (_AltFocus && this.SelectedTab != null && this.SelectedTab.Panel != null)
            {
                Control panel = this.SelectedTab.Panel;
                foreach (Control c in panel.Controls)
                {
                    if (c is RibbonBar && c.Visible)
                    {
                        if (IsMnemonic(charCode, c.Text))
                        {
                            // Select RibbonBar for Alt+ Key access
                            this.ShowKeyTips = false;
                            ((RibbonBar)c).ShowKeyTips = true;
                            if (_AltFocusedControl != null)
                                _AltFocusedControl.ShowKeyTips = false;
                            _AltFocusedControl = (IKeyTipsControl)c;
                            return true;
                        }
                    }
                }
            }

            return false;
        }

        protected override void OnActiveWindowChanged()
        {
            ReleaseAltFocus();
            base.OnActiveWindowChanged();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        {
            return base.ProcessCmdKey(ref msg, keyData);
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            return base.ProcessDialogKey(keyData);
        }
        #endregion

        #region Caption Container Support
        /// <summary>
        /// Called when item on popup container is right-clicked.
        /// </summary>
        /// <param name="item">Instance of the item that is right-clicked.</param>
        protected override void OnPopupItemRightClick(BaseItem item)
        {
            MetroShell rc = this.GetMetroTab();
            if (rc != null)
                rc.ShowCustomizeContextMenu(item, false);
        }
        //private MetroTab GetMetroTab()
        //{
        //    Control parent = this.Parent;
        //    while (parent != null && !(parent is MetroTab))
        //        parent = parent.Parent;
        //    if (parent is MetroTab)
        //        return parent as MetroTab;
        //    return null;
        //}
        protected override void OnMouseLeave(EventArgs e)
        {
            if (_TitleTextMarkup != null)
                _TitleTextMarkup.MouseLeave(this);
            base.OnMouseLeave(e);
        }

        internal bool MouseDownOnCaption
        {
            get
            {
                return _MouseDownOnCaption;
            }
        }

        private MetroShell GetMetroTab()
        {
            return this.Parent as MetroShell;
        }

        internal void ShowSystemMenu(Point p)
        {
            Form form = this.FindForm();
            if (form is MetroAppForm)
                ((MetroAppForm)form).ShowSystemWindowMenu(p);
            else
            {
                const int TPM_RETURNCMD = 0x0100;
                byte[] bx = BitConverter.GetBytes(p.X);
                byte[] by = BitConverter.GetBytes(p.Y);
                byte[] blp = new byte[] { bx[0], bx[1], by[0], by[1] };
                int lParam = BitConverter.ToInt32(blp, 0);
                this.Capture = false;
                NativeFunctions.SendMessage(form.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.TrackPopupMenu(
                    NativeFunctions.GetSystemMenu(form.Handle, false), TPM_RETURNCMD, p.X, p.Y, 0, form.Handle, IntPtr.Zero), lParam);
            }
        }

        private bool _MouseDownOnCaption = false;
        protected override void OnMouseDown(MouseEventArgs e)
        {
            _MouseDownOnCaption = false;
            if (_CaptionVisible)
            {
                _MouseDownOnCaption = HitTestCaption(new Point(e.X, e.Y));
                if (e.Button == MouseButtons.Right && _MouseDownOnCaption)
                {
                    ShowSystemMenu(Control.MousePosition);
                    return;
                }

                if (_TitleTextMarkup != null)
                    _TitleTextMarkup.MouseDown(this, e);

                e = TranslateMouseEventArgs(e);
            }

            if (e.Button == MouseButtons.Right && this.QuickToolbarBounds.Contains(e.X, e.Y))
            {
                MetroShell mt = this.GetMetroTab();
                if (mt != null)
                    mt.OnTabStripRightClick(this, e.X, e.Y);
            }

            base.OnMouseDown(e);
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            if (_CaptionVisible)
            {
                if (_TitleTextMarkup != null)
                    _TitleTextMarkup.MouseUp(this, e);

                e = TranslateMouseEventArgs(e);
            }
            _MouseDownOnCaption = false;
            base.OnMouseUp(e);
        }

        internal BaseItem GetApplicationButton()
        {
            if (!this.CaptionVisible)
                return null;
            BaseItem cont = this.CaptionContainerItem;

            if (this.EffectiveStyle == eDotNetBarStyle.Office2010 && this.Items.Count > 0 && this.Items[0] is MetroAppButton)
                return this.Items[0];

            if (cont.SubItems.Count > 0 && cont.SubItems[0] is MetroAppButton)
                return cont.SubItems[0];

            if (cont.SubItems.Count > 0 && cont.SubItems[0] is ButtonItem && ((ButtonItem)cont.SubItems[0]).HotTrackingStyle == eHotTrackingStyle.Image)
                return cont.SubItems[0];

            return null;
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (_CaptionVisible)
            {
                if (e.Button == MouseButtons.Left && _MouseDownOnCaption && HitTestCaption(new Point(e.X, e.Y)))
                {
                    Form form = this.FindForm();
                    if (form != null && form.WindowState == FormWindowState.Normal)
                    {
                        PopupItem popup = GetApplicationButton() as PopupItem;
                        if (popup != null && popup.Expanded) popup.Expanded = false;
                        const int HTCAPTION = 2;
                        Point p = Control.MousePosition;
                        byte[] bx = BitConverter.GetBytes(p.X);
                        byte[] by = BitConverter.GetBytes(p.Y);
                        byte[] blp = new byte[] { bx[0], bx[1], by[0], by[1] };
                        int lParam = BitConverter.ToInt32(blp, 0);
                        this.Capture = false;
                        NativeFunctions.SendMessage(form.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MOVE + HTCAPTION, lParam);
                        _MouseDownOnCaption = false;
                        return;
                    }
                }
                if (_TitleTextMarkup != null)
                    _TitleTextMarkup.MouseMove(this, e);
                e = TranslateMouseEventArgs(e);
            }
            base.OnMouseMove(e);
        }

        protected override void InternalOnClick(MouseButtons mb, Point mousePos)
        {
            if (_CaptionVisible)
            {
                MouseEventArgs e = new MouseEventArgs(mb, 0, mousePos.X, mousePos.Y, 0);
                e = TranslateMouseEventArgs(e);
                mousePos = new Point(e.X, e.Y);
            }

            base.InternalOnClick(mb, mousePos);
        }

        private MouseEventArgs TranslateMouseEventArgs(MouseEventArgs e)
        {
            if (e.Y <= 6)
            {
                Form form = this.FindForm();
                if (form != null && form.WindowState == FormWindowState.Maximized && form is RibbonForm)
                {
                    if (e.X <= 4)
                    {
                        BaseItem sb = GetApplicationButton();
                        if (sb != null)
                        {
                            e = new MouseEventArgs(e.Button, e.Clicks, sb.LeftInternal + 1, sb.TopInternal + 1, e.Delta);
                        }
                    }
                    else if (e.X >= this.Width - 6)
                        e = new MouseEventArgs(e.Button, e.Clicks, this.SystemCaptionItem.DisplayRectangle.Right - 4, this.SystemCaptionItem.DisplayRectangle.Top + 4, e.Delta);
                    else
                        e = new MouseEventArgs(e.Button, e.Clicks, e.X, this.CaptionContainerItem.TopInternal + 5, e.Delta);
                }
            }
            return e;
        }

        protected override void OnClick(EventArgs e)
        {
            if (_CaptionVisible)
            {
                if (_TitleTextMarkup != null)
                    _TitleTextMarkup.Click(this);
            }
            base.OnClick(e);
        }

        protected override void OnDoubleClick(EventArgs e)
        {
            if (_CaptionVisible)
            {
                // Check whether double click is on caption so window can be maximized/restored
                Point p = this.PointToClient(Control.MousePosition);
                if (HitTestCaption(p))
                {
                    Form form = this.FindForm();
                    if (form != null && form.MaximizeBox && (form.FormBorderStyle == FormBorderStyle.Sizable || form.FormBorderStyle == FormBorderStyle.SizableToolWindow))
                    {
                        if (form.WindowState == FormWindowState.Normal)
                        {
                            NativeFunctions.PostMessage(form.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MAXIMIZE, 0);
                        }
                        else if (form.WindowState == FormWindowState.Maximized)
                        {
                            NativeFunctions.PostMessage(form.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_RESTORE, 0);
                        }
                    }
                }
            }
            base.OnDoubleClick(e);
        }

        /// <summary>
        /// Returns true if point is inside the caption area.
        /// </summary>
        /// <param name="p">Client point coordinates.</param>
        /// <returns>True if point is inside of caption area otherwise false.</returns>
        protected bool HitTestCaption(Point p)
        {
            return _CaptionBounds.Contains(p);
        }

        private void SystemCaptionClick(object sender, EventArgs e)
        {
            SystemCaptionItem sci = sender as SystemCaptionItem;
            Form frm = this.FindForm();

            if (frm == null)
                return;

            if (sci.LastButtonClick == sci.MouseDownButton)
            {
                if (sci.LastButtonClick == SystemButton.Minimize)
                {
                    NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MINIMIZE, 0);
                }
                else if (sci.LastButtonClick == SystemButton.Maximize)
                {
                    NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MAXIMIZE, 0);
                }
                else if (sci.LastButtonClick == SystemButton.Restore)
                {
                    NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_RESTORE, 0);
                }
                else if (sci.LastButtonClick == SystemButton.Close)
                {
                    NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_CLOSE, 0);
                }
                else if (sci.LastButtonClick == SystemButton.Help)
                {
                    NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_CONTEXTHELP, 0);
                }
            }
        }

        internal void CloseParentForm()
        {
            Form frm = this.FindForm();

            if (frm == null)
                return;
            
            NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_CLOSE, 0);
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            if (!_CaptionVisible)
                return;

            Form frm = this.FindForm();
            if (frm == null)
                return;

            if (frm.WindowState == FormWindowState.Maximized || frm.WindowState == FormWindowState.Minimized)
                _StripContainer.SystemCaptionItem.RestoreEnabled = true;
            else
                _StripContainer.SystemCaptionItem.RestoreEnabled = false;
        }

        /// <summary>
        /// Returns collection of items on a bar.
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content), Browsable(false)]
        public SubItemsCollection QuickToolbarItems
        {
            get { return _StripContainer.CaptionContainer.SubItems; }
        }

        /// <summary>
        /// Gets the reference to the internal container item for the items displayed in control caption.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GenericItemContainer CaptionContainerItem
        {
            get { return _StripContainer.CaptionContainer; }
        }

        /// <summary>
        /// Gets the reference to the internal container for the ribbon tabs and other items.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public GenericItemContainer StripContainerItem
        {
            get { return _StripContainer.RibbonStripContainer; }
        }

        internal MetroStripContainerItem MetroStripContainer
        {
            get
            {
                return _StripContainer;
            }
        }

        protected override void PaintKeyTips(Graphics g)
        {
            if (!this.ShowKeyTips)
                return;

            KeyTipsRendererEventArgs e = new KeyTipsRendererEventArgs(g, Rectangle.Empty, "", GetKeyTipFont(), null);

            DevComponents.DotNetBar.Rendering.BaseRenderer renderer = GetRenderer();
            PaintContainerKeyTips(_StripContainer.RibbonStripContainer, renderer, e);
            if (_CaptionVisible)
                PaintContainerKeyTips(_StripContainer.CaptionContainer, renderer, e);
        }

        protected override Rectangle GetKeyTipRectangle(Graphics g, BaseItem item, Font font, string keyTip)
        {
            Rectangle r = base.GetKeyTipRectangle(g, item, font, keyTip);
            if (this.QuickToolbarItems.Contains(item))
                r.Y += 4;
            return r;
        }
        #endregion

        #region Mdi Child System Item
        internal void ClearMDIChildSystemItems(bool bRecalcLayout)
        {
            if (_StripContainer.RibbonStripContainer == null)
                return;
            bool recalc = false;
            try
            {
                if (this.Items.Contains("dotnetbarsysiconitem"))
                {
                    this.Items.Remove("dotnetbarsysiconitem");
                    recalc = true;
                }
                if (this.Items.Contains("dotnetbarsysmenuitem"))
                {
                    this.Items.Remove("dotnetbarsysmenuitem");
                    recalc = true;
                }
                if (bRecalcLayout && recalc)
                    this.RecalcLayout();
            }
            catch (Exception)
            {
            }
        }

        internal void ShowMDIChildSystemItems(System.Windows.Forms.Form objMdiChild, bool bRecalcLayout)
        {
            ClearMDIChildSystemItems(bRecalcLayout);

            if (objMdiChild == null)
                return;

            MDISystemItem mdi = new MDISystemItem("dotnetbarsysmenuitem");
            if (!objMdiChild.ControlBox)
                mdi.CloseEnabled = false;
            if (!objMdiChild.MinimizeBox)
                mdi.MinimizeEnabled = false;
            if (!objMdiChild.MaximizeBox)
            {
                mdi.RestoreEnabled = false;
            }
            mdi.ItemAlignment = eItemAlignment.Far;
            mdi.Click += new System.EventHandler(this.MDISysItemClick);

            this.Items.Add(mdi);

            if (bRecalcLayout)
                this.RecalcLayout();
        }

        private void MDISysItemClick(object sender, System.EventArgs e)
        {
            MDISystemItem mdi = sender as MDISystemItem;
            Form frm = this.FindForm();
            if (frm != null)
                frm = frm.ActiveMdiChild;
            if (frm == null)
            {
                ClearMDIChildSystemItems(true);
                return;
            }
            if (mdi.LastButtonClick == SystemButton.Minimize)
            {
                NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_MINIMIZE, 0);
            }
            else if (mdi.LastButtonClick == SystemButton.Restore)
            {
                NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_RESTORE, 0);
            }
            else if (mdi.LastButtonClick == SystemButton.Close)
            {
                NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_CLOSE, 0);
            }
            else if (mdi.LastButtonClick == SystemButton.NextWindow)
            {
                NativeFunctions.PostMessage(frm.Handle, NativeFunctions.WM_SYSCOMMAND, NativeFunctions.SC_NEXTWINDOW, 0);
            }
        }
        #endregion

        internal void InvokeMouseDown(MouseEventArgs e)
        {
            OnMouseDown(e);
        }

        internal void InvokeMouseMove(MouseEventArgs e)
        {
            OnMouseMove(e);
        }

        internal void InvokeMouseUp(MouseEventArgs e)
        {
            OnMouseUp(e);
        }

        internal void InvokeClick(EventArgs e)
        {
            OnClick(e);
        }
    }
}
