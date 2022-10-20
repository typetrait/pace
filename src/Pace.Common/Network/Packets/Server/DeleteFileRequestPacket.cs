using MessagePack;

namespace Pace.Common.Network.Packets.Server;

[MessagePackObject]
public class DeleteFileRequestPacket : IPacket
{
    [Key(0)]
    public string Path { get; set; }

    public DeleteFileRequestPacket(string path)
    {
        Path = path;
    }
}