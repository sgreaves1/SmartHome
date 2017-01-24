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

        public Server()
        {
            WaitForMessage();
        }

        private async void WaitForMessage()
        {
            await WaitForMessageAsync();
        }

        async Task<int> WaitForMessageAsync()
        {
            int port = 8989;
            IPAddress local = IPAddress.Parse("127.0.0.1");

            TcpListener serverSocket = new TcpListener(local, port);
            serverSocket.Start();
            TcpClient clientSocket = default(TcpClient);
            clientSocket = await serverSocket.AcceptTcpClientAsync();
            int requestCount = 0;

            while ((true))
            {
                try
                {
                    requestCount = requestCount + 1;
                    NetworkStream networkStream = clientSocket.GetStream();
                    byte[] bytesFrom = new byte[10025];
                    networkStream.Read(bytesFrom, 0, (int)clientSocket.ReceiveBufferSize);
                    string dataFromClient = Encoding.ASCII.GetString(bytesFrom);
                    dataFromClient = dataFromClient.Substring(0, dataFromClient.IndexOf("$"));
                    Console.WriteLine(" >> Data from client - " + dataFromClient);

                    MessageReceived?.Invoke(this, new MessageReceivedEventArgs(dataFromClient));

                    string serverResponse = "Last Message from client" + dataFromClient;
                    Byte[] sendBytes = Encoding.ASCII.GetBytes(serverResponse);
                    networkStream.Write(sendBytes, 0, sendBytes.Length);
                    networkStream.Flush();
                    Console.WriteLine(" >> " + serverResponse);
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
}
