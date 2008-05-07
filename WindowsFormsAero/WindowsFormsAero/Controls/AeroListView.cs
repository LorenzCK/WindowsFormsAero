using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("Code")]
    [System.Drawing.ToolboxBitmap(typeof(ListView))]
    public class AeroListView : ListView
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            NativeMethods.SetWindowTheme(new HandleRef(this, Handle), "Explorer", null);

            NativeMethods.SendMessage(new HandleRef(this, Handle),
                WindowMessages.LVM_SETEXTENDEDLISTVIEWSTYLE,
                new IntPtr(WindowStyles.LVS_EX_DOUBLEBUFFER),
                new IntPtr(WindowStyles.LVS_EX_DOUBLEBUFFER));
        }
    }
}
