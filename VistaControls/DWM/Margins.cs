/*****************************************************
 *            Vista Controls for .NET 2.0
 * 
 * http://www.codeplex.com/vistacontrols
 * 
 * @author: Lorenz Cuno Klopfenstein
 * Licensed under Microsoft Community License (Ms-CL)
 * 
 *****************************************************/

using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace VistaControls.DWM {

    /// <summary>Margins structure for the Glass Sheet effect.</summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct Margins {
        public Margins(int left, int right, int top, int bottom) {
            Left = left;
            Right = right;
            Top = top;
            Bottom = bottom;
        }

        public Margins(int allMargins) {
            Left = allMargins;
            Right = allMargins;
            Top = allMargins;
            Bottom = allMargins;
        }

        public int Left;
        public int Right;
        public int Top;
        public int Bottom;

		public bool IsMarginless {
			get {
				return (
					Left < 0 && Right < 0 &&
					Top < 0 && Bottom < 0
				);
			}
		}
    }

}
