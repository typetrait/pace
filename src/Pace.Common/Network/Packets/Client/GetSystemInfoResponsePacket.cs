using MessagePack;

namespace Pace.Common.Network.Packets.Client;

[MessagePackObject]
public class GetSystemInfoResponsePacket : IPacket
{
    [Key(0)]
    public string Identifier { get; set; }

    [Key(1)]
    public string Address { get; set; }

    [Key(2)]
    public int Port { get; set; }

    [Key(3)]
    public string Username { get; set; }

    [Key(4)]
    public string ComputerName { get; set; }

    [Key(5)]
    public string OS { get; set; }

    public GetSystemInfoResponsePacket(string identifier, string address, int port, string username, string computerName, string os)
    {
        Identifier = identifier;
        Address = address;
        Port = port;
        Username = username;
        ComputerName = computerName;
        OS = os;
    }
}