namespace Naccs.Common.CustomControls
{
    using System;
    using System.Drawing;
    using System.Windows.Forms;
    using Naccs.Common.Properties;

    public class DesignControls
    {
        public static Type[] AllControls = new Type[] { 
            Label, RanLabel, TextBox, MaskedTextBox, DivTextBox, ComboBox, InputComboBox, CheckBox, RadioButton, DataTimePicker, Button, GroupBox, Panel, TabControl, TabPage, GridView, 
            GridViewColumn, Navigator, JobPanel, CommaText, Page, Ran, PrtTextBox, Line, Rectangle, GStamp, PageLabel, TitlePanel, Barcode, HandOcr, CompOcr
         };
        public static Type Barcode = typeof(LBBarcode);
        public static Type Button = typeof(System.Windows.Forms.Button);
        public static Type CheckBox = typeof(LBCheckBox);
        public static Type ComboBox = typeof(LBComboBox);
        public static Type CommaText = typeof(LBCommaTextBox);
        public static Type CompOcr = typeof(LBCompOcr);
        public static Type DataTimePicker = typeof(LBDateTimePicker);
        public static Type DivTextBox = typeof(LBDivTextBox);
        public static Color ErrorColor = Color.FromArgb(0xff, 0xff, 0xc0);
        public const string fmFileName = "FileName";
        public const string fmJobCode = "JobCode";
        public const string fmJobName = "JobName";
        public const string fmPage = "Page";
        public const string fmPageMax = "PageMax";
        public const string fmPagePro = "PageProgress";
        public const string fmPageTitle = "PageTitle";
        public const string fmPageTitle2 = "PageTitle2";
        public const string fmPrintDate = "PrintDate";
        public const string fmPrintTime = "PrintTime";
        public const string fmReceiveDateTime = "ReceiveDateTime";
        public static Type GridView = typeof(LBDataGridView);
        public static Type GridViewCellStyle = typeof(DataGridViewCellStyle);
        public static Type GridViewColumn = typeof(DataGridViewColumn);
        public static Type GroupBox = typeof(LBGroupBox);
        public static Type GStamp = typeof(LBGStamp);
        public static Type HandOcr = typeof(LBHandOcr);
        public static Type InputComboBox = typeof(LBInputComboBox);
        public static Type JobPanel = typeof(Naccs.Common.CustomControls.JobPanel);
        public static Type Label = typeof(LBLabel);
        public static Type Line = typeof(LBLine);
        public static Color LinkForeColor = Color.BlueViolet;
        public static Color MandatoryBackColor = Color.LightCyan;
        public static Type MaskedTextBox = typeof(LBMaskedTextBox);
        public static Type Navigator = typeof(LBNavigatorDesign);
        public static Color NormalBackColor = SystemColors.Window;
        public static Type Page = typeof(LBPage);
        public static Type PageLabel = typeof(LBPageLabel);
        public static Type Panel = typeof(LBPanel);
        public static Type PrtTextBox = typeof(LBPrtTextBox);
        public static Type RadioButton = typeof(LBRadioButton);
        public static Type Ran = typeof(LBRan);
        public static Type RanLabel = typeof(Naccs.Common.CustomControls.RanLabel);
        public static Color ReadOnlyBackColor = SystemColors.Control;
        public static Type Rectangle = typeof(LBRectangle);
        public static readonly string stMsgRequired = Resources.ResourceManager.GetString("COM18");
        public static Color SysErrorColor = Color.FromArgb(0xc0, 0xff, 0xc0);
        public static Type TabControl = typeof(LBTabControl);
        public static Type TabPage = typeof(System.Windows.Forms.TabPage);
        public static Type TextBox = typeof(LBTextBox);
        public static Type TextBoxColumn = typeof(LBTextBoxColumn);
        public static Type TitlePanel = typeof(LBTitlePanel);
        public static Type[] ToolBoxControls = new Type[] { Label, RanLabel, GroupBox, Panel, TabControl, Navigator, GridView };
        public static Color WarningColor = Color.FromArgb(0xff, 0xc0, 0xff);
        public static int WM_COPY = 0x301;
        public static int WM_CUT = 0x300;
        public static int WM_PASTE = 770;
    }
}

