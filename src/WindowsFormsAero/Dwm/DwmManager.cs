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
using WindowsFormsAero.Resources;

namespace WindowsFormsAero.Dwm {

    /// <summary>
    /// Access to DWM features (e.g., thumbnail registration, glass sheet effect, and
    /// blur behind).
    /// </summary>
    public static class DwmManager {

        #region Thumbnail registration

        /// <summary>
        /// Registers a thumbnail to be drawn on a Windows Form.
        /// </summary>
        /// <remarks>
        /// The thumbnail will not be drawn until you update the thumbnail's properties
        /// using <see cref="Thumbnail.Update"/>.
        /// </remarks>
        /// <param name="destination">The Windows Form instance on which to draw the thumbnail.</param>
        /// <param name="source">The handle (HWND) of the window that has to be drawn.</param>
        /// <returns>A Thumbnail instance, needed to unregister and to update properties.</returns>
        public static Thumbnail Register(Form destination, IntPtr source) {
            return Register(destination.Handle, source);
        }

        /// <summary>
        /// Registers a thumbnail to be drawn on a window.
        /// </summary>
        /// <remarks>
        /// The thumbnail will not be drawn until you update the thumbnail's properties
        /// using <see cref="Thumbnail.Update"/>.
        /// </remarks>
        /// <param name="destination">The handle (Win32 HWND) of the window on which the thumbnail will be drawn.</param>
        /// <param name="source">The handle (Win32 HWND) of the window that has to be drawn.</param>
        /// <returns>A Thumbnail instance, needed to unregister and to update properties.</returns>
        public static Thumbnail Register(IntPtr destination, IntPtr source) {
            if (!OsSupport.IsVistaOrLater)
                throw new DwmCompositionException(ExceptionMessages.DwmOsNotSupported);

            if (!OsSupport.IsCompositionEnabled)
                throw new DwmCompositionException(ExceptionMessages.DwmNotEnabled);

            if (destination == source)
                throw new DwmCompositionException(ExceptionMessages.DwmWindowMatch);

            if (DwmMethods.DwmRegisterThumbnail(destination, source, out IntPtr ret) == 0) {
                return new Thumbnail(ret);
            }
            else {
                throw new DwmCompositionException(string.Format(ExceptionMessages.NativeCallFailure, nameof(DwmMethods.DwmRegisterThumbnail)));
            }
        }

        #endregion

        #region Blur Behind

