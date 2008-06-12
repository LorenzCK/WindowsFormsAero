using System;

namespace WindowsFormsAero.InteropServices
{
    [Flags, Serializable]
    internal enum RichEditObjectFlags : uint
    {
        None = 0x00000000,	// No flags
        ReadWriteMask = 0x0000003F,	// Mask out RO bits
        DontNeedPalette = 0x00000020,	// Object doesn't need palette
        Blank = 0x00000010,	// Object is blank
        DynamicSize = 0x00000008,	// Object defines size always
        InvertedSelect = 0x00000004,	// Object drawn all inverted if sel
        BelowBaseline = 0x00000002,	// Object sits below the baseline
        Resizable = 0x00000001,	// Object may be resized
        Link = 0x80000000,	// Object is a link (RO)
        Static = 0x40000000,	// Object is static (RO)
        Selected = 0x08000000,	// Object selected (RO)
        Open = 0x04000000,	// Object open in its server (RO)
        InPlaceActive = 0x02000000,	// Object in place active (RO)
        Hilited = 0x01000000,	// Object is to be hilited (RO)
        LinkAvailable = 0x00800000,	// Link believed available (RO)
        GetMetafile = 0x00400000	// Object requires metafile (RO)
    }
}
