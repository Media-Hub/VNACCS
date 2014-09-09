namespace Naccs.Core.Main
{
    using System;
    using System.Windows.Forms;

    public class ComboManager
    {
        public string DeleteID(ComboBox cbID)
        {
            string text = cbID.Text;
            for (int i = 0; i < cbID.Items.Count; i++)
            {
                string str2 = cbID.Items[i].ToString();
                if (str2 == text)
                {
                    cbID.DroppedDown = false;
                    cbID.Items.RemoveAt(i);
                    return str2;
                }
            }
            return "";
        }
    }
}

