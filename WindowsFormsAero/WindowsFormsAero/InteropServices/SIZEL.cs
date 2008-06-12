using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    internal struct SIZEL
    {
        public int X;
        public int Y;
    }
}
