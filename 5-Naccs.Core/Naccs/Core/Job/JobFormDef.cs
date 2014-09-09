namespace Naccs.Core.Job
{
    using System;
    using System.Windows.Forms;

    internal class JobFormDef
    {
        public const string ATR_ACTION = "action";
        public const string ATR_AUTO_JOBCODE = "autojobcode";
        public const string ATR_AUTOSEND = "autosend";
        public const string ATR_COLUMNID = "column_id";
        public const string ATR_DISPLAYCODE = "displaycode";
        public const string ATR_FROM = "from";
        public const string ATR_FROM_SOURCE = "from_source";
        public const string ATR_JOBCODE = "jobcode";
        public const string ATR_LINKID = "linkid";
        public const string ATR_NO = "no";
        public const string ATR_REPID = "rep_id";
        public const string ATR_SOURCE = "source";
        public const string ATR_SYSTEM = "system";
        public const string ATR_TEXT = "text";
        public const string ATR_TO = "to";
        public const string ATR_TO_SOURCE = "to_source";
        public const string ATR_VALUE = "value";
        public const string AttachFilterTxt = "Type(*.txt;*.csv;*.doc;*.xls;*.jpg;*.bmp;*.tif;*.pdf;*.xml;*.docx;*.xlsx;*.jpeg;*.html;*.htm;*.gif;*.tiff)|*.txt;*.csv;*.doc;*.xls;*.jpg;*.bmp;*.tif;*.pdf;*.xml;*.docx;*.xlsx;*.jpeg;*.html;*.htm;*.gif;*.tiff";
        public const string CMB_AIR = "Air-NACCS";
        public const string CMB_ANIPAS = "ANIPAS";
        public const string CMB_COMMON = "Air/Sea共通";
        public const string CMB_EDI = "港湾EDI";
        public const string CMB_FAINS = "FAINS";
        public const string CMB_IFS = "IFS";
        public const string CMB_JETRAS = "JETRAS";
        public const string CMB_PASSENGER = "乗員乗客システム";
        public const string CMB_PQ = "PQ-NETWORK";
        public const string CMB_SEA = "Sea-NACCS";
        public const string COMBI = "combi";
        public const int DEF_CMN_HEIGHT = 0x145;
        public const int DEF_CODE_HEIGHT = 0xfe;
        public const int DEF_COM_WIDTH = 300;
        public const ModeFont DEF_FONTMODE = ModeFont.normal;
        public const int DEF_GUIDE_HEIGHT = 0x73;
        public const int DEF_HEAD_HEIGHT = 0xc3;
        public const int DEF_HEIGHT = 0x300;
        public const FormWindowState DEF_MAXIMIZED = FormWindowState.Normal;
        public const int DEF_MSG_HEIGHT = 80;
        public const char DEF_SYSMODE = '2';
        public const string DEF_SYSTEM_CIS = "CIS";
        public const string DEF_SYSTEM_NACCS = "NACCS";
        public const int DEF_WIDTH = 0x400;
        public const string DIVIDE = "divide";
        public const string FilterCsv = "CSV file(*.csv)|*.csv|All files(*.*)|*.*";
        public const string FilterTxt = "Text file(*.txt)|*.txt|All files(*.*)|*.*";
        public const string NORMAL = "normal";
        public const string TAG_CATEGORY = "category";
        public const string TAG_CMN_HEIGHT = "PnlRegularSizeH";
        public const string TAG_CODE_HEIGHT = "PnlResCodeSizeH";
        public const string TAG_COM_WIDTH = "PnlCommonSizeW";
        public const string TAG_DESTINATION = "destination";
        public const string TAG_FONTMODE = "FontMode";
        public const string TAG_GUIDE_HEIGHT = "PnlGuideSizeH";
        public const string TAG_HEAD_HEIGHT = "PnlHeaderSizeH";
        public const string TAG_HEIGHT = "SizeH";
        public const string TAG_ITEM = "item";
        public const string TAG_LINK = "link";
        public const string TAG_MAXIMIZED = "Maximized";
        public const string TAG_MSG_HEIGHT = "PnlResMsgSizeH";
        public const string TAG_ROOT = "JobConfig";
        public const string TAG_SYSTEM = "System";
        public const string TAG_WIDTH = "SizeW";
    }
}

