using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using WindowsFormsAero.InteropServices;
using System.Runtime.InteropServices;

namespace WindowsFormsAero
{
    internal static class ControlExtensions
    {
        public static IntPtr SendMessage(/*this*/ Control control, UInt32 message, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.SendMessage(new HandleRef(control, control.Handle), message, wParam, lParam);
        }
    }
}
