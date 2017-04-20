/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero {

    /// <summary>
    /// Represents the state of a progress bar.
    /// </summary>
    public enum ProgressBarState {
        Normal,
        Paused,
        Error
    }

    internal static class ProgressBarStateExtensions {

        /// <summary>
        /// Converts a <see cref="ProgressBarState"/> value into a native
        /// Win32 value, represented by <see cref="Native.ProgressBarState"/>.
        /// </summary>
        public static Native.ProgressBarState ToNative(this ProgressBarState state) {
            switch (state) {
                default:
                case ProgressBarState.Normal:
                    return Native.ProgressBarState.Normal;

                case ProgressBarState.Error:
                    return Native.ProgressBarState.Error;

                case ProgressBarState.Paused:
                    return Native.ProgressBarState.Paused;
            }
        }

    }

}
