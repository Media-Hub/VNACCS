using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class tForm : DevComponents.DotNetBar.OfficeForm
    {
        public tForm()
        {
            InitializeComponent();            
        }

        private void tForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = false;        
        }

        public void tShow1(string strM)
        {
            ToastNotification.Show(this, strM,eToastPosition.MiddleCenter);
        }
    }
}
