using Pace.Server.Network;
using Pace.Server.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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