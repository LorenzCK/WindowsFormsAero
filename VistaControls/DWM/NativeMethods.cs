/*****************************************************
 *            Vista Controls for .NET 2.0
 * 
 * http://www.codeplex.com/vistacontrols
 * 
 * @author: Lorenz Cuno Klopfenstein
 * Licensed under Microsoft Community License (Ms-CL)
 * 
 *****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace VistaControls.DWM
{
    internal static class NativeMethods
    {

        #region DWM Thumbnail methods

        [Flags()]
        public enum DwmThumbnailFlags
        {
            RectDestination = 0x1,
            RectSource = 0x2,
            Opacity = 0x4,
            Visible = 0x8,
            SourceClientAreaOnly = 0x10
        }

        public struct DwmThumbnailProperties
        {
            public DwmThumbnailFlags dwFlags;
            public Native.RECT rcDestination;
            public Native.RECT rcSource;
            public byte opacity;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fVisible;
            [MarshalAs(UnmanagedType.Bool)]
            public bool fSourceClientAreaOnly;
        }



        [DllImport("dwmapi.dll")]
        public static extern int DwmRegisterThumbnail(IntPtr hwndDestination, IntPtr hwndSource, out Thumbnail phThumbnailId);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUnregisterThumbnail(IntPtr hThumbnailId);

        [DllImport("dwmapi.dll")]
        public static extern int DwmUpdateThumbnailProperties(Thumbnail hThumbnailId, ref DwmThumbnailProperties ptnProperties);

        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)] out bool pfEnabled);

        #endregion

        #region DWM Blur Behind

        [StructLayout(LayoutKind.Sequential)]
        public struct BlurBehind {
            public BlurBehindFlags dwFlags;
            public bool fEnable;
            public IntPtr hRgnBlur;
            public bool fTransitionOnMaximized;
        }

        public enum BlurBehindFlags : int {
            Enable = 0x00000001,
            BlurRegion = 0x00000002,
            TransitionOnMaximized = 0x00000004
        }

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern int DwmEnableBlurBehindWindow(IntPtr hWnd, ref BlurBehind pBlurBehind);

        #endregion

        #region DWM Glass Frame

        [DllImport("dwmapi.dll", PreserveSig = false)]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref Margins pMarInset);

        #endregion

	}
}
