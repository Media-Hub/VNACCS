using System;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.ComponentModel;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;

namespace DevComponents.SuperGrid.Design
{
	/// <summary>
	/// Represents the class used for picking an image from image list
	/// </summary>
	public class ImageIndexEditor : UITypeEditor
    {
        #region Private variables

        private ImageList _ImageList;

        #endregion

        #region GetEditStyle

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
                 _ImageList = GetImageList(context);

            return (UITypeEditorEditStyle.DropDown);
        }

        #region GetImageList

        private ImageList GetImageList(ITypeDescriptorContext context)
        {
            IReferenceService r = (IReferenceService)context.GetService(typeof(IReferenceService));

            object[] objs = r.GetReferences(typeof(GridPanel));

            if (objs.Length > 0)
            {
                GridPanel panel = (GridPanel)objs[0];
                GridElement item = panel.SuperGrid.DesignerElement;

                if (item != null)
                    return (item.GridPanel.ImageList);

                return (panel.ImageList);
            }

            return (null);
        }

        #endregion

        #endregion

        #region GetPaintValueSupported

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return (true);
        }

        #endregion

        #region PaintValue

        public override void PaintValue(PaintValueEventArgs e)
        {
            if (e.Value != null)
            {
                Image img = GetImage((int)e.Value);

                if (img != null)
                    e.Graphics.DrawImage(img, e.Bounds);
            }
        }

        #region GetImage

        private Image GetImage(int index)
        {
            if (_ImageList != null && (uint)index < _ImageList.Images.Count)
                return (_ImageList.Images[index]);

            return (null);
        }

        #endregion

        #endregion

        #region EditValue

        public override object EditValue(
            ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService =
                provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (editorService != null && value is int)
            {
                ImageIndexTypeDropDown dd = new
                    ImageIndexTypeDropDown((int)value, _ImageList, editorService, context);

                dd.EscapePressed = false;

                editorService.DropDownControl(dd);

                if (dd.EscapePressed == true)
                    context.PropertyDescriptor.SetValue(context.Instance, value);
                else
                    return (dd.Value);
            }

            return (base.EditValue(context, provider, value));
        }

        #endregion
    }
}
