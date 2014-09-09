namespace Naccs.Core.Settings
{
    using Naccs.Common.JobConfig;
    using System;
    using System.IO;
    using System.Xml.Serialization;

    [XmlRoot(ElementName="HelpPath")]
    public class HelpPathClass : AbstractSettings
    {
        public const string default_guide_index_name = "guide_index.htm";
        public const string default_gym_err_index_name = "gym_err_index.html";
        public const string default_gym_err_stylesheet = "ErrorHelp.xslt";
        public const string default_term_msg_index_name = "Tnm_Msg_Index.html";
        public const string default_term_msg_name = "Tnm_Msg.xml";
        public const string default_term_msg_stylesheet = "Term_Msg.xslt";
        public const string defautl_guide_stylesheet = "InputHelp.xslt";
        private string guide_index_html;
        private string guide_path;
        private string gym_err_index_html;
        private string gym_err_path;
        private string term_msg_index_html;
        private string term_msg_path;

        public string GuideIndexHtml
        {
            get
            {
                if (this.guide_index_html == null)
                {
                    return (PathInfo.CreateInstance().GuideRoot + "guide_index.htm");
                }
                return this.guide_index_html;
            }
        }

        [XmlElement("GuideIndex")]
        public string GuideIndexHtmlUser
        {
            get
            {
                return this.guide_index_html;
            }
            set
            {
                this.guide_index_html = value;
                if (value == null)
                {
                    this.guide_path = null;
                    JobConfigFactory.GuidePath = PathInfo.CreateInstance().GuideRoot;
                }
                else
                {
                    this.guide_path = Path.GetDirectoryName(value) + @"\guide\";
                    JobConfigFactory.GuidePath = this.guide_path;
                }
            }
        }

        public string GuidePath
        {
            get
            {
                if (this.guide_path == null)
                {
                    return PathInfo.CreateInstance().GuideRoot;
                }
                return this.guide_path;
            }
        }

        [XmlIgnore]
        public string GymErrIndexHtml
        {
            get
            {
                if (this.gym_err_index_html == null)
                {
                    return (PathInfo.CreateInstance().DefaultHelpPath + "gym_err_index.html");
                }
                return this.gym_err_index_html;
            }
        }

        [XmlElement(ElementName="JobErrCodeHtml")]
        public string GymErrIndexHtmlUser
        {
            get
            {
                return this.gym_err_index_html;
            }
            set
            {
                if (this.gym_err_index_html != value)
                {
                    base.updateFlg = this.gym_err_index_html != value;
                }
                this.gym_err_index_html = value;
                if (value == null)
                {
                    this.gym_err_path = null;
                    JobConfigFactory.GymErrPath = PathInfo.CreateInstance().DefaultGymErrPath;
                }
                else
                {
                    this.gym_err_path = Path.GetDirectoryName(value) + @"\gym_err\";
                    JobConfigFactory.GymErrPath = this.gym_err_path;
                }
            }
        }

        public string GymErrPath
        {
            get
            {
                if (this.gym_err_path == null)
                {
                    return PathInfo.CreateInstance().DefaultGymErrPath;
                }
                return this.gym_err_path;
            }
        }

        public string GymErrStylesheet
        {
            get
            {
                return (this.GymErrPath + "ErrorHelp.xslt");
            }
        }

        public string TermMsg
        {
            get
            {
                return (this.TermMsgPath + "Tnm_Msg.xml");
            }
        }

        [XmlIgnore]
        public string TermMsgIndexHtml
        {
            get
            {
                if (this.term_msg_index_html == null)
                {
                    return (PathInfo.CreateInstance().DefaultHelpPath + "Tnm_Msg_Index.html");
                }
                return this.term_msg_index_html;
            }
        }

        [XmlElement(ElementName="TrmErrCodeHtml")]
        public string TermMsgIndexHtmlUser
        {
            get
            {
                return this.term_msg_index_html;
            }
            set
            {
                if (this.term_msg_index_html != value)
                {
                    base.updateFlg = this.term_msg_index_html != value;
                }
                this.term_msg_index_html = value;
                if (value == null)
                {
                    this.term_msg_path = null;
                }
                else
                {
                    this.term_msg_path = Path.GetDirectoryName(value) + @"\term\";
                }
            }
        }

        public string TermMsgPath
        {
            get
            {
                if (this.term_msg_path == null)
                {
                    return PathInfo.CreateInstance().DefaultTermMsgPath;
                }
                return this.term_msg_path;
            }
        }

        public string TermMsgStylesheet
        {
            get
            {
                return (this.TermMsgPath + "Term_Msg.xslt");
            }
        }
    }
}

