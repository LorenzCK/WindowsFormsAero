using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Windows.Forms.VisualStyles;

namespace WindowsFormsAero.InteropServices
{
    [Serializable, Flags]
    internal enum DttOptionsFlags : int
    {
        None = 0x0,
        TextColor = 0x1,
        BorderColor= 0x2,
        ShadowColor = 0x4,
        ShadowType = 0x8,
        ShadowOffset = 0x10,
        BorderSize = 0x20,
        FontProp = 0x40,
        //DTT_COLORPROP = 128,
        //DTT_STATEID = 256,
        //DTT_CALCRECT = 512,
        ApplyOverlay = 0x400,
        GlowSize = 0x800,
        //DTT_CALLBACK = 4096,
        Composited = 0x2000,
    }

    [Serializable]
    public enum ThemeFont : int
    {
        None = 0,
        Caption = 801,
        SmallCaption = 802,
        Menu = 803,
        Status = 804,
        MsgBox = 805,
        IconTitle = 806,
    }


    [Serializable, StructLayout(LayoutKind.Sequential)]
    internal sealed class DTTOPTS
    {
        private readonly int dwSize = Marshal.SizeOf(typeof(DTTOPTS));

        private DttOptionsFlags dwFlags;
        private int crText;
        private int crBorder;
        private int crShadow;
        private TextShadowType iTextShadowType;
        private POINT ptShadowOffset;
        private int iBorderSize;
        private ThemeFont iFontPropId;
        private int iColorPropId;
        private int iStateId;
        private bool fApplyOverlay;
        private int iGlowSize;
        private IntPtr pfnDrawTextCallback;
        private IntPtr lParam;

        public Color TextColor
        {
            get { return ColorTranslator.FromWin32(crText); }
            set
            {
                crText = ColorTranslator.ToWin32(value);
                dwFlags |= DttOptionsFlags.TextColor;
            }
        }

        public Color BorderColor
        {
            get { return ColorTranslator.FromWin32(crBorder); }
            set
            {
                crBorder = ColorTranslator.ToWin32(value);
                dwFlags |= DttOptionsFlags.BorderColor;
            }
        }

        public Int32 BorderSize
        {
            get { return iBorderSize; }
            set
            {
                iBorderSize = value;
                dwFlags |= DttOptionsFlags.BorderSize;
            }
        }

        public Color ShadowColor
        {
            get { return ColorTranslator.FromWin32(crShadow); }
            set
            {
                crShadow = ColorTranslator.ToWin32(value);
                dwFlags |= DttOptionsFlags.ShadowColor;
            }
        }

        public TextShadowType ShadowType
        {
            get { return iTextShadowType; }
            set
            {
                iTextShadowType = value;
                dwFlags |= DttOptionsFlags.ShadowType;
            }
        }

        public Point ShadowOffset
        {
            get { return ptShadowOffset.ToPoint(); }
            set
            {
                ptShadowOffset = new POINT(value);
                dwFlags |= DttOptionsFlags.ShadowOffset;
            }
        }

        public Int32 GlowSize
        {
            get { return iGlowSize; }
            set
            {
                iGlowSize = value;
                dwFlags |= DttOptionsFlags.GlowSize;
            }
        }

        public Boolean ApplyOverlay
        {
            get { return fApplyOverlay; }
            set
            {
                fApplyOverlay = value;
                dwFlags |= DttOptionsFlags.ApplyOverlay;
            }
        }

        public ThemeFont Font
        {
            get { return iFontPropId; }
            set
            {
                iFontPropId = value;
                dwFlags |= DttOptionsFlags.FontProp;
            }
        }
    }
}
