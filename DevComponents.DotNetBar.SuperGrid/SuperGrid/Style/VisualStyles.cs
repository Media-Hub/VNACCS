using System;
using System.ComponentModel;
using System.Globalization;

namespace DevComponents.DotNetBar.SuperGrid.Style
{
    ///<summary>
    /// VisualStyles
    ///</summary>
    [TypeConverter(typeof(VisualStylesConverter))]
    public class VisualStyles<T> : INotifyPropertyChanged where T : BaseVisualStyle, new()
    {
        #region Private variables

        private readonly T[] _Styles;

        #endregion

        ///<summary>
        /// Constructor
        ///</summary>
        public VisualStyles()
        {
            _Styles = new T[Enum.GetValues(typeof(StyleType)).Length - 1];
        }

        #region Public properties

        #region Indexer

        /// <summary>
        /// Gets or sets the visual style
        /// assigned to the element. Default value is null.
        /// </summary>
        [DefaultValue(null), Category("Style")]
        [Description("Indicates visual style assigned to the element")]
        public T this[StyleType e]
        {
            get
            {
                int n = ((IConvertible)e).ToInt32(null);

                if (n < 0 || n > _Styles.Length)
                    throw new Exception("Invalid Style indexer");

                return (GetStyle(n));
            }

            set
            {
                int n = ((IConvertible)e).ToInt32(null);

                if (n < 0 || n > _Styles.Length)
                    throw new Exception("Invalid Style indexer");

                SetStyle(n, value);
            }
        }

        #endregion

        #region Default

