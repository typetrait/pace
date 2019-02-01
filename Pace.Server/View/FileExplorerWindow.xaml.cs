using Pace.Server.Model;
using Pace.Server.Network;
using Pace.Server.ViewModel;
using System.Windows;

namespace Pace.Server.View
{
    /// <summary>
    /// Interaction logic for FileExplorerWindow.xaml
    /// </summary>
    public partial class FileExplorerWindow : Window
    {
        public FileExplorerWindow(PaceServer server, ClientInfo client)
        {
            InitializeComponent();
            DataContext = new FileExplorerViewModel(server, client);
        }
    }
}