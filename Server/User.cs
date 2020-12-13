using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace practice_1
{
    class User
    {
        //Сокет подключения клиенту
        private Socket Socket { get; set; }
        //Ссылка на класс сервера
        private Server Server;
        //Ид клиента
        private int Id { get; set; }
        //Количество принятых содинений во время обработки 
        private int Tasks = 0;
        //Конструктор в котором мы задаем сокет клиента его ид и сервер обработки 
        public User(Server Server, Socket Socket, int Id)
        {
            this.Socket = Socket;
            this.Server = Server;
            this.Id = Id;
        }
        //Фукция для потока в которой обрабатываются запросы клиентов 
        public void Processing()
        {
            try
            {
                while (Server.Work)
                {
                    double number = Reading();

                    if (Tasks == 0)
                    {
                        Tasks = 1;
                        Task task = new Task(() => Working(number));
                        task.Start();
                    }
                    else
                    {
                        Tasks++;
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(DateTime.Now + " User: " + Id + " Error: " + e.Message);
            }

            Exit();
        }
        //Фукция кторой читает запрос клиента 
        private double Reading()
        {
            byte[] receiveBuf = new byte[8];
            Socket.Receive(receiveBuf, receiveBuf.Length, SocketFlags.None);
            Console.WriteLine(DateTime.Now + " User: " + Id + " Request received." + BitConverter.ToDouble(receiveBuf, 0));
            return Math.Round(BitConverter.ToDouble(receiveBuf, 0));
        }
        //Фукция обработки запроса клиентов 
        private void Working(double number)
        {
            Thread.Sleep(70);

            Answer(BitConverter.GetBytes((int)number));
        }
        //Фукция для оправки ответа клиенту 
        private void Answer(byte[] answer)
        {
            for (int i = 0; i < Tasks; i++)
            {
                Socket.Send(answer);
                Console.WriteLine(DateTime.Now + " User " + Id + "  A response to the request was sent.");
            }
            Tasks = 0;
        }
        //Фукция которое закрывает соединение с клиентом 
        private void Exit()
        {
            Socket.Close();
        }

    }
}