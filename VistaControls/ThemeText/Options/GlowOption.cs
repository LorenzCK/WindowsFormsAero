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

namespace VistaControls.ThemeText.Options {
	public class GlowOption : IThemeTextOption {

		public const int cDefaultSize = 10;
		public const int cWord2007Size = 15;
		public const int cPreciseGlow = 2;

		public GlowOption(int size) {
			Size = size;
		}

		public int Size { get; set; }

		#region IThemeTextOption Members

		public void Apply(ref NativeMethods.DTTOPTS options) {
			options.dwFlags |= NativeMethods.DTTOPSFlags.DTT_GLOWSIZE;
			options.iGlowSize = Size;
		}

		#endregion
	}
}
