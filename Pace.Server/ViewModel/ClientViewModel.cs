using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Pace.Server.ViewModel
{
    public class ClientViewModel
    {
        private readonly PaceServer server;

        public ObservableCollection<Client> Clients { get; set; }

        public ClientViewModel()
        {
            Clients = new ObservableCollection<Client>();

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
            MessageBox.Show(
                $"Client connected from {e.Client.TcpClient.Client.RemoteEndPoint}.",
                "Pace",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            e.Client.SendPacket(new GetSystemInfoRequestPacket());
        }

        private void Server_ClientDisconnected(object sender, ClientEventArgs e)
        {
            throw new NotImplementedException();
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

            Application.Current.Dispatcher.Invoke(() =>
            {
                Clients.Add(client);
            });
        }
    }
}