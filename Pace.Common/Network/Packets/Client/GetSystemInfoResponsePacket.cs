using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets.Client
{
    [Serializable]
    public class GetSystemInfoResponsePacket : IPacket
    {
        public string Identifier { get; set; }
        public string Address { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string ComputerName { get; set; }
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
}