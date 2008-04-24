using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsAero.InteropServices
{
    internal delegate IntPtr KeyboardLowLevelHookProc(Int32 nCode, IntPtr wParam, KeyboardLowLevelHookInfo info);

    [Flags, Serializable]
    internal enum KeyboardLowLevelHookFlags
    {
        None = 0,
        Extended = 0x01,
        Injected = 0x10,
        AltDown = 0x20,
        KeyUp = 0x80,
    }

    [StructLayout(LayoutKind.Sequential)]
    internal sealed class KeyboardLowLevelHookInfo
    {
        private Keys _vkCode;
        private UInt32 _scanCode;
        private KeyboardLowLevelHookFlags _flags;
        private UInt32 _time;
        private IntPtr _dwExtraInfo;

        public KeyboardLowLevelHookInfo()
        {
        }

        public bool IsKeyUp
        {
            get { return (_flags & KeyboardLowLevelHookFlags.KeyUp) == KeyboardLowLevelHookFlags.KeyUp; }
        }

        public bool IsKeyDown
        {
            get { return !IsKeyUp; }
        }

        public uint ScanCode
        {
            get { return _scanCode; }
        }

        public Keys KeyCode
        {
            get { return _vkCode; }
        }

        public Keys KeyData
        {
            get
            {
                Keys k = _vkCode;

                if ((_flags & KeyboardLowLevelHookFlags.AltDown) == KeyboardLowLevelHookFlags.AltDown)
                {
                    k |= Keys.Alt;
                }

                return k;
            }
        }

        public override string ToString()
        {
            return string.Format(
                System.Globalization.CultureInfo.InvariantCulture,

                "ScanCode  = 0x{0:x2}\n" +
                "KeyCode   = 0x{1:x2} ({2})\n" +
                "IsKeyDown = {3}",

                ScanCode, (uint)(KeyCode), KeyCode, IsKeyDown);
        }
    }
}
