namespace Naccs.Core.Job
{
    using Naccs.Common.CustomControls;
    using System;
    using System.Collections.Generic;
    using System.Xml;

    internal class CmbJPRec
    {
        private List<AuCtrlInfo> auCtrlInfoList;
        private string code = "";
        private string correlationFileName = "";
        private string dcode;
        private XmlDocument docGuide;
        private string fileName = "";
        private string guideFile = "";
        private string historyFile = "";
        private string jcode;
        private JobErrInfo jobErrs;
        private Naccs.Core.Job.JobPanel jP;
        private JobPanelStatus jpStatus = JobPanelStatus.Closed;

        public List<AuCtrlInfo> AuCtrlInfoList
        {
            get
            {
                return this.auCtrlInfoList;
            }
            set
            {
                this.auCtrlInfoList = value;
            }
        }

        public string Code
        {
            get
            {
                return this.code;
            }
            set
            {
                this.code = value;
            }
        }

        public string CorrelationFileName
        {
            get
            {
                return this.correlationFileName;
            }
            set
            {
                this.correlationFileName = value;
            }
        }

        public string DCode
        {
            get
            {
                return this.dcode;
            }
            set
            {
                this.dcode = value;
            }
        }

        public XmlDocument DocGuide
        {
            get
            {
                return this.docGuide;
            }
            set
            {
                this.docGuide = value;
            }
        }

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

        public string GuideFile
        {
            get
            {
                return this.guideFile;
            }
            set
            {
                this.guideFile = value;
            }
        }

        public string HistoryFile
        {
            get
            {
                return this.historyFile;
            }
            set
            {
                this.historyFile = value;
            }
        }

        public string JCode
        {
            get
            {
                return this.jcode;
            }
            set
            {
                this.jcode = value;
            }
        }

        public JobErrInfo JobErrs
        {
            get
            {
                return this.jobErrs;
            }
            set
            {
                this.jobErrs = value;
            }
        }

        public Naccs.Core.Job.JobPanel JP
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

