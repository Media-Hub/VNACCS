using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using DevComponents.Editors;

namespace DevComponents.DotNetBar.Controls
{
    public class DataGridViewMaskedTextBoxAdvCell : DataGridViewTextBoxCell
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        static extern IntPtr PostMessage(IntPtr hWnd, int msg, int wParam, int lParam);

        #region Public properties

        #region EditType

        /// <summary>
        /// Gets the Type of the editing control associated with the cell
        /// </summary>
        public override Type EditType
        {
            get { return (typeof(DataGridViewMaskedTextBoxAdvEditingControl)); }
        }

        #endregion

        #endregion

        #region InitializeEditingControl

        /// <summary>
        /// InitializeEditingControl
        /// </summary>
        /// <param name="rowIndex"></param>
        /// <param name="initialFormattedValue"></param>
        /// <param name="dataGridViewCellStyle"></param>
        public override void InitializeEditingControl(int rowIndex,
            object initialFormattedValue, DataGridViewCellStyle dataGridViewCellStyle)
        {
            DetachEditingControl();

            base.InitializeEditingControl(rowIndex, initialFormattedValue, dataGridViewCellStyle);

            DataGridViewMaskedTextBoxAdvEditingControl ctl =
                (DataGridViewMaskedTextBoxAdvEditingControl) DataGridView.EditingControl;

            DataGridViewMaskedTextBoxAdvColumn oc = OwningColumn as DataGridViewMaskedTextBoxAdvColumn;

            if (oc != null)
            {
                MaskedTextBoxAdv tb = oc.MaskedTextBoxAdv;

                ctl.AllowPromptAsInput = tb.AllowPromptAsInput;
                ctl.AsciiOnly = tb.AsciiOnly;
                ctl.BeepOnError = false;
                ctl.Culture = tb.Culture;
                ctl.CutCopyMaskFormat = tb.CutCopyMaskFormat;
                ctl.DropDownControl = tb.DropDownControl;
                ctl.Enabled = tb.Enabled;
                ctl.FocusHighlightColor = tb.FocusHighlightColor;
                ctl.FocusHighlightEnabled = tb.FocusHighlightEnabled;
                ctl.HidePromptOnLeave = tb.HidePromptOnLeave;
                ctl.ImeMode = tb.ImeMode;
                ctl.InsertKeyMode = tb.InsertKeyMode;
                ctl.Mask = tb.Mask;
                ctl.PasswordChar = tb.PasswordChar;
                ctl.PromptChar = tb.PromptChar;
                ctl.RejectInputOnFirstFailure = tb.RejectInputOnFirstFailure;
                ctl.ResetOnPrompt = tb.ResetOnPrompt;
                ctl.ResetOnSpace = tb.ResetOnSpace;
                ctl.RightToLeft = tb.RightToLeft;
                ctl.SkipLiterals = tb.SkipLiterals;
                ctl.TextAlign = GetTextAlignment(dataGridViewCellStyle.Alignment);
                ctl.TextMaskFormat = tb.TextMaskFormat;
                ctl.UseSystemPasswordChar = tb.UseSystemPasswordChar;
                ctl.ValidatingType = tb.ValidatingType;
                ctl.WatermarkBehavior = tb.WatermarkBehavior;
                ctl.WatermarkColor = tb.WatermarkColor;
                ctl.WatermarkEnabled = tb.WatermarkEnabled;
                ctl.WatermarkFont = tb.WatermarkFont;
                ctl.WatermarkText = tb.WatermarkText;

                // The underlying MaskedTextBox's MultiLine property
                // is "not fully supported" - but we'll set it anyway.

                ctl.MaskedTextBox.Multiline = (dataGridViewCellStyle.WrapMode == DataGridViewTriState.True);

                ctl.BackgroundStyle.ApplyStyle(tb.BackgroundStyle);
                ctl.BackgroundStyle.Class = tb.BackgroundStyle.Class;

                tb.ButtonClear.CopyToItem(ctl.ButtonClear);
                tb.ButtonCustom.CopyToItem(ctl.ButtonCustom);
                tb.ButtonCustom2.CopyToItem(ctl.ButtonCustom2);
                tb.ButtonDropDown.CopyToItem(ctl.ButtonDropDown);

                ctl.ButtonClearClick += ButtonClearClick;
                ctl.ButtonCustomClick += ButtonCustomClick;
                ctl.ButtonCustom2Click += ButtonCustom2Click;
                ctl.ButtonDropDownClick += ButtonDropDownClick;
                ctl.KeyDown += KeyDown;

                ctl.Text = GetValue(initialFormattedValue);

                ctl.BeepOnError = tb.BeepOnError;
            }
        }

