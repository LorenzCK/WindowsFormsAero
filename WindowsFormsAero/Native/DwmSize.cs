/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System.Runtime.InteropServices;

namespace WindowsFormsAero.Native {

    [StructLayout(LayoutKind.Sequential)]
    internal struct DwmSize {

        public int Width;
        public int Height;

        public Size ToNativeSize() {
            return new Size(Width, Height);
        }

        public System.Drawing.Size ToSize() {
            return new System.Drawing.Size(Width, Height);
        }

    }

}
