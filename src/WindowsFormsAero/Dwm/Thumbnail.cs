/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Drawing;
using System.Security.Permissions;
using WindowsFormsAero.Native;
using WindowsFormsAero.Resources;

namespace WindowsFormsAero.Dwm {

    /// <summary>
    /// Handle to a DWM Thumbnail.
    /// </summary>
    /// <remarks>
    /// Handles to <see cref="Thumbnail"/> can be created only through
    /// <see cref="DwmManager"/> by registering a new thumbnail of an existing Form or
    /// Win32 window. Thumbnails can be manipulated and should be disposed through this
    /// class.
    /// Thumbnails can be automatically handled by the <see cref="ThumbnailViewer"/>
    /// Windows Forms control.
    /// The <see cref="Update(Rectangle, byte, bool, bool)"/> or <see
    /// cref="Update(Rectangle, Rectangle, byte, bool, bool)"/> methods must be called
    /// at least once in order for the Thumbnail to be visible.
    /// </remarks>
    public sealed class Thumbnail : System.Runtime.InteropServices.SafeHandle {

        internal Thumbnail()
            : base(IntPtr.Zero, true) {
        }

        internal Thumbnail(IntPtr handle)
            : base(IntPtr.Zero, true) {

            SetHandle(handle);
        }

        #region Handle logic

