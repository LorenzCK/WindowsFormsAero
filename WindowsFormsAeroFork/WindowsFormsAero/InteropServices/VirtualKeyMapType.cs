using System;

namespace WindowsFormsAero.InteropServices
{
    [Serializable]
    internal enum VirtualKeyMapType
    {
        VirtualKeyToScanCode = 0,
        ScanCodeToVirtualKey = 1,
        VirtualKeyToChar = 2,
        ScanCodeToVirtualKeyEx = 3,
        VirtualKeyToScanCodeEx = 4,
    }
}
