using System;
using System.Runtime.InteropServices;
using System.Security;

namespace WindowsFormsAero.InteropServices
{
    [ComImport]
    [SuppressUnmanagedCodeSecurity]
    [Guid("0000000a-0000-0000-C000-000000000046")]
    [InterfaceType(ComInterfaceType.InterfaceIsIUnknown)]
    internal interface ILockBytes
    {
        UInt32 ReadAt(
            [In]      UInt64 ulOffset,
            [In]      IntPtr pv,
            [In]      UInt32 cb);

        UInt32 WriteAt(
            [In]      UInt64 ulOffset,
            [In]      IntPtr pv,
            [In]      UInt32 cb);

        void Flush();

        void SetSize([In] UInt64 cb);

        void LockRegion(
            [In] UInt64 libOffset,
            [In] UInt64 cb,
            [In] UInt32 dwLockType);

        void UnlockRegion(
            [In] UInt64 libOffset,
            [In] UInt64 cb,
            [In] UInt32 dwLockType);

        void Stat(
            [Out] out System.Runtime.InteropServices.ComTypes.STATSTG pstatstg,
            [In]      UInt32 grfStatFlag);

    }
}