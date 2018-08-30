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

        public static int DwmSetWindowForceIconicRepresentation(IntPtr hwnd, bool forceIconic) {
            return DwmSetBoolAttribute(hwnd, DwmWindowAttribute.DWMWA_FORCE_ICONIC_REPRESENTATION, forceIconic);
        }

        public static int DwmSetWindowFlip3dPolicy(IntPtr hwnd, Flip3DPolicy policy) {
            return DwmSetIntAttribute(hwnd, DwmWindowAttribute.DWMWA_FLIP3D_POLICY, (int)policy);
        }

        public static int DwmSetWindowDisallowPeek(IntPtr hwnd, bool disallowPeek) {
            return DwmSetBoolAttribute(hwnd, DwmWindowAttribute.DWMWA_DISALLOW_PEEK, disallowPeek);
        }

        public static int DwmSetWindowExcludedFromPeek(IntPtr hwnd, bool preventPeek) {
            return DwmSetBoolAttribute(hwnd, DwmWindowAttribute.DWMWA_EXCLUDED_FROM_PEEK, preventPeek);
        }

        public static int DwmSetWindowCloaked(IntPtr hwnd, bool cloak) {
            return DwmSetBoolAttribute(hwnd, DwmWindowAttribute.DWMWA_CLOAK, cloak);
        }

        public static int DwmSetWindowFreezeRepresentation(IntPtr hwnd, bool freeze) {
            return DwmSetBoolAttribute(hwnd, DwmWindowAttribute.DWMWA_FREEZE_REPRESENTATION, freeze);
        }

        private static int DwmSetIntAttribute(IntPtr hwnd, DwmWindowAttribute att, int v) {
            return DwmSetWindowAttribute(hwnd, att, ref v, Marshal.SizeOf(typeof(int)));
        }

        private static int DwmSetBoolAttribute(IntPtr hwnd, DwmWindowAttribute att, bool v) {
            int iV = v ? 1 : 0;
            return DwmSetWindowAttribute(hwnd, att, ref iV, Marshal.SizeOf(typeof(int)));
        }

        [DllImport("dwmapi.dll")]
        public static extern int DwmGetWindowAttribute(IntPtr hwnd, DwmWindowAttribute dwAttribute, out int pvAttribute, int cbAttribute);

        public static Dwm.CloakedStatus DwmGetWindowCloaked(IntPtr hwnd) {
            DwmGetWindowAttribute(hwnd, DwmWindowAttribute.DWMWA_CLOAKED, out int retValue, Marshal.SizeOf(typeof(int)));

            if (!Enum.IsDefined(typeof(Dwm.CloakedStatus), retValue)) {
                return Dwm.CloakedStatus.Uncloaked;
            }

            return (Dwm.CloakedStatus)retValue;
        }

        public static bool DwmGetWindowFreezeRepresentation(IntPtr hwnd) {
            if(DwmGetWindowAttribute(hwnd, DwmWindowAttribute.DWMWA_CLOAKED, out int retValue, Marshal.SizeOf(typeof(int))) != 0) {
                return false;
            }

            return (retValue != 0);
        }

        #endregion

    }

}
