using System;

namespace WindowsFormsAero.InteropServices
{
    [Serializable, Flags]
    internal enum LoadImageFlags
    {
        DefaultColor = 0x0,
        Monochrome = 0x1,
        Color = 0x2,
        CopyReturnOriginal = 0x4,
        CopyDeleteOriginal = 0x8,
        LoadFromFile = 0x10,
        LoadTransparent = 0x20,
        DefaultSize = 0x40,
        VgaColor = 0x80,
        LoadMap3DColors = 0x1000,
        CreateDibSection = 0x2000,
        CopyFromResource = 0x4000,
        Shared = 0x8000,
    }
}
