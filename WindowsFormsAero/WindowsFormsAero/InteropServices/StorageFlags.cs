using System;

namespace WindowsFormsAero.InteropServices
{
    [Serializable, Flags]
    internal enum StorageFlags
    {
        Read = 0x00000000,
        Write = 0x00000001,
        ReadWrite = 0x00000002,
        ShareDenyNone = 0x00000040,
        ShareDenyRead = 0x00000030,
        ShareDenyWrite = 0x00000020,
        ShareExclusive = 0x00000010,
        Priority = 0x00040000,
        Create = 0x00001000,
        Convert = 0x00020000,
        FailIfThere = 0x00000000,
        Direct = 0x00000000,
        Transacted = 0x00010000,
        NoScratch = 0x00100000,
        NoSnapshot = 0x00200000,
        Simple = 0x08000000,
        DirectSwmr = 0x00400000,
        DeleteOnRelease = 0x04000000,

    }
}
