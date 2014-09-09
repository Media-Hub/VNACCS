using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace DevComponents.DotNetBar.SuperGrid
{
    ///<summary>
    ///SortableBindingList
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class SortableBindingList<T> : BindingList<T>
    {
        #region Private variables

        private bool _IsSorted;

        private ListSortDirection _ListSortDirection;
        private PropertyDescriptor _PropertyDescriptor;

        private readonly Dictionary<Type, PropertyComparer<T>> _Comparers;

        #endregion

        #region Constructors

        ///<summary>
        ///SortableBindingList
        ///</summary>
        public SortableBindingList()
            : base(new List<T>())
        {
            _Comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        ///<summary>
        ///SortableBindingList
        ///</summary>
        ///<param name="enumeration"></param>
        public SortableBindingList(IEnumerable<T> enumeration)
            : base(new List<T>(enumeration))
        {
            _Comparers = new Dictionary<Type, PropertyComparer<T>>();
        }

        #endregion

        #region Protected properties

        #region IsSortedCore

        protected override bool IsSortedCore
        {
            get { return (_IsSorted); }
        }

        #endregion

        #region SortDirectionCore

        protected override ListSortDirection SortDirectionCore
        {
            get { return (_ListSortDirection); }
        }

        #endregion

        #region SortPropertyCore

        protected override PropertyDescriptor SortPropertyCore
        {
            get { return (_PropertyDescriptor); }
        }

        #endregion

        #region SupportsSearchingCore

        protected override bool SupportsSearchingCore
        {
            get { return (true); }
        }

        #endregion

        #region SupportsSortingCore

        protected override bool SupportsSortingCore
        {
            get { return (true); }
        }

        #endregion

        #endregion

        #region ApplySortCore

        protected override void ApplySortCore(
            PropertyDescriptor property, ListSortDirection direction)
        {
            List<T> itemsList = (List<T>) Items;

            Type propertyType = property.PropertyType;

            PropertyComparer<T> comparer;
            if (_Comparers.TryGetValue(propertyType, out comparer) == false)
            {
                comparer = new PropertyComparer<T>(property, direction);
                _Comparers.Add(propertyType, comparer);
            }

            comparer.SetPropertyAndDirection(property, direction);

            itemsList.Sort(comparer);

            _PropertyDescriptor = property;
            _ListSortDirection = direction;
            _IsSorted = true;

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        #endregion

        #region RemoveSortCore

        protected override void RemoveSortCore()
        {
            _IsSorted = false;
            _PropertyDescriptor = base.SortPropertyCore;
            _ListSortDirection = base.SortDirectionCore;

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        #endregion

        #region FindCore

        protected override int FindCore(PropertyDescriptor property, object key)
        {
            int count = Count;

            for (int i = 0; i < count; ++i)
            {
                object o = property.GetValue(this[i]);

                if (o != null && o.Equals(key))
                    return (i);
            }

            return (-1);
        }

        #endregion
    }

    ///<summary>
    ///PropertyComparer
    ///</summary>
    ///<typeparam name="T"></typeparam>
    public class PropertyComparer<T> : IComparer<T>
    {
        #region Private variables

        private ListSortDirection _SortDirection;
        private PropertyDescriptor _PropertyDescriptor;
        private IComparer _Comparer;

        #endregion

        ///<summary>
        ///PropertyComparer
        ///</summary>
        ///<param name="property"></param>
        ///<param name="direction"></param>
        public PropertyComparer(PropertyDescriptor property, ListSortDirection direction)
        {
            _PropertyDescriptor = property;
            _SortDirection = direction;

            Type comparerForPropertyType = typeof(Comparer<>).MakeGenericType(property.PropertyType);

            _Comparer = (IComparer)comparerForPropertyType.InvokeMember(
                "Default", BindingFlags.Static | BindingFlags.GetProperty | BindingFlags.Public, null, null, null);
        }

        #region IComparer<T> Members

        public int Compare(T x, T y)
        {
            int result = _Comparer.Compare(_PropertyDescriptor.GetValue(x),
                                           _PropertyDescriptor.GetValue(y));

            if (_SortDirection == ListSortDirection.Descending)
                return (result * -1);

            return (result);
        }

        #endregion

        #region SetPropertyAndDirection

        ///<summary>
        ///SetPropertyAndDirection
        ///</summary>
        ///<param name="descriptor"></param>
        ///<param name="direction"></param>
        public void SetPropertyAndDirection(
            PropertyDescriptor descriptor, ListSortDirection direction)
        {
            _PropertyDescriptor = descriptor;
            _SortDirection = direction;
        }

        #endregion
    }
}