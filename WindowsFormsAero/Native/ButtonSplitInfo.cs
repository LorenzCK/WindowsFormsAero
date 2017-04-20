/*****************************************************
 * WindowsFormsAero
 * https://github.com/LorenzCK/WindowsFormsAero
 * http://windowsformsaero.codeplex.com
 *
 * Author: Lorenz Cuno Klopfenstein <lck@klopfenstein.net>
 *****************************************************/

using System;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.Native {

    /// <summary>
    /// Contains information that defines a split button.
    /// </summary>
    /// <remarks>
    /// See: https://msdn.microsoft.com/en-us/library/windows/desktop/bb775955(v=vs.85).aspx
    /// </remarks>
    [StructLayout(LayoutKind.Sequential)]
    internal struct ButtonSplitInfo {

        [Flags]
        public enum MaskType : uint {
            Glyph = 0x0001,
            Image = 0x0002,
            Style = 0x0004,
            Size = 0x0008
        }

        [Flags]
        public enum SplitStyle : uint {
            None = 0,
            NoSplit = 0x0001,
            Stretch = 0x0002,
            AlignLeft = 0x0004,
            Image = 0x0008
        }

        public MaskType Mask;
        public IntPtr GlyphList;
        public SplitStyle Style;
        public Size Size;

    }

}
