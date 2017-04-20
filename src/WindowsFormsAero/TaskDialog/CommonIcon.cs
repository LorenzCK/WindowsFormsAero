/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero.TaskDialog {

    /// <summary>
    /// Common Task Dialog icon values.
    /// </summary>
    /// <remarks>
    /// Common icon values also determine how the task dialog's main instruction is styled.
    /// </remarks>
    public enum CommonIcon : long {
        Information         = ushort.MaxValue - 2,
        Warning             = ushort.MaxValue,
        Stop                = ushort.MaxValue - 1,
        None                = 0,
        SecurityWarning     = ushort.MaxValue - 5,
        SecurityError       = ushort.MaxValue - 6,
        SecuritySuccess     = ushort.MaxValue - 7,
        SecurityShield      = ushort.MaxValue - 3,
        SecurityShieldBlue  = ushort.MaxValue - 4,
        SecurityShieldGray  = ushort.MaxValue - 8
    }

}
