namespace Naccs.Common.CustomControls
{
    using System;
    using System.ComponentModel;
    using System.Windows.Forms;

    public class BindingSourceEx : BindingSource
    {
        private RanLabel positionItem;

        public BindingSourceEx()
        {
            base.PositionChanged += new EventHandler(this.BindingSourceEx_PositionChanged);
        }

        public BindingSourceEx(IContainer container) : base(container)
        {
            base.PositionChanged += new EventHandler(this.BindingSourceEx_PositionChanged);
        }

        public BindingSourceEx(object dataSource, string dataMember) : base(dataSource, dataMember)
        {
            base.PositionChanged += new EventHandler(this.BindingSourceEx_PositionChanged);
        }

        private void BindingSourceEx_PositionChanged(object sender, EventArgs e)
        {
            if (this.positionItem != null)
            {
                this.positionItem.Value = base.Position + 1;
            }
        }

        public void OnPositionChangedEx(ListChangedEventArgs e)
        {
            this.OnPositionChanged(e);
        }

        public RanLabel PositionItem
        {
            get
            {
                return this.positionItem;
            }
            set
            {
                this.positionItem = value;
            }
        }
    }
}

