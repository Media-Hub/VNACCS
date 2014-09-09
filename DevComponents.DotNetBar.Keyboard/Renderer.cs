using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing.Drawing2D;

namespace DevComponents.DotNetBar.Keyboard
{
    /// <summary>
    /// Represents a renderer for a VirtualKeyboard.
    /// </summary>
    public abstract class Renderer
    {
        private VirtualKeyboardColorTable _ColorTable;
        /// <summary>
        /// Gets or sets the ColorTable used by this renderer to render the virtual keyboard.
        /// </summary>
        public VirtualKeyboardColorTable ColorTable
        {
            get { return _ColorTable; }
            set { _ColorTable = value; }
        }

        /// <summary>
        /// Draws the top bar of the VirtualKeyboard.
        /// </summary>
        /// <param name="args"></param>
        public abstract void DrawTopBar(TopBarRendererEventArgs args);


        public abstract void DrawCloseButton(CloseButtonRendererEventArgs args);

        /// <summary>
        /// Draws the background of the VirtualKeyboard.
        /// </summary>
        /// <param name="args">Provides context information.</param>
        public abstract void DrawBackground(BackgroundRendererEventArgs args);

        /// <summary>
        /// Draws a key of the VirtualKeyboard.
        /// </summary>
        /// <param name="args">Provides context information.</param>
        public abstract void DrawKey(KeyRendererEventArgs args);

        /// <summary>
        /// Called when the control starts rendering a new frame.
        /// </summary>
        /// <param name="args">Provides context information.</param>
        public virtual void BeginFrame(BeginFrameRendererEventArgs args) { }

        /// <summary>
        /// Called after the control finished rendering the frame.
        /// </summary>
        public virtual void EndFrame() { }
    }


    /// <summary>
    /// Represents a VirtualKeyboard renderer with a flat style.
    /// </summary>
    public class FlatStyleRenderer : Renderer
    {
        // The ratio between the key size and the font that displays the letters on the keys.
        private const float _KeyFontSizeRatio = 4;

        // The ratio between the key size and the font that displays the hints (e.g. Underline, Italic) on the keys.
        private const float _HintFontSizeRatio = 7;

        private StringFormat _CenteredTextStringFormat;
        private StringFormat _TopCenteredTextStringFormat;
        private float _ScaleFactorX;
        private float _ScaleFactorY;
        private Font _KeyFont;
        private Font _HintFont;
        private Rectangle _KeysBounds;

        private static VirtualKeyboardColorTable _DefaultColorTable = new VirtualKeyboardColorTable();


        /// <summary>
        /// Gets or sets whether anti-alias smoothing is used while painting. Default value is true.
        /// </summary>
        public bool ForceAntiAlias { get; set; }


        /// <summary>
        /// Starts rendering a new frame with the black style. This gives the renderer a chance to cash some resources.
        /// </summary>
        public override void BeginFrame(BeginFrameRendererEventArgs args)
        {
            _CenteredTextStringFormat = new StringFormat();
            _CenteredTextStringFormat.Alignment = StringAlignment.Center;
            _CenteredTextStringFormat.LineAlignment = StringAlignment.Center;

            _TopCenteredTextStringFormat = new StringFormat();
            _TopCenteredTextStringFormat.Alignment = StringAlignment.Center;
            _TopCenteredTextStringFormat.LineAlignment = StringAlignment.Near;

            // With this fx and fy we convert between logical units and pixels.
            _ScaleFactorX = (float)args.Bounds.Width / args.Layout.LogicalWidth;
            _ScaleFactorY = (float)args.Bounds.Height / args.Layout.LogicalHeight;

            _KeysBounds = args.Bounds;

            float min = Math.Min(_ScaleFactorX, _ScaleFactorY);

            float keyFontSize = min * LinearKeyboardLayout.NormalKeySize / _KeyFontSizeRatio;
            float hintFontSize = min * LinearKeyboardLayout.NormalKeySize / _HintFontSizeRatio;

            if (keyFontSize < 1)
                keyFontSize = 1;
            if (hintFontSize < 1)
                hintFontSize = 1;

            _KeyFont = new Font(args.Font.FontFamily, keyFontSize);
            _HintFont = new Font(args.Font.FontFamily, hintFontSize);

            if (ForceAntiAlias)
                args.Graphics.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        }


