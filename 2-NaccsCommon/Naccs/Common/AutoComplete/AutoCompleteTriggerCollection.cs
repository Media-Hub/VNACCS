namespace Naccs.Common.AutoComplete
{
    using System;
    using System.Collections;
    using System.ComponentModel.Design;
    using System.Reflection;
    using System.Windows.Forms;

    [Serializable]
    public class AutoCompleteTriggerCollection : CollectionBase
    {
        public void Add(AutoCompleteTrigger item)
        {
            base.InnerList.Add(item);
        }

        public virtual TriggerState OnCommandKey(Keys keyData)
        {
            foreach (AutoCompleteTrigger trigger in base.InnerList)
            {
                TriggerState state = trigger.OnCommandKey(keyData);
                if (state != TriggerState.None)
                {
                    return state;
                }
            }
            return TriggerState.None;
        }

        public virtual TriggerState OnTextChanged(string text)
        {
            foreach (AutoCompleteTrigger trigger in base.InnerList)
            {
                TriggerState state = trigger.OnTextChanged(text);
                if (state != TriggerState.None)
                {
                    return state;
                }
            }
            return TriggerState.None;
        }

        public void Remove(AutoCompleteTrigger item)
        {
            base.InnerList.Remove(item);
        }

        public AutoCompleteTrigger this[int index]
        {
            get
            {
                return (base.InnerList[index] as AutoCompleteTrigger);
            }
        }

        public class AutoCompleteTriggerCollectionEditor : CollectionEditor
        {
            public AutoCompleteTriggerCollectionEditor(System.Type type) : base(type)
            {
            }

            protected override bool CanSelectMultipleInstances()
            {
                return false;
            }

            protected override System.Type CreateCollectionItemType()
            {
                return typeof(ShortCutTrigger);
            }

            protected override System.Type[] CreateNewItemTypes()
            {
                return new System.Type[] { typeof(ShortCutTrigger), typeof(TextLengthTrigger) };
            }
        }
    }
}

