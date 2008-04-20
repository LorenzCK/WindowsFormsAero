using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using WindowsFormsAero.InteropServices;
using System.Windows.Forms.VisualStyles;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    public static class ThemeTextRenderer
    {
        public static void DrawText(IDeviceContext dc, String text, Rectangle bounds, TextFormatFlags format)
        {
            try
            {
                var options = new DTTOPTS();
                NativeMethods.DrawThemeTextEx(Renderer.Handle, dc.GetHdc(), 0, 0, text, text.Length, format, RECT.FromRectangle(bounds), options);
            }
            finally
            {
                dc.ReleaseHdc();
            }
        }

        [ThreadStatic]
        private static readonly VisualStyleRenderer Renderer = 
            new VisualStyleRenderer(VisualStyleElement.Window.Caption.Active);
    }
}
