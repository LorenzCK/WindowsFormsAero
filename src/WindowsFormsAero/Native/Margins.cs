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
    internal struct Margins {

        public Margins(int left, int right, int top, int bottom) {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public Margins(int all) {
            Left = all;
            Right = all;
            Top = all;
            Bottom = all;
        }

        public int Left;
        public int Right;
        public int Top;
        public int Bottom;

        /// <summary>
        /// Gets a static readonly 0-pixel margin.
        /// </summary>
        public static readonly Margins Zero = new Margins(0);

        /// <summary>
        /// Converts margins to a <see cref="Padding"/> instance.
        /// </summary>
        public System.Windows.Forms.Padding ToPadding() {
            return new System.Windows.Forms.Padding(Left, Top, Right, Bottom);
        }

        /// <summary>
        /// Creates margins from a <see cref="Padding"/> instance.
        /// </summary>
        public static Margins FromPadding(System.Windows.Forms.Padding padding) {
            return new Margins(padding.Left, padding.Right, padding.Top, padding.Bottom);
        }

        public override string ToString() {
            return string.Format("{{{0},{1},{2},{3}}}", Left, Right, Top, Bottom);
        }

    }

}
