using System;
using System.Runtime.ConstrainedExecution;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices.ComTypes;

namespace WindowsFormsAero.InteropServices
{
    [SuppressUnmanagedCodeSecurity]
    internal static class NativeMethods
    {
        #region dwmapi!DwmDefWindowProc

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("dwmapi", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool DwmDefWindowProc(
            [In] HandleRef hwnd,
            [In] Int32 msg,
            [In] IntPtr wParam,
            [In] IntPtr lParam,
            [Out] out IntPtr plResult
        );

        #endregion

        #region dwmapi!DwmGetColorizationColor

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("dwmapi", ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool DwmGetColorizationColor(
            [Out] out int pcrColorization
        );

        #endregion

        #region dwmapi!DwmIsCompositionEnabled

        [DllImport("dwmapi", ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool DwmIsCompositionEnabled();

        #endregion

        #region dwmapi!DwmEnableComposition

        [DllImport("dwmapi", ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern void DwmEnableComposition(
            [In] UInt32 uCompositionAction
        );

        #endregion

        #region dwmapi!DwmEnableBlurBehindWindow

        [DllImport("dwmapi", ExactSpelling = true, PreserveSig = false, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern void DwmEnableBlurBehindWindow(
            [In] HandleRef hWnd,
            [In] DwmBlurBehind pBlurBehind);

        #endregion

        #region dwmapi!DwmExtendFrameIntoClientArea

        [DllImport("dwmapi", ExactSpelling = true, PreserveSig = false, SetLastError = false )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern void DwmExtendFrameIntoClientArea(
            [In] HandleRef hWnd,
            [In] MARGINS pMargins);

        #endregion


        #region gdi32!GetDeviceCaps

        [DllImport(
            "gdi32",
            SetLastError = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern int GetDeviceCaps([In] IntPtr hDC, [In] DeviceCapability nIndex);

        #endregion


        #region kernel32!FreeLibrary

        [DllImport("kernel32", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool FreeLibrary(IntPtr handle);

        #endregion

        #region kernel32!GetProcAddress

        [DllImport("kernel32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr GetProcAddress(
            [In] NativeModule handle, 
            [In, MarshalAs(UnmanagedType.LPStr)]
                 String lpProcName);

        #endregion

        #region kernel32!IsWow64Process

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32", BestFitMapping = false, ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool IsWow64Process(
            [In] IntPtr hProcess, 
            [Out, MarshalAs(UnmanagedType.Bool)] 
                 out bool pOldValue);

        #endregion

        #region kernel32!LoadLibrary

        [DllImport("kernel32", BestFitMapping = false, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern NativeModule LoadLibrary(string lpFileName);

        #endregion

        #region kernel32!Wow64DisableWow64FsRedirection

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32", BestFitMapping = false, ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool Wow64DisableWow64FsRedirection([Out] out IntPtr pOldValue);

        #endregion

        #region kernel32!Wow64RevertWow64FsRedirection

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32", BestFitMapping = false, ExactSpelling = true, CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool Wow64RevertWow64FsRedirection([In] IntPtr pOldValue);

        #endregion


        #region ole32!CreateILockBytesOnHGlobal

        [DllImport(
            "ole32",
            PreserveSig = false,
            SetLastError = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern ILockBytes CreateILockBytesOnHGlobal(
            [In] IntPtr hGlobal,
            [In, MarshalAs(UnmanagedType.Bool)] 
                 Boolean fDeleteOnRelease);

        #endregion

        #region ole32!OleCreateStaticFromData

        [DllImport("ole32", PreserveSig = false, CharSet = CharSet.Unicode, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr OleCreateStaticFromData(
            [In, MarshalAs(UnmanagedType.Interface)]
                 System.Runtime.InteropServices.ComTypes.IDataObject pSrcDataObj,
            [In] ref Guid riid, 
            [In] OleRender renderopt, 
            [In] ref FORMATETC pFormatEtc,
            [In, MarshalAs(UnmanagedType.Interface)]
                 IOleClientSite pClientSite,
            [In, MarshalAs(UnmanagedType.Interface)]
                 IStorage pStg);

        #endregion

        #region ole32!OleSetContainedObject

        [DllImport("ole32", PreserveSig = false, CharSet = CharSet.Unicode, SetLastError = false)]
        internal static extern void OleSetContainedObject(
            [In] IntPtr pUnk, 
            [In, MarshalAs(UnmanagedType.Bool)]
                 Boolean fContained);


        #endregion

        #region ole32!StgCreateDocfileOnILockBytes

        [return: MarshalAs(UnmanagedType.Interface)]
        [DllImport("ole32", PreserveSig = false, CharSet = CharSet.Unicode, SetLastError = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IStorage StgCreateDocfileOnILockBytes(
            [In] ILockBytes plkbyt,
            [In] StorageFlags grfMode,
            [In] UInt32 reserved);

        #endregion


        #region user32!AdjustWindowRectEx

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32", SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool AdjustWindowRectEx(
            [In, Out] ref RECT    lpRect,
            [In]      UInt32  dwStyle,
            [In, MarshalAs(UnmanagedType.Bool)] Boolean bMenu,
            [In]      Int32   dwExStyle);

        #endregion

        #region user32!CallNextHookEx

        [DllImport(
            "user32",
            CharSet = CharSet.Auto,
            SetLastError = true,
            BestFitMapping = false,
            CallingConvention = CallingConvention.Winapi)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr CallNextHookEx(
            [In] IntPtr hhk,
            [In] Int32 nCode,
            [In] IntPtr wParam,
            [In] KeyboardLowLevelHookInfo lParam);

        #endregion

        #region user32!DestroyIcon

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32", BestFitMapping = false, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool DestroyIcon(IntPtr hIcon);

        #endregion

        #region user32!GetAncestor

        [DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr GetAncestor(IntPtr hWnd, AncestorType gaFlags);

        #endregion

        #region user32!GetDC

        [DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr GetDC(HandleRef hWnd);

        #endregion

        #region user32!GetForegroundWindow

        [DllImport(
            "user32",
            SetLastError = true,
            ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr GetForegroundWindow();

        #endregion

        #region user32!GetKeyboardLayout

        [DllImport(
            "user32",
            EntryPoint = "GetKeyboardLayout",
            ExactSpelling = true,
            SetLastError = true,
            CallingConvention = CallingConvention.Winapi)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr GetKeyboardLayout([In] Int32 dwThread);

        #endregion

        #region user32!GetKeyboardState

        [DllImport(
            "user32",
            SetLastError = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [return: MarshalAs(UnmanagedType.Bool)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool GetKeyboardState(byte[] lpKeyState);

        #endregion

        #region user32!GetWindowThreadProcessId

        [DllImport(
            "user32",
            SetLastError = true,
            ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [SuppressUnmanagedCodeSecurity]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern Int32 GetWindowThreadProcessId(
            [In] IntPtr hWnd,
            [Out] out Int32 lpdwProcessId
        );

        #endregion

        #region user32!IsChild

        [DllImport(
            "user32",
            SetLastError = false,
            ExactSpelling = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool IsChild(HandleRef hWndParent, IntPtr hWnd);

        #endregion

        #region user32!LoadImage

        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true, ThrowOnUnmappableChar = true, BestFitMapping = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr LoadImage(
            [In] IntPtr hinst,
            [In] IntPtr lpszName,
            [In] ImageType uType,
            [In] Int32 cxDesired,
            [In] Int32 cyDesired,
            [In] LoadImageFlags fuLoad
        );

        #endregion

        #region user32!MapVirtualKey

        [DllImport(
            "user32",
            SetLastError = true,
            CharSet = CharSet.Auto,
            BestFitMapping = false,
            ThrowOnUnmappableChar = true,
            CallingConvention = CallingConvention.Winapi
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern uint MapVirtualKey(
            [In] UInt32 uCode,
            [In] VirtualKeyMapType uMapType
        );

        #endregion

        #region user32!RegisterWindowMessage

        [DllImport(
            "user32",
            CharSet = CharSet.Auto,
            SetLastError = true,
            BestFitMapping = false,
            CallingConvention = CallingConvention.Winapi)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern Int32 RegisterWindowMessage(
            [In, MarshalAs(UnmanagedType.LPTStr)] String lpString
        );

        #endregion

        #region user32!ReleaseDC

        [DllImport("user32", CharSet = CharSet.Auto, ExactSpelling = true, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool ReleaseDC(HandleRef hWnd, IntPtr hDC);

        #endregion

        #region user32!SendMessage(IntPtr, UInt32, IntPtr, IntPtr)

        [DllImport(
            "user32",
            BestFitMapping = false,
            SetLastError = true
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr SendMessage(
            [In] HandleRef hWnd,
            [In] UInt32 Msg,
            [In] IntPtr wParam,
            [In] IntPtr lParam);

        #endregion

        #region user32!SendMessage(IntPtr, UInt32, IntPtr, [IUnknown] out Object)

        [DllImport(
            "user32",
            BestFitMapping = false,
            SetLastError = true
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr SendMessage(
            [In] HandleRef hWnd,
            [In] UInt32 Msg,
            [In] IntPtr wParam,
            [Out, MarshalAs(UnmanagedType.IUnknown)] 
                 out Object lParam);

        #endregion

        #region user32!SendMessage(IntPtr, UInt32, IntPtr, [Any] Object)

        [DllImport(
            "user32",
            BestFitMapping = false,
            SetLastError = true
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr SendMessage(
            [In] HandleRef hWnd,
            [In] UInt32 Msg,
            [In] IntPtr wParam,
            [In, Out, MarshalAs(UnmanagedType.AsAny)] 
                 Object lParam);

        #endregion

        #region user32!SendMessage(IntPtr, UInt32, IntPtr, ref RECT)

        [DllImport(
            "user32",
            BestFitMapping = false,
            SetLastError = true
        )]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern IntPtr SendMessage(
            [In] HandleRef hWnd,
            [In] UInt32 Msg,
            [In] IntPtr wParam,
            [In, Out] 
                 ref RECT lParam);

        #endregion

        #region user32!SetWindowPos

        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("user32", CharSet = CharSet.Auto, SetLastError = true)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool SetWindowPos(
            [In] HandleRef hWnd,
            [In] IntPtr hWndInsertAfter,
            [In] Int32 x,
            [In] Int32 y,
            [In] Int32 cx,
            [In] Int32 cy,
            [In] SetWindowPosFlags uFlags);

        #endregion

        #region user32!SetWindowsHookEx

        [DllImport(
            "user32",
            BestFitMapping = false,
            CharSet = CharSet.Unicode,
            EntryPoint = "SetWindowsHookExW",
            ExactSpelling = true,
            SetLastError = true,
            CallingConvention = CallingConvention.Winapi)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern SafeWindowsHookHandle SetWindowsHookEx(
            [In] WindowsHookType idHook,
            [In] IntPtr lpfn,
            [In] NativeModule hMod,
            [In] Int32 dwThreadId);

        internal static SafeWindowsHookHandle SetWindowsHookEx(WindowsHookType hookType, Delegate procedure)
        {
            return SetWindowsHookEx(hookType, Marshal.GetFunctionPointerForDelegate(procedure), NativeModule.Invalid, 0);
        }

        internal static SafeWindowsHookHandle SetWindowsHookEx(KeyboardLowLevelHookProc proc)
        {
            return SetWindowsHookEx(WindowsHookType.KeyboardLowLevel, proc);
        }

        #endregion

        #region user32!ToUnicodeEx

        [DllImport(
            "user32",
            BestFitMapping = false, 
            CharSet = CharSet.Unicode,
            EntryPoint = "ToUnicodeEx",
            ExactSpelling = true, 
            SetLastError = true,
            ThrowOnUnmappableChar = true,
            CallingConvention = CallingConvention.Winapi)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern Int32 ToUnicodeEx(
            [In] Keys wVirtKey,
            [In] UInt32 wScanCode,
            [In] Byte[] lpKeyState,
            [Out, MarshalAs(UnmanagedType.LPWStr)] StringBuilder pwszBuff,
            [In] Int32 cchBuff,
            [In] UInt32 wFlags,
            [In] IntPtr hkl);

        #endregion

        #region user32!UnhookWindowsHookEx

        [DllImport(
            "user32",
            BestFitMapping = false,
            CharSet = CharSet.Unicode,
            EntryPoint = "UnhookWindowsHookEx",
            ExactSpelling = true,
            SetLastError = true,
            CallingConvention = CallingConvention.Winapi)]
        [return: MarshalAs(UnmanagedType.Bool)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern bool UnhookWindowsHookEx(
            [In] IntPtr hhk);

        #endregion

        #region uxtheme!DrawThemeTextEx

        [DllImport("uxtheme", CharSet = CharSet.Unicode, SetLastError = false, PreserveSig = false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern void DrawThemeTextEx(
            [In] IntPtr hTheme,
            [In] IntPtr hdc,
            [In] Int32 iPartId,
            [In] Int32 iStateId,
            [In] String text, 
            [In] Int32 iCharCount, 
            [In] TextFormatFlags dwFlags,
            [In, Out] ref RECT pRect,
            [In] DTTOPTS pOptions);

        #endregion

        #region uxtheme!SetWindowTheme

        [DllImport("uxtheme", CharSet= CharSet.Unicode, PreserveSig =false, SetLastError=false)]
        [ReliabilityContract(Consistency.WillNotCorruptState, Cer.MayFail)]
        internal static extern void SetWindowTheme(
            [In] HandleRef hWnd,
            [In] String pszSubAppName,
            [In] String pszSubIdList);

        #endregion
    }
}
