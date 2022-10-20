using Pace.Common.Network;
using Pace.Common.Network.Packets;
using System;
using System.Collections.Generic;

namespace Pace.Client.Network;

public class PacketChannel
{
    public readonly Dictionary<Type, Action<PaceClient, IPacket>> Handlers;

    public PacketChannel()
    {
        Handlers = new Dictionary<Type, Action<PaceClient, IPacket>>();
    }

    public void HandlePacket(PaceClient client, IPacket packet)
    {
        foreach (var action in Handlers)
        {
            if (action.Key.Equals(packet.GetType()))
            {
                action.Value(client, packet);
                return;
            }
        }
    }

    public void RegisterHandler<TPacket>(Action<PaceClient, IPacket> handler)
    {
        if (Handlers.ContainsKey(typeof(TPacket)))
        {
            Handlers.Remove(typeof(TPacket));
        }

        Handlers.Add(typeof(TPacket), handler);
    }
}