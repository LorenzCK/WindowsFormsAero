/*
* VISTA CONTROLS FOR .NET 2.0
* SHIELD BUTTON
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
* A copy of this license is available at
* http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedcommunitylicense.mspx
*/

using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.ComponentModel;

namespace System.Windows.Forms.Vista
{
    public class ShieldButton : Button
    {
        #region Platform Invoke

        private const uint BCM_SETSHIELD = 0x0000160C;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);
        
        #endregion

        public ShieldButton()
            : base()
        {
            this.FlatStyle = FlatStyle.System;
            this.ShowShield = true;
        }

        private bool _showShield;
        [DefaultValue(true)]
        public bool ShowShield
        {
            get { return _showShield; }
            set
            {
                _showShield = value;
                SendMessage(new HandleRef(this, this.Handle), BCM_SETSHIELD, IntPtr.Zero, new IntPtr(_showShield ? 1 : 0));
            }
        }
    }
}