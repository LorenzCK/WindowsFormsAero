using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    internal sealed class NativeModule : SafeHandle
    {
        private NativeModule()
            : base(IntPtr.Zero, true)
        {
        }

        public IntPtr GetProcedureAddress(string name)
        {
            IntPtr result = NativeMethods.GetProcAddress(this, name);

            if (result == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return result;
        }

        public bool ContainsProcedure(string name)
        {
            return NativeMethods.GetProcAddress(this, name) != IntPtr.Zero;
        }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.FreeLibrary(handle);
        }

        public static NativeModule Invalid
        {
            get { return new NativeModule(); }
        }

        public static NativeModule TryLoad(string path)
        {
            var handle = NativeMethods.LoadLibrary(path);

            if (handle.IsInvalid)
            {
                handle.Dispose();
                return null;
            }

            return handle;
        }

        public static NativeModule Load(string path)
        {
            var handle = NativeMethods.LoadLibrary(path);

            if (handle.IsInvalid)
            {
                handle.Dispose();
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return handle;
        }

    }
}
