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
        internal static readonly TabStripAeroRenderer Renderer = new TabStripAeroRenderer();

        [STAThread]
        private static void Main()
        {
            Application.SetCompatibleTextRenderingDefault(false);
            ToolStripManager.Renderer = new TabStripAeroRenderer();

            using (var form = new Form1())
            {
                Application.Run(form);
            }
        }
    }
}
