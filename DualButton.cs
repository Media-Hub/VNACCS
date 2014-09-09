using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using DevComponents.DotNetBar.Rendering;

namespace DevComponents.DotNetBar
{
    public class DualButton : BaseItem
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the DualButton class.
        /// </summary>
        public DualButton()
        {
            RecreatePaths(_ButtonSize);
        }
        protected override void Dispose(bool disposing)
        {
            if (_TopPartPath != null)
            {
                _TopPartPath.Dispose();
                _TopPartPath = null;
            }
            if (_BottomPartPath != null)
            {
                _BottomPartPath.Dispose();
                _BottomPartPath = null;
            }

            base.Dispose(disposing);
        }
        #endregion

        #region Implementation
        public override BaseItem Copy()
        {
            DualButton button = new DualButton();
            CopyToItem(button);
            return button;
        }
        protected override void CopyToItem(BaseItem objCopy)
        {
            DualButton button = (DualButton)objCopy;
            button.ButtonSize = _ButtonSize;
            button.Text2 = _Text2;
            base.CopyToItem(objCopy);
        }
        private Size _ButtonSize = new Size(38, 24);
        public Size ButtonSize
        {
            get { return _ButtonSize; }
            set
            {
                if (value.IsEmpty)
                    throw new ArgumentException("Empty size is not valid size");
                if (value.Width < 1 || value.Width < 1)
                    throw new ArgumentException("Width or Height cannot be less than 1");
                if (value != _ButtonSize)
                {
                    Size oldValue = _ButtonSize;
                    _ButtonSize = value;
                    OnButtonSizeChanged(oldValue, value);
                }
            }
        }
        private void RecreatePaths(Size buttonSize)
        {
            if (_TopPartPath != null) _TopPartPath.Dispose();
            if (_BottomPartPath != null) _BottomPartPath.Dispose();
            _TopPartPath = new GraphicsPath();
            _TopPartPath.AddLines(new PointF[] { new PointF(0, buttonSize.Height - 3), new PointF(0, 0), new PointF(buttonSize.Width - 2, 0) });
            _TopPartPath.CloseAllFigures();

            _BottomPartPath = new GraphicsPath();
            _BottomPartPath.AddLines(new PointF[] { new PointF(2, buttonSize.Height - 1), new PointF(buttonSize.Width - 1, buttonSize.Height - 1), new PointF(buttonSize.Width - 1, 2) });
            _BottomPartPath.CloseAllFigures();
        }
        /// <summary>
        /// Called when ButtonSize property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnButtonSizeChanged(Size oldValue, Size newValue)
        {
            RecreatePaths(newValue);
            //OnPropertyChanged(new PropertyChangedEventArgs("ButtonSize"));
        }
        private GraphicsPath _TopPartPath = null, _BottomPartPath = null;

        public override void RecalcSize()
        {
            m_Rect.Size = _ButtonSize;
            base.RecalcSize();
        }

