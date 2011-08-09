using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Globalization;

namespace WindowsFormsAero.InteropServices
{
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    internal struct POINT : IEquatable<POINT>, IFormattable
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

        public override bool Equals(object obj)
        {
            if (obj is POINT)
            {
                return Equals((POINT)(obj));
            }

            return false;
        }

        public bool Equals(POINT other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override int GetHashCode()
        {
            return X ^ Y;
        }

        public Point ToPoint()
        {
            return new Point(X, Y);
        }

        public override string ToString()
        {
            return ToString(null, null);
        }

        public string ToString(string format, IFormatProvider formatProvider)
        {
            return string.Format(formatProvider, "{0}, {1}", X, Y);
        }

        public static bool operator ==(POINT a, POINT b)
        {
            return a.Equals(b);
        }

        public static bool operator !=(POINT a, POINT b)
        {
            return !a.Equals(b);
        }
    }
}