        #endregion

        #region DetachEditingControl

        /// <summary>
        /// DetachEditingControl
        /// </summary>
        public override void DetachEditingControl()
        {
            if (DataGridView != null && DataGridView.EditingControl != null)
            {
                DataGridViewMaskedTextBoxAdvEditingControl di =
                    DataGridView.EditingControl as DataGridViewMaskedTextBoxAdvEditingControl;

                if (di != null)
                {
                    di.ButtonClearClick -= ButtonClearClick;
                    di.ButtonCustomClick -= ButtonCustomClick;
                    di.ButtonCustom2Click -= ButtonCustom2Click;
                    di.ButtonDropDownClick -= ButtonDropDownClick;
                    di.KeyDown -= KeyDown;
                }
            }

            base.DetachEditingControl();
        }

        #endregion

        #region Event processing

        #region ButtonClearClick

        /// <summary>
        /// ButtonClearClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonClearClick(object sender, CancelEventArgs e)
        {
            ((DataGridViewMaskedTextBoxAdvColumn)OwningColumn).DoButtonClearClick(sender, e);
        }

        #endregion

        #region ButtonCustomClick

        /// <summary>
        /// ButtonCustomClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonCustomClick(object sender, EventArgs e)
        {
            ((DataGridViewMaskedTextBoxAdvColumn)OwningColumn).DoButtonCustomClick(sender, e);
        }

        #endregion

        #region ButtonCustom2Click

        /// <summary>
        /// ButtonCustom2Click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonCustom2Click(object sender, EventArgs e)
        {
            ((DataGridViewMaskedTextBoxAdvColumn)OwningColumn).DoButtonCustom2Click(sender, e);
        }

        #endregion

        #region ButtonDropDownClick

        /// <summary>
        /// ButtonDropDownClick
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void ButtonDropDownClick(object sender, CancelEventArgs e)
        {
            ((DataGridViewMaskedTextBoxAdvColumn)OwningColumn).DoButtonDropDownClick(sender, e);
        }

        #endregion

        #region KeyDown

        /// <summary>
        /// KeyDown routine forwards all DataGridView sent keys to
        /// the underlying focusable control
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void KeyDown(object sender, KeyEventArgs e)
        {
            if (DataGridView != null && DataGridView.EditingControl != null)
            {
                DataGridViewMaskedTextBoxAdvEditingControl di =
                    DataGridView.EditingControl as DataGridViewMaskedTextBoxAdvEditingControl;

                if (di != null)
                {
                    di.MaskedTextBox.SelectAll();

                    PostMessage(di.MaskedTextBox.Handle, 256, (int) e.KeyCode, 1);
                }
            }
        }

        #endregion

        #endregion

        #region PositionEditingControl

        /// <summary>
        /// PositionEditingControl
        /// </summary>
        /// <param name="setLocation"></param>
        /// <param name="setSize"></param>
        /// <param name="cellBounds"></param>
        /// <param name="cellClip"></param>
        /// <param name="cellStyle"></param>
        /// <param name="singleVerticalBorderAdded"></param>
        /// <param name="singleHorizontalBorderAdded"></param>
        /// <param name="isFirstDisplayedColumn"></param>
        /// <param name="isFirstDisplayedRow"></param>
        public override void PositionEditingControl(bool setLocation, bool setSize, Rectangle cellBounds,
            Rectangle cellClip, DataGridViewCellStyle cellStyle, bool singleVerticalBorderAdded,
            bool singleHorizontalBorderAdded, bool isFirstDisplayedColumn, bool isFirstDisplayedRow)
        {
            Rectangle editingControlBounds =
                PositionEditingPanel(cellBounds, cellClip, cellStyle, singleVerticalBorderAdded,
                                     singleHorizontalBorderAdded, isFirstDisplayedColumn, isFirstDisplayedRow);

            editingControlBounds = GetAdjustedEditingControlBounds(editingControlBounds, cellStyle);

            DataGridView.EditingControl.Location = new Point(editingControlBounds.X, editingControlBounds.Y);
            DataGridView.EditingControl.Size = new Size(editingControlBounds.Width, editingControlBounds.Height);
        }

