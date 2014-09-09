namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.Windows.Forms;

    public class LBTextBoxEditingControl : DataGridViewTextBoxEditingControl
    {
        private string _attribute = "";
        private string _id = "";
        private bool checkSelectNextCtl;
        private int digit;
        private int fMaxByteLength;
        private StrFunc SF = StrFunc.CreateInstance();

        public LBTextBoxEditingControl()
        {
            base.KeyPress += new KeyPressEventHandler(this.LBTextBoxEditingControl_KeyPress);
            base.TextChanged += new EventHandler(this.LBTextBoxEditingControl_TextChanged);
            base.CharacterCasing = CharacterCasing.Upper;
        }

        private void LBTextBoxEditingControl_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((((e.KeyChar != '\b') && (e.KeyChar != '\b')) && ((e.KeyChar != '\x0016') && (e.KeyChar != '\x0003'))) && (((e.KeyChar != '\x0018') && (e.KeyChar != '\x001a')) && (e.KeyChar != '\x0001')))
            {
                string attribute = this.attribute;
                if (attribute != null)
                {
                    if (!(attribute == "I") && !(attribute == "F"))
                    {
                        if (attribute == "A")
                        {
                            if (!this.SF.ForbiddenCharCheck(e.KeyChar, true))
                            {
                                e.Handled = true;
                            }
                        }
                        else if ((attribute == "W") && !this.SF.ForbiddenConvCheck(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                    }
                    else
                    {
                        string str = this.Text.Substring(0, base.SelectionStart);
                        string str2 = this.Text.Remove(0, base.SelectionStart + this.SelectionLength);
                        if (this.attribute == "F")
                        {
                            if (!this.SF.IsNumeric(str + e.KeyChar + str2))
                            {
                                e.Handled = true;
                            }
                        }
                        else if (!this.SF.IsIntegralNum(str + e.KeyChar + str2))
                        {
                            e.Handled = true;
                        }
                    }
                }
                this.checkSelectNextCtl = true;
            }
        }

        private void LBTextBoxEditingControl_TextChanged(object sender, EventArgs e)
        {
            if ("W".Equals(this.attribute))
            {
                this.Text = this.SF.CutStringLength(this.Text, this.digit);
            }
            else
            {
                this.Text = this.SF.CutByteLength(this.Text, this.MaxByteLength);
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (((e.X < 0) || (e.Y < 0)) || ((e.X >= base.ClientSize.Width) || (e.Y >= base.ClientSize.Height))))
            {
                base.Capture = false;
            }
            base.OnMouseMove(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == DesignControls.WM_PASTE)
            {
                IDataObject dataObject = Clipboard.GetDataObject();
                if (dataObject.GetDataPresent(DataFormats.UnicodeText))
                {
                    string data = (string) dataObject.GetData(DataFormats.UnicodeText);
                    if (this.attribute == "A")
                    {
                        data = data.ToUpper();
                        if (!this.SF.ForbiddenCharCheck(data))
                        {
                            return;
                        }
                    }
                    else if (this.attribute == "W")
                    {
                        if (!this.SF.ForbiddenConvCheck(data))
                        {
                            return;
                        }
                    }
                    else
                    {
                        string st = this.Text.Substring(0, base.SelectionStart);
                        string str3 = this.Text.Remove(0, base.SelectionStart + this.SelectionLength);
                        int byteLength = this.SF.GetByteLength(st);
                        int num2 = this.SF.GetByteLength(data);
                        int num3 = this.SF.GetByteLength(str3);
                        while (((byteLength + num2) + num3) > this.MaxByteLength)
                        {
                            data = data.Remove(data.Length - 1);
                            num2 = this.SF.GetByteLength(data);
                        }
                        if (this.attribute == "I")
                        {
                            if (!this.SF.IsIntegralNum(st + data + str3))
                            {
                                return;
                            }
                        }
                        else if (!this.SF.IsNumeric(st + data + str3))
                        {
                            return;
                        }
                    }
                }
            }
            base.WndProc(ref m);
        }

        public string attribute
        {
            get
            {
                return this._attribute;
            }
            set
            {
                this._attribute = value;
                if (value == "W")
                {
                    base.CharacterCasing = CharacterCasing.Normal;
                }
                else
                {
                    base.CharacterCasing = CharacterCasing.Upper;
                }
            }
        }

        public bool CheckSelectNextCtl
        {
            get
            {
                return this.checkSelectNextCtl;
            }
            set
            {
                this.checkSelectNextCtl = value;
            }
        }

        public int Digit
        {
            get
            {
                return this.digit;
            }
        }

        public string id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        public int MaxByteLength
        {
            get
            {
                return this.fMaxByteLength;
            }
            set
            {
                this.fMaxByteLength = value;
                this.MaxLength = value;
                if (value > 0)
                {
                    this.digit = value / 3;
                }
            }
        }
    }
}

