using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;
using System.Security.Permissions;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("code")]
    [System.Drawing.ToolboxBitmap(typeof(ProgressBar))]
    public class AeroProgressBar : ProgressBar
    {
        private AeroProgressBarStatus _status = AeroProgressBarStatus.Normal;

        [Browsable(true)]
        [DefaultValue(AeroProgressBarStatus.Normal)]
        public AeroProgressBarStatus Status
        {
            get { return _status; }
            set
            {
                if (_status != value)
                {
                    _status = value;

                    if (IsHandleCreated)
                    {
                        UpdateStatus();
                    }
                }
            }
        }

        protected override void OnStyleChanged(EventArgs e)
        {
            base.OnStyleChanged(e);
            UpdateStatus();
        }

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
            get
            {
                var cp = base.CreateParams;

                cp.Style |= WindowStyles.PBS_SMOOTHREVERSE;

                return cp;
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            UpdateStatus();
            base.OnHandleCreated(e);
        }

        private void UpdateStatus()
        {
            ControlExtensions.SendMessage(this, WindowMessages.PBM_SETSTATE, (int)(_status));
        }
    }
}
