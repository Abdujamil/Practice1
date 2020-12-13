 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Configuration;
using System.Collections.Specialized;

namespace practice_1
{
    class Program
    {
        //Входная фукция для запуска сервера
        static void Main(string[] args)
        {
            //Создается 2 переменные которые ссылаются в конфиг на адреса и порта 
            string Number;
            Number = ConfigurationManager.AppSettings.Get("Number");
            string Number1;
            Number1 = ConfigurationManager.AppSettings.Get("Number1");
            NameValueCollection sAll;
            sAll = ConfigurationManager.AppSettings;
            Server server = new Server(Convert.ToInt32(Number), Number1);
            server.Start();
            //
            //если цикл while "правда" то выполняется действие, под 
            while (true)
            {
                string command;
                command = Console.ReadLine();
                if (command == "Exit")
                {
                    server.Stop();
                    break;
                }

                if (command == "Restart")
                {
                    server.Stop();
                    server.Start();
                }
            }
        }
    }
}
