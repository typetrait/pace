using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets.Server
{
    [Serializable]
    public class DownloadFileRequestPacket : IPacket
    {
        public string Url { get; set; }

        public DownloadFileRequestPacket(string url)
        {
            Url = url;
        }
    }
}