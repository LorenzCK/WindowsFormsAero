using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero.Internal
{
    public abstract class AeroButtonBase : Control
    {
        protected override CreateParams CreateParams
        {
            get
            {
                var cp = base.CreateParams;


                return cp;
            }
        }
    }
}
