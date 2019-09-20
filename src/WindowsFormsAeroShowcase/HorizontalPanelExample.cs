using System;

namespace WindowsFormsAeroShowcase {

    public partial class HorizontalPanelExample : WindowsFormsAero.AeroForm {

        public HorizontalPanelExample() {
            InitializeComponent();
        }

        private void HorizontalPanelExample_Load(object sender, EventArgs e) {
            label4.Text = Environment.MachineName.ToString();
            label5.Text = "3.00 GB";
            label6.Text = "Intel(R) Pentium(R) 4 CPU 3.40 Ghz (Quad Core)";
        }

        private void PanelTop_Paint(object sender, System.Windows.Forms.PaintEventArgs e)
        {

        }
    }
}
