using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [Serializable, Flags]
    public enum DwmBlurBehindFlags
    {
        None = 0,
        Enable = 1,
        BlurRegion = 2,
        TransitionOnMaximized = 4,
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public sealed class DwmBlurBehind
    {
        private DwmBlurBehindFlags _dwFlags;
        private Boolean _fEnable;
        private IntPtr _hRgnBlur;
        private Boolean _fTransitionOnMaximized;

        public DwmBlurBehind()
        {
        }

        public bool Enabled
        {
            get { return _fEnable; }
            set
            {
                _fEnable = value;
                _dwFlags |= DwmBlurBehindFlags.Enable;
            }
        }

        public bool TransitionOnMaximized
        {
            get { return _fTransitionOnMaximized; }
            set
            {
                _fTransitionOnMaximized = value;
                _dwFlags |= DwmBlurBehindFlags.TransitionOnMaximized;
            }
        }

        public IntPtr Region
        {
            get { return _hRgnBlur; }
            set
            {
                _hRgnBlur = value;
                _dwFlags |= DwmBlurBehindFlags.BlurRegion;
            }
        }
    }
}
