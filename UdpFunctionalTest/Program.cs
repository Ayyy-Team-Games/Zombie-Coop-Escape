using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace UdpFunctionalTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new UdpClient();
            var multiplayerEndpoint = new IPEndPoint(IPAddress.Parse("23.115.161.200"), 5612);
            client.Connect(multiplayerEndpoint);

            string positions = String.Format("{0},{1},{2},{3},{4},{5},{6}",
               "testing123",
               "12",
               "2",
               "2",
               "2",
               "2",
               "2"
           );
            
            byte[] sendBuffer = Encoding.ASCII.GetBytes(positions);
            client.Send(sendBuffer, sendBuffer.Length);

            var recievedStream = client.Receive(ref multiplayerEndpoint);
            Console.WriteLine(recievedStream);
            string recievedData = Encoding.ASCII.GetString(recievedStream, 0, recievedStream.Length);

            Console.WriteLine(recievedData);
        }
    }
}
