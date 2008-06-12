using System;

namespace WindowsFormsAero.InteropServices
{
    [Flags, Serializable]
    internal enum GetRichEditObjectInterfaces
    {
        None = 0,
        OleObject = 1,
        Storage = 2,
        OleSite = 4,
        All = OleObject | OleSite | Storage,
    }
}
