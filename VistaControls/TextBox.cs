using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace VistaControls
{
    [ToolboxBitmap(typeof(TextBox))]
    public class TextBox:System.Windows.Forms.TextBox
    {
        private string cueBannerText_ = string.Empty;
        [Description("Gets or sets the cue text that is displayed on a TextBox control."), Category("Appearance"), DefaultValue("")]
        public string CueBannerText
        {
            get {
                return cueBannerText_;
            }
            set {
                cueBannerText_ = value;
                this.SetCueText();
            }
        }

        private void SetCueText()
        {
            NativeMethods.SendMessage(this.Handle, NativeMethods.EM_SETCUEBANNER, IntPtr.Zero, cueBannerText_);
        }
    }
}
