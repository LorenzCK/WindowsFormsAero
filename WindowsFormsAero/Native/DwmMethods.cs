/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.Native {

    internal static class DwmMethods {

        #region DWM Thumbnail methods

        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(IntPtr hwndDestination, IntPtr hwndSource, out IntPtr phThumbnailId);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr hThumbnailId);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(IntPtr hThumbnailId, ref DwmThumbnailProperties ptnProperties);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)] out bool pfEnabled);

        [DllImport("dwmapi.dll")]
        public static extern int DwmQueryThumbnailSourceSize(IntPtr hThumbnail, out DwmSize pSize);

        #endregion

        #region DWM Glass Frame

        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

        #endregion

        #region DWM Blur Behind

        [DllImport("dwmapi.dll")]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, ref DwmBlurBehind pBlurBehind);

        #endregion

        #region Attributes

        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, ref int pvAttribute, int cbAttribute);

        public static int DwmSetWindowFlip3dPolicy(IntPtr hwnd, Flip3DPolicy policy) {
            int iPolicy = (int)policy;
            return DwmSetWindowAttribute(hwnd, DwmWindowAttribute.DWMWA_FLIP3D_POLICY,
                ref iPolicy, Marshal.SizeOf(typeof(int)));
        }

        public static int DwmSetWindowDisallowPeek(IntPtr hwnd, bool disallowPeek) {
            int iPrevent = (disallowPeek) ? 1 : 0;
            return DwmSetWindowAttribute(hwnd, DwmWindowAttribute.DWMWA_DISALLOW_PEEK,
                ref iPrevent, Marshal.SizeOf(typeof(int)));
        }

        public static int DwmSetWindowExcludedFromPeek(IntPtr hwnd, bool preventPeek) {
            int iPrevent = (preventPeek) ? 1 : 0;
            return DwmSetWindowAttribute(hwnd, DwmWindowAttribute.DWMWA_EXCLUDED_FROM_PEEK,
                ref iPrevent, Marshal.SizeOf(typeof(int)));
        }

        #endregion

    }

}
