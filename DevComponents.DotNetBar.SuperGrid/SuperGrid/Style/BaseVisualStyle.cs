using System.ComponentModel;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    /// <summary>
    /// Represents the base visual style.
    /// </summary>
    [ToolboxItem(false), DesignTimeVisible(false)]
    public class BaseVisualStyle : INotifyPropertyChanged
    {
        #region Private variables

        private string _Class = "";
        private object _Tag;

        #endregion

        #region Public properties

        #region Class

        /// <summary>
        /// Gets or sets the class style belongs to. 
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public string Class
        {
            get { return (_Class); }

            set
            {
                if (_Class != value)
                {
                    _Class = value;

                    OnPropertyChangedEx("Class", VisualChangeType.Layout);
                }
            }
        }

        #endregion

        #region IsEmpty

        /// <summary>
        /// Gets whether the style is logically Empty.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("Gets whether the style is logically Empty.")]
        public virtual bool IsEmpty
        {
            get { return (_Tag == null); }
        }

        #endregion

        #region Tag

        /// <summary>
        /// Gets or sets the user defined reference Tag.
        /// </summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        [Description("User defined reference Tag.")]
        public object Tag
        {
            get { return (_Tag); }
            set { _Tag = value; }
        }

        #endregion

        #endregion

        #region ApplyStyle

        /// <summary>
        /// Applies the style to instance of this style.
        /// </summary>
        /// <param name="style">Style to apply.</param>
        public void ApplyStyle(BaseVisualStyle style)
        {
        }

        #endregion

        #region GetApplyStyleTypes

        internal StyleType[] GetApplyStyleTypes(StyleType e)
        {
            StyleType[] css = null;

            switch (e)
            {
                case StyleType.Default:
                    css = new StyleType[] { StyleType.Default};
                    break;

                case StyleType.Empty:
                case StyleType.MouseOver:
                case StyleType.ReadOnly:
                case StyleType.Selected:
                case StyleType.NotSelectable:
                    css = new StyleType[] { StyleType.Default, e };
                    break;

                case StyleType.SelectedMouseOver:
                    css = new StyleType[] { StyleType.Default, StyleType.Selected, e };
                    break;

                case StyleType.ReadOnlyMouseOver:
                    css = new StyleType[] { StyleType.Default, StyleType.ReadOnly, e };
                    break;

                case StyleType.ReadOnlySelected:
                    css = new StyleType[] { StyleType.Default, StyleType.Selected, StyleType.ReadOnly, e };
                    break;

                case StyleType.ReadOnlySelectedMouseOver:
                    css = new StyleType[] { StyleType.Default, StyleType.Selected, StyleType.ReadOnly, StyleType.ReadOnlySelected, e };
                    break;
            }

            return (css);
        }

        #endregion

        #region Copy

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public BaseVisualStyle Copy()
        {
            BaseVisualStyle style = new BaseVisualStyle();

            CopyTo(style);

            return (style);
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Returns the copy of the style.
        /// </summary>
        /// <returns>Copy of the style.</returns>
        public void CopyTo(BaseVisualStyle copy)
        {
            copy.Class = _Class;
            copy.Tag = _Tag;
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

            if (eh != null)
                eh(this, e);
        }

        /// <summary>
        /// Default PropertyChanged processing
        /// </summary>
        /// <param name="s"></param>
        protected void OnPropertyChangedEx(string s)
        {
            OnPropertyChanged(new VisualPropertyChangedEventArgs(s));
        }

        /// <summary>
        /// Default PropertyChanged processing
        /// </summary>
        /// <param name="s"></param>
        /// <param name="changeType">invalidate</param>
        protected void OnPropertyChangedEx(string s, VisualChangeType changeType)
        {
            OnPropertyChanged(new VisualPropertyChangedEventArgs(s, changeType));
        }

        #endregion

        #region UpdateChangeHandler

        protected void UpdateChangeHandler(
            INotifyPropertyChanged oldValue, INotifyPropertyChanged newValue)
        {
            if (oldValue != null)
                oldValue.PropertyChanged -= ValuePropertyChanged;

            if (newValue != null)
                newValue.PropertyChanged += ValuePropertyChanged;
        }

        void ValuePropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        #endregion
    }

    #region enums

    #region Alignment

    ///<summary>
    /// Alignment of the content
    ///</summary>
    public enum Alignment
    {
        ///<summary>
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// TopLeft
        ///</summary>
        TopLeft,

        ///<summary>
        /// TopCenter
        ///</summary>
        TopCenter,

        ///<summary>
        /// TopRight
        ///</summary>
        TopRight,

        ///<summary>
        /// MiddleLeft
        ///</summary>
        MiddleLeft,

        ///<summary>
        /// MiddleCenter
        ///</summary>
        MiddleCenter,

        /// <summary>
        /// MiddleRight
        /// </summary>
        MiddleRight,

        /// <summary>
        /// BottomLeft
        /// </summary>
        BottomLeft,

        /// <summary>
        /// BottomCenter
        /// </summary>
        BottomCenter,

        /// <summary>
        /// BottomRight
        /// </summary>
        BottomRight,
    }

    #endregion

    #region BorderSide

    internal enum BorderSide
    {
        Top,
        Left,
        Bottom,
        Right
    }

    #endregion

    #region CellHighlightMode

    ///<summary>
    /// Specifies the mode of cell highlighting
    /// to employ when a grid cell is selected
    ///</summary>
    public enum CellHighlightMode
    {
        ///<summary>
        /// No highlighting
        ///</summary>
        None,

        ///<summary>
        /// Entire cell will be Highlighted
        ///</summary>
        Full,

        ///<summary>
        /// Partial cell will be Highlighted
        ///</summary>
        Partial,

        ///<summary>
        /// Cell content only will be Highlighted
        ///</summary>
        Content,
    }

    #endregion

    #region ImageHighlightMode

    ///<summary>
    /// ImageHighlightMode
    ///</summary>
    public enum ImageHighlightMode
    {
        ///<summary>
        /// NotSet
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// Default
        ///</summary>
        Default,

        ///<summary>
        /// Always
        ///</summary>
        Always,

        ///<summary>
        /// Never
        ///</summary>
        Never,
    }

    #endregion

    #region ImageOverlay

    ///<summary>
    /// How to Overlay the Image with
    /// respect to the content
    ///</summary>
    public enum ImageOverlay
    {
        ///<summary>
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// None
        ///</summary>
        None,

        ///<summary>
        /// Top
        ///</summary>
        Top,

        ///<summary>
        /// Bottom
        ///</summary>
        Bottom,
    }

    #endregion

    #region Tbool

    ///<summary>
    /// TBool - Three state boolean
    ///</summary>
    public enum Tbool
    {
        ///<summary>
        /// NotSet
        ///</summary>
        NotSet,

        ///<summary>
        /// True
        ///</summary>
        True,

        ///<summary>
        /// False
        ///</summary>
        False
    }

    #endregion

    #region VisualChangeType

    /// <summary>
    /// Defines visual property change type.
    /// </summary>
    public enum VisualChangeType
    {
        /// <summary>
        /// Visual style has changed so layout is impacted
        /// </summary>
        Layout,

        /// <summary>
        /// Visual style has changed so visuals are impacted, but not layout
        /// </summary>
        Render
    }

    #endregion

    #endregion
}
