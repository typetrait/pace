using MaterialDesignThemes.Wpf;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;

namespace Pace.Server.ViewModel
{
    public class ClientViewModel
    {
        private readonly PaceServer server;

        public ObservableCollection<Client> Clients { get; set; }
        public SnackbarMessageQueue ConnectedMessageQueue { get; set; }

        public ClientViewModel()
        {
            Clients = new ObservableCollection<Client>();

            ConnectedMessageQueue = new SnackbarMessageQueue(TimeSpan.FromMilliseconds(5000));

            #if DEBUG
                if (DesignerProperties.GetIsInDesignMode(new DependencyObject())) return;
            #endif

            server = new PaceServer();
            server.ClientConnected += Server_ClientConnected;
            server.ClientDisconnected += Server_ClientDisconnected;

            server.PacketChannel.RegisterHandler<GetSystemInfoResponsePacket>(HandleSystemInfo);

            server.Start();
        }

        private void Server_ClientConnected(object sender, ClientEventArgs e)
        {
            ConnectedMessageQueue.Enqueue($"Client connected from {e.Client.TcpClient.Client.RemoteEndPoint}.");

            e.Client.SendPacket(new GetSystemInfoRequestPacket());
        }

        private void Server_ClientDisconnected(object sender, ClientEventArgs e)
        {
            var list = new List<Client>(Clients);
            var client = list.Find(c => c.Owner == e.Client);

            Application.Current.Dispatcher.Invoke(() =>
            {
                Clients.Remove(client);
            });
        }

        private void HandleSystemInfo(object packet)
        {
            var systemInfoResponse = (GetSystemInfoResponsePacket)packet;

            var client = new Client()
            {
                Identifier = systemInfoResponse.Identifier,
                Address = systemInfoResponse.Address,
                Port = systemInfoResponse.Port,
                Username = systemInfoResponse.Username,
                ComputerName = systemInfoResponse.ComputerName,
                OS = systemInfoResponse.OS
            };

            client.Owner = server.ConnectedClients.Find(c => client.Address == c.TcpClient.Client.RemoteEndPoint.ToString().Split(':')[0]);

            Application.Current.Dispatcher.Invoke(() =>
            {
                Clients.Add(client);
            });
        }
    }
}