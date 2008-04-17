using System;
using System.ComponentModel;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    [System.ComponentModel.DesignerCategory("code")]
    [System.Drawing.ToolboxBitmap(typeof(TextBox))]
    public class AeroTextBox : TextBox
    {
        private string _cueText;
        private bool _cueFocused;

        [Browsable(true)]
        [DefaultValue(null)]
        public string CueBannerText
        {
            get { return _cueText; }
            set
            {
                if (_cueText != value)
                {
                    _cueText = value;
                    UpdateCue();
                }
            }
        }

        [Browsable(true)]
        [DefaultValue(false)]
        public bool CueBannerFocused
        {
            get { return _cueFocused; }
            set
            {
                if (_cueFocused != value)
                {
                    _cueFocused = value;
                    UpdateCue();
                }
            }
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            UpdateCue();
        }

        private void UpdateCue()
        {
            if (IsHandleCreated)
            {
                ControlExtensions.SendMessage(this, WindowMessages.EM_SETCUEBANNER, _cueFocused ? 1 : 0, _cueText);
            }
        }
    }
}
