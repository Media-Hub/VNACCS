namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(PageSetupDialog)), DisplayName("Print Page Label")]
    public class LBPageLabel : Label
    {
        private string pageFormat;

        public LBPageLabel()
        {
            this.PageFormat = "";
            this.DoubleBuffered = true;
        }

        [Category("Service"), TypeConverter(typeof(PageFormatConverter)), Description("Page : Page number\nPageMax : Max page number\nPageProgress : Page progress number\nPageTitle: Page title 1\nPageTitle2 : Page title 2\nPrintDate: Print date\nPrintTime : Print time\nFileName : File Name\nJobName : Service name\nJobCode : Service code\nReceiveDateTime : Receive datetime\n")]
        public string PageFormat
        {
            get
            {
                return this.pageFormat;
            }
            set
            {
                this.pageFormat = value;
                if (this.pageFormat == "Page")
                {
                    this.Text = "pp";
                }
                else if (this.pageFormat == "PageMax")
                {
                    this.Text = "mp";
                }
                else if (this.pageFormat == "PageProgress")
                {
                    this.Text = "pp / mp";
                }
                else if ((this.pageFormat != "PageTitle") && (this.pageFormat != "PageTitle2"))
                {
                    if (this.pageFormat == "PrintDate")
                    {
                        this.Text = "yyyy/MM/dd";
                    }
                    else if (this.pageFormat == "PrintTime")
                    {
                        this.Text = "HH:mm:ss";
                    }
                    else if (this.pageFormat == "FileName")
                    {
                        this.Text = "filename";
                    }
                    else if (this.pageFormat == "JobName")
                    {
                        this.Text = "Jobname";
                    }
                    else if (this.pageFormat == "JobCode")
                    {
                        this.Text = "JOBCD";
                    }
                    else if (this.pageFormat == "ReceiveDateTime")
                    {
                        this.Text = "dd/MM/yyyy HH:mm";
                    }
                    else
                    {
                        this.Text = "(none)";
                    }
                }
            }
        }
    }
}

