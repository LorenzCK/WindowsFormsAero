//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace WindowsFormsAero
{
    internal static class GraphicsExtensions
    {
        public static ClippingRegionSwitcher SwitchClip(/*this*/ Graphics gfx, Region clip)
        {
            var cookie = new ClippingRegionSwitcher(gfx, gfx.Clip);
            gfx.SetClip(clip, CombineMode.Replace);

            return cookie;
        }
    }

    internal struct ClippingRegionSwitcher : IDisposable
    {
        private Graphics _gfx;
        private Region _oldClip;

        public ClippingRegionSwitcher(Graphics gfx, Region oldClip)
        {
            _gfx = gfx;
            _oldClip = oldClip;
        }

        public void Dispose()
        {
            if (_gfx != null)
            {
                _gfx.Clip = _oldClip;
            }

            _oldClip = null;
            _gfx = null;
        }
    }
}