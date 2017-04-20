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
    internal enum WindowExStyles : long {
        AppWindow = 0x00040000L,
        ClientEdge = 0x00000200L,
        ControlParent = 0x00010000L,
        Layered = 0x00080000,
        NoActivate = 0x08000000L,
        ToolWindow = 0x00000080L,
        TopMost = 0x00000008L,
        Transparent = 0x00000020L
    }

}
