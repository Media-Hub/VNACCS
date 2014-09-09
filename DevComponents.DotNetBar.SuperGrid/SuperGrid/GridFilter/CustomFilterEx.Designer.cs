namespace DevComponents.DotNetBar.SuperGrid
{
    partial class CustomFilterEx
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CustomFilterEx));
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.btnOk = new DevComponents.DotNetBar.ButtonX();
            this.btnApply = new DevComponents.DotNetBar.ButtonX();
            this.btnReset = new DevComponents.DotNetBar.ButtonX();
            this.lblFilterName = new DevComponents.DotNetBar.LabelX();
            this.tbxDesc = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.lblDesc = new DevComponents.DotNetBar.LabelX();
            this.cbFilterName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.btnNewFilter = new DevComponents.DotNetBar.ButtonX();
            this.btnDeleteFilter = new DevComponents.DotNetBar.ButtonX();
            this.lblEnterText = new DevComponents.DotNetBar.LabelX();
            this.cbxShowInPopup = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.btnClear = new DevComponents.DotNetBar.ButtonX();
            this.filterExprEdit1 = new DevComponents.DotNetBar.SuperGrid.FilterExprEdit();
            this.SuspendLayout();
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancelClick);
            // 
            // btnOk
            // 
            this.btnOk.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOk.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.btnOk.Name = "btnOk";
            this.btnOk.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOk.Click += new System.EventHandler(this.BtnOkClick);
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
            // btnReset
            // 
            this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnReset, "btnReset");
            this.btnReset.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnReset.Name = "btnReset";
            this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReset.Click += new System.EventHandler(this.BtnResetClick);
            // 
            // lblFilterName
            // 
            // 
            // 
            // 
            this.lblFilterName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            resources.ApplyResources(this.lblFilterName, "lblFilterName");
            this.lblFilterName.Name = "lblFilterName";
            // 
            // tbxDesc
            // 
            resources.ApplyResources(this.tbxDesc, "tbxDesc");
            // 
            // 
            // 
            this.tbxDesc.Border.Class = "TextBoxBorder";
            this.tbxDesc.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.tbxDesc.Name = "tbxDesc";
            this.tbxDesc.TextChanged += new System.EventHandler(this.TbxDescTextChanged);
            // 
            // lblDesc
            // 
            resources.ApplyResources(this.lblDesc, "lblDesc");
            // 
            // 
            // 
            this.lblDesc.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblDesc.Name = "lblDesc";
            // 
            // cbFilterName
            // 
            resources.ApplyResources(this.cbFilterName, "cbFilterName");
            this.cbFilterName.DisplayMember = "Text";
            this.cbFilterName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterName.FormattingEnabled = true;
            this.cbFilterName.Name = "cbFilterName";
            this.cbFilterName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbFilterName.SelectedIndexChanged += new System.EventHandler(this.CbFilterNameSelectedIndexChanged);
            this.cbFilterName.TextUpdate += new System.EventHandler(this.CbFilterNameTextUpdate);
            this.cbFilterName.DropDown += new System.EventHandler(this.CbFilterNameDropDown);
            // 
            // btnNewFilter
            // 
            this.btnNewFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnNewFilter, "btnNewFilter");
            this.btnNewFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNewFilter.Name = "btnNewFilter";
            this.btnNewFilter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNewFilter.Click += new System.EventHandler(this.BtnNewFilterClick);
            // 
            // btnDeleteFilter
            // 
            this.btnDeleteFilter.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnDeleteFilter, "btnDeleteFilter");
            this.btnDeleteFilter.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnDeleteFilter.Name = "btnDeleteFilter";
            this.btnDeleteFilter.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnDeleteFilter.Click += new System.EventHandler(this.BtnDeleteFilterClick);
            // 
            // lblEnterText
            // 
            resources.ApplyResources(this.lblEnterText, "lblEnterText");
            // 
            // 
            // 
            this.lblEnterText.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblEnterText.Name = "lblEnterText";
            // 
            // cbxShowInPopup
            // 
            resources.ApplyResources(this.cbxShowInPopup, "cbxShowInPopup");
            // 
            // 
            // 
            this.cbxShowInPopup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.cbxShowInPopup.Name = "cbxShowInPopup";
            this.cbxShowInPopup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cbxShowInPopup.CheckedChanged += new System.EventHandler(this.CbxShowInPopupCheckedChanged);
            // 
            // btnClear
            // 
            this.btnClear.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            resources.ApplyResources(this.btnClear, "btnClear");
            this.btnClear.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClear.Name = "btnClear";
            this.btnClear.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClear.Click += new System.EventHandler(this.BtnClearClick);
            // 
            // filterExprEdit1
            // 
            resources.ApplyResources(this.filterExprEdit1, "filterExprEdit1");
            this.filterExprEdit1.Name = "filterExprEdit1";
            // 
            // CustomFilterEx
            // 
            this.AcceptButton = this.btnApply;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.btnClear);
            this.Controls.Add(this.filterExprEdit1);
            this.Controls.Add(this.tbxDesc);
            this.Controls.Add(this.lblEnterText);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.cbxShowInPopup);
            this.Controls.Add(this.btnOk);
            this.Controls.Add(this.btnNewFilter);
            this.Controls.Add(this.btnReset);
            this.Controls.Add(this.lblFilterName);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.cbFilterName);
            this.Controls.Add(this.lblDesc);
            this.Controls.Add(this.btnDeleteFilter);
            this.DoubleBuffered = true;
            this.EnableGlass = false;
            this.HelpButton = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CustomFilterEx";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.TitleText = "Custom Filter";
            this.HelpButtonClicked += new System.ComponentModel.CancelEventHandler(this.CustomFilterHelpButtonClicked);
            this.Shown += new System.EventHandler(this.CustomFilterShown);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.ButtonX btnOk;
        private ButtonX btnApply;
        private ButtonX btnReset;
        private LabelX lblFilterName;
        private DevComponents.DotNetBar.Controls.TextBoxX tbxDesc;
        private LabelX lblDesc;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cbFilterName;
        private ButtonX btnNewFilter;
        private ButtonX btnDeleteFilter;
        private LabelX lblEnterText;
        private DevComponents.DotNetBar.Controls.CheckBoxX cbxShowInPopup;
        private FilterExprEdit filterExprEdit1;
        private ButtonX btnClear;
    }
}