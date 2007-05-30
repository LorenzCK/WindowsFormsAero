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
        [DllImport("uxtheme", CharSet = CharSet.Unicode)]
        public extern static Int32 SetWindowTheme(IntPtr hWnd, String textSubAppName, String textSubIdList);

        private void button1_Click(object sender, EventArgs e)
        {
            //System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form2));

            //this.button1.Image = ((System.Drawing.Bitmap)(resources.GetObject("button1.ImageItem")));
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.progressBar2.ProgressState = VistaControls.ProgressBar.States.Error;
            this.progressBar3.ProgressState = VistaControls.ProgressBar.States.Paused;
            this.comboBox1.SelectedIndex = 0;
            SetWindowTheme(this.listView3.Handle, "explorer", null); //Explorer style

            VistaControls.VistaConstants.SendMessage(this.listView3.Handle, VistaControls.VistaConstants.LVM_SETEXTENDEDLISTVIEWSTYLE, VistaControls.VistaConstants.LVS_EX_DOUBLEBUFFER, VistaControls.VistaConstants.LVS_EX_DOUBLEBUFFER);//Affect only one extended style// | VistaControls.VistaConstants.LVS_EX_FULLROWSELECT); //Blue selection, fixes other extended styles being overrided
            
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