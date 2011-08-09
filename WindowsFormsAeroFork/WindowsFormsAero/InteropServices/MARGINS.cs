using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsAero.InteropServices
{
    [StructLayout(LayoutKind.Sequential)]
    internal sealed class MARGINS
    {
        private readonly int _left;
        private readonly int _right;
        private readonly int _top;
        private readonly int _bottom;

        public MARGINS(int left, int top, int right, int bottom)
        {
            _left = left;
            _right = right;
            _top = top;
            _bottom = bottom;
        }

        public bool IsEmpty
        {
            get
            {
                return 
                    _left   == 0 &&
                    _right  == 0 &&
                    _top    == 0 &&
                    _bottom == 0;
            }
        }

        public int Left
        {
            get { return _left; }
        }

        public int Right
        {
            get { return _right; }
        }

        public int Top
        {
            get { return _top; }
        }

        public int Bottom
        {
            get { return _bottom; }
        }

        public Padding ToPadding()
        {
            return new Padding(_left, _top, _right, _bottom);
        }

        public static MARGINS FromPadding(Padding padding)
        {
            return new MARGINS(padding.Left, padding.Top, padding.Right, padding.Bottom);
        }
    }
}
