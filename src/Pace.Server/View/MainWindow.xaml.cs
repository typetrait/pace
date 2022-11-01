using Pace.Server.ViewModel;
using System.Windows;

namespace Pace.Server;

public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
}
