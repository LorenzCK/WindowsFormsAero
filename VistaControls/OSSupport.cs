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

namespace VistaControls {
	
	/// <summary>
	/// 
	/// </summary>
	public static class OSSupport {

		const int VistaMajorVersion = 6;

		public static bool IsVistaOrBetter {
			get {
				return (Environment.OSVersion.Platform == PlatformID.Win32NT &&
					Environment.OSVersion.Version.Major >= VistaMajorVersion);
			}
		}

		public static bool IsCompositionEnabled {
			get {
				try {
					bool enabled;
					DwmIsCompositionEnabled(out enabled);

					return enabled;
				}
				catch (Exception) {
					return false;
				}
			}
		}

		[DllImport("dwmapi.dll")]
		private static extern int DwmIsCompositionEnabled([MarshalAs(UnmanagedType.Bool)] out bool pfEnabled);
	}

}
