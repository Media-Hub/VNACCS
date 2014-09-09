namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    [DisplayName("ExTextBox"), ToolboxBitmap(typeof(TextBox))]
    public class ExTextBox : TextBox
    {
        private string _attribute = "";
        protected bool barcodeSend;
        protected bool bSelectionAndKeyEventHandled;
        protected int digit;
        protected int displayWidth;
        protected bool DoBarCodeSend;
        protected bool DoSelectNextControl;
        private const int EM_REPLACESEL = 0xc2;
        private const int EM_SETWORDBREAKPROC = 0xd0;
        private EditWordBreakProc EW;
        private int fFigure;
        protected Naccs.Common.Function.GetLength GetLength;
        private StrFunc SF = StrFunc.CreateInstance();
        protected const string SOFTWARE_NEWLINE = "\r\r\n";

        public ExTextBox()
        {
            base.KeyPress += new KeyPressEventHandler(this.ExTextBox_KeyPress);
            base.KeyDown += new KeyEventHandler(this.ExTextBox_KeyDown);
            this.EW = new EditWordBreakProc(ExTextBox.MyEditWordBreakProc);
            this.DoubleBuffered = true;
            this.GetLength = new Naccs.Common.Function.GetLength(this.SF.GetByteLength);
        }

        private void ExTextBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool foward = e.Modifiers != Keys.Shift;
                ControlFunc.SelectNextControlEx(this, foward);
            }
        }

        protected virtual void ExTextBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            string str3;
            if (((((e.KeyChar != '\b') && (e.KeyChar != '\b')) && ((e.KeyChar != '\x0016') && (e.KeyChar != '\x0003'))) && (((e.KeyChar != '\x0018') && (e.KeyChar != '\x001a')) && (e.KeyChar != '\x0001'))) && ((str3 = this.attribute) != null))
            {
                if (!(str3 == "I") && !(str3 == "F"))
                {
                    if (!(str3 == "A"))
                    {
                        if ((str3 == "W") && !this.SF.ForbiddenConvCheck(e.KeyChar))
                        {
                            e.Handled = true;
                        }
                        return;
                    }
                }
                else
                {
                    string str = this.Text.Substring(0, base.SelectionStart).Replace(Environment.NewLine, "");
                    string str2 = this.Text.Remove(0, base.SelectionStart + this.SelectionLength).Replace(Environment.NewLine, "");
                    if (this.attribute == "F")
                    {
                        if (!this.SF.IsNumeric(str + e.KeyChar + str2))
                        {
                            e.Handled = true;
                            return;
                        }
                        return;
                    }
                    if (!this.SF.IsIntegralNum(str + e.KeyChar + str2))
                    {
                        e.Handled = true;
                    }
                    return;
                }
                if (!this.SF.ForbiddenCharCheck(e.KeyChar, true))
                {
                    e.Handled = true;
                }
            }
        }

        private static int MyEditWordBreakProc(IntPtr lpch, int ichCurrent, int cch, int code)
        {
            return (ichCurrent = 0);
        }

        protected override void OnEnter(EventArgs e)
        {
            base.SelectAll();
            base.OnEnter(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if ((e.Button == MouseButtons.Right) && (((e.X < 0) || (e.Y < 0)) || ((e.X >= base.ClientSize.Width) || (e.Y >= base.ClientSize.Height))))
            {
                base.Capture = false;
            }
            base.OnMouseMove(e);
        }

        [DllImport("user32.dll", CharSet=CharSet.Unicode)]
        private static extern IntPtr SendMessage(IntPtr hWndControl, int msgId, int wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWndControl, int msgId, IntPtr wParam, EditWordBreakProc lParam);
        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWndControl, int msgId, IntPtr wParam, IntPtr lParam);
        public void SetNoWordWrap()
        {
            SendMessage(base.Handle, 0xd0, IntPtr.Zero, this.EW);
        }

        protected override void WndProc(ref Message m)
        {
            string str;
            int num;
            if (m.Msg != DesignControls.WM_PASTE)
            {
                if (m.Msg == 0x102)
                {
                    int num8 = 0;
                    int digit = 0;
                    if (((ushort) ((int) m.WParam)) != 8)
                    {
                        digit = this.figure;
                        if ("W".Equals(this.attribute))
                        {
                            digit = this.digit;
                        }
                        if (this.SelectionLength == 0)
                        {
                            string str7 = null;
                            if (((ushort) ((int) m.WParam)) == 13)
                            {
                                str7 = this.Text + Environment.NewLine;
                            }
                            else
                            {
                                str7 = this.Text + ((char) ((int) m.WParam));
                            }
                            str7 = str7.Replace("\r\r\n", "");
                            num8 = this.GetLength(str7);
                        }
                        else
                        {
                            string str8 = this.Text.Substring(0, base.SelectionStart).Replace("\r\r\n", "");
                            string str9 = this.Text.Remove(0, base.SelectionStart + this.SelectionLength).Replace("\r\r\n", "");
                            if (((ushort) ((int) m.WParam)) == 13)
                            {
                                num8 = this.GetLength(str8 + str9 + Environment.NewLine);
                            }
                            else
                            {
                                num8 = this.GetLength(str8 + str9 + ((char) ((int) m.WParam)));
                            }
                        }
                        if (num8 > digit)
                        {
                            return;
                        }
                        if (num8 == digit)
                        {
                            this.DoSelectNextControl = true;
                            if (this.barcodeSend)
                            {
                                this.DoBarCodeSend = true;
                            }
                        }
                    }
                }
                string selectedText = null;
                if (m.Msg == DesignControls.WM_COPY)
                {
                    selectedText = this.SelectedText;
                }
                base.WndProc(ref m);
                if (m.Msg == DesignControls.WM_COPY)
                {
                    IDataObject dataObject = Clipboard.GetDataObject();
                    if (dataObject.GetDataPresent(DataFormats.UnicodeText))
                    {
                        string data = (string) dataObject.GetData(DataFormats.UnicodeText);
                        if (data == selectedText)
                        {
                            Clipboard.SetDataObject(data.Replace("\r\r\n", ""));
                        }
                    }
                }
                return;
            }
            if (!base.ReadOnly)
            {
                IDataObject obj2 = Clipboard.GetDataObject();
                if (!obj2.GetDataPresent(DataFormats.UnicodeText))
                {
                    return;
                }
                str = (string) obj2.GetData(DataFormats.UnicodeText);
                if (string.IsNullOrEmpty(str))
                {
                    return;
                }
                string str2 = str;
                string attribute = this.attribute;
                if (attribute == null)
                {
                    goto Label_0096;
                }
                if (!(attribute == "A"))
                {
                    if ((attribute == "W") && !this.SF.ForbiddenConvCheck(str2))
                    {
                        return;
                    }
                    goto Label_0096;
                }
                str2 = str2.ToUpper();
                if (this.SF.ForbiddenCharCheck(str2))
                {
                    goto Label_0096;
                }
            }
            return;
        Label_0096:
            num = base.SelectionStart;
            string s = this.Text.Substring(0, num).Replace("\r\r\n", "");
            int num2 = this.GetLength(s);
            this.SelectedText.Replace("\r\r\n", "");
            string str4 = this.Text.Substring(base.SelectionStart + this.SelectionLength, (this.Text.Length - base.SelectionStart) - this.SelectionLength).Replace("\r\r\n", "");
            int num3 = this.GetLength(str4);
            int figure = this.figure;
            if ("W".Equals(this.attribute))
            {
                figure = this.digit;
            }
            if ((num2 + num3) < figure)
            {
                for (int i = this.GetLength(str); ((num2 + i) + num3) > figure; i = this.GetLength(str))
                {
                    str = str.Remove(str.Length - 1);
                    if (str.EndsWith("\r"))
                    {
                        str = str.Remove(str.Length - 1);
                    }
                }
                if (this.attribute == "F")
                {
                    if (!this.SF.IsNumeric(s + str + str4))
                    {
                        return;
                    }
                }
                else if ((this.attribute == "I") && !this.SF.IsIntegralNum(s + str + str4))
                {
                    return;
                }
                int lineFromCharIndex = this.GetLineFromCharIndex(num);
                int firstCharIndexFromLine = base.GetFirstCharIndexFromLine(lineFromCharIndex);
                string str5 = this.Text.Substring(firstCharIndexFromLine, num - firstCharIndexFromLine);
                bool flag = false;
                StringBuilder builder = new StringBuilder(str5);
                foreach (char ch in str)
                {
                    if (this.Multiline)
                    {
                        if (flag)
                        {
                            builder.Remove(0, builder.Length);
                            flag = false;
                        }
                        else if (ComponentProperty.IsNewLineNeeded(builder.ToString() + ch.ToString(), this))
                        {
                            num += "\r\r\n".Length;
                            builder.Remove(0, builder.Length);
                        }
                    }
                    builder.Append(ch);
                    num++;
                    if (ch == '\n')
                    {
                        flag = true;
                    }
                }
                this.bSelectionAndKeyEventHandled = true;
                string str6 = s + str + str4;
                base.SelectAll();
                IntPtr lParam = Marshal.StringToHGlobalUni(str6);
                SendMessage(base.Handle, 0xc2, 1, lParam);
                Marshal.FreeHGlobal(lParam);
                base.SelectionStart = num;
            }
        }

        [Browsable(true), Localizable(true), Category("Service"), Description("Attribute")]
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
                    this.GetLength = new Naccs.Common.Function.GetLength(this.SF.GetStringLength);
                }
                else
                {
                    base.CharacterCasing = CharacterCasing.Upper;
                }
            }
        }

        public int DisplayWidth
        {
            get
            {
                return this.displayWidth;
            }
            set
            {
                this.displayWidth = value;
            }
        }

        [Description("Digit number"), Category("Service"), Browsable(true), Localizable(true)]
        public int figure
        {
            get
            {
                return this.fFigure;
            }
            set
            {
                this.fFigure = value;
                if (value > 0)
                {
                    this.digit = value / 3;
                }
            }
        }

        private delegate int EditWordBreakProc(IntPtr lpch, int ichCurrent, int cch, int code);
    }
}

