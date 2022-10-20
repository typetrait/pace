using MessagePack;

namespace Pace.Common.Network.Packets.Client;

[MessagePackObject]
public class NotifyStatusResponsePacket : IPacket
{
    [Key(0)]
    public string StatusMessage { get; set; }

    public NotifyStatusResponsePacket(string statusMessage)
    {
        StatusMessage = statusMessage;
    }
}