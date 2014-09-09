namespace Naccs.Common.AutoComplete
{
    using System;
    using System.Windows.Forms;

    [Serializable]
    public abstract class AutoCompleteTrigger
    {
        protected AutoCompleteTrigger()
        {
        }

        public virtual TriggerState OnCommandKey(Keys keyData)
        {
            return TriggerState.None;
        }

        public virtual TriggerState OnTextChanged(string text)
        {
            return TriggerState.None;
        }
    }
}

