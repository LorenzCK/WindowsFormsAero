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
        public static IntPtr SendMessage(/*this*/ Control control, UInt32 message, Int32 wParam)
        {
            return SendMessage(control, message, wParam, IntPtr.Zero);
        }

        public static IntPtr SendMessage(/*this*/ Control control, UInt32 message, Int32 wParam, String lParam)
        {
            IntPtr lpsz = Marshal.StringToHGlobalAuto(lParam);

            try
            {
                return SendMessage(control, message, wParam, lpsz);
            }
            finally
            {
                Marshal.FreeHGlobal(lpsz);
            }
        }

        public static IntPtr SendMessage(/*this*/ Control control, UInt32 message, Int32 wParam, IntPtr lParam)
        {
            return SendMessage(control, message, new IntPtr(wParam), lParam);
        }

        public static IntPtr SendMessage(/*this*/ Control control, UInt32 message, IntPtr wParam, IntPtr lParam)
        {
            return NativeMethods.SendMessage(new HandleRef(control, control.Handle), message, wParam, lParam);
        }


    }
}
