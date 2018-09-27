using Pace.Common.Network;
using Pace.Common.Network.Packets;
using System;

namespace Pace.Server.Network
{
    public class PacketEventArgs : EventArgs
    {
        public IPacket Packet { get; set; }
        public PaceClient Client { get; set; }

        public PacketEventArgs(IPacket packet, PaceClient client)
        {
            Packet = packet;
            Client = client;
        }
    }
}