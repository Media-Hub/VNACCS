namespace Naccs.Common.AutoComplete
{
    using System;

    [Serializable]
    public class AutoCompleteEntry : IAutoCompleteEntry
    {
        private string displayName;
        private string[] matchStrings;

        public AutoCompleteEntry()
        {
            this.displayName = string.Empty;
        }

        public AutoCompleteEntry(string name, string[] matchList)
        {
            this.displayName = string.Empty;
            this.displayName = name;
            this.matchStrings = matchList;
        }

        public override string ToString()
        {
            return this.DisplayName;
        }

        public string DisplayName
        {
            get
            {
                return this.displayName;
            }
            set
            {
                this.displayName = value;
            }
        }

        public string[] MatchStrings
        {
            get
            {
                if (this.matchStrings == null)
                {
                    this.matchStrings = new string[] { this.DisplayName };
                }
                return this.matchStrings;
            }
        }
    }
}

