using Pace.Common.Model;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;

namespace Pace.Server.ViewModel
{
    public class FileExplorerViewModel : ViewModelBase
    {
        public ObservableCollection<FileSystemEntry> Files { get; set; }

        public string Path { get; set; }

        private FileSystemEntry currentDirectory;
        public FileSystemEntry CurrentDirectory
        {
            get { return currentDirectory; }
            set
            {
                currentDirectory = value;
                OnPropertyChanged(() => CurrentDirectory);
            }
        }

        private FileSystemEntry selectedFile;
        public FileSystemEntry SelectedFile
        {
            get { return selectedFile; }
            set
            {
                selectedFile = value;
                OnPropertyChanged(() => selectedFile);
            }
        }

        public ICommand NavigateCommand { get; set; }
        public ICommand NavigateSelectedCommand { get; set; }

        public ClientInfo Client { get; set; }

        public FileExplorerViewModel(PaceServer server, ClientInfo client)
        {
            Files = new ObservableCollection<FileSystemEntry>();

            server.PacketChannel.RegisterHandler<GetDirectoryResponsePacket>(HandleGetDirectory);
            server.PacketChannel.RegisterHandler<NotifyStatusPacket>(HandleNotifyStatus);

            if (server.ConnectedClients.Count > 0)
            {
                client.Owner.SendPacket(new GetDirectoryRequestPacket(string.Empty));
            }

            Client = client;

            NavigateCommand = new RelayCommand<string>(Navigate);
            NavigateSelectedCommand = new RelayCommand<string>(NavigateSelected);
        }

        private void HandleGetDirectory(IPacket packet)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Files.Clear();
            });

            var directoryResponse = (GetDirectoryResponsePacket)packet;

            CurrentDirectory = new FileSystemEntry(directoryResponse.Name, directoryResponse.Path, 0, FileType.Directory);

            for (int i = 0; i < directoryResponse.Folders.Length; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Files.Add(new FileSystemEntry
                    (
                        directoryResponse.Folders[i],
                        System.IO.Path.Combine(CurrentDirectory.Path, directoryResponse.Folders[i]),
                        0,
                        FileType.Directory
                    ));
                });
            }

            for (int i = 0; i < directoryResponse.Files.Length; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Files.Add(new FileSystemEntry
                    (
                        directoryResponse.Files[i],
                        System.IO.Path.Combine(CurrentDirectory.Path, directoryResponse.Files[i]),
                        directoryResponse.FileSizes[i],
                        FileType.File
                    ));
                });
            }
        }

        private void HandleNotifyStatus(IPacket packet)
        {
            var statusPacket = (NotifyStatusPacket)packet;
            MessageBox.Show(statusPacket.StatusMessage, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        private void Navigate(string path)
        {
            Client.Owner.SendPacket(new GetDirectoryRequestPacket(path));
        }

        private void NavigateSelected(string s)
        {
            if (SelectedFile.Type != FileType.Directory)
                return;

            Client.Owner.SendPacket(new GetDirectoryRequestPacket(SelectedFile.Path));
        }
    }
}