using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel;
using System.Collections;
using System.ComponentModel.Design;
using DevComponents.DotNetBar.Rendering;
using System.Drawing;
using DevComponents.DotNetBar.Metro;

namespace DevComponents.DotNetBar.Design
{
    public class MetroStatusBarDesigner : BarBaseControlDesigner
    {
        public MetroStatusBarDesigner()
		{
			this.EnableItemDragDrop=true;
		}

        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            if (component == null || component.Site == null || !component.Site.DesignMode)
                return;

#if !TRIAL
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (dh != null)
                dh.LoadComplete += new EventHandler(dh_LoadComplete);
#endif
        }

#if FRAMEWORK20
        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            SetDesignTimeDefaults();
        }
#else
		public override void OnSetComponentDefaults()
		{
            SetDesignTimeDefaults();
			base.OnSetComponentDefaults();
		}
#endif

        private void SetDesignTimeDefaults()
        {
            MetroStatusBar bar = this.Control as MetroStatusBar;
            bar.Dock = System.Windows.Forms.DockStyle.Bottom;
            try
            {
                bar.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            catch
            {
                
            }
#if !TRIAL
            string key = GetLicenseKey();
            bar.LicenseKey = key;
#endif
        }

        public override DesignerVerbCollection Verbs
        {
            get
            {
                Bar bar = this.Control as Bar;
                DesignerVerb[] verbs = null;
                verbs = new DesignerVerb[]
						{
							new DesignerVerb("Add Button", new EventHandler(CreateButton)),
							new DesignerVerb("Add Horizontal Container", new EventHandler(CreateHorizontalContainer)),
							new DesignerVerb("Add Vertical Container", new EventHandler(CreateVerticalContainer)),
                            new DesignerVerb("Add Slider", new EventHandler(CreateSliderItem)),
							new DesignerVerb("Add Text Box", new EventHandler(CreateTextBox)),
							new DesignerVerb("Add Combo Box", new EventHandler(CreateComboBox)),
							new DesignerVerb("Add Label", new EventHandler(CreateLabel)),
							new DesignerVerb("Add Color Picker", new EventHandler(CreateColorPicker)),
                            new DesignerVerb("Add Micro-Chart", new EventHandler(CreateMicroChart)),
                            new DesignerVerb("Add Switch Button", new EventHandler(CreateSwitch)),
                            new DesignerVerb("Add Progress bar", new EventHandler(CreateProgressBar)),
                            new DesignerVerb("Add Check box", new EventHandler(CreateCheckBox)),
                            new DesignerVerb("Add WinForms Control Container", new EventHandler(CreateControlContainer))
                        };
                return new DesignerVerbCollection(verbs);
            }
        }

        private void CreateVerticalContainer(object sender, EventArgs e)
        {
            CreateContainer(this.GetItemContainer(), eOrientation.Vertical);
        }

        private void CreateHorizontalContainer(object sender, EventArgs e)
        {
            CreateContainer(this.GetItemContainer(), eOrientation.Horizontal);
        }

        private void CreateContainer(BaseItem parent, eOrientation orientation)
        {
            m_CreatingItem = true;
            try
            {
                DesignerSupport.CreateItemContainer(this, parent, orientation);
            }
            finally
            {
                m_CreatingItem = false;
            }
            this.RecalcLayout();
        }

        #region Licensing Stuff
#if !TRIAL
        private string GetLicenseKey()
        {
            string key = "";
            Microsoft.Win32.RegistryKey regkey = Microsoft.Win32.Registry.LocalMachine;
            regkey = regkey.OpenSubKey("Software\\DevComponents\\Licenses", false);
            if (regkey != null)
            {
                object keyValue = regkey.GetValue("DevComponents.DotNetBar.DotNetBarManager2");
                if (keyValue != null)
                    key = keyValue.ToString();
            }
            return key;
        }
        private void dh_LoadComplete(object sender, EventArgs e)
        {
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (dh != null)
                dh.LoadComplete -= new EventHandler(dh_LoadComplete);

            string key = GetLicenseKey();
            MetroStatusBar bar = this.Control as MetroStatusBar;
            if (key != "" && bar != null && bar.LicenseKey == "" && bar.LicenseKey != key)
                TypeDescriptor.GetProperties(bar)["LicenseKey"].SetValue(bar, key);
        }
#endif
        #endregion
    }
}
