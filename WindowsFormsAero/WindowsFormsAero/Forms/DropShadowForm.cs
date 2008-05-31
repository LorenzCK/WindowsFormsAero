using System;
using System.Security.Permissions;
using System.Windows.Forms;

namespace WindowsFormsAero
{
    public class DropShadowForm : AeroForm
    {
        private const int CS_DROPSHADOW = 0x00020000;

        protected override CreateParams CreateParams
        {
            [SecurityPermission(SecurityAction.LinkDemand, UnmanagedCode = true)]
            get
            {
                var parameters = base.CreateParams;

                if (VistaOSFeature.Feature.IsPresent(VistaOSFeature.DropShadow))
                {
                    parameters.ClassStyle |= CS_DROPSHADOW;
                }

                return parameters;
            }
        }
    }
}
