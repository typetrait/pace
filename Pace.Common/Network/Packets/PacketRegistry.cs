using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using System;

namespace Pace.Common.Network.Packets
{
    public static class PacketRegistry
    {
        public static Type[] GetPacketTypes()
        {
            return new Type[]
            {
                typeof(DownloadFileRequestPacket),
                typeof(GetSystemInfoRequestPacket),
                typeof(GetSystemInfoResponsePacket),
                typeof(TakeScreenshotRequestPacket),
                typeof(TakeScreenshotResponsePacket),
                typeof(SendFileRequestPacket),
                typeof(GetDirectoryRequestPacket),
                typeof(GetDirectoryResponsePacket),
                typeof(DeleteFileRequestPacket),
                typeof(NotifyStatusPacket),
                typeof(GetDrivesRequestPacket),
                typeof(GetDrivesResponsePacket),
                typeof(RestartRequestPacket)
            };
        }
    }
}