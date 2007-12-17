using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace VistaControls.Native {

	[StructLayout(LayoutKind.Sequential)]
	public struct POINT {
		public POINT(int x, int y) {
			X = x;
			Y = y;
		}
		public POINT(System.Drawing.Point p) {
			X = p.X;
			Y = p.Y;
		}
		public POINT(System.Drawing.PointF p) {
			X = (int)p.X;
			Y = (int)p.Y;
		}

		public int X;
		public int Y;
	}

}
