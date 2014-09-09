namespace DevComponents.DotNetBar
{
    partial class TestControl
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
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.comboTree1 = new DevComponents.DotNetBar.Controls.ComboTree();
            this.columnHeader1 = new DevComponents.AdvTree.ColumnHeader();
            this.columnHeader2 = new DevComponents.AdvTree.ColumnHeader();
            this.comboBoxEx1 = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.SuspendLayout();
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(412, 91);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 0;
            // 
            // comboTree1
            // 
            this.comboTree1.BackColor = System.Drawing.SystemColors.Window;
            // 
            // 
            // 
            this.comboTree1.BackgroundStyle.Class = "TextBoxBorder";
            this.comboTree1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.comboTree1.ButtonDropDown.Visible = true;
            this.comboTree1.Columns.Add(this.columnHeader1);
            this.comboTree1.Columns.Add(this.columnHeader2);
            this.comboTree1.DropDownHeight = 300;
            this.comboTree1.DropDownWidth = 300;
            this.comboTree1.LicenseKey = "F962CEC7-CD8F-4911-A9E9-CAB39962FC1F";
            this.comboTree1.Location = new System.Drawing.Point(333, 238);
            this.comboTree1.Name = "comboTree1";
            this.comboTree1.SelectedDisplayMember = "TEN";
            this.comboTree1.SelectionBoxStyle = DevComponents.AdvTree.eSelectionStyle.FullRowSelect;
            this.comboTree1.Size = new System.Drawing.Size(217, 25);
            this.comboTree1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboTree1.TabIndex = 2;
            // 
            // columnHeader1
            // 
            this.columnHeader1.DataFieldName = "MA";
            this.columnHeader1.Name = "columnHeader1";
            this.columnHeader1.Text = "Column";
            this.columnHeader1.Width.Absolute = 150;
            // 
            // columnHeader2
            // 
            this.columnHeader2.DataFieldName = "TEN";
            this.columnHeader2.Name = "columnHeader2";
            this.columnHeader2.Text = "Columndfd";
            this.columnHeader2.Width.Absolute = 150;
            // 
            // comboBoxEx1
            // 
            this.comboBoxEx1.Col1Width = 50;
            this.comboBoxEx1.Col2Width = 100;
            this.comboBoxEx1.Col3Width = 150;
            this.comboBoxEx1.Col4Width = 150;
            this.comboBoxEx1.DisplayMember = "MA";
            this.comboBoxEx1.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.comboBoxEx1.DropDownColumns = "MA,TEN";
            this.comboBoxEx1.DropDownColumnsHeaders = "mã\r\ntên";
            this.comboBoxEx1.FocusHighlightEnabled = true;
            this.comboBoxEx1.FormattingEnabled = true;
            this.comboBoxEx1.ItemHeight = 14;
            this.comboBoxEx1.Location = new System.Drawing.Point(323, 178);
            this.comboBoxEx1.MoRongKhiForcus = true;
            this.comboBoxEx1.Name = "comboBoxEx1";
            this.comboBoxEx1.Size = new System.Drawing.Size(227, 20);
            this.comboBoxEx1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.comboBoxEx1.TabIndex = 1;
            this.comboBoxEx1.ValueMember = "MA";
            this.comboBoxEx1.TextChanged += new System.EventHandler(this.comboBoxEx1_TextChanged);
            // 
            // TestControl
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1053, 494);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.comboTree1);
            this.Controls.Add(this.comboBoxEx1);
            this.Name = "TestControl";
            this.Text = "TestControl";
            this.Load += new System.EventHandler(this.TestControl_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private Controls.ComboBoxEx comboBoxEx1;
        private Controls.ComboTree comboTree1;
        private AdvTree.ColumnHeader columnHeader1;
        private AdvTree.ColumnHeader columnHeader2;
        private System.Windows.Forms.TextBox textBox1;


    }
}