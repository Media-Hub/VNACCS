using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Drawing;

namespace DevComponents.DotNetBar.Rendering
{
    /// <summary>
    /// Defines colors used by Radial Menu Component.
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false), TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
    public class RadialMenuColorTable : Component, INotifyPropertyChanged
    {
        #region Implementation
        public void Apply(RadialMenuColorTable table)
        {
            table.CircularBackColor = this.CircularBackColor;
            table.CircularBorderColor = this.CircularBorderColor;
            table.CircularForeColor = this.CircularForeColor;
            table.RadialMenuBackground = this.RadialMenuBackground;
            table.RadialMenuBorder = this.RadialMenuBorder;
            table.RadialMenuButtonBackground = this.RadialMenuButtonBackground;
            table.RadialMenuButtonBorder = this.RadialMenuButtonBorder;
            table.RadialMenuExpandForeground = this.RadialMenuExpandForeground;
            table.RadialMenuInactiveBorder = this.RadialMenuInactiveBorder;
            table.RadialMenuItemForeground = this.RadialMenuItemForeground;
            table.RadialMenuItemMouseOverBackground = this.RadialMenuItemMouseOverBackground;
            table.RadialMenuItemMouseOverForeground = this.RadialMenuItemMouseOverForeground;
            table.RadialMenuMouseOverBorder = this.RadialMenuMouseOverBorder;
        }
        private void OnPropertyChanged(string propertyName)
        {
            OnPropertyChanged(new PropertyChangedEventArgs(propertyName));
        }

        private Color _RadialMenuButtonBackground = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the Radial Menu button background.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of Radial Menu button background.")]
        public Color RadialMenuButtonBackground
        {
            get { return _RadialMenuButtonBackground; }
            set { _RadialMenuButtonBackground = value; OnPropertyChanged("RadialMenuButtonBorder"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuButtonBackground()
        {
            return !_RadialMenuButtonBackground.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuButtonBackground()
        {
            this.RadialMenuButtonBackground = Color.Empty;
        }

        private Color _RadialMenuButtonBorder = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the radial menu button border.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of Radial Menu Button border.")]
        public Color RadialMenuButtonBorder
        {
            get { return _RadialMenuButtonBorder; }
            set { _RadialMenuButtonBorder = value; OnPropertyChanged("RadialMenuButtonBorder"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuButtonBorder()
        {
            return !_RadialMenuButtonBorder.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuButtonBorder()
        {
            this.RadialMenuButtonBorder = Color.Empty;
        }

        private Color _RadialMenuBackground = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the Radial Menu background.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of Radial Menu background.")]
        public Color RadialMenuBackground
        {
            get { return _RadialMenuBackground; }
            set { _RadialMenuBackground = value; OnPropertyChanged("RadialMenuBackground"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuBackground()
        {
            return !_RadialMenuBackground.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuBackground()
        {
            this.RadialMenuBackground = Color.Empty;
        }

        private Color _RadialMenuBorder = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the Radial Menu border
        /// </summary>
        [Category("Appearance"), Description("Indicates color of Radial Menu border.")]
        public Color RadialMenuBorder
        {
            get { return _RadialMenuBorder; }
            set { _RadialMenuBorder = value; OnPropertyChanged("RadialMenuBorder"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuBorder()
        {
            return !_RadialMenuBorder.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuBorder()
        {
            this.RadialMenuBorder = Color.Empty;
        }

        private Color _RadialMenuMouseOverBorder = Color.Empty;
        /// <summary>
        /// Gets or sets the color of border for mouse over state for the radial menu item expand part.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of border for mouse over state for the radial menu items expand part.")]
        public Color RadialMenuMouseOverBorder
        {
            get { return _RadialMenuMouseOverBorder; }
            set { _RadialMenuMouseOverBorder = value; OnPropertyChanged("RadialMenuMouseOverBorder"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuMouseOverBorder()
        {
            return !_RadialMenuMouseOverBorder.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuMouseOverBorder()
        {
            this.RadialMenuMouseOverBorder = Color.Empty;
        }

        private Color _RadialMenuInactiveBorder = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the radial menu border for parts where items do not have sub-items, i.e. non-active area of the border.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of radial menu border for parts where items do not have sub-items, i.e. non-active area of the border..")]
        public Color RadialMenuInactiveBorder
        {
            get { return _RadialMenuInactiveBorder; }
            set { _RadialMenuInactiveBorder = value; OnPropertyChanged("RadialMenuInactiveBorder"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuInactiveBorder()
        {
            return !_RadialMenuInactiveBorder.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuInactiveBorder()
        {
            this.RadialMenuInactiveBorder = Color.Empty;
        }

        private Color _RadialMenuExpandForeground = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the expand sign which shows menu item sub-items.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of expand sign which shows menu item sub-items..")]
        public Color RadialMenuExpandForeground
        {
            get { return _RadialMenuExpandForeground; }
            set { _RadialMenuExpandForeground = value; OnPropertyChanged("RadialMenuExpandForeground"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuExpandForeground()
        {
            return !_RadialMenuExpandForeground.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuExpandForeground()
        {
            this.RadialMenuExpandForeground = Color.Empty;
        }

        private Color _RadialMenuItemForeground = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the radial menu item text.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of radial menu item text.")]
        public Color RadialMenuItemForeground
        {
            get { return _RadialMenuItemForeground; }
            set { _RadialMenuItemForeground = value; OnPropertyChanged("RadialMenuItemForeground"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuItemForeground()
        {
            return !_RadialMenuItemForeground.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuItemForeground()
        {
            this.RadialMenuItemForeground = Color.Empty;
        }

        private Color _RadialMenuItemMouseOverBackground = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the radial menu item mouse over background.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of radial menu item mouse over background.")]
        public Color RadialMenuItemMouseOverBackground
        {
            get { return _RadialMenuItemMouseOverBackground; }
            set { _RadialMenuItemMouseOverBackground = value; OnPropertyChanged("RadialMenuItemMouseOverBackground"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuItemMouseOverBackground()
        {
            return !_RadialMenuItemMouseOverBackground.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuItemMouseOverBackground()
        {
            this.RadialMenuItemMouseOverBackground = Color.Empty;
        }

        private Color _RadialMenuItemMouseOverForeground = Color.Empty;
        /// <summary>
        /// Gets or sets the color of the radial menu item mouse over foreground.
        /// </summary>
        [Category("Appearance"), Description("Indicates color of radial menu item mouse over foreground.")]
        public Color RadialMenuItemMouseOverForeground
        {
            get { return _RadialMenuItemMouseOverForeground; }
            set { _RadialMenuItemMouseOverForeground = value; OnPropertyChanged("RadialMenuItemMouseOverForeground"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeRadialMenuItemMouseOverForeground()
        {
            return !_RadialMenuItemMouseOverForeground.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetRadialMenuItemMouseOverForeground()
        {
            this.RadialMenuItemMouseOverForeground = Color.Empty;
        }
        #endregion

        #region Circular Menu Type
        private Color _CircularBackColor = Color.Empty;
        /// <summary>
        /// Gets or sets background color of the circular menu item type. Applies only to circular menu types.
        /// </summary>
        [Category("Appearance"), Description("Indicates background color of the circular menu item type.")]
        public Color CircularBackColor
        {
            get { return _CircularBackColor; }
            set { _CircularBackColor = value; OnPropertyChanged("CircularBackColor"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCircularBackColor()
        {
            return !_CircularBackColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetCircularBackColor()
        {
            this.CircularBackColor = Color.Empty;
        }

        private Color _CircularForeColor = Color.Empty;
        /// <summary>
        /// Gets or sets text color of the circular menu item type. Applies only to circular menu types.
        /// </summary>
        [Category("Appearance"), Description("Indicates text color of the circular menu item type.")]
        public Color CircularForeColor
        {
            get { return _CircularForeColor; }
            set { _CircularForeColor = value; OnPropertyChanged("CircularForeColor"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCircularForeColor()
        {
            return !_CircularForeColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetCircularForeColor()
        {
            this.CircularForeColor = Color.Empty;
        }

        private Color _CircularBorderColor = Color.Empty;
        /// <summary>
        /// Gets or sets border color of the circular menu item type. Applies only to circular menu types.
        /// </summary>
        [Category("Appearance"), Description("Indicates border color of the circular menu item type.")]
        public Color CircularBorderColor
        {
            get { return _CircularBorderColor; }
            set { _CircularBorderColor = value; OnPropertyChanged("CircularBorderColor"); }
        }
        /// <summary>
        /// Gets whether property should be serialized.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public bool ShouldSerializeCircularBorderColor()
        {
            return !_CircularBorderColor.IsEmpty;
        }
        /// <summary>
        /// Resets property to its default value.
        /// </summary>
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void ResetCircularBorderColor()
        {
            this.CircularBorderColor = Color.Empty;
        }
        #endregion        

        #region INotifyPropertyChanged Members
        /// <summary>
        /// Occurs when property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Raises the PropertyChanged event.
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            PropertyChangedEventHandler eh = PropertyChanged;
            if (eh != null) eh(this, e);
        }
        #endregion
    }
}
