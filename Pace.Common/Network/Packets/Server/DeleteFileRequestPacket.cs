using System;

namespace Pace.Common.Network.Packets.Server
{
    [Serializable]
    public class DeleteFileRequestPacket : IPacket
    {
        public string File { get; set; }

        public DeleteFileRequestPacket(string file)
        {
            File = file;
        }
    }
}