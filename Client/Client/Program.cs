using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Collections.Specialized;

namespace Client
{
    class Program
    {//Входная фукция в котором запускается сервер
        static void Main(string[] args)
        {
            string sAttr;                               
            sAttr = ConfigurationManager.AppSettings.Get("Number");


            {
                Client client = new Client(1000, "127.0.0.1");
                client.Start();

                while (true)
                {
                    string command;
                    command = Console.ReadLine();
                    if (command == "Exit")
                    {
                        client.Stop();
                        break;
                    }
                }
            }
        }
    }
}


