using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Windows;

namespace Pace.Server.ViewModel;

public class ClientViewModel : ViewModelBase
{
    private readonly PaceServer server;
    private readonly FileWindowService fileManagerService;

    private ClientInfo selectedClient;
    public ClientInfo SelectedClient
    {
        get { return selectedClient; }
        set
        {
            selectedClient = value;
            OnPropertyChanged(() => selectedClient);
            OnPropertyChanged(() => IsItemSelected);
        }
    }

    public bool IsItemSelected => SelectedClient != null;

    public ObservableCollection<ClientInfo> Clients { get; set; }

    public RelayCommand<ClientInfo> OpenFileManagerCommand { get; set; }
    public RelayCommand<ClientInfo> RestartCommand { get; set; }

    public ClientViewModel()
    {
        fileManagerService = new FileWindowService();

        Clients = new ObservableCollection<ClientInfo>();

        OpenFileManagerCommand = new RelayCommand<ClientInfo>(OpenFileManager);
        RestartCommand = new RelayCommand<ClientInfo>(RestartClient);

        #if DEBUG
        if (DesignerProperties.GetIsInDesignMode(new DependencyObject()))
        {
            return;
        }
        #endif

        server = new PaceServer();
        server.ClientConnected += Server_ClientConnected;
        server.ClientDisconnected += Server_ClientDisconnected;

        server.PacketChannel.RegisterHandler<GetSystemInfoResponsePacket>(HandleSystemInfo);

        server.Start();
    }

    private void Server_ClientConnected(object sender, ClientEventArgs e)
    {
        e.Client.SendPacket(new GetSystemInfoRequestPacket());
    }

    private void Server_ClientDisconnected(object sender, ClientEventArgs e)
    {
        var list = new List<ClientInfo>(Clients);
        var client = list.Find(c => c.Owner == e.Client);

        if (client == SelectedClient)
        {
            SelectedClient = null;
        }

        Application.Current.Dispatcher.Invoke(() =>
        {
            Clients.Remove(client);
        });
    }

    private void OpenFileManager(ClientInfo client)
    {
        fileManagerService.ShowWindow(server, SelectedClient);
    }

    private void RestartClient(ClientInfo client)
    {
        SelectedClient.Owner.SendPacket(new RestartRequestPacket());
    }

    private void HandleSystemInfo(IPacket packet)
    {
        var systemInfoResponse = packet as GetSystemInfoResponsePacket;

        var clientInfo = new ClientInfo()
        {
            Owner = server.ConnectedClients.First(),
            Identifier = systemInfoResponse.Identifier,
            Address = systemInfoResponse.Address,
            Port = systemInfoResponse.Port,
            Username = systemInfoResponse.Username,
            ComputerName = systemInfoResponse.ComputerName,
            OS = systemInfoResponse.OS
        };

        Application.Current.Dispatcher.Invoke(() =>
        {
            Clients.Add(clientInfo);
        });
    }
}