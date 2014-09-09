using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Windows.Forms;
using System.Drawing;
using DevComponents.DotNetBar.Metro.Rendering;
using System.Collections;

namespace DevComponents.DotNetBar.Metro
{
    /// <summary>
	/// Represents panel used by MetroTabItem as a container panel for the control.
	/// </summary>
    [ToolboxItem(false), Designer(typeof(DevComponents.DotNetBar.Design.MetroTabPanelDesigner))]
    public class MetroTabPanel: PanelControl, IKeyTipsControl
	{
		#region Private Variables
		private const string INFO_TEXT="Drop controls here. Drag and Drop tabs and items to re-order.";
		private MetroTabItem _TabItem=null;
		private bool _UseCustomStyle=false;
		#endregion

		#region Internal Implementation
		/// <summary>
		/// Creates new instance of the panel.
		/// </summary>
        public MetroTabPanel()
            : base()
		{
            this.ColorSchemeStyle = eDotNetBarStyle.StyleManagerControlled;
            if (StyleManager.Style == eStyle.Metro)
                this.BackColor = MetroRender.GetColorTable().CanvasColor;
            //this.BackColor=SystemColors.Control;
		}

        protected override ElementStyle GetStyle()
        {
            if (!this.Style.Custom)
            {
                return GetDefaultStyle();
            }
            return base.GetStyle();
        }

        private bool _PopupMode = false;
        private MetroShell _MetroTab = null;
        internal MetroShell GetMetroTab()
        {
            if (_PopupMode)
                return _MetroTab;
            return this.Parent as MetroShell;
        }

        internal bool IsPopupMode
        {
            get { return _PopupMode; }
        }

        internal void SetPopupMode(bool popupMode, MetroShell rc)
        {
            _PopupMode = popupMode;
            if (_PopupMode)
            {
                _MetroTab = rc;

#if FRAMEWORK20
                if (this.Padding.Bottom > 0)
                {
                    this.Height += this.Padding.Bottom;
                    this.Padding = new System.Windows.Forms.Padding(this.Padding.Left, this.Padding.Bottom, this.Padding.Right, this.Padding.Bottom);
                }
#else
                if (this.DockPadding.Bottom > 0)
                {
                    this.DockPadding.Top = this.DockPadding.Bottom;
                    this.Height += this.DockPadding.Bottom;
                }
#endif
            }
            else
            {
#if FRAMEWORK20
                if (this.Padding.Top > 0)
                {
                    this.Height -= this.Padding.Top;
                    this.Padding = new System.Windows.Forms.Padding(this.Padding.Left, 0, this.Padding.Right, this.Padding.Bottom);
                }
#else
                if (this.DockPadding.Top > 0)
                {
                    this.Height -= this.DockPadding.Top;
                    this.DockPadding.Top = 0;
                }
#endif
                _MetroTab = null;
            }
        }

        private ElementStyle GetDefaultStyle()
        {
            return MetroRender.GetColorTable().MetroTab.TabPanelBackgroundStyle;
        }


		private Rectangle GetThemedRect(Rectangle r)
		{
			const int offset=6;
			r.Y-=offset;
			r.Height+=offset;
			return r;
		}
		
		protected override void OnPaint(PaintEventArgs e)
		{
			bool baseCall=true;
			if(DrawThemedPane && BarFunctions.ThemedOS)
			{
				Rectangle r=GetThemedRect(this.ClientRectangle);
				eTabStripAlignment tabAlignment=eTabStripAlignment.Top;
				
				Rectangle rTemp=new Rectangle(0,0,r.Width,r.Height);
				if(tabAlignment==eTabStripAlignment.Right || tabAlignment==eTabStripAlignment.Left)
					rTemp=new Rectangle(0,0,rTemp.Height,rTemp.Width);
				if(m_ThemeCachedBitmap==null || m_ThemeCachedBitmap.Size!=rTemp.Size)
				{
					DisposeThemeCachedBitmap();
					Bitmap bmp=new Bitmap(rTemp.Width,rTemp.Height,e.Graphics);
					try
					{
						Graphics gTemp=Graphics.FromImage(bmp);
						try
						{
							using(SolidBrush brush=new SolidBrush(Color.Transparent))
								gTemp.FillRectangle(brush,0,0,bmp.Width,bmp.Height);
							this.ThemeTab.DrawBackground(gTemp,ThemeTabParts.Pane,ThemeTabStates.Normal,rTemp);
						}
						finally
						{
							gTemp.Dispose();
						}
					}
					finally
					{
						if(tabAlignment==eTabStripAlignment.Left)
							bmp.RotateFlip(RotateFlipType.Rotate270FlipNone);
						else if(tabAlignment==eTabStripAlignment.Right)
							bmp.RotateFlip(RotateFlipType.Rotate90FlipNone);
						else if(tabAlignment==eTabStripAlignment.Bottom)
							bmp.RotateFlip(RotateFlipType.Rotate180FlipNone);
						e.Graphics.DrawImageUnscaled(bmp,r.X,r.Y);							
						m_ThemeCachedBitmap=bmp;
					}
				}
				else
					e.Graphics.DrawImageUnscaled(m_ThemeCachedBitmap,r.X,r.Y);

				baseCall=false;
			}

			if(baseCall)
				base.OnPaint(e);

			if(this.DesignMode && this.Controls.Count==0 && this.Text=="")
			{
				Rectangle r=this.ClientRectangle;
				r.Inflate(-2,-2);
				StringFormat sf=BarFunctions.CreateStringFormat();
				sf.Alignment=StringAlignment.Center;
				sf.LineAlignment=StringAlignment.Center;
				sf.Trimming=StringTrimming.EllipsisCharacter;
				Font font=new Font(this.Font,FontStyle.Bold);
				e.Graphics.DrawString(INFO_TEXT,font,new SolidBrush(ControlPaint.Dark(this.Style.BackColor)),r,sf);
				font.Dispose();
				sf.Dispose();
			}
            if (this.Parent is MetroShell) ((MetroShell)this.Parent).MetroTabStrip.InvalidateKeyTipsCanvas();
		}

