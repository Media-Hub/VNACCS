using System;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Primitives
{
    /// <summary>
    /// Represents custom collection with INotifyPropertyChanged and INotifyCollectionChanged interface support.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    [Serializable, DebuggerDisplay("Count = {Count}"), ComVisible(false)]
    public class CustomCollection<T> : IList<T>, IList, INotifyPropertyChanged, INotifyCollectionChanged
    {
        #region Events
        /// <summary>
        /// Occurs when property value has changed.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
        #endregion

        #region Private variables

        private SimpleMonitor _monitor;
        private object _SyncRoot;
        private readonly IList<T> _Items;
        private bool _FloatLastItem;

        #endregion

        #region Constructors

        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        public CustomCollection()
        {
            _monitor = new SimpleMonitor();
            _Items = new List<T>();
        }

        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        public CustomCollection(int capacity)
        {
            _monitor = new SimpleMonitor();
            _Items = new List<T>(capacity);
        }

        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="list">List to initialize collection with.</param>
        public CustomCollection(IList<T> list)
        {
            if (list == null)
                throw new ArgumentNullException("list");

            _monitor = new SimpleMonitor();
            _Items = list;
        }

        #endregion

        #region Add

        /// <summary>
        /// Add item to collection.
        /// </summary>
        /// <param name="item">Item to add.</param>
        public void Add(T item)
        {
            if (_Items.IsReadOnly)
                throw new NotSupportedException("collection is read-only");

            InsertItem(_Items.Count, item);
        }

        #endregion

        #region Clear

        /// <summary>
        /// Remove all items from collection.
        /// </summary>
        public void Clear()
        {
            if (_Items.IsReadOnly)
                throw new NotSupportedException("collection is read-only");

            ClearItems();
        }

        /// <summary>
        /// Remove all items from collection.
        /// </summary>
        protected virtual void ClearItems()
        {
            CheckReentrancy();

            T item = default(T);

            if (_FloatLastItem == true)
            {
                if (_Items.Count > 0)
                    item = _Items[_Items.Count - 1];
            }

            _Items.Clear();

            OnPropertyChanged(new PropertyChangedEventArgs(CountString));
            OnPropertyChanged(new PropertyChangedEventArgs(ItemString));

            OnCollectionReset();

            if (_FloatLastItem == true)
                _Items.Add(item);
        }

        #endregion

        #region Contains

        /// <summary>
        /// Checks whether collection contains item.
        /// </summary>
        /// <param name="item">Item to look for.</param>
        /// <returns>true if item is in collection.</returns>
        public bool Contains(T item)
        {
            OnCollectionReadAccess();

            return (_Items.Contains(item));
        }

        #endregion

        #region CopyTo

        /// <summary>
        /// Copy collection to array.
        /// </summary>
        /// <param name="array">Array to copy to.</param>
        /// <param name="index">Index to copy from.</param>
        public void CopyTo(T[] array, int index)
        {
            OnCollectionReadAccess();

            _Items.CopyTo(array, index);
        }

        #endregion

        #region Count

        /// <summary>
        /// Returns number of items in collection.
        /// </summary>
        public int Count
        {
            get
            {
                OnCollectionReadAccess();

                return (_Items.Count);
            }
        }

        #endregion

        #region CountString

        private string CountString
        {
            get { return ("Count"); }
        }

        #endregion

        #region FloatLastItem

        internal bool FloatLastItem
        {
            get { return (_FloatLastItem); }
            set { _FloatLastItem = value; }
        }

        #endregion

        #region GetEnumerator

        /// <summary>
        /// Gets enumerator for collection.
        /// </summary>
        /// <returns>Enumerator.</returns>
        public IEnumerator<T> GetEnumerator()
        {
            OnCollectionReadAccess();

            return (_Items.GetEnumerator());
        }

        #endregion

        #region Indexer[int]

        /// <summary>
        /// Returns item at index.
        /// </summary>
        /// <param name="index">Index of item.</param>
        /// <returns>Item at index.</returns>
        public T this[int index]
        {
            get
            {
                OnCollectionReadAccess();

                return (_Items[index]);
            }

            set
            {
                if (_Items.IsReadOnly)
                    throw new NotSupportedException("collection is read-only");

                if ((index < 0) || (index >= _Items.Count))
                    throw new ArgumentOutOfRangeException();

                SetItem(index, value);
            }
        }

        #endregion

        #region IndexOf

        /// <summary>
        /// Returns index of an item.
        /// </summary>
        /// <param name="item">Reference to item.</param>
        /// <returns>Index of item.</returns>
        public int IndexOf(T item)
        {
            OnCollectionReadAccess();
            return (_Items.IndexOf(item));
        }

        #endregion

        #region Insert

        /// <summary>
        /// Insert item at specified location.
        /// </summary>
        /// <param name="index">Index to insert item in.</param>
        /// <param name="item">Item to insert.</param>
        public void Insert(int index, T item)
        {
            if ((index < 0) || (index > this._Items.Count))
                throw new ArgumentOutOfRangeException("index");

            InsertItem(index, item);
        }

        #endregion

        #region InsertItem

        /// <summary>
        /// Inserts item.
        /// </summary>
        /// <param name="index">Index to insert item at.</param>
        /// <param name="item">Reference to item.</param>
        protected virtual void InsertItem(int index, T item)
        {
            CheckReentrancy();

            if (_FloatLastItem == true)
            {
                if (_Items.Count > 0 && index == _Items.Count)
                    index--;
            }

            _Items.Insert(index, item);

            OnPropertyChanged(new PropertyChangedEventArgs(CountString));
            OnCollectionChanged(NotifyCollectionChangedAction.Add, item, index);
        }

        #endregion

        #region IsCompatibleObject

        private static bool IsCompatibleObject(object value)
        {
            return (value is T) || (value == null && !typeof(T).IsValueType);
        }

        #endregion

        #region ItemString

        private string ItemString
        {
            get { return ("Item[]"); }
        }

        #endregion

        #region Remove

        /// <summary>
        /// Removes item from collection.
        /// </summary>
        /// <param name="item">Item to remove.</param>
        /// <returns>true if item was removed.</returns>
        public bool Remove(T item)
        {
            if (_Items.IsReadOnly)
                throw new NotSupportedException("collection is read-only");

            int index = _Items.IndexOf(item);

            if (index < 0)
                return (false);

            RemoveItem(index);

            return (true);
        }

        #endregion

        #region RemoveAt

        /// <summary>
        /// Remove item at specified location.
        /// </summary>
        /// <param name="index">Index of item to remove.</param>
        public void RemoveAt(int index)
        {
            if (_Items.IsReadOnly)
                throw new NotSupportedException("collection is read-only");

            if ((index < 0) || (index >= _Items.Count))
                throw new ArgumentOutOfRangeException();

            RemoveItem(index);
        }

        #endregion

        #region RemoveItem

        /// <summary>
        /// Remove item at specified location.
        /// </summary>
        /// <param name="index">Index of item to remove.</param>
        protected virtual void RemoveItem(int index)
        {
            CheckReentrancy();

            object item = _Items[index];
            _Items.RemoveAt(index);

            OnPropertyChanged(new PropertyChangedEventArgs(CountString));
            OnCollectionChanged(NotifyCollectionChangedAction.Remove, item, index);
        }

        #endregion

        #region SetItem

        /// <summary>
        /// Set item on location.
        /// </summary>
        /// <param name="index">Index</param>
        /// <param name="item">Item to assign.</param>
        protected virtual void SetItem(int index, T item)
        {
            CheckReentrancy();

            T oldItem = _Items[index];
            _Items[index] = item;

            OnPropertyChanged(new PropertyChangedEventArgs(CountString));
            OnPropertyChanged(new PropertyChangedEventArgs(ItemString));

            OnCollectionChanged(NotifyCollectionChangedAction.Replace, oldItem, item, index);
        }

        #endregion

        #region IList support

        #region Add

        int IList.Add(object value)
        {
            if (_Items.IsReadOnly)
                throw new NotSupportedException("collection is read-only");

            VerifyValueType(value);

            Add((T)value);

            return (Count - 1);
        }

        #endregion

        #region Contains

        bool IList.Contains(object value)
        {
            return (IsCompatibleObject(value) && Contains((T)value));
        }

        #endregion

        #region GetItemsDirect

        /// <summary>
        /// Returns items directly without checks.
        /// </summary>
        /// <returns>List of items.</returns>
        protected IList<T> GetItemsDirect()
        {
            return (_Items);
        }

        #endregion

        #region Indexer[int]

        object IList.this[int index]
        {
            get
            {
                OnCollectionReadAccess();
                return this._Items[index];
            }
            set
            {
                VerifyValueType(value);
                this[index] = (T)value;
            }
        }

        #region IndexOf

        int IList.IndexOf(object value)
        {
            OnCollectionReadAccess();

            return (IsCompatibleObject(value) ? IndexOf((T)value) : -1);
        }

        #endregion

        #region Insert

        void IList.Insert(int index, object value)
        {
            if (this._Items.IsReadOnly)
                throw new NotSupportedException("collection is read-only");

            VerifyValueType(value);

            Insert(index, (T)value);
        }

        #endregion

        #region IsFixedSize

        bool IList.IsFixedSize
        {
            get
            {
                IList items = _Items as IList;

                return ((items != null) && items.IsFixedSize);
            }
        }

        #endregion

        #region IsReadOnly

        bool IList.IsReadOnly
        {
            get { return (_Items.IsReadOnly); }
        }

        #endregion

        #region Items

        /// <summary>
        /// Returns the IList interface for items in collection.
        /// </summary>
        protected IList<T> Items
        {
            get
            {
                OnCollectionReadAccess();

                return (_Items);
            }
        }

        #endregion

        #region Remove

        void IList.Remove(object value)
        {
            if (_Items.IsReadOnly)
                throw new NotSupportedException("collection is read-only");

            if (IsCompatibleObject(value))
                Remove((T)value);
        }

        #endregion



        #endregion

        #region VerifyValueType

        private static void VerifyValueType(object value)
        {
            if (!IsCompatibleObject(value))
                throw new ArgumentException("value is of wrong type");
        }

        #endregion

        #endregion

        #region IEnumerable.GetEnumerator

        IEnumerator IEnumerable.GetEnumerator()
        {
            OnCollectionReadAccess();

            return (_Items.GetEnumerator());
        }

        #endregion

        #region ICollection support

        #region CopyTo

        void ICollection.CopyTo(Array array, int index)
        {
            CheckReentrancy();
            OnCollectionReadAccess();

            if (array == null)
                throw new ArgumentNullException("array");

            if (array.Rank != 1)
                throw new ArgumentException("Argument array.Rank multi-dimensional not supported");

            if (array.GetLowerBound(0) != 0)
                throw new ArgumentException("Argument array non zero lower bound not supported");

            if (index < 0)
                throw new ArgumentOutOfRangeException("index", "index must be non-negative number");

            if ((array.Length - index) < this.Count)
                throw new ArgumentException("array too small");

            T[] localArray = array as T[];

            if (localArray != null)
            {
                _Items.CopyTo(localArray, index);
            }
            else
            {
                Type elementType = array.GetType().GetElementType();
                Type c = typeof(T);

                if (elementType == null ||
                    (!elementType.IsAssignableFrom(c) && !c.IsAssignableFrom(elementType)))
                {
                    throw new ArgumentException("Argument array of invalid type");
                }

                object[] objArray = array as object[];

                if (objArray == null)
                    throw new ArgumentException("Argument array invalid type");

                int count = this._Items.Count;

                try
                {
                    for (int i = 0; i < count; i++)
                        objArray[index++] = this._Items[i];
                }
                catch (ArrayTypeMismatchException)
                {
                    throw new ArgumentException("Argument array invalid type");
                }
            }
        }

        #endregion

        #region IsSynchronized

        bool ICollection.IsSynchronized
        {
            get { return false; }
        }

        #endregion

        #region IsReadOnly

        bool ICollection<T>.IsReadOnly
        {
            get { return this._Items.IsReadOnly; }
        }

        #endregion

        #region SyncRoot

        object ICollection.SyncRoot
        {
            get
            {
                if (_SyncRoot == null)
                {
                    ICollection items = _Items as ICollection;

                    if (items != null)
                        _SyncRoot = items.SyncRoot;
                    else
                        System.Threading.Interlocked.CompareExchange(ref _SyncRoot, new object(), null);
                }

                return (_SyncRoot);
            }
        }

        #endregion

        #endregion

        #region OnCollectionReadAccess

        /// <summary>
        /// Occurs when collection is read.
        /// </summary>
        protected virtual void OnCollectionReadAccess()
        {
        }

        #endregion

        #region OnPropertyChanged

        /// <summary>
        /// Occurs when collection property has changed.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, e);
        }

        #endregion

        #region BlockReentrancy

        /// <summary>
        /// Blocks the collection reentrancy.
        /// </summary>
        /// <returns>IDisposable to end re-entrancy</returns>
        protected IDisposable BlockReentrancy()
        {
            _monitor.Enter();

            return (_monitor);
        }

        #endregion

        #region CheckReentrancy

        /// <summary>
        /// Checks whether call creates reentrancy.
        /// </summary>
        protected void CheckReentrancy()
        {
            if ((_monitor.Busy && (CollectionChanged != null)) &&
                (CollectionChanged.GetInvocationList().Length > 1))
            {
                throw new InvalidOperationException("CustomCollectionReentrancyNotAllowed");
            }
        }

        #endregion

        #region SimpleMonitor

        [Serializable]
        private class SimpleMonitor : IDisposable
        {
            // Fields
            private int _BusyCount;

            // Methods
            public void Dispose()
            {
                _BusyCount--;
            }

            public void Enter()
            {
                _BusyCount++;
            }

            // Properties
            public bool Busy
            {
                get { return (_BusyCount > 0); }
            }
        }
        #endregion

        #region INotifyCollectionChanged Members

        /// <summary>
        /// Occurs when collection has changed.
        /// </summary>
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object item, int index)
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, item, index));
        }

        private void OnCollectionChanged(NotifyCollectionChangedAction action, object oldItem, object newItem, int index)
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(action, newItem, oldItem, index));
        }

        private void OnCollectionReset()
        {
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
        }

        /// <summary>
        /// Called when collection has changed.
        /// </summary>
        /// <param name="e">Event arguments.</param>
        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
            {
                using (this.BlockReentrancy())
                {
                    this.CollectionChanged(this, e);
                }
            }
        }
        #endregion
    }

    #region INotifyCollectionChanged
    /// <summary>
    /// Represents collection changed notification interface.
    /// </summary>
    public interface INotifyCollectionChanged
    {
        /// <summary>
        /// Occurs when collection changed.
        /// </summary>
        event NotifyCollectionChangedEventHandler CollectionChanged;
    }
    /// <summary>
    /// Defines change actions.
    /// </summary>
    public enum NotifyCollectionChangedAction
    {
        /// <summary>
        /// Items were added.
        /// </summary>
        Add,
        /// <summary>
        /// Items were removed.
        /// </summary>
        Remove,
        /// <summary>
        /// Items were replaced.
        /// </summary>
        Replace,
        /// <summary>
        /// Items were moved.
        /// </summary>
        Move,
        /// <summary>
        /// Collection was reset.
        /// </summary>
        Reset
    }
    /// <summary>
    /// Defines delegate for collection notification events.
    /// </summary>
    /// <param name="sender">Event sender.</param>
    /// <param name="e">Event arguments.</param>
    public delegate void NotifyCollectionChangedEventHandler(object sender, NotifyCollectionChangedEventArgs e);
    /// <summary>
    /// Defines collection change notification event arguments.
    /// </summary>
    public class NotifyCollectionChangedEventArgs : EventArgs
    {
        #region Private Vars

        private NotifyCollectionChangedAction _Action;
        private IList _NewItems;
        private int _NewStartingIndex;
        private IList _OldItems;
        private int _OldStartingIndex;

        #endregion

        /// <summary>
        /// Create new instance of object.
        /// </summary>
        /// <param name="action">Action</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Reset)
            {
                throw new ArgumentException("WrongActionForCtor");
            }
            this.InitializeAdd(action, null, -1);
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Specifies action.</param>
        /// <param name="changedItems">List of changed items.</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (((action != NotifyCollectionChangedAction.Add) && (action != NotifyCollectionChangedAction.Remove)) && (action != NotifyCollectionChangedAction.Reset))
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor");
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem");
                }
                this.InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                {
                    throw new ArgumentNullException("changedItems");
                }
                this.InitializeAddOrRemove(action, changedItems, -1);
            }
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Specifies action.</param>
        /// <param name="changedItem">Item that was changed.</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (((action != NotifyCollectionChangedAction.Add) && (action != NotifyCollectionChangedAction.Remove)) && (action != NotifyCollectionChangedAction.Reset))
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor");
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItem != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem");
                }
                this.InitializeAdd(action, null, -1);
            }
            else
            {
                this.InitializeAddOrRemove(action, new object[] { changedItem }, -1);
            }
        }

        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action.</param>
        /// <param name="newItems">New items in collection.</param>
        /// <param name="oldItems">Old items in collection.</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor");
            }
            if (newItems == null)
            {
                throw new ArgumentNullException("newItems");
            }
            if (oldItems == null)
            {
                throw new ArgumentNullException("oldItems");
            }
            this.InitializeMoveOrReplace(action, newItems, oldItems, -1, -1);
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action.</param>
        /// <param name="changedItems">List of changed items.</param>
        /// <param name="startingIndex">Starting index of change.</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (((action != NotifyCollectionChangedAction.Add) && (action != NotifyCollectionChangedAction.Remove)) && (action != NotifyCollectionChangedAction.Reset))
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor");
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItems != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem");
                }
                if (startingIndex != -1)
                {
                    throw new ArgumentException("ResetActionRequiresIndexMinus1");
                }
                this.InitializeAdd(action, null, -1);
            }
            else
            {
                if (changedItems == null)
                {
                    throw new ArgumentNullException("changedItems");
                }
                if (startingIndex < -1)
                {
                    throw new ArgumentException("IndexCannotBeNegative");
                }
                this.InitializeAddOrRemove(action, changedItems, startingIndex);
            }
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="changedItem">Changed item</param>
        /// <param name="index">Index of change</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (((action != NotifyCollectionChangedAction.Add) && (action != NotifyCollectionChangedAction.Remove)) && (action != NotifyCollectionChangedAction.Reset))
            {
                throw new ArgumentException("MustBeResetAddOrRemoveActionForCtor");
            }
            if (action == NotifyCollectionChangedAction.Reset)
            {
                if (changedItem != null)
                {
                    throw new ArgumentException("ResetActionRequiresNullItem");
                }
                if (index != -1)
                {
                    throw new ArgumentException("ResetActionRequiresIndexMinus1");
                }
                this.InitializeAdd(action, null, -1);
            }
            else
            {
                this.InitializeAddOrRemove(action, new object[] { changedItem }, index);
            }
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="newItem">New item</param>
        /// <param name="oldItem">Old item</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor");
            }
            this.InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, -1, -1);
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="newItems">New items.</param>
        /// <param name="oldItems">Removed items.</param>
        /// <param name="startingIndex">Starting index of change.</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor");
            }
            if (newItems == null)
            {
                throw new ArgumentNullException("newItems");
            }
            if (oldItems == null)
            {
                throw new ArgumentNullException("oldItems");
            }
            this.InitializeMoveOrReplace(action, newItems, oldItems, startingIndex, startingIndex);
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="changedItems">Changed items</param>
        /// <param name="index">New index</param>
        /// <param name="oldIndex">Old index</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, IList changedItems, int index, int oldIndex)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Move)
            {
                throw new ArgumentException("WrongActionForCtor");
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative");
            }
            this.InitializeMoveOrReplace(action, changedItems, changedItems, index, oldIndex);
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action</param>
        /// <param name="changedItem">Changed item</param>
        /// <param name="index">New index</param>
        /// <param name="oldIndex">Old index</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object changedItem, int index, int oldIndex)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Move)
            {
                throw new ArgumentException("WrongActionForCtor");
            }
            if (index < 0)
            {
                throw new ArgumentException("IndexCannotBeNegative");
            }
            object[] newItems = new object[] { changedItem };
            this.InitializeMoveOrReplace(action, newItems, newItems, index, oldIndex);
        }
        /// <summary>
        /// Creates new instance of object.
        /// </summary>
        /// <param name="action">Action.</param>
        /// <param name="newItem">New item</param>
        /// <param name="oldItem">Old item</param>
        /// <param name="index">New index</param>
        public NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction action, object newItem, object oldItem, int index)
        {
            this._NewStartingIndex = -1;
            this._OldStartingIndex = -1;
            if (action != NotifyCollectionChangedAction.Replace)
            {
                throw new ArgumentException("WrongActionForCtor");
            }
            this.InitializeMoveOrReplace(action, new object[] { newItem }, new object[] { oldItem }, index, index);
        }

        private void InitializeAdd(NotifyCollectionChangedAction action, IList newItems, int newStartingIndex)
        {
            this._Action = action;
            this._NewItems = (newItems == null) ? null : ArrayList.ReadOnly(newItems);
            this._NewStartingIndex = newStartingIndex;
        }

        private void InitializeAddOrRemove(NotifyCollectionChangedAction action, IList changedItems, int startingIndex)
        {
            if (action == NotifyCollectionChangedAction.Add)
            {
                this.InitializeAdd(action, changedItems, startingIndex);
            }
            else if (action == NotifyCollectionChangedAction.Remove)
            {
                this.InitializeRemove(action, changedItems, startingIndex);
            }
        }

        private void InitializeMoveOrReplace(NotifyCollectionChangedAction action, IList newItems, IList oldItems, int startingIndex, int oldStartingIndex)
        {
            this.InitializeAdd(action, newItems, startingIndex);
            this.InitializeRemove(action, oldItems, oldStartingIndex);
        }

        private void InitializeRemove(NotifyCollectionChangedAction action, IList oldItems, int oldStartingIndex)
        {
            this._Action = action;
            this._OldItems = (oldItems == null) ? null : ArrayList.ReadOnly(oldItems);
            this._OldStartingIndex = oldStartingIndex;
        }

        /// <summary>
        /// Gets the type of the collection change action.
        /// </summary>
        public NotifyCollectionChangedAction Action
        {
            get
            {
                return this._Action;
            }
        }
        /// <summary>
        /// Gets list of newly added items.
        /// </summary>
        public IList NewItems
        {
            get
            {
                return this._NewItems;
            }
        }
        /// <summary>
        /// Gets new starting index.
        /// </summary>
        public int NewStartingIndex
        {
            get
            {
                return this._NewStartingIndex;
            }
        }
        /// <summary>
        /// Gets list of removed items.
        /// </summary>
        public IList OldItems
        {
            get
            {
                return this._OldItems;
            }
        }
        /// <summary>
        /// Old starting index.
        /// </summary>
        public int OldStartingIndex
        {
            get
            {
                return this._OldStartingIndex;
            }
        }
    }
    #endregion

}