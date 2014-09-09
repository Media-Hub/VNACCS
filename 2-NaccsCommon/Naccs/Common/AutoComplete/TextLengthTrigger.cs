namespace Naccs.Common.AutoComplete
{
    using System;

    [Serializable]
    public class TextLengthTrigger : AutoCompleteTrigger
    {
        private int textLength;

        public TextLengthTrigger()
        {
            this.textLength = 2;
        }

        public TextLengthTrigger(int length)
        {
            this.textLength = 2;
            this.textLength = length;
        }

        public override TriggerState OnTextChanged(string text)
        {
            if (text.Length >= this.TextLength)
            {
                return TriggerState.Show;
            }
            if (text.Length < this.TextLength)
            {
                return TriggerState.Hide;
            }
            return TriggerState.None;
        }

        public int TextLength
        {
            get
            {
                return this.textLength;
            }
            set
            {
                this.textLength = value;
            }
        }
    }
}

