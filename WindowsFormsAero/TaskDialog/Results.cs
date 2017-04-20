/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero.TaskDialog {

    /// <summary>
    /// Class that aggregates the results of a Task Dialog when closed.
    /// </summary>
    public class TaskDialogResult {

        internal TaskDialogResult(int buttonId, int radioId, bool selVerification) {
            ButtonID = buttonId;
            RadioID = radioId;
            IsVerificationChecked = selVerification;
        }

        /// <summary>
        /// Gets the ID of the clicked button.
        /// </summary>
        public int ButtonID { get; set; }

        /// <summary>
        /// Gets the ID of the checked radio control.
        /// </summary>
        public int RadioID { get; set; }

        /// <summary>
        /// Gets whether verification was checked.
        /// </summary>
        public bool IsVerificationChecked { get; set; }

        /// <summary>
        /// Gets the clicked common button, if any.
        /// </summary>
        /// <remarks>
        /// Will return <see cref="CommonButtonResult.None"/> if no common button was
        /// clicked or if a custom button with an out of range ID was clicked.
        /// </remarks>
        public CommonButtonResult CommonButton {
            get {
                if (ButtonID > 0 && ButtonID <= 8)
                    return (CommonButtonResult)ButtonID;
                else
                    return CommonButtonResult.None;
            }
        }
    }

}