        public override bool IsInvalid {
            [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
            get {
                return (IsClosed || handle == IntPtr.Zero);
            }
        }

        [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
        protected override bool ReleaseHandle() {
            if(handle == IntPtr.Zero) {
                return true;
            }

            //Unregister the thumbnail
            return (DwmMethods.DwmUnregisterThumbnail(handle) == 0);
        }

        #endregion Handle logic

        #region Thumbnail properties and methods

        byte _opacity = 255;

        /// <summary>
        /// Gets or sets the thumbnail opacity value, from 0 (transparent) to 255 (fully
        /// opaque).
        /// </summary>
        /// <remarks>
        /// This property appears to be ignored in Windows 10 Anniversary Update.
        /// </remarks>
        public byte Opacity {
            set {
                var prop = new DwmThumbnailProperties {
                    opacity = value,
                    dwFlags = DwmThumbnailFlags.Opacity
                };

                if (DwmMethods.DwmUpdateThumbnailProperties(handle, ref prop) != 0) {
                    throw new DwmCompositionException(ExceptionMessages.DwmThumbnailUpdateFailure);
                }

                _opacity = value;
            }
            get {
                return _opacity;
            }
        }

        bool _clientArea = false;

        /// <summary>
        /// Gets or sets whether only the client area of the thumbnailed window should be
        /// shown or its entire window area.
        /// </summary>
        public bool ShowOnlyClientArea {
            set {
                var prop = new DwmThumbnailProperties {
                    fSourceClientAreaOnly = value,
                    dwFlags = DwmThumbnailFlags.SourceClientAreaOnly
                };

                if (DwmMethods.DwmUpdateThumbnailProperties(handle, ref prop) != 0)
                    throw new DwmCompositionException(ExceptionMessages.DwmThumbnailUpdateFailure);

                _clientArea = value;
            }
            get {
                return _clientArea;
            }
        }

        private Rectangle _destination;

        /// <summary>
        /// Gets or sets the area in the destination window on which the thumbnail should
        /// be drawn.
        /// </summary>
        public Rectangle DestinationRectangle {
            set {
                var prop = new DwmThumbnailProperties {
                    rcDestination = new Rect(value),
                    dwFlags = DwmThumbnailFlags.RectDestination
                };

                if (DwmMethods.DwmUpdateThumbnailProperties(handle, ref prop) != 0)
                    throw new DwmCompositionException(ExceptionMessages.DwmThumbnailUpdateFailure);
            }
            get {
                return _destination;
            }
        }

        /// <summary>
        /// Sets the region of the source window that should be drawn.
        /// </summary>
        /// <remarks>
        /// This read-only property cannot be unset once set.
        /// In order to reset the Thumbnail's source rectangle, a new  instance must be
        /// created.
        /// </remarks>
        public Rectangle SourceRectangle {
            set {
                if (value.Width < 1 || value.Height < 1)
                    throw new ArgumentException(ExceptionMessages.DwmThumbnailSourceInvalid);

                var prop = new DwmThumbnailProperties {
                    rcSource = new Rect(value),
                    dwFlags = DwmThumbnailFlags.RectSource
                };

                if (DwmMethods.DwmUpdateThumbnailProperties(handle, ref prop) != 0)
                    throw new DwmCompositionException(ExceptionMessages.DwmThumbnailUpdateFailure);
            }
        }

        bool _visible = false;

        /// <summary>
        /// Gets or sets whether the thumbnail should be shown or not.
        /// </summary>
        public bool Visible {
            set {
                var prop = new DwmThumbnailProperties {
                    fVisible = value,
                    dwFlags = DwmThumbnailFlags.Visible
                };

                if (DwmMethods.DwmUpdateThumbnailProperties(handle, ref prop) != 0)
                    throw new DwmCompositionException(ExceptionMessages.DwmThumbnailUpdateFailure);
            }
            get {
                return _visible;
            }
        }

        /// <summary>
        /// Retrieves the thumbnailed window's size.
        /// </summary>
        public System.Drawing.Size GetSourceSize() {
            DwmSize size;
            if (DwmMethods.DwmQueryThumbnailSourceSize(handle, out size) != 0)
                throw new DwmCompositionException(ExceptionMessages.DwmThumbnailQueryFailure);

            return size.ToSize();
        }

        /// <summary>
        /// Updates the thumbnail's display settings.
        /// </summary>
        /// <param name="destination">Drawing region on destination window.</param>
        /// <param name="source">Origin region from source window.</param>
        /// <param name="opacity">Opacity. 0 is transparent, 255 opaque.</param>
        /// <param name="visible">Visibility flag.</param>
        /// <param name="onlyClientArea">
        /// If true, only the client area of the window will be rendered. Otherwise, the
        /// borders will be be rendered as well.
        /// </param>
        public void Update(Rectangle destination, Rectangle source, byte opacity, bool visible, bool onlyClientArea) {
            if (source.Width < 1 || source.Height < 1)
                throw new ArgumentException(ExceptionMessages.DwmThumbnailSourceInvalid);

            var prop = new DwmThumbnailProperties {
                rcDestination = new Rect(destination),
                rcSource = new Rect(source),
                opacity = opacity,
                fVisible = visible,
                fSourceClientAreaOnly = onlyClientArea,
                dwFlags = DwmThumbnailFlags.RectDestination |
                          DwmThumbnailFlags.RectSource |
                          DwmThumbnailFlags.Opacity |
                          DwmThumbnailFlags.Visible |
                          DwmThumbnailFlags.SourceClientAreaOnly
            };

            if (DwmMethods.DwmUpdateThumbnailProperties(handle, ref prop) != 0)
                throw new DwmCompositionException(ExceptionMessages.DwmThumbnailUpdateFailure);

            _destination = destination;
            _opacity = opacity;
            _visible = visible;
            _clientArea = ShowOnlyClientArea;
        }

        /// <summary>
        /// Updates the thumbnail's display settings.
        /// </summary>
        /// <param name="destination">Drawing region on destination window.</param>
        /// <param name="opacity">Opacity. 0 is transparent, 255 opaque.</param>
        /// <param name="visible">Visibility flag.</param>
        /// <param name="onlyClientArea">
        /// If true, only the client area of the window will be rendered. Otherwise, the
        /// borders will be be rendered as well.
        /// </param>
        public void Update(Rectangle destination, byte opacity, bool visible, bool onlyClientArea) {
            var prop = new DwmThumbnailProperties {
                rcDestination = new Rect(destination),
                opacity = opacity,
                fVisible = visible,
                fSourceClientAreaOnly = onlyClientArea,
                dwFlags = DwmThumbnailFlags.RectDestination |
                          DwmThumbnailFlags.Opacity |
                          DwmThumbnailFlags.Visible |
                          DwmThumbnailFlags.SourceClientAreaOnly
            };

            if (DwmMethods.DwmUpdateThumbnailProperties(handle, ref prop) != 0)
                throw new DwmCompositionException(ExceptionMessages.DwmThumbnailUpdateFailure);

            _destination = destination;
            _opacity = opacity;
            _visible = visible;
            _clientArea = ShowOnlyClientArea;
        }

        #endregion

    }

}
