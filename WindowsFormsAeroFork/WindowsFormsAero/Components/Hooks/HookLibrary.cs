using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Runtime.InteropServices;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    internal sealed class HookLibrary
    {
        private HookInfoDictionary _hooks;
        private NativeModule _module;
        private String _modulePath;
        
        public HookLibrary(IntPtr hWnd)
        {
            _modulePath = GetModulePath(hWnd);

            using (var hookLib = OpenHookLibStream())
            {
                using (var file = File.OpenWrite(_modulePath))
                {
                    WriteStreamTo(hookLib, file);
                }
            }

            _module = NativeModule.Load(_modulePath);
            _hooks = new HookInfoDictionary(_module);
        }

        public void Dispose()
        {
            foreach (var hook in _hooks.Values)
            {
                hook.Enabled = false;
            }

            if (_module != null)
            {
                _module.Dispose();
                _module = null;
            }

            if (_modulePath != null)
            {
                File.Delete(_modulePath);
                _modulePath = null;
            }
        }

        public void SetHook(WindowsHookType hookType, bool enabled)
        {
            _hooks[hookType].Enabled = enabled;
        }

        private string GetModulePath(IntPtr handle)
        {
            return Path.Combine(
                Path.GetTempPath(),
                String.Concat(GetType().Name, "-0x", handle.ToString("x8"), ".dll")
            );
        }

        private static void WriteStreamTo(Stream input, Stream output)
        {
            var buffer = new byte[0x1000];
            var read = 0;

            do
            {
                read = input.Read(buffer, 0, buffer.Length);

                if (read > 0)
                {
                    output.Write(buffer, 0, read);
                }
            }
            while (read > 0);
        }

        private static Stream OpenHookLibStream()
        {
            return OpenStream(typeof(Resources.Images), "HookLib.dll");
        }

        private static Stream OpenStream(Type type, String name)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(type, name);
        }

        private sealed class HookInfoDictionary : Dictionary<WindowsHookType, HookInfo>
        {
            public HookInfoDictionary(NativeModule module)
            {
                Add(WindowsHookType.Keyboard, module, "KeyboardProc");
            }

            private void Add(WindowsHookType hookType, NativeModule module, String procedure)
            {
                Add(hookType, new HookInfo(module, hookType, procedure));
            }
        }

        private sealed class HookInfo
        {
            private readonly WindowsHookType _type;
            private readonly NativeModule _module;
            private readonly String _procedure;

            private SafeWindowsHookHandle _handle;

            public HookInfo(NativeModule module, WindowsHookType type, String procedure)
            {
                _type = type;
                _module = module;
                _procedure = procedure;
            }

            public bool Enabled
            {
                get { return _handle != null && !_handle.IsInvalid; }
                set
                {
                    if (value != Enabled)
                    {
                        if (value)
                        {
                            _handle = NativeMethods.SetWindowsHookEx(
                                _type,
                                _module.GetProcedureAddress(_procedure),
                                _module,
                                0);

                            if (_handle.IsInvalid)
                            {
                                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
                            }
                        }
                        else
                        {
                            _handle.Dispose();
                            _handle = null;
                        }
                    }
                }
            }
        }
    }
}
