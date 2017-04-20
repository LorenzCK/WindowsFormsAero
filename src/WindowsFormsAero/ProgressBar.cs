/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Marco Minerva <marco.minerva@gmail.com>
 *         Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsAero.Native;

namespace WindowsFormsAero {

    [ToolboxBitmap(typeof(ProgressBar))]
    public class ProgressBar : System.Windows.Forms.ProgressBar {

        protected override CreateParams CreateParams {
            get {
                CreateParams cParams = base.CreateParams;

                // Enables smooth transition even when progressbar value is decreased
                cParams.Style |= (int)Native.ProgressBarStyle.SmoothReverse;

                return cParams;
            }
        }

        private ProgressBarState _state = ProgressBarState.Normal;

        [
        Description("Gets or sets the ProgressBar state."),
        Category("Appearance"),
        DefaultValue(ProgressBarState.Normal)
        ]
        public ProgressBarState State {
            get {
                return _state;
            }
            set {
                if(_state != value) {
                    SetState(value);
                }

                _state = value;
            }
        }

        private void SetState(ProgressBarState targetState) {
            if (!IsHandleCreated)
                return;

            Methods.SendMessage(Handle, (uint)WindowMessage.PBM_SETSTATE,
                (int)targetState.ToNative(), 0);
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            SetState(_state);
        }

        protected override void WndProc(ref Message m) {
            //Intercept PBM_SETPOS messages that update the progressbar's value
            //and switch to normal state if the progress bar is paused or in error
            //(which prevents the progressbar from updating its value correctly).
            if (m.Msg == (int)WindowMessage.PBM_SETPOS && _state != ProgressBarState.Normal) {
                SetState(ProgressBarState.Normal);
            }

            base.WndProc(ref m);

            //Switch back to original state if needed
            if(m.Msg == (int)WindowMessage.PBM_SETPOS && _state != ProgressBarState.Normal) {
                SetState(_state);
            }
        }

    }

}
