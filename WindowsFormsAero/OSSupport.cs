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

namespace WindowsFormsAero {
	
	/// <summary>
	/// Static class providing information about the current support for Vista-only features.
	/// </summary>
	public static class OsSupport {

		const int VistaMajorVersion = 6;
        const int SevenMinorVersion = 1;
        const int EightMinorVersion = 2;

		/// <summary>
        /// Gets whether the running operating system is Windows Vista or a more recent version.
        /// </summary>
		public static bool IsVistaOrBetter {
			get {
				return (Environment.OSVersion.Platform == PlatformID.Win32NT &&
					Environment.OSVersion.Version.Major >= VistaMajorVersion);
			}
		}

        /// <summary>
        /// Gets whether the running operating system is Windows Seven or a more recent version.
        /// </summary>
        public static bool IsSevenOrBetter {
            get {
                if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                    return false;

                var version = Environment.OSVersion.Version;

                if (version.Major < VistaMajorVersion)
                    return false;
                else if (version.Major == VistaMajorVersion)
                    return (version.Minor >= SevenMinorVersion);
                else
                    return true;
            }
        }

        /// <summary>
        /// Gets whether the running operating system is Windows 8 or a more recent version.
        /// </summary>
        public static bool IsEightOrBetter {
            get {
                if (Environment.OSVersion.Platform != PlatformID.Win32NT)
                    return false;

                var version = Environment.OSVersion.Version;

                if (version.Major < VistaMajorVersion)
                    return false;
                else if (version.Major == VistaMajorVersion)
                    return (version.Minor >= EightMinorVersion);
                else
                    return true;
            }
        }

		/// <summary>Is true if the DWM composition engine is currently enabled.</summary>
		public static bool IsCompositionEnabled {
			get {
				try {
					bool enabled;
					Dwm.NativeMethods.DwmIsCompositionEnabled(out enabled);

					return enabled;
				}
				catch (Exception) {
					return false;
				}
			}
		}

	}

}
