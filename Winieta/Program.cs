using System.Runtime.InteropServices;

namespace Winieta
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        
        [DllImport(@"C:\Users\Bartek\source\repos\Winieta\x64\Debug\WinietaDLL.dll")]
        static extern int MyProc1(int a, int b);
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}