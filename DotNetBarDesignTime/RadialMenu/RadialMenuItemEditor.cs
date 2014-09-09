using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel.Design;
using System.Windows.Forms.Design;
using System.Reflection;
using DevComponents.AdvTree;

namespace DevComponents.DotNetBar.Design
{
    public partial class RadialMenuItemEditor : UserControl
    {
        private RadialMenuDesigner _Designer = null;
        private RadialMenuContainer _RadialMenuContainer = null;
        private RadialMenu _RadialMenu = null;
        public RadialMenuItemEditor()
        {
            InitializeComponent();
        }

        #region Implementation
        private object GetDesignService(Type serviceType)
        {
            if (_Designer != null)
            {
                return _Designer.GetDesignService(serviceType);
            }
            return null;
        }

        private RadialMenuItem CreateItem()
        {
            IDesignerHost dh = (IDesignerHost)GetDesignService(typeof(IDesignerHost));
            if (dh == null)
                return null;

            RadialMenuItem item = (RadialMenuItem)dh.CreateComponent(typeof(RadialMenuItem));
            int index = 1;
            if (_RadialMenu != null)
                index = _RadialMenu.Items.Count + 1;
            else if (_RadialMenuContainer != null)
                index = _RadialMenuContainer.SubItems.Count + 1;

            item.Text = "Item " + index.ToString();
            return item;
        }

        private void AddNewItem()
        {
            AddNewItem(null);
        }

        private void OnComponentChanging(RadialMenuItem parent, IComponentChangeService cc)
        {
            if (parent == null)
            {
                if (_RadialMenu != null)
                    cc.OnComponentChanging(_RadialMenu, TypeDescriptor.GetProperties(_RadialMenu)["Items"]);
                else if (_RadialMenuContainer != null)
                    cc.OnComponentChanging(_RadialMenuContainer, TypeDescriptor.GetProperties(_RadialMenuContainer)["SubItems"]);
            }
            else
                cc.OnComponentChanging(parent, TypeDescriptor.GetProperties(parent)["SubItems"]);
        }
        private void OnComponentChanged(RadialMenuItem parent, IComponentChangeService cc)
        {
            if (parent == null)
            {
                if (_RadialMenu != null)
                    cc.OnComponentChanged(_RadialMenu, TypeDescriptor.GetProperties(_RadialMenu)["Items"], null, null);
                else if (_RadialMenuContainer != null)
                    cc.OnComponentChanged(_RadialMenuContainer, TypeDescriptor.GetProperties(_RadialMenuContainer)["SubItems"], null, null);
            }
            else
                cc.OnComponentChanged(parent, TypeDescriptor.GetProperties(parent)["SubItems"], null, null);
        }
        private void AddNewItem(RadialMenuItem parent)
        {
            IDesignerHost dh = (IDesignerHost)GetDesignService(typeof(IDesignerHost));
            DesignerTransaction dt = null;
            if (dh != null)
            {
                dt = dh.CreateTransaction("New RadialMenuItem");
            }
            //bool isEmpty = advTree1.Nodes.Count == 0;
            RadialMenuItem item = CreateItem();
            if (item == null) return;

            IComponentChangeService cc = GetDesignService(typeof(IComponentChangeService)) as IComponentChangeService;
            if (cc != null)
            {
                OnComponentChanging(parent, cc);
            }

            if (parent == null)
            {
                if (_RadialMenu != null)
                    _RadialMenu.Items.Add(item);
                else if (_RadialMenuContainer != null)
                    _RadialMenuContainer.SubItems.Add(item);
            }
            else
                parent.SubItems.Add(item);

            if (cc != null)
            {
                OnComponentChanged(parent, cc);
            }

            if (dt != null)
                dt.Commit();
            Node node = CreateNodeForItem(item);
            if (parent == null)
                advTree1.Nodes.Add(node);
            else
            {
                advTree1.SelectedNode.Nodes.Add(node);
                advTree1.SelectedNode.Expand();
            }

            //if (isEmpty && advTree1.SelectedNode == null && advTree1.Nodes.Count > 0)
            //    advTree1.SelectedNode = advTree1.Nodes[0];
            advTree1.SelectedNode = node;
        }
        #endregion

