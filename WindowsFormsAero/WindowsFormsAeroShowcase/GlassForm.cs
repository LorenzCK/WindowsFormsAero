using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero.Demo
{
    public partial class GlassForm : Form
    {
        public GlassForm()
        {
            InitializeComponent();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (DesktopComposition.IsEnabled)
            {
                
            }
        }
    }
}
