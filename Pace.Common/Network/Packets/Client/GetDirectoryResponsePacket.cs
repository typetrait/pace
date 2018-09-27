using System;

namespace Pace.Common.Network.Packets.Client
{
    [Serializable]
    public class GetDirectoryResponsePacket : IPacket
    {
        public string[] Folders { get; set; }
        public string[] Files { get; set; }
    }
}