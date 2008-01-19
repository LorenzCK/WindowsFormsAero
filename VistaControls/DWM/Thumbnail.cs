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

namespace VistaControls.DWM
{
    /// <summary>Handle to a DWM Thumbnail.</summary>
    public sealed class Thumbnail : System.Runtime.InteropServices.SafeHandle
    {
        internal Thumbnail()
            : base(IntPtr.Zero, true) {
        }

        #region Handle logic

        /// <summary>Returns true if the handle is valid, false if the handle has been closed or hasn't been initialized.</summary>
        public override bool IsInvalid {
            [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
            get {
                return (IsClosed || handle == IntPtr.Zero);
            }
        }

        [System.Security.Permissions.SecurityPermission(System.Security.Permissions.SecurityAction.LinkDemand, UnmanagedCode = true)]
        protected override bool ReleaseHandle() {
            //Unregister the thumbnail
            return (NativeMethods.DwmUnregisterThumbnail(handle) == 0);
        }

        #endregion

        #region Thumbnail properties

		/// <summary>Sets the thumbnail opacity value, from 0 to 255 (opaque).</summary>
        public byte Opacity {
            set {
                NativeMethods.DwmThumbnailProperties prop = new NativeMethods.DwmThumbnailProperties();
                prop.dwFlags = NativeMethods.DwmThumbnailFlags.Opacity;

                prop.opacity = value;

                if (NativeMethods.DwmUpdateThumbnailProperties(this, ref prop) != 0)
                    throw new DWMCompositionException("Unable to update thumbnail properties.");
            }
        }

		/// <summary>Sets whether only the client area of the thumbnailed window should be shown or
		/// the entire window area.</summary>
        public bool ShowOnlyClientArea {
            set {
                NativeMethods.DwmThumbnailProperties prop = new NativeMethods.DwmThumbnailProperties();
                prop.dwFlags = NativeMethods.DwmThumbnailFlags.SourceClientAreaOnly;

                prop.fSourceClientAreaOnly = value;

                if (NativeMethods.DwmUpdateThumbnailProperties(this, ref prop) != 0)
                    throw new DWMCompositionException("Unable to update thumbnail properties.");
            }
        }

		/// <summary>Area in the destination window on which the thumbnail should be drawn.</summary>
        public System.Drawing.Rectangle DestinationRectangle {
            set {
                NativeMethods.DwmThumbnailProperties prop = new NativeMethods.DwmThumbnailProperties();
                prop.dwFlags = NativeMethods.DwmThumbnailFlags.RectDestination;

                prop.rcDestination = new Native.RECT(value);

                if (NativeMethods.DwmUpdateThumbnailProperties(this, ref prop) != 0)
                    throw new DWMCompositionException("Unable to update thumbnail properties.");
            }
        }

		/// <summary>Region of the source window that should be drawn.</summary>
        public System.Drawing.Rectangle SourceRectangle {
            set {
                NativeMethods.DwmThumbnailProperties prop = new NativeMethods.DwmThumbnailProperties();
                prop.dwFlags = NativeMethods.DwmThumbnailFlags.RectSource;

				prop.rcSource = new Native.RECT(value);

                if (NativeMethods.DwmUpdateThumbnailProperties(this, ref prop) != 0)
                    throw new DWMCompositionException("Unable to update thumbnail properties.");
            }
        }

		/// <summary>Sets whether the thumbnail should be drawn or not.</summary>
        public bool Visible {
            set {
                NativeMethods.DwmThumbnailProperties prop = new NativeMethods.DwmThumbnailProperties();
                prop.dwFlags = NativeMethods.DwmThumbnailFlags.Visible;

                prop.fVisible = value;

                if (NativeMethods.DwmUpdateThumbnailProperties(this, ref prop) != 0)
                    throw new DWMCompositionException("Unable to update thumbnail properties.");
            }
        }

        #endregion

        #region Thumbnail updating

        /// <summary>Updates the thumbnail's display settings.</summary>
        /// <param name="destination">Drawing region on destination window.</param>
        /// <param name="source">Origin region from source window.</param>
        /// <param name="opacity">Opacity. 0 is transparent, 255 opaque.</param>
        /// <param name="visible">Visibility flag.</param>
        /// <param name="onlyClientArea">If true, only the client area of the window will be rendered. Otherwise, the borders will be be rendered as well.</param>
        public void Update(Rectangle destination, Rectangle source, byte opacity, bool visible, bool onlyClientArea) {
            //Full update
            NativeMethods.DwmThumbnailProperties prop = new NativeMethods.DwmThumbnailProperties();
            prop.dwFlags = NativeMethods.DwmThumbnailFlags.RectDestination |
                           NativeMethods.DwmThumbnailFlags.RectSource |
                           NativeMethods.DwmThumbnailFlags.Opacity |
                           NativeMethods.DwmThumbnailFlags.Visible |
                           NativeMethods.DwmThumbnailFlags.SourceClientAreaOnly;

			prop.rcDestination = new Native.RECT(destination);
			prop.rcSource = new Native.RECT(source);
            prop.opacity = opacity;
            prop.fVisible = visible;
            prop.fSourceClientAreaOnly = onlyClientArea;

            if (NativeMethods.DwmUpdateThumbnailProperties(this, ref prop) != 0)
                throw new DWMCompositionException("Unable to update thumbnail properties.");
        }

        /// <summary>Updates the thumbnail's display settings.</summary>
        /// <param name="destination">Drawing region on destination window.</param>
        /// <param name="opacity">Opacity. 0 is transparent, 255 opaque.</param>
        /// <param name="visible">Visibility flag.</param>
        /// <param name="onlyClientArea">If true, only the client area of the window will be rendered. Otherwise, the borders will be be rendered as well.</param>
        public void Update(Rectangle destination, byte opacity, bool visible, bool onlyClientArea) {
            //Full update
            NativeMethods.DwmThumbnailProperties prop = new NativeMethods.DwmThumbnailProperties();
            prop.dwFlags = NativeMethods.DwmThumbnailFlags.RectDestination |
                           NativeMethods.DwmThumbnailFlags.Opacity |
                           NativeMethods.DwmThumbnailFlags.Visible |
                           NativeMethods.DwmThumbnailFlags.SourceClientAreaOnly;

			prop.rcDestination = new Native.RECT(destination);
            prop.opacity = opacity;
            prop.fVisible = visible;
            prop.fSourceClientAreaOnly = onlyClientArea;

            if (NativeMethods.DwmUpdateThumbnailProperties(this, ref prop) != 0)
                throw new DWMCompositionException("Unable to update thumbnail properties.");
        }

        #endregion

    }
}
