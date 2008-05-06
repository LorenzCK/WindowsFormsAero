using System;
using System.Collections.Generic;
using System.Text;

namespace WindowsFormsAero.InteropServices
{
    [Serializable, Flags]
    internal enum ToolTipFlags
    {
        IdIsHwnd = 0x0001,

        CenterTip = 0x0002,
        RtlReading = 0x0004,
        Subclass = 0x0010,

        Track = 0x0020,
        Absolute = 0x0080,
        Transparent = 0x0100,

        ParseLinks = 0x1000,

        //DISetItem = 0x8000,
    }
}
