using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    [ToolboxItem(false)] // dont show up in the toolbox, this will be created by the Add TabStripPage verb on the TabPageSwitcherDesigner
    [Docking(DockingBehavior.Never)]  // dont ask about docking
    [System.ComponentModel.DesignerCategory("Code")] // dont bring up the component designer when opened
    public class TabStripPage : Panel
    {
        public TabStripPage()
        {
        }


        /// <summary>
        /// Bring this TabStripPage to the front of the switcher.
        /// </summary>
        public void Activate()
        {
            TabPageSwitcher tabPageSwitcher = this.Parent as TabPageSwitcher;
            if (tabPageSwitcher != null)
            {
                tabPageSwitcher.SelectedTabStripPage = this;
            }

        }
    }
}
