/*
* VISTA CONTROLS FOR .NET 2.0
* TEXTBOX WITH CUE BANNER
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
    public class CueBannerTextBox : TextBox
    {
        #region Platform Invoke
        
        private const uint ECM_FIRST = 0x1500;
        private const uint EM_SETCUEBANNER = ECM_FIRST + 1;

        [DllImport("user32", CharSet = CharSet.Unicode)]
        private static extern bool SendMessage(IntPtr hWnd, UInt32 message, IntPtr wParam, string lParam);

        #endregion

        public CueBannerTextBox()
            : base()
        {
            this.ShowCueBanner = true;
        }

        private string _cueBannerText = "Cue Banner Text";
        public string CueBannerText
        {
            get { return _cueBannerText; }
            set { _cueBannerText = value; }
        }

        private bool _showCueBanner;
        [DefaultValue(true)]
        public bool ShowCueBanner
        {
            get { return _showCueBanner; }
            set 
            { 
                _showCueBanner = value;
                SendMessage(this.Handle, EM_SETCUEBANNER, IntPtr.Zero, _showCueBanner ? _cueBannerText : string.Empty);
            }
        }
	
    }
}