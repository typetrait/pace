using MessagePack;
using Pace.Common.Compression;
using Pace.Common.Network.Packets;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Pace.Common.Network;

public class PaceClient
{
    public event EventHandler PacketReceived;
    public event EventHandler PacketSent;

    public TcpClient TcpClient { get; set; }

    public IPEndPoint RemoteAddress => TcpClient.Client.RemoteEndPoint as IPEndPoint;
    public IPEndPoint LocalAddress => TcpClient.Client.RemoteEndPoint as IPEndPoint;
    public int Port => LocalAddress.Port;

    public PaceClient()
    {
        TcpClient = new TcpClient();
    }

    public PaceClient(TcpClient client)
    {
        TcpClient = client;
    }

    public void Connect(IPAddress address, int port)
    {
        TcpClient = new TcpClient();
        TcpClient.Connect(address, port);
    }

    public IPacket ReadPacket()
    {
        byte[] packetBytes = QuickLZ.Decompress(ReadData());

        PacketReceived?.Invoke(this, EventArgs.Empty);

        using var ms = new MemoryStream(packetBytes);
        return MessagePackSerializer.Deserialize<IPacket>(ms);
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
            MessagePackSerializer.Serialize(ms, packet);

            byte[] packetBytes = QuickLZ.Compress(ms.ToArray(), 3);
            SendData(packetBytes);
        }

        PacketSent?.Invoke(this, EventArgs.Empty);
    }

    public void SendData(byte[] data)
    {
        TcpClient.GetStream().Write(BitConverter.GetBytes(data.Length), 0, 4);
        TcpClient.GetStream().Write(data, 0, data.Length);
    }
}