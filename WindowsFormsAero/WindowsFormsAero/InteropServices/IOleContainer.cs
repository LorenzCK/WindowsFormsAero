using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [Guid("0000011B-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOleContainer
    {
        void ParseDisplayName(
            [In, MarshalAs(UnmanagedType.Interface)] object pbc,
            [In, MarshalAs(UnmanagedType.BStr)] string pszDisplayName,
            [Out, MarshalAs(UnmanagedType.LPArray)] int[] pchEaten,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] object[] ppmkOut);

        IEnumUnknown EnumObjects(
            [In, MarshalAs(UnmanagedType.U4)] int grfFlags);

        void LockContainer(bool fLock);
    }
}