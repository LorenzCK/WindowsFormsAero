﻿using System;
using System.Drawing;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero
{
    public static class DesktopComposition
    {
        public static Color ColorizationColor
        {
            get 
            {
                EnsureIsSupported();
                
                int color;
                NativeMethods.DwmGetColorizationColor(out color);

                return Color.FromArgb(color);
            }
        }

        public static bool IsColorizationOpaque
        {
            get
            {
                EnsureIsSupported();

                int color;
                return NativeMethods.DwmGetColorizationColor(out color);
            }
        }

        public static bool IsSupportedByOS
        {
            get { return VistaOSFeature.Feature.IsPresent(VistaOSFeature.DesktopComposition); }
        }

        public static bool IsEnabled
        {
            get { return IsSupportedByOS && NativeMethods.DwmIsCompositionEnabled(); }
            set
            {
                EnsureIsSupported();
                NativeMethods.DwmEnableComposition(value ? (uint)(1) : (uint)(0));
            }
        }

        private static void EnsureIsSupported()
        {
            if (!IsSupportedByOS)
            {
                throw new PlatformNotSupportedException(Resources.Strings.DwmNotSupported);
            }
        }
    }
}
