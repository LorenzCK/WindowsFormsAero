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
    public class CommandLink : Button {

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;

                //Fix for XP provided by jonpreece (http://windowsformsaero.codeplex.com/Thread/View.aspx?ThreadId=81391)
                if (OsSupport.IsVistaOrLater)
                    cp.Style |= (int)(IsDefault ? ButtonStyle.DefaultCommandLink : ButtonStyle.CommandLink);
                else
                    cp.Style |= (int)(IsDefault ? ButtonStyle.PushButton : ButtonStyle.DefaultPushButton);
                return cp;
            }
        }

        private string _note = string.Empty;

        [
        Description("Gets or sets the note that is displayed on a button control."),
        Category("Appearance"),
        DefaultValue("")
        ]
        public string Note {
            get {
                return _note;
            }
            set {
                if (IsHandleCreated) {
                    Methods.SendMessage(Handle, (uint)WindowMessage.BCM_SETNOTE, 0, _note);
                }

                _note = value;
            }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            Methods.SendMessage(Handle, (uint)WindowMessage.BCM_SETNOTE, 0, _note);
        }

    }

}
