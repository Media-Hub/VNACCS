namespace Naccs.Core.Job
{
    using Naccs.Common.Generator;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class JobPanel : CommonJobPanel
    {
        private IContainer components;

        public JobPanel()
        {
            base.Items = new Naccs.Common.Generator.ItemInfo();
            this.InitializeComponent();
        }

        public int CreateCmbPanel(string fileName)
        {
            return base.CreatePanel(fileName);
        }

        public override int CreatePanel(string fileName)
        {
            int num = base.Items.CreateDataSet(fileName);
            if (num != 0)
            {
                return num;
            }
            return base.CreatePanel(fileName);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        public string GetData(Naccs.Common.Generator.ItemInfo.JobDataStatus jds)
        {
            return base.Items.GetDataList(jds);
        }

        public string GetIdDataFromTbl(string stID, int iNo)
        {
            string idData = base.Items.GetIdData(stID, iNo);
            if (idData == null)
            {
                idData = "";
            }
            return idData;
        }

        private void InitializeComponent()
        {
            this.components = new Container();
        }

        public override void ItemClearAll()
        {
            int newIndex = -1;
            try
            {
                if (base.bsRepeat != null)
                {
                    newIndex = base.bsRepeat.Position;
                    base.bsRepeat.RaiseListChangedEvents = false;
                    base.bsRepeat.SuspendBinding();
                }
                base.Items.ClearAll();
            }
            finally
            {
                if (base.bsRepeat != null)
                {
                    base.bsRepeat.RaiseListChangedEvents = true;
                    base.bsRepeat.ResumeBinding();
                    base.bsRepeat.ResetBindings(false);
                    if (base.bsRepeat.Position == newIndex)
                    {
                        ListChangedEventArgs e = new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex);
                        base.bsRepeat.OnPositionChangedEx(e);
                    }
                    else
                    {
                        base.bsRepeat.Position = newIndex;
                    }
                }
                base.lblNextpage.Text = "";
            }
        }

        public override void ItemClearCommon()
        {
            base.Items.ClearCommon();
        }

        public void SetCorrelation(string FileName)
        {
            base.Items.ReadCorrelation(FileName);
        }

        public void SetData(List<string> stList, Naccs.Common.Generator.ItemInfo.JobDataStatus jds, string tblName)
        {
            int newIndex = -1;
            try
            {
                if (base.bsRepeat != null)
                {
                    newIndex = base.bsRepeat.Position;
                    base.bsRepeat.RaiseListChangedEvents = false;
                    base.bsRepeat.SuspendBinding();
                }
                base.Items.SetDataList(stList, jds, tblName);
            }
            finally
            {
                if (base.bsRepeat != null)
                {
                    base.bsRepeat.RaiseListChangedEvents = true;
                    base.bsRepeat.ResumeBinding();
                    base.bsRepeat.ResetBindings(false);
                    if (base.bsRepeat.Position == newIndex)
                    {
                        ListChangedEventArgs e = new ListChangedEventArgs(ListChangedType.ItemMoved, newIndex);
                        base.bsRepeat.OnPositionChangedEx(e);
                    }
                    else
                    {
                        base.bsRepeat.Position = newIndex;
                    }
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
    }
}

