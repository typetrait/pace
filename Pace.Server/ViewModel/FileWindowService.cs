using Pace.Server.Network;
using Pace.Server.View;

namespace Pace.Server.ViewModel
{
    public class FileWindowService
    {
        public void ShowWindow(PaceServer server)
        {
            var window = new FileExplorerWindow(server);
            window.Show();
        }
    }
}