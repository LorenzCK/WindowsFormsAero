using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsAero.InteropServices
{
    internal static class WindowMessages
    {
        public const uint BCM_FIRST = 0x1600;      // Button control messages
        public const uint BCM_SETSHIELD = (BCM_FIRST + 0x000C);
    }
}
