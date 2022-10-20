using MessagePack;

namespace Pace.Common.Network.Packets.Client;

[MessagePackObject]
public class TakeScreenshotResponsePacket : IPacket
{
    [Key(0)]
    public byte[] ImageData { get; set; }

    public TakeScreenshotResponsePacket(byte[] imageData)
    {
        ImageData = imageData;
    }
}