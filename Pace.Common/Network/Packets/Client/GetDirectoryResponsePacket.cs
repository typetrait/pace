using System;

namespace Pace.Common.Network.Packets.Client
{
    [Serializable]
    public class GetDirectoryResponsePacket : IPacket
    {
        public string Path { get; set; }
        public string[] Folders { get; set; }
        public string[] Files { get; set; }
        public long[] FolderSizes { get; set; }
        public long[] FileSizes { get; set; }
    }
}