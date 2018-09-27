using System;
using System.Collections.Generic;

namespace Pace.Common.Network.Packets
{
    public class PacketChannel
    {
        public readonly Dictionary<Type, Action<IPacket>> Handlers;

        public PacketChannel()
        {
            Handlers = new Dictionary<Type, Action<IPacket>>();
        }

        public void HandlePacket(IPacket packet)
        {
            foreach (var action in Handlers)
            {
                if (action.Key.Equals(packet.GetType()))
                {
                    action.Value(packet);
                    return;
                }
            }
        }

        public void RegisterHandler<TPacket>(Action<IPacket> handler)
        {
            if (Handlers.ContainsKey(typeof(TPacket)))
            {
                Handlers.Remove(typeof(TPacket));
            }

            Handlers.Add(typeof(TPacket), handler);
        }
    }
}