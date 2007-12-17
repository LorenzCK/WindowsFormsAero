using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace VistaControls.Native {
	internal static class Messaging {

		[DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
		public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

		[return: MarshalAs(UnmanagedType.Bool)]
		[DllImport("user32.dll", SetLastError = false)]
		public static extern bool PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

	}
}
