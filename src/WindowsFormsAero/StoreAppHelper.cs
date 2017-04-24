/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System.Text;
using WindowsFormsAero.Native;

namespace WindowsFormsAero {

    /// <summary>
    /// Exposes helpers and auxilary methods for Windows Store apps
    /// (i.e., desktop applications converted through Centennial).
    /// </summary>
    public static class StoreAppHelper {

        /// <summary>
        /// Gets whether the current process is running inside an UWP container
        /// "Windows Store" application.
        /// </summary>
        /// <remarks>
        /// Taken from https://github.com/qmatteoq/DesktopBridgeHelpers by Matteo Pagani.
        /// </remarks>
        public static bool IsRunningAsStoreApp() {
            if (!OsSupport.IsEightOrBetter) {
                return false;
            }
            else {
                var sb = new StringBuilder(0);
                int length = 0;
                int result = UwpMethods.GetCurrentPackageFullName(ref length, ref sb);

                // If there is a package name, the 0-length buffer will be insufficient
                return (result == (int)SystemErrorCode.ErrorInsufficientBuffer);
            }
        }

    }

}
