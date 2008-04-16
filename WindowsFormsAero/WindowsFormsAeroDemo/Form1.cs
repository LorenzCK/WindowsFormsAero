using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace WindowsFormsAero.Demo
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void aeroTabControl1_NewTabButtonClick(object sender, EventArgs e)
        {
            aeroTabControl1.TabPages.Add(new AeroTabPage());
        }

        private void aeroTabControl1_CloseButtonClick(object sender, AeroTabPageEventArgs e)
        {
            aeroTabControl1.TabPages.Remove(e.Page);
        }

        //protected override void OnLoad(EventArgs e)
        //{
        //    base.OnLoad(e);

        //    button1.Enabled = DesktopComposition.IsSupportedByOS;

        //    if (DesktopComposition.IsSupportedByOS)
        //    {
        //        label1.BackColor = DesktopComposition.ColorizationColor;
        //        label1.Text = DesktopComposition.IsColorizationOpaque ? "Opaque" : "Transparent";
        //    }
        //}

        //private void button1_Click(object sender, EventArgs e)
        //{
        //    DesktopComposition.IsEnabled = !DesktopComposition.IsEnabled;
        //}
    }
}