        /// <summary>
        /// Ends rendering a new frame with the black style. This gives the renderer a chance to free resources used during the frame.
        /// </summary>
        public override void EndFrame()
        {
            if (_CenteredTextStringFormat != null)
                _CenteredTextStringFormat.Dispose();

            if (_TopCenteredTextStringFormat != null)
                _TopCenteredTextStringFormat.Dispose();

            if (_KeyFont != null)
                _KeyFont.Dispose();

            if (_HintFont != null)
                _HintFont.Dispose();
        }


        /// <summary>
        /// Creates a new renderer for the black style.
        /// </summary>
        public FlatStyleRenderer()
        {
            ColorTable = _DefaultColorTable;
        }


        /// <summary>
        /// Draws the background of a VirtualKeyboard.
        /// </summary>
        /// <param name="args">Provides context information.</param>
        public override void DrawBackground(BackgroundRendererEventArgs args)
        {
            if (ColorTable != null)
                args.Graphics.FillRectangle(ColorTable.BackgroundBrush, args.ClipRectangle);
        }


        internal static void DrawBackspaceSign(Graphics g, Rectangle rect, Brush background, Brush foreground)
        {
            SmoothingMode osm = g.SmoothingMode; // old smoothing mode, to restore it at the end.
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int unit = Math.Min(rect.Width, rect.Height);

            Point[] points = new Point[]
            {
                new Point(-2 * unit, 0),
                new Point(-1 * unit, -1 * unit),
                new Point(2 * unit, -1 * unit),
                new Point(2 * unit, 1 * unit),
                new Point(-1 * unit, 1 * unit),
                new Point(-2 * unit, 0)
            };

            g.TranslateTransform(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);
            g.ScaleTransform(0.125f, 0.125f);

            g.FillPolygon(foreground, points);

            g.ScaleTransform(8, 8);


            g.ScaleTransform(0.125f, 0.125f);
            g.TranslateTransform(unit / 2, 0);

            using (Pen p = new Pen(background, unit * 8 / 20))
            {
                g.DrawLine(p, -unit / 2, -unit / 2, unit / 2, unit / 2);
                g.DrawLine(p, -unit / 2, unit / 2, unit / 2, -unit / 2);
            }

            g.TranslateTransform(-unit / 2, 0);
            g.ScaleTransform(8, 8);

            g.TranslateTransform(-(rect.Left + rect.Width / 2), -(rect.Top + rect.Height / 2));
            g.SmoothingMode = osm;
        }


