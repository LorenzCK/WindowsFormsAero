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
    internal struct Point {

        public Point(int x, int y) {
            X = x;
            Y = y;
        }

        public Point(System.Drawing.Point p) {
            X = p.X;
            Y = p.Y;
        }

        public Point(System.Drawing.PointF p) {
            X = (int)p.X;
            Y = (int)p.Y;
        }

        public int X;
        public int Y;

    }

}
