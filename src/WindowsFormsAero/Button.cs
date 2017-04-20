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

    [ToolboxBitmap(typeof(Button))]
    public class Button : System.Windows.Forms.Button {

        private bool _showShield = false;

        [
        Description("Gets or sets whether if the control should use an elevated shield icon."),
        Category("Appearance"),
        DefaultValue(false)
        ]
        public bool ShowShield {
            get {
                return _showShield;
            }
            set {
                if(_showShield != value) {
                    if(value) {
                        //Shields are visible only with FlatStyle.System
                        //which hides the button's image however
                        FlatStyle = FlatStyle.System;
                    }
                    else if(Image != null) {
                        //If no shield but image displayed, switch to FlatStyle.System
                        //which is the only style showing the image
                        FlatStyle = FlatStyle.Standard;
                    }

                    if (IsHandleCreated) {
                        Methods.SendMessage(Handle,
                            (uint)WindowMessage.BCM_SETSHIELD,
                            0,
                            (value) ? 1 : 0
                        );
                    }
                }

                _showShield = value;
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            Methods.SendMessage(Handle,
                (uint)WindowMessage.BCM_SETSHIELD,
                0,
                (_showShield) ? 1 : 0
            );
        }

    }

}
