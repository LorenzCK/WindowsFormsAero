/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Marco Minerva <marco.minerva@gmail.com>
 *****************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using WindowsFormsAero.Native;

namespace WindowsFormsAero {

    [ToolboxBitmap(typeof(TextBox))]
    public class TextBox : System.Windows.Forms.TextBox {

        private string _cueBannerText = string.Empty;

        /// <summary>
        /// Gets or sets the cue text that is displayed on the TextBox control.
        /// </summary>
        [
        Description("Text that is displayed as Cue banner."),
        Category("Appearance"),
        DefaultValue("")
        ]
        public string CueBannerText {
            get {
                return _cueBannerText;
            }
            set {
                _cueBannerText = value;
                UpdateControl();
            }
        }

        /*
        [Browsable(false)]
        public new bool Multiline {
            get { return base.Multiline; }
            set { base.Multiline = false; }
        }
        */
        //TODO: check this

        private bool _showCueFocused = false;

        /// <summary>
        /// Gets or sets whether the Cue text should be displyed even
        /// when the control has keybord focus.
        /// </summary>
        /// <remarks>
        /// If true, the Cue text will disappear as soon as the user starts typing.
        /// </remarks>
        [
        Description("If true, the Cue text will be displayed even when the control has keyboard focus."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool ShowCueFocused {
            get {
                return _showCueFocused;
            }
            set {
                _showCueFocused = value;
                UpdateControl();
            }
        }

        private void UpdateControl() {
            if (IsHandleCreated) {
                Methods.SendMessage(Handle,
                    (uint)WindowMessage.EM_SETCUEBANNER,
                    (_showCueFocused) ? 1 : 0,
                    _cueBannerText);
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            UpdateControl();
        }

    }

}
