using System;

namespace TcpServer.EventArguments
{
    public class MessageReceivedEventArgs : EventArgs
    {
        public MessageReceivedEventArgs(string msg)
        {
            try
            {
                message = new MessageFromClient();

                string[] words = msg.Split(new [] {':'});

                message.Ip = words[0];
                message.Message = words[1];
            }
            catch (Exception)
            {
                
            }
        }

        public MessageFromClient message;
    }
}
