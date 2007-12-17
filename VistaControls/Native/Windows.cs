using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace VistaControls.Native {
	internal static class Windows {

		/// <summary>Returns the active windows on the current thread.</summary>
		[DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
		public static extern IntPtr GetActiveWindow();

	}
}
