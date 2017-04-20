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
        DWMWA_FLIP3D_POLICY             = 8,
        DWMWA_DISALLOW_PEEK             = 11,
        DWMWA_EXCLUDED_FROM_PEEK        = 12,
    }

}
