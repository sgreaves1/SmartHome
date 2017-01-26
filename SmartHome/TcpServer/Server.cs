using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using TcpServer.EventArguments;

namespace TcpServer
{
    public class Server
    {
        public EventHandler MessageReceived;

        private TcpClient clientSocket;

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
                clientSocket = default(TcpClient);
                clientSocket = serverSocket.AcceptTcpClient();
                int requestCount = 0;

                requestCount = requestCount + 1;
                NetworkStream networkStream = clientSocket.GetStream();
                byte[] bytesFrom = new byte[10025];

                while ((true))
                {
                    try
                    {
                        networkStream.Read(bytesFrom, 0, (int) clientSocket.ReceiveBufferSize);
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

                clientSocket.Close();
                serverSocket.Stop();
                Console.WriteLine(" >> exit");
                Console.ReadLine();
            }
        }

        public void SendMessage(string ip, string message)
        {
            try
            {
                Byte[] sendBytes = Encoding.ASCII.GetBytes(message);
                NetworkStream networkStream = clientSocket.GetStream();
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
