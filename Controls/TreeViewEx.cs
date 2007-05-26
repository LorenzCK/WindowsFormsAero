/*
* VISTA CONTROLS FOR .NET 2.0
* ENHANCED TREEVIEW
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
* A copy of this license is available at
* http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedcommunitylicense.mspx
*/

using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace System.Windows.Form.Vista
{
    public class TreeViewEx : TreeView
    {
        #region Platform Invoke

        private const int TV_FIRST = 0x1100;
        private const int TVM_SETEXTENDEDSTYLE = TV_FIRST + 44;
        private const int TVM_GETEXTENDEDSTYLE = TV_FIRST + 45;
        private const int TVS_NOHSCROLL = 0x8000;
        private const int TVS_EX_AUTOHSCROLL = 0x0020;
        private const int TVS_EX_FADEINOUTEXPANDOS = 0x0040;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int SendMessage(IntPtr hWnd, UInt32 Msg, int wParam, int lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern void SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("uxtheme.dll", CharSet = CharSet.Unicode)]
        private extern static int SetWindowTheme(IntPtr hWnd, string pszSubAppName, string pszSubIdList);

        #endregion

        public TreeViewEx()
        {
            base.HotTracking = true;
            base.ShowLines = false;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= TVS_NOHSCROLL;
                return cp;
            }
        }

        public new bool HotTracking
        {
            get { return base.HotTracking; }
            set { base.HotTracking = true; }
        }

        public new bool ShowLines
        {
            get { return base.ShowLines; }
            set { base.ShowLines = true; }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            int style = SendMessage(base.Handle, TVM_GETEXTENDEDSTYLE, 0, 0);
            style |= (TVS_EX_AUTOHSCROLL | TVS_EX_FADEINOUTEXPANDOS);       
            SendMessage(base.Handle, TVM_SETEXTENDEDSTYLE, 0, style);
            SetWindowTheme(base.Handle, "explorer", null);
        }
    }
}