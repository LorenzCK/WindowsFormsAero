using System;
using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    internal sealed class TabStripTabListButton : TabStripButtonBase
    {
        private TabListDropDownMenu _dropdown;

        public TabStripTabListButton()
        {
            DisplayStyle = ToolStripItemDisplayStyle.Image;
        }

        public override Size GetPreferredSize(Size constrainingSize)
        {
            return new Size(17, constrainingSize.Height);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);

            if (disposing && _dropdown != null)
            {
                _dropdown.Dispose();
            }

            _dropdown = null;
        }

        protected override void OnClick(EventArgs e)
        {
            if (_dropdown == null)
            {
                _dropdown = new TabListDropDownMenu();
            }

            _dropdown.PopulateItems(Owner);
            _dropdown.Show(this);

            base.OnClick(e);
        }

        protected override void OnPaintImage(ToolStripItemImageRenderEventArgs e)
        {
            Renderer.DrawArrow(new ToolStripArrowRenderEventArgs(e.Graphics, e.Item, e.ImageRectangle, SystemColors.ControlText, ArrowDirection.Down));
        }

        internal override Size ImageSize
        {
            get { return new Size(5,3); }
        }

        private sealed class TabListDropDownMenu : ToolStripDropDownMenu
        {
            public void PopulateItems(TabStrip strip)
            {
                Items.Clear();

                foreach (var item in strip.Items)
                {
                    var tab = item as TabStripButton;

                    if (tab != null)
                    {
                        Items.Add(new TabListDropDownMenuItem(tab));
                    }
                }
            }

            public void Show(TabStripTabListButton owner)
            {
                var pt = owner.Bounds.Location;
                pt.Y += owner.Height;

                Show(owner.Owner, pt, ToolStripDropDownDirection.BelowRight);
            }
        }

        private sealed class TabListDropDownMenuItem : ToolStripMenuItem
        {
            private readonly TabStripButton _button;

            public TabListDropDownMenuItem(TabStripButton button)
                : base(button.Text)
            {
                _button = button;

                if (button == button.Owner.SelectedTab)
                {
                    Checked = true;
                    Font = new Font(Font, FontStyle.Bold);
                }
                else
                {
                    Image = button.Image;
                }
            }

            protected override void Dispose(bool disposing)
            {
                if (disposing && Checked)
                {
                    Font.Dispose();
                    Font = null;
                }

                base.Dispose(disposing);
            }
            protected override void OnClick(EventArgs e)
            {
                _button.Owner.SelectedTab = _button;
            }
        }
    }
}
