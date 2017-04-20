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

    [ToolboxBitmap(typeof(ComboBox))]
    public class ComboBox : System.Windows.Forms.ComboBox {

        public ComboBox() {
            FlatStyle = FlatStyle.System;
            DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private string _cueBannerText = string.Empty;

        [
        Description("Gets or sets the cue text that is displayed on a ComboBox control."),
        Category("Appearance"),
        DefaultValue("")
        ]
        public string CueBannerText {
            get {
                return _cueBannerText;
            }
            set {
                if (value == null)
                    throw new ArgumentNullException();

                if(_cueBannerText != value) {
                    Methods.SendMessage(Handle, (uint)WindowMessage.CB_SETCUEBANNER, 0, value);
                }

                _cueBannerText = value;
            }
        }

    }

}