        #endregion

        #region GetAdjustedEditingControlBounds

        /// <summary>
        /// GetAdjustedEditingControlBounds
        /// </summary>
        /// <param name="editingControlBounds"></param>
        /// <param name="cellStyle"></param>
        /// <returns></returns>
        private Rectangle GetAdjustedEditingControlBounds(
            Rectangle editingControlBounds, DataGridViewCellStyle cellStyle)
        {
            // Add a 1 pixel padding around the editing control

            editingControlBounds.X += 1;
            editingControlBounds.Y += 1;
            editingControlBounds.Width = Math.Max(0, editingControlBounds.Width - 2);
            editingControlBounds.Height = Math.Max(0, editingControlBounds.Height - 2);

            // Adjust the vertical location of the editing control

            Rectangle r = GetCellBounds(editingControlBounds);

            if (cellStyle.WrapMode != DataGridViewTriState.True)
            {
                if (r.Height < editingControlBounds.Height)
                {
                    switch (cellStyle.Alignment)
                    {
                        case DataGridViewContentAlignment.MiddleLeft:
                        case DataGridViewContentAlignment.MiddleCenter:
                        case DataGridViewContentAlignment.MiddleRight:
                            editingControlBounds.Y += (editingControlBounds.Height - r.Height)/2;
                            break;

                        case DataGridViewContentAlignment.BottomLeft:
                        case DataGridViewContentAlignment.BottomCenter:
                        case DataGridViewContentAlignment.BottomRight:
                            editingControlBounds.Y += (editingControlBounds.Height - r.Height);
                            break;
                    }
                }

                editingControlBounds.Height = Math.Max(1, r.Height);
            }

            editingControlBounds.Width = Math.Max(1, editingControlBounds.Width);

            return (editingControlBounds);
        }

        #endregion

        #region GetPreferredSize

        #region GetPreferredSize

        /// <summary>
        /// GetPreferredSize
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="cellStyle"></param>
        /// <param name="rowIndex"></param>
        /// <param name="constraintSize"></param>
        /// <returns></returns>
        protected override Size GetPreferredSize(Graphics graphics,
               DataGridViewCellStyle cellStyle, int rowIndex, Size constraintSize)
        {
            if (DataGridView == null)
                return (new Size(-1, -1));

            Size preferredSize = base.GetPreferredSize(graphics, cellStyle, rowIndex, constraintSize);

            DataGridViewMaskedTextBoxAdvColumn oc = OwningColumn as DataGridViewMaskedTextBoxAdvColumn;

            if (oc != null)
            {
                MaskedTextBoxAdv msk = oc.MaskedTextBoxAdv;

                preferredSize.Height = msk.Height;

                if (constraintSize.Width == 0)
                {
                    preferredSize.Width += GetImageWidth(msk.ButtonClear);
                    preferredSize.Width += GetImageWidth(msk.ButtonCustom);
                    preferredSize.Width += GetImageWidth(msk.ButtonCustom2);
                    preferredSize.Width += GetImageWidth(msk.ButtonDropDown);
                }
            }

            return (preferredSize);
        }

        #endregion

        #region GetFormattedValue

        protected override object GetFormattedValue(object value, int rowIndex,
            ref DataGridViewCellStyle cellStyle, TypeConverter valueTypeConverter,
            TypeConverter formattedValueTypeConverter, DataGridViewDataErrorContexts context)
        {
            DataGridViewMaskedTextBoxAdvColumn oc = OwningColumn as DataGridViewMaskedTextBoxAdvColumn;

            if (oc != null)
            {
                MaskedTextBoxAdv msk = oc.MaskedTextBoxAdv;
                msk.Text = GetValue(value);

                return (msk.Text);
            }

            return (base.GetFormattedValue(value, rowIndex,
                ref cellStyle, valueTypeConverter, formattedValueTypeConverter, context));
        }

