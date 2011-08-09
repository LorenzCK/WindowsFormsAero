using System;

namespace WindowsFormsAero.InteropServices
{
    internal sealed class WindowStyles
    {
        internal const int LVS_EX_FULLROWSELECT = 0x00000020;
        internal const int LVS_EX_DOUBLEBUFFER = 0x00010000;

        internal const int TTS_ALWAYSTIP = 0x01;
        internal const int TTS_NOPREFIX = 0x02;
        internal const int TTS_NOANIMATE = 0x10;
        internal const int TTS_NOFADE = 0x20;
        internal const int TTS_BALLOON = 0x40;
        internal const int TTS_CLOSE = 0x80;
        internal const int TTS_USEVISUALSTYLE = 0x100;

        internal const int PBS_SMOOTHREVERSE = 0x10;

        internal const int WS_OVERLAPPED = 0x00000000;
        internal const int WS_POPUP = unchecked((int)(0x80000000));
        internal const int WS_CHILD = 0x40000000;
        internal const int WS_MINIMIZE = 0x20000000;
        internal const int WS_VISIBLE = 0x10000000;
        internal const int WS_DISABLED = 0x08000000;
        internal const int WS_CLIPSIBLINGS = 0x04000000;
        internal const int WS_CLIPCHILDREN = 0x02000000;
        internal const int WS_MAXIMIZE = 0x01000000;
        internal const int WS_CAPTION = 0x00C00000;     /* WS_BORDER | WS_DLGFRAME  */
        internal const int WS_BORDER = 0x00800000;
        internal const int WS_DLGFRAME = 0x00400000;
        internal const int WS_VSCROLL = 0x00200000;
        internal const int WS_HSCROLL = 0x00100000;
        internal const int WS_SYSMENU = 0x00080000;
        internal const int WS_THICKFRAME = 0x00040000;
        internal const int WS_GROUP = 0x00020000;
        internal const int WS_TABSTOP = 0x00010000;

        internal const int WS_MINIMIZEBOX = 0x00020000;
        internal const int WS_MAXIMIZEBOX = 0x00010000;


        internal const int WS_TILED = WS_OVERLAPPED;
        internal const int WS_ICONIC = WS_MINIMIZE;
        internal const int WS_SIZEBOX = WS_THICKFRAME;
        internal const int WS_TILEDWINDOW = WS_OVERLAPPEDWINDOW;

        internal const int WS_OVERLAPPEDWINDOW = (WS_OVERLAPPED |
                                                WS_CAPTION |
                                                WS_SYSMENU |
                                                WS_THICKFRAME |
                                                WS_MINIMIZEBOX |
                                                WS_MAXIMIZEBOX);

        internal const int WS_POPUPWINDOW = (WS_POPUP | WS_BORDER | WS_SYSMENU);

        internal const int WS_CHILDWINDOW = (WS_CHILD);


        internal const int WS_EX_DLGMODALFRAME = 0x00000001;
        internal const int WS_EX_NOPARENTNOTIFY = 0x00000004;
        internal const int WS_EX_TOPMOST = 0x00000008;
        internal const int WS_EX_ACCEPTFILES = 0x00000010;
        internal const int WS_EX_TRANSPARENT = 0x00000020;

        internal const int WS_EX_MDICHILD = 0x00000040;
        internal const int WS_EX_TOOLWINDOW = 0x00000080;
        internal const int WS_EX_WINDOWEDGE = 0x00000100;
        internal const int WS_EX_CLIENTEDGE = 0x00000200;
        internal const int WS_EX_CONTEXTHELP = 0x00000400;




        internal const int WS_EX_RIGHT = 0x00001000;
        internal const int WS_EX_LEFT = 0x00000000;
        internal const int WS_EX_RTLREADING = 0x00002000;
        internal const int WS_EX_LTRREADING = 0x00000000;
        internal const int WS_EX_LEFTSCROLLBAR = 0x00004000;
        internal const int WS_EX_RIGHTSCROLLBAR = 0x00000000;

        internal const int WS_EX_CONTROLPARENT = 0x00010000;
        internal const int WS_EX_STATICEDGE = 0x00020000;
        internal const int WS_EX_APPWINDOW = 0x00040000;


        internal const int WS_EX_OVERLAPPEDWINDOW = (WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE);
        internal const int WS_EX_PALETTEWINDOW = (WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST);




        internal const int WS_EX_LAYERED = 0x00080000;





        internal const int WS_EX_NOINHERITLAYOUT = 0x00100000; // Disable inheritence of mirroring by children;
        internal const int WS_EX_LAYOUTRTL = 0x00400000; // Right to left mirroring;



        internal const int WS_EX_COMPOSITED = 0x02000000;


        internal const int WS_EX_NOACTIVATE = 0x08000000;


    }
}
