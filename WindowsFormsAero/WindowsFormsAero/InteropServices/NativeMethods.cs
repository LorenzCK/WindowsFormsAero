using System;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;

namespace WindowsFormsAero.InteropServices
{
    internal static class NativeMethods
    {
        private static class Dll
        {
            public const string DwmApi = "dwmapi";
            public const string Kernel32 = "kernel32";
            public const string Ole32 = "ole32";
            public const string User32 = "user32";
            public const string UxTheme = "uxtheme";
        }

        #region dwmapi!DwmGetColorizationColor

        [DllImport(Dll.DwmApi, ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern bool DwmGetColorizationColor(
            [Out] out int pcrColorization
        );

        #endregion

        #region dwmapi!DwmIsCompositionEnabled

        [DllImport(Dll.DwmApi, ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DwmIsCompositionEnabled();

        #endregion

        #region dwmapi!DwmEnableComposition

        [DllImport(Dll.DwmApi, ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern void DwmEnableComposition(
            [In] UInt32 uCompositionAction
        );

        #endregion

        #region dwmapi!DwmEnableBlurBehindWindow

        [DllImport(Dll.DwmApi, ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern void DwmEnableBlurBehindWindow(
            [In] HandleRef hWnd,
            [In] DwmBlurBehind pBlurBehind);

        #endregion

        #region dwmapi!DwmExtendFrameIntoClientArea

        [DllImport(Dll.DwmApi, ExactSpelling = true, PreserveSig = false, SetLastError = false )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern void DwmExtendFrameIntoClientArea(
            [In] HandleRef hWnd,
            [In] Margins pMargins);

        #endregion

        #region kernel32!FreeLibrary

        [DllImport(Dll.Kernel32, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool FreeLibrary(IntPtr handle);

        #endregion

        #region kernel32!GetProcAddress

        [DllImport(Dll.Kernel32, BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern IntPtr GetProcAddress(
            [In] SafeNativeModuleHandle handle, 
            [In, MarshalAs(UnmanagedType.LPStr)]
                 String lpProcName);

        #endregion

        #region kernel32!LoadLibrary

        [DllImport(Dll.Kernel32, BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern SafeNativeModuleHandle LoadLibrary(string lpFileName);

        #endregion

        #region user32!DestroyIcon

        [DllImport(Dll.User32, BestFitMapping = false, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        #endregion

        #region user32!SendMessage(IntPtr, UInt32, IntPtr, IntPtr)

        [DllImport(
            Dll.User32,
            BestFitMapping = false,
            SetLastError = true
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern IntPtr SendMessage(
            [In] HandleRef hWnd,
            [In] UInt32 Msg,
            [In] IntPtr wParam,
            [In] IntPtr lParam);

        #endregion

        #region uxtheme!GetThemeMargins

        [DllImport(Dll.UxTheme, ExactSpelling = true, PreserveSig = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public extern static void GetThemeMargins(
            IntPtr hTheme,
            IntPtr hdc,
            Int32 iPartId,
            Int32 iStateId,
            MarginType iPropId,
            IntPtr rect,
            out Margins margins);

        #endregion

    }
}
