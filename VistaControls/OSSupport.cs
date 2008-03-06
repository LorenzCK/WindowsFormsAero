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
	/// Static class providing information about the current support for Vista-only features.
	/// </summary>
	public static class OSSupport {

		const int VistaMajorVersion = 6;

		/// <summary>Is true if the running operating system is Windows Vista or a more recent version.</summary>
		public static bool IsVistaOrBetter {
			get {
				return (Environment.OSVersion.Platform == PlatformID.Win32NT &&
					Environment.OSVersion.Version.Major >= VistaMajorVersion);
			}
		}

		/// <summary>Is true if the DWM composition engine is currently enabled.</summary>
		public static bool IsCompositionEnabled {
			get {
				try {
					bool enabled;
					DWM.NativeMethods.DwmIsCompositionEnabled(out enabled);

					return enabled;
				}
				catch (Exception) {
					return false;
				}
			}
		}

	}

}
