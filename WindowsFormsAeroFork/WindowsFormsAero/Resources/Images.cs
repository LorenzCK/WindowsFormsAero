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
using System.Runtime.InteropServices;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;

namespace WindowsFormsAero.Resources
{
    internal static class Images
    {
        public static readonly Image NewTab = ReadIcon("NewTab.ico", 16, 16);
        public static readonly Image TabBusy = ReadBitmap("TabBusy.png");
        //public static readonly Image SearchStart = ReadBitmap("SearchStart.png");
        //public static readonly Image SearchStop = ReadBitmap("SearchStop.png");

        public static readonly Cursor TabDragAero = ReadCursor(typeof(Images).Assembly, 100);
        public static readonly Cursor TabDragClassic = ReadCursor(typeof(Images).Assembly, 101);
        public static readonly Cursor TabDropNA = ReadCursor(typeof(Images).Assembly, 102);

        private static Cursor ReadCursor(Assembly asm, int id)
        {
            var hCur = NativeMethods.LoadImage(
                Marshal.GetHINSTANCE(asm.ManifestModule), 
                new IntPtr(id), 
                ImageType.Cursor, 
                0, 0, 
                LoadImageFlags.DefaultSize | LoadImageFlags.Shared);

            if (hCur == IntPtr.Zero)
            {
                throw Marshal.GetExceptionForHR(Marshal.GetHRForLastWin32Error());
            }

            return new Cursor(hCur);
        }

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
