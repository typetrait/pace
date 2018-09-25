using NetSerializer;
using Pace.Common.Network;
using Pace.Common.Network.Packets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Server.Network
{
    public class PaceServer
    {
        public event EventHandler<PacketEventArgs> PacketReceived;
        public event EventHandler<ClientEventArgs> ClientConnected;
        public event EventHandler<ClientEventArgs> ClientDisconnected;

        public readonly PacketChannel PacketChannel;
        public List<PaceClient> ConnectedClients { get; set; }
        public bool Listening { get; set; }
        public Serializer Serializer { get; set; }

        private TcpListener listener;

        public PaceServer()
        {
            PacketChannel = new PacketChannel();
        }

        public void Start()
        {
            ConnectedClients = new List<PaceClient>();

            listener = new TcpListener(IPAddress.Any, 7777);
            listener.Start();

            Listening = true;

            Serializer = new Serializer(PacketRegistry.GetPacketTypes());

            Task.Factory.StartNew(HandleClientConnection);
        }

        public void Shutdown()
        {
            Listening = false;
        }

        protected void OnClientConnected(PaceClient client)
        {
            ConnectedClients.Add(client);
            ClientConnected?.Invoke(this, new ClientEventArgs(client));
        }

        protected void OnClientDisconnected(PaceClient client)
        {
            ConnectedClients.Remove(client);
            ClientDisconnected?.Invoke(this, new ClientEventArgs(client));
        }

        protected void OnPacketReceived(IPacket packet, PaceClient client)
        {
            PacketReceived?.Invoke(this, new PacketEventArgs(packet, client));
            PacketChannel.HandlePacket(packet);
        }

        private void HandleClientConnection()
        {
            while (Listening)
            {
                var client = new PaceClient(listener.AcceptTcpClient());
                OnClientConnected(client);

                Task.Factory.StartNew(() =>
                {
                    while (true)
                    {
                        var packet = client.ReadPacket();
                        OnPacketReceived(packet, client);
                    }
                });
            }

            listener.Stop();
        }
    }
}