using System;
using System.Collections.Generic;
using System.Text;
using MultiplayerServer.Core;

namespace MultiplayerServer
{
    class PacketHandler : IRecieveHandler
    {
        Dictionary<string, string> packetInfo;

        public PacketHandler()
        {
            packetInfo = new Dictionary<string, string>();
        }

        public string SeralizePacketInfo()
        {
            string seralizedPacketInfo = "";

            foreach(var e in packetInfo)
            {
                seralizedPacketInfo += packetInfo[e.Key] + "|";
            }

            return seralizedPacketInfo;
        }

        public byte[] Invoke(byte[] byteStream)
        {
            string strStream = Encoding.ASCII.GetString(byteStream, 0, byteStream.Length);
            Console.WriteLine(strStream);
            string uid = strStream.Split(',')[0];

            if (packetInfo.ContainsKey(uid))
            {
                packetInfo[uid] = strStream;
            } else
            {
                packetInfo.Add(uid, strStream);
            }

            return Encoding.ASCII.GetBytes(SeralizePacketInfo());
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var handler = new PacketHandler();

            var server = new Server(5612);
            server.StartRecieve(handler);
        }
    }
}
