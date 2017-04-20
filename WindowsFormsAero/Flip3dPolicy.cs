/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero {
    
    /// <summary>
    /// Determines a window's Flip 3D policy.
    /// </summary>
    public enum Flip3DPolicy : int {
        /// <summary>
        /// Default Flip 3D behavior.
        /// </summary>
        Default,
        /// <summary>
        /// Excludes the window from Flip 3D and hides it behind the animation.
        /// </summary>
        ExcludeBelow,
        /// <summary>
        /// Excludes the window from Flip 3D and shows it above the animation.
        /// </summary>
        ExcludeAbove
    }

}
