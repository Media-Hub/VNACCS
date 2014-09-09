namespace Naccs.Common.CustomControls
{
    using Naccs.Common.Function;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.Design;
    using System.Drawing;
    using System.Security.Permissions;
    using System.Windows.Forms;
    using System.Windows.Forms.Design;
    using System.Xml;

    [PermissionSet(SecurityAction.Demand, Name="FullTrust")]
    internal class PrintPageDesigner : DocumentDesigner
    {
        private void findRan(Control control, List<string> list)
        {
            if (control is LBRan)
            {
                list.Add((control as LBRan).repetition_id);
            }
            foreach (Control control2 in control.Controls)
            {
                this.findRan(control2, list);
            }
        }

        private string getAttribute(XmlNode node, string attrName)
        {
            if (node.Attributes[attrName] != null)
            {
                return node.Attributes[attrName].Value;
            }
            return "";
        }

        private Control getRan()
        {
            IDesignerHost service = (IDesignerHost) this.GetService(typeof(IDesignerHost));
            if (service != null)
            {
                foreach (IComponent component in service.Container.Components)
                {
                    IContainerAttributes attributes = component as IContainerAttributes;
                    if ((attributes != null) && (attributes.repetition_id.Length > 0))
                    {
                        return (component as Control);
                    }
                }
            }
            return null;
        }

        protected override void OnDragDrop(DragEventArgs de)
        {
            if (de.Effect == DragDropEffects.Link)
            {
                IDesignerHost host = (IDesignerHost) this.GetService(typeof(IDesignerHost));
                IComponentChangeService service = (IComponentChangeService) this.GetService(typeof(IComponentChangeService));
                PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(this.Control);
                Control control = this.getRan();
                List<string> list = null;
                ListView.SelectedListViewItemCollection data = de.Data.GetData(typeof(ListView.SelectedListViewItemCollection)) as ListView.SelectedListViewItemCollection;
                if (data == null)
                {
                    return;
                }
                DesignerTransaction transaction = host.CreateTransaction("Drug and Drop from Item Information)");
                using (transaction)
                {
                    this.Control.SuspendLayout();
                    Point point = this.Control.PointToClient(new Point(de.X, de.Y));
                    CharsWidth width = new CharsWidth("Courier New, 11pt");
                    int num = 0;
                    int num2 = 2;
                    bool flag = false;
                    foreach (ListViewItem item in data)
                    {
                        if ((item.Group.Name == "com") && (item.Tag is XmlNode))
                        {
                            XmlNode tag = item.Tag as XmlNode;
                            Control o = (Control) host.CreateComponent(DesignControls.Label);
                            o.Font = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                            this.setXmlAttributeToProperty(tag, o, point.X, point.Y + num, this.getAttribute(tag, "name"));
                            this.Control.Controls.Add(o);
                            LBPrtTextBox box = (LBPrtTextBox) host.CreateComponent(DesignControls.PrtTextBox);
                            box.Font = new Font("Courier New", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                            this.setXmlAttributeToProperty(tag, box, (point.X + o.Width) + 2, (point.Y + num) + num2, null);
                            box.BorderStyle = BorderStyle.None;
                            box.Width = width.getWidth(box.figure, box.attribute);
                            this.Control.Controls.Add(box);
                            flag = true;
                            num += box.Height + 10;
                        }
                    }
                    int y = 0;
                    foreach (ListViewItem item2 in data)
                    {
                        if ((item2.Group.Name == "ran") && (item2.Tag is XmlNode))
                        {
                            XmlNode node = item2.Tag as XmlNode;
                            if (control == null)
                            {
                                string str;
                                control = (Control) host.CreateComponent(DesignControls.Ran);
                                control.Location = new Point(point.X, point.Y + num);
                                list = new List<string>();
                                this.findRan(this.Control, list);
                                if (node.ParentNode.Attributes["repetition_id"] != null)
                                {
                                    str = node.ParentNode.Attributes["repetition_id"].Value;
                                }
                                else
                                {
                                    int num4 = 1;
                                    str = "R" + num4.ToString("D2");
                                    while (list.Contains(str))
                                    {
                                        num4++;
                                        str = "R" + num4.ToString("D2");
                                    }
                                }
                                ((IContainerAttributes) control).repetition_id = str;
                                if (node.ParentNode.Attributes["repetition_max"] != null)
                                {
                                    ((IContainerAttributes) control).repetition_max = int.Parse(node.ParentNode.Attributes["repetition_max"].Value);
                                }
                            }
                            Control control3 = (Control) host.CreateComponent(DesignControls.Label);
                            control3.Font = new Font("Tahoma", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                            this.setXmlAttributeToProperty(node, control3, 0, y, this.getAttribute(node, "name"));
                            control.Controls.Add(control3);
                            LBPrtTextBox box2 = (LBPrtTextBox) host.CreateComponent(DesignControls.PrtTextBox);
                            box2.Font = new Font("Courier New", 11f, FontStyle.Regular, GraphicsUnit.Point, 0x80);
                            this.setXmlAttributeToProperty(node, box2, control3.Width + 2, y + num2, null);
                            box2.BorderStyle = BorderStyle.None;
                            box2.Width = width.getWidth(box2.figure, box2.attribute);
                            control.Controls.Add(box2);
                            flag = true;
                            y += box2.Height + 10;
                        }
                    }
                    if (control != null)
                    {
                        this.Control.Controls.Add(control);
                    }
                    if (flag)
                    {
                        service.OnComponentChanged(this.Control, properties["Controls"], this.Control.Controls, this.Control.Controls);
                        transaction.Commit();
                    }
                    this.Control.ResumeLayout();
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

