namespace Naccs.Core.Job
{
    using Naccs.Common.CustomControls;
    using Naccs.Common.Generator;
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class DivJobPanel : CommonJobPanel
    {
        private IContainer components;

        public DivJobPanel()
        {
            this.InitializeComponent();
        }

        private void ClearCom(Control root)
        {
            foreach (Control control in root.Controls)
            {
                System.Type c = control.GetType();
                if (typeof(IItemAttributesEx).IsAssignableFrom(c))
                {
                    IItemAttributesEx ex = (IItemAttributesEx) control;
                    if (ex.Rep_ID == "")
                    {
                        base.Items.ClearCommonID(ex.id);
                    }
                }
                else if (control.HasChildren)
                {
                    this.ClearCom(control);
                }
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        public override void ItemClearAll()
        {
            this.ItemClearCommon();
            if (base.bsRepeat.DataMember != "")
            {
                base.Items.ClearCurrentToEnd(base.bsRepeat.DataMember, 0);
                base.lblNextpage.Text = "";
            }
        }

        public override void ItemClearCommon()
        {
            this.ClearCom(this);
            base.Items.DsItems.Tables[Naccs.Common.Generator.ItemInfo.tblComName].AcceptChanges();
        }

        public void SetlblNextpage()
        {
            if (base.Items.CheckDataExists(base.bsRepeat.DataMember, base.bsRepeat.Position + base.repBindingSources.Count))
            {
                base.lblNextpage.Text = "*";
            }
            else
            {
                base.lblNextpage.Text = "";
            }
        }
    }
}

