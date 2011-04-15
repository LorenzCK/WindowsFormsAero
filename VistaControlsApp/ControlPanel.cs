using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VistaControlsApp
{
    public partial class ControlPanel : WindowsFormsAero.Dwm.Helpers.GlassForm
    {
        public ControlPanel()
        {
            InitializeComponent();
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);

            //Initialize glass sheet
            GlassMargins = new WindowsFormsAero.Dwm.Margins(0, 0, 30, 28);

            // Place the panel on the form... since I'm not docking this control currently I just manually
            // put the position and the height in.
            aeroVerticalPanel1.Top = 30;
            aeroVerticalPanel1.Left = 0;
            aeroVerticalPanel1.Height = 365;

            // Now, move the normal panel with the white background to fill the rest.
            panel1.Top = aeroVerticalPanel1.Top;
            panel1.Left = aeroVerticalPanel1.Right;
            panel1.Height = aeroVerticalPanel1.Height;

        }

        private void ControlPanel_Load(object sender, EventArgs e)
        {

        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Close();
        }
    }
}
