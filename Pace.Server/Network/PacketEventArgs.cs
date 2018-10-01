using Pace.Common.Network;
using Pace.Common.Network.Packets;
using System;

namespace Pace.Server.Network
{
    public class PacketEventArgs : EventArgs
    {
        public PaceClient Client { get; set; }
        public IPacket Packet { get; set; }

        public PacketEventArgs(PaceClient client, IPacket packet)
        {
            Packet = packet;
            Client = client;
        }
    }
}