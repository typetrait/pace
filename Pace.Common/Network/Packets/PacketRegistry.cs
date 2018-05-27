using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
                typeof(GetDirectoryResponsePacket)
            };
        }
    }
}