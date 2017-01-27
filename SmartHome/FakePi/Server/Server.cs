using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakePi.Server
{
    public class Server
    {
        TcpClient _writeSocket = new TcpClient();

        private TcpClient _readSocket;

        public EventHandler Connected;
        public EventHandler Disconnected;
        public EventHandler DataRecieved;

        public Server()
        {
            Task heartBeatTask = new Task(Heartbeat);
            heartBeatTask.Start();

            Task task = new Task(WaitForMessage);
            task.Start();
        }

        async void Heartbeat()
        {
            Task task = HeartbeatAsync();
            await task;
        }

        async void WaitForMessage()
        {
            Task task = WaitForMessageAsync();
            await task;
        }

        async Task HeartbeatAsync()
        {
            while (true)
            {
                try
                {
                    Console.WriteLine("Client Started");
                    _writeSocket.Connect("127.0.0.1", 8989);
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

        async Task WaitForMessageAsync()
        {
            while (true)
            {
                try
                {
                    int port = 7000;
                    IPAddress local = IPAddress.Parse("127.0.0.1");

                    TcpListener serverSocket = new TcpListener(local, port);
                    serverSocket.Start();
                    _readSocket = default(TcpClient);
                    _readSocket = serverSocket.AcceptTcpClient();

                    NetworkStream networkStream = _readSocket.GetStream();
                    byte[] bytesFrom = new byte[70025];

                    while ((true))
                    {
                        try
                        {
                            networkStream.Read(bytesFrom, 0, (int) _readSocket.ReceiveBufferSize);
                            string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                            dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                            Console.WriteLine(" >> Data from client - " + dataFromClient);
                            DataRecieved?.Invoke(this, new DataRecievedEventArgs() {Message = dataFromClient});

                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine(ex.ToString());
                        }
                    }

                    _readSocket.Close();
                    serverSocket.Stop();
                    Console.WriteLine(" >> exit");
                    Console.ReadLine();

                }
                catch (Exception)
                {
                }
            }
        }

        public void SendMessage(string message)
        {
            try
            {
                NetworkStream serverStream = _writeSocket.GetStream();
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
