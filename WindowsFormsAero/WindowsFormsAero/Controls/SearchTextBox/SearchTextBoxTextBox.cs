using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using WindowsFormsAero.InteropServices;
using System.Windows.Forms.VisualStyles;
using Microsoft.Win32;

namespace WindowsFormsAero
{
    internal sealed class SearchTextBoxTextBox : AeroTextBox
    {
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            // TODO listen for theme changes
        }

        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;

                if (VistaOSFeature.IsRunningAeroTheme)
                {
                    cp.ExStyle &= ~WindowStyles.WS_EX_CLIENTEDGE;
                }

                return cp;
            }
        }

        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);

            if (Parent != null)
            {
                Parent.Invalidate();
            }
        }

        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (Parent != null)
            {
                Parent.Invalidate();
            }
        }
    }
    //internal sealed class SearchTextBoxLayoutEngine : LayoutEngine
    //{
    //    public override bool Layout(object container, LayoutEventArgs layoutEventArgs)
    //    {
    //        var box = container as SearchTextBoxStrip;

    //        if ((box != null) && (box.ControlHost != null) && (box.SearchButton != null))
    //        {
    //            box.ControlHost.AutoSize = false;
    //            box.ControlHost.Width = box.ClientSize.Width - box.SearchButton.Width;

    //            if (box.RightToLeft == RightToLeft.Yes)
    //            {
    //                box.LayoutItem(box.SearchButton, new Point(0, 0));
    //                box.LayoutItem(box.ControlHost, new Point(box.SearchButton.Width, 0));
    //            }
    //            else
    //            {
    //                box.LayoutItem(box.ControlHost, new Point(0, 0));
    //                box.LayoutItem(box.SearchButton, new Point(box.ControlHost.Width, 0));
    //            }

    //            return true;
    //        }
    //        else
    //        {
    //            return base.Layout(container, layoutEventArgs);
    //        }
    //    }
    //}
}