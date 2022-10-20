using MessagePack;

namespace Pace.Common.Network.Packets.Server;

[MessagePackObject]
public class GetDirectoryRequestPacket : IPacket
{
    [Key(0)]
    public string Path { get; set; }

    public GetDirectoryRequestPacket(string path)
    {
        Path = path;
    }
}