/*
* VISTA CONTROLS FOR .NET 2.0
* FORM EXAMPLE
* 
* Written by Marco Minerva, mailto:marco.minerva@gmail.com
* 
* This code is released under the Microsoft Community License (Ms-CL).
* A copy of this license is available at
* http://www.microsoft.com/resources/sharedsource/licensingbasics/limitedcommunitylicense.mspx
*/

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace VistaControls
{
    public partial class frmExample : Form
    {
        public frmExample()
        {
            InitializeComponent();
        }

        private void chkShowShield_CheckedChanged(object sender, EventArgs e)
        {
            shieldButton.ShowShield = chkShowShield.Checked;
        }

        private void shieldButton_Click(object sender, EventArgs e)
        {
            MessageBox.Show("The Shield is shown: " + shieldButton.ShowShield, "Vista Controls for .NET 2.0", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void chkShowCueBanner_CheckedChanged(object sender, EventArgs e)
        {
            cueBannerTextBox.ShowCueBanner = chkShowCueBanner.Checked;
        }

        private void btnTreeViewEx_Click(object sender, EventArgs e)
        {
            using (frmTreeViewEx frm = new frmTreeViewEx())
                frm.ShowDialog();
        }
    }
}