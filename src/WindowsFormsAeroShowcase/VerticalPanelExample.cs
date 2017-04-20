using System;
using System.Windows.Forms;

namespace WindowsFormsAeroShowcase {

    public partial class ControlPanel : WindowsFormsAero.AeroForm {

        public ControlPanel() {
            InitializeComponent();
        }

        private void linkLabel3_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            this.Close();
        }

    }

}
