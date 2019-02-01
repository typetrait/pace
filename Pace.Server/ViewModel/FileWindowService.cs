using Pace.Server.Model;
using Pace.Server.Network;
using Pace.Server.View;

namespace Pace.Server.ViewModel
{
    public class FileWindowService
    {
        public void ShowWindow(PaceServer server, ClientInfo client)
        {
            var window = new FileExplorerWindow(server, client);
            window.Show();
        }
    }
}