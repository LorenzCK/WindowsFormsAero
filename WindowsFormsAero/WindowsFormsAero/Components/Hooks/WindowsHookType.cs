using System;

namespace WindowsFormsAero
{
    [Serializable]
    public enum WindowsHookType
    {
        MessageFilter = -1,
        JournalRecord = 0,
        JournalPlayback = 1,
        Keyboard = 2,
        GetMessage = 3,
        CallWindowProc = 4,
        Cbt = 5,
        SysMessageFilter = 6,
        Mouse = 7,
        Debug = 9,
        Shell = 10,
        ForegroundIdle = 11,
        CallWindowProcReturn = 12,
        KeyboardLowLevel = 13,
        MouseLowLevel = 14,
    }
}
