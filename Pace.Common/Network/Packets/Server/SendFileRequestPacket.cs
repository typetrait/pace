using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets.Server
{
    [Serializable]
    public class SendFileRequestPacket : IPacket
    {
        public string Filename { get; set; }
        public byte[] FileData { get; set; }

        public SendFileRequestPacket(string filename, byte[] fileData)
        {
            Filename = filename;
            FileData = fileData;
        }
    }
}