namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Design;
    using System.IO;
    using System.Windows.Forms;

    [ToolboxBitmap(typeof(ComboBox)), DisplayName("ComboBox")]
    public class LBComboBox : ComboBoxBase, IItemAttributesEx, IItemAttributes
    {
        private IContainer components;
        private bool flgDropDown;
        private string SearchText = "";

        public LBComboBox()
        {
            base.DropDownStyle = ComboBoxStyle.DropDownList;
            base.SelectedIndexChanged += new EventHandler(this.LBComboBox_SelectedIndexChanged);
            base.DropDown += new EventHandler(this.LBComboBox_DropDown);
            base.DropDownClosed += new EventHandler(this.LBComboBox_DropDownClosed);
            base.KeyPress += new KeyPressEventHandler(this.LBComboBox_KeyPress);
            base.KeyDown += new KeyEventHandler(this.LBComboBox_KeyDown);
            base.GotFocus += new EventHandler(this.LBComboBox_GotFocus);
            this.DoubleBuffered = true;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.components != null))
            {
                this.components.Dispose();
            }
            base.Dispose(disposing);
        }

        private void LBComboBox_DropDown(object sender, EventArgs e)
        {
            this.flgDropDown = true;
        }

        private void LBComboBox_DropDownClosed(object sender, EventArgs e)
        {
            this.flgDropDown = false;
            this.SetBackColor();
        }

        private void LBComboBox_GotFocus(object sender, EventArgs e)
        {
            this.SearchText = "";
        }

        private void LBComboBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                bool foward = e.Modifiers != Keys.Shift;
                ControlFunc.SelectNextControlEx(this, foward);
            }
            if (e.KeyCode == Keys.Delete)
            {
                this.SearchItems(' ');
                e.Handled = true;
            }
        }

        private void LBComboBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (this.SearchItems(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void LBComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (base.DataBindings.Count > 0)
            {
                this.SetBackColor();
                base.DataBindings["SelectedValue"].WriteValue();
            }
        }

        private bool SearchItems(char C)
        {
            bool flag = false;
            for (int i = 0; i < base.dropDownSource.Count; i++)
            {
                string data = ((ComboExItem) base.dropDownSource[i]).Data;
                if (data == "")
                {
                    if ((C != ' ') && (C != '\b'))
                    {
                        continue;
                    }
                    this.SelectedIndex = i;
                    flag = true;
                    this.SearchText = "";
                    break;
                }
                string str2 = C.ToString().ToUpper();
                string str3 = this.SearchText + str2;
                if (data.StartsWith(str3))
                {
                    this.SelectedIndex = i;
                    flag = true;
                    if (data == str3)
                    {
                        this.SearchText = "";
                    }
                    else
                    {
                        this.SearchText = str3;
                    }
                    break;
                }
            }
            if (!flag)
            {
                this.SearchText = "";
            }
            return flag;
        }

        public override void SetBackColor()
        {
            if (!this.flgDropDown)
            {
                if ((base.DataBindings.Count > 0) && (base.JobErr != null))
                {
                    string selectedValue = (string) base.SelectedValue;
                    if (selectedValue == null)
                    {
                        selectedValue = "";
                    }
                    if (base.JobErr.CheckErrInfo(base.id, base.CurrentPage, selectedValue))
                    {
                        this.BackColor = DesignControls.ErrorColor;
                        return;
                    }
                }
                if (this.SelectedIndex > -1)
                {
                    this.BackColor = DesignControls.NormalBackColor;
                }
                else if (base.required == "M")
                {
                    this.BackColor = DesignControls.MandatoryBackColor;
                }
                else
                {
                    this.BackColor = DesignControls.NormalBackColor;
                }
            }
        }

        [Category("Service"), Description("Input the data of message or the contents of pull-down display delimited by thousand separator. \nFormat：\n\t[Code],[Displayed contents]"), Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor)), Localizable(true)]
        public string choice_keyvalue
        {
            get
            {
                StringWriter writer = new StringWriter();
                foreach (ComboExItem item in base.dropDownSource)
                {
                    writer.WriteLine(item.ToString());
                }
                return writer.ToString();
            }
            set
            {
                if ((value == null) || (value.Length == 0))
                {
                    base.dropDownSource.Clear();
                    base.DropDownWidth = base.Width;
                }
                else
                {
                    string str;
                    StringReader reader = new StringReader(value);
                    ArrayList list = new ArrayList();
                    while ((str = reader.ReadLine()) != null)
                    {
                        foreach (string str2 in str.Split(new char[] { ',' }))
                        {
                            list.Add(str2);
                        }
                    }
                    base.dropDownSource.Clear();
                    for (int i = 0; (i + 1) < list.Count; i += 2)
                    {
                        ComboExItem item = new ComboExItem((string) list[i], (string) list[i + 1]);
                        base.dropDownSource.Add(item);
                    }
                    base.setDropDownWidth();
                }
            }
        }
    }
}

