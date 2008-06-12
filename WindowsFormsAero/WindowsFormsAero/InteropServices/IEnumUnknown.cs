using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [Guid("00000100-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IEnumUnknown
    {
        void Next(
            [In, MarshalAs(UnmanagedType.U4)] 
                  int celt,
            [Out, MarshalAs(UnmanagedType.LPArray, ArraySubType = UnmanagedType.IUnknown)] 
                  object[] rgelt,
            [Out] out int pceltFetched);

        void Skip([In, MarshalAs(UnmanagedType.U4)] int celt);

        void Reset();

        IEnumUnknown Clone();
    }
}