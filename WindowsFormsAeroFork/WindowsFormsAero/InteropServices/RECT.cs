using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    internal struct RECT
    {
        public readonly int Left;
        public readonly int Top;
        public readonly int Right;
        public readonly int Bottom;

        public RECT(int left, int  top, int right, int bottom)
        {
            Left = left;
            Top = top;
            Right = right;
            Bottom = bottom;
        }

        public static RECT FromRectangle(Rectangle rect)
        {
            return new RECT(rect.Left, rect.Top, rect.Right, rect.Bottom);
        }
    }
}
