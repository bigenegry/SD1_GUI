using System;
using System.Windows.Forms;

namespace SD1_GUI
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Start the main form ONLY
            Application.Run(new Form1());
        }
    }
}
