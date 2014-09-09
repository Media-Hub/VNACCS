namespace Naccs.Common.AutoComplete
{
    using System;
    using System.Windows.Forms;

    [Serializable]
    public class ShortCutTrigger : AutoCompleteTrigger
    {
        private TriggerState result;
        private Keys shortCut;

        public ShortCutTrigger()
        {
        }

        public ShortCutTrigger(Keys shortCutKeys, TriggerState resultState)
        {
            this.shortCut = shortCutKeys;
            this.result = resultState;
        }

        public override TriggerState OnCommandKey(Keys keyData)
        {
            if (keyData == this.ShortCut)
            {
                return this.ResultState;
            }
            return TriggerState.None;
        }

        public TriggerState ResultState
        {
            get
            {
                return this.result;
            }
            set
            {
                this.result = value;
            }
        }

        public Keys ShortCut
        {
            get
            {
                return this.shortCut;
            }
            set
            {
                this.shortCut = value;
            }
        }
    }
}

