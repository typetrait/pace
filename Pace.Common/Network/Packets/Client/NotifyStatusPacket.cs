using System;

namespace Pace.Common.Network.Packets.Client
{
    [Serializable]
    public class NotifyStatusPacket : IPacket
    {
        public string StatusMessage { get; set; }

        public NotifyStatusPacket(string statusMessage)
        {
            StatusMessage = statusMessage;
        }
    }
}