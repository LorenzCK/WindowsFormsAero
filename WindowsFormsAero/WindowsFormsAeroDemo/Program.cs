//--
// Windows Forms Aero Controls
// http://www.CodePlex.com/VistaControls
//
// Copyright (c) 2008 Jachym Kouba
// Licensed under Microsoft Reciprocal License (Ms-RL) 
//--
using System;
using System.Windows.Forms;

namespace WindowsFormsAero.Demo
{
    internal static class Program
    {
        internal static readonly TabStripSystemRenderer Renderer = new TabStripSystemRenderer();

        [STAThread]
        private static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            ToolStripManager.Renderer = new TabStripDebugRenderer();

            using (var form = new MainForm())
            {
                Application.Run(form);
            }
        }
    }
}