        public override void Paint(ItemPaintArgs p)
        {
            Graphics g = p.Graphics;
            Matrix oldMatrix = g.Transform;
            g.TranslateTransform(m_Rect.X, m_Rect.Y, MatrixOrder.Append);
            LinearGradientColorTable border = null, back = null, back2 = null;
            Color textColor = Color.Empty;



            if (_TopPartPath != null)
            {
                Office2007ButtonItemStateColorTable table = GetOffice2007StateColorTable(p, eDualButtonPart.Up);
                if (table != null)
                {
                    if (table.Background != null)
                        back = table.Background;
                    else
                    {
                        back = table.TopBackground;
                        back2 = table.BottomBackground;
                    }
                    border = table.OuterBorder;
                    textColor = table.Text;
                }
                DrawBackground(g, _TopPartPath, border, back, back2);
                //g.FillPath(_LeftMouseButtonDown == eDualButtonPart.Up ? Brushes.Blue : (_MouseOverPart == eDualButtonPart.Up ? Brushes.Yellow : Brushes.Red), _TopPartPath);
                Rectangle textBounds = Rectangle.Round(_TopPartPath.GetBounds());
                textBounds.Offset(0, -1);
                textBounds.Width = textBounds.Width / 2;
                TextDrawing.DrawString(g, Text, _Font ?? p.Font, textColor, textBounds, eTextFormat.HorizontalCenter | eTextFormat.Top);
            }
            if (_BottomPartPath != null)
            {
                Office2007ButtonItemStateColorTable table = GetOffice2007StateColorTable(p, eDualButtonPart.Down);
                back2 = null;
                if (table != null)
                {
                    if (table.Background != null)
                        back = table.Background;
                    else
                    {
                        back = table.TopBackground;
                        back2 = table.BottomBackground;
                    }
                    border = table.OuterBorder;
                    textColor = table.Text;
                }
                DrawBackground(g, _BottomPartPath, border, back, back2);
                //g.FillPath(_LeftMouseButtonDown == eDualButtonPart.Down ? Brushes.Blue : (_MouseOverPart == eDualButtonPart.Down ? Brushes.Yellow : Brushes.Green), _BottomPartPath);
                Rectangle textBounds = Rectangle.Round(_BottomPartPath.GetBounds());
                textBounds.Offset(textBounds.Width / 2, 1);
                textBounds.Width = textBounds.Width / 2;
                TextDrawing.DrawString(g, Text2, _Font ?? p.Font, textColor, textBounds, eTextFormat.HorizontalCenter | eTextFormat.Bottom);
            }
            g.Transform = oldMatrix;

        }

        private Office2007ButtonItemStateColorTable GetOffice2007StateColorTable(ItemPaintArgs p, eDualButtonPart part)
        {
            if (BarFunctions.IsOffice2007Style(EffectiveStyle))
            {
                if (p.Renderer is Office2007Renderer)
                {
                    Office2007ColorTable ct = ((Office2007Renderer)p.Renderer).ColorTable;
                    Office2007ButtonItemColorTable buttonColorTable = ct.ButtonItemColors[Enum.GetName(typeof(eButtonColor), eButtonColor.OrangeWithBackground)];
                    if (!this.Enabled)
                        return buttonColorTable.Disabled;
                    else if (_LeftMouseButtonDown == part)
                        return buttonColorTable.Pressed;
                    else if (_MouseOverPart == part)
                        return buttonColorTable.MouseOver;
                    else if (this.IsSelected == part)
                        return buttonColorTable.Checked;
                    else
                        return buttonColorTable.Default;
                }
            }

            return null;
        }

        private void DrawBackground(Graphics g, GraphicsPath path, LinearGradientColorTable border, LinearGradientColorTable back, LinearGradientColorTable back2)
        {
            if (back != null && !back.IsEmpty)
            {
                if (back2 != null && !back2.IsEmpty)
                {
                    using (LinearGradientBrush brush = new LinearGradientBrush(path.GetBounds(), back.Start, back2.End, 90))
                    {
                        ColorBlend cb = new ColorBlend(4);
                        cb.Colors = new Color[] { back.Start, back.End, back2.Start, back2.End };
                        cb.Positions = new float[] { 0f, .5f, .5f, 1f };
                        brush.InterpolationColors = cb;
                        g.FillPath(brush, path);
                    }
                }
                else
                    DisplayHelp.FillPath(g, path, back);
            }
            if (border != null && !border.IsEmpty)
            {
                using (Brush brush = DisplayHelp.CreateBrush(Rectangle.Round(path.GetBounds()), border))
                {
                    using (Pen pen = new Pen(brush, 1))
                        g.DrawPath(pen, path);
                }
                //DisplayHelp.DrawGradientPathBorder(g, path, border, 1);
            }
        }

