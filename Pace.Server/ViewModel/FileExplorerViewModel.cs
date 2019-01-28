using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System.Collections.ObjectModel;
using System.Windows;

namespace Pace.Server.ViewModel
{
    public class FileExplorerViewModel : ViewModelBase
    {
        public ObservableCollection<File> Files { get; set; }

        private string currentDirectory;
        public string CurrentDirectory 
        {
            get { return currentDirectory; }
            set
            {
                currentDirectory = value;
                OnPropertyChanged(() => CurrentDirectory); 
            }
        }

        public FileExplorerViewModel(PaceServer server)
        {
            server.PacketChannel.RegisterHandler<GetDirectoryResponsePacket>(HandleGetDirectory);

            if (server.ConnectedClients.Count > 0)
            {
                server.ConnectedClients[0].SendPacket(new GetDirectoryRequestPacket(@"D:\Files\Temporary")); // TODO: Clean this up.
                CurrentDirectory = @"D:\Files\Temporary";
            }

            Files = new ObservableCollection<File>();
        }

        private void HandleGetDirectory(IPacket packet)
        {
            var directoryResponse = (GetDirectoryResponsePacket)packet;

            for (int i = 0; i < directoryResponse.Files.Length; i++)
            {
                Application.Current.Dispatcher.Invoke(() =>
                {
                    Files.Add(new File(directoryResponse.Files[i], directoryResponse.FileSizes[i]));
                });
            }
        }
    }
}