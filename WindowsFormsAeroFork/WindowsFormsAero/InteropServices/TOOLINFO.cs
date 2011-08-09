using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class TOOLINFO
    {
        private readonly int cbSize = Marshal.SizeOf(typeof(TOOLINFO));

        private ToolTipFlags uFlags;
        private IntPtr hwnd;
        private IntPtr uId;
        private RECT rect;
        private IntPtr hinst;
        [MarshalAs(UnmanagedType.LPWStr)]
        private String lpszText;
        private IntPtr lParam;
        private IntPtr lpReserved;

        public IntPtr WindowHandle
        {
            get { return hwnd; }
            set { hwnd = value; }
        }

        public IntPtr Id
        {
            get { return uId; }
            set { uId = value; }
        }

        public String Text
        {
            get { return lpszText; }
            set { lpszText = value; }
        }

        public RECT Rect
        {
            get { return rect; }
            set { rect = value; }
        }

        public ToolTipFlags Flags
        {
            get { return uFlags; }
            set { uFlags = value; }
        }
    }
}
