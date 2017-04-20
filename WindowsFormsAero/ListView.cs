/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Marco Minerva <marco.minerva@gmail.com>
 *         Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Drawing;
using WindowsFormsAero.Native;

namespace WindowsFormsAero {

    [ToolboxBitmap(typeof(ListView))]
    public class ListView : System.Windows.Forms.ListView {

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            //Set Explorer style
            Methods.SetWindowTheme(Handle, "explorer", null);

            //Blue selection, keeps other extended styles
            Methods.SendMessage(Handle,
                (uint)WindowMessage.LVM_SETEXTENDEDLISTVIEWSTYLE,
                (uint)ListViewExtendedStyle.LVS_EX_DOUBLEBUFFER,
                (uint)ListViewExtendedStyle.LVS_EX_DOUBLEBUFFER);
        }

    }

}
