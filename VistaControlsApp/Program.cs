using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace VistaControlsApp
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

			//Compatibility check
			if (!VistaControls.OSSupport.IsVistaOrBetter)
				if (MessageBox.Show("It appears you are not running on Windows Vista. The controls and dialogs implemented in this application might not work or crash.\n\nDo you want to continue?", "Windows Vista required", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
					return;

            Application.Run(new Main());
        }
    }
}