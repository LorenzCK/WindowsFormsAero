/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;

namespace WindowsFormsAero.Native {

    [Flags]
    internal enum DwmBlurBehindFlags : int {
        Enable                  = 0x00000001,
        BlurRegion              = 0x00000002,
        TransitionOnMaximized   = 0x00000004
    }

}
