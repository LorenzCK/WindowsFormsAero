using System;
using System.Diagnostics;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    public static class WindowsOnWindows64
    {
        public static IDisposable SuspendFilesystemRedirection()
        {
            return new Wow64FileSystemRedirectionDisabledScope();
        }

        public static bool IsCurrentProcessWow64
        {
            get
            {
                bool result;

                if (IntPtr.Size != 4)
                {
                    return false;
                }

                using (var process = Process.GetCurrentProcess())
                {
                    if (!NativeMethods.IsWow64Process(process.Handle, out result))
                    {
                        throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                    }
                }

                return result;
            }
        }

        private sealed class Wow64FileSystemRedirectionDisabledScope : IDisposable
        {
            private readonly IntPtr _oldValue;

            public Wow64FileSystemRedirectionDisabledScope()
            {
                if (!NativeMethods.Wow64DisableWow64FsRedirection(out _oldValue))
                {
                    throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                }
            }

            public void Dispose()
            {
                if (!NativeMethods.Wow64RevertWow64FsRedirection(_oldValue))
                {
                    throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                }
            }
        }
    }
}
