using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsAero.InteropServices
{
    internal static class WindowMessages
    {
        public const uint BCM_FIRST = 0x1600;      // Button control messages
        public const uint BCM_SETSHIELD = (BCM_FIRST + 0x000C);

        public const int PBM_SETSTATE = WM_USER + 16;

        public const int WM_KEYDOWN = 0x0100;
        public const int WM_KEYUP = 0x0101;
        public const int WM_SYSKEYDOWN = 0x0104;
        public const int WM_SYSKEYUP = 0x0105;
        public const int WM_USER = 0x0400;
    }
}
