/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System.Drawing;
using System.Windows.Forms;

namespace WindowsFormsAero {

    internal static class GeometryExtensions {

        /// <summary>
        /// Returns true if any side of the padding is a positive, non-zero value.
        /// </summary>
        public static bool IsPositive(this Padding p) {
            return (p.All > 0 || p.Top > 0 || p.Bottom > 0 || p.Left > 0 || p.Right > 0);
        }

        /// <summary>
        /// Returns true if all sides are negative.
        /// </summary>
        public static bool AllNegative(this Padding p) {
            return (p.Top < 0 && p.Bottom < 0 && p.Left < 0 && p.Right < 0);
        }

        /// <summary>
        /// Returns whether a point in client coordinates is outside the padded region.
        /// </summary>
        /// <param name="point">Point in client coordinates.</param>
        /// <param name="size">Full size of the region on which padding is applied.</param>
        public static bool IsOutside(this Padding p, Point point, Size size) {
            return (point.X < p.Left ||
                    point.X > (size.Width - p.Right) ||
                    point.Y < p.Top ||
                    point.Y > (size.Height - p.Bottom));
        }

    }

}
