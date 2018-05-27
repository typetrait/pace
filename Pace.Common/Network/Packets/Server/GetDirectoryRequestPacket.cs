using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets.Server
{
    [Serializable]
    public class GetDirectoryRequestPacket : IPacket
    {
        public string Path { get; set; }

        public GetDirectoryRequestPacket(string path)
        {
            Path = path;
        }
    }
}