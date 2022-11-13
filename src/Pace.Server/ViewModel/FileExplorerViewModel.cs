using Pace.Common.Model;
using Pace.Common.Network.Packets;
using Pace.Common.Network.Packets.Client;
using Pace.Common.Network.Packets.Server;
using Pace.Server.Model;
using Pace.Server.Network;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace Pace.Server.ViewModel;

public class FileExplorerViewModel : ViewModelBase
{
    public ObservableCollection<FileSystemEntry> Files { get; set; }

    public Stack<FileSystemEntry> BackHistory { get; set; }
    public Stack<FileSystemEntry> ForwardHistory { get; set; }

    public bool CanGoBackward => BackHistory.Count > 0;
    public bool CanGoForward => ForwardHistory.Count > 0;

    private string[] _drives;
    public string[] Drives
    {
        get { return _drives; }
        set
        {
            _drives = value;
            OnPropertyChanged(() => Drives);
        }
    }

    private FileSystemEntry _currentDirectory;
    public FileSystemEntry CurrentDirectory
    {
        get { return _currentDirectory; }
        set
        {
            _currentDirectory = value;
            OnPropertyChanged(() => CurrentDirectory);
        }
    }

    private FileSystemEntry _selectedFile;
    public FileSystemEntry SelectedFile
    {
        get { return _selectedFile; }
        set
        {
            _selectedFile = value;
            OnPropertyChanged(() => _selectedFile);
        }
    }

    public ICommand NavigateCommand { get; set; }
    public ICommand NavigateSelectedCommand { get; set; }
    public ICommand NavigateUpCommand { get; set; }
    public ICommand NavigateForwardCommand { get; set; }
    public ICommand NavigateBackCommand { get; set; }
    public ICommand DeleteFileCommand { get; set; }

    public ClientInfo Client { get; set; }

    public FileExplorerViewModel(PaceServer server, ClientInfo client)
    {
        Files = new ObservableCollection<FileSystemEntry>();

        BackHistory = new Stack<FileSystemEntry>();
        ForwardHistory = new Stack<FileSystemEntry>();

        server.PacketChannel.RegisterHandler<GetDirectoryResponsePacket>(HandleGetDirectory);
        server.PacketChannel.RegisterHandler<GetDrivesResponsePacket>(HandleGetDrives);
        server.PacketChannel.RegisterHandler<NotifyStatusResponsePacket>(HandleNotifyStatus);

        if (server.ConnectedClients.Count > 0)
        {
            client.Owner.SendPacket(new GetDirectoryRequestPacket(string.Empty));
            client.Owner.SendPacket(new GetDrivesRequestPacket());
        }

        Client = client;

        NavigateCommand = new RelayCommand<string>(NavigateWithHistory);
        NavigateSelectedCommand = new RelayCommand<string>(NavigateSelected);
        NavigateUpCommand = new RelayCommand<string>(NavigateUp);
        NavigateForwardCommand = new RelayCommand<string>(NavigateForward);
        NavigateBackCommand = new RelayCommand<string>(NavigateBack);
        DeleteFileCommand = new RelayCommand<string>(DeleteFile);
    }

    private void HandleGetDirectory(IPacket packet)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            Files.Clear();
        });

        var directoryResponse = (GetDirectoryResponsePacket)packet;
        CurrentDirectory = new FileSystemEntry(directoryResponse.Name, directoryResponse.Path, 0, FileType.Directory);

        OnPropertyChanged(() => CanGoBackward);
        OnPropertyChanged(() => CanGoForward);

        for (int i = 0; i < directoryResponse.Folders.Length; i++)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                Files.Add(new FileSystemEntry
                (
                    directoryResponse.Folders[i],
                    Path.Combine(CurrentDirectory.Path, directoryResponse.Folders[i]),
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
                    Path.Combine(CurrentDirectory.Path, directoryResponse.Files[i]),
                    directoryResponse.FileSizes[i],
                    FileType.File
                ));
            });
        }
    }

    private void HandleGetDrives(IPacket packet)
    {
        var getDrivesPacket = (GetDrivesResponsePacket)packet;
        Drives = getDrivesPacket.Drives;
    }

    private void HandleNotifyStatus(IPacket packet)
    {
        var statusPacket = (NotifyStatusResponsePacket)packet;
        MessageBox.Show(statusPacket.StatusMessage, "Notification", MessageBoxButton.OK, MessageBoxImage.Information);
    }

    private void Navigate(string path)
    {
        Client.Owner.SendPacket(new GetDirectoryRequestPacket(path));
    }

    private void NavigateWithHistory(string path)
    {
        if (path != CurrentDirectory.Path)
        {
            BackHistory.Push(CurrentDirectory);
        }

        Navigate(path);
    }

    private void NavigateSelected(string s)
    {
        if (SelectedFile.Type != FileType.Directory)
        {
            return;
        }

        NavigateWithHistory(SelectedFile.Path);
    }

    private void NavigateUp(string s)
    {
        NavigateWithHistory(Path.Combine(CurrentDirectory.Path, ".."));
    }

    private void NavigateForward(string s)
    {
        BackHistory.Push(CurrentDirectory);

        FileSystemEntry nextDirectory = ForwardHistory.Pop();
        Navigate(nextDirectory.Path);
    }

    private void NavigateBack(string s)
    {
        ForwardHistory.Push(CurrentDirectory);

        FileSystemEntry previousDirectory = BackHistory.Pop();
        Navigate(previousDirectory.Path);
    }

    private void DeleteFile(string s)
    {
        MessageBoxResult result = MessageBox.Show($"Delete {SelectedFile.Name}?", "File deletion", MessageBoxButton.YesNo, MessageBoxImage.Warning);

        if (result == MessageBoxResult.Yes)
        {
            Client.Owner.SendPacket(new DeleteFileRequestPacket(SelectedFile.Path));
        }
    }
}
