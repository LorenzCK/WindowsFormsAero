using System;

namespace WindowsFormsAero.InteropServices
{
    [Serializable, Flags]
    internal enum SetWindowPosFlags
    {
        NoSize = 0x0001,
        NoMove = 0x0002,
        NoZOrder = 0x0004,
        NoRedraw = 0x0008,
        NoActivate = 0x0010,
        FrameChanged = 0x0020,  /* The frame changed: send WM_NCCALCSIZE */
        ShowWindow = 0x0040,
        HideWindow = 0x0080,
        NoCopyBits = 0x0100,
        NoOwnerZOrder = 0x0200,  /* Don't do owner Z ordering */
        NoSendChanging = 0x0400,  /* Don't send WM_WINDOWPOSCHANGING */

        DeferErase = 0x2000,
        AsyncWindowPos = 0x4000,
    }
}
