using System;

namespace WindowsFormsAero.InteropServices
{
    internal static class WindowMessages
    {
        internal const int WM_ACTIVATE = 0x0006;
        internal const int WM_SETREDRAW = 0x000B;
        internal const int WM_NOTIFY = 0x004e;
        internal const int WM_NCCALCSIZE = 0x0083;
        internal const int WM_NCHITTEST = 0x0084;

        internal const int WM_KEYDOWN = 0x0100;
        internal const int WM_KEYUP = 0x0101;
        internal const int WM_SYSKEYDOWN = 0x0104;
        internal const int WM_SYSKEYUP = 0x0105;

        internal const int WM_MOUSEFIRST = 0x0200;
        internal const int WM_MOUSEMOVE = 0x0200;
        internal const int WM_LBUTTONDOWN = 0x0201;
        internal const int WM_LBUTTONUP = 0x0202;
        internal const int WM_LBUTTONDBLCLK = 0x0203;
        internal const int WM_RBUTTONDOWN = 0x0204;
        internal const int WM_RBUTTONUP = 0x0205;
        internal const int WM_RBUTTONDBLCLK = 0x0206;
        internal const int WM_MBUTTONDOWN = 0x0207;
        internal const int WM_MBUTTONUP = 0x0208;
        internal const int WM_MBUTTONDBLCLK = 0x0209;
        internal const int WM_MOUSEWHEEL = 0x020A;
        internal const int WM_XBUTTONDOWN = 0x020B;
        internal const int WM_XBUTTONUP = 0x020C;
        internal const int WM_XBUTTONDBLCLK = 0x020D;
        internal const int WM_MOUSEHWHEEL = 0x020E;
        internal const int WM_MOUSELAST = 0x020E;
        internal const int WM_CAPTURECHANGED = 0x215;

        internal const int WM_DWMCOMPOSITIONCHANGED = 0x031E;
        internal const int WM_DWMNCRENDERINGCHANGED = 0x031F;
        internal const int WM_DWMCOLORIZATIONCOLORCHANGED = 0x0320;
        internal const int WM_DWMWINDOWMAXIMIZEDCHANGE = 0x0321;

        internal const int WM_USER = 0x0400;
        internal const int WM_REFLECT = 0x2000;
        //
        // Button Messages
        //

        internal const uint BCM_FIRST = 0x1600;
        internal const uint BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        //
        // Progress Bar Messages
        //

        internal const int PBM_SETSTATE = WM_USER + 16;

        //
        // Edit Control Messages
        //

        internal const int ECM_FIRST = 0x1500;
        internal const int EM_SETCUEBANNER = ECM_FIRST + 1;
        internal const int EM_GETRECT = 0x00B2;
        internal const int EM_GETFIRSTVISIBLELINE = 0x00CE;
        internal const int EM_GETEVENTMASK = (WM_USER + 59);
        internal const int EM_GETOLEINTERFACE = (WM_USER + 60);
        internal const int EM_SETEVENTMASK = (WM_USER + 69);

        //
        // List View Messages
        //

        internal const int LVM_FIRST = 0x1000;
        internal const int LVM_SETEXTENDEDLISTVIEWSTYLE = LVM_FIRST + 54;

        //
        // Tool Tip Control Messages
        //

        internal const int TTM_ACTIVATE = (WM_USER + 1);
        internal const int TTM_SETDELAYTIME = (WM_USER + 3);
        internal const int TTM_ADDTOOLW = (WM_USER + 50);
        internal const int TTM_DELTOOLW = (WM_USER + 51);
        internal const int TTM_NEWTOOLRECTW = (WM_USER + 52);
        internal const int TTM_RELAYEVENT = (WM_USER + 7);
        internal const int TTM_GETTOOLINFO = (WM_USER + 53);
        internal const int TTM_SETTOOLINFO = (WM_USER + 54);
        internal const int TTM_HITTEST = (WM_USER + 55);
        internal const int TTM_GETTEXT = (WM_USER + 56);
        internal const int TTM_UPDATETIPTEXT = (WM_USER + 57);
        internal const int TTM_GETTOOLCOUNT = (WM_USER + 13);
        internal const int TTM_ENUMTOOLS = (WM_USER + 58);
        internal const int TTM_GETCURRENTTOOL = (WM_USER + 59);
        internal const int TTM_WINDOWFROMPOINT = (WM_USER + 16);
        internal const int TTM_TRACKACTIVATE = (WM_USER + 17);  // wParam = TRUE/FALSE start end  lparam = LPTOOLINFO
        internal const int TTM_TRACKPOSITION = (WM_USER + 18);  // lParam = dwPos
        internal const int TTM_SETTIPBKCOLOR = (WM_USER + 19);
        internal const int TTM_SETTIPTEXTCOLOR = (WM_USER + 20);
        internal const int TTM_GETDELAYTIME = (WM_USER + 21);
        internal const int TTM_GETTIPBKCOLOR = (WM_USER + 22);
        internal const int TTM_GETTIPTEXTCOLOR = (WM_USER + 23);
        internal const int TTM_SETMAXTIPWIDTH = (WM_USER + 24);
        internal const int TTM_GETMAXTIPWIDTH = (WM_USER + 25);
        internal const int TTM_SETMARGIN = (WM_USER + 26);  // lParam = lprc
        internal const int TTM_GETMARGIN = (WM_USER + 27);  // lParam = lprc
        internal const int TTM_POP = (WM_USER + 28);
        internal const int TTM_UPDATE = (WM_USER + 29);
        internal const int TTM_GETBUBBLESIZE = (WM_USER + 30);
        internal const int TTM_ADJUSTRECT = (WM_USER + 31);
        internal const int TTM_SETTITLE = (WM_USER + 33);  // wParam = TTI_*, lParam = wchar* szTitle
        internal const int TTM_POPUP = (WM_USER + 34);
        internal const int TTM_GETTITLE = (WM_USER + 35); // wParam = 0, lParam = TTGETTITLE*

    }
}