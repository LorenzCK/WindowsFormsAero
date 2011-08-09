using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace WindowsFormsAero
{
    public sealed class AeroTabControlTabListButton : AeroTabControlButton
    {
        public AeroTabControlTabListButton(TabStrip tabStrip) 
            : base(tabStrip)
        {
        }

        public override string Text
        {
            get { return TabStrip.TabListButtonText; }
            set { TabStrip.TabListButtonText = value; }
        }

        public override bool Visible
        {
            get { return TabStrip.TabListButtonVisible; }
            set { TabStrip.TabListButtonVisible = value; }
        }

        protected override Keys DefaultShortcutKeys
        {
            get { return Keys.None; }
        }

        protected override string DefaultText
        {
            get { return TabStrip.DefaultTabListButtonText; }
        }
    }
}
