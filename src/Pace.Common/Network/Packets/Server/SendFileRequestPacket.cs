using MessagePack;

namespace Pace.Common.Network.Packets.Server;

[MessagePackObject]
public class SendFileRequestPacket : IPacket
{
    [Key(0)]
    public string Filename { get; set; }

    [Key(1)]
    public byte[] FileData { get; set; }

    public SendFileRequestPacket(string filename, byte[] fileData)
    {
        Filename = filename;
        FileData = fileData;
    }
}