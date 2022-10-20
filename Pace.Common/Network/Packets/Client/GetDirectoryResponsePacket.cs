using MessagePack;

namespace Pace.Common.Network.Packets.Client;

[MessagePackObject]
public class GetDirectoryResponsePacket : IPacket
{
    [Key(0)]
    public string Name { get; set; }

    [Key(1)]
    public string Path { get; set; }

    [Key(2)]
    public string[] Folders { get; set; }

    [Key(3)]
    public string[] Files { get; set; }

    [Key(4)]
    public long[] FolderSizes { get; set; }

    [Key(5)]
    public long[] FileSizes { get; set; }
}