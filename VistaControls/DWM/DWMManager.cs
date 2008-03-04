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
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VistaControls.DWM
{

	/// <summary>Main DWM class, provides Thumbnail registration, glass sheet effect and blur behind.</summary>
    public static class DWMManager
    {

        #region Thumbnail registration and unregistration

        /// <summary>Registers a thumbnail to be drawn on a Windows Form.</summary>
        /// <remarks>The thumbnail will not be drawn until you update the thumbnail's properties calling Update().</remarks>
        /// <param name="destination">The Windows Form instance on which to draw the thumbnail.</param>
        /// <param name="source">The handle (HWND) of the window that has to be drawn.</param>
        /// <returns>A Thumbnail instance, needed to unregister and to update properties.</returns>
        public static Thumbnail Register(Form destination, IntPtr source) {
            return Register(destination.Handle, source);
        }

        /// <summary>Registers a thumbnail to be drawn on a window.</summary>
        /// <remarks>The thumbnail will not be drawn until you update the thumbnail's properties calling Update().</remarks>
        /// <param name="destination">The handle (HWND) of the window on which the thumbnail will be drawn.</param>
        /// <param name="source">The handle (HWND) of the window that has to be drawn.</param>
        /// <returns>A Thumbnail instance, needed to unregister and to update properties.</returns>
        public static Thumbnail Register(IntPtr destination, IntPtr source) {
            if (!OSSupport.IsVistaOrBetter)
                throw new DWMCompositionException(Resources.ExceptionMessages.DWMOsNotSupported);

            if (!OSSupport.IsCompositionEnabled)
                throw new DWMCompositionException(Resources.ExceptionMessages.DWMNotEnabled);

            if (destination == source)
                throw new DWMCompositionException(Resources.ExceptionMessages.DWMWindowMatch);

            Thumbnail ret = new Thumbnail();

			if (NativeMethods.DwmRegisterThumbnail(destination, source, out ret) == 0) {
				return ret;
			}
			else {
				throw new DWMCompositionException(String.Format(Resources.ExceptionMessages.NativeCallFailure, "DwmRegisterThumbnail"));
			}
        }


        /// <summary>Unregisters the thumbnail handle.</summary>
        /// <remarks>The handle is unvalid after the call and should not be used again.</remarks>
        /// <param name="handle">A handle to a registered thumbnail.</param>
        public static void Unregister(Thumbnail handle) {
            if (handle != null && !handle.IsInvalid) {
                handle.Close();
            }
        }

        #endregion

        #region Blur Behind

        /// <summary>Enable the Aero "Blur Behind" effect on the whole client area. Background must be black.</summary>
        public static void EnableBlurBehind(IntPtr hWnd) {
            NativeMethods.BlurBehind bb = new NativeMethods.BlurBehind();
            bb.dwFlags = NativeMethods.BlurBehindFlags.Enable;
            bb.fEnable = true;
            bb.hRgnBlur = (IntPtr)0;

            NativeMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

		/// <summary>Enable the Aero "Blur Behind" effect on the whole client area. Background must be black.</summary>
		/// <param name="form"></param>
		public static void EnableBlurBehind(Form form) {
			EnableBlurBehind(form.Handle);
		}

        /// <summary>Enable the Aero "Blur Behind" effect on a specific region. Background of the region must be black.</summary>
        public static void EnableBlurBehind(IntPtr hWnd, IntPtr regionHandle) {
            NativeMethods.BlurBehind bb = new NativeMethods.BlurBehind();
            bb.dwFlags = NativeMethods.BlurBehindFlags.Enable | NativeMethods.BlurBehindFlags.BlurRegion;
            bb.fEnable = true;
            bb.hRgnBlur = regionHandle;

            NativeMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

		/// <summary>Disables the Aero "Blur Behind" effect.</summary>
        public static void DisableBlurBehind(IntPtr hWnd) {
            NativeMethods.BlurBehind bb = new NativeMethods.BlurBehind();
            bb.dwFlags = NativeMethods.BlurBehindFlags.Enable;
            bb.fEnable = false;

            NativeMethods.DwmEnableBlurBehindWindow(hWnd, ref bb);
        }

        #endregion

        #region Glass Frame

        /// <summary>Extends the Aero "Glass Frame" into the client area. Background must be black.</summary>
        public static void EnableGlassFrame(Form window, Margins margins) {
            InternalGlassFrame(window.Handle, margins);
        }

        /// <summary>Extends the Aero "Glass Frame" into the client area. Background must be black.</summary>
        public static void EnableGlassFrame(IntPtr hWnd, Margins margins) {
            InternalGlassFrame(hWnd, margins);
        }

        /// <summary>Extends the Aero "Glass Frame" to the whole client area ("Glass Sheet" effect). Background must be black.</summary>
        public static void EnableGlassSheet(Form window) {
            InternalGlassFrame(window.Handle, new Margins(-1));
        }

        /// <summary>Extends the Aero "Glass Frame" to the whole client area ("Glass Sheet" effect). Background must be black.</summary>
        public static void EnableGlassSheet(IntPtr hWnd) {
            InternalGlassFrame(hWnd, new Margins(-1));
        }

		/// <summary>Disables the Aero "Glass Frame".</summary>
		public static void DisableGlassFrame(Form window) {
			InternalGlassFrame(window.Handle, new Margins(0));
		}

        /// <summary>Disables the Aero "Glass Frame".</summary>
        public static void DisableGlassFrame(IntPtr hWnd) {
            InternalGlassFrame(hWnd, new Margins(0));
        }

        private static void InternalGlassFrame(IntPtr hWnd, Margins margins) {
            if (NativeMethods.DwmExtendFrameIntoClientArea(hWnd, ref margins) != 0)
                throw new DWMCompositionException(String.Format(Resources.ExceptionMessages.NativeCallFailure, "DwmExtendFrameIntoClientArea"));
        }

        #endregion

	}
}
