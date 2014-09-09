using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro.ColorTables;
using System.Drawing;

namespace DevComponents.DotNetBar.Metro.Rendering
{
    public static class MetroRender
    {
        private static MetroRenderer[] _Renderers;
        static MetroRender()
        {
            _Renderers = new MetroRenderer[Enum.GetNames(typeof(Renderers)).Length];
            _Renderers[(int)Renderers.MetroForm] = new MetroFormRenderer();
            _Renderers[(int)Renderers.MetroTabItem] = new MetroTabItemPainter();
            _Renderers[(int)Renderers.MetroTabStrip] = new MetroTabStripPainter();
            _Renderers[(int)Renderers.MetroStatusBar] = new MetroStatusBarPainter();
            _Renderers[(int)Renderers.MetroToolbar] = new MetroToolbarPainter();
            _Renderers[(int)Renderers.MetroTileItem] = new MetroTileItemPainter();
        }

        /// <summary>
        /// Renders the MetroTileItem.
        /// </summary>
        /// <param name="item">MetroTileItem to render.</param>
        /// <param name="e">Rendering event arguments.</param>
        public static void Paint(MetroTileItem item, ItemPaintArgs pa)
        {
            MetroRenderer renderer = GetRenderer(Renderers.MetroTileItem);
            renderer.Render(GetRenderingInfo(item, pa, GetColorTable()));
        }

        /// <summary>
        /// Renders the MetroForm.
        /// </summary>
        /// <param name="form">Form to render.</param>
        /// <param name="e">Rendering event arguments.</param>
        public static void Paint(MetroAppForm form, PaintEventArgs e)
        {
            MetroRenderer renderer = GetRenderer(Renderers.MetroForm);
            renderer.Render(GetRenderingInfo(form, e, GetColorTable()));
        }

        /// <summary>
        /// Renders the MetroForm.
        /// </summary>
        /// <param name="formOverlay">Form to render.</param>
        /// <param name="e">Rendering event arguments.</param>
        internal static void Paint(BorderOverlay formOverlay, PaintEventArgs e)
        {
            MetroRenderer renderer = GetRenderer(Renderers.MetroForm);
            renderer.Render(GetRenderingInfo(formOverlay, e, GetColorTable()));
        }

        /// <summary>
        /// Renders the MetroTabItem.
        /// </summary>
        /// <param name="tab">Form to render.</param>
        /// <param name="e">Rendering event arguments.</param>
        public static void Paint(MetroTabItem tab, ItemPaintArgs pa)
        {
            MetroRenderer renderer = GetRenderer(Renderers.MetroTabItem);
            renderer.Render(GetRenderingInfo(tab, pa, GetColorTable()));
        }

        /// <summary>
        /// Renders the MetroTabStrip
        /// </summary>
        /// <param name="tabStrip">TabStrip to render.</param>
        /// <param name="pa">Paint args</param>
        public static void Paint(MetroTabStrip tabStrip, ItemPaintArgs pa)
        {
            MetroRenderer renderer = GetRenderer(Renderers.MetroTabStrip);
            renderer.Render(GetRenderingInfo(tabStrip, pa, GetColorTable()));
        }
        /// <summary>
        /// Renders the MetroStatusBar.
        /// </summary>
        /// <param name="bar">Status bar to render</param>
        /// <param name="pa">Paint args</param>
        public static void Paint(MetroStatusBar bar, ItemPaintArgs pa)
        {
            MetroRenderer renderer = GetRenderer(Renderers.MetroStatusBar);
            renderer.Render(GetRenderingInfo(bar, pa, GetColorTable()));
        }

        /// <summary>
        /// Renders the MetroStatusBar.
        /// </summary>
        /// <param name="bar">Status bar to render</param>
        /// <param name="pa">Paint args</param>
        public static void Paint(MetroToolbar bar, ItemPaintArgs pa)
        {
            MetroRenderer renderer = GetRenderer(Renderers.MetroToolbar);
            renderer.Render(GetRenderingInfo(bar, pa, GetColorTable()));
        }

        private static MetroRenderer GetRenderer(Renderers renderer)
        {
            return _Renderers[(int)renderer];
        }

        private static MetroRendererInfo _RenderingInfo = new MetroRendererInfo();
        private static MetroRendererInfo GetRenderingInfo(Control control, PaintEventArgs e, MetroColorTable colorTable)
        {
            _RenderingInfo.Control = control;
            _RenderingInfo.PaintEventArgs = e;
            _RenderingInfo.ColorTable = colorTable;
            _RenderingInfo.DefaultFont = control.Font;
            _RenderingInfo.RightToLeft = (control.RightToLeft == RightToLeft.Yes);
            return _RenderingInfo;
        }
        private static MetroRendererInfo GetRenderingInfo(Control control, ItemPaintArgs e, MetroColorTable colorTable)
        {
            _RenderingInfo.Control = control;
            _RenderingInfo.PaintEventArgs = e.PaintEventArgs;
            _RenderingInfo.DefaultFont = e.Font;
            _RenderingInfo.ColorTable = colorTable;
            _RenderingInfo.RightToLeft = e.RightToLeft;
            _RenderingInfo.ItemPaintArgs = e;
            return _RenderingInfo;
        }
        private static MetroRendererInfo GetRenderingInfo(object control, ItemPaintArgs e, MetroColorTable colorTable)
        {
            _RenderingInfo.Control = control;
            _RenderingInfo.PaintEventArgs = e.PaintEventArgs;
            _RenderingInfo.DefaultFont = e.Font;
            _RenderingInfo.ColorTable = colorTable;
            _RenderingInfo.RightToLeft = e.RightToLeft;
            _RenderingInfo.ItemPaintArgs = e;
            return _RenderingInfo;
        }

        private static MetroColorTable _ColorTable = null;
        public static MetroColorTable GetColorTable()
        {
            if (_ColorTable == null)
            {
                UpdateColorTable(MetroColorGeneratorParameters.Default);
            }
            return _ColorTable;
        }

        internal static void UpdateColorTable(MetroColorGeneratorParameters colorParams)
        {
            //GetColorTable();
            _ColorTable = MetroColorTableInitializer.CreateColorTable(colorParams);
        }
        
    }

    /// <summary>
    /// Defines class for passing rendering information to renderer.
    /// </summary>
    public class MetroRendererInfo
    {
        /// <summary>
        /// Gets or sets the control to render.
        /// </summary>
        public object Control = null;
        /// <summary>
        /// Gets or sets the paint event arguments to use to render out control.
        /// </summary>
        public PaintEventArgs PaintEventArgs = null;
        /// <summary>
        /// Gets or sets the current color table.
        /// </summary>
        public MetroColorTable ColorTable;
        /// <summary>
        /// Gets or sets default font.
        /// </summary>
        public Font DefaultFont = SystemFonts.DefaultFont;
        /// <summary>
        /// Gets or sets right-to-left setting.
        /// </summary>
        public bool RightToLeft = false;
        /// <summary>
        /// Gets or sets the paint information for items.
        /// </summary>
        public ItemPaintArgs ItemPaintArgs = null;
    }
    /// <summary>
    /// Abstract renderer for rendering Metro-UI controls.
    /// </summary>
    public abstract class MetroRenderer
    {
        
        /// <summary>
        /// Renders the 
        /// </summary>
        /// <param name="renderingInfo"></param>
        public abstract void Render(MetroRendererInfo renderingInfo);
    }

    public enum Renderers : int
    {
        MetroForm,
        MetroTabItem,
        MetroTabStrip,
        MetroStatusBar,
        MetroToolbar,
        MetroTileItem
    }
}
