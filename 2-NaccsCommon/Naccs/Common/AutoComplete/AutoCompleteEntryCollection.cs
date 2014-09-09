namespace Naccs.Common.AutoComplete
{
    using System;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.Reflection;

    [Serializable]
    public class AutoCompleteEntryCollection : CollectionBase
    {
        public void Add(AutoCompleteEntry entry)
        {
            base.InnerList.Add(entry);
        }

        public void Add(IAutoCompleteEntry entry)
        {
            base.InnerList.Add(entry);
        }

        public void AddRange(ICollection col)
        {
            base.InnerList.AddRange(col);
        }

        public object[] ToObjectArray()
        {
            return base.InnerList.ToArray();
        }

        public IAutoCompleteEntry this[int index]
        {
            get
            {
                return (base.InnerList[index] as AutoCompleteEntry);
            }
        }

        public class AutoCompleteEntryCollectionEditor : CollectionEditor
        {
            public AutoCompleteEntryCollectionEditor(Type type) : base(type)
            {
            }

            protected override bool CanSelectMultipleInstances()
            {
                return false;
            }

            protected override Type CreateCollectionItemType()
            {
                return typeof(AutoCompleteEntry);
            }
        }
    }
}

