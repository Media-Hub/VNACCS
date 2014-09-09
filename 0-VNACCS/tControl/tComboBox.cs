using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevComponents.DotNetBar
{
    public class tComboBox: ComboBoxEx 
    {   
        public tComboBox()
        {
            InitializeComponent();
        }

        public bool BắtBuộc
        {
            get;
            set;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // tComboBox
            // 
            this.IntegralHeight = false;
            this.ResumeLayout(false);

        }
    }
}
