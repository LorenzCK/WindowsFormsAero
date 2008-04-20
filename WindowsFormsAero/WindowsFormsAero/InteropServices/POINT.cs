using System;
using System.Drawing;
using System.Runtime.InteropServices;

namespace WindowsFormsAero.InteropServices
{
    [StructLayout(LayoutKind.Sequential)]
    public struct POINT
    {
        public readonly int X;
        public readonly int Y;

        public POINT(int x, int y)
        {
            X = x;
            Y = y;
        }

        public POINT(Point p)
        {
            X = p.X;
            Y = p.Y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }
    }
}
