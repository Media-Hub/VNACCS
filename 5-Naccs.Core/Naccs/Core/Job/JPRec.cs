namespace Naccs.Core.Job
{
    using System;

    internal class JPRec
    {
        private string fileName = "";
        private DivJobPanel jP;
        private JobPanelStatus jpStatus = JobPanelStatus.Closed;

        public string FileName
        {
            get
            {
                return this.fileName;
            }
            set
            {
                this.fileName = value;
            }
        }

        public DivJobPanel JP
        {
            get
            {
                return this.jP;
            }
            set
            {
                this.jP = value;
            }
        }

        public JobPanelStatus JPStatus
        {
            get
            {
                return this.jpStatus;
            }
            set
            {
                this.jpStatus = value;
            }
        }
    }
}

