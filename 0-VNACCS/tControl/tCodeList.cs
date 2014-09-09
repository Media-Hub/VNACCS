using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public partial class tCodeList : UserControl, IComponent
    {
        public event EventHandler SelectedIndexChanged;
        public event EventHandler Enter;

        public tCodeList()
        {
            InitializeComponent();            
        }

        public bool BắtBuộc
        {
            get;
            set;
        }

        public void SetToolTip(SuperTooltip tip)
        {
            tip.SetSuperTooltip(tCODE, new SuperTooltipInfo("", "", "Dữ liệu bắt buộc", null, null, eTooltipColor.Lemon, false, false, new Size(0, 0)));
            tip.SetSuperTooltip(tLIST, new SuperTooltipInfo("", "", "Dữ liệu bắt buộc", null, null, eTooltipColor.Lemon, false, false, new Size(0, 0)));
        }

        public void ClearToolTip(SuperTooltip tip)
        {
            tip.SetSuperTooltip(tCODE,null);
            tip.SetSuperTooltip(tLIST, null);
        }

        public string WatermarkText
        {
            get { return tLIST.WatermarkText; }
            set { tLIST.WatermarkText = value; }
        }

        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public object SelectedValue
        {
            get { return tLIST.SelectedValue; }
            set {
                tLIST.SelectedValue = value;
                tCODE.SelectedValue = value;
            }
        }

        public string Text
        {
            get { return tLIST.Text; }
        }

        public string ValueMember 
        {
            get { return tLIST.ValueMember; }
            set {
                tLIST.ValueMember = value;
                tCODE.ValueMember = value;
                tCODE.DisplayMember = value;
            }
        }

        public string DisplayMember
        {
            get { return tLIST.DisplayMember; }
            set
            {
                tLIST.DisplayMember = value;
            }
        }

        public string DropDownColumns
        {
            get { return tLIST.DropDownColumns; }
            set
            {
                tLIST.DropDownColumns = value;              
                tCODE.DropDownColumns = value;
            }
        }

        public string DropDownColumnsHeaders
        {
            get { return tLIST.DropDownColumnsHeaders; }
            set
            {
                tLIST.DropDownColumnsHeaders = value;
                tCODE.DropDownColumnsHeaders = value;
            }
        }

        public object DataSource
        {
            get { return tLIST.DataSource; }
            set {
                tCODE.DataSource = value;
                tLIST.DataSource = value;
            }
        }

        public int CODEWidth
        {
            get { return tCODE.Width; }
            set {
                tCODE.Width = value;
                this.Width = tCODE.Width + tLIST.Width;
            }
        }

        public int LISTWidth
        {
            get { return tLIST.Width; }
            set
            {
                tLIST.Width = value;
                this.Width = tCODE.Width + tLIST.Width;
            }
        }

        private void tLIST_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.SelectedIndexChanged != null)
                this.SelectedIndexChanged(sender, e);
        }

        private void tCODE_SelectedIndexChanged(object sender, EventArgs e)
        {
            //ko cần vì có tLIST raise rồi, nếu raise nữa thì thành 2 lần mất
        }

        private void tCODE_Enter(object sender, EventArgs e)
        {
            if (this.Enter != null)
                this.Enter(this, e);
        }

        private void tLIST_Enter(object sender, EventArgs e)
        {
            if (this.Enter != null)
                this.Enter(this, e);
        }
    }
}
