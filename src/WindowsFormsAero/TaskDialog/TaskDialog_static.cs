/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;

namespace WindowsFormsAero.TaskDialog {

    // Static convenience methods
    public partial class TaskDialog {

        #region Static display methods

        /// <summary>Displays a task dialog that has a message.</summary>
        /// <param name="text">The text to display.</param>
        public static CommonButtonResult Show(string instruction) {
            return InternalShow(IntPtr.Zero, string.Empty, instruction, null, CommonButton.OK, CommonIcon.None);
        }

        /// <summary>Displays a task dialog that has a message and a title.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        public static CommonButtonResult Show(string instruction, string title) {
            return InternalShow(IntPtr.Zero, title, instruction, null, CommonButton.OK, CommonIcon.None);
        }

        /// <summary>Displays a task dialog that has a message, a title, and an instruction.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        /// <param name="instruction">The instruction shown below the main text.</param>
        public static CommonButtonResult Show(string instruction, string title, string content) {
            return InternalShow(IntPtr.Zero, title, instruction, content, CommonButton.OK, CommonIcon.None);
        }

        /// <summary>Displays a task dialog that has a message, a title, an instruction, and one or more buttons.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        /// <param name="instruction">The instruction shown below the main text.</param>
        /// <param name="buttons">Value that specifies which button or buttons to display.</param>
        public static CommonButtonResult Show(string instruction, string title, string content, CommonButton buttons) {
            return InternalShow(IntPtr.Zero, title, instruction, content, buttons, CommonIcon.None);
        }

        /// <summary>Displays a task dialog that has a message, a title, an instruction, one or more buttons, and an icon.</summary>
        /// <param name="text">The text to display.</param>
        /// <param name="title">The title bar caption of the dialog.</param>
        /// <param name="instruction">The instruction shown below the main text.</param>
        /// <param name="buttons">Value that specifies which button or buttons to display.</param>
        /// <param name="icon">The icon to display.</param>
        public static CommonButtonResult Show(string instruction, string title, string content, CommonButton buttons, CommonIcon icon) {
            return InternalShow(IntPtr.Zero, title, instruction, content, buttons, icon);
        }

        private static CommonButtonResult InternalShow(IntPtr parent, string title, string instruction, string content, CommonButton commonButtons, CommonIcon icon) {
            int dlgValue;

            try {
                //Get handle for parent window if none specified (behave like MessageBox)
                if (parent == IntPtr.Zero)
                    parent = Native.Methods.GetActiveWindow();

                if (NativeMethods.TaskDialog(parent, IntPtr.Zero, title, instruction, content, (int)commonButtons, new IntPtr((long)icon), out dlgValue) != 0)
                    throw new Exception(string.Format(Resources.ExceptionMessages.NativeCallFailure, "TaskDialog"));
            }
            catch (EntryPointNotFoundException ex) {
                throw new Exception(Resources.ExceptionMessages.CommonControlEntryPointNotFound, ex);
            }
            catch (Exception ex) {
                throw new Exception(Resources.ExceptionMessages.TaskDialogFailure, ex);
            }

            //Convert int value to common dialog result
            if (dlgValue > 0 && dlgValue <= 8)
                return (CommonButtonResult)dlgValue;
            else
                return CommonButtonResult.None;
        }

        #endregion

    }

}
