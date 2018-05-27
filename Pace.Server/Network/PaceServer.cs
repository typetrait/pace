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
        public event EventHandler<ClientConnectedEventArgs> ClientConnected;

        public List<PaceClient> ConnectedClients { get; set; }
        public bool Listening { get; set; }
        public Serializer Serializer { get; set; }

        private TcpListener listener;

        public void Start()
        {
            ConnectedClients = new List<PaceClient>();

            listener = new TcpListener(IPAddress.Any, 7777);
            listener.Start();

            Listening = true;

            Serializer = new Serializer(PacketRegistry.GetPacketTypes());

            Task.Factory.StartNew(() => HandleClientConnection());
        }

        public void Shutdown()
        {
            Listening = false;
        }

        protected void OnClientConnected(PaceClient client)
        {
            ConnectedClients.Add(client);
            ClientConnected?.Invoke(this, new ClientConnectedEventArgs(client));
        }

        private void HandleClientConnection()
        {
            while (Listening)
            {
                var client = new PaceClient(listener.AcceptTcpClient());
                OnClientConnected(client);
            }

            listener.Stop();
        }
    }
}