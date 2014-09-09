namespace Naccs.Core.DataView.Arrange
{
    using Naccs.Core.Classes;
    using System;
    using System.Windows.Forms;

    public static class SerchFromGridView
    {
        public static void serch(DataGridView dgv, string clName, string SearchStr)
        {
            if (dgv.RowCount >= 1)
            {
                int num;
                if (dgv.SelectedRows.Count <= 0)
                {
                    num = -1;
                }
                else
                {
                    int num2 = -1;
                    for (int i = 0; i < dgv.RowCount; i++)
                    {
                        if (dgv.Rows[i].Selected)
                        {
                            num2 = i;
                            break;
                        }
                    }
                    num = num2;
                    dgv.ClearSelection();
                }
                bool flag = true;
                for (bool flag2 = true; flag; flag2 = false)
                {
                    int num4;
                    if (flag2)
                    {
                        num4 = num + 1;
                    }
                    else
                    {
                        num4 = 0;
                    }
                    for (int j = num4; j < dgv.RowCount; j++)
                    {
                        if (!flag2 && (j > num))
                        {
                            using (MessageDialog dialog = new MessageDialog())
                            {
                                dialog.ShowMessage("I501", null, null);
                            }
                            return;
                        }
                        if (dgv.Rows[j].Cells[clName].Value.ToString().ToUpper().IndexOf(SearchStr.ToUpper()) > -1)
                        {
                            dgv.ClearSelection();
                            dgv.Rows[j].Selected = true;
                            dgv.CurrentCell = dgv.Rows[j].Cells[clName];
                            if (!dgv.Rows[j].Displayed)
                            {
                                dgv.FirstDisplayedScrollingRowIndex = dgv.Rows[j].Index;
                            }
                            flag = false;
                            return;
                        }
                        if (!flag2 && (j == num))
                        {
                            using (MessageDialog dialog2 = new MessageDialog())
                            {
                                dialog2.ShowMessage("I501", null, null);
                            }
                            return;
                        }
                    }
                }
            }
        }
    }
}

