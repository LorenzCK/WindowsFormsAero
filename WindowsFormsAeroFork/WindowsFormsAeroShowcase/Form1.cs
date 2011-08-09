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

            Font = SystemFonts.MenuFont;
            ToolStripManager.RendererChanged += new EventHandler(ToolStripManager_RendererChanged);
        }

        void ToolStripManager_RendererChanged(object sender, EventArgs e)
        {
            chkDebugRender.Checked = ToolStripManager.Renderer is TabStripDebugRenderer;
        }

        private void aeroTabControl1_NewTabButtonClick(object sender, EventArgs e)
        {
            aeroTabControl1.TabPages.Add(new AeroTabPage());
        }

        private void aeroTabControl1_CloseButtonClick(object sender, AeroTabPageEventArgs e)
        {
            aeroTabControl1.TabPages.Remove(e.Page);
        }

        private void chkDebugRender_CheckedChanged(object sender, EventArgs e)
        {
            if (chkDebugRender.Checked)
            {
                ToolStripManager.Renderer = new TabStripDebugRenderer();
            }
            else
            {
                ToolStripManager.Renderer = new TabStripAeroRenderer();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var form = new GlassForm())
            {
                form.ShowDialog();
            }
        }

        private void OnHookKeyDown(object sender, KeyEventArgs e)
        {
            hookLog.AppendText("DOWN: " + e.KeyData.ToString() + '\n');
        }

        private void OnHookKeyPress(object sender, KeyPressEventArgs e)
        {
            hookLog.AppendText("PRESS: " + e.KeyChar + '\n');
        }

        private void OnHookKeyUp(object sender, KeyEventArgs e)
        {
            hookLog.AppendText("UP: " + e.KeyData.ToString() + '\n');
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
