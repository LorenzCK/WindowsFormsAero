using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace VistaControls
{
    [ToolboxBitmap(typeof(ProgressBar))]
    public class ProgressBar:System.Windows.Forms.ProgressBar
    {
        public ProgressBar()
        {
            VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, VistaConstants.PBST_NORMAL, 0);
        }
        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cParams = base.CreateParams;
                //Allows for smooth transition even when progressbar value is subtracted
                cParams.Style |= VistaConstants.PBS_SMOOTHREVERSE;
                return cParams;
            }
        }
        public enum States
        {
            Normal, Error, Paused
        }
        private States ps_ = States.Normal;
        [Description("Gets or sets the ProgressBar state."), Category("Appearance"), DefaultValue(States.Normal)]
        public States ProgressState
        {
            get
            {
                return ps_;
            }
            set
            {
                ps_ = value;
                SetState(ps_);
            }
        }
        public void SetState(States State)
        {
            VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, VistaConstants.PBST_NORMAL, 0);
            //above required for values to be updated properly, but causes a slight flicker
            switch (State)
            {
                case States.Normal:
                    VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, VistaConstants.PBST_NORMAL, 0);
                    break;
                case States.Error:
                    VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, VistaConstants.PBST_ERROR, 0);
                    break;
                case States.Paused:
                    VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, VistaConstants.PBST_PAUSED, 0);
                    break;
                //case States.Partial:
                //The blue progressbar is not available
                //    VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, PBST_PARTIAL, 0);
                //    break;
                default:
                    VistaConstants.SendMessage(this.Handle, VistaConstants.PBM_SETSTATE, VistaConstants.PBST_NORMAL, 0);
                    break;
            }
        }
        protected override void WndProc(ref Message m)
        {
            // Listen for operating system messages.
            switch (m.Msg)
            {
                case 15:
                    //Paint event
                    SetState(ps_); //Paint the progressbar properly
                    break;
            }
            base.WndProc(ref m);
        }

    }
}
