namespace DevComponents.DotNetBar.SuperGrid
{
    partial class FilterExprEdit
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FilterExprEdit));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtbOutput = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.rtbInput = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.rtbOutput, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rtbInput, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(398, 92);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // rtbOutput
            // 
            // 
            // 
            // 
            this.rtbOutput.BackgroundStyle.BackColor = System.Drawing.SystemColors.Control;
            this.rtbOutput.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtbOutput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtbOutput.BackgroundStyle.PaddingBottom = 4;
            this.rtbOutput.BackgroundStyle.PaddingLeft = 4;
            this.rtbOutput.BackgroundStyle.PaddingRight = 4;
            this.rtbOutput.BackgroundStyle.PaddingTop = 4;
            this.rtbOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbOutput.Location = new System.Drawing.Point(3, 49);
            this.rtbOutput.Name = "rtbOutput";
            this.rtbOutput.ReadOnly = true;
            this.rtbOutput.Size = new System.Drawing.Size(392, 40);
            this.rtbOutput.TabIndex = 2;
            // 
            // rtbInput
            // 
            this.rtbInput.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("rtbInput.BackgroundImage")));
            // 
            // 
            // 
            this.rtbInput.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtbInput.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtbInput.BackgroundStyle.PaddingBottom = 4;
            this.rtbInput.BackgroundStyle.PaddingLeft = 4;
            this.rtbInput.BackgroundStyle.PaddingRight = 4;
            this.rtbInput.BackgroundStyle.PaddingTop = 4;
            this.rtbInput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtbInput.Location = new System.Drawing.Point(3, 3);
            this.rtbInput.Name = "rtbInput";
            this.rtbInput.Size = new System.Drawing.Size(392, 40);
            this.rtbInput.TabIndex = 1;
            this.rtbInput.TextChanged += new System.EventHandler(this.RichTextBoxEx1TextChanged);
            // 
            // FilterExprEdit
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "FilterExprEdit";
            this.Size = new System.Drawing.Size(398, 92);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtbOutput;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtbInput;
    }
}
