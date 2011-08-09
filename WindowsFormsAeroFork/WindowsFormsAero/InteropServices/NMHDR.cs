using System;

namespace WindowsFormsAero.InteropServices
{
    [Serializable]
    internal struct NMHDR
    {
        public IntPtr hwndFrom;
        public IntPtr idFrom;
        public UInt32 code;
    }
}
