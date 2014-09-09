using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    /// <summary>
    /// Manages whether ambient property settings (BackColor, ForeColor etc.) are applied to child controls of the form when StyleManager component changes style.
    /// </summary>
    [ToolboxBitmap(typeof(StyleManagerAmbient), "StyleManager.ico"), ToolboxItem(true)]
    [ProvideProperty("EnableAmbientSettings", typeof(Control))]
    public class StyleManagerAmbient : Component, IExtenderProvider
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the StyleManagerAmbient class.
        /// </summary>
        public StyleManagerAmbient()
        {
            StyleManager.RegisterAmbientManager(this);
        }

        /// <summary>
        /// Initializes a new instance of the StyleManagerAmbient class with the specified container.
        /// </summary>
        /// <param name="container">An IContainer that represents the container for the command.</param>
        public StyleManagerAmbient(IContainer container)
            : this()
        {
            container.Add(this);
        }

        protected override void Dispose(bool disposing)
        {
            StyleManager.UnregisterAmbientManager(this);
            base.Dispose(disposing);
        }
        #endregion

        #region Implementation
        private Dictionary<Control, eAmbientSettings> _Settings = new Dictionary<Control, eAmbientSettings>();
        /// <summary>
        /// Gets ambient settings StyleManager is allowed to change on the control.
        /// </summary>
        [DefaultValue(eAmbientSettings.All), Description("Indicates ambient settings StyleManager is allowed to change on the control.")]
        [System.ComponentModel.Editor("DevComponents.DotNetBar.Design.FlagEnumUIEditor, DevComponents.DotNetBar.Design, Version=11.1.0.0, Culture=neutral,  PublicKeyToken=90f470f34c89ccaf", typeof(System.Drawing.Design.UITypeEditor))]
        public eAmbientSettings GetEnableAmbientSettings(Control c)
        {
            eAmbientSettings settings = eAmbientSettings.All;
            if (_Settings.TryGetValue(c, out settings))
                return settings;

            return eAmbientSettings.All;
        }

        /// <summary>
        /// Sets the ambient settings StyleManager is allowed to change on component.
        /// </summary>
        /// <param name="c">Reference to supported component.</param>
        /// <param name="ambientSettings">Ambient settings that StyleManager may change.</param>
        public void SetEnableAmbientSettings(Control c, eAmbientSettings ambientSettings)
        {
            if (_Settings.ContainsKey(c))
            {
                if (ambientSettings == eAmbientSettings.All)
                    _Settings.Remove(c);
                else
                    _Settings[c] = ambientSettings;
            }
            else if (ambientSettings != eAmbientSettings.All)
            {
                _Settings.Add(c, ambientSettings);
            }
        }

        internal bool Contains(Control c)
        {
            return _Settings.ContainsKey(c);
        }
        #endregion

        #region IExtenderProvider Members

        public bool CanExtend(object extendee)
        {
            if (extendee is Control)
                return true;
            return false;
        }

        #endregion

    }

    /// <summary>
    /// Specifies ambient settings enabled on the control for StyleManager.
    /// </summary>
    [Flags]
    public enum eAmbientSettings
    {
        /// <summary>
        /// All ambient settings are allowed to change.
        /// </summary>
        All = BackColor | ForeColor | ChildControls,
        /// <summary>
        /// StyleManager cannot change ambient settings.
        /// </summary>
        None = 0,
        /// <summary>
        /// StyleManager should process child controls.
        /// </summary>
        ChildControls = 1,
        /// <summary>
        /// StyleManager should change BackColor.
        /// </summary>
        BackColor = 2,
        /// <summary>
        /// StyleManager should change ForeColor.
        /// </summary>
        ForeColor = 4
    }
}
