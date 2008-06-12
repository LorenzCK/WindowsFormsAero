using System;

namespace WindowsFormsAero.InteropServices
{
    [Flags, Serializable]
    internal enum DVASPECT
    {
        None = 0,
        Content = 1,
        Thumbnail = 2,
        Icon = 4,
        DocPrint = 8,
        Opaque = 16,
        Transparent = 32,
    }
}
