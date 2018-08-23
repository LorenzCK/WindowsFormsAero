using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsAero.Dwm {

    /// <summary>
    /// Describes whether and why a window is cloaked by the DWM.
    /// </summary>
    [Flags]
    public enum CloakedStatus : int {
        /// <summary>
        /// Window is not cloaked.
        /// </summary>
        Uncloaked = 0,
        /// <summary>
        /// The window was cloaked by its owner application.
        /// </summary>
        ApplicationLevel = 0x0000001,
        /// <summary>
        /// The window was cloaked by the Shell.
        /// </summary>
        ShellLevel = 0x0000002,
        /// <summary>
        /// The cloak value was inherited from its owner window.
        /// </summary>
        Inherited = 0x0000004
    }

}
