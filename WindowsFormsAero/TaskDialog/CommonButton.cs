/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;

namespace WindowsFormsAero.TaskDialog {

    /// <summary>
    /// Common Task Dialog buttons.
    /// </summary>
    [Flags]
    public enum CommonButton : int {
        OK      = 0x0001,
        Cancel  = 0x0008,
        Yes     = 0x0002,
        No      = 0x0004,
        Retry   = 0x0010,
        Close   = 0x0020
    }

}
