namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(ComboBox)), DisplayName("ComboBox for Input")]
    public class LBInputComboBox : ComboBoxBase, IItemAttributesEx, IItemAttributes
    {
        private string _TableFile = "";
        private string[] AryUndotext = new string[2];
        private bool BindFlg;
        private bool CheckSelectNextCtl;
        private IContainer components;
        private IntPtr hEdit;
        private StrFunc SF = StrFunc.CreateInstance();
        private int stat_flag = 1;
        private const string UseCharset = "UTF-8";

        public LBInputComboBox()
        {
            base.DropDownStyle = ComboBoxStyle.DropDown;
            base.SelectedIndexChanged += new EventHandler(this.LBInputComboBox_SelectedIndexChanged);
            base.KeyDown += new KeyEventHandler(this.LBInputComboBox_KeyDown);
            base.KeyPress += new KeyPressEventHandler(this.LBInputComboBox_KeyPress);
            base.TextChanged += new EventHandler(this.LBInputComboBox_TextChanged);
            base.BindingContextChanged += new EventHandler(this.LBInputComboBox_BindingContextChanged);
            base.FormattingEnabled = true;
            this.DoubleBuffered = true;
        }

        private void BindingManagerBase_CurrentChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(base.form) && (this.Text.Trim().Length == 0))
            {
                this.Text = base.form;
                base.DataBindings["Text"].WriteValue();
            }
        }

        public void Copy()
        {
            SendMessage(this.hEdit, DesignControls.WM_COPY, 0, 0);
        }

        public void Cut()
        {
            SendMessage(this.hEdit, DesignControls.WM_CUT, 0, 0);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        [DllImport("user32")]
        private static extern bool GetComboBoxInfo(IntPtr hwndCombo, ref ComboBoxInfo info);
        private void LBInputComboBox_BindingContextChanged(object sender, EventArgs e)
        {
            if ((base.DataBindings.Count > 0) && (base.DataBindings[0].BindingManagerBase != null))
            {
                if (!this.BindFlg)
                {
                    base.DataBindings[0].BindingManagerBase.CurrentChanged += new EventHandler(this.BindingManagerBase_CurrentChanged);
                    this.BindFlg = true;
                }
                this.BindingManagerBase_CurrentChanged(null, null);
            }
        }

        private void LBInputComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool foward = e.Modifiers != Keys.Shift;
                ControlFunc.SelectNextControlEx(this, foward);
            }
        }

        private void LBInputComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if ((((e.KeyChar != '\b') && (e.KeyChar != '\b')) && ((e.KeyChar != '\x0016') && (e.KeyChar != '\x0003'))) && (((e.KeyChar != '\x0018') && (e.KeyChar != '\x001a')) && (e.KeyChar != '\x0001')))
            {
                string attribute = base.attribute;
                if (attribute != null)
                {
                    if (!(attribute == "I") && !(attribute == "F"))
                    {
                        if (attribute == "A")
                        {
                            e.KeyChar = char.ToUpper(e.KeyChar);
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
                        string str2 = this.Text.Remove(0, base.SelectionStart + base.SelectionLength);
                        if (base.attribute == "F")
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
                if (!e.Handled)
                {
                    this.CheckSelectNextCtl = true;
                }
            }
        }

        private void LBInputComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (base.DataBindings.Count > 0)
            {
                this.SetBackColor();
                base.DataBindings["Text"].WriteValue();
            }
        }

        private void LBInputComboBox_TextChanged(object sender, EventArgs e)
        {
            this.SetBackColor();
            this.Text = this.SF.CutByteLength(this.Text, base.figure);
            if (this.CheckSelectNextCtl)
            {
                if ((this.Text != null) && (this.SF.GetByteLength(this.Text) == base.figure))
                {
                    ControlFunc.SelectNextControlEx(this, true);
                }
                this.CheckSelectNextCtl = false;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            ComboBoxInfo info=new ComboBoxInfo();
            base.OnHandleCreated(e);
            info = new ComboBoxInfo();
            info.cbSize = Marshal.SizeOf(info);
            if (GetComboBoxInfo(base.Handle, ref info))
            {
                this.hEdit = info.hwndEdit;
            }
        }

        public void Paste()
        {
            IDataObject dataObject = Clipboard.GetDataObject();
            if (dataObject.GetDataPresent(DataFormats.UnicodeText))
            {
                string data = (string) dataObject.GetData(DataFormats.UnicodeText);
                if (base.attribute == "A")
                {
                    data = data.ToUpper();
                    if (!this.SF.ForbiddenCharCheck(data))
                    {
                        return;
                    }
                }
                else if ((base.attribute == "W") && !this.SF.ForbiddenConvCheck(data))
                {
                    return;
                }
                string st = this.Text.Substring(0, base.SelectionStart);
                string str3 = this.Text.Remove(0, base.SelectionStart + base.SelectionLength);
                int byteLength = this.SF.GetByteLength(st);
                int num2 = this.SF.GetByteLength(data);
                int num3 = this.SF.GetByteLength(str3);
                while (((byteLength + num2) + num3) > base.figure)
                {
                    data = data.Remove(data.Length - 1);
                    num2 = this.SF.GetByteLength(data);
                }
                if (base.attribute == "I")
                {
                    if (!this.SF.IsIntegralNum(st + data + str3))
                    {
                        return;
                    }
                }
                else if ((base.attribute == "F") && !this.SF.IsNumeric(st + data + str3))
                {
                    return;
                }
                int selectionStart = base.SelectionStart;
                this.Text = st + data + str3;
                base.SelectionStart = selectionStart + data.Length;
            }
        }

        public bool ReadTableFile(string sDir)
        {
            if (!string.IsNullOrEmpty(this.TableFile))
            {
                try
                {
                    if (File.Exists(sDir + this.TableFile))
                    {
                        StreamReader reader = new StreamReader(sDir + this.TableFile, Encoding.GetEncoding("UTF-8"));
                        try
                        {
                            string str;
                            new StringBuilder();
                            new ArrayList();
                            base.dropDownSource.Clear();
                            base.SetDataSource(null, "", "");
                            while ((str = reader.ReadLine()) != null)
                            {
                                string[] strArray = str.Split(new char[] { ',' });
                                if ((strArray.Length == 3) || (strArray.Length == 2))
                                {
                                    ComboExItem item = new ComboExItem(this.SF.CutByteLength(strArray[0], base.figure), strArray[1]);
                                    base.dropDownSource.Add(item);
                                    if ((strArray.Length == 3) && (strArray[2] == "*"))
                                    {
                                        base.form = this.SF.CutByteLength(strArray[0], base.figure);
                                    }
                                }
                            }
                            base.SetDataSource(base.dropDownSource, "Data", "Data");
                            base.setDropDownWidth();
                            if (base.dropDownSource.Count > 0)
                            {
                                return true;
                            }
                        }
                        finally
                        {
                            reader.Close();
                        }
                    }
                }
                catch
                {
                    base.dropDownSource.Clear();
                    base.DropDownWidth = base.Width;
                    return false;
                }
            }
            return false;
        }

        [DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hwnd, int wMsg, int wParam, int lParam);
        public override void SetBackColor()
        {
            if (((base.DataBindings.Count > 0) && (base.JobErr != null)) && base.JobErr.CheckErrInfo(base.id, base.CurrentPage, this.Text))
            {
                this.BackColor = DesignControls.ErrorColor;
            }
            else if ((base.required == "M") && (this.Text.Length == 0))
            {
                this.BackColor = DesignControls.MandatoryBackColor;
            }
            else
            {
                this.BackColor = DesignControls.NormalBackColor;
            }
        }

        public void undo()
        {
            if (base.input_output != "-")
            {
                if (this.stat_flag == 1)
                {
                    this.AryUndotext[0] = this.Text.ToString();
                    this.Text = this.AryUndotext[1];
                    this.stat_flag = 0;
                }
                else if (this.stat_flag == 0)
                {
                    this.AryUndotext[1] = this.Text.ToString();
                    this.Text = this.AryUndotext[0];
                    this.stat_flag = 1;
                }
                base.SelectAll();
            }
        }

        [Localizable(true), Browsable(true), Description("Enter the ComboBox value setup file used for drop down setup "), Category("Service")]
        public string TableFile
        {
            get
            {
                return this._TableFile;
            }
            set
            {
                this._TableFile = value;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ComboBoxInfo
        {
            public int cbSize;
            public LBInputComboBox.RECT rcItem;
            public LBInputComboBox.RECT rcButton;
            public IntPtr stateButton;
            public IntPtr hwndCombo;
            public IntPtr hwndEdit;
            public IntPtr hwndList;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct RECT
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
        }
    }
}