        public override void InternalMouseDown(System.Windows.Forms.MouseEventArgs objArg)
        {
            if (objArg.Button == System.Windows.Forms.MouseButtons.Left)
            {
                this.LeftMouseButtonDown = HitTest(objArg.X, objArg.Y);
            }
            base.InternalMouseDown(objArg);
        }

        public override void InternalMouseUp(System.Windows.Forms.MouseEventArgs objArg)
        {
            if (objArg.Button == System.Windows.Forms.MouseButtons.Left)
            {
                if (LeftMouseButtonDown != eDualButtonPart.None && MouseOverPart == LeftMouseButtonDown)
                {
                    if (LeftMouseButtonDown == eDualButtonPart.Up)
                        ExecuteCommand();
                    else
                        ExecuteCommand2();
                }
                LeftMouseButtonDown = eDualButtonPart.None;
            }
            base.InternalMouseUp(objArg);
        }

        public override void InternalMouseMove(System.Windows.Forms.MouseEventArgs objArg)
        {
            this.MouseOverPart = HitTest(objArg.X, objArg.Y);
            //if (objArg.Button == System.Windows.Forms.MouseButtons.Left && LeftMouseButtonDownPart == eDualButtonPart.None && _MouseOverPart != eDualButtonPart.None)
            //    LeftMouseButtonDownPart = _MouseOverPart;
            if (_LeftMouseButtonDown != eDualButtonPart.None && _MouseOverPart != _LeftMouseButtonDown)
                LeftMouseButtonDown = eDualButtonPart.None;
            base.InternalMouseMove(objArg);
        }

        public eDualButtonPart HitTest(Point clientPoint)
        {
            return HitTest(clientPoint.X, clientPoint.Y);
        }
        public eDualButtonPart HitTest(int x, int y)
        {
            Point p = new Point(x - m_Rect.X, y - m_Rect.Y);
            if (_TopPartPath.IsVisible(p))
                return eDualButtonPart.Up;
            else if (_BottomPartPath.IsVisible(p))
                return eDualButtonPart.Down;

            return eDualButtonPart.None;
        }

        public override void InternalMouseLeave()
        {
            this.MouseOverPart = eDualButtonPart.None;
            LeftMouseButtonDown = eDualButtonPart.None;
            base.InternalMouseLeave();
        }

