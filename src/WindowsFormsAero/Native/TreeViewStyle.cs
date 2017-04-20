/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero.Native {

    internal enum TreeViewStyle : uint {
        TVS_HASBUTTONS = 0x0001,
        TVS_HASLINES = 0x0002,
        TVS_LINESATROOT = 0x0004,
        TVS_EDITLABELS = 0x0008,
        TVS_DISABLEDRAGDROP = 0x0010,
        TVS_SHOWSELALWAYS = 0x0020,
        TVS_RTLREADING = 0x0040,
        TVS_NOTOOLTIPS = 0x0080,
        TVS_CHECKBOXES = 0x0100,
        TVS_TRACKSELECT = 0x0200,
        TVS_SINGLEEXPAND = 0x0400,
        TVS_INFOTIP = 0x0800,
        TVS_FULLROWSELECT = 0x1000,
        TVS_NOSCROLL = 0x2000,
        TVS_NONEVENHEIGHT = 0x4000,
        TVS_NOHSCROLL = 0x8000,
    }

}
