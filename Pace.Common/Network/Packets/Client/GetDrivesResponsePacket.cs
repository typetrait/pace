using MessagePack;

namespace Pace.Common.Network.Packets.Client;

[MessagePackObject]
public class GetDrivesResponsePacket : IPacket
{
    [Key(0)]
    public string[] Drives { get; set; }

    public GetDrivesResponsePacket(string[] drives)
    {
        Drives = drives;
    }
}