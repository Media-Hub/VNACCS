using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar.Design
{
    public class RichTextBoxExDesigner : ControlDesigner
    {
        public override void InitializeNewComponent(System.Collections.IDictionary defaultValues)
        {
            RichTextBoxEx rtb = (RichTextBoxEx)this.Control;
            rtb.BackgroundStyle.Class = ElementStyleClassKeys.RichTextBoxBorderKey;
            base.InitializeNewComponent(defaultValues);
        }
    }
}
