/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.TaskDialog {

    /// <summary>
    /// Stores a Task Dialog message that will be sent to a dialog in order to update its state.
    /// </summary>
    internal struct Message {
        
        /// <summary>Text values that can be updated.</summary>
        public enum DialogElements : int {
            TDE_CONTENT,
            TDE_EXPANDED_INFORMATION,
            TDE_FOOTER,
            TDE_MAIN_INSTRUCTION
        }

        /// <summary>Simple int, int message.</summary>
        public Message(NativeMethods.TaskDialogMessages msg, int w, int l) {
            UnsafeHandle = IntPtr.Zero;
            MessageType = msg;
            WParam = w;
            LParam = l;
            ContainsTaskDialogConfig = false;
        }

        /// <summary>Simple int, bool message.</summary>
        public Message(NativeMethods.TaskDialogMessages msg, int w, bool l) {
            UnsafeHandle = IntPtr.Zero;
            MessageType = msg;
            WParam = w;
            LParam = (l) ? 1 : 0;
            ContainsTaskDialogConfig = false;
        }

        /// <summary>Simple bool, bool message.</summary>
        public Message(NativeMethods.TaskDialogMessages msg, bool w, bool l) {
            UnsafeHandle = IntPtr.Zero;
            MessageType = msg;
            WParam = (w) ? 1 : 0;
            LParam = (l) ? 1 : 0;
            ContainsTaskDialogConfig = false;
        }

        /// <summary>Simple bool, int message.</summary>
        public Message(NativeMethods.TaskDialogMessages msg, bool w, int l) {
            UnsafeHandle = IntPtr.Zero;
            MessageType = msg;
            WParam = (w) ? 1 : 0;
            LParam = l;
            ContainsTaskDialogConfig = false;
        }

        /// <summary>Simple int, long (hi word and lo word) message.</summary>
        public Message(NativeMethods.TaskDialogMessages msg, int w, int l_hi, int l_lo) {
            UnsafeHandle = IntPtr.Zero;
            MessageType = msg;
            WParam = w;
            LParam = (l_lo << 16) + l_hi;
            ContainsTaskDialogConfig = false;
        }

        /// <summary>Text updating message.</summary>
        /// <remarks>The string will be marshaled: the Message must be correctly disposed after use.</remarks>
        public Message(NativeMethods.TaskDialogMessages msg, DialogElements element, string s) {
            UnsafeHandle = Marshal.StringToHGlobalUni(s);
            MessageType = msg;
            WParam = (int)element;
            LParam = (int)UnsafeHandle;
            ContainsTaskDialogConfig = false;
        }

        /// <summary>Navigation message.</summary>
        /// <remarks>The config structure will be marshaled: must be correctly disposed after use.</remarks>
        public Message(NativeMethods.TaskDialogMessages msg, int w, NativeMethods.TaskDialogConfig config) {
            UnsafeHandle = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.TaskDialogConfig)));
            Marshal.StructureToPtr(config, UnsafeHandle, false);
            MessageType = msg;
            WParam = w;
            LParam = UnsafeHandle.ToInt32();
            ContainsTaskDialogConfig = true;
        }

        private IntPtr UnsafeHandle;

        public NativeMethods.TaskDialogMessages MessageType;

        public int WParam;

        public int LParam;

        private bool ContainsTaskDialogConfig;

        /// <summary>
        /// Cleans up unmanages memory allocated by the message.
        /// </summary>
        /// <remarks>
        /// This pattern is used instead of <see cref="IDisposable"/> to avoid
        /// unnecessary memory allocations and boxing.
        /// </remarks>
        public static void Cleanup(Message m) {
            if (m.UnsafeHandle != IntPtr.Zero) {
                if (m.ContainsTaskDialogConfig) {
                    Marshal.DestroyStructure(m.UnsafeHandle, typeof(NativeMethods.TaskDialogConfig));
                }
                Marshal.FreeHGlobal(m.UnsafeHandle);

                m.UnsafeHandle = IntPtr.Zero;
            }
        }

    }
}
