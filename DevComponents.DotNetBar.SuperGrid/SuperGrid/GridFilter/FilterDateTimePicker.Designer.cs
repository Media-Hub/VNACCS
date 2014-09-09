namespace DevComponents.DotNetBar.SuperGrid
{
    partial class FilterDateTimePicker
    {
        /// <summary> 
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterDateTimePicker));
            this.cboDateRelative = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnApply = new DevComponents.DotNetBar.ButtonX();
            this.btnCustom = new DevComponents.DotNetBar.ButtonX();
            this.cbxDateRelative = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxDateRange = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxShowAll = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxShowNull = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.cbxShowNotNull = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.dtiFromDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtiToDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cbxDateSpecific = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dtiSpecificDate = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.cbxCustom = new DevComponents.DotNetBar.Controls.CheckBoxX();
            ((System.ComponentModel.ISupportInitialize)(this.dtiFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiSpecificDate)).BeginInit();
            this.SuspendLayout();
            // 
            // cboDateRelative
            // 
            resources.ApplyResources(this.cboDateRelative, "cboDateRelative");
            this.cboDateRelative.DisplayMember = "Text";
            this.cboDateRelative.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cboDateRelative.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDateRelative.FormattingEnabled = true;
            this.cboDateRelative.Name = "cboDateRelative";
            this.cboDateRelative.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cboDateRelative.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CboDateRelativeMouseDown);
            // 
            // btnApply
            // 
            this.btnApply.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnApply, "btnApply");
            this.btnApply.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnApply.Name = "btnApply";
            this.btnApply.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnApply.Click += new System.EventHandler(this.BtnApplyClick);
            // 
            // btnCustom
            // 
            this.btnCustom.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnCustom, "btnCustom");
            this.btnCustom.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCustom.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCustom.Name = "btnCustom";
            this.btnCustom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCustom.Click += new System.EventHandler(this.BtnCustomClick);
            // 
            // cbxDateRelative
            // 
            resources.ApplyResources(this.cbxDateRelative, "cbxDateRelative");
            // 
            // 
            // 
            this.cbxDateRelative.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxDateRelative.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDateRelative.Name = "cbxDateRelative";
            this.cbxDateRelative.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // cbxDateRange
            // 
            resources.ApplyResources(this.cbxDateRange, "cbxDateRange");
            // 
            // 
            // 
            this.cbxDateRange.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxDateRange.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDateRange.Name = "cbxDateRange";
            this.cbxDateRange.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // cbxShowAll
            // 
            // 
            // 
            // 
            this.cbxShowAll.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxShowAll.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxShowAll.Checked = true;
            this.cbxShowAll.CheckState = System.Windows.Forms.CheckState.Checked;
            this.cbxShowAll.CheckValue = "Y";
            resources.ApplyResources(this.cbxShowAll, "cbxShowAll");
            this.cbxShowAll.Name = "cbxShowAll";
            this.cbxShowAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // cbxShowNull
            // 
            resources.ApplyResources(this.cbxShowNull, "cbxShowNull");
            // 
            // 
            // 
            this.cbxShowNull.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxShowNull.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxShowNull.Name = "cbxShowNull";
            this.cbxShowNull.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // cbxShowNotNull
            // 
            resources.ApplyResources(this.cbxShowNotNull, "cbxShowNotNull");
            // 
            // 
            // 
            this.cbxShowNotNull.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxShowNotNull.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxShowNotNull.Name = "cbxShowNotNull";
            this.cbxShowNotNull.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.Name = "btnOk";
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // dtiFromDate
            // 
            this.dtiFromDate.AllowEmptyState = false;
            resources.ApplyResources(this.dtiFromDate, "dtiFromDate");
            // 
            // 
            // 
            this.dtiFromDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiFromDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiFromDate.ButtonDropDown.Visible = true;
            this.dtiFromDate.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dtiFromDate.IsPopupCalendarOpen = false;
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.AnnuallyMarkedDates = ((System.DateTime[])(resources.GetObject("dtiFromDate.MonthCalendar.AnnuallyMarkedDates")));
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiFromDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiFromDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.MonthCalendar.DisplayMonth = new System.DateTime(2012, 5, 1, 0, 0, 0, 0);
            this.dtiFromDate.MonthCalendar.MarkedDates = ((System.DateTime[])(resources.GetObject("dtiFromDate.MonthCalendar.MarkedDates")));
            this.dtiFromDate.MonthCalendar.MonthlyMarkedDates = ((System.DateTime[])(resources.GetObject("dtiFromDate.MonthCalendar.MonthlyMarkedDates")));
            // 
            // 
            // 
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiFromDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiFromDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiFromDate.MonthCalendar.WeeklyMarkedDays = ((System.DayOfWeek[])(resources.GetObject("dtiFromDate.MonthCalendar.WeeklyMarkedDays")));
            this.dtiFromDate.Name = "dtiFromDate";
            this.dtiFromDate.ShowUpDown = true;
            this.dtiFromDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiFromDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DtiDateRangeMouseDown);
            // 
            // dtiToDate
            // 
            this.dtiToDate.AllowEmptyState = false;
            resources.ApplyResources(this.dtiToDate, "dtiToDate");
            // 
            // 
            // 
            this.dtiToDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiToDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiToDate.ButtonDropDown.Visible = true;
            this.dtiToDate.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dtiToDate.IsPopupCalendarOpen = false;
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.AnnuallyMarkedDates = ((System.DateTime[])(resources.GetObject("dtiToDate.MonthCalendar.AnnuallyMarkedDates")));
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiToDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiToDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.MonthCalendar.DisplayMonth = new System.DateTime(2012, 5, 1, 0, 0, 0, 0);
            this.dtiToDate.MonthCalendar.MarkedDates = ((System.DateTime[])(resources.GetObject("dtiToDate.MonthCalendar.MarkedDates")));
            this.dtiToDate.MonthCalendar.MonthlyMarkedDates = ((System.DateTime[])(resources.GetObject("dtiToDate.MonthCalendar.MonthlyMarkedDates")));
            // 
            // 
            // 
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiToDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiToDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiToDate.MonthCalendar.WeeklyMarkedDays = ((System.DayOfWeek[])(resources.GetObject("dtiToDate.MonthCalendar.WeeklyMarkedDays")));
            this.dtiToDate.Name = "dtiToDate";
            this.dtiToDate.ShowUpDown = true;
            this.dtiToDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiToDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DtiDateRangeMouseDown);
            // 
            // cbxDateSpecific
            // 
            resources.ApplyResources(this.cbxDateSpecific, "cbxDateSpecific");
            // 
            // 
            // 
            this.cbxDateSpecific.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxDateSpecific.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxDateSpecific.Name = "cbxDateSpecific";
            this.cbxDateSpecific.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // dtiSpecificDate
            // 
            this.dtiSpecificDate.AllowEmptyState = false;
            resources.ApplyResources(this.dtiSpecificDate, "dtiSpecificDate");
            this.dtiSpecificDate.AutoSelectDate = true;
            // 
            // 
            // 
            this.dtiSpecificDate.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtiSpecificDate.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiSpecificDate.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtiSpecificDate.ButtonDropDown.Visible = true;
            this.dtiSpecificDate.Format = DevComponents.Editors.eDateTimePickerFormat.Long;
            this.dtiSpecificDate.IsPopupCalendarOpen = false;
            // 
            // 
            // 
            this.dtiSpecificDate.MonthCalendar.AnnuallyMarkedDates = ((System.DateTime[])(resources.GetObject("dtiSpecificDate.MonthCalendar.AnnuallyMarkedDates")));
            // 
            // 
            // 
            this.dtiSpecificDate.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiSpecificDate.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtiSpecificDate.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtiSpecificDate.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtiSpecificDate.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiSpecificDate.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtiSpecificDate.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtiSpecificDate.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtiSpecificDate.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtiSpecificDate.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiSpecificDate.MonthCalendar.DisplayMonth = new System.DateTime(2012, 5, 1, 0, 0, 0, 0);
            this.dtiSpecificDate.MonthCalendar.MarkedDates = ((System.DateTime[])(resources.GetObject("dtiSpecificDate.MonthCalendar.MarkedDates")));
            this.dtiSpecificDate.MonthCalendar.MonthlyMarkedDates = ((System.DateTime[])(resources.GetObject("dtiSpecificDate.MonthCalendar.MonthlyMarkedDates")));
            // 
            // 
            // 
            this.dtiSpecificDate.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtiSpecificDate.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtiSpecificDate.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtiSpecificDate.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtiSpecificDate.MonthCalendar.TodayButtonVisible = true;
            this.dtiSpecificDate.MonthCalendar.WeeklyMarkedDays = ((System.DayOfWeek[])(resources.GetObject("dtiSpecificDate.MonthCalendar.WeeklyMarkedDays")));
            this.dtiSpecificDate.Name = "dtiSpecificDate";
            this.dtiSpecificDate.ShowUpDown = true;
            this.dtiSpecificDate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtiSpecificDate.MouseDown += new System.Windows.Forms.MouseEventHandler(this.DtiSpecificDateMouseDown);
            // 
            // cbxCustom
            // 
            resources.ApplyResources(this.cbxCustom, "cbxCustom");
            // 
            // 
            // 
            this.cbxCustom.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxCustom.CheckBoxStyle = DevComponents.DotNetBar.eCheckBoxStyle.RadioButton;
            this.cbxCustom.Name = "cbxCustom";
            this.cbxCustom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            // 
            // FilterDateTimePicker
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dtiToDate);
            this.Controls.Add(this.dtiFromDate);
            this.Controls.Add(this.dtiSpecificDate);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.cbxShowNotNull);
            this.Controls.Add(this.cbxShowNull);
            this.Controls.Add(this.cboDateRelative);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.btnCustom);
            this.Controls.Add(this.cbxCustom);
            this.Controls.Add(this.cbxDateSpecific);
            this.Controls.Add(this.cbxDateRelative);
            this.Controls.Add(this.cbxDateRange);
            this.Controls.Add(this.cbxShowAll);
            this.DoubleBuffered = true;
            this.MinimumSize = new System.Drawing.Size(258, 340);
            this.Name = "FilterDateTimePicker";
            resources.ApplyResources(this, "$this");
            ((System.ComponentModel.ISupportInitialize)(this.dtiFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtiSpecificDate)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.CheckBoxX cbxShowAll;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDateRange;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDateRelative;
        private ButtonX btnCustom;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cboDateRelative;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxShowNull;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxShowNotNull;
        private ButtonX btnOk;
        private ButtonX btnCancel;
        internal ButtonX btnApply;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiFromDate;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiToDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxDateSpecific;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtiSpecificDate;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxCustom;
    }
}
