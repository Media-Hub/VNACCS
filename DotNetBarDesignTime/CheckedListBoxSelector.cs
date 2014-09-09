using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar.Design
{
    public partial class CheckedListBoxSelector : UserControl
    {
        public CheckedListBoxSelector()
        {
            InitializeComponent();
        }

        private void checkedListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            buttonMoveUp.Enabled = checkedListBox1.SelectedIndex >= 0;
            buttonMoveDown.Enabled = buttonMoveUp.Enabled;
        }

        public CheckedListBox ListBox
        {
            get
            {
                return checkedListBox1;
            }
        }

        private void buttonMoveUp_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex > 0)
            {
                int index = checkedListBox1.SelectedIndex;
                object item = checkedListBox1.Items[index];
                CheckState checkState = checkedListBox1.GetItemCheckState(index);
                checkedListBox1.Items.RemoveAt(index);
                index--;
                checkedListBox1.Items.Insert(index, item);
                checkedListBox1.SelectedIndex = index;
                checkedListBox1.SetItemCheckState(index, checkState);
            }
        }

        private void buttonMoveDown_Click(object sender, EventArgs e)
        {
            if (checkedListBox1.SelectedIndex < checkedListBox1.Items.Count - 1)
            {
                int index = checkedListBox1.SelectedIndex;
                object item = checkedListBox1.Items[index];
                CheckState checkState = checkedListBox1.GetItemCheckState(index);
                checkedListBox1.Items.RemoveAt(index);
                index++;
                checkedListBox1.Items.Insert(index, item);
                checkedListBox1.SelectedIndex = index;
                checkedListBox1.SetItemCheckState(index, checkState);
            }
        }
    }
}
