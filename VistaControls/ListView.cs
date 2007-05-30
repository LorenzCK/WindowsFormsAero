using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace VistaControls
{
    public class ListView:System.Windows.Forms.ListView
    {
        public ListView()
        {
        }
        //Imports the UXTheme DLL
        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);
        private Boolean elv = false;
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case 15:
                    //Paint event
                    if (!elv)
                    {
                        //1-time run needed
                        SetWindowTheme(this.Handle, "explorer", null); //Explorer style
                        VistaConstants.SendMessage(this.Handle, VistaConstants.LVM_SETEXTENDEDLISTVIEWSTYLE, VistaConstants.LVS_EX_DOUBLEBUFFER, VistaConstants.LVS_EX_DOUBLEBUFFER); //Blue selection, keeps other extended styles
                        elv = true;
                    }
                    break;
            }
            base.WndProc(ref m);
        }

    }
}
