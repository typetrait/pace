using System;

namespace Pace.Common.Network.Packets.Server
{
    [Serializable]
    public class DeleteFileRequestPacket : IPacket
    {
        public string Path { get; set; }

        public DeleteFileRequestPacket(string path)
        {
            Path = path;
        }
    }
}