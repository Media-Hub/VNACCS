using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.SuperGrid;

namespace DevComponents.SuperGrid.Design
{
    public class ValueTypeEditor : UITypeEditor
    {
        #region Private variables

        private GridCell _Cell;
        private bool _HasBoolDropDown;

        #endregion

        #region GetEditStyle

        public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
        {
            if (context != null)
            {
                _Cell = context.Instance as GridCell;

                if (_Cell != null)
                    _HasBoolDropDown = CellHasBoolDropDown(_Cell);
            }

            return (UITypeEditorEditStyle.DropDown);
        }

        #region CellHasBoolDropDown

        private bool CellHasBoolDropDown(GridCell cell)
        {
            Type editType = GetEditType(cell);

            if (editType == typeof(GridCheckBoxEditControl) ||
                editType == typeof(GridCheckBoxXEditControl) ||
                editType == typeof(GridSwitchButtonEditControl))
            {
                return (true);
            }

            return (false);
        }

        #endregion

        #region GetEditType

        private Type GetEditType(GridCell cell)
        {
            if (cell.EditorType != null)
                return (cell.EditorType);

            if (cell.GridColumn != null)
                return (cell.GridColumn.EditorType);

            return (null);
        }

        #endregion

        #region IsDropDownResizable

        public override bool IsDropDownResizable
        {
            get { return (_HasBoolDropDown == false); }
        }

        #endregion

        #endregion

        #region GetPaintValueSupported

        public override bool GetPaintValueSupported(ITypeDescriptorContext context)
        {
            return (false);
        }

        #endregion

        #region EditValue

        public override object EditValue(
            ITypeDescriptorContext context, IServiceProvider provider, object value)
        {
            IWindowsFormsEditorService editorService =
                provider.GetService(typeof (IWindowsFormsEditorService)) as IWindowsFormsEditorService;

            if (editorService != null)
            {
                GridCell cell = context.Instance as GridCell;

                if (cell != null)
                {
                    if (_HasBoolDropDown == true)
                    {
                        BoolValueTypeDropDown bv = new BoolValueTypeDropDown(value, editorService, context);

                        bv.EscapePressed = false;

                        editorService.DropDownControl(bv);

                        if (bv.EscapePressed == true)
                            context.PropertyDescriptor.SetValue(context.Instance, value);
                        else
                            return (bv.Value);
                    }
                    else
                    {
                        TextBox tb = new TextBox();

                        tb.Multiline = true;
                        tb.Size = new Size(300, 100);
                        tb.ScrollBars = ScrollBars.Both;

                        if (value != null)
                            tb.Text = value.ToString();

                        editorService.DropDownControl(tb);

                        return (tb.Text);
                    }
                }
            }

            return (base.EditValue(context, provider, value));
        }
        
        #endregion
    }
}

