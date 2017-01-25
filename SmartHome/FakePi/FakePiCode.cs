using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FakePi
{
    public class FakePiCode
    {
        public FakePiCode()
        {
            Task task = new Task(Heartbeat);
            task.Start();
        }

        static async void Heartbeat()
        {
            Task task = HeartbeatAsync();
            await task;
        }

        static async Task HeartbeatAsync()
        {
            while (true)
            {
                try
                {
                    TcpClient clientSocket = new TcpClient();
                    Console.WriteLine("Client Started");
                    clientSocket.Connect("127.0.0.1", 8989);
                    Console.WriteLine("Client Socket Program - Server Connected ...");

                    while (true)
                    {
                        NetworkStream serverStream = clientSocket.GetStream();
                        byte[] outStream = Encoding.ASCII.GetBytes("192.168.0.15: HeartBeat" + "$");
                        Console.WriteLine("Heartbeat");
                        serverStream.Write(outStream, 0, outStream.Length);
                        serverStream.Flush();

                        Thread.Sleep(3000);
                    }

                    //byte[] inStream = new byte[10025];
                    //serverStream.Read(inStream, 0, (int) clientSocket.ReceiveBufferSize);
                    //string returndata = System.Text.Encoding.ASCII.GetString(inStream);
                    //Console.WriteLine(returndata);
                    //Console.WriteLine("");
                }
                catch (Exception)
                {

                }

                Thread.Sleep(5000);
            }
        }
    }
}
