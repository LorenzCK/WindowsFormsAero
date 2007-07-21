using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace VistaControlsApp
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void commandLink1_Click(object sender, EventArgs e)
        {
            if (this.progressBar2.Value != this.progressBar2.Maximum)
            {
                this.progressBar1.Value += 10;
                this.progressBar2.Value += 10;
                this.progressBar3.Value += 10;
            }

        }

        private void commandLink2_Click(object sender, EventArgs e)
        {
            if (this.progressBar2.Value != this.progressBar2.Minimum)
            {
                this.progressBar1.Value -= 10;
                this.progressBar2.Value -= 10;
                this.progressBar3.Value -= 10;
            }
        }
    }
}