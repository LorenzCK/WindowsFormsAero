using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    internal sealed class SafeWindowsHookHandle : SafeHandle
    {
        public SafeWindowsHookHandle()
            : base(IntPtr.Zero, true)
        {
        }

        public override bool IsInvalid
        {
            get { return handle == IntPtr.Zero; }
        }

        protected override bool ReleaseHandle()
        {
            return NativeMethods.UnhookWindowsHookEx(handle);
        }

        public static SafeWindowsHookHandle InvalidHandle
        {
            get { return new SafeWindowsHookHandle(); }
        }
    }
}
