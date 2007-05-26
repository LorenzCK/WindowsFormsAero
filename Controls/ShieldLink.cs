/*
* VISTA CONTROLS FOR .NET 2.0
* SHIELD LINK
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
* A copy of this license is available at
* http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedcommunitylicense.mspx
*/

using System;
using System.ComponentModel;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Vista
{
    public class ShieldLink : Button
    {
        #region Platform Invoke

        private const int BS_COMMANDLINK = 0x0000000E;
        private const uint BCM_SETNOTE = 0x00001609;
        private const uint BCM_SETSHIELD = 0x0000160C;

        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        private static extern IntPtr SendMessage(HandleRef hWnd, UInt32 Msg, IntPtr wParam, IntPtr lParam);

        #endregion

        public ShieldLink()
        {
            this.FlatStyle = FlatStyle.System;
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.Style |= BS_COMMANDLINK;
                return cp;
            }
        }

        public new FlatStyle FlatStyle
        {
            get { return base.FlatStyle; }
            set { base.FlatStyle = FlatStyle.System; }
        }

        private string _textNote;
        [DefaultValue("")]
        public string TextNote
        {
            get
            {
                return _textNote;
            }
            set
            {
                _textNote = value;
                SendMessage(new HandleRef(this, this.Handle), BCM_SETNOTE, IntPtr.Zero, _textNote);
            }
        }

        private bool _showShield;
        [DefaultValue(false)]
        public bool ShowShield
        {
            get
            {
                return _showShield;
            }
            set
            {
                _showShield = value;
                SendMessage(new HandleRef(this, this.Handle), BCM_SETSHIELD, IntPtr.Zero, new IntPtr(value ? 1 : 0));
            }
        }
    }
}
