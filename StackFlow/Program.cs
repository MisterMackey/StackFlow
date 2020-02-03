using StackFlow.Controllers;
using System;
using System.Windows.Forms;

namespace StackFlow
{
    static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1(new AggregateController()));
            Application.Run(new SupportingForms.TestForm());
        }
    }
}
