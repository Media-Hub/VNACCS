namespace DevComponents.DotNetBar
{
    partial class tCodeList
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
            this.tLIST = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.tCODE = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // tLIST
            // 
            this.tLIST.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tLIST.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tLIST.BackColor = System.Drawing.SystemColors.Window;
            this.tLIST.Col1Width = 50;
            this.tLIST.Col2Width = 100;
            this.tLIST.Col3Width = 150;
            this.tLIST.Col4Width = 150;
            this.tLIST.DisplayMember = "Text";
            this.tLIST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tLIST.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tLIST.DropDownHeight = 200;
            this.tLIST.DropDownWidth = 270;
            this.tLIST.FocusHighlightEnabled = true;
            this.tLIST.FormattingEnabled = true;
            this.tLIST.IntegralHeight = false;
            this.tLIST.ItemHeight = 14;
            this.tLIST.Location = new System.Drawing.Point(70, 0);
            this.tLIST.MaxDropDownItems = 20;
            this.tLIST.MoRongKhiForcus = true;
            this.tLIST.Name = "tLIST";
            this.tLIST.Size = new System.Drawing.Size(200, 20);
            this.tLIST.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tLIST.TabIndex = 1;
            this.tLIST.WatermarkText = "Tên";
            this.tLIST.SelectedIndexChanged += new System.EventHandler(this.tLIST_SelectedIndexChanged);
            this.tLIST.Enter += new System.EventHandler(this.tLIST_Enter);
            // 
            // tCODE
            // 
            this.tCODE.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.tCODE.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.tCODE.BackColor = System.Drawing.SystemColors.Window;
            this.tCODE.Col1Width = 50;
            this.tCODE.Col2Width = 100;
            this.tCODE.Col3Width = 150;
            this.tCODE.Col4Width = 150;
            this.tCODE.DisplayMember = "Text";
            this.tCODE.Dock = System.Windows.Forms.DockStyle.Left;
            this.tCODE.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.tCODE.DropDownHeight = 200;
            this.tCODE.DropDownWidth = 270;
            this.tCODE.FocusCuesEnabled = false;
            this.tCODE.FocusHighlightEnabled = true;
            this.tCODE.FormattingEnabled = true;
            this.tCODE.IntegralHeight = false;
            this.tCODE.ItemHeight = 14;
            this.tCODE.Location = new System.Drawing.Point(0, 0);
            this.tCODE.MoRongKhiForcus = false;
            this.tCODE.Name = "tCODE";
            this.tCODE.Size = new System.Drawing.Size(70, 20);
            this.tCODE.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.tCODE.TabIndex = 0;
            this.tCODE.WatermarkText = "Mã";
            this.tCODE.SelectedIndexChanged += new System.EventHandler(this.tCODE_SelectedIndexChanged);
            this.tCODE.Enter += new System.EventHandler(this.tCODE_Enter);
            // 
            // tCodeList
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tLIST);
            this.Controls.Add(this.tCODE);
            this.Name = "tCodeList";
            this.Size = new System.Drawing.Size(270, 20);
            this.ResumeLayout(false);

        }

        #endregion

        public Controls.ComboBoxEx tCODE;
        public Controls.ComboBoxEx tLIST;

    }
}
