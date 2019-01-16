using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pace.Server.ViewModel
{
    public class FileExplorerViewModel
    {
        public ObservableCollection<File> Files { get; set; }

        public FileExplorerViewModel(PaceServer server)
        {
            server.PacketChannel.RegisterHandler<GetDirectoryResponsePacket>(HandleGetDirectory);

            server.ConnectedClients[0].SendPacket(new GetDirectoryRequestPacket(@"D:\Files\Temporary")); // TODO: Clean this up.
        }

        private void HandleGetDirectory(IPacket packet)
        {
            var directoryResponse = (GetDirectoryResponsePacket)packet;

            var files = directoryResponse.Files.Select(file => new File(file));
            Files = new ObservableCollection<File>(files);
        }
    }
}