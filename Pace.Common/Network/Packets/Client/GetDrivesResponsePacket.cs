using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets.Client
{
    [Serializable]
    public class GetDrivesResponsePacket : IPacket
    {
        public string[] Drives { get; set; }

        public GetDrivesResponsePacket(string[] drives)
        {
            Drives = drives;
        }
    }
}