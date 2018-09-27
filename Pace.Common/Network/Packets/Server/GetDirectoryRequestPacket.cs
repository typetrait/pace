using System;

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