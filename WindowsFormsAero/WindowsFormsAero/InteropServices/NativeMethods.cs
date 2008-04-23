using System;
using System.Runtime.InteropServices;
using System.Runtime.ConstrainedExecution;
using System.Windows.Forms;

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

        #region dwmapi!DwmDefWindowProc

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(Dll.DwmApi, CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern bool DwmDefWindowProc(
            [In] HandleRef hwnd,
            [In] Int32 msg,
            [In] IntPtr wParam,
            [In] IntPtr lParam,
            [Out] out IntPtr plResult
        );

        #endregion

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
            [In] MARGINS pMargins);

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

        #region user32!AdjustWindowRectEx

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(Dll.User32, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern bool AdjustWindowRectEx(
            [In, Out] RECT    lpRect,
            [In]      UInt32  dwStyle,
            [In, MarshalAs(UnmanagedType.Bool)] Boolean bMenu,
            [In]      Int32   dwExStyle);

        #endregion

        #region user32!DestroyIcon

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(Dll.User32, BestFitMapping = false, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern bool DestroyIcon(IntPtr hIcon);

        #endregion

        #region user32!GetAncestor

        [DllImport(Dll.User32, CharSet = CharSet.Auto, ExactSpelling = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern IntPtr GetAncestor(IntPtr hWnd, AncestorType gaFlags);

        #endregion

        #region user32!GetKeyState

        [DllImport(Dll.User32, CharSet = CharSet.Auto, ExactSpelling = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern short GetKeyState(Keys keyCode);

        #endregion

        #region user32!LoadImage

        [DllImport(Dll.User32, CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern IntPtr LoadImage(
            [In] IntPtr hinst,
            [In] IntPtr lpszName,
            [In] ImageType uType,
            [In] Int32 cxDesired,
            [In] Int32 cyDesired,
            [In] LoadImageFlags fuLoad
        );

        #endregion

        #region user32!IsChild

        [DllImport(
            Dll.User32,
            SetLastError = false,
            ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool IsChild(HandleRef hWndParent, IntPtr hWnd);

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

        #region user32!SetWindowPos

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport(Dll.User32, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern bool SetWindowPos(
            [In] HandleRef hWnd,
            [In] IntPtr hWndInsertAfter,
            [In] Int32 x,
            [In] Int32 y,
            [In] Int32 cx,
            [In] Int32 cy,
            [In] SetWindowPosFlags uFlags);

        #endregion

        #region uxtheme!DrawThemeTextEx

        [DllImport(Dll.UxTheme, CharSet = CharSet.Unicode, SetLastError = false, PreserveSig = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        public static extern void DrawThemeTextEx(
            [In] IntPtr hTheme,
            [In] IntPtr hdc,
            [In] Int32 iPartId,
            [In] Int32 iStateId,
            [In] String text, 
            [In] Int32 iCharCount, 
            [In] TextFormatFlags dwFlags,
            [In, Out] RECT pRect,
            [In] DTTOPTS pOptions);

        #endregion
    }
}
