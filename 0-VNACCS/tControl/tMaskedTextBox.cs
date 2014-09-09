using DevComponents.DotNetBar.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using System.Windows.Forms;

namespace DevComponents.DotNetBar
{
    public class tMaskedTextBox:MaskedTextBoxAdv
    {
        public tMaskedTextBox()
        {
            InitializeComponent();
        }

        
        [Bindable(BindableSupport.Yes, BindingDirection.TwoWay)]
        public DateTime? DateValue
        {
            get {
                DateTime _DateValue = default(DateTime);
                if (DateTime.TryParseExact(this.Text, "dd/MM/yyyy", null, DateTimeStyles.None, out _DateValue) == true)
                {
                    return _DateValue;
                }
                else
                {
                    return null;
                }
            }
            set
            {
                DateTime? _DateValue = value;
                if (_DateValue.HasValue == false)
                {
                    this.Text = string.Empty;
                    return;
                }
                this.Text = _DateValue.Value.ToString("dd/MM/yyyy");
            }
        }

        public bool BắtBuộc
        {
            get;
            set;
        }

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // tMaskedTextBox
            // 
            this.AsciiOnly = true;
            // 
            // 
            // 
            this.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.CutCopyMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.FocusHighlightEnabled = true;
            this.InsertKeyMode = System.Windows.Forms.InsertKeyMode.Default;
            this.Mask = "00/00/0000";
            this.Size = new System.Drawing.Size(100, 20);
            this.Text = "  /  /";
            this.TextAlign = System.Windows.Forms.HorizontalAlignment.Left;
            this.TextMaskFormat = System.Windows.Forms.MaskFormat.IncludeLiterals;
            this.MaskedTextBox.Enter += MaskedTextBox_Enter;
            this.MaskedTextBox.MouseClick += new System.Windows.Forms.MouseEventHandler(this.MaskedTextBox_MouseClick);
            this.MaskedTextBox.GotFocus+=MaskedTextBox_GotFocus;
            this.MaskedTextBox.KeyPress += MaskedTextBox_KeyPress;
            this.MaskedTextBox.KeyDown += MaskedTextBox_KeyDown;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        void MaskedTextBox_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Space)
            {
                this.Text = DateTime.Now.ToString("dd/MM/yyyy");
                return;
            }

            if (e.KeyCode == Keys.Up)
            {
                if (this.DateValue.HasValue == false) return;
                this.Text = this.DateValue.Value.AddDays(1).ToString("dd/MM/yyyy");
                return;
            }

            if (e.KeyCode == Keys.Down)
            {
                if (this.DateValue.HasValue == false) return;
                this.Text = this.DateValue.Value.AddDays(-1).ToString("dd/MM/yyyy");
                return;
            }
        }

        void MaskedTextBox_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
            
        }

        void MaskedTextBox_Enter(object sender, EventArgs e)
        {
            this.MaskedTextBox.Select(0, 15);
        }

        void MaskedTextBox_MouseClick(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            //this.MaskedTextBox.Select(0, 15);
        }

        void MaskedTextBox_GotFocus(object sender, EventArgs e)
        {
            this.MaskedTextBox.Select(0, 15);
        }             
    }
}
