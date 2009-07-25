using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VistaControlsApp
{
    public partial class HorizontalPanelExample : VistaControls.Dwm.Helpers.GlassForm
    {
        public HorizontalPanelExample()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //Initialize glass sheet
            GlassMargins = new VistaControls.Dwm.Margins(0, 0, 30, 0);

            // Place the panel on the form... since I'm not docking this control currently I just manually
            // put the position and the height in.
            panel1.Top = 30;
            panel1.Height = this.Height - 120;
            horizontalPanel1.Top = panel1.Bottom;
           

        }

        private void HorizontalPanelExample_Load(object sender, EventArgs e)
        {
            label4.Text = System.Environment.MachineName.ToString();
            label5.Text = "3.00 GB";
            label6.Text = "Intel(R) Pentium(R) 4 CPU 3.40 Ghz (Quad Core)";
        }

    }
}
