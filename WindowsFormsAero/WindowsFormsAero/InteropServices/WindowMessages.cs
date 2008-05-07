using System;

namespace WindowsFormsAero.InteropServices
{
    internal static class WindowMessages
    {
        public const int WM_ACTIVATE = 0x0006;
        public const int WM_SETREDRAW = 0x000B;
        public const int WM_NCCALCSIZE = 0x0083;
        public const int WM_NCHITTEST = 0x0084;

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;

        public const int WM_MOUSEFIRST = 0x0200;
        public const int WM_MOUSEMOVE = 0x0200;
        public const int WM_LBUTTONDOWN = 0x0201;
        public const int WM_LBUTTONUP = 0x0202;
        public const int WM_LBUTTONDBLCLK = 0x0203;
        public const int WM_RBUTTONDOWN = 0x0204;
        public const int WM_RBUTTONUP = 0x0205;
        public const int WM_RBUTTONDBLCLK = 0x0206;
        public const int WM_MBUTTONDOWN = 0x0207;
        public const int WM_MBUTTONUP = 0x0208;
        public const int WM_MBUTTONDBLCLK = 0x0209;
        public const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_XBUTTONDOWN = 0x020B;
        public const int WM_XBUTTONUP = 0x020C;
        public const int WM_XBUTTONDBLCLK = 0x020D;
        public const int WM_MOUSEHWHEEL = 0x020E;
        public const int WM_MOUSELAST = 0x020E;
        public const int WM_CAPTURECHANGED = 0x215;

        public const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
        public const int WM_DWMNCRENDERINGCHANGED = 0x031F;
        public const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
        public const int WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321;

        public const int WM_USER = 0x0400;

        //
        // Button Messages
        //

        public const uint BCM_FIRST = 0x1600;
        public const uint BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        //
        // Progress Bar Messages
        //

        public const int PBM_SETSTATE = WM_USER + 16;

        //
        // Edit Control Messages
        //

        public const int ECM_FIRST = 0x1500;
        public const int EM_SETCUEBANNER = ECM_FIRST + 1;

        //
        // List View Messages
        //

        public const int LVM_FIRST = 0x1000;
        public const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;

        //
        // Tool Tip Control Messages
        //

        public const int TTM_ACTIVATE = (WM_USER + 1);
        public const int TTM_SETDELAYTIME = (WM_USER + 3);
        public const int TTM_ADDTOOLW = (WM_USER + 50);
        public const int TTM_DELTOOLW = (WM_USER + 51);
        public const int TTM_NEWTOOLRECTW = (WM_USER + 52);
        public const int TTM_RELAYEVENT = (WM_USER + 7);
        public const int TTM_GETTOOLINFO = (WM_USER + 53);
        public const int TTM_SETTOOLINFO = (WM_USER + 54);
        public const int TTM_HITTEST = (WM_USER + 55);
        public const int TTM_GETTEXT = (WM_USER + 56);
        public const int TTM_UPDATETIPTEXT = (WM_USER + 57);
        public const int TTM_GETTOOLCOUNT = (WM_USER + 13);
        public const int TTM_ENUMTOOLS = (WM_USER + 58);
        public const int TTM_GETCURRENTTOOL = (WM_USER + 59);
        public const int TTM_WINDOWFROMPOINT = (WM_USER + 16);
        public const int TTM_TRACKACTIVATE = (WM_USER + 17);  // wParam = TRUE/FALSE start end  lparam = LPTOOLINFO
        public const int TTM_TRACKPOSITION = (WM_USER + 18);  // lParam = dwPos
        public const int TTM_SETTIPBKCOLOR = (WM_USER + 19);
        public const int TTM_SETTIPTEXTCOLOR = (WM_USER + 20);
        public const int TTM_GETDELAYTIME = (WM_USER + 21);
        public const int TTM_GETTIPBKCOLOR = (WM_USER + 22);
        public const int TTM_GETTIPTEXTCOLOR = (WM_USER + 23);
        public const int TTM_SETMAXTIPWIDTH = (WM_USER + 24);
        public const int TTM_GETMAXTIPWIDTH = (WM_USER + 25);
        public const int TTM_SETMARGIN = (WM_USER + 26);  // lParam = lprc
        public const int TTM_GETMARGIN = (WM_USER + 27);  // lParam = lprc
        public const int TTM_POP = (WM_USER + 28);
        public const int TTM_UPDATE = (WM_USER + 29);
        public const int TTM_GETBUBBLESIZE = (WM_USER + 30);
        public const int TTM_ADJUSTRECT = (WM_USER + 31);
        public const int TTM_SETTITLE = (WM_USER + 33);  // wParam = TTI_*, lParam = wchar* szTitle
        public const int TTM_POPUP = (WM_USER + 34);
        public const int TTM_GETTITLE = (WM_USER + 35); // wParam = 0, lParam = TTGETTITLE*

    }
}