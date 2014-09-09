namespace Naccs.Core.BatchDoc
{
    using Naccs.Core.Classes;
    using Naccs.Core.Properties;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Text;
    using System.Windows.Forms;

    public class ConditionChange : Form
    {
        private Button btCancel;
        private Button btOK;
        private ComboBox cmOutCode;
        private IContainer components;
        private Label lbGuide;
        private Label lbOutCode;
        private const int OutcodeLength = 6;

        public ConditionChange()
        {
            this.InitializeComponent();
        }

        private void btOK_Click(object sender, EventArgs e)
        {
            base.DialogResult = DialogResult.None;
            string outcode = this.Outcode;
            string str2 = null;
            if (Encoding.GetEncoding("UTF-8").GetByteCount(outcode) != 6)
            {
                str2 = Resources.ResourceManager.GetString("CORE01");
            }
            else if (!this.IsNumOrAlfaData(outcode))
            {
                if (this.Outcode.IndexOf('_') >= 0)
                {
                    str2 = Resources.ResourceManager.GetString("CORE02");
                }
                else
                {
                    str2 = Resources.ResourceManager.GetString("CORE03");
                }
            }
            if (string.IsNullOrEmpty(str2))
            {
                base.DialogResult = DialogResult.OK;
            }
            else
            {
                using (MessageDialogSimpleForm form = new MessageDialogSimpleForm())
                {
                    form.ShowMessage(ButtonPatern.OK_ONLY, MessageKind.Warning, str2);
                }
                this.cmOutCode.Focus();
            }
        }

        private void cmOutCode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (char.IsControl(e.KeyChar))
            {
                if (e.KeyChar == '\x0016')
                {
                    if (Clipboard.GetDataObject().GetDataPresent(DataFormats.UnicodeText))
                    {
                        string data = (string) Clipboard.GetDataObject().GetData(DataFormats.UnicodeText);
                        if (this.IsNumOrAlfaData(data))
                        {
                            data = data.ToUpper();
                            ComboBox box = (ComboBox) sender;
                            int selectionStart = box.SelectionStart;
                            string text = box.Text;
                            if (box.SelectionLength > 0)
                            {
                                text = text.Remove(selectionStart, box.SelectionLength);
                            }
                            text = text.Insert(box.SelectionStart, data);
                            box.Text = text;
                            box.SelectionStart = selectionStart + data.Length;
                            box.SelectionLength = 0;
                        }
                    }
                    e.Handled = true;
                }
            }
            else
            {
                char ch = char.ToUpper(e.KeyChar);
                if (this.IsNumOrAlfa(ch))
                {
                    e.KeyChar = ch;
                }
                else
                {
                    e.Handled = true;
                }
            }
        }

        private void ConditionChange_Load(object sender, EventArgs e)
        {
            this.cmOutCode.AutoCompleteCustomSource.Clear();
            OutcodeTbl tbl = new OutcodeTbl();
            for (int i = 0; i < tbl.OutcodeList.Count; i++)
            {
                string item = tbl.OutcodeList[i].OutCode + " " + tbl.OutcodeList[i].DocName;
                this.cmOutCode.Items.Add(item);
            }
            this.cmOutCode.Visible = true;
            this.SetDropDownWidth();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.cmOutCode = new ComboBox();
            this.lbOutCode = new Label();
            this.btOK = new Button();
            this.btCancel = new Button();
            this.lbGuide = new Label();
            base.SuspendLayout();
//            this.cmOutCode.DropDownWidth = 280;
            this.cmOutCode.Location = new Point(12, 0x1c);
            this.cmOutCode.MaxDropDownItems = 20;
            this.cmOutCode.Name = "cmOutCode";
            this.cmOutCode.Size = new Size(0x147, 20);
            this.cmOutCode.TabIndex = 0;
            this.cmOutCode.KeyPress += new KeyPressEventHandler(this.cmOutCode_KeyPress);
            this.lbOutCode.AutoSize = true;
            this.lbOutCode.Location = new Point(10, 9);
            this.lbOutCode.Name = "lbOutCode";
            this.lbOutCode.Size = new Size(0xe8, 12);
            this.lbOutCode.TabIndex = 1;
            this.lbOutCode.Text = "取り出したい出力情報コードを入力してください。";
//            this.btOK.DialogResult = DialogResult.OK;
            this.btOK.Location = new Point(0x43, 0x60);
            this.btOK.Name = "btOK";
            this.btOK.Size = new Size(0x4b, 0x17);
            this.btOK.TabIndex = 2;
            this.btOK.Text = "OK";
            this.btOK.UseVisualStyleBackColor = true;
            this.btOK.Click += new EventHandler(this.btOK_Click);
//            this.btCancel.DialogResult = DialogResult.Cancel;
            this.btCancel.Location = new Point(0xd5, 0x60);
            this.btCancel.Name = "btCancel";
            this.btCancel.Size = new Size(0x4b, 0x17);
            this.btCancel.TabIndex = 3;
            this.btCancel.Text = "キャンセル";
            this.btCancel.UseVisualStyleBackColor = true;
            this.lbGuide.Location = new Point(0x19, 0x3b);
            this.lbGuide.Name = "lbGuide";
            this.lbGuide.Size = new Size(0x12b, 0x22);
            this.lbGuide.TabIndex = 4;
            this.lbGuide.Text = "※選択した出力情報コードに[ __ ]が含まれている場合、\r\n    [ __ ]部分に官署コードを入力してください。";
            base.AcceptButton = this.btOK;
            base.AutoScaleDimensions = new SizeF(6f, 12f);
//            base.AutoScaleMode = AutoScaleMode.Font;
            base.CancelButton = this.btCancel;
            base.ClientSize = new Size(0x16b, 0x83);
            base.ControlBox = false;
            base.Controls.Add(this.lbGuide);
            base.Controls.Add(this.btCancel);
            base.Controls.Add(this.btOK);
            base.Controls.Add(this.lbOutCode);
            base.Controls.Add(this.cmOutCode);
//            base.FormBorderStyle = FormBorderStyle.FixedDialog;
            base.MaximizeBox = false;
            base.MinimizeBox = false;
            base.Name = "ConditionChange";
            base.ShowIcon = false;
            base.StartPosition = FormStartPosition.CenterParent;
            this.Text = "条件変更";
            base.Load += new EventHandler(this.ConditionChange_Load);
            base.ResumeLayout(false);
            base.PerformLayout();
        }

        private bool IsNumOrAlfa(char ch)
        {
            if ((((ch < '0') || (ch > '9')) && ((ch < 'A') || (ch > 'Z'))) && (((ch < 'a') || (ch > 'z')) && (ch != ' ')))
            {
                return false;
            }
            return true;
        }

        private bool IsNumOrAlfaData(string data)
        {
            char[] chArray = data.ToCharArray();
            for (int i = 0; i < chArray.Length; i++)
            {
                if (chArray[i] == ' ')
                {
                    return false;
                }
                if (!this.IsNumOrAlfa(chArray[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private void SetDropDownWidth()
        {
            Graphics graphics = base.CreateGraphics();
            float num = 0f;
            for (int i = 0; i < this.cmOutCode.Items.Count; i++)
            {
                num = Math.Max(num, graphics.MeasureString(this.cmOutCode.Items[i].ToString(), this.cmOutCode.Font).Width);
            }
            int num3 = (int) decimal.Round((decimal) num, 0);
            num3 += 30;
            this.cmOutCode.DropDownWidth = Math.Max(base.Width, num3);
            graphics.Dispose();
        }

        public string Outcode
        {
            get
            {
                string text = this.cmOutCode.Text;
                if (text.Length > 6)
                {
                    text = text.Substring(0, 6);
                }
                if (text.Length > 0)
                {
                    return text.TrimEnd(new char[0]);
                }
                return "";
            }
            set
            {
                if (!string.IsNullOrEmpty(value))
                {
                    this.cmOutCode.Text = value;
                }
            }
        }
    }
}

