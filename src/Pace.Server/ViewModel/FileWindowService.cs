using Pace.Server.Model;
using Pace.Server.Network;

namespace Pace.Server.ViewModel;

public class FileWindowService
{
    public void ShowWindow(PaceServer server, ClientInfo client)
    {
        //var window = new FileExplorerWindow(server, client);

        //// TODO: Probably not an optimal place to put this in, remember to handle this somewhere else
        //server.ClientDisconnected += (sender, args) =>
        //{
        //    if (args.Client == client.Owner)
        //    {
        //        Dispatcher.UIThread.InvokeAsync(() =>
        //        {
        //            window.Close();
        //        });
        //    }
        //};

        //window.Show();
    }
}