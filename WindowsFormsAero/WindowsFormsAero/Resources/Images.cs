//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace WindowsFormsAero.Resources
{
    internal static class Images
    {
        public static readonly Image NewTab = ReadIcon("NewTab.ico", 16, 16);
        public static readonly Image TabBusy = ReadBitmap("TabBusy.png");

        private static Bitmap ReadBitmap(string name)
        {
            using (var stream = OpenStream(name))
            {
                return new Bitmap(stream);
            }
        }

        private static Bitmap ReadIcon(string name, int width, int height)
        {
            using (var stream = OpenStream(name))
            {
                using (var icon = new Icon(stream, width, height))
                {
                    return icon.ToBitmap();
                }
            }
        }

        private static Stream OpenStream(string name)
        {
            return Assembly.GetExecutingAssembly().GetManifestResourceStream(typeof(Images).Namespace + '.' + name);
        }
    }
}
