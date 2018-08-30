/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Windows.Forms;
using WindowsFormsAero.Native;

namespace WindowsFormsAero {

    /// <summary>
    /// Exposes methods that enable an application to interact with groups of
    /// windows that form virtual workspaces.
    /// </summary>
    public static class VirtualDesktopManager {

        private static readonly Guid VirtualDesktopManagerTypeGuid = new Guid("aa509086-5ca9-4c25-8f95-589d3c07b48a");

        private static IVirtualDesktopManager _com;

        private static IVirtualDesktopManager Internal {
            get {
                if(_com == null) {
                    var t = Type.GetTypeFromCLSID(VirtualDesktopManagerTypeGuid);
                    _com = (IVirtualDesktopManager)Activator.CreateInstance(t);
                }
                return _com;
            }
        }

        /// <summary>
        /// Indicates whether the provided window is on the currently active virtual desktop.
        /// </summary>
        /// <param name="topLevelHwnd">Top-level window handle.</param>
        /// <returns>True if the provided window is on the currently active virtual desktop or if there is only one desktop.</returns>
        /// <remarks>Always returns true on OSs that do not support virtual desktops.</remarks>
        public static bool IsWindowOnCurrentVirtualDesktop(IntPtr topLevelHwnd) {
            if(!OsSupport.IsTenOrBetter) {
                // Only one virtual desktop exists
                return true;
            }

            return Internal.IsWindowOnCurrentVirtualDesktop(topLevelHwnd);
        }

        /// <summary>
        /// Indicates whether the provided form is on the currently active virtual desktop.
        /// </summary>
        /// <param name="form">Top-level form.</param>
        /// <returns>True if the provided window is on the currently active virtual desktop or if there is only one desktop.</returns>
        /// <remarks>Always returns true on OSs that do not support virtual desktops.</remarks>
        public static bool IsWindowOnCurrentVirtualDesktop(Form form) {
            return IsWindowOnCurrentVirtualDesktop(form.Handle);
        }

        /// <summary>
        /// Gets the handle for the virtual desktop hosting the provided top-level window.
        /// </summary>
        /// <param name="topLevelHwnd">Top-level window handle.</param>
        /// <returns>Virtual desktop handle hosting the provided top-level window.</returns>
        /// <remarks>Returns default virtual desktop handle on OSs that do not support virtual desktops.</remarks>
        public static VirtualDesktop GetWindowDesktopId(IntPtr topLevelHwnd) {
            if (!OsSupport.IsTenOrBetter) {
                // Default to virtual desktop with empty ID
                return new VirtualDesktop(Guid.Empty);
            }

            return new VirtualDesktop(Internal.GetWindowDesktopId(topLevelHwnd));
        }

        /// <summary>
        /// Gets the handle for the virtual desktop hosting the provided top-level form.
        /// </summary>
        /// <param name="form">Top-level form.</param>
        /// <returns>Virtual desktop handle hosting the provided top-level window.</returns>
        /// <remarks>Returns default virtual desktop handle on OSs that do not support virtual desktops.</remarks>
        public static VirtualDesktop GetWindowDesktopId(Form form) {
            return GetWindowDesktopId(form.Handle);
        }

        /// <summary>
        /// Moves a window to the specified virtual desktop.
        /// </summary>
        /// <param name="topLevelHwnd">Top-level window handle.</param>
        /// <param name="target">Target virtual desktop handle.</param>
        /// <remarks>Is ignored on OSs that do not support virtual desktops.</remarks>
        public static void MoveWindowToDesktop(IntPtr topLevelHwnd, VirtualDesktop target) {
            if(!OsSupport.IsTenOrBetter) {
                return;
            }

            var id = target.Id;
            Internal.MoveWindowToDesktop(topLevelHwnd, ref id);
        }

        /// <summary>
        /// Moves a form to the specified virtual desktop.
        /// </summary>
        /// <param name="form">Top-level form.</param>
        /// <param name="target">Target virtual desktop handle.</param>
        /// <remarks>Is ignored on OSs that do not support virtual desktops.</remarks>
        public static void MoveWindowToDesktop(Form form, VirtualDesktop target) {
            MoveWindowToDesktop(form.Handle, target);
        }

    }

}
