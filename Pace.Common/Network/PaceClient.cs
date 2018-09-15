using NetSerializer;
using Pace.Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network
{
    public class PaceClient
    {
        public TcpClient TcpClient { get; set; }

        private Serializer serializer;

        public PaceClient()
        {
            TcpClient = new TcpClient();

            serializer = new Serializer(PacketRegistry.GetPacketTypes());
        }

        public PaceClient(TcpClient client)
        {
            TcpClient = client;

            serializer = new Serializer(PacketRegistry.GetPacketTypes());
        }

        public void Connect(IPAddress address, int port)
        {
            TcpClient.Connect(address, port);
        }

        public IPacket ReadPacket()
        {
            byte[] packetBytes = ReadData();

            using (var ms = new MemoryStream(packetBytes))
            {
                return (IPacket)serializer.Deserialize(ms);
            }
        }

        public byte[] ReadData()
        {
            var stream = TcpClient.GetStream();

            byte[] packetLengthBytes = new byte[4];

            int headerBytesRead = 0;
            int headerRead = 0;

            while (headerBytesRead < packetLengthBytes.Length)
            {
                headerRead = stream.Read(packetLengthBytes, headerBytesRead, packetLengthBytes.Length);
                headerBytesRead += headerRead;
            }

            int packetLength = BitConverter.ToInt32(packetLengthBytes, 0);

            byte[] packetData = new byte[packetLength];

            int bytesRead = 0;
            int read = 0;

            while (bytesRead < packetLength)
            {
                read = stream.Read(packetData, bytesRead, packetLength - bytesRead);
                bytesRead += read;
            }

            return packetData;
        }

        public void SendPacket(IPacket packet)
        {
            using (var ms = new MemoryStream())
            {
                serializer.Serialize(ms, packet);

                byte[] packetBytes = ms.ToArray();
                SendData(packetBytes);
            }
        }

        public void SendData(byte[] data)
        {
            TcpClient.GetStream().Write(BitConverter.GetBytes(data.Length), 0, 4);
            TcpClient.GetStream().Write(data, 0, data.Length);
        }
    }
}