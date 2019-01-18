using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System.Collections.ObjectModel;

namespace Pace.Server.ViewModel
{
    public class FileExplorerViewModel
    {
        public ObservableCollection<File> Files { get; set; }

        public FileExplorerViewModel(PaceServer server)
        {
            server.PacketChannel.RegisterHandler<GetDirectoryResponsePacket>(HandleGetDirectory);

            if (server.ConnectedClients.Count > 0)
                server.ConnectedClients[0].SendPacket(new GetDirectoryRequestPacket(@"D:\Files\Temporary")); // TODO: Clean this up.

            Files = new ObservableCollection<File>();
        }

        private void HandleGetDirectory(IPacket packet)
        {
            var directoryResponse = (GetDirectoryResponsePacket)packet;

            for (int i = 0; i < directoryResponse.Files.Length; i++)
            {
                Files.Add(new File(directoryResponse.Files[i], directoryResponse.FileSizes[i]));
            }
        }
    }
}