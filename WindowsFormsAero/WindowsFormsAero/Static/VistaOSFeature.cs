using System;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsAero
{
    public class VistaOSFeature : OSFeature
    {
        public static readonly object DesktopComposition = new object();

        public static new VistaOSFeature Feature
        {
            get { return new VistaOSFeature(); }
        }

        public override Version GetVersionPresent(object feature)
        {
            if (feature == DesktopComposition)
            {
                if (OnVista)
                {
                    using (var dwmapi = NativeModule.TryLoad("dwmapi.dll"))
                    {
                        if (dwmapi != null)
                        {
                            if (dwmapi.ContainsProcedure("DwmIsCompositionEnabled"))
                            {

                                return new Version(0, 0, 0, 0);
                            }
                        }
                    }
                }
            }

            return base.GetVersionPresent(feature);
        }

        internal static bool IsRunningAeroTheme
        {
            get
            {
                return 
                    OnVista &&
                    VisualStyleInformation.IsEnabledByUser &&
                    VisualStyleInformation.DisplayName.IndexOf("Aero", StringComparison.OrdinalIgnoreCase) != -1;
            }
        }

        private static bool OnVista
        {
            get
            {
                if (Environment.OSVersion.Platform == PlatformID.Win32NT)
                {
                    return Environment.OSVersion.Version.CompareTo(new Version(6, 0, 0, 0)) >= 0;
                }

                return false;
            }
        }
    }
}
