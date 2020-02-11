using System;
using System.Threading.Tasks;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;

namespace MultiplayerServer.Core
{
    public interface IRecieveHandler
    {
        byte[] Invoke(byte[] byteStream);
    }

    public class Server
    {
        public readonly UdpClient UdpInstance;
        public readonly uint Port;

        private bool IsRunning { get; set; } = true;

        public Server(uint port)
        {
            UdpInstance = new UdpClient((int)port);
            Port = port;
        }

        public void StartRecieve(IRecieveHandler handler)
        {
            var endpointGroup = new IPEndPoint(IPAddress.Any, (int)Port); // @@ idkkkk
            
            try
            {
                while(IsRunning)
                {                    
                    byte[] byteStream = UdpInstance.Receive(ref endpointGroup);
                    byte[] handlerOutput = handler.Invoke(byteStream);

                    // @@ this should be elsewhere? 
                    UdpInstance.Send(handlerOutput, handlerOutput.Length, endpointGroup);
                                                            
                    Debug.WriteLine("Udp Server is now free!");
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
