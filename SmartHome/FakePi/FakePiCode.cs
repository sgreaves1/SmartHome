using System;
using System.Net.Sockets;
using System.Text;

namespace FakePi
{
    public class FakePiCode
    {
        private TcpClient clientSocket = new TcpClient();

        public FakePiCode()
        {

            Console.WriteLine("Client Started");
            clientSocket.Connect("127.0.0.1", 8989);
            Console.WriteLine("Client Socket Program - Server Connected ...");

            NetworkStream serverStream = clientSocket.GetStream();
            byte[] outStream = Encoding.ASCII.GetBytes("192.168.0.15: Connected" + "$");
            serverStream.Write(outStream, 0, outStream.Length);
            serverStream.Flush();

            byte[] inStream = new byte[10025];
            serverStream.Read(inStream, 0, (int) clientSocket.ReceiveBufferSize);
            string returndata = Encoding.ASCII.GetString(inStream);
            Console.WriteLine(returndata);
            Console.WriteLine("");
        }
    }
}