        private void RadialMenuItemEditor_Load(object sender, EventArgs e)
        {
            IUIService service = GetDesignService(typeof(IUIService)) as IUIService;
            if (service != null)
            {
                PropertyInfo pi = propertyGrid1.GetType().GetProperty("ToolStripRenderer", System.Reflection.BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance);
                if (pi != null)
                {
                    pi.SetValue(propertyGrid1, (ToolStripProfessionalRenderer)service.Styles["VsToolWindowRenderer"], null);
                }
            }
        }

        private void advTree1_MouseUp(object sender, MouseEventArgs e)
        {
            Node node = advTree1.GetNodeAt(e.Y);
            if (node == null) advTree1.SelectedNode = null;
        }

        internal void UpdateDisplay()
        {
            advTree1.Nodes.Clear();
            if (_RadialMenu == null && _RadialMenuContainer == null) return;

            advTree1.BeginUpdate();

            SubItemsCollection collection = null;
            if (_RadialMenu != null)
                collection = _RadialMenu.Items;
            else
                collection = _RadialMenuContainer.SubItems;

            foreach (BaseItem item in collection)
            {
                RadialMenuItem menu = item as RadialMenuItem;
                if (menu != null)
                {
                    Node node = CreateNodeForItem(menu);
                    advTree1.Nodes.Add(node);
                    LoadSubItems(node, menu);
                }
            }

            advTree1.EndUpdate();
        }

        private void LoadSubItems(Node parent, RadialMenuItem item)
        {
            foreach (BaseItem o in item.SubItems)
            {
                if (o is RadialMenuItem)
                {
                    RadialMenuItem menu = (RadialMenuItem)o;
                    Node node = CreateNodeForItem(menu);
                    parent.Nodes.Add(node);
                    LoadSubItems(node, menu);
                }
            }
        }

        private Node CreateNodeForItem(RadialMenuItem item)
        {
            Node node = new Node();
            node.Expanded = true;
            node.Tag = item;
            node.Text = item.Text + " [" + item.Name + "]";
            //node.Image = item.GetItemImage();
            //node.Cells.Add(new Cell(item.Name));
            return node;
        }
        private string GetText(RadialMenuItem item)
        {
            return item.Text + " [" + item.Name + "]";
        }

        public RadialMenuDesigner Designer
        {
            get { return _Designer; }
            set
            {
                _Designer = value;
                if (_Designer != null)
                    this.propertyGrid1.Site = new PropertyGridSite((IServiceProvider)_Designer.Component.Site, this.propertyGrid1);
            }
        }

        public RadialMenu RadialMenu
        {
            get { return _RadialMenu; }
            set
            {
                if (value != _RadialMenu)
                {
                    RadialMenu oldValue = _RadialMenu;
                    _RadialMenu = value;
                    OnRadialMenuChanged(oldValue, value);
                }
            }
        }
        private void OnRadialMenuChanged(RadialMenu oldValue, RadialMenu newValue)
        {
            UpdateDisplay();
        }

