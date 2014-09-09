namespace Naccs.Core.Classes
{
    using Naccs.Core.Settings;
    using System;
    using System.Windows.Forms;

    public class UserKeySet
    {
        private UserKey uk = UserKey.CreateInstance();

        private string ShortCutAdd(string NameStr)
        {
            for (int i = 1; i < this.uk.JobKeyList.Count; i++)
            {
                for (int j = 0; j < this.uk.JobKeyList[i].KeyList.Count; j++)
                {
                    if (this.uk.JobKeyList[i].KeyList[j].Name == NameStr)
                    {
                        return this.uk.JobKeyList[i].KeyList[j].Sckey;
                    }
                }
            }
            return "None";
        }

        private void ShortCutDropDownLoop(ToolStripMenuItem ts)
        {
            for (int i = 0; i < ts.DropDownItems.Count; i++)
            {
                if ((ts.DropDownItems[i].Tag != null) && (ts.DropDownItems[i].Tag.ToString() != ""))
                {
                    ToolStripMenuItem item = (ToolStripMenuItem) ts.DropDownItems[i];
                    string text = this.ShortCutAdd(this.TagName(item.Tag.ToString()));
                    KeysConverter converter = new KeysConverter();
                    item.ShortcutKeys = (Keys) converter.ConvertFromInvariantString(text);
                }
                else if ((ts.DropDownItems[i].GetType() == typeof(ToolStripMenuItem)) && (((ToolStripMenuItem) ts.DropDownItems[i]).DropDownItems.Count > 0))
                {
                    this.ShortCutDropDownLoop((ToolStripMenuItem) ts.DropDownItems[i]);
                }
            }
        }

        public void ShortCutSet(ToolStripItemCollection tsic)
        {
            for (int i = 0; i < tsic.Count; i++)
            {
                if ((tsic[i].Tag != null) && (tsic[i].Tag.ToString() != ""))
                {
                    ToolStripMenuItem item = (ToolStripMenuItem) tsic[i];
                    string text = this.ShortCutAdd(this.TagName(item.Tag.ToString()));
                    KeysConverter converter = new KeysConverter();
                    item.ShortcutKeys = (Keys) converter.ConvertFromInvariantString(text);
                }
                else if (tsic[i].GetType() == typeof(ToolStripMenuItem))
                {
                    ToolStripMenuItem ts = (ToolStripMenuItem) tsic[i];
                    if (ts.DropDownItems.Count > 0)
                    {
                        this.ShortCutDropDownLoop(ts);
                    }
                }
            }
        }

        private string TagName(string tag)
        {
            if (tag.IndexOf(',') > 0)
            {
                return tag.Remove(tag.IndexOf(','));
            }
            return tag;
        }
    }
}