        #endregion

        #region GetImageWidth

        private int GetImageWidth(InputButtonSettings ibs)
        {
            if (ibs.Visible == true)
                return (ibs.Image != null ? ibs.Image.Width : 16);

            return (0);
        }

        #endregion

        #endregion

        #region Paint

        #region Paint

        /// <summary>
        /// Cell painting
        /// </summary>
        /// <param name="graphics"></param>
        /// <param name="clipBounds"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="elementState"></param>
        /// <param name="value"></param>
        /// <param name="formattedValue"></param>
        /// <param name="errorText"></param>
        /// <param name="cellStyle"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <param name="paintParts"></param>
        protected override void Paint(Graphics graphics,
            Rectangle clipBounds, Rectangle cellBounds, int rowIndex,
            DataGridViewElementStates elementState, object value, object formattedValue,
            string errorText, DataGridViewCellStyle cellStyle,
            DataGridViewAdvancedBorderStyle advancedBorderStyle,
            DataGridViewPaintParts paintParts)
        {
            if (DataGridView != null)
            {
                // First paint the borders of the cell

                if (PartsSet(paintParts, DataGridViewPaintParts.Border))
                    PaintBorder(graphics, clipBounds, cellBounds, cellStyle, advancedBorderStyle);

                // Now paint the background and content

                if (PartsSet(paintParts, DataGridViewPaintParts.Background))
                {
                    Rectangle rBk = GetBackBounds(cellBounds, advancedBorderStyle);

                    if (rBk.Height > 0 && rBk.Width > 0)
                    {
                        DataGridViewMaskedTextBoxAdvColumn oc = (DataGridViewMaskedTextBoxAdvColumn)OwningColumn;
                        Bitmap bm = oc.GetCellBitmap(cellBounds);

                        if (bm != null)
                        {
                            using (Graphics g = Graphics.FromImage(bm))
                            {
                                PaintCellBackground(g, cellStyle, rBk);
                                PaintCellContent(g, cellBounds, rowIndex, formattedValue, cellStyle, paintParts, bm);

                                graphics.DrawImageUnscaledAndClipped(bm, rBk);
                            }

                            if ((DataGridView.ShowCellErrors == true) &&
                                (paintParts & DataGridViewPaintParts.ErrorIcon) == DataGridViewPaintParts.ErrorIcon)
                            {
                                base.PaintErrorIcon(graphics, clipBounds, cellBounds, errorText);
                            }
                        }
                    }
                }
            }
        }

        #endregion

        #region PaintCellBackground

        /// <summary>
        /// Paints the cell background
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cellStyle"></param>
        /// <param name="rBack"></param>
        private void PaintCellBackground(Graphics g,
            DataGridViewCellStyle cellStyle, Rectangle rBack)
        {
            Rectangle r = rBack;
            r.Location = new Point(0, 0);

            DataGridViewX dx = DataGridView as DataGridViewX;

            if (dx != null && dx.Enabled == true && Selected == true)
            {
                Office2007ButtonItemPainter.PaintBackground(g, dx.ButtonStateColorTable,
                    r, RoundRectangleShapeDescriptor.RectangleShape, false,
                    false);
            }
            else
            {
                Color color = (Selected == true)
                    ? cellStyle.SelectionBackColor : cellStyle.BackColor;

                using (Brush br = new SolidBrush(color))
                    g.FillRectangle(br, r);
            }
        }

        #endregion

        #region PaintCellContent