        ///<summary>
        /// The normal, default style
        ///</summary>
        [Description("Style to use for normal, default items")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T Default
        {
            get { return (GetStyle((int) StyleType.Default)); }
            set { SetStyle((int) StyleType.Default, value); }
        }

        #endregion

        #region Empty

        ///<summary>
        /// Style to use when a cell item is empty
        ///</summary>
        [Description("Style to use when a cell item is empty")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T Empty
        {
            get { return (GetStyle((int)StyleType.Empty)); }
            set { SetStyle((int)StyleType.Empty, value); }
        }

        #endregion

        #region MouseOver

        ///<summary>
        /// MouseOver
        ///</summary>
        [Description("Style to use when the Mouse is over an item")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T MouseOver
        {
            get { return (GetStyle((int)StyleType.MouseOver)); }
            set { SetStyle((int)StyleType.MouseOver, value); }
        }

        #endregion

        #region NotSelectable

        ///<summary>
        /// Style to use for non-selectable items
        ///</summary>
        [Description("Style to use for non-selectable items")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T NotSelectable
        {
            get { return (GetStyle((int)StyleType.NotSelectable)); }
            set { SetStyle((int)StyleType.NotSelectable, value); }
        }

        #endregion

        #region Selected

        ///<summary>
        /// Style to use for selected items.
        ///</summary>
        [Description("Style to use for selected items.")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T Selected
        {
            get { return (GetStyle((int)StyleType.Selected)); }
            set { SetStyle((int)StyleType.Selected, value); }
        }

        #endregion

        #region SelectedMouseOver

        ///<summary>
        /// Style to use for MouseOver selected items
        ///</summary>
        [Description("Style to use for MouseOver selected items")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T SelectedMouseOver
        {
            get { return (GetStyle((int)StyleType.SelectedMouseOver)); }
            set { SetStyle((int)StyleType.SelectedMouseOver, value); }
        }

        #endregion

        #region ReadOnly

        ///<summary>
        /// Style to use for ReadOnly items
        ///</summary>
        [Description("Style to use for ReadOnly items.")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T ReadOnly
        {
            get { return (GetStyle((int)StyleType.ReadOnly)); }
            set { SetStyle((int)StyleType.ReadOnly, value); }
        }

        #endregion

        #region ReadOnlyMouseOver

        ///<summary>
        /// Style to use for ReadOnly, MouseOver items
        ///</summary>
        [Description("Style to use for ReadOnly, MouseOver items.")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T ReadOnlyMouseOver
        {
            get { return (GetStyle((int)StyleType.ReadOnlyMouseOver)); }
            set { SetStyle((int)StyleType.ReadOnlyMouseOver, value); }
        }

        #endregion

        #region ReadOnlySelected

        ///<summary>
        /// ReadOnlySelected
        ///</summary>
        [Description("Style to use for ReadOnly, Selected items.")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T ReadOnlySelected
        {
            get { return (GetStyle((int)StyleType.ReadOnlySelected)); }
            set { SetStyle((int)StyleType.ReadOnlySelected, value); }
        }

        #endregion

        #region ReadOnlySelectedMouseOver

        ///<summary>
        /// Style to use for ReadOnly, Selected, MouseOver items
        ///</summary>
        [Description("Style to use for ReadOnly, Selected, MouseOver items.")]
        [TypeConverter(typeof(VisualStylesConverter))]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public T ReadOnlySelectedMouseOver
        {
            get { return (GetStyle((int)StyleType.ReadOnlySelectedMouseOver)); }
            set { SetStyle((int)StyleType.ReadOnlySelectedMouseOver, value); }
        }

        #endregion

        #region Styles

        ///<summary>
        /// Style to use when a cell item is empty
        ///</summary>
        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public T[] Styles
        {
            get { return (_Styles); }
        }

        #endregion

        #endregion

        #region GetStyle

        private T GetStyle(int n)
        {
            if (_Styles[n] == null)
            {
                _Styles[n] = new T();

                OnStyleChangeHandler(null, _Styles[n]);
            }

            return (_Styles[n]);
        }

        #endregion

        #region SetStyle

        private void SetStyle(int n, T value)
        {
            if (_Styles[n] != value)
            {
                T oldValue = _Styles[n];

                _Styles[n] = value;

                OnStyleChanged(Enum.GetName(typeof(StyleType), n), oldValue, value);
            }
        }

        #endregion

        #region OnStyleChanged

        private void OnStyleChanged(
            string property, T oldValue, T newValue)
        {
            OnStyleChangeHandler(oldValue, newValue);

            OnPropertyChanged(new VisualPropertyChangedEventArgs(property));
        }

        #endregion

        #region IsValid

        internal bool IsValid(Enum e)
        {
            int n = ((IConvertible) e).ToInt32(null);

            if (n < 0 || n > _Styles.Length)
                throw new Exception("Invalid Style indexer");

            return (_Styles[n] != null);
        }

        #endregion

        #region Style support routines

        #region StyleVisualChangeHandler

        private void OnStyleChangeHandler(T oldValue, T newValue)
        {
            if (oldValue != null)
                oldValue.PropertyChanged -= StyleChanged;

            if (newValue != null)
                newValue.PropertyChanged += StyleChanged;
        }

        #endregion

        #region StyleChanged

        /// <summary>
        /// Occurs when one of element visual styles has property changes.
        /// Default implementation invalidates visual appearance of element.
        /// </summary>
        /// <param name="sender">VisualStyle that changed.</param>
        /// <param name="e">Event arguments.</param>
        protected virtual void StyleChanged(object sender, PropertyChangedEventArgs e)
        {
            OnPropertyChanged(e);
        }

        #endregion

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

        #endregion
    }

    #region enums

    #region StyleType

    ///<summary>
    /// StyleType
    ///</summary>
    public enum StyleType
    {
        ///<summary>
        /// CellStyle is Not Set
        ///</summary>
        NotSet = -1,

        ///<summary>
        /// Default
        ///</summary>
        Default = 0,

        ///<summary>
        /// MouseOver
        ///</summary>
        MouseOver,

        ///<summary>
        /// Selected
        ///</summary>
        Selected,

        ///<summary>
        /// SelectedMouseOver
        ///</summary>
        SelectedMouseOver,

        ///<summary>
        /// ReadOnly
        ///</summary>
        ReadOnly,

        ///<summary>
        /// ReadOnlyMouseOver
        ///</summary>
        ReadOnlyMouseOver,

        ///<summary>
        /// ReadOnlySelected
        ///</summary>
        ReadOnlySelected,

        ///<summary>
        /// ReadOnlySelectedMouseOver
        ///</summary>
        ReadOnlySelectedMouseOver,

        ///<summary>
        /// Empty, non-populated cell
        ///</summary>
        Empty,

        ///<summary>
        /// Empty, mouseOver non-populated cell
        ///</summary>
        EmptyMouseOver,

        ///<summary>
        /// Empty, MouseOver, Selected, non-populated cell
        ///</summary>
        EmptyMouseOverSelected,

        ///<summary>
        /// Not Selectable cell
        ///</summary>
        NotSelectable,
    }

    #endregion

    #region StyleState

    ///<summary>
    /// StyleState
    ///</summary>
    [Flags]
    public enum StyleState
    {
        ///<summary>
        /// Default
        ///</summary>
        Default = 0,

        ///<summary>
        /// MouseOver
        ///</summary>
        MouseOver = (1 << 0),

        ///<summary>
        /// Selected
        ///</summary>
        Selected  = (1 << 1),

        ///<summary>
        /// ReadOnly
        ///</summary>
        ReadOnly  = (1 << 2),
    }

    #endregion

    #endregion

    #region VisualStylesConverter

    ///<summary>
    /// VisualStylesConverter
    ///</summary>
    public class VisualStylesConverter : ExpandableObjectConverter
    {
        public override object ConvertTo(
            ITypeDescriptorContext context, CultureInfo culture, object value, Type destinationType)
        {
            if (destinationType == typeof(string))
                return (" ");

            return (base.ConvertTo(context, culture, value, destinationType));
        }
    }

    #endregion

}
