namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Xml;

    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    internal class LBRanDesigner : ParentControlDesigner
    {
        private string getAttribute(XmlNode node, string attrName)
        {
            if (node.Attributes[attrName] != null)
            {
                return node.Attributes[attrName].Value;
            }
            return "";
        }

        protected override void OnDragDrop(DragEventArgs de)
        {
            if (de.Effect == DragDropEffects.Link)
            {
                IDesignerHost host = (IDesignerHost) this.GetService(typeof(IDesignerHost));
                IComponentChangeService service = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.Control);
                ListView.SelectedListViewItemCollection data = de.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;
                if (data == null)
                {
                    return;
                }
                DesignerTransaction transaction = host.CreateTransaction("Drug and Drop from Item Information)");
                using (transaction)
                {
                    Point point = this.Control.PointToClient(new Point(de.X, de.Y));
                    CharsWidth width = new CharsWidth("Courier New, 11pt");
                    int num = 0;
                    bool flag = false;
                    foreach (ListViewItem item in data)
                    {
                        XmlNode tag = item.Tag as XmlNode;
                        if ((item.Group.Name == "ran") && (tag != null))
                        {
                            Control o = (Control) host.CreateComponent(DesignControls.Label);
                            o.Font = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                            this.setXmlAttributeToProperty(tag, o, point.X, point.Y + num, this.getAttribute(tag, "name"));
                            this.Control.Controls.Add(o);
                            LBPrtTextBox box = (LBPrtTextBox) host.CreateComponent(DesignControls.PrtTextBox);
                            box.Font = new Font("Courier New", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                            this.setXmlAttributeToProperty(tag, box, (point.X + o.Width) + 2, (point.Y + num) + 2, null);
                            box.BorderStyle = BorderStyle.None;
                            box.Width = width.getWidth(box.figure, box.attribute);
                            this.Control.Controls.Add(box);
                            flag = true;
                            num += box.Height + 10;
                        }
                    }
                    if (flag)
                    {
                        service.OnComponentChanged(this.Control, properties["Controls"], this.Control.Controls, this.Control.Controls);
                        transaction.Commit();
                    }
                    width.disposeGraphics();
                    return;
                }
            }
            base.OnDragDrop(de);
        }

        protected override void OnDragEnter(DragEventArgs de)
        {
            de.Effect = DragDropEffects.Link;
        }

        private void setXmlAttributeToProperty(XmlNode node, object o, int X, int Y, string text)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(o);
            foreach (XmlAttribute attribute in node.Attributes)
            {
                PropertyDescriptor descriptor = properties[attribute.Name];
                if (descriptor != null)
                {
                    descriptor.SetValue(o, descriptor.Converter.ConvertFromInvariantString(attribute.Value));
                }
            }
            Point point = new Point(X, Y);
            properties["Location"].SetValue(o, point);
            if (text != null)
            {
                properties["Text"].SetValue(o, text);
            }
        }
    }
}

