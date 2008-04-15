using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    internal sealed class TabStripTabListButton : TabStripButtonBase
    {
        private ToolStripDropDownMenu _dropdown;
        public TabStripTabListButton()
        {
            _dropdown = new ToolStripDropDownMenu();
        }

        protected override void OnClick(EventArgs e)
        {
            var pt = Bounds.Location;
            pt.Y += Height;

            _dropdown.Items.Add("xX'x");
            _dropdown.Show(Owner, pt, ToolStripDropDownDirection.BelowLeft);

            base.OnClick(e);
        }

        public override System.Drawing.Size GetPreferredSize(System.Drawing.Size constrainingSize)
        {
            return new System.Drawing.Size(17, constrainingSize.Height);
        }
    }
}
