/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System.Runtime.InteropServices;

namespace WindowsFormsAero.TaskDialog {

    /// <summary>
    /// Custom button shown on a Task Dialog.
    /// </summary>
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode, Pack = 1)]
    public struct CustomButton {

        private int _id;

        [MarshalAs(UnmanagedType.LPWStr)]
        private string _txt;

        /// <summary>
        /// Instantiates a new custom button with an ID and a text.
        /// </summary>
        /// <param name="id">Unique ID that will be returned by the Task Dialog if the button is clicked.</param>
        /// <param name="text">Text label shown on the button.</param>
        public CustomButton(int id, string text) {
            _id = id;
            _txt = text;
        }

        /// <summary>
        /// Instantiates a new custom button with an ID and a text.
        /// </summary>
        /// <param name="id">Common ID that will be returned by the Task Dialog if the button is clicked.</param>
        /// <param name="text">Text label shown on the button.</param>
        public CustomButton(CommonButtonResult commonResult, string text) {
            _id = (int)commonResult;
            _txt = text;
        }

        /// <summary>
        /// Unique ID that will be returned by the Task Dialog if the button is clicked.
        /// Use values greater than 8 to prevent conflicts with common button IDs.
        /// </summary>
        public int Id {
            get {
                return _id;
            }
            set {
                _id = value;
            }
        }

        /// <summary>
        /// Text label shown on the button.
        /// </summary>
        /// <remarks>
        /// If you enable Command Links, a newline here separates the upper from the
        /// lower string on the button.
        /// </remarks>
        public string Text {
            get {
                return _txt;
            }
            set {
                _txt = value;
            }
        }

    }

}
