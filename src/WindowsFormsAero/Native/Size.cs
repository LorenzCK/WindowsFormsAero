/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System.Runtime.InteropServices;

namespace WindowsFormsAero.Native {

    /// <summary>
    /// Specifies the width and height of a rectangle.
    /// </summary>
    /// <remarks>
    /// See: https://msdn.microsoft.com/en-us/library/windows/desktop/dd145106(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct Size {

        public Size(int w, int h) {
            Width = w;
            Height = h;
        }

        public int Width;
        public int Height;

    }

}
