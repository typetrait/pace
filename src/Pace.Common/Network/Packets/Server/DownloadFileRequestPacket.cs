using MessagePack;

namespace Pace.Common.Network.Packets.Server;

[MessagePackObject]
public class DownloadFileRequestPacket : IPacket
{
    [Key(0)]
    public string Url { get; set; }

    public DownloadFileRequestPacket(string url)
    {
        Url = url;
    }
}