        /// <summary>
        /// Enable the Aero "Blur Behind" effect on the whole client area.
        /// Background of the clint area must be painted in black.
        /// </summary>
        public static void EnableBlurBehind(IntPtr hWnd) {
            if (!OsSupport.IsVistaOrLater || !OsSupport.IsCompositionEnabled)
                return;

            var bb = new DwmBlurBehind {
                fEnable = true,
                hRgnBlur = IntPtr.Zero,
                dwFlags = DwmBlurBehindFlags.Enable
            };

            DwmMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

        /// <summary>
        /// Enable the Aero "Blur Behind" effect on the whole client area.
        /// Background of the client area must be painted black.
        /// </summary>
        public static void EnableBlurBehind(Form form) {
            EnableBlurBehind(form.Handle);
        }

        /// <summary>
        /// Disables the Aero "Blur Behind" effect.
        /// </summary>
        public static void DisableBlurBehind(IntPtr hWnd) {
            if (!OsSupport.IsVistaOrLater || !OsSupport.IsCompositionEnabled)
                return;

            var bb = new DwmBlurBehind {
                fEnable = false,
                dwFlags = DwmBlurBehindFlags.Enable
            };

            DwmMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

        #endregion

        #region Glass Frame

        /// <summary>
        /// Extends the Aero "Glass Frame" into the client area.
        /// Background on which the frame is extended must be black.
        /// </summary>
        public static void EnableGlassFrame(Form window, Padding margins) {
            InternalGlassFrame(window.Handle, margins);
        }

        /// <summary>
        /// Extends the Aero "Glass Frame" into the client area.
        /// Background on which the frame is extended must be black.
        /// </summary>
        public static void EnableGlassFrame(IntPtr hWnd, Padding margins) {
            InternalGlassFrame(hWnd, margins);
        }

        /// <summary>
        /// Extends the Aero "Glass Frame" to the whole client area.
        /// Background of the window must be black.
        /// </summary>
        public static void EnableGlassSheet(Form window) {
            InternalGlassFrame(window.Handle, new Padding(-1));
        }

        /// <summary>
        /// Extends the Aero "Glass Frame" to the whole client area.
        /// Background of the window must be black.
        /// </summary>
        public static void EnableGlassSheet(IntPtr hWnd) {
            InternalGlassFrame(hWnd, new Padding(-1));
        }

        /// <summary>
        /// Disables the Aero "Glass Frame".
        /// </summary>
        public static void DisableGlassFrame(Form window) {
            InternalGlassFrame(window.Handle, new Padding(0));
        }

        /// <summary>
        /// Disables the Aero "Glass Frame".
        /// </summary>
        public static void DisableGlassFrame(IntPtr hWnd) {
            InternalGlassFrame(hWnd, new Padding(0));
        }

        private static void InternalGlassFrame(IntPtr hWnd, Padding margins) {
            if (!OsSupport.IsVistaOrLater || !OsSupport.IsCompositionEnabled)
                return;

            var nativeMargins = Margins.FromPadding(margins);
            if (DwmMethods.DwmExtendFrameIntoClientArea(hWnd, ref nativeMargins) != 0)
                throw new DwmCompositionException(string.Format(ExceptionMessages.NativeCallFailure, nameof(DwmMethods.DwmExtendFrameIntoClientArea)));
        }

        #endregion Glass Frame

        #region DWM Attributes

        /// <summary>
        /// Sets a window's Flip 3D policy.
        /// </summary>
        /// <param name="form">Form whose Flip 3D state should be altered.</param>
        /// <param name="policy">Desired Flip 3D policy.</param>
        /// <remarks>Is ignored on OSs that do not support Aero.</remarks>
        public static void SetWindowFlip3dPolicy(Form form, Flip3DPolicy policy) {
            SetWindowFlip3dPolicy(form.Handle, policy);
        }

        /// <summary>
        /// Sets a window's Flip 3D policy.
        /// </summary>
        /// <param name="hwnd">Handle of the window whose Flip 3D state should be altered.</param>
        /// <param name="policy">Desired Flip 3D policy.</param>
        /// <remarks>Is ignored on OSs that do not support Aero.</remarks>
        public static void SetWindowFlip3dPolicy(IntPtr hwnd, Flip3DPolicy policy) {
            // Works only on Vista
            if (!OsSupport.IsVistaOrLater || OsSupport.IsEightOrLater)
                return;

            if (!OsSupport.IsCompositionEnabled)
                return;

            if (DwmMethods.DwmSetWindowFlip3dPolicy(hwnd, policy) != 0)
                throw new Exception(ExceptionMessages.DwmFlip3dFailPolicy);
        }

        /// <summary>
        /// Sets whether Aero Peek is enabled or disabled on a window.
        /// </summary>
        /// <param name="form">Form whose Aero Peek state should be altered.</param>
        /// <param name="disallowPeek">True if Aero Peek should be disabled for the window. False otherwise.</param>
        /// <remarks>Is ignored on OSs that do not support Aero Peek.</remarks>
        public static void SetDisallowPeek(Form form, bool disallowPeek) {
            SetDisallowPeek(form.Handle, disallowPeek);
        }

        /// <summary>
        /// Sets whether Aero Peek is enabled or disabled on a window.
        /// </summary>
        /// <param name="hwnd">Handle of the window whose Aero Peek state should be altered.</param>
        /// <param name="disallowPeek">True if Aero Peek should be disabled for the window. False otherwise.</param>
        /// <remarks>Is ignored on OSs that do not support Aero Peek.</remarks>
        public static void SetDisallowPeek(IntPtr hwnd, bool disallowPeek) {
            if (!OsSupport.IsSevenOrLater || !OsSupport.IsCompositionEnabled)
                return;

            if (DwmMethods.DwmSetWindowDisallowPeek(hwnd, disallowPeek) != 0)
                throw new Exception(ExceptionMessages.DwmDisallowPeekFail);
        }

        /// <summary>
        /// Sets whether Aero Peek excludes or includes a window.
        /// </summary>
        /// <param name="form">Form whose Aero Peek exclusion state is to be set.</param>
        /// <param name="excluded">Set to true to exlude the window from Aero Peek.</param>
        /// <remarks>Is ignored on OSs that do not support Aero Peek.</remarks>
        public static void SetExcludeFromPeek(Form form, bool excluded) {
            if (!OsSupport.IsSevenOrLater || !OsSupport.IsCompositionEnabled)
                return;

            if (DwmMethods.DwmSetWindowExcludedFromPeek(form.Handle, excluded) != 0)
                throw new Exception(ExceptionMessages.DwmExcludePeekFail);
        }

        /// <summary>
        /// Gets whether a form is cloaked.
        /// </summary>
        /// <remarks>Always returns <see cref="CloakedStatus.Uncloaked"/> on unsupported OSs.</remarks>
        public static CloakedStatus IsCloaked(Form form) {
            return IsCloaked(form.Handle);
        }

        /// <summary>
        /// Gets whether a window is cloaked.
        /// </summary>
        /// <remarks>Always returns <see cref="CloakedStatus.Uncloaked"/> on unsupported OSs.</remarks>
        public static CloakedStatus IsCloaked(IntPtr hwnd) {
            if (!OsSupport.IsEightOrLater)
                return CloakedStatus.Uncloaked;

            return DwmMethods.DwmGetWindowCloaked(hwnd);
        }

        /// <summary>
        /// Sets a form's cloak status.
        /// </summary>
        /// <remarks>Is ignored on OSs that do not support cloaking.</remarks>
        public static void SetCloak(Form form, bool cloak) {
            SetCloak(form.Handle, cloak);
        }

        /// <summary>
        /// Sets a window's cloak status.
        /// </summary>
        /// <remarks>Is ignored on OSs that do not support cloaking.</remarks>
        public static void SetCloak(IntPtr hwnd, bool cloak) {
            if (!OsSupport.IsEightOrLater)
                return;

            if (DwmMethods.DwmSetWindowCloaked(hwnd, cloak) != 0)
                throw new Exception(ExceptionMessages.DwmCloakFail);
        }

        /// <summary>
        /// Gets whether a window's DWM representation is frozen.
        /// </summary>
        /// <param name="hwnd">Handle of the window whose representation status must be returned.</param>
        /// <returns>Returns false on OSs that do not support frozen DWM representation.</returns>
        public static bool IsFrozen(IntPtr hwnd) {
            if (!OsSupport.IsEightOrLater)
                return false;

            return DwmMethods.DwmGetWindowFreezeRepresentation(hwnd);
        }

        /// <summary>
        /// Gets whether a window's DWM representation is frozen.
        /// </summary>
        /// <param name="form">Form whose representation status must be returned.</param>
        /// <returns>Returns false on OSs that do not support frozen DWM representation.</returns>
        public static bool IsFrozen(Form form) {
            return IsFrozen(form.Handle);
        }

        /// <summary>
        /// Sets whether a window's DWM representation is frozen.
        /// </summary>
        /// <param name="hwnd">Handle of the window whose representation status should be set.</param>
        /// <param name="freeze">True if the window's DWM representation should be frozen.</param>
        /// <remarks>Is ignored on OSs that do not support frozen DWM representation.</remarks>
        public static void SetFrozenRepresentation(IntPtr hwnd, bool freeze) {
            if (!OsSupport.IsEightOrLater)
                return;

            if(DwmMethods.DwmSetWindowFreezeRepresentation(hwnd, freeze) != 0) {
                throw new Exception(ExceptionMessages.DwmFreezeRepresentationFail);
            }
        }

        /// <summary>
        /// Sets whether a window's DWM representation is frozen.
        /// </summary>
        /// <param name="form">Form whose representation status should be set.</param>
        /// <param name="freeze">True if the window's DWM representation should be frozen.</param>
        /// <remarks>Is ignored on OSs that do not support frozen DWM representation.</remarks>
        public static void SetFrozenRepresentation(Form form, bool freeze) {
            SetFrozenRepresentation(form.Handle, freeze);
        }

            #endregion DWM Attributes

        }

}
