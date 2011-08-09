using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using Microsoft.Win32;

namespace WindowsFormsAero {

    /// <summary>
    /// Base form class that automatically sets its font according to the Windows UX guidelines.
    /// </summary>
    public class AeroForm : Form {

        UserPreferenceChangedEventHandler _preferencesHandler;

        /// <summary>
        /// Constructs a new Aero styled form.
        /// </summary>
        public AeroForm() {
            Font = SystemFonts.MessageBoxFont;

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

    }

}
