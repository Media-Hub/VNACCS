using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing.Drawing2D;
using System.Drawing;
using DevComponents.DotNetBar.Metro.Rendering;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Metro
{
    public class MetroForm : OfficeForm
    {
        #region Private Vars
        private const string DefaultSettingsButtonText = "<font size=\"7\">SETTINGS</font>";
        private const string DefaultHelpButtonText = "<font size=\"7\">HELP</font>";
        private ButtonItem _Settings = null;
        private ButtonItem _Help = null;
        #endregion

        #region Internal Implementation
        /// <summary>
        /// Initializes a new instance of the MetroForm class.
        /// </summary>
        public MetroForm()
        {
            if (StyleManager.Style != eStyle.Metro)
                StyleManager.Style = eStyle.Metro;
            
            StyleManager.Register(this);
            base.EnableGlass = false;
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing) StyleManager.Unregister(this);
            base.Dispose(disposing);
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateColorScheme();
            base.OnHandleCreated(e);
        }
        /// <summary>
        /// Gets the array of LinearGradientColorTable objects that describe the border colors. The colors with index 0 is used as the outer most
        /// border.
        /// </summary>
        /// <returns>Array of LinearGradientColorTable</returns>
        protected override Color[] GetBorderColors(int borderSize)
        {
            DevComponents.DotNetBar.Metro.ColorTables.MetroColorTable metroColorTable = MetroRender.GetColorTable();
            DevComponents.DotNetBar.Metro.ColorTables.MetroFormColorTable ct = metroColorTable.MetroForm;
            Color canvas = metroColorTable.CanvasColor;
            Color[] colors = new Color[((FormBorderStyle == FormBorderStyle.FixedSingle) ? 1 : ct.BorderColors.Length + (borderSize > ct.BorderColors.Length ? 1 : 0))];
            for (int i = 0; i < colors.Length; i++)
            {
                if (i > ct.BorderColors.Length - 1)
                    colors[i] = canvas;
                else
                    colors[i] = ct.BorderColors[i].Left;
            }
            
            return colors;
        }
        /// <summary>
        /// Called by StyleManager to notify control that style on manager has changed and that control should refresh its appearance if
        /// its style is controlled by StyleManager.
        /// </summary>
        /// <param name="newStyle">New active style.</param>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override void StyleManagerStyleChanged(eDotNetBarStyle newStyle)
        {
            UpdateColorScheme();
        }

        private void UpdateColorScheme()
        {
            StyleManager.UpdateMetroAmbientColors(this);
        }

        /// <summary>
        /// This property is not applicable for MetroForm.
        /// </summary>
        [Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), EditorBrowsable(EditorBrowsableState.Never)]
        public override bool EnableGlass
        {
            get
            {
                return base.EnableGlass;
            }
            set
            {
                base.EnableGlass = value;
            }
        }

        /// <summary>
        /// Gets the form path for the given input bounds.
        /// </summary>
        /// <param name="bounds">Represent the form bounds.</param>
        /// <returns></returns>
        protected override GraphicsPath GetFormPath(Rectangle bounds)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(bounds);
            return path;
        }

        protected override Rectangle GetInnerFormBounds()
        {
            Rectangle r = new Rectangle(3, 3, this.Width - 7, this.Height - 2);

#if FRAMEWORK20
            if (this.RightToLeftLayout) r = new Rectangle(3, 3, this.Width - 6, this.Height - 2);
#endif

            return r;
        }

        protected override LabelItem CreateTitleLabel()
        {
            LabelItem label = new LabelItem();
            label.GlobalItem = false;
            try
            {
                label.Font = new Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            catch
            {
                label.Font = SystemFonts.MenuFont; // SystemInformation.MenuFont;
            }
            label.Stretch = true;
            label.TextLineAlignment = StringAlignment.Center;
            label.TextAlignment = StringAlignment.Center;
            label.Text = this.Text;
            label.PaddingLeft = 3;
            label.PaddingRight = 1;

            return label;
        }

        protected override void CreateAdditionalCaptionItems(GenericItemContainer captionContainer)
        {
            // Add Settings and Help buttons
            _Settings = new ButtonItem("sysSettingsButton");
            _Settings.Text = DefaultSettingsButtonText;
            //_Settings.ItemAlignment = eItemAlignment.Far;
            _Settings.Click += InternalSettingsButtonClick;
            _Settings.SetSystemItem(true);
            _Settings.CanCustomize = false;
            _Settings.Visible = false;
            captionContainer.SubItems.Add(_Settings);

            _Help = new ButtonItem("sysHelpButton");
            _Help.Text = DefaultHelpButtonText;
            _Help.SetSystemItem(true);
            _Help.CanCustomize = false;
            _Help.Visible = false;
            //_Help.ItemAlignment = eItemAlignment.Far;
            _Help.Click += InternalHelpButtonClick;
            captionContainer.SubItems.Add(_Help);

            base.CreateAdditionalCaptionItems(captionContainer);
        }

        /// Gets or sets whether SETTINGS button is visible.
        /// </summary>
        [DefaultValue(false), Category("Appearance"), Description("Indicates whether SETTINGS button is visible.")]
        public bool SettingsButtonVisible
        {
            get { return _Settings.Visible; }
            set
            {
                if (value != _Settings.Visible)
                {
                    _Settings.Visible = value;
                    this.RecalcSize();
                }
            }
        }

        /// Gets or sets whether HELP button is visible.
        /// </summary>
        [DefaultValue(false), Category("Appearance"), Description("Indicates whether HELP button is visible.")]
        public bool HelpButtonVisible
        {
            get { return _Help.Visible; }
            set
            {
                if (value != _Help.Visible)
                {
                    _Help.Visible = value;
                    this.RecalcSize();
                }
            }
        }

        private string _HelpButtonText = "";
        /// <summary>
        /// Gets or sets the HELP button text.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates HELP button text")]
        public string HelpButtonText
        {
            get { return _HelpButtonText; }
            set
            {
                if (value != _HelpButtonText)
                {
                    string oldValue = _HelpButtonText;
                    _HelpButtonText = value;
                    OnHelpButtonTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when HelpButtonText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnHelpButtonTextChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
                _Help.Text = DefaultHelpButtonText;
            else
                _Help.Text = "<font size=\"7\">" + newValue + "</font>";
            this.RecalcSize();
        }
        private string _SettingsButtonText = "";
        /// <summary>
        /// Gets or sets the SETTINGS button text.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates SETTINGS button text")]
        public string SettingsButtonText
        {
            get { return _SettingsButtonText; }
            set
            {
                if (value != _SettingsButtonText)
                {
                    string oldValue = _SettingsButtonText;
                    _SettingsButtonText = value;
                    OnSettingsButtonTextChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when SettingsButtonText property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnSettingsButtonTextChanged(string oldValue, string newValue)
        {
            if (string.IsNullOrEmpty(newValue))
                _Settings.Text = DefaultSettingsButtonText;
            else
                _Settings.Text = "<font size=\"7\">" + newValue + "</font>";
            this.RecalcSize();
        }
        private void InternalSettingsButtonClick(object sender, EventArgs e)
        {
            OnSettingsButtonClick(e);
        }
        private void InternalHelpButtonClick(object sender, EventArgs e)
        {
            OnHelpButtonClick(e);
        }
        /// <summary>
        /// Occurs when SETTINGS button, if displayed, is clicked.
        /// </summary>
        [Description("Occurs when SETTINGS button, if displayed, is clicked.")]
        public event EventHandler SettingsButtonClick;
        /// <summary>
        /// Raises SettingsButtonClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnSettingsButtonClick(EventArgs e)
        {
            EventHandler handler = SettingsButtonClick;
            if (handler != null)
                handler(this, e);
        }
        /// <summary>
        /// Occurs when HELP button, if displayed, is clicked.
        /// </summary>
        [Description("Occurs when HELP button, if displayed, is clicked.")]
        public event EventHandler HelpButtonClick;
        /// <summary>
        /// Raises HelpButtonClick event.
        /// </summary>
        /// <param name="e">Provides event arguments.</param>
        protected virtual void OnHelpButtonClick(EventArgs e)
        {
            EventHandler handler = HelpButtonClick;
            if (handler != null)
                handler(this, e);
        }
        #endregion
    }
}