        /// <summary>
        /// Paints the cell content
        /// </summary>
        /// <param name="g"></param>
        /// <param name="cellBounds"></param>
        /// <param name="rowIndex"></param>
        /// <param name="value"></param>
        /// <param name="cellStyle"></param>
        /// <param name="paintParts"></param>
        /// <param name="bm"></param>
        private void PaintCellContent(Graphics g, Rectangle cellBounds, int rowIndex, object value,
            DataGridViewCellStyle cellStyle, DataGridViewPaintParts paintParts, Bitmap bm)
        {
            DataGridViewMaskedTextBoxAdvColumn oc = (DataGridViewMaskedTextBoxAdvColumn)OwningColumn;
            MaskedTextBoxAdv di = oc.MaskedTextBoxAdv;

            Point ptCurrentCell = DataGridView.CurrentCellAddress;
            bool cellCurrent = ptCurrentCell.X == ColumnIndex && ptCurrentCell.Y == rowIndex;
            bool cellEdited = cellCurrent && DataGridView.EditingControl != null;

            // If the cell is in editing mode, there is nothing else to paint

            if (cellEdited == false && rowIndex < DataGridView.RowCount)
            {
                if (PartsSet(paintParts, DataGridViewPaintParts.ContentForeground))
                {
                    cellBounds.X = 0;
                    cellBounds.Y = 0;
                    cellBounds.Width -= (oc.DividerWidth + 1);
                    cellBounds.Height -= 1;

                    di.Font = cellStyle.Font;
                    di.ForeColor = cellStyle.ForeColor;
                    di.BackColor = cellStyle.BackColor;

                    di.TextAlign = GetTextAlignment(cellStyle.Alignment);
                    di.Text = GetValue(value);

                    Rectangle r = GetAdjustedEditingControlBounds(cellBounds, cellStyle);

                    oc.OnBeforeCellPaint(rowIndex, ColumnIndex);

                    if (oc.DisplayControlForCurrentCellOnly == false)
                        DrawControl(di, cellStyle, r, bm, g, oc);
                    else
                        DrawText(di, cellStyle, r, g);
                }
            }
        }

        #region DrawControl

        /// <summary>
        /// DrawControl
        /// </summary>
        /// <param name="di"></param>
        /// <param name="cellStyle"></param>
        /// <param name="r"></param>
        /// <param name="bm"></param>
        /// <param name="g"></param>
        /// <param name="oc"></param>
        private void DrawControl(MaskedTextBoxAdv di, DataGridViewCellStyle cellStyle, Rectangle r,
            Bitmap bm, Graphics g, DataGridViewMaskedTextBoxAdvColumn oc)
        {
            if (di.ButtonGroup.Items.Count > 0)
            {
                // Determine if we must perform some tom-foolery in order
                // to get the control to DrawToBitmap correctly in older
                // Windows versions

                if (MustRenderVisibleControl() == true)
                {
                    di.Location = oc.DataGridView.Location;

                    if (di.Parent == null)
                    {
                        Form form = oc.DataGridView.FindForm();

                        if (form != null)
                            di.Parent = form;
                    }

                    di.SendToBack();
                    di.Visible = true;
                }

                using (Bitmap bm2 = new Bitmap(bm))
                {
                    di.Bounds = r;
                    di.DrawToBitmap(bm2, r);

                    foreach (VisualItem item in di.ButtonGroup.Items)
                    {
                        if (item.Visible == true)
                        {
                            Rectangle t = item.RenderBounds;
                            t.X += r.X;
                            t.Y += r.Y;

                            g.DrawImage(bm2, t, t, GraphicsUnit.Pixel);

                            if (t.Left < r.Right)
                                r.Width -= (r.Right - t.Left - 1);
                        }
                    }
                }

                di.Visible = false;
            }

            DrawText(di, cellStyle, r, g);
        }

        #endregion

        #region DrawText

        /// <summary>
        /// DrawText
        /// </summary>
        /// <param name="di"></param>
        /// <param name="cellStyle"></param>
        /// <param name="r"></param>
        /// <param name="g"></param>
        private void DrawText(MaskedTextBoxAdv di, DataGridViewCellStyle cellStyle, Rectangle r, Graphics g)
        {
            r.Inflate(-2, 0);

            eTextFormat tf = eTextFormat.Default | eTextFormat.NoPrefix;

            switch (di.TextAlign)
            {
                case HorizontalAlignment.Center:
                    tf |= eTextFormat.HorizontalCenter;
                    break;

                case HorizontalAlignment.Right:
                    tf |= eTextFormat.Right;
                    break;
            }

            if (cellStyle.WrapMode == DataGridViewTriState.True)
                tf |= eTextFormat.WordBreak;

            switch (cellStyle.Alignment)
            {
                case DataGridViewContentAlignment.TopLeft:
                case DataGridViewContentAlignment.TopCenter:
                case DataGridViewContentAlignment.TopRight:
                    tf |= eTextFormat.Top;
                    break;

                case DataGridViewContentAlignment.BottomLeft:
                case DataGridViewContentAlignment.BottomCenter:
                case DataGridViewContentAlignment.BottomRight:
                    tf |= eTextFormat.Bottom;
                    break;

                default:
                    tf |= eTextFormat.VerticalCenter;
                    break;
            } 
            
            TextDrawing.DrawString(g, di.Text, di.Font, di.ForeColor, r, tf);
        }

