using Pace.Common.Network;

namespace Pace.Server.Model;

public class ClientInfo
{
    public PaceClient Owner { get; init; }
    public string Identifier { get; init; }
    public string Address { get; init; }
    public int Port { get; init; }
    public string Username { get; init; }
    public string ComputerName { get; init; }
    public string OS { get; init; }
}