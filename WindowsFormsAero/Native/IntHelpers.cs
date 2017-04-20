/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

namespace WindowsFormsAero.Native {

    internal static class IntHelpers {

        public static ushort LowWord(uint val) {
            return (ushort)val;
        }

        public static ushort HighWord(uint val) {
            return (ushort)(val >> 16);
        }

    }
}