        internal static void DrawSmileySign(Graphics g, Rectangle rect, Brush background, Brush foreground)
        {
            SmoothingMode osm = g.SmoothingMode; // old smoothing mode, to restore it at the end.
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int unit = Math.Min(rect.Width, rect.Height) * 2 / 5;
            float u = unit / 20f;

            PointF[] points = new PointF[]
            {
                new PointF(-5, 0),
                new PointF(-7, 1),
                new PointF(-6, 5),
                new PointF(-2, 8),
                new PointF(2, 8),
                new PointF(6, 5),
                new PointF(7, 1),
                new PointF(5, 0),
                new PointF(0, 2)
            };

            for (int i = 0; i < points.Length; i++)
            {
                points[i].X *= u;
                points[i].Y *= u;
            }

            g.TranslateTransform(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

            g.FillEllipse(foreground, new Rectangle(-unit / 2, -unit / 2, unit, unit));

            g.FillClosedCurve(background, points, System.Drawing.Drawing2D.FillMode.Winding, 0.5f);

            g.FillEllipse(background, new Rectangle((int)(-7 * u), (int)(-7 * u), (int)(5 * u), (int)(5 * u)));
            g.FillEllipse(background, new Rectangle((int)(2 * u), (int)(-7 * u), (int)(5 * u), (int)(5 * u)));

            g.TranslateTransform(-(rect.Left + rect.Width / 2), -(rect.Top + rect.Height / 2));

            g.SmoothingMode = osm;
        }


        internal static void DrawUpArrow(Graphics g, Rectangle rect, Brush background, Brush foreground)
        {
            SmoothingMode osm = g.SmoothingMode; // old smoothing mode, to restore it at the end.
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int unit = Math.Min(rect.Width, rect.Height) / 2 / 4;

            g.TranslateTransform(rect.Left + rect.Width / 2, rect.Top + rect.Height / 2);

            using (Pen p = new Pen(foreground, unit / 2f))
            {
                g.DrawLine(p, 0, -unit * 1.5f, 0, unit * 1.5f);
                g.DrawLine(p, 0, -unit * 1.5f, -unit, 0);
                g.DrawLine(p, 0, -unit * 1.5f, unit, 0);
            }

            g.TranslateTransform(-(rect.Left + rect.Width / 2), -(rect.Top + rect.Height / 2));

            g.SmoothingMode = osm;
        }



        /// <summary>
        /// Draws the key of a VirtualKeyboard.
        /// </summary>
        /// <param name="args">Provides context information.</param>
        public override void DrawKey(KeyRendererEventArgs args)
        {
            Key key = args.Key;

            if (ColorTable != null)
            {
                Rectangle rect = args.Bounds;

                if (args.ClipRectangle.IntersectsWith(rect))
                {
                    Brush keyBrush = ColorTable.KeysBrush;
                    if (key.Style == KeyStyle.Dark)
                        keyBrush = ColorTable.DarkKeysBrush;
                    else if (key.Style == KeyStyle.Light)
                        keyBrush = ColorTable.LightKeysBrush;
                    else if (key.Style == KeyStyle.Pressed)
                        keyBrush = ColorTable.PressedKeysBrush;
                    else if (key.Style == KeyStyle.Toggled)
                        keyBrush = ColorTable.KeysBrush;

                    if (args.IsDown)
                        keyBrush = ColorTable.DownKeysBrush;

                    Brush textBrush = ColorTable.TextBrush;
                    if (key.Style == KeyStyle.Toggled)
                        textBrush = ColorTable.ToggleTextBrush;
                    if (args.IsDown)
                        textBrush = ColorTable.DownTextBrush;

                    args.Graphics.FillRectangle(keyBrush, rect);


                    if (key.Info == "{BACKSPACE}")
                    {
                        DrawBackspaceSign(args.Graphics, rect, keyBrush, textBrush);
                    }
                    else if (key.Caption == ":-)")
                    {
                        DrawSmileySign(args.Graphics, rect, keyBrush, textBrush);
                    }
                    else if (key.Caption == "Shift")
                    {
                        DrawUpArrow(args.Graphics, rect, keyBrush, textBrush);
                    }
                    else
                    {
                        args.Graphics.DrawString(key.Caption, _KeyFont, textBrush, rect, _CenteredTextStringFormat);

                        if (key.Hint != null)
                        {
                            args.Graphics.DrawString(key.Hint, _HintFont, textBrush, rect, _TopCenteredTextStringFormat);
                        }
                    }
                }
            }
        }


        public override void DrawCloseButton(CloseButtonRendererEventArgs args)
        {
            Rectangle rect = args.Bounds;
            rect.Inflate(-4, -4);
            if (args.IsDown)
            {
                args.Graphics.FillRectangle(ColorTable.DownKeysBrush, args.Bounds);
                using (Pen p = new Pen(ColorTable.DownTextBrush, 4))
                {
                    args.Graphics.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Bottom);
                    args.Graphics.DrawLine(p, rect.Left, rect.Bottom, rect.Right, rect.Top);
                }
            }
            else
            {
                //args.Graphics.FillRectangle(ColorTable.KeysBrush, args.Bounds);
                using (Pen p = new Pen(ColorTable.TextBrush, 4))
                {
                    args.Graphics.DrawLine(p, rect.Left, rect.Top, rect.Right, rect.Bottom);
                    args.Graphics.DrawLine(p, rect.Left, rect.Bottom, rect.Right, rect.Top);
                }
            }

        }


        public override void DrawTopBar(TopBarRendererEventArgs args)
        {
            args.Graphics.DrawString(args.Text, args.Font, ColorTable.TopBarTextBrush, args.Bounds);
        }
    }


    /// <summary>
    /// Provides data for Key rendering.
    /// </summary>
    public class KeyRendererEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a reference to the Key instance being rendered.
        /// </summary>
        public Key Key { get; private set; }

        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Gets a reference to the Graphics object to be used for rendering.
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets a value indicating if this key is currently down.
        /// </summary>
        public bool IsDown { get; private set; }

