/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;

namespace WindowsFormsAero.Native {

    internal enum DwmWindowAttribute : int {
        DWMWA_NCRENDERING_ENABLED       = 1,
        DWMWA_NCRENDERING_POLICY        = 2,
        DWMWA_TRANSITIONS_FORCEDISABLED = 3,
        DWMWA_ALLOW_NCPAINT             = 4,
        DWMWA_CAPTION_BUTTON_BOUNDS     = 5,
        DWMWA_NONCLIENT_RTL_LAYOUT      = 6,
        DWMWA_FORCE_ICONIC_REPRESENTATION = 7,
        DWMWA_FLIP3D_POLICY             = 8,
        DWMWA_EXTENDED_FRAME_BOUNDS     = 9,
        DWMWA_HAS_ICONIC_BITMAP         = 10,
        DWMWA_DISALLOW_PEEK             = 11,
        DWMWA_EXCLUDED_FROM_PEEK        = 12,
        DWMWA_CLOAK                     = 13,
        DWMWA_CLOAKED                   = 14,
        DWMWA_FREEZE_REPRESENTATION     = 15,
        DWMWA_LAST                      = 16
    }

}
