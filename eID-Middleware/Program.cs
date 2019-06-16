using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using eidReader;
using Newtonsoft.Json;
using WebServerNamespace;
using System.Diagnostics;

namespace eID_Middleware
{
     class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            if (args.Length == 1 && args[0] == "INSTALLER") {
                Process.Start(Application.ExecutablePath);
                return;
            }
            

            var ws = new WebServer(SendResponse, "http://localhost:55515/eid/");
            ws.Run();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Form f = new Form1();
            Application.Run();
            f.Hide();



        }
        public static string SendResponse(HttpListenerRequest request)
        {

            ReadData dataTest = new ReadData("beidpkcs11.dll");
            idInfo id = dataTest.GetInfo();
            if (id.name == "" && id.surname == "")
            {
                return "No card found!";
            }
            return JsonConvert.SerializeObject(id);

        }


    }
}
