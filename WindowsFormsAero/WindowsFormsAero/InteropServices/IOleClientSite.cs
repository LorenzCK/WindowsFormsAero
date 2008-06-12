using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [Guid("00000118-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface IOleClientSite
    {
        void SaveObject();

        [return: MarshalAs(UnmanagedType.IUnknown)]
        object GetMoniker(
            [In, MarshalAs(UnmanagedType.U4)] int dwAssign,
            [In, MarshalAs(UnmanagedType.U4)] int dwWhichMoniker);

        [return: MarshalAs(UnmanagedType.Interface)]
        IOleContainer GetContainer();

        void ShowObject();
        void OnShowWindow(int fShow);
        void RequestNewObjectLayout();
    }
}