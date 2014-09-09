using System;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Drawing;
using System.ComponentModel.Design;
using System.ComponentModel;

namespace DevComponents.DotNetBar.Design
{
	/// <summary>
	/// Summary description for SuperTooltipDesigner.
	/// </summary>
	public class SuperTooltipDesigner:ComponentDesigner
    {
        #region Internal Implementation
        public override void Initialize(IComponent component)
        {
            base.Initialize(component);
            if (!component.Site.DesignMode)
                return;
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
			base.OnSetComponentDefaults();
			SetDesignTimeDefaults();
		}
#endif

		private void SetDesignTimeDefaults()
		{
			SuperTooltip sp=this.Component as SuperTooltip;
			if(sp==null)
				return;           
        }
        #endregion
    }
}
