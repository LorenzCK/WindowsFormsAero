/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero.Native {

    internal enum ProgressBarState : int {
        Normal  = 0x0001, //PBST_NORMAL
        Error   = 0x0002, //PBST_ERROR
        Paused  = 0x0003  //PBST_PAUSED
    }

}
