using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    partial class TabStrip
    {        
        public sealed class TabStripAccessibleObject : ToolStrip.ToolStripAccessibleObject
        {
            //private TabStrip _owner;

            public TabStripAccessibleObject(TabStrip owner)
                : base(owner)
            {
            }

            public override AccessibleRole Role
            {
                get { return AccessibleRole.PageTabList; }
            }
        }
    }
}