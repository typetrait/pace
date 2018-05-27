using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets.Client
{
    [Serializable]
    public class GetDirectoryResponsePacket : IPacket
    {
        public string[] Folders { get; set; }
        public string[] Files { get; set; }
    }
}