        private string _Text2 = string.Empty;
        /// <summary>
        /// Gets or sets the second button part text.
        /// </summary>
        [DefaultValue(""), Category("Appearance"), Description("Indicates second button part text."), Localizable(true)]
        public string Text2
        {
            get { return _Text2; }
            set
            {
                if (value == null) value = string.Empty;
                if (value != _Text2)
                {
                    string oldValue = _Text2;
                    _Text2 = value;
                    OnText2Changed(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Text2 property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnText2Changed(string oldValue, string newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Text2"));
            this.Refresh();
        }

        private eDualButtonPart _MouseOverPart;
        public eDualButtonPart MouseOverPart
        {
            get { return _MouseOverPart; }
            private set
            {
                if (value != _MouseOverPart)
                {
                    eDualButtonPart oldValue = _MouseOverPart;
                    _MouseOverPart = value;
                    OnMouseOverPartChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when MouseOverPart property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnMouseOverPartChanged(eDualButtonPart oldValue, eDualButtonPart newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("MouseOverPart"));
            this.Refresh();
        }

        private eDualButtonPart _LeftMouseButtonDown = eDualButtonPart.None;
        public eDualButtonPart LeftMouseButtonDown
        {
            get { return _LeftMouseButtonDown; }
            private set
            {
                if (value != _LeftMouseButtonDown)
                {
                    eDualButtonPart oldValue = _LeftMouseButtonDown;
                    _LeftMouseButtonDown = value;
                    OnLeftMouseButtonDownChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when LeftMouseButtonDownPart property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnLeftMouseButtonDownChanged(eDualButtonPart oldValue, eDualButtonPart newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("LeftMouseButtonDownPart"));
            this.Refresh();
        }

        protected virtual void ExecuteCommand2()
        {
            if (_Command2 == null) return;
            CommandManager.ExecuteCommand(new ExtraCommandSource(_Command2, _Command2Parameter));
        }

        internal void ExecuteCommand2Internal()
        {
            ExecuteCommand2();
        }

        private ICommand _Command2 = null;
        /// <summary>
        /// Gets or sets the command assigned to the item. Default value is null.
        /// <remarks>Note that for ButtonItem instances if this property is set to null and command was assigned previously, Enabled property will be set to false automatically to disable the item.</remarks>
        /// </summary>
        [DefaultValue(null), Category("Commands"), Description("Indicates the command assigned to the item.")]
        public virtual ICommand Command2
        {
            get
            {
                return _Command2;
            }
            set
            {
                bool changed = false;
                if (_Command2 != value)
                    changed = true;

                if (_Command2 != null)
                    CommandManager.UnRegisterCommandSource(this, _Command2);
                _Command2 = value;
                if (value != null)
                    CommandManager.RegisterCommand(this, value);
                if (changed)
                    OnCommand2Changed();
            }
        }
        /// <summary>
        /// Called when Command property value changes.
        /// </summary>
        protected virtual void OnCommand2Changed()
        {
        }

        private object _Command2Parameter = null;
        /// <summary>
        /// Gets or sets user defined data value that can be passed to the command when it is executed.
        /// </summary>
        [Browsable(true), DefaultValue(null), Category("Commands"), Description("Indicates user defined data value that can be passed to the command when it is executed."), System.ComponentModel.TypeConverter(typeof(System.ComponentModel.StringConverter)), System.ComponentModel.Localizable(true)]
        public virtual object Command2Parameter
        {
            get
            {
                return _Command2Parameter;
            }
            set
            {
                _Command2Parameter = value;
            }
        }

        private eDualButtonPart _IsSelected = eDualButtonPart.None;
        /// <summary>
        /// Gets or sets the selected part of button.
        /// </summary>
        [DefaultValue(eDualButtonPart.None), Category("Behavior"), Description("Indicates selected part of button.")]
        public eDualButtonPart IsSelected
        {
            get { return _IsSelected; }
            set
            {
                if (value != _IsSelected)
                {
                    eDualButtonPart oldValue = _IsSelected;
                    _IsSelected = value;
                    OnIsSelectedChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when IsSelected property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnIsSelectedChanged(eDualButtonPart oldValue, eDualButtonPart newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("IsSelected"));

        }

        private Font _Font = null;
        /// <summary>
        /// Gets or sets the text font.
        /// </summary>
        [DefaultValue(null), Category("Appearance"), Description("Gets or sets the text font.")]
        public Font Font
        {
            get { return _Font; }
            set
            {
                if (value != _Font)
                {
                    Font oldValue = _Font;
                    _Font = value;
                    OnFontChanged(oldValue, value);
                }
            }
        }
        /// <summary>
        /// Called when Font property has changed.
        /// </summary>
        /// <param name="oldValue">Old property value</param>
        /// <param name="newValue">New property value</param>
        protected virtual void OnFontChanged(Font oldValue, Font newValue)
        {
            //OnPropertyChanged(new PropertyChangedEventArgs("Font"));
            
        }
        #endregion

        #region ExtraCommandSource
        public class ExtraCommandSource : ICommandSource
        {
            public ExtraCommandSource(ICommand command, object commandParam)
            {
                _Command = command;
                _CommandParameter = commandParam;
            }

            #region ICommandSource Members
            private ICommand _Command;
            public ICommand Command
            {
                get
                {
                    return _Command;
                }
                set
                {
                    _Command = value;
                }
            }
            private object _CommandParameter = null;
            public object CommandParameter
            {
                get
                {
                    return _CommandParameter;
                }
                set
                {
                    _CommandParameter = value;
                }
            }
            #endregion

            #endregion
        }
    }

    public enum eDualButtonPart
    {
        None,
        Up,
        Down
    }
}
