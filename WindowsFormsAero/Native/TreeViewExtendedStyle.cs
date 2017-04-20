/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero.Native {

    internal enum TreeViewExtendedStyle : uint {
        TVS_EX_NOSINGLECOLLAPSE = 0x0001,
        TVS_EX_MULTISELECT = 0x0002,
        TVS_EX_DOUBLEBUFFER = 0x0004,
        TVS_EX_NOINDENTSTATE = 0x0008,
        TVS_EX_RICHTOOLTIP = 0x0010,
        TVS_EX_AUTOHSCROLL = 0x0020,
        TVS_EX_FADEINOUTEXPANDOS = 0x0040,
        TVS_EX_PARTIALCHECKBOXES = 0x0080,
        TVS_EX_EXCLUSIONCHECKBOXES = 0x0100,
        TVS_EX_DIMMEDCHECKBOXES = 0x0200,
        TVS_EX_DRAWIMAGEASYNC = 0x0400,
    }

}
