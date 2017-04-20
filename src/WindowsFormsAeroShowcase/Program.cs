using System;
using System.Windows.Forms;

namespace WindowsFormsAeroShowcase {

    internal static class Program {

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        private static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Compatibility check
            if (!WindowsFormsAero.OsSupport.IsVistaOrBetter)
                if (MessageBox.Show("It appears you are not running on Windows Vista (or better). The controls and dialogs implemented in this application might not work or crash.\n\nDo you want to continue?", "Windows Vista required", MessageBoxButtons.YesNo, MessageBoxIcon.Error) == DialogResult.No)
                    return;

            Application.Run(new Main());
        }

    }

}
