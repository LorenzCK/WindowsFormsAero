/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;
using System.ComponentModel;
using WindowsFormsAero.Dwm;
using WindowsFormsAero.Native;
using System;

namespace WindowsFormsAero {

    /// <summary>
    /// Base form class that automatically sets its font according
    /// to the Windows UX guidelines and supports some Aero properties.
    /// </summary>
    public class AeroForm : Form {

        private UserPreferenceChangedEventHandler _preferencesHandler;

        public AeroForm() {
            Font = SystemFonts.MessageBoxFont;
            ResizeRedraw = true;

            _preferencesHandler = new UserPreferenceChangedEventHandler(SystemEvents_UserPreferenceChanged);
            SystemEvents.UserPreferenceChanged += _preferencesHandler;
        }

        protected override void Dispose(bool disposing) {
            SystemEvents.UserPreferenceChanged -= _preferencesHandler;

            base.Dispose(disposing);
        }

        void SystemEvents_UserPreferenceChanged(object sender, UserPreferenceChangedEventArgs e) {
            Font = SystemFonts.MessageBoxFont;
        }

        #region Glass

        Padding _glassMargins = Padding.Empty;
        bool _glassEnabled = false;
        bool _glassFull = false;

        /// <summary>
        /// Gets or sets the glass margins of the form.
        /// If set to <see cref="Padding.Empty"/> the glass effect is disabled.
        /// </summary>
        /// <remarks>
        /// The <see cref="Padding"/> value contains the borders (in pixels) that are
        /// extended into the client area. If all padding values are negative, then the
        /// glass area extends to the whole client area.
        /// Client area that is marked as glass MUST have a full black background (this
        /// is handled automatically by <see cref="AeroForm"/>. Controls rendered on TOP
        /// of the glass region must use GDI+ for correct alpha handling (default Win32
        /// and Windows Forms controls do not render correctly). Use the provided
        /// <see cref="ThemeLabel"/> to render text on top of glass.
        /// </remarks>
        [
        Description("The glass margins which are extended inside the client area of the window."),
        Category("Appearance"),
        ]
        public Padding GlassMargins {
            get {
                return _glassMargins;
            }
            set {
                _glassMargins = value;

                _glassEnabled = (_glassMargins != Padding.Empty);
                _glassFull = _glassMargins.AllNegative();

                UpdateGlass();
            }
        }

        private void UpdateGlass() {
            if (DesignMode)
                return;

            if (_glassEnabled) {
                DwmManager.EnableGlassFrame(this, _glassMargins);
            }
            else {
                DwmManager.DisableGlassFrame(this);
            }

            Invalidate();
        }

        #endregion

        #region Style

        bool _hideTitle = false;

        /// <summary>
        /// Gets or sets whether the window title and icon should be hidden.
        /// </summary>
        /// <remarks>
        /// The window caption will still be visible, but title text and icon will not.
        /// A form with a hidden title will look like an Explorer window on Windows Vista
        /// or Windows 7.
        /// </remarks>
        [
        Description("Shows or hides the title and icon of the window."),
        Category("Appearance"), 
        DefaultValue(false)
        ]
        public bool HideTitle {
            get {
                return _hideTitle;
            }
            set {
                if (value != _hideTitle) {
                    _hideTitle = value;
                    ApplyWindowTheme();
                }
            }
        }

        bool _hideCaption = false;

        /// <summary>
        /// Gets or sets whether the window caption should be hidden altogether.
        /// </summary>
        /// <remarks>
        /// Should be set before handle creation.
        /// </remarks>
        [
        Description("Shows or hides the window caption completely."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool HideCaption {
            get {
                return _hideCaption;
            }
            set {
                if (value != _hideCaption) {
                    _hideCaption = value;

                    if (IsHandleCreated) {
                        RecreateHandle();
                    }
                }
            }
        }

        protected override CreateParams CreateParams {
            get {
                var parms = base.CreateParams;

                if (HideCaption) {
                    parms.Style &= ~(int)WindowStyles.Caption; // Removes WS_CAPTION style
                }

                return parms;
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            ApplyWindowTheme();
        }

        private void ApplyWindowTheme() {
            var attr = (HideTitle) ?
                WindowThemeNonClientAttributes.NoDrawCaption | WindowThemeNonClientAttributes.NoDrawIcon :
                WindowThemeNonClientAttributes.NullAttribute;

            WindowTheme.SetWindowThemeNonClientAttributes(Handle,
                WindowThemeNonClientAttributes.NoDrawCaption | WindowThemeNonClientAttributes.NoDrawIcon,
                attr);
        }

        #endregion

        /// <summary>
        /// Gets or sets whether mouse dragging on glass should be handled automatically.
        /// </summary>
        [
        Description("True if mouse dragging of the window on glass should be handled automatically."),
        Category("Behavior"),
        DefaultValue(true)
        ]
        public bool HandleMouseOnGlass {
            get;
            set;
        } = true;

        protected override void OnPaint(PaintEventArgs e) {
            base.OnPaint(e);

            if (_glassEnabled) {
                //Paint glass regions in black
                if (_glassFull) {
                    e.Graphics.Clear(Color.Black);
                }
                else {
                    e.Graphics.FillRectangles(Brushes.Black, new Rectangle[] {
                        new Rectangle(0, 0, ClientSize.Width, _glassMargins.Top),
                        new Rectangle(ClientSize.Width - _glassMargins.Right, 0, _glassMargins.Right, ClientSize.Height),
                        new Rectangle(0, ClientSize.Height - _glassMargins.Bottom, ClientSize.Width, _glassMargins.Bottom),
                        new Rectangle(0, 0, _glassMargins.Left, ClientSize.Height)
                    });
                }
            }
        }

        protected override void WndProc(ref Message m) {
            base.WndProc(ref m);

            if (!HandleMouseOnGlass || !_glassEnabled) {
                return;
            }

            // Respond to hit test messages with Caption if mouse on glass:
            // this automatically enables form dragging on glass
            if (m.Msg == (int)WindowMessage.WM_NCHITTEST &&
                m.Result.ToInt32() == (int)HitTest.HTCLIENT) {

                // Check if mouse pointer is on glass part of form
                if (_glassFull) {
                    m.Result = (IntPtr)HitTest.HTCAPTION;
                }
                else {
                    uint lparam = (uint)m.LParam.ToInt32();
                    ushort x = IntHelpers.LowWord(lparam);
                    ushort y = IntHelpers.HighWord(lparam);

                    var clientPoint = PointToClient(new System.Drawing.Point(x, y));
                    if (_glassMargins.IsOutside(clientPoint, ClientSize)) {
                        m.Result = (IntPtr)HitTest.HTCAPTION;
                    }
                }
            }
        }

    }

}
