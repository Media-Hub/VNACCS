using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class TestControl : Form
    {
        public TestControl()
        {
            InitializeComponent();
        }

        private void TestControl_Load(object sender, EventArgs e)
        {
            comboBoxEx1.DataSource = st.SPHANLOAIGIAHOADON;
            comboTree1.DataSource = st.SPHANLOAIGIAHOADON;
        }

        private void comboBoxEx1_TextChanged(object sender, EventArgs e)
        {
            if (comboBoxEx1._MultiColumnDisplay == null) return;
            AdvTree.AdvTree t = comboBoxEx1._MultiColumnDisplay;
            DataTable d=t.DataSource as DataTable;
            if(d==null)return;
            DataRow[] rr = d.Select(string.Format("{0} LIKE '%{1}%'", comboBoxEx1.ValueMember, comboBoxEx1.Text));
            if(rr==null)return;
            if(rr.Length<1)return;

            AdvTree.Node n = t.FindNodeByCellText(string.Format("{0}", rr[0][comboBoxEx1.ValueMember]));
            if (n == null) return;
            t.SelectedNode = n;
        }
    }
}