        public RadialMenuContainer RadialMenuContainer
        {
            get { return _RadialMenuContainer; }
            set
            {
                if (value != _RadialMenuContainer)
                {
                    RadialMenuContainer oldValue = _RadialMenuContainer;
                    _RadialMenuContainer = value;
                    OnRadialMenuContainerChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when RadialMenuContainer property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnRadialMenuContainerChanged(RadialMenuContainer oldValue, RadialMenuContainer newValue)
        {
            UpdateDisplay();
        }

        private void buttonAddItem_Click(object sender, EventArgs e)
        {
            AddNewItem(null);
        }

        private void buttonNewSubMenu_Click(object sender, EventArgs e)
        {
            RadialMenuItem parent = null;
            if (advTree1.SelectedNode != null) parent = advTree1.SelectedNode.Tag as RadialMenuItem;
            AddNewItem(parent);
        }

        private void buttonRemoveItem_Click(object sender, EventArgs e)
        {
            if (advTree1.SelectedNode == null) return;
            RadialMenuItem item = advTree1.SelectedNode.Tag as RadialMenuItem;
            if (item == null) return;
            advTree1.SelectedNode.Remove();
            DeleteItem(item);
        }

        private void DeleteItem(RadialMenuItem item)
        {
            IDesignerHost dh = (IDesignerHost)GetDesignService(typeof(IDesignerHost));
            if (dh == null)
                return;

            IComponentChangeService cc = GetDesignService(typeof(IComponentChangeService)) as IComponentChangeService;
            RadialMenuItem parent = item.Parent as RadialMenuItem;
            if (cc != null)
                OnComponentChanging(parent, cc);

            if (parent == null)
            {
                if (_RadialMenu != null)
                    _RadialMenu.Items.Remove(item);
                else if (_RadialMenuContainer != null)
                    _RadialMenuContainer.SubItems.Remove(item);
            }
            else
                parent.SubItems.Remove(item);

            if (cc != null)
                OnComponentChanged(parent, cc);

            dh.DestroyComponent(item);
        }

        private void advTree1_AfterNodeSelect(object sender, AdvTreeNodeEventArgs e)
        {
            buttonRemove.Enabled = e.Node != null;
            buttonNewSubMenu.Enabled = e.Node != null;
            if (e.Node != null)
                propertyGrid1.SelectedObject = e.Node.Tag;
            else
                propertyGrid1.SelectedObject = null;
        }

        private void advTree1_AfterNodeDrop(object sender, TreeDragDropEventArgs e)
        {
            IDesignerHost dh = (IDesignerHost)GetDesignService(typeof(IDesignerHost));
            DesignerTransaction dt = null;
            if (dh != null) dt = dh.CreateTransaction("Move items");

            IComponentChangeService cc = GetDesignService(typeof(IComponentChangeService)) as IComponentChangeService;

            try
            {
                RadialMenuItem movedItem = e.Node.Tag as RadialMenuItem;
                RadialMenuItem parent = movedItem.Parent as RadialMenuItem;
                
                if (cc != null)
                {
                    OnComponentChanging(parent, cc);
                }

                if (parent == null)
                {
                    if (_RadialMenu != null)
                        _RadialMenu.Items.Remove(movedItem);
                    else if (_RadialMenuContainer != null)
                        _RadialMenuContainer.SubItems.Remove(movedItem);
                }
                else
                    parent.SubItems.Remove(movedItem);

                if (cc != null)
                {
                    OnComponentChanged(parent, cc);
                }

                if (e.NewParentNode == null)
                {
                    int index = advTree1.Nodes.IndexOf(e.Node);
                    if (cc != null)
                    {
                        if (_RadialMenu != null)
                            cc.OnComponentChanging(_RadialMenu, TypeDescriptor.GetProperties(_RadialMenu)["Items"]);
                        else if (_RadialMenuContainer != null)
                            cc.OnComponentChanging(_RadialMenuContainer, TypeDescriptor.GetProperties(_RadialMenuContainer)["SubItems"]);
                    }

                    if (_RadialMenu != null)
                        _RadialMenu.Items.Insert(index, movedItem);
                    else if (_RadialMenuContainer != null)
                        _RadialMenuContainer.SubItems.Insert(index, movedItem);

                    if (cc != null)
                    {
                        if (_RadialMenu != null)
                            cc.OnComponentChanged(_RadialMenu, TypeDescriptor.GetProperties(_RadialMenu)["Items"], null, null);
                        else if (_RadialMenuContainer != null)
                            cc.OnComponentChanged(_RadialMenuContainer, TypeDescriptor.GetProperties(_RadialMenuContainer)["SubItems"], null, null);
                    }
                }
                else
                {
                    parent = e.NewParentNode.Tag as RadialMenuItem;
                    int index = e.NewParentNode.Nodes.IndexOf(e.Node);
                    if (cc != null) cc.OnComponentChanging(parent, TypeDescriptor.GetProperties(parent)["SubItems"]);
                    parent.SubItems.Insert(index, movedItem);
                    if (cc != null) cc.OnComponentChanged(parent, TypeDescriptor.GetProperties(parent)["SubItems"], null, null);
                }
            }
            catch
            {
                if (dt != null)
                    dt.Cancel();
                throw;
            }
            finally
            {
                if (dt != null && !dt.Canceled)
                    dt.Commit();
            }
        }

        private void propertyGrid1_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
        {
            if (advTree1.SelectedNode == null) return;
            Node node = advTree1.SelectedNode;
            RadialMenuItem item = (RadialMenuItem)propertyGrid1.SelectedObject;
            if (e.ChangedItem.PropertyDescriptor.Name == "Text" || e.ChangedItem.PropertyDescriptor.Name == "Name")
            {
                node.Text = GetText(item);
            }
        }


        private void buttonX1_Click(object sender, EventArgs e)
        {
            Form form = this.FindForm();
            if (form != null) form.Close();
        }

        private void radialMenu1_BeforeMenuOpen(object sender, DevComponents.DotNetBar.Events.CancelableEventSourceArgs e)
        {
            SubItemsCollection collection = null;
            if (_RadialMenu != null)
            {
                collection = _RadialMenu.Items;
                radialMenu1.MenuType = _RadialMenu.MenuType;
                radialMenu1.Diameter = _RadialMenu.Diameter;
                radialMenu1.CenterButtonDiameter = _RadialMenu.CenterButtonDiameter;
                radialMenu1.Font = _RadialMenu.Font;
            }
            else if (_RadialMenuContainer != null)
            {
                collection = _RadialMenuContainer.SubItems;
                radialMenu1.MenuType = _RadialMenuContainer.MenuType;
                radialMenu1.Diameter = _RadialMenuContainer.Diameter;
                radialMenu1.CenterButtonDiameter = _RadialMenuContainer.CenterButtonDiameter;
            }
            if (collection == null) return;

            
            if (radialMenu1.Items.Count > 0)
            {
                BaseItem[] cleanUpItems = new BaseItem[radialMenu1.Items.Count];
                radialMenu1.Items.CopyTo(cleanUpItems, 0);
                radialMenu1.Items.Clear();
                foreach (BaseItem item in cleanUpItems)
                {
                    item.Dispose();
                }
            }

            foreach (BaseItem item in collection)
            {
                if (item is RadialMenuItem)
                {
                    BaseItem copy = item.Copy();
                    radialMenu1.Items.Add(copy);
                }
            }
        }

        #region PropertyGridSite
        internal class PropertyGridSite : ISite, IServiceProvider
        {
            // Fields
            private IComponent comp;
            private bool inGetService;
            private IServiceProvider sp;

            // Methods
            public PropertyGridSite(IServiceProvider sp, IComponent comp)
            {
                this.sp = sp;
                this.comp = comp;
            }

            public object GetService(Type t)
            {
                if (!this.inGetService && (this.sp != null))
                {
                    try
                    {
                        this.inGetService = true;
                        return this.sp.GetService(t);
                    }
                    finally
                    {
                        this.inGetService = false;
                    }
                }
                return null;
            }

            // Properties
            public IComponent Component
            {
                get
                {
                    return this.comp;
                }
            }

            public IContainer Container
            {
                get
                {
                    return null;
                }
            }

            public bool DesignMode
            {
                get
                {
                    return false;
                }
            }

            public string Name
            {
                get
                {
                    return null;
                }
                set
                {
                }
            }
        }
        #endregion


    }
}


