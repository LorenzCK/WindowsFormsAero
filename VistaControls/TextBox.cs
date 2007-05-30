using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace VistaControls
{
    public class TextBox:System.Windows.Forms.TextBox
    {
        public TextBox()
        {

        }
        private string cueBannerText_ = "";
        [Description("Gets or sets the cue text that is displayed on a TextBox control."), Category("Appearance"), DefaultValue("")]
        public string CueBannerText
        {
            get {
                return cueBannerText_;
            }
            set {
                cueBannerText_ = value;
                SetCueText();
            }
        }
        public void SetCueText()
        {
            VistaConstants.SendMessage(this.Handle, VistaConstants.EM_SETCUEBANNER, IntPtr.Zero, cueBannerText_);
        }
    }
}
