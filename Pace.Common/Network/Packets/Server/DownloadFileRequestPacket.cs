using System;

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