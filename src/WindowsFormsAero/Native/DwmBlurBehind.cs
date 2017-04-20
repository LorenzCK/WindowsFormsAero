/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.Native {

    [StructLayout(LayoutKind.Sequential)]
    internal struct DwmBlurBehind {
        public DwmBlurBehindFlags dwFlags;
        public bool fEnable;
        public IntPtr hRgnBlur;
        public bool fTransitionOnMaximized;
    }

}
