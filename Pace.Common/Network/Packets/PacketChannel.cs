using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Common.Network.Packets
{
    public class PacketChannel
    {
        public Dictionary<Type, Action<object>> Actions { get; private set; }

        public PacketChannel()
        {
            Actions = new Dictionary<Type, Action<object>>();
        }

        public void HandlePacket(IPacket packet)
        {
            foreach (var action in Actions)
            {
                if (action.Key.Equals(packet.GetType()))
                {
                    action.Value(packet);
                }
            }
        }

        public void RegisterHandler<TPacket>(Action<object> handler)
        {
            Actions.Add(typeof(TPacket), handler);
        }
    }
}