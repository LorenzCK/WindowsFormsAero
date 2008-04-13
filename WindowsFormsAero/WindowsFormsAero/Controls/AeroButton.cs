using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    [ToolboxBitmap(typeof(Button))]
    [System.ComponentModel.DesignerCategory("Code")]
    public class AeroButton : Button
    {
        private Boolean _requiresElevation;

        public AeroButton()
        {
            FlatStyle = FlatStyle.System;
        }

        [Browsable(false)]
        [DefaultValue(FlatStyle.System)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = value; }
        }

        [Browsable(false)]
        [EditorBrowsable(EditorBrowsableState.Never)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public new FlatButtonAppearance FlatAppearance
        {
            get { return base.FlatAppearance; }
        }

        [Browsable(true)]
        [DefaultValue(false)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool RequiresElevation
        {
            get { return _requiresElevation; }
            set 
            {
                if (_requiresElevation != value)
                {
                    _requiresElevation = value;
                    SetShield(value);
                }
            }
        }

        private void SetShield(bool value)
        {
            ControlExtensions.SendMessage(this, WindowMessages.BCM_SETSHIELD, IntPtr.Zero, new IntPtr(value ? 1 : 0));
        }
    }
}
