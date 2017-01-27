using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using TcpServer.EventArguments;

namespace TcpServer
{
    public class Server
    {
        public EventHandler MessageReceived;

        private TcpClient _readSocket;
        private TcpClient _writeSocket = new TcpClient();

        public Server()
        {
            Task task = new Task(WaitForMessage);
            task.Start();
        }

        async void WaitForMessage()
        {
            Task task = WaitForMessageAsync();
            await task;
        }

        async Task WaitForMessageAsync()
        {
            while (true)
            {
                int port = 8989;
                IPAddress local = IPAddress.Parse("127.0.0.1");

                TcpListener serverSocket = new TcpListener(local, port);
                serverSocket.Start();
                _readSocket = default(TcpClient);
                _readSocket = serverSocket.AcceptTcpClient();

                NetworkStream networkStream = _readSocket.GetStream();
                byte[] bytesFrom = new byte[10025];

                while ((true))
                {
                    try
                    {
                        networkStream.Read(bytesFrom, 0, (int)_readSocket.ReceiveBufferSize);
                        string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                        dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                        Console.WriteLine(" >> Data from client - " + dataFromClient);

                        MessageReceived?.Invoke(this, new MessageReceivedEventArgs(dataFromClient));
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
        }

        public void SendMessage(string ip, string message)
        {
            try
            {
                _writeSocket.Connect("127.0.0.1", 9989);
                NetworkStream networkStream = _writeSocket.GetStream();
                byte[] sendBytes = Encoding.ASCII.GetBytes(message + "$");
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
