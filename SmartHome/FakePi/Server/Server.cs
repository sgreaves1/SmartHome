using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakePi.Server
{
    public class Server
    {
        TcpClient _clientSocket = new TcpClient();

        public EventHandler Connected;
        public EventHandler Disconnected;

        public Server()
        {
            Task heartBeatTask = new Task(Heartbeat);
            heartBeatTask.Start();
        }

        async void Heartbeat()
        {
            Task task = HeartbeatAsync();
            await task;
        }

        async Task HeartbeatAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Client Started");
                    _clientSocket.Connect("127.0.0.1", 8989);
                    Console.WriteLine("Client Socket Program - Server Connected ...");
                    Connected?.Invoke(this, EventArgs.Empty);

                    while (true)
                    {
                        SendMessage("192.168.0.15: HeartBeat");

                        Thread.Sleep(3000);
                    }
                }
                catch (Exception)
                {
                    Disconnected?.Invoke(this, EventArgs.Empty);
                }

                Thread.Sleep(5000);
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                NetworkStream serverStream = _clientSocket.GetStream();
                byte[] outStream = Encoding.ASCII.GetBytes(message + "$");
                Console.WriteLine("Send message: " + message);
                serverStream.Write(outStream, 0, outStream.Length);
                serverStream.Flush();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