        /// <summary>
        /// Gets the rectangle in which to paint.
        /// </summary>
        public Rectangle ClipRectangle { get; private set; }

        /// <summary>
        /// Creates a new instance and initializes it with provided values.
        /// </summary>
        /// <param name="graphics">The Graphics object to be used for rendering.</param>
        /// <param name="key">The Key instance being rendered.</param>
        /// <param name="isDown">Indicates if the key is currently hold down.</param>
        /// <param name="clipRectangle">The rectangle in which to paint.</param>
        public KeyRendererEventArgs(Graphics graphics, Key key, bool isDown, Rectangle clipRectangle, Rectangle bounds)
        {
            Key = key;
            Graphics = graphics;
            IsDown = isDown;
            ClipRectangle = clipRectangle;
            Bounds = bounds;
        }
    }


    /// <summary>
    /// Provides data for rendering the background of a VirtualKeyboard.
    /// </summary>
    public class BackgroundRendererEventArgs : EventArgs
    {
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Gets a reference to the Graphics object to be used for rendering.
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets the rectangle in which to paint.
        /// </summary>
        public Rectangle ClipRectangle { get; private set; }

        /// <summary>
        /// Creates a new instance and initializes it with provided values.
        /// </summary>
        /// <param name="graphics">The Graphics object to be used for rendering.</param>
        /// <param name="size">The area where the background should be drawn.</param>
        public BackgroundRendererEventArgs(Graphics graphics, Rectangle clipRectangle, Rectangle bounds)
        {
            Graphics = graphics;
            ClipRectangle = clipRectangle;
            Bounds = bounds;
        }
    }


    public class BeginFrameRendererEventArgs : EventArgs
    {
        /// <summary>
        /// Gets a reference to the Graphics object to be used for rendering.
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets the rectangle in which to paint.
        /// </summary>
        public Rectangle ClipRectangle { get; private set; }

        /// <summary>
        /// Gets the size of the background area in which to render the keyboard.
        /// </summary>
        //public Size Size { get; private set; }
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Gets the font on the control.
        /// </summary>
        public Font Font { get; private set; }

        /// <summary>
        /// Gets the current KeyboardLayout which will be rendered in this frame.
        /// </summary>
        public KeyboardLayout Layout { get; private set; }

        /// <summary>
        /// Creates a new instance and initializes it with provided values.
        /// </summary>
        /// <param name="size">The size of the background area.</param>
        /// <param name="font">The font of the control.</param>
        /// <param name="layout">The current KeyboardLayout that will be rendered in this frame.</param>
        public BeginFrameRendererEventArgs(Graphics graphics, Rectangle clipRectangle, Rectangle bounds, Font font, KeyboardLayout layout)
        {
            Graphics = graphics;
            ClipRectangle = clipRectangle;
            Bounds = bounds;
            Font = font;
            Layout = layout;
        }
    }


    public class TopBarRendererEventArgs : EventArgs
    {
        public string Text { get; private set; }

        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Gets a reference to the Graphics object to be used for rendering.
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets the rectangle in which to paint.
        /// </summary>
        public Rectangle ClipRectangle { get; private set; }

        /// <summary>
        /// Gets the font on the control.
        /// </summary>
        public Font Font { get; private set; }

        public TopBarRendererEventArgs(Graphics graphics, Rectangle clipRectangle, Rectangle bounds, string text, Font font)
        {
            Graphics = graphics;
            ClipRectangle = clipRectangle;
            Bounds = bounds;
            Text = text;
            Font = font;
        }
    }


    public class CloseButtonRendererEventArgs : EventArgs
    {
        public Rectangle Bounds { get; private set; }

        /// <summary>
        /// Gets a reference to the Key instance being rendered.
        /// </summary>
        public Graphics Graphics { get; private set; }

        /// <summary>
        /// Gets the area where background should be drawn.
        /// </summary>
        public Rectangle ClipRectangle { get; private set; }

        /// <summary>
        /// Gets a value indicating if this key is currently down.
        /// </summary>
        public bool IsDown { get; private set; }

        public CloseButtonRendererEventArgs(Graphics graphics, bool isDown, Rectangle clipRectangle, Rectangle bounds)
        {
            Graphics = graphics;
            ClipRectangle = clipRectangle;
            Bounds = bounds;
            IsDown = isDown;
        }
    }
}
