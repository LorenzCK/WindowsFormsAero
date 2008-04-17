using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.ComponentModel;

namespace WindowsFormsAero
{
    public sealed class AeroTabControlNewTabButton : AeroTabControlButton
    {
        internal AeroTabControlNewTabButton(TabStrip tabStrip)
            : base(tabStrip)
        {
        }

        public override string Text
        {
            get { return TabStrip.NewTabButtonText; }
            set { TabStrip.NewTabButtonText = value; }
        }

        public override bool Visible
        {
            get { return TabStrip.NewTabButtonVisible; }
            set { TabStrip.NewTabButtonVisible = value; }
        }

        protected override string DefaultText
        {
            get { return TabStrip.DefaultNewTabButtonText; }
        }

        protected override Keys DefaultShortcutKeys
        {
            get { return Keys.Control | Keys.T; }
        }
    }
}
