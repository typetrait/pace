using Pace.Common.Network;

namespace Pace.Server.Model
{
    public class ClientInfo
    {
        public PaceClient Owner { get; set; }
        public string Identifier { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string ComputerName { get; set; }
        public string OS { get; set; }
    }
}