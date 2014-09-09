using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace DevComponents.SuperGrid.Design
{
    [ToolboxItem(false)]
    public class ImageListBox : ListBox
    {
        #region Private variables

        private ImageList _ImageList;

        #endregion

        public ImageListBox()
        {
            MinimumSize = new Size(20, 20);
            IntegralHeight = true;

            DrawMode = DrawMode.OwnerDrawVariable;
        }

        #region Public properties

        public ImageList ImageList
        {
            get { return (_ImageList); }
            set { _ImageList = value; }
        }

        #endregion

        #region OnMeasureItem

        protected override void OnMeasureItem(MeasureItemEventArgs e)
        {
            base.OnMeasureItem(e);

            e.ItemWidth += 22;

            if (e.ItemHeight < 20)
                e.ItemHeight = 20;
        }

        #endregion

        #region OnDrawItem

        protected override void OnDrawItem(DrawItemEventArgs e)
        {
            Graphics g = e.Graphics;

            e.DrawBackground();

            if (e.Index < Items.Count)
            {
                Rectangle r = e.Bounds;
                r.Width = 18;

                if (r.Height > 18)
                {
                    r.Y += (r.Height - 18)/2;
                    r.Height = 18;
                }

                ImageListBoxItem item = (ImageListBoxItem) Items[e.Index];

                Image image = (uint) item.ImageIndex < _ImageList.Images.Count
                                  ? _ImageList.Images[item.ImageIndex]
                                  : null;

                if (image != null)
                    g.DrawImage(image, r);

                g.DrawRectangle(Pens.Black, r);

                using (Brush br = new SolidBrush(e.ForeColor))
                {
                    using (StringFormat sf = new StringFormat())
                    {
                        sf.Alignment = StringAlignment.Near;
                        sf.LineAlignment = StringAlignment.Center;

                        r = e.Bounds;
                        r.X += 30;
                        r.Width -= 30;

                        g.DrawString(item.Text, e.Font, br, r, sf);
                    }
                }
            }
        }

        #endregion
    }

    public class ImageListBoxItem
    {
        #region Private variables

        private string _Text;
        private int _ImageIndex;

        #endregion

        #region Constructors

        public ImageListBoxItem(string text, int index)
        {
            Text = text;
            ImageIndex = index;
        }

        public ImageListBoxItem(string text)
            : this(text, -1)
        {
        }

        public ImageListBoxItem()
            : this("", -1)
        {
        }

        #endregion

        #region Public properties

        public int ImageIndex
        {
            get { return (_ImageIndex); }
            set { _ImageIndex = value; }
        }

        public string Text
        {
            get { return (_Text); }
            set { _Text = value; }
        }

        #endregion
    }
}
