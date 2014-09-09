using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.Text;

namespace DevComponents.DotNetBar
{
    public class tTextBox: TextBoxX
    {
        public bool BắtBuộc
        {
            get;
            set;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // tTextBox
            // 
            // 
            // 
            // 
            this.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.FocusHighlightEnabled = true;
            this.ResumeLayout(false);

        }
    }
}
