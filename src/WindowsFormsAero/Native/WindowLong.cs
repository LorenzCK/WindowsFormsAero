/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero.Native {

    internal enum WindowLong : int {
        WndProc = (-4),
        HInstance = (-6),
        HwndParent = (-8),
        Style = (-16),
        ExStyle = (-20),
        UserData = (-21),
        Id = (-12)
    }

}