		/// <summary>
		/// Indicates whether style of the panel is managed by tab control automatically.
		/// Set this to true if you would like to control style of the panel.
		/// </summary>
		[Browsable(true),DefaultValue(false),Category("Appearance"),Description("Indicates whether style of the panel is managed by tab control automatically. Set this to true if you would like to control style of the panel.")]
		public bool UseCustomStyle
		{
			get {return _UseCustomStyle;}
			set {_UseCustomStyle=value;}
		}

		/// <summary>
		/// Gets or sets TabItem that this panel is attached to.
		/// </summary>
		[Browsable(false),DefaultValue(null),DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public MetroTabItem MetroTabItem
		{
			get {return _TabItem;}
			set	{_TabItem=value;}
		}

		protected override void OnResize(EventArgs e)
		{
			DisposeThemeCachedBitmap();
			base.OnResize(e);
		}

		/// <summary>
		/// Gets or sets which edge of the parent container a control is docked to.
		/// </summary>
		[Browsable(false),DefaultValue(DockStyle.None)]
		public override DockStyle Dock
		{
			get {return base.Dock;}
			set {base.Dock=value;}
		}

		/// <summary>
		/// Gets or sets the size of the control.
		/// </summary>
		[Browsable(false)]
		public new Size Size
		{
			get {return base.Size;}
			set {base.Size=value;}
		}

		/// <summary>
		/// Gets or sets the coordinates of the upper-left corner of the control relative to the upper-left corner of its container.
		/// </summary>
		[Browsable(false)]
		public new Point Location
		{
			get {return base.Location;}
			set {base.Location=value;}
		}

		/// <summary>
		/// Gets or sets a value indicating whether the control is displayed.
		/// </summary>
		[Browsable(false)]
		public new bool Visible
		{
			get {return base.Visible;}
			set {base.Visible=value;}
		}

		/// <summary>
		/// Gets or sets which edges of the control are anchored to the edges of its container.
		/// </summary>
		[Browsable(false)]
		public override AnchorStyles Anchor
		{
			get {return base.Anchor;}
			set {base.Anchor=value;}
		}

        [Browsable(false), DefaultValue("")]
        public override string Text
        {
            get
            {
                return base.Text;
            }
            set
            {
                base.Text = value;
            }
        }

        private class XPositionComparer : IComparer
        {
            // Calls CaseInsensitiveComparer.Compare with the parameters reversed.
            int IComparer.Compare(object x, object y)
            {
                if (x is Control && y is Control)
                {
                    return ((Control)x).Left - ((Control)y).Left;
                }
                else
                    return ((new CaseInsensitiveComparer()).Compare(x, y));
            }
        }

        bool IKeyTipsControl.ProcessMnemonicEx(char charCode)
        {
            if (this.Controls.Count == 0) return false;

            Control[] ca = new Control[this.Controls.Count];
            this.Controls.CopyTo(ca, 0);
            ArrayList controls = new ArrayList(ca);
            controls.Sort(new XPositionComparer());
            foreach (Control c in controls)
            {
                IKeyTipsControl ktc = c as IKeyTipsControl;
                if (ktc!=null && c.Visible && c.Enabled)
                {
                    string oldStack = ktc.KeyTipsKeysStack;
                    bool ret = ktc.ProcessMnemonicEx(charCode);
                    if (ret)
                        return true;
                    if (ktc.KeyTipsKeysStack != oldStack)
                    {
                        ((IKeyTipsControl)this).KeyTipsKeysStack = ktc.KeyTipsKeysStack;
                        return false;
                    }
                }
            }
            return false;
        }

        private bool _ShowKeyTips = false;
        bool IKeyTipsControl.ShowKeyTips
        {
            get
            {
                return _ShowKeyTips;
            }
            set
            {
                _ShowKeyTips = value;
                Control[] controls = new Control[this.Controls.Count];
                this.Controls.CopyTo(controls, 0);
                foreach (Control c in controls)
                {
                    if (c is IKeyTipsControl && c.Enabled && (c.Visible || !_ShowKeyTips))
                        ((IKeyTipsControl)c).ShowKeyTips = _ShowKeyTips;
                }
            }
        }

        private string m_KeyTipsKeysStack = "";
        string IKeyTipsControl.KeyTipsKeysStack
        {
            get { return m_KeyTipsKeysStack; }
            set
            {
                m_KeyTipsKeysStack = value;
                foreach (Control c in this.Controls)
                {
                    if (c is IKeyTipsControl && c.Visible && c.Enabled)
                        ((IKeyTipsControl)c).KeyTipsKeysStack = m_KeyTipsKeysStack;
                }
            }
        }

        protected override void OnLayout(LayoutEventArgs levent)
        {
            base.OnLayout(levent);
        }
		#endregion
	}
}
