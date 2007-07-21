using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace VistaControls
{
    [ToolboxBitmap(typeof(ListView))]
    public class ListView : System.Windows.Forms.ListView
    {
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
                        NativeMethods.SendMessage(this.Handle, NativeMethods.LVM_SETEXTENDEDLISTVIEWSTYLE, NativeMethods.LVS_EX_DOUBLEBUFFER, NativeMethods.LVS_EX_DOUBLEBUFFER); //Blue selection, keeps other extended styles
                        elv = true;
                    }
                    break;
            }
            base.WndProc(ref m);
        }
    }
}