        #endregion

        #endregion

        #endregion

        #region MustRenderVisibleControl

        /// <summary>
        /// MustRenderVisibleControl
        /// </summary>
        /// <returns></returns>
        private bool MustRenderVisibleControl()
        {
            OperatingSystem osInfo = Environment.OSVersion;

            return (osInfo.Platform == PlatformID.Win32Windows ||
                (osInfo.Platform == PlatformID.Win32NT && osInfo.Version.Major < 6));

        }

        #endregion

        #region GetBackBounds

        /// <summary>
        /// Gets the background bounds for the given cell
        /// </summary>
        /// <param name="cellBounds"></param>
        /// <param name="advancedBorderStyle"></param>
        /// <returns></returns>
        private Rectangle GetBackBounds(
            Rectangle cellBounds, DataGridViewAdvancedBorderStyle advancedBorderStyle)
        {
            DataGridViewMaskedTextBoxAdvColumn oc = (DataGridViewMaskedTextBoxAdvColumn)OwningColumn;

            Rectangle r = BorderWidths(advancedBorderStyle);

            cellBounds.Offset(r.X, r.Y);
            cellBounds.Width -= r.Right;
            cellBounds.Height -= r.Bottom;

            if (Selected == true)
                cellBounds.Width += oc.DividerWidth;

            return (cellBounds);
        }
        #endregion

        #region GetCellBounds

        /// <summary>
        /// Gets the button bounds for the given cell
        /// </summary>
        /// <param name="cellBounds"></param>
        /// <returns></returns>
        private Rectangle GetCellBounds(Rectangle cellBounds)
        {
            DataGridViewMaskedTextBoxAdvColumn oc = (DataGridViewMaskedTextBoxAdvColumn)OwningColumn;

            Size size = oc.MaskedTextBoxAdv.PreferredSize;

            cellBounds.Location = new Point(1, 1);

            cellBounds.Width -= oc.DividerWidth;
            cellBounds.Height = size.Height - 1;

            return (cellBounds);
        }

        #endregion

        #region GetValue

        /// <summary>
        /// GetValue
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private string GetValue(object value)
        {
            return (value != Convert.DBNull ? Convert.ToString(value) : "");
        }

        #endregion

        #region GetTextAlignment

        /// <summary>
        /// GetTextAlignment
        /// </summary>
        /// <param name="alignment"></param>
        /// <returns></returns>
        private HorizontalAlignment GetTextAlignment(DataGridViewContentAlignment alignment)
        {
            switch (alignment)
            {
                case DataGridViewContentAlignment.TopCenter:
                case DataGridViewContentAlignment.MiddleCenter:
                case DataGridViewContentAlignment.BottomCenter:
                    return (HorizontalAlignment.Center);

                case DataGridViewContentAlignment.TopRight:
                case DataGridViewContentAlignment.MiddleRight:
                case DataGridViewContentAlignment.BottomRight:
                    return (HorizontalAlignment.Right);

                default:
                    return (HorizontalAlignment.Left);
            }
        }

        #endregion

        #region PartsSet

        /// <summary>
        /// Determines if the given part is set
        /// </summary>
        /// <param name="paintParts"></param>
        /// <param name="parts"></param>
        /// <returns></returns>
        private bool PartsSet(DataGridViewPaintParts paintParts, DataGridViewPaintParts parts)
        {
            return ((paintParts & parts) == parts);
        }

        #endregion
    }
}
