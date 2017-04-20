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
    internal enum DttOptsFlags : int {
        DTT_TEXTCOLOR = 1,
        DTT_BORDERCOLOR = 2,
        DTT_SHADOWCOLOR = 4,
        DTT_SHADOWTYPE = 8,
        DTT_SHADOWOFFSET = 16,
        DTT_BORDERSIZE = 32,
        //DTT_FONTPROP = 64,		commented values are currently unused
        //DTT_COLORPROP = 128,
        //DTT_STATEID = 256,
        DTT_CALCRECT = 512,
        DTT_APPLYOVERLAY = 1024,
        DTT_GLOWSIZE = 2048,
        //DTT_CALLBACK = 4096,
        DTT_COMPOSITED = 8192
    }

}
