using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;

namespace Pace.Common.Network.Packets;

[MessagePack.Union(0, typeof(GetDirectoryResponsePacket))]
[MessagePack.Union(1, typeof(GetDrivesResponsePacket))]
[MessagePack.Union(2, typeof(GetSystemInfoResponsePacket))]
[MessagePack.Union(3, typeof(NotifyStatusResponsePacket))]
[MessagePack.Union(4, typeof(TakeScreenshotResponsePacket))]
[MessagePack.Union(5, typeof(DeleteFileRequestPacket))]
[MessagePack.Union(6, typeof(DownloadFileRequestPacket))]
[MessagePack.Union(7, typeof(GetDirectoryRequestPacket))]
[MessagePack.Union(8, typeof(GetDrivesRequestPacket))]
[MessagePack.Union(9, typeof(GetSystemInfoRequestPacket))]
[MessagePack.Union(10, typeof(RestartRequestPacket))]
[MessagePack.Union(11, typeof(SendFileRequestPacket))]
[MessagePack.Union(12, typeof(TakeScreenshotRequestPacket))]
public interface IPacket
{

}