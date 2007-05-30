using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace VistaControls
{
    [ToolboxBitmap(typeof(TreeView))]
    public class TreeView:System.Windows.Forms.TreeView
    {
        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);
        public TreeView()
        {
            SetWindowTheme(this.Handle, "explorer", null);
            this.HotTracking = true;
            this.ShowLines = false;
            VistaConstants.SendMessage(this.Handle, VistaConstants.TVM_SETEXTENDEDSTYLE, 0, VistaConstants.TVS_EX_FADEINOUTEXPANDOS);
        }
    }
}
