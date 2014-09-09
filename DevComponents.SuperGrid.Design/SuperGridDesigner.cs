using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;
using System;
using System.Collections;

namespace DevComponents.SuperGrid.Design
{
    /// <summary>
    /// SuperGridDesigner
    /// </summary>
    public class SuperGridDesigner : ControlDesigner
    {
        #region Private variables

        private SuperGridControl _SuperGrid;
        private DesignerActionListCollection _ActionLists;

        #endregion

        #region Initialize

        /// <summary>
        /// Initializes our designer
        /// </summary>
        /// <param name="component"></param>
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);

            if (component.Site.DesignMode == true)
                _SuperGrid = component as SuperGridControl;
				
#if !TRIAL
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (dh != null)
                dh.LoadComplete += new EventHandler(DesignerLoadComplete);
#endif
        }

        #endregion

        #region Verbs

        /// <summary>
        /// Creates our verb collection
        /// </summary>
        public override DesignerVerbCollection Verbs
        {
            get
            {
                DesignerVerb[] verbs = new DesignerVerb[]
                {
                    //new DesignerVerb("KnobStyle 1", SetStyle1),
                    //new DesignerVerb("KnobStyle 2", SetStyle2),
                    //new DesignerVerb("KnobStyle 3", SetStyle3),
                    //new DesignerVerb("KnobStyle 4", SetStyle4),
                };

                return (new DesignerVerbCollection(verbs));
            }
        }

        #endregion

        #region ActionLists

        /// <summary>
        /// Gets our DesignerActionListCollection list
        /// </summary>
        public override DesignerActionListCollection ActionLists
        {
            get
            {
                if (_ActionLists == null)
                {
                    _ActionLists = new DesignerActionListCollection();

                    _ActionLists.Add(new SuperGridActionList(_SuperGrid));
                }

                return (_ActionLists);
            }
        }

        #endregion

        #region OnSetCursor

        protected override void OnSetCursor()
        {
            if (InScrollBar(Control.MousePosition) == true)
                Cursor.Current = Cursors.Default;

            base.OnSetCursor();
        }

        #region InScrollBar

        private bool InScrollBar(Point point)
        {
            if (_SuperGrid != null)
            {
                Point pt = _SuperGrid.PointToClient(point);

                if (_SuperGrid.HScrollBar.Visible)
                {
                    if (_SuperGrid.HScrollBar.Bounds.Contains(pt))
                        return (true);
                }

                if (_SuperGrid.VScrollBar.Visible)
                {
                    if (_SuperGrid.VScrollBar.Bounds.Contains(pt))
                        return (true);
                }
            }

            return (false);
        }

        #endregion

        #endregion

        #region GetHitTest

        protected override bool GetHitTest(Point point)
        {
            if (InScrollBar(point) == true)
                return (true);

            return (base.GetHitTest(point));
        }

        #endregion

        #region Licensing Stuff
        public override void InitializeNewComponent(IDictionary defaultValues)
        {
            base.InitializeNewComponent(defaultValues);
            SetDesignTimeDefaults();
        }
        private void SetDesignTimeDefaults()
        {
            SuperGridControl c = this.Control as SuperGridControl;
#if !TRIAL
            string key = GetLicenseKey();
            c.LicenseKey = key;
#endif
        }
#if !TRIAL
        internal static string GetLicenseKey()
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
        private void DesignerLoadComplete(object sender, EventArgs e)
        {
            IDesignerHost dh = this.GetService(typeof(IDesignerHost)) as IDesignerHost;
            if (dh != null)
                dh.LoadComplete -= new EventHandler(DesignerLoadComplete);

            string key = GetLicenseKey();
            SuperGridControl grid = this.Control as SuperGridControl;
            if (key != "" && grid != null && grid.LicenseKey == "" && grid.LicenseKey != key)
                TypeDescriptor.GetProperties(grid)["LicenseKey"].SetValue(grid, key);
        }
#endif
        #endregion
    }
}
