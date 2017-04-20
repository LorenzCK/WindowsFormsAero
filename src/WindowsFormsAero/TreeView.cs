/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Marco Minerva <marco.minerva@gmail.com>
 *****************************************************/

using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using WindowsFormsAero.Native;

namespace WindowsFormsAero {

    [ToolboxBitmap(typeof(TreeView))]
    public class TreeView : System.Windows.Forms.TreeView {

        public TreeView() {
            base.HotTracking = true;
            base.ShowLines = false;
        }

        protected override CreateParams CreateParams {
            get {
                CreateParams cp = base.CreateParams;
                cp.Style |= (int)TreeViewStyle.TVS_NOHSCROLL;
                return cp;
            }
        }

        [Browsable(false)]
        private new bool HotTracking {
            get { return base.HotTracking; }
            set { }
        }

        [Browsable(false)]
        private new bool ShowLines {
            get { return base.ShowLines; }
            set { }
        }

        protected override void OnHandleCreated(EventArgs e) {
            base.OnHandleCreated(e);

            Methods.SetWindowTheme(Handle, "explorer", null);

            uint style = (uint)(Methods.SendMessage(Handle,
                (uint)WindowMessage.TVM_GETEXTENDEDSTYLE, 0, 0).ToInt64());
            style |= (uint)TreeViewExtendedStyle.TVS_EX_AUTOHSCROLL;
            style |= (uint)TreeViewExtendedStyle.TVS_EX_FADEINOUTEXPANDOS;
            style |= (uint)TreeViewExtendedStyle.TVS_EX_DOUBLEBUFFER;
            Methods.SendMessage(Handle, (uint)WindowMessage.TVM_SETEXTENDEDSTYLE, 0, style);
        }

    }

}
