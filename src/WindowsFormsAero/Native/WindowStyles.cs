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
    internal enum WindowStyles : long {
        None        = 0x00000000L,
        Border      = 0x00800000L,
        Caption     = 0x00C00000L,
        Child       = 0x40000000L,
        DialogFrame = 0x00400000L,
        Disabled    = 0x08000000L,
        Maximize    = 0x01000000L,
        MaximizeBox = 0x00010000L,
        Minimize    = 0x20000000L,
        MinimizeBox = 0x00020000L,
        Overlapped  = 0x00000000L,
        SysMenu     = 0x00080000L,
        ThickFrame  = 0x00040000L,
        Visible     = 0x10000000L,
        OverlappedWindow = Overlapped | Caption | SysMenu | ThickFrame | MinimizeBox | MaximizeBox
    }

}
