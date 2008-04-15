using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace WindowsFormsAero.InteropServices
{
    [StructLayout(LayoutKind.Sequential)]
    internal struct Margins
    {
        private int _left;
        private int _right;
        private int _top;
        private int _bottom;

        //public Margins()
        //{
        //}

        public Margins(int left, int top, int right, int bottom)
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
            //set { _left = value; }
        }

        public int Right
        {
            get { return _right; }
            //set { _right = value; }
        }

        public int Top
        {
            get { return _top; }
            //set { _top = value; }
        }

        public int Bottom
        {
            get { return _bottom; }
            //set { _bottom = value; }
        }

        public Padding ToPadding()
        {
            return new Padding(_left, _top, _right, _bottom);
        }

        public static Margins FromPadding(Padding padding)
        {
            return new Margins(padding.Left, padding.Top, padding.Right, padding.Bottom);
        }
    }
}
