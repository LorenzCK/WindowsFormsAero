using System;
using Microsoft.Win32.SafeHandles;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    internal sealed class SafeNativeModuleHandle : SafeHandleZeroOrMinusOneIsInvalid
    {
        public SafeNativeModuleHandle() 
            : this(true)
        {
        }

        public SafeNativeModuleHandle(bool ownsHandle)
            : base(ownsHandle)
        {
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.FreeLibrary(handle);
        }
    }

    internal sealed class NativeModule : IDisposable
    {
        private readonly SafeNativeModuleHandle _handle;

        private NativeModule(SafeNativeModuleHandle handle)
	    {
            if (handle.IsInvalid)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            _handle = handle;
	    }

        public void Dispose()
        {
            if (_handle != null)
            {
                _handle.Dispose();
            }
        }

        public IntPtr GetProcedureAddress(string name)
        {
            IntPtr result = NativeMethods.GetProcAddress(_handle, name);

            if (result == IntPtr.Zero)
            {
                Marshal.ThrowExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return result;
        }

        public bool ContainsProcedure(string name)
        {
            return NativeMethods.GetProcAddress(_handle, name) != IntPtr.Zero;
        }

        public static NativeModule TryLoad(string path)
        {
            var handle = NativeMethods.LoadLibrary(path);

            if (handle.IsInvalid)
            {
                return null;
            }

            return new NativeModule(handle);
        }

        public static NativeModule Load(string path)
        {
            return new NativeModule(NativeMethods.LoadLibrary(path));
        }
    }
}
